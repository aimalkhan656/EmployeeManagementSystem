using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS
{
    internal class AttendanceRecord
    {
        public int AttendanceID { get; set; }     // DB only
        public int EmployeeCode { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
