using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 用户的Vip信息
    /// </summary>
    public class CustomerVipInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public long customerId { get; set; }
        /// <summary>
        /// 对应公司编号
        /// </summary>
        public int companyId { get; set; }
        /// <summary>
        /// 对应的公司Vip政策编号
        /// </summary>
        public long companyVipId { get; set; }
        /// <summary>
        /// 用户在该公司累计消费的次数
        /// </summary>
        public int userCompletedOrderCount { get; set; }
        /// <summary>
        /// 对应的折扣值（（0-1]）
        /// </summary>
        public double userTotalOrderAmount { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 上次验证点单的时间
        /// </summary>
        public DateTime lastOrderTime { get; set; }
        /// <summary>
        /// 当天验证次数
        /// </summary>
        public int currentDayCount { get; set; }
    }
}
