using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace General.Areas.Administrator.Controllers
{
    public class AdministratorPageController : Controller
    {
        // GET: Administrator/AdministratorPage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AdministratorPage()
        {
            return View();
        }
    }
}