namespace Proyecto
{
    partial class principal
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.departamentos = new System.Windows.Forms.Button();
            this.empleados = new System.Windows.Forms.Button();
            this.contratos = new System.Windows.Forms.Button();
            this.btnAttendance = new System.Windows.Forms.Button();
            this.btnPayroll = new System.Windows.Forms.Button();
            this.btnClients = new System.Windows.Forms.Button();
            this.btnQuotes = new System.Windows.Forms.Button();
            this.btnInvoicing = new System.Windows.Forms.Button();
            this.btnPayments = new System.Windows.Forms.Button();
            this.btnProducts = new System.Windows.Forms.Button();
            this.btnAssets = new System.Windows.Forms.Button();
            this.btnAccounting = new System.Windows.Forms.Button();
            this.btnAudit = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.lblKPI = new System.Windows.Forms.Label();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.scrlMain = new System.Windows.Forms.Panel();
            this.lblHrSection = new System.Windows.Forms.Label();
            this.lblSalesSection = new System.Windows.Forms.Label();
            this.lblInventorySection = new System.Windows.Forms.Label();
            this.lblAccountingSection = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // departamentos
            // 
            this.departamentos.Location = new System.Drawing.Point(20, 110);
            this.departamentos.Name = "departamentos";
            this.departamentos.Size = new System.Drawing.Size(170, 60);
            this.departamentos.TabIndex = 0;
            this.departamentos.Text = "Departamentos";
            this.departamentos.UseVisualStyleBackColor = true;
            this.departamentos.Click += new System.EventHandler(this.button1_Click);
            // 
            // empleados
            // 
            this.empleados.Location = new System.Drawing.Point(200, 110);
            this.empleados.Name = "empleados";
            this.empleados.Size = new System.Drawing.Size(170, 60);
            this.empleados.TabIndex = 1;
            this.empleados.Text = "Empleados";
            this.empleados.UseVisualStyleBackColor = true;
            this.empleados.Click += new System.EventHandler(this.button2_Click);
            // 
            // contratos
            // 
            this.contratos.Location = new System.Drawing.Point(380, 110);
            this.contratos.Name = "contratos";
            this.contratos.Size = new System.Drawing.Size(170, 60);
            this.contratos.TabIndex = 2;
            this.contratos.Text = "Contratos HR";
            this.contratos.UseVisualStyleBackColor = true;
            this.contratos.Click += new System.EventHandler(this.btnContracts_Click);
            // 
            // btnPayroll
            // 
            this.btnPayroll.Location = new System.Drawing.Point(560, 110);
            this.btnPayroll.Name = "btnPayroll";
            this.btnPayroll.Size = new System.Drawing.Size(170, 60);
            this.btnPayroll.TabIndex = 4;
            this.btnPayroll.Text = "Nómina y Cálculos";
            this.btnPayroll.UseVisualStyleBackColor = true;
            this.btnPayroll.Click += new System.EventHandler(this.btnPayroll_Click);
            // 
            // btnAttendance
            // 
            this.btnAttendance.Location = new System.Drawing.Point(740, 110);
            this.btnAttendance.Name = "btnAttendance";
            this.btnAttendance.Size = new System.Drawing.Size(170, 60);
            this.btnAttendance.TabIndex = 3;
            this.btnAttendance.Text = "Control de Asistencia";
            this.btnAttendance.UseVisualStyleBackColor = true;
            this.btnAttendance.Click += new System.EventHandler(this.btnAttendance_Click);
            // 
            // btnClients
            //
            this.btnClients.Location = new System.Drawing.Point(20, 210);
            this.btnClients.Name = "btnClients";
            this.btnClients.Size = new System.Drawing.Size(170, 60);
            this.btnClients.TabIndex = 5;
            this.btnClients.Text = "CRM Clientes";
            this.btnClients.Click += new System.EventHandler(this.btnClients_Click);
            //
            // btnQuotes
            //
            this.btnQuotes.Location = new System.Drawing.Point(200, 210);
            this.btnQuotes.Name = "btnQuotes";
            this.btnQuotes.Size = new System.Drawing.Size(170, 60);
            this.btnQuotes.TabIndex = 6;
            this.btnQuotes.Text = "Cotizaciones";
            this.btnQuotes.Click += new System.EventHandler(this.btnQuotes_Click);
            //
            // btnInvoicing
            //
            this.btnInvoicing.Location = new System.Drawing.Point(380, 210);
            this.btnInvoicing.Name = "btnInvoicing";
            this.btnInvoicing.Size = new System.Drawing.Size(170, 60);
            this.btnInvoicing.TabIndex = 7;
            this.btnInvoicing.Text = "Facturas y DIAN";
            this.btnInvoicing.Click += new System.EventHandler(this.btnInvoicing_Click);
            //
            // btnPayments
            //
            this.btnPayments.Location = new System.Drawing.Point(560, 210);
            this.btnPayments.Name = "btnPayments";
            this.btnPayments.Size = new System.Drawing.Size(170, 60);
            this.btnPayments.TabIndex = 8;
            this.btnPayments.Text = "Pagos / CXC";
            this.btnPayments.Click += new System.EventHandler(this.btnPayments_Click);
            // 
            // btnProducts
            //
            this.btnProducts.Location = new System.Drawing.Point(20, 310);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Size = new System.Drawing.Size(170, 60);
            this.btnProducts.TabIndex = 9;
            this.btnProducts.Text = "Inventario y Productos";
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // btnAssets
            //
            this.btnAssets.Location = new System.Drawing.Point(200, 310);
            this.btnAssets.Name = "btnAssets";
            this.btnAssets.Size = new System.Drawing.Size(170, 60);
            this.btnAssets.TabIndex = 10;
            this.btnAssets.Text = "Control Activos Fijos";
            this.btnAssets.Click += new System.EventHandler(this.btnAssets_Click);
            
