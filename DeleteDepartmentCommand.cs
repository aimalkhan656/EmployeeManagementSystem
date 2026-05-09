using System.Collections.Generic;

namespace EMS
{
    class DeleteDepartmentCommand : IUndoableCommand
    {
        Dictionary<int, Department> deptMap;
        Department deletedDept;
        DepartmentForm form;

        public DeleteDepartmentCommand(
            Dictionary<int, Department> map,
            Department dept,
            DepartmentForm f)
        {
            deptMap = map;
            deletedDept = dept;
            form = f;
        }

        public void Execute()
        {
            deptMap.Remove(deletedDept.DepartmentCode);
            form.DeleteFromDatabase(deletedDept.DepartmentCode.ToString());
        }

        public void Undo()
        {
            deptMap[deletedDept.DepartmentCode] = deletedDept;
            form.InsertIntoDatabase(
                deletedDept.DepartmentCode.ToString(),
                deletedDept.DepartmentName,
                deletedDept.Description);
        }
    }
}
