using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 常用沽清
    /// </summary>
    public class CommonCurrentSellOffInfo
    {
        public int Id { set; get; }
        /// <summary>
        /// 菜谱ID
        /// </summary>
        public int menuId { get; set; }
        /// <summary>
        /// 菜ID
        /// </summary>
        public int DishI18nID { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 沽清次数
        /// </summary>
        public int currentSellOffCount { get; set; }
        /// <summary>
        /// 规格对应Id
        /// </summary>
        public int DishPriceI18nID { get; set; }
    }
}
