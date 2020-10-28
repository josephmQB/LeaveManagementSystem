using LeaveManagementSystem.DomainModel.Identity;
using LeaveManagementSystem.ServiceLayer;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LeaveManagementSystem.Controllers
{
    public class LeaveController : Controller
    {
        ILeaveService ls;
        public LeaveController(ILeaveService ls)
        {
            this.ls = ls;
        }
        // GET: Leave
        public ActionResult LeaveRequest()
        {
            var appDbContext = new ApplicationDbContext();
            var userStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(userStore);
            var user = userManager.FindById(User.Identity.GetUserId());
            ViewBag.empId = user.EmpID;
            return View();
        }
    }
}