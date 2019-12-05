using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace General.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Remember this browser?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = " لطفاً کد کاربری خود را وارد نمایید")]
        [DisplayName("کد کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = " لطفاً رمزعبور خود را وارد نمایید")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }
        [Required(ErrorMessage = " لطفاً {0} را وارد نمایید")]
        [Display(Name = "حاصل جمع")]
        public string Captcha { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
    public class LoginViewModel2
    {
        [Required(ErrorMessage = " لطفاً کد بازاریابی خود را وارد نمایید")]
        [DisplayName("کد بازاریابی")]
        public string MarketingCode { get; set; }
       // [Required(ErrorMessage = " لطفاً شماره بیمه نامه خود را وارد نمایید")]
        [DisplayName("شماره بیمه"),TypeConverter("NvarChar(18)"), MaxLength(18, ErrorMessage = " طول فيلد [ {0} ] 18 کاراکتر است"), MinLength(18, ErrorMessage = " طول فيلد [ {0} ] 18 کاراکتر است")]
        public string InsuranceNumber { get; set; }

        [Required(ErrorMessage = " لطفاً رمزعبور خود را وارد نمایید")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password2 { get; set; }
        [Required(ErrorMessage = " لطفاً {0} را وارد نمایید")]
        [Display(Name = "حاصل جمع")]
        public string Captcha { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
    public class RegisterViewModel
    {
        #region Props
        [Key]
        //=============================================================== UserName ==========================================================================
        [DisplayName(" کد کاربری"), Required(ErrorMessage = " لطفاً فیلد [ {0} ] را وارد نماييد"), TypeConverter("NvarChar(18)"), MaxLength(18, ErrorMessage = " طول فيلد [ {0} ] 18 کاراکتر است"), MinLength(18, ErrorMessage = " طول فيلد [ {0} ] 18 کاراکتر است")]

        public string UserName { get; set; }
        //=============================================================== Counter ==========================================================================
        [DisplayName(" شمارنده ردیف")]

        public int Counter { get; set; }
        //=============================================================== FirstName ==========================================================================
        [DisplayName("نام"), /*Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد"), */TypeConverter("NvarChar(35)"), StringLength(35, ErrorMessage = " کاربرگرامی طول فيلد [ {0} ] 35 کاراکتر است")]
        public string FirstName { get; set; }
        //=============================================================== LastName ==========================================================================
        [DisplayName("نام خانوادگی"), /*Required(ErrorMessage = " كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد"), */TypeConverter("NvarChar(45)"), StringLength(45, ErrorMessage = " کاربرگرامی طول فيلد [ {0} ] 45 کاراکتر است")]
        public string LastName { get; set; }
        //=============================================================== FullName ==========================================================================
        [TypeConverter("NvarChar(80)")]
        public string FullName { get; set; }
        //=============================================================== FathersName ==========================================================================
        [DisplayName("نام پدر"), /*Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد"),*/TypeConverter("NvarChar(35)"), StringLength(35, ErrorMessage = " کاربرگرامی طول فيلد [ {0} ] 35 کاراکتر است")]
        public string FathersName { get; set; }
        //=============================================================== BirthCertificateId ==========================================================================
        [DisplayName("شماره شناسنامه"), /*Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد"),*/ TypeConverter("nvarchar(10)"), StringLength(10, ErrorMessage = " کاربرگرامی طول فيلد [ {0} ] 10 کاراکتر است")]
        public string BirthCertificateId { get; set; }
        //=============================================================== NationalCode ==========================================================================
        [DisplayName("کد ملی"), /*Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد"),*/TypeConverter("NvarChar(10)"), StringLength(10, ErrorMessage = " کاربرگرامی طول فيلد [ {0} ] 10 کاراکتر است")]
        public string NationalCode { get; set; }
        //=============================================================== DayofBirth ==========================================================================
        [DisplayName("روز تولد")/*, Required(ErrorMessage = " كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد") */]
        public int? DayofBirth { get; set; }
        //=============================================================== MounthofBirth ==========================================================================
        [DisplayName("ماه تولد"), /*Required(ErrorMessage = " كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد"),*/TypeConverter("NvarChar(15)"), StringLength(15, ErrorMessage = " کاربرگرامی طول فيلد [ {0} ] 15 کاراکتر است")]
        public string MounthofBirth { get; set; }
        //=============================================================== YearofBirth ==========================================================================
        [DisplayName("سال تولد"), /*Required(ErrorMessage = " كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد"),*/TypeConverter("NvarChar(4)"), StringLength(4, ErrorMessage = " کاربرگرامی طول فيلد [ {0} ] 4 کاراکتر است")]
        public string YearofBirth { get; set; }
        //=============================================================== Sex ==========================================================================
        [DisplayName("جنسیت"), /*Required(ErrorMessage = " كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد"),*/TypeConverter("NvarChar(5)"), StringLength(5, ErrorMessage = " کاربرگرامی طول فيلد [ {0} ] 5 کاراکتر است")]
        public string Sex { get; set; }
        //=============================================================== Address ==========================================================================
        [DisplayName("آدرس"),/* Required(ErrorMessage = " كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد"), */TypeConverter("NvarChar(300)"), StringLength(300, ErrorMessage = " کاربرگرامی طول فيلد [ {0} ] 300 کاراکتر است")]
        public string Address { get; set; }
        //=============================================================== PostalCode ==========================================================================
        [DisplayName("کد پستی")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public string PostalCode { get; set; }
        //=============================================================== Phone Number ==========================================================================
        [DisplayName("شماره تلفن ثابت"), /*Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد"),*/ TypeConverter("NvarChar(13)"), StringLength(13, ErrorMessage = " کاربرگرامی طول فيلد [ {0} ] 13 کاراکتر است")]
        public string Phone { get; set; }
        //=============================================================== Mobile Number ==========================================================================
        [DisplayName("شماره تلفن موبایل"), /*Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد"),*/ TypeConverter("NvarChar(13)"), StringLength(13, ErrorMessage = " کاربرگرامی طول فيلد [ {0} ] 13 کاراکتر است")]
        public string MobileNumber { get; set; }
        //=============================================================== Gmail ==========================================================================
        [EmailAddress(ErrorMessage = "لطفاْ فیلد [{0}] را ‍‍‍‍‍‍به درستی پر کنید")]
        [Display(Name = "جیمیل")]
       [Required(ErrorMessage = "لطفاْ فیلد [{0}] را ‍‍‍‍‍‍بر کنید")]

        public string Email { get; set; }
        //=============================================================== Acount Number ==========================================================================
        [DisplayName("شماره حساب بانکی")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public string AcountNumber { get; set; }
        //=============================================================== Card Number ==========================================================================
        [DisplayName("شماره کارت ")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public string CardNumber { get; set; }

        //=============================================================== Branch Name ==========================================================================
        [DisplayName("نام شعبه بانک"),/* Required(ErrorMessage = " كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد"), */TypeConverter("NvarChar(50)"), StringLength(50, ErrorMessage = " کاربرگرامی طول فيلد [ {0} ] 50 کاراکتر است")]
        public string BranchName { get; set; }
        //=============================================================== Branch Code ==========================================================================
        [DisplayName("کد شعبه")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public int BranchCode { get; set; }
        //=============================================================== ShabaName ==========================================================================
        [DisplayName("شماره شبا ")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public string ShabaName { get; set; }
        //=============================================================== Insurance Number ==========================================================================
        [DisplayName("شماره بیمه نامه")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public string InsuranceNumber1 { get; set; }
        [DisplayName("شماره بیمه نامه")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public string InsuranceNumber2 { get; set; }
        [DisplayName("شماره بیمه نامه")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public string InsuranceNumber3 { get; set; }
        [DisplayName("شماره بیمه نامه")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public string InsuranceNumber4 { get; set; }
        [DisplayName("شماره بیمه نامه")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public string InsuranceNumber5 { get; set; }
        [DisplayName("شماره بیمه نامه")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public string InsuranceNumber6 { get; set; }
        //=============================================================== Password ==========================================================================
        //[Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        [StringLength(100, ErrorMessage = " {0}" + " " + "می بایست حداقل" + " " + "{2}" + " " + "کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عیور")]
        public string Password { get; set; }
        //=============================================================== Repeat Password ==========================================================================
        //[Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [Compare("Password", ErrorMessage = "کاربر گرامی ، رمزهای عبور یکسان نیستند")]
        public string ConfirmPassword { get; set; }
        //=============================================================== Marketing Code ==========================================================================
        [DisplayName("کد بازاریابی"), TypeConverter("NvarChar(13)"), MaxLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"), MinLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"),/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public string MarketingCode { get; set; }
        //=============================================================== Leader Marketing Code ==========================================================================
        [DisplayName("کد بازاریابی سرگروه"), TypeConverter("NvarChar(13)"), MaxLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"), MinLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"),/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public string LeadersMarketingCode { get; set; }
        //=============================================================== Reagent Marketing Code ==========================================================================
        [DisplayName("کد بازاریابی معرف"), TypeConverter("NvarChar(13)"), MaxLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"), MinLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"),/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public string ReagentMarketingCode { get; set; }
        //======================================================================= Level  ==========================================================================
        //[DisplayName("سطح")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        //public int? Level { get; set; }
        //======================================================================= Arms  ==========================================================================
        //[DisplayName("بازو")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        //public int? ArmsNum { get; set; }
        //=============================================================== Password2 ==========================================================================
        [StringLength(100, ErrorMessage = " {0}" + " " + "می بایست حداقل" + " " + "{2}" + " " + "کاراکتر باشد", MinimumLength = 6)]
        //[Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عیور")]
        public string Password2 { get; set; }
        //=============================================================== Repeat Password2 ==========================================================================
        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [Compare("Password2", ErrorMessage = "رمزهای عبور یکسان نیستند")]
        public string ConfirmPassword2 { get; set; }
        //=============================================================== Captcha ==========================================================================
        [Display(Name = "حاصل جمع")]
        public string Captcha { get; set; }
        //=============================================================== Captcha2 ==========================================================================
        [Display(Name = "حاصل جمع")]
        public string Captcha2 { get; set; }
        //=============================================================== CooperationStatus ==========================================================================
        [Display(Name = "وضعیت همکاری")]
        public bool CooperationStatus { get; set; }
        //=============================================================== CooperationStatus ==========================================================================
        [Display(Name = "وضعیت ثبت نام")]
        public int RegisterStatus { get; set; }
        //=============================================================== DisconnectCooperationDate ==========================================================================
        [DisplayName("تاریخ قطع همکاری"), TypeConverter("NvarChar(6)")]
        public string DisconnectCooperationDate { get; set; }
        //=============================================================== DisconnectCooperationsCause ==========================================================================
        [DisplayName("علت قطع همکاری"), TypeConverter("NvarChar(500)"),StringLength(500,ErrorMessage = " کاربرگرامی طول فيلد [ {0} ] 500 کاراکتر است")]
        public string DisconnectCooperationCause { get; set; }
        #endregion
        #region Relation
        /// <summary>
        /// ارتباط یک به چند با جدول شهر
        /// </summary>
        [DisplayName("شماره")]
        public int? CId { get; set; }
        [DisplayName("نام شهر")]
        public virtual Areas.BasicInformation.Models.City CName { get; set; }
        /// <summary>
        /// ارتباط یک به چند با جدول تحصیلات
        /// </summary>
        [DisplayName("شماره")]
        public int? EId { get; set; }
        [DisplayName("نام مدرک")]
        public virtual Areas.BasicInformation.Models.Education EName { get; set; }
        /// <summary>
        /// ارتباط یک به چند با جدول تحصیلات
        /// </summary>
        [DisplayName("شماره")]
        public int? SId { get; set; }
        [DisplayName("نام استان")]
        public virtual Areas.BasicInformation.Models.State SName { get; set; }
        #endregion
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
    public class ForgetPassword1ViewModel
    {
        [DisplayName(" کد کاربری"), Required(ErrorMessage = " لطفاً فیلد [ {0} ] را وارد نماييد"), TypeConverter("NvarChar(10)"), MaxLength(10, ErrorMessage = " طول فيلد [ {0} ] 10 کاراکتر است"), MinLength(10, ErrorMessage = " طول فيلد [ {0} ] 10 کاراکتر است")]

        public string UserName { get; set; }

        [DisplayName("کد بازاریابی"), TypeConverter("NvarChar(13)"), MaxLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"), MinLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"),/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        [Display(Name = "کد بازاریابی")]
        public string MarketingCode { get; set; }

        [Required(ErrorMessage = " لطفاً {0} را وارد نمایید")]
        [Display(Name = "حاصل جمع")]
        public string Captcha { get; set; }

    }
    public class ForgetPassword2ViewModel
    {
        [DisplayName(" کد کاربری"), Required(ErrorMessage = " لطفاً فیلد [ {0} ] را وارد نماييد"), TypeConverter("NvarChar(10)"), MaxLength(10, ErrorMessage = " طول فيلد [ {0} ] 10 کاراکتر است"), MinLength(10, ErrorMessage = " طول فيلد [ {0} ] 10 کاراکتر است")]

        public string UserName { get; set; }

        [DisplayName("کد بازاریابی"), TypeConverter("NvarChar(13)"), MaxLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"), MinLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"),/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        [Display(Name = "کد بازاریابی")]
        public string MarketingCode { get; set; }
        [Required(ErrorMessage = " لطفاً {0} را وارد نمایید")]
        [Display(Name = "حاصل جمع")]
        public string Captcha { get; set; }

    }
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
