using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Proyecto.Model;
using Proyecto.Controler;

namespace Proyecto.View
{
    /// <summary>
    /// FASE 4 - Task 37: Company Configuration Settings
    /// Admin panel for company name, NIT, address, and logo
    /// </summary>
    public partial class frmSettings : Form
    {
        private Company currentCompany;

        public frmSettings()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            try
            {
                // Load company info from database
                conexionModel conexion = new conexionModel();
                const string query = @"
                    SELECT TOP 1 
                        CompanyName, Nit, Address, City, Phone, Email, LogoPath
                    FROM Company
                    ORDER BY Id DESC";

                DataTable dt = conexion.ejecutarConsulta(query);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    currentCompany = new Company
                    {
                        CompanyName = row["CompanyName"].ToString(),
                        Nit = row["Nit"].ToString(),
                        Address = row["Address"].ToString(),
                        City = row["City"].ToString(),
                        Phone = row["Phone"].ToString(),
                        Email = row["Email"].ToString(),
                        LogoPath = row["LogoPath"].ToString()
                    };

                    // Display in form
                    txtCompanyName.Text = currentCompany.CompanyName;
                    txtNit.Text = currentCompany.Nit;
                    txtAddress.Text = currentCompany.Address;
                    txtCity.Text = currentCompany.City;
                    txtPhone.Text = currentCompany.Phone;
                    txtEmail.Text = currentCompany.Email;

                    // Load logo if exists
                    if (!string.IsNullOrEmpty(currentCompany.LogoPath) && File.Exists(currentCompany.LogoPath))
                    {
                        picLogo.Image = Image.FromFile(currentCompany.LogoPath);
                    }
                }
                else
                {
                    currentCompany = new Company();
                }
            }
            catch (Exception ex)
            {
                // If Company table doesn't exist yet, show setup message and create default company object
                if (ex.Message.Contains("Company") || ex.Message.Contains("Invalid object"))
                {
                    currentCompany = new Company();
                    MessageBox.Show("Database not yet configured. Please run SQL installation script first." + Environment.NewLine +
                                  "Script: Database\\10_Master_Installation.sql", 
                                  "Database Setup Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Error loading settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnBrowseLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Image Files (*.bmp;*.jpg;*.jpeg;*.png;*.gif)|*.bmp;*.jpg;*.jpeg;*.png;*.gif",
                Title = "Seleccionar Logo"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    picLogo.Image = Image.FromFile(ofd.FileName);
                    currentCompany.LogoPath = ofd.FileName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
                {
                    ValidationHelper.ShowValidationError("Nombre de empresa es requerido.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtNit.Text))
                {
                    ValidationHelper.ShowValidationError("NIT es requerido.");
                    return;
                }

                currentCompany.CompanyName = txtCompanyName.Text;
                currentCompany.Nit = txtNit.Text;
                currentCompany.Address = txtAddress.Text;
                currentCompany.City = txtCity.Text;
                currentCompany.Phone = txtPhone.Text;
                currentCompany.Email = txtEmail.Text;

                // Copy logo to app folder for portability
                if (currentCompany.LogoPath != null && File.Exists(currentCompany.LogoPath))
                {
                    string appLogoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo.png");
                    File.Copy(currentCompany.LogoPath, appLogoPath, true);
                    currentCompany.LogoPath = appLogoPath;
                }

                // Save to database
                SaveToDatabase();

                MessageBox.Show("Configuración guardada exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveToDatabase()
        {
            try
            {
                conexionModel conexion = new conexionModel();

                // Check if company exists
                const string checkQuery = "SELECT COUNT(*) as cnt FROM Company";
                DataTable checkDt = conexion.ejecutarConsulta(checkQuery);
                bool exists = checkDt != null && Convert.ToInt32(checkDt.Rows[0]["cnt"]) > 0;

                string query;
                if (exists)
                {
                    query = @"
                        UPDATE Company 
                        SET CompanyName = @name, Nit = @nit, Address = @addr, 
                            City = @city, Phone = @phone, Email = @email, LogoPath = @logo
                        WHERE Id = 1";
                }
                else
                {
                    query = @"
                        INSERT INTO Company (CompanyName, Nit, Address, City, Phone, Email, LogoPath)
                        VALUES (@name, @nit, @addr, @city, @phone, @email, @logo)";
                }

                var parameters = new System.Collections.Generic.Dictionary<string, object>
                {
                    ["@name"] = currentCompany.CompanyName,
                    ["@nit"] = currentCompany.Nit,
                    ["@addr"] = currentCompany.Address,
                    ["@city"] = currentCompany.City,
                    ["@phone"] = currentCompany.Phone,
                    ["@email"] = currentCompany.Email,
                    ["@logo"] = currentCompany.LogoPath ?? ""
                };

                conexion.ejecutarComandoParametrizado(query, parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving to database: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public class Company
        {
            public int Id { get; set; }
            public string CompanyName { get; set; }
            public string Nit { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string LogoPath { get; set; }
        }
    }
}
