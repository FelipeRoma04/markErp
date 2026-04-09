namespace Proyecto.View
{
    partial class frmSettings
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblCompanyName = new System.Windows.Forms.Label();
            this.txtCompanyName = new System.Windows.Forms.TextBox();
            this.lblNit = new System.Windows.Forms.Label();
            this.txtNit = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblCity = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblLogo = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.btnBrowseLogo = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.SuspendLayout();

            // lblCompanyName
            this.lblCompanyName.AutoSize = true;
            this.lblCompanyName.Location = new System.Drawing.Point(20, 20);
            this.lblCompanyName.Name = "lblCompanyName";
            this.lblCompanyName.Size = new System.Drawing.Size(130, 13);
            this.lblCompanyName.TabIndex = 0;
            this.lblCompanyName.Text = "Nombre de Empresa:";

            // txtCompanyName
            this.txtCompanyName.Location = new System.Drawing.Point(20, 36);
            this.txtCompanyName.Name = "txtCompanyName";
            this.txtCompanyName.Size = new System.Drawing.Size(300, 20);
            this.txtCompanyName.TabIndex = 1;

            // lblNit
            this.lblNit.AutoSize = true;
            this.lblNit.Location = new System.Drawing.Point(20, 65);
            this.lblNit.Name = "lblNit";
            this.lblNit.Size = new System.Drawing.Size(33, 13);
            this.lblNit.TabIndex = 2;
            this.lblNit.Text = "NIT:";

            // txtNit
            this.txtNit.Location = new System.Drawing.Point(20, 81);
            this.txtNit.Name = "txtNit";
            this.txtNit.Size = new System.Drawing.Size(150, 20);
            this.txtNit.TabIndex = 3;

            // lblAddress
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(20, 110);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(59, 13);
            this.lblAddress.TabIndex = 4;
            this.lblAddress.Text = "Dirección:";

            // txtAddress
            this.txtAddress.Location = new System.Drawing.Point(20, 126);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(300, 40);
            this.txtAddress.TabIndex = 5;

            // lblCity
            this.lblCity.AutoSize = true;
            this.lblCity.Location = new System.Drawing.Point(20, 175);
            this.lblCity.Name = "lblCity";
            this.lblCity.Size = new System.Drawing.Size(52, 13);
            this.lblCity.TabIndex = 6;
            this.lblCity.Text = "Ciudad:";

            // txtCity
            this.txtCity.Location = new System.Drawing.Point(20, 191);
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(150, 20);
            this.txtCity.TabIndex = 7;

            // lblPhone
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(20, 220);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(56, 13);
            this.lblPhone.TabIndex = 8;
            this.lblPhone.Text = "Teléfono:";

            // txtPhone
            this.txtPhone.Location = new System.Drawing.Point(20, 236);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(150, 20);
            this.txtPhone.TabIndex = 9;

            // lblEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(20, 265);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 13);
            this.lblEmail.TabIndex = 10;
            this.lblEmail.Text = "Email:";

            // txtEmail
            this.txtEmail.Location = new System.Drawing.Point(20, 281);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(250, 20);
            this.txtEmail.TabIndex = 11;

            // lblLogo
            this.lblLogo.AutoSize = true;
            this.lblLogo.Location = new System.Drawing.Point(350, 20);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(40, 13);
            this.lblLogo.TabIndex = 12;
            this.lblLogo.Text = "Logo:";

            // picLogo
            this.picLogo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picLogo.Location = new System.Drawing.Point(350, 36);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(180, 180);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 13;
            this.picLogo.TabStop = false;

            // btnBrowseLogo
            this.btnBrowseLogo.Location = new System.Drawing.Point(350, 220);
            this.btnBrowseLogo.Name = "btnBrowseLogo";
            this.btnBrowseLogo.Size = new System.Drawing.Size(180, 23);
            this.btnBrowseLogo.TabIndex = 14;
            this.btnBrowseLogo.Text = "Seleccionar Logo";
            this.btnBrowseLogo.UseVisualStyleBackColor = true;
            this.btnBrowseLogo.Click += new System.EventHandler(this.btnBrowseLogo_Click);

            // btnSave
            this.btnSave.Location = new System.Drawing.Point(340, 320);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 30);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Guardar";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.Location = new System.Drawing.Point(445, 320);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            // frmSettings
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 370);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnBrowseLogo);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.lblLogo);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this.lblCity);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtNit);
            this.Controls.Add(this.lblNit);
            this.Controls.Add(this.txtCompanyName);
            this.Controls.Add(this.lblCompanyName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configuración de Empresa";
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblCompanyName;
        private System.Windows.Forms.TextBox txtCompanyName;
        private System.Windows.Forms.Label lblNit;
        private System.Windows.Forms.TextBox txtNit;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblCity;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Button btnBrowseLogo;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}
