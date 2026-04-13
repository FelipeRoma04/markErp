using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmProducts : Form
    {
        productControler prodCtrl;

        public frmProducts()
        {
            InitializeComponent();
            prodCtrl = new productControler();
            ApplyStyle();
        }

        private void ApplyStyle()
        {
            this.BackColor = UITheme.BgColor;
            pnlHeader.BackColor = UITheme.PrimaryColor;
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = UITheme.FontHeader;

            pnlForm.BackColor = Color.White;
            pnlActions.BackColor = Color.FromArgb(236, 240, 241);

            // Labels
            UITheme.StyleLabel(lblCode);
            UITheme.StyleLabel(lblName);
            UITheme.StyleLabel(lblCost);
            UITheme.StyleLabel(lblSale);
            UITheme.StyleLabel(lblStock);
            UITheme.StyleLabel(lblMinStock);

            // TextBoxes
            UITheme.StyleTextBox(txtCode);
            UITheme.StyleTextBox(txtName);
            UITheme.StyleTextBox(txtCost);
            UITheme.StyleTextBox(txtSale);
            UITheme.StyleTextBox(txtStock);
            UITheme.StyleTextBox(txtMinStock);

            // Buttons
            btnSave.BackColor = UITheme.SuccessColor;
            btnExportExcel.BackColor = UITheme.SecondaryColor;

            // Grid
            UITheme.StyleDataGrid(dgvProducts);
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
                            row.DefaultCellStyle.BackColor = Color.MistyRose;
                            row.DefaultCellStyle.SelectionBackColor = Color.LightCoral;
                        }
                    }
                }
            }
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            DataTable dt = prodCtrl.GetInventory();
            if (dt != null && dt.Rows.Count > 0)
            {
                if (ReportControler.ExportToExcel(dt, "Proyecto_Inventario_" + DateTime.Now.ToString("yyyyMMdd")))
                {
                    ValidationHelper.ShowSuccess("Inventario exportado correctamente a CSV.");
                }
            }
            else
            {
                ValidationHelper.ShowValidationError("No hay productos en inventario para exportar.");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCode.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                ValidationHelper.ShowValidationError("El código y el nombre del producto son obligatorios.");
                return;
            }

            try
            {
                if (!ValidationHelper.IsValidDecimal(txtCost.Text, out decimal cost) ||
                    !ValidationHelper.IsValidDecimal(txtSale.Text, out decimal sale) ||
                    !ValidationHelper.IsValidInteger(txtStock.Text, out int stock) ||
                    !ValidationHelper.IsValidInteger(txtMinStock.Text, out int minStock))
                {
                    ValidationHelper.ShowValidationError("Verifique que los precios y el stock sean valores numéricos válidos.");
                    return;
                }

                prodCtrl.Code = txtCode.Text.Trim();
                prodCtrl.Name = txtName.Text.Trim();
                prodCtrl.CostPrice = cost;
                prodCtrl.SalePrice = sale;
                prodCtrl.Stock = stock;
                prodCtrl.MinStock = minStock;

                if (prodCtrl.AddProduct())
                {
                    ValidationHelper.ShowSuccess("Producto guardado correctamente en el inventario.");
                    LoadInventory();
                    ClearFields();
                }
                else
                {
                    ValidationHelper.ShowError("Error al guardar el producto. Verifique si el código ya existe.");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error inesperado: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtCode.Clear();
            txtName.Clear();
            txtCost.Text = "0.00";
            txtSale.Text = "0.00";
            txtStock.Text = "0";
            txtMinStock.Text = "5";
            txtCode.Focus();
        }
    }
}
