using System.Drawing;
using Proyecto.Controler;

namespace Proyecto.View
{
    partial class frmAttendance
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.chkAbsent = new System.Windows.Forms.CheckBox();
            this.txtOvertime = new System.Windows.Forms.TextBox();
            this.lblOvertime = new System.Windows.Forms.Label();
            this.txtHours = new System.Windows.Forms.TextBox();
            this.lblHours = new System.Windows.Forms.Label();
            this.dtpWorkDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.txtEmployeeId = new System.Windows.Forms.TextBox();
            this.lblEmployee = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlForm.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(430, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(262, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Control de Asistencia";
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.White;
            this.pnlForm.Controls.Add(this.btnSave);
            this.pnlForm.Controls.Add(this.txtNotes);
            this.pnlForm.Controls.Add(this.lblNotes);
            this.pnlForm.Controls.Add(this.chkAbsent);
            this.pnlForm.Controls.Add(this.txtOvertime);
            this.pnlForm.Controls.Add(this.lblOvertime);
            this.pnlForm.Controls.Add(this.txtHours);
            this.pnlForm.Controls.Add(this.lblHours);
            this.pnlForm.Controls.Add(this.dtpWorkDate);
            this.pnlForm.Controls.Add(this.lblDate);
            this.pnlForm.Controls.Add(this.txtEmployeeId);
            this.pnlForm.Controls.Add(this.lblEmployee);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForm.Location = new System.Drawing.Point(0, 60);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Padding = new System.Windows.Forms.Padding(30);
            this.pnlForm.Size = new System.Drawing.Size(430, 480);
            this.pnlForm.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(34, 400);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(360, 45);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "✍️ Registrar Asistencia";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtNotes
            // 
            this.txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNotes.Location = new System.Drawing.Point(34, 345);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(360, 30);
            this.txtNotes.TabIndex = 10;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNotes.Location = new System.Drawing.Point(34, 322);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(51, 20);
            this.lblNotes.TabIndex = 9;
            this.lblNotes.Text = "Notas:";
            // 
            // chkAbsent
            // 
            this.chkAbsent.AutoSize = true;
            this.chkAbsent.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkAbsent.Location = new System.Drawing.Point(34, 280);
            this.chkAbsent.Name = "chkAbsent";
            this.chkAbsent.Size = new System.Drawing.Size(126, 27);
            this.chkAbsent.TabIndex = 8;
            this.chkAbsent.Text = "Es Ausencia";
            this.chkAbsent.UseVisualStyleBackColor = true;
            // 
            // txtOvertime
            // 
            this.txtOvertime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtOvertime.Location = new System.Drawing.Point(34, 235);
            this.txtOvertime.Name = "txtOvertime";
            this.txtOvertime.Size = new System.Drawing.Size(360, 30);
            this.txtOvertime.TabIndex = 7;
            this.txtOvertime.Text = "0";
            // 
            // lblOvertime
            // 
            this.lblOvertime.AutoSize = true;
            this.lblOvertime.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblOvertime.Location = new System.Drawing.Point(34, 212);
            this.lblOvertime.Name = "lblOvertime";
            this.lblOvertime.Size = new System.Drawing.Size(87, 20);
            this.lblOvertime.TabIndex = 6;
            this.lblOvertime.Text = "Horas Extra:";
            // 
            // txtHours
            // 
            this.txtHours.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtHours.Location = new System.Drawing.Point(34, 175);
            this.txtHours.Name = "txtHours";
            this.txtHours.Size = new System.Drawing.Size(360, 30);
            this.txtHours.TabIndex = 5;
            this.txtHours.Text = "8";
            // 
            // lblHours
            // 
            this.lblHours.AutoSize = true;
            this.lblHours.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHours.Location = new System.Drawing.Point(34, 152);
            this.lblHours.Name = "lblHours";
            this.lblHours.Size = new System.Drawing.Size(130, 20);
            this.lblHours.TabIndex = 4;
            this.lblHours.Text = "Horas Trabajadas:";
            // 
            // dtpWorkDate
            // 
            this.dtpWorkDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpWorkDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpWorkDate.Location = new System.Drawing.Point(34, 115);
            this.dtpWorkDate.Name = "dtpWorkDate";
            this.dtpWorkDate.Size = new System.Drawing.Size(360, 30);
            this.dtpWorkDate.TabIndex = 3;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDate.Location = new System.Drawing.Point(34, 92);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(126, 20);
            this.lblDate.TabIndex = 2;
            this.lblDate.Text = "Fecha de Trabajo:";
            // 
            // txtEmployeeId
            // 
            this.txtEmployeeId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmployeeId.Location = new System.Drawing.Point(34, 55);
            this.txtEmployeeId.Name = "txtEmployeeId";
            this.txtEmployeeId.Size = new System.Drawing.Size(360, 30);
            this.txtEmployeeId.TabIndex = 1;
            // 
            // lblEmployee
            // 
            this.lblEmployee.AutoSize = true;
            this.lblEmployee.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEmployee.Location = new System.Drawing.Point(34, 32);
            this.lblEmployee.Name = "lblEmployee";
            this.lblEmployee.Size = new System.Drawing.Size(98, 20);
            this.lblEmployee.TabIndex = 0;
            this.lblEmployee.Text = "ID Empleado:";
            // 
            // frmAttendance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(430, 540);
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.pnlHeader);
            this.Name = "frmAttendance";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Asistencia — MarkErp";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtEmployeeId;
        private System.Windows.Forms.DateTimePicker dtpWorkDate;
        private System.Windows.Forms.TextBox txtHours;
        private System.Windows.Forms.TextBox txtOvertime;
        private System.Windows.Forms.CheckBox chkAbsent;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label lblEmployee;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblHours;
        private System.Windows.Forms.Label lblOvertime;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlForm;
    }
}
