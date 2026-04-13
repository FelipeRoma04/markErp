using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmAudit : Form
    {
        public frmAudit()
        {
            InitializeComponent();
            ApplyStyle();
            this.Load += FrmAudit_Load;
        }

        private void ApplyStyle()
        {
            this.BackColor = UITheme.BgColor;
            pnlHeader.BackColor = UITheme.PrimaryColor;
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = UITheme.FontHeader;

            pnlFilters.BackColor = Color.White;
            
            UITheme.StyleLabel(lblFilter);
            UITheme.StyleLabel(lblAction);
            UITheme.StyleLabel(lblFrom);
            
            btnFilter.BackColor = UITheme.AccentColor;
            btnExport.BackColor = UITheme.SuccessColor;

            UITheme.StyleDataGrid(dgvLogs);
        }

        private void FrmAudit_Load(object sender, EventArgs e)
        {
            if (cmbTable.Items.Count > 0) cmbTable.SelectedIndex = 0;
            if (cmbAction.Items.Count > 0) cmbAction.SelectedIndex = 0;
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
            {
                if (ReportControler.ExportToExcel(dt, "AuditLog_" + DateTime.Now.ToString("yyyyMMdd")))
                {
                    ValidationHelper.ShowSuccess("Historial exportado correctamente.");
                }
            }
            else
            {
                ValidationHelper.ShowValidationError("No hay datos para exportar con los filtros actuales.");
            }
        }

        private void CargarLogs()
        {
            try
            {
                string tabla = cmbTable.SelectedIndex == 0 ? null : cmbTable.SelectedItem.ToString();
                string accion = cmbAction.SelectedIndex == 0 ? null : cmbAction.SelectedItem.ToString();

                DataTable dt = AuditControler.GetLogs(tabla, accion, dtpFrom.Value);
                if (dt != null)
                {
                    dgvLogs.DataSource = dt;
                    lblCount.Text = $"Registros encontrados: {dt.Rows.Count}";
                    
                    // Style columns after binding
                    if (dgvLogs.Columns["Id"] != null) dgvLogs.Columns["Id"].Width = 60;
                    if (dgvLogs.Columns["CreatedAt"] != null) dgvLogs.Columns["CreatedAt"].DefaultCellStyle.Format = "g";
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error al cargar auditoría: " + ex.Message);
            }
        }
    }
}
