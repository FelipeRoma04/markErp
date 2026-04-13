using System.Drawing;
using Proyecto.Controler;

namespace Proyecto.View
{
    partial class frmQuotes
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
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.dtpExpire = new System.Windows.Forms.DateTimePicker();
            this.lblExpire = new System.Windows.Forms.Label();
            this.dtpIssue = new System.Windows.Forms.DateTimePicker();
            this.lblIssue = new System.Windows.Forms.Label();
            this.txtClientId = new System.Windows.Forms.TextBox();
            this.lblClient = new System.Windows.Forms.Label();
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
            this.lblTitle.Size = new System.Drawing.Size(183, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Cotizaciones";
            // 
            // pnlForm
            // 
            this.pnlForm.BackColor = System.Drawing.Color.White;
            this.pnlForm.Controls.Add(this.btnSave);
            this.pnlForm.Controls.Add(this.txtTotal);
            this.pnlForm.Controls.Add(this.lblTotal);
            this.pnlForm.Controls.Add(this.dtpExpire);
            this.pnlForm.Controls.Add(this.lblExpire);
            this.pnlForm.Controls.Add(this.dtpIssue);
            this.pnlForm.Controls.Add(this.lblIssue);
            this.pnlForm.Controls.Add(this.txtClientId);
            this.pnlForm.Controls.Add(this.lblClient);
            this.pnlForm.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlForm.Location = new System.Drawing.Point(0, 60);
            this.pnlForm.Name = "pnlForm";
            this.pnlForm.Padding = new System.Windows.Forms.Padding(30);
            this.pnlForm.Size = new System.Drawing.Size(430, 360);
            this.pnlForm.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(34, 275);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(360, 45);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "💾 Guardar Cotización";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtTotal
            // 
            this.txtTotal.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTotal.Location = new System.Drawing.Point(180, 205);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(214, 30);
            this.txtTotal.TabIndex = 7;
            this.txtTotal.Text = "0.00";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTotal.Location = new System.Drawing.Point(34, 210);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(126, 20);
            this.lblTotal.TabIndex = 6;
            this.lblTotal.Text = "Subtotal (Sin IVA):";
            // 
            // dtpExpire
            // 
            this.dtpExpire.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpExpire.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpExpire.Location = new System.Drawing.Point(180, 145);
            this.dtpExpire.Name = "dtpExpire";
            this.dtpExpire.Size = new System.Drawing.Size(214, 30);
            this.dtpExpire.TabIndex = 5;
            // 
            // lblExpire
            // 
            this.lblExpire.AutoSize = true;
            this.lblExpire.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblExpire.Location = new System.Drawing.Point(34, 150);
            this.lblExpire.Name = "lblExpire";
            this.lblExpire.Size = new System.Drawing.Size(115, 20);
            this.lblExpire.TabIndex = 4;
            this.lblExpire.Text = "Expiration Date:";
            // 
            // dtpIssue
            // 
            this.dtpIssue.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpIssue.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpIssue.Location = new System.Drawing.Point(180, 85);
            this.dtpIssue.Name = "dtpIssue";
            this.dtpIssue.Size = new System.Drawing.Size(214, 30);
            this.dtpIssue.TabIndex = 3;
            // 
            // lblIssue
            // 
            this.lblIssue.AutoSize = true;
            this.lblIssue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblIssue.Location = new System.Drawing.Point(34, 90);
            this.lblIssue.Name = "lblIssue";
            this.lblIssue.Size = new System.Drawing.Size(80, 20);
            this.lblIssue.TabIndex = 2;
            this.lblIssue.Text = "Issue Date:";
            // 
            // txtClientId
            // 
            this.txtClientId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtClientId.Location = new System.Drawing.Point(180, 30);
            this.txtClientId.Name = "txtClientId";
            this.txtClientId.Size = new System.Drawing.Size(214, 30);
            this.txtClientId.TabIndex = 1;
            // 
            // lblClient
            // 
            this.lblClient.AutoSize = true;
            this.lblClient.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblClient.Location = new System.Drawing.Point(34, 35);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(69, 20);
            this.lblClient.TabIndex = 0;
            this.lblClient.Text = "Client ID:";
            // 
            // frmQuotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(430, 420);
            this.Controls.Add(this.pnlForm);
            this.Controls.Add(this.pnlHeader);
            this.Name = "frmQuotes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cotizaciones — MarkErp";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlForm.ResumeLayout(false);
            this.pnlForm.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtClientId;
        private System.Windows.Forms.DateTimePicker dtpIssue;
        private System.Windows.Forms.DateTimePicker dtpExpire;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.Label lblIssue;
        private System.Windows.Forms.Label lblExpire;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlForm;
    }
}
