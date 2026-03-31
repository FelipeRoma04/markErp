namespace Proyecto.View
{
    partial class frmAccounting
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl        = new System.Windows.Forms.TabControl();
            this.tabPUC            = new System.Windows.Forms.TabPage();
            this.tabDiario         = new System.Windows.Forms.TabPage();
            this.tabReportes       = new System.Windows.Forms.TabPage();

            // ── Tab PUC ──
            this.txtPucFilter      = new System.Windows.Forms.TextBox();
            this.btnPucSearch      = new System.Windows.Forms.Button();
            this.dgvPUC            = new System.Windows.Forms.DataGridView();

            // ── Tab Diario ──
            this.lblEntryDate      = new System.Windows.Forms.Label();
            this.dtpEntryDate      = new System.Windows.Forms.DateTimePicker();
            this.lblEntryDesc      = new System.Windows.Forms.Label();
            this.txtEntryDesc      = new System.Windows.Forms.TextBox();
            this.btnCreateEntry    = new System.Windows.Forms.Button();
            this.dgvJournal        = new System.Windows.Forms.DataGridView();
            this.lblLineTitle      = new System.Windows.Forms.Label();
            this.lblLineAccount    = new System.Windows.Forms.Label();
            this.cmbLineAccount    = new System.Windows.Forms.ComboBox();
            this.lblLineDebit      = new System.Windows.Forms.Label();
            this.txtLineDebit      = new System.Windows.Forms.TextBox();
            this.lblLineCredit     = new System.Windows.Forms.Label();
            this.txtLineCredit     = new System.Windows.Forms.TextBox();
            this.btnAddLine        = new System.Windows.Forms.Button();
            this.dgvLines          = new System.Windows.Forms.DataGridView();
            this.lblBalanceCheck   = new System.Windows.Forms.Label();

            // ── Tab Reportes ──
            this.lblRptFrom        = new System.Windows.Forms.Label();
            this.dtpRptFrom        = new System.Windows.Forms.DateTimePicker();
            this.lblRptTo          = new System.Windows.Forms.Label();
            this.dtpRptTo          = new System.Windows.Forms.DateTimePicker();
            this.btnBalance        = new System.Windows.Forms.Button();
            this.btnPL             = new System.Windows.Forms.Button();
            this.btnExportRpt      = new System.Windows.Forms.Button();
            this.dgvReport         = new System.Windows.Forms.DataGridView();
            this.lblRptTitle       = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvPUC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJournal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();

            // ── TAB CONTROL ──
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Size = new System.Drawing.Size(900, 560);
            this.tabControl.Controls.Add(this.tabPUC);
            this.tabControl.Controls.Add(this.tabDiario);
            this.tabControl.Controls.Add(this.tabReportes);

            // ── TAB PUC ──
            this.tabPUC.Text = "Plan de Cuentas PUC";
            this.tabPUC.BackColor = System.Drawing.SystemColors.Control;

            this.txtPucFilter.Location = new System.Drawing.Point(15, 15); this.txtPucFilter.Size = new System.Drawing.Size(250, 22);
            this.btnPucSearch.Location = new System.Drawing.Point(275, 13); this.btnPucSearch.Size = new System.Drawing.Size(100, 26); this.btnPucSearch.Text = "🔍 Buscar";
            this.btnPucSearch.Click += new System.EventHandler(this.btnPucSearch_Click);
            this.dgvPUC.Location = new System.Drawing.Point(15, 50); this.dgvPUC.Size = new System.Drawing.Size(855, 460);
            this.dgvPUC.ReadOnly = true; this.dgvPUC.AllowUserToAddRows = false;
            this.dgvPUC.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.tabPUC.Controls.AddRange(new System.Windows.Forms.Control[] { txtPucFilter, btnPucSearch, dgvPUC });

            // ── TAB DIARIO ──
            this.tabDiario.Text = "Libro Diario (Asientos)";
            this.tabDiario.BackColor = System.Drawing.SystemColors.Control;

            int y = 15;
            this.lblEntryDate.Location = new System.Drawing.Point(15, y); this.lblEntryDate.Text = "Fecha:"; this.lblEntryDate.AutoSize = true;
            this.dtpEntryDate.Location = new System.Drawing.Point(75, y); this.dtpEntryDate.Size = new System.Drawing.Size(160, 22);
            this.lblEntryDesc.Location = new System.Drawing.Point(250, y); this.lblEntryDesc.Text = "Descripción:"; this.lblEntryDesc.AutoSize = true;
            this.txtEntryDesc.Location = new System.Drawing.Point(330, y); this.txtEntryDesc.Size = new System.Drawing.Size(350, 22);
            this.btnCreateEntry.Location = new System.Drawing.Point(695, y - 2); this.btnCreateEntry.Size = new System.Drawing.Size(150, 26);
            this.btnCreateEntry.Text = "➕ Crear Asiento"; this.btnCreateEntry.Click += new System.EventHandler(this.btnCreateEntry_Click);

            y += 40;
            this.dgvJournal.Location = new System.Drawing.Point(15, y); this.dgvJournal.Size = new System.Drawing.Size(855, 130);
            this.dgvJournal.ReadOnly = true; this.dgvJournal.AllowUserToAddRows = false;
            this.dgvJournal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvJournal.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvJournal_CellMouseClick);

            y += 145;
            this.lblLineTitle.Location = new System.Drawing.Point(15, y); this.lblLineTitle.Text = "── Líneas del Asiento Seleccionado ──";
            this.lblLineTitle.AutoSize = true; this.lblLineTitle.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);

            y += 25;
            this.lblLineAccount.Location = new System.Drawing.Point(15, y); this.lblLineAccount.Text = "Cuenta PUC:"; this.lblLineAccount.AutoSize = true;
            this.cmbLineAccount.Location = new System.Drawing.Point(105, y); this.cmbLineAccount.Size = new System.Drawing.Size(300, 22);
            this.cmbLineAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lblLineDebit.Location = new System.Drawing.Point(420, y); this.lblLineDebit.Text = "Débito:"; this.lblLineDebit.AutoSize = true;
            this.txtLineDebit.Location = new System.Drawing.Point(470, y); this.txtLineDebit.Size = new System.Drawing.Size(90, 22); this.txtLineDebit.Text = "0";
            this.lblLineCredit.Location = new System.Drawing.Point(575, y); this.lblLineCredit.Text = "Crédito:"; this.lblLineCredit.AutoSize = true;
            this.txtLineCredit.Location = new System.Drawing.Point(625, y); this.txtLineCredit.Size = new System.Drawing.Size(90, 22); this.txtLineCredit.Text = "0";
            this.btnAddLine.Location = new System.Drawing.Point(730, y - 2); this.btnAddLine.Size = new System.Drawing.Size(120, 26);
            this.btnAddLine.Text = "➕ Añadir Línea"; this.btnAddLine.Click += new System.EventHandler(this.btnAddLine_Click);

            y += 38;
            this.dgvLines.Location = new System.Drawing.Point(15, y); this.dgvLines.Size = new System.Drawing.Size(855, 130);
            this.dgvLines.ReadOnly = true; this.dgvLines.AllowUserToAddRows = false;
            this.dgvLines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            y += 140;
            this.lblBalanceCheck.Location = new System.Drawing.Point(15, y); this.lblBalanceCheck.AutoSize = true;
            this.lblBalanceCheck.Font = new System.Drawing.Font("Segoe UI", 9, System.Drawing.FontStyle.Bold);
            this.lblBalanceCheck.Text = "✔ Partida doble: pendiente";

            this.tabDiario.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblEntryDate, dtpEntryDate, lblEntryDesc, txtEntryDesc, btnCreateEntry,
                dgvJournal, lblLineTitle, lblLineAccount, cmbLineAccount,
                lblLineDebit, txtLineDebit, lblLineCredit, txtLineCredit, btnAddLine,
                dgvLines, lblBalanceCheck
            });

            // ── TAB REPORTES ──
            this.tabReportes.Text = "Reportes Financieros";
            this.tabReportes.BackColor = System.Drawing.SystemColors.Control;

            y = 15;
            this.lblRptFrom.Location = new System.Drawing.Point(15,  y); this.lblRptFrom.Text = "Desde:"; this.lblRptFrom.AutoSize = true;
            this.dtpRptFrom.Location = new System.Drawing.Point(70,  y); this.dtpRptFrom.Size = new System.Drawing.Size(150, 22); this.dtpRptFrom.Value = System.DateTime.Today.AddMonths(-1);
            this.lblRptTo.Location   = new System.Drawing.Point(235, y); this.lblRptTo.Text = "Hasta:"; this.lblRptTo.AutoSize = true;
            this.dtpRptTo.Location   = new System.Drawing.Point(285, y); this.dtpRptTo.Size = new System.Drawing.Size(150, 22);
            this.btnBalance.Location = new System.Drawing.Point(455, y - 2); this.btnBalance.Size = new System.Drawing.Size(150, 28);
            this.btnBalance.Text = "📊 Balance General"; this.btnBalance.Click += new System.EventHandler(this.btnBalance_Click);
            this.btnPL.Location      = new System.Drawing.Point(615, y - 2); this.btnPL.Size = new System.Drawing.Size(140, 28);
            this.btnPL.Text = "📈 Estado de Resultados"; this.btnPL.Click += new System.EventHandler(this.btnPL_Click);
            this.btnExportRpt.Location = new System.Drawing.Point(763, y - 2); this.btnExportRpt.Size = new System.Drawing.Size(110, 28);
            this.btnExportRpt.Text = "⬇ Exportar CSV"; this.btnExportRpt.Click += new System.EventHandler(this.btnExportRpt_Click);

            y += 45;
            this.lblRptTitle.Location = new System.Drawing.Point(15, y); this.lblRptTitle.AutoSize = true;
            this.lblRptTitle.Text = "Seleccione un reporte para generar.";
            this.lblRptTitle.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);

            y += 30;
            this.dgvReport.Location = new System.Drawing.Point(15, y); this.dgvReport.Size = new System.Drawing.Size(858, 440);
            this.dgvReport.ReadOnly = true; this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            this.tabReportes.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblRptFrom, dtpRptFrom, lblRptTo, dtpRptTo,
                btnBalance, btnPL, btnExportRpt, lblRptTitle, dgvReport
            });

            // ── FORM ──
            this.Controls.Add(this.tabControl);
            this.ClientSize = new System.Drawing.Size(900, 595);
            this.Text = "Contabilidad — Plan PUC Colombiano";
            this.Name = "frmAccounting";

            ((System.ComponentModel.ISupportInitialize)(this.dgvPUC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJournal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPUC, tabDiario, tabReportes;
        private System.Windows.Forms.TextBox txtPucFilter;
        private System.Windows.Forms.Button btnPucSearch;
        private System.Windows.Forms.DataGridView dgvPUC;
        private System.Windows.Forms.Label lblEntryDate, lblEntryDesc;
        private System.Windows.Forms.DateTimePicker dtpEntryDate;
        private System.Windows.Forms.TextBox txtEntryDesc;
        private System.Windows.Forms.Button btnCreateEntry;
        private System.Windows.Forms.DataGridView dgvJournal;
        private System.Windows.Forms.Label lblLineTitle, lblLineAccount, lblLineDebit, lblLineCredit;
        private System.Windows.Forms.ComboBox cmbLineAccount;
        private System.Windows.Forms.TextBox txtLineDebit, txtLineCredit;
        private System.Windows.Forms.Button btnAddLine;
        private System.Windows.Forms.DataGridView dgvLines;
        private System.Windows.Forms.Label lblBalanceCheck;
        private System.Windows.Forms.Label lblRptFrom, lblRptTo, lblRptTitle;
        private System.Windows.Forms.DateTimePicker dtpRptFrom, dtpRptTo;
        private System.Windows.Forms.Button btnBalance, btnPL, btnExportRpt;
        private System.Windows.Forms.DataGridView dgvReport;
    }
}
