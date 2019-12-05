using General.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace General.Areas.Administrator.Controllers
{
    public class UsersAdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        int PageSize = 10;
        int PageIndex = 1;
        public UsersAdminController()
        {
        }

        public UsersAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //
        // GET: /Users/
        [Authorize(Roles = "Admin")]

        // GET: Users/Supports
        public ActionResult Index(int PageIndex = 1)
        {
            int CFull = db.Users.Count();
            ViewBag.Count = CFull;
            ViewBag.PageIndex = PageIndex;
            ViewBag.PageCount = CFull / PageSize;
            var UsersQuery = db.Users
                .OrderByDescending(u=>u.UserName)
                .Skip((PageIndex - 1) * PageSize)
                .Take(PageSize)
                .ToList();
            return View(UsersQuery.ToList());
        }
        [Authorize(Roles = "Admin")]

        //
        // GET: /Users/
        public async Task<ActionResult> IndexDisconnectetCoppertion()
        {
            return View(await UserManager.Users.Where(s => s.CooperationStatus == false).ToListAsync());
        }
        //
        // GET: /Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            return View(user);
        }

        [Authorize(Roles = "Admin")]

        // GET: /Users/Create
        public async Task<ActionResult> Create()
        {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            ViewBag.CId = new SelectList(db.Cities, "CId", "CName");
            ViewBag.SId = new SelectList(db.States, "SId", "SName");
            ViewBag.EId = new SelectList(db.Educations, "EId", "EName");

            return View();
        }
        [Authorize(Roles = "Admin")]

        //
        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(General.Models.RegisterViewModel userViewModel, params string[] selectedRoles)
       {
            if (ModelState.IsValid)
            {
                userViewModel.CooperationStatus = true;
                var user = new ApplicationUser { UserName = userViewModel.UserName,CooperationStatus=true,Email=userViewModel.Email,RegisterStatus= 0
                    ,InsuranceNumber1=userViewModel.InsuranceNumber1,InsuranceNumber2=userViewModel.InsuranceNumber2 ,InsuranceNumber3=userViewModel.InsuranceNumber3
                ,InsuranceNumber4=userViewModel.InsuranceNumber4,InsuranceNumber5=userViewModel.InsuranceNumber5,InsuranceNumber6=userViewModel.InsuranceNumber6};
                var Administratorresult = await UserManager.CreateAsync(user, userViewModel.Password);
                //Add User to the selected Roles 
                if (Administratorresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        //======================================================== Send Email ==================================================
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                        //جهت عدم تغییر توکن در مسیر حتما کد ذیل را قبل از ارسال رایانامه بنویسید
                        code = System.Web.HttpUtility.UrlEncode(code);
                        //ارسال رایانامه
                        var insurance = userViewModel.InsuranceNumber1 + userViewModel.InsuranceNumber2 + userViewModel.InsuranceNumber3 + userViewModel.InsuranceNumber4
                            + userViewModel.InsuranceNumber5 + userViewModel.InsuranceNumber6;
                        General.Helpers.Utilities.Email.SendEmailAfterRegistration(insurance, userViewModel.Email, userViewModel.UserName, userViewModel.Password, code);
                        //======================================================== Save In DataBase =============================================
                        var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);

                        if (!result.Succeeded)
                        {
                            userViewModel.CooperationStatus = true;
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                            //ViewBag.CId = new SelectList(db.Cities, "CId", "CName", userViewModel.CId);
                            //ViewBag.SId = new SelectList(db.States, "SId", "SName", userViewModel.SId);
                            //ViewBag.EId = new SelectList(db.Educations, "EId", "EName", userViewModel.EId);

                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", Administratorresult.Errors.First());
                    ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                    //ViewBag.CId = new SelectList(db.Cities, "CId", "CName", userViewModel.CId);
                    //ViewBag.SId = new SelectList(db.States, "SId", "SName", userViewModel.SId);
                    //ViewBag.EId = new SelectList(db.Educations, "EId", "EName", userViewModel.EId);
                    return View();

                }

                userViewModel.CooperationStatus = true;
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
            ViewBag.CId = new SelectList(db.Cities, "CId", "CName",userViewModel.CId);
            ViewBag.SId = new SelectList(db.States, "SId", "SName", userViewModel.SId);
            ViewBag.EId = new SelectList(db.Educations, "EId", "EName", userViewModel.EId);
            return View();
        }
        [Authorize(Roles = "Admin")]

        //
        // GET: /Users/Create
        public async Task<ActionResult> CreateAdministrator()
        {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");

            return View();
        }
        [Authorize(Roles = "Admin")]

        //
        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> CreateAdministrator(General.Models.RegisterViewModel userViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = userViewModel.UserName,
                    FirstName = userViewModel.FirstName,
                    LastName = userViewModel.LastName
                    ,
                    FullName = userViewModel.FirstName + " " + userViewModel.LastName,
                    FathersName = userViewModel.FathersName,
                    BirthCertificateId = userViewModel.BirthCertificateId,
                    NationalCode = userViewModel.NationalCode
                    //DateofBirth = userViewModel.DateofBirth
                    ,
                    Sex = userViewModel.Sex,
                    CId = userViewModel.CId,
                    SId = userViewModel.SId,
                    EId = userViewModel.EId,
                    Address = userViewModel.Address,
                    Phone = userViewModel.Phone,
                    MobileNumber = userViewModel.MobileNumber,
                    PostalCode = userViewModel.PostalCode
                    ,
                    AcountNumber = userViewModel.AcountNumber,
                    CardNumber = userViewModel.CardNumber,
                    BranchName = userViewModel.BranchName,
                    BranchCode = userViewModel.BranchCode,
                    ShabaName = userViewModel.ShabaName,
                    InsuranceNumber1 = userViewModel.InsuranceNumber1,
                    InsuranceNumber2 = userViewModel.InsuranceNumber2,
                    InsuranceNumber3 = userViewModel.InsuranceNumber3,
                    InsuranceNumber4 = userViewModel.InsuranceNumber4,
                    InsuranceNumber5 = userViewModel.InsuranceNumber5,
                    InsuranceNumber6 = userViewModel.InsuranceNumber6,
                    Email = userViewModel.Email
                };
                var Administratorresult = await UserManager.CreateAsync(user, userViewModel.Password);

                //Add User to the selected Roles 
                if (Administratorresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                            ViewBag.CId = new SelectList(db.Cities, "CId", "CName", userViewModel.CId);
                            ViewBag.SId = new SelectList(db.States, "SId", "SName", userViewModel.SId);
                            ViewBag.EId = new SelectList(db.Educations, "EId", "EName", userViewModel.EId);
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", Administratorresult.Errors.First());
                    ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                    ViewBag.CId = new SelectList(db.Cities, "CId", "CName", userViewModel.CId);
                    ViewBag.SId = new SelectList(db.States, "SId", "SName", userViewModel.SId);
                    ViewBag.EId = new SelectList(db.Educations, "EId", "EName", userViewModel.EId);
                    return View();

                }
                return RedirectToAction("Index");
            }

            return View();
        }
        [Authorize(Roles = "Admin")]

        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);
            ViewBag.InsuranceNumber = user.InsuranceNumber1 + user.InsuranceNumber2 + user.InsuranceNumber3 + user.InsuranceNumber4 + user.InsuranceNumber5 + user.InsuranceNumber6;
            return View(new EditUserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FathersName=user.FathersName,
                BirthCertificateId = user.BirthCertificateId,
                NationalCode = user.NationalCode,
                DayofBirth = user.DayofBirth,
                MounthofBirth = user.MounthofBirth,
                YearofBirth = user.YearofBirth,
                Sex = user.Sex,
                Address = user.Address,
                PostalCode = user.PostalCode,
                Phone = user.Phone,
                MobileNumber = user.MobileNumber,
                AcountNumber = user.AcountNumber,
                CardNumber = user.CardNumber,
                BranchName = user.BranchName,
                BranchCode = user.BranchCode,
                ShabaName = user.ShabaName,
                InsuranceNumber1 = user.InsuranceNumber1,
                InsuranceNumber2 = user.InsuranceNumber2,
                InsuranceNumber3 = user.InsuranceNumber3,
                InsuranceNumber4 = user.InsuranceNumber4,
                InsuranceNumber5 = user.InsuranceNumber5,
                InsuranceNumber6 = user.InsuranceNumber6,
                CName = user.CName,

                  Email = user.Email,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });

        }
        [Authorize(Roles = "Admin")]

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,UserName,FirstName,LastName,FathersName,FullName,BirthCertificateId,NationalCode,DayofBirth,MounthofBirth,YearofBirth,Sex,Email,Address,PostalCode,Phone,MobileNumber,AcountNumber,CardNumber,BranchName,BranchCode,ShabaName,LeadersMarketingCode,ReagentMarketingCode,Level,CId,EId,SId")] EditUserViewModel editUser, params string[] selectedRole)
        {
            //if (ModelState.IsValid)
            //{
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.UserName = editUser.UserName;
                user.FirstName = editUser.FirstName;
                user.LastName = editUser.LastName;
                user.FullName = editUser.FirstName + editUser.LastName;
                user.FathersName = editUser.FathersName;
                user.BirthCertificateId = editUser.BirthCertificateId;
                user.NationalCode = editUser.NationalCode;
                user.DayofBirth = editUser.DayofBirth;
                user.MounthofBirth = editUser.MounthofBirth;
                user.YearofBirth = editUser.YearofBirth;
                user.Sex = editUser.Sex;
                user.Address = editUser.Address;
                user.PostalCode = editUser.PostalCode;
                user.Phone = editUser.Phone;
                user.MobileNumber = editUser.MobileNumber;
                user.Email = editUser.Email;
                user.AcountNumber = editUser.AcountNumber;
                user.CardNumber = editUser.CardNumber;
                user.BranchName = editUser.BranchName;
                user.BranchCode = editUser.BranchCode;
                user.ShabaName = editUser.ShabaName;
                user.InsuranceNumber1 = editUser.InsuranceNumber1;
                user.InsuranceNumber2 = editUser.InsuranceNumber2;
                user.InsuranceNumber3 = editUser.InsuranceNumber3;
                user.InsuranceNumber4 = editUser.InsuranceNumber4;
                user.InsuranceNumber5 = editUser.InsuranceNumber5;
                user.InsuranceNumber6 = editUser.InsuranceNumber6;
                user.Email = editUser.Email;

                var userRoles = await UserManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            //}
            ModelState.AddModelError("", "Something failed.");
            return View();
        }
        [Authorize(Roles = "Admin,User")]

        public async Task<ActionResult> CreateUser(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.CId = new SelectList(db.Cities, "CId", "CName", user.CId);
            ViewBag.SId = new SelectList(db.States, "SId", "SName", user.SId);
            ViewBag.EId = new SelectList(db.Educations, "EId", "EName", user.EId);

            return View(new EditUserViewModel()
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                FathersName = user.FathersName,
                BirthCertificateId = user.BirthCertificateId,
                NationalCode = user.NationalCode,
                DayofBirth = user.DayofBirth,
                MounthofBirth = user.MounthofBirth,
                YearofBirth = user.YearofBirth,
                Sex = user.Sex,
                Address = user.Address,
                PostalCode = user.PostalCode,
                Phone = user.Phone,
                MobileNumber = user.MobileNumber,
                AcountNumber = user.AcountNumber,
                CardNumber = user.CardNumber,
                BranchName = user.BranchName,
                BranchCode = user.BranchCode,
                ShabaName = user.ShabaName,
                InsuranceNumber1 = user.InsuranceNumber1,
                InsuranceNumber2 = user.InsuranceNumber2,
                InsuranceNumber3 = user.InsuranceNumber3,
                InsuranceNumber4 = user.InsuranceNumber4,
                InsuranceNumber5 = user.InsuranceNumber5,
                InsuranceNumber6 = user.InsuranceNumber6,
                CId = user.CId,
                CName = user.CName,
                EId = user.EId,
                SId = user.SId,
                Email = user.Email,

            });
        }
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "User")]
        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUser([Bind(Include = "Id,UserName,FirstName,LastName,FathersName,FullName,BirthCertificateId,NationalCode,DayofBirth,MounthofBirth,YearofBirth,Sex,Email,Address,PostalCode,Phone,MobileNumber,AcountNumber,CardNumber,BranchName,BranchCode,ShabaName,InsuranceNumber1,InsuranceNumber2,InsuranceNumber3,InsuranceNumber4,InsuranceNumber5,InsuranceNumber6,Password2,ConfirmPassword2,MarketingCode,LeadersMarketingCode,ReagentMarketingCode,Level,CId,EId,SId")] EditUserViewModel editUser)
        {
            if (ModelState.IsValid)
            {
                string code = await UserManager.GenerateEmailConfirmationTokenAsync(editUser.Id);

                //جهت عدم تغییر توکن در مسیر حتما کد ذیل را قبل از ارسال رایانامه بنویسید
                code = System.Web.HttpUtility.UrlEncode(code);
                //ارسال رایانامه

                //General.Helpers.Utilities.Email.SendEmailAfterRegistration(editUser.Email, editUser.UserName, editUser.Password, code);
                //======================================================== Save In DataBase =============================================
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.UserName = editUser.UserName;
                user.FirstName = editUser.FirstName;
                user.LastName = editUser.LastName;
                user.FullName = editUser.FirstName + editUser.LastName;
                user.FathersName = editUser.FathersName;
                user.BirthCertificateId = editUser.BirthCertificateId;
                user.NationalCode = editUser.NationalCode;
                user.DayofBirth = editUser.DayofBirth;
                user.MounthofBirth = editUser.MounthofBirth;
                user.YearofBirth = editUser.YearofBirth;

                user.Sex = editUser.Sex;
                user.Address = editUser.Address;
                user.PostalCode = editUser.PostalCode;
                user.Phone = editUser.Phone;
                user.MobileNumber = editUser.MobileNumber;
                user.Email = editUser.Email;
                user.AcountNumber = editUser.AcountNumber;
                user.CardNumber = editUser.CardNumber;
                user.BranchName = editUser.BranchName;
                user.BranchCode = editUser.BranchCode;
                user.ShabaName = editUser.ShabaName;
                user.InsuranceNumber1 = editUser.InsuranceNumber1;
                    user.InsuranceNumber2 = editUser.InsuranceNumber2;
                   user.InsuranceNumber3 = editUser.InsuranceNumber3;
                    user.InsuranceNumber4 = editUser.InsuranceNumber4;
                    user.InsuranceNumber5 = editUser.InsuranceNumber5;
                    user.InsuranceNumber6 = editUser.InsuranceNumber6;      
                user.Email = editUser.Email;
                user.MarketingCode = editUser.MarketingCode;
                user.LeadersMarketingCode = editUser.LeadersMarketingCode;
                user.ReagentMarketingCode = editUser.ReagentMarketingCode;
                user.Password2 = editUser.Password2;
                user.ConfirmPassword2 = editUser.ConfirmPassword2;

                //user.Email = editUser.Email;
                var userRoles = await UserManager.GetRolesAsync(user.Id);


                var result = await UserManager.AddToRolesAsync(user.Id);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id);

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                //db.Entry(editUser).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "مشکلی بوجود آمده است");
            ViewBag.CId = new SelectList(db.Cities, "CId", "CName", editUser.CId);
            ViewBag.SId = new SelectList(db.States, "SId", "SName", editUser.SId);
            ViewBag.EId = new SelectList(db.Educations, "EId", "EName", editUser.EId);
            return View();
        }
        [Authorize(Roles = "Admin")]
        // GET: /Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
        [Authorize(Roles = "User")]
        //[Authorize(Roles = "Admin")]

        public async Task<ActionResult> UserPage()
        {
            return View(await UserManager.Users.ToListAsync());
        }

        #region CkeckMarketingCode

        /// <summary>
        ///     
        /// </summary>
        /// <param name="plantareacode"></param>
        /// <returns></returns>
        //[HttpPost]
        public ActionResult CkeckMarketingCode(string MarketingCode)
        {
            var query = db.Users
                .Where(m => m.MarketingCode == MarketingCode)
                .FirstOrDefault();

            return Json(query.MarketingCode, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region CheckUserRegister

        /// <summary>
        /// چک کردن تعداد بازو
        /// </summary>
        /// <param name="plantareacode"></param>
        /// <returns></returns>
        //[HttpPost]
        public ActionResult CheckUserArms(string LeaderMK)
        {
            //string ArmsNum = "";
            var query = db.Users
                .Where(m => m.LeadersMarketingCode == LeaderMK)
                .OrderByDescending(c=>c.Counter)
                .ToList();

            var CountArmsofUser = query.Count();

            return Json(CountArmsofUser, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region CheckUserRegister

        /// <summary>
        /// چک کردن تعداد بازو
        /// </summary>
        /// <param name="plantareacode"></param>
        /// <returns></returns>
        //[HttpPost]
        public ActionResult CheckUserRegister(string UserId)
        {
            var query = db.Users
                .Where(m => m.UserName == UserId)
                .FirstOrDefault();
            string MarketingCode = "" ;
            if (query.MarketingCode == null || query.MarketingCode == "")
            {
                MarketingCode = "0";
            }
            else
            {
                MarketingCode = query.MarketingCode;
            }
            return Json(MarketingCode, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region ArmsNum

        /// <summary>
        /// تعداد بازو مقرر شده
        /// </summary>
        /// <param name="plantareacode"></param>
        /// <returns></returns>
        //[HttpPost]
        public ActionResult ArmsNum()
        {
            //string ArmsNum = "";
            var query = db.Arms.FirstOrDefault();

            return Json(query.ArmsNumber, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Counter

        /// <summary>
        ///آخرین عدد
        /// </summary>
        /// <param name="plantareacode"></param>
        /// <returns></returns>
        //[HttpPost]
        public ActionResult GetLastCounter()
        {
            //string ArmsNum = "";
            var query = db.Users.
                OrderByDescending(c=>c.Counter).
                FirstOrDefault();

            return Json(query.Counter, JsonRequestBehavior.AllowGet);

        }
        #endregion


        #region GetLastMKInRage

        /// <summary>
        ///آخرین عدد
        /// </summary>
        /// <param name="plantareacode"></param>
        /// <returns></returns>
        //[HttpPost]
        public ActionResult GetLastMKInRage(string marketingcode)
        {
            var LastMK = "0";
            //string ArmsNum = "";
            var query = db.Users
                .Where(m=>m.MarketingCode.Contains(marketingcode))
                .OrderBy(m=>m.MarketingCode)
                .ToList();
            if (query.Count == 0)
            {
                 LastMK = "0000000";
            }
            else
            {
                 LastMK = query.Last().MarketingCode;

            }
            return Json(LastMK, JsonRequestBehavior.AllowGet);

        }
        #endregion
        #region UpdateUserData

        /// <summary>
        ///آخرین عدد
        /// </summary>
        /// <param name="plantareacode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateUserData(ApplicationUser UserData)
        {
            if (ModelState.IsValid)
            { 
                ApplicationUser updateuserdata = (from c in db.Users
                                                  where c.UserName == UserData.UserName
                                                  select c).FirstOrDefault();
                //updateuserdata.UserName = UserData.UserName;
                updateuserdata.FirstName = UserData.FirstName;
                updateuserdata.LastName = UserData.LastName;
                updateuserdata.FullName = UserData.FullName;
                updateuserdata.FathersName = UserData.FathersName;
                updateuserdata.BirthCertificateId = UserData.BirthCertificateId;
                updateuserdata.NationalCode = UserData.NationalCode;
                updateuserdata.Sex = UserData.Sex;
                updateuserdata.Address = UserData.Address;
                updateuserdata.PostalCode = UserData.PostalCode;
                updateuserdata.Phone = UserData.Phone;
                updateuserdata.MobileNumber = UserData.MobileNumber;
                updateuserdata.AcountNumber = UserData.AcountNumber;
                updateuserdata.CardNumber = UserData.CardNumber;
                updateuserdata.BranchName = UserData.BranchName;
                updateuserdata.BranchCode = UserData.BranchCode;
                updateuserdata.ShabaName = UserData.ShabaName;
                updateuserdata.Password2 = UserData.Password2;
                updateuserdata.ConfirmPassword2 = UserData.ConfirmPassword2;
                updateuserdata.MarketingCode = UserData.MarketingCode;
                updateuserdata.LeadersMarketingCode = UserData.LeadersMarketingCode;
                updateuserdata.ReagentMarketingCode = UserData.ReagentMarketingCode;
                updateuserdata.CId = UserData.CId;
                updateuserdata.SId = UserData.SId;
                updateuserdata.EId = UserData.EId;
                updateuserdata.Email = UserData.Email;
                updateuserdata.PhoneNumber = UserData.PhoneNumber;
                updateuserdata.DayofBirth = UserData.DayofBirth;
                updateuserdata.MounthofBirth = UserData.MounthofBirth;
                updateuserdata.YearofBirth = UserData.YearofBirth;
                updateuserdata.InsuranceNumber1 = UserData.InsuranceNumber1;
                updateuserdata.InsuranceNumber2 = UserData.InsuranceNumber2;
                updateuserdata.InsuranceNumber3 = UserData.InsuranceNumber3;
                updateuserdata.InsuranceNumber4 = UserData.InsuranceNumber4;
                updateuserdata.InsuranceNumber5 = UserData.InsuranceNumber5;
                updateuserdata.InsuranceNumber6 = UserData.InsuranceNumber6;
                updateuserdata.RegisterStatus = 1;

            //string ArmsNum = "";
            db.SaveChanges();
        }
            return new EmptyResult();

        }
        #endregion

        #region UpdateUserData
        public ActionResult DisconnectCooperationStatus(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        public ActionResult connectCooperationStatus(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        #endregion
        /// <summary>
        ///آخرین عدد
        /// </summary>
        /// <param name="plantareacode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateUserCoopertionStatusDisconnect(string PID, string MK,string DisconnectCooperationCause)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser updateuserdata = (from c in db.Users
                                                  where c.Id == PID && c.MarketingCode == MK
                                                  select c).FirstOrDefault();
                updateuserdata.CooperationStatus = false;
                updateuserdata.DisconnectCooperationDate = DateTime.Now.ToPeString("yy/MM/dd");
                updateuserdata.DisconnectCooperationCause = DisconnectCooperationCause;
                //string ArmsNum = "";
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        ///آخرین عدد
        /// </summary>
        /// <param name="plantareacode"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateUserCoopertionStatusConnect(string PID, string MK)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser updateuserdata = (from c in db.Users
                                                  where c.Id == PID && c.MarketingCode == MK
                                                  select c).FirstOrDefault();
                updateuserdata.CooperationStatus = true;
                updateuserdata.DisconnectCooperationDate = " ";
                updateuserdata.DisconnectCooperationCause = " ";

                //string ArmsNum = "";
                db.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);

        }
        public ActionResult FindMK(string MK)

        {
            List<string> PersonsStatus = new List<string>();
            var query = db.Users.Where(u => u.MarketingCode == MK).FirstOrDefault();
            PersonsStatus.Add(query.CooperationStatus.ToString());
            PersonsStatus.Add(query.UserName);
            PersonsStatus.Add(query.FirstName);
            PersonsStatus.Add(query.LastName);
            //PersonsStatus.Add(query.FathersName);
            PersonsStatus.Add(query.MarketingCode);
            PersonsStatus.Add(query.InsuranceNumber1);
            PersonsStatus.Add(query.InsuranceNumber2);
            PersonsStatus.Add(query.InsuranceNumber3);
            PersonsStatus.Add(query.InsuranceNumber4);
            PersonsStatus.Add(query.InsuranceNumber5);
            PersonsStatus.Add(query.InsuranceNumber6);
            PersonsStatus.Add(query.Id);
            return Json(PersonsStatus, JsonRequestBehavior.AllowGet);

        }
        public ActionResult FindNewMK()

        {
            List<string> PersonsStatus = new List<string>();
            //RegisterStatus == 1 ==> یوزر کد بازاریابی گرفته است
            var query = db.Users.Where(u => u.RegisterStatus == 1).ToList();
            if (query.Count != 0)
            {
                foreach (var item in query)
                {
                    var Insurance = item.InsuranceNumber1 + item.InsuranceNumber2 + item.InsuranceNumber3 + item.InsuranceNumber4 + item.InsuranceNumber5 + item.InsuranceNumber6;
                    
                    PersonsStatus.Add(item.CooperationStatus.ToString());
                    PersonsStatus.Add(item.UserName);
                    PersonsStatus.Add(item.FirstName);
                    PersonsStatus.Add(item.LastName);
                    //PersonsStatus.Add(query.FathersName);
                    PersonsStatus.Add(item.MarketingCode);
                    PersonsStatus.Add(Insurance);
                    PersonsStatus.Add(item.Id);
                }
                }
            
            return Json(PersonsStatus, JsonRequestBehavior.AllowGet);

        }

        //
        // GET: /Users/Create
        public async Task<ActionResult> ForgetPassword()
        {
            //db.Supports.Add()

            //return RedirectToAction("UserMainPage", "GeneralPages", new { area = "Users" });
            return View();  
        }
    }
}

