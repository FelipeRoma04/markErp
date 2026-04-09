namespace Proyecto.View
{
    partial class frmPayments
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
            this.btnSave = new System.Windows.Forms.Button();
            this.txtInvoiceId = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.cmbMethod = new System.Windows.Forms.ComboBox();
            this.lblDebt = new System.Windows.Forms.Label();
            
            this.lblInvoice = new System.Windows.Forms.Label();
            this.lblAmount = new System.Windows.Forms.Label();
            this.lblMethod = new System.Windows.Forms.Label();
            
            // Task 30: Add payment history controls
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPayment = new System.Windows.Forms.TabPage();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.dgvPaymentHistory = new System.Windows.Forms.DataGridView();
            this.lblHistoryDebt = new System.Windows.Forms.Label();
            
            this.SuspendLayout();

            // Tab Control
            this.tabMain.Location = new System.Drawing.Point(10, 10);
            this.tabMain.Size = new System.Drawing.Size(600, 400);
            this.tabMain.TabIndex = 0;
            
            // Tab 1: Payment Entry
            this.tabPayment.Text = "Registrar Pago";
            this.tabPayment.Location = new System.Drawing.Point(4, 22);
            this.tabPayment.Size = new System.Drawing.Size(592, 374);
            this.tabPayment.Controls.Add(this.lblInvoice);
            this.tabPayment.Controls.Add(this.txtInvoiceId);
            this.tabPayment.Controls.Add(this.lblAmount);
            this.tabPayment.Controls.Add(this.txtAmount);
            this.tabPayment.Controls.Add(this.lblMethod);
            this.tabPayment.Controls.Add(this.cmbMethod);
            this.tabPayment.Controls.Add(this.btnSave);
            this.tabPayment.Controls.Add(this.lblDebt);

            this.lblInvoice.Location = new System.Drawing.Point(30, 30);
            this.lblInvoice.Text = "Invoice ID:";
            this.lblInvoice.AutoSize = true;
            this.txtInvoiceId.Location = new System.Drawing.Point(150, 30);
            this.txtInvoiceId.Size = new System.Drawing.Size(100, 20);
            this.txtInvoiceId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInvoiceId_KeyDown);

            this.lblAmount.Location = new System.Drawing.Point(30, 70);
            this.lblAmount.Text = "Monto Abonado:";
            this.lblAmount.AutoSize = true;
            this.txtAmount.Location = new System.Drawing.Point(150, 70);
            this.txtAmount.Size = new System.Drawing.Size(100, 20);

            this.lblMethod.Location = new System.Drawing.Point(30, 110);
            this.lblMethod.Text = "Método de Pago:";
            this.lblMethod.AutoSize = true;
            this.cmbMethod.Location = new System.Drawing.Point(150, 110);
            this.cmbMethod.Size = new System.Drawing.Size(200, 20);
            this.cmbMethod.Items.AddRange(new object[] {
            "Transferencia Bancaria",
            "Efectivo",
            "Tarjeta de Crédito",
            "Cheque"});
            this.cmbMethod.SelectedIndex = 0;

            this.btnSave.Location = new System.Drawing.Point(150, 160);
            this.btnSave.Text = "Registrar Pago";
            this.btnSave.Size = new System.Drawing.Size(120, 30);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.lblDebt.Location = new System.Drawing.Point(30, 200);
            this.lblDebt.Size = new System.Drawing.Size(400, 40);
            this.lblDebt.AutoSize = false;
            this.lblDebt.Text = "Pendiente factura: -";

            // Tab 2: Payment History
            this.tabHistory.Text = "Historial de Abonos";
            this.tabHistory.Location = new System.Drawing.Point(4, 22);
            this.tabHistory.Size = new System.Drawing.Size(592, 374);
            this.tabHistory.Controls.Add(this.dgvPaymentHistory);
            this.tabHistory.Controls.Add(this.lblHistoryDebt);

            this.dgvPaymentHistory.Location = new System.Drawing.Point(10, 10);
            this.dgvPaymentHistory.Size = new System.Drawing.Size(570, 300);
            this.dgvPaymentHistory.ReadOnly = true;
            this.dgvPaymentHistory.AllowUserToAddRows = false;
            this.dgvPaymentHistory.AllowUserToDeleteRows = false;
            this.dgvPaymentHistory.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.SystemColors.Control;
            this.dgvPaymentHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;

            this.lblHistoryDebt.Location = new System.Drawing.Point(10, 320);
            this.lblHistoryDebt.Size = new System.Drawing.Size(570, 40);
            this.lblHistoryDebt.AutoSize = false;
            this.lblHistoryDebt.Text = "Total Pendiente: -";
            this.lblHistoryDebt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            this.tabMain.TabPages.Add(this.tabPayment);
            this.tabMain.TabPages.Add(this.tabHistory);

            this.ClientSize = new System.Drawing.Size(620, 430);
            this.Controls.Add(this.tabMain);
            this.Text = "Cuentas por Cobrar";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtInvoiceId;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.ComboBox cmbMethod;
        private System.Windows.Forms.Label lblInvoice;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblMethod;
        private System.Windows.Forms.Label lblDebt;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPayment;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.DataGridView dgvPaymentHistory;
        private System.Windows.Forms.Label lblHistoryDebt;
    }
}
