using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Proyecto.Controler;

namespace Proyecto.View
{
    public partial class frmEmpleados : Form
    {
        empleadosControler empleadoCtrl;
        byte[] fotoSeleccionada = null;
        bool canCreate;
        bool canEdit;
        bool canDelete;

        public frmEmpleados()
        {
            InitializeComponent();
            empleadoCtrl = new empleadosControler();
        }

        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            ApplyStyle();
            ApplyPermissions();
            CargarDepartamentos();
            ActualizarGrilla();
            LimpiarFormulario();
        }

        private void ApplyStyle()
        {
            this.BackColor = UITheme.BgColor;
            UITheme.StyleLabel(id);
            UITheme.StyleLabel(name);
            UITheme.StyleLabel(primerApellido);
            UITheme.StyleLabel(segundoApellido);
            UITheme.StyleLabel(Email);
            UITheme.StyleLabel(departamento);

            UITheme.StyleTextBox(txtId);
            UITheme.StyleTextBox(txtName);
            UITheme.StyleTextBox(txtLastName);
            UITheme.StyleTextBox(txtSecondName);
            UITheme.StyleTextBox(txtEmail);
            
            UITheme.StyleDataGrid(dgvEmpleados);

            btnAgregar.BackColor = UITheme.SuccessColor;
            btnModificar.BackColor = UITheme.AccentColor;
            btnBorrar.BackColor = UITheme.DangerColor;
            btnCancelar.BackColor = UITheme.SecondaryColor;
        }

        private void ApplyPermissions()
        {
            canCreate = PermissionHelper.HasPermission(PermissionHelper.Feature.Employees, PermissionHelper.Action.Create);
            canEdit = PermissionHelper.HasPermission(PermissionHelper.Feature.Employees, PermissionHelper.Action.Edit);
            canDelete = PermissionHelper.HasPermission(PermissionHelper.Feature.Employees, PermissionHelper.Action.Delete);
        }

        private void CargarDepartamentos()
        {
            try
            {
                Proyecto.Model.conexionModel cnx = new Proyecto.Model.conexionModel();
                DataTable dt = cnx.ejecutarConsulta("SELECT * FROM Departamentos");
                cbxDepartamento.DataSource = dt;
                cbxDepartamento.DisplayMember = "departamento";
                cbxDepartamento.ValueMember = "Id";
                cbxDepartamento.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error al cargar departamentos: " + ex.Message);
            }
        }

        private void ActualizarGrilla()
        {
            dgvEmpleados.DataSource = empleadoCtrl.listar();
            if (dgvEmpleados.Columns.Count > 0)
            {
                if (dgvEmpleados.Columns.Contains("Id")) dgvEmpleados.Columns["Id"].Visible = false;
                if (dgvEmpleados.Columns.Contains("PicFoto")) dgvEmpleados.Columns["PicFoto"].Visible = false;
                if (dgvEmpleados.Columns.Contains("DepartmentId")) dgvEmpleados.Columns["DepartmentId"].Visible = false;
            }
        }

        private void LimpiarFormulario()
        {
            txtId.Clear();
            txtName.Clear();
            txtLastName.Clear();
            txtSecondName.Clear();
            txtEmail.Clear();
            cbxDepartamento.SelectedIndex = -1;
            picFoto.Image = null;
            fotoSeleccionada = null;
            
            btnAgregar.Enabled = canCreate;
            btnModificar.Enabled = false;
            btnBorrar.Enabled = false;
        }

