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
            return model.InsertPayment(InvoiceId, DateTime.Now, PaymentAmount, PaymentMethod);
        }
    }
}
