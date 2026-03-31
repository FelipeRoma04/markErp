using System;
using System.Data;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmAccounting : Form
    {
        accountingControler ctrl;
        int selectedJournalId = -1;
        DataTable currentReport = null;

        public frmAccounting()
        {
            InitializeComponent();
            ctrl = new accountingControler();
            this.Load += FrmAccounting_Load;
        }

        private void FrmAccounting_Load(object sender, EventArgs e)
        {
            CargarPUC();
            CargarCuentasCombo();
            CargarJournal();
        }

        // ─────────────────────────────────────────
        // TAB 1: Plan de Cuentas PUC
        // ─────────────────────────────────────────

        private void CargarPUC(string filter = null)
        {
            DataTable dt = ctrl.GetAccounts(filter);
            if (dt != null) dgvPUC.DataSource = dt;
        }

        private void CargarCuentasCombo()
        {
            DataTable dt = ctrl.GetAccounts();
            if (dt != null)
            {
                cmbLineAccount.DataSource    = dt;
                cmbLineAccount.DisplayMember = "Name";
                cmbLineAccount.ValueMember   = "Id";
                cmbLineAccount.SelectedIndex = -1;
            }
        }

        private void btnPucSearch_Click(object sender, EventArgs e)
        {
            CargarPUC(txtPucFilter.Text.Trim());
        }

        // ─────────────────────────────────────────
        // TAB 2: Libro Diario
        // ─────────────────────────────────────────

        private void CargarJournal()
        {
            DataTable dt = ctrl.GetJournalEntries();
            if (dt != null) dgvJournal.DataSource = dt;
        }

        private void btnCreateEntry_Click(object sender, EventArgs e)
        {
            ctrl.EntryDate   = dtpEntryDate.Value;
            ctrl.Description = txtEntryDesc.Text.Trim();

            int newId = ctrl.CreateJournalEntry();
            if (newId > 0)
            {
                selectedJournalId = newId;
                MessageBox.Show($"Asiento #{newId} creado. Ahora añade las líneas.", "OK",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtEntryDesc.Clear();
                CargarJournal();
                RefrescarLineas();
                ActualizarBalanceCheck();
            }
        }

        private void dgvJournal_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            selectedJournalId = Convert.ToInt32(dgvJournal.Rows[e.RowIndex].Cells["Id"].Value);
            ctrl.JournalEntryId = selectedJournalId;
            RefrescarLineas();
            ActualizarBalanceCheck();
        }

        private void btnAddLine_Click(object sender, EventArgs e)
        {
            if (selectedJournalId < 0)
            {
                MessageBox.Show("Selecciona o crea un asiento primero.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ctrl.JournalEntryId = selectedJournalId;
            ctrl.AccountId      = cmbLineAccount.SelectedValue != null ? Convert.ToInt32(cmbLineAccount.SelectedValue) : 0;
            ctrl.Debit          = ParseDecimal(txtLineDebit.Text);
            ctrl.Credit         = ParseDecimal(txtLineCredit.Text);

            if (ctrl.AddLine())
            {
                txtLineDebit.Text  = "0";
                txtLineCredit.Text = "0";
                cmbLineAccount.SelectedIndex = -1;
                RefrescarLineas();
                ActualizarBalanceCheck();
            }
            else
            {
                MessageBox.Show("Error al añadir línea. Verifica los datos.", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RefrescarLineas()
        {
            ctrl.JournalEntryId = selectedJournalId;
            DataTable dt = ctrl.GetJournalLines();
            if (dt != null) dgvLines.DataSource = dt;
        }

        private void ActualizarBalanceCheck()
        {
            ctrl.JournalEntryId = selectedJournalId;
            DataTable dt = ctrl.GetJournalLines();
            if (dt == null || dt.Rows.Count == 0)
            {
                lblBalanceCheck.Text      = "Partida doble: sin líneas aún.";
                lblBalanceCheck.ForeColor = System.Drawing.Color.Gray;
                return;
            }

            decimal totalDebit = 0, totalCredit = 0;
            foreach (DataRow row in dt.Rows)
            {
                totalDebit  += Convert.ToDecimal(row["Debit"]);
                totalCredit += Convert.ToDecimal(row["Credit"]);
            }

            if (totalDebit == totalCredit)
            {
                lblBalanceCheck.Text      = $"✔ Partida doble balanceada — Débito: {totalDebit:C}  Crédito: {totalCredit:C}";
                lblBalanceCheck.ForeColor = System.Drawing.Color.DarkGreen;
            }
            else
            {
                lblBalanceCheck.Text      = $"✘ Desbalance — Débito: {totalDebit:C}  Crédito: {totalCredit:C}  (Diferencia: {Math.Abs(totalDebit - totalCredit):C})";
                lblBalanceCheck.ForeColor = System.Drawing.Color.Crimson;
            }
        }

        // ─────────────────────────────────────────
        // TAB 3: Reportes Financieros
        // ─────────────────────────────────────────

        private void btnBalance_Click(object sender, EventArgs e)
        {
            currentReport         = ctrl.GetBalanceGeneral();
            dgvReport.DataSource  = currentReport;
            lblRptTitle.Text      = "BALANCE GENERAL";
        }

        private void btnPL_Click(object sender, EventArgs e)
        {
            currentReport         = ctrl.GetPL(dtpRptFrom.Value, dtpRptTo.Value);
            dgvReport.DataSource  = currentReport;
            lblRptTitle.Text      = $"ESTADO DE RESULTADOS — {dtpRptFrom.Value:dd/MM/yyyy} al {dtpRptTo.Value:dd/MM/yyyy}";
        }

        private void btnExportRpt_Click(object sender, EventArgs e)
        {
            if (currentReport == null || currentReport.Rows.Count == 0)
            {
                MessageBox.Show("Genera un reporte primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ReportControler.ExportToExcel(currentReport, lblRptTitle.Text.Replace(" ", "_"));
        }

        // ─────────────────────────────────────────
        // Helpers
        // ─────────────────────────────────────────

        private decimal ParseDecimal(string val)
        {
            decimal result;
            decimal.TryParse(val?.Replace(",", "."),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out result);
            return result;
        }
    }
}
