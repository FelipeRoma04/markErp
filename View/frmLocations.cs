using System;
using System.Data;
using System.Windows.Forms;
using Proyecto.Controler;
using Proyecto.Model;

namespace Proyecto.View
{
    public partial class frmLocations : Form
    {
        private conexionModel conn;

        public frmLocations()
        {
            InitializeComponent();
            conn = new conexionModel();
            InitializeControls();
        }

        private void InitializeControls()
        {
            dgvLocations.AutoGenerateColumns = false;
            dgvLocations.Columns.Clear();

            dgvLocations.Columns.Add("Id", "ID");
            dgvLocations.Columns.Add("Name", "Nombre Sede");
            dgvLocations.Columns.Add("Address", "Dirección");
            dgvLocations.Columns.Add("City", "Ciudad");
            dgvLocations.Columns.Add("Phone", "Teléfono");
            dgvLocations.Columns.Add("Manager", "Gerente");

            LoadLocations();
        }

        private void LoadLocations()
        {
            try
            {
                DataTable locations = conn.ejecutarConsulta("SELECT * FROM Locations ORDER BY Name");
                if (locations != null)
                    dgvLocations.DataSource = locations;
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error cargando ubicaciones: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidationHelper.IsNotEmpty(txtName.Text) ||
                    !ValidationHelper.IsNotEmpty(txtAddress.Text) ||
                    !ValidationHelper.IsNotEmpty(txtCity.Text))
                {
                    ValidationHelper.ShowValidationError("Completa todos los campos requeridos.");
                    return;
                }

                string query = "INSERT INTO Locations (Name, Address, City, Phone, Manager) " +
                              "VALUES (@name, @address, @city, @phone, @manager)";
                var parameters = new System.Collections.Generic.Dictionary<string, object>
                {
                    { "@name", txtName.Text },
                    { "@address", txtAddress.Text },
                    { "@city", txtCity.Text },
                    { "@phone", txtPhone.Text },
                    { "@manager", txtManager.Text }
                };

                if (conn.ejecutarComandoParametrizado(query, parameters) > 0)
                {
                    ValidationHelper.ShowSuccess("Sede agregada correctamente!");
                    ClearFields();
                    LoadLocations();
                }
                else
                {
                    ValidationHelper.ShowError("Error al agregar sede.");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLocations.SelectedRows.Count == 0)
                {
                    ValidationHelper.ShowValidationError("Selecciona una sede para editar.");
                    return;
                }

                int id = Convert.ToInt32(dgvLocations.SelectedRows[0].Cells["Id"].Value);

                string query = "UPDATE Locations SET Name=@name, Address=@address, City=@city, " +
                              "Phone=@phone, Manager=@manager WHERE Id=@id";
                var parameters = new System.Collections.Generic.Dictionary<string, object>
                {
                    { "@id", id },
                    { "@name", txtName.Text },
                    { "@address", txtAddress.Text },
                    { "@city", txtCity.Text },
                    { "@phone", txtPhone.Text },
                    { "@manager", txtManager.Text }
                };

                if (conn.ejecutarComandoParametrizado(query, parameters) > 0)
                {
                    ValidationHelper.ShowSuccess("Sede actualizada correctamente!");
                    ClearFields();
                    LoadLocations();
                }
                else
                {
                    ValidationHelper.ShowError("Error al actualizar sede.");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvLocations.SelectedRows.Count == 0)
                {
                    ValidationHelper.ShowValidationError("Selecciona una sede para eliminar.");
                    return;
                }

                if (MessageBox.Show("¿Estás seguro?", "Confirmar eliminación", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    return;

                int id = Convert.ToInt32(dgvLocations.SelectedRows[0].Cells["Id"].Value);
                string query = "DELETE FROM Locations WHERE Id=@id";
                var parameters = new System.Collections.Generic.Dictionary<string, object> { { "@id", id } };

                if (conn.ejecutarComandoParametrizado(query, parameters) > 0)
                {
                    ValidationHelper.ShowSuccess("Sede eliminada correctamente!");
                    LoadLocations();
                }
                else
                {
                    ValidationHelper.ShowError("Error al eliminar sede.");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error: " + ex.Message);
            }
        }

        private void dgvLocations_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            txtName.Text = dgvLocations.Rows[e.RowIndex].Cells["Name"].Value?.ToString() ?? "";
            txtAddress.Text = dgvLocations.Rows[e.RowIndex].Cells["Address"].Value?.ToString() ?? "";
            txtCity.Text = dgvLocations.Rows[e.RowIndex].Cells["City"].Value?.ToString() ?? "";
            txtPhone.Text = dgvLocations.Rows[e.RowIndex].Cells["Phone"].Value?.ToString() ?? "";
            txtManager.Text = dgvLocations.Rows[e.RowIndex].Cells["Manager"].Value?.ToString() ?? "";
        }

        private void ClearFields()
        {
            txtName.Clear();
            txtAddress.Clear();
            txtCity.Clear();
            txtPhone.Clear();
            txtManager.Clear();
        }
    }
}
