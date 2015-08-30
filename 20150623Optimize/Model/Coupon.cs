﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using VAGastronomistMobileApp.Model.Interface;

/*----------------------------------------------------------------
// 功能描述：抵价券实体接口，系统自动生成，不得手动修改
// 创建标识：2015/1/30 15:52:08
//
// 修改标识：
// 修改描述：
----------------------------------------------------------------*/

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 抵价券
    /// </summary>
    public class Coupon : ICoupon
    {
        /// <summary>
        /// CouponId
        /// </summary>
        public int CouponId { get; set; }
        /// <summary>
        /// CouponName
        /// </summary>
        public string CouponName { get; set; }
        /// <summary>
        /// ValidityPeriod
        /// </summary>
        public int ValidityPeriod { get; set; }
        /// <summary>
        /// StartDate
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// EndDate
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// SheetNumber
        /// </summary>
        public int SheetNumber { get; set; }
        /// <summary>
        /// SendCount
        /// </summary>
        public int? SendCount { get; set; }
        /// <summary>
        /// ShopId
        /// </summary>
        public int? ShopId { get; set; }
        /// <summary>
        /// RequirementMoney
        /// </summary>
        public double RequirementMoney { get; set; }
        /// <summary>
        /// SortOrder
        /// </summary>
        public int SortOrder { get; set; }
        /// <summary>
        /// DeductibleAmount
        /// </summary>
        public double DeductibleAmount { get; set; }
        /// <summary>
        /// State
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// CreatedBy
        /// </summary>
        public int CreatedBy { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// LastUpdatedBy
        /// </summary>
        public int? LastUpdatedBy { get; set; }
        /// <summary>
        /// LastUpdatedTime
        /// </summary>
        public DateTime? LastUpdatedTime { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// RefuseReason
        /// </summary>
        public string RefuseReason { get; set; }
        /// <summary>
        /// AuditEmployee
        /// </summary>
        public int? AuditEmployee { get; set; }
        /// <summary>
        /// AuditTime
        /// </summary>
        public DateTime? AuditTime { get; set; }
        /// <summary>
        /// ShopName
        /// </summary>
        public string ShopName { get; set; }
        /// <summary>
        /// ShopAddress
        /// </summary>
        public string ShopAddress { get; set; }
        /// <summary>
        /// IsGot
        /// </summary>
        public bool IsGot { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Latitude
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// DeductibleProportion
        /// </summary>
        public double DeductibleProportion { get; set; }
        /// <summary>
        /// Image
        /// </summary>
        public string Image { get; set; }
        /// <summary>
        /// CouponType
        /// </summary>
        public int CouponType { get; set; }
        /// <summary>
        /// IsDisplay
        /// </summary>
        public bool IsDisplay { get; set; }

        public int CityId { get; set; }


        public double SubsidyAmount { get; set; }

        //抵扣券最大金额
        public double MaxAmount { get; set; }

        public int TicketType { get; set; }

        ////开始时间 时分秒
        //public TimeSpan? BeginTime { get; set; }

        //public TimeSpan? EndTime { get; set; }

        //public bool IsGeneralHolidays { get; set; } 

        /// <summary>
        /// 补贴类型
        /// </summary>
        public short SubsidyType
        {
            get;
            set;
        }
    }
    //public class CouponQueryObject
    //{
    //    /// <summary>
    //    /// ID
    //    /// </summary>
    //    public int? CouponId { get; set; }
    //    /// <summary>
    //    /// 抵价券名称
    //    /// </summary>
    //    public string CouponName { get; set; }
    //    /// <summary>
    //    /// 抵价券名称模糊查询
    //    /// </summary>
    //    public string CouponNameFuzzy { get; set; }

    //    /// <summary>
    //    /// 有效期
    //    /// </summary>
    //    public int? ValidityPeriod { get; set; }
    //    /// <summary>
    //    /// 有效期起始
    //    /// </summary>
    //    public int? ValidityPeriodFrom { get; set; }
    //    /// <summary>
    //    /// 有效期截至
    //    /// </summary>
    //    public int? ValidityPeriodTo { get; set; }
    //    /// <summary>
    //    /// 活动起始日期
    //    /// </summary>
    //    public DateTime? StartDate { get; set; }
    //    /// <summary>
    //    /// 活动起始日期起始
    //    /// </summary>
    //    public DateTime? StartDateFrom { get; set; }
    //    /// <summary>
    //    /// 活动起始日期截至
    //    /// </summary>
    //    public DateTime? StartDateTo { get; set; }
    //    /// <summary>
    //    /// 活动截至时间
    //    /// </summary>
    //    public DateTime? EndDate { get; set; }
    //    /// <summary>
    //    /// 活动截至时间起始
    //    /// </summary>
    //    public DateTime? EndDateFrom { get; set; }
    //    /// <summary>
    //    /// 活动截至时间截至
    //    /// </summary>
    //    public DateTime? EndDateTo { get; set; }
    //    /// <summary>
    //    /// 数量
    //    /// </summary>
    //    public int? SheetNumber { get; set; }
    //    /// <summary>
    //    /// 数量起始
    //    /// </summary>
    //    public int? SheetNumberFrom { get; set; }
    //    /// <summary>
    //    /// 数量截至
    //    /// </summary>
    //    public int? SheetNumberTo { get; set; }
    //    /// <summary>
    //    /// 发送数量
    //    /// </summary>
    //    public int? SendCount { get; set; }
    //    /// <summary>
    //    /// 发送数量起始
    //    /// </summary>
    //    public int? SendCountFrom { get; set; }
    //    /// <summary>
    //    /// 发送数量截至
    //    /// </summary>
    //    public int? SendCountTo { get; set; }
    //    /// <summary>
    //    /// 所属门店
    //    /// </summary>
    //    public int? ShopId { get; set; }
    //    /// <summary>
    //    /// 条件金额
    //    /// </summary>
    //    public double? RequirementMoney { get; set; }
    //    /// <summary>
    //    /// 条件金额起始
    //    /// </summary>
    //    public double? RequirementMoneyFrom { get; set; }
    //    /// <summary>
    //    /// 条件金额截至
    //    /// </summary>
    //    public double? RequirementMoneyTo { get; set; }
    //    /// <summary>
    //    /// 排序
    //    /// </summary>
    //    public int? SortOrder { get; set; }
    //    /// <summary>
    //    /// 抵扣金额
    //    /// </summary>
    //    public double? DeductibleAmount { get; set; }
    //    /// <summary>
    //    /// 抵扣金额起始
    //    /// </summary>
    //    public double? DeductibleAmountFrom { get; set; }
    //    /// <summary>
    //    /// 抵扣金额截至
    //    /// </summary>
    //    public double? DeductibleAmountTo { get; set; }
    //    /// <summary>
    //    /// 状态
    //    /// </summary>
    //    public int? State { get; set; }
    //    /// <summary>
    //    /// 创建人
    //    /// </summary>
    //    public int? CreatedBy { get; set; }
    //    /// <summary>
    //    /// 创建时间
    //    /// </summary>
    //    public DateTime? CreateTime { get; set; }
    //    /// <summary>
    //    /// 创建时间起始
    //    /// </summary>
    //    public DateTime? CreateTimeFrom { get; set; }
    //    /// <summary>
    //    /// 创建时间截至
    //    /// </summary>
    //    public DateTime? CreateTimeTo { get; set; }
    //    /// <summary>
    //    /// 最后更新人
    //    /// </summary>
    //    public int? LastUpdatedBy { get; set; }
    //    /// <summary>
    //    /// 最后更新时间
    //    /// </summary>
    //    public DateTime? LastUpdatedTime { get; set; }
    //    /// <summary>
    //    /// 备注
    //    /// </summary>
    //    public string Remark { get; set; }
    //    /// <summary>
    //    /// 拒绝理由
    //    /// </summary>
    //    public string RefuseReason { get; set; }


    //    /// <summary>
    //    /// 是否已领完
    //    /// </summary>
    //    public bool? IsGot { get; set; }
    //}
    //public enum CouponOrderColumn
    //{
    //    CouponId,
    //    CouponName,
    //    ValidityPeriod,
    //    StartDate,
    //    EndDate,
    //    SheetNumber,
    //    SendCount,
    //    ShopId,
    //    RequirementMoney,
    //    SortOrder,
    //    DeductibleAmount,
    //    State,
    //    CreatedBy,
    //    CreateTime,
    //    LastUpdatedBy,
    //    LastUpdatedTime,
    //    Remark,
    //}

    public class VAPromotionActivityList
    {
        public List<VAPromotionActivity> PromotionActivity { get; set; }
        public bool isMore { get; set; }
    }

    /// <summary>
    /// 悠先服务推广活动
    /// </summary>
    public class VAPromotionActivity
    {
        /// <summary>
        /// 活动唯一ID
        /// </summary>
        public int activityId { get; set; }
        /// <summary>
        /// 活动类别
        /// </summary>
        public PromotionActivityType activityType { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string activityName { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string activityStatus { get; set; }
    }

    public class VAPromotionCouponDetail
    {
        /// <summary>
        /// 抵扣券名称
        /// </summary>
        public string CouponName { get; set; }
        /// <summary>
        /// 抵扣券内容
        /// </summary>
        public string CouponContent { get; set; }
        /// <summary>
        /// 活动起始日期
        /// </summary>
        public double StartDate { get; set; }
        /// <summary>
        /// 活动截至时间
        /// </summary>
        public double EndDate { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int SheetNumber { get; set; }
        /// <summary>
        /// 发送数量
        /// </summary>
        public int SendCount { get; set; }
        /// <summary>
        /// 使用数量
        /// </summary>
        public int usedCount { get; set; }
        /// <summary>
        /// 未使用数量
        /// </summary>
        public int unusedCount { get; set; }
        /// <summary>
        /// 带动消费金额
        /// </summary>
        public double consumeAmount { get; set; }    
        /// <summary>
        /// 拒绝理由
        /// </summary>
        public string RefuseReason { get; set; }
        /// <summary>
        /// 状态值
        /// </summary>
        public int activityStatus { get; set; }
    }

    public class CouponList
    {
        /// <summary>
        /// 券ID
        /// </summary>
        public int CouponId { get; set; }

        /// <summary>
        /// 券名称
        /// </summary>
        public string CouponName { get; set; }

        /// <summary>
        /// 门店ID
        /// </summary>
        public int ShopID { get; set; }
    }
}