namespace Proyecto.View
{
    partial class frmAudit
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblFilter    = new System.Windows.Forms.Label();
            this.cmbTable     = new System.Windows.Forms.ComboBox();
            this.lblAction    = new System.Windows.Forms.Label();
            this.cmbAction    = new System.Windows.Forms.ComboBox();
            this.lblFrom      = new System.Windows.Forms.Label();
            this.dtpFrom      = new System.Windows.Forms.DateTimePicker();
            this.btnFilter    = new System.Windows.Forms.Button();
            this.btnExport    = new System.Windows.Forms.Button();
            this.dgvLogs      = new System.Windows.Forms.DataGridView();
            this.lblCount     = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).BeginInit();
            this.SuspendLayout();

            // lblFilter
            this.lblFilter.Location = new System.Drawing.Point(15, 15);
            this.lblFilter.Text = "Módulo:"; this.lblFilter.AutoSize = true;

            // cmbTable
            this.cmbTable.Location = new System.Drawing.Point(80, 12);
            this.cmbTable.Size = new System.Drawing.Size(150, 24);
            this.cmbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTable.Items.AddRange(new object[] { "(Todos)", "Assets", "Empleados", "JournalEntries", "Payroll_Log" });
            this.cmbTable.SelectedIndex = 0;

            // lblAction
            this.lblAction.Location = new System.Drawing.Point(245, 15);
            this.lblAction.Text = "Acción:"; this.lblAction.AutoSize = true;

            // cmbAction
            this.cmbAction.Location = new System.Drawing.Point(300, 12);
            this.cmbAction.Size = new System.Drawing.Size(120, 24);
            this.cmbAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAction.Items.AddRange(new object[] { "(Todas)", "CREATE", "UPDATE", "DELETE" });
            this.cmbAction.SelectedIndex = 0;

            // lblFrom
            this.lblFrom.Location = new System.Drawing.Point(435, 15);
            this.lblFrom.Text = "Desde:"; this.lblFrom.AutoSize = true;

            // dtpFrom
            this.dtpFrom.Location = new System.Drawing.Point(485, 12);
            this.dtpFrom.Size = new System.Drawing.Size(150, 24);
            this.dtpFrom.Value = System.DateTime.Today.AddMonths(-1);

            // btnFilter
            this.btnFilter.Location = new System.Drawing.Point(650, 10);
            this.btnFilter.Size = new System.Drawing.Size(110, 28);
            this.btnFilter.Text = "🔍 Filtrar";
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);

            // btnExport
            this.btnExport.Location = new System.Drawing.Point(770, 10);
            this.btnExport.Size = new System.Drawing.Size(110, 28);
            this.btnExport.Text = "📊 Exportar CSV";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);

            // dgvLogs
            this.dgvLogs.Location = new System.Drawing.Point(15, 50);
            this.dgvLogs.Size = new System.Drawing.Size(865, 450);
            this.dgvLogs.ReadOnly = true;
            this.dgvLogs.AllowUserToAddRows = false;
            this.dgvLogs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // lblCount
            this.lblCount.Location = new System.Drawing.Point(15, 508);
            this.lblCount.AutoSize = true;
            this.lblCount.Text = "Total registros: 0";

            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                lblFilter, cmbTable, lblAction, cmbAction,
                lblFrom, dtpFrom, btnFilter, btnExport,
                dgvLogs, lblCount
            });

            this.ClientSize = new System.Drawing.Size(895, 535);
            this.Text = "Auditoría — Historial de Cambios";
            this.Name = "frmAudit";

            ((System.ComponentModel.ISupportInitialize)(this.dgvLogs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.ComboBox cmbTable;
        private System.Windows.Forms.Label lblAction;
        private System.Windows.Forms.ComboBox cmbAction;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.DataGridView dgvLogs;
        private System.Windows.Forms.Label lblCount;
    }
}
