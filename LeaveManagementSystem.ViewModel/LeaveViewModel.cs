using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.ViewModel
{
    public class LeaveViewModel
    {
        public int LeaveID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NoOfDays { get; set; }
        public string LeaveStatus { get; set; }
        public string LeaveDescription { get; set; }
        public int EmployeeID { get; set; }

    }
}
