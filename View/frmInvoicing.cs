using System;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmInvoicing : Form
    {
        salesControler salesCtrl;

        public frmInvoicing()
        {
            InitializeComponent();
            salesCtrl = new salesControler();
            dtpIssue.Value = DateTime.Now;
            dtpDue.Value = DateTime.Now.AddDays(15);
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
                    MessageBox.Show("Factura generada correctamente!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
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
    }
}
