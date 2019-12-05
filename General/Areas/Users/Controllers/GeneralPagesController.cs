using General.Areas.Administrator.Models;
using General.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace General.Areas.Users.Controllers
{
    public class GeneralPagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users/GeneralPages
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Installments
        /// </summary>
        /// <returns></returns>
        public ActionResult Installment(string Id)
        {
            #region UsersVar
            var User = db.Users.Where(i => i.Id == Id).FirstOrDefault();
            #endregion
            var UserInstallment = db.Installments
                .Where(i => i.MarketingCode == User.MarketingCode && i.InstallmentsNumberStatus == 0)
                .OrderBy(i=>i.DateWithoutPoints)
                .ToList();
            ViewBag.UserInstallmentCount = UserInstallment.Count();
            //ViewBag.InstallmentNums = db.Installments.Where()
            return View(UserInstallment);
        }
        public ActionResult UserPayment(Guid? id)
        {
            var UserId = Session["Id"];
            var user = db.Users.Where(u => u.Id == UserId).FirstOrDefault();
            Installment installment = db.Installments.Find(id);
            installment.InstallmentsNumberStatus = 1;
            db.SaveChanges();
            return RedirectToAction( "Installment", new { area = "Users", @id= UserId });
        }

        public ActionResult UserMainPage()
        {
            //ViewBag.InstallmentNums = db.Installments.Where()
            return View();
        }
    }
}