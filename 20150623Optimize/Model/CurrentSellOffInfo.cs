using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 沽清
    /// </summary>
    public class CurrentSellOffInfo
    {
        public int Id { set; get; }

        /// <summary>
        /// 公司ID
        /// </summary>
        public int companyId { get; set; }
        /// <summary>
        /// 门店ID
        /// </summary>
        public int shopId { get; set; }
        /// <summary>
        /// 菜谱ID
        /// </summary>
        public int menuId { get; set; }
        /// <summary>
        /// 菜ID
        /// </summary>
        public int DishI18nID { get; set; }
        /// <summary>
        /// 状态（沽清，取消沽清）
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 规格对应Id
        /// </summary>
        public int DishPriceI18nID { get; set; }
        /// <summary>
        /// 沽清过期时间
        /// </summary>
        public DateTime expirationTime { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime operateTime { get; set; }
        /// <summary>
        /// 操作员工编号
        /// </summary>
        public int operateEmployeeId { get; set; }
    }
}
