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
    public class PercentagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrator/Percentages
        public ActionResult Index()
        {
            return View(db.Percentages.ToList());
        }

        // GET: Administrator/Percentages/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Percentage percentage = db.Percentages.Find(id);
            if (percentage == null)
            {
                return HttpNotFound();
            }
            return View(percentage);
        }

        // GET: Administrator/Percentages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Percentages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PercentageId,Level1,Commission1,Level2,Commission2,Level3,Commission3,Level4,Commission4,Level5,Commission5,Level6,Commission6,Level7,Commission7,Level8,Commission8,Level9,Commission9")] Percentage percentage)
        {
            if (ModelState.IsValid)
            {
                percentage.PercentageId = Guid.NewGuid();
                db.Percentages.Add(percentage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(percentage);
        }

        // GET: Administrator/Percentages/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Percentage percentage = db.Percentages.Find(id);
            if (percentage == null)
            {
                return HttpNotFound();
            }
            return View(percentage);
        }

        // POST: Administrator/Percentages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PercentageId,Level1,Commission1,Level2,Commission2,Level3,Commission3,Level4,Commission4,Level5,Commission5,Level6,Commission6,Level7,Commission7,Level8,Commission8,Level9,Commission9")] Percentage percentage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(percentage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(percentage);
        }

        // GET: Administrator/Percentages/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Percentage percentage = db.Percentages.Find(id);
            if (percentage == null)
            {
                return HttpNotFound();
            }
            return View(percentage);
        }

        // POST: Administrator/Percentages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Percentage percentage = db.Percentages.Find(id);
            db.Percentages.Remove(percentage);
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
