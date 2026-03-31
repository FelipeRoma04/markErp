using System;
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
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.IsValidInteger(txtEmployeeId.Text, out int empId))
            {
                ValidationHelper.ShowValidationError("Employee ID debe ser numérico.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtJobTitle.Text))
            {
                ValidationHelper.ShowValidationError("Job Title es obligatorio.");
                return;
            }
            if (!ValidationHelper.IsValidDecimal(txtSalary.Text, out decimal salary))
            {
                ValidationHelper.ShowValidationError("Salario base inválido.");
                return;
            }
            if (!ValidationHelper.IsNotEmpty(txtContractType.Text))
            {
                ValidationHelper.ShowValidationError("Contract Type es obligatorio.");
                return;
            }

            hrCtrl.EmployeeId = empId;
            hrCtrl.JobTitle = txtJobTitle.Text;
            hrCtrl.BaseSalary = salary;
            hrCtrl.ContractType = txtContractType.Text;
            hrCtrl.StartDate = DateTime.Now;

            if (hrCtrl.AddContract())
            {
                ValidationHelper.ShowSuccess("Contract saved successfully!");
            }
            else
            {
                ValidationHelper.ShowError("Error saving contract.");
            }
        }
    }
}
