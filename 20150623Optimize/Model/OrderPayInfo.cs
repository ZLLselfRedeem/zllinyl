using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 点单支付详情
    /// </summary>
    public class OrderPayInfo
    {
        /// <summary>
        /// 支付详情编号
        /// </summary>
        public int OrderPayID { get; set; }
        /// <summary>
        /// 对应点单编号
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 支付方式编号
        /// </summary>
        public int PaymentID { get; set; }
        /// <summary>
        /// 支付金额
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public DateTime OrderPayTime { get; set; }
        /// <summary>
        /// 支付详情状态
        /// -1：已删除，1：正常
        /// </summary>
        public int OrderPayStatus { get; set; }
        /// <summary>
        /// 挂账人编号
        /// </summary>
        public int cityLedgerCustomerID { get; set; }
        /// <summary>
        /// 挂账单位编号
        /// </summary>
        public int cityLedgerCompanyID { get; set; }
    }
}
