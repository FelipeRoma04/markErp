using System;
using System.Data;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmAssetAssignments : Form
    {
        private assetControler assetCtrl;

        public frmAssetAssignments()
        {
            InitializeComponent();
            assetCtrl = new assetControler();
            InitializeControls();
        }

        private void InitializeControls()
        {
            // Initialize DataGridView
            dgvAssignments.AutoGenerateColumns = false;
            dgvAssignments.Columns.Clear();

            dgvAssignments.Columns.Add("AssetId", "Asset ID");
            dgvAssignments.Columns.Add("EmployeeId", "Employee ID");
            dgvAssignments.Columns.Add("AssignedDate", "Fecha Asignación");
            dgvAssignments.Columns.Add("ReturnedDate", "Fecha Devolución");
            dgvAssignments.Columns.Add("Status", "Estado");

            LoadAssignments();
        }

        private void LoadAssignments()
        {
            try
            {
                // TODO: Implement data retrieval from assetModel
                // For now, show empty grid with message
                MessageBox.Show("Cargar datos desde base de datos", "Info");
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error cargando asignaciones: " + ex.Message);
            }
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidationHelper.IsValidInteger(txtAssetId.Text, out int assetId) ||
                    !ValidationHelper.IsValidInteger(txtEmployeeId.Text, out int empId))
                {
                    ValidationHelper.ShowValidationError("Asset ID y Employee ID deben ser números válidos.");
                    return;
                }

                // TODO: Insert assignment record
                ValidationHelper.ShowSuccess("Activo asignado correctamente!");
                ClearFields();
                LoadAssignments();
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error: " + ex.Message);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvAssignments.SelectedRows.Count == 0)
                {
                    ValidationHelper.ShowValidationError("Selecciona un registro para devolver.");
                    return;
                }

                // TODO: Update return date
                ValidationHelper.ShowSuccess("Activo devuelto correctamente!");
                LoadAssignments();
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtAssetId.Clear();
            txtEmployeeId.Clear();
        }
    }
}
