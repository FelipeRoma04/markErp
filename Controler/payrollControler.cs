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
        public DateTime PaymentDate { get; set; }

        public PayrollBreakdown Breakdown => PayrollBreakdown.Calculate(GrossPay);
        public decimal Deductions => Breakdown.TotalDeductions;
        public decimal NetPay => GrossPay - Deductions;

        public bool ProcessPayroll()
        {
            payrollModel model = new payrollModel();
            return model.InsertPayrollLog(EmployeeId, PayPeriodStart, PayPeriodEnd, GrossPay, Deductions, NetPay, PaymentDate);
        }
    }

    internal class PayrollBreakdown
    {
        public decimal Salud { get; set; }
        public decimal Pension { get; set; }
        public decimal Parafiscales { get; set; }
        public decimal Cesantias { get; set; }
        public decimal PrimaServicios { get; set; }
        public decimal Vacaciones { get; set; }
        public decimal TotalDeductions { get; set; }

        public static PayrollBreakdown Calculate(decimal gross)
        {
            decimal salud = Math.Round(gross * 0.04m, 2);
            decimal pension = Math.Round(gross * 0.04m, 2);
            decimal parafiscales = Math.Round(gross * 0.09m, 2);
            decimal cesantias = Math.Round(gross * 0.0833m, 2);
            decimal prima = Math.Round(gross * 0.0833m, 2);
            decimal vacaciones = Math.Round(gross * 0.0417m, 2);

            return new PayrollBreakdown
            {
                Salud = salud,
                Pension = pension,
                Parafiscales = parafiscales,
                Cesantias = cesantias,
                PrimaServicios = prima,
                Vacaciones = vacaciones,
                TotalDeductions = salud + pension + parafiscales + cesantias + prima + vacaciones
            };
        }
    }
}
