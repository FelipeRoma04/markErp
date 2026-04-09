using System;
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
            dtpPayment.Value = DateTime.Now;
            
            // Task 22: Wire up auto-load event for Employee ID
            txtEmployeeId.KeyDown += TxtEmployeeId_KeyDown;
            
            ApplyPermissions();
        }

        // Task 22: Auto-load salary from active contract
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
                            MessageBox.Show($"Salario base cargado: {baseSalary:C}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("No se encontró contrato activo para este empleado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al cargar salario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    ValidationHelper.ShowValidationError("Salario bruto debe ser un número válido.");
                    return;
                }

                payCtrl.GrossPay = gross;
                var breakdown = payCtrl.Breakdown;
                
                txtDeductions.Text = payCtrl.Deductions.ToString("0.00");
                txtNet.Text = payCtrl.NetPay.ToString("0.00");
                
                // Task 23: Show detailed Colombian deductions breakdown
                string deductionsDetail = $@"DESGLOSE DE DEDUCCIONES COLOMBIANAS:

Salario Base: {gross:C}
────────────────────────────────────
Salud (empleado 4%):           {breakdown.SaludEmpleado:C}
Pensión (empleado 4%):         {breakdown.PensionEmpleado:C}
Fondo Solidaridad (1%):        {breakdown.FondoSolidaridad:C}
────────────────────────────────────
TOTAL DEDUCCIONES:             {payCtrl.Deductions:C}
────────────────────────────────────
SALARIO NETO:                  {payCtrl.NetPay:C}

APORTE PATRONAL (informativo):
Parafiscales (9%):             {breakdown.Parafiscales:C}
Cesantías (8.33%):             {breakdown.Cesantias:C}
Prima Servicios (8.33%):       {breakdown.PrimaServicios:C}
Vacaciones (4.17%):            {breakdown.Vacaciones:C}";
                
                MessageBox.Show(deductionsDetail, "Desglose Detallado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch { ValidationHelper.ShowError("Error en cálculo de nómina"); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!canCreate)
            {
                PermissionHelper.DenyAccess(PermissionHelper.Feature.Payroll);
                return;
            }

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
                    
                    // Task 25: Offer to generate payroll slip PDF
                    DialogResult dr = MessageBox.Show(
                        "¿Deseas generar el comprobante de nómina en PDF?",
                        "Comprobante de Nómina",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);
                    
                    if (dr == DialogResult.Yes)
                    {
                        GeneratePayrollSlipPDF(empId);
                    }
                    
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

        // Task 25: Generate payroll slip PDF
        private void GeneratePayrollSlipPDF(int employeeId)
        {
            try
            {
                // Get the latest payroll log entry for this employee
                payrollModel payrollMdl = new payrollModel();
                // Query the latest payroll entry
                var conn = new Model.conexionModel();
                string query = $"SELECT TOP 1 Id FROM Payroll_Log WHERE EmployeeId = {employeeId} ORDER BY Id DESC";
                var dt = conn.ejecutarConsulta(query);
                
                if (dt == null || dt.Rows.Count == 0)
                {
                    MessageBox.Show("No se encontró el registro de nómina.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int payrollLogId = Convert.ToInt32(dt.Rows[0]["Id"]);
                var payslipData = PayrollSlipPDF.LoadPayslipData(payrollLogId);
                
                if (payslipData == null)
                {
                    MessageBox.Show("Error cargando datos de nómina.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (PayrollSlipPDF.GeneratePayrollSlipPDF(payrollLogId, payslipData))
                {
                    MessageBox.Show("Comprobante de nómina generado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error generando el comprobante.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void ApplyPermissions()
        {
            canCreate = PermissionHelper.HasPermission(PermissionHelper.Feature.Payroll, PermissionHelper.Action.Create);
            btnSave.Enabled = canCreate;
            btnCalc.Enabled = PermissionHelper.HasPermission(PermissionHelper.Feature.Payroll, PermissionHelper.Action.View);
        }
    }
}
