using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS
{
    class AssignEmployeeCommand : IUndoableCommand
    {
        int empCode;
        string oldDept;
        string newDept;
        DepartmentForm form;

        public AssignEmployeeCommand(
            int employeeCode,
            string oldDeptCode,
            string newDeptCode,
            DepartmentForm f)
        {
            empCode = employeeCode;
            oldDept = oldDeptCode;
            newDept = newDeptCode;
            form = f;
        }

        public void Execute()
        {
            form.AssignEmployee(empCode, newDept);
        }

        public void Undo()
        {
            form.AssignEmployee(empCode, oldDept);
        }
    }
}
