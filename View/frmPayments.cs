using System;
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
    }
}
