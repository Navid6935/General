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
    public class CommisionOnArmsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrator/CommisionOnArms
        public ActionResult Index()
        {
            return View(db.CommisionOnArms.ToList());
        }

        // GET: Administrator/CommisionOnArms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommisionOnArms commisionOnArms = db.CommisionOnArms.Find(id);
            if (commisionOnArms == null)
            {
                return HttpNotFound();
            }
            return View(commisionOnArms);
        }

        // GET: Administrator/CommisionOnArms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/CommisionOnArms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CAId,CAAmount")] CommisionOnArms commisionOnArms)
        {
            if (ModelState.IsValid)
            {
                db.CommisionOnArms.Add(commisionOnArms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commisionOnArms);
        }

        // GET: Administrator/CommisionOnArms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommisionOnArms commisionOnArms = db.CommisionOnArms.Find(id);
            if (commisionOnArms == null)
            {
                return HttpNotFound();
            }
            return View(commisionOnArms);
        }

        // POST: Administrator/CommisionOnArms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CAId,CAAmount")] CommisionOnArms commisionOnArms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commisionOnArms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commisionOnArms);
        }

        // GET: Administrator/CommisionOnArms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommisionOnArms commisionOnArms = db.CommisionOnArms.Find(id);
            if (commisionOnArms == null)
            {
                return HttpNotFound();
            }
            return View(commisionOnArms);
        }

        // POST: Administrator/CommisionOnArms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommisionOnArms commisionOnArms = db.CommisionOnArms.Find(id);
            db.CommisionOnArms.Remove(commisionOnArms);
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
