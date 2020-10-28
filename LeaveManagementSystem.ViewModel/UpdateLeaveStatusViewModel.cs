﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.ViewModel
{
    public class UpdateLeaveStatusViewModel
    {
        [Required]
        public int LeaveID { get; set; }
        [Required]
        public string LeaveStatus { get; set; }
    }
}
