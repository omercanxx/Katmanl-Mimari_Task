using Elmah;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.DataAccess.Abstract;
using Task.DataAccess.Concrete.Ef;
using Task.ViewModel;

namespace Task.WebUI.Controllers
{
    [_PasswordController]
    [Authorize(Roles = "manager")]
    [HandleError]
    public class ProjectController : Controller
    {
        readonly IProject projectDal = new ProjectDal();
        // GET: Project
        public ActionResult Project()
        {
            List<ProjectViewModel> projectList = projectDal.GetProjects();
            return View(projectList);
        }
        public ActionResult Create()
        {
            List<object> status = new List<object>();
            status.Add(new SelectListItem { Text = "Active", Value = "active" });
            status.Add(new SelectListItem { Text = "Passive", Value = "passive" });

            ViewBag.status = status;
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProjectViewModel model)
        {
            model.UserId = User.Identity.GetUserId();
            try
            {
                projectDal.Insert(model);
            }
            catch(Exception ex)
            {
                ErrorLog.GetDefault(null).Log(new Error(ex));
                return View("Error", new HandleErrorInfo(ex, "Project", "Create"));
            }
            return RedirectToAction("Project");
        }
        public ActionResult Delete(int id)
        {
            try
            {
                projectDal.Delete(id);
            }
            catch(Exception ex)
            {
                ErrorLog.GetDefault(null).Log(new Error(ex));
                return View("Error", new HandleErrorInfo(ex, "Admin", "Delete"));
            }
            return RedirectToAction("Project");
        }
    }
}