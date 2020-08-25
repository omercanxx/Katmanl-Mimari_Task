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
    [Authorize(Roles = "manager")]
    public class EmployeeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        IAppUser userDal = new UserDal();
        // GET: Employee
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection form)
        {
            bool isAvailable = true;
            ViewBag.error = "";
            var UserManager = new Microsoft.AspNet.Identity.UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            string userName = form["textEmail"];
            string email = form["textEmail"];
            string password = "Caretta.97";

            List<UserViewModel> uList = userDal.GetAll();
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
                user.UserName = userName;
                user.Email = email;
                user.PasswordChanged = false;
                var newUser = UserManager.Create(user, password);
                UserManager.AddToRole(user.Id, "employee");
                ViewBag.error = "The employee you want to add has been successfully registered.";
            }

            return View();
        }
    }
}