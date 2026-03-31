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

            this.Load += FrmDepartamentos_Load;
            btnModificar.Click += BtnModificar_Click;
            btnBorrar.Click += BtnBorrar_Click;
            btnCancelar.Click += BtnCancelar_Click;
            dgvDepartamentos.CellMouseClick += DgvDepartamentos_CellMouseClick;
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
                //obtener info de la interfaz grafica
                recuperarInformacion();
                
                if (odepartamentoControler.agregar())
                {
                    MessageBox.Show("Departamento agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo agregar el departamento.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de sistema: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnModificar_Click(object sender, EventArgs e)
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

        private void BtnBorrar_Click(object sender, EventArgs e)
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

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void FrmDepartamentos_Load(object sender, EventArgs e)
        {
            ApplyPermissions();
            CargarDepartamentos();
            Limpiar();
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

        private void DgvDepartamentos_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDepartamentos.Rows[e.RowIndex];
                txtId.Text = row.Cells["Id"].Value.ToString();
                txtNombreDepartamento.Text = row.Cells["departamento"].Value.ToString();

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
