using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace General.Areas.Administrator.Models
{
    public class Installment
    {
        #region Ctor
        public Installment()
        {
            InstallmentId = Guid.NewGuid();
        }
        #endregion
        #region Props
        [Key, DisplayName("شماره")]

        public Guid InstallmentId { get; set; }
        [DisplayName("کد بازاریابی"), TypeConverter("NvarChar(13)"), MaxLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"), MinLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public string MarketingCode { get; set; }
        [DisplayName("نام"), TypeConverter("NvarChar(25)"),StringLength(25,ErrorMessage = "طول فيلد [ {0} ] 25 کاراکتر است"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public string FirstName { get; set; }
        [DisplayName("نام خانوادگی"), TypeConverter("NvarChar(35)"), StringLength(35, ErrorMessage = "طول فيلد [ {0} ] 35 کاراکتر است"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public string LastName { get; set; }
        [DisplayName("شماره بیمه نامه"), TypeConverter("NvarChar(18)"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public string InsuranceNumber { get; set; }
        [DisplayName("مبلغ قسط"), TypeConverter("NvarChar(10)"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        [System.ComponentModel.DataAnnotations.DisplayFormat
            (ApplyFormatInEditMode = false, DataFormatString = "{0:#,##0 ريال}")]
        public string InstallmentsNumbersAmount { get; set; }
        [DisplayName("تعداد اقساط")]
        public int InstallmentsNumbersNum { get; set; }
        [DisplayName("شماره قسط"), TypeConverter("NvarChar(10)")]
        public string InstallmentsNumberId { get; set; }
       [DisplayName("روز")]
        public int DayInstallmentNumber { get; set; }
        [DisplayName("ماه")]
        public int MounthInstallmentNumber { get; set; }
        [DisplayName("سال")]
        public int YearInstallmentNumber { get; set; }
        [DisplayName("وضعیت قسط")]
        public int InstallmentsNumberStatus { get; set; }
        [DisplayName("وضعیت نظر مدیر")]
        public int InstallmentsNumberMansStatus { get; set; }
        [DisplayName("ردیف")]
        public int RowNum { get; set; }
        [DisplayName("تاریخ بدون ممیز")/*, TypeConverter("NvarChar(8)")*/]
        public int DateWithoutPoints { get; set; }
        #endregion
        #region Relations

        #endregion
    }
}