using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace General.Areas.BasicInformation.Models
{
    public class City 
    {
        #region Ctor
        public City()
        {

        }
        #endregion
        #region Props
        [Key, DisplayName("ردیف")]
        public int CId { get; set; }
        [DisplayName("نام شهر"), TypeConverter("NvarChar(50)"), StringLength(50, ErrorMessage = "کاربرگرامی طول فيلد [ {0} ] 50 کاراکتر است"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public string CName { get; set; }
        #endregion
        #region Relations
        /// <summary>
        /// ارتباط يك به چند با جدول یوزر
        /// </summary>
        public virtual IList<General.Models.RegisterViewModel> registerViewModel { get; set; }
        #endregion
    }
}