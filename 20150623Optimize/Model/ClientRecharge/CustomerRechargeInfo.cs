using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 客户充值记录
    /// 创建日期：2014-5-4
    /// </summary>
    public class CustomerRechargeInfo
    {
        /// <summary>
        /// 记录ID
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 客户ID
        /// </summary>
        public long customerId { get; set; }
        /// <summary>
        /// 客户cookie
        /// </summary>
        public string customerCookie { get; set; }
        /// <summary>
        /// 客户UUID
        /// </summary>
        public string customerUUID { get; set; }
        /// <summary>
        /// 充值活动ID
        /// </summary>
        public int rechargeId { get; set; }
        /// <summary>
        /// 店铺ID（非必填）
        /// </summary>
        public int shopId { get; set; }
        /// <summary>
        /// 点单ID（非必填）
        /// </summary>
        public long preOrder19dianId { get; set; }
        /// <summary>
        /// 创建充值时间
        /// </summary>
        public DateTime rechargeTime { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        public int payStatus { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime payTime { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public int payMode { get; set; }
        /// <summary>
        /// 充值金额
        /// </summary>
        public double rechargeCondition { get; set; }
        /// <summary>
        /// 赠送金额
        /// </summary>
        public double rechargePresent { get; set; }
    }
}
