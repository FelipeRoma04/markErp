using System;
using System.Windows.Forms;
using Proyecto.Controler;
using System.Data;

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
            ApplyPermissions();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!canCreatePayment)
            {
                PermissionHelper.DenyAccess(PermissionHelper.Feature.Sales);
                return;
            }

            try
            {
                salesCtrl.InvoiceId = int.Parse(txtInvoiceId.Text);
                salesCtrl.QuoteId = salesCtrl.InvoiceId; // permite pagar cotizacion ingresando su Id
                salesCtrl.PaymentAmount = decimal.Parse(txtAmount.Text);
                salesCtrl.PaymentMethod = cmbMethod.Text;

                if (salesCtrl.ApplyPayment())
                {
                    MessageBox.Show($"Pago por {salesCtrl.PaymentAmount:C} procesado via {salesCtrl.PaymentMethod}.", "Cobro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDebt();
                    LoadPaymentHistory(); // Task 30: Refresh payment history after payment
                    txtAmount.Clear();
                }
                else
                {
                    ValidationHelper.ShowError(salesCtrl.PaymentResultMessage ?? "No se pudo registrar el pago.");
                }
            }
            catch (Exception)
            {
                ValidationHelper.ShowValidationError("Los campos numericos son obligatorios y deben ser validos.");
            }
        }

        private void ApplyPermissions()
        {
            canCreatePayment = PermissionHelper.HasPermission(PermissionHelper.Feature.Sales, PermissionHelper.Action.Create);
            btnSave.Enabled = canCreatePayment;
        }

        private void LoadDebt()
        {
            if (!int.TryParse(txtInvoiceId.Text, out int invoiceId))
            {
                lblDebt.Text = "Pendiente cliente: -";
                return;
            }

            decimal pending = salesCtrl.GetInvoicePending(invoiceId);
            lblDebt.Text = $"Pendiente factura: {pending:C}";
        }

        // Task 30: Auto-load payment history when InvoiceId changes
        private void txtInvoiceId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter)
            {
                LoadDebt();
                LoadPaymentHistory();
                e.Handled = true;
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
                
                // Rename columns for display
                if (dgvPaymentHistory.Columns.Contains("PaymentDate"))
                    dgvPaymentHistory.Columns["PaymentDate"].HeaderText = "Fecha de Pago";
                if (dgvPaymentHistory.Columns.Contains("Amount"))
                    dgvPaymentHistory.Columns["Amount"].HeaderText = "Monto";
                if (dgvPaymentHistory.Columns.Contains("PaymentMethod"))
                    dgvPaymentHistory.Columns["PaymentMethod"].HeaderText = "Método";

                // Calculate and display pending balance
                decimal invoiceTotal = salesCtrl.GetInvoiceTotal(invoiceId);
                decimal totalPaid = salesCtrl.GetInvoiceTotalPaid(invoiceId);
                decimal pending = invoiceTotal - totalPaid;

                lblHistoryDebt.Text = $"Total Factura: {invoiceTotal:C}  |  Total Pagado: {totalPaid:C}  |  Pendiente: {pending:C}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando historial: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
