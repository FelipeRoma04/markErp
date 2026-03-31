namespace Proyecto.View
{
    partial class frmEmployeeContracts
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
            this.txtEmployeeId = new System.Windows.Forms.TextBox();
            this.txtJobTitle = new System.Windows.Forms.TextBox();
            this.txtSalary = new System.Windows.Forms.TextBox();
            this.txtContractType = new System.Windows.Forms.TextBox();
            this.lblEmployee = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSalary = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.SuspendLayout();
            
            this.lblEmployee.Location = new System.Drawing.Point(30, 30);
            this.lblEmployee.Text = "Employee ID:";
            
            this.txtEmployeeId.Location = new System.Drawing.Point(150, 30);
            
            this.lblTitle.Location = new System.Drawing.Point(30, 70);
            this.lblTitle.Text = "Job Title:";
            
            this.txtJobTitle.Location = new System.Drawing.Point(150, 70);
            
            this.lblSalary.Location = new System.Drawing.Point(30, 110);
            this.lblSalary.Text = "Base Salary:";
            
            this.txtSalary.Location = new System.Drawing.Point(150, 110);
            
            this.lblType.Location = new System.Drawing.Point(30, 150);
            this.lblType.Text = "Contract Type:";
            
            this.txtContractType.Location = new System.Drawing.Point(150, 150);

            this.btnSave.Location = new System.Drawing.Point(150, 200);
            this.btnSave.Text = "Save Contract";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtEmployeeId);
            this.Controls.Add(this.txtJobTitle);
            this.Controls.Add(this.txtSalary);
            this.Controls.Add(this.txtContractType);
            this.Controls.Add(this.lblEmployee);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblSalary);
            this.Controls.Add(this.lblType);
            this.Text = "Employee Contracts";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.TextBox txtEmployeeId;
        private System.Windows.Forms.TextBox txtJobTitle;
        private System.Windows.Forms.TextBox txtSalary;
        private System.Windows.Forms.TextBox txtContractType;
        private System.Windows.Forms.Label lblEmployee;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSalary;
        private System.Windows.Forms.Label lblType;
    }
}
