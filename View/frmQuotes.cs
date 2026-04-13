using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Controler;
using Proyecto.Model;

namespace Proyecto.View
{
    public partial class frmQuotes : Form
    {
        salesControler salesCtrl;
        conexionModel conexion;
        bool canCreate;
        string currentClientName = "";

        public frmQuotes()
        {
            InitializeComponent();
            salesCtrl = new salesControler();
            conexion = new conexionModel();
            
            ApplyStyle();
            
            dtpIssue.Value = DateTime.Now;
            dtpExpire.Value = DateTime.Now.AddDays(30);
            
            // Wire up auto-load event
            txtClientId.KeyDown += TxtClientId_KeyDown;
            
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
            UITheme.StyleLabel(lblClient);
            UITheme.StyleLabel(lblIssue);
            UITheme.StyleLabel(lblExpire);
            UITheme.StyleLabel(lblTotal);

            // TextBoxes
            UITheme.StyleTextBox(txtClientId);
            UITheme.StyleTextBox(txtTotal);

            // Buttons
            btnSave.BackColor = UITheme.SuccessColor;
        }

        // Auto-load client name when Client ID is entered
        private void TxtClientId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (int.TryParse(txtClientId.Text, out int clientId))
                {
                    LoadClientName(clientId);
                }
            }
        }

        private void LoadClientName(int clientId)
        {
            try
            {
                DataTable dt = conexion.ejecutarConsulta($"SELECT Name FROM Clients WHERE Id = {clientId}");
                if (dt != null && dt.Rows.Count > 0)
                {
                    currentClientName = dt.Rows[0]["Name"].ToString();
                    ValidationHelper.ShowSuccess($"Cliente: {currentClientName}");
                }
                else
                {
                    currentClientName = "";
                    ValidationHelper.ShowValidationError("Cliente no encontrado.");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error al cargar cliente: " + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!canCreate)
            {
                PermissionHelper.DenyAccess(PermissionHelper.Feature.Quotes);
                return;
            }

            try
            {
                if (!ValidationHelper.IsValidInteger(txtClientId.Text, out int clientId) ||
                    !ValidationHelper.IsValidDecimal(txtTotal.Text, out decimal subtotal))
                {
                    ValidationHelper.ShowValidationError("Verifique los campos numéricos.");
                    return;
                }

                salesCtrl.ClientId = clientId;
                salesCtrl.IssueDate = dtpIssue.Value;
                salesCtrl.ExpirationDate = dtpExpire.Value;
                salesCtrl.Subtotal = subtotal;

                // Calculate taxes purely from Subtotal
                decimal finalTotal = salesCtrl.Total; 

                if (salesCtrl.CreateQuote())
                {
                    ValidationHelper.ShowSuccess($"Cotización Guardada! Total con IVA: {finalTotal:C}");
                    ClearFields();
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error en guardado: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtClientId.Clear();
            txtTotal.Text = "0.00";
            dtpIssue.Value = DateTime.Now;
            dtpExpire.Value = DateTime.Now.AddDays(30);
            currentClientName = "";
        }

        private void ApplyPermissions()
        {
            canCreate = PermissionHelper.HasPermission(PermissionHelper.Feature.Quotes, PermissionHelper.Action.Create);
            btnSave.Enabled = canCreate;
        }
    }
}
