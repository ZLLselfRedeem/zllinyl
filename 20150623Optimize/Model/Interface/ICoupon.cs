using System; 
//
// 功能描述：Coupon实体接口，系统自动生成，请勿手动修改
// 创建标识：2015/5/5 17:23:54
//
// 修改标识：
// 修改描述：

namespace VAGastronomistMobileApp.Model.Interface
{
    /// <summary>
    /// Coupon
    /// </summary>
    public interface ICoupon
    {
        /// <summary>
        /// CouponId
        /// </summary>
        int  CouponId { get; set; }
        /// <summary>
        /// CouponName
        /// </summary>
        string  CouponName { get; set; }
        /// <summary>
        /// ValidityPeriod
        /// </summary>
        int  ValidityPeriod { get; set; }
        /// <summary>
        /// StartDate
        /// </summary>
        DateTime  StartDate { get; set; }
        /// <summary>
        /// EndDate
        /// </summary>
        DateTime  EndDate { get; set; }
        /// <summary>
        /// SheetNumber
        /// </summary>
        int  SheetNumber { get; set; }
        /// <summary>
        /// SendCount
        /// </summary>
        int?   SendCount { get; set; }
        /// <summary>
        /// ShopId
        /// </summary>
        int?   ShopId { get; set; }
        /// <summary>
        /// RequirementMoney
        /// </summary>
        double  RequirementMoney { get; set; }
        /// <summary>
        /// SortOrder
        /// </summary>
        int  SortOrder { get; set; }
        /// <summary>
        /// DeductibleAmount
        /// </summary>
        double  DeductibleAmount { get; set; }
        /// <summary>
        /// State
        /// </summary>
        int  State { get; set; }
        /// <summary>
        /// CreatedBy
        /// </summary>
        int  CreatedBy { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        DateTime  CreateTime { get; set; }
        /// <summary>
        /// LastUpdatedBy
        /// </summary>
        int?   LastUpdatedBy { get; set; }
        /// <summary>
        /// LastUpdatedTime
        /// </summary>
        DateTime?   LastUpdatedTime { get; set; }
        /// <summary>
        /// Remark
        /// </summary>
        string  Remark { get; set; }
        /// <summary>
        /// RefuseReason
        /// </summary>
        string  RefuseReason { get; set; }
        /// <summary>
        /// AuditEmployee
        /// </summary>
        int?   AuditEmployee { get; set; }
        /// <summary>
        /// AuditTime
        /// </summary>
        DateTime?   AuditTime { get; set; }
        /// <summary>
        /// ShopName
        /// </summary>
        string  ShopName { get; set; }
        /// <summary>
        /// ShopAddress
        /// </summary>
        string  ShopAddress { get; set; }
        /// <summary>
        /// IsGot
        /// </summary>
        bool  IsGot { get; set; }
        /// <summary>
        /// Longitude
        /// </summary>
        double  Longitude { get; set; }
        /// <summary>
        /// Latitude
        /// </summary>
        double  Latitude { get; set; }
        /// <summary>
        /// DeductibleProportion
        /// </summary>
        double  DeductibleProportion { get; set; }
        /// <summary>
        /// Image
        /// </summary>
        string  Image { get; set; }
        /// <summary>
        /// CouponType
        /// </summary>
        int  CouponType { get; set; }
        /// <summary>
        /// IsDisplay
        /// </summary>
        bool  IsDisplay { get; set; }


        int CityId { get; set; }

        double SubsidyAmount { get; set; }

        double MaxAmount { get; set; }

        short SubsidyType  { get; set; }

        //TimeSpan? BeginTime { get; set; }

        //TimeSpan? EndTime { get; set; }

        //bool IsGeneralHolidays { get; set; } 
    }
}