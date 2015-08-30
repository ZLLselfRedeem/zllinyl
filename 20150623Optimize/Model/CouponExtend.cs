using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class CouponExtend
    {
    }
    public class ClientCouponPacketDetail
    {
        public int couponGetDetailId { get; set; }
        public int shopId { get; set; }
        public string shopName { get; set; }
        public string shopLogoUrl { get; set; }
        public long couponId { get; set; }
        public DateTime? CheckTime { get; set; }
        public double couponRequirementMoney { get; set; }
        public double couponDeductibleAmount { get; set; }
        public double couponValidityEnd { get; set; }
        public int couponSatatus { get; set; }//1表示未使用；2表示已使用；-1表示已过期;-2已下线
        public int cityId { get; set; }//券所属城市
    }

    /// <summary>
    /// 抵扣卷
    /// </summary>
    public class ClientDeductionVolumeDetail : ClientCouponPacketDetail
    {
        /// <summary>
        /// 抵扣卷使用时间
        /// </summary>
        public double couponTime { set; get; }

        /// <summary>
        /// 使用提示
        /// </summary>
        public string usePrompt { set; get; }
    }

    /// <summary>
    /// 抵扣券数据model
    /// </summary>
    public class ClientDeductionVolumeDBEntity
    {
        public int CouponGetDetailID { get; set; }
        public int ShopId { get; set; }
        public string ShopName { get; set; }
        public string Image { get; set; }
        public long CouponId { get; set; }
        public DateTime? CheckTime { get; set; }
        public double RequirementMoney { get; set; }
        public double DeductibleAmount { get; set; }
        public DateTime ValidityEnd { get; set; }
        public int State { get; set; }//1表示未使用；2表示已使用；-1表示已过期
        public int CityId { get; set; }//券所属城市
        public double MaxAmount { set; get; }
        public DateTime? UseTime { set; get; }
    }


    public class OrderPaymentCouponDetail
    {
        public long couponId { get; set; }
        public string couponName { get; set; }
        public double requirementMoney { get; set; }//条件金额
        public double deductibleAmount { get; set; }//抵扣金额
        public double couponValidityEnd { get; set; }//过期时间
        public double maxAmount { get; set; } // 封顶金额
        public bool isGeneralHolidays { get; set; }// 使用方式 工作日or节假日使用
        /// <summary>
        /// 抵扣券类型 1、用户自己领取的 2、店铺的
        /// </summary>
        public CouponTypeEnum couponType { get; set; }
    }

    public class CustomerCouponDetail
    {
        public int shopId { get; set; }//门店ID
        public string shopName { get; set; }//门店名称
        public string couponName { get; set; }//优惠券名称
        public int couponExpireDay { get; set; }//还有几天过期

    }
}
