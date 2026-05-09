using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EMS
{
    class AddAttendanceCommand : IUndoableCommand
    {
        AttendanceRecord att;
        Dictionary<int, List<AttendanceRecord>> map;

        public AddAttendanceCommand(
            AttendanceRecord attendance,
            Dictionary<int, List<AttendanceRecord>> attendanceMap)
        {
            att = attendance;
            map = attendanceMap;
        }

        public void Execute()
        {
            if (!map.ContainsKey(att.EmployeeCode))
                map[att.EmployeeCode] = new List<AttendanceRecord>();

            map[att.EmployeeCode].Add(att);

            DBHelper.ExecuteNonQuery(
                "INSERT INTO Attendance (EmployeeCode, Date, Status) VALUES (@code,@date,@status)",
                CommandType.Text,
                new SqlParameter("@code", att.EmployeeCode),
                new SqlParameter("@date", att.Date),
                new SqlParameter("@status", att.Status)
            );
        }

        public void Undo()
        {
            map[att.EmployeeCode].Remove(att);

            DBHelper.ExecuteNonQuery(
                "DELETE FROM Attendance WHERE EmployeeCode=@code AND Date=@date",
                CommandType.Text,
                new SqlParameter("@code", att.EmployeeCode),
                new SqlParameter("@date", att.Date)
            );
        }
    }
}