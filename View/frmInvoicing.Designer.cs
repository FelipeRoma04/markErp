namespace Proyecto.View
{
    partial class frmInvoicing
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
            this.btnCalc = new System.Windows.Forms.Button();
            this.txtQuoteId = new System.Windows.Forms.TextBox();
            this.txtClientId = new System.Windows.Forms.TextBox();
            this.dtpIssue = new System.Windows.Forms.DateTimePicker();
            this.dtpDue = new System.Windows.Forms.DateTimePicker();
            this.txtSubtotal = new System.Windows.Forms.TextBox();
            this.txtTax = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            
            this.lblQuote = new System.Windows.Forms.Label();
            this.lblClient = new System.Windows.Forms.Label();
            this.lblIssue = new System.Windows.Forms.Label();
            this.lblDue = new System.Windows.Forms.Label();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblTax = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.lblQuote.Location = new System.Drawing.Point(30, 30);
            this.lblQuote.Text = "Ref Quote ID:";
            this.txtQuoteId.Location = new System.Drawing.Point(150, 30);

            this.lblClient.Location = new System.Drawing.Point(30, 70);
            this.lblClient.Text = "Client ID:";
            this.txtClientId.Location = new System.Drawing.Point(150, 70);

            this.lblIssue.Location = new System.Drawing.Point(30, 110);
            this.lblIssue.Text = "Misión Factura:";
            this.dtpIssue.Location = new System.Drawing.Point(150, 110);

            this.lblDue.Location = new System.Drawing.Point(30, 150);
            this.lblDue.Text = "Vencimiento:";
            this.dtpDue.Location = new System.Drawing.Point(150, 150);

            this.lblSubtotal.Location = new System.Drawing.Point(30, 190);
            this.lblSubtotal.Text = "Subtotal Base:";
            this.txtSubtotal.Location = new System.Drawing.Point(150, 190);
            this.txtSubtotal.Text = "0.00";

            this.btnCalc.Location = new System.Drawing.Point(30, 230);
            this.btnCalc.Text = "Calcular Impuestos";
            this.btnCalc.Size = new System.Drawing.Size(120, 30);
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);

            this.lblTax.Location = new System.Drawing.Point(180, 230);
            this.lblTax.Text = "IVA (19%):";
            this.txtTax.Location = new System.Drawing.Point(260, 230);
            this.txtTax.ReadOnly = true;

            this.lblTotal.Location = new System.Drawing.Point(180, 260);
            this.lblTotal.Text = "Total Factura:";
            this.txtTotal.Location = new System.Drawing.Point(260, 260);
            this.txtTotal.ReadOnly = true;

            this.btnSave.Location = new System.Drawing.Point(150, 300);
            this.btnSave.Text = "Emitir Factura DIAN";
            this.btnSave.Size = new System.Drawing.Size(150, 30);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.ClientSize = new System.Drawing.Size(430, 360);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.txtQuoteId);
            this.Controls.Add(this.txtClientId);
            this.Controls.Add(this.dtpIssue);
            this.Controls.Add(this.dtpDue);
            this.Controls.Add(this.txtSubtotal);
            this.Controls.Add(this.txtTax);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.lblQuote);
            this.Controls.Add(this.lblClient);
            this.Controls.Add(this.lblIssue);
            this.Controls.Add(this.lblDue);
            this.Controls.Add(this.lblSubtotal);
            this.Controls.Add(this.lblTax);
            this.Controls.Add(this.lblTotal);
            this.Text = "Facturación";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.TextBox txtQuoteId;
        private System.Windows.Forms.TextBox txtClientId;
        private System.Windows.Forms.DateTimePicker dtpIssue;
        private System.Windows.Forms.DateTimePicker dtpDue;
        private System.Windows.Forms.TextBox txtSubtotal;
        private System.Windows.Forms.TextBox txtTax;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblQuote;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.Label lblIssue;
        private System.Windows.Forms.Label lblDue;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblTax;
        private System.Windows.Forms.Label lblTotal;
    }
}
