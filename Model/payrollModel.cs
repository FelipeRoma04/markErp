using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Proyecto.Model
{
    internal class payrollModel
    {
        conexionModel conexion;

        public payrollModel()
        {
            conexion = new conexionModel();
        }

        public bool InsertPayrollLog(int employeeId, DateTime periodStart, DateTime periodEnd, decimal grossPay, decimal deductions, decimal netPay, DateTime paymentDate)
        {
            const string query = "INSERT INTO Payroll_Log (EmployeeId, PayPeriodStart, PayPeriodEnd, GrossPay, Deductions, NetPay, PaymentDate) VALUES (@empId, @pStart, @pEnd, @gross, @ded, @net, @pay)";
            var parameters = new Dictionary<string, object>
            {
                ["@empId"] = employeeId,
                ["@pStart"] = periodStart,
                ["@pEnd"] = periodEnd,
                ["@gross"] = grossPay,
                ["@ded"] = deductions,
                ["@net"] = netPay,
                ["@pay"] = paymentDate
            };
            return conexion.ejecutarComandoParametrizado(query, parameters) > 0;
        }

        // Task 22: Get active contract salary for employee
        public decimal GetActiveContractSalary(int employeeId)
        {
            try
            {
                const string query = @"
                    SELECT TOP 1 BaseSalary FROM Contracts 
                    WHERE EmployeeId = @empId 
                    AND Status = 'Active'
                    AND (EndDate IS NULL OR EndDate >= GETDATE())
                    ORDER BY StartDate DESC";
                
                var parameters = new Dictionary<string, object>
                {
                    ["@empId"] = employeeId
                };

                DataTable dt = conexion.ejecutarConsultaParametrizada(query, parameters);
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    object salaryObj = dt.Rows[0]["BaseSalary"];
                    if (salaryObj != DBNull.Value)
                    {
                        return Convert.ToDecimal(salaryObj);
                    }
                }
                
                return 0;
            }
            catch
            {
                return 0;
            }
        }
    }
}
