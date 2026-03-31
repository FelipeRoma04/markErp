using System;
using System.Data;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmAudit : Form
    {
        public frmAudit()
        {
            InitializeComponent();
            this.Load += FrmAudit_Load;
        }

        private void FrmAudit_Load(object sender, EventArgs e)
        {
            CargarLogs();
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            CargarLogs();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = AuditControler.GetLogs(
                cmbTable.SelectedIndex == 0 ? null : cmbTable.SelectedItem.ToString(),
                cmbAction.SelectedIndex == 0 ? null : cmbAction.SelectedItem.ToString(),
                dtpFrom.Value);

            if (dt != null && dt.Rows.Count > 0)
                ReportControler.ExportToExcel(dt, "AuditLog");
            else
                MessageBox.Show("No hay datos para exportar.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CargarLogs()
        {
            string tabla  = cmbTable.SelectedIndex  == 0 ? null : cmbTable.SelectedItem.ToString();
            string accion = cmbAction.SelectedIndex == 0 ? null : cmbAction.SelectedItem.ToString();

            DataTable dt = AuditControler.GetLogs(tabla, accion, dtpFrom.Value);
            if (dt != null)
            {
                dgvLogs.DataSource = dt;
                lblCount.Text = $"Total registros: {dt.Rows.Count}";
            }
        }
    }
}
