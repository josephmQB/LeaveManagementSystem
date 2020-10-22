using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using LeaveManagementSystem.DomainModel.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartup(typeof(LeaveManagementSystem.Startup))]

namespace LeaveManagementSystem
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions() { AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie, LoginPath = new PathString("/Account/Login") });
            this.CreateRolesAndUsers();
        }

        public void CreateRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var appDbContext = new ApplicationDbContext();
            var appUserStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(appUserStore);

           
            //Create Admin Role
            if (!roleManager.RoleExists("Employee"))
            {
               
                var role = new IdentityRole();
                role.Name = "Employee";
                roleManager.Create(role);
            }
        }
    }
}
