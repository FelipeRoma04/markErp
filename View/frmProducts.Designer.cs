namespace Proyecto.View
{
    partial class frmProducts
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
            this.btnSave = new System.Windows.Forms.Button();
            this.txtCode = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.txtMinStock = new System.Windows.Forms.TextBox();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            
            this.lblCode = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblStock = new System.Windows.Forms.Label();
            this.lblMinStock = new System.Windows.Forms.Label();
            
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.SuspendLayout();

            this.lblCode.Location = new System.Drawing.Point(30, 30);
            this.lblCode.Text = "Código / SKU:";
            this.txtCode.Location = new System.Drawing.Point(130, 30);

            this.lblName.Location = new System.Drawing.Point(30, 70);
            this.lblName.Text = "Nombre Prod:";
            this.txtName.Location = new System.Drawing.Point(130, 70);
            this.txtName.Size = new System.Drawing.Size(180, 20);

            this.lblPrice.Location = new System.Drawing.Point(30, 110);
            this.lblPrice.Text = "Precio Base:";
            this.txtPrice.Location = new System.Drawing.Point(130, 110);
            this.txtPrice.Text = "0.00";

            this.lblStock.Location = new System.Drawing.Point(30, 150);
            this.lblStock.Text = "Stock Físico:";
            this.txtStock.Location = new System.Drawing.Point(130, 150);
            this.txtStock.Text = "0";

            this.lblMinStock.Location = new System.Drawing.Point(30, 190);
            this.lblMinStock.Text = "Stock Mínimo:";
            this.txtMinStock.Location = new System.Drawing.Point(130, 190);
            this.txtMinStock.Text = "5";

            this.btnSave.Location = new System.Drawing.Point(130, 230);
            this.btnSave.Text = "Ingresar Producto";
            this.btnSave.Size = new System.Drawing.Size(140, 30);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnExportExcel.Location = new System.Drawing.Point(630, 260); // Down below DGV
            this.btnExportExcel.Text = "Export AS Excel (.CSV)";
            this.btnExportExcel.Size = new System.Drawing.Size(120, 30);
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);

            this.dgvProducts.Location = new System.Drawing.Point(330, 30);
            this.dgvProducts.Size = new System.Drawing.Size(420, 230);
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.AllowUserToAddRows = false;

            this.ClientSize = new System.Drawing.Size(780, 300);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnExportExcel);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtStock);
            this.Controls.Add(this.txtMinStock);
            this.Controls.Add(this.lblCode);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.lblMinStock);
            this.Controls.Add(this.dgvProducts);
            this.Text = "Gestor de Inventario y Productos";
            this.Load += new System.EventHandler(this.frmProducts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.TextBox txtMinStock;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.Label lblMinStock;
        private System.Windows.Forms.DataGridView dgvProducts;
    }
}
