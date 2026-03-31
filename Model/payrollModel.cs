using System;
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
            string query = $"INSERT INTO Payroll_Log (EmployeeId, PayPeriodStart, PayPeriodEnd, GrossPay, Deductions, NetPay, PaymentDate) VALUES ({employeeId}, '{periodStart.ToString("yyyy-MM-dd")}', '{periodEnd.ToString("yyyy-MM-dd")}', {grossPay}, {deductions}, {netPay}, '{paymentDate.ToString("yyyy-MM-dd")}')";
            return conexion.ejecutarSinRetornoDatos(query);
        }
    }
}
