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
    public class ArmsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrator/Arms
        public ActionResult Index()
        {
            return View(db.Arms.ToList());
        }

        // GET: Administrator/Arms/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arms arms = db.Arms.Find(id);
            if (arms == null)
            {
                return HttpNotFound();
            }
            return View(arms);
        }

        // GET: Administrator/Arms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Arms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArmId,ArmsNumber")] Arms arms)
        {
            if (ModelState.IsValid)
            {
                arms.ArmId = Guid.NewGuid();
                db.Arms.Add(arms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(arms);
        }

        // GET: Administrator/Arms/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arms arms = db.Arms.Find(id);
            if (arms == null)
            {
                return HttpNotFound();
            }
            return View(arms);
        }

        // POST: Administrator/Arms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArmId,ArmsNumber")] Arms arms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(arms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(arms);
        }

        // GET: Administrator/Arms/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Arms arms = db.Arms.Find(id);
            if (arms == null)
            {
                return HttpNotFound();
            }
            return View(arms);
        }

        // POST: Administrator/Arms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Arms arms = db.Arms.Find(id);
            db.Arms.Remove(arms);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
