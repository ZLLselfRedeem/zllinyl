using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VAGastronomistMobileApp.Model
{
    public class UnionPayInfo
    {

        /// <summary>
        /// 商户订单号(12-32位数字、字母)
        /// </summary>
        public string merchantOrderId { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime merchantOrderTime { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime orderPayTime { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public VAAlipayOrderStatus orderStatus { get; set; }
        /// <summary>
        /// 支付订单与19点订单关联类型
        /// </summary>
        public VAPayOrderType conn19dianOrderType { get; set; }
        /// <summary>
        /// 关联编号
        /// </summary>
        public long connId { get; set; }
        /// <summary>
        /// 订单金额：单位为分。1为1分，10为1角；该处请不要出现特殊符号，例如“.”
        /// </summary>
        public double merchantOrderAmt { get; set; }
        /// <summary>
        /// 商品内容描述
        /// </summary>
        public string merchantOrderDesc { get; set; }
        /// <summary>
        /// 商品银联流水号
        /// </summary>
        public string cupsQid { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public long customerId { get; set; }
    }
}