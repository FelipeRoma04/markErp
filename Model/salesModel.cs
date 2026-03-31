using System;

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
            string q = $"INSERT INTO Quotes (ClientId, IssueDate, ExpirationDate, TotalAmount, Status) VALUES ({clientId}, '{issueDate:yyyy-MM-dd}', '{expireDate:yyyy-MM-dd}', {total}, 'Pendiente')";
            return conexion.ejecutarSinRetornoDatos(q);
        }

        public bool InsertInvoice(int quoteId, int clientId, DateTime issueDate, DateTime dueDate, decimal subtotal, decimal totalTax, decimal total)
        {
            string q = $"INSERT INTO Invoices (QuoteId, ClientId, IssueDate, DueDate, Subtotal, TotalTax, Total, PaymentStatus) VALUES ({quoteId}, {clientId}, '{issueDate:yyyy-MM-dd}', '{dueDate:yyyy-MM-dd}', {subtotal}, {totalTax}, {total}, 'Por Cobrar')";
            return conexion.ejecutarSinRetornoDatos(q);
        }
        
        public bool InsertPayment(int invoiceId, DateTime paymentDate, decimal amount, string method)
        {
            string q = $"INSERT INTO Payments (InvoiceId, PaymentDate, Amount, Method) VALUES ({invoiceId}, '{paymentDate:yyyy-MM-dd}', {amount}, '{method}')";
            return conexion.ejecutarSinRetornoDatos(q);
        }
    }
}
