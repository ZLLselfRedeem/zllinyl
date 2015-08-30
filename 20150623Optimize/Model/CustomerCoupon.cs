using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 用户与优惠券关系信息（收藏）
    /// </summary>
    public class CustomerCoupon
    {
        /// <summary>
        /// 用户与优惠券关系编号
        /// </summary>
        public long customerConnCouponID { get; set; }
        /// <summary>
        /// 用户编号
        /// </summary>
        public long customerID { get; set; }
        /// <summary>
        /// 优惠券编号
        /// </summary>
        public long couponID { get; set; }
        /// <summary>
        /// 优惠券验证码
        /// </summary>
        public string verificationCode { get; set; }
        /// <summary>
        /// 用户已购买的该优惠券状态
        /// </summary>
        public VACustomerCouponStatus status { get; set; }
        /// <summary>
        /// 限量优惠券的序号（可以为0），如：3，代表第3个
        /// </summary>
        public int limitedSerial { get; set; }
        /// <summary>
        /// 下载时间
        /// </summary>
        public DateTime downloadTime { get; set; }
        /// <summary>
        /// 优惠券下载价格
        /// </summary>
        public double couponDownloadPrice { get; set; }
        /// <summary>
        /// 优惠券使用奖励
        /// </summary>
        public double couponVerifyReward { get; set; }

        /// <summary>
        /// 优惠券有效开始时间
        /// </summary>
        public DateTime couponValidStartTime { get; set; }
        /// <summary>
        /// 优惠券有效结束时间
        /// </summary>
        public DateTime couponValidEndTime { get; set; }

        /// <summary>
        /// 优惠卷使用时间
        /// </summary>
        public DateTime useTime { get; set; }
        /// <summary>
        /// 优惠卷对应活动类型
        /// </summary>
        public int Encouragetype { get; set; }
        /// <summary>
        /// 活动ID
        /// </summary>
        public int EncourageID { get; set; }
    }
}
