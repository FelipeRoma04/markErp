using System;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Controler;
using Proyecto.Model;

namespace Proyecto.View
{
    public partial class frmLogin : Form
    {
        authControler _authCtrl;

        public frmLogin()
        {
            InitializeComponent();
            _authCtrl = new authControler();
            ApplyStyles();
            TestDbConnection();
        }

        private void ApplyStyles()
        {
            this.BackColor = UITheme.BgColor;
            
            // Header
            pnlHeader.BackColor = UITheme.PrimaryColor;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = Color.White;

            // Labels
            UITheme.StyleLabel(lblUser);
            UITheme.StyleLabel(lblPass);
            
            // TextBoxes
            UITheme.StyleTextBox(txtUser);
            UITheme.StyleTextBox(txtPass);

            // Button
            btnLogin.BackColor = UITheme.SuccessColor;
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Cursor = Cursors.Hand;
            btnLogin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
        }

        private void TestDbConnection()
        {
            try
            {
                var conn = new conexionModel();
                var err = conn.ProbarConexion();
                
                if (!string.IsNullOrWhiteSpace(err))
                {
                    MessageBox.Show("Advertencia: No se pudo verificar la conexión a la base de datos local. El inicio de sesión podría fallar.", 
                                    "Error de Conexión", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch { /* Ignore non-critical test errors during load */ }
        }

        private void chkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPass.PasswordChar = chkShowPass.Checked ? '\0' : '●';
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                ValidationHelper.ShowValidationError("Por favor, complete todos los campos.");
                return;
            }

            _authCtrl.Username = txtUser.Text.Trim();
            _authCtrl.Password = txtPass.Text.Trim();

            try
            {
                if (_authCtrl.IniciarSesion())
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    ValidationHelper.ShowError("Usuario o contraseña incorrectos.");
                }
            }
            catch (Exception ex)
            {
                // Log error
                try
                {
                    var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cliente-crash.log");
                    System.IO.File.AppendAllText(path, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Login error: {ex}\n\n");
                }
                catch { }

                ValidationHelper.ShowError("Ocurrió un error inesperado. Consulte el log de sistema.");
            }
        }
    }
}
