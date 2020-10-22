using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.DomainModel
{
    public class LeaveManagementDatabaseDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
    }
}
