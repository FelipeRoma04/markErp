using System;
using System.Data;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmMaintenance : Form
    {
        private assetControler assetCtrl;

        public frmMaintenance()
        {
            InitializeComponent();
            assetCtrl = new assetControler();
            InitializeControls();
        }

        private void InitializeControls()
        {
            dgvSchedule.AutoGenerateColumns = false;
            dgvSchedule.Columns.Clear();

            dgvSchedule.Columns.Add("Id", "ID");
            dgvSchedule.Columns.Add("AssetId", "Asset ID");
            dgvSchedule.Columns.Add("MaintenanceType", "Tipo Mantenimiento");
            dgvSchedule.Columns.Add("ScheduledDate", "Fecha Programada");
            dgvSchedule.Columns.Add("LastPerformedDate", "Última Realización");
            dgvSchedule.Columns.Add("NextDueDate", "Próxima Fecha");
            dgvSchedule.Columns.Add("Status", "Estado");

            LoadSchedules();
        }

        private void LoadSchedules()
        {
            try
            {
                // TODO: Load maintenance schedules from database
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error cargando programación: " + ex.Message);
            }
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidationHelper.IsValidInteger(txtAssetId.Text, out int assetId) ||
                    !ValidationHelper.IsNotEmpty(cmbMaintenanceType.Text))
                {
                    ValidationHelper.ShowValidationError("Completa todos los campos requeridos.");
                    return;
                }

                // TODO: Insert maintenance schedule
                ValidationHelper.ShowSuccess("Mantenimiento programado correctamente!");
                ClearFields();
                LoadSchedules();
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error: " + ex.Message);
            }
        }

        private void btnCompleteTask_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvSchedule.SelectedRows.Count == 0)
                {
                    ValidationHelper.ShowValidationError("Selecciona una programación para marcar como completa.");
                    return;
                }

                // TODO: Mark maintenance as completed
                ValidationHelper.ShowSuccess("Mantenimiento marcado como completado!");
                LoadSchedules();
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error: " + ex.Message);
            }
        }

        private void ClearFields()
        {
            txtAssetId.Clear();
            cmbMaintenanceType.SelectedIndex = -1;
            dtpScheduledDate.Value = DateTime.Now.AddDays(7);
        }
    }
}
