using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using General.Models;
using System.Net.Mail;
using System.Net;
using System.IO;

namespace General.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

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

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (Session["Captcha"] == null || Session["Captcha"].ToString() != model.Captcha)
            {
                ModelState.AddModelError("Captcha", "مجموع اشتباه است");

            }
            if (!ModelState.IsValid)
            {


                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var user = db.Users.Where(u => u.UserName == model.UserName).FirstOrDefault();

           
            //if (user.UserName == "123456789123456789")
            //{
            //    Session["PID"] = model.UserName;
            //    Session["Id"] = user.Id;
            //    Session["Name"] = user.FirstName;
            //    Session["Family"] = user.LastName;

            //    //Session["Name"] = user.FirstName;
            //    //Session["Family"] = user.LastName;
            //    //result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: true);

            //    return RedirectToAction("AdministratorsMainPage", "AdministratorGeneralpages", new { area = "Administrator", Id = user.Id });

            //}
           var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: true);
            //var pass = user.PasswordHash;
            //var pass2 = model.Password;

                switch (result)
                {

                    case SignInStatus.Success:
                        if (user.CooperationStatus == false)
                        {
                            ModelState.AddModelError("", "نام كاربري يا رمز عبور اشتباه مي باشد");
                            return View(model);

                        }
                        if (user.UserName == "483004311139639539")
                        {
                            Session["PID"] = model.UserName;
                            Session["Id"] = user.Id;
                        Session["Name"] = user.FirstName;
                        Session["Family"] = user.LastName;
                        return RedirectToAction("AdminsMainPage", "AdminGeneralpages", new { area = "Administrator", Id = user.Id });
                        }

                        else
                        {
                            Session["PID"] = model.UserName;
                            Session["Id"] = user.Id;
                        Session["Name"] = user.FirstName;
                        Session["Family"] = user.LastName;
                        return RedirectToAction("UserPage", "UsersAdmin", new { area = "Administrator", Id = user.Id });
                        }

                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });

                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "نام كاربري يا رمز عبور اشتباه مي باشد");
                        return View(model);
                

            
            }
        }

        [AllowAnonymous]
        public ActionResult Login2(string returnUrl)
        {
            if (Session["MK"] != null)
            {
                var MK = Session["MK"];
                var user = db.Users.Where(u => u.MarketingCode == MK.ToString()).FirstOrDefault();
                return RedirectToAction("UserMainPage", "GeneralPages", new { area = "Users", Id = user.Id });

            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login2(LoginViewModel2 model)
        {
            if (Session["Captcha"] == null || Session["Captcha"].ToString() != model.Captcha)
            {
                ModelState.AddModelError("Captcha", "مجموع اشتباه است");

            }
            if (!ModelState.IsValid)
            {


                return View(model);
            }
            var user = db.Users.Where(u => u.MarketingCode == model.MarketingCode).FirstOrDefault();

            //if (Session["MK"] != null)
            //{
            //    return RedirectToAction("UserMainPage", "GeneralPages", new { area = "Users", Id = user.Id });

            //}
            //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            //var Insuranse = user.InsuranceNumber1 + user.InsuranceNumber2 + user.InsuranceNumber3 + user.InsuranceNumber4 + user.InsuranceNumber5 + user.InsuranceNumber6;
            if (user.MarketingCode == model.MarketingCode && user.Password2 == model.Password2 /*&& Insuranse == model.InsuranceNumber*/)
            {
                //Session["PId"] = user.UserName;
                Session["MK"] = model.MarketingCode;

                return RedirectToAction("UserMainPage", "GeneralPages", new { area = "Users", Id = user.Id });
            }
            else
            {
                ModelState.AddModelError("", "لطفاً ورودی ها را چک کنید");
                return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }


        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            Session.Abandon();
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
        [AllowAnonymous]

        public ActionResult ForgetPasswordError()
        {
            return View();
        }
        [AllowAnonymous]

        public ActionResult ForgetPasswordSuccess()
        {
            return View();
        }
        [AllowAnonymous]

        public ActionResult ForgetPassword1()
        {
            return View();
        }
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgetPassword1(ForgetPassword1ViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.UserName);
                //if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                //{

                //    // Don't reveal that the user does not exist or is not confirmed
                //    return View("ایمیل رمز عبور شما ارسال گردید");
                //}

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                //======================================================== Send Email ==================================================
                string code1 = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);

                //جهت عدم تغییر توکن در مسیر حتما کد ذیل را قبل از ارسال رایانامه بنویسید
                code1 = System.Web.HttpUtility.UrlEncode(code1);
                Random rnd = new Random();
                var Password = "PNM" + rnd.Next(1000) + user.UserName + "#";
                General.Helpers.Utilities.Email.SendEmailForgotPassword(user.UserName, user.Email, Password, code1);

                //var result = await UserManager.ResetPasswordAsync(user.Id, code1, Password);
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }



            return View(model);
        }
        [AllowAnonymous]

        public ActionResult ForgetPassword2()
        {
            return View();
        }
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgetPassword2(ForgetPassword1ViewModel model)
        {
            var user = db.Users.Where(m => m.MarketingCode == model.MarketingCode && m.UserName == model.UserName).FirstOrDefault();
            if (user == null)
            {
                return Redirect("ForgetPasswordError");
            }
            else
            {
                if (Session["Captcha"] == null || Session["Captcha"].ToString() != model.Captcha)
                {
                    ModelState.AddModelError("Captcha", "مجموع اشتباه است");
                    return View(model);

                }
                if (ModelState.IsValid)
                {
                    General.Areas.Users.Models.Support support = new General.Areas.Users.Models.Support();
                    //db.Supports.Add(model.MarketingCode.ToString());
                    support.SUId = Guid.NewGuid();
                    support.MarketingCode = model.MarketingCode;
                    support.SupportIssue = "فراموش کردن رمز دوم";
                    support.SupportDescription = "مدیریت محترم سایت با سلام اینجانب " + user.FirstName + " " + user.LastName + " " +
                        "به کد بازاریابی" + user.MarketingCode +
                        " " + "رمز عبور دوم خود را فراموش کرده ام";
                    support.SupportResponse = "";
                    support.Image = "b7c3ca6e9e.png";
                    db.Supports.Add(support);

                    db.SaveChanges();
                    return Redirect("ForgetPasswordSuccess");
                }
            }



            return View(model);
        }
        #region Emails
        //++++++++++++++++++++++++++++++++++++++++++++ ايميل ايجاد درخواست - گيرنده درخواست كننده +++++++++++++++++++++++++++++++++++
        public ActionResult SendEmail(string Email)
        {
            //SmtpClient client = new SmtpClient();
            //string Message = string.Format("<p>رمز عبور شما تغییر یافت</p>");
            //string subject = "درخواست خريد خدمات"; // موضوع ایمیل
            //string emailFrom = "navid.soleimani6935@gmail.com";// ایمیل فرستنده//
            //string emailPassword = "Nv910055!"; // رمز ایمیل فرستنده
            //string FromDiplayName = "تغییر رمز عبور"; // نام را که بجای آدرس ایمیل می خواهید نمایش داده شود
            //string emailTo = Email; // ایمیل گیرنده
            //string body = string.Format("<div style=\"font-family:B Homa;font-size:24px;\" class='row' dir=\"rtl\" ><div class='col m12'><img src=\"http://ha.entekhabgroup.com/sys/ItSupport/SiteAssets/EmailSnowaBanner.png\" />{0}</div><div class='col m12'><img src=\"http://ha.entekhabgroup.com/sys/ItSupport/SiteAssets/FooterLine.png\" /></div></div>", Message); // متن ایمیل

            //// مشخص کردن ایمیل فرستنده و گیرنده و همچنین نام مورد نظر برای نمایش بجای ایمیل فرستنده
            //System.Net.Mail.MailMessage oMailMessage = new System.Net.Mail.MailMessage(
            //    new System.Net.Mail.MailAddress(emailFrom, FromDiplayName),
            //    new System.Net.Mail.MailAddress(emailTo));
            ////oMailMessage.Bcc.Add("n.soleimani@haierasa.ir");
            //oMailMessage.Subject = subject; // موضوع ایمیل
            //oMailMessage.Body = body; // متن ایمیل
            //oMailMessage.IsBodyHtml = true; // امکان ارسال تگ های اچ تی ام ال در ایمیل
            //System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com"); // مشخص کردن اس م تی پی
            //smtp.Credentials = new System.Net.NetworkCredential(emailFrom, emailPassword);  // مشخص کردن ایمیل فرستنده و رمز آن

            ////امضا الکترونیکی ایمیل را مشخص میکنیم
            ////سکیوت ساکت لایر -انتقال امن دیتا در جی میل که اولین ارائه کننده این سرویس است رایگان است
            ////اگر جی میل در نظر میگیرید حتما این گزینه را تورو قرار دهید
            //smtp.EnableSsl = true;
            //smtp.Port = 587; // پورت ارسال با ایمیل 
            //smtp.Send(oMailMessage);// ارسال ایمیل

            //ViewBag.Email1 = "ایمیل ارسال گردید";
            //return View("Index");
            using (MailMessage mm = new MailMessage("navid.soleimani6935@gmail.com", "navid.soleimani6935@gmail.com"))
            {
                mm.Subject = "شسیشسی";
                mm.Body = "شسیشسی";

                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("navid.soleimani6935@gmail.com", "Nv910055!");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
                ViewBag.Message = "ایمیل ارسال شد.";
            }
            return View("Index");

        }
        #endregion
        public class GMailer
        {
            public static string GmailUsername { get; set; }
            public static string GmailPassword { get; set; }
            public static string GmailHost { get; set; }
            public static int GmailPort { get; set; }
            public static bool GmailSSL { get; set; }

            public string ToEmail { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public bool IsHtml { get; set; }

            static GMailer()
            {
                GmailHost = "smtp.gmail.com";
                GmailPort = 25; // Gmail can use ports 25, 465 & 587; but must be 25 for medium trust environment.
                GmailSSL = true;
            }

            public void Send()
            {
                SmtpClient smtp = new SmtpClient();
                smtp.Host = GmailHost;
                smtp.Port = GmailPort;
                smtp.EnableSsl = GmailSSL;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(GmailUsername, GmailPassword);
                GMailer.GmailUsername = "navid.soleimani6935@gmail.com";
                GMailer.GmailPassword = "Nv910055!";

                GMailer mailer = new GMailer();
                mailer.ToEmail = "sumitchourasia91@gmail.com";
                mailer.Subject = "Verify your email id";
                mailer.Body = "Thanks for Registering your account.<br> please verify your email id by clicking the link <br> <a href='youraccount.com/verifycode=12323232'>verify</a>";
                mailer.IsHtml = true;
                mailer.Send();
                using (var message = new MailMessage(GmailUsername, ToEmail))
                {
                    message.Subject = Subject;
                    message.Body = Body;
                    message.IsBodyHtml = IsHtml;
                    smtp.Send(message);
                }
            }

        }
    }

}