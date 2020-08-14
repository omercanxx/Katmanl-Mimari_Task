﻿using Microsoft.AspNet.Identity;
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
    [Authorize(Roles = "yonetici")]
    [HandleError]
    public class ProjectController : Controller
    {
        IProject repoProject = new ProjectDal();
        // GET: Project
        public ActionResult Project()
        {
            List<MyModel> activePrjList = repoProject.GetActiveAll();
            return View(activePrjList);
        }
        public ActionResult Create()
        {
            List<object> status = new List<object>();
            status.Add(new SelectListItem { Text = "Aktif", Value = "aktif" });
            status.Add(new SelectListItem { Text = "Pasif", Value = "pasif" });

            ViewBag.status = status;
            return View();
        }
        [HttpPost]
        public ActionResult Create(ProjectViewModel model)
        {
            model.UserId = User.Identity.GetUserId();
            try
            {
                repoProject.Insert(model);
            }
            catch
            {
                throw new Exception();
            }
            return RedirectToAction("Project");
        }
        public ActionResult Delete(int id)
        {
            try
            {
                repoProject.Delete(id);
            }
            catch
            {
                throw new Exception();
            }
            return RedirectToAction("Project");
        }
    }
}