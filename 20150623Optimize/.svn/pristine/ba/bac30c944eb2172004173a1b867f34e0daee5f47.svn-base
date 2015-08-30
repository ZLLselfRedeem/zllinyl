using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 全局奖品设置
    /// </summary>
    public class ViewAllocAward
    {
        /// <summary>
        /// 奖品ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 奖品类别（4.红包；5.第三方；）
        /// </summary>
        public AwardType AwardType { get; set; }
        /// <summary>
        /// 红包活动ID
        /// </summary>
        public int ActivityId { get; set; }
        /// <summary>
        /// 第三方奖品名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 第三方奖品URL
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 数量（红包， 第三方）
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        /// 数据是否有效
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
        /// <summary>
        /// 最后修改者
        /// </summary>
        public string LastUpdatedBy { get; set; }

    }

    /// <summary>
    /// 用户中红包奖品请求
    /// </summary>
    public class RedEnvelopeAwardRequest
    {
        public long customerId { get; set; }
        public string mobile { get; set; }
        public string cookie { get; set; }
        public string uuid { get; set; }
        public int activityId { get; set; }
        public int shopId { get; set; }
        /// <summary>
        /// 第三方支付金额
        /// </summary>
        public double thirdPayAmount { get; set; }
    }

    /// <summary>
    /// 用户中红包奖品返回结果
    /// </summary>
    public class RedEnvelopeAwardResponse
    {
        public bool result { get; set; }
        public string message { get; set; }
        public RedEnvelope redEnvelope { get; set; }
    }
}
