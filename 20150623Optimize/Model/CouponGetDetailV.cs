﻿using System;
using VAGastronomistMobileApp.Model.Interface; 

/*----------------------------------------------------------------
// 功能描述：CouponGetDetailV实体接口，系统自动生成，请勿手动修改
// 创建标识：2015/5/19 14:16:43
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 
    /// </summary>
    public class CouponGetDetailV:ICouponGetDetailV
    {
        /// <summary>
        /// CouponGetDetailID
        /// </summary>
        public int  CouponGetDetailId { get; set; }
        /// <summary>
        /// CouponDetailNumber
        /// </summary>
        public string  CouponDetailNumber { get; set; }
        /// <summary>
        /// GetTime
        /// </summary>
        public DateTime  GetTime { get; set; }
        /// <summary>
        /// ValidityEnd
        /// </summary>
        public DateTime  ValidityEnd { get; set; }
        /// <summary>
        /// RequirementMoney
        /// </summary>
        public double  RequirementMoney { get; set; }
        /// <summary>
        /// DeductibleAmount
        /// </summary>
        public double  DeductibleAmount { get; set; }
        /// <summary>
        /// State
        /// </summary>
        public int  State { get; set; }
        /// <summary>
        /// MobilePhoneNumber
        /// </summary>
        public string  MobilePhoneNumber { get; set; }
        /// <summary>
        /// CouponId
        /// </summary>
        public int  CouponId { get; set; }
        /// <summary>
        /// UseTime
        /// </summary>
        public DateTime?   UseTime { get; set; }
        /// <summary>
        /// PreOrder19DianId
        /// </summary>
        public long?   PreOrder19DianId { get; set; }
        /// <summary>
        /// IsCorrected
        /// </summary>
        public bool  IsCorrected { get; set; }
        /// <summary>
        /// CorrectTime
        /// </summary>
        public DateTime?   CorrectTime { get; set; }
        /// <summary>
        /// SharePreOrder19DianId
        /// </summary>
        public long  SharePreOrder19DianId { get; set; }
        /// <summary>
        /// OriginalNumber
        /// </summary>
        public string  OriginalNumber { get; set; }
        /// <summary>
        /// CheckTime
        /// </summary>
        public DateTime?   CheckTime { get; set; }
        /// <summary>
        /// CouponSendDetailID
        /// </summary>
        public long?   CouponSendDetailId { get; set; }
        /// <summary>
        /// ShopId
        /// </summary>
        public int?   ShopId { get; set; }
        /// <summary>
        /// CityId
        /// </summary>
        public int  CityId { get; set; }
        /// <summary>
        /// ShopName
        /// </summary>
        public string  ShopName { get; set; }
        /// <summary>
        /// ShopName
        /// </summary>
        public double? PreOrderSum { get; set; }

        public byte? IsApproved { get; set; }

        public double? MaxAmount { get; set; }
        public double RealDeductibleAmount { get; set; }

        public double CalculationAmount
        {
            get;
            set;
        }
    }
}