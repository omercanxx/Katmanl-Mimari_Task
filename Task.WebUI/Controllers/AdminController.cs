using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.DataAccess.Abstract;
using Task.DataAccess.Concrete.Ef;
using Task.ViewModel;
using Task.WebUI.Models;

namespace Task.WebUI.Controllers
{
    [_PasswordController]
    [Authorize(Roles = "admin")]
    [HandleError]
    public class AdminController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        IProject repoProject = new ProjectDal();
        IAppUser repoUser = new UserDal();
        // GET: Admin

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult DeletedProject()
        {
            var model = repoProject.GetPasiveAll();
            return View(model);
        }

        public ActionResult Recovery(int id)
        {
            try
            {
                repoProject.Recovery(id);
            }
            catch
            {
                throw new Exception();
            }
            return RedirectToAction("DeletedProject");
        }
        public ActionResult CreateManager()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateManager(FormCollection form)
        {
            
            bool isAvailable = true;
            ViewBag.error = "";
            var UserManager = new Microsoft.AspNet.Identity.UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            string UserName = form["txtEmail"];
            string email = form["txtEmail"];
            string pwd = "Caretta.97";

            List<UserViewModel> uList = repoUser.GetAll();
            foreach (var item in uList)
            {
                if (item.Email == email)
                {
                    ViewBag.error = "The user you want to add is already registered.";
                    isAvailable = false;
                }
            }
            if (isAvailable)
            {
                var user = new ApplicationUser();
                user.UserName = UserName;
                user.Email = email;
                user.PasswordChanged = false;
                var newUser = UserManager.Create(user, pwd);
                UserManager.AddToRole(user.Id, "yonetici");
                ViewBag.error = "The manager you want to add has been successfully registered.";
            }

            return View();
        }



        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRole(FormCollection form)
        {
            ViewBag.RoleUyari = "";
            string RoleName = form["RoleName"];
            var roleManager = new Microsoft.AspNet.Identity.RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            if (!roleManager.RoleExists(RoleName))
            {
                var role = new IdentityRole(RoleName);
                roleManager.Create(role);
            }
            else
            {
                ViewBag.RoleUyari = "The role you want to add is already registered.";
                return View();
            }
            return View();
        }

        public ActionResult AssignRole()
        {
            ViewBag.Roles = context.Roles.Select(r => new SelectListItem { Value = r.Name, Text = r.Name }).ToList();
            ViewBag.User = context.Users.Where(r => r.Roles.Count == 0).Select(r => new SelectListItem { Value = r.Email, Text = r.Email }).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AssignRole(FormCollection form)
        {
            string username = form["txtUserName"];
            string rolname = form["RoleName"];

            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            var userManager = new Microsoft.AspNet.Identity.UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            userManager.AddToRole(user.Id, rolname);
            return View("Index");
        }
    }
}