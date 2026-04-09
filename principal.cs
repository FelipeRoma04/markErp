using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Proyecto.Controler;

namespace Proyecto
{
    public partial class principal : Form
    {
        private static readonly string LOG_FILE = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "debug.log");

        public principal()
        {
            InitializeComponent();
        }

        private void LogError(string formName, Exception ex)
        {
            try
            {
                string message = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Error opening {formName}: {ex.GetType().Name} - {ex.Message}\n{ex.StackTrace}\n";
                File.AppendAllText(LOG_FILE, message);
            }
            catch { /* ignore logging errors */ }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            ApplyButtonStyles();
            ApplyRoleUi();
            
            // FASE 4 - Task 39: Initialize automatic backup (admin only)
            BackupUtility.InitializeBackupSchedule(2, 0); // 2:00 AM daily
            
            await LoadKpisAsync();
        }

        private void ApplyButtonStyles()
        {
            // HR buttons - Blue
            var hrColor = System.Drawing.Color.FromArgb(52, 152, 219);
            departamentos.BackColor = hrColor;
            departamentos.ForeColor = System.Drawing.Color.White;
            empleados.BackColor = hrColor;
            empleados.ForeColor = System.Drawing.Color.White;
            contratos.BackColor = hrColor;
            contratos.ForeColor = System.Drawing.Color.White;
            btnPayroll.BackColor = hrColor;
            btnPayroll.ForeColor = System.Drawing.Color.White;
            btnAttendance.BackColor = hrColor;
            btnAttendance.ForeColor = System.Drawing.Color.White;

            // Sales buttons - Green
            var salesColor = System.Drawing.Color.FromArgb(46, 204, 113);
            btnClients.BackColor = salesColor;
            btnClients.ForeColor = System.Drawing.Color.White;
            btnQuotes.BackColor = salesColor;
            btnQuotes.ForeColor = System.Drawing.Color.White;
            btnInvoicing.BackColor = salesColor;
            btnInvoicing.ForeColor = System.Drawing.Color.White;
            btnPayments.BackColor = salesColor;
            btnPayments.ForeColor = System.Drawing.Color.White;

            // Inventory buttons - Orange
            var inventoryColor = System.Drawing.Color.FromArgb(230, 126, 34);
            btnProducts.BackColor = inventoryColor;
            btnProducts.ForeColor = System.Drawing.Color.White;
            btnAssets.BackColor = inventoryColor;
            btnAssets.ForeColor = System.Drawing.Color.White;

            // Accounting buttons - Purple
            var accountingColor = System.Drawing.Color.FromArgb(155, 89, 182);
            btnAccounting.BackColor = accountingColor;
            btnAccounting.ForeColor = System.Drawing.Color.White;
            btnAudit.BackColor = accountingColor;
            btnAudit.ForeColor = System.Drawing.Color.White;

            // Apply flat style to all buttons
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is Button btn)
                {
                    btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
                }
            }
        }

        private void ApplyRoleUi()
        {
            var role = Proyecto.Controler.UserSession.Role ?? "Usuario";
            this.Text = $"ERP Dashboard - {Proyecto.Controler.UserSession.Username} ({role})";

            // Disable / hide buttons per role
            departamentos.Enabled = Proyecto.Controler.PermissionHelper.HasFeatureAccess(Proyecto.Controler.PermissionHelper.Feature.Departments);
            empleados.Enabled = Proyecto.Controler.PermissionHelper.HasFeatureAccess(Proyecto.Controler.PermissionHelper.Feature.Employees);
            contratos.Enabled = Proyecto.Controler.PermissionHelper.HasFeatureAccess(Proyecto.Controler.PermissionHelper.Feature.Contracts);
            btnAttendance.Enabled = Proyecto.Controler.PermissionHelper.HasFeatureAccess(Proyecto.Controler.PermissionHelper.Feature.Attendance);
            btnPayroll.Enabled = Proyecto.Controler.PermissionHelper.HasFeatureAccess(Proyecto.Controler.PermissionHelper.Feature.Payroll);
            btnClients.Enabled = Proyecto.Controler.PermissionHelper.HasFeatureAccess(Proyecto.Controler.PermissionHelper.Feature.Clients);
            btnQuotes.Enabled = Proyecto.Controler.PermissionHelper.HasFeatureAccess(Proyecto.Controler.PermissionHelper.Feature.Quotes);
            btnInvoicing.Enabled = Proyecto.Controler.PermissionHelper.HasFeatureAccess(Proyecto.Controler.PermissionHelper.Feature.Invoicing);
            btnPayments.Enabled = Proyecto.Controler.PermissionHelper.HasFeatureAccess(Proyecto.Controler.PermissionHelper.Feature.Sales);
            btnProducts.Enabled = Proyecto.Controler.PermissionHelper.HasFeatureAccess(Proyecto.Controler.PermissionHelper.Feature.Products);
            btnAssets.Enabled = Proyecto.Controler.PermissionHelper.HasFeatureAccess(Proyecto.Controler.PermissionHelper.Feature.Assets);
            btnAccounting.Enabled = Proyecto.Controler.PermissionHelper.HasFeatureAccess(Proyecto.Controler.PermissionHelper.Feature.Accounting);
            btnAudit.Enabled = Proyecto.Controler.PermissionHelper.HasFeatureAccess(Proyecto.Controler.PermissionHelper.Feature.Audit);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                View.frmDepartamentos frm = new View.frmDepartamentos();
                frm.Show();
            }
            catch (Exception ex)
            {
                LogError("frmDepartamentos", ex);
                MessageBox.Show("Error al abrir Departamentos: " + ex.Message + "\n\nVer debug.log para más detalles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                View.frmEmpleados frm = new View.frmEmpleados();
                frm.Show();
            }
            catch (Exception ex)
            {
                LogError("frmEmpleados", ex);
                MessageBox.Show("Error al abrir Empleados: " + ex.Message + "\n\nVer debug.log para más detalles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnContracts_Click(object sender, EventArgs e)
        {
            try
            {
                View.frmEmployeeContracts frm = new View.frmEmployeeContracts();
                frm.Show();
            }
            catch (Exception ex)
            {
                LogError("frmEmployeeContracts", ex);
                MessageBox.Show("Error al abrir Contratos: " + ex.Message + "\n\nVer debug.log para más detalles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            try
            {
                View.frmAttendance frm = new View.frmAttendance();
                frm.Show();
            }
            catch (Exception ex)
            {
                LogError("frmAttendance", ex);
                MessageBox.Show("Error al abrir Asistencia: " + ex.Message + "\n\nVer debug.log para más detalles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPayroll_Click(object sender, EventArgs e)
        {
            try
            {
                View.frmPayroll frm = new View.frmPayroll();
                frm.Show();
            }
            catch (Exception ex)
            {
                LogError("frmPayroll", ex);
                MessageBox.Show("Error al abrir Nómina: " + ex.Message + "\n\nVer debug.log para más detalles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            try
            {
                View.frmClients frm = new View.frmClients();
                frm.Show();
            }
            catch (Exception ex)
            {
                LogError("frmClients", ex);
                MessageBox.Show("Error al abrir Clientes: " + ex.Message + "\n\nVer debug.log para más detalles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnQuotes_Click(object sender, EventArgs e)
        {
            try
            {
                View.frmQuotes frm = new View.frmQuotes();
                frm.Show();
            }
            catch (Exception ex)
            {
                LogError("frmQuotes", ex);
                MessageBox.Show("Error al abrir Cotizaciones: " + ex.Message + "\n\nVer debug.log para más detalles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInvoicing_Click(object sender, EventArgs e)
        {
            try
            {
                View.frmInvoicing frm = new View.frmInvoicing();
                frm.Show();
            }
            catch (Exception ex)
            {
                LogError("frmInvoicing", ex);
                MessageBox.Show("Error al abrir Facturas: " + ex.Message + "\n\nVer debug.log para más detalles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            try
            {
                View.frmPayments frm = new View.frmPayments();
                frm.Show();
            }
            catch (Exception ex)
            {
                LogError("frmPayments", ex);
                MessageBox.Show("Error al abrir Pagos: " + ex.Message + "\n\nVer debug.log para más detalles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            try
            {
                View.frmProducts frm = new View.frmProducts();
                frm.Show();
            }
            catch (Exception ex)
            {
                LogError("frmProducts", ex);
                MessageBox.Show("Error al abrir Inventario: " + ex.Message + "\n\nVer debug.log para más detalles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAssets_Click(object sender, EventArgs e)
        {
            try
            {
                View.frmAssets frm = new View.frmAssets();
                frm.Show();
            }
            catch (Exception ex)
            {
                LogError("frmAssets", ex);
                MessageBox.Show("Error al abrir Activos: " + ex.Message + "\n\nVer debug.log para más detalles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAccounting_Click(object sender, EventArgs e)
        {
            try
            {
                View.frmAccounting frm = new View.frmAccounting();
                frm.Show();
            }
            catch (Exception ex)
            {
                LogError("frmAccounting", ex);
                MessageBox.Show("Error al abrir Contabilidad: " + ex.Message + "\n\nVer debug.log para más detalles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAudit_Click(object sender, EventArgs e)
        {
            try
            {
                View.frmAudit frm = new View.frmAudit();
                frm.Show();
            }
            catch (Exception ex)
            {
                LogError("frmAudit", ex);
                MessageBox.Show("Error al abrir Auditoría: " + ex.Message + "\n\nVer debug.log para más detalles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadKpisAsync()
        {
            lblKPI.Text = "Cargando KPIs...";
            try
            {
                var dashCtrl = new Proyecto.Controler.dashboardControler();
                var kpis = await dashCtrl.GetKpiSnapshotAsync();
                
                // Format with Colombian locale
                var coCulture = new System.Globalization.CultureInfo("es-CO");
                string salesFormatted = kpis.TotalSales.ToString("C", coCulture);
                string overdueFormatted = kpis.OverdueAmount.ToString("C", coCulture);
                
                // Task 27: Add critical stock alert
                string criticalStockAlert = "";
                if (kpis.CriticalStockCount > 0)
                {
                    criticalStockAlert = $"⚠️ ALERTA STOCK: {kpis.CriticalStockCount} producto(s) en nivel crítico\n";
                }
                
                lblKPI.Text = criticalStockAlert +
                              $"Productos: {kpis.TotalProducts}\n" +
                              $"Ventas (histórico): {salesFormatted}\n" +
                              $"Facturas por cobrar: {kpis.PendingInvoices}\n" +
                              $"Vencido: {overdueFormatted}\n" +
                              $"Clientes: {kpis.TotalClients}";
            }
            catch
            {
                lblKPI.Text = "Error loading metrics.";
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            try
            {
                // FASE 4 - Task 37: Admin-only Company Settings
                if (Proyecto.Controler.UserSession.Role != "Admin")
                {
                    MessageBox.Show("Solo administradores pueden acceder a la configuración.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                View.frmSettings frm = new View.frmSettings();
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                LogError("frmSettings", ex);
                MessageBox.Show("Error al abrir Configuración: " + ex.Message + "\n\nVer debug.log para más detalles", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
