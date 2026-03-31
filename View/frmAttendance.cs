using System;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmAttendance : Form
    {
        hrControler hrCtrl;

        public frmAttendance()
        {
            InitializeComponent();
            hrCtrl = new hrControler();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.IsValidInteger(txtEmployeeId.Text, out int empId))
            {
                ValidationHelper.ShowValidationError("Employee ID debe ser numérico.");
                return;
            }
            if (!ValidationHelper.IsValidDecimal(txtHours.Text, out decimal hours))
            {
                ValidationHelper.ShowValidationError("Horas trabajadas inválidas.");
                return;
            }
            if (!ValidationHelper.IsValidDecimal(txtOvertime.Text, out decimal overtime))
            {
                ValidationHelper.ShowValidationError("Horas extra inválidas.");
                return;
            }

            hrCtrl.EmployeeId = empId;
            hrCtrl.WorkDate = dtpWorkDate.Value;
            hrCtrl.HoursWorked = hours;
            hrCtrl.OvertimeHours = overtime;
            hrCtrl.IsAbsent = chkAbsent.Checked;
            hrCtrl.Notes = txtNotes.Text;

            if (hrCtrl.AddAttendance())
            {
                ValidationHelper.ShowSuccess("Attendance saved successfully!");
            }
            else
            {
                ValidationHelper.ShowError("Error saving attendance.");
            }
        }
    }
}