        private void RecolectarDatos()
        {
            empleadoCtrl.id = string.IsNullOrEmpty(txtId.Text) ? 0 : (int.TryParse(txtId.Text, out int parsedId) ? parsedId : 0);
            empleadoCtrl.name = txtName.Text;
            empleadoCtrl.lastName = txtLastName.Text;
            empleadoCtrl.secondName = txtSecondName.Text;
            empleadoCtrl.email = txtEmail.Text;
            
            if (cbxDepartamento.SelectedValue != null)
            {
                int.TryParse(cbxDepartamento.SelectedValue.ToString(), out int deptoId);
                empleadoCtrl.departamento = deptoId;
            }
            else
            {
                empleadoCtrl.departamento = 0;
            }

            empleadoCtrl.picFoto = fotoSeleccionada;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.IsNotEmpty(txtName.Text) || !ValidationHelper.IsNotEmpty(txtLastName.Text))
            {
                ValidationHelper.ShowValidationError("Nombre y Primer Apellido son obligatorios.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !ValidationHelper.IsValidEmail(txtEmail.Text))
            {
                ValidationHelper.ShowValidationError("Correo no tiene un formato válido.");
                return;
            }

            try
            {
                RecolectarDatos();
                if (empleadoCtrl.agregar())
                {
                    ValidationHelper.ShowSuccess("Empleado agregado correctamente.");
                    ActualizarGrilla();
                    LimpiarFormulario();
                }
                else
                {
                    ValidationHelper.ShowError("No se pudo agregar el empleado.");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error de sistema: " + ex.Message);
            }
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.IsValidInteger(txtId.Text, out _))
            {
                ValidationHelper.ShowValidationError("Selecciona un registro válido para modificar.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !ValidationHelper.IsValidEmail(txtEmail.Text))
            {
                ValidationHelper.ShowValidationError("Correo no tiene un formato válido.");
                return;
            }

            try
            {
                RecolectarDatos();
                if (empleadoCtrl.modificar())
                {
                    ValidationHelper.ShowSuccess("Empleado modificado correctamente.");
                    ActualizarGrilla();
                    LimpiarFormulario();
                }
                else
                {
                    ValidationHelper.ShowError("No se pudo modificar el empleado.");
                }
            }
            catch (Exception ex)
            {
                ValidationHelper.ShowError("Error de sistema: " + ex.Message);
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.IsValidInteger(txtId.Text, out _))
            {
                ValidationHelper.ShowValidationError("Selecciona un registro válido para eliminar.");
                return;
            }

            if (MessageBox.Show("¿Seguro que deseas eliminar este empleado?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    RecolectarDatos();
                    if (empleadoCtrl.eliminar())
                    {
                        ValidationHelper.ShowSuccess("Empleado eliminado definitivamente.");
                        ActualizarGrilla();
                        LimpiarFormulario();
                    }
                    else
                    {
                        ValidationHelper.ShowError("No se pudo eliminar el empleado.");
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
            LimpiarFormulario();
        }

        private void btnExaminar_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Filter = "Imagenes|*.jpg;*.jpeg;*.png;*.bmp;";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    picFoto.Image = Image.FromFile(dlg.FileName);
                    
                    using (MemoryStream ms = new MemoryStream())
                    {
                        picFoto.Image.Save(ms, picFoto.Image.RawFormat);
                        fotoSeleccionada = ms.ToArray();
                    }
                }
            }
        }

        private void dgvEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvEmpleados.Rows[e.RowIndex];
                txtId.Text = row.Cells["Id"].Value.ToString();
                txtName.Text = row.Cells["Name"].Value.ToString();
                txtLastName.Text = row.Cells["LastName"].Value.ToString();
                txtSecondName.Text = row.Cells["SecondName"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                
                if (row.Cells["DepartmentId"].Value != DBNull.Value)
                    cbxDepartamento.SelectedValue = row.Cells["DepartmentId"].Value;

                if (row.Cells["PicFoto"].Value != DBNull.Value && row.Cells["PicFoto"].Value is byte[] bytes)
                {
                    fotoSeleccionada = bytes;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        picFoto.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    picFoto.Image = null;
                    fotoSeleccionada = null;
                }

                btnAgregar.Enabled = false;
                btnModificar.Enabled = canEdit;
                btnBorrar.Enabled = canDelete;
            }
        }
    }
}
