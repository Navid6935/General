using General.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace General.Areas.Users.Controllers
{
    public class ReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //====================================================== متغیر تعداد نفرات در یک بازو
        int ArmsUser = 0;
        //====================================================== متغیر تعداد کل نفرات 
        int AllUserCount = 0;
        //====================================================== شمارنده تعداد سطح
        int Level = 0;

        int LevelMK = 0;
        string[] MarketingCodeArray;
        int[] UserCount;
        //====================================================== لیست کد بازاریابی
        List<string> MarketingCodelist;
        //====================================================== لیست نهایی شمارش اعضای هر یازوی چند نفر
        List<string> PersonInArmsList;

        // GET: Users/Reports
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MountlyCashReports()
        {

            return View();
        }
        public ActionResult CashReportsOnPeriod()
        {

            return View();
        }
        #region گزارش جمع درامد ماهیانه
        /// <summary>
        ///گزارش جمع درامد ماهیانه
        /// </summary>
        /// <param name="MachinName"></param>
        /// <returns></returns>
        public JsonResult GetMontlyReport(int Year, int Month)

        {
            var UserId = Session["Id"];
            var User = db.Users.Where(u => u.Id == UserId).FirstOrDefault();
            IEnumerable<Areas.Users.Models.UsersWallet> usersWallets = new List<Models.UsersWallet>();
            var query = db.UsersWallets
                .Where(m => m.UserId == UserId && m.UWYearDeposit == Year && m.UWMonthDeposit == Month)
                .ToList();
            usersWallets = query.Select(X =>
            new Models.UsersWallet()
            {
                UWMarketingCode = X.UWMarketingCode,
                UWDayDeposit = X.UWDayDeposit,
                UWMonthDeposit = X.UWMonthDeposit,
                UWYearDeposit = X.UWYearDeposit,
                UWAmountDeposit = X.UWAmountDeposit
            });


            //if (LeaderMK == null)
            //{
            //    LeaderMK = "Null";

            //}
            return Json(usersWallets, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region گزارش جمع درامد ماهیانه
        /// <summary>
        ///گزارش جمع درامد زمانی
        /// </summary>
        /// <param name="MachinName"></param>
        /// <returns></returns>
        public JsonResult GetPeriodlyReport(int FromYear, int FromMounth, int ToYear, int ToMounth)

        {
            var UserId = Session["Id"];
            var User = db.Users.Where(u => u.Id == UserId).FirstOrDefault();
            IEnumerable<Areas.Users.Models.UsersWallet> usersWallets = new List<Models.UsersWallet>();
            var query = db.UsersWallets
                .Where(m => m.UserId == UserId && m.UWYearDeposit >= FromYear && m.UWYearDeposit <= ToYear && m.UWMonthDeposit >= FromMounth && m.UWMonthDeposit <= ToMounth)
                .ToList();

            usersWallets = query.Select(X =>
            new Models.UsersWallet()
            {
                UWMarketingCode = X.UWMarketingCode,
                UWDayDeposit = X.UWDayDeposit,
                UWMonthDeposit = X.UWMonthDeposit,
                UWYearDeposit = X.UWYearDeposit,
                UWAmountDeposit = X.UWAmountDeposit
            });


            //if (LeaderMK == null)
            //{
            //    LeaderMK = "Null";

            //}
            return Json(usersWallets, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region گرفتن تعداد بازو

        #endregion
        public ActionResult PersonsInArms(string id)
        {
            //====================================================== شمارنده تعداد سرگروهی یوزر
            int Counter = 0;

            //====================================================== شمارنده تعداد یوزر در هر بازو
            int LeadersUsersCounter = 0;

            //====================================================== بدست آوردن یوزر مورد نظر
            var User = db.Users.Where(i => i.Id == id).FirstOrDefault();
            //====================================================== بدست آوردن کد بازاریابی یوزر (بازوها)
            var MarketingCode = User.MarketingCode;

            //====================================================== بدست آوردن تعداد سرگروهی یوزر
            var CountLeaderMK = db.Users.Where(l => l.LeadersMarketingCode == MarketingCode).ToList();
            UserCount = new int[CountLeaderMK.Count];
            ViewBag.UserCount = new int[CountLeaderMK.Count];
            ViewBag.LevelCount = new int[CountLeaderMK.Count];

            //====================================================== بدست آوردن تعداد سرگروهی یوزر
            if (CountLeaderMK.Count != 0)
            {

                //======================================================( پیدا کردن بازوها ) آرایه شماره سطرهای سرگروهی یوزر
                string[] LeadersMarketingCodeArray = new string[CountLeaderMK.Count];
                //====================================================== حلقه ریختن شماره سطرهای سرگروهی یوزر در آرایه ( جدا کردن بازوها )
                foreach (var item in CountLeaderMK)
                {
                    LeadersMarketingCodeArray[Counter] = item.Id;
                    Counter++;
                }
                for (int i = 0; i < Counter; i++)
                {

                    //======================================================  مقداردهی اولیه تعداد سطح
                    Level = 1;
                    //======================================================  مقداردهی اولیه متغیر تعداد نفرات در بازو
                    ArmsUser = 1;
                    //======================================================  مقداردهی اولیه متغیر تعداد کل نفرات
                    AllUserCount = AllUserCount + 1;
                    //====================================================== شمارنده تعداد یوزرهای بازو
                    int ArmUsersCounter = 0;
                    //======================================================  شماره سطر بازوی ادمین
                    var GetID = LeadersMarketingCodeArray[i];
                    //======================================================  مشخصات  بازوی ادمین
                    var GetLeaderId = db.Users.Where(n => n.Id == GetID).FirstOrDefault();
                    //======================================================  کد بازاریابی بازوی ادمین
                    var GetLeaderMK = db.Users.Where(u => u.MarketingCode == GetLeaderId.MarketingCode).FirstOrDefault();
                    GetMKUsersFunc(GetLeaderMK.MarketingCode);
                    //====================================================== متغیرهای مورد نیاز
                    ViewBag.Counter = Counter;
                    ViewBag.AllUserCount = AllUserCount;
                    UserCount[i] = ArmsUser;
                    ViewBag.UserCount[i] = ArmsUser;
                    ViewBag.LevelCount[i] = Level;
                    if (i - 1 != -1)
                    {
                        if (ViewBag.LevelCount[i] > ViewBag.LevelCount[i - 1])
                        {
                            int HighLevel = ViewBag.LevelCount[i];
                            ViewBag.HighLevel = HighLevel;
                        }
                        if (ViewBag.LevelCount[i] < ViewBag.LevelCount[i - 1])
                        {
                            int HighLevel = ViewBag.LevelCount[i - 1];
                            ViewBag.HighLevel = HighLevel;
                        }
                    }


                }
            }

            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MarketingCode"></param>
        /// <returns></returns>
        public int GetLeaderMKFunc(string MarketingCode)
        {
            var GetMK = db.Users.Where(m => m.LeadersMarketingCode == MarketingCode).ToList();
            var MKCounter = -1;
            if (GetMK.Count != 0 && GetMK.Count > MKCounter++)
            {
                for (int i = 1; i <= GetMK.Count; i++)
                {
                    ArmsUser = ArmsUser + GetMK.Count;
                    //======================================================  آرایه کدهای بازاریابی بازوی بازوی ادمین
                    string[] GetMKArray = new string[GetMK.Count];
                    //======================================================  مقداردهی آرایه کدهای بازاریابی بازوی بازوی ادمین
                    foreach (var item in GetMK)
                    {
                        GetMKArray[MKCounter] = item.MarketingCode;


                        MKCounter++;

                    }
                }
            }
            //if (GetMK.Count == 0)
            //{
            //    GetMK = db.Users.Where(m => m.LeadersMarketingCode == MarketingCode).ToList();
            //}
            return (ArmsUser);
        }
        public int GetMKCountFunc(string MarketingCode)
        {
            var MKCount = db.Users.Where(m => m.LeadersMarketingCode == MarketingCode).ToList();
            return (MKCount.Count);
        }

        public int GetMKUsersFunc(string MarketingCode)
        {
            int ArmsCounter = 0;
            var MKUsers = db.Users.Where(m => m.LeadersMarketingCode == MarketingCode).ToList();
            Level++;
            ArmsUser = ArmsUser + MKUsers.Count;
            AllUserCount = AllUserCount + MKUsers.Count;
            if (MKUsers.Count != 0)
            {
                MarketingCodeArray = new string[MKUsers.Count];
                //======================================================  مقداردهی آرایه کدهای بازاریابی بازوی بازوی ادمین
                foreach (var item in MKUsers)
                {
                    MarketingCodeArray[ArmsCounter] = item.MarketingCode;

                    ArmsCounter++;
                }
                GetMKUsersFunc2(MarketingCodeArray);
            }

            return (ArmsUser);
        }
        public int GetMKUsersFunc2(string[] MKArray)
        {
            int ArmsCounter = 0;
            for (int i = 0; i < MKArray.Length; i++)
            {
                var MK = MKArray[i];
                var MKUsers = db.Users.Where(m => m.LeadersMarketingCode == MK).ToList();
                if (MKUsers.Count != 0)
                {
                    var MKUsersMK = db.Users.Where(m => m.MarketingCode == MK).FirstOrDefault();
                    GetMKUsersFunc(MKUsersMK.MarketingCode);
                }
            }
            return (ArmsUser);
        }
        // GET: Users/Reports/LevelPresonelArms
        public ActionResult LevelPresonelArms()
        {
            return View();
        }
        /// <summary>
        /// تابع تعداد نفرات بر اساس لول فرستاده شده
        /// </summary>
        /// <param name="LevelMK"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public ActionResult FindUserMK(int LevelMK, string UserId)

        {
            //======================================================== بدست آوردن یوزرها از روی آی دی
            var User = db.Users.Where(u => u.UserName == UserId).FirstOrDefault();
            var Counter = 0;
            var GetMKUsersListOnId = db.Users.Where(m => m.LeadersMarketingCode == User.MarketingCode).ToList();
            //======================================================  آرایه کدهای بازاریابی بازوی بازوی ادمین
            string[] GetMKOnIdArray = new string[GetMKUsersListOnId.Count];
            //======================================================  آرایه کدهای بازاریابی بازوی بازوی ادمین
            string[] StatusMkOnIdArray = new string[GetMKUsersListOnId.Count];
            //======================================================  آرایه کدهای بازاریابی بازوی بازوی ادمین
            string[] GetNameOnIdArray = new string[GetMKUsersListOnId.Count];          
            //======================================================  آرایه کدهای بازاریابی بازوی بازوی ادمین
            string[] GetMoblieOnIdArray = new string[GetMKUsersListOnId.Count];
            if (GetMKUsersListOnId.Count != 0)
            {

                //======================================================  مقداردهی آرایه کدهای بازاریابی بازوی بازوی ادمین
                foreach (var item in GetMKUsersListOnId)
                {
                    StatusMkOnIdArray[Counter] = item.MarketingCode;
                    StatusMkOnIdArray[Counter] = item.FullName;
                    StatusMkOnIdArray[Counter] = item.MobileNumber;
                    GetMKOnIdArray[Counter] = item.MarketingCode;
                    

                    //======================================================== تابع بدت آوزدن یوزر از روی کد بازاریابی

                    //GetMKUserListOnMK(GetMKOnIdArray[Counter]);
                    Counter++;

                }
                if (LevelMK == 1)
                {
                    int Counter2 = 0;
                    PersonInArmsList = new List<string>();
                    //====================================================== متغیر تعداد بازوی نرم افزار
                    var UsersArms = db.Arms.FirstOrDefault();
                    var UserArmsNum = UsersArms.ArmsNumber;
                    foreach (var item in GetMKUsersListOnId)
                    {
                        PersonInArmsList.Add(item.ReagentMarketingCode);
                        PersonInArmsList.Add(item.LeadersMarketingCode);
                        PersonInArmsList.Add(item.MarketingCode);
                        //=============================================== اگر مقدار شماره تلفن خالی بود
                        if (item.MobileNumber == null || item.MobileNumber == "")
                        {

                            PersonInArmsList.Add(" ");

                        }
                        else
                        {
                            PersonInArmsList.Add(item.MobileNumber);

                        }
                        //=============================================== اگر مقدار نام خالی بود

                        if (item.FullName == null)
                        {

                            PersonInArmsList.Add(" ");
                        }
                        else
                        {
                            PersonInArmsList.Add(item.FullName);

                        }

                        //=============================================== بدست آوردن بازو  
                        var MKLeaderList = db.Users.Where(m => m.LeadersMarketingCode == item.MarketingCode).ToList();
                        if (MKLeaderList.Count != 0)
                        {
                            for (int n = MKLeaderList.Count; n < UserArmsNum; n++)
                            {
                                PersonInArmsList.Add("0");
                            }
                            foreach (var item2 in MKLeaderList)
                            {
                                var marketingCode = item2.MarketingCode;
                                //=============================================== حلقه ارسال به تابع های بدست آوردن تعداد یوزر به تعداد بازو

                                ArmsUser = 1;

                                GetMKCountArms(item2.MarketingCode);
                                PersonInArmsList.Add(ArmsUser.ToString());
                                Counter2++;
                            }
                        }
                        else
                        {
                            //=============================================== حلقه ارسال به تابع های بدست آوردن تعداد یوزر به تعداد بازو
                            for (int a = 0; a < UserArmsNum; a++)
                            {
                                ArmsUser = 0;
                                PersonInArmsList.Add(ArmsUser.ToString());
                            }
                        }
                        Counter++;
                    }
                    return Json(PersonInArmsList, JsonRequestBehavior.AllowGet);

                }

            
                if (LevelMK > 1)
                {
                    GetMKUsersonLevelFunc(GetMKOnIdArray, LevelMK);

                }

                //if (LevelMK != 1)
                //{
                //}
                //else
                //{
                //    MarketingCodelist = GetMKOnIdArray.ToList();

                //    GetMKListCount(MarketingCodelist);

                //}
                return Json(PersonInArmsList, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Content("این لول وجود ندارد");
            }
            return View();
        }
        /// <summary>
        /// تابع تعداد نفرات بر اساس لول فرستاده شده
        /// </summary>
        /// <param name="LevelMK"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public List<string>  GetMKUsersonLevelFunc(string[] UserId,int levelMK)

        {
            //============================================== شمارنده 
            var Counter = 0;
            //============================================== شمارنده 
            var Counter2 = 0;
            //============================================== تعداد یوزر 
            var userCount = 0;
            //============================================== حلقه پیدا کردن اعضا  لول فرستاده شده
            for (int j = 1; j < levelMK; j++)
            {
                //============================================== مقدار حلقه بر روی یک باشد
                if (j == 1)
                {
                    MarketingCodelist = new List<string>();
                    PersonInArmsList = new List<string>();

                }
                //============================================== مقدار حلقه بر روی یک نباشد
                else
                {
                    UserId = MarketingCodelist.ToArray();
                    MarketingCodelist = new List<string>();
                    PersonInArmsList = new List<string>();

                }
                //============================================== حلقه پیدا کردن اعضا
                for (int i = 0; i < UserId.Length; i++)
            {
                var MK = UserId[i];
                var MKUsers = db.Users.Where(m => m.LeadersMarketingCode == MK).ToList();
                    //====================================================== متغیر تعداد بازوی نرم افزار
                    var UsersArms = db.Arms.FirstOrDefault();
                    var UserArmsNum = UsersArms.ArmsNumber;
                if (MKUsers.Count != 0)
                {
                    foreach (var item in MKUsers)
                        {
                            PersonInArmsList.Add(item.ReagentMarketingCode);
                            PersonInArmsList.Add(item.LeadersMarketingCode);
                            PersonInArmsList.Add(item.MarketingCode);
                            //=============================================== اگر مقدار شماره تلفن خالی بود
                            if (item.MobileNumber == null || item.MobileNumber == "")
                            {

                                PersonInArmsList.Add(" ");

                            }
                            else
                            {
                                PersonInArmsList.Add(item.MobileNumber);

                            }
                            //=============================================== اگر مقدار نام خالی بود

                                if (item.FullName == null)
                            {

                                PersonInArmsList.Add(" ");
                            }
                            else
                            {
                                PersonInArmsList.Add(item.FullName);

                            }
                            //=============================================== لیست کد بازاریابی  

                            MarketingCodelist.Add(item.MarketingCode);
                            //=============================================== بدست آوردن بازو  
                            var MKLeaderList = db.Users.Where(m => m.LeadersMarketingCode == item.MarketingCode).ToList();
                            if (MKLeaderList.Count != 0)
                            {
                                for (int n = MKLeaderList.Count; n < UserArmsNum; n++)
                                {
                                    PersonInArmsList.Add("0");
                                }
                                foreach (var item2 in MKLeaderList)
                                {
                                    var marketingCode = item2.MarketingCode;
                                    //=============================================== حلقه ارسال به تابع های بدست آوردن تعداد یوزر به تعداد بازو

                                        ArmsUser = 1;

                                        GetMKCountArms(item2.MarketingCode);
                                        PersonInArmsList.Add(ArmsUser.ToString());
                                    Counter2++;
                                }
                            }
                            else
                            {
                                //=============================================== حلقه ارسال به تابع های بدست آوردن تعداد یوزر به تعداد بازو
                                for (int a = 0; a < UserArmsNum; a++)
                                {
                                    ArmsUser = 0;
                                    PersonInArmsList.Add(ArmsUser.ToString());
                                }
                            }
                            Counter++;
                    }

                }
            }
            }
            //GetMKListCount(MarketingCodelist);
            return (MarketingCodelist);

        }
        /// <summary>
        /// تابع فرعی بدست آوردن زیرمجموعه و شمردن
        /// </summary>
        /// <param name="MKArray"></param>
        /// <returns></returns>
        public int GetMKListCount(List<string> MKArray)
        {

            for (int i = 0; i < MKArray.Count; i++)
            {
                var ArmsCounter = 0;
                var MK = MKArray[i];
                var MKUsers = db.Users.Where(m => m.LeadersMarketingCode == MK).ToList();
                if (MKUsers.Count != 0)
                {
                    ArmsUser = ArmsUser + MKUsers.Count;
                    MarketingCodeArray = new string[MKUsers.Count];
                    //======================================================  مقداردهی آرایه کدهای بازاریابی بازوی بازوی ادمین
                    foreach (var item in MKUsers)
                    {
                        MarketingCodeArray[ArmsCounter] = item.MarketingCode;

                        ArmsCounter++;
                    }
                    GetMKCountArms(MarketingCodeArray);

                }
            }

            return (ArmsUser);
        }
        public int GetMKCountArms(string[] MKArray)
        
{
            int ArmsCounter = 0;
            for (int i = 0; i < MKArray.Length; i++)
            {
                var MK = MKArray[i];
                var MKUsers = db.Users.Where(m => m.LeadersMarketingCode == MK).ToList();
                if (MKUsers.Count != 0)
                {
                    var MKUsersMK = db.Users.Where(m => m.MarketingCode == MK).FirstOrDefault();
                    GetMKCountArms(MKUsersMK.MarketingCode);

                }

            }

            return (ArmsUser);
        }
        /// <summary>
        /// تابع بدست آورنده اعضای بازو و شمارش
        /// </summary>
        /// <param name="MarketingCode"></param>
        /// <returns></returns>
        public int GetMKCountArms(string MarketingCode)
        {
            int ArmsCounter = 0;
            var MKUsersCountArms = db.Users.Where(m => m.LeadersMarketingCode == MarketingCode).ToList();
            ArmsUser = ArmsUser + MKUsersCountArms.Count;
            if (MKUsersCountArms.Count != 0)
            {
                MarketingCodeArray = new string[MKUsersCountArms.Count];
                //======================================================  مقداردهی آرایه کدهای بازاریابی بازوی بازوی ادمین
                foreach (var item in MKUsersCountArms)
                {
                    MarketingCodeArray[ArmsCounter] = item.MarketingCode;

                    ArmsCounter++;
                }
                GetMKCountArms(MarketingCodeArray);
            }

            return (ArmsUser);
        }

        /// <summary>
        /// تابع بدست آورنده اعضای بازو و شمارش
        /// </summary>
        /// <param name="MarketingCode"></param>
        /// <returns></returns>
        public JsonResult FindArmsNum()
        {
            var UserId = Session["Id"];
            var User = db.Users.Where(u => u.Id == UserId).FirstOrDefault();
            IEnumerable<Areas.Users.Models.UsersWallet> usersWallets = new List<Models.UsersWallet>();
            var query = db.Arms.FirstOrDefault();
            var ArmNum = query.ArmsNumber;
            //if (LeaderMK == null)
            //{
            //    LeaderMK = "Null";

            //}
            return Json(ArmNum, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CommisionPeymented()
        {
            var UserId = Session["PID"];
            if (UserId == null)
            {
                return RedirectToAction("Login", "Account", new { area = ""});
            }
            var User = db.Users.Where(u => u.UserName == UserId).FirstOrDefault();
            var UWGroup = db.UsersWallets
                .Where(m => m.UWMarketingCode == User.MarketingCode)
                .Where(lc => lc.ListCode !=null && lc.FollowUpNO != null)
               .OrderBy(w=>w.UWDatePeyment)
                .ToList();
            return View(UWGroup);
        }
        public ActionResult GetCommisionOnDate( int FromDate, int ToDate)
        {
            var UserId = Session["PID"];
            if (UserId == null)
            {
                return RedirectToAction("Login", "Account", new { area = "" });
            }
            List<string> AllCommisionList = new List<string>();
            var User = db.Users.Where(u => u.UserName == UserId).FirstOrDefault();
            var Commisions = db.UsersWallets
                .Where(m => m.UWMarketingCode == User.MarketingCode)
               .Where(y => y.UWDateWithoutPoints >= FromDate && y.UWDateWithoutPoints <= ToDate)
                .Where(lc => lc.ListCode !=null && lc.FollowUpNO != null)
               .OrderBy(w=>w.UWDatePeyment)
                .ToList();
            if (Commisions.Count != 0)
            {
                foreach (var item in Commisions)
                {
                    AllCommisionList.Add(item.ListCode.ToString());
                    AllCommisionList.Add(item.FollowUpNO);
                    AllCommisionList.Add(item.UWDatePeyment.ToString());
                    AllCommisionList.Add(item.UWFor);
                    AllCommisionList.Add(item.UWAmountDeposit);

                }

            }
            return Json(AllCommisionList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// صفحه اصلی گزارش پورسانت هر تیم یوزر
        /// </summary>
        /// <returns></returns>
        // GET: Users/UsersWallets
        public ActionResult CommissionOnTeamInUsers()
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
    }
}