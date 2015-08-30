using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 功能描述: 
    /// 创建标识:wangcheng 20131122
    /// </summary>
    public class MoneyRefundDetail
    {
        /// <summary>
        ///  主键，自增长
        /// </summary>
        public long RefundId { get; set; }
        /// <summary>
        /// 点单Id
        /// </summary>
        public long preOrder19dianId { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary>
        public decimal refundMoney { get; set; }
        /// <summary>
        /// 退款原因
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 操作人员
        /// </summary>
        public string operUser { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime operTime { get; set; }

        /// <summary>
        /// 订单总ID
        /// </summary>
        public Guid orderID { get; set; }
    }

    /// <summary>
    /// 退款日志专用 add by zhujinlei 2015/07/15
    /// </summary>
    public class MoneyRefundOrder
    {
        /// <summary>
        /// 点单Id
        /// </summary>
        public long preOrder19dianId { get; set; }

        /// <summary>
        /// 订单总ID
        /// </summary>
        public Guid orderID { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public double moneyAmount { get; set; }
    }

}
