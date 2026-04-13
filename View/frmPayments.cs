using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmPayments : Form
    {
        private readonly salesControler salesCtrl;
        private bool canCreatePayment;

        public frmPayments()
        {
            InitializeComponent();
            salesCtrl = new salesControler();
            ApplyStyle();
            ApplyPermissions();
        }

        private void ApplyStyle()
        {
            this.BackColor = UITheme.BgColor;
            pnlHeader.BackColor = UITheme.PrimaryColor;
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = UITheme.FontHeader;

            tabPayment.BackColor = Color.White;
            tabHistory.BackColor = Color.White;

            // Labels
            UITheme.StyleLabel(lblInvoice);
            UITheme.StyleLabel(lblAmount);
            UITheme.StyleLabel(lblMethod);
            
            lblDebt.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblDebt.ForeColor = UITheme.DangerColor;

            // TextBoxes
            UITheme.StyleTextBox(txtInvoiceId);
            UITheme.StyleTextBox(txtAmount);

            // Button
            btnSave.BackColor = UITheme.PrimaryColor;

            // History styles
            UITheme.StyleDataGrid(dgvPaymentHistory);
            lblHistoryDebt.BackColor = Color.FromArgb(236, 240, 241);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!canCreatePayment)
            {
                PermissionHelper.DenyAccess(PermissionHelper.Feature.Sales);
                return;
            }

            if (!ValidationHelper.IsValidInteger(txtInvoiceId.Text, out int invoiceId) ||
                !ValidationHelper.IsValidDecimal(txtAmount.Text, out decimal amount))
            {
                ValidationHelper.ShowValidationError("Verifique que el ID de factura y el monto sean valores numéricos válidos.");
                return;
            }

            try
            {
                salesCtrl.InvoiceId = invoiceId;
                salesCtrl.QuoteId = invoiceId; 
                salesCtrl.PaymentAmount = amount;
                salesCtrl.PaymentMethod = cmbMethod.Text;

                if (salesCtrl.ApplyPayment())
                {
                    ValidationHelper.ShowSuccess($"Pago por {amount:C} procesado exitosamente.");
                    LoadDebt();
                    LoadPaymentHistory();
                    txtAmount.Clear();
                }
                else
                {
                    ValidationHelper.ShowError(salesCtrl.PaymentResultMessage ?? "No se pudo registrar el pago.");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error técnico: " + ex.Message);
            }
        }

        private void txtInvoiceId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                LoadDebt();
                LoadPaymentHistory();
            }
        }

        private void LoadDebt()
        {
            if (int.TryParse(txtInvoiceId.Text, out int invoiceId))
            {
                decimal pending = salesCtrl.GetInvoicePending(invoiceId);
                lblDebt.Text = $"Pendiente factura: {pending:C}";
                lblDebt.ForeColor = pending > 0 ? UITheme.DangerColor : UITheme.SuccessColor;
            }
            else
            {
                lblDebt.Text = "Pendiente factura: -";
                lblDebt.ForeColor = UITheme.TextSecondary;
            }
        }

        private void LoadPaymentHistory()
        {
            if (!int.TryParse(txtInvoiceId.Text, out int invoiceId))
            {
                dgvPaymentHistory.DataSource = null;
                lblHistoryDebt.Text = "Total Pendiente: -";
                return;
            }

            try
            {
                DataTable paymentData = salesCtrl.GetPaymentHistory(invoiceId);
                dgvPaymentHistory.DataSource = paymentData;

                decimal invoiceTotal = salesCtrl.GetInvoiceTotal(invoiceId);
                decimal totalPaid = salesCtrl.GetInvoiceTotalPaid(invoiceId);
                decimal pending = invoiceTotal - totalPaid;

                lblHistoryDebt.Text = $"Resumen -> Total Factura: {invoiceTotal:C}  |  Pagado: {totalPaid:C}  |  PENDIENTE: {pending:C}";
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error cargando historial: " + ex.Message);
            }
        }

        private void ApplyPermissions()
        {
            canCreatePayment = PermissionHelper.HasPermission(PermissionHelper.Feature.Sales, PermissionHelper.Action.Create);
            btnSave.Enabled = canCreatePayment;
        }
    }
}
