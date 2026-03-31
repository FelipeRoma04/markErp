using System;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Controler;
using System.Data;

namespace Proyecto.View
{
    public partial class frmProducts : Form
    {
        productControler prodCtrl;

        public frmProducts()
        {
            InitializeComponent();
            prodCtrl = new productControler();
        }

        private void frmProducts_Load(object sender, EventArgs e)
        {
            LoadInventory();
        }

        private void LoadInventory()
        {
            DataTable dt = prodCtrl.GetInventory();
            if (dt != null)
            {
                dgvProducts.DataSource = dt;
                
                // Colorize rows based on MinStock
                foreach (DataGridViewRow row in dgvProducts.Rows)
                {
                    if (row.Cells["Stock"].Value != DBNull.Value && row.Cells["MinStock"].Value != DBNull.Value)
                    {
                        int stock = Convert.ToInt32(row.Cells["Stock"].Value);
                        int minStock = Convert.ToInt32(row.Cells["MinStock"].Value);

                        if (stock < minStock)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                        }
                    }
                }
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = prodCtrl.GetInventory();
            if(dt != null) 
            {
                if(ReportControler.ExportToExcel(dt, "ProductsInventory_Export"))
                {
                    MessageBox.Show("Excel CSV Generado correctamente.");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                prodCtrl.Code = txtCode.Text;
                prodCtrl.Name = txtName.Text;
                prodCtrl.Price = decimal.Parse(txtPrice.Text);
                prodCtrl.Stock = int.Parse(txtStock.Text);
                prodCtrl.MinStock = int.Parse(txtMinStock.Text);

                if (prodCtrl.AddProduct())
                {
                    MessageBox.Show("Producto guardado correctamente en inventario.", "Agregado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInventory(); // Refresh grid
                }
            }
            catch (Exception)
            {
                ValidationHelper.ShowValidationError("Los campos numéricos para precio y stock son inválidos.");
            }
        }
    }
}
