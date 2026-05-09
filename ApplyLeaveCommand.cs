using System;
using System.Data;
using System.Data.SqlClient;

namespace EMS
{
    public class ApplyLeaveCommand : IUndoableCommand
    {
        int employeeCode;
        string leaveType;
        DateTime fromDate;
        DateTime toDate;

        public ApplyLeaveCommand(
            int empCode,
            string type,
            DateTime from,
            DateTime to)
        {
            employeeCode = empCode;
            leaveType = type;
            fromDate = from;
            toDate = to;
        }

        public void Execute()
        {
            DBHelper.ExecuteNonQuery(
                "sp_ApplyLeave",
                CommandType.StoredProcedure,

                new SqlParameter("@EmployeeCode", employeeCode),
                new SqlParameter("@LeaveType", leaveType),
                new SqlParameter("@FromDate", fromDate),
                new SqlParameter("@ToDate", toDate)
            );
        }

        public void Undo()
        {
            DBHelper.ExecuteNonQuery(
                @"DELETE TOP(1)
                  FROM Leaves
                  WHERE EmployeeCode=@EmployeeCode
                  ORDER BY LeaveID DESC",

                CommandType.Text,

                new SqlParameter("@EmployeeCode", employeeCode)
            );
        }
    }
}