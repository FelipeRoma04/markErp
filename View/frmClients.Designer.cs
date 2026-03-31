namespace Proyecto.View
{
    partial class frmClients
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
            this.txtDocId = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblDocId = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.lblDocId.Location = new System.Drawing.Point(30, 30);
            this.lblDocId.Text = "Documento (NIT/CC):";
            this.lblDocId.Size = new System.Drawing.Size(120, 20);
            this.txtDocId.Location = new System.Drawing.Point(160, 30);

            this.lblName.Location = new System.Drawing.Point(30, 70);
            this.lblName.Text = "Razón Social / Nombre:";
            this.lblName.Size = new System.Drawing.Size(120, 20);
            this.txtName.Location = new System.Drawing.Point(160, 70);
            this.txtName.Size = new System.Drawing.Size(200, 20);

            this.lblEmail.Location = new System.Drawing.Point(30, 110);
            this.lblEmail.Text = "Correo Electrónico:";
            this.txtEmail.Location = new System.Drawing.Point(160, 110);
            this.txtEmail.Size = new System.Drawing.Size(200, 20);

            this.lblPhone.Location = new System.Drawing.Point(30, 150);
            this.lblPhone.Text = "Work Phone:";
            this.txtPhone.Location = new System.Drawing.Point(160, 150);

            this.lblAddress.Location = new System.Drawing.Point(30, 190);
            this.lblAddress.Text = "Regional Address:";
            this.txtAddress.Location = new System.Drawing.Point(160, 190);
            this.txtAddress.Size = new System.Drawing.Size(200, 20);

            this.btnSave.Location = new System.Drawing.Point(160, 240);
            this.btnSave.Text = "Guardar Cliente";
            this.btnSave.Size = new System.Drawing.Size(120, 30);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtDocId);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblDocId);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblAddress);
            this.Text = "Administración de Clientes";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtDocId;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblDocId;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblAddress;
    }
}
