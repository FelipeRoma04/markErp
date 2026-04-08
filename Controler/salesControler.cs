using System;
using Proyecto.Model;

namespace Proyecto.Controler
{
    internal class salesControler
    {
        public int ClientId { get; set; }
        public int QuoteId { get; set; }
        public int InvoiceId { get; set; }
        
        // Quote properties
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime DueDate { get; set; }
        
        // Financial properties
        public decimal Subtotal { get; set; }
        public decimal TotalTax 
        {
            get { return Subtotal * 0.19m; } // IVA 19% automatico default
        }
        public decimal Total 
        {
            get { return Subtotal + TotalTax; }
        }

        // Payment properties
        public decimal PaymentAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentResultMessage { get; private set; }
        
        public bool CreateQuote()
        {
            salesModel model = new salesModel();
            return model.InsertQuote(ClientId, IssueDate, ExpirationDate, Total);
        }

        public bool CreateInvoice()
        {
            salesModel model = new salesModel();
            return model.InsertInvoice(QuoteId, ClientId, IssueDate, DueDate, Subtotal, TotalTax, Total);
        }

        public bool ApplyPayment()
        {
            salesModel model = new salesModel();
            int targetInvoiceId = InvoiceId;

            // If invoice does not exist, allow paying the quote by converting it
            if (targetInvoiceId <= 0 && QuoteId > 0)
            {
                targetInvoiceId = model.EnsureInvoiceForQuote(QuoteId, ClientId);
            }
            else if (!model.InvoiceExists(targetInvoiceId) && model.QuoteExists(targetInvoiceId))
            {
                // User typed a QuoteId in the invoice box
                targetInvoiceId = model.EnsureInvoiceForQuote(targetInvoiceId, ClientId);
            }

            if (targetInvoiceId <= 0)
            {
                PaymentResultMessage = "No se encontro la factura ni la cotizacion indicada.";
                return false;
            }

            bool ok = model.InsertPayment(targetInvoiceId, DateTime.Now, PaymentAmount, PaymentMethod);
            InvoiceId = targetInvoiceId; // expose back to caller
            PaymentResultMessage = ok ? "Pago registrado y estado de factura actualizado." : "No se pudo registrar el pago.";
            return ok;
        }

        public System.Data.DataTable GetClientDebt(int clientId)
        {
            salesModel model = new salesModel();
            return model.GetClientDebt(clientId);
        }

        public decimal GetInvoicePending(int invoiceId)
        {
            salesModel model = new salesModel();
            return model.GetInvoicePending(invoiceId);
        }
    }
}
