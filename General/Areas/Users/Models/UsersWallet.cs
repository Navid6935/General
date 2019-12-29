using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace General.Areas.Users.Models
{
    public class UsersWallet
    {
        #region Ctor
        public UsersWallet()
        {
            UWId = Guid.NewGuid();
        }
        #endregion
        #region Props
        [Key, DisplayName("شماره")]

        public Guid UWId { get; set; }
        [DisplayName("کد یوزر"), TypeConverter("NvarChar(100)"), MaxLength(100, ErrorMessage = " طول فيلد [ {0} ] 100 کاراکتر است")]

        public string UserId { get; set; }
        [DisplayName("کد بازاریابی"), TypeConverter("NvarChar(13)"), MaxLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"), MinLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public string UWMarketingCode { get; set; }
        [DisplayName("شماره بیمه نامه"), TypeConverter("NvarChar(18)")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public string UWInsuranceNumber { get; set; }
        [DisplayName("کد بازاریابی ارسال کننده"), TypeConverter("NvarChar(13)"), MaxLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"), MinLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public string UWMarketingCodeFrom { get; set; }
        //=============================================================== FirstName ==========================================================================
        [DisplayName("نام"), /*Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد"), */TypeConverter("NvarChar(35)"), StringLength(35, ErrorMessage = " کاربرگرامی طول فيلد [ {0} ] 35 کاراکتر است")]
        public string UWFirstName { get; set; }
        //=============================================================== LastName ==========================================================================
        [DisplayName("نام خانوادگی"), /*Required(ErrorMessage = " كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد"), */TypeConverter("NvarChar(45)"), StringLength(45, ErrorMessage = " کاربرگرامی طول فيلد [ {0} ] 45 کاراکتر است")]
        public string UWLastName { get; set; }
        [DisplayName("شماره حساب بانکی")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public string UWAcountNumber { get; set; }
        [DisplayName("شماره کارت ")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public string UWCardNumber { get; set; }
        [DisplayName("شماره شبا ")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public string UWShabaId { get; set; }
        [DisplayName("کد شعبه ")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public int UWBranchCode { get; set; }
        [DisplayName("روز واریز")]
        public int UWDayDeposit { get; set; }
        [DisplayName("ماه واریز")]
        public int UWMonthDeposit { get; set; }
        [DisplayName("سال واریز")]
        public int UWYearDeposit { get; set; }
        [DisplayName("مبلغ واریز"), TypeConverter("NvarChar(50)"), StringLength(50, ErrorMessage = "طول فيلد {0} 50 کاراکتر است")]
        public string UWAmountDeposit { get; set; }
        [DisplayName("کد لیست")]
        public int? ListCode { get; set; }
        [DisplayName("شماره پیگیری"), TypeConverter("NvarChar(50)"), StringLength(50, ErrorMessage = "طول فيلد {0} 50 کاراکتر است")]
        public string FollowUpNO { get; set; }
        [DisplayName("تاریخ پرداخت")]
        public int UWDatePeyment { get; set; }
        //[DisplayName("ماه پرداخت")]
        //public int UWMonthPeyment { get; set; }
        //[DisplayName("سال پرداخت")]
        //public int UWYearPeyment { get; set; }
        [DisplayName("بابت"), TypeConverter("NvarChar(50)"), StringLength(50, ErrorMessage = "طول فيلد {0} 50 کاراکتر است")]
        public string UWFor { get; set; }
        [DisplayName("تاریخ بدون ممیز")/*, TypeConverter("NvarChar(8)")*/]
        public int UWDateWithoutPoints { get; set; }

        #endregion
        #region Relations

        #endregion
    }
}