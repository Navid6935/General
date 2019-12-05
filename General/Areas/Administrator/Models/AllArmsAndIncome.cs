using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace General.Areas.Administrator.Models
{
    public class AllArmsAndIncome
    {
        #region Ctor
        public AllArmsAndIncome()
        {
            ArmAndIncomeId = Guid.NewGuid();

        }
        #endregion
        #region Props
        [Key, DisplayName("شماره")]

        public Guid ArmAndIncomeId { get; set; }
        [DisplayName("تعداد بازو"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public int ArmsNumber { get; set; }
        [DisplayName("سقف درآمد هر بازو"), TypeConverter("NvarChar(50)"), Required(ErrorMessage = "كاربر گرامي ، لطفاً فیلد [ {0} ] را وارد نماييد")]
        public string LimitIncomes { get; set; }
        #endregion
        #region Relations

        #endregion
    }
}