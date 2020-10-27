using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeaveManagementSystem.DomainModel;

namespace LeaveManagementSystem.Repository
{
    public interface IEmployeeRepository
    {
        void InsertEmployee(Employee e);
        void UpdateEmpolyeeDetails(Employee e);
        void DeleteEmployee(int EmpID);
        List<Employee> GetEmployees();
        Employee GetEmployeeById(int EmpID);
        int GetLastestEmployeeId();
        void UpdateEmpolyeeImage(Employee e);
    }
    public class EmployeeRepository : IEmployeeRepository
    {
        LeaveManagementDatabaseDbContext db;
        public EmployeeRepository()
        {
            db = new LeaveManagementDatabaseDbContext();
        }

        public void DeleteEmployee(int EmpID)
        {
            Employee emp = db.Employees.Where(temp => temp.EmployeeID == EmpID).FirstOrDefault();
            if (emp != null)
            {
                db.Employees.Remove(emp);
                db.SaveChanges();
            }
        }

        public Employee GetEmployeeById(int EmpID)
        {
            Employee emp = db.Employees.Where(temp => temp.EmployeeID == EmpID).FirstOrDefault();
            return emp;
        }

        public List<Employee> GetEmployees()
        {
            throw new NotImplementedException();
        }

        public int GetLastestEmployeeId()
        {
            int empId = db.Employees.Select(temp => temp.EmployeeID).Max();
            return empId;
        }

        public void InsertEmployee(Employee e)
        {
            db.Employees.Add(e);
            db.SaveChanges();
        }

        public void UpdateEmpolyeeDetails(Employee e)
        {
            Employee emp = db.Employees.Where(temp => temp.EmployeeID == e.EmployeeID).FirstOrDefault();
            if (emp != null)
            {
                emp.EmployeeName = e.EmployeeName;
                emp.DateOfBirth = e.DateOfBirth;
                emp.Address = e.Address;
                emp.Phone = e.Phone;
                db.SaveChanges();
            }
        }
        public void UpdateEmpolyeeImage(Employee e)
        {
            Employee emp = db.Employees.Where(temp => temp.EmployeeID == e.EmployeeID).FirstOrDefault();
            if (emp != null)
            {
                emp.UserImg = e.UserImg;
                db.SaveChanges();
            }
        }
    }
}
