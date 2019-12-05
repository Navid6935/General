using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace General.Areas.Administrator.Models
{
    public class EducationalPamphlets
    {
        #region Ctor
        public EducationalPamphlets()
        {
            EPId = Guid.NewGuid();
        }

        #endregion
        #region Props
        [Key, DisplayName("شماره")]

        public Guid EPId { get; set; }

        [DisplayName("موضوع"), TypeConverter("NvarChar(35)"), StringLength(35, ErrorMessage = " طول فيلد [ {0} ] 35 کاراکتر است"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public string EPIssue { get; set; }
        [DisplayName("شرح"), TypeConverter("NvarChar(1000)"), StringLength(1000, ErrorMessage = " طول فيلد [ {0} ] 35 کاراکتر است"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public string EPDescription { get; set; }

        [DisplayName("ارسال فایل")]
        [Display(Name = "ارسال فایل")]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }
        #endregion
        #region Relations

        #endregion
    }
}