namespace EMS
{
    partial class LeaveAttendanceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpLeave = new System.Windows.Forms.GroupBox();
            this.btnClearLeaveFilter = new System.Windows.Forms.Button();
            this.btnFilterLeaves = new System.Windows.Forms.Button();
            this.cmbStatusFilter = new System.Windows.Forms.ComboBox();
            this.lblLeaveBalance = new System.Windows.Forms.Label();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnApprove = new System.Windows.Forms.Button();
            this.btnApplyLeave = new System.Windows.Forms.Button();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbLeaveType = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmployeeCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpAttendance = new System.Windows.Forms.GroupBox();
            this.btnDeleteAttendance = new System.Windows.Forms.Button();
            this.btnMarkAttendance = new System.Windows.Forms.Button();
            this.cmbAttendanceStatus = new System.Windows.Forms.ComboBox();
            this.Status = new System.Windows.Forms.Label();
            this.dtpAttendanceDate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.txtAttendanceEmployeeCode = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvLeaves = new System.Windows.Forms.DataGridView();
            this.dgvAttendance = new System.Windows.Forms.DataGridView();
            this.btnBack = new System.Windows.Forms.Button();
            this.grpLeave.SuspendLayout();
            this.grpAttendance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttendance)).BeginInit();
            this.SuspendLayout();
            // 
            // grpLeave
            // 
            this.grpLeave.Controls.Add(this.btnClearLeaveFilter);
            this.grpLeave.Controls.Add(this.btnFilterLeaves);
            this.grpLeave.Controls.Add(this.cmbStatusFilter);
            this.grpLeave.Controls.Add(this.lblLeaveBalance);
            this.grpLeave.Controls.Add(this.btnReject);
            this.grpLeave.Controls.Add(this.btnApprove);
            this.grpLeave.Controls.Add(this.btnApplyLeave);
            this.grpLeave.Controls.Add(this.dtpToDate);
            this.grpLeave.Controls.Add(this.label4);
            this.grpLeave.Controls.Add(this.dtpFromDate);
            this.grpLeave.Controls.Add(this.label3);
            this.grpLeave.Controls.Add(this.cmbLeaveType);
            this.grpLeave.Controls.Add(this.label2);
            this.grpLeave.Controls.Add(this.txtEmployeeCode);
            this.grpLeave.Controls.Add(this.label1);
            this.grpLeave.Location = new System.Drawing.Point(30, 41);
            this.grpLeave.Name = "grpLeave";
            this.grpLeave.Size = new System.Drawing.Size(355, 325);
            this.grpLeave.TabIndex = 0;
            this.grpLeave.TabStop = false;
            this.grpLeave.Text = "Leave Management";
            // 
            // btnClearLeaveFilter
            // 
            this.btnClearLeaveFilter.Location = new System.Drawing.Point(260, 251);
            this.btnClearLeaveFilter.Name = "btnClearLeaveFilter";
            this.btnClearLeaveFilter.Size = new System.Drawing.Size(75, 43);
            this.btnClearLeaveFilter.TabIndex = 14;
            this.btnClearLeaveFilter.Text = "Clear Filter";
            this.btnClearLeaveFilter.UseVisualStyleBackColor = true;
            this.btnClearLeaveFilter.Click += new System.EventHandler(this.btnClearLeaveFilter_Click);
            // 
            // btnFilterLeaves
            // 
            this.btnFilterLeaves.Location = new System.Drawing.Point(31, 251);
            this.btnFilterLeaves.Name = "btnFilterLeaves";
            this.btnFilterLeaves.Size = new System.Drawing.Size(75, 43);
            this.btnFilterLeaves.TabIndex = 13;
            this.btnFilterLeaves.Text = "Filter Leaves";
            this.btnFilterLeaves.UseVisualStyleBackColor = true;
            this.btnFilterLeaves.Click += new System.EventHandler(this.btnFilterLeaves_Click);
            // 
            // cmbStatusFilter
            // 
            this.cmbStatusFilter.FormattingEnabled = true;
            this.cmbStatusFilter.Items.AddRange(new object[] {
            "All",
            "Pending",
            "Approved",
            "Rejected"});
            this.cmbStatusFilter.Location = new System.Drawing.Point(114, 261);
            this.cmbStatusFilter.Name = "cmbStatusFilter";
            this.cmbStatusFilter.Size = new System.Drawing.Size(121, 25);
            this.cmbStatusFilter.TabIndex = 12;
            // 
            // lblLeaveBalance
            // 
            this.lblLeaveBalance.AutoSize = true;
            this.lblLeaveBalance.Location = new System.Drawing.Point(9, 149);
            this.lblLeaveBalance.Name = "lblLeaveBalance";
            this.lblLeaveBalance.Size = new System.Drawing.Size(115, 17);
            this.lblLeaveBalance.TabIndex = 11;
            this.lblLeaveBalance.Text = "Remaining Leaves:";
            // 
            // btnReject
            // 
            this.btnReject.Location = new System.Drawing.Point(239, 186);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(75, 43);
            this.btnReject.TabIndex = 10;
            this.btnReject.Text = "Reject";
            this.btnReject.UseVisualStyleBackColor = true;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // btnApprove
            // 
            this.btnApprove.Location = new System.Drawing.Point(135, 186);
            this.btnApprove.Name = "btnApprove";
            this.btnApprove.Size = new System.Drawing.Size(75, 43);
            this.btnApprove.TabIndex = 9;
            this.btnApprove.Text = "Approve";
            this.btnApprove.UseVisualStyleBackColor = true;
            this.btnApprove.Click += new System.EventHandler(this.btnApprove_Click);
            // 
            // btnApplyLeave
            // 
            this.btnApplyLeave.Location = new System.Drawing.Point(31, 186);
            this.btnApplyLeave.Name = "btnApplyLeave";
            this.btnApplyLeave.Size = new System.Drawing.Size(75, 43);
            this.btnApplyLeave.TabIndex = 8;
            this.btnApplyLeave.Text = "Apply Leave";
            this.btnApplyLeave.UseVisualStyleBackColor = true;
            this.btnApplyLeave.Click += new System.EventHandler(this.btnApplyLeave_Click);
            // 
            // dtpToDate
            // 
            this.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpToDate.Location = new System.Drawing.Point(135, 114);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(200, 25);
            this.dtpToDate.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "To Date";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFromDate.Location = new System.Drawing.Point(135, 79);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(200, 25);
            this.dtpFromDate.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "From Date";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbLeaveType
            // 
            this.cmbLeaveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLeaveType.FormattingEnabled = true;
            this.cmbLeaveType.Items.AddRange(new object[] {
            "Casual",
            "Sick",
            "Annual"});
            this.cmbLeaveType.Location = new System.Drawing.Point(135, 46);
            this.cmbLeaveType.Name = "cmbLeaveType";
            this.cmbLeaveType.Size = new System.Drawing.Size(121, 25);
            this.cmbLeaveType.TabIndex = 3;
            this.cmbLeaveType.SelectedIndexChanged += new System.EventHandler(this.cmbLeaveType_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Leave Type";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtEmployeeCode
            // 
            this.txtEmployeeCode.Location = new System.Drawing.Point(135, 18);
            this.txtEmployeeCode.MaxLength = 6;
            this.txtEmployeeCode.Name = "txtEmployeeCode";
            this.txtEmployeeCode.Size = new System.Drawing.Size(100, 25);
            this.txtEmployeeCode.TabIndex = 1;
            this.txtEmployeeCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Employee Code";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpAttendance
            // 
            this.grpAttendance.Controls.Add(this.btnDeleteAttendance);
            this.grpAttendance.Controls.Add(this.btnMarkAttendance);
            this.grpAttendance.Controls.Add(this.cmbAttendanceStatus);
            this.grpAttendance.Controls.Add(this.Status);
            this.grpAttendance.Controls.Add(this.dtpAttendanceDate);
            this.grpAttendance.Controls.Add(this.label6);
            this.grpAttendance.Controls.Add(this.txtAttendanceEmployeeCode);
            this.grpAttendance.Controls.Add(this.label5);
            this.grpAttendance.Location = new System.Drawing.Point(405, 41);
            this.grpAttendance.Name = "grpAttendance";
            this.grpAttendance.Size = new System.Drawing.Size(355, 270);
            this.grpAttendance.TabIndex = 11;
            this.grpAttendance.TabStop = false;
            this.grpAttendance.Text = "Attendance Management";
            // 
            // btnDeleteAttendance
            // 
            this.btnDeleteAttendance.Location = new System.Drawing.Point(212, 199);
            this.btnDeleteAttendance.Name = "btnDeleteAttendance";
            this.btnDeleteAttendance.Size = new System.Drawing.Size(100, 43);
            this.btnDeleteAttendance.TabIndex = 16;
            this.btnDeleteAttendance.Text = "Delete Attendance";
            this.btnDeleteAttendance.UseVisualStyleBackColor = true;
            this.btnDeleteAttendance.Click += new System.EventHandler(this.btnDeleteAttendance_Click);
            // 
            // btnMarkAttendance
            // 
            this.btnMarkAttendance.Location = new System.Drawing.Point(75, 199);
            this.btnMarkAttendance.Name = "btnMarkAttendance";
            this.btnMarkAttendance.Size = new System.Drawing.Size(100, 43);
            this.btnMarkAttendance.TabIndex = 15;
            this.btnMarkAttendance.Text = "Mark Attendance";
            this.btnMarkAttendance.UseVisualStyleBackColor = true;
            this.btnMarkAttendance.Click += new System.EventHandler(this.btnMarkAttendance_Click);
            // 
            // cmbAttendanceStatus
            // 
            this.cmbAttendanceStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAttendanceStatus.FormattingEnabled = true;
            this.cmbAttendanceStatus.Items.AddRange(new object[] {
            "Present",
            "Absent"});
            this.cmbAttendanceStatus.Location = new System.Drawing.Point(75, 126);
            this.cmbAttendanceStatus.Name = "cmbAttendanceStatus";
            this.cmbAttendanceStatus.Size = new System.Drawing.Size(121, 25);
            this.cmbAttendanceStatus.TabIndex = 14;
            // 
            // Status
            // 
            this.Status.AutoSize = true;
            this.Status.Location = new System.Drawing.Point(6, 134);
            this.Status.Name = "Status";
            this.Status.Size = new System.Drawing.Size(53, 17);
            this.Status.TabIndex = 13;
            this.Status.Text = "To Date";
            this.Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpAttendanceDate
            // 
            this.dtpAttendanceDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpAttendanceDate.Location = new System.Drawing.Point(75, 72);
            this.dtpAttendanceDate.Name = "dtpAttendanceDate";
            this.dtpAttendanceDate.Size = new System.Drawing.Size(200, 25);
            this.dtpAttendanceDate.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 79);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(35, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "Date";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAttendanceEmployeeCode
            // 
            this.txtAttendanceEmployeeCode.Location = new System.Drawing.Point(137, 23);
            this.txtAttendanceEmployeeCode.MaxLength = 6;
            this.txtAttendanceEmployeeCode.Name = "txtAttendanceEmployeeCode";
            this.txtAttendanceEmployeeCode.Size = new System.Drawing.Size(100, 25);
            this.txtAttendanceEmployeeCode.TabIndex = 2;
            this.txtAttendanceEmployeeCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 17);
            this.label5.TabIndex = 1;
            this.label5.Text = "Employee Code";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvLeaves
            // 
            this.dgvLeaves.AllowUserToAddRows = false;
            this.dgvLeaves.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLeaves.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLeaves.Location = new System.Drawing.Point(30, 372);
            this.dgvLeaves.MultiSelect = false;
            this.dgvLeaves.Name = "dgvLeaves";
            this.dgvLeaves.ReadOnly = true;
            this.dgvLeaves.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLeaves.Size = new System.Drawing.Size(355, 172);
            this.dgvLeaves.TabIndex = 12;
            this.dgvLeaves.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvLeaves_CellFormatting_1);
            // 
            // dgvAttendance
            // 
            this.dgvAttendance.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAttendance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAttendance.Location = new System.Drawing.Point(405, 372);
            this.dgvAttendance.Name = "dgvAttendance";
            this.dgvAttendance.ReadOnly = true;
            this.dgvAttendance.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAttendance.Size = new System.Drawing.Size(355, 172);
            this.dgvAttendance.TabIndex = 13;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(511, 334);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(127, 23);
            this.btnBack.TabIndex = 16;
            this.btnBack.Text = "Back to Dashboard";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // LeaveAttendanceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(856, 737);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.dgvAttendance);
            this.Controls.Add(this.dgvLeaves);
            this.Controls.Add(this.grpAttendance);
            this.Controls.Add(this.grpLeave);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.MidnightBlue;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "LeaveAttendanceForm";
            this.Text = "DepartmentForm";
            this.Load += new System.EventHandler(this.LeaveAttendanceForm_Load);
            this.grpLeave.ResumeLayout(false);
            this.grpLeave.PerformLayout();
            this.grpAttendance.ResumeLayout(false);
            this.grpAttendance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLeaves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAttendance)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpLeave;
        private System.Windows.Forms.TextBox txtEmployeeCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbLeaveType;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnApplyLeave;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnApprove;
        private System.Windows.Forms.GroupBox grpAttendance;
        private System.Windows.Forms.DateTimePicker dtpAttendanceDate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtAttendanceEmployeeCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnMarkAttendance;
        private System.Windows.Forms.ComboBox cmbAttendanceStatus;
        private System.Windows.Forms.Label Status;
        private System.Windows.Forms.DataGridView dgvLeaves;
        private System.Windows.Forms.DataGridView dgvAttendance;
        private System.Windows.Forms.Label lblLeaveBalance;
        private System.Windows.Forms.ComboBox cmbStatusFilter;
        private System.Windows.Forms.Button btnClearLeaveFilter;
        private System.Windows.Forms.Button btnFilterLeaves;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnDeleteAttendance;
    }
}