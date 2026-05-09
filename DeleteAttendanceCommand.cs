using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EMS
{
    class DeleteAttendanceCommand : IUndoableCommand
    {
        AttendanceRecord attendance;
        Dictionary<int, List<AttendanceRecord>> map;

        public DeleteAttendanceCommand(
            AttendanceRecord att,
            Dictionary<int, List<AttendanceRecord>> attendanceMap)
        {
            attendance = att;
            map = attendanceMap;
        }

        public void Execute()
        {
            map[attendance.EmployeeCode]
                .RemoveAll(a => a.AttendanceID == attendance.AttendanceID);

            DBHelper.ExecuteNonQuery(
                "DELETE FROM Attendance WHERE AttendanceID=@id",
                CommandType.Text,
                new SqlParameter("@id", attendance.AttendanceID)
            );
        }

        public void Undo()
        {
            if (!map.ContainsKey(attendance.EmployeeCode))
                map[attendance.EmployeeCode] = new List<AttendanceRecord>();

            map[attendance.EmployeeCode].Add(attendance);

            DBHelper.ExecuteNonQuery(
                "INSERT INTO Attendance (EmployeeCode, Date, Status) VALUES (@code,@date,@status)",
                CommandType.Text,
                new SqlParameter("@code", attendance.EmployeeCode),
                new SqlParameter("@date", attendance.Date),
                new SqlParameter("@status", attendance.Status)
            );
        }
    }
}