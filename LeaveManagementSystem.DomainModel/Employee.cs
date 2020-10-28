using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.DomainModel
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserImg { get; set; }
        public int LeaveID { get; set; }
   
        [ForeignKey("LeaveID")]
        public virtual List<Leave> Leaves { get; set; }  
    }
}
