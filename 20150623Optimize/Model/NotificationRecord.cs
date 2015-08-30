using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class NotificationRecord
    {
        /// <summary>
        /// id
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool isLocked { get; set; }
        /// <summary>
        /// token
        /// </summary>
        public string pushToken { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime addTime { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime sendTime { get; set; }
        /// <summary>
        /// 是否发送
        /// </summary>
        public bool isSent { get; set; }
        /// <summary>
        /// 发送次数
        /// </summary>
        public int sendCount { get; set; }
        /// <summary>
        /// app类型
        /// </summary>
        public int appType { get; set; }
        /// <summary>
        /// 推送内容
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 推送自定义内容的类型（枚举VANotificationsCustomType）在AllNotifications中定义
        /// </summary>
        public int customType { get; set; }
        /// <summary>
        /// 推送自定义内容的值
        /// </summary>
        public string customValue { get; set; }
    }
}