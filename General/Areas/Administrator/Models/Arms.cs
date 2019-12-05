using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace General.Areas.Administrator.Models
{
    public class Arms
    {
        #region Ctor
        public Arms()
        {
            ArmId = Guid.NewGuid();
        }
        #endregion
        #region Props
        [Key, DisplayName("شماره")]

        public Guid ArmId { get; set; }

        [DisplayName("تعداد بازو"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public int ArmsNumber { get; set; }
        #endregion
        #region Relations

        #endregion
    }
}