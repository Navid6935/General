using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using General.Areas.Administrator.Models;
using General.Models;

namespace General.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersArmsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrator/UsersArms
        public ActionResult Index()
        {
            return View(db.UsersArms.ToList());
        }

        // GET: Administrator/UsersArms/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersArms usersArms = db.UsersArms.Find(id);
            if (usersArms == null)
            {
                return HttpNotFound();
            }
            return View(usersArms);
        }

        // GET: Administrator/UsersArms/Create
        public ActionResult Create(Guid? id)
        {
            ViewBag.UserId = id;

            return View();
        }

        // POST: Administrator/UsersArms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArmsId,MarketingCode,InsuranceNumber,ArmsNumber")] UsersArms usersArms)
        {
            if (ModelState.IsValid)
            {
                usersArms.ArmsId = Guid.NewGuid();
                db.UsersArms.Add(usersArms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usersArms);
        }

        // GET: Administrator/UsersArms/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersArms usersArms = db.UsersArms.Find(id);
            if (usersArms == null)
            {
                return HttpNotFound();
            }
            return View(usersArms);
        }

        // POST: Administrator/UsersArms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArmsId,MarketingCode,InsuranceNumber,ArmsNumber")] UsersArms usersArms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usersArms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usersArms);
        }

        // GET: Administrator/UsersArms/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersArms usersArms = db.UsersArms.Find(id);
            if (usersArms == null)
            {
                return HttpNotFound();
            }
            return View(usersArms);
        }

        // POST: Administrator/UsersArms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            UsersArms usersArms = db.UsersArms.Find(id);
            db.UsersArms.Remove(usersArms);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        #region FindUserStatus
        /// <summary>
        /// پیدا کردن اطلاعات شخص
        /// </summary>
        /// <param name="MachinName"></param>
        /// <returns></returns>
        public ActionResult FindUserStatus(string Id)

        {

            //بدست آوردن آخرین کد
            var query = db.Users
                .Where(m => m.Id == Id)
                .OrderByDescending(e => e.MarketingCode)
                .FirstOrDefault();
            var UserStatus = new
            {
                MarketingCode = query.MarketingCode,
                FirstName = query.FirstName,
                LastName = query.LastName,
                InsuranceNumber1 = query.InsuranceNumber1,
                InsuranceNumber2 = query.InsuranceNumber2,
                InsuranceNumber3 = query.InsuranceNumber3,
                InsuranceNumber4 = query.InsuranceNumber4,
                InsuranceNumber5 = query.InsuranceNumber5,
                InsuranceNumber6 = query.InsuranceNumber6,

            };

            return Json(UserStatus, JsonRequestBehavior.AllowGet);
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
