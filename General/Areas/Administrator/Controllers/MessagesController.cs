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

    public class MessagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "User")]

        // GET: Administrator/Messages
        public ActionResult Index()
        {
            return View(db.Messages.ToList());
        }

    [Authorize(Roles = "Admin")]
        // GET: Administrator/Messages/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messages messages = db.Messages.Find(id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            return View(messages);
        }

    [Authorize(Roles = "Admin")]
        // GET: Administrator/Messages/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]

        // POST: Administrator/Messages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MId,Mday,MMounth,MYear,MIssue,MDescription")] Messages messages)
        {
            if (ModelState.IsValid)
            {
                messages.MId = Guid.NewGuid();
                db.Messages.Add(messages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(messages);
        }

    [Authorize(Roles = "Admin")]
        // GET: Administrator/Messages/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messages messages = db.Messages.Find(id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            return View(messages);
        }

    [Authorize(Roles = "Admin")]
        // POST: Administrator/Messages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MId,Mday,MMounth,MYear,MIssue,MDescription")] Messages messages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(messages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(messages);
        }

    [Authorize(Roles = "Admin")]
        // GET: Administrator/Messages/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Messages messages = db.Messages.Find(id);
            if (messages == null)
            {
                return HttpNotFound();
            }
            return View(messages);
        }

    [Authorize(Roles = "Admin")]
        // POST: Administrator/Messages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Messages messages = db.Messages.Find(id);
            db.Messages.Remove(messages);
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
