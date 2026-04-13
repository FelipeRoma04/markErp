using System;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmEmployeeContracts : Form
    {
        hrControler hrCtrl;

        public frmEmployeeContracts()
        {
            InitializeComponent();
            hrCtrl = new hrControler();
            ApplyStyle();
        }

        private void ApplyStyle()
        {
            this.BackColor = UITheme.BgColor;
            pnlHeader.BackColor = UITheme.PrimaryColor;
            lblTitleHeader.ForeColor = Color.White;
            lblTitleHeader.Font = UITheme.FontHeader;

            pnlForm.BackColor = Color.White;

            // Labels
            UITheme.StyleLabel(lblEmployeeId);
            UITheme.StyleLabel(lblJobTitle);
            UITheme.StyleLabel(lblSalary);
            UITheme.StyleLabel(lblType);

            // TextBoxes
            UITheme.StyleTextBox(txtEmployeeId);
            UITheme.StyleTextBox(txtJobTitle);
            UITheme.StyleTextBox(txtSalary);
            UITheme.StyleTextBox(txtContractType);

            // Button
            btnSave.BackColor = UITheme.SuccessColor;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.IsValidInteger(txtEmployeeId.Text, out int empId))
            {
                ValidationHelper.ShowValidationError("El ID de Empleado debe ser un número válido.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtJobTitle.Text))
            {
                ValidationHelper.ShowValidationError("El Cargo/Puesto es obligatorio.");
                return;
            }

            if (!ValidationHelper.IsValidDecimal(txtSalary.Text, out decimal salary))
            {
                ValidationHelper.ShowValidationError("El Salario Base debe ser un valor numérico válido.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtContractType.Text))
            {
                ValidationHelper.ShowValidationError("El Tipo de Contrato es obligatorio.");
                return;
            }

            try
            {
                hrCtrl.EmployeeId = empId;
                hrCtrl.JobTitle = txtJobTitle.Text.Trim();
                hrCtrl.BaseSalary = salary;
                hrCtrl.ContractType = txtContractType.Text.Trim();
                hrCtrl.StartDate = DateTime.Now;

                if (hrCtrl.AddContract())
                {
                    ValidationHelper.ShowSuccess("Contrato laboral registrado exitosamente.");
                    ClearFields();
                }
                else
                {
                    ValidationHelper.ShowError("No se pudo guardar el contrato. Verifique si el empleado ya tiene un contrato activo.");
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
            txtJobTitle.Clear();
            txtSalary.Text = "0.00";
            txtContractType.Clear();
            txtEmployeeId.Focus();
        }
    }
}
