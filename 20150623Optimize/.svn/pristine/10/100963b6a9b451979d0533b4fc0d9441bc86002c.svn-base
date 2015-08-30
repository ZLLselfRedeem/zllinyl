using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 宝箱记录
    /// 2014-7-31
    /// </summary>
    [Serializable]
    [DataContract]
    public class TreasureChest
    {
        [DataMember]
        /// <summary>
        /// 宝箱Id
        /// </summary>
        public long treasureChestId { get; set; }
        [DataMember]
        /// <summary>
        /// 宝箱配置Id
        /// </summary>
        public int treasureChestConfigId { get; set; }
        [DataMember]
        /// <summary>
        /// 活动Id
        /// </summary>
        public int activityId { get; set; }
        [DataMember]
        /// <summary>
        /// 宝箱金额
        /// </summary>
        public double amount { get; set; }
        [DataMember]
        /// <summary>
        /// 宝箱省余金额
        /// </summary>
        public double remainAmount { get; set; }
        [DataMember]
        /// <summary>
        /// 共需几个人解锁
        /// </summary>
        public int count { get; set; }
        [DataMember]
        /// <summary>
        /// 还需几个人解锁
        /// </summary>
        public int lockCount { get; set; }
        [DataMember]
        /// <summary>
        /// 箱主手机号码
        /// </summary>
        public string mobilePhoneNumber { get; set; }
        [DataMember]
        /// <summary>
        /// 箱主cookie
        /// </summary>
        public string cookie { get; set; }
        [DataMember]
        /// <summary>
        /// 宝箱创建时间
        /// </summary>
        public DateTime createTime { get; set; }
        [DataMember]
        /// <summary>
        /// 分享宝箱的url
        /// </summary>
        public string url { get; set; }
        [DataMember]
        /// <summary>
        /// 宝箱生效时间
        /// </summary>
        public DateTime executedTime { get; set; }
        [DataMember]
        /// <summary>
        /// 是否过期
        /// 0没过期,1过期
        /// </summary>
        public bool isExpire { get; set; }
        [DataMember]
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime expireTime { get; set; }
        [DataMember]
        /// <summary>
        /// 宝箱状态
        /// </summary>
        public bool status { get; set; }
    }
}
