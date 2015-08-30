using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 宝箱配置信息
    /// 2014-7-29
    /// </summary>
    [Serializable]
    [DataContract]
    public class TreasureChestConfig
    {
        [DataMember]
        /// <summary>
        /// 宝箱Id
        /// </summary>
        public int treasureChestConfigId { get; set; }
        [DataMember]
        /// <summary>
        /// 活动Id
        /// </summary>
        public int activityId { get; set; }
        [DataMember]
        /// <summary>
        /// 活动名称，Table中无此栏位
        /// </summary>
        public string activityName { get; set; }
        [DataMember]
        /// <summary>
        /// 宝箱金额
        /// </summary>
        public double amount { get; set; }
        [DataMember]
        /// <summary>
        /// 需几个人解锁
        /// </summary>
        public int count { get; set; }
        [DataMember]
        /// <summary>
        /// 获取红包最小值
        /// </summary>
        public double min { get; set; }
        [DataMember]
        /// <summary>
        /// 获取红包最大值
        /// </summary>
        public double max { get; set; }
        [DataMember]
        /// <summary>
        /// 宝箱数量
        /// </summary>
        public int quantity { get; set; }
        [DataMember]
        /// <summary>
        /// 剩余宝箱数量
        /// </summary>
        public int remainQuantity { get; set; }
        [DataMember]
        /// <summary>
        /// 宝箱状态
        /// </summary>
        public bool status { get; set; }
        [DataMember]
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }
        [DataMember]
        /// <summary>
        /// 创建者
        /// </summary>
        public int createdBy { get; set; }
        [DataMember]
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime updateTime { get; set; }
        [DataMember]
        /// <summary>
        /// 更新者
        /// </summary>
        public int updatedBy { get; set; }
        [DataMember]
        /// <summary>
        /// 红包取值规则
        /// 1 最小值/最大值
        /// 2 概率取值
        /// </summary>
        public int amountRule { get; set; }
        [DataMember]
        /// <summary>
        /// 金额区间（默认用户）
        /// </summary>
        public string defaultAmountRange { get; set; }
        [DataMember]
        /// <summary>
        /// 概率区间（默认用户）
        /// </summary>
        public string defaultRateRange { get; set; }
        [DataMember]
        /// <summary>
        ///  金额区间（新用户）
        /// </summary>
        public string newAmountRange { get; set; }
        [DataMember]
        /// <summary>
        /// 概率区间（新用户）
        /// </summary>
        public string newRateRange { get; set; }
        [DataMember]
        /// <summary>
        /// 是否防作弊
        /// </summary>
        public bool isPreventCheat { get; set; }
    }
    /// <summary>
    /// 红包取值规则
    /// </summary>
    public enum RedEnvelopeAmountRule
    {
        最小最大值 = 1,
        概率取值 = 2
    }
}
