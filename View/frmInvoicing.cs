using System;
using System.Data;
using System.Drawing;
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
            
            ApplyStyle();
            
            dtpIssue.Value = DateTime.Now;
            dtpDue.Value = DateTime.Now.AddDays(15);
            
            // Wire up events for auto-loading
            txtQuoteId.KeyDown += TxtQuoteId_KeyDown;
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
            pnlResults.BackColor = Color.FromArgb(236, 240, 241);

            // Labels
            UITheme.StyleLabel(lblQuote);
            UITheme.StyleLabel(lblClient);
            UITheme.StyleLabel(lblIssue);
            UITheme.StyleLabel(lblDue);
            UITheme.StyleLabel(lblSubtotal);
            UITheme.StyleLabel(lblTax);
            UITheme.StyleLabel(lblTotal, true);

            // TextBoxes
            UITheme.StyleTextBox(txtQuoteId);
            UITheme.StyleTextBox(txtClientId);
            UITheme.StyleTextBox(txtSubtotal);
            
            // Results styling
            txtTax.ForeColor = UITheme.TextSecondary;
            txtTotal.ForeColor = UITheme.SuccessColor;

            // Buttons
            btnCalc.BackColor = UITheme.AccentColor;
            btnSave.BackColor = UITheme.PrimaryColor;
        }

        // Auto-load quote data when Quote ID is entered
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
                            
                            LoadClientName(clientId);
                            ValidationHelper.ShowSuccess("Cotización cargada correctamente.");
                        }
                        else
                        {
                            ValidationHelper.ShowValidationError("Cotización no encontrada.");
                        }
                    }
                    catch (Exception ex)
                    {
                        ValidationHelper.ShowError("Error al cargar cotización: " + ex.Message);
                    }
                }
            }
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
                    // We can show it in a tooltip or status bar if we had one, for now just a toast
                }
                else
                {
                    currentClientName = "";
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error al cargar cliente: " + ex.Message);
            }
        }

        private void btnCalc_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidationHelper.IsValidDecimal(txtSubtotal.Text, out decimal subtotal))
                {
                    ValidationHelper.ShowValidationError("El subtotal debe ser un número válido.");
                    return;
                }

                salesCtrl.Subtotal = subtotal;
                txtTax.Text = salesCtrl.TotalTax.ToString("C");
                txtTotal.Text = salesCtrl.Total.ToString("C");
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
                    ValidationHelper.ShowValidationError("Verifique que todos los campos numéricos sean correctos.");
                    return;
                }

                salesCtrl.QuoteId = quoteId;
                salesCtrl.ClientId = clientId;
                salesCtrl.IssueDate = dtpIssue.Value;
                salesCtrl.DueDate = dtpDue.Value;
                salesCtrl.Subtotal = subtotal;

                if (salesCtrl.CreateInvoice())
                {
                    DataTable dt = conexion.ejecutarConsulta($@"
                        SELECT TOP 1 Id FROM Invoices WHERE ClientId = {clientId} 
                        ORDER BY Id DESC");
                    
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        currentInvoiceId = Convert.ToInt32(dt.Rows[0]["Id"]);
                        
                        // Generate PDF DIAN Invoice
                        int consecutiveNumber = currentInvoiceId;
                        string clientName = currentClientName.Length > 0 ? currentClientName : "Cliente ID: " + clientId;
                        
                        ReportControler.GenerateDianInvoicePDF(
                            currentInvoiceId, 
                            clientName, 
                            "800.123.456-7", // Demo NIT
                            "Dirección Comercial ERP",
                            subtotal, 
                            salesCtrl.TotalTax, 
                            salesCtrl.Total,
                            dtpIssue.Value,
                            dtpDue.Value,
                            consecutiveNumber
                        );
                        
                        ValidationHelper.ShowSuccess("Factura emitida y PDF generado correctamente.");
                        ClearFields();
                    }
                }
                else
                {
                    ValidationHelper.ShowError("No se pudo registrar la factura en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error crítico: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtQuoteId.Clear();
            txtClientId.Clear();
            txtSubtotal.Text = "0.00";
            txtTax.Text = "0.00";
            txtTotal.Text = "0.00";
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
