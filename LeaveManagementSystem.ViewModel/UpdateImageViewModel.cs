using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.ViewModel
{
    public class UpdateImageViewModel
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public string UserImg { get; set; }
    }
}
