using System;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Controler;
using Proyecto.Model;

namespace Proyecto.View
{
    public partial class frmPayroll : Form
    {
        payrollControler payCtrl;
        bool canCreate;

        public frmPayroll()
        {
            InitializeComponent();
            payCtrl = new payrollControler();
            ApplyStyle();
            
            dtpPayment.Value = DateTime.Now;
            txtEmployeeId.KeyDown += TxtEmployeeId_KeyDown;
            
            ApplyPermissions();
        }

        private void ApplyStyle()
        {
            this.BackColor = UITheme.BgColor;
            pnlHeader.BackColor = UITheme.PrimaryColor;
            lblTitleHeader.ForeColor = Color.White;
            lblTitleHeader.Font = UITheme.FontHeader;

            pnlForm.BackColor = Color.White;
            pnlResults.BackColor = Color.FromArgb(236, 240, 241);

            // Labels
            UITheme.StyleLabel(lblEmployee);
            UITheme.StyleLabel(lblStart);
            UITheme.StyleLabel(lblEnd);
            UITheme.StyleLabel(lblGross);
            UITheme.StyleLabel(lblPay);
            UITheme.StyleLabel(lblDeduct);
            UITheme.StyleLabel(lblNet, true);

            // TextBoxes
            UITheme.StyleTextBox(txtEmployeeId);
            UITheme.StyleTextBox(txtGross);
            
            // Results styling
            txtDeductions.ForeColor = UITheme.TextSecondary;
            txtNet.ForeColor = UITheme.SuccessColor;

            // Buttons
            btnCalc.BackColor = UITheme.AccentColor;
            btnSave.BackColor = UITheme.PrimaryColor;
        }

        private void TxtEmployeeId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (int.TryParse(txtEmployeeId.Text, out int empId))
                {
                    try
                    {
                        if (payCtrl.LoadSalaryFromContract(empId, out decimal baseSalary))
                        {
                            txtGross.Text = baseSalary.ToString("0.00");
                            ValidationHelper.ShowSuccess($"Salario base cargado: {baseSalary:C}");
                        }
                        else
                        {
                            ValidationHelper.ShowValidationError("No se encontró contrato activo para este empleado.");
                        }
                    }
                    catch (Exception ex)
                    {
                        ValidationHelper.ShowError("Error al cargar salario: " + ex.Message);
                    }
                }
            }
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            try 
            {
                if (!ValidationHelper.IsValidDecimal(txtGross.Text, out decimal gross))
                {
                    ValidationHelper.ShowValidationError("El salario bruto debe ser un número válido.");
                    return;
                }

                payCtrl.GrossPay = gross;
                var breakdown = payCtrl.Breakdown;
                
                txtDeductions.Text = payCtrl.Deductions.ToString("C");
                txtNet.Text = payCtrl.NetPay.ToString("C");
                
                string deductionsDetail = $@"DESGLOSE DE DEDUCCIONES (COL):

Salario Base: {gross:C}
────────────────────────────
Salud (4%):          {breakdown.SaludEmpleado:C}
Pensión (4%):        {breakdown.PensionEmpleado:C}
Fondo Solid. (1%):   {breakdown.FondoSolidaridad:C}
────────────────────────────
TOTAL DEDUCCIÓN:     {payCtrl.Deductions:C}
────────────────────────────
SALARIO NETO:        {payCtrl.NetPay:C}

Aportes Patronales (Ref):
Parafiscales (9%):   {breakdown.Parafiscales:C}
Cesantías (8.33%):   {breakdown.Cesantias:C}";
                
                MessageBox.Show(deductionsDetail, "Detalle de Liquidación", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { ValidationHelper.ShowError("Error en cálculo de nómina."); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!canCreate)
            {
                PermissionHelper.DenyAccess(PermissionHelper.Feature.Payroll);
                return;
            }

            if (!ValidationHelper.IsValidInteger(txtEmployeeId.Text, out int empId) ||
                !ValidationHelper.IsValidDecimal(txtGross.Text, out decimal gross))
            {
                ValidationHelper.ShowValidationError("Verifique que el ID de empleado y el salario bruto sean correctos.");
                return;
            }

            try 
            {
                payCtrl.EmployeeId = empId;
                payCtrl.PayPeriodStart = dtpStart.Value;
                payCtrl.PayPeriodEnd = dtpEnd.Value;
                payCtrl.GrossPay = gross;
                payCtrl.PaymentDate = dtpPayment.Value;

                if (payCtrl.ProcessPayroll())
                {
                    ValidationHelper.ShowSuccess("Nómina procesada exitosamente.");
                    
                    DialogResult dr = MessageBox.Show(
                        "¿Desea generar el comprobante de nómina en PDF ahora?",
                        "Comprobante de Nómina",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    
                    if (dr == DialogResult.Yes)
                    {
                        GeneratePayrollSlip(empId);
                    }
                    
                    ClearFields();
                }
                else
                {
                    ValidationHelper.ShowError("No se pudo registrar la nómina en el sistema.");
                }
            } 
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error inesperado: " + ex.Message);
            }
        }

        private void GeneratePayrollSlip(int employeeId)
        {
            try
            {
                var conn = new Model.conexionModel();
                var dt = conn.ejecutarConsulta($"SELECT TOP 1 Id FROM Payroll_Log WHERE EmployeeId = {employeeId} ORDER BY Id DESC");
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    int logId = Convert.ToInt32(dt.Rows[0]["Id"]);
                    var data = PayrollSlipPDF.LoadPayslipData(logId);
                    
                    if (data != null && PayrollSlipPDF.GeneratePayrollSlipPDF(logId, data))
                    {
                        ValidationHelper.ShowSuccess("Comprobante PDF generado correctamente.");
                    }
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error al generar PDF: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtEmployeeId.Clear();
            txtGross.Text = "0.00";
            txtDeductions.Text = "0.00";
            txtNet.Text = "0.00";
            dtpStart.Value = DateTime.Now;
            dtpEnd.Value = DateTime.Now;
            dtpPayment.Value = DateTime.Now;
            txtEmployeeId.Focus();
        }

        private void ApplyPermissions()
        {
            canCreate = PermissionHelper.HasPermission(PermissionHelper.Feature.Payroll, PermissionHelper.Action.Create);
            btnSave.Enabled = canCreate;
            btnCalc.Enabled = PermissionHelper.HasPermission(PermissionHelper.Feature.Payroll, PermissionHelper.Action.View);
        }
    }
}
