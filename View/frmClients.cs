using System;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmClients : Form
    {
        clientControler cliCtrl;
        bool canCreate;

        public frmClients()
        {
            InitializeComponent();
            cliCtrl = new clientControler();
            ApplyStyle();
            ApplyPermissions();
        }

        private void ApplyStyle()
        {
            this.BackColor = UITheme.BgColor;
            pnlHeader.BackColor = UITheme.PrimaryColor;
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = UITheme.FontHeader;

            pnlForm.BackColor = Color.White;

            // Labels
            UITheme.StyleLabel(lblDocId);
            UITheme.StyleLabel(lblName);
            UITheme.StyleLabel(lblEmail);
            UITheme.StyleLabel(lblPhone);
            UITheme.StyleLabel(lblAddress);
            UITheme.StyleLabel(lblCity);

            // TextBoxes
            UITheme.StyleTextBox(txtDocId);
            UITheme.StyleTextBox(txtName);
            UITheme.StyleTextBox(txtEmail);
            UITheme.StyleTextBox(txtPhone);
            UITheme.StyleTextBox(txtAddress);
            UITheme.StyleTextBox(txtCity);

            // Buttons
            btnSave.BackColor = UITheme.SuccessColor;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!canCreate)
            {
                PermissionHelper.DenyAccess(PermissionHelper.Feature.Clients);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDocId.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                ValidationHelper.ShowValidationError("El Documento y la Razón Social son obligatorios.");
                return;
            }

            cliCtrl.DocumentId = txtDocId.Text.Trim();
            cliCtrl.Name = txtName.Text.Trim();
            cliCtrl.Email = txtEmail.Text.Trim();
            cliCtrl.Phone = txtPhone.Text.Trim();
            cliCtrl.Address = txtAddress.Text.Trim();
            cliCtrl.City = txtCity.Text.Trim();

            try
            {
                if (cliCtrl.AddClient())
                {
                    ValidationHelper.ShowSuccess("Cliente guardado correctamente.");
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error al guardar cliente: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtDocId.Clear();
            txtName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            txtCity.Clear();
            txtDocId.Focus();
        }

        private void ApplyPermissions()
        {
            canCreate = PermissionHelper.HasPermission(PermissionHelper.Feature.Clients, PermissionHelper.Action.Create);
            btnSave.Enabled = canCreate;
        }
    }
}
