using System;

namespace EMS
{
    partial class PayrollForm
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
            this.btnCalculate = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.grpPayrollDetails = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNetSalary = new System.Windows.Forms.TextBox();
            this.txtDeductions = new System.Windows.Forms.TextBox();
            this.txtAllowances = new System.Windows.Forms.TextBox();
            this.txtBasicSalary = new System.Windows.Forms.TextBox();
            this.txtEmployeeCode = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvPayroll = new System.Windows.Forms.DataGridView();
            this.btnViewPayslip = new System.Windows.Forms.Button();
            this.btnDownloadPayslip = new System.Windows.Forms.Button();
            this.cmbSortSalary = new System.Windows.Forms.ComboBox();
            this.btnSortSalary = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.grpPayrollDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayroll)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCalculate
            // 
            this.btnCalculate.Location = new System.Drawing.Point(435, 94);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(75, 23);
            this.btnCalculate.TabIndex = 0;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(435, 132);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(324, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(216, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "Payroll Management";
            // 
            // grpPayrollDetails
            // 
            this.grpPayrollDetails.Controls.Add(this.label6);
            this.grpPayrollDetails.Controls.Add(this.label4);
            this.grpPayrollDetails.Controls.Add(this.label5);
            this.grpPayrollDetails.Controls.Add(this.label3);
            this.grpPayrollDetails.Controls.Add(this.label2);
            this.grpPayrollDetails.Controls.Add(this.txtNetSalary);
            this.grpPayrollDetails.Controls.Add(this.txtDeductions);
            this.grpPayrollDetails.Controls.Add(this.txtAllowances);
            this.grpPayrollDetails.Controls.Add(this.txtBasicSalary);
            this.grpPayrollDetails.Controls.Add(this.txtEmployeeCode);
            this.grpPayrollDetails.Location = new System.Drawing.Point(38, 69);
            this.grpPayrollDetails.Name = "grpPayrollDetails";
            this.grpPayrollDetails.Size = new System.Drawing.Size(377, 197);
            this.grpPayrollDetails.TabIndex = 3;
            this.grpPayrollDetails.TabStop = false;
            this.grpPayrollDetails.Text = "Payroll Details";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Calculated Salary:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Deductions:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(22, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Allowances/Bonuses:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Basic Salary:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Employee Code:";
            // 
            // txtNetSalary
            // 
            this.txtNetSalary.Location = new System.Drawing.Point(185, 162);
            this.txtNetSalary.Name = "txtNetSalary";
            this.txtNetSalary.ReadOnly = true;
            this.txtNetSalary.Size = new System.Drawing.Size(100, 20);
            this.txtNetSalary.TabIndex = 4;
            // 
            // txtDeductions
            // 
            this.txtDeductions.Location = new System.Drawing.Point(185, 127);
            this.txtDeductions.Name = "txtDeductions";
            this.txtDeductions.Size = new System.Drawing.Size(100, 20);
            this.txtDeductions.TabIndex = 3;
            // 
            // txtAllowances
            // 
            this.txtAllowances.Location = new System.Drawing.Point(185, 91);
            this.txtAllowances.Name = "txtAllowances";
            this.txtAllowances.Size = new System.Drawing.Size(100, 20);
            this.txtAllowances.TabIndex = 2;
            // 
            // txtBasicSalary
            // 
            this.txtBasicSalary.Location = new System.Drawing.Point(185, 56);
            this.txtBasicSalary.Name = "txtBasicSalary";
            this.txtBasicSalary.Size = new System.Drawing.Size(100, 20);
            this.txtBasicSalary.TabIndex = 1;
            // 
            // txtEmployeeCode
            // 
            this.txtEmployeeCode.Location = new System.Drawing.Point(185, 20);
            this.txtEmployeeCode.Name = "txtEmployeeCode";
            this.txtEmployeeCode.Size = new System.Drawing.Size(100, 20);
            this.txtEmployeeCode.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(435, 233);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(435, 198);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(435, 166);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 6;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dgvPayroll
            // 
            this.dgvPayroll.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPayroll.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPayroll.Location = new System.Drawing.Point(50, 288);
            this.dgvPayroll.Name = "dgvPayroll";
            this.dgvPayroll.ReadOnly = true;
            this.dgvPayroll.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPayroll.Size = new System.Drawing.Size(709, 150);
            this.dgvPayroll.TabIndex = 7;
            // 
            // btnViewPayslip
            // 
            this.btnViewPayslip.Location = new System.Drawing.Point(527, 95);
            this.btnViewPayslip.Name = "btnViewPayslip";
            this.btnViewPayslip.Size = new System.Drawing.Size(75, 23);
            this.btnViewPayslip.TabIndex = 8;
            this.btnViewPayslip.Text = "View Payslip";
            this.btnViewPayslip.UseVisualStyleBackColor = true;
            this.btnViewPayslip.Click += new System.EventHandler(this.btnViewPayslip_Click);
            // 
            // btnDownloadPayslip
            // 
            this.btnDownloadPayslip.Location = new System.Drawing.Point(527, 132);
            this.btnDownloadPayslip.Name = "btnDownloadPayslip";
            this.btnDownloadPayslip.Size = new System.Drawing.Size(75, 44);
            this.btnDownloadPayslip.TabIndex = 9;
            this.btnDownloadPayslip.Text = "Download Payslip";
            this.btnDownloadPayslip.UseVisualStyleBackColor = true;
            this.btnDownloadPayslip.Click += new System.EventHandler(this.btnDownloadPayslip_Click);
            // 
            // cmbSortSalary
            // 
            this.cmbSortSalary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSortSalary.FormattingEnabled = true;
            this.cmbSortSalary.Items.AddRange(new object[] {
            "Ascending",
            "Descending"});
            this.cmbSortSalary.Location = new System.Drawing.Point(658, 116);
            this.cmbSortSalary.Name = "cmbSortSalary";
            this.cmbSortSalary.Size = new System.Drawing.Size(121, 21);
            this.cmbSortSalary.TabIndex = 12;
            // 
            // btnSortSalary
            // 
            this.btnSortSalary.Location = new System.Drawing.Point(684, 143);
            this.btnSortSalary.Name = "btnSortSalary";
            this.btnSortSalary.Size = new System.Drawing.Size(75, 23);
            this.btnSortSalary.TabIndex = 13;
            this.btnSortSalary.Text = "Sort Salary";
            this.btnSortSalary.UseVisualStyleBackColor = true;
            this.btnSortSalary.Click += new System.EventHandler(this.btnSortSalary_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(684, 184);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 37);
            this.button1.TabIndex = 14;
            this.button1.Text = "Reset Sorting";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(632, 238);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(127, 23);
            this.btnBack.TabIndex = 15;
            this.btnBack.Text = "Back to Dashboard";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // PayrollForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(856, 733);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnSortSalary);
            this.Controls.Add(this.cmbSortSalary);
            this.Controls.Add(this.btnDownloadPayslip);
            this.Controls.Add(this.btnViewPayslip);
            this.Controls.Add(this.dgvPayroll);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.grpPayrollDetails);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnCalculate);
            this.ForeColor = System.Drawing.Color.MidnightBlue;
            this.Name = "PayrollForm";
            this.Text = "Payroll Management";
            this.Load += new System.EventHandler(this.PayrollForm_Load);
            this.grpPayrollDetails.ResumeLayout(false);
            this.grpPayrollDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPayroll)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpPayrollDetails;
        private System.Windows.Forms.TextBox txtNetSalary;
        private System.Windows.Forms.TextBox txtDeductions;
        private System.Windows.Forms.TextBox txtAllowances;
        private System.Windows.Forms.TextBox txtBasicSalary;
        private System.Windows.Forms.TextBox txtEmployeeCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvPayroll;
        private System.Windows.Forms.Button btnViewPayslip;
        private System.Windows.Forms.Button btnDownloadPayslip;
        private System.Windows.Forms.ComboBox cmbSortSalary;
        private System.Windows.Forms.Button btnSortSalary;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnBack;
    }
}