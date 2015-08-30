using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 自定义推送（悠先点菜）
    /// 2014-8-11
    /// </summary>
    public class CustomPushRecord
    {
        /// <summary>
        /// 序列号
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
        /// 自定义发送时间
        /// </summary>
        public DateTime customSendTime { get; set; }
        /// <summary>
        /// 实际发送时间
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
        public string customType { get; set; }
        /// <summary>
        /// 推送自定义内容的值
        /// </summary>
        public string customValue { get; set; }
        /// <summary>
        /// 接收推送的手机号码
        /// </summary>
        public string mobilePhoneNumber { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public string customerId { get; set; }
    }

    /// <summary>
    /// 用户设备信息
    /// </summary>
    public class CustomerDevicePushInfo
    {
        public long CustomerID { get; set; }
        public string UserName { get; set; }
        public string mobilePhoneNumber { get; set; }
        public string pushToken { get; set; }
        public VAAppType appType { get; set; }
        public string appBuild { get; set; }
    }
    /// <summary>
    /// 用户支付/入座/退款 推送通知
    /// </summary>
    public class UniPushInfo
    {
        /// <summary>
        /// 用户手机
        /// </summary>
        public string customerPhone { get; set; }
        /// <summary>
        /// 店铺名称
        /// </summary>
        public string shopName { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        public long preOrder19dianId { get; set; }

        public VAPushMessage pushMessage { get; set; }
        public string clientBuild { get; set; }

        /// <summary>
        /// 总的订单ID
        /// </summary>
        public Guid orderId { get; set; }
    }

    public class UniPushAfterLottery
    {
        /// <summary>
        /// 用户手机
        /// </summary>
        public string customerPhone { get; set; }
        /// <summary>
        /// 订单Id
        /// </summary>
        public long preOrder19dianId { get; set; }
        public string pushMessage { get; set; }
        public string clientBuild { get; set; }

    }
}
