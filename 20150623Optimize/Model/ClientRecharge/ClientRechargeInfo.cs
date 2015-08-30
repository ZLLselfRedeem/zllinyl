using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 客户端充值活动管理
    /// 创建日期：2014-5-4
    /// </summary>
    public class ClientRechargeInfo
    {
        /// <summary>
        /// 充值活动ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 充值活动名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 充值条件（金额）
        /// </summary>
        public double rechargeCondition { get; set; }
        /// <summary>
        /// 赠送金额
        /// </summary>
        public double present { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public string beginTime { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public string endTime { get; set; }
        /// <summary>
        /// 对外已售份数
        /// </summary>
        public int externalSold { get; set; }
        /// <summary>
        /// 实际已售份数
        /// </summary>
        public int actualSold { get; set; }
        /// <summary>
        /// 活动状态（开启/关闭）
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public int sequence { get; set; }
    }
}
