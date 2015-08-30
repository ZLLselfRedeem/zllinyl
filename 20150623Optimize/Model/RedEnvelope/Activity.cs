﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 红包活动
    /// 2014-7-29
    /// </summary>
    [Serializable]
    [DataContract]
    public class Activity
    {
        [DataMember]
        /// <summary>
        /// 活动Id
        /// </summary>
        public int activityId { get; set; }
        [DataMember]
        /// <summary>
        /// 活动名称
        /// </summary>
        public string name { get; set; }
        [DataMember]
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime beginTime { get; set; }
        [DataMember]
        /// <summary>
        /// 结束时间
        /// 当
        /// <code>this.expirationTimeRule==ExpirationTimeRule.unify</code>
        /// 使用该值
        /// </summary>
        public DateTime endTime { get; set; }
        [DataMember]
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool enabled { get; set; }
        [DataMember]
        /// <summary>
        /// 数据是否有效
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
        /// 过期时间规
        /// </summary>
        public ExpirationTimeRule expirationTimeRule { get; set; }
        [DataMember]
        /// <summary>
        /// 顺延指定(天)数
        /// 当
        /// <code>this.expirationTimeRule==ExpirationTimeRule.postpone</code>
        /// 使用该值
        /// </summary>
        public int ruleValue { get; set; }
        [DataMember]
        /// <summary>
        /// 活动规则
        /// </summary>
        public string activityRule { get; set; }
        [DataMember]

        /// <summary>
        /// 活动类型
        /// </summary>
        public ActivityType activityType { get; set; }
        [DataMember]
        /// <summary>
        /// 红包有效开始时间
        /// </summary>
        public DateTime redEnvelopeEffectiveBeginTime { get; set; }
        [DataMember]
        /// <summary>
        /// 红包有效结束时间
        /// </summary>
        public DateTime redEnvelopeEffectiveEndTime { get; set; }
    }

    public enum ExpirationTimeRule
    {
        /// <summary>
        /// 顺延指定天数
        /// </summary>
        postpone = 1,
        /// <summary>
        /// 统一时间点
        /// </summary>
        unify = 2
    }

    public enum ActivityType
    {
        大红包 = 1,
        天天红包 = 2,
        节日免单红包 = 3,
        赠送红包 = 4,
        抽奖红包 = 5,
    }
}
