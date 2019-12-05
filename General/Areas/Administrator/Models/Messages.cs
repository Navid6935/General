using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace General.Areas.Administrator.Models
{
    public class Messages
    {
        #region Ctor
        public Messages()
        {
            MId = Guid.NewGuid();
        }

        #endregion
        #region Props
        [Key, DisplayName("شماره")]

        public Guid MId { get; set; }
        [DisplayName("روز")]
        public int Mday { get; set; }
        [DisplayName("ماه")]
        public int MMounth { get; set; }

        [DisplayName("سال")]
        public int MYear { get; set; }

        [DisplayName("موضوع"), TypeConverter("NvarChar(35)"), StringLength(35, ErrorMessage = " طول فيلد [ {0} ] 35 کاراکتر است"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public string MIssue { get; set; }
        [DisplayName("شرح"), TypeConverter("NvarChar(1000)"), StringLength(1000, ErrorMessage = " طول فيلد [ {0} ] 35 کاراکتر است"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public string MDescription { get; set; }

        //[DisplayName("ارسال فایل")]
        //[Display(Name = "ارسال فایل")]
        //[DataType(DataType.ImageUrl)]
        //public string Image { get; set; }
        #endregion
        #region Relations

        #endregion
    }
}