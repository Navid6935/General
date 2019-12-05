using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace General.Areas.Administrator.Models
{
    public class UsersArms
    {
        #region Ctor
        public UsersArms()
        {
           ArmsId = Guid.NewGuid();

        }
        #endregion
        #region Props
        [Key, DisplayName("شماره")]

        public Guid ArmsId { get; set; }
        [DisplayName("کد بازاریابی"), TypeConverter("NvarChar(13)"), MaxLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"), MinLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public string MarketingCode { get; set; }
        [DisplayName("شماره بیمه نامه"), TypeConverter("NvarChar(18)"), MaxLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"), MinLength(13, ErrorMessage = " طول فيلد [ {0} ] 13 کاراکتر است"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public string InsuranceNumber { get; set; }
        [DisplayName("تعداد بازو"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public int ArmsNumber { get; set; }
        #endregion
        #region Relations

        #endregion
    }
}