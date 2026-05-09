using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EMS
{
    public partial class MainDashboardForm : Form
    {
        int CurrentEmployeeCode;
        string CurrentEmployeeName;
        string CurrentUserRole;
        public MainDashboardForm()
        {
            InitializeComponent();
        }
        public MainDashboardForm(int empCode, string role)
        {
            InitializeComponent();

            CurrentEmployeeCode = empCode;
            CurrentUserRole = role;
        }

        private void MainDashboardForm_Load(object sender, EventArgs e)
        {
            if (CurrentEmployeeCode > 0)
            {
                ApplyRolePermissions();
            }
            else
            {
                ResetDashboard();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtEmployeeCode.Text, out int empCode))
            {
                MessageBox.Show("Invalid Employee Code");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Enter password");
                return;
            }

            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    using (SqlCommand cmd = new SqlCommand("sp_LoginUser", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@EmployeeCode", empCode);
                        cmd.Parameters.AddWithValue("@Password", txtPassword.Text);

                        con.Open();

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (!dr.Read())
                            {
                                MessageBox.Show("Invalid credentials");
                                return;
                            }

                            CurrentEmployeeCode = empCode;
                            CurrentEmployeeName = dr["EmployeeName"].ToString();
                            CurrentUserRole = dr["Role"].ToString();
                        }
                    }
                }

                MessageBox.Show($"Welcome {CurrentEmployeeName} ({CurrentUserRole})");

                ApplyRolePermissions();
                btnLogin.Enabled = false;
                txtEmployeeCode.Enabled = false;
                txtPassword.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        void ApplyRolePermissions()
        {
            // =========================
            // DEFAULT
            // =========================
            btnEmployee.Enabled = true;
            btnLeaveAttendance.Enabled = true;
            btnPayroll.Enabled = true;
            btnAnalytics.Enabled = true;
            btnLogout.Enabled = true;

            btnDepartment.Enabled = false;

            // =========================
            // ADMIN
            // =========================
            if (CurrentUserRole == "Admin")
            {
                btnDepartment.Enabled = true;
            }

            // =========================
            // HR
            // =========================
            else if (CurrentUserRole == "HR")
            {
                btnDepartment.Enabled = true;
            }

            // =========================
            // EMPLOYEE
            // =========================
            else if (CurrentUserRole == "Employee")
            {
                // Employee cannot access Department module
                btnDepartment.Enabled = false;
            }
        }

        void ResetDashboard()
        {
            btnEmployee.Enabled = false;
            btnLeaveAttendance.Enabled = false;
            btnDepartment.Enabled = false;
            btnPayroll.Enabled = false;
            btnAnalytics.Enabled = false;
            btnLogout.Enabled = false;
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            new EmployeeForm(CurrentEmployeeCode, CurrentUserRole)
            {
                Owner = this
            }.Show();

            this.Hide();
        }

        private void btnLeaveAttendance_Click(object sender, EventArgs e)
        {
            new LeaveAttendanceForm(CurrentEmployeeCode, CurrentUserRole)
            {
                Owner = this
            }.Show();

            this.Hide();
        }

        private void btnDepartment_Click(object sender, EventArgs e)
        {
            new DepartmentForm(CurrentEmployeeCode, CurrentUserRole)
            {
                Owner = this
            }.Show();

            this.Hide();
        }

        private void btnPayroll_Click(object sender, EventArgs e)
        {
            new PayrollForm(CurrentEmployeeCode, CurrentUserRole)
            {
                Owner = this
            }.Show();

            this.Hide();
        }

        private void btnAnalytics_Click(object sender, EventArgs e)
        {
            new AnalyticsForm(CurrentEmployeeCode, CurrentUserRole)
            {
                Owner = this
            }.Show();

            this.Hide();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            CurrentEmployeeCode = 0;
            CurrentEmployeeName = "";
            CurrentUserRole = "";

            txtEmployeeCode.Clear();
            txtPassword.Clear();

            ResetDashboard();

            MessageBox.Show("Logged out successfully");
            btnLogin.Enabled = false;

            txtEmployeeCode.Enabled = false;
            txtPassword.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
        }
    }
}