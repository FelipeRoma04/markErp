using System;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmClients : Form
    {
        clientControler cliCtrl;

        public frmClients()
        {
            InitializeComponent();
            cliCtrl = new clientControler();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            cliCtrl.DocumentId = txtDocId.Text;
            cliCtrl.Name = txtName.Text;
            cliCtrl.Email = txtEmail.Text;
            cliCtrl.Phone = txtPhone.Text;
            cliCtrl.Address = txtAddress.Text;

            try
            {
                if (cliCtrl.AddClient())
                {
                    MessageBox.Show("Cliente guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    // Error is caught by ValidationHelper inside the controller mostly
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de red: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
