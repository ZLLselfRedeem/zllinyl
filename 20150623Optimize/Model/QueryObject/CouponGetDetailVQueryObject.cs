﻿using System;

/*----------------------------------------------------------------
// 功能描述：查询对象，系统自动生成，请勿手动修改
// 创建标识：2015/5/19 14:16:43
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/

namespace VAGastronomistMobileApp.Model.QueryObject
{
    public  class CouponGetDetailVQueryObject
    {
        /// <summary>
        /// CouponGetDetailID
        /// </summary>
        public int? CouponGetDetailId { get; set; }
        /// <summary>
        /// CouponDetailNumber
        /// </summary>
        public string CouponDetailNumber { get; set; }
        /// <summary>
        /// CouponDetailNumber模糊查询
        /// </summary>
        public string CouponDetailNumberFuzzy { get; set; }
        
        /// <summary>
        /// GetTime
        /// </summary>
        public DateTime? GetTime { get; set; }
        /// <summary>
        /// GetTime起始
        /// </summary>
        public DateTime? GetTimeFrom { get; set; }
        /// <summary>
        /// GetTime截至
        /// </summary>
        public DateTime? GetTimeTo { get; set; }
        /// <summary>
        /// ValidityEnd
        /// </summary>
        public DateTime? ValidityEnd { get; set; }
        /// <summary>
        /// ValidityEnd起始
        /// </summary>
        public DateTime? ValidityEndFrom { get; set; }
        /// <summary>
        /// ValidityEnd截至
        /// </summary>
        public DateTime? ValidityEndTo { get; set; }
        /// <summary>
        /// RequirementMoney
        /// </summary>
        public double? RequirementMoney { get; set; }
        /// <summary>
        /// RequirementMoney起始
        /// </summary>
        public double? RequirementMoneyFrom { get; set; }
        /// <summary>
        /// RequirementMoney截至
        /// </summary>
        public double? RequirementMoneyTo { get; set; }
        /// <summary>
        /// DeductibleAmount
        /// </summary>
        public double? DeductibleAmount { get; set; }
        /// <summary>
        /// DeductibleAmount起始
        /// </summary>
        public double? DeductibleAmountFrom { get; set; }
        /// <summary>
        /// DeductibleAmount截至
        /// </summary>
        public double? DeductibleAmountTo { get; set; }
        /// <summary>
        /// State
        /// </summary>
        public int? State { get; set; }
        /// <summary>
        /// MobilePhoneNumber
        /// </summary>
        public string MobilePhoneNumber { get; set; }
        /// <summary>
        /// MobilePhoneNumber模糊查询
        /// </summary>
        public string MobilePhoneNumberFuzzy { get; set; }
        
        /// <summary>
        /// CouponId
        /// </summary>
        public int? CouponId { get; set; }
        /// <summary>
        /// UseTime
        /// </summary>
        public DateTime? UseTime { get; set; }
        /// <summary>
        /// UseTime起始
        /// </summary>
        public DateTime? UseTimeFrom { get; set; }
        /// <summary>
        /// UseTime截至
        /// </summary>
        public DateTime? UseTimeTo { get; set; }
        /// <summary>
        /// PreOrder19DianId
        /// </summary>
        public long? PreOrder19DianId { get; set; }
        /// <summary>
        /// IsCorrected
        /// </summary>
        public bool? IsCorrected { get; set; }
        /// <summary>
        /// CorrectTime
        /// </summary>
        public DateTime? CorrectTime { get; set; }
        /// <summary>
        /// CorrectTime起始
        /// </summary>
        public DateTime? CorrectTimeFrom { get; set; }
        /// <summary>
        /// CorrectTime截至
        /// </summary>
        public DateTime? CorrectTimeTo { get; set; }
        /// <summary>
        /// SharePreOrder19DianId
        /// </summary>
        public long? SharePreOrder19DianId { get; set; }
        /// <summary>
        /// OriginalNumber
        /// </summary>
        public string OriginalNumber { get; set; }
        /// <summary>
        /// OriginalNumber模糊查询
        /// </summary>
        public string OriginalNumberFuzzy { get; set; }
        
        /// <summary>
        /// CheckTime
        /// </summary>
        public DateTime? CheckTime { get; set; }
        /// <summary>
        /// CouponSendDetailID
        /// </summary>
        public long? CouponSendDetailID { get; set; }
        /// <summary>
        /// ShopId
        /// </summary>
        public int? ShopId { get; set; }
        /// <summary>
        /// CityId
        /// </summary>
        public int? CityId { get; set; }
        /// <summary>
        /// ShopName
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// ShopName模糊查询
        /// </summary>
        public string ShopNameFuzzy { get; set; }

        public int? IsApproved { get; set; }

        public DateTime? ReconciliationTimeFrom { get; set; }

        public DateTime? ReconciliationTimeTo { get; set; }

        /// <summary>
        /// 入座时间其实
        /// </summary>
        public DateTime? ConfirmTimeTo { get; set; }

        /// <summary>
        /// 入座时间截止
        /// </summary>
        public DateTime? ConfirmTimeFrom { get; set; }
        
    }
    public enum CouponGetDetailVOrderColumn
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
        SharePreOrder19DianId,
        OriginalNumber,
        CheckTime,
        CouponSendDetailId,
        ShopId,
        CityId,
        ShopName,
    }
}