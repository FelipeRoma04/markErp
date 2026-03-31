namespace Proyecto.View
{
    partial class frmQuotes
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
            this.txtClientId = new System.Windows.Forms.TextBox();
            this.dtpIssue = new System.Windows.Forms.DateTimePicker();
            this.dtpExpire = new System.Windows.Forms.DateTimePicker();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lblClient = new System.Windows.Forms.Label();
            this.lblIssue = new System.Windows.Forms.Label();
            this.lblExpire = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.lblClient.Location = new System.Drawing.Point(30, 30);
            this.lblClient.Text = "Client ID:";
            this.txtClientId.Location = new System.Drawing.Point(150, 30);

            this.lblIssue.Location = new System.Drawing.Point(30, 70);
            this.lblIssue.Text = "Issue Date:";
            this.dtpIssue.Location = new System.Drawing.Point(150, 70);

            this.lblExpire.Location = new System.Drawing.Point(30, 110);
            this.lblExpire.Text = "Expiration Date:";
            this.dtpExpire.Location = new System.Drawing.Point(150, 110);

            this.lblTotal.Location = new System.Drawing.Point(30, 150);
            this.lblTotal.Text = "Subtotal (Sin IVA):";
            this.txtTotal.Location = new System.Drawing.Point(150, 150);
            this.txtTotal.Text = "0.00";

            this.btnSave.Location = new System.Drawing.Point(150, 200);
            this.btnSave.Text = "Guardar Cotización";
            this.btnSave.Size = new System.Drawing.Size(120, 30);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.ClientSize = new System.Drawing.Size(400, 280);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtClientId);
            this.Controls.Add(this.dtpIssue);
            this.Controls.Add(this.dtpExpire);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.lblClient);
            this.Controls.Add(this.lblIssue);
            this.Controls.Add(this.lblExpire);
            this.Controls.Add(this.lblTotal);
            this.Text = "Gestor de Cotizaciones";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtClientId;
        private System.Windows.Forms.DateTimePicker dtpIssue;
        private System.Windows.Forms.DateTimePicker dtpExpire;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.Label lblIssue;
        private System.Windows.Forms.Label lblExpire;
        private System.Windows.Forms.Label lblTotal;
    }
}
