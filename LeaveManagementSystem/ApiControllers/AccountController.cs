using LeaveManagementSystem.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LeaveManagementSystem.ApiControllers
{
    public class AccountController : ApiController
    {
        IEmployeeService es;
        public AccountController(IEmployeeService es)
        {
            this.es = es;
        }
        public string Get(string Email)
        {
            if (this.es.GetEmployeeByEmail(Email) != null)
            {
                return "Found";
            }
            else
            {
                return "Not Found";
            }
        }
    }
}
