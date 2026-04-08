using System;
using System.Collections.Generic;
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
    }
}
