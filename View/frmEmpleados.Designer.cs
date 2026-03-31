namespace Proyecto.View
{
    partial class frmEmpleados
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.id = new System.Windows.Forms.Label();
            this.txtId = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtLastName = new System.Windows.Forms.TextBox();
            this.txtSecondName = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.cbxDepartamento = new System.Windows.Forms.ComboBox();
            this.name = new System.Windows.Forms.Label();
            this.primerApellido = new System.Windows.Forms.Label();
            this.segundoApellido = new System.Windows.Forms.Label();
            this.Email = new System.Windows.Forms.Label();
            this.departamento = new System.Windows.Forms.Label();
            this.picFoto = new System.Windows.Forms.PictureBox();
            this.btnExaminar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnModificar = new System.Windows.Forms.Button();
            this.btnBorrar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.dgvEmpleados = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).BeginInit();
            this.SuspendLayout();
            // 
            // id
            // 
            this.id.AutoSize = true;
            this.id.Location = new System.Drawing.Point(226, 3);
            this.id.Name = "id";
            this.id.Size = new System.Drawing.Size(23, 16);
            this.id.TabIndex = 0;
            this.id.Text = "ID:";
            // 
            // txtId
            // 
            this.txtId.Location = new System.Drawing.Point(216, 32);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(167, 22);
            this.txtId.TabIndex = 1;
            this.txtId.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(425, 32);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(210, 22);
            this.txtName.TabIndex = 2;
            // 
            // txtLastName
            // 
            this.txtLastName.Location = new System.Drawing.Point(216, 99);
            this.txtLastName.Name = "txtLastName";
            this.txtLastName.Size = new System.Drawing.Size(167, 22);
            this.txtLastName.TabIndex = 3;
            // 
            // txtSecondName
            // 
            this.txtSecondName.Location = new System.Drawing.Point(425, 99);
            this.txtSecondName.Name = "txtSecondName";
            this.txtSecondName.Size = new System.Drawing.Size(210, 22);
            this.txtSecondName.TabIndex = 4;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(216, 165);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(419, 22);
            this.txtEmail.TabIndex = 5;
            // 
            // cbxDepartamento
            // 
            this.cbxDepartamento.FormattingEnabled = true;
            this.cbxDepartamento.Location = new System.Drawing.Point(216, 232);
            this.cbxDepartamento.Name = "cbxDepartamento";
            this.cbxDepartamento.Size = new System.Drawing.Size(419, 24);
            this.cbxDepartamento.TabIndex = 6;
            // 
            // name
            // 
            this.name.AutoSize = true;
            this.name.Location = new System.Drawing.Point(437, 3);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(59, 16);
            this.name.TabIndex = 7;
            this.name.Text = "Nombre:";
            // 
            // primerApellido
            // 
            this.primerApellido.AutoSize = true;
            this.primerApellido.Location = new System.Drawing.Point(226, 71);
            this.primerApellido.Name = "primerApellido";
            this.primerApellido.Size = new System.Drawing.Size(102, 16);
            this.primerApellido.TabIndex = 8;
            this.primerApellido.Text = "Primer Apellido:";
            // 
            // segundoApellido
            // 
            this.segundoApellido.AutoSize = true;
            this.segundoApellido.Location = new System.Drawing.Point(437, 71);
            this.segundoApellido.Name = "segundoApellido";
            this.segundoApellido.Size = new System.Drawing.Size(118, 16);
            this.segundoApellido.TabIndex = 9;
            this.segundoApellido.Text = "Segundo Apellido:";
            // 
            // Email
            // 
            this.Email.AutoSize = true;
            this.Email.Location = new System.Drawing.Point(226, 137);
            this.Email.Name = "Email";
            this.Email.Size = new System.Drawing.Size(51, 16);
            this.Email.TabIndex = 10;
            this.Email.Text = "Correo:";
            // 
            // departamento
            // 
            this.departamento.AutoSize = true;
            this.departamento.Location = new System.Drawing.Point(226, 202);
            this.departamento.Name = "departamento";
            this.departamento.Size = new System.Drawing.Size(96, 16);
            this.departamento.TabIndex = 11;
            this.departamento.Text = "Departamento:";
            this.departamento.Click += new System.EventHandler(this.label6_Click);
            // 
            // picFoto
            // 
            this.picFoto.Location = new System.Drawing.Point(28, 3);
            this.picFoto.Name = "picFoto";
            this.picFoto.Size = new System.Drawing.Size(172, 253);
            this.picFoto.TabIndex = 12;
            this.picFoto.TabStop = false;
            this.picFoto.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btnExaminar
            // 
            this.btnExaminar.Location = new System.Drawing.Point(37, 274);
            this.btnExaminar.Name = "btnExaminar";
            this.btnExaminar.Size = new System.Drawing.Size(159, 23);
            this.btnExaminar.TabIndex = 13;
            this.btnExaminar.Text = "Examinar...";
            this.btnExaminar.UseVisualStyleBackColor = true;
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(229, 274);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(75, 23);
            this.btnAgregar.TabIndex = 14;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = true;
            // 
            // btnModificar
            // 
            this.btnModificar.Location = new System.Drawing.Point(333, 274);
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(75, 23);
            this.btnModificar.TabIndex = 15;
            this.btnModificar.Text = "Modificar";
            this.btnModificar.UseVisualStyleBackColor = true;
            // 
            // btnBorrar
            // 
            this.btnBorrar.Location = new System.Drawing.Point(433, 274);
            this.btnBorrar.Name = "btnBorrar";
            this.btnBorrar.Size = new System.Drawing.Size(75, 23);
            this.btnBorrar.TabIndex = 16;
            this.btnBorrar.Text = "Borrar";
            this.btnBorrar.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(533, 274);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(75, 23);
            this.btnCancelar.TabIndex = 17;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // dgvEmpleados
            // 
            this.dgvEmpleados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEmpleados.Location = new System.Drawing.Point(37, 312);
            this.dgvEmpleados.Name = "dgvEmpleados";
            this.dgvEmpleados.RowHeadersWidth = 51;
            this.dgvEmpleados.RowTemplate.Height = 24;
            this.dgvEmpleados.Size = new System.Drawing.Size(667, 169);
            this.dgvEmpleados.TabIndex = 18;
            // 
            // frmEmpleados
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 493);
            this.Controls.Add(this.dgvEmpleados);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnBorrar);
            this.Controls.Add(this.btnModificar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnExaminar);
            this.Controls.Add(this.picFoto);
            this.Controls.Add(this.departamento);
            this.Controls.Add(this.Email);
            this.Controls.Add(this.segundoApellido);
            this.Controls.Add(this.primerApellido);
            this.Controls.Add(this.name);
            this.Controls.Add(this.cbxDepartamento);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.txtSecondName);
            this.Controls.Add(this.txtLastName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtId);
            this.Controls.Add(this.id);
            this.Name = "frmEmpleados";
            this.Text = "frmEmpleados";
            ((System.ComponentModel.ISupportInitialize)(this.picFoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEmpleados)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label id;
        private System.Windows.Forms.TextBox txtId;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtLastName;
        private System.Windows.Forms.TextBox txtSecondName;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.ComboBox cbxDepartamento;
        private System.Windows.Forms.Label name;
        private System.Windows.Forms.Label primerApellido;
        private System.Windows.Forms.Label segundoApellido;
        private System.Windows.Forms.Label Email;
        private System.Windows.Forms.Label departamento;
        private System.Windows.Forms.PictureBox picFoto;
        private System.Windows.Forms.Button btnExaminar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnModificar;
        private System.Windows.Forms.Button btnBorrar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DataGridView dgvEmpleados;
    }
}