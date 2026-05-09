using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS
{
    internal class Payroll
    {
        public int PayrollID { get; set; }
        public int EmployeeCode { get; set; }
        public double BasicSalary { get; set; }
        public double Allowances { get; set; }
        public double Deductions { get; set; }
        public double NetSalary { get; set; }
    }
}
