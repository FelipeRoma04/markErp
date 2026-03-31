using System;
using Proyecto.Model;

namespace Proyecto.Controler
{
    internal class hrControler
    {
        public int EmployeeId { get; set; }
        public string JobTitle { get; set; }
        public DateTime StartDate { get; set; }
        public decimal BaseSalary { get; set; }
        public string ContractType { get; set; }

        public DateTime WorkDate { get; set; }
        public decimal HoursWorked { get; set; }
        public decimal OvertimeHours { get; set; }
        public bool IsAbsent { get; set; }
        public string Notes { get; set; }

        public bool AddContract()
        {
            hrModel model = new hrModel();
            return model.InsertContract(EmployeeId, JobTitle, StartDate, BaseSalary, ContractType);
        }

        public bool AddAttendance()
        {
            hrModel model = new hrModel();
            return model.InsertAttendance(EmployeeId, WorkDate, HoursWorked, OvertimeHours, IsAbsent, Notes);
        }
    }
}
