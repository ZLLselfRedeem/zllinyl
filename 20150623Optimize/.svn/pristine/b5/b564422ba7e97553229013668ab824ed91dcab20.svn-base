using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// Order
    /// </summary>
    public class Order 
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// ShopId
        /// </summary>
        public int ShopId { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        public byte Status { get; set; }
        /// <summary>
        /// CustomerId
        /// </summary>
        public long CustomerId { get; set; }
        /// <summary>
        /// PreOrderSum
        /// </summary>
        public double PreOrderServerSum { get; set; }
        /// <summary>
        /// PreOrderTime
        /// </summary>
        public DateTime PreOrderTime { get; set; }
        /// <summary>
        /// IsPaid
        /// </summary>
        public byte IsPaid { get; set; }
        /// <summary>
        /// PrePaidSum
        /// </summary>
        public double PrePaidSum { get; set; }
        /// <summary>
        /// PrePayTime
        /// </summary>
        public DateTime PrePayTime { get; set; }
        /// <summary>
        /// RefundMoneySum
        /// </summary>
        public double RefundMoneySum { get; set; }
        /// <summary>
        /// RefundMoneyClosedSum
        /// </summary>
        public double RefundMoneyClosedSum { get; set; }
        /// <summary>
        /// IsEvaluation
        /// </summary>
        public byte IsEvaluation { get; set; }
        /// <summary>
        /// ExpireTime
        /// </summary>
        public DateTime ExpireTime { get; set; }
        /// <summary>
        /// VerifiedSaving
        /// </summary>
        public double VerifiedSaving { get; set; }
        /// <summary>
        /// IsShopConfirmed
        /// </summary>
        public byte IsShopConfirmed { get; set; }
        /// <summary>
        /// InvoiceTitle
        /// </summary>
        public string InvoiceTitle { get; set; }

        /// <summary>
        /// 补差价金额
        /// </summary>
        public double PayDifferenceSum { get; set; }

        public string Remark { get; set; }
    }

    public class OrderPaidDetail
    {
        /// <summary>
        /// 总订单号
        /// </summary>
        public Guid id { get; set; }
        /// <summary>
        /// 订单原总价
        /// </summary>
        public double PreOrderServerSum { get; set; }
        /// <summary>
        /// 订单支付总额
        /// </summary>
        public double PrePaidSum { get; set; }
        /// <summary>
        /// 订单总折扣金额
        /// </summary>
        public double VerifiedSaving { get; set; }
        /// <summary>
        /// 订单申请总退款金额
        /// </summary>
        public double refundMoneySum { get; set; }
    }
}
