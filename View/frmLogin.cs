using System;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmLogin : Form
    {
        authControler _authCtrl;

        public frmLogin()
        {
            InitializeComponent();
            _authCtrl = new authControler();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Please fill all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _authCtrl.Username = txtUser.Text.Trim();
            _authCtrl.Password = txtPass.Text.Trim(); // In real app, hash this before sending

            if (_authCtrl.IniciarSesion())
            {
                this.DialogResult = DialogResult.OK; // Signals Program.cs to continue
                this.Close();
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
