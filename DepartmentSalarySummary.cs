using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS
{
    internal class DepartmentSalarySummary
    {
        public string Department { get; set; }
        public double TotalSalary { get; set; }
        public double AverageSalary { get; set; }
        public int EmployeeCount { get; set; }
    }
}
