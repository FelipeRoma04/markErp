using System;
using System.Data;
using System.Drawing;
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
            ApplyStyle();
            this.Load += FrmAccounting_Load;
        }

        private void ApplyStyle()
        {
            this.BackColor = UITheme.BgColor;
            pnlHeader.BackColor = UITheme.PrimaryColor;
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = UITheme.FontHeader;

            // Tab Pages
            tabPUC.BackColor = Color.White;
            tabDiario.BackColor = Color.White;
            tabReportes.BackColor = Color.White;

            // Grids
            UITheme.StyleDataGrid(dgvPUC);
            UITheme.StyleDataGrid(dgvJournal);
            UITheme.StyleDataGrid(dgvLines);
            UITheme.StyleDataGrid(dgvReport);

            // Labels
            UITheme.StyleLabel(lblEntryDate);
            UITheme.StyleLabel(lblEntryDesc);
            UITheme.StyleLabel(lblLineAccount);
            UITheme.StyleLabel(lblLineDebit);
            UITheme.StyleLabel(lblLineCredit);
            UITheme.StyleLabel(lblRptFrom);
            UITheme.StyleLabel(lblRptTo);

            // TextBoxes
            UITheme.StyleTextBox(txtPucFilter);
            UITheme.StyleTextBox(txtEntryDesc);
            UITheme.StyleTextBox(txtLineDebit);
            UITheme.StyleTextBox(txtLineCredit);

            // Buttons
            btnPucSearch.BackColor = UITheme.AccentColor;
            btnCreateEntry.BackColor = UITheme.PrimaryColor;
            btnAddLine.BackColor = UITheme.AccentColor;
            btnBalance.BackColor = UITheme.PrimaryColor;
            btnPL.BackColor = UITheme.AccentColor;
            btnExportRpt.BackColor = UITheme.SuccessColor;

            // Subheaders
            lblLineTitle.BackColor = Color.FromArgb(236, 240, 241);
            lblLineTitle.ForeColor = UITheme.PrimaryColor;
            lblLineTitle.Font = UITheme.FontSubHeader;

            lblRptTitle.BackColor = Color.FromArgb(236, 240, 241);
            lblRptTitle.ForeColor = UITheme.PrimaryColor;
            lblRptTitle.Font = UITheme.FontSubHeader;
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
                cmbLineAccount.DataSource = dt;
                cmbLineAccount.DisplayMember = "Name";
                cmbLineAccount.ValueMember = "Id";
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
            if (string.IsNullOrWhiteSpace(txtEntryDesc.Text))
            {
                ValidationHelper.ShowValidationError("La descripción del asiento es obligatoria.");
                return;
            }

            ctrl.EntryDate = dtpEntryDate.Value;
            ctrl.Description = txtEntryDesc.Text.Trim();

            int newId = ctrl.CreateJournalEntry();
            if (newId > 0)
            {
                selectedJournalId = newId;
                ValidationHelper.ShowSuccess($"Asiento #{newId} creado. Ahora puede añadir los movimientos.");
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
                ValidationHelper.ShowValidationError("Debe seleccionar o crear un asiento antes de añadir líneas.");
                return;
            }

            if (cmbLineAccount.SelectedIndex == -1)
            {
                ValidationHelper.ShowValidationError("Debe seleccionar una cuenta contable.");
                return;
            }

            ctrl.JournalEntryId = selectedJournalId;
            ctrl.AccountId = Convert.ToInt32(cmbLineAccount.SelectedValue);
            ctrl.Debit = ParseDecimal(txtLineDebit.Text);
            ctrl.Credit = ParseDecimal(txtLineCredit.Text);

            if (ctrl.Debit == 0 && ctrl.Credit == 0)
            {
                ValidationHelper.ShowValidationError("El valor del Débito o Crédito debe ser mayor a cero.");
                return;
            }

            if (ctrl.AddLine())
            {
                txtLineDebit.Text = "0";
                txtLineCredit.Text = "0";
                cmbLineAccount.SelectedIndex = -1;
                RefrescarLineas();
                ActualizarBalanceCheck();
            }
            else
            {
                ValidationHelper.ShowError("Error al registrar el movimiento contable.");
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
                lblBalanceCheck.Text = "✔ Partida doble: sin líneas aún.";
                lblBalanceCheck.ForeColor = UITheme.TextSecondary;
                return;
            }

            decimal totalDebit = 0, totalCredit = 0;
            foreach (DataRow row in dt.Rows)
            {
                totalDebit += Convert.ToDecimal(row["Debit"]);
                totalCredit += Convert.ToDecimal(row["Credit"]);
            }

            if (totalDebit == totalCredit)
            {
                lblBalanceCheck.Text = $"✔ Balanceado — Total: {totalDebit:C}";
                lblBalanceCheck.ForeColor = UITheme.SuccessColor;
            }
            else
            {
                decimal diff = Math.Abs(totalDebit - totalCredit);
                lblBalanceCheck.Text = $"✘ Desbalanceado — Diferencia: {diff:C} (D:{totalDebit:C} C:{totalCredit:C})";
                lblBalanceCheck.ForeColor = UITheme.DangerColor;
            }
        }

        // ─────────────────────────────────────────
        // TAB 3: Reportes Financieros
        // ─────────────────────────────────────────

        private void btnBalance_Click(object sender, EventArgs e)
        {
            currentReport = ctrl.GetBalanceGeneral();
            dgvReport.DataSource = currentReport;
            lblRptTitle.Text = "ESTADO DE SITUACIÓN FINANCIERA (BALANCE GENERAL)";
        }

        private void btnPL_Click(object sender, EventArgs e)
        {
            currentReport = ctrl.GetPL(dtpRptFrom.Value, dtpRptTo.Value);
            dgvReport.DataSource = currentReport;
            lblRptTitle.Text = $"ESTADO DE RESULTADOS INTEGRAL ({dtpRptFrom.Value:dd/MM/yy} - {dtpRptTo.Value:dd/MM/yy})";
        }

        private void btnExportRpt_Click(object sender, EventArgs e)
        {
            if (currentReport == null || currentReport.Rows.Count == 0)
            {
                ValidationHelper.ShowValidationError("Primero debe generar un reporte antes de exportarlo.");
                return;
            }
            
            if (ReportControler.ExportToExcel(currentReport, "Reporte_Contable_" + DateTime.Now.ToString("yyyyMMdd")))
            {
                ValidationHelper.ShowSuccess("Reporte exportado correctamente.");
            }
        }

        private decimal ParseDecimal(string val)
        {
            decimal result;
            string clean = val?.Replace("$", "").Replace(" ", "").Replace(",", ".");
            decimal.TryParse(clean, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out result);
            return result;
        }
    }
}
