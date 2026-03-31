using System;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmPayroll : Form
    {
        payrollControler payCtrl;

        public frmPayroll()
        {
            InitializeComponent();
            payCtrl = new payrollControler();
            dtpPayment.Value = DateTime.Now;
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            try 
            {
                if (!ValidationHelper.IsValidDecimal(txtGross.Text, out decimal gross))
                {
                    ValidationHelper.ShowValidationError("Salario bruto debe ser un número válido.");
                    return;
                }

                payCtrl.GrossPay = gross;
                txtDeductions.Text = payCtrl.Deductions.ToString("0.00");
                txtNet.Text = payCtrl.NetPay.ToString("0.00");
            }
            catch { ValidationHelper.ShowError("Error en cálculo de nómina"); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try 
            {
                // Validations
                if (!ValidationHelper.IsNotEmpty(txtEmployeeId.Text) ||
                    !ValidationHelper.IsValidInteger(txtEmployeeId.Text, out int empId) ||
                    !ValidationHelper.IsValidDecimal(txtGross.Text, out decimal gross))
                {
                    ValidationHelper.ShowValidationError("Verifique todos los campos requeridos.");
                    return;
                }

                payCtrl.EmployeeId = empId;
                payCtrl.PayPeriodStart = dtpStart.Value;
                payCtrl.PayPeriodEnd = dtpEnd.Value;
                payCtrl.GrossPay = gross;
                payCtrl.PaymentDate = dtpPayment.Value;

                if (payCtrl.ProcessPayroll())
                {
                    MessageBox.Show("Nómina procesada exitosamente!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Error insertando nómina.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } 
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Ingreso inválido: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtEmployeeId.Clear();
            txtGross.Clear();
            txtDeductions.Clear();
            txtNet.Clear();
            dtpStart.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now;
            dtpPayment.Value = DateTime.Now;
        }
    }
}
