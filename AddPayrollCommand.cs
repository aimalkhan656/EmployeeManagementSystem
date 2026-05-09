using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EMS
{
    class AddPayrollCommand : IUndoableCommand
    {
        Payroll payroll;
        Dictionary<int, Payroll> map;

        public AddPayrollCommand(Payroll p, Dictionary<int, Payroll> m)
        {
            payroll = p;
            map = m;
        }

        public void Execute()
        {
            map[payroll.EmployeeCode] = payroll;

            DBHelper.ExecuteNonQuery(
                "sp_InsertPayroll",
                CommandType.StoredProcedure,
                new SqlParameter("@EmployeeCode", payroll.EmployeeCode),
                new SqlParameter("@BasicSalary", payroll.BasicSalary),
                new SqlParameter("@Allowances", payroll.Allowances),
                new SqlParameter("@Deductions", payroll.Deductions)
            );
        }

        public void Undo()
        {
            map.Remove(payroll.EmployeeCode);

            DBHelper.ExecuteNonQuery(
                "sp_DeletePayroll",
                CommandType.StoredProcedure,
                new SqlParameter("@EmployeeCode", payroll.EmployeeCode)
            );
        }
    }
}