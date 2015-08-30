﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 功能描述: 商户账户流水表
    /// 创建标识:XXX 20131120
    /// </summary>
    public class MoneyMerchantAccountDetail
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
        /// 金额
        /// </summary>
        public double accountMoney { get; set; }
        /// <summary>
        /// 当前余额
        /// </summary>
        public double remainMoney { get; set; }
        /// <summary>
        /// 来源类型1:支付宝充值，2:银联充值，3：用户取消订单 4.商户退款 5.财务对账 6.商户结账
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
        /// 公司
        /// </summary>
        public int companyId { get; set; }
        /// <summary>
        /// 店铺
        /// </summary>
        public int shopId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 单据状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 财务平账时间
        /// </summary>
        public DateTime? confirmTime { get; set; }
    }

    public class MoneyMerchantAccountDetailResponse
    {
        public string accountTypeConnId { get; set; }
        public DateTime operTime { get; set; }
        public int accountType { get; set; }
        public string accountTypeName { get; set; }
        public double accountMoney { get; set; }
        public double remainMoney { get; set; }
        public long accountId { get; set; }

        //public DateTime checkTime { set; get; }
        public string mobilePhoneNumber { set; get; }

        public Guid OrderId { get; set; }

        public int CouponType { get; set; }
    }

    public class MoneyMerchantAccountSumResponse
    {
        public DateTime date { get; set; }
        public double total { set; get; }
        public int count { set; get; }
    }

    public class MoneyMerchantAccountDetailListResponse<T>
    {
        public List<T> list { get; set; }
        public double totalMoney { get; set; }
        public double orderCount { set; get; }
        public PageNav page { get; set; }
    }
}