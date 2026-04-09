using System;
using System.Data;
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
            dtpIssue.Value = DateTime.Now;
            dtpExpire.Value = DateTime.Now.AddDays(30);
            
            // Wire up auto-load event (Task 21)
            txtClientId.KeyDown += TxtClientId_KeyDown;
            
            ApplyPermissions();
        }

        // Auto-load client name when Client ID is entered (Task 21)
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
                    MessageBox.Show($"Cliente: {currentClientName}", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cliente no encontrado.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    currentClientName = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                salesCtrl.ClientId = int.Parse(txtClientId.Text);
                salesCtrl.IssueDate = dtpIssue.Value;
                salesCtrl.ExpirationDate = dtpExpire.Value;
                salesCtrl.Subtotal = decimal.Parse(txtTotal.Text); // Quick hack to re-use Total property

                // Calculate taxes purely from Subtotal
                decimal finalTotal = salesCtrl.Total; 

                if (salesCtrl.CreateQuote())
                {
                    MessageBox.Show($"Cotización Guardada! Total con IVA: {finalTotal:C}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowValidationError("Error en guardado: " + ex.Message);
            }
        }

        private void ApplyPermissions()
        {
            canCreate = PermissionHelper.HasPermission(PermissionHelper.Feature.Quotes, PermissionHelper.Action.Create);
            btnSave.Enabled = canCreate;
        }
    }
}
