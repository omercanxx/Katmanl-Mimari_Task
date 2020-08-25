using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Task.Entity;
using Task.WebUI.Models;

[assembly: OwinStartupAttribute(typeof(Task.WebUI.Startup))]
namespace Task.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateUserAndRoles();
        }

        public void CreateUserAndRoles()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!roleManager.RoleExists("admin"))
            {

                var role = new IdentityRole("admin");
                roleManager.Create(role);

                var roleEmployee = new IdentityRole("employee");
                roleManager.Create(roleEmployee);

                var manager = new IdentityRole("manager");
                roleManager.Create(manager);
                //Here we create a Admin super user who will maintain the website                   

                var user = new ApplicationUser();
                user.UserName = "admin@caretta.net";
                user.Email = "admin@caretta.net";
                string userPWD = "Caretta.97";
                user.PasswordChanged = true;
                var chkUser = UserManager.Create(user, userPWD);

                if (chkUser.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "admin");
                }
            }

        }
    }
}