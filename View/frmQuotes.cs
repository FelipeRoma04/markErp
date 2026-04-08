using System;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmQuotes : Form
    {
        salesControler salesCtrl;
        bool canCreate;

        public frmQuotes()
        {
            InitializeComponent();
            salesCtrl = new salesControler();
            dtpIssue.Value = DateTime.Now;
            dtpExpire.Value = DateTime.Now.AddDays(30);
            ApplyPermissions();
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
