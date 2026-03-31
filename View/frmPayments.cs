using System;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmPayments : Form
    {
        salesControler salesCtrl;

        public frmPayments()
        {
            InitializeComponent();
            salesCtrl = new salesControler();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                salesCtrl.InvoiceId = int.Parse(txtInvoiceId.Text);
                salesCtrl.PaymentAmount = decimal.Parse(txtAmount.Text);
                salesCtrl.PaymentMethod = cmbMethod.Text;

                if (salesCtrl.ApplyPayment())
                {
                    MessageBox.Show($"Pago por {salesCtrl.PaymentAmount:C} procesado vía {salesCtrl.PaymentMethod}.", "Cobro Exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                ValidationHelper.ShowValidationError("Los campos numéricos son obligatorios y deben ser válidos.");
            }
        }
    }
}
