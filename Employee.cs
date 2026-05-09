using System;

namespace EMS
{
    public class Employee
    {
        public int EmployeeCode { get; set; }   // ✅ FIXED (int everywhere)
        public string EmployeeName { get; set; }
        public int DepartmentCode { get; set; }
        public double Salary { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
    }
}