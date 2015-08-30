﻿using System;

/*----------------------------------------------------------------
// 功能描述：Coupon查询对象，系统自动生成，请勿手动修改
// 创建标识：2015/5/5 17:23:59
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/

namespace VAGastronomistMobileApp.Model.QueryObject
{
    public  class CouponQueryObject
    {
        public bool IsGiftCoupon;
        /// <summary>
        /// CouponId
        /// </summary>
        public int? CouponId { get; set; }
        /// <summary>
        /// CouponName
        /// </summary>
        public string CouponName { get; set; }
        /// <summary>
        /// CouponName模糊查询
        /// </summary>
        public string CouponNameFuzzy { get; set; }
        
        /// <summary>
        /// ValidityPeriod
        /// </summary>
        public int? ValidityPeriod { get; set; }
        /// <summary>
        /// ValidityPeriod起始
        /// </summary>
        public int? ValidityPeriodFrom { get; set; }
        /// <summary>
        /// ValidityPeriod截至
        /// </summary>
        public int? ValidityPeriodTo { get; set; }
        /// <summary>
        /// StartDate
        /// </summary>
        public DateTime? StartDate { get; set; }
        /// <summary>
        /// StartDate起始
        /// </summary>
        public DateTime? StartDateFrom { get; set; }
        /// <summary>
        /// StartDate截至
        /// </summary>
        public DateTime? StartDateTo { get; set; }
        /// <summary>
        /// EndDate
        /// </summary>
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// EndDate起始
        /// </summary>
        public DateTime? EndDateFrom { get; set; }
        /// <summary>
        /// EndDate截至
        /// </summary>
        public DateTime? EndDateTo { get; set; }
        /// <summary>
        /// SheetNumber
        /// </summary>
        public int? SheetNumber { get; set; }
        /// <summary>
        /// SheetNumber起始
        /// </summary>
        public int? SheetNumberFrom { get; set; }
        /// <summary>
        /// SheetNumber截至
        /// </summary>
        public int? SheetNumberTo { get; set; }
        /// <summary>
        /// SendCount
        /// </summary>
        public int? SendCount { get; set; }
        /// <summary>
        /// SendCount起始
        /// </summary>
        public int? SendCountFrom { get; set; }
        /// <summary>
        /// SendCount截至
        /// </summary>
        public int? SendCountTo { get; set; }
        /// <summary>
        /// ShopId
        /// </summary>
        public int? ShopId { get; set; }
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
        /// SortOrder
        /// </summary>
        public int? SortOrder { get; set; }
        /// <summary>
        /// SortOrder起始
        /// </summary>
        public int? SortOrderFrom { get; set; }
        /// <summary>
        /// SortOrder截至
        /// </summary>
        public int? SortOrderTo { get; set; }
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
        /// CreatedBy
        /// </summary>
        public int? CreatedBy { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// CreateTime起始
        /// </summary>
        public DateTime? CreateTimeFrom { get; set; }
        /// <summary>
        /// CreateTime截至
        /// </summary>
        public DateTime? CreateTimeTo { get; set; }
        /// <summary>
        /// LastUpdatedBy
        /// </summary>
        public int? LastUpdatedBy { get; set; }
        /// <summary>
        /// LastUpdatedTime
        /// </summary>
        public DateTime? LastUpdatedTime { get; set; }
        /// <summary>
        /// LastUpdatedTime起始
        /// </summary>
        public DateTime? LastUpdatedTimeFrom { get; set; }
        /// <summary>
        /// LastUpdatedTime截至
        /// </summary>
        public DateTime? LastUpdatedTimeTo { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// Remark模糊查询
        /// </summary>
        public string RemarkFuzzy { get; set; }
        
        /// <summary>
        /// RefuseReason
        /// </summary>
        public string RefuseReason { get; set; }
        /// <summary>
        /// RefuseReason模糊查询
        /// </summary>
        public string RefuseReasonFuzzy { get; set; }
        
        /// <summary>
        /// AuditEmployee
        /// </summary>
        public int? AuditEmployee { get; set; }
        /// <summary>
        /// AuditTime
        /// </summary>
        public DateTime? AuditTime { get; set; }
        /// <summary>
        /// AuditTime起始
        /// </summary>
        public DateTime? AuditTimeFrom { get; set; }
        /// <summary>
        /// AuditTime截至
        /// </summary>
        public DateTime? AuditTimeTo { get; set; }
        /// <summary>
        /// ShopName
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// ShopName模糊查询
        /// </summary>
        public string ShopNameFuzzy { get; set; }
        
        /// <summary>
        /// ShopAddress
        /// </summary>
        public string ShopAddress { get; set; }
        /// <summary>
        /// ShopAddress模糊查询
        /// </summary>
        public string ShopAddressFuzzy { get; set; }
        
        /// <summary>
        /// IsGot
        /// </summary>
        public bool? IsGot { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        public double? Longitude { get; set; }
        /// <summary>
        /// Longitude起始
        /// </summary>
        public double? LongitudeFrom { get; set; }
        /// <summary>
        /// Longitude截至
        /// </summary>
        public double? LongitudeTo { get; set; }
        /// <summary>
        /// Latitude
        /// </summary>
        public double? Latitude { get; set; }
        /// <summary>
        /// Latitude起始
        /// </summary>
        public double? LatitudeFrom { get; set; }
        /// <summary>
        /// Latitude截至
        /// </summary>
        public double? LatitudeTo { get; set; }
        /// <summary>
        /// DeductibleProportion
        /// </summary>
        public double? DeductibleProportion { get; set; }
        /// <summary>
        /// DeductibleProportion起始
        /// </summary>
        public double? DeductibleProportionFrom { get; set; }
        /// <summary>
        /// DeductibleProportion截至
        /// </summary>
        public double? DeductibleProportionTo { get; set; }
        /// <summary>
        /// Image
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// Image模糊查询
        /// </summary>
        public string ImageFuzzy { get; set; }
        
        /// <summary>
        /// CouponType
        /// </summary>
        public int? CouponType { get; set; }
        /// <summary>
        /// IsDisplay
        /// </summary>
        public bool? IsDisplay { get; set; }

        public int? CityId { get; set; }
    }
    public enum CouponOrderColumn
    {
        CouponId,
        CouponName,
        ValidityPeriod,
        StartDate,
        EndDate,
        SheetNumber,
        SendCount,
        ShopId,
        RequirementMoney,
        SortOrder,
        DeductibleAmount,
        State,
        CreatedBy,
        CreateTime,
        LastUpdatedBy,
        LastUpdatedTime,
        Remark,
        RefuseReason,
        AuditEmployee,
        AuditTime,
        ShopName,
        ShopAddress,
        IsGot,
        Longitude,
        Latitude,
        DeductibleProportion,
        Image,
        CouponType,
        IsDisplay,
    }
}