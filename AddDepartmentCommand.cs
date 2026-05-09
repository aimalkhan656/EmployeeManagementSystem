using System.Collections.Generic;

namespace EMS
{
    class AddDepartmentCommand : IUndoableCommand
    {
        Dictionary<int, Department> deptMap;
        Department dept;
        DepartmentForm form;

        public AddDepartmentCommand(
            Dictionary<int, Department> map,
            Department d,
            DepartmentForm f)
        {
            deptMap = map;
            dept = d;
            form = f;
        }

        public void Execute()
        {
            deptMap[dept.DepartmentCode] = dept;
            form.InsertIntoDatabase(
                dept.DepartmentCode.ToString(),
                dept.DepartmentName,
                dept.Description);
        }

        public void Undo()
        {
            deptMap.Remove(dept.DepartmentCode);
            form.DeleteFromDatabase(dept.DepartmentCode.ToString());
        }
    }
}