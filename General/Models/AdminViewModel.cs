using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace General.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "سطح دسترسی")]
        public string Name { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }
        //[Required(AllowEmptyStrings = false)]
        [DisplayName(" کد کاربری"), Required(ErrorMessage = " لطفاً فیلد [ {0} ] را وارد نماييد"), TypeConverter("NvarChar(10)"), MaxLength(18, ErrorMessage = " طول فيلد [ {0} ] 18 کاراکتر است"), MinLength(18, ErrorMessage = " طول فيلد [ {0} ] 18 کاراکتر است")]

        public string UserName { get; set; }
        //=============================================================== Counter ==========================================================================
        [DisplayName(" شمارنده ردیف")]

        public int Counter { get; set; }

        [DisplayName("نام"), Required(ErrorMessage = " لطفاً فیلد [ {0} ] را وارد نماييد"), TypeConverter("NvarChar(35)"), StringLength(35, ErrorMessage = "  طول فيلد [ {0} ] 35 کاراکتر است")]
        public string FirstName { get; set; }
        [DisplayName("نام خانوادگی"), Required(ErrorMessage = "  لطفاً فیلد [ {0} ] را وارد نماييد"), TypeConverter("NvarChar(45)"), StringLength(45, ErrorMessage = "  طول فيلد [ {0} ] 45 کاراکتر است")]
        public string LastName { get; set; }
        public string FullName { get; set; }
        [DisplayName("نام پدر"), Required(ErrorMessage = " لطفاً فیلد [ {0} ] را وارد نماييد"), TypeConverter("NvarChar(35)"), StringLength(35, ErrorMessage = "  طول فيلد [ {0} ] 35 کاراکتر است")]

        public string FathersName { get; set; }
        [DisplayName("شماره شناسنامه"), Required(ErrorMessage = " لطفاً فیلد [ {0} ] را وارد نماييد"), TypeConverter("nvarchar(10)"), StringLength(10, ErrorMessage = "  طول فيلد [ {0} ] 10 کاراکتر است")]

        public string BirthCertificateId { get; set; }
        [DisplayName("کد ملی"), Required(ErrorMessage = " لطفاً فیلد [ {0} ] را وارد نماييد"), TypeConverter("NvarChar(10)"), StringLength(10, ErrorMessage = "  طول فيلد [ {0} ] 10 کاراکتر است")]

        public string NationalCode { get; set; }
        [DisplayName("روز تولد"), Required(ErrorMessage = "  لطفاً فیلد [ {0} ] را وارد نماييد")]
        public int? DayofBirth { get; set; }
        [DisplayName("ماه تولد"), Required(ErrorMessage = "  لطفاً فیلد [ {0} ] را وارد نماييد"), TypeConverter("NvarChar(15)"), StringLength(15, ErrorMessage = "  طول فيلد [ {0} ] 15 کاراکتر است")]
        public string MounthofBirth { get; set; }
        [DisplayName("سال تولد"), Required(ErrorMessage = "  لطفاً فیلد [ {0} ] را وارد نماييد"), TypeConverter("NvarChar(4)"), StringLength(4, ErrorMessage = "  طول فيلد [ {0} ] 4 کاراکتر است")]
        public string YearofBirth { get; set; }
        [DisplayName("جنسیت"), Required(ErrorMessage = "  لطفاً فیلد [ {0} ] را وارد نماييد"), TypeConverter("NvarChar(5)"), StringLength(5, ErrorMessage = "  طول فيلد [ {0} ] 5 کاراکتر است")]

        public string Sex { get; set; }
        [DisplayName("آدرس"), Required(ErrorMessage = "  لطفاً فیلد [ {0} ] را وارد نماييد"), TypeConverter("NvarChar(300)"), StringLength(300, ErrorMessage = "  طول فيلد [ {0} ] 300 کاراکتر است")]

        public string Address { get; set; }
        [DisplayName("کد پستی"), Required(ErrorMessage = " لطفاً فیلد [ {0} ] را وارد نماييد")]

        public string PostalCode { get; set; }
        [DisplayName("شماره تلفن ثابت"), Required(ErrorMessage = " لطفاً فیلد [ {0} ] را وارد نماييد"), TypeConverter("NvarChar(13)"), StringLength(13, ErrorMessage = "  طول فيلد [ {0} ] 13 کاراکتر است")]

        public string Phone { get; set; }
        [DisplayName("شماره تلفن موبایل"), Required(ErrorMessage = " لطفاً فیلد [ {0} ] را وارد نماييد"), TypeConverter("NvarChar(13)"), StringLength(13, ErrorMessage = "  طول فيلد [ {0} ] 13 کاراکتر است")]

        public string MobileNumber { get; set; }
        [DisplayName("شماره حساب بانکی"), Required(ErrorMessage = " لطفاً فیلد [ {0} ] را وارد نماييد")]

        public string AcountNumber { get; set; }
        [DisplayName("شماره کارت "), Required(ErrorMessage = " لطفاً فیلد [ {0} ] را وارد نماييد")]

        public string CardNumber { get; set; }
        [DisplayName("نام شعبه بانک"), Required(ErrorMessage = "  لطفاً فیلد [ {0} ] را وارد نماييد"), TypeConverter("NvarChar(50)"), StringLength(50, ErrorMessage = "  طول فيلد [ {0} ] 50 کاراکتر است")]

        public string BranchName { get; set; }
        [DisplayName("کد شعبه"), Required(ErrorMessage = " لطفاً فیلد [ {0} ] را وارد نماييد")]

        public int BranchCode { get; set; }
        [DisplayName("شماره شبا "), Required(ErrorMessage = " لطفاً فیلد [ {0} ] را وارد نماييد")]

        public string ShabaName { get; set; }
        [Required(ErrorMessage = " لطفاً شماره بیمه نامه خود را وارد نمایید")]
        [DisplayName("شماره بیمه نامه"), TypeConverter("NvarChar(2)"), MaxLength(2, ErrorMessage = " طول فيلد [ {0} ] 2 کاراکتر است"), MinLength(2, ErrorMessage = " طول فيلد [ {0} ] 2 کاراکتر است")]
        public string InsuranceNumber1 { get; set; }
        [DisplayName("شماره بیمه"), TypeConverter("NvarChar(5)"), MaxLength(5, ErrorMessage = " طول فيلد [ {0} ] 5 کاراکتر است"), MinLength(5, ErrorMessage = " طول فيلد [ {0} ] 5 کاراکتر است")]
        public string InsuranceNumber2 { get; set; }
        [DisplayName("شماره بیمه"), TypeConverter("NvarChar(1)"), StringLength(1, ErrorMessage = " طول فيلد [ {0} ] 1 کاراکتر است")]
        public string InsuranceNumber3 { get; set; }
        [DisplayName("شماره بیمه"), TypeConverter("NvarChar(1)"), StringLength(1, ErrorMessage = " طول فيلد [ {0} ] 1 کاراکتر است")]
        public string InsuranceNumber4 { get; set; }
        [DisplayName("شماره بیمه"), TypeConverter("NvarChar(4)"), MaxLength(4, ErrorMessage = " طول فيلد [ {0} ] 4 کاراکتر است"), MinLength(4, ErrorMessage = " طول فيلد [ {0} ] 4 کاراکتر است")]
        public string InsuranceNumber5 { get; set; }
        [DisplayName("شماره بیمه"), TypeConverter("NvarChar(5)"), MaxLength(5, ErrorMessage = " طول فيلد [ {0} ] 5 کاراکتر است"), MinLength(5, ErrorMessage = " طول فيلد [ {0} ] 5 کاراکتر است")]
        public string InsuranceNumber6 { get; set; }
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
        //[DisplayName("سطح")/*, Required(ErrorMessage = " لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        //public int? Level { get; set; }
        //======================================================================= Arms  ==========================================================================
        //[DisplayName("بازو")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        //public int? ArmsNum { get; set; }
        //=============================================================== Password ==========================================================================
        [StringLength(100, ErrorMessage = " {0}" + " " + "می بایست حداقل" + " " + "{2}" + " " + "کاراکتر باشد", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عیور")]
        public string Password2 { get; set; }
        //=============================================================== Repeat Password ==========================================================================
        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        [System.Web.Mvc.Compare("Password2", ErrorMessage = " رمزهای عبور یکسان نیستند")]
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
        //=============================================================== DisconnectCooperationStatus ==========================================================================
        [DisplayName("تاریخ قطع همکاری"), TypeConverter("NvarChar(6)")]
        public string DisconnectCooperationDate { get; set; }
        //=============================================================== DisconnectCooperationsCause ==========================================================================
        [DisplayName("علت قطع همکاری"), TypeConverter("NvarChar(500)"), StringLength(500, ErrorMessage = " کاربرگرامی طول فيلد [ {0} ] 500 کاراکتر است")]
        public string DisconnectCooperationCause { get; set; }
        public int? CId { get; set; }
        [DisplayName("نام شهر")]
        public virtual Areas.BasicInformation.Models.City CName { get; set; }

        public int? EId { get; set; }
        public virtual Areas.BasicInformation.Models.Education EName { get; set; }

        public int? SId { get; set; }
        public virtual Areas.BasicInformation.Models.State SName { get; set; }

        //[DisplayName("نام کارخانه")]
        //public int FacId { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "رایانامه")]
        [EmailAddress]
        public string Email { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}