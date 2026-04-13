using System.Drawing;
using Proyecto.Controler;

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
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.lblBrand = new System.Windows.Forms.Label();
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblUserStatus = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.flpKpis = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlKpiProducts = new System.Windows.Forms.Panel();
            this.lblKpiProductsVal = new System.Windows.Forms.Label();
            this.lblKpiProductsTitle = new System.Windows.Forms.Label();
            this.pnlKpiSales = new System.Windows.Forms.Panel();
            this.lblKpiSalesVal = new System.Windows.Forms.Label();
            this.lblKpiSalesTitle = new System.Windows.Forms.Label();
            this.pnlKpiPending = new System.Windows.Forms.Panel();
            this.lblKpiPendingVal = new System.Windows.Forms.Label();
            this.lblKpiPendingTitle = new System.Windows.Forms.Label();
            this.pnlSidebar.SuspendLayout();
            this.pnlHeader.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.flpKpis.SuspendLayout();
            this.pnlKpiProducts.SuspendLayout();
            this.pnlKpiSales.SuspendLayout();
            this.pnlKpiPending.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.pnlSidebar.Controls.Add(this.btnSettings);
            this.pnlSidebar.Controls.Add(this.btnAudit);
            this.pnlSidebar.Controls.Add(this.btnAccounting);
            this.pnlSidebar.Controls.Add(this.btnAssets);
            this.pnlSidebar.Controls.Add(this.btnProducts);
            this.pnlSidebar.Controls.Add(this.btnPayments);
            this.pnlSidebar.Controls.Add(this.btnInvoicing);
            this.pnlSidebar.Controls.Add(this.btnQuotes);
            this.pnlSidebar.Controls.Add(this.btnClients);
            this.pnlSidebar.Controls.Add(this.btnPayroll);
            this.pnlSidebar.Controls.Add(this.btnAttendance);
            this.pnlSidebar.Controls.Add(this.contratos);
            this.pnlSidebar.Controls.Add(this.empleados);
            this.pnlSidebar.Controls.Add(this.departamentos);
            this.pnlSidebar.Controls.Add(this.lblBrand);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(220, 650);
            this.pnlSidebar.TabIndex = 0;
            // 
            // lblBrand
            // 
            this.lblBrand.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblBrand.ForeColor = System.Drawing.Color.White;
            this.lblBrand.Location = new System.Drawing.Point(12, 18);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(196, 45);
            this.lblBrand.TabIndex = 0;
            this.lblBrand.Text = "MarkErp v2";
            this.lblBrand.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // departamentos
            // 
            this.departamentos.BackColor = System.Drawing.Color.Transparent;
            this.departamentos.FlatAppearance.BorderSize = 0;
            this.departamentos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.departamentos.ForeColor = System.Drawing.Color.White;
            this.departamentos.Location = new System.Drawing.Point(0, 80);
            this.departamentos.Name = "departamentos";
            this.departamentos.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.departamentos.Size = new System.Drawing.Size(220, 40);
            this.departamentos.TabIndex = 1;
            this.departamentos.Text = "Departamentos";
            this.departamentos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.departamentos.UseVisualStyleBackColor = false;
            this.departamentos.Click += new System.EventHandler(this.button1_Click);
            // 
            // empleados
            // 
            this.empleados.BackColor = System.Drawing.Color.Transparent;
            this.empleados.FlatAppearance.BorderSize = 0;
            this.empleados.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.empleados.ForeColor = System.Drawing.Color.White;
            this.empleados.Location = new System.Drawing.Point(0, 120);
            this.empleados.Name = "empleados";
            this.empleados.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.empleados.Size = new System.Drawing.Size(220, 40);
            this.empleados.TabIndex = 2;
            this.empleados.Text = "Empleados";
            this.empleados.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.empleados.UseVisualStyleBackColor = false;
            this.empleados.Click += new System.EventHandler(this.button2_Click);
            // 
            // contratos
            // 
            this.contratos.BackColor = System.Drawing.Color.Transparent;
            this.contratos.FlatAppearance.BorderSize = 0;
            this.contratos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.contratos.ForeColor = System.Drawing.Color.White;
            this.contratos.Location = new System.Drawing.Point(0, 160);
            this.contratos.Name = "contratos";
            this.contratos.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.contratos.Size = new System.Drawing.Size(220, 40);
            this.contratos.TabIndex = 3;
            this.contratos.Text = "Contratos";
            this.contratos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.contratos.UseVisualStyleBackColor = false;
            this.contratos.Click += new System.EventHandler(this.btnContracts_Click);
            // 
            // btnAttendance
            // 
            this.btnAttendance.BackColor = System.Drawing.Color.Transparent;
            this.btnAttendance.FlatAppearance.BorderSize = 0;
            this.btnAttendance.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAttendance.ForeColor = System.Drawing.Color.White;
            this.btnAttendance.Location = new System.Drawing.Point(0, 200);
            this.btnAttendance.Name = "btnAttendance";
            this.btnAttendance.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnAttendance.Size = new System.Drawing.Size(220, 40);
            this.btnAttendance.TabIndex = 4;
            this.btnAttendance.Text = "Asistencia";
            this.btnAttendance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAttendance.UseVisualStyleBackColor = false;
            this.btnAttendance.Click += new System.EventHandler(this.btnAttendance_Click);
            // 
            // btnPayroll
            // 
            this.btnPayroll.BackColor = System.Drawing.Color.Transparent;
            this.btnPayroll.FlatAppearance.BorderSize = 0;
            this.btnPayroll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayroll.ForeColor = System.Drawing.Color.White;
            this.btnPayroll.Location = new System.Drawing.Point(0, 240);
            this.btnPayroll.Name = "btnPayroll";
            this.btnPayroll.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnPayroll.Size = new System.Drawing.Size(220, 40);
            this.btnPayroll.TabIndex = 5;
            this.btnPayroll.Text = "Nómina";
            this.btnPayroll.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPayroll.UseVisualStyleBackColor = false;
            this.btnPayroll.Click += new System.EventHandler(this.btnPayroll_Click);
            // 
            // btnClients
            // 
            this.btnClients.BackColor = System.Drawing.Color.Transparent;
            this.btnClients.FlatAppearance.BorderSize = 0;
            this.btnClients.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClients.ForeColor = System.Drawing.Color.White;
            this.btnClients.Location = new System.Drawing.Point(0, 280);
            this.btnClients.Name = "btnClients";
            this.btnClients.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnClients.Size = new System.Drawing.Size(220, 40);
            this.btnClients.TabIndex = 6;
            this.btnClients.Text = "Clientes";
            this.btnClients.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClients.UseVisualStyleBackColor = false;
            this.btnClients.Click += new System.EventHandler(this.btnClients_Click);
            // 
            // btnQuotes
            // 
            this.btnQuotes.BackColor = System.Drawing.Color.Transparent;
            this.btnQuotes.FlatAppearance.BorderSize = 0;
            this.btnQuotes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuotes.ForeColor = System.Drawing.Color.White;
            this.btnQuotes.Location = new System.Drawing.Point(0, 320);
            this.btnQuotes.Name = "btnQuotes";
            this.btnQuotes.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnQuotes.Size = new System.Drawing.Size(220, 40);
            this.btnQuotes.TabIndex = 7;
            this.btnQuotes.Text = "Cotizaciones";
            this.btnQuotes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnQuotes.UseVisualStyleBackColor = false;
            this.btnQuotes.Click += new System.EventHandler(this.btnQuotes_Click);
            // 
            // btnInvoicing
            // 
            this.btnInvoicing.BackColor = System.Drawing.Color.Transparent;
            this.btnInvoicing.FlatAppearance.BorderSize = 0;
            this.btnInvoicing.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInvoicing.ForeColor = System.Drawing.Color.White;
            this.btnInvoicing.Location = new System.Drawing.Point(0, 360);
            this.btnInvoicing.Name = "btnInvoicing";
            this.btnInvoicing.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnInvoicing.Size = new System.Drawing.Size(220, 40);
            this.btnInvoicing.TabIndex = 8;
            this.btnInvoicing.Text = "Facturación";
            this.btnInvoicing.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnInvoicing.UseVisualStyleBackColor = false;
            this.btnInvoicing.Click += new System.EventHandler(this.btnInvoicing_Click);
            // 
            // btnPayments
            // 
            this.btnPayments.BackColor = System.Drawing.Color.Transparent;
            this.btnPayments.FlatAppearance.BorderSize = 0;
            this.btnPayments.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayments.ForeColor = System.Drawing.Color.White;
            this.btnPayments.Location = new System.Drawing.Point(0, 400);
            this.btnPayments.Name = "btnPayments";
            this.btnPayments.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnPayments.Size = new System.Drawing.Size(220, 40);
            this.btnPayments.TabIndex = 9;
            this.btnPayments.Text = "Pagos";
            this.btnPayments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPayments.UseVisualStyleBackColor = false;
            this.btnPayments.Click += new System.EventHandler(this.btnPayments_Click);
            // 
            // btnProducts
            // 
            this.btnProducts.BackColor = System.Drawing.Color.Transparent;
            this.btnProducts.FlatAppearance.BorderSize = 0;
            this.btnProducts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProducts.ForeColor = System.Drawing.Color.White;
            this.btnProducts.Location = new System.Drawing.Point(0, 440);
            this.btnProducts.Name = "btnProducts";
            this.btnProducts.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnProducts.Size = new System.Drawing.Size(220, 40);
            this.btnProducts.TabIndex = 10;
            this.btnProducts.Text = "Inventario";
            this.btnProducts.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProducts.UseVisualStyleBackColor = false;
            this.btnProducts.Click += new System.EventHandler(this.btnProducts_Click);
            // 
            // btnAssets
            // 
            this.btnAssets.BackColor = System.Drawing.Color.Transparent;
            this.btnAssets.FlatAppearance.BorderSize = 0;
            this.btnAssets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAssets.ForeColor = System.Drawing.Color.White;
            this.btnAssets.Location = new System.Drawing.Point(0, 480);
            this.btnAssets.Name = "btnAssets";
            this.btnAssets.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnAssets.Size = new System.Drawing.Size(220, 40);
            this.btnAssets.TabIndex = 11;
            this.btnAssets.Text = "Activos Fijos";
            this.btnAssets.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAssets.UseVisualStyleBackColor = false;
            this.btnAssets.Click += new System.EventHandler(this.btnAssets_Click);
            // 
            // btnAccounting
            // 
            this.btnAccounting.BackColor = System.Drawing.Color.Transparent;
            this.btnAccounting.FlatAppearance.BorderSize = 0;
            this.btnAccounting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccounting.ForeColor = System.Drawing.Color.White;
            this.btnAccounting.Location = new System.Drawing.Point(0, 520);
            this.btnAccounting.Name = "btnAccounting";
            this.btnAccounting.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnAccounting.Size = new System.Drawing.Size(220, 40);
            this.btnAccounting.TabIndex = 12;
            this.btnAccounting.Text = "Contabilidad";
            this.btnAccounting.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAccounting.UseVisualStyleBackColor = false;
            this.btnAccounting.Click += new System.EventHandler(this.btnAccounting_Click);
            // 
            // btnAudit
            // 
            this.btnAudit.BackColor = System.Drawing.Color.Transparent;
            this.btnAudit.FlatAppearance.BorderSize = 0;
            this.btnAudit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAudit.ForeColor = System.Drawing.Color.White;
            this.btnAudit.Location = new System.Drawing.Point(0, 560);
            this.btnAudit.Name = "btnAudit";
            this.btnAudit.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnAudit.Size = new System.Drawing.Size(220, 40);
            this.btnAudit.TabIndex = 13;
            this.btnAudit.Text = "Auditoría";
            this.btnAudit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAudit.UseVisualStyleBackColor = false;
            this.btnAudit.Click += new System.EventHandler(this.btnAudit_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.Location = new System.Drawing.Point(0, 600);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.btnSettings.Size = new System.Drawing.Size(220, 40);
            this.btnSettings.TabIndex = 14;
            this.btnSettings.Text = "Configuración";
            this.btnSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.White;
            this.pnlHeader.Controls.Add(this.lblUserStatus);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(220, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(830, 70);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblUserStatus
            // 
            this.lblUserStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUserStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblUserStatus.Location = new System.Drawing.Point(520, 20);
            this.lblUserStatus.Name = "lblUserStatus";
            this.lblUserStatus.Size = new System.Drawing.Size(300, 30);
            this.lblUserStatus.TabIndex = 1;
            this.lblUserStatus.Text = "Usuario: Admin";
            this.lblUserStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(193, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Vista de Mandos";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.flpKpis);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(220, 70);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(830, 580);
            this.pnlMain.TabIndex = 2;
            // 
            // flpKpis
            // 
            this.flpKpis.Controls.Add(this.pnlKpiProducts);
            this.flpKpis.Controls.Add(this.pnlKpiSales);
            this.flpKpis.Controls.Add(this.pnlKpiPending);
            this.flpKpis.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpKpis.Location = new System.Drawing.Point(20, 20);
            this.flpKpis.Name = "flpKpis";
            this.flpKpis.Size = new System.Drawing.Size(790, 150);
            this.flpKpis.TabIndex = 0;
            // 
            // pnlKpiProducts
            // 
            this.pnlKpiProducts.BackColor = System.Drawing.Color.White;
            this.pnlKpiProducts.Controls.Add(this.lblKpiProductsVal);
            this.pnlKpiProducts.Controls.Add(this.lblKpiProductsTitle);
            this.pnlKpiProducts.Location = new System.Drawing.Point(10, 10);
            this.pnlKpiProducts.Margin = new System.Windows.Forms.Padding(10);
            this.pnlKpiProducts.Name = "pnlKpiProducts";
            this.pnlKpiProducts.Size = new System.Drawing.Size(220, 100);
            this.pnlKpiProducts.TabIndex = 0;
            // 
            // lblKpiProductsVal
            // 
            this.lblKpiProductsVal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblKpiProductsVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblKpiProductsVal.Location = new System.Drawing.Point(10, 40);
            this.lblKpiProductsVal.Name = "lblKpiProductsVal";
            this.lblKpiProductsVal.Size = new System.Drawing.Size(200, 40);
            this.lblKpiProductsVal.TabIndex = 1;
            this.lblKpiProductsVal.Text = "0";
            this.lblKpiProductsVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpiProductsTitle
            // 
            this.lblKpiProductsTitle.AutoSize = true;
            this.lblKpiProductsTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKpiProductsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblKpiProductsTitle.Location = new System.Drawing.Point(10, 10);
            this.lblKpiProductsTitle.Name = "lblKpiProductsTitle";
            this.lblKpiProductsTitle.Size = new System.Drawing.Size(100, 20);
            this.lblKpiProductsTitle.TabIndex = 0;
            this.lblKpiProductsTitle.Text = "PRODUCTOS";
            // 
            // pnlKpiSales
            // 
            this.pnlKpiSales.BackColor = System.Drawing.Color.White;
            this.pnlKpiSales.Controls.Add(this.lblKpiSalesVal);
            this.pnlKpiSales.Controls.Add(this.lblKpiSalesTitle);
            this.pnlKpiSales.Location = new System.Drawing.Point(250, 10);
            this.pnlKpiSales.Margin = new System.Windows.Forms.Padding(10);
            this.pnlKpiSales.Name = "pnlKpiSales";
            this.pnlKpiSales.Size = new System.Drawing.Size(220, 100);
            this.pnlKpiSales.TabIndex = 1;
            // 
            // lblKpiSalesVal
            // 
            this.lblKpiSalesVal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblKpiSalesVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(174)))), ((int)(((byte)(96)))));
            this.lblKpiSalesVal.Location = new System.Drawing.Point(10, 40);
            this.lblKpiSalesVal.Name = "lblKpiSalesVal";
            this.lblKpiSalesVal.Size = new System.Drawing.Size(200, 40);
            this.lblKpiSalesVal.TabIndex = 1;
            this.lblKpiSalesVal.Text = "$ 0";
            this.lblKpiSalesVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpiSalesTitle
            // 
            this.lblKpiSalesTitle.AutoSize = true;
            this.lblKpiSalesTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKpiSalesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblKpiSalesTitle.Location = new System.Drawing.Point(10, 10);
            this.lblKpiSalesTitle.Name = "lblKpiSalesTitle";
            this.lblKpiSalesTitle.Size = new System.Drawing.Size(66, 20);
            this.lblKpiSalesTitle.TabIndex = 0;
            this.lblKpiSalesTitle.Text = "VENTAS";
            // 
            // pnlKpiPending
            // 
            this.pnlKpiPending.BackColor = System.Drawing.Color.White;
            this.pnlKpiPending.Controls.Add(this.lblKpiPendingVal);
            this.pnlKpiPending.Controls.Add(this.lblKpiPendingTitle);
            this.pnlKpiPending.Location = new System.Drawing.Point(490, 10);
            this.pnlKpiPending.Margin = new System.Windows.Forms.Padding(10);
            this.pnlKpiPending.Name = "pnlKpiPending";
            this.pnlKpiPending.Size = new System.Drawing.Size(220, 100);
            this.pnlKpiPending.TabIndex = 2;
            // 
            // lblKpiPendingVal
            // 
            this.lblKpiPendingVal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblKpiPendingVal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.lblKpiPendingVal.Location = new System.Drawing.Point(10, 40);
            this.lblKpiPendingVal.Name = "lblKpiPendingVal";
            this.lblKpiPendingVal.Size = new System.Drawing.Size(200, 40);
            this.lblKpiPendingVal.TabIndex = 1;
            this.lblKpiPendingVal.Text = "0";
            this.lblKpiPendingVal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblKpiPendingTitle
            // 
            this.lblKpiPendingTitle.AutoSize = true;
            this.lblKpiPendingTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblKpiPendingTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.lblKpiPendingTitle.Location = new System.Drawing.Point(10, 10);
            this.lblKpiPendingTitle.Name = "lblKpiPendingTitle";
            this.lblKpiPendingTitle.Size = new System.Drawing.Size(89, 20);
            this.lblKpiPendingTitle.TabIndex = 0;
            this.lblKpiPendingTitle.Text = "PENDIENTS";
            // 
            // principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.ClientSize = new System.Drawing.Size(1050, 650);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlSidebar);
            this.MinimumSize = new System.Drawing.Size(1000, 600);
            this.Name = "principal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MarkErp Enterprise Dashboard";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.flpKpis.ResumeLayout(false);
            this.pnlKpiProducts.ResumeLayout(false);
            this.pnlKpiProducts.PerformLayout();
            this.pnlKpiSales.ResumeLayout(false);
            this.pnlKpiSales.PerformLayout();
            this.pnlKpiPending.ResumeLayout(false);
            this.pnlKpiPending.PerformLayout();
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
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblUserStatus;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.FlowLayoutPanel flpKpis;
        private System.Windows.Forms.Panel pnlKpiProducts;
        private System.Windows.Forms.Label lblKpiProductsVal;
        private System.Windows.Forms.Label lblKpiProductsTitle;
        private System.Windows.Forms.Panel pnlKpiSales;
        private System.Windows.Forms.Label lblKpiSalesVal;
        private System.Windows.Forms.Label lblKpiSalesTitle;
        private System.Windows.Forms.Panel pnlKpiPending;
        private System.Windows.Forms.Label lblKpiPendingVal;
        private System.Windows.Forms.Label lblKpiPendingTitle;
    }
}
