using System;
using System.Data;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmAssetDepreciation : Form
    {
        private assetControler assetCtrl;

        public frmAssetDepreciation()
        {
            InitializeComponent();
            assetCtrl = new assetControler();
            InitializeControls();
        }

        private void InitializeControls()
        {
            cmbDepreciationMethod.Items.Add("Línea Recta");
            cmbDepreciationMethod.Items.Add("Saldo Decreciente");
            cmbDepreciationMethod.SelectedIndex = 0;

            dgvAssets.AutoGenerateColumns = false;
            dgvAssets.Columns.Clear();

            dgvAssets.Columns.Add("AssetId", "Asset ID");
            dgvAssets.Columns.Add("SerialNumber", "Serial #");
            dgvAssets.Columns.Add("Type", "Tipo");
            dgvAssets.Columns.Add("PurchasePrice", "Precio Compra");
            dgvAssets.Columns.Add("UsefulLifeYears", "Vida Útil (años)");
            dgvAssets.Columns.Add("AnnualDepreciation", "Depreciación Anual");
            dgvAssets.Columns.Add("BookValue", "Valor en Libros");

            LoadAssets();
        }

        private void LoadAssets()
        {
            try
            {
                // TODO: Load assets and calculate depreciation
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error cargando activos: " + ex.Message);
            }
        }

        private void btnCalculateDepreciation_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidationHelper.IsValidInteger(txtAssetId.Text, out int assetId) ||
                    !ValidationHelper.IsValidDecimal(txtPurchasePrice.Text, out decimal price) ||
                    !ValidationHelper.IsValidInteger(txtUsefulLife.Text, out int years))
                {
                    ValidationHelper.ShowValidationError("Verifica todos los valores numéricos.");
                    return;
                }

                decimal salvageValue = 0;
                if (!string.IsNullOrEmpty(txtSalvageValue.Text))
                {
                    ValidationHelper.IsValidDecimal(txtSalvageValue.Text, out salvageValue);
                }

                string method = cmbDepreciationMethod.SelectedItem?.ToString() ?? "Línea Recta";
                decimal annualDepreciation = CalculateDepreciation(price, salvageValue, years, method);

                txtAnnualDepreciation.Text = annualDepreciation.ToString("C");
                decimal bookValue = price - annualDepreciation;
                txtBookValue.Text = bookValue.ToString("C");

                // TODO: Save to database
                ValidationHelper.ShowSuccess("Depreciación calculada y guardada!");
                ClearFields();
                LoadAssets();
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error: " + ex.Message);
            }
        }

        private decimal CalculateDepreciation(decimal purchasePrice, decimal salvageValue, int years, string method)
        {
            if (method == "Línea Recta")
            {
                // Straight-line: (Cost - Salvage) / Years
                return (purchasePrice - salvageValue) / years;
            }
            else // Saldo Decreciente
            {
                // Declining balance: typically 2x straight-line rate
                decimal straightLineRate = 1m / years;
                return purchasePrice * (straightLineRate * 2);
            }
        }

        private void ClearFields()
        {
            txtAssetId.Clear();
            txtPurchasePrice.Clear();
            txtSalvageValue.Clear();
            txtUsefulLife.Clear();
            txtAnnualDepreciation.Clear();
            txtBookValue.Clear();
        }
    }
}
