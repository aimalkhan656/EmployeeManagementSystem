using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EMS
{
    public class DeleteEmployeeCommand : IUndoableCommand
    {
        Employee emp;
        SqlConnection con;

        public DeleteEmployeeCommand(
            Employee e,
            Dictionary<int, Employee> map,
            AVLTree treeObj,
            SqlConnection connection)
        {
            emp = e;
            con = connection;
        }

        // =========================
        // DELETE
        // =========================
        public void Execute()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Open();

                SqlTransaction transaction =
                    con.BeginTransaction();

                // DELETE PAYROLL
                SqlCommand payrollCmd =
                    new SqlCommand(
                        "DELETE FROM Payroll WHERE EmployeeCode=@id",
                        con,
                        transaction);

                payrollCmd.Parameters.AddWithValue(
                    "@id",
                    emp.EmployeeCode);

                payrollCmd.ExecuteNonQuery();

                // DELETE ATTENDANCE
                SqlCommand attendanceCmd =
                    new SqlCommand(
                        "DELETE FROM Attendance WHERE EmployeeCode=@id",
                        con,
                        transaction);

                attendanceCmd.Parameters.AddWithValue(
                    "@id",
                    emp.EmployeeCode);

                attendanceCmd.ExecuteNonQuery();

                // DELETE LEAVES
                SqlCommand leaveCmd =
                    new SqlCommand(
                        "DELETE FROM Leaves WHERE EmployeeCode=@id",
                        con,
                        transaction);

                leaveCmd.Parameters.AddWithValue(
                    "@id",
                    emp.EmployeeCode);

                leaveCmd.ExecuteNonQuery();

                // DELETE EMPLOYEE
                SqlCommand empCmd =
                    new SqlCommand(
                        "DELETE FROM Employees WHERE EmployeeCode=@id",
                        con,
                        transaction);

                empCmd.Parameters.AddWithValue(
                    "@id",
                    emp.EmployeeCode);

                empCmd.ExecuteNonQuery();

                transaction.Commit();
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
            }
        }

        // =========================
        // UNDO
        // =========================
        public void Undo()
        {
            try
            {
                if (con.State == ConnectionState.Open)
                    con.Close();

                con.Open();

                SqlCommand cmd =
                    new SqlCommand(
                        @"INSERT INTO Employees
                        (
                            EmployeeCode,
                            EmployeeName,
                            DepartmentCode,
                            Salary,
                            Role,
                            Password
                        )
                        VALUES
                        (
                            @EmployeeCode,
                            @EmployeeName,
                            @DepartmentCode,
                            @Salary,
                            @Role,
                            @Password
                        )",
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

                cmd.ExecuteNonQuery();
            }
            finally
            {
                con.Close();
            }
        }
    }
}