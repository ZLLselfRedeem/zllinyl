using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 功能描述:公司账户流水表
    /// 创建标识:XXX 20131120
    /// </summary>
    public class MoneyViewallocAccountDetail
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Int64 accountId { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public double accountMoney { get; set; }
        /// <summary>
        /// 当前余额
        /// </summary>
        public double remainMoney { get; set; }
        /// <summary>
        /// 收支来源1:支付宝充值，2:银联充值，3：用户取消订单 4.商户退款 5.财务对账 6.商户结账
        /// </summary>
        public int accountType { get; set; }
        /// <summary>
        /// 来源关联ID(点单ID，回款ID等)
        /// </summary>
        public string accountTypeConnId { get; set; }
        /// <summary>
        /// 收支类型，1：收入，2：支出
        /// </summary>
        public int inoutcomeType { get; set; }
        /// <summary>
        /// 操作人员
        /// </summary>
        public string operUser { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime operTime { get; set; }
        /// <summary>
        /// 提款公司
        /// </summary>
        public int outcomeCompanyId { get; set; }
        /// <summary>
        /// 提款店铺
        /// </summary>
        public int outcomeShopId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}