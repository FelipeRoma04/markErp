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

        // Task 22: Auto-load salary from active contract
        public bool LoadSalaryFromContract(int employeeId, out decimal baseSalary)
        {
            baseSalary = 0;
            payrollModel model = new payrollModel();
            baseSalary = model.GetActiveContractSalary(employeeId);
            return baseSalary > 0;
        }
    }

    internal class PayrollBreakdown
    {
        // Task 23: Colombian payroll deductions with Fondo Solidaridad
        private static readonly decimal SMLV_2024 = 4_700_000m;  // Colombian Minimum Wage 2024
        private static readonly decimal SMLV_THRESHOLD = 4m;     // 4 SMLV threshold for Fondo Solidaridad

        public decimal SaludEmpleado { get; set; }      // Salud empleado: 4%
        public decimal PensionEmpleado { get; set; }     // Pensión empleado: 4%
        public decimal FondoSolidaridad { get; set; }    // Fondo Solidaridad: 1% if salary > 4 SMLV
        public decimal Parafiscales { get; set; }
        public decimal Cesantias { get; set; }
        public decimal PrimaServicios { get; set; }
        public decimal Vacaciones { get; set; }
        public decimal TotalDeductions { get; set; }

        public static PayrollBreakdown Calculate(decimal gross)
        {
            // Employee deductions
            decimal saludEmpleado = Math.Round(gross * 0.04m, 2);      // 4%
            decimal pensionEmpleado = Math.Round(gross * 0.04m, 2);    // 4%
            
            // Fondo Solidaridad: 1% if salary > 4 SMLV
            decimal fondoSolidaridad = 0;
            if (gross > (SMLV_2024 * SMLV_THRESHOLD))
            {
                fondoSolidaridad = Math.Round(gross * 0.01m, 2);       // 1%
            }

            // Employer contributions (informational only, not deducted from employee)
            decimal parafiscales = Math.Round(gross * 0.09m, 2);
            decimal cesantias = Math.Round(gross * 0.0833m, 2);
            decimal prima = Math.Round(gross * 0.0833m, 2);
            decimal vacaciones = Math.Round(gross * 0.0417m, 2);

            return new PayrollBreakdown
            {
                SaludEmpleado = saludEmpleado,
                PensionEmpleado = pensionEmpleado,
                FondoSolidaridad = fondoSolidaridad,
                Parafiscales = parafiscales,
                Cesantias = cesantias,
                PrimaServicios = prima,
                Vacaciones = vacaciones,
                TotalDeductions = saludEmpleado + pensionEmpleado + fondoSolidaridad + parafiscales + cesantias + prima + vacaciones
            };
        }

        public string GetDeductionsSummary()
        {
            return $@"DEDUCCIONES POR NÓMINA - Salario Base: {(0.04m):C}
─────────────────────────────────────────
Salud (Empleado 4%):           {SaludEmpleado:C}
Pensión (Empleado 4%):         {PensionEmpleado:C}
Fondo Solidaridad (1%):        {FondoSolidaridad:C}
─────────────────────────────────────────
SUB-TOTAL DEDUCCIONES:         {(SaludEmpleado + PensionEmpleado + FondoSolidaridad):C}
─────────────────────────────────────────
Contribuciones Patronales (Info):
  Parafiscales (9%):           {Parafiscales:C}
  + Cesantías (8.33%):         {Cesantias:C}
  + Prima Servicios (8.33%):   {PrimaServicios:C}
  + Vacaciones (4.17%):        {Vacaciones:C}";
        }
    }
}
