﻿using System;
//
// 功能描述：CouponGetDetailV实体接口，系统自动生成，请勿手动修改
// 创建标识：2015/5/19 14:16:43
//
// 修改标识：
// 修改描述：

namespace VAGastronomistMobileApp.Model.Interface
{
    /// <summary>
    /// CouponGetDetailV
    /// </summary>
    public interface ICouponGetDetailV
    {
        /// <summary>
        /// CouponGetDetailID
        /// </summary>
        int CouponGetDetailId { get; set; }
        /// <summary>
        /// CouponDetailNumber
        /// </summary>
        string CouponDetailNumber { get; set; }
        /// <summary>
        /// GetTime
        /// </summary>
        DateTime GetTime { get; set; }
        /// <summary>
        /// ValidityEnd
        /// </summary>
        DateTime ValidityEnd { get; set; }
        /// <summary>
        /// RequirementMoney
        /// </summary>
        double RequirementMoney { get; set; }
        /// <summary>
        /// DeductibleAmount
        /// </summary>
        double DeductibleAmount { get; set; }
        /// <summary>
        /// State
        /// </summary>
        int State { get; set; }
        /// <summary>
        /// MobilePhoneNumber
        /// </summary>
        string MobilePhoneNumber { get; set; }
        /// <summary>
        /// CouponId
        /// </summary>
        int CouponId { get; set; }
        /// <summary>
        /// UseTime
        /// </summary>
        DateTime? UseTime { get; set; }
        /// <summary>
        /// PreOrder19DianId
        /// </summary>
        long? PreOrder19DianId { get; set; }
        /// <summary>
        /// IsCorrected
        /// </summary>
        bool IsCorrected { get; set; }
        /// <summary>
        /// CorrectTime
        /// </summary>
        DateTime? CorrectTime { get; set; }
        /// <summary>
        /// SharePreOrder19DianId
        /// </summary>
        long SharePreOrder19DianId { get; set; }
        /// <summary>
        /// OriginalNumber
        /// </summary>
        string OriginalNumber { get; set; }
        /// <summary>
        /// CheckTime
        /// </summary>
        DateTime? CheckTime { get; set; }
        /// <summary>
        /// CouponSendDetailID
        /// </summary>
        long? CouponSendDetailId { get; set; }
        /// <summary>
        /// ShopId
        /// </summary>
        int? ShopId { get; set; }
        /// <summary>
        /// CityId
        /// </summary>
        int CityId { get; set; }
        /// <summary>
        /// ShopName
        /// </summary>
        string ShopName { get; set; }



        byte? IsApproved { get; set; }

        double? PreOrderSum { get; set; }

        double? MaxAmount { get; set; }
        double RealDeductibleAmount { get; set; }
    }
}