using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.WebUI.Models;

namespace Task.WebUI.Controllers
{
    public class _PasswordControllerAttribute : ActionFilterAttribute, IActionFilter
    {
        ApplicationDbContext context = new ApplicationDbContext();
        // GET: _Password
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            List<ApplicationUser> userList = context.Users.Where(m => m.PasswordChanged == false).ToList();
            string name = filterContext.HttpContext.User.Identity.GetUserName();

            if (!HttpContext.Current.Response.IsRequestBeingRedirected)
            {
                foreach (var item in userList)
                {
                    if (item.UserName == name)
                    {
                        filterContext.HttpContext.Response.Redirect("/Manage/ChangePassword");
                    }
                }
            }
        }
    }
}