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
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCalc = new System.Windows.Forms.Button();
            this.txtEmployeeId = new System.Windows.Forms.TextBox();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpPayment = new System.Windows.Forms.DateTimePicker();
            this.txtGross = new System.Windows.Forms.TextBox();
            this.txtDeductions = new System.Windows.Forms.TextBox();
            this.txtNet = new System.Windows.Forms.TextBox();
            
            this.lblEmployee = new System.Windows.Forms.Label();
            this.lblStart = new System.Windows.Forms.Label();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lblGross = new System.Windows.Forms.Label();
            this.lblDeduct = new System.Windows.Forms.Label();
            this.lblNet = new System.Windows.Forms.Label();
            this.lblPay = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.lblEmployee.Location = new System.Drawing.Point(30, 30);
            this.lblEmployee.Text = "Employee ID:";
            this.txtEmployeeId.Location = new System.Drawing.Point(150, 30);

            this.lblStart.Location = new System.Drawing.Point(30, 70);
            this.lblStart.Text = "Period Start:";
            this.dtpStart.Location = new System.Drawing.Point(150, 70);

            this.lblEnd.Location = new System.Drawing.Point(30, 110);
            this.lblEnd.Text = "Period End:";
            this.dtpEnd.Location = new System.Drawing.Point(150, 110);

            this.lblGross.Location = new System.Drawing.Point(30, 150);
            this.lblGross.Text = "Gross Pay:";
            this.txtGross.Location = new System.Drawing.Point(150, 150);
            this.txtGross.Text = "0.00";

            this.lblDeduct.Location = new System.Drawing.Point(30, 190);
            this.lblDeduct.Text = "Deductions (8%):";
            this.txtDeductions.Location = new System.Drawing.Point(150, 190);
            this.txtDeductions.Text = "0.00";
            this.txtDeductions.ReadOnly = true;

            this.btnCalc.Location = new System.Drawing.Point(30, 230);
            this.btnCalc.Text = "Calculate Net ->";
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);

            this.lblNet.Location = new System.Drawing.Point(150, 230);
            this.lblNet.Text = "Net Pay:";
            this.txtNet.Location = new System.Drawing.Point(230, 230);
            this.txtNet.ReadOnly = true;

            this.lblPay.Location = new System.Drawing.Point(30, 270);
            this.lblPay.Text = "Payment Date:";
            this.dtpPayment.Location = new System.Drawing.Point(150, 270);

            this.btnSave.Location = new System.Drawing.Point(150, 310);
            this.btnSave.Text = "Process Payroll";
            this.btnSave.Size = new System.Drawing.Size(120, 30);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.ClientSize = new System.Drawing.Size(400, 380);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.txtEmployeeId);
            this.Controls.Add(this.dtpStart);
            this.Controls.Add(this.dtpEnd);
            this.Controls.Add(this.dtpPayment);
            this.Controls.Add(this.txtGross);
            this.Controls.Add(this.txtDeductions);
            this.Controls.Add(this.txtNet);
            this.Controls.Add(this.lblEmployee);
            this.Controls.Add(this.lblStart);
            this.Controls.Add(this.lblEnd);
            this.Controls.Add(this.lblGross);
            this.Controls.Add(this.lblDeduct);
            this.Controls.Add(this.lblNet);
            this.Controls.Add(this.lblPay);
            this.Text = "Payroll Processing";
            this.ResumeLayout(false);
            this.PerformLayout();
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
    }
}
