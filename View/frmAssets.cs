using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmAssets : Form
    {
        assetControler actCtrl;

        public frmAssets()
        {
            InitializeComponent();
            actCtrl = new assetControler();
            ApplyStyle();
            this.Load += FrmAssets_Load;
        }

        private void ApplyStyle()
        {
            this.BackColor = UITheme.BgColor;
            pnlHeader.BackColor = UITheme.PrimaryColor;
            lblTitle.ForeColor = Color.White;
            lblTitle.Font = UITheme.FontHeader;

            // Tabs
            tabActivos.BackColor = Color.White;
            tabAsignacion.BackColor = Color.White;
            tabMantenimiento.BackColor = Color.White;

            // Grids
            UITheme.StyleDataGrid(dgvActivos);
            UITheme.StyleDataGrid(dgvHistory);
            UITheme.StyleDataGrid(dgvMaint);

            // Labels
            UITheme.StyleLabel(lblSerial);
            UITheme.StyleLabel(lblType);
            UITheme.StyleLabel(lblBrand);
            UITheme.StyleLabel(lblSede);
            UITheme.StyleLabel(lblPurchasePrice);
            UITheme.StyleLabel(lblSalvage);
            UITheme.StyleLabel(lblUsefulLife);
            UITheme.StyleLabel(lblAssetId);
            UITheme.StyleLabel(lblEmpId);
            UITheme.StyleLabel(lblAsign);
            UITheme.StyleLabel(lblMaintAssetId);
            UITheme.StyleLabel(lblMaintDesc);
            UITheme.StyleLabel(lblMaintDate);
            
            UITheme.StyleLabel(lblDepreciation, true);
            lblDepreciationValue.ForeColor = UITheme.SuccessColor;

            // TextBoxes
            UITheme.StyleTextBox(txtSerial);
            UITheme.StyleTextBox(txtBrand);
            UITheme.StyleTextBox(txtPurchasePrice);
            UITheme.StyleTextBox(txtSalvage);
            UITheme.StyleTextBox(txtUsefulLife);
            UITheme.StyleTextBox(txtAssetId);
            UITheme.StyleTextBox(txtEmpId);
            UITheme.StyleTextBox(txtMaintAssetId);
            UITheme.StyleTextBox(txtMaintDesc);

            // Buttons
            btnSaveAsset.BackColor = UITheme.SuccessColor;
            btnSearch.BackColor = UITheme.AccentColor;
            btnAssign.BackColor = UITheme.PrimaryColor;
            btnScheduleMaint.BackColor = UITheme.AccentColor;
            btnLoadMaint.BackColor = UITheme.SecondaryColor;
        }

        private void FrmAssets_Load(object sender, EventArgs e)
        {
            CargarSedes();
            CargarActivos();
        }

        // ─────────────────────────────────────────
        // TAB 1: Registro de Activos
        // ─────────────────────────────────────────

        private void CargarSedes()
        {
            DataTable dt = actCtrl.ObtenerSedes();
            if (dt != null)
            {
                cmbSede.DataSource = dt;
                cmbSede.DisplayMember = "Name";
                cmbSede.ValueMember = "Id";
                cmbSede.SelectedIndex = 0;
            }
        }

        private void CargarActivos()
        {
            DataTable dt = actCtrl.ConsultarActivos();
            if (dt != null) dgvActivos.DataSource = dt;
        }

        private void btnSaveAsset_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSerial.Text) || string.IsNullOrWhiteSpace(txtBrand.Text))
            {
                ValidationHelper.ShowValidationError("El número de serie y la marca son obligatorios.");
                return;
            }

            try
            {
                actCtrl.SerialNumber = txtSerial.Text;
                actCtrl.Type = cmbType.Text;
                actCtrl.Brand = txtBrand.Text;
                actCtrl.PurchasePrice = ParseDecimal(txtPurchasePrice.Text);
                actCtrl.SalvageValue = ParseDecimal(txtSalvage.Text);
                actCtrl.UsefulLifeYears = ParseInt(txtUsefulLife.Text);
                actCtrl.LocationId = cmbSede.SelectedValue != null ? Convert.ToInt32(cmbSede.SelectedValue) : 0;

                if (actCtrl.CreateAsset())
                {
                    ValidationHelper.ShowSuccess("Activo registrado correctamente.");
                    CargarActivos();
                    LimpiarFormActivo();
                }
                else
                {
                    ValidationHelper.ShowError("No se pudo registrar el activo. Verifique la conexión a base de datos.");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error inesperado: " + ex.Message);
            }
        }

        private void dgvActivos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DataGridViewRow row = dgvActivos.Rows[e.RowIndex];
                txtSerial.Text = row.Cells["SerialNumber"].Value?.ToString();
                cmbType.Text = row.Cells["Type"].Value?.ToString();
                txtBrand.Text = row.Cells["Brand"].Value?.ToString();
                txtPurchasePrice.Text = row.Cells["PurchasePrice"].Value?.ToString();
                txtSalvage.Text = row.Cells["SalvageValue"].Value?.ToString();
                txtUsefulLife.Text = row.Cells["UsefulLifeYears"].Value?.ToString();

                decimal purchase = ParseDecimal(row.Cells["PurchasePrice"].Value?.ToString());
                decimal salvage = ParseDecimal(row.Cells["SalvageValue"].Value?.ToString());
                int life = ParseInt(row.Cells["UsefulLifeYears"].Value?.ToString());
                
                DateTime purchaseDate = DateTime.Now;
                if (row.Cells["PurchaseDate"].Value != null && row.Cells["PurchaseDate"].Value != DBNull.Value)
                    purchaseDate = Convert.ToDateTime(row.Cells["PurchaseDate"].Value);

                lblDepreciationValue.Text = actCtrl.CalculateCurrentValue(purchase, salvage, life, purchaseDate);
            }
            catch { }
        }

        private void LimpiarFormActivo()
        {
            txtSerial.Clear();
            txtBrand.Clear();
            txtPurchasePrice.Text = "0";
            txtSalvage.Text = "0";
            txtUsefulLife.Text = "0";
            cmbType.SelectedIndex = 0;
            lblDepreciationValue.Text = "—";
        }

        // ─────────────────────────────────────────
        // TAB 2: Asignación
        // ─────────────────────────────────────────

        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAssetId.Text) || string.IsNullOrWhiteSpace(txtEmpId.Text))
            {
                ValidationHelper.ShowValidationError("Debe indicar el ID del activo y el ID del empleado.");
                return;
            }

            try
            {
                actCtrl.AssetId = int.Parse(txtAssetId.Text);
                actCtrl.EmployeeId = int.Parse(txtEmpId.Text);
                actCtrl.AssignDate = dtpAssign.Value;

                if (actCtrl.BindAssetToEmployee())
                {
                    ValidationHelper.ShowSuccess("Activo asignado exitosamente al empleado.");
                    LoadHistory();
                }
            }
            catch (Exception)
            {
                ValidationHelper.ShowValidationError("Asegúrese de que los IDs sean números válidos.");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAssetId.Text)) return;
            try
            {
                actCtrl.AssetId = int.Parse(txtAssetId.Text);
                LoadHistory();
            }
            catch { }
        }

        private void LoadHistory()
        {
            DataTable dt = actCtrl.ConsultHistory();
            if (dt != null) dgvHistory.DataSource = dt;
        }

        // ─────────────────────────────────────────
        // TAB 3: Mantenimiento
        // ─────────────────────────────────────────

        private void btnScheduleMaint_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaintAssetId.Text) || string.IsNullOrWhiteSpace(txtMaintDesc.Text))
            {
                ValidationHelper.ShowValidationError("Indique el ID del activo y la descripción del mantenimiento.");
                return;
            }

            try
            {
                actCtrl.AssetId = ParseInt(txtMaintAssetId.Text);
                string desc = txtMaintDesc.Text.Trim();
                DateTime fecha = dtpMaint.Value;

                if (actCtrl.ScheduleMaintenance(desc, fecha))
                {
                    ValidationHelper.ShowSuccess("Mantenimiento programado.");
                    CargarMantenimiento();
                    txtMaintDesc.Clear();
                }
            }
            catch (Exception ex) { ValidationHelper.ShowError(ex.Message); }
        }

        private void btnLoadMaint_Click(object sender, EventArgs e)
        {
            CargarMantenimiento();
        }

        private void CargarMantenimiento()
        {
            if (string.IsNullOrWhiteSpace(txtMaintAssetId.Text)) return;
            actCtrl.AssetId = ParseInt(txtMaintAssetId.Text);
            DataTable dt = actCtrl.ConsultMaintenance();
            if (dt != null) dgvMaint.DataSource = dt;
        }

        // ─────────────────────────────────────────
        // Helpers
        // ─────────────────────────────────────────

        private decimal ParseDecimal(string val)
        {
            decimal result;
            string clean = val?.Replace("$", "").Replace(" ", "").Replace(",", ".");
            decimal.TryParse(clean, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out result);
            return result;
        }

        private int ParseInt(string val)
        {
            int result;
            int.TryParse(val, out result);
            return result;
        }
    }
}
