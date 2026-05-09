using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EMS
{
    public partial class DepartmentForm : Form
    {
        int CurrentEmployeeCode;
        string CurrentUserRole;

        public DepartmentForm(int empCode, string role)
        {
            InitializeComponent();
            CurrentEmployeeCode = empCode;
            CurrentUserRole = role;
        }

        private void DepartmentForm_Load(object sender, EventArgs e)
        {
            LoadDepartments();
            LoadEmployees();
            ApplyRolePermissions();
        }

        // =========================
        // ROLE SECURITY
        // =========================
        void ApplyRolePermissions()
        {
            if (CurrentUserRole == "Employee")
            {
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                btnAssignEmployee.Enabled = false;
            }
        }

        // =========================
        // LOAD DEPARTMENTS
        // =========================
        void LoadDepartments()
        {
            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT * FROM vw_DepartmentDetails", con);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvDepartments.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =========================
        // LOAD EMPLOYEES
        // =========================
        void LoadEmployees()
        {
            try
            {
                cmbEmployees.Items.Clear();

                using (SqlConnection con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT EmployeeCode, EmployeeName FROM Employees",
                        con);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        cmbEmployees.Items.Add(
                            dr["EmployeeCode"] + " - " + dr["EmployeeName"]);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =========================
        // ADD
        // =========================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            InsertIntoDatabase(
                txtDeptCode.Text,
                txtDeptName.Text,
                txtDescription.Text);

            MessageBox.Show("Department Added");
            LoadDepartments();
        }

        // =========================
        // UPDATE
        // =========================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            UpdateInDatabase(
                txtDeptCode.Text,
                txtDeptName.Text,
                txtDescription.Text);

            MessageBox.Show("Department Updated");
            LoadDepartments();
        }

        // =========================
        // DELETE
        // =========================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int deptCode = int.Parse(txtDeptCode.Text);

                using (SqlConnection con = DBHelper.GetConnection())
                {
                    SqlCommand funcCmd = new SqlCommand(
                        "SELECT dbo.fn_DepartmentEmployeeCount(@code)", con);

                    funcCmd.Parameters.AddWithValue("@code", deptCode);

                    con.Open();

                    int count = (int)funcCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Cannot delete department with employees");
                        return;
                    }
                }

                DeleteFromDatabase(txtDeptCode.Text);

                MessageBox.Show("Department Deleted");
                LoadDepartments();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =========================
        // ASSIGN EMPLOYEE
        // =========================
        private void btnAssignEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                int empCode = int.Parse(
                    cmbEmployees.Text.Split('-')[0].Trim());

                AssignEmployee(empCode, txtDeptCode.Text);

                MessageBox.Show("Employee Assigned");

                LoadDepartments();
                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =========================
        // SEARCH
        // =========================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    SqlDataAdapter da = new SqlDataAdapter(
                        @"SELECT * FROM vw_DepartmentDetails
                          WHERE DepartmentCode=@code",
                        con);

                    da.SelectCommand.Parameters.AddWithValue(
                        "@code", int.Parse(txtDeptCode.Text));

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvDepartments.DataSource = dt;
                }
            }
            catch
            {
                MessageBox.Show("Invalid Department Code");
            }
        }

        // =========================
        // GRID CLICK
        // =========================
        private void dgvDepartments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDepartments.Rows[e.RowIndex];

                txtDeptCode.Text = row.Cells["DepartmentCode"].Value.ToString();
                txtDeptName.Text = row.Cells["DepartmentName"].Value.ToString();
                txtDescription.Text = row.Cells["Description"].Value.ToString();
            }
        }

        // =========================
        // VALIDATION
        // =========================
        bool ValidateInputs()
        {
            if (txtDeptCode.Text == "" || txtDeptName.Text == "")
            {
                MessageBox.Show("Fill required fields");
                return false;
            }

            if (!int.TryParse(txtDeptCode.Text, out _))
            {
                MessageBox.Show("Invalid Department Code");
                return false;
            }

            return true;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        // =========================
        // UNDO
        // =========================
        private void btnUndo_Click(object sender, EventArgs e)
        {
            try
            {
                UndoRedoManager.Undo();
                LoadDepartments();
            }
            catch
            {
                MessageBox.Show("Nothing to undo");
            }
        }

        // =========================
        // REDO
        // =========================
        private void btnRedo_Click(object sender, EventArgs e)
        {
            try
            {
                UndoRedoManager.Redo();
                LoadDepartments();
            }
            catch
            {
                MessageBox.Show("Nothing to redo");
            }
        }

        // =========================
        // DATABASE METHODS
        // =========================
        public void InsertIntoDatabase(string code, string name, string desc)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_AddDepartment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DepartmentCode", int.Parse(code));
                cmd.Parameters.AddWithValue("@DepartmentName", name);
                cmd.Parameters.AddWithValue("@Description", desc);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateInDatabase(string code, string name, string desc)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_UpdateDepartment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DepartmentCode", int.Parse(code));
                cmd.Parameters.AddWithValue("@DepartmentName", name);
                cmd.Parameters.AddWithValue("@Description", desc);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteFromDatabase(string code)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                SqlCommand cmd = new SqlCommand("sp_DeleteDepartment", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@DepartmentCode", int.Parse(code));

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void AssignEmployee(int empCode, string deptCode)
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Employees SET DepartmentCode=@dept WHERE EmployeeCode=@emp",
                    con);

                cmd.Parameters.AddWithValue("@dept", int.Parse(deptCode));
                cmd.Parameters.AddWithValue("@emp", empCode);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}