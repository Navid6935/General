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
            List<string> MarketingCodelist = new List<string>();
            List<int> Amountlist = new List<int>();

            var UWGroup = db.UsersWallets
                .Where(m => m.ListCode == null && m.FollowUpNO == null)
                 .GroupBy(m => new { m.UWMarketingCode })
                 .Select(g => g.FirstOrDefault())
                .ToList();
            //foreach (var item in UWGroup)
            //{

            //    MarketingCodelist.Add(item.UWMarketingCode);
            //    MarketingCodelist.Add(item.UWBranchCode.ToString());
            //    MarketingCodelist.Add(item.UWCardNumber);
            //    GetSumofCommision(item.UWMarketingCode);
            //}
            
            //foreach (var item in MarketingCodelist)
            //{
            //    var sum = db.UsersWallets.tolist()
            //}

            return View(UWGroup);
        }
        // GET: Users/UsersWallets
        public int GetSumofCommision(string MarketingCode,int FromDate,int ToDate)
        {
            int Temp = 0;

            var GetAllCommisionOnMK = db.UsersWallets
                .Where(m => m.ListCode == null && m.FollowUpNO == null)
                .Where(m=>m.UWMarketingCode == MarketingCode)
               .Where(y => y.UWDateWithoutPoints >= FromDate && y.UWDateWithoutPoints <= ToDate)


                .ToList();
            foreach (var item in GetAllCommisionOnMK)
            {
                Temp += int.Parse(item.UWAmountDeposit);
            }
            //foreach (var item in MarketingCodelist)
            //{
            //    var sum = db.UsersWallets.tolist()
            //}
            ViewBag.Temp=Temp;
            return Temp;
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
                .GroupBy(m => new { m.UWMarketingCode })
                 .Select(g => g.FirstOrDefault())
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
                    AllWholeList.Add(GetSumofCommision(item.UWMarketingCode, FromDate,ToDate).ToString());
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
        ///ثبت کد لیست و کد پرداخت
        /// </summary>
        /// <param name="plantareacode"></param>
        /// <returns></returns>
        public ActionResult GetListAllWholePayments(int FromDate, int ToDate,int CodeList,string FollowUpNO,string For)
        {
            //if (ModelState.IsValid)
            //{
                var query = db.UsersWallets
               .Where(m => m.ListCode == null && m.FollowUpNO == null)
                .Where(y => y.UWDateWithoutPoints >= FromDate && y.UWDateWithoutPoints <= ToDate)
                .ToList();
            List<Guid> AllUWIdList = new List<Guid>();
            foreach (var item in query)
            {
                AllUWIdList.Add(item.UWId);
                UpdateAllWholePayments(CodeList, FollowUpNO, For, item.UWId);


            }
            //}
            return Json(true, JsonRequestBehavior.AllowGet);

        }
        #endregion
        // GET: Users/UsersWallets
        public ActionResult AllWholeThingPeymented()
        {
            return View(db.UsersWallets.Where(m => m.ListCode != null && m.FollowUpNO != null).OrderBy(d=>d.UWDatePeyment).ToList());
        }
        // =========================================================================== گزارش کل پورسانت های پرداخت شده

        /// <summary>
        ///ثبت کد لیست و کد پرداخت و بابت
        /// </summary>
        /// <param name="plantareacode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateAllWholePayments(int ListCode ,string FollowUpNO,string For, Guid UWId)
        {
            if (ModelState.IsValid)
            {
                var AllWholePaymentdata = (from c in db.UsersWallets
                                           where c.UWId == UWId
                                           select c).FirstOrDefault();
                var UWDatePeyment =DateTime.Now.ToPeString("yyyy")  +DateTime.Now.ToPeString("MM")+ DateTime.Now.ToPeString("dd");
                AllWholePaymentdata.ListCode = ListCode;
                AllWholePaymentdata.FollowUpNO = FollowUpNO;
                AllWholePaymentdata.UWFor = For;
                AllWholePaymentdata.UWDatePeyment = int.Parse(UWDatePeyment);
                //AllWholePaymentdata.UWMonthPeyment = userwallet.UWMonthPeyment;
                //AllWholePaymentdata.UWYearPeyment = userwallet.UWYearPeyment;

                //string ArmsNum = "";
                db.SaveChanges();
            }
            return new EmptyResult();

        }
        // =========================================================================== گزارش کل پورسانت های پرداخت شده

        /// <summary>
        ///ثبت کد لیست و کد پرداخت
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
                AllWholePaymentdata.UWDatePeyment = userwallet.UWDatePeyment;
                //AllWholePaymentdata.UWMonthPeyment = userwallet.UWMonthPeyment;
                //AllWholePaymentdata.UWYearPeyment = userwallet.UWYearPeyment;

                //string ArmsNum = "";
                db.SaveChanges();
            }
            return new EmptyResult();

        }
        //// GET: Users/UsersWallets
        //public ActionResult AllWholeThingPeymented()
        //{
        //    return View(db.UsersWallets.Where(m => m.ListCode != null && m.FollowUpNO != null).ToList());
        //}
        // =========================================================================== گزارش کل پورسانت های پرداخت شده

        public ActionResult GetAllWholePeymented(int FromDate, int ToDate)
        {

            List<string> AllWholeList = new List<string>();
            var query = db.UsersWallets
                .Where(m => m.ListCode != null && m.FollowUpNO != null)
                .ToList();

                query = db.UsersWallets

                   .Where(m => m.ListCode != null && m.FollowUpNO != null)
                   .Where(y => y.UWDatePeyment >= FromDate && y.UWDatePeyment <= ToDate)
                   .OrderBy(m => m.UWDatePeyment)
                   //.ThenBy(y => y.UWYearDeposit)
                   .ToList();
            

            //ViewBag.SumAllWholes;
            if (query.Count != 0)
            {
                foreach (var item in query)
                {
                    AllWholeList.Add(item.UWMarketingCode);
                    AllWholeList.Add(item.UWInsuranceNumber);
                    AllWholeList.Add(item.UWFirstName);
                    AllWholeList.Add(item.UWLastName);
                    AllWholeList.Add(item.ListCode.ToString());
                    AllWholeList.Add(item.FollowUpNO);
                    AllWholeList.Add(item.UWDatePeyment.ToString());
                    AllWholeList.Add(item.UWFor);
                    AllWholeList.Add(item.UWAmountDeposit.ToString());
                    //AllWholeList.Add(item.UWAmountDeposit.ToString());
                    ////AllWholeList.Add(item.UWId.ToString());
                    //AllWholeList.Add(item.ListCode.ToString());
                    //AllWholeList.Add(item.FollowUpNO.ToString());
                    ;

                }

            }
            return Json(AllWholeList, JsonRequestBehavior.AllowGet);

        }
        // =========================================================================== گزارش کل پورسانت های پرداخت شده بر اساس کد بازاریابی

        public ActionResult GetAllWholePeymentedOnMarketingCode(string MarketingCode)
        {

            List<string> AllWholeList = new List<string>();
            var query = db.UsersWallets
                .Where(m => m.ListCode != null && m.FollowUpNO != null)
                .ToList();


            query = db.UsersWallets

               .Where(m => m.ListCode != null && m.FollowUpNO != null)
               .Where(y => y.UWMarketingCode == MarketingCode)
               .OrderBy(m => m.UWDatePeyment)
               .ToList();


            //ViewBag.SumAllWholes;
            if (query.Count != 0)
            {
                foreach (var item in query)
                {
                    AllWholeList.Add(item.UWMarketingCode);
                    AllWholeList.Add(item.UWInsuranceNumber);
                    AllWholeList.Add(item.UWFirstName);
                    AllWholeList.Add(item.UWLastName);
                    AllWholeList.Add(item.ListCode.ToString());
                    AllWholeList.Add(item.FollowUpNO);
                    AllWholeList.Add(item.UWDatePeyment.ToString());
                    AllWholeList.Add(item.UWFor);
                    AllWholeList.Add(item.UWAmountDeposit.ToString());


                }

            }
            return Json(AllWholeList, JsonRequestBehavior.AllowGet);

        }

        // =========================================================================== گزارش کل پورسانت های پرداخت شده بر اساس شماره بیمه نامه

        public ActionResult GetAllWholePeymentedOnInsuranceNum(string InsuranceNum)
        {

            List<string> AllWholeList = new List<string>();
            var query = db.UsersWallets
                .Where(m => m.ListCode != null && m.FollowUpNO != null)
                .ToList();


            query = db.UsersWallets

               .Where(m => m.ListCode != null && m.FollowUpNO != null)
               .Where(y => y.UWInsuranceNumber == InsuranceNum)
               .OrderBy(m => m.UWDatePeyment)
               .ToList();


            //ViewBag.SumAllWholes;
            if (query.Count != 0)
            {
                foreach (var item in query)
                {
                    AllWholeList.Add(item.UWMarketingCode);
                    AllWholeList.Add(item.UWInsuranceNumber);
                    AllWholeList.Add(item.UWFirstName);
                    AllWholeList.Add(item.UWLastName);
                    AllWholeList.Add(item.ListCode.ToString());
                    AllWholeList.Add(item.FollowUpNO);
                    AllWholeList.Add(item.UWDatePeyment.ToString());
                    AllWholeList.Add(item.UWFor);
                    AllWholeList.Add(item.UWAmountDeposit.ToString());


                }

            }
            return Json(AllWholeList, JsonRequestBehavior.AllowGet);

        }
    }

}
