using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//importa desde proyecto la carpeta Controler
using Proyecto.Controler;
using Proyecto.Model;

namespace Proyecto.View
{
    public partial class frmDepartamentos : Form
    {

        departamentoControler odepartamentoControler;
        bool canCreate;
        bool canEdit;
        bool canDelete;


        public frmDepartamentos()
        {
            odepartamentoControler = new departamentoControler();
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.IsNotEmpty(txtNombreDepartamento.Text))
            {
                ValidationHelper.ShowValidationError("El nombre del departamento no puede estar vacío.");
                return;
            }

            try 
            {
                recuperarInformacion();
                
                if (odepartamentoControler.agregar())
                {
                    ValidationHelper.ShowSuccess("Departamento agregado exitosamente.");
                    CargarDepartamentos();
                    Limpiar();
                }
                else
                {
                    ValidationHelper.ShowError("No se pudo agregar el departamento.");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error de sistema: " + ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.IsValidInteger(txtId.Text, out int id))
            {
                ValidationHelper.ShowValidationError("Selecciona un registro válido.");
                return;
            }

            if (!ValidationHelper.IsNotEmpty(txtNombreDepartamento.Text))
            {
                ValidationHelper.ShowValidationError("El nombre del departamento no puede estar vacío.");
                return;
            }

            try
            {
                recuperarInformacion();
                if (odepartamentoControler.modificar())
                {
                    ValidationHelper.ShowSuccess("Departamento actualizado.");
                    CargarDepartamentos();
                    Limpiar();
                }
                else
                {
                    ValidationHelper.ShowError("No se pudo actualizar el departamento.");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error de sistema: " + ex.Message);
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.IsValidInteger(txtId.Text, out int id))
            {
                ValidationHelper.ShowValidationError("Selecciona un registro válido.");
                return;
            }

            if (MessageBox.Show("¿Eliminar el departamento seleccionado?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    recuperarInformacion();
                    if (odepartamentoControler.eliminar())
                    {
                        ValidationHelper.ShowSuccess("Departamento eliminado.");
                        CargarDepartamentos();
                        Limpiar();
                    }
                    else
                    {
                        ValidationHelper.ShowError("No se pudo eliminar el departamento.");
                    }
                }
                catch (Exception ex)
                {
                    ValidationHelper.ShowError("Error de sistema: " + ex.Message);
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void frmDepartamentos_Load(object sender, EventArgs e)
        {
            ApplyStyle();
            ApplyPermissions();
            CargarDepartamentos();
            Limpiar();
        }

        private void ApplyStyle()
        {
            this.BackColor = UITheme.BgColor;
            UITheme.StyleLabel(id);
            UITheme.StyleLabel(nombreDepartamento);
            UITheme.StyleTextBox(txtId);
            UITheme.StyleTextBox(txtNombreDepartamento);
            UITheme.StyleDataGrid(dgvDepartamentos);
            
            // Buttons are already styled via Designer properties I set, 
            // but ensuring they match theme colors just in case:
            btnAgregar.BackColor = UITheme.SuccessColor;
            btnModificar.BackColor = UITheme.AccentColor;
            btnBorrar.BackColor = UITheme.DangerColor;
            btnCancelar.BackColor = UITheme.SecondaryColor;
        }

        private void CargarDepartamentos()
        {
            dgvDepartamentos.DataSource = odepartamentoControler.listar();
            dgvDepartamentos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void Limpiar()
        {
            txtId.Clear();
            txtNombreDepartamento.Clear();
            btnAgregar.Enabled = canCreate;
            btnModificar.Enabled = false;
            btnBorrar.Enabled = false;
        }

        private void dgvDepartamentos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDepartamentos.Rows[e.RowIndex];
                txtId.Text = row.Cells[0].Value.ToString();
                txtNombreDepartamento.Text = row.Cells[1].Value.ToString();

                btnAgregar.Enabled = false;
                btnModificar.Enabled = canEdit;
                btnBorrar.Enabled = canDelete;
            }
        }

        private void ApplyPermissions()
        {
            canCreate = PermissionHelper.HasPermission(PermissionHelper.Feature.Departments, PermissionHelper.Action.Create);
            canEdit = PermissionHelper.HasPermission(PermissionHelper.Feature.Departments, PermissionHelper.Action.Edit);
            canDelete = PermissionHelper.HasPermission(PermissionHelper.Feature.Departments, PermissionHelper.Action.Delete);
        }
        private void recuperarInformacion () 
        {
            int id = 0;
            if (!string.IsNullOrEmpty(txtId.Text))
            {
                int.TryParse(txtId.Text, out id);
            }
            odepartamentoControler.id = id;
            odepartamentoControler.departamento = txtNombreDepartamento.Text;
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
