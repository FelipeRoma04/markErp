using System;
using System.Collections.Generic;
using System.Data;

namespace Proyecto.Model
{
    internal class salesModel
    {
        conexionModel conexion;

        public salesModel()
        {
            conexion = new conexionModel();
        }

        public bool InsertQuote(int clientId, DateTime issueDate, DateTime expireDate, decimal total)
        {
            const string q = "INSERT INTO Quotes (ClientId, IssueDate, ExpirationDate, TotalAmount, Status) VALUES (@clientId, @issue, @exp, @total, 'Pendiente'); SELECT SCOPE_IDENTITY();";
            var parameters = new Dictionary<string, object>
            {
                ["@clientId"] = clientId,
                ["@issue"] = issueDate,
                ["@exp"] = expireDate,
                ["@total"] = total
            };
            var dt = conexion.ejecutarConsultaParametrizada(q, parameters);
            return dt != null && dt.Rows.Count > 0;
        }

        public bool InsertInvoice(int quoteId, int clientId, DateTime issueDate, DateTime dueDate, decimal subtotal, decimal totalTax, decimal total)
        {
            const string q = "INSERT INTO Invoices (QuoteId, ClientId, IssueDate, DueDate, Subtotal, TotalTax, Total, PaymentStatus) VALUES (@quoteId, @clientId, @issue, @due, @sub, @tax, @total, 'Por Cobrar'); SELECT SCOPE_IDENTITY();";
            var parameters = new Dictionary<string, object>
            {
                ["@quoteId"] = quoteId,
                ["@clientId"] = clientId,
                ["@issue"] = issueDate,
                ["@due"] = dueDate,
                ["@sub"] = subtotal,
                ["@tax"] = totalTax,
                ["@total"] = total
            };
            var dt = conexion.ejecutarConsultaParametrizada(q, parameters);
            return dt != null && dt.Rows.Count > 0;
        }
        
        public bool InsertPayment(int invoiceId, DateTime paymentDate, decimal amount, string method)
        {
            const string q = "INSERT INTO Payments (InvoiceId, PaymentDate, Amount, Method) VALUES (@invoiceId, @date, @amount, @method); SELECT SCOPE_IDENTITY();";
            var parameters = new Dictionary<string, object>
            {
                ["@invoiceId"] = invoiceId,
                ["@date"] = paymentDate,
                ["@amount"] = amount,
                ["@method"] = method
            };

            var dt = conexion.ejecutarConsultaParametrizada(q, parameters);
            bool ok = dt != null && dt.Rows.Count > 0;
            if (ok)
            {
                UpdateInvoiceStatus(invoiceId);
            }
            return ok;
        }

        public bool InvoiceExists(int invoiceId)
        {
            var dt = conexion.ejecutarConsulta($"SELECT Id FROM Invoices WHERE Id = {invoiceId}");
            return dt != null && dt.Rows.Count > 0;
        }

        public bool QuoteExists(int quoteId)
        {
            var dt = conexion.ejecutarConsulta($"SELECT Id FROM Quotes WHERE Id = {quoteId}");
            return dt != null && dt.Rows.Count > 0;
        }

        public int EnsureInvoiceForQuote(int quoteId, int clientId)
        {
            var existing = conexion.ejecutarConsulta($"SELECT TOP 1 Id FROM Invoices WHERE QuoteId = {quoteId} ORDER BY Id DESC");
            if (existing != null && existing.Rows.Count > 0)
                return Convert.ToInt32(existing.Rows[0]["Id"]);

            var quote = conexion.ejecutarConsulta($"SELECT TotalAmount FROM Quotes WHERE Id = {quoteId}");
            if (quote == null || quote.Rows.Count == 0) return 0;

            decimal total = Convert.ToDecimal(quote.Rows[0]["TotalAmount"]);
            const string insert = "INSERT INTO Invoices (QuoteId, ClientId, IssueDate, DueDate, Subtotal, TotalTax, Total, PaymentStatus) " +
                                  "VALUES (@quoteId, @clientId, GETDATE(), DATEADD(DAY,30,GETDATE()), @sub, 0, @total, 'Por Cobrar'); " +
                                  "SELECT SCOPE_IDENTITY();";
            var parameters = new Dictionary<string, object>
            {
                ["@quoteId"] = quoteId,
                ["@clientId"] = clientId,
                ["@sub"] = total,
                ["@total"] = total
            };
            var dt = conexion.ejecutarConsultaParametrizada(insert, parameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }
            return 0;
        }

