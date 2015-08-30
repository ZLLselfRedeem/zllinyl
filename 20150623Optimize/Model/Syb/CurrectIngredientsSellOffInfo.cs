using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 配菜沽清模型层
    /// </summary>
    public class CurrectIngredientsSellOffInfo
    {
        public long Id { get; set; }
        public int ingredientsId { get; set; }
        public int shopId { get; set; }
        public int companyId { get; set; }
        public bool status { get; set; }
        public DateTime expirationTime { get; set; }
        public DateTime operateTime { get; set; }
        public int operateEmployeeId { get; set; }
    }
}
