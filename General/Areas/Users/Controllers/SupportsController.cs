using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using General.Areas.Users.Models;
using General.Models;

namespace General.Areas.Users.Controllers
{
    public class SupportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "Admin")]

        // GET: Users/Supports
        public ActionResult Index()
        {
            return View(db.Supports.ToList());
        }
        [Authorize(Roles = "Admin")]

        // GET: Users/Supports
        public ActionResult IndexManager()
        {
            return View(db.Supports.Where(s=>s.SupportResponse == null || s.SupportResponse == "").ToList());
        }
        [Authorize(Roles = "Admin")]

        // GET: Users/Supports/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Support support = db.Supports.Find(id);
            if (support == null)
            {
                return HttpNotFound();
            }
            return View(support);
        }
        [Authorize(Roles = "User")]
        //[Authorize(Roles = "Admin")]
        // GET: Users/Supports/Create
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "User")]
        //[Authorize(Roles = "Admin")]

        // POST: Users/Supports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SUId,MarketingCode,SupportIssue,SupportDescription,SupportResponse,Image")] Support support, HttpPostedFileBase UploadImage)
        {
            if (ModelState.IsValid)
            {

                    support.SUId = Guid.NewGuid();
                support.MarketingCode = Session["MK"].ToString();

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

                        //support.Image = "121.png";
                        //string strPathName0 = Server.MapPath("~") + "Uploads\\" + support.Image;
                    }
                    if ((UploadImage.ContentLength > 1000 * 300))
                    {
                        ModelState.AddModelError("", "سایز فایل بزرگتر از حد مورد قبول می باشد");
                        return View();
                    }
                    else { support.Image = UploadImage.FileName; }
                }
                if ((UploadImage == null
                || (UploadImage.ContentLength == 0)
                
                || (support.Image == null)))

                {
                    support.Image = "b7c3ca6e9e.png";
                    string strPathName0 = Server.MapPath("~") + "Uploads\\" + support.Image;

                }

                else
                {
                    string strPath = Server.MapPath("~") + "Uploads\\";

                    if (System.IO.Directory.Exists(strPath) == false)
                    {
                        System.IO.Directory.CreateDirectory(strPath);
                    }

                    string strPathName =
                        string.Format("{0}\\{1}", strPath, support.Image);

                    UploadImage.SaveAs(strPathName);
                }
                support.SupportStatus = 0;

                db.Supports.Add(support);
                db.SaveChanges();
                return RedirectToAction("UserPage", "UsersAdmin", new { area = "Administrator" });
            }

            return View(support);
        }
        [Authorize(Roles = "Admin")]

        // GET: Users/Supports/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Support support = db.Supports.Find(id);
            if (support == null)
            {
                return HttpNotFound();
            }
            return View(support);
        }

        // POST: Users/Supports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.  
        [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SUId,MarketingCode,SupportIssue,SupportDescription,SupportResponse,Image")] Support support)
        {
            if (ModelState.IsValid)
            {
                db.Entry(support).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(support);
        }
        [Authorize(Roles = "Admin")]

        // GET: Users/Supports/Edit/5
        public ActionResult Response(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Support support = db.Supports.Find(id);
            if (support == null)
            {
                return HttpNotFound();
            }
            return View(support);
        }

        // POST: Users/Supports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598. 
        [Authorize(Roles = "Admin")]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Response([Bind(Include = "SUId,MarketingCode,SupportIssue,SupportDescription,SupportResponse,Image")] Support support)
        {
            if (ModelState.IsValid)
            {
                db.Entry(support).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexManager");
            }
            return View(support);
        }
        // GET: Users/Supports/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Support support = db.Supports.Find(id);
            if (support == null)
            {
                return HttpNotFound();
            }
            return View(support);
        }
        [Authorize(Roles = "Admin")]

        // POST: Users/Supports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Support support = db.Supports.Find(id);
            db.Supports.Remove(support);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]

        #region گزارش مجموع پیغام های خوانده نشده
        /// <summary>
        ///گزارش مجموع پیغام های خوانده نشده
        /// </summary>
        /// <param name="MachinName"></param>
        /// <returns></returns>
        public JsonResult GetMessagesNotRead()

        {
            var NotRead = db.Supports.Where(u => u.SupportStatus == 0).Count();


            //if (LeaderMK == null)
            //{
            //    LeaderMK = "Null";

            //}
            return Json(NotRead, JsonRequestBehavior.AllowGet);
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
