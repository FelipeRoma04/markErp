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
            this.SuspendLayout();

            this.lblInvoice.Location = new System.Drawing.Point(30, 30);
            this.lblInvoice.Text = "Invoice ID:";
            this.txtInvoiceId.Location = new System.Drawing.Point(150, 30);

            this.lblAmount.Location = new System.Drawing.Point(30, 70);
            this.lblAmount.Text = "Monto Abonado:";
            this.txtAmount.Location = new System.Drawing.Point(150, 70);

            this.lblMethod.Location = new System.Drawing.Point(30, 110);
            this.lblMethod.Text = "Método de Pago:";
            this.cmbMethod.Location = new System.Drawing.Point(150, 110);
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
            this.lblDebt.Size = new System.Drawing.Size(280, 20);
            this.lblDebt.Text = "Pendiente factura: -";

            this.ClientSize = new System.Drawing.Size(350, 230);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtInvoiceId);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.cmbMethod);
            this.Controls.Add(this.lblInvoice);
            this.Controls.Add(this.lblAmount);
            this.Controls.Add(this.lblMethod);
            this.Controls.Add(this.lblDebt);
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
    }
}
