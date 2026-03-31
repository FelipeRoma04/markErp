using System;
using System.Data;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmAuditLog : Form
    {
        public frmAuditLog()
        {
            InitializeComponent();
            InitializeControls();
        }

        private void InitializeControls()
        {
            dgvAuditLog.AutoGenerateColumns = false;
            dgvAuditLog.Columns.Clear();

            dgvAuditLog.Columns.Add("Id", "ID");
            dgvAuditLog.Columns.Add("UserLogin", "Usuario");
            dgvAuditLog.Columns.Add("ActionType", "Tipo Acción");
            dgvAuditLog.Columns.Add("TableName", "Tabla");
            dgvAuditLog.Columns.Add("RecordId", "Record ID");
            dgvAuditLog.Columns.Add("Description", "Descripción");
            dgvAuditLog.Columns.Add("CreatedAt", "Fecha/Hora");

            cmbActionType.Items.Add("CREATE");
            cmbActionType.Items.Add("UPDATE");
            cmbActionType.Items.Add("DELETE");

            LoadAuditLog();
        }

        private void LoadAuditLog(string table = null, string action = null, DateTime? from = null)
        {
            try
            {
                DataTable logs = AuditControler.GetLogs(table, action, from);
                if (logs != null)
                    dgvAuditLog.DataSource = logs;
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error cargando auditoria: " + ex.Message);
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            try
            {
                string tableFilter = string.IsNullOrWhiteSpace(txtTableFilter.Text) ? null : txtTableFilter.Text;
                string actionFilter = cmbActionType.SelectedItem?.ToString();
                DateTime? dateFilter = chkDateFilter.Checked ? dtpFromDate.Value : (DateTime?)null;

                LoadAuditLog(tableFilter, actionFilter, dateFilter);
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error filtrando: " + ex.Message);
            }
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtTableFilter.Clear();
            cmbActionType.SelectedIndex = -1;
            chkDateFilter.Checked = false;
            dtpFromDate.Value = DateTime.Now.AddMonths(-1);
            LoadAuditLog();
        }

        private void btnExportAudit_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "Excel Files (*.xlsx)|*.xlsx",
                    FileName = $"AuditLog_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    DataTable source = (DataTable)dgvAuditLog.DataSource;
                    if (ExcelHelper.ExportToExcel(source, sfd.FileName, "Audit Log"))
                    {
                        ValidationHelper.ShowSuccess($"Auditoría exportada a: {sfd.FileName}");
                    }
                    else
                    {
                        ValidationHelper.ShowError("Error al exportar a Excel.");
                    }
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error: " + ex.Message);
            }
        }
    }
}
