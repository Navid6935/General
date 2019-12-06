using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace General.Areas.Administrator.Models
{
    public class CommisionOnArms
    {
        #region Ctor
        public CommisionOnArms()
        {

        }
        public CommisionOnArms(int caid,string caamount)
        {
            this.CAId = caid;
            this.CAAmount = caamount;
        }
        #endregion
        #region Props
        [Key, DisplayName("شماره")]
        public int CAId { get; set; }
        [DisplayName("مبلغ حد کمیسیون"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public string CAAmount { get; set; }
        #endregion
        #region Relations

        #endregion
    }
}