using System;
using System.Data;
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
            this.Load += FrmAssets_Load;
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
            try
            {
                actCtrl.SerialNumber = txtSerial.Text;
                actCtrl.Type = cmbType.Text;
                actCtrl.Brand = txtBrand.Text;
                actCtrl.PurchasePrice = ParseDecimal(txtPurchasePrice.Text);
                actCtrl.SalvageValue = ParseDecimal(txtSalvage.Text);
                actCtrl.UsefulLifeYears = ParseInt(txtUsefulLife.Text);
                actCtrl.LocationId = cmbSede.SelectedValue != null ? ParseInt(cmbSede.SelectedValue.ToString()) : 0;

                if (actCtrl.CreateAsset())
                {
                    MessageBox.Show("Activo registrado correctamente.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarActivos();
                    LimpiarFormActivo();
                }
                else
                {
                    MessageBox.Show("Error al registrar el activo. Verifica los datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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

                // Show current depreciated value
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
            try
            {
                actCtrl.AssetId = int.Parse(txtAssetId.Text);
                actCtrl.EmployeeId = int.Parse(txtEmpId.Text);
                actCtrl.AssignDate = dtpAssign.Value;

                if (actCtrl.BindAssetToEmployee())
                {
                    MessageBox.Show("Activo asignado al empleado.", "Asignación", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadHistory();
                }
            }
            catch (Exception)
            {
                ValidationHelper.ShowValidationError("Revisa que los IDs sean válidos.");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                actCtrl.AssetId = int.Parse(txtAssetId.Text);
                LoadHistory();
            }
            catch (Exception) { }
        }

        private void LoadHistory()
        {
            DataTable dt = actCtrl.ConsultHistory();
            if (dt != null) dgvHistory.DataSource = dt;
        }

        // ─────────────────────────────────────────
        // TAB 3: Mantenimiento Preventivo
        // ─────────────────────────────────────────

        private void btnScheduleMaint_Click(object sender, EventArgs e)
        {
            try
            {
                actCtrl.AssetId = ParseInt(txtMaintAssetId.Text);
                string desc = txtMaintDesc.Text.Trim();
                DateTime fecha = dtpMaint.Value;

                if (actCtrl.ScheduleMaintenance(desc, fecha))
                {
                    MessageBox.Show("Mantenimiento programado exitosamente.", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarMantenimiento();
                    txtMaintDesc.Clear();
                }
                else
                {
                    MessageBox.Show("Error al programar el mantenimiento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void btnLoadMaint_Click(object sender, EventArgs e)
        {
            CargarMantenimiento();
        }

        private void CargarMantenimiento()
        {
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
            decimal.TryParse(val?.Replace(",", "."), System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out result);
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
