using LeaveManagementSystem.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.Repository
{
    public interface ILeaveRepository
    {
        void InsertLeave(Leave l);
        Leave GetLeavesByID(int LeaveID);
        List<Leave> GetLeavesByEmpolyeeID(int EmpID);
        void UpdateLeaveStatus(Leave l);
        int GetLastestLeaveId();

    }
    public class LeaveRepository : ILeaveRepository
    {
        LeaveManagementDatabaseDbContext db;
        public LeaveRepository()
        {
            db = new LeaveManagementDatabaseDbContext();
        }
        public List<Leave> GetLeavesByEmpolyeeID(int EmpID)
        {
            List<Leave> leave = db.Leaves.Where(temp => temp.EmployeeID == EmpID).ToList();
            return leave;
        }

        public Leave GetLeavesByID(int LeaveID)
        {
            Leave leave = db.Leaves.Where(temp => temp.LeaveID == LeaveID).FirstOrDefault();
            return leave;
        }

        public void InsertLeave(Leave l)
        {
            db.Leaves.Add(l);
            db.SaveChanges();
        }

        public void UpdateLeaveStatus(Leave l)
        {
            Leave leave = db.Leaves.Where(temp => temp.LeaveID == l.LeaveID).FirstOrDefault();
            if(leave!=null)
            {
                leave.LeaveStatus = l.LeaveStatus;
                db.SaveChanges();
            }
        }
        public int GetLastestLeaveId()
        {
            int leaveId = db.Leaves.Select(temp => temp.LeaveID).Max();
            return leaveId ;
        }
    }
}
