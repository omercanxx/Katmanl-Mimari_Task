﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Task.DataAccess.Abstract;
using Task.DataAccess.Concrete.Ef;
using Task.Entity;
using Task.ViewModel;

namespace Task.WebUI.Controllers
{
    [_PasswordController]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}