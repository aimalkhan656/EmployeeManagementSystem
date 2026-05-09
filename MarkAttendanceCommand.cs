using System;
using System.Data;
using System.Data.SqlClient;

namespace EMS
{
    public class MarkAttendanceCommand : IUndoableCommand
    {
        int employeeCode;
        string status;
        DateTime attendanceDate;

        public MarkAttendanceCommand(
            int empCode,
            string stat,
            DateTime date)
        {
            employeeCode = empCode;
            status = stat;
            attendanceDate = date;
        }

        public void Execute()
        {
            DBHelper.ExecuteNonQuery(
                "sp_MarkAttendance",
                CommandType.StoredProcedure,

                new SqlParameter("@EmployeeCode", employeeCode),
                new SqlParameter("@AttendanceDate", attendanceDate),
                new SqlParameter("@Status", status)
            );
        }

        public void Undo()
        {
            DBHelper.ExecuteNonQuery(
                @"DELETE FROM Attendance
                  WHERE EmployeeCode=@EmployeeCode
                  AND Dated=@Date",

                CommandType.Text,

                new SqlParameter("@EmployeeCode", employeeCode),
                new SqlParameter("@Date", attendanceDate)
            );
        }
    }
}