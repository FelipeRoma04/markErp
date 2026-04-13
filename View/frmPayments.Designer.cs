using System.Drawing;
using Proyecto.Controler;

namespace Proyecto.View
{
    partial class frmPayments
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
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPayment = new System.Windows.Forms.TabPage();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblDebt = new System.Windows.Forms.Label();
            this.cmbMethod = new System.Windows.Forms.ComboBox();
            this.lblMethod = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.txtInvoiceId = new System.Windows.Forms.TextBox();
            this.lblInvoice = new System.Windows.Forms.Label();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.lblHistoryDebt = new System.Windows.Forms.Label();
            this.dgvPaymentHistory = new System.Windows.Forms.DataGridView();
            this.pnlHeader.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPayment.SuspendLayout();
            this.tabHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(700, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(257, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Cuentas por Cobrar";
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabPayment);
            this.tabMain.Controls.Add(this.tabHistory);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabMain.Location = new System.Drawing.Point(0, 60);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(700, 490);
            this.tabMain.TabIndex = 1;
            // 
            // tabPayment
            // 
            this.tabPayment.BackColor = System.Drawing.Color.White;
            this.tabPayment.Controls.Add(this.btnSave);
            this.tabPayment.Controls.Add(this.lblDebt);
            this.tabPayment.Controls.Add(this.cmbMethod);
            this.tabPayment.Controls.Add(this.lblMethod);
            this.tabPayment.Controls.Add(this.txtAmount);
            this.tabPayment.Controls.Add(this.lblAmount);
            this.tabPayment.Controls.Add(this.txtInvoiceId);
            this.tabPayment.Controls.Add(this.lblInvoice);
            this.tabPayment.Location = new System.Drawing.Point(4, 32);
            this.tabPayment.Name = "tabPayment";
            this.tabPayment.Padding = new System.Windows.Forms.Padding(30);
            this.tabPayment.Size = new System.Drawing.Size(692, 454);
            this.tabPayment.TabIndex = 0;
            this.tabPayment.Text = "Registrar Pago";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(33, 220);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(300, 45);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "💳 Registrar Pago";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblDebt
            // 
            this.lblDebt.AutoSize = true;
            this.lblDebt.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDebt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(57)))), ((int)(((byte)(43)))));
            this.lblDebt.Location = new System.Drawing.Point(33, 280);
            this.lblDebt.Name = "lblDebt";
            this.lblDebt.Size = new System.Drawing.Size(188, 25);
            this.lblDebt.TabIndex = 6;
            this.lblDebt.Text = "Pendiente factura: -";
            // 
            // cmbMethod
            // 
            this.cmbMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMethod.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbMethod.FormattingEnabled = true;
            this.cmbMethod.Items.AddRange(new object[] {
            "Transferencia Bancaria",
            "Efectivo",
            "Tarjeta de Crédito",
            "Cheque"});
            this.cmbMethod.Location = new System.Drawing.Point(180, 155);
            this.cmbMethod.Name = "cmbMethod";
            this.cmbMethod.Size = new System.Drawing.Size(250, 31);
            this.cmbMethod.TabIndex = 5;
            // 
            // lblMethod
            // 
            this.lblMethod.AutoSize = true;
            this.lblMethod.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMethod.Location = new System.Drawing.Point(33, 160);
            this.lblMethod.Name = "lblMethod";
            this.lblMethod.Size = new System.Drawing.Size(124, 20);
            this.lblMethod.TabIndex = 4;
            this.lblMethod.Text = "Método de Pago:";
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtAmount.Location = new System.Drawing.Point(180, 110);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(250, 30);
            this.txtAmount.TabIndex = 3;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAmount.Location = new System.Drawing.Point(33, 115);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(119, 20);
            this.lblAmount.TabIndex = 2;
            this.lblAmount.Text = "Monto Abonado:";
            // 
            // txtInvoiceId
            // 
            this.txtInvoiceId.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtInvoiceId.Location = new System.Drawing.Point(180, 65);
            this.txtInvoiceId.Name = "txtInvoiceId";
            this.txtInvoiceId.Size = new System.Drawing.Size(250, 30);
            this.txtInvoiceId.TabIndex = 1;
            this.txtInvoiceId.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInvoiceId_KeyDown);
            // 
            // lblInvoice
            // 
            this.lblInvoice.AutoSize = true;
            this.lblInvoice.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblInvoice.Location = new System.Drawing.Point(33, 70);
            this.lblInvoice.Name = "lblInvoice";
            this.lblInvoice.Size = new System.Drawing.Size(78, 20);
            this.lblInvoice.TabIndex = 0;
            this.lblInvoice.Text = "Invoice ID:";
            // 
            // tabHistory
            // 
            this.tabHistory.BackColor = System.Drawing.Color.White;
            this.tabHistory.Controls.Add(this.lblHistoryDebt);
            this.tabHistory.Controls.Add(this.dgvPaymentHistory);
            this.tabHistory.Location = new System.Drawing.Point(4, 32);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Padding = new System.Windows.Forms.Padding(10);
            this.tabHistory.Size = new System.Drawing.Size(692, 454);
            this.tabHistory.TabIndex = 1;
            this.tabHistory.Text = "Historial de Abonos";
            // 
            // lblHistoryDebt
            // 
            this.lblHistoryDebt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.lblHistoryDebt.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblHistoryDebt.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblHistoryDebt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblHistoryDebt.Location = new System.Drawing.Point(10, 394);
            this.lblHistoryDebt.Name = "lblHistoryDebt";
            this.lblHistoryDebt.Padding = new System.Windows.Forms.Padding(10);
            this.lblHistoryDebt.Size = new System.Drawing.Size(672, 50);
            this.lblHistoryDebt.TabIndex = 1;
            this.lblHistoryDebt.Text = "Total Pendiente: -";
            this.lblHistoryDebt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dgvPaymentHistory
            // 
            this.dgvPaymentHistory.AllowUserToAddRows = false;
            this.dgvPaymentHistory.AllowUserToDeleteRows = false;
            this.dgvPaymentHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPaymentHistory.BackgroundColor = System.Drawing.Color.White;
            this.dgvPaymentHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPaymentHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPaymentHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPaymentHistory.Location = new System.Drawing.Point(10, 10);
            this.dgvPaymentHistory.Name = "dgvPaymentHistory";
            this.dgvPaymentHistory.ReadOnly = true;
            this.dgvPaymentHistory.RowHeadersVisible = false;
            this.dgvPaymentHistory.Size = new System.Drawing.Size(672, 434);
            this.dgvPaymentHistory.TabIndex = 0;
            // 
            // frmPayments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(700, 550);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.pnlHeader);
            this.Name = "frmPayments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cuentas por Cobrar — MarkErp";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabPayment.ResumeLayout(false);
            this.tabPayment.PerformLayout();
            this.tabHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPaymentHistory)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPayment;
        private System.Windows.Forms.TabPage tabHistory;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblDebt;
        private System.Windows.Forms.ComboBox cmbMethod;
        private System.Windows.Forms.Label lblMethod;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.TextBox txtInvoiceId;
        private System.Windows.Forms.Label lblInvoice;
        private System.Windows.Forms.DataGridView dgvPaymentHistory;
        private System.Windows.Forms.Label lblHistoryDebt;
    }
}
