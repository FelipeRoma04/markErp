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
            this.SuspendLayout();

            this.lblUser.Location = new System.Drawing.Point(40, 40);
            this.lblUser.Text = "Username:";
            this.txtUser.Location = new System.Drawing.Point(120, 40);
            this.txtUser.Size = new System.Drawing.Size(150, 22);

            this.lblPass.Location = new System.Drawing.Point(40, 80);
            this.lblPass.Text = "Password:";
            this.txtPass.Location = new System.Drawing.Point(120, 80);
            this.txtPass.Size = new System.Drawing.Size(150, 22);
            this.txtPass.PasswordChar = '*';

            this.btnLogin.Location = new System.Drawing.Point(120, 130);
            this.btnLogin.Text = "Login";
            this.btnLogin.Size = new System.Drawing.Size(100, 30);
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);

            this.ClientSize = new System.Drawing.Size(320, 200);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.txtUser);
            this.Controls.Add(this.txtPass);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.lblPass);
            this.Text = "ERP Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPass;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblPass;
    }
}
