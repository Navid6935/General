using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace General.Areas.Users.Models
{
    public class Support
    {
        #region Ctor
        public Support()
        {
            SUId = Guid.NewGuid();

        }
        #endregion
        #region Props
        [Key, DisplayName("شماره")]

        public Guid SUId { get; set; }
        //=============================================================== Marketing Code ==========================================================================
        [DisplayName("کد بازاریابی"), TypeConverter("NvarChar(13)"), StringLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است")/*, Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")*/]
        public string MarketingCode { get; set; }

        [DisplayName("موضوع"), TypeConverter("NvarChar(35)"), StringLength(35, ErrorMessage = " طول فيلد [ {0} ] 35 کاراکتر است"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public string SupportIssue { get; set; }
        [DisplayName("شرح"), TypeConverter("NvarChar(1000)"), StringLength(1000, ErrorMessage = " طول فيلد [ {0} ] 35 کاراکتر است"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public string SupportDescription { get; set; }
        [DisplayName("پاسخ"), TypeConverter("NvarChar(1000)"), StringLength(1000, ErrorMessage = " طول فيلد [ {0} ] 35 کاراکتر است")]
        public string SupportResponse { get; set; }
        [DisplayName("وضعیت")]
        public int? SupportStatus { get; set; }
        [DisplayName("ارسال فایل")]
        [Display(Name = "ارسال فایل")]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }
        #endregion
        #region Relations

        #endregion
    }
}