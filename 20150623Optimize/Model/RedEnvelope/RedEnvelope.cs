﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 宝箱中的红包
    /// 2014-7-31
    /// </summary>
    public class RedEnvelope
    {
        /// <summary>
        /// 红包Id
        /// </summary>
        public long redEnvelopeId { get; set; }
        /// <summary>
        /// 活动Id
        /// </summary>
        public int activityId { get; set; }

        /// <summary>
        /// 宝箱Id
        /// </summary>
        public long treasureChestId { get; set; }

        /// <summary>
        /// 微信用户id
        /// </summary>
        public Guid WeChatUser_Id { set; get; }

        /// <summary>
        /// 红包金额
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// 用户手机
        /// </summary>
        public string mobilePhoneNumber { get; set; }
        /// <summary>
        /// 红包状态
        /// </summary>
        public bool status { get; set; }
        /// <summary>
        /// 领取时间
        /// </summary>
        public DateTime getTime { get; set; }
        /// <summary>
        /// 红包状态
        /// </summary>
        public int isExecuted { get; set; }
        /// <summary>
        /// 是否过期
        /// </summary>
        public bool isExpire { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime expireTime { get; set; }
        /// <summary>
        /// 红包中未使用金额
        /// </summary>
        public double unusedAmount { get; set; }

        /// <summary>
        /// 是否是箱主的红包
        /// </summary>
        public bool isOwner { get; set; }
        public bool isOverflow { get; set; }

        public string cookie { get; set; }

        public bool isChange { get; set; }
        public string uuid { get; set; }
        /// <summary>
        /// 红包生效时间
        /// </summary>
        public DateTime effectTime { get; set; }
        public string from { get; set; }
    }

    public class RedEnvelopeViewModel
    {
        public long redEnvelopeId { get; set; }
        public double Amount { get; set; }
        public string isExecuted { get; set; }
        public string mobilePhoneNumber { get; set; }
        public string UserName { get; set; }
        public DateTime getTime { get; set; }
        public string stateType { get; set; }
    }

    public class CustomerRedEnvelope
    {
        public string mobilePhoneNumber { get; set; }
        public string uuid { get; set; }
        //public string cookie { get; set; }
    }
    public class NotEffectiveRedEnvelope
    {
        public long redEnvelopeId { get; set; }
        public double Amount { get; set; }
    }

    public class AppUUIDModel
    {
        public string uuid { get; set; }
        public DateTime updateTime { get; set; }
    }
    [Serializable]
    [DataContract]
    public class TopRedEnvelopeRankList
    {
        [DataMember]
        public string mobilePhoneNumber { get; set; }
        [DataMember]
        public double amount { get; set; }
    }

    //感恩节活动待退款的订单
    public class RefundOrder
    {
        public long preOrder19dianId { get; set; }
        public string mobilePhoneNumber { get; set; }
        public int activityId { get; set; }
    }

    public class RedEnvelopeConnOrder
    {
        public long preOrder19dianId { get; set; }
        public long redEnvelopeId { get; set; }
        public double currectUsedAmount { get; set; }
        public ActivityType activityType { get; set; }
    }

    public class RedEnvelopeConnOrder2
    {
        public long preOrder19dianId { get; set; }
        public long redEnvelopeId { get; set; }
        public double currectUsedAmount { get; set; }
        public ActivityType activityType { get; set; }

        public int activityId { get; set; }
    }

    public class RedEnvelopeConnOrder3
    {
        public long preOrder19dianId { get; set; }
        public long redEnvelopeId { get; set; }
        public double currectUsedAmount { get; set; }
    }
    public class RedEnvelopeRefund
    {
        public long redEnvelopeId { get; set; }
        public double currectUsedAmount { get; set; }
        public double canRefundAmount { get; set; }
    }
    /// <summary>
    /// 红包ID及活动ID
    /// </summary>
    public class RedEnvelopeOfActivity
    {
        public long redEnvelopeId { get; set; }
        public int activityId { get; set; }
    }

    public class RedEnvelopeConsume
    {
        public int activityId { get; set; }
        /// <summary>
        /// 已消耗数量
        /// </summary>
        public int consumeCount { get; set; }
        /// <summary>
        /// 已消耗金额
        /// </summary>
        public double consumeAmount { get; set; }
    }
}
