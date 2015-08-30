using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 微信支付订单信息
    /// </summary>
    public class WechatPayOrderInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long outTradeNo { get; set; }
        /// <summary>
        /// 订单总金额，单位为分
        /// </summary>
        public double totalFee { get; set; }
        /// <summary>
        /// 订单创建时间
        /// </summary>
        public DateTime orderCreateTime { get; set; }
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
        /// 关联编号(实际点单编号)
        /// </summary>
        public long connId { get; set; }
        /// <summary>
        /// 商品描述。参数长度：128 字节以下
        /// </summary>
        public string body { get; set; }
        /// <summary>
        /// 对应的微信订单号,32 个字符内
        /// </summary>
        public string wechatPrePayId { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public long customerId { get; set; }
        /// <summary>
        /// 支付该笔订单的用户 ID
        /// </summary>
        public string openId { get; set; }
        /// <summary>
        /// 付款银行
        /// </summary>
        public string bankType { get; set; }
        /// <summary>
        /// 银行订单号
        /// </summary>
        public string bankBillno { get; set; }
        /// <summary>
        /// 通知 ID
        /// </summary>
        public string notifyId { get; set; }
        /// <summary>
        /// 财付通订单号
        /// </summary>
        public string transactionId { get; set; }
    }

    public class WechatNotify
    {
        public string out_trade_no { get; set; }
        public long wechatOrderConnOrderId { get; set; }
        public int wechatOrderConnOrderType { get; set; }

        public double totalFee{ get; set; }

        public string payAccount{ get; set; } 
    }    
}
