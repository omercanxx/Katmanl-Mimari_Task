using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Task.WebUI.Controllers
{
    [_PasswordController]
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }
    }
}