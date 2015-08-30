using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 批量打款申请
    /// </summary>
    public class BatchMoneyApply
    {
        public int batchMoneyApplyId { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime createdTime { get; set; }
        /// <summary>
        /// 有多少笔
        /// </summary>
        public int advanceCount { get; set; }
        /// <summary>
        /// 实际多少笔
        /// </summary>
        public int practicalCount { get; set; }
        /// <summary>
        /// 应该打多少钱
        /// </summary>
        public double advanceAmount { get; set; }
        /// <summary>
        /// 实际打多少钱
        /// </summary>
        public double practicalAmount { get; set; }
        /// <summary>
        /// 操作员工
        /// </summary>
        public int operateEmployee { get; set; }
        /// <summary>
        /// 状态(1为成功,0为失败)
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 城市id
        /// </summary>
        public int cityId { get; set; }
    }
    /// <summary>
    /// 批量打款详情申请
    /// </summary>
    public class BatchMoneyApplyDetail
    {
        public long batchMoneyApplyDetailId { get; set; }
        /// <summary>
        /// 批量打款申请id
        /// </summary>
        public int batchMoneyApplyId { get; set; }
        /// <summary>
        /// 操作员工
        /// </summary>
        public int operateEmployee { get; set; }
        /// <summary>
        /// 公司id
        /// </summary>
        public int companyId { get; set; }
        /// <summary>
        /// 店id
        /// </summary>
        public int shopId { get; set; }
        /// <summary>
        /// 帐户号
        /// </summary>
        public string accountNum { get; set; }
        /// <summary>
        /// 银行名字
        /// </summary>
        public string bankName { get; set; }
        /// <summary>
        /// 帐户名字
        /// </summary>
        public string accountName { get; set; }
        /// <summary>
        /// 应该打钱
        /// </summary>
        public double applyAmount { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string serialNumberOrRemark { get; set; }
        /// <summary>
        /// 状态(1成功,2失败,-1取消)
        /// </summary>
        public BatchMoneyStatus status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <summary>
        /// 
        /// </summary>
        /// <summary>
        /// 
        /// </summary>
        public double haveAdjustAmount { get; set; }
        /// <summary>
        /// 帐户id
        /// </summary>
        public long accountId { get; set; }
        /// <summary>
        /// 城市id
        /// </summary>
        public int cityId { get; set; }
        /// <summary>
        /// 余额
        /// </summary>
        public double remainMoney { get; set; }
        /// <summary>
        /// 红包支付
        /// </summary>
        public double redEnvelopeAmount { get; set; }
        /// <summary>
        /// 粮票支付
        /// </summary>
        public double foodCouponAmount { get; set; }
        /// <summary>
        /// 微信支付
        /// </summary>
        public double wechatPayAmount { get; set; }
        /// <summary>
        /// 支付宝支付
        /// </summary>
        public double alipayAmount { get; set; }
        /// <summary>
        /// 佣金金额
        /// </summary>
        public double commissionAmount { get; set; }
        /// <summary>
        /// 财务打款时间
        /// </summary>
        public DateTime financePlayMoneyTime { get; set; }
    }

    /// <summary>
    /// 批量打款明细据状态
    /// </summary>
    public enum BatchMoneyStatus
    {
        /// <summary>
        /// 申请未提交
        /// </summary>
        [Description("申请未提交")]
        wait_for_check = 5,
        /// <summary>
        /// 申请被撤回
        /// </summary>
        [Description("申请被撤回")]
        check_rejected=6,
        /// <summary>
        /// 申请提交至出纳
        /// </summary>
        [Description("申请提交至出纳")]
        wait_for_confirm = 7,
        /// <summary>
        /// 出纳已确认帐目
        /// </summary>
        [Description("出纳已确认帐目")]
        confirmed = 8,
        /// <summary>
        /// 主管提交至银行
        /// </summary>
        [Description("主管提交至银行")]
        sumbmit_bank = 9,
        /// <summary>
        /// 银行已受理
        /// </summary>
        [Description("银行已受理")]
        bank_receive = 10,
        /// <summary>
        /// 银行未受理
        /// </summary>
        [Description("银行未受理")]
        bank_unreceive = 11,
        /// <summary>
        /// 银行打款成功
        /// </summary>
        [Description("银行打款成功")]
        bank_pay_success = 12,
        /// <summary>
        /// 银行打款失败
        /// </summary>
        [Description("银行打款失败")]
        bank_pay_failure = 13,

        /// <summary>
        /// 软件删除状态
        /// </summary>
        [Description("软件删除状态")]
        delete = -1,

        /// <summary>
        /// 旧版已完成
        /// </summary>
        [Description("旧版已完成")]
        com = 2,
    }
}
