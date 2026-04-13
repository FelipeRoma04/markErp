using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Proyecto.Model;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmSettings : Form
    {
        private Company currentCompany;

        public frmSettings()
        {
            InitializeComponent();
            ApplyStyle();
            LoadSettings();
        }

        private void ApplyStyle()
        {
            this.BackColor = UITheme.BgColor;
            pnlHeader.BackColor = UITheme.PrimaryColor;
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = UITheme.FontHeader;

            // Tabs
            tabGeneral.BackColor = Color.White;
            tabContact.BackColor = Color.White;

            // Labels & TextBoxes
            UITheme.StyleLabel(lblCompanyName);
            UITheme.StyleLabel(lblNit);
            UITheme.StyleLabel(lblAddress);
            UITheme.StyleLabel(lblCity);
            UITheme.StyleLabel(lblPhone);
            UITheme.StyleLabel(lblEmail);
            UITheme.StyleLabel(lblLogo);

            UITheme.StyleTextBox(txtCompanyName);
            UITheme.StyleTextBox(txtNit);
            UITheme.StyleTextBox(txtAddress);
            UITheme.StyleTextBox(txtCity);
            UITheme.StyleTextBox(txtPhone);
            UITheme.StyleTextBox(txtEmail);

            // Buttons
            btnSave.BackColor = UITheme.SuccessColor;
            btnCancel.BackColor = UITheme.SecondaryColor;
            btnBrowseLogo.BackColor = UITheme.AccentColor;
        }

        private void LoadSettings()
        {
            try
            {
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

                    txtCompanyName.Text = currentCompany.CompanyName;
                    txtNit.Text = currentCompany.Nit;
                    txtAddress.Text = currentCompany.Address;
                    txtCity.Text = currentCompany.City;
                    txtPhone.Text = currentCompany.Phone;
                    txtEmail.Text = currentCompany.Email;

                    if (!string.IsNullOrEmpty(currentCompany.LogoPath) && File.Exists(currentCompany.LogoPath))
                    {
                        using (var stream = new FileStream(currentCompany.LogoPath, FileMode.Open, FileAccess.Read))
                        {
                            picLogo.Image = Image.FromStream(stream);
                        }
                    }
                }
                else
                {
                    currentCompany = new Company();
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Company") || ex.Message.Contains("Invalid object"))
                {
                    currentCompany = new Company();
                    ValidationHelper.ShowError("La tabla 'Company' no existe. Asegúrese de ejecutar los scripts de base de datos.");
                }
                else
                {
                    ValidationHelper.ShowError($"Error al cargar configuración: {ex.Message}");
                }
            }
        }

        private void btnBrowseLogo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "Imagenes (*.bmp;*.jpg;*.jpeg;*.png;*.gif)|*.bmp;*.jpg;*.jpeg;*.png;*.gif",
                Title = "Seleccionar Logo Corporativo"
            })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        picLogo.Image = Image.FromFile(ofd.FileName);
                        currentCompany.LogoPath = ofd.FileName;
                    }
                    catch (Exception ex)
                    {
                        ValidationHelper.ShowError($"Error al cargar imagen: {ex.Message}");
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCompanyName.Text))
            {
                ValidationHelper.ShowValidationError("El nombre de la empresa es obligatorio.");
                return;
            }

            try
            {
                currentCompany.CompanyName = txtCompanyName.Text;
                currentCompany.Nit = txtNit.Text;
                currentCompany.Address = txtAddress.Text;
                currentCompany.City = txtCity.Text;
                currentCompany.Phone = txtPhone.Text;
                currentCompany.Email = txtEmail.Text;

                // Save logo locally if it changed and is external
                if (!string.IsNullOrEmpty(currentCompany.LogoPath) && File.Exists(currentCompany.LogoPath))
                {
                    string appLogoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo_corp.png");
                    if (currentCompany.LogoPath != appLogoPath)
                    {
                        File.Copy(currentCompany.LogoPath, appLogoPath, true);
                        currentCompany.LogoPath = appLogoPath;
                    }
                }

                SaveToDatabase();
                ValidationHelper.ShowSuccess("Configuración guardada exitosamente.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError($"Error al guardar: {ex.Message}");
            }
        }

        private void SaveToDatabase()
        {
            conexionModel conexion = new conexionModel();
            const string checkQuery = "SELECT COUNT(*) as cnt FROM Company";
            DataTable checkDt = conexion.ejecutarConsulta(checkQuery);
            bool exists = checkDt != null && Convert.ToInt32(checkDt.Rows[0]["cnt"]) > 0;

            string query = exists ? 
                @"UPDATE Company SET CompanyName = @name, Nit = @nit, Address = @addr, City = @city, Phone = @phone, Email = @email, LogoPath = @logo WHERE Id = (SELECT TOP 1 Id FROM Company)" :
                @"INSERT INTO Company (CompanyName, Nit, Address, City, Phone, Email, LogoPath) VALUES (@name, @nit, @addr, @city, @phone, @email, @logo)";

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
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
