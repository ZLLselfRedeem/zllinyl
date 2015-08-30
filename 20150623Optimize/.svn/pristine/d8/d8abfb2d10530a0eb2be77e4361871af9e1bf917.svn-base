using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class CustomerInfo
    {
        public CustomerInfo() {
            CustomerRankID = 0;
            RegisterDate = new DateTime(1970, 1, 1);
            CustomerSex = 0;
            CustomerBirthday = new DateTime(1970, 1, 1);
            CustomerStatus = 0;
            localAlarm = false;
            localAlarmHour = 0;
            receivePushForFavoriteRestaurants = false;
            isVIP = false;
            registerCityId = 0;
            preOrderTotalAmount = 0;
            preOrderTotalQuantity = 0;
            defaultPayment = 0;
            verificationCodeErrCnt = 0;
        }
        /// <summary>
        /// 用户编号
        /// </summary>
        public long CustomerID { get; set; }
        /// <summary>
        /// 用户等级编号
        /// </summary>
        public int? CustomerRankID { get; set; }
        /// <summary>
        /// 用户登录名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户登录密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户注册时间
        /// </summary>
        public DateTime? RegisterDate { get; set; }
        /// <summary>
        /// 用户姓
        /// </summary>
        public string CustomerFirstName { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string CustomerLastName { get; set; }
        /// <summary>
        /// 用户性别
        /// 2：女，1：男，0：不详
        /// </summary>
        public int? CustomerSex { get; set; }
        /// <summary>
        /// 用户生日
        /// </summary>
        public DateTime? CustomerBirthday { get; set; }
        /// <summary>
        /// 用户电话
        /// </summary>
        public string mobilePhoneNumber { get; set; }
        /// <summary>
        /// 用户住址
        /// </summary>
        public string CustomerAddress { get; set; }
        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string customerEmail { get; set; }
        /// <summary>
        /// 用户状态(-1:已删除，1：正常)
        /// </summary>
        public int? CustomerStatus { get; set; }
        /// <summary>
        /// 用户证书
        /// </summary>
        public string cookie { get; set; }
        /// <summary>
        /// 密保问题
        /// </summary>
        public string securityQuestion { get; set; }
        /// <summary>
        /// 密保问题对应答案
        /// </summary>
        public string securityAnswer { get; set; }
        /// <summary>
        /// 是否开启本地定时提醒
        /// </summary>
        public bool? localAlarm { get; set; }
        /// <summary>
        /// 本地提醒的点
        /// </summary>
        public int? localAlarmHour { get; set; }
        /// <summary>
        /// 本地提醒的分
        /// </summary>
        public int? localAlarmMinute { get; set; }
        /// <summary>
        /// 是否接受推送提醒
        /// </summary>
        public bool? receivePushForFavoriteRestaurants { get; set; }
        /// <summary>
        /// 是否VIP用户
        /// </summary>
        public bool? isVIP { get; set; }
        /// <summary>
        /// VIP过期时间
        /// </summary>
        public DateTime? vipExpireDate { get; set; }
        /// <summary>
        /// 微信号码
        /// </summary>
        public string wechatId { get; set; }
        /// <summary>
        /// 用户尊称（最多10个汉字）
        /// </summary>
        public string titleName { get; set; }

        /// <summary>
        /// 注册城市编号
        /// </summary>
        public int? registerCityId { get; set; }

        /// <summary>
        /// 验证点单总金额
        /// </summary>
        public double? preOrderTotalAmount { get; set; }
        /// <summary>
        /// 验证点单总份数
        /// </summary>
        public int? preOrderTotalQuantity { get; set; }
        /// <summary>
        /// 客户端存储个人图片信息
        /// </summary>
        public string personalImgInfo { get; set; }
        /// <summary>
        /// 用户默认支付方式 add by wangc 20140512
        /// </summary>
        public int? defaultPayment { get; set; }

        public bool IsUpdatePicture { get; set; }
        public string Picture { get; set; }
        /// <summary>
        /// 验证码错误次数
        /// </summary>
        public int? verificationCodeErrCnt { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        public string verificationCode { set; get; }

        /// <summary>
        /// 验证码手机号
        /// </summary>
        public string verificationCodeMobile { set; get; }

        /// <summary>
        /// 设备锁定时间
        /// </summary>
        public DateTime unlockTime { set; get; }

        /// <summary>
        /// 发送验证码时间
        /// </summary>
        public DateTime verificationCodeTime { set; get; }
        
        /// <summary>
        /// 是否修改过用户名
        /// </summary>
        public int? IsUpdateUserName { set; get; }
    }


}