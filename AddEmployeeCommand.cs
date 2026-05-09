using System.Data;
using System.Data.SqlClient;

namespace EMS
{
    public class AddEmployeeCommand : IUndoableCommand
    {
        private Employee emp;
        private SqlConnection con;

        public AddEmployeeCommand(Employee e, SqlConnection connection)
        {
            emp = e;
            con = connection;
        }

        public void Execute()
        {
            
            // USING STORED PROCEDURE (DBMS CONCEPT)
            
            SqlCommand cmd = new SqlCommand("sp_AddEmployee", con);

            cmd.CommandType = CommandType.StoredProcedure;

            // PARAMETERS (SAFE + SQL STANDARD)
            cmd.Parameters.AddWithValue("@EmployeeCode", emp.EmployeeCode);
            cmd.Parameters.AddWithValue("@EmployeeName", emp.EmployeeName);
            cmd.Parameters.AddWithValue("@DepartmentCode", emp.DepartmentCode);
            cmd.Parameters.AddWithValue("@Salary", emp.Salary);
            cmd.Parameters.AddWithValue("@Role", emp.Role);
            cmd.Parameters.AddWithValue("@Password", emp.Password);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void Undo()
        {
            // USING STORED PROCEDURE FOR DELETE
            
            SqlCommand cmd = new SqlCommand("sp_DeleteEmployee", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmployeeCode", emp.EmployeeCode);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }
    }

}