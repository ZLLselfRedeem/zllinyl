using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class AliRefundOrderInfo
    {
        /// <summary>
        /// 记录ID
        /// </summary>
        public long id;
        /// <summary>
        /// 退款批次号
        /// </summary>
        public string batchNo;
        /// <summary>
        /// 退款请求时间
        /// </summary>
        public DateTime refundDate;
        /// <summary>
        /// 退款总笔数
        /// </summary>
        public int batchNum;
        /// <summary>
        /// 原付款支付宝交易号
        /// </summary>
        public string aliTradeNo;
        /// <summary>
        /// 退款总金额
        /// </summary>
        public float refundSum;
        /// <summary>
        /// 退款理由
        /// </summary>
        public string refundReason;
        /// <summary>
        /// 单据状态
        /// </summary>
        public int refundStatus;
        /// <summary>
        /// 通知时间
        /// </summary>
        public string notifyTime;
        /// <summary>
        /// 通知类型
        /// </summary>
        public string notifyType;
        /// <summary>
        /// 通知校验ID
        /// </summary>
        public string notifyId;
        /// <summary>
        /// 退款成功总数
        /// </summary>
        public string successNum;
        /// <summary>
        /// 点单流水号
        /// </summary>
        public long connId;
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime lastUpdateTime;
        /// <summary>
        /// 原路退款单号
        /// </summary>
        public long originalId;
        /// <summary>
        /// 用户ID
        /// </summary>
        public long customerId;
        /// <summary>
        /// 接收通知状态
        /// </summary>
        public int notifyStatus;
    }
}
