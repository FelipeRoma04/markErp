using System;
using System.Drawing;
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
            ApplyStyle();
        }

        private void ApplyStyle()
        {
            this.BackColor = UITheme.BgColor;
            pnlHeader.BackColor = UITheme.PrimaryColor;
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = UITheme.FontHeader;

            pnlForm.BackColor = Color.White;

            // Labels
            UITheme.StyleLabel(lblEmployee);
            UITheme.StyleLabel(lblDate);
            UITheme.StyleLabel(lblHours);
            UITheme.StyleLabel(lblOvertime);
            UITheme.StyleLabel(lblNotes);

            // TextBoxes
            UITheme.StyleTextBox(txtEmployeeId);
            UITheme.StyleTextBox(txtHours);
            UITheme.StyleTextBox(txtOvertime);
            UITheme.StyleTextBox(txtNotes);

            // Button
            btnSave.BackColor = UITheme.PrimaryColor;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.IsValidInteger(txtEmployeeId.Text, out int empId))
            {
                ValidationHelper.ShowValidationError("El ID de Empleado debe ser un número válido.");
                return;
            }

            if (!ValidationHelper.IsValidDecimal(txtHours.Text, out decimal hours))
            {
                ValidationHelper.ShowValidationError("Las horas trabajadas deben ser un valor numérico.");
                return;
            }

            if (!ValidationHelper.IsValidDecimal(txtOvertime.Text, out decimal overtime))
            {
                ValidationHelper.ShowValidationError("Las horas extra deben ser un valor numérico.");
                return;
            }

            try
            {
                hrCtrl.EmployeeId = empId;
                hrCtrl.WorkDate = dtpWorkDate.Value;
                hrCtrl.HoursWorked = hours;
                hrCtrl.OvertimeHours = overtime;
                hrCtrl.IsAbsent = chkAbsent.Checked;
                hrCtrl.Notes = txtNotes.Text.Trim();

                if (hrCtrl.AddAttendance())
                {
                    ValidationHelper.ShowSuccess("Registro de asistencia guardado exitosamente.");
                    ClearFields();
                }
                else
                {
                    ValidationHelper.ShowError("No se pudo registrar la asistencia. Verifique el contrato del empleado.");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error inesperado: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtEmployeeId.Clear();
            txtHours.Text = "8";
            txtOvertime.Text = "0";
            txtNotes.Clear();
            chkAbsent.Checked = false;
            txtEmployeeId.Focus();
        }
    }
}
