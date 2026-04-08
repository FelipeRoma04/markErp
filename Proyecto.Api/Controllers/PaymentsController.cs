using Microsoft.AspNetCore.Mvc;
using Proyecto.Api.Data;
using Proyecto.Api.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Proyecto.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly DbService _db;
        public PaymentsController(DbService db) => _db = db;

        // POST /api/payments
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentRequest req)
        {
            if (req.Amount <= 0) return BadRequest("Amount debe ser mayor a cero.");

            int invoiceId = req.InvoiceId;
            if (invoiceId <= 0 && req.QuoteId.HasValue)
            {
                invoiceId = await EnsureInvoiceForQuote(req.QuoteId.Value);
            }
            if (invoiceId <= 0) return BadRequest("InvoiceId requerido o no se pudo generar desde la cotizaciÃ³n.");

            var inv = await _db.QueryAsync("SELECT Id, ClientId, Total FROM Invoices WHERE Id=@id",
                new Dictionary<string, object> { ["@id"] = invoiceId });
            if (inv.Rows.Count == 0) return NotFound(new { error = $"Factura {invoiceId} no existe." });

            DateTime payDate = req.PaymentDate ?? DateTime.Now;
            bool ok = await _db.ExecuteAsync(
                "INSERT INTO Payments (InvoiceId, PaymentDate, Amount, Method) VALUES (@invId, @date, @amount, @method)",
                new Dictionary<string, object>
                {
                    ["@invId"] = invoiceId,
                    ["@date"] = payDate,
                    ["@amount"] = req.Amount,
                    ["@method"] = string.IsNullOrWhiteSpace(req.Method) ? "Transferencia" : req.Method
                });
            if (!ok) return BadRequest(new { error = "No se pudo registrar el pago." });

            var status = await UpdateInvoiceStatus(invoiceId);
            await AuditHelper.LogAsync(_db, req.User ?? "api", "PAYMENT", "Payments", invoiceId.ToString(), $"Pago {req.Amount} {req.Method}");
            return Ok(new
            {
                message = "Pago registrado",
                invoiceId,
                status.InvoiceStatus,
                status.Paid,
                status.Pending
            });
        }

        // GET /api/payments/client/{clientId}
        [HttpGet("client/{clientId:int}")]
        public async Task<IActionResult> GetClientReceivables(int clientId)
        {
            const string sql = @"
                WITH pay AS (
                    SELECT InvoiceId, SUM(Amount) AS Paid
                    FROM Payments
                    GROUP BY InvoiceId
                )
                SELECT i.Id, i.ClientId, i.DueDate, i.Total, ISNULL(p.Paid,0) AS Paid,
                       i.Total - ISNULL(p.Paid,0) AS Pending,
                       CASE WHEN i.DueDate < GETDATE() AND (i.Total - ISNULL(p.Paid,0)) > 0 THEN 1 ELSE 0 END AS IsOverdue,
                       i.PaymentStatus
                FROM Invoices i
                LEFT JOIN pay p ON p.InvoiceId = i.Id
                WHERE i.ClientId = @clientId
                ORDER BY i.DueDate ASC;";

            var dt = await _db.QueryAsync(sql, new Dictionary<string, object> { ["@clientId"] = clientId });
            decimal pendingTotal = 0;
            decimal overdue = 0;
            foreach (DataRow row in dt.Rows)
            {
                pendingTotal += Convert.ToDecimal(row["Pending"]);
                if (Convert.ToInt32(row["IsOverdue"]) == 1)
                    overdue += Convert.ToDecimal(row["Pending"]);
            }

            return Ok(new
            {
                clientId,
                pendingTotal,
                overdueTotal = overdue,
                invoices = DbService.ToList(dt)
            });
        }

        private async Task<int> EnsureInvoiceForQuote(int quoteId)
        {
            // If there is already an invoice for this quote, reuse it
            var existing = await _db.QueryAsync("SELECT TOP 1 Id FROM Invoices WHERE QuoteId=@qId ORDER BY Id DESC",
                new Dictionary<string, object> { ["@qId"] = quoteId });
            if (existing.Rows.Count > 0)
                return Convert.ToInt32(existing.Rows[0]["Id"]);

            // Pull quote totals to generate an invoice
            var quote = await _db.QueryAsync("SELECT ClientId, TotalAmount FROM Quotes WHERE Id=@qId",
                new Dictionary<string, object> { ["@qId"] = quoteId });
            if (quote.Rows.Count == 0) return 0;

            int clientId = Convert.ToInt32(quote.Rows[0]["ClientId"]);
            decimal total = Convert.ToDecimal(quote.Rows[0]["TotalAmount"]);

            // Create invoice with totals from quote (assume TotalAmount ya incluye impuestos)
            var dt = await _db.QueryAsync(
                "DECLARE @newId INT; " +
                "INSERT INTO Invoices (QuoteId, ClientId, IssueDate, DueDate, Subtotal, TotalTax, Total, PaymentStatus) " +
                "VALUES (@qId, @cId, GETDATE(), DATEADD(DAY,30,GETDATE()), @subtotal, 0, @total, 'Por Cobrar'); " +
                "SET @newId = SCOPE_IDENTITY(); SELECT @newId AS NewId;",
                new Dictionary<string, object>
                {
                    ["@qId"] = quoteId,
                    ["@cId"] = clientId,
                    ["@subtotal"] = total,
                    ["@total"] = total
                });

            if (dt.Rows.Count == 0) return 0;
            return Convert.ToInt32(dt.Rows[0]["NewId"]);
        }

        private async Task<(string InvoiceStatus, decimal Paid, decimal Pending)> UpdateInvoiceStatus(int invoiceId)
        {
            const string sql = @"
                WITH pay AS (SELECT InvoiceId, SUM(Amount) AS Paid FROM Payments GROUP BY InvoiceId)
                SELECT i.Total, ISNULL(p.Paid,0) AS Paid
                FROM Invoices i
                LEFT JOIN pay p ON p.InvoiceId = i.Id
                WHERE i.Id = @id";

            var dt = await _db.QueryAsync(sql, new Dictionary<string, object> { ["@id"] = invoiceId });
            if (dt.Rows.Count == 0) return ("Por Cobrar", 0, 0);

            decimal total = Convert.ToDecimal(dt.Rows[0]["Total"]);
            decimal paid = Convert.ToDecimal(dt.Rows[0]["Paid"]);
            decimal pending = Math.Max(0, total - paid);

            string newStatus;
            if (pending <= 0.01m) newStatus = "Pagada Total";
            else if (paid > 0) newStatus = "Pagada Parcial";
            else newStatus = "Por Cobrar";

            await _db.ExecuteAsync(
                "UPDATE Invoices SET PaymentStatus=@st WHERE Id=@id",
                new Dictionary<string, object> { ["@st"] = newStatus, ["@id"] = invoiceId });

            return (newStatus, paid, pending);
        }

        public class CreatePaymentRequest
        {
            public int InvoiceId { get; set; }
            public int? QuoteId { get; set; }
            public decimal Amount { get; set; }
            public string Method { get; set; }
            public DateTime? PaymentDate { get; set; }
            public string User { get; set; }
        }
    }
}
