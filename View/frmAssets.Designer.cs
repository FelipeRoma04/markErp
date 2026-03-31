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
            this.tabAsignacion = new System.Windows.Forms.TabPage();
            this.tabMantenimiento = new System.Windows.Forms.TabPage();
            
            // --- Tab 1: Registro de Activos ---
            this.lblSerial = new System.Windows.Forms.Label();
            this.txtSerial = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.lblBrand = new System.Windows.Forms.Label();
            this.txtBrand = new System.Windows.Forms.TextBox();
            this.lblSede = new System.Windows.Forms.Label();
            this.cmbSede = new System.Windows.Forms.ComboBox();
            this.lblPurchasePrice = new System.Windows.Forms.Label();
            this.txtPurchasePrice = new System.Windows.Forms.TextBox();
            this.lblSalvage = new System.Windows.Forms.Label();
            this.txtSalvage = new System.Windows.Forms.TextBox();
            this.lblUsefulLife = new System.Windows.Forms.Label();
            this.txtUsefulLife = new System.Windows.Forms.TextBox();
            this.btnSaveAsset = new System.Windows.Forms.Button();
            this.dgvActivos = new System.Windows.Forms.DataGridView();
            this.lblDepreciation = new System.Windows.Forms.Label();
            this.lblDepreciationValue = new System.Windows.Forms.Label();

            // --- Tab 2: Asignación ---
            this.lblAssetId = new System.Windows.Forms.Label();
            this.txtAssetId = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblEmpId = new System.Windows.Forms.Label();
            this.txtEmpId = new System.Windows.Forms.TextBox();
            this.lblAsign = new System.Windows.Forms.Label();
            this.dtpAssign = new System.Windows.Forms.DateTimePicker();
            this.btnAssign = new System.Windows.Forms.Button();
            this.dgvHistory = new System.Windows.Forms.DataGridView();

            // --- Tab 3: Mantenimiento ---
            this.lblMaintAssetId = new System.Windows.Forms.Label();
            this.txtMaintAssetId = new System.Windows.Forms.TextBox();
            this.lblMaintDesc = new System.Windows.Forms.Label();
            this.txtMaintDesc = new System.Windows.Forms.TextBox();
            this.lblMaintDate = new System.Windows.Forms.Label();
            this.dtpMaint = new System.Windows.Forms.DateTimePicker();
            this.btnScheduleMaint = new System.Windows.Forms.Button();
            this.btnLoadMaint = new System.Windows.Forms.Button();
            this.dgvMaint = new System.Windows.Forms.DataGridView();

            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaint)).BeginInit();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();

            // ===================== TAB CONTROL =====================
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Size = new System.Drawing.Size(860, 520);
            this.tabControl.Controls.Add(this.tabActivos);
            this.tabControl.Controls.Add(this.tabAsignacion);
            this.tabControl.Controls.Add(this.tabMantenimiento);

            // ===================== TAB 1 ACTIVOS =====================
            this.tabActivos.Text = "Registro de Activos";
            this.tabActivos.BackColor = System.Drawing.SystemColors.Control;

            int y = 20;
            this.lblSerial.Location = new System.Drawing.Point(20, y); this.lblSerial.Text = "No. Serie:"; this.lblSerial.AutoSize = true;
            this.txtSerial.Location = new System.Drawing.Point(150, y); this.txtSerial.Size = new System.Drawing.Size(180, 22);

            y += 40;
            this.lblType.Location = new System.Drawing.Point(20, y); this.lblType.Text = "Tipo:"; this.lblType.AutoSize = true;
            this.cmbType.Location = new System.Drawing.Point(150, y); this.cmbType.Size = new System.Drawing.Size(180, 22);
            this.cmbType.Items.AddRange(new object[] { "Laptop", "Desktop", "Mobile", "Vehicle", "Monitor", "Otro" });
            this.cmbType.SelectedIndex = 0;

            y += 40;
            this.lblBrand.Location = new System.Drawing.Point(20, y); this.lblBrand.Text = "Marca/Modelo:"; this.lblBrand.AutoSize = true;
            this.txtBrand.Location = new System.Drawing.Point(150, y); this.txtBrand.Size = new System.Drawing.Size(180, 22);

            y += 40;
            this.lblSede.Location = new System.Drawing.Point(20, y); this.lblSede.Text = "Sede:"; this.lblSede.AutoSize = true;
            this.cmbSede.Location = new System.Drawing.Point(150, y); this.cmbSede.Size = new System.Drawing.Size(180, 22);
            this.cmbSede.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            y += 40;
            this.lblPurchasePrice.Location = new System.Drawing.Point(20, y); this.lblPurchasePrice.Text = "Valor Compra ($):"; this.lblPurchasePrice.AutoSize = true;
            this.txtPurchasePrice.Location = new System.Drawing.Point(150, y); this.txtPurchasePrice.Size = new System.Drawing.Size(120, 22); this.txtPurchasePrice.Text = "0";

            y += 40;
            this.lblSalvage.Location = new System.Drawing.Point(20, y); this.lblSalvage.Text = "Valor Residual ($):"; this.lblSalvage.AutoSize = true;
            this.txtSalvage.Location = new System.Drawing.Point(150, y); this.txtSalvage.Size = new System.Drawing.Size(120, 22); this.txtSalvage.Text = "0";

            y += 40;
            this.lblUsefulLife.Location = new System.Drawing.Point(20, y); this.lblUsefulLife.Text = "Vida Útil (años):"; this.lblUsefulLife.AutoSize = true;
            this.txtUsefulLife.Location = new System.Drawing.Point(150, y); this.txtUsefulLife.Size = new System.Drawing.Size(80, 22); this.txtUsefulLife.Text = "0";

            y += 50;
            this.btnSaveAsset.Location = new System.Drawing.Point(150, y);
            this.btnSaveAsset.Text = "💾 Registrar Activo"; this.btnSaveAsset.Size = new System.Drawing.Size(160, 30);
            this.btnSaveAsset.Click += new System.EventHandler(this.btnSaveAsset_Click);

            y += 45;
            this.lblDepreciation.Location = new System.Drawing.Point(20, y); this.lblDepreciation.Text = "Valor Actual Depreciado:"; this.lblDepreciation.AutoSize = true;
            this.lblDepreciationValue.Location = new System.Drawing.Point(180, y); this.lblDepreciationValue.Text = "—"; 
            this.lblDepreciationValue.Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold);
            this.lblDepreciationValue.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblDepreciationValue.AutoSize = true;

            this.dgvActivos.Location = new System.Drawing.Point(370, 20);
            this.dgvActivos.Size = new System.Drawing.Size(460, 450);
            this.dgvActivos.ReadOnly = true;
            this.dgvActivos.AllowUserToAddRows = false;
            this.dgvActivos.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvActivos_CellMouseClick);

            this.tabActivos.Controls.Add(this.lblSerial); this.tabActivos.Controls.Add(this.txtSerial);
            this.tabActivos.Controls.Add(this.lblType); this.tabActivos.Controls.Add(this.cmbType);
            this.tabActivos.Controls.Add(this.lblBrand); this.tabActivos.Controls.Add(this.txtBrand);
            this.tabActivos.Controls.Add(this.lblSede); this.tabActivos.Controls.Add(this.cmbSede);
            this.tabActivos.Controls.Add(this.lblPurchasePrice); this.tabActivos.Controls.Add(this.txtPurchasePrice);
            this.tabActivos.Controls.Add(this.lblSalvage); this.tabActivos.Controls.Add(this.txtSalvage);
            this.tabActivos.Controls.Add(this.lblUsefulLife); this.tabActivos.Controls.Add(this.txtUsefulLife);
            this.tabActivos.Controls.Add(this.btnSaveAsset);
            this.tabActivos.Controls.Add(this.lblDepreciation); this.tabActivos.Controls.Add(this.lblDepreciationValue);
            this.tabActivos.Controls.Add(this.dgvActivos);

            // ===================== TAB 2 ASIGNACION =====================
            this.tabAsignacion.Text = "Asignación de Activos";
            this.tabAsignacion.BackColor = System.Drawing.SystemColors.Control;

            y = 20;
            this.lblAssetId.Location = new System.Drawing.Point(20, y); this.lblAssetId.Text = "Asset ID:"; this.lblAssetId.AutoSize = true;
            this.txtAssetId.Location = new System.Drawing.Point(140, y); this.txtAssetId.Size = new System.Drawing.Size(100, 22);
            this.btnSearch.Location = new System.Drawing.Point(250, y); this.btnSearch.Text = "🔍 Historial"; this.btnSearch.Size = new System.Drawing.Size(120, 26);
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);

            y += 45;
            this.lblEmpId.Location = new System.Drawing.Point(20, y); this.lblEmpId.Text = "Employee ID:"; this.lblEmpId.AutoSize = true;
            this.txtEmpId.Location = new System.Drawing.Point(140, y); this.txtEmpId.Size = new System.Drawing.Size(100, 22);

            y += 40;
            this.lblAsign.Location = new System.Drawing.Point(20, y); this.lblAsign.Text = "Fecha Entrega:"; this.lblAsign.AutoSize = true;
            this.dtpAssign.Location = new System.Drawing.Point(140, y); this.dtpAssign.Size = new System.Drawing.Size(200, 22);

            y += 50;
            this.btnAssign.Location = new System.Drawing.Point(140, y); this.btnAssign.Text = "👤 Asignar a Empleado"; this.btnAssign.Size = new System.Drawing.Size(180, 30);
            this.btnAssign.Click += new System.EventHandler(this.btnAssign_Click);

            this.dgvHistory.Location = new System.Drawing.Point(20, 200);
            this.dgvHistory.Size = new System.Drawing.Size(800, 260);
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.AllowUserToAddRows = false;

            this.tabAsignacion.Controls.Add(this.lblAssetId); this.tabAsignacion.Controls.Add(this.txtAssetId);
            this.tabAsignacion.Controls.Add(this.btnSearch);
            this.tabAsignacion.Controls.Add(this.lblEmpId); this.tabAsignacion.Controls.Add(this.txtEmpId);
            this.tabAsignacion.Controls.Add(this.lblAsign); this.tabAsignacion.Controls.Add(this.dtpAssign);
            this.tabAsignacion.Controls.Add(this.btnAssign);
            this.tabAsignacion.Controls.Add(this.dgvHistory);

            // ===================== TAB 3 MANTENIMIENTO =====================
            this.tabMantenimiento.Text = "Mantenimiento Preventivo";
            this.tabMantenimiento.BackColor = System.Drawing.SystemColors.Control;

            y = 20;
            this.lblMaintAssetId.Location = new System.Drawing.Point(20, y); this.lblMaintAssetId.Text = "Asset ID:"; this.lblMaintAssetId.AutoSize = true;
            this.txtMaintAssetId.Location = new System.Drawing.Point(140, y); this.txtMaintAssetId.Size = new System.Drawing.Size(100, 22);

            y += 40;
            this.lblMaintDesc.Location = new System.Drawing.Point(20, y); this.lblMaintDesc.Text = "Descripción:"; this.lblMaintDesc.AutoSize = true;
            this.txtMaintDesc.Location = new System.Drawing.Point(140, y); this.txtMaintDesc.Size = new System.Drawing.Size(320, 22);

            y += 40;
            this.lblMaintDate.Location = new System.Drawing.Point(20, y); this.lblMaintDate.Text = "Fecha Programada:"; this.lblMaintDate.AutoSize = true;
            this.dtpMaint.Location = new System.Drawing.Point(140, y); this.dtpMaint.Size = new System.Drawing.Size(200, 22);

            y += 50;
            this.btnScheduleMaint.Location = new System.Drawing.Point(140, y); this.btnScheduleMaint.Text = "📅 Programar"; this.btnScheduleMaint.Size = new System.Drawing.Size(130, 30);
            this.btnScheduleMaint.Click += new System.EventHandler(this.btnScheduleMaint_Click);
            this.btnLoadMaint.Location = new System.Drawing.Point(285, y); this.btnLoadMaint.Text = "📋 Ver Historial"; this.btnLoadMaint.Size = new System.Drawing.Size(130, 30);
            this.btnLoadMaint.Click += new System.EventHandler(this.btnLoadMaint_Click);

            this.dgvMaint.Location = new System.Drawing.Point(20, 190);
            this.dgvMaint.Size = new System.Drawing.Size(800, 270);
            this.dgvMaint.ReadOnly = true;
            this.dgvMaint.AllowUserToAddRows = false;

            this.tabMantenimiento.Controls.Add(this.lblMaintAssetId); this.tabMantenimiento.Controls.Add(this.txtMaintAssetId);
            this.tabMantenimiento.Controls.Add(this.lblMaintDesc); this.tabMantenimiento.Controls.Add(this.txtMaintDesc);
            this.tabMantenimiento.Controls.Add(this.lblMaintDate); this.tabMantenimiento.Controls.Add(this.dtpMaint);
            this.tabMantenimiento.Controls.Add(this.btnScheduleMaint);
            this.tabMantenimiento.Controls.Add(this.btnLoadMaint);
            this.tabMantenimiento.Controls.Add(this.dgvMaint);

            // ===================== MAIN FORM =====================
            this.Controls.Add(this.tabControl);
            this.ClientSize = new System.Drawing.Size(860, 555);
            this.Text = "Gestión de Activos Fijos — FASE 3";
            this.Name = "frmAssets";

            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvActivos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMaint)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Tab controls
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabActivos;
        private System.Windows.Forms.TabPage tabAsignacion;
        private System.Windows.Forms.TabPage tabMantenimiento;

        // Tab 1 — Asset Registration
        private System.Windows.Forms.Label lblSerial;
        private System.Windows.Forms.TextBox txtSerial;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.TextBox txtBrand;
        private System.Windows.Forms.Label lblSede;
        private System.Windows.Forms.ComboBox cmbSede;
        private System.Windows.Forms.Label lblPurchasePrice;
        private System.Windows.Forms.TextBox txtPurchasePrice;
        private System.Windows.Forms.Label lblSalvage;
        private System.Windows.Forms.TextBox txtSalvage;
        private System.Windows.Forms.Label lblUsefulLife;
        private System.Windows.Forms.TextBox txtUsefulLife;
        private System.Windows.Forms.Button btnSaveAsset;
        private System.Windows.Forms.DataGridView dgvActivos;
        private System.Windows.Forms.Label lblDepreciation;
        private System.Windows.Forms.Label lblDepreciationValue;

        // Tab 2 — Assignment
        private System.Windows.Forms.Label lblAssetId;
        private System.Windows.Forms.TextBox txtAssetId;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblEmpId;
        private System.Windows.Forms.TextBox txtEmpId;
        private System.Windows.Forms.Label lblAsign;
        private System.Windows.Forms.DateTimePicker dtpAssign;
        private System.Windows.Forms.Button btnAssign;
        private System.Windows.Forms.DataGridView dgvHistory;

        // Tab 3 — Maintenance
        private System.Windows.Forms.Label lblMaintAssetId;
        private System.Windows.Forms.TextBox txtMaintAssetId;
        private System.Windows.Forms.Label lblMaintDesc;
        private System.Windows.Forms.TextBox txtMaintDesc;
        private System.Windows.Forms.Label lblMaintDate;
        private System.Windows.Forms.DateTimePicker dtpMaint;
        private System.Windows.Forms.Button btnScheduleMaint;
        private System.Windows.Forms.Button btnLoadMaint;
        private System.Windows.Forms.DataGridView dgvMaint;
    }
}
