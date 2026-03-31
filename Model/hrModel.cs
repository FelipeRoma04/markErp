using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Proyecto.Model
{
    internal class hrModel
    {
        conexionModel conexion;

        public hrModel()
        {
            conexion = new conexionModel();
        }

        public bool InsertContract(int employeeId, string jobTitle, DateTime startDate, decimal baseSalary, string contractType)
        {
            const string query = "INSERT INTO Contracts (EmployeeId, JobTitle, StartDate, BaseSalary, ContractType) VALUES (@employeeId, @jobTitle, @startDate, @baseSalary, @contractType)";
            var parameters = new Dictionary<string, object>
            {
                {"@employeeId", employeeId},
                {"@jobTitle", jobTitle},
                {"@startDate", startDate},
                {"@baseSalary", baseSalary},
                {"@contractType", contractType}
            };
            return conexion.ejecutarComandoParametrizado(query, parameters) > 0;
        }

        public bool InsertAttendance(int employeeId, DateTime workDate, decimal hoursWorked, decimal overtimeHours, bool isAbsent, string notes)
        {
            const string query = "INSERT INTO Attendance (EmployeeId, WorkDate, HoursWorked, OvertimeHours, IsAbsent, Notes) VALUES (@employeeId, @workDate, @hoursWorked, @overtimeHours, @isAbsent, @notes)";
            var parameters = new Dictionary<string, object>
            {
                {"@employeeId", employeeId},
                {"@workDate", workDate},
                {"@hoursWorked", hoursWorked},
                {"@overtimeHours", overtimeHours},
                {"@isAbsent", isAbsent},
                {"@notes", notes ?? string.Empty}
            };
            return conexion.ejecutarComandoParametrizado(query, parameters) > 0;
        }
    }
}
