using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 抵价券使用记录
    /// 2015-2-3
    /// </summary>
    public class CouponUseRecord
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// 抵价券领取ID
        /// </summary>
        public long CouponGetDetailID { get; set; }
        /// <summary>
        /// 抵价券ID
        /// </summary>
        public int CouponId { get; set; }
        /// <summary>
        /// 已使用/已退款
        /// </summary>
        public CouponUseStateType StateType { get; set; }
        /// <summary>
        /// 变更原因
        /// shopName+支付，shopName+退款
        /// </summary>
        public string ChangeReason { get; set; }
        /// <summary>
        /// 变更时间
        /// </summary>
        public DateTime ChangeTime { get; set; }
        /// <summary>
        /// 点单Id
        /// </summary>
        public long PreOrder19DianId { get; set; }
    }

    public enum CouponUseStateType
    {
        pay = 2,
        refund = 1,
        inUse = 3,//使用中
        auto=4 // 程序自动领取的商家券

    }
}
