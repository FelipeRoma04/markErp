using System.Drawing;
using Proyecto.Controler;

namespace Proyecto.View
{
    partial class frmPayroll
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
            this.lblTitleHeader = new System.Windows.Forms.Label();
            this.pnlForm = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.pnlResults = new System.Windows.Forms.Panel();
            this.txtNet = new System.Windows.Forms.TextBox();
            this.lblNet = new System.Windows.Forms.Label();
            this.txtDeductions = new System.Windows.Forms.TextBox();
            this.lblDeduct = new System.Windows.Forms.Label();
            this.btnCalc = new System.Windows.Forms.Button();
            this.dtpPayment = new System.Windows.Forms.DateTimePicker();
            this.lblPay = new System.Windows.Forms.Label();
            this.txtGross = new System.Windows.Forms.TextBox();
            this.lblGross = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.lblStart = new System.Windows.Forms.Label();
            this.txtEmployeeId = new System.Windows.Forms.TextBox();
            this.lblEmployee = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlForm.SuspendLayout();
            this.pnlResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlHeader.Controls.Add(this.lblTitleHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(460, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitleHeader
            // 
            this.lblTitleHeader.AutoSize = true;
            this.lblTitleHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitleHeader.ForeColor = System.Drawing.Color.White;
            this.lblTitleHeader.Location = new System.Drawing.Point(20, 12);
            this.lblTitleHeader.Name = "lblTitleHeader";
            this.lblTitleHeader.Size = new System.Drawing.Size(283, 37);
            this.lblTitleHeader.TabIndex = 0;
            this.lblTitleHeader.Text = "Liquidación de Nómina";
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.White;
            this.pnlForm.Controls.Add(this.btnSave);
            this.pnlForm.Controls.Add(this.pnlResults);
            this.pnlForm.Controls.Add(this.btnCalc);
            this.pnlForm.Controls.Add(this.dtpPayment);
            this.pnlForm.Controls.Add(this.lblPay);
            this.pnlForm.Controls.Add(this.txtGross);
            this.pnlForm.Controls.Add(this.lblGross);
            this.pnlForm.Controls.Add(this.dtpEnd);
            this.pnlForm.Controls.Add(this.lblEnd);
            this.pnlForm.Controls.Add(this.dtpStart);
            this.pnlForm.Controls.Add(this.lblStart);
            this.pnlForm.Controls.Add(this.txtEmployeeId);
            this.pnlForm.Controls.Add(this.lblEmployee);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForm.Location = new System.Drawing.Point(0, 60);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Padding = new System.Windows.Forms.Padding(30);
            this.pnlForm.Size = new System.Drawing.Size(460, 560);
            this.pnlForm.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(33, 480);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(394, 45);
            this.btnSave.TabIndex = 12;
            this.btnSave.Text = "📑 Procesar Nómina";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // pnlResults
            // 
            this.pnlResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlResults.Controls.Add(this.txtNet);
            this.pnlResults.Controls.Add(this.lblNet);
            this.pnlResults.Controls.Add(this.txtDeductions);
            this.pnlResults.Controls.Add(this.lblDeduct);
            this.pnlResults.Location = new System.Drawing.Point(33, 350);
            this.pnlResults.Name = "pnlResults";
            this.pnlResults.Size = new System.Drawing.Size(394, 110);
            this.pnlResults.TabIndex = 11;
            // 
            // txtNet
            // 
            this.txtNet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtNet.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNet.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.txtNet.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.txtNet.Location = new System.Drawing.Point(170, 60);
            this.txtNet.Name = "txtNet";
            this.txtNet.ReadOnly = true;
            this.txtNet.Size = new System.Drawing.Size(200, 32);
            this.txtNet.TabIndex = 3;
            this.txtNet.Text = "0.00";
            this.txtNet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblNet
            // 
            this.lblNet.AutoSize = true;
            this.lblNet.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblNet.Location = new System.Drawing.Point(20, 62);
            this.lblNet.Name = "lblNet";
            this.lblNet.Size = new System.Drawing.Size(134, 28);
            this.lblNet.TabIndex = 2;
            this.lblNet.Text = "Salario Neto:";
            // 
            // txtDeductions
            // 
            this.txtDeductions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.txtDeductions.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtDeductions.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtDeductions.Location = new System.Drawing.Point(170, 20);
            this.txtDeductions.Name = "txtDeductions";
            this.txtDeductions.ReadOnly = true;
            this.txtDeductions.Size = new System.Drawing.Size(200, 25);
            this.txtDeductions.TabIndex = 1;
            this.txtDeductions.Text = "0.00";
            this.txtDeductions.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblDeduct
            // 
            this.lblDeduct.AutoSize = true;
            this.lblDeduct.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDeduct.Location = new System.Drawing.Point(20, 22);
            this.lblDeduct.Name = "lblDeduct";
            this.lblDeduct.Size = new System.Drawing.Size(111, 23);
            this.lblDeduct.TabIndex = 0;
            this.lblDeduct.Text = "Deducciones:";
            // 
            // btnCalc
            // 
            this.btnCalc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnCalc.FlatAppearance.BorderSize = 0;
            this.btnCalc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCalc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCalc.ForeColor = System.Drawing.Color.White;
            this.btnCalc.Location = new System.Drawing.Point(33, 300);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(180, 35);
            this.btnCalc.TabIndex = 10;
            this.btnCalc.Text = "🔄 Calcular Neto";
            this.btnCalc.UseVisualStyleBackColor = false;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // dtpPayment
            // 
            this.dtpPayment.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpPayment.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpPayment.Location = new System.Drawing.Point(180, 245);
            this.dtpPayment.Name = "dtpPayment";
            this.dtpPayment.Size = new System.Drawing.Size(247, 30);
            this.dtpPayment.TabIndex = 9;
            // 
            // lblPay
            // 
            this.lblPay.AutoSize = true;
            this.lblPay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPay.Location = new System.Drawing.Point(33, 250);
            this.lblPay.Name = "lblPay";
            this.lblPay.Size = new System.Drawing.Size(111, 20);
            this.lblPay.TabIndex = 8;
            this.lblPay.Text = "Fecha de Pago:";
            // 
            // txtGross
            // 
            this.txtGross.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGross.Location = new System.Drawing.Point(180, 200);
            this.txtGross.Name = "txtGross";
            this.txtGross.Size = new System.Drawing.Size(247, 30);
            this.txtGross.TabIndex = 7;
            this.txtGross.Text = "0.00";
            // 
            // lblGross
            // 
            this.lblGross.AutoSize = true;
            this.lblGross.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblGross.Location = new System.Drawing.Point(33, 205);
            this.lblGross.Name = "lblGross";
            this.lblGross.Size = new System.Drawing.Size(100, 20);
            this.lblGross.TabIndex = 6;
            this.lblGross.Text = "Salario Bruto:";
            // 
            // dtpEnd
            // 
            this.dtpEnd.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEnd.Location = new System.Drawing.Point(180, 155);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(247, 30);
            this.dtpEnd.TabIndex = 5;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEnd.Location = new System.Drawing.Point(33, 160);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(95, 20);
            this.lblEnd.TabIndex = 4;
            this.lblEnd.Text = "Periodo Fin:";
            // 
            // dtpStart
            // 
            this.dtpStart.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStart.Location = new System.Drawing.Point(180, 110);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(247, 30);
            this.dtpStart.TabIndex = 3;
            // 
            // lblStart
            // 
            this.lblStart.AutoSize = true;
            this.lblStart.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStart.Location = new System.Drawing.Point(33, 115);
            this.lblStart.Name = "lblStart";
            this.lblStart.Size = new System.Drawing.Size(110, 20);
            this.lblStart.TabIndex = 2;
            this.lblStart.Text = "Periodo Inicio:";
            // 
            // txtEmployeeId
            // 
            this.txtEmployeeId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtEmployeeId.Location = new System.Drawing.Point(180, 65);
            this.txtEmployeeId.Name = "txtEmployeeId";
            this.txtEmployeeId.Size = new System.Drawing.Size(247, 30);
            this.txtEmployeeId.TabIndex = 1;
            // 
            // lblEmployee
            // 
            this.lblEmployee.AutoSize = true;
            this.lblEmployee.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblEmployee.Location = new System.Drawing.Point(33, 70);
            this.lblEmployee.Name = "lblEmployee";
            this.lblEmployee.Size = new System.Drawing.Size(98, 20);
            this.lblEmployee.TabIndex = 0;
            this.lblEmployee.Text = "ID Empleado:";
            // 
            // frmPayroll
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(460, 620);
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.pnlHeader);
            this.Name = "frmPayroll";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pago de Nómina — MarkErp";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.pnlResults.ResumeLayout(false);
            this.pnlResults.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.TextBox txtEmployeeId;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpPayment;
        private System.Windows.Forms.TextBox txtGross;
        private System.Windows.Forms.TextBox txtDeductions;
        private System.Windows.Forms.TextBox txtNet;
        private System.Windows.Forms.Label lblEmployee;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lblGross;
        private System.Windows.Forms.Label lblDeduct;
        private System.Windows.Forms.Label lblNet;
        private System.Windows.Forms.Label lblPay;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitleHeader;
        private System.Windows.Forms.Panel pnlForm;
        private System.Windows.Forms.Panel pnlResults;
    }
}
