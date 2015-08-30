using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VA.AllNotifications
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum VANotificationsMessageType
    {
        //以IIS形式将推送发布为Web服务时用的类型
        //NOTIFICATIONS_SEND_TO_IOS_REQUEST = 1,
        //NOTIFICATIONS_SEND_TO_IOS_RESPONSE = 2,
    }
    /// <summary>
    /// 消息自定义内容的类型
    /// </summary>
    public enum VANotificationsCustomType
    {
        //推送内容中的自定义内容的类型（AddCustom）
        NOTIFICATIONS_COUPON = 1,//优惠活动，对应的value为couponId
        NOTIFICATIONS_LOGIN_REWORD = 2,//登录奖励，对应的value为-999
        NOTIFICATIONS_REGISTER_REWORD = 3,//注册奖励，对应的value为-999
        NOTIFICATIONS_VERIFY_COUPON_REWORD = 4,//使用优惠券奖励，对应的value为-999
        NOTIFICATIONS_VERIFY_MOBILE_REWORD = 5,//验证手机奖励，对应的value为-999
        NOTIFICATIONS_INVITE_CUSTOMER_REWORD = 6,//验证手机奖励，对应的value为-999
        NOTIFICATIONS_CUSTOM_ENCOURAGE_SEND_MONEY = 7,//自定义奖励直接送钱，对应value为-999
        NOTIFICATIONS_CUSTOM_ENCOURAGE_SEND_COUPON = 8,//自定义奖励直接送券，对应value为券的编号
        NOTIFICATIONS_CUSTOM_PAYBACK = 9,//支付送券，对应value为用户所获得券的编号
        NOTIFICATIONS_SYB_REFUND = 10,//收银宝退款
        NOTIFICATIONS_EVALUATION = 11,//评价，对应的value为点单编号
        /// <summary>
        /// 推送任务状态
        /// </summary>
        NOTIFICATIONS_TASK = 12,
        /// <summary>
        /// 推荐专题
        /// </summary>
        NOTIFICATIONS_RECOMMEND = 13,//对应value为url
        /// <summary>
        /// 点菜页面
        /// </summary>
        NOTIFICATIONS_ORDER = 14,//对应value为shopId
        /// <summary>
        /// 红包列表
        /// </summary>
        NOTIFICATIONS_REDENVELOPE = 15,//对应value为url
        /// <summary>
        /// 点单列表
        /// </summary>
        NOTIFICATIONS_ORDERLIST = 16,//对应value为-999
        /// <summary>
        /// 点好菜
        /// </summary>
        NOTIFICATIONS_ORDERCOMPLETE = 17,//对应value为shopId+dishId
        /// <summary>
        /// 充值页面
        /// </summary>
        NOTIFICATIONS_RECHARGE = 18,//对应value为充值活动Id
    }
    /// <summary>
    /// 返回结果
    /// </summary>
    public enum VANotificationsResult
    {
        VA_OK = 1,

        VA_FAILED_BAD_DEVICE_TOKEN = -80,
        VA_FAILED_DISCONNECTED = -81,
        VA_FAILED_NOTIFICATION_TOO_LONG = -82,
        VA_FAILED_QUEUE = -83,

        VA_FAILED_LOGIN_STATUS_ERROR = -94,
        VA_FAILED_MSG_ERROR = -95,

        VA_FAILED_DB_ERROR = -98,
        VA_FAILED_OTHER = -99,

    }
    /// <summary>
    /// 通用静态变量
    /// </summary>
    public static class Global
    {
        private static VANotificationsResult _result = VANotificationsResult.VA_FAILED_OTHER;
        /// <summary>
        /// 错误字符
        /// </summary>
        public static VANotificationsResult result
        {
            get { return _result; }
            set { _result = value; }
        }
    }
    public class VASendApnsRequest
    {
        public string message { get; set; }
        public List<string> pushToken { get; set; }
        public string p12FileName { get; set; }
        public string p12FilePassword { get; set; }
        //Number of milliseconds to wait in between sending notifications in the loop
        // This is just to demonstrate that the APNS connection stays alive between messages
        public int sleepBetweenNotifications { get; set; }
        //True if you are using sandbox certificate, or false if using production
        public bool sandbox { get; set; }
        public VANotificationsCustomType notificationsContentType { get; set; }
        public string notificationsContentValue { get; set; }
    }
    public class VASendApnsResponse
    {
        public VANotificationsResult result { get; set; }
    }
}
