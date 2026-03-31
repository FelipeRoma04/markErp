using System;
using Proyecto.Model;

namespace Proyecto.Controler
{
    internal class payrollControler
    {
        public int EmployeeId { get; set; }
        public DateTime PayPeriodStart { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public decimal GrossPay { get; set; }
        public decimal Deductions 
        { 
            get 
            { 
                // Deducciones de Ley (Health = 4%, Pension = 4%) = 8% Total Deductions
                return GrossPay * 0.08m; 
            } 
        }
        
        public decimal NetPay 
        { 
            get { return GrossPay - Deductions; } 
        }
        
        public DateTime PaymentDate { get; set; }

        public bool ProcessPayroll()
        {
            payrollModel model = new payrollModel();
            return model.InsertPayrollLog(EmployeeId, PayPeriodStart, PayPeriodEnd, GrossPay, Deductions, NetPay, PaymentDate);
        }
    }
}
