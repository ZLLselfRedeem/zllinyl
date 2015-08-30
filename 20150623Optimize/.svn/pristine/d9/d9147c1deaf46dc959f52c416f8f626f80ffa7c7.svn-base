using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UmengPush
{
    public class SendResult
    {
        /// <summary>
        /// 返回结果，"SUCCESS"或者"FAIL"
        /// </summary>
        public string ret { get; set; }
        public backData data { get; set; }
    }
    public class backData
    {
        /// <summary>
        /// 当type为unicast、listcast或者customizedcast且alias不为空时:
        /// 仅当"ret"为"SUCCESS"时返回
        /// </summary>
        public string msg_id { get; set; }
        /// <summary>
        /// 当type为于broadcast、groupcast、filecast、customizedcast且file_id不为空的情况(任务)
        /// 仅当"ret"为"SUCCESS"时返回
        /// </summary>
        public string task_id { get; set; }
        /// <summary>
        /// 文件上传成功后返回ID
        /// </summary>
        public string file_id { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public string error_code { get; set; }
        /// <summary>
        /// 如果开发者填写了thirdparty_id, 接口也会返回该值
        /// 仅当"ret"为"FAIL"时返回
        /// </summary>
        public string thirdparty_id { get; set; }
    }
    public enum type
    {
        [DescriptionAttribute("单播")]
        unicast = 1,
        [DescriptionAttribute("列播")]
        listcast = 2,
        [DescriptionAttribute("广播")]
        broadcast = 3,
        [DescriptionAttribute("组播")]
        groupcast = 4,
        [DescriptionAttribute("文件播")]
        filecast = 5,
        [DescriptionAttribute("自定义播")]
        customizedcast = 6
    }
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
        /// <summary>
        /// 点单详情页面
        /// </summary>
        NOTIFICATIONS_ORDERDETAIL = 20,//对应value为点单Id （orderId）
        /// <summary>
        /// 领红包H5
        /// </summary>
        NOTIFICATIONS_REDENVELOPE_GET = 21,//对应value为url
        /// <summary>
        /// 美食广场
        /// </summary>
        NOTIFICATIONS_FOODPLAZA = 22,
        /// <summary>
        /// 首页
        /// </summary>
        NOTIFICATIONS_INDEX = 23,
        /// <summary>
        /// 福利页
        /// </summary>
        NOTIFICATIONS_WELFIRE = 24,
        /// <summary>
        /// 积分
        /// </summary>
        NOTIFICATIONS_INTEGRAL = 25,
    }
}