            // Re-adjust Attendance button downwards to fit Assets
            this.btnAttendance.Location = new System.Drawing.Point(740, 110);
            this.btnAttendance.Size = new System.Drawing.Size(170, 60);

            //
            // btnAccounting
            //
            this.btnAccounting.Location = new System.Drawing.Point(20, 410);
            this.btnAccounting.Name = "btnAccounting";
            this.btnAccounting.Size = new System.Drawing.Size(170, 60);
            this.btnAccounting.TabIndex = 12;
            this.btnAccounting.Text = "📒 Contabilidad PUC";
            this.btnAccounting.UseVisualStyleBackColor = true;
            this.btnAccounting.Click += new System.EventHandler(this.btnAccounting_Click);
            //
            // btnAudit
            //
            this.btnAudit.Location = new System.Drawing.Point(200, 410);
            this.btnAudit.Name = "btnAudit";
            this.btnAudit.Size = new System.Drawing.Size(170, 60);
            this.btnAudit.TabIndex = 13;
            this.btnAudit.Text = "🔍 Auditoría";
            this.btnAudit.UseVisualStyleBackColor = true;
            this.btnAudit.Click += new System.EventHandler(this.btnAudit_Click);
            
            // btnSettings
            //
            this.btnSettings.Location = new System.Drawing.Point(380, 410);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(170, 60);
            this.btnSettings.TabIndex = 14;
            this.btnSettings.Text = "⚙️ Configuración";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            
            // lblHrSection
            //
            this.lblHrSection.AutoSize = true;
            this.lblHrSection.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblHrSection.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblHrSection.Location = new System.Drawing.Point(20, 85);
            this.lblHrSection.Text = "RECURSOS HUMANOS";

            // lblSalesSection
            //
            this.lblSalesSection.AutoSize = true;
            this.lblSalesSection.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblSalesSection.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblSalesSection.Location = new System.Drawing.Point(20, 185);
            this.lblSalesSection.Text = "VENTAS Y CRM";

            // lblInventorySection
            //
            this.lblInventorySection.AutoSize = true;
            this.lblInventorySection.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblInventorySection.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblInventorySection.Location = new System.Drawing.Point(20, 285);
            this.lblInventorySection.Text = "INVENTARIO Y ACTIVOS";

            // lblAccountingSection
            //
            this.lblAccountingSection.AutoSize = true;
            this.lblAccountingSection.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.lblAccountingSection.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);
            this.lblAccountingSection.Location = new System.Drawing.Point(20, 385);
            this.lblAccountingSection.Text = "CONTABILIDAD Y AUDITORÍA";

            // lblKPI
            // 
            this.lblKPI.AutoSize = false;
            this.lblKPI.Location = new System.Drawing.Point(20, 10);
            this.lblKPI.Name = "lblKPI";
            this.lblKPI.Size = new System.Drawing.Size(900, 60);
            this.lblKPI.Font = new System.Drawing.Font("Arial", 11F);
            this.lblKPI.TabIndex = 14;
            this.lblKPI.Text = "Cargando KPIs...";

            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 550);
            this.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);
            this.Controls.Add(this.lblAccountingSection);
            this.Controls.Add(this.lblInventorySection);
            this.Controls.Add(this.lblSalesSection);
            this.Controls.Add(this.lblHrSection);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnAudit);
            this.Controls.Add(this.btnAccounting);
            this.Controls.Add(this.lblKPI);
            this.Controls.Add(this.btnAssets);
            this.Controls.Add(this.btnProducts);
            this.Controls.Add(this.btnPayments);
            this.Controls.Add(this.btnInvoicing);
            this.Controls.Add(this.btnQuotes);
            this.Controls.Add(this.btnClients);
            this.Controls.Add(this.btnPayroll);
            this.Controls.Add(this.btnAttendance);
            this.Controls.Add(this.contratos);
            this.Controls.Add(this.empleados);
            this.Controls.Add(this.departamentos);
            this.Name = "Form1";
            this.Text = "ERP Dashboard Principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button departamentos;
        private System.Windows.Forms.Button empleados;
        private System.Windows.Forms.Button contratos;
        private System.Windows.Forms.Button btnAttendance;
        private System.Windows.Forms.Button btnPayroll;
        private System.Windows.Forms.Button btnClients;
        private System.Windows.Forms.Button btnQuotes;
        private System.Windows.Forms.Button btnInvoicing;
        private System.Windows.Forms.Button btnPayments;
        private System.Windows.Forms.Button btnProducts;
        private System.Windows.Forms.Button btnAssets;
        private System.Windows.Forms.Button btnAccounting;
        private System.Windows.Forms.Button btnAudit;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Label lblKPI;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel scrlMain;
        private System.Windows.Forms.Label lblHrSection;
        private System.Windows.Forms.Label lblSalesSection;
        private System.Windows.Forms.Label lblInventorySection;
        private System.Windows.Forms.Label lblAccountingSection;
    }
}

