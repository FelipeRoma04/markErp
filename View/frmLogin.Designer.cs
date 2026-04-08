namespace Proyecto.View
{
    partial class frmLogin
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
            this.btnLogin = new System.Windows.Forms.Button();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPass = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPass = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.SuspendLayout();

            // Header Panel
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height = 80;
            this.pnlHeader.Controls.Add(this.lblTitle);

            // Title Label
            this.lblTitle.AutoSize = false;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Text = "markErp System";
            this.lblTitle.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Username Label
            this.lblUser.Location = new System.Drawing.Point(50, 110);
            this.lblUser.Text = "Username:";
            this.lblUser.Font = new System.Drawing.Font("Arial", 11F);
            this.lblUser.AutoSize = true;

            // Username TextBox
            this.txtUser.Location = new System.Drawing.Point(50, 135);
            this.txtUser.Size = new System.Drawing.Size(300, 28);
            this.txtUser.Font = new System.Drawing.Font("Arial", 11F);
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Password Label
            this.lblPass.Location = new System.Drawing.Point(50, 175);
            this.lblPass.Text = "Password:";
            this.lblPass.Font = new System.Drawing.Font("Arial", 11F);
            this.lblPass.AutoSize = true;

            // Password TextBox
            this.txtPass.Location = new System.Drawing.Point(50, 200);
            this.txtPass.Size = new System.Drawing.Size(300, 28);
            this.txtPass.Font = new System.Drawing.Font("Arial", 11F);
            this.txtPass.PasswordChar = '*';
            this.txtPass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            // Login Button
            this.btnLogin.Location = new System.Drawing.Point(50, 255);
            this.btnLogin.Text = "Login";
            this.btnLogin.Size = new System.Drawing.Size(300, 40);
            this.btnLogin.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            // Form
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 350);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.lblPass);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.pnlHeader);
            this.Text = "ERP Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblPass;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlHeader;
    }
}
