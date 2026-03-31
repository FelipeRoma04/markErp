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

        public frmEmpleados()
        {
            InitializeComponent();
            empleadoCtrl = new empleadosControler();
            
            // Wire events
            btnAgregar.Click += BtnAgregar_Click;
            btnModificar.Click += BtnModificar_Click;
            btnBorrar.Click += BtnBorrar_Click;
            btnCancelar.Click += BtnCancelar_Click;
            btnExaminar.Click += BtnExaminar_Click;
            dgvEmpleados.CellMouseClick += DgvEmpleados_CellMouseClick;
            this.Load += FrmEmpleados_Load;
        }

        private void FrmEmpleados_Load(object sender, EventArgs e)
        {
            CargarDepartamentos();
            ActualizarGrilla();
            LimpiarFormulario();
        }

        private void CargarDepartamentos()
        {
            // Simple query to fill cbxDepartamento
            Proyecto.Model.conexionModel cnx = new Proyecto.Model.conexionModel();
            DataTable dt = cnx.ejecutarConsulta("SELECT * FROM Departamentos");
            cbxDepartamento.DataSource = dt;
            cbxDepartamento.DisplayMember = "departamento";
            cbxDepartamento.ValueMember = "Id";
        }

        private void ActualizarGrilla()
        {
            dgvEmpleados.DataSource = empleadoCtrl.listar();
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
            
            btnAgregar.Enabled = true;
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

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.IsNotEmpty(txtName.Text) || !ValidationHelper.IsNotEmpty(txtLastName.Text))
            {
                ValidationHelper.ShowValidationError("Nombre y Primer Apellido son obligatorios.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !ValidationHelper.IsValidEmail(txtEmail.Text))
            {
                ValidationHelper.ShowValidationError("Correo no tiene un formato vÃ¡lido.");
                return;
            }

            RecolectarDatos();
            if (empleadoCtrl.agregar())
            {
                ValidationHelper.ShowSuccess("Empleado agregado correctamente.");
                ActualizarGrilla();
                LimpiarFormulario();
            }
            else
            {
                ValidationHelper.ShowError("Error al agregar empleado.");
            }
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.IsValidInteger(txtId.Text, out _))
            {
                ValidationHelper.ShowValidationError("Selecciona un registro vÃ¡lido para modificar.");
                return;
            }

            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !ValidationHelper.IsValidEmail(txtEmail.Text))
            {
                ValidationHelper.ShowValidationError("Correo no tiene un formato vÃ¡lido.");
                return;
            }

            RecolectarDatos();
            if (empleadoCtrl.modificar())
            {
                ValidationHelper.ShowSuccess("Empleado modificado correctamente.");
                ActualizarGrilla();
                LimpiarFormulario();
            }
            else
            {
                ValidationHelper.ShowError("Error al modificar empleado.");
            }
        }

                private void BtnBorrar_Click(object sender, EventArgs e)
        {
            if (!ValidationHelper.IsValidInteger(txtId.Text, out _))
            {
                ValidationHelper.ShowValidationError("Selecciona un registro válido para eliminar.");
                return;
            }

            RecolectarDatos();
            if (MessageBox.Show("¿Seguro que deseas eliminar este empleado?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (empleadoCtrl.eliminar())
                {
                    ValidationHelper.ShowSuccess("Empleado eliminado.");
                    ActualizarGrilla();
                    LimpiarFormulario();
                }
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void BtnExaminar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Imagenes|*.jpg;*.jpeg;*.png;*.bmp;";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                picFoto.Image = Image.FromFile(dlg.FileName);
                picFoto.SizeMode = PictureBoxSizeMode.Zoom;
                
                using (MemoryStream ms = new MemoryStream())
                {
                    picFoto.Image.Save(ms, picFoto.Image.RawFormat);
                    fotoSeleccionada = ms.ToArray();
                }
            }
        }

        private void DgvEmpleados_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
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

                if (row.Cells["PicFoto"].Value != DBNull.Value)
                {
                    byte[] bytes = (byte[])row.Cells["PicFoto"].Value;
                    fotoSeleccionada = bytes;
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        picFoto.Image = Image.FromStream(ms);
                        picFoto.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
                else
                {
                    picFoto.Image = null;
                    fotoSeleccionada = null;
                }

                btnAgregar.Enabled = false;
                btnModificar.Enabled = true;
                btnBorrar.Enabled = true;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e) { }
        private void textBox1_TextChanged(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
    }
}

