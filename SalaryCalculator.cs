using System;

namespace EMS
{
    internal static class SalaryCalculator
    {
        public static PayrollResult Calculate(
            double basicSalary,
            double allowance,
            double deduction)
        {
            PayrollResult result = new PayrollResult();

            result.GrossSalary = basicSalary + allowance;
            result.NetSalary = result.GrossSalary - deduction;

            return result;
        }
    }
}
