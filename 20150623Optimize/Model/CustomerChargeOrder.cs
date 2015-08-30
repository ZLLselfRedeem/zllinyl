using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 用户的充值订单信息
    /// </summary>
    public class CustomerChargeOrder
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime payTime { get; set; }
        /// <summary>
        /// 优惠券订单状态
        /// </summary>
        public VACustomerChargeOrderStatus status { get; set; }
        /// <summary>
        /// 用户的Cookie
        /// </summary>
        public string customerCookie { get; set; }
        /// <summary>
        /// 用户的设备编号
        /// </summary>
        public string customerUUID { get; set; }
        /// <summary>
        /// 订单总价
        /// </summary>
        public double priceSum { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public long customerId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string subjectName { get; set; }
    }
}
