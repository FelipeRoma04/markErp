using System;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto
{
    public partial class principal : Form
    {
        public principal()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try 
            {
                Proyecto.Controler.dashboardControler dashCtrl = new Proyecto.Controler.dashboardControler();
                lblKPI.Text = dashCtrl.GetKpiSummary();
            }
            catch { lblKPI.Text = "Error loading metrics."; }

            ApplyRoleUi();
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
            View.frmDepartamentos frm = new View.frmDepartamentos();
            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            View.frmEmpleados frm = new View.frmEmpleados();
            frm.Show();
        }

        private void btnContracts_Click(object sender, EventArgs e)
        {
            View.frmEmployeeContracts frm = new View.frmEmployeeContracts();
            frm.Show();
        }

        private void btnAttendance_Click(object sender, EventArgs e)
        {
            View.frmAttendance frm = new View.frmAttendance();
            frm.Show();
        }

        private void btnPayroll_Click(object sender, EventArgs e)
        {
            View.frmPayroll frm = new View.frmPayroll();
            frm.Show();
        }

        private void btnClients_Click(object sender, EventArgs e)
        {
            View.frmClients frm = new View.frmClients();
            frm.Show();
        }

        private void btnQuotes_Click(object sender, EventArgs e)
        {
            View.frmQuotes frm = new View.frmQuotes();
            frm.Show();
        }

        private void btnInvoicing_Click(object sender, EventArgs e)
        {
            View.frmInvoicing frm = new View.frmInvoicing();
            frm.Show();
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            View.frmPayments frm = new View.frmPayments();
            frm.Show();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            View.frmProducts frm = new View.frmProducts();
            frm.Show();
        }

        private void btnAssets_Click(object sender, EventArgs e)
        {
            View.frmAssets frm = new View.frmAssets();
            frm.Show();
        }

        private void btnAccounting_Click(object sender, EventArgs e)
        {
            View.frmAccounting frm = new View.frmAccounting();
            frm.Show();
        }

        private void btnAudit_Click(object sender, EventArgs e)
        {
            View.frmAudit frm = new View.frmAudit();
            frm.Show();
        }
    }
}
