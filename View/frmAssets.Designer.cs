using System.Drawing;
using Proyecto.Controler;

namespace Proyecto.View
{
    partial class frmAssets
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabActivos = new System.Windows.Forms.TabPage();
            this.dgvActivos = new System.Windows.Forms.DataGridView();
            this.pnlAssetForm = new System.Windows.Forms.Panel();
            this.lblDepreciationValue = new System.Windows.Forms.Label();
            this.lblDepreciation = new System.Windows.Forms.Label();
            this.btnSaveAsset = new System.Windows.Forms.Button();
            this.txtUsefulLife = new System.Windows.Forms.TextBox();
            this.lblUsefulLife = new System.Windows.Forms.Label();
            this.txtSalvage = new System.Windows.Forms.TextBox();
            this.lblSalvage = new System.Windows.Forms.Label();
            this.txtPurchasePrice = new System.Windows.Forms.TextBox();
            this.lblPurchasePrice = new System.Windows.Forms.Label();
            this.cmbSede = new System.Windows.Forms.ComboBox();
            this.lblSede = new System.Windows.Forms.Label();
            this.txtBrand = new System.Windows.Forms.TextBox();
            this.lblBrand = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtSerial = new System.Windows.Forms.TextBox();
            this.lblSerial = new System.Windows.Forms.Label();
            this.tabAsignacion = new System.Windows.Forms.TabPage();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.pnlAsignForm = new System.Windows.Forms.Panel();
            this.btnAssign = new System.Windows.Forms.Button();
            this.dtpAssign = new System.Windows.Forms.DateTimePicker();
            this.lblAsign = new System.Windows.Forms.Label();
            this.txtEmpId = new System.Windows.Forms.TextBox();
            this.lblEmpId = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtAssetId = new System.Windows.Forms.TextBox();
            this.lblAssetId = new System.Windows.Forms.Label();
            this.tabMantenimiento = new System.Windows.Forms.TabPage();
            this.dgvMaint = new System.Windows.Forms.DataGridView();
            this.pnlMaintForm = new System.Windows.Forms.Panel();
            this.btnLoadMaint = new System.Windows.Forms.Button();
            this.btnScheduleMaint = new System.Windows.Forms.Button();
            this.dtpMaint = new System.Windows.Forms.DateTimePicker();
            this.lblMaintDate = new System.Windows.Forms.Label();
            this.txtMaintDesc = new System.Windows.Forms.TextBox();
            this.lblMaintDesc = new System.Windows.Forms.Label();
            this.txtMaintAssetId = new System.Windows.Forms.TextBox();
            this.lblMaintAssetId = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabActivos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivos)).BeginInit();
            this.pnlAssetForm.SuspendLayout();
            this.tabAsignacion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.pnlAsignForm.SuspendLayout();
            this.tabMantenimiento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaint)).BeginInit();
            this.pnlMaintForm.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabActivos);
            this.tabControl.Controls.Add(this.tabAsignacion);
            this.tabControl.Controls.Add(this.tabMantenimiento);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabControl.Location = new System.Drawing.Point(0, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1000, 640);
            this.tabControl.TabIndex = 0;
            // 
            // tabActivos
            // 
            this.tabActivos.Controls.Add(this.dgvActivos);
            this.tabActivos.Controls.Add(this.pnlAssetForm);
            this.tabActivos.Location = new System.Drawing.Point(4, 29);
            this.tabActivos.Name = "tabActivos";
            this.tabActivos.Padding = new System.Windows.Forms.Padding(10);
            this.tabActivos.Size = new System.Drawing.Size(992, 607);
            this.tabActivos.TabIndex = 0;
            this.tabActivos.Text = "Registro de Activos";
            this.tabActivos.UseVisualStyleBackColor = true;
            // 
            // dgvActivos
            // 
            this.dgvActivos.AllowUserToAddRows = false;
            this.dgvActivos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvActivos.BackgroundColor = System.Drawing.Color.White;
            this.dgvActivos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvActivos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvActivos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvActivos.Location = new System.Drawing.Point(360, 10);
            this.dgvActivos.Name = "dgvActivos";
            this.dgvActivos.ReadOnly = true;
            this.dgvActivos.RowHeadersVisible = false;
            this.dgvActivos.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvActivos.Size = new System.Drawing.Size(622, 587);
            this.dgvActivos.TabIndex = 1;
            this.dgvActivos.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvActivos_CellMouseClick);
            // 
            // pnlAssetForm
            // 
            this.pnlAssetForm.Controls.Add(this.lblDepreciationValue);
            this.pnlAssetForm.Controls.Add(this.lblDepreciation);
            this.pnlAssetForm.Controls.Add(this.btnSaveAsset);
            this.pnlAssetForm.Controls.Add(this.txtUsefulLife);
            this.pnlAssetForm.Controls.Add(this.lblUsefulLife);
            this.pnlAssetForm.Controls.Add(this.txtSalvage);
            this.pnlAssetForm.Controls.Add(this.lblSalvage);
            this.pnlAssetForm.Controls.Add(this.txtPurchasePrice);
            this.pnlAssetForm.Controls.Add(this.lblPurchasePrice);
            this.pnlAssetForm.Controls.Add(this.cmbSede);
            this.pnlAssetForm.Controls.Add(this.lblSede);
            this.pnlAssetForm.Controls.Add(this.txtBrand);
            this.pnlAssetForm.Controls.Add(this.lblBrand);
            this.pnlAssetForm.Controls.Add(this.cmbType);
            this.pnlAssetForm.Controls.Add(this.lblType);
            this.pnlAssetForm.Controls.Add(this.txtSerial);
            this.pnlAssetForm.Controls.Add(this.lblSerial);
            this.pnlAssetForm.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlAssetForm.Location = new System.Drawing.Point(10, 10);
            this.pnlAssetForm.Name = "pnlAssetForm";
            this.pnlAssetForm.Size = new System.Drawing.Size(350, 587);
            this.pnlAssetForm.TabIndex = 0;
            // 
            // lblDepreciationValue
            // 
            this.lblDepreciationValue.AutoSize = true;
            this.lblDepreciationValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblDepreciationValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblDepreciationValue.Location = new System.Drawing.Point(15, 520);
            this.lblDepreciationValue.Name = "lblDepreciationValue";
            this.lblDepreciationValue.Size = new System.Drawing.Size(24, 28);
            this.lblDepreciationValue.TabIndex = 16;
            this.lblDepreciationValue.Text = "—";
            // 
            // lblDepreciation
            // 
            this.lblDepreciation.AutoSize = true;
            this.lblDepreciation.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDepreciation.Location = new System.Drawing.Point(15, 495);
            this.lblDepreciation.Name = "lblDepreciation";
            this.lblDepreciation.Size = new System.Drawing.Size(185, 20);
            this.lblDepreciation.TabIndex = 15;
            this.lblDepreciation.Text = "Valor Actual Depreciado:";
            // 
            // btnSaveAsset
            // 
            this.btnSaveAsset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnSaveAsset.FlatAppearance.BorderSize = 0;
            this.btnSaveAsset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveAsset.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSaveAsset.ForeColor = System.Drawing.Color.White;
            this.btnSaveAsset.Location = new System.Drawing.Point(15, 430);
            this.btnSaveAsset.Name = "btnSaveAsset";
            this.btnSaveAsset.Size = new System.Drawing.Size(300, 40);
            this.btnSaveAsset.TabIndex = 14;
            this.btnSaveAsset.Text = "💾 Registrar Activo";
            this.btnSaveAsset.UseVisualStyleBackColor = false;
            this.btnSaveAsset.Click += new System.EventHandler(this.btnSaveAsset_Click);
            // 
            // txtUsefulLife
            // 
            this.txtUsefulLife.Location = new System.Drawing.Point(15, 380);
            this.txtUsefulLife.Name = "txtUsefulLife";
            this.txtUsefulLife.Size = new System.Drawing.Size(120, 27);
            this.txtUsefulLife.TabIndex = 13;
            this.txtUsefulLife.Text = "0";
            // 
            // lblUsefulLife
            // 
            this.lblUsefulLife.AutoSize = true;
            this.lblUsefulLife.Location = new System.Drawing.Point(15, 357);
            this.lblUsefulLife.Name = "lblUsefulLife";
            this.lblUsefulLife.Size = new System.Drawing.Size(111, 20);
            this.lblUsefulLife.TabIndex = 12;
            this.lblUsefulLife.Text = "Vida Útil (años):";
            // 
            // txtSalvage
            // 
            this.txtSalvage.Location = new System.Drawing.Point(15, 320);
            this.txtSalvage.Name = "txtSalvage";
            this.txtSalvage.Size = new System.Drawing.Size(300, 27);
            this.txtSalvage.TabIndex = 11;
            this.txtSalvage.Text = "0";
            // 
            // lblSalvage
            // 
            this.lblSalvage.AutoSize = true;
            this.lblSalvage.Location = new System.Drawing.Point(15, 297);
            this.lblSalvage.Name = "lblSalvage";
            this.lblSalvage.Size = new System.Drawing.Size(125, 20);
            this.lblSalvage.TabIndex = 10;
            this.lblSalvage.Text = "Valor Residual ($):";
            // 
            // txtPurchasePrice
            // 
            this.txtPurchasePrice.Location = new System.Drawing.Point(15, 260);
            this.txtPurchasePrice.Name = "txtPurchasePrice";
            this.txtPurchasePrice.Size = new System.Drawing.Size(300, 27);
            this.txtPurchasePrice.TabIndex = 9;
            this.txtPurchasePrice.Text = "0";
            // 
            // lblPurchasePrice
            // 
            this.lblPurchasePrice.AutoSize = true;
            this.lblPurchasePrice.Location = new System.Drawing.Point(15, 237);
            this.lblPurchasePrice.Name = "lblPurchasePrice";
            this.lblPurchasePrice.Size = new System.Drawing.Size(122, 20);
            this.lblPurchasePrice.TabIndex = 8;
            this.lblPurchasePrice.Text = "Valor Compra ($):";
            // 
            // cmbSede
            // 
            this.cmbSede.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSede.FormattingEnabled = true;
            this.cmbSede.Location = new System.Drawing.Point(15, 200);
            this.cmbSede.Name = "cmbSede";
            this.cmbSede.Size = new System.Drawing.Size(300, 28);
            this.cmbSede.TabIndex = 7;
            // 
            // lblSede
            // 
            this.lblSede.AutoSize = true;
            this.lblSede.Location = new System.Drawing.Point(15, 177);
            this.lblSede.Name = "lblSede";
            this.lblSede.Size = new System.Drawing.Size(45, 20);
            this.lblSede.TabIndex = 6;
            this.lblSede.Text = "Sede:";
            // 
            // txtBrand
            // 
            this.txtBrand.Location = new System.Drawing.Point(15, 140);
            this.txtBrand.Name = "txtBrand";
            this.txtBrand.Size = new System.Drawing.Size(300, 27);
            this.txtBrand.TabIndex = 5;
            // 
            // lblBrand
            // 
            this.lblBrand.AutoSize = true;
            this.lblBrand.Location = new System.Drawing.Point(15, 117);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(112, 20);
            this.lblBrand.TabIndex = 4;
            this.lblBrand.Text = "Marca/Modelo:";
            // 
            // cmbType
            // 
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "Laptop",
            "Desktop",
            "Mobile",
            "Vehicle",
            "Monitor",
            "Otro"});
            this.cmbType.Location = new System.Drawing.Point(15, 80);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(300, 28);
            this.cmbType.TabIndex = 3;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(15, 57);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(42, 20);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Tipo:";
            // 
            // txtSerial
            // 
            this.txtSerial.Location = new System.Drawing.Point(15, 25);
            this.txtSerial.Name = "txtSerial";
            this.txtSerial.Size = new System.Drawing.Size(300, 27);
            this.txtSerial.TabIndex = 1;
            // 
            // lblSerial
            // 
            this.lblSerial.AutoSize = true;
            this.lblSerial.Location = new System.Drawing.Point(15, 2);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new System.Drawing.Size(73, 20);
            this.lblSerial.TabIndex = 0;
            this.lblSerial.Text = "No. Serie:";
            // 
            // tabAsignacion
            // 
            this.tabAsignacion.Controls.Add(this.dgvHistory);
            this.tabAsignacion.Controls.Add(this.pnlAsignForm);
            this.tabAsignacion.Location = new System.Drawing.Point(4, 29);
            this.tabAsignacion.Name = "tabAsignacion";
            this.tabAsignacion.Padding = new System.Windows.Forms.Padding(10);
            this.tabAsignacion.Size = new System.Drawing.Size(992, 607);
            this.tabAsignacion.TabIndex = 1;
            this.tabAsignacion.Text = "Asignación de Activos";
            this.tabAsignacion.UseVisualStyleBackColor = true;
            // 
            // dgvHistory
            // 
            this.dgvHistory.AllowUserToAddRows = false;
            this.dgvHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistory.Location = new System.Drawing.Point(10, 200);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowHeadersVisible = false;
            this.dgvHistory.Size = new System.Drawing.Size(972, 397);
            this.dgvHistory.TabIndex = 1;
            // 
            // pnlAsignForm
            // 
            this.pnlAsignForm.Controls.Add(this.btnAssign);
            this.pnlAsignForm.Controls.Add(this.dtpAssign);
            this.pnlAsignForm.Controls.Add(this.lblAsign);
            this.pnlAsignForm.Controls.Add(this.txtEmpId);
            this.pnlAsignForm.Controls.Add(this.lblEmpId);
            this.pnlAsignForm.Controls.Add(this.btnSearch);
            this.pnlAsignForm.Controls.Add(this.txtAssetId);
            this.pnlAsignForm.Controls.Add(this.lblAssetId);
            this.pnlAsignForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAsignForm.Location = new System.Drawing.Point(10, 10);
            this.pnlAsignForm.Name = "pnlAsignForm";
            this.pnlAsignForm.Size = new System.Drawing.Size(972, 190);
            this.pnlAsignForm.TabIndex = 0;
            // 
            // btnAssign
            // 
            this.btnAssign.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnAssign.FlatAppearance.BorderSize = 0;
            this.btnAssign.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAssign.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAssign.ForeColor = System.Drawing.Color.White;
            this.btnAssign.Location = new System.Drawing.Point(140, 135);
            this.btnAssign.Name = "btnAssign";
            this.btnAssign.Size = new System.Drawing.Size(250, 35);
            this.btnAssign.TabIndex = 7;
            this.btnAssign.Text = "👤 Asignar a Empleado";
            this.btnAssign.UseVisualStyleBackColor = false;
            this.btnAssign.Click += new System.EventHandler(this.btnAssign_Click);
            // 
            // dtpAssign
            // 
            this.dtpAssign.Location = new System.Drawing.Point(140, 95);
            this.dtpAssign.Name = "dtpAssign";
            this.dtpAssign.Size = new System.Drawing.Size(250, 27);
            this.dtpAssign.TabIndex = 6;
            // 
            // lblAsign
            // 
            this.lblAsign.AutoSize = true;
            this.lblAsign.Location = new System.Drawing.Point(20, 100);
            this.lblAsign.Name = "lblAsign";
            this.lblAsign.Size = new System.Drawing.Size(107, 20);
            this.lblAsign.TabIndex = 5;
            this.lblAsign.Text = "Fecha Entrega:";
            // 
            // txtEmpId
            // 
            this.txtEmpId.Location = new System.Drawing.Point(140, 55);
            this.txtEmpId.Name = "txtEmpId";
            this.txtEmpId.Size = new System.Drawing.Size(120, 27);
            this.txtEmpId.TabIndex = 4;
            // 
            // lblEmpId
            // 
            this.lblEmpId.AutoSize = true;
            this.lblEmpId.Location = new System.Drawing.Point(20, 58);
            this.lblEmpId.Name = "lblEmpId";
            this.lblEmpId.Size = new System.Drawing.Size(97, 20);
            this.lblEmpId.TabIndex = 3;
            this.lblEmpId.Text = "Employee ID:";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(280, 13);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(110, 30);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "🔍 Historial";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtAssetId
            // 
            this.txtAssetId.Location = new System.Drawing.Point(140, 15);
            this.txtAssetId.Name = "txtAssetId";
            this.txtAssetId.Size = new System.Drawing.Size(120, 27);
            this.txtAssetId.TabIndex = 1;
            // 
            // lblAssetId
            // 
            this.lblAssetId.AutoSize = true;
            this.lblAssetId.Location = new System.Drawing.Point(20, 18);
            this.lblAssetId.Name = "lblAssetId";
            this.lblAssetId.Size = new System.Drawing.Size(66, 20);
            this.lblAssetId.TabIndex = 0;
            this.lblAssetId.Text = "Asset ID:";
            // 
            // tabMantenimiento
            // 
            this.tabMantenimiento.Controls.Add(this.dgvMaint);
            this.tabMantenimiento.Controls.Add(this.pnlMaintForm);
            this.tabMantenimiento.Location = new System.Drawing.Point(4, 29);
            this.tabMantenimiento.Name = "tabMantenimiento";
            this.tabMantenimiento.Padding = new System.Windows.Forms.Padding(10);
            this.tabMantenimiento.Size = new System.Drawing.Size(992, 607);
            this.tabMantenimiento.TabIndex = 2;
            this.tabMantenimiento.Text = "Mantenimiento Preventivo";
            this.tabMantenimiento.UseVisualStyleBackColor = true;
            // 
            // dgvMaint
            // 
            this.dgvMaint.AllowUserToAddRows = false;
            this.dgvMaint.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMaint.BackgroundColor = System.Drawing.Color.White;
            this.dgvMaint.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMaint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMaint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMaint.Location = new System.Drawing.Point(10, 200);
            this.dgvMaint.Name = "dgvMaint";
            this.dgvMaint.ReadOnly = true;
            this.dgvMaint.RowHeadersVisible = false;
            this.dgvMaint.Size = new System.Drawing.Size(972, 397);
            this.dgvMaint.TabIndex = 1;
            // 
            // pnlMaintForm
            // 
            this.pnlMaintForm.Controls.Add(this.btnLoadMaint);
            this.pnlMaintForm.Controls.Add(this.btnScheduleMaint);
            this.pnlMaintForm.Controls.Add(this.dtpMaint);
            this.pnlMaintForm.Controls.Add(this.lblMaintDate);
            this.pnlMaintForm.Controls.Add(this.txtMaintDesc);
            this.pnlMaintForm.Controls.Add(this.lblMaintDesc);
            this.pnlMaintForm.Controls.Add(this.txtMaintAssetId);
            this.pnlMaintForm.Controls.Add(this.lblMaintAssetId);
            this.pnlMaintForm.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMaintForm.Location = new System.Drawing.Point(10, 10);
            this.pnlMaintForm.Name = "pnlMaintForm";
            this.pnlMaintForm.Size = new System.Drawing.Size(972, 190);
            this.pnlMaintForm.TabIndex = 0;
            // 
            // btnLoadMaint
            // 
            this.btnLoadMaint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnLoadMaint.FlatAppearance.BorderSize = 0;
            this.btnLoadMaint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadMaint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLoadMaint.ForeColor = System.Drawing.Color.White;
            this.btnLoadMaint.Location = new System.Drawing.Point(280, 135);
            this.btnLoadMaint.Name = "btnLoadMaint";
            this.btnLoadMaint.Size = new System.Drawing.Size(130, 35);
            this.btnLoadMaint.TabIndex = 7;
            this.btnLoadMaint.Text = "📋 Ver Historial";
            this.btnLoadMaint.UseVisualStyleBackColor = false;
            this.btnLoadMaint.Click += new System.EventHandler(this.btnLoadMaint_Click);
            // 
            // btnScheduleMaint
            // 
            this.btnScheduleMaint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnScheduleMaint.FlatAppearance.BorderSize = 0;
            this.btnScheduleMaint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScheduleMaint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnScheduleMaint.ForeColor = System.Drawing.Color.White;
            this.btnScheduleMaint.Location = new System.Drawing.Point(140, 135);
            this.btnScheduleMaint.Name = "btnScheduleMaint";
            this.btnScheduleMaint.Size = new System.Drawing.Size(130, 35);
            this.btnScheduleMaint.TabIndex = 6;
            this.btnScheduleMaint.Text = "📅 Programar";
            this.btnScheduleMaint.UseVisualStyleBackColor = false;
            this.btnScheduleMaint.Click += new System.EventHandler(this.btnScheduleMaint_Click);
            // 
            // dtpMaint
            // 
            this.dtpMaint.Location = new System.Drawing.Point(140, 95);
            this.dtpMaint.Name = "dtpMaint";
            this.dtpMaint.Size = new System.Drawing.Size(250, 27);
            this.dtpMaint.TabIndex = 5;
            // 
            // lblMaintDate
            // 
            this.lblMaintDate.AutoSize = true;
            this.lblMaintDate.Location = new System.Drawing.Point(20, 100);
            this.lblMaintDate.Name = "lblMaintDate";
            this.lblMaintDate.Size = new System.Drawing.Size(117, 20);
            this.lblMaintDate.TabIndex = 4;
            this.lblMaintDate.Text = "Fecha Prog:";
            // 
            // txtMaintDesc
            // 
            this.txtMaintDesc.Location = new System.Drawing.Point(140, 55);
            this.txtMaintDesc.Name = "txtMaintDesc";
            this.txtMaintDesc.Size = new System.Drawing.Size(450, 27);
            this.txtMaintDesc.TabIndex = 3;
            // 
            // lblMaintDesc
            // 
            this.lblMaintDesc.AutoSize = true;
            this.lblMaintDesc.Location = new System.Drawing.Point(20, 58);
            this.lblMaintDesc.Name = "lblMaintDesc";
            this.lblMaintDesc.Size = new System.Drawing.Size(90, 20);
            this.lblMaintDesc.TabIndex = 2;
            this.lblMaintDesc.Text = "Descripción:";
            // 
            // txtMaintAssetId
            // 
            this.txtMaintAssetId.Location = new System.Drawing.Point(140, 15);
            this.txtMaintAssetId.Name = "txtMaintAssetId";
            this.txtMaintAssetId.Size = new System.Drawing.Size(120, 27);
            this.txtMaintAssetId.TabIndex = 1;
            // 
            // lblMaintAssetId
            // 
            this.lblMaintAssetId.AutoSize = true;
            this.lblMaintAssetId.Location = new System.Drawing.Point(20, 18);
            this.lblMaintAssetId.Name = "lblMaintAssetId";
            this.lblMaintAssetId.Size = new System.Drawing.Size(66, 20);
            this.lblMaintAssetId.TabIndex = 0;
            this.lblMaintAssetId.Text = "Asset ID:";
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1000, 60);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(425, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Gestión de Activos Fijos — FASE 3";
            // 
            // frmAssets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlHeader);
            this.Name = "frmAssets";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Activos Fijos — MarkErp";
            this.tabControl.ResumeLayout(false);
            this.tabActivos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivos)).EndInit();
            this.pnlAssetForm.ResumeLayout(false);
            this.pnlAssetForm.PerformLayout();
            this.tabAsignacion.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.pnlAsignForm.ResumeLayout(false);
            this.pnlAsignForm.PerformLayout();
            this.tabMantenimiento.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaint)).EndInit();
            this.pnlMaintForm.ResumeLayout(false);
            this.pnlMaintForm.PerformLayout();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabActivos, tabAsignacion, tabMantenimiento;
        private System.Windows.Forms.Label lblSerial, lblType, lblBrand, lblSede, lblPurchasePrice, lblSalvage, lblUsefulLife;
        private System.Windows.Forms.TextBox txtSerial, txtBrand, txtPurchasePrice, txtSalvage, txtUsefulLife;
        private System.Windows.Forms.ComboBox cmbType, cmbSede;
        private System.Windows.Forms.Button btnSaveAsset;
        private System.Windows.Forms.DataGridView dgvActivos;
        private System.Windows.Forms.Label lblDepreciation, lblDepreciationValue;
        private System.Windows.Forms.Label lblAssetId, lblEmpId, lblAsign;
        private System.Windows.Forms.TextBox txtAssetId, txtEmpId;
        private System.Windows.Forms.DateTimePicker dtpAssign;
        private System.Windows.Forms.Button btnSearch, btnAssign;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Label lblMaintAssetId, lblMaintDesc, lblMaintDate;
        private System.Windows.Forms.TextBox txtMaintAssetId, txtMaintDesc;
        private System.Windows.Forms.DateTimePicker dtpMaint;
        private System.Windows.Forms.Button btnScheduleMaint, btnLoadMaint;
        private System.Windows.Forms.DataGridView dgvMaint;
        private System.Windows.Forms.Panel pnlHeader, pnlAssetForm, pnlAsignForm, pnlMaintForm;
        private System.Windows.Forms.Label lblTitle;
    }
}
