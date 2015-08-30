using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class PrePayPrivilegeConnCouponInfo
    {
        /// <summary>
        /// 主键，自增长
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 预付返现活动ID
        /// </summary>
        public int prePayPrivilegeId { get; set; }
        /// <summary>
        /// 最高消费金额
        /// </summary>
        public double prePayCashMax { get; set; }
        /// <summary>
        /// 最低消费金额
        /// </summary>
        public double prePayCashMin { get; set; }
        /// <summary>
        /// 优惠券ID
        /// </summary>
        public int couponId { get; set; }
        /// <summary>
        /// 规则，不规则
        /// </summary>
        public int returnCouponRule { get; set; }
    }
}
