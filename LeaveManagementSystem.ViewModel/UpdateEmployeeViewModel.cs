using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.ViewModel
{
    public class UpdateEmployeeViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime? DOB { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
