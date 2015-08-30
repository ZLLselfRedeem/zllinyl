using System; 
//
// 功能描述：CouponV实体接口，系统自动生成，请勿手动修改
// 创建标识：2015/4/25 20:33:04
//
// 修改标识：
// 修改描述：

namespace VAGastronomistMobileApp.Model.Interface
{
    /// <summary>
    /// CouponV
    /// </summary>
    public interface ICouponV
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
        int  ShopId { get; set; }
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
        /// shopName
        /// </summary>
        string  ShopName { get; set; }
        /// <summary>
        /// cityID
        /// </summary>
        int?   CityId { get; set; }
        /// <summary>
        /// countyID
        /// </summary>
        int?   CountyId { get; set; }
        /// <summary>
        /// companyID
        /// </summary>
        int?   CompanyId { get; set; }
        /// <summary>
        /// RefuseReason
        /// </summary>
        string  RefuseReason { get; set; }
        /// <summary>
        /// cityName
        /// </summary>
        string  CityName { get; set; }
        /// <summary>
        /// AuditEmployee
        /// </summary>
        int?   AuditEmployee { get; set; }
        /// <summary>
        /// AuditTime
        /// </summary>
        DateTime?   AuditTime { get; set; }
        /// <summary>
        /// shopStatus
        /// </summary>
        int?   ShopStatus { get; set; }
        /// <summary>
        /// isHandle
        /// </summary>
        int?   IsHandle { get; set; }
        /// <summary>
        /// DeductibleProportion
        /// </summary>
        double  DeductibleProportion { get; set; }
        /// <summary>
        /// longitude
        /// </summary>
        double?   Longitude { get; set; }
        /// <summary>
        /// latitude
        /// </summary>
        double?   Latitude { get; set; }

        /// <summary>
        /// IsGot
        /// </summary>
        bool IsGot { get; set; }


        string ShopImagePath { get; set; }


        /// <summary>
        /// 门店Logo
        /// </summary>
        string ShopLogo { get; set; }

        /// <summary>
        /// 门脸图
        /// </summary>
        string PublicityPhotoPath { get; set; }

        /// <summary>
        /// 门店地址
        /// </summary>
        string ShopAddress { get; set; }
    }
}