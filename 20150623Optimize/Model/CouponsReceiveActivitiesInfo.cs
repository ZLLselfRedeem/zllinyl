using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 自定义优惠券领取活动
    /// created by wangcheng 2013/9/16
    /// </summary>
    public class CouponsReceiveActivitiesInfo
    {
        /// <summary>
        /// 活动名称
        /// </summary>
        public string couponsReceiveActivitiesName { get; set; }
        /// <summary>
        /// 公司id
        /// </summary>
        public int companyId { get; set; }
        /// <summary>
        /// 门店id
        /// </summary>
        public int shopId { get; set; }
        /// <summary>
        /// 优惠券id
        /// </summary>
        public int couponId { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime activitiesValidStartTime { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime activitiesValidEndTime { get; set; }
        /// <summary>
        /// 活动描述
        /// </summary>
        public string couponsReceiveActivitiesDes { get; set; }
        /// <summary>
        /// 优惠券
        /// </summary>
        public double couponValidDayCount { get; set; }
        /// <summary>
        /// 预留字段
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 指向该优惠券相对时间还是绝对时间
        /// </summary>
        public int timeType { get; set; }

        /// <summary>
        /// 获得创建时间
        /// </summary>
        public DateTime couponsReceiveActivitiesCreateTime { get; set; }
    }
}
