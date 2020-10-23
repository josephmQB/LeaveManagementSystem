using LeaveManagementSystem.DomainModel;
using LeaveManagementSystem.DomainModel.Identity;
using LeaveManagementSystem.Repository;
using LeaveManagementSystem.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace LeaveManagementSystem.ServiceLayer
{
    public interface IEmployeeService
    {
        int? InsertEmployee(RegisterViewModel rvm);
        void UpdateEmployeeDetails(UpdateEmployeeViewModel uevm);
       
        void DeleteEmployee(int empId);
        List<EmployeeViewModel> GetEmployee();
        EmployeeViewModel GetEmployeeByEmail(string Email);
        EmployeeViewModel GetEmployeeByID(int EmpID);
    }
    public class EmployeeService : IEmployeeService
    {
        IEmployeeRepository er;
        public EmployeeService()
        {
            er = new EmployeeRepository();
        }

        public void DeleteEmployee(int empId)
        {
            throw new NotImplementedException();
        }

        public List<EmployeeViewModel> GetEmployee()
        {
            throw new NotImplementedException();
        }

        public EmployeeViewModel GetEmployeeByEmail(string Email)
        {
            var appDbContext = new ApplicationDbContext();
            var userStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(userStore);
            ApplicationUser user = appDbContext.Users.Where(temp => temp.Email == Email).FirstOrDefault();
            Employee e = new Employee();
            e = er.GetEmployeeById(user.EmpID);
            EmployeeViewModel evm = null;
            if (e != null)
                evm = new EmployeeViewModel() { EmployeeID = e.EmployeeID, Name = e.EmployeeName, Address = e.Address, DOB = e.DateOfBirth, Phone = e.Phone ,Email = user.Email };
            return evm;
        }

        public EmployeeViewModel GetEmployeeByID(int EmpID)
        {
            Employee e = new Employee();
            e= er.GetEmployeeById(EmpID);
            var appDbContext = new ApplicationDbContext();
            var userStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(userStore);
            ApplicationUser user = appDbContext.Users.Where(temp => temp.EmpID == EmpID).FirstOrDefault();
            EmployeeViewModel evm = null;
            if(e!=null)
                evm = new EmployeeViewModel() { EmployeeID = e.EmployeeID, Name = e.EmployeeName, Address = e.Address, DOB = e.DateOfBirth, Phone = e.Phone , Email = user.Email };
            return evm;
        }

        public int? InsertEmployee(RegisterViewModel rvm)
        {
            Employee e = new Employee();
            e.EmployeeName = rvm.Name;
            e.Phone = rvm.Phone;
            er.InsertEmployee(e);
            int? empId = er.GetLastestEmployeeId();

            var appDbContext = new ApplicationDbContext();
            var userStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(userStore);
            var passwordHash = Crypto.HashPassword(rvm.Password);
            var user = new ApplicationUser() { Email = rvm.Email, UserName = rvm.Name, PasswordHash = passwordHash,EmpID = (int)empId };
            IdentityResult result = userManager.Create(user);

            if (result.Succeeded)
            {
                userManager.AddToRole(user.Id, "Employee");
                return (int)empId;
            }
            else
            {
                er.DeleteEmployee((int)empId);
                return null;
            }
           

        }

        public void UpdateEmployeeDetails(UpdateEmployeeViewModel uevm)
        {
            Employee e = new Employee();
            e.EmployeeID = uevm.EmployeeId;
            e.EmployeeName = uevm.Name;
            e.DateOfBirth = uevm.DOB;
            e.Address = uevm.Address;
            e.Phone = uevm.Phone;
            er.UpdateEmpolyeeDetails(e);
        }
    }
}
