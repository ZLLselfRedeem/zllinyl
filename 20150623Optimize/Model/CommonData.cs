using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
//
//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2012-04-10.
//
namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 返回结果
    /// </summary>
    public enum VAResult
    {
        /// <summary>
        /// 成功
        /// </summary>
        VA_OK = 1,

        // place order -1 ~ -9
        VA_FAILED_DISH_NOT_FOUND = -1,
        VA_FAILED_DISH_SOLD_OUT = -2,

        // get table status -10 ~ -19
        VA_FAILED_TABLE_INVALID = -10,

        // add Item -20 ~ -29
        VA_FAILED_ORDERID_NOT_FOUND = -20,
        VA_FAILED_ORDER_STATUS_ERROR = -21,

        // VIP LOGIN
        VA_LOGIN_USER_NOT_EXIST = -30,
        VA_LOGIN_PASSWORD_INCORRECT = -31,

        // reserve
        VA_FAILED_RESERVEID_NOT_FOUND = -40,
        VA_FAILED_RESERVE_STATUS_ERROR = -41,

        // Waiting line
        VA_WAITING_LINE_INFO_REQUEST_FAILED_OTHER = -50,

        VA_WAITING_LINE_ORDER_START_FAILED_INVALID_CALL_NUMBER = -51,
        VA_WAITING_LINE_ORDER_START_FAILED_INCORRECT_PASSWORD = -52,
        VA_WAITING_LINE_ORDER_START_FAILED_OTHER = -53,

        VA_WAITING_LINE_ORDER_SUBMIT_FAILED_INCORRECT_CALL_NUMBER = -54,
        VA_WAITING_LINE_ORDER_SUBMIT_FAILED_INCORRECT_PASSWORD = -55,
        VA_WAITING_LINE_ORDER_SUBMIT_FAILED_OTHER = -56,

        VA_WAITING_LINE_ORDER_QUERY_FAILED_INCORRECT_CALL_NUMBER = -57,
        VA_WAITING_LINE_ORDER_QUERY_FAILED_INCORRECT_PASSWORD = -58, // OPTIONAL, UP TO CUSTOMER SETTING
        VA_FAILED_VERIFICATIONCODE_NOT_FOUND = -59,

        //checkout order
        VA_PRECHECKOUT_FAILED_NEED_WEIGH = -65,

        //
        VA_FAILED_NETWORK_ERROR = -93,
        VA_FAILED_LOGIN_STATUS_ERROR = -94,
        VA_FAILED_MSG_ERROR = -95,
        VA_FAILED_TABLE_STATUS_ERROR = -96,
        VA_FAILED_TABLE_NOT_FOUND = -97,
        VA_FAILED_DB_ERROR = -98,
        VA_FAILED_OTHER = -99,

        //tableArea
        VA_TABLE_AREA_NOT_FOUND = -100,

        RESULT_SPACE_BETWEEN_19DIAN_AND_MSP = -1000,
        //通用
        VA_FAILED_TYPE_ERROR = -1001,//消息内容中的type与发送消息的type不一致，应修改后再重试
        VA_FAILED_UUID_NULL = -1002,//uuid为空，确保uuid不为空后重试
        VA_FAILED_COOKIE_NULL = -1003,//cookie为空，确保cookie不为空后重试
        VA_FAILED_UUID_NOT_FOUND = -1004,//UUID找不到

        //客户端证书登录
        VA_FAILED_COOKIE_NOT_FOUND = -1101,//cookie在服务器中无法匹配，应重新登录或者注册以获取新的cookie

        //短信发送
        //VA_FAILED_SMS_SERIALNUMBER_INVALID = -1121,//短信平台序列号错误,联系管理员修复
        //VA_FAILED_SMS_PHONENUMBER_INVALID = -1122,//电话号码无效，修改电话号码后重试
        //VA_FAILED_SMS_MONEY_NOT_ENOUGH = -1123,//短信平台余额不足，联系管理员充值
        //VA_FAILED_SMS_OTHER = -1124,//短信平台其他错误，重试

        //000	成送成功(前三位)！
        VA_FAILED_SMS_MONEY_NOT_ENOUGH = -1121,//-01	当前账号余额不足！
        VA_FAILED_SMS_USERID_INVALID = -1122,//-02	当前用户ID错误！
        VA_FAILED_SMS_PASSWORD_INVALID = -1123,//-03	当前密码错误！
        VA_FAILED_SMS_PARAMETER_ERROR = -1124,//-04	参数不够或参数内容的类型错误！
        VA_FAILED_SMS_PHONENUMBER_INVALID = -1125,//-05	手机号码格式不对！（目前还未实现）
        VA_FAILED_SMS_MESSAGE_ENCODE_ERROR = -1126,//-06	短信内容编码不对！（目前还未实现）
        VA_FAILED_SMS_MESSAGE_INVALID = -1127,//-07	短信内容含有敏感字符！（目前还未实现）
        VA_FAILED_SMS_NODATA = -1128,//null	无接收数据
        VA_FAILED_SMS_SYSTEM_MAINTENANCE = -1129,//-09	系统维护中.. （目前还未实现）
        VA_FAILED_SMS_PHONENUMBER_TOOLONG = -1130,//-10	手机号码数量超长！（100个/次 超100个请自行做循环）（目前还未实现）
        VA_FAILED_SMS_MESSAGE_TOOLONG = -1131,//-11	短信内容超长！（70个字符）目前已取消，如果内容超长，会自动分成多条发送
        VA_FAILED_SMS_OTHER = -1132,//-12	其它错误！


        //客户端手机验证
        VA_FAILED_PHONENUMBER_NULL = -1141,//手机号码为空，确保手机号码不为空后重试
        VA_FAILED_PHONENUMBER_ALREADY_VERIFIED = -1142,//该手机号码已经验证过，请直接登录或者换号码重试
        VA_FAILED_VERIFICATIONCODE_WRONG = -1143,//验证码错误，重新输入验证码重试，或者重新接收新验证码后重试
        VA_FAILED_VERIFICATIONCODE_OUTOFTIME = -1144,//验证码过期，重新接收新验证码后重试
        VA_FAILED_PASSWORD_NULL = -1145,//密码为空，确保密码不为空后重试
        VA_FAILED_DEVICE_LOCKED = -1146,//设备被封锁

        //客户端手机登录
        VA_FAILED_PHONENUMBER_NOT_FOUND = -1161,//该手机号码未验证，重新输入手机号码后重试
        VA_FAILED_PASSWORD_WRONG = -1162,//密码不正确

        //客户端求开通城市
        VA_FAILED_CITY_NOT_FOUND = -1181,//该城市的编号在服务器中不存在，检查城市编号后重试
        VA_FAILED_CITY_ALREADY_ONLINE = -1182,//该城市以及上线，不要再发了

        //客户端下载优惠券（客户端领取购买优惠券）
        VA_FAILED_CAMPAIGN_NOT_FOUND = -1201,//优惠活动不存在，请确认优惠活动编号后重试
        VA_FAILED_CAMPAIGN_OVERDUE = -1202,//该优惠活动已过期，不要再试了
        VA_FAILED_CAMPAIGN_SOLDOUT = -1203,//该优惠活动已经卖光了，不用试了
        VA_FAILED_CAMPAIGN_ISVIPONLY = -1204,//该优惠活动是会员独享的，哥们你不是会员吧，还是你会员过期了吧
        VA_FAILED_CAMPAIGN_CAN_DOWNLOAD_ONLY_ONCE = -1205,//该优惠活动每人只能下载一次，你已经有了
        VA_FAILED_MONEYREMAINED_NOT_ENOUGH = -1206,//该用户余额不足，先去充值吧
        VA_FAILED_MONEYREMAINED_NOT_ENOUGH_AND_NO_PREORDERPAYMODE = -1207,//该用户余额不足，并且没有默认支付方式
        VA_FAILED_WECHATPAY_NOTFOUND_PREPAYID = -1208,//调用微信支付接口未获取到prepayId
        VA_FAILED_WECHATPAY_UPDATE_PREPAYID = -1209,//更新微信支付预点单的prepayId失败
        VA_FAILED_WECHATPAY_NOTFOUND_ACCESSTOKEN = -1210,//调用微信支付接口未获取到AccessToken


        //客户端绑定OpenId
        VA_FAILED_OPENIDUID_NULL = -1221,//第三方UID为空，确保不为空后重试
        VA_FAILED_OPENIDSESSION_NULL = -1222,//第三方Session为空，确保不为空后重试
        VA_FAILED_OPENUID_ALREADY_BIND = -1223,//该UID已经绑定过了

        //客户端OpenId登录
        VA_FAILED_OPENIDUID_NOT_FOUND = -1241,//该UID在服务器未找到，修改UID后重试，或者先去绑定

        //客户端手机更改号码

        //客户端修改密码
        VA_FAILED_USER_NOT_BIND = -1281,//该用户还未绑定手机或者第三方号码，不能设置密码，请先绑定

        //客户端忘记密码
        VA_FAILED_EMAIL_NOT_BIND = -1301,//该用户还未绑定电子邮箱，无法使用忘记密码，sorry
        VA_FAILED_EMAIL_SEND_ERROR = -1302,//服务器发送邮件失败，请稍后再试
        VA_FAILED_EMAIL_BODY_ERROR = -1303,//服务器配置邮件内容失败，请稍后再试或者？？

        //添加、删除收藏
        VA_FAILED_COMPANY_COLLECTED = -1321,//该用户已经收藏过该公司了，你是不是想取消啊
        VA_FAILED_COMPANY_NOT_COLLECTED = -1322,//该用户还没有收藏过该公司，你是不是想收藏啊
        VA_FAILED_SHOP_COLLECTED = -1323,//该用户已经收藏过该门店了，你是不是想取消啊
        VA_FAILED_SHOP_NOT_COLLECTED = -1324,//该用户还没有收藏过该门店，你是不是想收藏啊
        //注册用户邀请用户
        VA_FAILED_MOBILEPHONE_INVITED_NULL = -1341,//被邀请者的手机号码为空

        //自定义奖励
        VA_FAILED_CUSTOMENCOURAGE_NOT_FOUND = -1361,//找不到自定义奖励，请重新选择
        VA_FAILED_CUSTOMER_NOT_FOUND = -1362,       //在发送自定义奖励时，找不到用户

        //预点单
        VA_FAILED_MENUORSHOP_NOT_FOUND = -1401,//该菜谱找不到或者对应的门店不存在
        VA_FAILED_PREORDER_NULL = -1402,
        VA_FAILED_USER_DO_NOT_HAVE_PREORDER_FOR_THIS_SHOP = -1403,
        VA_FAILED_NOTHING_SHARED = -1404,//分享请求中分享的openId为空
        VA_FAILED_PREORDER_NOT_SHARED = -1405,
        VA_FAILED_COMPANYID_NULL = -1406,
        VA_FAILED_PREORDER_NOT_FOUND = -1407,
        VA_FAILED_PREORDER_PAID = -1408,//该预点单已支付
        VA_FAILED_PREPAYPRIVILEGE_NOT_FOUND = -1409,//该预点单已支付
        VA_FAILED_PREORDER_NOT_TO_THE_TIME_TO_VERIFY = -1410,//预点单验证时间未到
        VA_FAILED_PREORDER_STATUS_ERROR = -1411,//预点单状态错误

        VA_FAILED_UNION_PAY_ORDER_SUMMIT_ERROR = -1412,//银联支付创建订单错误

        VA_FAILED_USECOUPON_ERROR = -1413,//优惠卷使用失败
        VA_FAILED_CURRENTUSECOUPON_ERROR = -1414,//当前优惠卷不能使用多次
        VA_FAILED_CURRENTUSEDIFFCOUPON_ERROR = -1415,//不同优惠卷不能使用多次

        //2013/9/15 wangcheng
        VA_ADDINVOICETITLE_ERROR = -1416,//添加当前点单发票抬头失败
        VA_DELETEINVOICETITLE_ERROR = -1417,//删除发票抬头失败
        VA_INVOICETITLE_NOT_FOUND = -1418,//常用发票抬头信息未找到(获取列表信息失败)

        //退款
        VA_PREORDER_REFUND_ERROR = -1420,//退款失败
        VA_REFUND_NOTPAYORCONFIRM = -1421,//单子已验证或是未付款
        VA_REFUND_NOOVERTIME_COUPON = -1422,//不含过期优惠卷不能退款
        VA_REFUND_NOTPAYORAPPROVED = -1423,//单子已对账或是未付款
        VA_REFUND_NO_MONEY = -1424, //没有这么多钱
        VA_PREORDER_AMOUNT_NOTENOUGH = -1430,  //点单金额 小于 优惠券最低使用金额

        ////客户端领取优惠券
        //VA_RECEIVE_ACTIVITY_ERROR = -1419,
        //VA_RECEIVE_ACTIVITY_COUPON_RECEIVED = -1420,//已经下载过（下载并且使用过不会报次ERROR）
        //VA_RECEIVE_ACTIVITY_COUPON_FINISHED = -1421,//本优惠券已经呗下载空了
        //VA_RECEIVE_ACTIVITY_COUPON_NOT_IN_THE_PERIOD_OF_VALIDITY = -1422,//当前活动展示不在有效期内
        //VA_RECEIVE_ACTIVITY_NOT_FOUND = -1423,//该活动为找到

        VA_FAIL_REMOTE_ORDER = -1440,//远程下单失败

        VA_FAILED_COUPONOVERTIME = -1441,//预点单含有过期优惠卷

        VA_FAILED_SHOPCONFIRMED = -1500,//该点单已被审核


        VA_FAILED_GETORDERJSON = -1900,//获取点单JSON失败

        //掌中宝模块
        //对账
        VA_FAILED_APPROVED_FAILURE = -2000,//对账失败
        VA_FAILED_APPROVED_ORDER_NOTFOUND = -2001,//未找到该点单
        VA_FAILED_APPROVED_ORDER_ISERROR = -2002,//点单不符合对账条件
        //审核
        VA_FAILED_CONFRIEM_ERROR_ORDER_ISAPPROVED = -2010,//当前点单已对账，无法取消审核
        VA_FAILED_CONFRIEM_ERROR_ORDER_ISNOTCONFRIM = -2011,//当前单子是未审核状态，无法取消审核
        VA_FAILED_CONFRIEM_ERROR_ORDER_ISCONFRIEM = -2012,//当前单子是已审核状态，无法审核
        VA_FAILED_CONFRIEM_ERROR_ORDER_NOTFOUND = -2013,//未找到该订单
        VA_FAILED_CONFRIEM_ERROR_ORDER_NOTVERIFIED = -2014,//该点单未验证
        VA_FAILED_CONFRIEM_ERROR_ORDER_HAVE_REFUND = -2015,//入座失败，点单已退款，或已原路退款
        VA_FAILED_CONFRIEM = -2016,//入座失败
        //点单详情
        VA_FAILED_NOT_FOUND_ORDER = -2030,//未找到该点单

        VA_FAILED_CLIENT_SEARCH_NOT_FOUND = -2040,//客户端查询门店列表为空

        /// <summary>
        /// 支付点单服务器端计算金额和客户端不一致
        /// </summary>
        VA_FAILED_PREORDER_COUNT_SERVER_NOT_EQUAL_CLIENT = -2100,//支付点单服务器端计算金额和客户端不一致
        //VA_FAILED_CUSTOMER_NOT_FOUND = -2101,//未找到当前用户信息
        VA_FAILED_NOT_BINDING_MOBILE_PHONE_NUMBER = -2102,//当前用户未绑定手机号码，无法完成支付
        VA_SHOP_NOT_SUPPOORT_PAYMENY = -2103,//当前门店暂不支持付款
        VA_WECHAT_ORDER_OK = -2014,//微信点菜操作成功
        VA_FAILED_COMMONDISH_NOT_FOUND = -2120,//未找到常用菜

        VA_FAILED_SMS_NOT_SEND = -2125,//短信发送失败

        VA_FAILED_EMPLOYEE_NOT_MATCH_SHOP = -2130,//该用户没有当前店铺权限 Add at 2014-3-28

        //亿美短信错误码  
        VA_FAILED_SMS_REGISTER = -2140,//号码注册激活失败
        VA_FAILED_SMS_CHARGEUP = -2141,//充值失败
        VA_FAILED_SMS_LOGOUT = -2142,//注销失败
        VA_FAILED_SMS_VOICE_TOO_OFTEN = -2143,//语音短信太频繁，10分钟只能发一次

        VA_FAILED_SHOP_NOT_ONLINE = -2150,//门店未上线
        //客户端充值抢购粮票模块
        VA_RECHARGE_ACTIVITY_CLOSED = -2160,//当前充值活动已关闭
        VA_PAYMODE_NULLITY = -2161,//充值支付方式无效
        VA_FAILED_ADD_RECHARGE_ORDER = -2162,//新增充值订单失败
        VA_RECHARGE_ACTIVITY_OUT_DATE = -2163,//当前活动已过期
        VA_MEET_THE_CONDITIONS_RECHARGE = -2164,//满足充值条件
        VA_RECHARGE_ACTIVITY_BEFORE_DATE = -2165,//当前活动未开始

        /// <summary>
        /// 未找到任务
        /// </summary>
        VA_NOT_FOUND_TASK = -3000,

        /*悠先服务客户追溯模块错误消息*/
        VA_FAILED_FIND_CUSTOMER = -3100,//未找到用户信息

        VA_FAILED_CHANGE_REMAINMONEY = -3101,//更新余额失败

        VA_NO_ACCESS_INTERFACE_AUTHORITY = -3102,//没有权限

        VA_FAILED_ADJUSTMENT_AMOUNT_UNDERSIZE = 3103,
        VA_FAILED_ADJUSTMENT_AMOUNT_OVERSIZE = 3104,

        VA_VERSION_IS_TOO_LOW = -3200,

        VA_FILED_FOUND_COUPON = -3300,//未找到优惠券

        VA_NOT_ALLOW_ZERO_PAY = -3500,

        VA_NOT_ALLOW_REDENVLOPE_PAY = -3501,

        VA_FAILED_UPDATE_UUID = -3600,//更新UUID失败

        VA_FAILED_WeChat_Binding = -3650,//绑定微信失败
        VA_FAILED_WeChat_BindingUpdate = -3651,//修改绑定微信失败
        VA_FAILED_WeChat_BindingExists = -3652,//该微信已绑定

        VA_FAILED_LOTTERY = -3680,//未中奖

        VA_PAY_FAIL = -9999,

    };
    /// <summary>
    /// 餐桌状态
    /// </summary>
    public enum VATableStatus
    {
        TABLE_EMPTY = 1,//空桌
        TABLE_ORDERING = 2,//点餐中
        TABLE_TO_BE_CONFIRMED = 3,//等待服务员确认
        TABLE_DINING = 4,//用餐中
        TABLE_ADDDISH,//加菜中
        TABLE_ADDDISH_TO_BE_CONFIRMED,//加菜等待服务员确认
        TABLE_CHECKOUT,//已结账
        TABLE_RESERVE//已预定
    };
    /// <summary>
    /// 点单状态
    /// </summary>
    public enum VAOrderStatus
    {
        ORDER_ORDERING = 1,//点餐中
        ORDER_TO_BE_CONFIRMED = 2,//等待服务员确认
        ORDER_DINING = 3,//用餐中
        ORDER_ADDDISH,//加菜中
        ORDER_ADDDISH_TO_BE_CONFIRMED,//加菜等待服务员确认
        ORDER_CHECKOUT,//已结账
        ORDER_CANCEL,//已作废
        ORDER_RESERVE,//已预定
        //ORDER_QUEUE,//排队      2012-02-20 xiaoyu 取消该状态
    };
    /// <summary>
    /// 点单详情状态
    /// </summary>
    public enum VAOrderDetailStatus
    {
        ORDETAIL_DELETED = -1,
        ORDETAIL_NORMAL = 1,
        ORDETAIL_RETREATED = 2,
    }
    /// <summary>
    /// 预定单状态
    /// </summary>
    public enum VAReserveStatus
    {
        RESERVE_DELETED = -1,//已删除
        RESERVE_RESERVED = 1,//正常（已预定）
        RESERVE_ARRIVED = 2,//已到达
        RESERVE_CANCEL = 3,//已取消
    }
    /// <summary>
    /// 排队状态
    /// </summary>
    public enum VAQueueStatus
    {
        QUEUE_WAITING = 1,//未点餐（正在排队）
        //QUEUE_ORDERING=2,//点餐中
        //QUEUE_ORDERED=3,//已点餐
        //QUEUE_PREPAID= 4,//已结账
        //QUEUE_COOKING=5,//烹饪中
        QUEUE_SEATED = 2,//已入座
        QUEUE_CANCEL = 3,//已取消
        QUEUE_CALLED = 4,//已叫号
    }
    /// <summary>
    /// 挂账状态
    /// </summary>
    public enum VACityLedgerStatus
    {
        CITYLEDGER_DELETED = -1,//已删除
        CITYLEDGER_NORMAL = 1,//正常
        CITYLEDGER_DISABLE = 2,//已停用
    }

    public enum VACookOrderStyle
    {
        COOK_ORDER_TABLESTYLE = 1,//台单模式
        COOK_ORDER_COOKSTYLE = 2,//每菜一单模式
    }
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum VAMessageType
    {
        // place order 
        ORDER_ADD_ITEM_REQUEST = 1,
        ORDER_ADD_ITEM_RESPONSE = 2,
        ORDER_REMOVE_ITEM_REQUEST = 3,
        ORDER_REMOVE_ITEM_RESPONSE = 4,
        ORDER_MODIFY_QUANTITY_REQUEST = 5,   // MODIFY QUANTITY
        ORDER_MODIFY_QUANTITY_RESPONSE = 6,
        ORDER_LIST_REQUEST = 7,  // LIST ORDERED ITEMS
        ORDER_LIST_RESPONSE = 8,
        ORDER_CUSTOMER_SUBMIT_REQUEST = 9, // MODIFY STATUS TO "SUBMITTED"
        ORDER_CUSTOMER_SUBMIT_RESPONSE = 10,
        ORDER_WAITER_CONFIRM_REQUEST = 11,
        ORDER_WAITER_CONFIRM_RESPONSE = 12,
        ORDER_RETREAT_ITEM_REQUEST = 13,
        ORDER_RETREAT_ITEM_RESPONSE = 14,
        ORDER_WEIGH_ITEM_REQUEST = 15,
        ORDER_WEIGH_ITEM_RESPONSE = 16,

        // get table status 
        ALL_TABLES_STATUS_REQUEST = 20,
        ALL_TABLES_STATUS_RESPONSE,

        // open one table
        OPEN_TABLE_REQUEST = 30,
        OPEN_TABLE_RESPONSE,
        CLEAR_TABLE_REQUEST,
        CLEAR_TABLE_RESPONSE,
        CHANGE_TABLE_REQUEST,
        CHANGE_TABLE_RESPONSE,

        // bill payment
        BILL_REQUEST = 40,
        BILL_RESPONSE,    // return total price, including service fee etc
        VIP_LOGIN_REQUEST = 50,// USER NAME & PASSWORD
        VIP_LOGIN_RESPONSE, // USER_ID, RESULT

        PAY_BY_VIP_ACCOUNT_REQUEST = 60,
        PAY_BY_VIP_ACCOUNT_RESPONSE,
        PAY_BY_CASH_REQUEST,
        PAY_BY_CASH_RESPONSE,

        EMPLOYEE_LOGIN_REQUEST = 70,
        EMPLOYEE_LOGIN_RESPONSE,
        EMPLOYEE_LOGOUT_REQUEST,
        EMPLOYEE_LOGOUT_RESPONSE,

        CUSTOMER_LOGIN_REQUEST = 80,
        CUSTOMER_LOGIN_RESPONSE,
        CUSTOMER_LOGOUT_REQUEST,
        CUSTOMER_LOGOUT_RESPONSE,

        RESERVE_ADD_REQUEST = 90,
        RESERVE_ADD_RESPONSE,
        RESERVE_REMOVE_REQUEST,
        RESERVE_REMOVE_RESPONSE,
        RESERVE_MODIFY_REQUEST,
        RESERVE_MODIFY_RESPONSE,
        RESERVE_ARRIVED_REQUEST,
        RESERVE_ARRIVED_RESPONSE,
        RESERVE_TABLE_REQUEST,
        RESERVE_TABLE_RESPONSE,
        RESERVE_TODAY_LIST_REQUEST,//当天预定列表
        RESERVE_TODAY_LIST_RESPONSE,
        RESERVE_DETAIL_REQUEST,//指定预定信息
        RESERVE_DETAIL_RESPONSE,
        RESERVE_CANCEL_REQUEST,//取消预定信息
        RESERVE_CANCEL_RESPONSE,
        RESERVE_ORDER_REQUEST,//预定点单信息
        RESERVE_ORDER_RESPONSE,

        WAITING_LINE_INFO_REQUEST = 110,     //查询所有队列信息
        WAITING_LINE_INFO_RESPONSE,        //返回所有队列信息
        WAITING_LINE_ORDER_START_REQUEST,    //开始点餐
        WAITING_LINE_ORDER_START_RESPONSE,    //
        WAITING_LINE_ORDER_SUBMIT_REQUEST,    //提交点餐
        WAITING_LINE_ORDER_SUBMIT_RESPONSE,    //
        WAITING_LINE_ORDER_QUERY_REQUEST,    //查询点餐
        WAITING_LINE_ORDER_QUERY_RESPONSE,

        //排队点菜验证
        WAITING_LINE_VERIFICATIONCODE_REQUEST,//
        WAITING_LINE_VERIFICATIONCODE_RESPONSE,//

        //快速结账
        QUICK_CHECKOUT_REQUEST = 130,
        QUICK_CHECKOUT_RESPONSE,

        //整单备注
        ORDER_NOTE_REQUEST = 140,
        ORDER_NOTE_RESPONSE,

        //餐桌区域TableArea
        TABLE_AREA_REQUEST = 150,
        TABLE_AREA_RESPONSE,

        //账单打印
        PRINT_BILLORDER_REQUEST = 160,
        PRINT_BILLORDER_RESPONSE,

        _SPACE_BETWEEN_19DIAN_AND_MSP = 1000,

        // 查询开通城市 (up: empty; down: 城市对象数组json)
        CITY_LIST_REQUEST = 1001,
        CITY_LIST_RESPONSE = 1002,

        // 刷新某城市请求数量和商户数量（up：city id；down：city info）
        CITY_INFO_REQUEST = 1003,
        CITY_INFO_RESPONSE = 1004,

        // 求开通（up：city id；down: city info)
        CITY_OPENNING_REQUEST = 1005,
        CITY_OPENNING_RESPONSE,

        // 查找优惠 （up 当前坐标，类型；down: 默认排序的若干条）
        COUPON_SEARCH_WITH_OPTION_REQUEST,
        COUPON_SEARCH_WITH_OPTION_RESPONSE,

        // 下载优惠券（up cookie，优惠活动id；down：结果，校验码）
        COUPON_DOWNLOAD_REQUEST,
        COUPON_DOWNLOAD_RESPONSE,

        // 激活设备（up：推送token，设备UUID；down：cookie）备注：推送token对APNS唯一，但是对设备可变，UUID对客户端唯一；cookie在应用服务器上对客户端唯一
        CLIENT_REGISTER_REQUEST,
        CLIENT_REGISTER_RESPONSE,

        // （刷新个人信息）证书登录（up：推送token，cookie；down：用户个人信息）
        CLIENT_COOKIE_LOGIN_REQUEST,
        CLIENT_COOKIE_LOGIN_RESPONSE,


        // 使用openid登录（up：推送token，cookie，vendor, openid_session；down:用户个人信息,cookie）
        CLIENT_OPEN_ID_LOGIN_REQUEST,
        CLIENT_OPEN_ID_LOGIN_RESPONSE,

        // 通过open id挂失
        CLIENT_REPORT_LOSS_OPEN_ID_REQUEST, // vendor, session 
        CLIENT_REPORT_LOSS_OPEN_ID_RESPONSE, // profile, cookie


        // 手机认证请求
        CLIENT_MOBILE_VERIFY_REQUEST,//手机号码，cookie,SMS短信验证码(第一次为空）
        CLIENT_MOBILE_VERIFY_RESPONSE,

        // 
        // 手机号码登陆
        CLIENT_MOBILE_LOGIN_REQUEST,//手机号码，推送token，设备UUID,SMS短信验证码(第一次为空）
        CLIENT_MOBILE_LOGIN_RESPONSE,

        // 通过手机号码挂失
        CLIENT_REPORT_LOSS_MOBILE_PHONE_REQUEST, // mobilephone, verification code （第一次提交verification为空）
        CLIENT_REPORT_LOSS_MOBILE_PHONE_RESPONSE, // profile, cookie （第一次回复均为空）

        // 绑定open id
        CLIENT_BIND_OPEN_ID_REQUEST, //cookie, uuid, vendor, open_id_session
        CLIENT_BIND_OPEN_ID_RESPONSE,

        // 更新用户profile 
        USER_INFO_MODIFY_REQUEST, // VAUserInfo
        USER_INFO_MODIFY_RESPONSE,

        // 手机号码更改
        CLIENT_MOBILE_MODIFY_REQUEST,//新手机号码，原密码,SMS短信验证码(第一次为空）
        CLIENT_MOBILE_MODIFY_RESPONSE,

        // 用户更改密码
        CLIENT_MODIFY_PASSWORD_REQUEST,//原密码,新密码
        CLIENT_MODIFY_PASSWORD_RESPONSE,

        // 用户忘记密码
        CLIENT_FORGET_PASSWORD_REQUEST,//原密码,新密码
        CLIENT_FORGET_PASSWORD_RESPONSE,

        // 查询用户的优惠券列表
        USER_COUPON_LIST_REQUEST, // 查询类型
        USER_COUPON_LIST_RESPONSE, // coupon 数组

        //店家验证与消费优惠券(web)
        CLIENT_VERIFY_COUPON_REQUEST, // 查询类型
        CLIENT_VERIFY_COUPON_RESPONSE, // coupon 数组

        //用户邀请其他用户
        CLIENT_INVITE_CUSTOMER_REQUEST,
        CLIENT_INVITE_CUSTOMER_RESPONSE,

        // 查询钱包交易
        USER_WALLET_TRANSACTION_LIST_REQUEST, // 空
        USER_WALLET_TRANSACTION_LIST_RESPONSE,// 余额，交易列表数组（时间、变化值、原因）

        //通过cityID查所有公司
        COMPANY_LIST_REQUEST, // city id
        COMPANY_LIST_RESPONSE, // company array

        //添加、删除收藏
        USER_SET_FAVORITE_COMPANY_REQUEST, // company id, true/false
        USER_SET_FAVORITE_COMPANY_RESPONSE,

        //通过公司id和cityid查门店
        RESTAURANT_LIST_BY_COMPANY_REQUEST, // company id, [cityid]
        RESTAURANT_LIST_BY_COMPANY_RESPONSE, // restaurant array

        //用户增加预点单
        CLIENT_PREORDER_ADD_REQUEST,
        CLIENT_PREORDER_ADD_RESPONSE,

        //用户查询预点单
        CLIENT_PREORDER_QUERY_REQUEST,//1051
        CLIENT_PREORDER_QUERY_RESPONSE,//1052

        //用户分享预点单
        CLIENT_PREORDER_SHARE_REQUEST,//1053
        CLIENT_PREORDER_SHARE_RESPONSE,//1054

        //用户查询预点单列表
        CLIENT_PREORDER_LIST_REQUEST,  // empty1055
        CLIENT_PREORDER_LIST_RESPONSE, // array of orders1056

        //用户支付预点单试图用余额支付预点单
        CLIENT_PREORDER_PREPAY_WITH_CREDIT_REQUEST, // preorder id, policy id1057
        CLIENT_PREORDER_PREPAY_WITH_CREDIT_RESPONSE, // 如果余额不足，带一个支付URL1058

        //用户申请退款，可以在confirmed = NO的情况下发送，以此查询可退换金额。confirm=YES时确认生效
        CLIENT_PREORDER_REFUND_REQUEST, //preorder id, isConfirmed1059
        CLIENT_PREORDER_REFUND_RESPONSE, // refundable amount, refunded amount1060

        //删除预点单
        CLIENT_PREORDER_DELETE_REQUEST, // 暂时不开发1061
        CLIENT_PREORDER_DELETE_RESPONSE, // 暂时不开发1062

        //商户查询预点单
        POSLITE_PREORDER_QUERY_REQUEST,//1063
        POSLITE_PREORDER_QUERY_RESPONSE,//1064

        //更新所有菜单
        UPDATE_ALL_MENU_REQUEST,//1065
        UPDATE_ALL_MENU__RESPONSE,//1066

        //用户充值（购买饭票）
        CLIENT_TOPUP_REQUEST,//1067
        CLIENT_TOPUP_RESPONSE,//1068

        CLIENT_COMPANY_DETAIL_REQUEST, // 通过 company id查company详情1069
        CLIENT_COMPANY_DETAIL_RESPONSE, // 返回VABrand的json//1070

        CLIENT_SHOP_DETAIL_REQUEST = 1071, // 通过 shop id查店铺详情
        CLIENT_SHOP_DETAIL_RESPONSE = 1072,

        CLIENT_SHOP_SELLOFF_REQUEST = 1073, // 通过 shop id查该店沽清详情
        CLIENT_SHOP_SELLOFF_RESPONSE = 1074,

        CLIENT_PREORDER_ADD_INVOICETITLE_REQUEST = 1075,//通过preorderId修改当前点单的发票抬头信息
        CLIENT_PREORDER_ADD_INVOICETITLE_RESPONSE = 1076,

        CLIENT_COUPON_ACTIVITY_REQUEST = 1077,//获取优惠券领取活动列表
        CLIENT_COUPON_ACTIVITY_RESPONSE = 1078, 

        CLIENT_FILL_RECEIVE_REQUEST = 1079,//自定义优惠券领取活动领取优惠券
        CLIENT_COUPON_RECEIVE_RESPONSE = 1080,

        CLIENT_REMOTE_ORDER_REQUEST = 1081,//远程下单
        CLIENT_REMOTE_ORDER_RESPONSE = 1082,

        COUPON_DETAIL_REQUEST = 1083,//查找优惠卷详情
        COUPON_DETAIL_RESPONSE = 1084,

        ACCOUNTDETAIL_REQUEST = 1085,//账户明细
        ACCOUNTDETAIL_RESPONSE = 1086,

        //掌中宝客户端操作模块
        /// <summary>
        /// 悠先服务-点单列表请求（拆分版）
        /// </summary>
        ZZB_CLIENT_PREORDERLIST_REQUEST = 1090,
        /// <summary>
        /// 悠先服务-点单列表回复（拆分版）
        /// </summary>
        ZZB_CLIENT_PREORDERLIST_RESPONSE = 1091,

        ZZB_CLIENT_PREORDERLISTDETAIL_REQUEST = 1092,//掌中宝客户端查询点单列表详情
        ZZB_CLIENT_PREORDERLISTDETAIL_RESPONSE = 1093,

        /// <summary>
        /// 掌中宝审核点单
        /// </summary>
        ZZB_CLIENT_PREORDERCONFRIM_REQUEST = 1094,//掌中宝审核点单
        ZZB_CLIENT_PREORDERCONFRIM_RESPONSE = 1095,

        ZZB_CLIENT_PREORDERAPPROVED_REQUEST = 1096,//掌中宝对账点单
        ZZB_CLIENT_PREORDERAPPROVED_RESPONSE = 1097,

        ZZB_CLIENT_PREORDERREFUND_REQUEST = 1098,//掌中宝退款点单
        ZZB_CLIENT_PREORDERREFUND_RESPONSE = 1099,

        CLIENT_INDEX_LIST_REQUEST = 1110,//201401客户端首页接口new
        CLIENT_INDEX_LIST_RESPONSE = 1111,
        //查询用户基本信息
        USER_INFO_QUERY_REQUEST = 1120,
        USER_INFO_QUERY_REPONSE = 1121,

        //客户端申请原路退款
        CLIENT_ORIGINAL_REFUNF_REQUEST = 1130,
        CLIENT_ORIGINAL_REFUNF_REPONSE = 1131,

        //标注一下，1120和1121已被占用
        USER_SETFAVORITESHOP_REQUEST = 1140,//201401客户端收藏接口new
        USER_SETFAVORITESHOP_RESPONSE = 1141,

        CLIENT_COMMON_DISHLIST_REQUEST = 1150,//201401客户端常用菜品列表
        CLIENT_COMMON_DISHLIST_REPONSE = 1151,

        CLIENT_FAST_PAYMENT_ORDER_REQUEST = 1160,//201401客户端一键支付点单
        CLIENT_FAST_PAYMENT_ORDER_REPONSE = 1161,

        // 手机认证请求new
        CLIENT_MOBILE_VERIFYNEW_REQUEST = 1170,//手机号码，cookie,SMS短信验证码(第一次为空）
        CLIENT_MOBILE_VERIFYNEW_RESPONSE = 1171,

        // 手机号码登陆new
        CLIENT_MOBILE_LOGINNEW_REQUEST = 1180,//手机号码，推送token，设备UUID,SMS短信验证码(第一次为空）
        CLIENT_MOBILE_LOGINNEW_RESPONSE = 1181,

        // 更新用户基本信息new 
        USER_INFO_MODIFYNEW_REQUEST = 1190, //性别、昵称
        USER_INFO_MODIFYNEW_RESPONSE = 1191,

        CLIENT_SEARCH_SHOP_LIST_REQUEST = 1200,//201401客户端搜索门店列表
        CLIENT_SEARCH_SHOP_LIST_RESPONSE = 1201,

        /// <summary>
        /// 悠先服务客户端手机验证
        /// </summary>
        ZZB_CLIENT_USER_MOBILE_REGISTER_REQUEST = 1202,//悠先服务客户端手机验证
        ZZB_CLIENT_USER_MOBILE_REGISTER_RESPONSE = 1203,

        ZZB_CLIENT_MODIFY_USERINFO_REQUEST = 1204,//悠先服务客户端手机登录
        ZZB_CLIENT_MODIFY_USERINFO_RESPONSE = 1205,

        ZZB_CLIENT_QUERY_SHOPLIST_REQUEST = 1206,//悠先服务客户端获取店铺列表
        ZZB_CLIENT_QUERY_SHOPLIST_RESPONSE = 1207,

        CLIENT_QUERY_PREORDER_EVALUATIONINFO_REQUEST = 1208,//客户端查询点单评分所需信息
        CLIENT_QUERY_PREORDER_EVALUATIONINFO_RESPONSE = 1209,

        CLIENT_EVALUATE_PREORDER_REQUEST = 1210,//客户端评价点单
        CLIENT_EVALUATE_PREORDER_RESPONSE = 1211,

        CLIENT_QUERY_PREORDER_NOTEVALUATED_REQUEST = 1212,//客户端查询未评价的点单
        CLIENT_QUERY_PREORDER_NOTEVALUATED_RESPONSE = 1213,

        CLIENT_DISH_PRAISE_REQUEST = 1214,//点单菜品点赞
        CLIENT_DISH_PRAISE_RESPONSE = 1215,


        CLIENT_DIRECT_PAYMENT_REQUEST = 1220,//客户端直接付款（add by wangc 20140319）
        CLIENT_DIRECT_PAYMENT_RESPONSE = 1221,

        ZZB_CLIENT_QUERY_UPDATEINFO_REQUEST = 1222,//悠先服务查询版本更新信息
        ZZB_CLIENT_QUERY_UPDATEINFO_RESPONSE = 1223,

        /// <summary>
        /// 客户端发送语音短信
        /// </summary>
        CLIENT_SEND_VOICEMESSAGE_REQUEST = 1224,//客户端发送语音短信
        CLIENT_SEND_VOICEMESSAGE_RESPONSE = 1225,

        CLIENT_SEND_ERRORMESSAGE_REQUEST = 1226,//客户端发送错误日志
        CLIENT_SEND_ERRORMESSAGE_RESPONSE = 1227,

        CLIENT_QUERY_BUILD_REQUEST = 1228,//客户端查询版本信息
        CLIENT_QUERY_BUILD_RESPONSE = 1229,

        ZZB_CLIENT_SEND_ERRORMESSAGE_REQUEST = 1230,//悠先服务客户端发送错误日志
        ZZB_CLIENT_SEND_ERRORMESSAGE_RESPONSE = 1231,

        /// <summary>
        /// 悠先服务客户端发送语音短信请求
        /// </summary>
        ZZB_CLIENT_SEND_VOICEMESSAGE_REQUEST = 1232,//悠先服务客户端发送语音短信
        /// <summary>
        /// 悠先服务客户端发送语音短信回复
        /// </summary>
        ZZB_CLIENT_SEND_VOICEMESSAGE_RESPONSE = 1233,

        /// <summary>
        /// 悠先服务客户端菜品管理权限请求
        /// </summary>
        ZZB_CLIENT_DISH_MANAGE_ROLE_REQUEST = 1234,
        /// <summary>
        /// 悠先服务客户端菜品管理权限回复
        /// </summary>
        ZZB_CLIENT_DISH_MANAGE_ROLE_RESPONSE = 1235,

        /// <summary>
        /// 悠先服务客户端菜单查询请求
        /// </summary>
        ZZB_CLIENT_DISH_SEARCH_REQUEST = 1236,
        /// <summary>
        /// 悠先服务客户端菜单查询回复
        /// </summary>
        ZZB_CLIENT_DISH_SEARCH_RESPONSE = 1237,

        /// <summary>
        /// 悠先服务客户端菜沽清请求
        /// </summary>
        ZZB_CLIENT_DISH_SELLOFF_REQUEST = 1238,
        /// <summary>
        /// 悠先服务客户端菜沽清回复
        /// </summary>
        ZZB_CLIENT_DISH_SELLOFF_RESPONSE = 1239,
        /// <summary>
        /// 悠先服务客户端push token更新请求
        /// </summary>
        ZZB_CLIENT_PUSHTOKEN_UPDATE_REQUEST = 1240,
        /// <summary>
        /// 悠先服务客户端push token更新回复
        /// </summary>
        ZZB_CLIENT_PUSHTOKEN_UPDATE_RESPONSE = 1241,
        /// <summary>
        /// 美食日记分享请求
        /// </summary>
        CLIENT_FOODDIARY_SHARED_REQUEST = 1242,
        /// <summary>
        /// 美食日记分享回复
        /// </summary>
        CLIENT_FOODDIARY_SHARED_RESPONSE = 1243,
        //客户端个人中心充值抢购粮票 add by wangc 20140504
        CLIENT_PERSON_CENTER_RECHARGE_REQUEST = 1250,
        CLIENT_PERSON_CENTER_RECHARGE_REPONSE = 1251,
        //客户端带充值功能支付接口 add by wangc 20140504
        CLIENT_RECHARGE_PAYMENT_ORDER_REQUEST = 1252,
        CLIENT_RECHARGE_PAYMENT_ORDER_RESPONSE = 1253,
        //客户端带充值功能支付接口V1 add by LinDY 20150528
        CLIENT_RECHARGE_PAYMENT_ORDER_V1_REQUEST = 125201,
        CLIENT_RECHARGE_PAYMENT_ORDER_V1_RESPONSE = 125301,
        //客户端带充值功能直接支付接口
        CLIENT_RECHARGE_DIRECT_PAYMENT_REQUEST = 1254,
        CLIENT_RECHARGE_DIRECT_PAYMENT_RESPONSE = 1255,
        //客户端带充值功能直接支付接口V1
        CLIENT_RECHARGE_DIRECT_PAYMENT_V1_REQUEST = 125401,
        CLIENT_RECHARGE_DIRECT_PAYMENT_V1_RESPONSE = 125501,

        /// <summary>
        /// 悠先点菜客户端更新用户push token请求
        /// </summary>
        CLIENT_PUSHTOKEN_UPDATE_REQUEST = 1261,
        /// <summary>
        /// 悠先点菜客户端更新用户push token回复
        /// </summary>
        CLIENT_PUSHTOKEN_UPDATE_RESPONSE = 1262,
        /// <summary>
        /// 悠先服务菜品价格修改请求
        /// </summary>
        ZZB_CLIENT_DISH_PRICE_MODIFY_REQUEST = 1263,
        /// <summary>
        /// 悠先服务菜品价格修改回复
        /// </summary>
        ZZB_CLIENT_DISH_PRICE_MODIFY_RESPONSE = 1264,
        /// <summary>
        /// 悠先服务任务状态查询请求
        /// </summary>
        ZZB_CLIENT_TASK_CHECK_STATUS_REQUEST = 1265,
        /// <summary>
        /// 悠先服务任务状态查询回复
        /// </summary>
        ZZB_CLIENT_TASK_CHECK_STATUS_RESPONSE = 1266,

        /*悠先服务  客户追溯模块*/
        //客户追溯用户信息
        ZZB_CLIENT_RETROSPECT_CUSTOMER_REQUEST = 1270,
        ZZB_CLIENT_RETROSPECT_CUSTOMER_RESPONSE = 1271,

        //查看追溯到当前用户的历史点单列表
        ZZB_CLIENT_RETROSPECT_CUSTOMER_ORDER_REQUEST = 1272,
        ZZB_CLIENT_RETROSPECT_CUSTOMER_ORDER_RESPONSE = 1273,

        //查询追溯到用户某个历史点单的详情内容
        ZZB_CLIENT_RETROSPECT_CUSTOMER_ORDERDETAIL_REQUEST = 1274,
        ZZB_CLIENT_RETROSPECT_CUSTOMER_ORDERDETAIL_RESPONSE = 1275,

        //调整追溯到用户的余额
        ZZB_CLIENT_RETROSPECT_CUSTOMER_CHANGEBALANCE_REQUEST = 1276,
        ZZB_CLIENT_RETROSPECT_CUSTOMER_CHANGEBALANCE_RESPONSE = 1277,

        //悠先服务客户追溯查看用户红包领取情况
        ZZB_CLIENT_CHECK_REDENVELOPE_DETAIL_REQUEST = 1278,
        ZZB_CLIENT_CHECK_REDENVELOPE_DETAIL_RESPONSE = 1279,

        //悠先服务查看门店会员数目
        ZZB_CLIENT_CHECK_SHOP_VIPUSERSINFO_REQUEST = 1280,
        ZZB_CLIENT_CHECK_SHOP_VIPUSERSINFO_RESPONSE = 1281,

        //悠先服务权限判断
        /// <summary>
        /// 悠先服务权限判断请求
        /// </summary>
        ZZB_CLIENT_ROLE_CHECK_REQUEST = 1282,
        /// <summary>
        /// 悠先服务权限判断回复
        /// </summary>
        ZZB_CLIENT_ROLE_CHECK_RESPONSE = 1283,

        /// <summary>
        /// 悠先服务-客户信息请求
        /// </summary>
        ZZB_CLINET_CUSTOMER_DETAILS_REQUEST = 1284,
        /// <summary>
        /// 悠先服务-客户信息回复
        /// </summary>
        ZZB_CLINET_CUSTOMER_DETAILS_RESPONSE = 1285,

        /// <summary>
        /// 悠先服务-点单列表请求
        /// </summary>
        ZZB_CLIENT_PREORDERLIST2_REQUEST = 1286,
        /// <summary>
        /// 悠先服务-点单列表回复
        /// </summary>
        ZZB_CLIENT_PREORDERLIST2_RESPONSE = 1287,

        /// <summary>
        /// 悠先服务-修改订单桌号请求
        /// </summary>
        ZZB_CLINET_MODIFY_DESK_NUMBER_REQUEST = 1288,
        /// <summary>
        /// 悠先服务-修改订单桌号回复
        /// </summary>
        ZZB_CLINET_MODIFY_DESK_NUMBER_RESPONSE = 1289,

        /// <summary>
        /// 悠先服务客户端菜单查询请求
        /// </summary>
        ZZB_CLIENT_DISH_ALL_SEARCH_REQUEST = 1290,
        /// <summary>
        /// 悠先服务客户端菜单查询回复
        /// </summary>
        ZZB_CLIENT_DISH_ALL_SEARCH_RESPONSE = 1291,

        /// <summary>
        /// 悠先服务-点单列表附加信息请求（拆分版）
        /// </summary>
        ZZB_CLIENT_PREORDERLIST_ATTACG_REQUEST = 1292,
        /// <summary>
        /// 悠先服务-点单列表附加信息回复（拆分版）
        /// </summary>
        ZZB_CLIENT_PREORDERLIST_ATTACG_RESPONSE = 1293,


        //客户端个人中心查看红包领用记录
        CLIENT_CHECK_REDENVELOPE_DETAIL_REQUEST = 1300,
        CLIENT_CHECK_REDENVELOPE_DETAIL_RESPONSE = 1301,

        //悠先点菜-客户端查看用户红包金额
        CLIENT_CHECK_REDENVELOPE_REQUEST = 1302,
        CLIENT_CHECK_REDENVELOPE_RESPONSE = 1303,

        //悠先点菜商圈
        CLIENT_CHECK_BUSINESSDISTRICT_REQUEST = 1311,
        CLIENT_CHECK_BUSINESSDISTRICT_RESPONSE = 1312,

        //悠先点菜美食广场
        CLIENT_CHECK_FOODPLAZA_REQUEST = 1313,
        CLIENT_CHECK_FOODPLAZA_REPONSE = 1314,

        //悠先服务配菜查询
        ZZB_CHECK_DISHINGREDIENTS_REQUEST = 1321,
        ZZB_CHECK_DISHINGREDIENTS_REPONSE = 1322,

        //悠先服务配菜沽清
        ZZB_SELLOFF_DISHINGREDIENTS_REQUEST = 1323,
        ZZB_SELLOFF_DISHINGREDIENTS_REPONSE = 1324,

        //悠先点菜客户端信息获取和举报
        CLIENT_SHOP_REPORT_REQUEST = 1500,
        CLIENT_SHOP_REPORT_RESPONSE = 1501,
        /// <summary>
        /// 店铺评价列表查询请求
        /// </summary>
        ZZB_CLIENT_SHOP_PREORDERLIST_REQUEST = 1502,
        /// <summary>
        /// 店铺评价列表查询返回
        /// </summary>
        ZZB_CLIENT_SHOP_PREORDERLIST_RESPONSE = 1503,

        CLIENT_CUSTOMER_COUPONDETAIL_REQUEST = 1600,
        CLIENT_CUSTOMER_COUPONDETAIL_RESPONSE = 1601,
        /// <summary>
        /// 悠先点菜更新设备的UUID请求
        /// </summary>
        CLIENT_UPDATE_UUID_RESQUEST = 1610,
        /// <summary>
        /// 悠先点菜更新设备的UUID返回
        /// </summary>
        CLIENT_UPDATE_UUID_RESPONSE = 1611,

        /// <summary>
        /// 当前抵扣卷请求
        /// </summary>
        CLIENT_CUSTOMER_CURRENTCOUPONDETAIL_REQUEST = 1612,

        /// <summary>
        /// 当前抵扣卷响应
        /// </summary>
        CLIENT_CUSTOMER_CURRENTCOUPONDETAIL_RESPONSE = 1613,

        /// <summary>
        /// 历史抵扣卷请求
        /// </summary>
        CLIENT_CUSTOMER_HISTORYCOUPONDETAIL_REQUEST = 1614,

        /// <summary>
        /// 历史抵扣卷响应
        /// </summary>
        CLIENT_CUSTOMER_HISTORYCOUPONDETAIL_RESPONSE = 1615,

        /// <summary>
        /// 悠先点菜剪刀手入座接口请求
        /// </summary>
        CLIENT_PREORDER_CONFIRM_REQUEST = 1602,
        /// <summary>
        /// 悠先点菜剪刀手入座接口返回
        /// </summary>
        CLIENT_PREORDER_CONFIRM_RESPONSE = 1603,
        /// <summary>
        /// 悠先点菜未入座点单提醒请求
        /// </summary>
        CLIENT_UNCONFIRM_PREORDER_REMIND_REQUEST = 1604,
        /// <summary>
        /// 悠先点菜未入座点单提醒返回
        /// </summary>
        CLIENT_UNCONFIRM_PREORDER_REMIND_RESPONSE = 1605,
        /// <summary>
        /// 悠先点菜用户是否绑定微信
        /// </summary>
        CLIENT_CUSTOMER_WECHAT_ISBINDING_REQUEST = 1700,
        /// <summary>
        /// 悠先点菜用户是否绑定微信
        /// </summary>
        CLIENT_CUSTOMER_WECHAT_ISBINDING_RESPONSE = 1701,
        /// <summary>
        /// 悠先点菜用户绑定微信
        /// </summary>
        CLIENT_CUSTOMER_WECHAT_BINDINGMOBILE_REQUEST = 1702,
        /// <summary>
        /// 悠先点菜用户绑定微信
        /// </summary>
        CLIENT_CUSTOMER_WECHAT_BINDINGMOBILE_RESPONSE = 1703,
        /// <summary>
        /// 悠先点菜用户是否绑定微信
        /// </summary>
        CLIENT_CUSTOMER_WECHAT_ISBINDINGMOBILE_REQUEST = 1704,
        /// <summary>
        /// 悠先点菜用户是否绑定微信
        /// </summary>
        CLIENT_CUSTOMER_WECHAT_ISBINDINGMOBILE_RESPONSE = 1705,

        /// <summary>
        /// 新增公告
        /// </summary>
        CLIENT_SHOP_NOTICE_REQUEST = 1721,
        CLIENT_SHOP_NOTICE_RESPONSE = 1722,

        /// <summary>
        /// 抽奖
        /// </summary>
        CLIENT_LOTTERY_REQUEST = 1723,
        CLIENT_LOTTERY_RESPONSE = 1724,

        /// <summary>
        /// 统计列表接口
        /// </summary>
        COUNTING_LIST_REQUEST = 1725,
        COUNTING_LIST_RESPONSE = 1726,

        /// <summary>
        /// 补差价查询
        /// </summary>
        CLIENT_PAY_DIFFERENCE_QUERY_REQUEST = 1706,
        CLIENT_PAY_DIFFERENCE_QUERY_RESPONSE = 1707,

        /// <summary>
        /// 补差价申请
        /// </summary>
        CLIENT_PAY_DIFFERENCE_REQUEST = 1708,
        CLIENT_PAY_DIFFERENCE_RESPONSE = 1709,

        /// <summary>
        /// 支付成功通知
        /// </summary>
        CIIENT_PAY_SUCCESS_REQUEST = 1710,
        CIIENT_PAY_SUCCESS_RESPONSE = 1711
    }
    /// <summary>
    /// ipad端控件类型
    /// </summary>
    public enum VAControlType
    {
        UIScrollView = 1,
        UIImageView = 2
    }
    /// <summary>
    /// ipad端控件属性
    /// </summary>
    public enum VAControlProperty
    {
        Width = 1,
        Height = 2,
        X = 3,
        Y = 4,
        Image = 5
    }
    /// <summary>
    /// 商户店铺状态
    /// </summary>
    public enum VAShopStatus
    {
        [Description("已停业")]
        SHOP_TINGYE = -2,
        [Description("已删除")]
        SHOP_DELETED = -1,
        [Description("营业中")]
        SHOP_NORMAL = 1,
    }
    /// <summary>
    /// 商户店铺审核情况
    /// </summary>
    public enum VAShopHandleStatus
    {
        [Description("未审核")]
        SHOP_UnHandle = -2,
        [Description("未审核通过")]
        SHOP_UnPass = -1,
        [Description("审核通过")]
        SHOP_Pass = 1,
    }
    /// <summary>
    /// 优惠券类型
    /// </summary>
    public enum VACouponType
    {
        [Description("通用折扣券")]
        DISCOUNT_GENERAL_CAMPAIGN_TYPE = 1, // 通用折扣券 不设特定菜品
        [Description("特定折扣券")]
        DISCOUNT_DISH_CAMPAIGN_TYPE = 2,    // 特定折扣券，至少一个特定菜品 
        [Description("通用抵价券")]
        DEDUCT_GENERAL_CAMPAIGN_TYPE = 21,   // 通用抵价券，不设特定菜品
        [Description("特定抵价券")]
        DEDUCT_DISH_CAMPAIGN_TYPE = 22,      // 特定抵价券，至少一个特定菜品
        [Description("套餐特价券")]
        MEAL_SPECIAL_CAMPAIGN_TYPE = 30,     // 套餐特价券，至少一个菜品
        [Description("其他券")]
        OTHER_COUPON_TYPE = 99    //其他
    }
    /// <summary>
    /// 优惠活动状态
    /// </summary>
    public enum VACouponStatus
    {
        [Description("已删除")]
        COUPON_DELETED = -1,
        [Description("正常")]
        COUPON_NORMAL = 1,
        [Description("已下架")]
        COUPON_OFFLINE = 2,
    }
    /// <summary>
    /// 优惠券使用类型
    /// </summary>
    public enum VACouponRequirementType
    {
        [Description("展示即可")]
        REQUIRE_DISPLAY = 1,      //展示即可使用
        [Description("需要下载")]
        REQUIRE_PURCHASE = 2,     //需要下载/购买
        [Description("其他")]
        REQUIRE_OTHER = 99        //其他
    }
    /// <summary>
    /// 城市状态
    /// </summary>
    public enum VACityStatus
    {
        [Description("未开通")]
        WEI_KAI_TONG = 1,
        [Description("已开通")]
        YI_KAI_TONG = 2,
    }
    /// <summary>
    /// 优惠券排序类别
    /// </summary>
    public enum VACouponSearchSortOption
    {
        MOSTRECOMMENDED = 1, // 默认推荐
        NEARESTDISTANCE = 2,       // 距离
        MOSTDISCOUNT = 3,       // 优惠力度
        MOSTDOWNLOADS = 4,      // 下载量
        LATESTPUBLISHED = 5,
    }
    /// <summary>
    /// 地图状态
    /// </summary>
    public enum VAMapStatus
    {
        [Description("已删除")]
        DELETED = -1,
        [Description("未使用")]
        NOT_IN_USE = 1,
        [Description("使用中")]
        IN_USE = 2,
    }
    /// <summary>
    /// 客户端类型
    /// 如果修改该枚举需要修改CustomEncourageOperate中的DealCustomEncourage
    /// </summary>
    public enum VAAppType
    {
        [Description("iPhone")]
        IPHONE = 1,
        [Description("iPad")]
        IPAD = 2,
        [Description("Android")]
        ANDROID = 3,
        [Description("WAP")]
        WAP = 4,
    }
    /// <summary>
    /// 悠先服务客户端类型
    /// </summary>
    public enum VAServiceType
    {
        [Description("iPhone")]
        IPHONE = 1,
        [Description("iPad")]
        IPAD = 2,
        [Description("Android")]
        ANDROID = 3,
        [Description("PC")]
        PC = 4,
    }
    /// <summary>
    /// Open_Id类型
    /// </summary>
    public enum VAOpenIdVendor
    {
        OPEN_ID_SINA = 1,
        OPEN_ID_QQ = 2,
        OPEN_ID_RENREN = 3,
        OPEN_ID_KAIXIN = 4,
        OPEN_ID_DOUBAN = 5,
        OPEN_ID_QZONE = 6,
    }
    /// <summary>
    /// 用户已购买的优惠券状态
    /// 对应数据库表[CustomerConnCoupon]中的status
    /// </summary>
    public enum VACustomerCouponStatus
    {
        [Description("未使用")]
        NOT_USED = 1,
        [Description("已使用")]
        USED = 2,
        [Description("已过期")]
        OVERDUE = 3,//由后台服务处理该状态

        //状态4（wangcheng）
        [Description("未下载，未查看")]
        NOT_DOWNLOAD = 4,//未下载，未查看
        //状态5（wangcheng）
        [Description("已查看，未下载")]
        CHECKED = 5,//未下载，已查看
        //4-5-1-2-3
    }

    /// <summary>
    /// 公司建立活动发送优惠券之后优惠券状态
    /// 对应数据库表[CompanyEncourageConnCustomerCoupon]中的status（wangcheng）
    /// </summary>
    public enum VACompanyEncourageConnCouponStatus
    {
        //成功建立活动后的默认状态
        [Description("已发送")]
        SEND = 1,
        //手机客户端下载优惠券后状态
        [Description("已下载")]
        DOWNLOAD = 2,
        //手机客户端已查看信息，但未下载优惠券
        [Description("已查看")]
        CHECKED = 3,
    }

    /// <summary>
    /// 优惠券订单状态
    /// 对应数据库表[CustomerCouponOrder]中的status
    /// </summary>
    public enum VACustomerCouponOrderStatus
    {
        [Description("未支付")]
        NOT_PAID = 1,
        [Description("已支付")]
        PAID = 2,
    }
    /// <summary>
    /// 充值订单状态
    /// 对应数据库表[CustomerChargeOrder]中的status
    /// </summary>
    public enum VACustomerChargeOrderStatus
    {
        [Description("未支付")]
        NOT_PAID = 1,
        [Description("已支付")]
        PAID = 2,
    }
    /// <summary>
    /// 支付宝订单状态
    /// 对应数据库表[AlipayOrderInfo]中的orderStatus
    /// </summary>
    public enum VAAlipayOrderStatus
    {
        [Description("未支付")]
        NOT_PAID = 1,
        [Description("已支付")]
        PAID = 2,
        /// <summary>
        /// 重复支付
        /// </summary>
        [Description("重复支付")]
        REPEAT_PAID = 3,
        /// <summary>
        /// 重复支付退款中
        /// </summary>
        [Description("重复支付退款中")]
        REPEAT_REFUNDING = 4,
        /// <summary>
        /// 重复支付已退款
        /// </summary>
        [Description("重复支付已退款")]
        REFUNDED = 5,
    }
    /// <summary>
    /// 客户端查询优惠券的类型
    /// 对应VAUserCouponListRequest
    /// </summary>
    public enum VAUserCouponType
    {
        USER_COUPON_VALID_AND_UNUSED,   // 未过期、未使用
        USER_COUPON_USED,               // 已使用
        USER_COUPON_EXPIRED_UNUSED,// 已过期、未使用
        USER_COUPON_USED_OK, //可使用
        USER_COUPON_USED_UNEFFECT, //尚未生效
        USER_COUPON_NOTUSED,//还没使用
    }
    /// <summary>
    /// 插入、修改、删除等函数返回结果
    /// </summary>
    public class FunctionResult
    {
        public string message { get; set; }
        public int returnResult { get; set; }
        //初始化
        public FunctionResult()
        {
            message = "";
            returnResult = 1;
        }
    }
    /// <summary>
    /// 用户货币变化原因
    /// 系统奖励部分，不包括CustomEncourage表中定义的自定义奖励原因
    /// </summary>
    public enum VA19dianMoneyChangeReason
    {
        [Description("每日登录奖励")]
        MONEY19DIAN_LOGIN_REWORD = 1,
        [Description("注册奖励")]
        MONEY19DIAN_REGISTER_REWORD = 2,
        [Description("使用优惠券奖励")]
        MONEY19DIAN_VERIFY_COUPON_REWORD = 3,
        [Description("验证手机奖励")]
        MONEY19DIAN_VERIFY_MOBILE_REWORD = 4,
        [Description("用户推荐奖励")]
        MONEY19DIAN_INVITE_CUSTOMER_REWORD = 5,
        [Description("下载优惠券")]
        MONEY19DIAN_DOWNLOAD_COUPON = 6,
        [Description("活动奖励")]
        MONEY19DIAN_CUSTOM_ENCOURG_SEND_MONEY = 7,
        [Description("支付{0}预点单")]
        MONEY19DIAN_PREPAY_PREORDER = 8,
        [Description("账户充值")]
        MONEY19DIAN_Account_Recharge = 9,
        [Description("预点单{0}退款")]
        MONEY19DIAN_REFUND_PREORDER = 10,
        [Description("直接付款{0}预点单")]
        MONEY19DIAN_DIRECT_PAYMENT = 11,
        [Description("平台充值")]
        MONEY19DIAN_SERVICE_RECHARGE = 12,
        [Description("平台充值奖励{0}")]
        MONEY19DIAN_SERVICE_RECHARGE_PRESENT = 13,
        [Description("补差价{0}")]
        MONEY19DIAN_SERVICE_PAY_DIFFERENCE = 14,
    }
    /// <summary>
    /// 自定义奖励类型
    /// 从10001开始，以便与VAMessageType区分
    /// </summary>
    public enum VACustomEncourageType
    {
        [Description("直接送钱奖励")]
        ENCOURGTYPE_SEND_MONEY = 10001,
        [Description("直接送券奖励")]
        ENCOURGTYPE_SEND_COUPON = 10002,
    }
    /// <summary>
    /// 微博分享类型
    /// </summary>
    public enum VAWeiboShareType
    {
        [Description("预点单分享")]
        PREORDER_SHARE = 1,
    }
    /// <summary>
    /// 网站中显示的图片的分类
    /// </summary>
    public enum VAImageType
    {
        [Description("菜品图片")]
        DISH_IMAGE = 1,
        [Description("横幅广告图片")]
        BANNER_IMAGE = 2,
        [Description("门店形象宣传图片")]
        SHOP_PUBLICITY_IMAGE = 3,
        [Description("门店Logo")]
        SHOP_LOGO_IMAGE = 4,
        [Description("门店环境图片")]
        SHOP_SETTING_IMAGE = 5,
        [Description("菜品大图")]
        DISH_IMAGE_BIG = 6,
        [Description("菜品广场图")]
        DISH_IMAGE_PLAZA = 7,
        [Description("门脸图版本2")]
        SHOP_PUBLICITY_IMAGE_VERSION2 = 8,
        [Description("广告图版本2")]
        BANNER_IMAGE_VERSION2 = 9,
        [Description("门店Logo版本2")]
        SHOP_LOGO_IMAGE_VERSION2 = 10,
        [Description("门店勋章")]
        SHOP_MEDAL_IMAGE = 11,
        [Description("用户头像")]
        CUSTOMER_IMAGE = 12,
        [Description("菜品小图")]
        DISH_IMAGE_SMALL = 13,
        [Description("门脸图版本3")]
        SHOP_PUBLICITY_IMAGE_VERSION3 = 14,
        [Description("门店详情版本3")]
        SHOP_DETAIL_IMAGE_VERSION3 = 15,
        [Description("预点单分享图片")]
        PREORDER_SHARE_IMAGE = 118,
        [Description("屏幕启动图")]
        START_MAP = 16,
        [Description("门脸图版本4")]
        SHOP_PUBLICITY_IMAGE_VERSION4 = 17,
        [Description("门脸图版本5")]
        SHOP_PUBLICITY_IMAGE_VERSION5 = 18,
        [Description("门脸图广告类型")]
        SHOP_PUBLICITY_AD_IMAGE = 19,
        [Description("消息Logo")]
        MESSAGE_LOGO = 20,
        [Description("消息图片")]
        MESSAGE_IMAGE = 21,
    }
    /// <summary>
    /// 支付订单与19点订单的关联类型
    /// Edit at 2014-4-2 增加店铺名称
    /// </summary>
    public enum VAPayOrderType
    {
        //[Description("购买优惠券")]
        //BUY_COUPON = 1,
        [Description("支付预点单")]
        PAY_PREORDER = 2,
        [Description("支付粮票")]
        PAY_CHARGE = 3,
        [Description("支付{0}点单")]
        PAY_PREORDER_NEW = 4,
        [Description("直接支付{0}点单")]
        DIRECT_PAYMENT = 5,
        [Description("支付{0}点单")]
        PAY_PREORDER_AND_RECHARGE = 6,
        [Description("直接支付{0}点单")]
        DIRECT_PAYMENT_AND_RECHARGE = 7,
        /// <summary>
        /// 补差价
        /// </summary>
        [Description("补差价{0}点单")]
        PAY_DIFFENENCE = 8,
    }
    /// <summary>
    /// 优惠活动显示状态
    /// </summary>
    public enum VACouponDisplayStatus
    {
        [Description("未开始")]
        NOT_STARTED = 1,
        [Description("正常")]
        NORMAL = 2,
        [Description("已结束")]
        HAS_ENDED = 3,
    }
    /// <summary>
    /// 优惠活动验证状态
    /// </summary>
    public enum VACouponVerifyStatus
    {
        [Description("未开始")]
        NOT_STARTED = 1,
        [Description("正常")]
        NORMAL = 2,
        [Description("已过期")]
        HAVE_EXPIRED = 3,
    }
    /// <summary>
    /// 预点单状态
    /// </summary>
    public enum VAPreorderStatus : byte
    {
        [Description("未提交")]
        Initial = 100,
        [Description("已提交")]
        Uploaded = 101,
        [Description("已付款")]
        Prepaid = 102,
        //[Description("已验证")]//20140228 wangcheng
        /// <summary>
        /// 已审核(已入座)
        /// </summary>
        [Description("已审核")]
        Completed = 103,
        [Description("已删除")]
        Deleted = 104,
        [Description("已退款")]
        Refund = 105,
        [Description("已过期")]
        Overtime = 106,
        [Description("退款中")]
        OriginalRefunding = 107,//原路退款中
        [Description("作废")]
        Cancel = 108,//原路退款中
        [Description("处理中")]
        Handling = 109,//原路退款中
    }

    public struct OrderType
    {
        /// <summary>
        /// 正常点单
        /// </summary>
        [Description("正常")]
        public static int Normal = 1;

        /// <summary>
        /// 补差价点单
        /// </summary>
         [Description("补差价")]
        public static int PayDifference = 2;
    }

    /// <summary>
    /// 投诉类型
    /// </summary>
    public enum VAComplainType
    {
        [Description("预点单")]
        Initial = 100,
        [Description("优惠券")]
        Uploaded = 101,
    }
    /// <summary>
    /// 商家支付给友络的佣金类型
    /// </summary>
    public enum VACommissionType
    {
        [Description("数值")]
        Normal_Value = 1,
        [Description("比例")]
        Proportion = 2,
    }
    /// <summary>
    /// 预支付返现的类型
    /// </summary>
    public enum VAPrePayCashBackType
    {
        [Description("数值")]
        Normal_Value = 1,
        [Description("比例")]
        Proportion = 2,
        [Description("优惠券")]
        Coupon = 3,
    }
    /// <summary>
    /// 预点单是否支付
    /// </summary>
    public enum VAPreorderIsPaid
    {
        [Description("未支付")]
        NOT_PAID = 0,
        [Description("已支付")]
        PAID = 1,
    }
    /// <summary>
    /// 预点单是否对账
    /// </summary>
    public enum VAPreorderIsApproved
    {
        [Description("未对账")]
        NOT_APPROVED = 0,
        [Description("已对账")]
        APPROVED = 1,
    }
    /// <summary>
    /// 预点单是否验证
    /// </summary>
    public enum VAPreorderIsShopVerified
    {
        [Description("未验证")]
        NOT_SHOPVERIFIED = 0,
        [Description("已验证")]
        SHOPVERIFIED = 1,
    }
    /// <summary>
    /// 预付后是否已经返现
    /// </summary>
    public enum VACashBackReceived
    {
        [Description("否")]
        NOT_CASHBACKRECEIVED = 0,
        [Description("是")]
        CASHBACKRECEIVED = 1,
    }

    public enum SubsidyTypes
    {
        /// <summary>
        /// 按订单
        /// </summary>
        ByOrder = 1,
        /// <summary>
        /// 按叠加次数
        /// </summary>
        ByCount = 2
    }
    /// <summary>
    /// 广告区域
    /// </summary>
    public enum VAAdvertisementArea
    {
        [Description("店铺Banner")]
        SHOP_BANNER = 1,
        [Description("优惠券Banner")]
        COUPON_BANNER = 2,
        [Description("宣传Banner")]
        DISSEMINATE_BANNER = 3,
        [Description("红包Banner")]
        REDENVELOPE_BANNER = 4,
        [Description("专题Banner")]
        SUBJECT_BANNER = 5,
        [Description("美食广场店铺Banner")]
        FOODPLAZA_SHOP_BANNER = 21,
        [Description("美食广场宣传Banner")]
        FOODPLAZA_DISSEMINATE_BANNER = 23,
        [Description("美食广场专题Banner")]
        FOODPLAZA_SUBJECT_BANNER = 25,
    }
    /// <summary>
    /// 广告大分类
    /// </summary>
    public enum VAAdvertisementClassify
    {
        /// <summary>
        /// 首页广告
        /// </summary>
        INDEX_AD = 1,
        /// <summary>
        /// 美食广场广告
        /// </summary>
        FOODPLAZA_AD = 2,
    }
    /// <summary>
    /// 广告类型
    /// </summary>
    public enum VAAdvertisementType
    {
        [Description("公司")]
        COMPANY = 1,
        [Description("优惠券")]
        COUPON = 2,
        [Description("其他")]
        OTHER = 3,
    }
    /// <summary>
    /// 广告开启状态
    /// </summary>
    public enum VAAdvertisementStatus
    {
        [Description("关闭")]
        NOT_AVAILABLE = 0,
        [Description("可用")]
        AVAILABLE = 1,

    }

    /// <summary>
    /// 预付款，友络和商家结算是否完成
    /// </summary>
    public enum VATransactionCompleted
    {
        [Description("未完成")]
        NOT_COMPLETED = 1,
        [Description("已完成")]
        COMPLETED = 2,

    }
    /// <summary>
    /// 投诉单的对应冻结资金状态
    /// </summary>
    public enum VAFreezeMoneyStatus
    {
        [Description("未冻结")]
        NOT_FREEZED = 1,
        [Description("已冻结")]
        FREEZED = 2,
        [Description("已解冻")]
        UN_FREEZED = 3,
    }
    /// <summary>
    /// 投诉单的投诉状态
    /// </summary>
    public enum VAComplainStatus
    {
        [Description("申诉中")]
        COMPLAINING = 1,
        [Description("申诉完成")]
        COMPLAIN_COMPLETE = 2,

    }
    /// <summary>
    /// 投诉单的赔款状态
    /// </summary>
    public enum VAReparationStatus
    {
        [Description("未赔款")]
        NOT_ISPAID = 1,
        [Description("已赔款")]
        ISPAID = 2,

    }
    /// <summary>
    /// 预点单是否被申请打款
    /// </summary>
    public enum VAPreOrderIsApplied
    {
        [Description("未申请")]
        NOT_ISAPPLIED = 1,
        [Description("已申请")]
        ISAPPLIED = 2,
    }
    /// <summary>
    /// 映射记录的映射类型
    /// </summary>
    public enum VAMappingStatus
    {
        [Description("预点单")]
        PREORDER = 1,
        [Description("投诉冻结金")]
        FREEZED = 2,
        [Description("投诉解冻金")]
        UNFREEZED = 3,
    }
    /// <summary>
    /// 回款申请的类型
    /// </summary>
    public enum VAApplyStatus
    {
        [Description("审核中")]
        CHECKING = 1,
        [Description("审核通过")]
        CHECK_PASSED = 2,
        [Description("审核未通过")]
        CHECK_NOT_PASS = 3,
        [Description("已汇款")]
        PAID = 4,
    }
    /// <summary>
    /// 申诉对应类型
    /// </summary>
    public enum VAComplainValue
    {
        [Description("预点单")]
        PREORDER = 1,
        [Description("优惠券")]
        TICKET = 2,

    }
    /// <summary>
    /// 支付类型
    /// </summary>
    public enum VAPaymentChannel
    {
        [Description("支付宝网页支付")]
        ALIPAY = 1,
        [Description("支付宝客户端支付")]
        ALIQUICKPAY = 2,
        [Description("银联")]
        UNIONPAY = 3,
    }
    /// <summary>
    /// 是否开启杂项收费（wangcheng）
    /// </summary>
    public enum VASundry
    {
        [Description("已开启")]
        OPENED = 1,
        [Description("未开启")]
        CLOSED = 2,
    }
    /// <summary>
    /// 店铺杂项收费模式（wangcheng）
    /// </summary>
    public enum VASundryChargeMode
    {
        [Description("固定金额")]
        FIXEDAMOUNT = 1,
        [Description("按比例")]
        PROPORTION = 2,
        [Description("按人次")]
        POPULATION = 3,
    }
    /// <summary>
    /// 悠先入座的开启状态（wangcheng）
    /// </summary>
    public enum VAUserLottery
    {
        [Description("已开启")]
        USERLOTTERY_OPENED = 1,
        [Description("未开启")]
        USERLOTTERY_CLOSED = 2,
    }
    /// <summary>
    /// 预点单验证（wangcheng）
    /// 2013-7-26 商家（shop）审核预点单
    /// </summary>
    public enum VAPreOrderShopConfirmed
    {
        [Description("预点单未审核")]
        NOT_SHOPCONFIRMED = 0,
        [Description("预点单审核")]
        SHOPCONFIRMED = 1,
    }

    /// <summary>
    /// 特殊权限（wangcheng）
    /// </summary>
    public enum VASpecialAuthority
    {
        //[Description("点单验证码权限")]
        //CHECK_PREORDER_VERIFICATION = 1,

        #region 客服操作
        [Description("（客服）点单对账权限")]
        CHECK_PREORDER_AMOUNT = 2,
        [Description("（客服）点单审核权限")]
        CHECK_PREORDER_CONFIRM = 3,
        //[Description("（客服）点单验证权限")]
        //CHECK_PREORDER_VERIFY = 4,
        //[Description("（客服）点单撤销权限")]
        //CHECK_PREORDER_CANCEL = 5,
        //[Description("（客服）点单退款权限")]
        //CHECK_PREORDER_REFUND = 19,
        #endregion

        #region 公司列表
        [Description("（公司列表）仅显示所有上线公司权限")]
        CHECK_PREORDER_ONLINE_COMPANY = 6,
        [Description("（公司列表）显示所有公司权限")]
        CHECK_ALL_COMPANY = 12,
        //[Description("（公司列表）分配城市公司列表")]
        //CHECK_COMPANY_BY_PROVINCE_CITY = 17,
        #endregion

        #region 公司奖励发布优惠券活动筛选用户特殊权限
        //[Description("（公司新建活动）所有注册用户")]
        //ALL_REGISTER_CUSTOMERS = 7,
        //[Description("（公司新建活动）所有点单用户")]
        //ALL_PREORDER_CUSTOMERS = 8,
        //[Description("（公司新建活动）菜均筛选用户")]
        //DISHAMOUNT_AVERAGE = 9,
        //[Description("（公司新建活动）到店次数筛选用户")]
        //BACKSHOP_CUSTOMERS = 10,
        //[Description("（公司新建活动）指定用户")]
        //APPOINT_CUSTOMERS = 11,
        #endregion

        #region 店铺信息操作
        //[Description("（店铺信息）设置预付返现")]
        //SHOP_PREPAYPRIVILEGELIST = 13,
        [Description("（店铺信息）餐厅杂项管理")]
        SHOP_SHOPSUNDRYMANAGE = 14,
        //[Description("（店铺信息）悠先入座管理")]
        //SHOP_USERLOTTERY = 15,
        [Description("（店铺信息）门店图片展示管理")]
        SHOP_SHOPIMAGEREVELATION = 16,
        [Description("（店铺信息）店铺VIP折扣管理")]
        SHOP_VIPDISCOUNT = 21,
        #endregion

        //[Description("（预付返现）显示所有可用优惠券")]
        //PREPAYPRIVILEGE_CHECK_ALL_ONLINE_COUPON = 18,

        [Description("（掌中宝）审核退款对账权限")]
        ZZB_ISCASHIERAUTHORITY = 20,
        //最大值21，添加时，注意
    }
    public enum VAClientPayMode
    {
        [Description("支付宝客户端支付")]
        ALI_PAY_PLUGIN = 1,
        [Description("支付宝网页支付")]
        ALI_PAY_WEB = 2,//已暂停使用20140129xiaoyu
        [Description("银联客户端支付")]
        UNION_PAY_PLUGIN = 3,
        [Description("微信客户端支付")]
        WECHAT_PAY_PLUGIN = 4,//add at 2014-3-12
    }
    /// <summary>
    /// 勋章类别
    /// </summary>
    public enum VAMedalType
    {
        [Description("公司勋章")]
        MEDAL_COMPANY = 1,
        [Description("门店勋章")]
        MEDAL_SHOP = 2,
    }
    /// <summary>
    /// 勋章图片规格MedalImageInfo
    /// </summary>
    public enum VAMedalImageType
    {
        [Description("勋章小图")]
        MEDAL_IMAGE_SMALL = 1,
        [Description("勋章大图")]
        MEDAL_IMAGE_BIG = 2,
    }
    /// <summary>
    /// 访问API的类型
    /// </summary>
    public enum VAInvokedAPIType
    {
        [Description("刷新公司列表")]
        API_REFRESH_COMPANY_LIST = 1,
        [Description("用户登录")]
        API_USERS_LOGIN = 2,
        [Description("用户激活")]
        API_USERS_REGISTER = 3,
        [Description("查询预点单列表")]
        API_SELECT_PREORDER_LIST = 4,
    }
    /// <summary>
    /// 免返送
    /// </summary>
    public enum VAQueueFreeAndCouponBackAndDishGift
    {
        [Description("免")]
        SHOP_QUEUERFEE = 1,
        [Description("返")]
        SHOP_COUPONBACK = 2,
        [Description("送")]
        SHOP_DISHGIFT = 3,
    }

    public enum VADishManageType
    {
        [Description("请先选择分类")]
        PLEASE_CHOSE_TYPE = 1,
        [Description("保存失败")]
        SAVE_FAIL = 2,
        [Description("保存成功")]
        SAVE_SUCCESS = 3,
        [Description("已存在相同菜名")]
        EXITS_SAME_DISHNAME = 4,
        [Description("请选择需要上传的图片")]
        PLEASE_CHOSE_IMAGE = 5,
        [Description("请输入规格名称")]
        PLEASE_INPUTNAME = 6,
        [Description("请输入规格价格")]
        PLEASE_INPUTSCALE = 7,
        [Description("保存并跟新项")]
        SAVEANDUPDATE = 8,
    }
    public enum VAShopDetailFillType
    {
        [Description("默认全部显示")]
        DEFAULT = 1,
        [Description("用户优惠卷列表显示")]
        CUSTOMERCOUPON = 2,
    }
    /// <summary>
    /// 优惠券相对时间和绝对时间
    /// </summary>
    public enum VACouponTimeType
    {
        [Description("相对时间")]
        RELATIVE_TIME = 1,
        [Description("绝对时间")]
        ABSOLUTE_TIME = 2,
    }
    /// <summary>
    /// 活动类型分类
    /// </summary>
    public enum VAEncouragetype
    {
        [Description("自定义活动")]
        FROM_ENCOURAGE = 1,
        [Description("预付返现活动")]
        FROM_PREPAYPRIVILEGE = 2,
        [Description("自定义优惠券领取活动")]
        FROM_COUPONSRECEIVEACTIVITIES = 3,
    }
    /// <summary>
    /// 是否判断 1：是，2否
    /// </summary>
    public enum VAWhethertype
    {
        [Description("是")]
        WHETHERYES = 1,
        [Description("否")]
        WHETHERNO = 2,
    }
    /// <summary>
    /// 发票操作类型
    /// </summary>
    public enum VAInvoiceTitleOperate
    {
        [Description("查询")]
        INVOICETITLE_QUERY = 1,
        [Description("删除")]
        INVOICETITLE_DELETE = 2,
        [Description("新增")]
        INVOICETITLE_ADD = 3,
        [Description("其他操作")]
        OTHER = 4,
    }
    /// <summary>
    /// 客服操作记录日志枚举
    /// </summary>
    public enum VACustomerServiceOperateType
    {
        [Description("对账")]
        ACCOUNT_CHECKING = 1,
        [Description("取消对账")]
        CANCLE_ACCOUNT_CHECKING = 2,
        [Description("审核")]
        ACCOUNT_CONFRIM = 3,
        [Description("取消审核")]
        CANCLE_ACCOUNT_CONFRIM = 4,
        [Description("验证")]
        ORDER_VERIFY = 5,
        [Description("撤单")]
        ORDER_CANCLE = 6,
        [Description("退款")]
        ORDER_REFUND = 7,
    }
    /// <summary>
    /// 员工操作的页面
    /// </summary>
    public enum VAEmployeeOperateLogOperatePageType
    {
        //公司CompanyManage
        [Description("公司信息")]
        COMPANYINFO = 1,
        [Description("公司银行账户")]
        COMPANY_ACCOUNTS = 2,
        [Description("公司佣金设置")]
        COMPANY_COMMISSIONANDFREEREFUNDHOUR = 3,
        [Description("vip等级设置")]
        COMPANY_VIPLIST = 4,
        [Description("勋章管理")]
        COMPANY_MEDALMANAGE = 5,

        //门店ShopManage
        [Description("店铺信息")]
        SHOPINFO = 21,
        [Description("预付奖励")]
        SHOP_PREPAYPRIVILEGELIST = 22,
        [Description("店铺审核")]
        SHOP_HANDLE = 23,
        [Description("店铺图片展示")]
        SHOP_IMAGEREVELATION = 24,
        [Description("店铺杂项")]
        SHOP_SUNDRYMANAGE = 25,
        [Description("店铺赠送")]
        SHOP_UPDATEGIFT = 26,
        [Description("悠先入座")]
        SHOP_USERLOTTERY = 27,

        //点单PreOrder19dianManage
        [Description("点单审核")]
        PREORDER_SHOPCONFIRMED = 41,
        [Description("点单对账")]
        PREORDER_SHOPAPPROVED = 42,
        [Description("点单验证")]
        PREORDER_SHOPVERIFIED = 43,
        [Description("点单撤单")]
        SHOP_CANCLE = 44,
        [Description("点单退款")]
        SHOP_REFUND = 45,
        [Description("密码修改")]
        PASSWORD_MODIFY = 200,

        //菜相关
        //菜谱MenuManage
        [Description("菜谱基本信息")]
        MENUINFO = 61,
        //菜品分类DishType
        [Description("菜品分类基本信息")]
        DISHTYPEINFO = 71,
        //菜DishManage
        [Description("菜基本信息")]
        DISHINFO = 81,
        [Description("沽清")]
        DISH_CURRENTSELLOFF = 82,
        [Description("菜价格规格信息")]
        DISH_PRICEINFO = 83,



        //广告
        [Description("广告基本信息")]
        ADVERTISEMENTINFO = 91,
        [Description("广告排期")]
        ADMATCHBANNER = 92,

        //收银宝退款
        [Description("收银宝退款")]
        SYB_REFUND = 100,

        [Description("财务统计")]
        FINANCE_STATISITICE = 101,
    }
    /// <summary>
    /// 员工操作类型
    /// </summary>
    public enum VAEmployeeOperateLogOperateType
    {
        [Description("增加")]
        ADD_OPERATE = 1,
        [Description("修改")]
        UPDATE_OPERATE = 2,
        [Description("删除")]
        DELETE_OPERATE = 3,
        [Description("查询")]
        QUERY_OPERATE = 4,
    }
    /// <summary>
    /// 功能描述:操作状态（批量进行操作时需要记录状态）
    /// 创建标识:罗国华 20131104
    /// </summary>
    public enum OperStatus
    {
        /// <summary>
        /// 新增
        /// </summary>
        Insert = 1,
        /// <summary>
        /// 修改
        /// </summary>
        Edit = 2,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 3,
        /// <summary>
        /// 多图新增
        /// </summary>
        MutInsert = 4
    }

    /// <summary>
    /// 功能描述:收支来源 1:支付宝充值，2:银联充值，3：用户取消订单 4.商户退款 5.财务对账 6.商户结账
    ///       (目前用于商户 客户及公司流水表)
    /// 创建标识:罗国华 20131120
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// 支付宝充值
        /// </summary>
        [Description("支付宝充值")]
        ALIPAY = 1,
        /// <summary>
        /// 银联充值
        /// </summary>
        [Description("银联充值")]
        UNIONPAY = 2,
        /// <summary>
        /// 客户端用户取消订单
        /// </summary>
        [Description("用户取消订单")]
        USER_CANCEL_ORDER = 3,
        /// <summary>
        /// 收银宝商户点单退款
        /// </summary>
        [Description("点单退款")]
        ORDER_OUTCOME = 4,
        /// <summary>
        /// 点单收入
        /// </summary>
        [Description("点单收入")]
        ORDER_INCOME = 5,
        /// <summary>
        /// 结账扣款
        /// </summary>
        [Description("结账扣款")]
        MERCHANT_CHECKOUT = 6,
        /// <summary>
        /// 用户消费
        /// </summary>
        [Description("用户消费")]
        USER_CONSUME = 7,
        /// <summary>
        /// 购买优惠券
        /// </summary>
        [Description("购买优惠券")]
        BUY_COUPON = 8,
        /// <summary>
        /// 邀请用户奖励
        /// </summary>
        [Description("邀请用户奖励")]
        INVITE_USER = 9,
        /// <summary>
        /// 每日登录奖励
        /// </summary>
        [Description("每日登录奖励")]
        USER_LOGIN = 10,
        /// <summary>
        /// 用户注册
        /// </summary>
        [Description("用户注册")]
        USER_REGISTER = 11,
        /// <summary>
        /// 绑定手机奖励
        /// </summary>
        [Description("绑定手机奖励")]
        BIND_MOBILE = 12,
        /// <summary>
        /// 友络佣金
        /// </summary>
        [Description("友络佣金")]
        VIEWALLOC_COMMISSION = 13,
        /// <summary>
        /// 微信支付
        /// </summary>
        [Description("微信支付")]
        WECHATPAY = 14,
        /// <summary>
        /// 平台充值
        /// </summary>
        [Description("平台充值")]
        SERVICERECHARGE = 15,
        /// <summary>
        /// 平台充值赠送
        /// </summary>
        [Description("平台充值赠送")]
        SERVICERECHARGE_PRESENT = 16,
        /// <summary>
        /// 商户购买套餐
        /// </summary>
        [Description("礼券套餐扣款")]
        PURCHASE_PACKAGES = 17
    }

    /// <summary>
    /// 功能描述:收支类型 1：收入，2：支出
    ///       (目前用于商户 客户及公司流水表)
    /// 创建标识:罗国华 20131120
    /// </summary>
    public enum InoutcomeType
    {
        /// <summary>
        /// 收入
        /// </summary>
        IN = 1,
        /// <summary>
        /// 支出
        /// </summary>
        OUT = 2
    }

    //推荐餐厅 类型: 主推荐餐厅 次推荐餐厅 特色餐厅
    public enum VARecommandType
    {
        [Description("主推荐餐厅")]
        RECOMMAND_FIRST = 0,
        [Description("次推荐餐厅")]
        RECOMMAND_SECOND = 1,
        [Description("特色餐厅")]
        RECOMMAND_FEATURE = 2,
    }
    /// <summary>
    /// 原路退款的类型
    /// </summary>
    public enum VAOriginalRefundType
    {
        /// <summary>
        /// 预点单
        /// </summary>
        [Description("预点单")]
        PREORDER = 1,
        /// <summary>
        /// 重复支付点单
        /// </summary>
        [Description("重复支付点单")]
        REPEAT_PREORDER = 2,
    }

    /// <summary>
    /// 原路退款申请单状态
    /// </summary>
    public enum VAOriginalRefundStatus
    {
        /// <summary>
        /// 打款申请中
        /// </summary>
        [Description("打款中")]
        REMITTING = 1,
        /// <summary>
        /// 已打款
        /// </summary>
        [Description("已打款")]
        REMITTED = 2,
        /// <summary>
        /// 打款处理中
        /// </summary>
        Processing = 3,
        /// <summary>
        /// 打款失败
        /// </summary>
        Failure = 4

    }
    /// <summary>
    /// 门店服务员积分获取积分方式（wangcheng 20140222）
    /// </summary>
    public enum PointVariationMethods
    {
        [Description("礼品兑换")]
        GOODS_EXCHANGE = 1,
        [Description("审核点单获取积分")]
        CUSTOMER_EXPENSE_GET = 2,
        [Description("友络公司奖励")]
        VIEWALLOC_REWARDS = 3,
        [Description("客户评分获得积分")]
        CLIENT_VALIDATION = 4,
        [Description("退款减积分")]
        REFUND_VALIDATION = 5,
        [Description("取消审核点单减少积分")]
        CUSTOMER_CANCEL_REDUCE = 6,
    }
    /// <summary>
    /// 客户端类型
    /// </summary>
    public enum VAClientType
    {
        [Description("uxian")]
        UXIAN = 1,
        [Description("uxianService")]
        UXIANSERVICE = 2,
    }
    /// <summary>
    /// 官网配置文件类别
    /// </summary>
    public enum VAOfficialWebType
    {
        [Description("精选成功案例")]
        CLASS_CASE = 1,
        [Description("部分合作商户")]
        COOPERATE = 2,
        [Description("最新动态")]
        RECENT_NEWS = 3,
        [Description("最新动态类别")]
        RECENT_NEWS_TYPE = 4,
        [Description("更新历史")]
        UPDATE_HISTORY = 5,
    }

    public enum VAAliRefundStatus
    {
        [Description("退款中")]
        REFUNDING = 1,
        [Description("退款成功")]
        REFUND_SUCCESS = 2,
        [Description("退款失败")]
        REFUND_FAILED = 3,
    }
    public enum VAOrderUsedPayMode
    {
        /// <summary>
        /// 余额
        /// </summary>
        [Description("粮票支付")]
        BALANCE = 1,
        /// <summary>
        /// 支付宝
        /// </summary>
        [Description("支付宝")]
        ALIPAY = 2,
        /// <summary>
        /// 微信
        /// </summary>
        [Description("微信支付")]
        WECHAT = 3,
        /// <summary>
        /// 银联
        /// </summary>
        [Description("银联支付")]
        UNIONPAY = 4,
        /// <summary>
        /// 未支付点单
        /// </summary>
        NOT_PAY_ORDER = 5,
        /// <summary>
        /// 红包支付
        /// </summary>
        [Description("红包支付")]
        REDENVELOPE = 6,
        /// <summary>
        /// 抵扣券
        /// </summary>
        [Description("抵扣券")]
        COUPON = 7

    }
    /// <summary>
    /// 单条推送信息类别
    /// </summary>
    public enum VAPushMessage
    {
        支付成功 = 1,
        入座成功 = 2,
        退款成功 = 3,
    }

    public enum VAAccessTokenType
    {
        /// <summary>
        /// 微信支付AccessToken
        /// </summary>
        wechatPay = 1,
        /// <summary>
        /// 微信公众号AccessToken
        /// </summary>
        wechatGongzhong = 2,
        /// <summary>
        /// 公众号用于调用微信JS接口的临时票据
        /// </summary>
        jsApiTicket = 3
    }

    /// <summary>
    /// 悠先服务推广活动类别
    /// </summary>
    public enum PromotionActivityType
    {
        /// <summary>
        /// 抵扣券
        /// </summary>
        [Description("抵扣券")]
        coupon = 1,
        /// <summary>
        /// 短信
        /// </summary>
        [Description("短信")]
        message = 2,
        /// <summary>
        /// App 推送
        /// </summary>
        [Description("app推送")]
        push = 3
    }
    /// <summary>
    /// 订单入座操作者类别
    /// </summary>
    public enum PreOrderConfirmOperater
    {
        /// <summary>
        /// 收银宝
        /// </summary>
        [Description("收银宝")]
        Cash = 1,
        /// <summary>
        /// 悠先服务
        /// </summary>        
        [Description("悠先服务")]
        Service = 2,
        /// <summary>
        /// 悠先点菜
        /// </summary>
        [Description("悠先点菜")]
        Client = 3
    }

    /// <summary>
    /// 抵扣券返回类型 add by zhujinlei 2015/06/16
    /// </summary>
    public enum CouponTypeEnum
    {
        [Description("用户自己领取的")]
        OneSelf = 1,
        [Description("店铺发布的")]
        Bussiness = 2
    }

    /// <summary>
    /// 点单类型 add by zhujinlei 2015/06/26
    /// </summary>
    public enum OrderTypeEnum
    { 
        /// <summary>
        /// 正常点单
        /// </summary>
        [Description("正常点单")]
        Normal=1,
        /// <summary>
        /// 补差价点单
        /// </summary>
        [Description("补差价点单")]
        PayDifference=2
    }
    /// <summary>
    /// 奖品类型
    /// </summary>
    public enum AwardType
    {
        [Description("未抽奖")]
        NotLottery = -1,
        [Description("未中奖")]
        NotWin = 1,
        [Description("免排队")]
        AvoidQueue = 2,
        [Description("送菜")]
        PresentDish = 3,
        [Description("送红包")]
        PresentRedEnvelope = 4,
        [Description("送第三方")]
        PresentThirdParty = 5
    }

    /// <summary>
    /// 位置选择
    /// </summary>
    public enum PositionType
    {
        [Description("本店")]
        ThisShop = 1,
        [Description("附近")]
        Nearby = 2
    }

    /// <summary>
    /// 计费类型
    /// </summary>
    public enum ValuationType
    {
        [Description("按次计费")]
        Started = 1,
        [Description("按人次计费")]
        ByPerson = 2
    }

    /// <summary>
    /// 模板是否启用
    /// </summary>
    public enum PackageStatus
    {
        [Description("未启用")]
        Disabled = 0,
        [Description("启用")]
        Enable = 1
    }

    /// <summary>
    /// 商户星级
    /// </summary>
    public enum ShopLevel
    {
        [Description("一星")]
        Level1 = 0,
        [Description("二星")]
        Level2 = 2,
        [Description("三星")]
        Level3 = 3,
        [Description("四星")]
        Level4 = 4,
        [Description("五星")]
        Level5 = 5,
        [Description("一钻")]
        Level6 = 6,
        [Description("二钻")]
        Level7 = 12,
        [Description("三钻")]
        Level8 = 18,
        [Description("四钻")]
        Level9 = 24,
        [Description("五钻")]
        Level10 = 30,
        [Description("一冠")]
        Level11 = 36,
        [Description("二冠")]
        Level12 = 72,
        [Description("三冠")]
        Level13 = 108,
        [Description("四冠")]
        Level14 = 144,
        [Description("五冠")]
        Level15 = 180
    }

    /// <summary>
    /// 券类型
    /// </summary>
    public enum CouponType
    {
        /// <summary>
        /// 通用券
        /// </summary>
        [Description("通用券")]
        GeneralVouchers = 1,
        /// <summary>
        /// 运营数据
        /// </summary>
        [Description("运营数据")]
        OperatingData = 2,
        /// <summary>
        /// 会员营销券
        /// </summary>
        [Description("会员营销券")]
        Package = 3
    }

    /// <summary>
    /// 活动消息类型
    /// </summary>
    public enum MsgType
    {
        /// <summary>
        /// 纯文本消息
        /// </summary>
        [Description("纯文本消息")]
        PureText = 1,
        /// <summary>
        /// 专题广告
        /// </summary>
        [Description("专题广告")]
        SpecialAdvertisement = 2,
        /// <summary>
        /// 红包广告
        /// </summary>
        [Description("红包广告")]
        RedEnvelopeAdvertisement = 3,
        /// <summary>
        /// 商户礼券
        /// </summary>
        [Description("商户礼券")]
        CommercialTenantPackage = 4
    }

    /// <summary>
    /// 业务事件类型
    /// </summary>
    public class BussnessEventType
    {
        /// <summary>
        /// 订单支付成功
        /// </summary>
        public static string OrderPayment = "79A50902-D360-4203-BF3B-2A88052173CB";
        /// <summary>
        /// 订单入座成功
        /// </summary>
        public static string OrderSeat = "CD78B218-91F4-4F9F-8B5C-E1DEB3913ECC";
        ///// <summary>
        ///// 订单评价
        ///// </summary>
        public static string OrderEvaluation = "2257A903-3A97-4600-9054-03EA94DAF38F";
        ///// <summary>
        ///// 菜品点赞
        ///// </summary>
        public static string DishesPraise = "AAA67F49-48B9-434A-ACB0-4458CA33348C";
        ///// <summary>
        ///// 完善个人信息
        ///// </summary>
        public static string EditUserInfo = "F9477B97-1ABF-4558-8648-26C05912E2A5";
        ///// <summary>
        ///// 收藏店铺
        ///// </summary>
        public static string CollectShop = "AC158CB3-8E28-47DD-8615-A3E22364F290";
        ///// <summary>
        ///// 分享店铺
        ///// </summary>
        public static string ShareShop = "E5D8C064-17E3-4CE3-A8E6-147B0E8490F4";
    }

    /// <summary>
    /// 事件类型
    /// </summary>
    public enum EventType
    {
        /// <summary>
        /// 首次
        /// </summary>
        [Description("首次")]
        First = 1,
        /// <summary>
        /// 区间
        /// </summary>
        [Description("区间")]
        Section = 2,
        /// <summary>
        /// 单次
        /// </summary>
        [Description("单次")]
        Single = 3,
        /// <summary>
        /// 累计
        /// </summary>
        [Description("累计")]
        Cumulative = 4

    }

    public enum EventComplement
    {
        /// <summary>
        /// 金额
        /// </summary>
        [Description("金额")]
        Amount = 1,
        /// <summary>
        /// 次数
        /// </summary>
        [Description("次数")]
        Count = 2
    }

    /// <summary>
    /// 积分规则状态
    /// </summary>
    public enum IntegrationStatus
    {
        [Description("未启用")]
        Disabled = 0,
        [Description("启用")]
        Enable = 1,
        [Description("删除")]
        Delete = -1
    }
}
