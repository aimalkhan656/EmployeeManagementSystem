using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace EMS
{
    public partial class LeaveAttendanceForm : Form
    {
        // ✅ INTERNAL Classes - No namespace conflict
        private class LeaveRecord
        {
            public int LeaveID { get; set; }
            public int EmployeeCode { get; set; }
            public string LeaveType { get; set; }
            public DateTime FromDate { get; set; }
            public DateTime ToDate { get; set; }
            public string Status { get; set; }
        }

        private class AttendanceRecord
        {
            public int AttendanceID { get; set; }
            public int EmployeeCode { get; set; }
            public DateTime Dated { get; set; }
            public string Status { get; set; }
        }

        // Rest of your code remains SAME...
        Dictionary<int, List<LeaveRecord>> leaveMap = new Dictionary<int, List<LeaveRecord>>();
        Dictionary<int, List<AttendanceRecord>> attendanceMap = new Dictionary<int, List<AttendanceRecord>>();

        int CurrentEmployeeCode;
        string CurrentUserRole;

        public LeaveAttendanceForm(int empCode, string role)
        {
            InitializeComponent();
            CurrentEmployeeCode = empCode;
            CurrentUserRole = role;
        }

        private void LeaveAttendanceForm_Load(object sender, EventArgs e)
        {
            ApplyRolePermissions();

            // =========================
            // LEAVE TYPES
            // =========================
            cmbLeaveType.Items.Clear();

            cmbLeaveType.Items.Add("Casual");
            cmbLeaveType.Items.Add("Sick");
            cmbLeaveType.Items.Add("Annual");

            // Default selection
            cmbLeaveType.SelectedIndex = 0;

            // Prevent typing custom values
            cmbLeaveType.DropDownStyle =
                ComboBoxStyle.DropDownList;

            // =========================
            // STATUS FILTER
            // =========================
            cmbStatusFilter.Items.Clear();

            cmbStatusFilter.Items.Add("All");
            cmbStatusFilter.Items.Add("Pending");
            cmbStatusFilter.Items.Add("Approved");
            cmbStatusFilter.Items.Add("Rejected");

            cmbStatusFilter.SelectedIndex = 0;

            // =========================
            // LOAD DATA
            // =========================
            LoadLeavesFromDB();
            LoadAttendanceFromDB();

            LoadLeavesGrid();
            LoadAttendanceGrid();
        }

        void ApplyRolePermissions()
        {
            txtEmployeeCode.Text =
                CurrentEmployeeCode.ToString();

            txtAttendanceEmployeeCode.Text =
                CurrentEmployeeCode.ToString();

            txtEmployeeCode.ReadOnly = true;
            txtAttendanceEmployeeCode.ReadOnly = true;

            // LEAVE DATE RESTRICTIONS
            dtpFromDate.MinDate =
                DateTime.Today.AddDays(1);

            dtpToDate.MinDate =
                DateTime.Today.AddDays(1);

            // ATTENDANCE DATE
            dtpAttendanceDate.Value =
                DateTime.Today;

            dtpAttendanceDate.MinDate =
                DateTime.Today;

            dtpAttendanceDate.MaxDate =
                DateTime.Today;

            // =========================
            // EMPLOYEE
            // =========================
            if (CurrentUserRole == "Employee")
            {
                btnApprove.Enabled = false;
                btnReject.Enabled = false;

                cmbAttendanceStatus.Items.Clear();
                cmbAttendanceStatus.Items.Add("Present");

                cmbAttendanceStatus.SelectedIndex = 0;
            }

            // =========================
            // HR / ADMIN
            // =========================
            else
            {
                cmbAttendanceStatus.Items.Clear();

                cmbAttendanceStatus.Items.Add("Present");
                cmbAttendanceStatus.Items.Add("Absent");

                cmbAttendanceStatus.SelectedIndex = 0;
            }
        }
        private void btnApplyLeave_Click(
    object sender,
    EventArgs e)
        {
            try
            {
                // NO PAST / PRESENT DATE
                if (dtpFromDate.Value.Date <= DateTime.Today)
                {
                    MessageBox.Show(
                        "Leave must be for future dates only");

                    return;
                }

                if (dtpToDate.Value.Date <
                    dtpFromDate.Value.Date)
                {
                    MessageBox.Show(
                        "Invalid leave dates");

                    return;
                }
                if (cmbLeaveType.SelectedIndex == -1)
                {
                    MessageBox.Show("Select leave type");
                    return;
                }
                ApplyLeaveCommand cmd =
                  new ApplyLeaveCommand(
                      CurrentEmployeeCode,
                      cmbLeaveType.Text,
                       dtpFromDate.Value.Date,
                       dtpToDate.Value.Date);

                UndoRedoManager.Execute(cmd);
                MessageBox.Show(
                    "Leave Applied");

                LoadLeavesFromDB();
                LoadLeavesGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadLeavesFromDB()
        {
            leaveMap.Clear();

            DataTable dt = DBHelper.GetDataTable("SELECT * FROM vw_Leaves");

            foreach (DataRow row in dt.Rows)
            {
                LeaveRecord l = new LeaveRecord
                {
                    LeaveID = Convert.ToInt32(row["LeaveID"]),
                    EmployeeCode = Convert.ToInt32(row["EmployeeCode"]),
                    LeaveType = row["LeaveType"].ToString(),
                    FromDate = Convert.ToDateTime(row["FromDate"]),
                    ToDate = Convert.ToDateTime(row["ToDate"]),
                    Status = row["Status"].ToString()
                };

                if (!leaveMap.ContainsKey(l.EmployeeCode))
                    leaveMap[l.EmployeeCode] = new List<LeaveRecord>();

                leaveMap[l.EmployeeCode].Add(l);
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            ChangeLeaveStatus("Approved");
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            ChangeLeaveStatus("Rejected");
        }

        void ChangeLeaveStatus(string status)
        {
            try
            {
                if (dgvLeaves.SelectedRows.Count == 0)
                {
                    MessageBox.Show(
                        "Select Leave Record");

                    return;
                }

                int leaveId =
                    Convert.ToInt32(
                        dgvLeaves.SelectedRows[0]
                        .Cells["LeaveID"].Value);

                int employeeCode =
                    Convert.ToInt32(
                        dgvLeaves.SelectedRows[0]
                        .Cells["EmployeeCode"].Value);

                // =========================
                // CANNOT APPROVE OWN LEAVE
                // =========================
                if (employeeCode ==
                    CurrentEmployeeCode)
                {
                    MessageBox.Show(
                        "You cannot approve/reject your own leave");

                    return;
                }

                using (SqlConnection con =
                    DBHelper.GetConnection())
                {
                    con.Open();

                    // GET EMPLOYEE ROLE
                    SqlCommand roleCmd =
                        new SqlCommand(
                            "SELECT Role FROM Employees WHERE EmployeeCode=@id",
                            con);

                    roleCmd.Parameters.AddWithValue(
                        "@id",
                        employeeCode);

                    string targetRole =
                        roleCmd.ExecuteScalar().ToString();

                    // =========================
                    // EMPLOYEE LEAVES
                    // =========================
                    if (targetRole == "Employee")
                    {
                        // HR + Admin allowed
                    }

                    // =========================
                    // HR LEAVES
                    // =========================
                    else if (targetRole == "HR")
                    {
                        if (CurrentUserRole != "Admin")
                        {
                            MessageBox.Show(
                                "Only Admin can approve HR leaves");

                            return;
                        }
                    }

                    // =========================
                    // ADMIN LEAVES
                    // =========================
                    else if (targetRole == "Admin")
                    {
                        if (CurrentUserRole != "Admin")
                        {
                            MessageBox.Show(
                                "Only another Admin can approve Admin leave");

                            return;
                        }
                    }

                    UpdateLeaveStatusCommand cmd =
    new UpdateLeaveStatusCommand(
        leaveId,
        status);

                    UndoRedoManager.Execute(cmd);
                }

                MessageBox.Show(
                    "Leave " + status);

                LoadLeavesFromDB();
                LoadLeavesGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnMarkAttendance_Click(
    object sender,
    EventArgs e)
        {
            try
            {
                // ONLY TODAY
                if (dtpAttendanceDate.Value.Date
                    != DateTime.Today)
                {
                    MessageBox.Show(
                        "Attendance can only be marked for today");

                    return;
                }

                int empCode =
                    Convert.ToInt32(
                        txtAttendanceEmployeeCode.Text);

                // EMPLOYEE CAN MARK ONLY SELF
                if (CurrentUserRole == "Employee" &&
                    empCode != CurrentEmployeeCode)
                {
                    MessageBox.Show(
                        "You can only mark your own attendance");

                    return;
                }
                using (SqlConnection checkCon =
    DBHelper.GetConnection())
                {
                    SqlCommand checkCmd =
                        new SqlCommand(
                            @"SELECT COUNT(*)
              FROM Attendance
              WHERE EmployeeCode=@EmployeeCode
              AND CAST(Dated AS DATE)=@AttendanceDate",
                            checkCon);

                    checkCmd.Parameters.AddWithValue(
                        "@EmployeeCode",
                        empCode);

                    checkCmd.Parameters.AddWithValue(
                        "@AttendanceDate",
                        dtpAttendanceDate.Value.Date);

                    checkCon.Open();

                    int count =
                        (int)checkCmd.ExecuteScalar();

                    checkCon.Close();

                    if (count > 0)
                    {
                        MessageBox.Show(
                            "Attendance for today already exists.\nDelete previous attendance first.");

                        return;
                    }
                }

                MarkAttendanceCommand cmd =
    new MarkAttendanceCommand(
        empCode,
        cmbAttendanceStatus.Text,
        dtpAttendanceDate.Value.Date);

                UndoRedoManager.Execute(cmd);

                MessageBox.Show(
                    "Attendance Marked");

                LoadAttendanceFromDB();
                LoadAttendanceGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void LoadAttendanceFromDB()
        {
            attendanceMap.Clear();

            DataTable dt = DBHelper.GetDataTable("SELECT * FROM vw_Attendance");

            foreach (DataRow row in dt.Rows)
            {
                AttendanceRecord a = new AttendanceRecord
                {
                    AttendanceID = Convert.ToInt32(row["AttendanceID"]),
                    EmployeeCode = Convert.ToInt32(row["EmployeeCode"]),
                    Dated = Convert.ToDateTime(row["Dated"]),  // ✅ FIXED
                    Status = row["Status"].ToString()
                };

                if (!attendanceMap.ContainsKey(a.EmployeeCode))
                    attendanceMap[a.EmployeeCode] = new List<AttendanceRecord>();

                attendanceMap[a.EmployeeCode].Add(a);
            }
        }

        void LoadLeavesGrid()
        {
            dgvLeaves.DataSource = null;
            dgvLeaves.DataSource = leaveMap.Values.SelectMany(x => x).ToList();
        }

        void LoadAttendanceGrid()
        {
            dgvAttendance.DataSource = null;
            dgvAttendance.DataSource = attendanceMap.Values.SelectMany(x => x).ToList();
        }

        private void btnFilterLeaves_Click(object sender, EventArgs e)
        {
            string status = cmbStatusFilter.Text;

            var filtered = leaveMap.Values
                .SelectMany(x => x)
                .Where(l => status == "All" || l.Status == status)
                .ToList();

            dgvLeaves.DataSource = filtered;
        }

        private void btnClearLeaveFilter_Click(object sender, EventArgs e)
        {
            LoadLeavesGrid();
        }

        private void btnDeleteAttendance_Click(object sender, EventArgs e)
        {
            if (dgvAttendance.SelectedRows.Count == 0)
            {
                MessageBox.Show("Select attendance record");
                return;
            }

            int attendanceId =
                Convert.ToInt32(
                    dgvAttendance.SelectedRows[0]
                    .Cells["AttendanceID"].Value);

            int employeeCode =
                Convert.ToInt32(
                    dgvAttendance.SelectedRows[0]
                    .Cells["EmployeeCode"].Value);

            // =========================
            // ROLE SECURITY
            // =========================

            // ADMIN -> can delete anyone
            if (CurrentUserRole == "Admin")
            {
                // allowed
            }

            // HR + EMPLOYEE -> only own attendance
            else
            {
                if (employeeCode != CurrentEmployeeCode)
                {
                    MessageBox.Show(
                        "You can delete only your own attendance");

                    return;
                }
            }

            // =========================
            // DELETE
            // =========================
            DBHelper.ExecuteNonQuery(
                "DELETE FROM Attendance WHERE AttendanceID=@id",
                CommandType.Text,
                new SqlParameter("@id", attendanceId)
            );

            MessageBox.Show("Attendance Deleted");

            LoadAttendanceFromDB();
            LoadAttendanceGrid();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            try
            {
                UndoRedoManager.Undo();
                LoadLeavesFromDB();
                LoadAttendanceFromDB();
                LoadLeavesGrid();
                LoadAttendanceGrid();
            }
            catch
            {
                MessageBox.Show("Nothing to Undo");
            }
        }
        private void dgvLeaves_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvLeaves.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString();
                if (status == "Approved")
                    e.CellStyle.BackColor = System.Drawing.Color.LightGreen;
                else if (status == "Rejected")
                    e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
                else if (status == "Pending")
                    e.CellStyle.BackColor = System.Drawing.Color.LightYellow;
            }
        }
        private void btnRedo_Click(object sender, EventArgs e)
        {
            try
            {
                UndoRedoManager.Redo();
                LoadLeavesFromDB();
                LoadAttendanceFromDB();
                LoadLeavesGrid();
                LoadAttendanceGrid();
            }
            catch
            {
                MessageBox.Show("Nothing to Redo");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void dgvLeaves_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvLeaves.Columns[e.ColumnIndex].Name == "Status")
            {
                if (e.Value != null && e.Value.ToString() == "Approved")
                    e.CellStyle.BackColor = System.Drawing.Color.LightGreen;
                else if (e.Value != null && e.Value.ToString() == "Rejected")
                    e.CellStyle.BackColor = System.Drawing.Color.LightCoral;
            }
        }

        private void cmbLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}