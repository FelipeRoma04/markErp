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
            this.btnSave = new System.Windows.Forms.Button();
            this.txtEmployeeId = new System.Windows.Forms.TextBox();
            this.dtpWorkDate = new System.Windows.Forms.DateTimePicker();
            this.txtHours = new System.Windows.Forms.TextBox();
            this.txtOvertime = new System.Windows.Forms.TextBox();
            this.chkAbsent = new System.Windows.Forms.CheckBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.lblEmployee = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblHours = new System.Windows.Forms.Label();
            this.lblNotes = new System.Windows.Forms.Label();
            this.lblOvertime = new System.Windows.Forms.Label();
            this.SuspendLayout();

            this.lblEmployee.Location = new System.Drawing.Point(30, 30);
            this.lblEmployee.Text = "Employee ID:";
            
            this.txtEmployeeId.Location = new System.Drawing.Point(150, 30);
            
            this.lblDate.Location = new System.Drawing.Point(30, 70);
            this.lblDate.Text = "Work Date:";
            
            this.dtpWorkDate.Location = new System.Drawing.Point(150, 70);
            
            this.lblHours.Location = new System.Drawing.Point(30, 110);
            this.lblHours.Text = "Hours Worked:";
            
            this.txtHours.Location = new System.Drawing.Point(150, 110);
            this.txtHours.Text = "8";
            
            this.lblOvertime.Location = new System.Drawing.Point(30, 150);
            this.lblOvertime.Text = "Overtime:";
            
            this.txtOvertime.Location = new System.Drawing.Point(150, 150);
            this.txtOvertime.Text = "0";
            
            this.chkAbsent.Location = new System.Drawing.Point(150, 190);
            this.chkAbsent.Text = "Is Absent";

            this.lblNotes.Location = new System.Drawing.Point(30, 230);
            this.lblNotes.Text = "Notes:";
            
            this.txtNotes.Location = new System.Drawing.Point(150, 230);

            this.btnSave.Location = new System.Drawing.Point(150, 280);
            this.btnSave.Text = "Save Attendance";
            this.btnSave.Size = new System.Drawing.Size(120, 30);
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.ClientSize = new System.Drawing.Size(400, 350);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtEmployeeId);
            this.Controls.Add(this.dtpWorkDate);
            this.Controls.Add(this.txtHours);
            this.Controls.Add(this.txtOvertime);
            this.Controls.Add(this.chkAbsent);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblEmployee);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblHours);
            this.Controls.Add(this.lblOvertime);
            this.Controls.Add(this.lblNotes);
            this.Text = "Attendance Tracker";
            this.ResumeLayout(false);
            this.PerformLayout();
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
    }
}
