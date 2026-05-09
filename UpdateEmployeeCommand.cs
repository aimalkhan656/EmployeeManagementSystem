using System.Collections.Generic;
using System.Data.SqlClient;

namespace EMS
{
    public class UpdateEmployeeCommand : IUndoableCommand
    {
        Employee oldEmp;
        Employee newEmp;

        Dictionary<int, Employee> map;

        AVLTree tree;

        SqlConnection con;

        public UpdateEmployeeCommand(
            Employee oldEmployee,
            Employee newEmployee,
            Dictionary<int, Employee> employeeMap,
            AVLTree avlTree,
            SqlConnection connection)
        {
            oldEmp = oldEmployee;
            newEmp = newEmployee;

            map = employeeMap;
            tree = avlTree;
            con = connection;
        }

        public void Execute()
        {
            map[newEmp.EmployeeCode] = newEmp;

            RebuildTree();

            UpdateDB(newEmp);
        }

        public void Undo()
        {
            map[oldEmp.EmployeeCode] = oldEmp;

            RebuildTree();

            UpdateDB(oldEmp);
        }

        void RebuildTree()
        {
            tree.Root = null;

            foreach (Employee e in map.Values)
            {
                tree.Root =
                    tree.Insert(tree.Root, e);
            }
        }

        void UpdateDB(Employee emp)
        {
            SqlCommand cmd =
                new SqlCommand(
                    @"UPDATE Employees
                      SET EmployeeName=@EmployeeName,
                          DepartmentCode=@DepartmentCode,
                          Salary=@Salary,
                          Role=@Role,
                          Password=@Password
                      WHERE EmployeeCode=@EmployeeCode",
                    con);

            cmd.Parameters.AddWithValue(
                "@EmployeeCode",
                emp.EmployeeCode);

            cmd.Parameters.AddWithValue(
                "@EmployeeName",
                emp.EmployeeName);

            cmd.Parameters.AddWithValue(
                "@DepartmentCode",
                emp.DepartmentCode);

            cmd.Parameters.AddWithValue(
                "@Salary",
                emp.Salary);

            cmd.Parameters.AddWithValue(
                "@Role",
                emp.Role);

            cmd.Parameters.AddWithValue(
                "@Password",
                emp.Password);

            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();
        }
    }
}