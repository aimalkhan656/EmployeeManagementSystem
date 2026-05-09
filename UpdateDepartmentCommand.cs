using System.Collections.Generic;

namespace EMS
{
    class UpdateDepartmentCommand : IUndoableCommand
    {
        Dictionary<int, Department> deptMap;
        Department oldDept;
        Department newDept;
        DepartmentForm form;

        public UpdateDepartmentCommand(
            Dictionary<int, Department> map,
            Department oldD,
            Department newD,
            DepartmentForm f)
        {
            deptMap = map;
            oldDept = oldD;
            newDept = newD;
            form = f;
        }

        public void Execute()
        {
            deptMap[newDept.DepartmentCode] = newDept;
            form.UpdateInDatabase(newDept.DepartmentCode.ToString(), newDept.DepartmentName, newDept.Description);
        }

        public void Undo()
        {
            deptMap[oldDept.DepartmentCode] = oldDept;
            form.UpdateInDatabase(oldDept.DepartmentCode.ToString(), oldDept.DepartmentName, oldDept.Description);
        }
    }
}
