using System.Drawing;
using Proyecto.Controler;

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
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPUC = new System.Windows.Forms.TabPage();
            this.dgvPUC = new System.Windows.Forms.DataGridView();
            this.pnlPucHeader = new System.Windows.Forms.Panel();
            this.btnPucSearch = new System.Windows.Forms.Button();
            this.txtPucFilter = new System.Windows.Forms.TextBox();
            this.tabDiario = new System.Windows.Forms.TabPage();
            this.lblBalanceCheck = new System.Windows.Forms.Label();
            this.dgvLines = new System.Windows.Forms.DataGridView();
            this.pnlLineInput = new System.Windows.Forms.Panel();
            this.btnAddLine = new System.Windows.Forms.Button();
            this.txtLineCredit = new System.Windows.Forms.TextBox();
            this.lblLineCredit = new System.Windows.Forms.Label();
            this.txtLineDebit = new System.Windows.Forms.TextBox();
            this.lblLineDebit = new System.Windows.Forms.Label();
            this.cmbLineAccount = new System.Windows.Forms.ComboBox();
            this.lblLineAccount = new System.Windows.Forms.Label();
            this.lblLineTitle = new System.Windows.Forms.Label();
            this.dgvJournal = new System.Windows.Forms.DataGridView();
            this.pnlEntryInput = new System.Windows.Forms.Panel();
            this.btnCreateEntry = new System.Windows.Forms.Button();
            this.txtEntryDesc = new System.Windows.Forms.TextBox();
            this.lblEntryDesc = new System.Windows.Forms.Label();
            this.dtpEntryDate = new System.Windows.Forms.DateTimePicker();
            this.lblEntryDate = new System.Windows.Forms.Label();
            this.tabReportes = new System.Windows.Forms.TabPage();
            this.dgvReport = new System.Windows.Forms.DataGridView();
            this.lblRptTitle = new System.Windows.Forms.Label();
            this.pnlReportFilters = new System.Windows.Forms.Panel();
            this.btnExportRpt = new System.Windows.Forms.Button();
            this.btnPL = new System.Windows.Forms.Button();
            this.btnBalance = new System.Windows.Forms.Button();
            this.dtpRptTo = new System.Windows.Forms.DateTimePicker();
            this.lblRptTo = new System.Windows.Forms.Label();
            this.dtpRptFrom = new System.Windows.Forms.DateTimePicker();
            this.lblRptFrom = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPUC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPUC)).BeginInit();
            this.pnlPucHeader.SuspendLayout();
            this.tabDiario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLines)).BeginInit();
            this.pnlLineInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJournal)).BeginInit();
            this.pnlEntryInput.SuspendLayout();
            this.tabReportes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).BeginInit();
            this.pnlReportFilters.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPUC);
            this.tabControl.Controls.Add(this.tabDiario);
            this.tabControl.Controls.Add(this.tabReportes);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabControl.Location = new System.Drawing.Point(0, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1000, 640);
            this.tabControl.TabIndex = 0;
            // 
            // tabPUC
            // 
            this.tabPUC.Controls.Add(this.dgvPUC);
            this.tabPUC.Controls.Add(this.pnlPucHeader);
            this.tabPUC.Location = new System.Drawing.Point(4, 29);
            this.tabPUC.Name = "tabPUC";
            this.tabPUC.Padding = new System.Windows.Forms.Padding(10);
            this.tabPUC.Size = new System.Drawing.Size(992, 607);
            this.tabPUC.TabIndex = 0;
            this.tabPUC.Text = "Plan de Cuentas PUC";
            this.tabPUC.UseVisualStyleBackColor = true;
            // 
            // dgvPUC
            // 
            this.dgvPUC.AllowUserToAddRows = false;
            this.dgvPUC.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPUC.BackgroundColor = System.Drawing.Color.White;
            this.dgvPUC.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPUC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPUC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPUC.Location = new System.Drawing.Point(10, 70);
            this.dgvPUC.Name = "dgvPUC";
            this.dgvPUC.ReadOnly = true;
            this.dgvPUC.RowHeadersVisible = false;
            this.dgvPUC.Size = new System.Drawing.Size(972, 527);
            this.dgvPUC.TabIndex = 1;
            // 
            // pnlPucHeader
            // 
            this.pnlPucHeader.Controls.Add(this.btnPucSearch);
            this.pnlPucHeader.Controls.Add(this.txtPucFilter);
            this.pnlPucHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlPucHeader.Location = new System.Drawing.Point(10, 10);
            this.pnlPucHeader.Name = "pnlPucHeader";
            this.pnlPucHeader.Size = new System.Drawing.Size(972, 60);
            this.pnlPucHeader.TabIndex = 2;
            // 
            // btnPucSearch
            // 
            this.btnPucSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnPucSearch.FlatAppearance.BorderSize = 0;
            this.btnPucSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPucSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPucSearch.ForeColor = System.Drawing.Color.White;
            this.btnPucSearch.Location = new System.Drawing.Point(312, 12);
            this.btnPucSearch.Name = "btnPucSearch";
            this.btnPucSearch.Size = new System.Drawing.Size(120, 35);
            this.btnPucSearch.TabIndex = 1;
            this.btnPucSearch.Text = "🔍 Buscar";
            this.btnPucSearch.UseVisualStyleBackColor = false;
            this.btnPucSearch.Click += new System.EventHandler(this.btnPucSearch_Click);
            // 
            // txtPucFilter
            // 
            this.txtPucFilter.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPucFilter.Location = new System.Drawing.Point(12, 15);
            this.txtPucFilter.Name = "txtPucFilter";
            this.txtPucFilter.Size = new System.Drawing.Size(280, 30);
            this.txtPucFilter.TabIndex = 0;
            // 
            // tabDiario
            // 
            this.tabDiario.Controls.Add(this.lblBalanceCheck);
            this.tabDiario.Controls.Add(this.dgvLines);
            this.tabDiario.Controls.Add(this.pnlLineInput);
            this.tabDiario.Controls.Add(this.lblLineTitle);
            this.tabDiario.Controls.Add(this.dgvJournal);
            this.tabDiario.Controls.Add(this.pnlEntryInput);
            this.tabDiario.Location = new System.Drawing.Point(4, 29);
            this.tabDiario.Name = "tabDiario";
            this.tabDiario.Padding = new System.Windows.Forms.Padding(10);
            this.tabDiario.Size = new System.Drawing.Size(992, 607);
            this.tabDiario.TabIndex = 1;
            this.tabDiario.Text = "Libro Diario (Asientos)";
            this.tabDiario.UseVisualStyleBackColor = true;
            // 
            // lblBalanceCheck
            // 
            this.lblBalanceCheck.AutoSize = true;
            this.lblBalanceCheck.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblBalanceCheck.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBalanceCheck.Location = new System.Drawing.Point(10, 574);
            this.lblBalanceCheck.Name = "lblBalanceCheck";
            this.lblBalanceCheck.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.lblBalanceCheck.Size = new System.Drawing.Size(227, 33);
            this.lblBalanceCheck.TabIndex = 5;
            this.lblBalanceCheck.Text = "✔ Partida doble: pendiente";
            // 
            // dgvLines
            // 
            this.dgvLines.AllowUserToAddRows = false;
            this.dgvLines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLines.BackgroundColor = System.Drawing.Color.White;
            this.dgvLines.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLines.Location = new System.Drawing.Point(10, 410);
            this.dgvLines.Name = "dgvLines";
            this.dgvLines.ReadOnly = true;
            this.dgvLines.RowHeadersVisible = false;
            this.dgvLines.Size = new System.Drawing.Size(972, 197);
            this.dgvLines.TabIndex = 4;
            // 
            // pnlLineInput
            // 
            this.pnlLineInput.Controls.Add(this.btnAddLine);
            this.pnlLineInput.Controls.Add(this.txtLineCredit);
            this.pnlLineInput.Controls.Add(this.lblLineCredit);
            this.pnlLineInput.Controls.Add(this.txtLineDebit);
            this.pnlLineInput.Controls.Add(this.lblLineDebit);
            this.pnlLineInput.Controls.Add(this.cmbLineAccount);
            this.pnlLineInput.Controls.Add(this.lblLineAccount);
            this.pnlLineInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLineInput.Location = new System.Drawing.Point(10, 350);
            this.pnlLineInput.Name = "pnlLineInput";
            this.pnlLineInput.Size = new System.Drawing.Size(972, 60);
            this.pnlLineInput.TabIndex = 3;
            // 
            // btnAddLine
            // 
            this.btnAddLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnAddLine.FlatAppearance.BorderSize = 0;
            this.btnAddLine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddLine.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAddLine.ForeColor = System.Drawing.Color.White;
            this.btnAddLine.Location = new System.Drawing.Point(825, 12);
            this.btnAddLine.Name = "btnAddLine";
            this.btnAddLine.Size = new System.Drawing.Size(130, 35);
            this.btnAddLine.TabIndex = 6;
            this.btnAddLine.Text = "➕ Añadir Línea";
            this.btnAddLine.UseVisualStyleBackColor = false;
            this.btnAddLine.Click += new System.EventHandler(this.btnAddLine_Click);
            // 
            // txtLineCredit
            // 
            this.txtLineCredit.Location = new System.Drawing.Point(705, 17);
            this.txtLineCredit.Name = "txtLineCredit";
            this.txtLineCredit.Size = new System.Drawing.Size(100, 27);
            this.txtLineCredit.TabIndex = 5;
            this.txtLineCredit.Text = "0";
            // 
            // lblLineCredit
            // 
            this.lblLineCredit.AutoSize = true;
            this.lblLineCredit.Location = new System.Drawing.Point(645, 20);
            this.lblLineCredit.Name = "lblLineCredit";
            this.lblLineCredit.Size = new System.Drawing.Size(61, 20);
            this.lblLineCredit.TabIndex = 4;
            this.lblLineCredit.Text = "Crédito:";
            // 
            // txtLineDebit
            // 
            this.txtLineDebit.Location = new System.Drawing.Point(525, 17);
            this.txtLineDebit.Name = "txtLineDebit";
            this.txtLineDebit.Size = new System.Drawing.Size(100, 27);
            this.txtLineDebit.TabIndex = 3;
            this.txtLineDebit.Text = "0";
            // 
            // lblLineDebit
            // 
            this.lblLineDebit.AutoSize = true;
            this.lblLineDebit.Location = new System.Drawing.Point(465, 20);
            this.lblLineDebit.Name = "lblLineDebit";
            this.lblLineDebit.Size = new System.Drawing.Size(57, 20);
            this.lblLineDebit.TabIndex = 2;
            this.lblLineDebit.Text = "Débito:";
            // 
            // cmbLineAccount
            // 
            this.cmbLineAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLineAccount.FormattingEnabled = true;
            this.cmbLineAccount.Location = new System.Drawing.Point(105, 17);
            this.cmbLineAccount.Name = "cmbLineAccount";
            this.cmbLineAccount.Size = new System.Drawing.Size(340, 28);
            this.cmbLineAccount.TabIndex = 1;
            // 
            // lblLineAccount
            // 
            this.lblLineAccount.AutoSize = true;
            this.lblLineAccount.Location = new System.Drawing.Point(12, 20);
            this.lblLineAccount.Name = "lblLineAccount";
            this.lblLineAccount.Size = new System.Drawing.Size(89, 20);
            this.lblLineAccount.TabIndex = 0;
            this.lblLineAccount.Text = "Cuenta PUC:";
            // 
            // lblLineTitle
            // 
            this.lblLineTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.lblLineTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLineTitle.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblLineTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblLineTitle.Location = new System.Drawing.Point(10, 310);
            this.lblLineTitle.Name = "lblLineTitle";
            this.lblLineTitle.Padding = new System.Windows.Forms.Padding(10, 5, 0, 5);
            this.lblLineTitle.Size = new System.Drawing.Size(972, 40);
            this.lblLineTitle.TabIndex = 2;
            this.lblLineTitle.Text = "── Líneas del Asiento Seleccionado ──";
            this.lblLineTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvJournal
            // 
            this.dgvJournal.AllowUserToAddRows = false;
            this.dgvJournal.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvJournal.BackgroundColor = System.Drawing.Color.White;
            this.dgvJournal.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvJournal.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJournal.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvJournal.Location = new System.Drawing.Point(10, 70);
            this.dgvJournal.Name = "dgvJournal";
            this.dgvJournal.ReadOnly = true;
            this.dgvJournal.RowHeadersVisible = false;
            this.dgvJournal.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvJournal.Size = new System.Drawing.Size(972, 240);
            this.dgvJournal.TabIndex = 1;
            this.dgvJournal.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvJournal_CellMouseClick);
            // 
            // pnlEntryInput
            // 
            this.pnlEntryInput.Controls.Add(this.btnCreateEntry);
            this.pnlEntryInput.Controls.Add(this.txtEntryDesc);
            this.pnlEntryInput.Controls.Add(this.lblEntryDesc);
            this.pnlEntryInput.Controls.Add(this.dtpEntryDate);
            this.pnlEntryInput.Controls.Add(this.lblEntryDate);
            this.pnlEntryInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEntryInput.Location = new System.Drawing.Point(10, 10);
            this.pnlEntryInput.Name = "pnlEntryInput";
            this.pnlEntryInput.Size = new System.Drawing.Size(972, 60);
            this.pnlEntryInput.TabIndex = 0;
            // 
            // btnCreateEntry
            // 
            this.btnCreateEntry.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnCreateEntry.FlatAppearance.BorderSize = 0;
            this.btnCreateEntry.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCreateEntry.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCreateEntry.ForeColor = System.Drawing.Color.White;
            this.btnCreateEntry.Location = new System.Drawing.Point(805, 12);
            this.btnCreateEntry.Name = "btnCreateEntry";
            this.btnCreateEntry.Size = new System.Drawing.Size(150, 35);
            this.btnCreateEntry.TabIndex = 4;
            this.btnCreateEntry.Text = "➕ Crear Asiento";
            this.btnCreateEntry.UseVisualStyleBackColor = false;
            this.btnCreateEntry.Click += new System.EventHandler(this.btnCreateEntry_Click);
            // 
            // txtEntryDesc
            // 
            this.txtEntryDesc.Location = new System.Drawing.Point(352, 17);
            this.txtEntryDesc.Name = "txtEntryDesc";
            this.txtEntryDesc.Size = new System.Drawing.Size(430, 27);
            this.txtEntryDesc.TabIndex = 3;
            // 
            // lblEntryDesc
            // 
            this.lblEntryDesc.AutoSize = true;
            this.lblEntryDesc.Location = new System.Drawing.Point(250, 20);
            this.lblEntryDesc.Name = "lblEntryDesc";
            this.lblEntryDesc.Size = new System.Drawing.Size(90, 20);
            this.lblEntryDesc.TabIndex = 2;
            this.lblEntryDesc.Text = "Descripción:";
            // 
            // dtpEntryDate
            // 
            this.dtpEntryDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEntryDate.Location = new System.Drawing.Point(72, 17);
            this.dtpEntryDate.Name = "dtpEntryDate";
            this.dtpEntryDate.Size = new System.Drawing.Size(150, 27);
            this.dtpEntryDate.TabIndex = 1;
            // 
            // lblEntryDate
            // 
            this.lblEntryDate.AutoSize = true;
            this.lblEntryDate.Location = new System.Drawing.Point(12, 20);
            this.lblEntryDate.Name = "lblEntryDate";
            this.lblEntryDate.Size = new System.Drawing.Size(49, 20);
            this.lblEntryDate.TabIndex = 0;
            this.lblEntryDate.Text = "Fecha:";
            // 
            // tabReportes
            // 
            this.tabReportes.Controls.Add(this.dgvReport);
            this.tabReportes.Controls.Add(this.lblRptTitle);
            this.tabReportes.Controls.Add(this.pnlReportFilters);
            this.tabReportes.Location = new System.Drawing.Point(4, 29);
            this.tabReportes.Name = "tabReportes";
            this.tabReportes.Padding = new System.Windows.Forms.Padding(10);
            this.tabReportes.Size = new System.Drawing.Size(992, 607);
            this.tabReportes.TabIndex = 2;
            this.tabReportes.Text = "Reportes Financieros";
            this.tabReportes.UseVisualStyleBackColor = true;
            // 
            // dgvReport
            // 
            this.dgvReport.AllowUserToAddRows = false;
            this.dgvReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvReport.BackgroundColor = System.Drawing.Color.White;
            this.dgvReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvReport.Location = new System.Drawing.Point(10, 110);
            this.dgvReport.Name = "dgvReport";
            this.dgvReport.ReadOnly = true;
            this.dgvReport.RowHeadersVisible = false;
            this.dgvReport.Size = new System.Drawing.Size(972, 487);
            this.dgvReport.TabIndex = 2;
            // 
            // lblRptTitle
            // 
            this.lblRptTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.lblRptTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblRptTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblRptTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblRptTitle.Location = new System.Drawing.Point(10, 70);
            this.lblRptTitle.Name = "lblRptTitle";
            this.lblRptTitle.Padding = new System.Windows.Forms.Padding(10, 5, 0, 5);
            this.lblRptTitle.Size = new System.Drawing.Size(972, 40);
            this.lblRptTitle.TabIndex = 1;
            this.lblRptTitle.Text = "Seleccione un reporte para generar.";
            this.lblRptTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlReportFilters
            // 
            this.pnlReportFilters.Controls.Add(this.btnExportRpt);
            this.pnlReportFilters.Controls.Add(this.btnPL);
            this.pnlReportFilters.Controls.Add(this.btnBalance);
            this.pnlReportFilters.Controls.Add(this.dtpRptTo);
            this.pnlReportFilters.Controls.Add(this.lblRptTo);
            this.pnlReportFilters.Controls.Add(this.dtpRptFrom);
            this.pnlReportFilters.Controls.Add(this.lblRptFrom);
            this.pnlReportFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlReportFilters.Location = new System.Drawing.Point(10, 10);
            this.pnlReportFilters.Name = "pnlReportFilters";
            this.pnlReportFilters.Size = new System.Drawing.Size(972, 60);
            this.pnlReportFilters.TabIndex = 0;
            // 
            // btnExportRpt
            // 
            this.btnExportRpt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnExportRpt.FlatAppearance.BorderSize = 0;
            this.btnExportRpt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportRpt.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnExportRpt.ForeColor = System.Drawing.Color.White;
            this.btnExportRpt.Location = new System.Drawing.Point(847, 12);
            this.btnExportRpt.Name = "btnExportRpt";
            this.btnExportRpt.Size = new System.Drawing.Size(120, 35);
            this.btnExportRpt.TabIndex = 6;
            this.btnExportRpt.Text = "⬇ Exportar CSV";
            this.btnExportRpt.UseVisualStyleBackColor = false;
            this.btnExportRpt.Click += new System.EventHandler(this.btnExportRpt_Click);
            // 
            // btnPL
            // 
            this.btnPL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnPL.FlatAppearance.BorderSize = 0;
            this.btnPL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPL.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPL.ForeColor = System.Drawing.Color.White;
            this.btnPL.Location = new System.Drawing.Point(645, 12);
            this.btnPL.Name = "btnPL";
            this.btnPL.Size = new System.Drawing.Size(180, 35);
            this.btnPL.TabIndex = 5;
            this.btnPL.Text = "📈 Resultados (P&L)";
            this.btnPL.UseVisualStyleBackColor = false;
            this.btnPL.Click += new System.EventHandler(this.btnPL_Click);
            // 
            // btnBalance
            // 
            this.btnBalance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnBalance.FlatAppearance.BorderSize = 0;
            this.btnBalance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBalance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnBalance.ForeColor = System.Drawing.Color.White;
            this.btnBalance.Location = new System.Drawing.Point(465, 12);
            this.btnBalance.Name = "btnBalance";
            this.btnBalance.Size = new System.Drawing.Size(170, 35);
            this.btnBalance.TabIndex = 4;
            this.btnBalance.Text = "📊 Balance General";
            this.btnBalance.UseVisualStyleBackColor = false;
            this.btnBalance.Click += new System.EventHandler(this.btnBalance_Click);
            // 
            // dtpRptTo
            // 
            this.dtpRptTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRptTo.Location = new System.Drawing.Point(292, 17);
            this.dtpRptTo.Name = "dtpRptTo";
            this.dtpRptTo.Size = new System.Drawing.Size(130, 27);
            this.dtpRptTo.TabIndex = 3;
            // 
            // lblRptTo
            // 
            this.lblRptTo.AutoSize = true;
            this.lblRptTo.Location = new System.Drawing.Point(235, 20);
            this.lblRptTo.Name = "lblRptTo";
            this.lblRptTo.Size = new System.Drawing.Size(50, 20);
            this.lblRptTo.TabIndex = 2;
            this.lblRptTo.Text = "Hasta:";
            // 
            // dtpRptFrom
            // 
            this.dtpRptFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRptFrom.Location = new System.Drawing.Point(75, 17);
            this.dtpRptFrom.Name = "dtpRptFrom";
            this.dtpRptFrom.Size = new System.Drawing.Size(130, 27);
            this.dtpRptFrom.TabIndex = 1;
            // 
            // lblRptFrom
            // 
            this.lblRptFrom.AutoSize = true;
            this.lblRptFrom.Location = new System.Drawing.Point(12, 20);
            this.lblRptFrom.Name = "lblRptFrom";
            this.lblRptFrom.Size = new System.Drawing.Size(54, 20);
            this.lblRptFrom.TabIndex = 0;
            this.lblRptFrom.Text = "Desde:";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1000, 60);
            this.pnlHeader.TabIndex = 6;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(512, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Contabilidad — Plan PUC Colombiano";
            // 
            // frmAccounting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlHeader);
            this.Name = "frmAccounting";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Contabilidad — MarkErp";
            this.tabControl.ResumeLayout(false);
            this.tabPUC.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPUC)).EndInit();
            this.pnlPucHeader.ResumeLayout(false);
            this.pnlPucHeader.PerformLayout();
            this.tabDiario.ResumeLayout(false);
            this.tabDiario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLines)).EndInit();
            this.pnlLineInput.ResumeLayout(false);
            this.pnlLineInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJournal)).EndInit();
            this.pnlEntryInput.ResumeLayout(false);
            this.pnlEntryInput.PerformLayout();
            this.tabReportes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReport)).EndInit();
            this.pnlReportFilters.ResumeLayout(false);
            this.pnlReportFilters.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.Panel pnlHeader, pnlPucHeader, pnlEntryInput, pnlLineInput, pnlReportFilters;
        private System.Windows.Forms.Label lblTitle;
    }
}
