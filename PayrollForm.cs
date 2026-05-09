using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.IO;
using System.Windows.Forms;

namespace EMS
{
    public partial class PayrollForm : Form
    {
        Dictionary<int, Payroll> payrollMap =
            new Dictionary<int, Payroll>();

        int CurrentEmployeeCode;
        string CurrentUserRole;

        public PayrollForm(int empCode, string role)
        {
            InitializeComponent();
            CurrentEmployeeCode = empCode;
            CurrentUserRole = role;
        }

        private void PayrollForm_Load(object sender, EventArgs e)
        {
            ApplyRolePermissions();
            LoadPayrollFromDB();
            LoadGrid();
        }

        // ===============================
        // ROLE CONTROL
        // ===============================
        void ApplyRolePermissions()
        {
            txtEmployeeCode.Text =
                CurrentEmployeeCode.ToString();

            // =========================
            // EMPLOYEE
            // =========================
            if (CurrentUserRole == "Employee")
            {
                txtEmployeeCode.ReadOnly = true;

                // Employee can ONLY VIEW
                btnSave.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;

                // Employee can still:
                // Calculate
                // View Payslip
                // Download
                // Search
                // Sort

                txtEmployeeCode.Enabled = false;
            }

            // =========================
            // HR
            // =========================
            else if (CurrentUserRole == "HR")
            {
                // HR can fully manage payroll
                btnSave.Enabled = true;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }

            // =========================
            // ADMIN
            // =========================
            else if (CurrentUserRole == "Admin")
            {
                // Full access
                btnSave.Enabled = true;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        // ===============================
        // VALIDATION
        // ===============================
        bool ValidateInputs()
        {
            if (txtEmployeeCode.Text == "" ||
                txtBasicSalary.Text == "" ||
                txtAllowances.Text == "" ||
                txtDeductions.Text == "")
            {
                MessageBox.Show("Fill all fields");
                return false;
            }

            if (!double.TryParse(txtBasicSalary.Text, out _) ||
                !double.TryParse(txtAllowances.Text, out _) ||
                !double.TryParse(txtDeductions.Text, out _))
            {
                MessageBox.Show("Invalid numeric values");
                return false;
            }

            return true;
        }

        // ===============================
        // CALCULATE
        // ===============================
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            try
            {
                double basic = double.Parse(txtBasicSalary.Text);
                double allow = double.Parse(txtAllowances.Text);
                double deduct = double.Parse(txtDeductions.Text);

                txtNetSalary.Text = (basic + allow - deduct).ToString("0.00");
            }
            catch
            {
                MessageBox.Show("Invalid values");
            }
        }

        // ===============================
        // LOAD FROM DB
        // ===============================
        void LoadPayrollFromDB()
        {
            payrollMap.Clear();

            DataTable dt;

            // =========================
            // EMPLOYEE -> ONLY OWN DATA
            // =========================
            if (CurrentUserRole == "Employee")
            {
                dt = DBHelper.GetDataTable(
                    $"SELECT * FROM vw_PayrollDetails " +
                    $"WHERE EmployeeCode={CurrentEmployeeCode}");
            }

            // =========================
            // ADMIN / HR -> ALL DATA
            // =========================
            else
            {
                dt = DBHelper.GetDataTable(
                    "SELECT * FROM vw_PayrollDetails");
            }

            foreach (DataRow row in dt.Rows)
            {
                Payroll p = new Payroll
                {
                    EmployeeCode =
                        Convert.ToInt32(row["EmployeeCode"]),

                    BasicSalary =
                        Convert.ToDouble(row["BasicSalary"]),

                    Allowances =
                        Convert.ToDouble(row["Allowances"]),

                    Deductions =
                        Convert.ToDouble(row["Deductions"]),

                    NetSalary =
                        Convert.ToDouble(row["NetSalary"])
                };

                payrollMap[p.EmployeeCode] = p;
            }
        }

        // SAVE
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            int code = int.Parse(txtEmployeeCode.Text);

            if (payrollMap.ContainsKey(code))
            {
                MessageBox.Show("Already exists. Use UPDATE.");
                return;
            }

            DBHelper.ExecuteNonQuery(
                "sp_InsertPayroll",
                CommandType.StoredProcedure,
                new SqlParameter("@EmployeeCode", code),
                new SqlParameter("@BasicSalary", txtBasicSalary.Text),
                new SqlParameter("@Allowances", txtAllowances.Text),
                new SqlParameter("@Deductions", txtDeductions.Text)
            );

            MessageBox.Show("Saved");

            LoadPayrollFromDB();
            LoadGrid();
        }

        // UPDATE
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            int code = int.Parse(txtEmployeeCode.Text);

            if (!payrollMap.ContainsKey(code))
            {
                MessageBox.Show("Record not found");
                return;
            }

            DBHelper.ExecuteNonQuery(
                "sp_UpdatePayroll",
                CommandType.StoredProcedure,
                new SqlParameter("@EmployeeCode", code),
                new SqlParameter("@BasicSalary", txtBasicSalary.Text),
                new SqlParameter("@Allowances", txtAllowances.Text),
                new SqlParameter("@Deductions", txtDeductions.Text)
            );

            MessageBox.Show("Updated");

