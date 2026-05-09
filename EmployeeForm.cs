using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EMS
{
    public partial class EmployeeForm : Form
    {
        int CurrentEmployeeCode;
        string CurrentUserRole;
        AVLTree tree = new AVLTree();
        Dictionary<int, Employee> employeeMap =
            new Dictionary<int, Employee>();


        public EmployeeForm(int empCode, string role)
        {
            InitializeComponent();

            CurrentEmployeeCode = empCode;
            CurrentUserRole = role;

            txtPassword.UseSystemPasswordChar = true;
        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            LoadEmployees();
            LoadDepartments();
            LoadFilterOptions();
            ApplyRolePermissions();
            txtFilterName.Text = "";

cmbFilterRole.SelectedIndex = -1;
cmbFilterDepartment.SelectedIndex = -1;

            // Prevent user deleting rows manually
            dgvEmployees.AllowUserToDeleteRows = false;

            // Read only grid
            dgvEmployees.ReadOnly = true;

            // Full row select
            dgvEmployees.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

        }

        // =========================
        // ROLE SECURITY
        // =========================
        // =========================
        // ROLE SECURITY
        // =========================
        void ApplyRolePermissions()
        {
            // =========================
            // EMPLOYEE
            // =========================
            if (CurrentUserRole == "Employee")
            {
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;

                txtPassword.Enabled = false;

                // Employee cannot edit others
                dgvEmployees.ReadOnly = true;
            }

            // =========================
            // HR
            // =========================
            else if (CurrentUserRole == "HR")
            {
                // HR can ADD only
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
            }

            // =========================
            // ADMIN
            // =========================
            else if (CurrentUserRole == "Admin")
            {
                // Full access
                btnAdd.Enabled = true;
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        // =========================
        // LOAD EMPLOYEES
        // =========================
        // =========================
        // LOAD EMPLOYEES
        // =========================
        void LoadEmployees()
        {
            try
            {
                employeeMap.Clear();
                tree.Root = null;

                using (SqlConnection con = DBHelper.GetConnection())
                {
                    SqlDataAdapter da = new SqlDataAdapter(
                        "SELECT * FROM vw_EmployeeDetails",
                        con);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dgvEmployees.DataSource = dt;
                    // Hide Password column for non-admin users
                    // Hide password column for non-admins
                    if (CurrentUserRole != "Admin")
                    {
                        if (dgvEmployees.Columns.Contains("Password"))
                        {
                            dgvEmployees.Columns["Password"].Visible = false;
                        }
                    }
                    // BUILD AVL TREE
                    foreach (DataRow row in dt.Rows)
                    {
                        Employee emp = new Employee();

                        emp.EmployeeCode =
                            Convert.ToInt32(row["EmployeeCode"]);

                        emp.EmployeeName =
                            row["EmployeeName"].ToString();

                        emp.DepartmentCode = 0;

                        emp.Salary =
                            Convert.ToDouble(row["Salary"]);

                        emp.Role =
                            row["Role"].ToString();

                        employeeMap[emp.EmployeeCode] = emp;

                        tree.Root = tree.Insert(tree.Root, emp);
                    }

                    LoadTreeGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Load Error:\n" + ex.Message);
            }
        }
        void ApplyFilters()
        {
            try
            {
                using (SqlConnection con =
                    DBHelper.GetConnection())
                {
                    string query =
                        @"SELECT *
                  FROM vw_EmployeeDetails
                  WHERE 1=1";

                    SqlCommand cmd =
                        new SqlCommand();

                    // NAME FILTER
                    if (!string.IsNullOrWhiteSpace(txtFilterName.Text))
                    {
                        query +=
                            " AND EmployeeName LIKE @name";

                        cmd.Parameters.AddWithValue(
                            "@name",
                            "%" + txtFilterName.Text + "%");
                    }

                    // ROLE FILTER
                    if (cmbFilterRole.SelectedIndex > 0)
                    {
                        query +=
                            " AND Role=@role";

                        cmd.Parameters.AddWithValue(
                            "@role",
                            cmbFilterRole.Text);
                    }

                    // DEPARTMENT FILTER
                    if (cmbFilterDepartment.SelectedIndex > 0)
                    {
                        query +=
                            " AND DepartmentName=@dept";

                        cmd.Parameters.AddWithValue(
                            "@dept",
                            cmbFilterDepartment.Text);
                    }

                    cmd.CommandText = query;
                    cmd.Connection = con;

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    DataTable dt =
                        new DataTable();

                    da.Fill(dt);

                    dgvEmployees.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void LoadFilterOptions()
        {
            cmbFilterRole.Items.Clear();
            cmbFilterDepartment.Items.Clear();

            cmbFilterRole.Items.Add("All");
            cmbFilterDepartment.Items.Add("All");

            cmbFilterRole.Items.Add("Admin");
            cmbFilterRole.Items.Add("HR");
            cmbFilterRole.Items.Add("Employee");

            using (SqlConnection con =
                DBHelper.GetConnection())
            {
                SqlCommand cmd =
                    new SqlCommand(
                        "SELECT DepartmentName FROM Departments",
                        con);

                con.Open();

                SqlDataReader dr =
                    cmd.ExecuteReader();

                while (dr.Read())
                {
                    cmbFilterDepartment.Items.Add(
                        dr["DepartmentName"].ToString());
                }
            }

            cmbFilterRole.SelectedIndex = 0;
            cmbFilterDepartment.SelectedIndex = 0;
        }
        // =========================
        // AVL TREE GRID
        // =========================
        void LoadTreeGrid()
        {
            treeEmployees.Nodes.Clear();

            AddNodes(tree.Root, null);

            treeEmployees.ExpandAll();
        }
        void AddNodes(AVLNode node, TreeNode parentNode)
        {
            if (node == null)
                return;

            TreeNode newNode = new TreeNode(
                node.Data.EmployeeCode +
                " - " +
                node.Data.EmployeeName);

            if (parentNode == null)
                treeEmployees.Nodes.Add(newNode);
            else
                parentNode.Nodes.Add(newNode);

            AddNodes(node.Left, newNode);
            AddNodes(node.Right, newNode);
        }

        // =========================
        // LOAD DEPARTMENTS
        // =========================
        void LoadDepartments()
        {
            try
            {
                cmbDepartment.Items.Clear();

                // FILTER COMBOBOXES
                cmbFilterDepartment.Items.Clear();

                cmbFilterRole.Items.Clear();
                cmbFilterRole.Items.Add("Admin");
                cmbFilterRole.Items.Add("HR");
                cmbFilterRole.Items.Add("Employee");

                using (SqlConnection con = DBHelper.GetConnection())
                {
                    SqlCommand cmd = new SqlCommand(
                        "SELECT DepartmentCode, DepartmentName FROM Departments",
                        con);

                    con.Open();

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        string deptName = dr["DepartmentName"].ToString();

                        // MAIN DEPARTMENT COMBO
                        cmbDepartment.Items.Add(
                            dr["DepartmentCode"] + " - " + deptName);

                        // FILTER DEPARTMENT COMBO
                        cmbFilterDepartment.Items.Add(deptName);
                    }

                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // =========================
        // ADD EMPLOYEE
        // =========================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            try
            {
                int deptCode =
                    int.Parse(
                        cmbDepartment.Text.Split('-')[0].Trim());

                Employee emp = new Employee()
                {
                    EmployeeCode = int.Parse(txtID.Text),
                    EmployeeName = txtName.Text,
                    DepartmentCode = deptCode,
                    Salary = Convert.ToDouble(txtSalary.Text),
                    Role = cmbRole.Text,
                    Password =
                        string.IsNullOrWhiteSpace(txtPassword.Text)
                        ? "123"
                        : txtPassword.Text
                };

                using (SqlConnection con =
                    DBHelper.GetConnection())
                {
                    AddEmployeeCommand cmd =
                        new AddEmployeeCommand(
                            emp,
                            con);

                    UndoRedoManager.Execute(cmd);
                }

                MessageBox.Show(
                    "Employee Added Successfully");

                ClearFields();

                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Add Failed:\n" + ex.Message);
            }
        }

        // =========================
        // UPDATE EMPLOYEE
        // =========================
        private void btnUpdate_Click(
    object sender,
    EventArgs e)
        {
            if (CurrentUserRole != "Admin")
            {
                MessageBox.Show(
                    "Only Admin can update");
                return;
            }

            if (!ValidateInputs())
                return;

            try
            {
                int deptCode =
                    int.Parse(
                        cmbDepartment.Text.Split('-')[0].Trim());

                Employee newEmp = new Employee()
                {
                    EmployeeCode =
                        int.Parse(txtID.Text),

                    EmployeeName =
                        txtName.Text,

                    DepartmentCode =
                        deptCode,

                    Salary =
                        Convert.ToDouble(txtSalary.Text),

                    Role =
                        cmbRole.Text,

                    Password =
                        txtPassword.Text
                };

                Employee oldEmp =
                    employeeMap[newEmp.EmployeeCode];

                using (SqlConnection con =
                    DBHelper.GetConnection())
                {
                    UpdateEmployeeCommand cmd =
                        new UpdateEmployeeCommand(
                            oldEmp,
                            newEmp,
                            employeeMap,
                            tree,
                            con);

                    UndoRedoManager.Execute(cmd);
                }

                MessageBox.Show(
                    "Employee Updated");

                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Update Failed:\n" + ex.Message);
            }
        }

        // UPDATE PASSWORD
        void UpdatePassword()
        {
            using (SqlConnection con = DBHelper.GetConnection())
            {
                SqlCommand cmd = new SqlCommand(
                    "UPDATE Employees SET Password=@Password WHERE EmployeeCode=@EmployeeCode", con);

                cmd.Parameters.AddWithValue("@Password", txtPassword.Text);
                cmd.Parameters.AddWithValue("@EmployeeCode", int.Parse(txtID.Text));

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // DELETE EMPLOYEE
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CurrentUserRole != "Admin")
            {
                MessageBox.Show(
                    "Only Admin can delete");
                return;
            }

            if (txtID.Text == "")
            {
                MessageBox.Show(
                    "Select employee first");
                return;
            }

            try
            {
                Employee emp = new Employee()
                {
                    EmployeeCode =
                        int.Parse(txtID.Text),

                    EmployeeName =
                        txtName.Text,

                    Salary =
                        Convert.ToDouble(txtSalary.Text),

                    Role =
                        cmbRole.Text,

                    Password =
                        txtPassword.Text
                };

                using (SqlConnection con =
                    DBHelper.GetConnection())
                {
                    DeleteEmployeeCommand cmd =
                        new DeleteEmployeeCommand(
                            emp,
                            employeeMap,
                            tree,
                            con);

                    UndoRedoManager.Execute(cmd);
                }

                MessageBox.Show(
                    "Employee Deleted");

                ClearFields();

                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Delete Failed:\n" + ex.Message);
            }
        }

        private void btnSearch_Click(
    object sender,
    EventArgs e)
        {
            try
            {
                using (SqlConnection con =
                    DBHelper.GetConnection())
                {
                    string query =
                        "SELECT * FROM vw_EmployeeDetails";

                    SqlCommand cmd =
                        new SqlCommand();

                    if (txtID.Text.Trim() != "")
                    {
                        query +=
                            " WHERE EmployeeCode=@code";

                        cmd.Parameters.AddWithValue(
                            "@code",
                            int.Parse(txtID.Text));
                    }

                    cmd.CommandText = query;
                    cmd.Connection = con;

                    SqlDataAdapter da =
                        new SqlDataAdapter(cmd);

                    DataTable dt =
                        new DataTable();

                    da.Fill(dt);

                    dgvEmployees.DataSource = dt;
                }
            }
            catch
            {
                MessageBox.Show(
                    "Invalid Employee Code");
            }
        }

        // GRID CLICK
        private void dgvEmployees_CellClick(
            object sender,
            DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    DataGridViewRow row =
                        dgvEmployees.Rows[e.RowIndex];

                    txtID.Text =
                        row.Cells["EmployeeCode"].Value.ToString();

                    txtName.Text =
                        row.Cells["EmployeeName"].Value.ToString();

                    txtSalary.Text =
                        row.Cells["Salary"].Value.ToString();

                    cmbRole.Text =
                        row.Cells["Role"].Value.ToString();

                    string dept =
                        row.Cells["DepartmentName"].Value.ToString();

                    for (int i = 0; i < cmbDepartment.Items.Count; i++)
                    {
                        if (cmbDepartment.Items[i]
                            .ToString()
                            .Contains(dept))
                        {
                            cmbDepartment.SelectedIndex = i;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            // Only Admin can view passwords
            if (CurrentUserRole == "Admin")
            {
                try
                {
                    using (SqlConnection con = DBHelper.GetConnection())
                    {
                        SqlCommand cmd = new SqlCommand(
                            "SELECT Password FROM Employees WHERE EmployeeCode=@id",
                            con);

                        cmd.Parameters.AddWithValue(
                            "@id",
                            txtID.Text);

                        con.Open();

                        object result = cmd.ExecuteScalar();

                        txtPassword.Text =
                            result == null ? "" : result.ToString();
                    }
                }
                catch
                {
                    txtPassword.Text = "";
                }
            }
            else
            {
                txtPassword.Text = "Hidden";
            }
        }

        // VALIDATION
        bool ValidateInputs()
        {
            if (txtID.Text == "" || txtName.Text == "" ||
                txtSalary.Text == "" || cmbDepartment.SelectedIndex == -1 ||
                cmbRole.SelectedIndex == -1)
            {
                MessageBox.Show("Fill all fields");
                return false;
            }

            if (!int.TryParse(txtID.Text, out _))
            {
                MessageBox.Show("Invalid Employee Code");
                return false;
            }

            if (!float.TryParse(txtSalary.Text, out _))
            {
                MessageBox.Show("Invalid Salary");
                return false;
            }

            return true;
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = DBHelper.GetConnection())
                {
                    string query =
                        @"SELECT EmployeeCode,
                         EmployeeName,
                         DepartmentName,
                         Salary,
                         Role
                  FROM vw_EmployeeDetails
                  WHERE 1=1";

                    SqlCommand cmd = new SqlCommand();

                    // NAME FILTER
                    if (!string.IsNullOrWhiteSpace(txtFilterName.Text))
                    {
                        query += " AND EmployeeName LIKE @name";

                        cmd.Parameters.AddWithValue(
                            "@name",
                            "%" + txtFilterName.Text.Trim() + "%");
                    }

                    // ROLE FILTER
                    if (cmbFilterRole.SelectedIndex > 0)
                    {
                        query += " AND Role=@role";

                        cmd.Parameters.AddWithValue(
                            "@role",
                            cmbFilterRole.Text);
                    }

                    // DEPARTMENT FILTER
                    if (cmbFilterDepartment.SelectedIndex > 0)
                    {
                        query += " AND DepartmentName=@dept";

                        cmd.Parameters.AddWithValue(
                            "@dept",
                            cmbFilterDepartment.Text);
                    }

                    cmd.CommandText = query;
                    cmd.Connection = con;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();

                    da.Fill(dt);

                    dgvEmployees.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        MessageBox.Show(
                            "No employee found",
                            "Filter",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClearFilter_Click( object sender,    EventArgs e)
        {
            ClearFields();
            LoadEmployees();
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            try
            {
                UndoRedoManager.Undo();
                LoadEmployees();
            }
            catch
            {
                MessageBox.Show("Nothing to undo");
            }
        }

        private void btnRedo_Click(object sender, EventArgs e)
        {
            try
            {
                UndoRedoManager.Redo();
                LoadEmployees();
            }
            catch
            {
                MessageBox.Show("Nothing to redo");
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        private void dgvEmployees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        // =========================
        // CLEAR
        // =========================
        void ClearFields()
        {
            txtID.Clear();
            txtName.Clear();
            txtSalary.Clear();
            txtPassword.Clear();

            cmbDepartment.SelectedIndex = -1;
            cmbRole.SelectedIndex = -1;
        }

        private void txtFilterName_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void cmbFilterRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void cmbFilterDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }
    }
}