﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 抵价券明细
    /// </summary>
    public class CouponGetDetail 
    {
        /// <summary>
        /// ID
        /// </summary>
        public long CouponGetDetailID { get; set; }
        /// <summary>
        /// 抵价券编号
        /// </summary>
        public string CouponDetailNumber { get; set; }
        /// <summary>
        /// 领用时间
        /// </summary>
        public DateTime GetTime { get; set; }
        /// <summary>
        /// 有效期截止
        /// </summary>
        public DateTime ValidityEnd { get; set; }
        /// <summary>
        /// 条件金额
        /// </summary>
        public double RequirementMoney { get; set; }
        /// <summary>
        /// 抵扣金额
        /// </summary>
        public double DeductibleAmount { get; set; }

        /// <summary>
        /// 实际抵扣金额
        /// </summary>
        public double RealDeductibleAmount { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhoneNumber { get; set; }
        /// <summary>
        /// 抵价券ID
        /// </summary>
        public int CouponId { get; set; }
        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime? UseTime { get; set; }
        /// <summary>
        /// 点单编号
        /// </summary>
        public long? PreOrder19DianId { get; set; }
        /// <summary>
        /// 是否已纠错
        /// </summary>
        public bool IsCorrected { get; set; }
        /// <summary>
        /// 纠错时间
        /// </summary>
        public DateTime? CorrectTime { get; set; }
        /// <summary>
        /// 原号码
        /// </summary>
        public string OriginalNumber { get; set; }
        /// <summary>
        /// 抵扣券发送编号
        /// </summary>
        public long SharePreOrder19DianId { get; set; }

        /// <summary>
        /// 抵扣券发送编号
        /// </summary>
        public long CouponSendDetailID { get; set; }

        /// <summary>
        /// 参与计算抵扣的金额
        /// </summary>
        public double CalculationAmount { get; set; }
    }

    public class CouponGetDetailQueryObject
    {
        /// <summary>
        /// ID
        /// </summary>
       public long? CouponGetDetailId { get; set; }
        /// <summary>
        /// 抵价券编号
        /// </summary>
       public string CouponDetailNumber { get; set; }
        /// <summary>
        /// 领用时间
        /// </summary>
       public DateTime? GetTime { get; set; }
        /// <summary>
        /// 领用时间起始
        /// </summary>
       public DateTime? GetTimeFrom { get; set; }
        /// <summary>
        /// 领用时间截至
        /// </summary>
       public DateTime? GetTimeTo { get; set; }
        /// <summary>
        /// 有效期截止
        /// </summary>
       public DateTime? ValidityEnd { get; set; }
        /// <summary>
        /// 有效期截止起始
        /// </summary>
       public DateTime? ValidityEndFrom { get; set; }
        /// <summary>
        /// 有效期截止截至
        /// </summary>
       public DateTime? ValidityEndTo { get; set; }
        /// <summary>
        /// 条件金额
        /// </summary>
       public double? RequirementMoney { get; set; }
        /// <summary>
        /// 条件金额起始
        /// </summary>
       public double? RequirementMoneyFrom { get; set; }
        /// <summary>
        /// 条件金额截至
        /// </summary>
        public double? RequirementMoneyTo { get; set; }
        /// <summary>
        /// 抵扣金额
        /// </summary>
        public double? DeductibleAmount { get; set; }
        /// <summary>
        /// 抵扣金额起始
        /// </summary>
        public double? DeductibleAmountFrom { get; set; }
        /// <summary>
        /// 抵扣金额截至
        /// </summary>
        public double? DeductibleAmountTo { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int? State { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string MobilePhoneNumber { get; set; }
        /// <summary>
        /// 手机号码模糊查询
        /// </summary>
        public string MobilePhoneNumberFuzzy { get; set; }

        /// <summary>
        /// 抵价券ID
        /// </summary>
        public int? CouponId { get; set; }
        /// <summary>
        /// 使用时间
        /// </summary>
        public DateTime? UseTime { get; set; }
        /// <summary>
        /// 使用时间起始
        /// </summary>
        public DateTime? UseTimeFrom { get; set; }
        /// <summary>
        /// 使用时间截至
        /// </summary>
        public DateTime? UseTimeTo { get; set; }
        /// <summary>
        /// 点单编号
        /// </summary>
        public long? PreOrder19DianId { get; set; }
        /// <summary>
        /// 是否已纠错
        /// </summary>
        public bool? IsCorrected { get; set; }
        /// <summary>
        /// 纠错时间
        /// </summary>
        public DateTime? CorrectTime { get; set; }
        /// <summary>
        /// 纠错时间起始
        /// </summary>
        public DateTime? CorrectTimeFrom { get; set; }
        /// <summary>
        /// 纠错时间截至
        /// </summary>
        public DateTime? CorrectTimeTo { get; set; }
        /// <summary>
        /// 原号码
        /// </summary>
        public string OriginalNumber { get; set; }
        /// <summary>
        /// 原号码模糊查询
        /// </summary>
        public string OriginalNumberFuzzy { get; set; }
        /// <summary>
        /// 抵扣券发送编号
        /// </summary>
        public long? SharePreOrder19DianId { get; set; }




        public long? CouponSendDetailID { get; set; }
    }
    public enum CouponGetDetailOrderColumn
    {
        CouponGetDetailId,
        CouponDetailNumber,
        GetTime,
        ValidityEnd,
        RequirementMoney,
        DeductibleAmount,
        State,
        MobilePhoneNumber,
        CouponId,
        UseTime,
        PreOrder19DianId,
        IsCorrected,
        CorrectTime,
        OriginalNumber,
        SharePreOrder19DianId,
    }
    
    /// <summary>
    /// 订单
    /// </summary>
    public class OrderCoupon
    {
        /// <summary>
        /// 订单Id
        /// </summary>
        public long preOrder19dianId { get; set; }
        /// <summary>
        /// 实际抵扣金额
        /// </summary>
        public double RealDeductibleAmount { get; set; }
    }
}
