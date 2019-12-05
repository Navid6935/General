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
    public class EducationalPamphletsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Administrator/EducationalPamphlets1
        public ActionResult Index()
        {
            return View(db.EducationalPamphlets.ToList());
        }

        // GET: Administrator/EducationalPamphlets1/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationalPamphlets educationalPamphlets = db.EducationalPamphlets.Find(id);
            if (educationalPamphlets == null)
            {
                return HttpNotFound();
            }
            return View(educationalPamphlets);
        }

        // GET: Administrator/EducationalPamphlets1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/EducationalPamphlets1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EPId,EPIssue,EPDescription,Image")] EducationalPamphlets educationalPamphlets, HttpPostedFileBase UploadImage)
        {
            if (ModelState.IsValid)
            {
                educationalPamphlets.EPId = Guid.NewGuid();
                if (UploadImage != null)

                {                    //میتوان از طریق این متغیر حجم فایل قبل از آپلودرا مورد بررسی قرارداد
                    System.Drawing.Image oImage =
                       System.Drawing.Image.FromStream(UploadImage.InputStream);
                    //میتوان از طریق این متغیر پسوند فایل تصویری را مورد بررسی قرارداد
                    string strFileExtension =
                    System.IO.Path.GetExtension(UploadImage.FileName).ToUpper();

                    //میتوان از طریق این متغیر ماهیت فایل تصویری را مورد بررسی قرارداد
                    string strContentType =
                    UploadImage.ContentType.ToUpper();
                    //اگر سایز فایل بزرگتر از حد معمول بود
                    if ((oImage.Width > 1600) || (oImage.Height > 900))

                    {
                        ModelState.AddModelError("", "سایز فایل بزرگتر از حد مورد قبول می باشد");
                        return View();
                    }
                    if ((UploadImage.ContentLength > 1000 * 300))
                    {
                        ModelState.AddModelError("", "سایز فایل بزرگتر از حد مورد قبول می باشد");
                        return View();
                    }
                    else { educationalPamphlets.Image = UploadImage.FileName; }
                }

                if ((UploadImage == null
                || (UploadImage.ContentLength == 0)

                || (educationalPamphlets.Image == null)))

                {
                    educationalPamphlets.Image = "b7c3ca6e9e.png";
                    string strPathName0 = Server.MapPath("~") + "Uploads\\" + educationalPamphlets.Image;

                }

                else
                {
                    string strPath = Server.MapPath("~") + "Uploads\\";

                    if (System.IO.Directory.Exists(strPath) == false)
                    {
                        System.IO.Directory.CreateDirectory(strPath);
                    }

                    string strPathName =
                        string.Format("{0}\\{1}", strPath, educationalPamphlets.Image);

                    UploadImage.SaveAs(strPathName);
                }
                db.EducationalPamphlets.Add(educationalPamphlets);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(educationalPamphlets);
        }

        // GET: Administrator/EducationalPamphlets1/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationalPamphlets educationalPamphlets = db.EducationalPamphlets.Find(id);
            if (educationalPamphlets == null)
            {
                return HttpNotFound();
            }
            return View(educationalPamphlets);
        }

        // POST: Administrator/EducationalPamphlets1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EPId,EPIssue,EPDescription,Image")] EducationalPamphlets educationalPamphlets)
        {
            if (ModelState.IsValid)
            {
                db.Entry(educationalPamphlets).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(educationalPamphlets);
        }

        // GET: Administrator/EducationalPamphlets1/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EducationalPamphlets educationalPamphlets = db.EducationalPamphlets.Find(id);
            if (educationalPamphlets == null)
            {
                return HttpNotFound();
            }
            return View(educationalPamphlets);
        }

        // POST: Administrator/EducationalPamphlets1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            EducationalPamphlets educationalPamphlets = db.EducationalPamphlets.Find(id);
            db.EducationalPamphlets.Remove(educationalPamphlets);
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