            LoadPayrollFromDB();
            LoadGrid();
        }

        // DELETE
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CurrentUserRole != "Admin")
            {
                MessageBox.Show("Only Admin can delete");
                return;
            }

            int code = int.Parse(txtEmployeeCode.Text);

            if (!payrollMap.ContainsKey(code))
            {
                MessageBox.Show("Not found");
                return;
            }

            if (MessageBox.Show("Delete?", "Confirm",
                MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            DBHelper.ExecuteNonQuery(
                "sp_DeletePayroll",
                CommandType.StoredProcedure,
                new SqlParameter("@EmployeeCode", code)
            );

            MessageBox.Show("Deleted");

            LoadPayrollFromDB();
            LoadGrid();
        }

        // GRID
        void LoadGrid()
        {
            dgvPayroll.DataSource = null;
            dgvPayroll.DataSource = payrollMap.Values.ToList();
        }

        // VIEW PAYSLIP
        private void btnViewPayslip_Click(object sender, EventArgs e)
        {
            try
            {
                int code = int.Parse(
                    txtEmployeeCode.Text);

                // Employee restriction
                if (CurrentUserRole == "Employee" &&
                    code != CurrentEmployeeCode)
                {
                    MessageBox.Show(
                        "Access denied");
                    return;
                }

                DataTable dt =
                    DBHelper.GetDataTable(
                        $"SELECT * FROM vw_Payslip " +
                        $"WHERE EmployeeCode={code}");

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No record");
                    return;
                }

                DataRow r = dt.Rows[0];

                string slip =
                    $"Employee: {r["EmployeeName"]}\n" +
                    $"Department: {r["DepartmentName"]}\n\n" +
                    $"Basic: {r["BasicSalary"]}\n" +
                    $"Allowances: {r["Allowances"]}\n" +
                    $"Deductions: {r["Deductions"]}\n" +
                    $"----------------------\n" +
                    $"Net Salary: {r["NetSalary"]}";

                MessageBox.Show(slip, "Payslip");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // DOWNLOAD PAYSLIP (TXT)
        private void btnDownloadPayslip_Click(
    object sender,
    EventArgs e)
        {
            try
            {
                int code =
                    int.Parse(txtEmployeeCode.Text);

                // Employee restriction
                if (CurrentUserRole == "Employee" &&
                    code != CurrentEmployeeCode)
                {
                    MessageBox.Show(
                        "Access denied");
                    return;
                }

                DataTable dt =
                    DBHelper.GetDataTable(
                        $"SELECT * FROM vw_Payslip " +
                        $"WHERE EmployeeCode={code}");

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No data");
                    return;
                }

                DataRow r = dt.Rows[0];

                string content =
                    $"Employee: {r["EmployeeName"]}\r\n" +
                    $"Department: {r["DepartmentName"]}\r\n\r\n" +
                    $"Basic: {r["BasicSalary"]}\r\n" +
                    $"Allowances: {r["Allowances"]}\r\n" +
                    $"Deductions: {r["Deductions"]}\r\n" +
                    $"----------------------\r\n" +
                    $"Net Salary: {r["NetSalary"]}";

                SaveFileDialog sfd =
                    new SaveFileDialog();

                sfd.Filter =
                    "Text File (*.txt)|*.txt";

                sfd.FileName =
                    "Payslip.txt";

                if (sfd.ShowDialog() ==
                    DialogResult.OK)
                {
                    File.WriteAllText(
                        sfd.FileName,
                        content);

                    MessageBox.Show(
                        "Downloaded");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // SEARCH
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(
                txtEmployeeCode.Text,
                out int code))
            {
                MessageBox.Show("Invalid code");
                return;
            }

            // Employee can only search own payroll
            if (CurrentUserRole == "Employee" &&
                code != CurrentEmployeeCode)
            {
                MessageBox.Show(
                    "You can only access your payroll");
                return;
            }

            if (!payrollMap.ContainsKey(code))
            {
                MessageBox.Show("Not found");
                return;
            }

            dgvPayroll.DataSource =
                new List<Payroll>()
                {
            payrollMap[code]
                };
        }

        // SORT
        private void btnSortSalary_Click(
    object sender,
    EventArgs e)
        {
            try
            {
                if (cmbSortSalary.SelectedIndex == -1)
                {
                    MessageBox.Show(
                        "Select sorting option");

                    return;
                }

                using (SqlConnection con =
                    DBHelper.GetConnection())
                {
                    SqlCommand cmd =
                        new SqlCommand(
                            "sp_SortPayrollBySalary",
                            con);

                    cmd.CommandType =
                        CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue(
                        "@Role",
                        CurrentUserRole);

                    cmd.Parameters.AddWithValue(
                        "@EmployeeCode",
                        CurrentEmployeeCode);

                    cmd.Parameters.AddWithValue(
                        "@SortOrder",
                        cmbSortSalary.Text);

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    DataTable dt =
                        new DataTable();

                    da.Fill(dt);

                    dgvPayroll.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // UNDO / REDO
        private void btnUndo_Click(object sender, EventArgs e)
        {
            UndoRedoManager.Undo();
            LoadPayrollFromDB();
            LoadGrid();
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            UndoRedoManager.Redo();
            LoadPayrollFromDB();
            LoadGrid();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}