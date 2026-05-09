using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace EMS
{
    class UpdatePayrollCommand : IUndoableCommand
    {
        Payroll oldP, newP;
        Dictionary<int, Payroll> map;

        public UpdatePayrollCommand(Payroll oldP, Payroll newP, Dictionary<int, Payroll> map)
        {
            this.oldP = oldP;
            this.newP = newP;
            this.map = map;
        }

        public void Execute()
        {
            map[newP.EmployeeCode] = newP;

            DBHelper.ExecuteNonQuery(
                "sp_UpdatePayroll",
                CommandType.StoredProcedure,
                new SqlParameter("@EmployeeCode", newP.EmployeeCode),
                new SqlParameter("@BasicSalary", newP.BasicSalary),
                new SqlParameter("@Allowances", newP.Allowances),
                new SqlParameter("@Deductions", newP.Deductions)
            );
        }

        public void Undo()
        {
            map[oldP.EmployeeCode] = oldP;

            DBHelper.ExecuteNonQuery(
                "sp_UpdatePayroll",
                CommandType.StoredProcedure,
                new SqlParameter("@EmployeeCode", oldP.EmployeeCode),
                new SqlParameter("@BasicSalary", oldP.BasicSalary),
                new SqlParameter("@Allowances", oldP.Allowances),
                new SqlParameter("@Deductions", oldP.Deductions)
            );
        }
    }
}