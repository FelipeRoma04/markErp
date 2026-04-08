using System;
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
            // Header styling
            if (pnlHeader != null)
            {
                pnlHeader.BackColor = System.Drawing.Color.FromArgb(41, 128, 185);
            }

            // Button styling
            btnLogin.BackColor = System.Drawing.Color.FromArgb(46, 204, 113);
            btnLogin.ForeColor = System.Drawing.Color.White;
            btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Cursor = System.Windows.Forms.Cursors.Hand;

            // TextBox styling
            txtUser.BackColor = System.Drawing.Color.White;
            txtUser.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            txtPass.BackColor = System.Drawing.Color.White;
            txtPass.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
        }

        private void TestDbConnection()
        {
            try
            {
                var conn = new conexionModel();
                var err = conn.ProbarConexion();
                var cs = conn.ObtenerCadenaConexion();
                string masked = cs;
                try
                {
                    // Mask password if present
                    if (cs != null && cs.IndexOf("Password=", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        var parts = cs.Split(';');
                        for (int i = 0; i < parts.Length; i++)
                        {
                            if (parts[i].StartsWith("Password=", StringComparison.OrdinalIgnoreCase))
                                parts[i] = "Password=****";
                        }
                        masked = string.Join(";", parts);
                    }
                }
                catch { masked = "(error al obtener cadena)"; }
                if (!string.IsNullOrWhiteSpace(err))
                {
                    // Show brief message and direct user to the crash log for full details
                    MessageBox.Show("No se pudo conectar a la base de datos. Revisa cliente-crash.log para más detalles.\n\n" +
                                    "Cadena usada:\n" + masked + "\n\n" +
                                    "Resumen error:\n" + (err.Length > 1000 ? err.Substring(0, 1000) + "..." : err),
                                    "Error conexión DB",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                try { System.IO.File.AppendAllText(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cliente-crash.log"), $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] TestDbConnection failure: {ex}\n\n"); } catch { }
                MessageBox.Show("Error al probar la conexión a la base de datos. Revisa cliente-crash.log.", "Error conexión DB", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            try
            {
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
            catch (Exception ex)
            {
                // Log minimal info to file next to exe to help debugging
                try
                {
                    var path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cliente-crash.log");
                    System.IO.File.AppendAllText(path, $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Login error: {ex}\n\n");
                }
                catch { }

                MessageBox.Show("Ocurrió un error durante el intento de autenticación. Revisa cliente-crash.log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
