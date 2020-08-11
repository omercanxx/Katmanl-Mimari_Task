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
    public class TaskController : Controller
    {
        ITask repoTask = new TaskDal();
        IAppUser repoUser = new UserDal();
        IProject repoProject = new ProjectDal();
        // GET: Task
        [Authorize(Roles = "yonetici")]
        public ActionResult Task()
        {
            string id = User.Identity.GetUserId();
            List<MyModel> approvedList = repoTask.Approved().ToList();
            return View(approvedList);
        }
        [Authorize(Roles = "yonetici")]
        public ActionResult ExpectedTask()
        {
            string id = User.Identity.GetUserId();
            List<MyModel> expectedList = repoTask.Expected().ToList();
            return View(expectedList);
        }
        [Authorize(Roles = "personel")]
        public ActionResult ToDoTask()
        {
            string id = User.Identity.GetUserId();
            List<MyModel> ToDo = repoTask.ToDo(id);
            return View(ToDo);
        }
        [Authorize(Roles = "yonetici")]
        public ActionResult Create()
        {
            string id = User.Identity.GetUserId();
            var personelList = repoUser.GetPersonelAll();
            var proje = repoProject.GetActiveAll();
            List<object> pList = new List<object>();
            foreach (var item in proje)
            {
                pList.Add(new SelectListItem { Text = item.Project.Name, Value = Convert.ToString(item.Project.Id) });
            }
            ViewBag.pList = pList;

            string name = User.Identity.GetUserName();

            List<object> userList = new List<object>();
            foreach (var item in personelList)
            {
                userList.Add(new SelectListItem { Text = item.Username, Value = Convert.ToString(item.UserID) });
            }
            ViewBag.userList = userList;
            return View();
        }
        [HttpPost]
        public ActionResult Create(TaskViewModel model)
        {
            repoTask.Insert(model);
            return RedirectToAction("Task");
        }
        public ActionResult Delete(int id)
        {
            repoTask.Delete(id);
            return RedirectToAction("Task");
        }

        public ActionResult onayla(int id)
        {
            repoTask.Onayla(id);
            return RedirectToAction("Task");
        }
        public ActionResult gonder(int id)
        {
            repoTask.Gonder(id);
            return RedirectToAction("ToDo");
        }
        public ActionResult reddet(int id, string exp)
        {
            repoTask.Reddet(id, exp);
            return RedirectToAction("ExpectedTask");
        }
        public string goster(int id)
        {
            string exp = repoTask.Goster(id);
            return exp;
        }
    }
}