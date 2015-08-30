using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 支付宝支付订单信息
    /// </summary>
    public class AlipayOrderInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long alipayOrderId { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime orderCreatTime { get; set; }
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
        /// 订单总价
        /// </summary>
        public double totalFee { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string subject { get; set; }
        /// <summary>
        /// 对应的支付宝编号
        /// </summary>
        public string aliTradeNo { get; set; }
        /// <summary>
        /// 买家的邮箱地址
        /// </summary>
        public string aliBuyerEmail { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public long customerId { get; set; }
    }

    public class AlipayNotify
    {
        public string out_trade_no { get; set; }
        public long alipayOrderConnOrderId { get; set; }
        public int alipayOrderConnOrderType { get; set; }

        /// <summary>
        /// 支付帐号
        /// </summary>
        public string buyerEmail { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        public double totalFee { get; set; }
    }
}
