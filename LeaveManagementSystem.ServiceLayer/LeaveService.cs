using LeaveManagementSystem.DomainModel;
using LeaveManagementSystem.Repository;
using LeaveManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeaveManagementSystem.ServiceLayer
{
    public interface ILeaveService
    {
        int InsertLeave(RequestLeaveViewModel rlvm);
        List<LeaveViewModel> GetLeaveByEmployeeID(int EmpID);
        LeaveViewModel GetLeaveByID(int LeaveID);
        void UpdateLeaveStatus(UpdateLeaveStatusViewModel ulsvm);
    }
    public class LeaveService : ILeaveService
    {
        ILeaveRepository lr;
        public LeaveService()
        {
            lr = new LeaveRepository();
        }
        public List<LeaveViewModel> GetLeaveByEmployeeID(int EmpID)
        {
            List<Leave> leaves = lr.GetLeavesByEmpolyeeID(EmpID);
            List<LeaveViewModel> lvm = null;
            if(leaves!=null)
            {
                foreach (var item in leaves)
                    lvm.Add(new LeaveViewModel() { LeaveID = item.LeaveID, StartDate = item.StartDate, EndDate = item.EndDate, NoOfDays = item.NoOfDays, LeaveStatus = item.LeaveStatus, LeaveDescription = item.LeaveDescription, EmployeeID = item.EmployeeID });
            }
            return lvm;
        }

        public LeaveViewModel GetLeaveByID(int LeaveID)
        {
            Leave l = lr.GetLeavesByID(LeaveID);
            LeaveViewModel lvm = null;
            if(l!=null)
                lvm = new LeaveViewModel() { LeaveID = l.LeaveID, StartDate = l.StartDate, EndDate = l.EndDate, NoOfDays = l.NoOfDays, LeaveStatus = l.LeaveStatus, LeaveDescription = l.LeaveDescription, EmployeeID = l.EmployeeID };
            return lvm;
        }

        public int InsertLeave(RequestLeaveViewModel rlvm)
        {
            Leave l = new Leave() { StartDate = rlvm.StartDate, EndDate = rlvm.EndDate, NoOfDays = rlvm.NoOfDays, LeaveDescription = rlvm.LeaveDescription, EmployeeID = rlvm.EmployeeID };
            l.LeaveStatus = "Pending";
            lr.InsertLeave(l);
            int leaveId = lr.GetLastestLeaveId();
            return leaveId;
        }

        public void UpdateLeaveStatus(UpdateLeaveStatusViewModel ulsvm)
        {
            Leave l = new Leave();
            l.LeaveID = ulsvm.LeaveID;
            l.LeaveStatus = ulsvm.LeaveStatus;
            lr.UpdateLeaveStatus(l);
        }
    }
}
