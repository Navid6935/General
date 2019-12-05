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
    [Authorize(Roles = "Admin")]
    public class UsersWalletsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Users/UsersWallets
        public ActionResult Index()
        {
            return View(db.UsersWallets.ToList());
        }

        // GET: Users/UsersWallets/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersWallet usersWallet = db.UsersWallets.Find(id);
            if (usersWallet == null)
            {
                return HttpNotFound();
            }
            return View(usersWallet);
        }

        // GET: Users/UsersWallets/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/UsersWallets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UWId,UserId,UWMarketingCode,UWDayDeposit,UWMonthDeposit,UWYearDeposit,UWAmountDeposit")] UsersWallet usersWallet)
        {
            if (ModelState.IsValid)
            {
                usersWallet.UWId = Guid.NewGuid();
                db.UsersWallets.Add(usersWallet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usersWallet);
        }

        // GET: Users/UsersWallets/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersWallet usersWallet = db.UsersWallets.Find(id);
            if (usersWallet == null)
            {
                return HttpNotFound();
            }
            return View(usersWallet);
        }

        // POST: Users/UsersWallets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UWId,UserId,UWMarketingCode,UWDayDeposit,UWMonthDeposit,UWYearDeposit,UWAmountDeposit")] UsersWallet usersWallet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usersWallet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usersWallet);
        }

        // GET: Users/UsersWallets/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UsersWallet usersWallet = db.UsersWallets.Find(id);
            if (usersWallet == null)
            {
                return HttpNotFound();
            }
            return View(usersWallet);
        }

        // POST: Users/UsersWallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            UsersWallet usersWallet = db.UsersWallets.Find(id);
            db.UsersWallets.Remove(usersWallet);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: Users/UsersWallets
        public ActionResult AllWholeThingPeyments()
        {
            return View(db.UsersWallets
                .Where(m => m.ListCode == null && m.FollowUpNO == null)
                .OrderBy(d => d.UWDateWithoutPoints)
                .ToList());
        }

        /// <summary>
        /// گزارش کل پورسانت های پرداختی
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAllWholes(int FromDate, int ToDate)
        {

            List<string> AllWholeList = new List<string>();
            var query = db.UsersWallets
                .Where(m => m.ListCode == null && m.FollowUpNO == null)
                .ToList();

            query = db.UsersWallets
               .Where(m => m.ListCode == null && m.FollowUpNO == null)
               .Where(y => y.UWDateWithoutPoints >= FromDate && y.UWDateWithoutPoints <= ToDate)
               .OrderBy(m => m.UWDateWithoutPoints)
               //.GroupBy(d => d.UWDateWithoutPoints)
               .ToList();
            //if (FromMounth > ToMounth)
            //{
            //    query = db.UsersWallets

            //       .Where(m => m.ListCode == null && m.FollowUpNO == null)
            //       .Where(y => y.UWYearDeposit >= FromYear && y.UWYearDeposit <= ToYear)
            //       .Where(m => m.UWMonthDeposit <= FromMounth && m.UWMonthDeposit >= ToMounth)
            //       .OrderBy(m => m.UWMonthDeposit)
            //       .ThenBy(y => y.UWYearDeposit)
            //       .ToList();
            //}
            //if (FromMounth == ToMounth)
            //{
            //     query = db.UsersWallets

            //        .Where(m => m.ListCode == null && m.FollowUpNO == null)
            //        .Where(y => y.UWYearDeposit >= FromYear && y.UWYearDeposit <= ToYear)
            //        .OrderBy(m => m.UWMonthDeposit)
            //        .ThenBy(y => y.UWYearDeposit)
            //        .ToList();
            //}

            //ViewBag.SumAllWholes;
            if (query.Count != 0)
            {
                foreach (var item in query)
                {
                    AllWholeList.Add(item.UWMarketingCode);
                    AllWholeList.Add(item.UWFirstName);
                    AllWholeList.Add(item.UWLastName);
                    AllWholeList.Add(item.UWAcountNumber);
                    AllWholeList.Add(item.UWCardNumber);
                    AllWholeList.Add(item.UWShabaId);
                    AllWholeList.Add(item.UWBranchCode.ToString());
                    AllWholeList.Add(item.UWAmountDeposit.ToString());
                    AllWholeList.Add(item.UWId.ToString());
                    ;

                }

            }
            return Json(AllWholeList, JsonRequestBehavior.AllowGet);

        }
        // =========================================================================== پزارش کل پورسانت های پرداختی
        public ActionResult GetAllWholeOnListCodeAndFolowUpNo(int? ListCode, int? FollowUpNO)
        {
            List<string> AllWholeList = new List<string>();
            var query = db.UsersWallets
                .Where(m => m.ListCode != null && m.ListCode == ListCode || m.FollowUpNO != null && m.FollowUpNO == FollowUpNO.ToString())
                .OrderBy(m => m.UWMonthDeposit)
                .ThenBy(y => y.UWYearDeposit)
                .ToList();
            //ViewBag.SumAllWholes = 
            if (query.Count != 0)
            {
                foreach (var item in query)
                {
                    AllWholeList.Add(item.UWMarketingCode);
                    AllWholeList.Add(item.UWFirstName);
                    AllWholeList.Add(item.UWLastName);
                    AllWholeList.Add(item.UWAcountNumber);
                    AllWholeList.Add(item.UWCardNumber);
                    AllWholeList.Add(item.UWYearDeposit.ToString());
                    AllWholeList.Add(item.UWMonthDeposit.ToString());
                    AllWholeList.Add(item.UWDayDeposit.ToString());
                    AllWholeList.Add(item.UWAmountDeposit.ToString());
                    AllWholeList.Add(item.UWId.ToString());
                    AllWholeList.Add(item.ListCode.ToString());
                    AllWholeList.Add(item.FollowUpNO.ToString());


                }

            }
            return Json(AllWholeList, JsonRequestBehavior.AllowGet);

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        #region UpdateUserData

        /// <summary>
        ///آخرین عدد
        /// </summary>
        /// <param name="plantareacode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateAllWholePayment(UsersWallet userwallet)
        {
            if (ModelState.IsValid)
            {
                var AllWholePaymentdata = (from c in db.UsersWallets
                                           where c.UWId == userwallet.UWId
                                           select c).FirstOrDefault();
                AllWholePaymentdata.ListCode = userwallet.ListCode;
                AllWholePaymentdata.FollowUpNO = userwallet.FollowUpNO;
                AllWholePaymentdata.UWDayPeyment = userwallet.UWDayPeyment;
                AllWholePaymentdata.UWMonthPeyment = userwallet.UWMonthPeyment;
                AllWholePaymentdata.UWYearPeyment = userwallet.UWYearPeyment;

                //string ArmsNum = "";
                db.SaveChanges();
            }
            return new EmptyResult();

        }
        #endregion
        // GET: Users/UsersWallets
        public ActionResult AllWholeThingPeymented()
        {
            return View(db.UsersWallets.Where(m => m.ListCode != null && m.FollowUpNO != null).ToList());
        }
        // =========================================================================== گزارش کل پورسانت های پرداخت شده

        public ActionResult GetAllWholePeymented(int FromYear, int FromMounth, int ToYear, int ToMounth)
        {

            List<string> AllWholeList = new List<string>();
            var query = db.UsersWallets
                .Where(m => m.ListCode != null && m.FollowUpNO != null)
                .ToList();
            if (FromMounth < ToMounth)
            {
                query = db.UsersWallets

                   .Where(m => m.ListCode != null && m.FollowUpNO != null)
                   .Where(y => y.UWYearDeposit >= FromYear && y.UWYearDeposit <= ToYear)
                   .Where(m => m.UWMonthDeposit >= FromMounth && m.UWMonthDeposit <= ToMounth)
                   .OrderBy(m => m.UWMonthDeposit)
                   .ThenBy(y => y.UWYearDeposit)
                   .ToList();
            }
            if (FromMounth > ToMounth)
            {
                query = db.UsersWallets

                   .Where(m => m.ListCode != null && m.FollowUpNO != null)
                   .Where(y => y.UWYearDeposit >= FromYear && y.UWYearDeposit <= ToYear)
                   .Where(m => m.UWMonthDeposit <= FromMounth && m.UWMonthDeposit >= ToMounth)
                   .OrderBy(m => m.UWMonthDeposit)
                   .ThenBy(y => y.UWYearDeposit)
                   .ToList();
            }
            if (FromMounth == ToMounth)
            {
                query = db.UsersWallets

                   .Where(m => m.ListCode != null && m.FollowUpNO != null)
                   .Where(y => y.UWYearDeposit >= FromYear && y.UWYearDeposit <= ToYear)
                   .OrderBy(m => m.UWMonthDeposit)
                   .ThenBy(y => y.UWYearDeposit)
                   .ToList();
            }

            //ViewBag.SumAllWholes;
            if (query.Count != 0)
            {
                foreach (var item in query)
                {
                    AllWholeList.Add(item.UWMarketingCode);
                    AllWholeList.Add(item.UWFirstName);
                    AllWholeList.Add(item.UWLastName);
                    AllWholeList.Add(item.UWAcountNumber);
                    AllWholeList.Add(item.UWCardNumber);
                    AllWholeList.Add(item.UWYearDeposit.ToString());
                    AllWholeList.Add(item.UWMonthDeposit.ToString());
                    AllWholeList.Add(item.UWDayDeposit.ToString());
                    AllWholeList.Add(item.UWAmountDeposit.ToString());
                    //AllWholeList.Add(item.UWId.ToString());
                    AllWholeList.Add(item.ListCode.ToString());
                    AllWholeList.Add(item.FollowUpNO.ToString());
                    ;

                }

            }
            return Json(AllWholeList, JsonRequestBehavior.AllowGet);

        }
        // =========================================================================== گزارش کل پورسانت های پرداخت شده

        public ActionResult GetAllWholePeymentedOnMarketingCode(string MarketingCode)
        {

            List<string> AllWholeList = new List<string>();
            var query = db.UsersWallets
                .Where(m => m.ListCode != null && m.FollowUpNO != null)
                .ToList();


            query = db.UsersWallets

               .Where(m => m.ListCode != null && m.FollowUpNO != null)
               .Where(y => y.UWMarketingCode == MarketingCode)
               .OrderBy(m => m.UWMonthDeposit)
               .ThenBy(y => y.UWYearDeposit)
               .ToList();


            //ViewBag.SumAllWholes;
            if (query.Count != 0)
            {
                foreach (var item in query)
                {
                    AllWholeList.Add(item.UWMarketingCode);
                    AllWholeList.Add(item.UWFirstName);
                    AllWholeList.Add(item.UWLastName);
                    AllWholeList.Add(item.UWAcountNumber);
                    AllWholeList.Add(item.UWCardNumber);
                    AllWholeList.Add(item.UWYearPeyment.ToString());
                    AllWholeList.Add(item.UWMonthPeyment.ToString());
                    AllWholeList.Add(item.UWDayPeyment.ToString());
                    AllWholeList.Add(item.UWAmountDeposit.ToString());
                    //AllWholeList.Add(item.UWId.ToString());
                    AllWholeList.Add(item.ListCode.ToString());
                    AllWholeList.Add(item.FollowUpNO.ToString());
                    ;

                }

            }
            return Json(AllWholeList, JsonRequestBehavior.AllowGet);

        }
    }

}