        public DataTable GetClientDebt(int clientId)
        {
            const string sql = @"
                WITH pay AS (SELECT InvoiceId, SUM(Amount) AS Paid FROM Payments GROUP BY InvoiceId)
                SELECT i.Id, i.ClientId, i.DueDate, i.Total, ISNULL(p.Paid,0) AS Paid,
                       i.Total - ISNULL(p.Paid,0) AS Pending,
                       CASE WHEN i.DueDate < GETDATE() AND (i.Total - ISNULL(p.Paid,0)) > 0 THEN 1 ELSE 0 END AS IsOverdue,
                       i.PaymentStatus
                FROM Invoices i
                LEFT JOIN pay p ON p.InvoiceId = i.Id
                WHERE i.ClientId = @clientId
                ORDER BY i.DueDate ASC";

            var parameters = new Dictionary<string, object> { ["@clientId"] = clientId };
            return conexion.ejecutarConsultaParametrizada(sql, parameters);
        }

        public decimal GetInvoicePending(int invoiceId)
        {
            const string sql = @"
                WITH pay AS (SELECT InvoiceId, SUM(Amount) AS Paid FROM Payments GROUP BY InvoiceId)
                SELECT i.Total - ISNULL(p.Paid,0) AS Pending
                FROM Invoices i
                LEFT JOIN pay p ON p.InvoiceId = i.Id
                WHERE i.Id = @id";

            var parameters = new Dictionary<string, object> { ["@id"] = invoiceId };
            var dt = conexion.ejecutarConsultaParametrizada(sql, parameters);
            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["Pending"] != DBNull.Value)
                return Convert.ToDecimal(dt.Rows[0]["Pending"]);
            return 0;
        }

        private void UpdateInvoiceStatus(int invoiceId)
        {
            var dt = conexion.ejecutarConsulta($@"
                SELECT i.Total, ISNULL(SUM(p.Amount),0) AS Paid
                FROM Invoices i
                LEFT JOIN Payments p ON p.InvoiceId = i.Id
                WHERE i.Id = {invoiceId}
                GROUP BY i.Total");

            if (dt == null || dt.Rows.Count == 0) return;

            decimal total = Convert.ToDecimal(dt.Rows[0]["Total"]);
            decimal paid = Convert.ToDecimal(dt.Rows[0]["Paid"]);
            decimal pending = Math.Max(0, total - paid);

            string status = pending <= 0.01m ? "Pagada Total" : paid > 0 ? "Pagada Parcial" : "Por Cobrar";
            var parameters = new Dictionary<string, object>
            {
                ["@st"] = status,
                ["@id"] = invoiceId
            };
            conexion.ejecutarComandoParametrizado("UPDATE Invoices SET PaymentStatus=@st WHERE Id=@id", parameters);
        }

        // Task 30: Get payment history for display
        public DataTable GetPaymentHistory(int invoiceId)
        {
            const string sql = @"
                SELECT PaymentDate, Amount, Method AS PaymentMethod
                FROM Payments
                WHERE InvoiceId = @invoiceId
                ORDER BY PaymentDate DESC";

            var parameters = new Dictionary<string, object> { ["@invoiceId"] = invoiceId };
            return conexion.ejecutarConsultaParametrizada(sql, parameters);
        }

        // Task 30: Get invoice total amount
        public decimal GetInvoiceTotal(int invoiceId)
        {
            const string sql = "SELECT Total FROM Invoices WHERE Id = @id";
            var parameters = new Dictionary<string, object> { ["@id"] = invoiceId };
            var dt = conexion.ejecutarConsultaParametrizada(sql, parameters);
            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["Total"] != DBNull.Value)
                return Convert.ToDecimal(dt.Rows[0]["Total"]);
            return 0;
        }

        // Task 30: Get total paid amount for invoice
        public decimal GetInvoiceTotalPaid(int invoiceId)
        {
            const string sql = @"
                SELECT ISNULL(SUM(Amount), 0) AS TotalPaid
                FROM Payments
                WHERE InvoiceId = @invoiceId";

            var parameters = new Dictionary<string, object> { ["@invoiceId"] = invoiceId };
            var dt = conexion.ejecutarConsultaParametrizada(sql, parameters);
            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["TotalPaid"] != DBNull.Value)
                return Convert.ToDecimal(dt.Rows[0]["TotalPaid"]);
            return 0;
        }
    }
}
