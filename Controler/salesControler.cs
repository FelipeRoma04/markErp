using System;
using Proyecto.Model;
using System.Data;

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
            bool invoiceCreated = model.InsertInvoice(QuoteId, ClientId, IssueDate, DueDate, Subtotal, TotalTax, Total);
            
            // Task 28: Auto-generate accounting entry when invoice is created
            if (invoiceCreated)
            {
                AccountingEntryLogger.LogInvoiceEntry(InvoiceId, ClientId, Subtotal, TotalTax, Total);
            }
            
            return invoiceCreated;
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
            
            // Task 29: Auto-generate accounting entry when payment is received
            if (ok)
            {
                // Get payment ID (would need to enhance model to return it)
                AccountingEntryLogger.LogPaymentEntry(targetInvoiceId, 0, PaymentAmount, PaymentMethod);
            }
            
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

        // Task 30: Expose payment history query
        public DataTable GetPaymentHistory(int invoiceId)
        {
            salesModel model = new salesModel();
            return model.GetPaymentHistory(invoiceId);
        }

        // Task 30: Expose invoice total query
        public decimal GetInvoiceTotal(int invoiceId)
        {
            salesModel model = new salesModel();
            return model.GetInvoiceTotal(invoiceId);
        }

        // Task 30: Expose total paid query
        public decimal GetInvoiceTotalPaid(int invoiceId)
        {
            salesModel model = new salesModel();
            return model.GetInvoiceTotalPaid(invoiceId);
        }
    }
}
