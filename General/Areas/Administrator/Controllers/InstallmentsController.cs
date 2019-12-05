using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using General.Areas.Administrator.Models;
using General.Areas.Users.Models;
using General.Models;

namespace General.Areas.Administrator.Controllers
{
    public class InstallmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [Authorize(Roles = "User,Admin")]
        //[Authorize(Roles = "Admin")]
        // GET: Administrator/Installments
        public ActionResult Index(string id)
        {
            var user = db.Users.Where(u => u.Id == id).FirstOrDefault();
            ViewBag.UserName = user.UserName;
            if (user.UserName == "483004311139639539")
            {
                var installment = db.Installments.ToList().OrderBy(i => i.RowNum);
                return View(installment);

            }
            else
            {
                var installment = db.Installments.Where(i => i.MarketingCode == user.MarketingCode).ToList().OrderBy(i => i.RowNum);
                return View(installment);

            }
        }
        [Authorize(Roles = "Admin")]

        // GET: Administrator/Installments/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Installment installment = db.Installments.Find(id);
            if (installment == null)
            {
                return HttpNotFound();
            }
            return View(installment);
        }
        [Authorize(Roles = "Admin")]

        // GET: Administrator/Installments/Create
        public ActionResult Create(Guid? id)
        {
            var User = db.Users.Where(u => u.Id == id.ToString()).FirstOrDefault();
            //ViewBag.Insurance = User.InsuranceNumber1 + User.InsuranceNumber2 + User.InsuranceNumber3 + User.InsuranceNumber4 + User.InsuranceNumber5 + User.InsuranceNumber6;
            ViewBag.UserId = id;
            ViewBag.Installment = db.Installments.Where(i => i.MarketingCode == User.MarketingCode).OrderBy(m => m.DateWithoutPoints).ToList();
            return View();
        }
        [Authorize(Roles = "Admin")]

        // POST: Administrator/Installments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InstallmentId,MarketingCode,FirstName,LastName,InsuranceNumber,InsuranceNumbersAmount,InsuranceNumbersNum,InsuranceNumberId,InsuranceNumberStatus,InstallmentsNumberMansStatus")] Installment installment)
        {
            if (ModelState.IsValid)
            {
                var RowNum = db.Installments.OrderByDescending(last => last.RowNum).FirstOrDefault();
                installment.InstallmentId = Guid.NewGuid();
                db.Installments.Add(installment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(installment);
        }
        [Authorize(Roles = "Admin")]

        // GET: Administrator/Installments/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Installment installment = db.Installments.Find(id);
            if (installment == null)
            {
                return HttpNotFound();
            }
            return View(installment);
        }
        [Authorize(Roles = "Admin")]

        // POST: Administrator/Installments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InstallmentId,MarketingCode,FirstName,LastName,InsuranceNumber,InstallmentsNumbersAmount,InstallmentsNumbersNum,InstallmentsNumberId,DayInstallmentNumber,MounthInstallmentNumber,YearInstallmentNumber,InsuranceNumberStatus,InstallmentsNumberMansStatus,RowNum,DateWithoutPoints")] Installment installment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(installment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { Id = Session["Id"] });
            }
            return View(installment);
        }
        [Authorize(Roles = "Admin")]

        // GET: Administrator/Installments/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Installment installment = db.Installments.Find(id);
            if (installment == null)
            {
                return HttpNotFound();
            }
            return View();
        }
        [Authorize(Roles = "Admin")]

        // POST: Administrator/Installments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Installment installment = db.Installments.Find(id);
            db.Installments.Remove(installment);
            db.SaveChanges();
            return RedirectToAction("Index", new { Id = Session["Id"] });
        }
        [Authorize(Roles = "Admin")]

        // GET: Administrator/Installments
        public ActionResult CheckPayment()


        {
            var Installment = db.Installments.Where(i => i.InstallmentsNumberStatus == 1 && i.InstallmentsNumberMansStatus == 0).OrderBy(d => d.DateWithoutPoints).ToList();
            ViewBag.InstallmentCount = Installment.Count();
            return View(Installment);
        }
        [Authorize(Roles = "Admin")]

        // GET: Administrator/Installments
        public ActionResult PaymentExelExport()


        {
            return View(db.Installments
                /*OrderBy(y=>y.YearInstallmentNumber).ThenBy(m=>m.MounthInstallmentNumber)*/
                .Where(s => s.InstallmentsNumberStatus == 1 && s.InstallmentsNumberMansStatus == 1).OrderBy(d => d.DateWithoutPoints).ToList());

            ////var Installment = db.Installments.Where(i => i.InstallmentsNumberStatus == 1 && i.InstallmentsNumberMansStatus == 0)
            ////     .Select(x => new { x.MarketingCode, x.FirstName, x.LastName, x.YearInstallmentNumber, x.MounthInstallmentNumber, x.DayInstallmentNumber,x.InstallmentsNumbersAmount })
            ////    .ToList();
            ////GridView gv = new GridView();
            ////gv.DataSource = Installment;
            ////gv.DataBind();
            ////Response.ClearContent();
            ////Response.Buffer = true;
            ////Response.AddHeader("content-disposition", "attachment; filename=" + "گزارش اقساط" + ".xls");
            ////Response.ContentType = "application/ms-excel";
            ////Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
            ////Response.Charset = "";
            ////StringWriter sw = new StringWriter();
            ////HtmlTextWriter htw = new HtmlTextWriter(sw);
            ////gv.RenderControl(htw);
            ////Response.Output.Write(sw.ToString());
            ////Response.Flush();
            ////Response.End();
            //return View();
        }
        [Authorize(Roles = "Admin")]

        // GET: Administrator/Installments
        public ActionResult PaymentNotConfirmExelExport()
        {
            var Installment = db.Installments.Where(i => i.InstallmentsNumberStatus == 1 && i.InstallmentsNumberMansStatus == 0)
                 .Select(x => new { x.MarketingCode, x.FirstName, x.LastName, x.YearInstallmentNumber, x.MounthInstallmentNumber, x.DayInstallmentNumber, x.InstallmentsNumbersAmount })
                .ToList();
            GridView gv = new GridView();
            gv.DataSource = Installment;
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=" + "گزارش اقساط" + ".xls");
            Response.ContentType = "application/ms-excel";
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            gv.RenderControl(htw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            return View("Index");
        }
        [Authorize(Roles = "Admin")]

        public ActionResult ConfirmPayment(Guid? id)
        {

            Installment installment = db.Installments.Find(id);
            installment.InstallmentsNumberMansStatus = 1;
            db.SaveChanges();

            return RedirectToAction("CheckPayment");
        }
        [Authorize(Roles = "Admin")]

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
        #region InsertInstallment
        [Authorize(Roles = "Admin")]

        //==============================================================  درج اقساط===================================================================
        /// <summary>
        /// درج اقساط
        /// </summary>
        /// <param name="installment"></param>
        /// <returns></returns>
        public ActionResult InsertInstallmentData(Installment installment)
        {
            if (ModelState.IsValid)
            {

                var RowNum = db.Installments.OrderByDescending(last => last.RowNum).FirstOrDefault();
                installment.RowNum = RowNum.RowNum + 1;
                string Mounth = installment.MounthInstallmentNumber.ToString();
                string Day = installment.DayInstallmentNumber.ToString();
                if (Mounth.Length == 1)
                {
                    Mounth = "0" + installment.MounthInstallmentNumber.ToString();
                }
                if (Day.Length == 1)
                {
                    Day = "0" + installment.DayInstallmentNumber.ToString();
                }
                installment.DateWithoutPoints = int.Parse(installment.YearInstallmentNumber.ToString() + Mounth + Day);
                db.Installments.Add(installment);
                var user = db.Users.Where(u => u.MarketingCode == installment.MarketingCode).FirstOrDefault();
                //ApplicationUser users = new ApplicationUser();
                user.RegisterStatus = 2;

                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();

            }
            return Json(installment);

        }
        #endregion
        [Authorize(Roles = "Admin")]

        /// <summary>
        /// پیدا کردن کد بازاریابی معرف
        /// </summary>
        /// <param name="MachinName"></param>
        /// <returns></returns>
        public ActionResult FindRegeantMK(string MarketingCode)

        {
            var query = db.Users
                .Where(m => m.MarketingCode == MarketingCode)
                .FirstOrDefault();
            var RegeantStatus = query.ReagentMarketingCode;
            if (RegeantStatus == null || RegeantStatus == "")
            {
                RegeantStatus = null;
            }
            return Json(RegeantStatus, JsonRequestBehavior.AllowGet);
        }
        [Authorize(Roles = "Admin")]

        /// <summary>
        /// واریز سود معرف
        /// </summary>
        /// <param name="disposing"></param>
        #region InsertCommisionRegeant
        public ActionResult InsertCommisionRegeant(UsersWallet userswallet)
        {
            if (ModelState.IsValid)
            {
                var Regeant = db.Users.Where(u => u.MarketingCode == userswallet.UWMarketingCode).FirstOrDefault();
                userswallet.UWDayDeposit = int.Parse(DateTime.Now.ToPeString("dd"));
                userswallet.UWMonthDeposit = int.Parse(DateTime.Now.ToPeString("MM"));
                userswallet.UWYearDeposit = int.Parse(DateTime.Now.ToPeString("yyyy"));
                userswallet.UWAcountNumber = Regeant.AcountNumber;
                userswallet.UWCardNumber = Regeant.CardNumber;
                userswallet.UWShabaId = Regeant.ShabaName;
                userswallet.UWBranchCode = Regeant.BranchCode;
                userswallet.UWFirstName = Regeant.FirstName;
                userswallet.UWLastName = Regeant.LastName;
                userswallet.UserId = Regeant.Id;

                string Mounth = userswallet.UWDayDeposit.ToString();
                string Day = userswallet.UWMonthDeposit.ToString();
                if (Mounth.Length == 1)
                {
                    Mounth = "0" + userswallet.UWMonthDeposit.ToString();
                }
                if (Day.Length == 1)
                {
                    Day = "0" + userswallet.UWDayDeposit.ToString();
                }
                userswallet.UWDateWithoutPoints = int.Parse(userswallet.UWYearDeposit + Mounth + Day);
                db.UsersWallets.Add(userswallet);

                db.SaveChanges();

            }
            return Json(userswallet);

        }
        #endregion
        [Authorize(Roles = "Admin")]

        #region پیدا کردن کد بازاریابی معرف
        /// <summary>
        /// پیدا کردن کد بازاریابی معرف
        /// </summary>
        /// <param name="MachinName"></param>
        /// <returns></returns>
        public ActionResult FindLeaderMK(string MarketingCode)

        {
            var LeaderMK = "";
            var query = db.Users
                .Where(m => m.MarketingCode == MarketingCode)
                .FirstOrDefault();
            if (query.LeadersMarketingCode == null || query.LeadersMarketingCode == "")
            {
                LeaderMK = "Null";

            }
            else
            {
                LeaderMK = query.LeadersMarketingCode;
            }

            return Json(LeaderMK, JsonRequestBehavior.AllowGet);
        }
        #endregion
        [Authorize(Roles = "Admin")]

        #region بدست آوردن لیست قسط ها
        /// <summary>
        /// بدست آوردن لیست قسط ها
        /// </summary>
        /// <param name="MachinName"></param>
        /// <returns></returns>
        public ActionResult GetInstallmentList(int FromDate, int ToDate)

        {
            List<string> InstallmentList = new List<string>();
            var query = db.Installments
                .Where(y => y.DateWithoutPoints >= FromDate && y.DateWithoutPoints <= ToDate)
                .Where(i => i.InstallmentsNumberStatus == 1 && i.InstallmentsNumberMansStatus == 0)
                .OrderBy(d => d.DateWithoutPoints)
                .ToList();
            if (query.Count != 0)
            {
                foreach (var item in query)
                {
                    InstallmentList.Add(item.MarketingCode);
                    InstallmentList.Add(item.FirstName);
                    InstallmentList.Add(item.LastName);
                    InstallmentList.Add(item.InsuranceNumber);
                    InstallmentList.Add(item.InstallmentsNumbersAmount);
                    InstallmentList.Add(item.DayInstallmentNumber.ToString());
                    InstallmentList.Add(item.MounthInstallmentNumber.ToString());
                    InstallmentList.Add(item.YearInstallmentNumber.ToString());
                    InstallmentList.Add(item.InstallmentId.ToString());

                }

            }

            return Json(InstallmentList, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 
        /// <summary>
        /// بدست آوردن لیست قسط ها بر اساس کد بازاریابی
        /// </summary>
        /// <param name="MachinName"></param>
        /// <returns></returns>
        public ActionResult GetInstallmentListOnMarketingCode(string MarketingCode)

        {
            List<string> InstallmentList = new List<string>();
            var query = db.Installments
                .Where(y => y.MarketingCode == MarketingCode)
                .Where(i => i.InstallmentsNumberStatus == 1 && i.InstallmentsNumberMansStatus == 0)
                .OrderBy(d => d.DateWithoutPoints)
                .ToList();
            if (query.Count != 0)
            {
                foreach (var item in query)
                {
                    InstallmentList.Add(item.MarketingCode);
                    InstallmentList.Add(item.FirstName);
                    InstallmentList.Add(item.LastName);
                    InstallmentList.Add(item.InsuranceNumber);
                    InstallmentList.Add(item.InstallmentsNumbersAmount);
                    InstallmentList.Add(item.DayInstallmentNumber.ToString());
                    InstallmentList.Add(item.MounthInstallmentNumber.ToString());
                    InstallmentList.Add(item.YearInstallmentNumber.ToString());
                    InstallmentList.Add(item.InstallmentId.ToString());

                }

            }

            return Json(InstallmentList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 
        /// <summary>
        /// بدست آوردن لیست قسط ها بر اساس شماره بیمه
        /// </summary>
        /// <param name="MachinName"></param>
        /// <returns></returns>
        public ActionResult GetInstallmentListOnInsuranceNum(string InsuranceNum)

        {
            List<string> InstallmentList = new List<string>();
            var query = db.Installments
                .Where(y => y.InsuranceNumber == InsuranceNum)
                .Where(i => i.InstallmentsNumberStatus == 1 && i.InstallmentsNumberMansStatus == 0)
                .OrderBy(d => d.DateWithoutPoints)
                .ToList();
            if (query.Count != 0)
            {
                foreach (var item in query)
                {
                    InstallmentList.Add(item.MarketingCode);
                    InstallmentList.Add(item.FirstName);
                    InstallmentList.Add(item.LastName);
                    InstallmentList.Add(item.InsuranceNumber);
                    InstallmentList.Add(item.InstallmentsNumbersAmount);
                    InstallmentList.Add(item.DayInstallmentNumber.ToString());
                    InstallmentList.Add(item.MounthInstallmentNumber.ToString());
                    InstallmentList.Add(item.YearInstallmentNumber.ToString());
                    InstallmentList.Add(item.InstallmentId.ToString());

                }

            }

            return Json(InstallmentList, JsonRequestBehavior.AllowGet);
        }
        #endregion


        [Authorize(Roles = "Admin")]

        #region 
        /// <summary>
        /// نمایش اقساط تایید شده بر اساس ماه
        /// </summary>
        /// <param name="MachinName"></param>
        /// <returns></returns>
        public ActionResult GetExcelPaymentOnPeriod(int FromDate, int ToDate)

        {
            List<string> InstalmetList = new List<string>();
            var query = db.Installments

                .Where(m => m.InstallmentsNumberStatus == 1 && m.InstallmentsNumberMansStatus == 1)
                .Where(y => y.DateWithoutPoints >= FromDate && y.DateWithoutPoints <= ToDate)
                //.Where(m => m.YearInstallmentNumber >= FromMounth && m.MounthInstallmentNumber <= ToMounth)
                .OrderBy(d => d.DateWithoutPoints)
                //.ThenBy(y => y.MounthInstallmentNumber)
                .ToList();
            if (query.Count != 0)
            {
                foreach (var item in query)
                {
                    InstalmetList.Add(item.MarketingCode);
                    InstalmetList.Add(item.FirstName);
                    InstalmetList.Add(item.LastName);
                    InstalmetList.Add(item.InsuranceNumber);
                    InstalmetList.Add(item.DayInstallmentNumber.ToString());
                    InstalmetList.Add(item.MounthInstallmentNumber.ToString());
                    InstalmetList.Add(item.YearInstallmentNumber.ToString());
                    InstalmetList.Add(item.InstallmentsNumbersNum.ToString());
                    InstalmetList.Add(item.InstallmentsNumbersAmount.ToString());
                    ;

                }

            }
            return Json(InstalmetList, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 
        /// <summary>
        /// نمایش اقساط تایید شده بر اساس ماه
        /// </summary>
        /// <param name="MachinName"></param>
        /// <returns></returns>
        public ActionResult GetListPaymentOnPeriodInIndex(int FromDate, int ToDate)

        {
            List<string> InstalmetList = new List<string>();
            var query = db.Installments

                //.Where(m => m.InstallmentsNumberStatus == 1 && m.InstallmentsNumberMansStatus == 1)
                .Where(y => y.DateWithoutPoints >= FromDate && y.DateWithoutPoints <= ToDate)
                //.Where(m => m.YearInstallmentNumber >= FromMounth && m.MounthInstallmentNumber <= ToMounth)
                .OrderBy(d => d.DateWithoutPoints)
                //.ThenBy(y => y.MounthInstallmentNumber)
                .ToList();
            if (query.Count != 0)
            {
                foreach (var item in query)
                {
                    InstalmetList.Add(item.MarketingCode);
                    InstalmetList.Add(item.FirstName);
                    InstalmetList.Add(item.LastName);
                    InstalmetList.Add(item.InstallmentsNumbersAmount.ToString());
                    InstalmetList.Add(item.InsuranceNumber);
                    InstalmetList.Add(item.DayInstallmentNumber.ToString());
                    InstalmetList.Add(item.MounthInstallmentNumber.ToString());
                    InstalmetList.Add(item.YearInstallmentNumber.ToString());

                    if (item.InstallmentsNumberStatus == 0)
                    {
                        InstalmetList.Add("در انتظار پرداخت");
                    }
                    if (item.InstallmentsNumberStatus == 1)
                    {
                        InstalmetList.Add("پرداخت شده");
                    }
                    if (item.InstallmentsNumberMansStatus == 0)
                    {
                        InstalmetList.Add("در انتظار تایید");
                    }
                    if (item.InstallmentsNumberMansStatus == 1)
                    {
                        InstalmetList.Add("تایید شده	");
                    }
                    InstalmetList.Add(item.InstallmentId.ToString());
                    //InstalmetList.Add(item.InstallmentsNumbersNum.ToString());

                }

            }
            return Json(InstalmetList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 
        /// <summary>
        /// نمایش اقساط تایید شده بر اساس کد بازاریابی
        /// </summary>
        /// <param name="MachinName"></param>
        /// <returns></returns>
        public ActionResult GetExcelPaymentOnPeriodOnMarketingCode(string MarketingCode)

        {
            List<string> InstalmetList = new List<string>();
            var query = db.Installments

                .Where(m => m.InstallmentsNumberStatus == 1 && m.InstallmentsNumberMansStatus == 1)
                .Where(y => y.MarketingCode == MarketingCode)
                //.Where(m => m.YearInstallmentNumber >= FromMounth && m.MounthInstallmentNumber <= ToMounth)
                .OrderBy(d => d.DateWithoutPoints)
                //.ThenBy(y => y.MounthInstallmentNumber)
                .ToList();
            if (query.Count != 0)
            {
                foreach (var item in query)
                {
                    InstalmetList.Add(item.MarketingCode);
                    InstalmetList.Add(item.FirstName);
                    InstalmetList.Add(item.LastName);
                    InstalmetList.Add(item.InsuranceNumber);
                    InstalmetList.Add(item.DayInstallmentNumber.ToString());
                    InstalmetList.Add(item.MounthInstallmentNumber.ToString());
                    InstalmetList.Add(item.YearInstallmentNumber.ToString());
                    InstalmetList.Add(item.InstallmentsNumbersNum.ToString());
                    InstalmetList.Add(item.InstallmentsNumbersAmount.ToString());
                    ;

                }

            }
            return Json(InstalmetList, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 
        /// <summary>
        /// نمایش اقساط تایید شده بر اساس کد بازاریابی در لیست
        /// </summary>
        /// <param name="MachinName"></param>
        /// <returns></returns>
        public ActionResult GetExcelPaymentOnPeriodOnMarketingCodeInIndex(string MarketingCode)

        {
            List<string> InstalmetList = new List<string>();
            var query = db.Installments

                //.Where(m => m.InstallmentsNumberStatus == 1 && m.InstallmentsNumberMansStatus == 1)
                .Where(y => y.MarketingCode == MarketingCode)
                //.Where(m => m.YearInstallmentNumber >= FromMounth && m.MounthInstallmentNumber <= ToMounth)
                .OrderBy(d => d.DateWithoutPoints)
                //.ThenBy(y => y.MounthInstallmentNumber)
                .ToList();
            if (query.Count != 0)
            {
                foreach (var item in query)
                {
                    InstalmetList.Add(item.MarketingCode);
                    InstalmetList.Add(item.FirstName);
                    InstalmetList.Add(item.LastName);
                    InstalmetList.Add(item.InstallmentsNumbersAmount.ToString());
                    InstalmetList.Add(item.InsuranceNumber);
                    InstalmetList.Add(item.DayInstallmentNumber.ToString());
                    InstalmetList.Add(item.MounthInstallmentNumber.ToString());
                    InstalmetList.Add(item.YearInstallmentNumber.ToString());

                    if (item.InstallmentsNumberStatus == 0)
                    {
                        InstalmetList.Add("در انتظار پرداخت");
                    }
                    if (item.InstallmentsNumberStatus == 1)
                    {
                        InstalmetList.Add("پرداخت شده");
                    }
                    if (item.InstallmentsNumberMansStatus == 0)
                    {
                        InstalmetList.Add("در انتظار تایید");
                    }
                    if (item.InstallmentsNumberMansStatus == 1)
                    {
                        InstalmetList.Add("تایید شده	");
                    }
                    InstalmetList.Add(item.InstallmentId.ToString());
                    //InstalmetList.Add(item.InstallmentsNumbersNum.ToString());


                }

            }
            return Json(InstalmetList, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 

        /// <summary>
        /// نمایش اقساط تایید شده بر اساس شماره بیمه نامه
        /// </summary>
        /// <param name="MachinName"></param>
        /// <returns></returns>
        public ActionResult GetExcelPaymentOnPeriodOnInsuranceNum(string InsuranceNum)

        {

            List<string> InstalmetList = new List<string>();
            var query = db.Installments

                .Where(m => m.InstallmentsNumberStatus == 1 && m.InstallmentsNumberMansStatus == 1)
                .Where(y => y.InsuranceNumber == InsuranceNum)
                //.Where(m => m.YearInstallmentNumber >= FromMounth && m.MounthInstallmentNumber <= ToMounth)
                .OrderBy(d => d.DateWithoutPoints)
                //.ThenBy(y => y.MounthInstallmentNumber)
                .ToList();
            if (query.Count != 0)
            {
                foreach (var item in query)
                {
                    InstalmetList.Add(item.MarketingCode);
                    InstalmetList.Add(item.FirstName);
                    InstalmetList.Add(item.LastName);
                    InstalmetList.Add(item.InsuranceNumber);
                    InstalmetList.Add(item.DayInstallmentNumber.ToString());
                    InstalmetList.Add(item.MounthInstallmentNumber.ToString());
                    InstalmetList.Add(item.YearInstallmentNumber.ToString());
                    InstalmetList.Add(item.InstallmentsNumbersNum.ToString());
                    InstalmetList.Add(item.InstallmentsNumbersAmount.ToString());
                    ;

                }

            }
            return Json(InstalmetList, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 

        /// <summary>
        /// نمایش لیست اقساط بر اساس شماره بیمه نامه
        /// </summary>
        /// <param name="MachinName"></param>
        /// <returns></returns>
        public ActionResult GetListInstallmentOnPeriodOnInsuranceNumberInIndex(string InsuranceNum)

        {

            List<string> InstalmetList = new List<string>();
            var query = db.Installments

                //.Where(m => m.InstallmentsNumberStatus == 1 && m.InstallmentsNumberMansStatus == 1)
                .Where(y => y.InsuranceNumber == InsuranceNum)
                //.Where(m => m.YearInstallmentNumber >= FromMounth && m.MounthInstallmentNumber <= ToMounth)
                .OrderBy(d => d.DateWithoutPoints)
                //.ThenBy(y => y.MounthInstallmentNumber)
                .ToList();
            if (query.Count != 0)
            {
                foreach (var item in query)
                {

                    InstalmetList.Add(item.MarketingCode);
                    InstalmetList.Add(item.FirstName);
                    InstalmetList.Add(item.LastName);
                    InstalmetList.Add(item.InstallmentsNumbersAmount.ToString());
                    InstalmetList.Add(item.InsuranceNumber);
                    InstalmetList.Add(item.DayInstallmentNumber.ToString());
                    InstalmetList.Add(item.MounthInstallmentNumber.ToString());
                    InstalmetList.Add(item.YearInstallmentNumber.ToString());

                    if (item.InstallmentsNumberStatus == 0)
                    {
                        InstalmetList.Add("در انتظار پرداخت");
                    }
                    if (item.InstallmentsNumberStatus == 1)
                    {
                        InstalmetList.Add("پرداخت شده");
                    }
                    if (item.InstallmentsNumberMansStatus == 0)
                    {
                        InstalmetList.Add("در انتظار تایید");
                    }
                    if (item.InstallmentsNumberMansStatus == 1)
                    {
                        InstalmetList.Add("تایید شده	");
                    }
                    InstalmetList.Add(item.InstallmentId.ToString());
                    //InstalmetList.Add(item.InstallmentsNumbersNum.ToString());


                }

            }
            return Json(InstalmetList, JsonRequestBehavior.AllowGet);
        }
        #endregion
        [Authorize(Roles = "Admin")]

        public ActionResult FindCommision1Level(int Level)

        {
            var query = db.Percentages.Select(p => p.Commission1).FirstOrDefault();
            return Json(query, JsonRequestBehavior.AllowGet);

        }
        [Authorize(Roles = "Admin")]

        public ActionResult FindCommision2Level(int Level)

        {
            var query = db.Percentages.Select(p => p.Commission2).FirstOrDefault();
            return Json(query, JsonRequestBehavior.AllowGet);

        }
        [Authorize(Roles = "Admin")]
        public ActionResult FindCommision3Level(int Level)

        {
            var query = db.Percentages.Select(p => p.Commission3).FirstOrDefault();
            return Json(query, JsonRequestBehavior.AllowGet);

        }
        [Authorize(Roles = "Admin")]
        public ActionResult FindCommision4Level(int Level)

        {
            var query = db.Percentages.Select(p => p.Commission4).FirstOrDefault();
            return Json(query, JsonRequestBehavior.AllowGet);

        }
        [Authorize(Roles = "Admin")]

        public ActionResult FindCommision5Level(int Level)

        {
            var query = db.Percentages.Select(p => p.Commission4).FirstOrDefault();
            return Json(query, JsonRequestBehavior.AllowGet);

        }
        [Authorize(Roles = "Admin")]

        public ActionResult FindCommision6Level(int Level)

        {
            var query = db.Percentages.Select(p => p.Commission4).FirstOrDefault();
            return Json(query, JsonRequestBehavior.AllowGet);

        }
        [Authorize(Roles = "Admin")]

        public ActionResult FindCommision7Level(int Level)

        {
            var query = db.Percentages.Select(p => p.Commission4).FirstOrDefault();
            return Json(query, JsonRequestBehavior.AllowGet);

        }
        [Authorize(Roles = "Admin")]

        public ActionResult FindCommision8Level(int Level)

        {
            var query = db.Percentages.Select(p => p.Commission4).FirstOrDefault();
            return Json(query, JsonRequestBehavior.AllowGet);

        }
        [Authorize(Roles = "Admin")]

        public ActionResult FindCommision9Level(int Level)

        {
            var query = db.Percentages.Select(p => p.Commission4).FirstOrDefault();
            return Json(query, JsonRequestBehavior.AllowGet);

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
