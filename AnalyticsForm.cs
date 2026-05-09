using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EMS
{
    public partial class AnalyticsForm : Form
    {
        int CurrentEmployeeCode;
        string CurrentUserRole;

        public AnalyticsForm(int empCode, string role)
        {
            InitializeComponent();
            CurrentEmployeeCode = empCode;
            CurrentUserRole = role;
            this.Text = $"Analytics - {CurrentUserRole}";
        }

        private void AnalyticsForm_Load(object sender, EventArgs e)
        {
            LoadAnalytics();
        }

        void LoadAnalytics()
        {
            try
            {
                LoadLeaveAttendanceAnalytics();
                LoadPayrollAnalytics();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Analytics Error:\n" + ex.Message);
            }
        }
        // ADMIN ANALYTICS
       
        void LoadAdminAnalytics()
        {
            LoadSalaryOutliers();
            LoadAttendanceRisk();
        }

        void LoadSalaryOutliers()
        {
            try
            {
                DataTable dt = DBHelper.GetDataTable(
                    "SELECT EmployeeCode, Name, Department, BasicSalary, Status " +
                    "FROM vw_SalaryOutliers ORDER BY BasicSalary DESC");

                if (dt.Rows.Count == 0)
                    dt.Rows.Add(0, "No Data", "N/A", 0, "No Records");

                dgvAnalyticsResults.DataSource = dt;
                dgvAnalyticsResults.Columns["Status"].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Message");
                dt.Rows.Add($"Salary Outliers Error: {ex.Message}");
                dgvAnalyticsResults.DataSource = dt;
            }
        }

        void LoadAttendanceRisk()
        {
            try
            {
                DataTable dt = DBHelper.GetDataTable("EXEC sp_AttendanceRisk");

                if (dt.Rows.Count == 0)
                    dt.Rows.Add(0, "No Risk", "N/A", 0, 0, 0);

                dgvNotifications.DataSource = dt;
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Message");
                dt.Rows.Add($"Attendance Risk Error: {ex.Message}");
                dgvNotifications.DataSource = dt;
            }
        }

        // ===============================
        // HR ANALYTICS
        // ===============================
        void LoadHRAnalytics()
        {
            LoadDepartmentWorkload();
            LoadLeaveTrends();
        }

        void LoadDepartmentWorkload()
        {
            try
            {
                DataTable dt = DBHelper.GetDataTable(
                    "SELECT DepartmentName, TotalEmployees, TotalAttendance, PresentDays " +
                    "FROM vw_DepartmentWorkload");

                if (dt.Rows.Count == 0)
                    dt.Rows.Add("No Data", 0, 0, 0);

                dgvAnalyticsResults.DataSource = dt;
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Message");
                dt.Rows.Add($"Dept Workload Error: {ex.Message}");
                dgvAnalyticsResults.DataSource = dt;
            }
        }

        void LoadLeaveTrends()
        {
            try
            {
                DataTable dt = DBHelper.GetDataTable("EXEC sp_LeaveTrends");

                if (dt.Rows.Count == 0)
                    dt.Rows.Add("No Data", 0, "N/A");

                dgvNotifications.DataSource = dt;
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Message");
                dt.Rows.Add($"Leave Trends Error: {ex.Message}");
                dgvNotifications.DataSource = dt;
            }
        }

        // ===============================
        // EMPLOYEE ANALYTICS
        // ===============================
        void LoadEmployeeAnalytics()
        {
            LoadMyLeaves();
            LoadMyPayrollStatus();
        }

        void LoadMyLeaves()
        {
            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    string query = @"
                        SELECT TOP 10 l.LeaveID, l.Status, l.FromDate, l.ToDate, l.LeaveType,
                               DATEDIFF(DAY, l.FromDate, l.ToDate) + 1 AS Days
                        FROM Leaves l 
                        WHERE l.EmployeeCode = @code 
                        ORDER BY l.FromDate DESC";

                    SqlDataAdapter da = new SqlDataAdapter(query, con);
                    da.SelectCommand.Parameters.AddWithValue("@code", CurrentEmployeeCode);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count == 0)
                        dt.Rows.Add(0, "No Leaves", DateTime.Now, DateTime.Now, "N/A", 0);

                    dgvNotifications.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Message");
                dt.Rows.Add($"My Leaves Error: {ex.Message}");
                dgvNotifications.DataSource = dt;
            }
        }

        void LoadMyPayrollStatus()
        {
            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    // Current month payroll check
                    SqlCommand cmd = new SqlCommand(@"
                        SELECT COUNT(*) FROM Payroll p 
                        JOIN Employees e ON p.EmployeeCode = e.EmployeeCode
                        WHERE p.EmployeeCode = @code 
                        AND MONTH(p.NetSalary) > 0", con);  // Simplified check

                    cmd.Parameters.AddWithValue("@code", CurrentEmployeeCode);
                    con.Open();
                    int count = (int)cmd.ExecuteScalar();

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Status");
                    dt.Columns.Add("Message");
                    dt.Columns.Add("Action");

                    if (count > 0)
                    {
                        dt.Rows.Add("✅ Ready", "Payroll Processed", "View Payslip");
                    }
                    else
                    {
                        dt.Rows.Add("⏳ Pending", "Not Processed", "Contact HR");
                    }

                    dgvAnalyticsResults.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Message");
                dt.Rows.Add($"Payroll Error: {ex.Message}");
                dgvAnalyticsResults.DataSource = dt;
            }
        }

        // BUTTON EVENTS
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAnalytics();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.WindowState = FormWindowState.Normal;
                this.Owner.Show();
            }
            this.Close();
        }
        void LoadLeaveAttendanceAnalytics()
        {
            try
            {
                using (SqlConnection con =
                    DBHelper.GetConnection())
                {
                    string query = "";

                    // ADMIN
                    if (CurrentUserRole == "Admin")
                    {
                        query =
                        @"SELECT
                    e.EmployeeCode,
                    e.EmployeeName,
                    e.Role,
                    COUNT(l.LeaveID) AS TotalLeaves,
                    SUM(
                        CASE
                        WHEN a.Status='Present'
                        THEN 1 ELSE 0 END
                    ) AS PresentDays
                  FROM Employees e
                  LEFT JOIN Leaves l
                    ON e.EmployeeCode=l.EmployeeCode
                  LEFT JOIN Attendance a
                    ON e.EmployeeCode=a.EmployeeCode
                  GROUP BY
                    e.EmployeeCode,
                    e.EmployeeName,
                    e.Role";
                    }

                    // HR
                    else if (CurrentUserRole == "HR")
                    {
                        query =
                        @"SELECT
                    e.EmployeeCode,
                    e.EmployeeName,
                    e.Role,
                    COUNT(l.LeaveID) AS TotalLeaves,
                    SUM(
                        CASE
                        WHEN a.Status='Present'
                        THEN 1 ELSE 0 END
                    ) AS PresentDays
                  FROM Employees e
                  LEFT JOIN Leaves l
                    ON e.EmployeeCode=l.EmployeeCode
                  LEFT JOIN Attendance a
                    ON e.EmployeeCode=a.EmployeeCode
                  WHERE e.Role IN ('HR','Employee')
                  GROUP BY
                    e.EmployeeCode,
                    e.EmployeeName,
                    e.Role";
                    }

                    // EMPLOYEE
                    else
                    {
                        query =
                        @"SELECT
                    e.EmployeeCode,
                    e.EmployeeName,
                    COUNT(l.LeaveID) AS TotalLeaves,
                    SUM(
                        CASE
                        WHEN a.Status='Present'
                        THEN 1 ELSE 0 END
                    ) AS PresentDays
                  FROM Employees e
                  LEFT JOIN Leaves l
                    ON e.EmployeeCode=l.EmployeeCode
                  LEFT JOIN Attendance a
                    ON e.EmployeeCode=a.EmployeeCode
                  WHERE e.EmployeeCode=@code
                  GROUP BY
                    e.EmployeeCode,
                    e.EmployeeName";
                    }

                    SqlDataAdapter da =
                        new SqlDataAdapter(query, con);

                    if (CurrentUserRole == "Employee")
                    {
                        da.SelectCommand.Parameters.AddWithValue(
                            "@code",
                            CurrentEmployeeCode);
                    }

                    DataTable dt =
                        new DataTable();

                    da.Fill(dt);

                    dgvNotifications.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadPayrollAnalytics()
        {
            try
            {
                using (SqlConnection con =
                    DBHelper.GetConnection())
                {
                    string query = "";

                    // ADMIN
                    if (CurrentUserRole == "Admin")
                    {
                        query =
                        @"SELECT
                    EmployeeCode,
                    EmployeeName,
                    DepartmentName,
                    NetSalary,
                    'Payroll Available'
                    AS Status
                  FROM vw_Payslip";
                    }

                    // HR
                    else if (CurrentUserRole == "HR")
                    {
                        query =
                        @"SELECT
                    EmployeeCode,
                    EmployeeName,
                    DepartmentName,
                    NetSalary,
                    'Payroll Available'
                    AS Status
                  FROM vw_Payslip
                  WHERE Role IN ('HR','Employee')";
                    }

                    // EMPLOYEE
                    else
                    {
                        query =
                        @"SELECT
                    EmployeeCode,
                    EmployeeName,
                    DepartmentName,
                    NetSalary,
                    'Your Payroll Is Ready'
                    AS Status
                  FROM vw_Payslip
                  WHERE EmployeeCode=@code";
                    }

                    SqlDataAdapter da =
                        new SqlDataAdapter(query, con);

                    if (CurrentUserRole == "Employee")
                    {
                        da.SelectCommand.Parameters.AddWithValue(
                            "@code",
                            CurrentEmployeeCode);
                    }

                    DataTable dt =
                        new DataTable();

                    da.Fill(dt);

                    dgvAnalyticsResults.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}