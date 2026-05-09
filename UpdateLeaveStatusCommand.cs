using System.Data;
using System.Data.SqlClient;

namespace EMS
{
    public class UpdateLeaveStatusCommand : IUndoableCommand
    {
        int leaveId;
        string newStatus;
        string oldStatus;

        public UpdateLeaveStatusCommand(
            int id,
            string status)
        {
            leaveId = id;
            newStatus = status;

            object result =
                DBHelper.ExecuteScalar(
                    "SELECT Status FROM Leaves WHERE LeaveID=@id",
                    new SqlParameter("@id", leaveId));

            oldStatus = result.ToString();
        }

        public void Execute()
        {
            DBHelper.ExecuteNonQuery(
                "sp_UpdateLeaveStatus",
                CommandType.StoredProcedure,

                new SqlParameter("@LeaveID", leaveId),
                new SqlParameter("@Status", newStatus)
            );
        }

        public void Undo()
        {
            DBHelper.ExecuteNonQuery(
                "sp_UpdateLeaveStatus",
                CommandType.StoredProcedure,

                new SqlParameter("@LeaveID", leaveId),
                new SqlParameter("@Status", oldStatus)
            );
        }
    }
}