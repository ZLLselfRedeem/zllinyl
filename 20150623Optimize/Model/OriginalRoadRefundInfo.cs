using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 原路退款申请信息
    /// </summary>
    public class OriginalRoadRefundInfo
    {
        public OriginalRoadRefundInfo()
        {
            RefundPayType = RefundPayType.其它;
        }

        /// <summary>
        /// 申请单编号
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 对应的退款类型
        /// </summary>
        public VAOriginalRefundType type { get; set; }
        /// <summary>
        /// 对应的退款内容编号
        /// </summary>
        public long connId { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary>
        public double refundAmount { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime applicationTime { get; set; }
        /// <summary>
        /// 打款时间
        /// </summary>
        public DateTime remittanceTime { get; set; }
        /// <summary>
        /// 申请单状态
        /// 1：申请中，2：已打款,3:打款中,4:打款失败
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 打款人编号
        /// </summary>
        public int remitEmployee { get; set; }
        /// <summary>
        /// 用户手机号码
        /// </summary>
        public string customerMobilephone { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string customerUserName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string note { get; set; }
        /// <summary>
        /// 退款服务员编号
        /// 如果是C端用户发起的退款则该项为0
        /// </summary>
        public int employeeId { get; set; }

        /// <summary>
        /// 退款类型
        /// </summary>
        public RefundPayType RefundPayType { set; get; }
        /// <summary>
        /// 我方生成的第三方支付交易号
        /// </summary>
        public string tradeNo { get; set; }
    }
}
