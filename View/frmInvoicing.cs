using System;
using System.Data;
using System.Windows.Forms;
using Proyecto.Controler;
using Proyecto.Model;

namespace Proyecto.View
{
    public partial class frmInvoicing : Form
    {
        salesControler salesCtrl;
        salesModel salesModel;
        conexionModel conexion;
        bool canCreate;
        string currentClientName = "";
        int currentInvoiceId = 0;

        public frmInvoicing()
        {
            InitializeComponent();
            salesCtrl = new salesControler();
            salesModel = new salesModel();
            conexion = new conexionModel();
            dtpIssue.Value = DateTime.Now;
            dtpDue.Value = DateTime.Now.AddDays(15);
            
            // Wire up events for auto-loading
            txtQuoteId.KeyDown += TxtQuoteId_KeyDown;
            txtClientId.KeyDown += TxtClientId_KeyDown;
            
            ApplyPermissions();
        }

        // Auto-load quote data when Quote ID is entered (Task 20)
        private void TxtQuoteId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                if (int.TryParse(txtQuoteId.Text, out int quoteId))
                {
                    try
                    {
                        DataTable dt = conexion.ejecutarConsulta($@"
                            SELECT ClientId, TotalAmount FROM Quotes WHERE Id = {quoteId}");
                        
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            int clientId = Convert.ToInt32(dt.Rows[0]["ClientId"]);
                            decimal amount = Convert.ToDecimal(dt.Rows[0]["TotalAmount"]);
                            
                            txtClientId.Text = clientId.ToString();
                            txtSubtotal.Text = amount.ToString("0.00");
                            
                            // Auto-load client name
                            LoadClientName(clientId);
                            MessageBox.Show("Cotización cargada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Cotización no encontrada.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al cargar cotización: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
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

        private void btnCalc_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidationHelper.IsValidDecimal(txtSubtotal.Text, out decimal subtotal))
                {
                    ValidationHelper.ShowValidationError("Subtotal debe ser un número válido.");
                    return;
                }

                salesCtrl.Subtotal = subtotal;
                txtTax.Text = salesCtrl.TotalTax.ToString("0.00");
                txtTotal.Text = salesCtrl.Total.ToString("0.00");
            }
            catch { ValidationHelper.ShowError("Error en cálculo de IVA"); }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!canCreate)
            {
                PermissionHelper.DenyAccess(PermissionHelper.Feature.Invoicing);
                return;
            }

            try
            {
                // Validations
                if (!ValidationHelper.IsValidInteger(txtQuoteId.Text, out int quoteId) ||
                    !ValidationHelper.IsValidInteger(txtClientId.Text, out int clientId) ||
                    !ValidationHelper.IsValidDecimal(txtSubtotal.Text, out decimal subtotal))
                {
                    ValidationHelper.ShowValidationError("Verifique todos los campos numéricos.");
                    return;
                }

                salesCtrl.QuoteId = quoteId;
                salesCtrl.ClientId = clientId;
                salesCtrl.IssueDate = dtpIssue.Value;
                salesCtrl.DueDate = dtpDue.Value;
                salesCtrl.Subtotal = subtotal;

                if (salesCtrl.CreateInvoice())
                {
                    // Get the invoice ID that was created
                    DataTable dt = conexion.ejecutarConsulta($@"
                        SELECT TOP 1 Id FROM Invoices WHERE ClientId = {clientId} 
                        ORDER BY Id DESC");
                    
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        currentInvoiceId = Convert.ToInt32(dt.Rows[0]["Id"]);
                        
                        // Generate PDF DIAN Invoice (Task 19)
                        int consecutiveNumber = currentInvoiceId; // Can be replaced with sequential number from DB
                        string clientName = currentClientName.Length > 0 ? currentClientName : "Cliente ID: " + clientId;
                        
                        ReportControler.GenerateDianInvoicePDF(
                            currentInvoiceId, 
                            clientName, 
                            "XXXXXXX-X",  // Client Document - ideally fetch from DB
                            "Dirección del Cliente",  // Client Address - ideally fetch from DB
                            subtotal, 
                            salesCtrl.TotalTax, 
                            salesCtrl.Total,
                            dtpIssue.Value,
                            dtpDue.Value,
                            consecutiveNumber
                        );
                        
                        MessageBox.Show("Factura generada correctamente y PDF abierto!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ClearFields();
                    }
                    else
                    {
                        MessageBox.Show("Factura creada pero no se pudo generar el PDF.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Error al crear factura.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtQuoteId.Clear();
            txtClientId.Clear();
            txtSubtotal.Clear();
            txtTax.Clear();
            txtTotal.Clear();
            dtpIssue.Value = DateTime.Now;
            dtpDue.Value = DateTime.Now.AddDays(15);
        }

        private void ApplyPermissions()
        {
            canCreate = PermissionHelper.HasPermission(PermissionHelper.Feature.Invoicing, PermissionHelper.Action.Create);
            btnSave.Enabled = canCreate;
            btnCalc.Enabled = PermissionHelper.HasPermission(PermissionHelper.Feature.Invoicing, PermissionHelper.Action.View);
        }
    }
}
