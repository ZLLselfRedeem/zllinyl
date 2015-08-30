using System;

namespace VAGastronomistMobileApp.Model
{
    public class TenpayRefundOrder
    {
        public TenpayRefundOrder()
        {
            OutRefundId = "";
            WechatPrePayId = "";
            RefundId = "";
            OpUserId = "";
            RecvUserId = "";
            ReccvUserName = "";
        }

        /// <summary>
        /// 获取或设置订单ID
        /// </summary>
        public long PreOrder19dianId { get; set; }
        public long Id { get; set; }
        /// <summary>
        /// 获取或设置退款订单号
        /// </summary>
        public string OutRefundId { get; set; }
        /// <summary>
        /// 获取或设置订单号
        /// </summary>
        public long OutTradeNo { get; set; }
        /// <summary>
        /// 获取或设置微信支付订单号
        /// </summary>
        public string WechatPrePayId { get; set; }
        /// <summary>
        /// 获取或设置微信退款订单号
        /// </summary>
        public string RefundId { get; set; }
        /// <summary>
        /// 获取或设置退款金额
        /// </summary>
        public double RefundFee { get; set; }
        /// <summary>
        /// 获取或设置退款渠道
        /// </summary>
        public TenpayRefundChannel RefundChannel { get; set; }
        /// <summary>
        /// 获取或设置操作员帐号,默认为商户号
        /// </summary>
        public string OpUserId { get; set; }
        /// <summary>
        /// 获取或设置接收人帐号
        /// <para>转账退款接收退款的财付通帐号</para>
        /// </summary>
        public string RecvUserId { get; set; }
        /// <summary>
        /// 获取或设置接收人姓名
        /// <para>转账退款接收退款的姓名(需与接收退款的财付通帐号绑定的姓名一致)</para>
        /// </summary>
        public string ReccvUserName { get; set; }
        /// <summary>
        /// 获取或设置退款状态
        /// </summary>
        public TenpayRefundStatus Status { get; set; }
        /// <summary>
        /// 获取或设置退款订单创建时间
        /// </summary>
        public DateTime CretaeTime { get; set; }
        /// <summary>
        /// 获取或设置更改状态时间
        /// </summary>
        public DateTime? ChangeStatusTime { get; set; }

        public long OriginalRoadRefundInfoId { set; get; }
    }

    public enum TenpayRefundChannel
    {
        财付通 = 0,
        银行 = 1
    }

    public enum TenpayRefundStatus
    {
        退款成功 = 1,
        退款失败 = 2,
        退款处理中 = 3,
        /// <summary>
        /// 需要商户原退款单号重新发起
        /// </summary>
        未确定 = 4,
        /// <summary>
        /// 退款到银行发现用户的卡作废或者冻结了，导致原路退款银行卡失败，资金回流到商户的现金帐号，需要商户人工干预，通过线下或者财付通转账的方式进行退款
        /// </summary>
        转入代发 = 5
    }
}
