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
    public class AllArmsAndIncomesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrator/AllArmsAndIncomes
        public ActionResult Index()
        {
            return View(db.AllArmsAndIncomes.ToList());
        }

        // GET: Administrator/AllArmsAndIncomes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllArmsAndIncome allArmsAndIncome = db.AllArmsAndIncomes.Find(id);
            if (allArmsAndIncome == null)
            {
                return HttpNotFound();
            }
            return View(allArmsAndIncome);
        }

        // GET: Administrator/AllArmsAndIncomes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/AllArmsAndIncomes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ArmAndIncomeId,ArmsNumber,LimitIncomes")] AllArmsAndIncome allArmsAndIncome)
        {
            if (ModelState.IsValid)
            {
                allArmsAndIncome.ArmAndIncomeId = Guid.NewGuid();
                db.AllArmsAndIncomes.Add(allArmsAndIncome);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(allArmsAndIncome);
        }

        // GET: Administrator/AllArmsAndIncomes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllArmsAndIncome allArmsAndIncome = db.AllArmsAndIncomes.Find(id);
            if (allArmsAndIncome == null)
            {
                return HttpNotFound();
            }
            return View(allArmsAndIncome);
        }

        // POST: Administrator/AllArmsAndIncomes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArmAndIncomeId,ArmsNumber,LimitIncomes")] AllArmsAndIncome allArmsAndIncome)
        {
            if (ModelState.IsValid)
            {
                db.Entry(allArmsAndIncome).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(allArmsAndIncome);
        }

        // GET: Administrator/AllArmsAndIncomes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AllArmsAndIncome allArmsAndIncome = db.AllArmsAndIncomes.Find(id);
            if (allArmsAndIncome == null)
            {
                return HttpNotFound();
            }
            return View(allArmsAndIncome);
        }

        // POST: Administrator/AllArmsAndIncomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AllArmsAndIncome allArmsAndIncome = db.AllArmsAndIncomes.Find(id);
            db.AllArmsAndIncomes.Remove(allArmsAndIncome);
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
