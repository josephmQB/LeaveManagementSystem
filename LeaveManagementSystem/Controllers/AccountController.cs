using LeaveManagementSystem.DomainModel.Identity;
using LeaveManagementSystem.ServiceLayer;
using LeaveManagementSystem.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace LeaveManagementSystem.Controllers
{
    public class AccountController : Controller
    {
        IEmployeeService es;

        public AccountController(IEmployeeService es)
        {
            this.es = es;
        }
        // GET: Account
        public ActionResult Register()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count >= 1)
                {
                    var file = Request.Files[0];
                    var imgBytes = new Byte[file.ContentLength];
                    file.InputStream.Read(imgBytes, 0, file.ContentLength);
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    rvm.UserImg = base64String;
                }
                int? id = this.es.InsertEmployee(rvm);
                if (id!=null)
                    return Content("<script language='javascript' type='text/javascript'>alert('Employee Created');</script>");
                return Content("<script language='javascript' type='text/javascript'>alert('Employee Not Created');</script>");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }
        public ActionResult Update(int id)
        {
            EmployeeViewModel evm = this.es.GetEmployeeByID(id);
            UpdateEmployeeViewModel uevm = new UpdateEmployeeViewModel() { EmployeeId =evm.EmployeeID,Name = evm.Name , Address = evm.Address, DOB = evm.DOB , Phone = evm.Phone,Email = evm.Email};
            return View(uevm);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Update(UpdateEmployeeViewModel uevm)
        {
            if (ModelState.IsValid)
            {
                this.es.UpdateEmployeeDetails(uevm);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(uevm);
            }
        }
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public ActionResult Login(LoginViewModel lvm)
        {
            //login
            var appDbContext = new ApplicationDbContext();
            var userStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(userStore);
            var user = userManager.FindByEmail(lvm.Email);
            var checkPassword = userManager.CheckPassword(user, lvm.Password);
            if (checkPassword)
            {
                //login
                var authenticationManager = HttpContext.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
                return RedirectToAction("MyProfile", "Account");
            }
            else
            {
                ModelState.AddModelError("myerror", "Invalid username or password");
                return View();
            }
        }
        public ActionResult MyProfile()
        {
            var appDbContext = new ApplicationDbContext();
            var userStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(userStore);
            var user = userManager.FindById(User.Identity.GetUserId());
            int EmpId = user.EmpID;
            EmployeeViewModel evm = this.es.GetEmployeeByID(EmpId);
            return View(evm);
        }
        public ActionResult Delete(int id)
        {
            this.es.DeleteEmployee(id);
            return Content("<script language='javascript' type='text/javascript'>alert('Employee Deleted');</script>");
        }
        [HttpPost]
        public ActionResult UpdateImage(UpdateImageViewModel uivm)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files.Count >= 1)
                {
                    var file = Request.Files[0];
                    var imgBytes = new Byte[file.ContentLength];
                    file.InputStream.Read(imgBytes, 0, file.ContentLength);
                    var base64String = Convert.ToBase64String(imgBytes, 0, imgBytes.Length);
                    uivm.UserImg = base64String;
                }
                this.es.UpdateEmployeeImage(uivm);
                return RedirectToAction("MyProfile", "Account");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View(uivm);
            }

        }
    }
}