using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace General.Areas.Administrator.Controllers
{
    //[Authorize(Roles = "User")]
    public class AdminGeneralpagesController : Controller
    {
        // GET: Administrator/AdministratorGeneralpages
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]

        public ActionResult AdminsMainPage()
        {
            return View();  
        }

    }
}