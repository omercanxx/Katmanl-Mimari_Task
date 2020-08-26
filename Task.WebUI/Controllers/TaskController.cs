using Elmah;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.DataAccess.Abstract;
using Task.DataAccess.Concrete.Ef;
using Task.Entity;
using Task.ViewModel;
using Task.WebUI.Models;

namespace Task.WebUI.Controllers
{
    [_PasswordController]
    [HandleError]
    public class TaskController : Controller
    {
        readonly ITask repoTask = new TaskDal();
        readonly IAppUser repoUser = new UserDal();
        readonly IProject repoProject = new ProjectDal();
        // GET: Task
        [Authorize(Roles = "manager")]
        public ActionResult Task()
        {
            List<MyModel> approvedList = repoTask.Approved().ToList();
            return View(approvedList);
        }
        [Authorize(Roles = "manager")]
        public ActionResult ExpectedTask()
        {
            List<MyModel> expectedList = repoTask.Expected().ToList();
            return View(expectedList);
        }
        [Authorize(Roles = "employee")]
        public ActionResult ToDoTask()
        {
            string id = User.Identity.GetUserId();
            List<MyModel> ToDo = repoTask.ToDo(id);
            return View(ToDo);
        }
        [Authorize(Roles = "manager")]
        public ActionResult Create()
        {
            var employeeList = repoUser.GetPersonelAll();
            var project = repoProject.GetActiveAll();
            List<object> eList = new List<object>();
            foreach (var item in project)
            {
                eList.Add(new SelectListItem { Text = item.Project.Name, Value = Convert.ToString(item.Project.Id) });
            }
            ViewBag.eList = eList;

            List<object> userList = new List<object>();
            foreach (var item in employeeList)
            {
                userList.Add(new SelectListItem { Text = item.UserName, Value = Convert.ToString(item.UserId) });
            }
            ViewBag.userList = userList;
            return View();
        }
        [HttpPost]
        public ActionResult Create(TaskViewModel model)
        {
            try
            {
                repoTask.Insert(model);
            }
            catch (Exception ex)
            {
                ErrorLog.GetDefault(null).Log(new Error(ex));
                return View("Error", new HandleErrorInfo(ex, "Task", "Create"));
            }
            return RedirectToAction("Task");
        }
        public ActionResult Delete(int id)
        {
            try
            {
                repoTask.Delete(id);
            }
            catch (Exception ex)
            {
                ErrorLog.GetDefault(null).Log(new Error(ex));
                return View("Error", new HandleErrorInfo(ex, "Task", "Delete"));
            }
            return RedirectToAction("Task");
        }

        public ActionResult Approve(int id)
        {
            try
            {
                repoTask.Approve(id);
            }
            catch (Exception ex)
            {
                ErrorLog.GetDefault(null).Log(new Error(ex));
                return View("Error", new HandleErrorInfo(ex, "Task", "Approve"));
            }
            return RedirectToAction("Task");
        }
        public ActionResult Send(int id)
        {
            try
            {
                repoTask.Send(id);
            }
            catch (Exception ex)
            {
                ErrorLog.GetDefault(null).Log(new Error(ex));
                return View("Error", new HandleErrorInfo(ex, "Task", "Send"));
            }
            return RedirectToAction("ToDo");
        }
        public ActionResult Reject(int id, string exp)
        {
            try
            {
                repoTask.Reject(id, exp);
            }
            catch (Exception ex)
            {
                ErrorLog.GetDefault(null).Log(new Error(ex));
                return View("Error", new HandleErrorInfo(ex, "Task", "Reject"));
            }
            return RedirectToAction("ExpectedTask");
        }
        public string Show(int id)
        {
            string explanation = repoTask.Show(id);
            return explanation;
        }
    }
}