using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 平账管理
    /// Add at 2015-4-10
    /// </summary>
    public class BalanceAccountDetail
    {
        /// <summary>
        ///  主键
        /// </summary>
        public Int64 accountId { get; set; }
        /// <summary>
        /// 流水号(11+商户Id)
        /// </summary>
        public string flowNumber { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string companyName { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string shopName { get; set; }
        /// <summary>
        /// 申请平账时间
        /// </summary>
        public DateTime operTime { get; set; }
        /// <summary>
        /// 财务平账时间
        /// </summary>
        public DateTime confirmTime { get; set; }
        /// <summary>
        /// 平账金额
        /// </summary>
        public double accountMoney { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public BalanceAccountStatus status { get; set; }
    }
    public class BalanceAccountShopMoney
    {
        /// <summary>
        /// 门店Id
        /// </summary>
        public int shopId { get; set; }
        /// <summary>
        /// 可打款金额
        /// </summary>
        public double remainMoney { get; set; }
        /// <summary>
        /// 冻结金额
        /// </summary>
        public double amountFrozen { get; set; }
        /// <summary>
        /// 对应可打款支付宝金额
        /// </summary>
        public double payAlipayAmount { get; set; }
        /// <summary>
        /// 对应可打款微信金额
        /// </summary>
        public double payWechatPayAmount { get; set; }
        /// <summary>
        /// 对应可打款红包金额
        /// </summary>
        public double payRedEnvelopeAmount { get; set; }
        /// <summary>
        /// 对应可打款粮票金额
        /// </summary>
        public double payFoodCouponAmount { get; set; }
        /// <summary>
        /// 佣金，只有扣款时用到
        /// </summary>
        public double payCommissionAmount { get; set; }
    }
    public class BalanceAccountShopRemainMoney
    {
        /// <summary>
        /// 可打款金额
        /// </summary>
        public double remainMoney { get; set; }
        /// <summary>
        /// 对应可打款支付宝金额
        /// </summary>
        public double remainAlipayAmount { get; set; }
        /// <summary>
        /// 对应可打款微信金额
        /// </summary>
        public double remainWechatPayAmount { get; set; }
        /// <summary>
        /// 对应可打款红包金额
        /// </summary>
        public double remainRedEnvelopeAmount { get; set; }
        /// <summary>
        /// 对应可打款粮票金额
        /// </summary>
        public double remainFoodCouponAmount { get; set; }
        /// <summary>
        /// 佣金，只有扣款时用到
        /// </summary>
        public double payCommissionAmount { get; set; }
    }

    public class BalanceAccountShopPayMoney
    {
        /// <summary>
        /// 可打款金额
        /// </summary>
        public double payMoney { get; set; }
        /// <summary>
        /// 对应可打款支付宝金额
        /// </summary>
        public double payAlipayAmount { get; set; }
        /// <summary>
        /// 对应可打款微信金额
        /// </summary>
        public double payWechatPayAmount { get; set; }
        /// <summary>
        /// 对应可打款红包金额
        /// </summary>
        public double payRedEnvelopeAmount { get; set; }
        /// <summary>
        /// 对应可打款粮票金额
        /// </summary>
        public double payFoodCouponAmount { get; set; }
    }
    /// <summary>
    /// 平账单据状态
    /// </summary>
    public enum BalanceAccountStatus
    {
        /// <summary>
        /// 待出纳确认
        /// </summary>
        [Description("申请提交至出纳")]
        wait_for_check = 1,
        /// <summary>
        /// 待主管平账
        /// </summary>
        [Description("待主管平账")]
        wait_for_confirm = 2,
        /// <summary>
        /// 主管已平账
        /// </summary>
        [Description("主管已平账")]
        confirmed = 3,
        /// <summary>
        /// 被撤回
        /// </summary>
        [Description("被撤回")]
        rejected = 4,
    }
}
