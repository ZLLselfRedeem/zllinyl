﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;
using System.Transactions;
using VA.AllNotifications;
using System.Configuration;
using LogDll;
using System.Collections;
using UmengPush;
using VA.CacheLogic;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 自定义推送（悠先点菜）
    /// </summary>
    public class CustomPushRecordOperate
    {
        CustomPushConfigManager pushManager = new CustomPushConfigManager();

        /// <summary>
        /// 新增一条自定义推送
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public long InsertCustomPushRecord(CustomPushRecord record)
        {
            return pushManager.InsertCustomPushRecord(record);
        }

        /// <summary>
        /// 批量新增自定义推送记录
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool BatchInsertCustomPushRecord(DataTable dt)
        {
            return pushManager.BatchInsertCustomPushRecord(dt);
        }

        /// <summary>
        /// 更新推送记录的发送相关信息
        /// isLocked, sendTime, isSent, sendCount（增量）
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public bool UpdateCustomPushRecord(CustomPushRecord record)
        {
            return pushManager.UpdateCustomPushRecord(record);
        }

        /// <summary>
        /// 删除一条未发送的自定义推送
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteCustomPushRecord(long id)
        {
            return pushManager.DeleteCustomPushRecord(id);
        }

        public DataTable LockNotification()
        {
            DataTable result = new DataTable();
            DataTable dtRecord = pushManager.SelectCustomPushRecord();
            if (dtRecord.Rows.Count == 1)
            {
                long id = Common.ToInt64(dtRecord.Rows[0]["id"]);
                using (TransactionScope scope = new TransactionScope())
                {
                    if (pushManager.UpdateRecordLock(id))
                    {
                        scope.Complete();
                        result = dtRecord;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 查询推送列表
        /// </summary>
        /// <returns></returns>
        public List<CustomPushRecord> QueryAllCustomPushRecord(SQLServerDAL.Persistence.Infrastructure.Page page, int customType, bool isSent, string beginTime, string endTime, out int cnt, long id = 0)
        {
            return pushManager.SelectAllCustomPushRecord(page, customType, isSent, beginTime, endTime, out cnt, id);
        }
        /// <summary>
        /// 查询推送列表
        /// </summary>
        /// <returns></returns>
        public DataTable QueryAllCustomPush(long id = 0, int customType = 0, string mobilePhone = "")
        {
            return pushManager.SelectAllCustomPush(id, customType, mobilePhone);
        }


        /// <summary>
        /// 根据用户手机号码检查其最新登录的设备信息
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public CustomerDevicePushInfo QueryCustomerInfo(string mobilePhoneNumber)
        {
            return pushManager.SelectCustomerInfo(mobilePhoneNumber);
        }

        /// <summary>
        /// 根据用户ID查询其所有未支付的订单
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable QueryNotPaidOrders(long customerId)
        {
            return pushManager.SelectNotPaidOrders(customerId);
        }

        /// <summary>
        /// 根据店铺名称查询店铺Id
        /// </summary>
        /// <param name="shopName"></param>
        /// <returns></returns>
        public string QueryShopId(string shopName)
        {
            return pushManager.SelectShopId(shopName);
        }

        /// <summary>
        /// 根据拼接好的手机号码组查询用户设备信息
        /// </summary>
        /// <param name="phoneArray">手机号码用英文逗号隔开</param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<CustomerDevicePushInfo> QueryCustomerInfoList(SQLServerDAL.Persistence.Infrastructure.Page page, string phoneArray, out int cnt)
        {
            return pushManager.SelectCustomerInfoList(page, phoneArray, out cnt);
        }

        public void RedEnevlopeNotification(long treasureChestId)
        {
            try
            {
                LogManager.WriteLog(LogFile.Trace, DateTime.Now + ":进入发送推送方法");
                RedEnvelopeManager redEnevlope = new RedEnvelopeManager();
                List<string> phoneArray = redEnevlope.SelectCustomerMobilePhone(treasureChestId);
                if (phoneArray != null && phoneArray.Count > 0)
                {
                    string strPhone = "";
                    for (int i = 0; i < phoneArray.Count; i++)
                    {
                        strPhone += "'" + phoneArray[i] + "',";
                    }
                    strPhone = strPhone.Remove(strPhone.Length - 1, 1);
                    LogManager.WriteLog(LogFile.Trace, DateTime.Now + ":找到此宝箱抢到红包的手机号码---" + strPhone);
                    int cnt = 0;
                    List<CustomerDevicePushInfo> customerDevice = pushManager.SelectCustomerInfoList(new SQLServerDAL.Persistence.Infrastructure.Page(1, 500), strPhone, out cnt);
                    if (customerDevice != null && customerDevice.Count > 0)
                    {
                        LogManager.WriteLog(LogFile.Trace, DateTime.Now + ":找到接收推送的设备" + customerDevice.Count + "个");
                        DataTable dt = new DataTable();
                        dt.Columns.Add(new DataColumn("isLocked", typeof(Boolean)));
                        dt.Columns.Add(new DataColumn("pushToken", typeof(String)));
                        dt.Columns.Add(new DataColumn("addTime", typeof(DateTime)));
                        dt.Columns.Add(new DataColumn("customSendTime", typeof(DateTime)));
                        dt.Columns.Add(new DataColumn("isSent", typeof(bool)));
                        dt.Columns.Add(new DataColumn("sendCount", typeof(Int32)));
                        dt.Columns.Add(new DataColumn("appType", typeof(Int32)));
                        dt.Columns.Add(new DataColumn("message", typeof(String)));
                        dt.Columns.Add(new DataColumn("customType", typeof(String)));
                        dt.Columns.Add(new DataColumn("customValue", typeof(String)));
                        dt.Columns.Add(new DataColumn("mobilePhoneNumber", typeof(String)));
                        dt.Columns.Add(new DataColumn("customerId", typeof(Int64)));

                        foreach (CustomerDevicePushInfo item in customerDevice)
                        {
                            DataRow dr = dt.NewRow();
                            dr["isLocked"] = false;
                            dr["pushToken"] = item.pushToken;
                            dr["addTime"] = DateTime.Now;
                            if (DateTime.Now.Hour >= 9 && DateTime.Now.Hour <= 22)
                            {
                                dr["customSendTime"] = DateTime.Now.AddMinutes(1);
                            }
                            else if (DateTime.Now.Hour > 22 && DateTime.Now.Hour <= 23)
                            {
                                dr["customSendTime"] = Common.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy/MM/dd") + " 09:00:00");
                            }
                            else if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 9)
                            {
                                dr["customSendTime"] = Common.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd") + " 09:00:00");
                            }

                            dr["isSent"] = false;
                            dr["sendCount"] = 0;
                            dr["appType"] = (int)item.appType;

                            double amount = redEnevlope.SelectCustomerRedEnvelopeAmount(treasureChestId, item.mobilePhoneNumber);
                            if (amount > 0.001)
                            {
                                dr["message"] = ConfigurationManager.AppSettings["RedEnvelopePush"].ToString().Replace("{0}", amount.ToString());
                            }
                            else
                            {
                                break;//没有钱则跳出，此客户不该收到推送，也不用收短信
                            }
                            dr["customType"] = ((int)VA.AllNotifications.VANotificationsCustomType.NOTIFICATIONS_REDENVELOPE).ToString();
                            dr["customValue"] = WebConfig.ServerDomain + "AppPages/RedEnvelope/list.aspx?mobilePhone=" + item.mobilePhoneNumber;
                            dr["mobilePhoneNumber"] = item.mobilePhoneNumber;
                            dr["customerId"] = item.CustomerID;
                            dt.Rows.Add(dr);
                            LogManager.WriteLog(LogFile.Trace, DateTime.Now + ":构造数据完毕：" + item.mobilePhoneNumber);
                        }

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            LogManager.WriteLog(LogFile.Trace, DateTime.Now + ":待推送数据不为空");
                            using (TransactionScope ts = new TransactionScope())
                            {
                                bool insert = pushManager.BatchInsertCustomPushRecord(dt);

                                if (insert)
                                {
                                    LogManager.WriteLog(LogFile.Trace, DateTime.Now + ":插入推送表成功");
                                    ts.Complete();
                                }
                            }
                        }
                        //customerDevice.Clear();//--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                        for (int i = 0; i < phoneArray.Count; i++)
                        {
                            bool exists = customerDevice.Exists(x => x.mobilePhoneNumber == phoneArray[i]);
                            if (!exists)//不存在则发短信
                            {
                                double amount = redEnevlope.SelectCustomerRedEnvelopeAmount(treasureChestId, phoneArray[i]);
                                if (amount > 0.001)
                                {
                                    //Common.SendMessageBySms(phoneArray[i], msg);
                                    SMSYimei.SMSYimeiVoice smsYimeiVoice = new SMSYimei.SMSYimeiVoice();

                                    DateTime sendTime = new DateTime();
                                    if (DateTime.Now.Hour >= 9 && DateTime.Now.Hour <= 22)
                                    {
                                        sendTime = DateTime.Now.AddMinutes(1);
                                    }
                                    else if (DateTime.Now.Hour > 22 && DateTime.Now.Hour <= 23)
                                    {
                                        sendTime = Common.ToDateTime(DateTime.Now.AddDays(1).ToString("yyyy/MM/dd") + " 09:00:00");
                                    }
                                    else if (DateTime.Now.Hour >= 0 && DateTime.Now.Hour < 9)
                                    {
                                        sendTime = Common.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd") + " 09:00:00");
                                    }
                                    string[] mobList = new string[] { phoneArray[i] };
                                    DateTime currentTime = DateTime.Now;

                                    string msg = ConfigurationManager.AppSettings["RedEnvelopeMessage"].ToString().Replace("{0}", amount.ToString());
                                    long smsId = Common.ToInt64(Common.ToSecondFrom1970(currentTime).ToString() + Common.randomStrAndNum(5));
                                    if (smsYimeiVoice.SendMessage(sendTime.ToString("yyyyMMddHHmmss"), mobList, msg, smsId) == VAResult.VA_OK)
                                    {
                                        LogManager.WriteLog(LogFile.Trace, DateTime.Now + ":定制短信提交成功：" + phoneArray[i] + "--时间：" + sendTime.ToString("yyyyMMddHHmmss"));
                                    }
                                    else
                                    {
                                        LogManager.WriteLog(LogFile.Trace, DateTime.Now + ":定制短信提交失败：" + phoneArray[i] + "--时间：" + sendTime.ToString("yyyyMMddHHmmss"));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(LogFile.Trace, DateTime.Now + ":Exception：" + ex.Message);
            }
        }

        /// <summary>
        /// 支付/入座/退款 成功后，给客户发推送
        /// </summary>
        /// <param name="uniPushInfo"></param>
        public void UniPush(object objUniPushInfo)
        {
            UniPushInfo uniPushInfo = (UniPushInfo)objUniPushInfo;
            CustomPushConfigManager customPushConfigManager = new CustomPushConfigManager();
            CustomerDevicePushInfo customerPushInfo = customPushConfigManager.SelectCustomerInfo(uniPushInfo.customerPhone);
            if (customerPushInfo != null)
            {
                string pushMsg = "";
                switch (uniPushInfo.pushMessage)
                {
                    case VAPushMessage.支付成功:
                        //pushMsg = uniPushInfo.shopName + VAPushMessage.支付成功.ToString();
                        pushMsg = "您有1笔订单未入座";
                        break;
                    case VAPushMessage.入座成功:
                        //pushMsg = uniPushInfo.shopName + VAPushMessage.入座成功.ToString();
                        pushMsg = "您有1笔订单未评价";
                        break;
                    case VAPushMessage.退款成功:
                        pushMsg = uniPushInfo.shopName + VAPushMessage.退款成功.ToString();
                        break;
                    default:
                        pushMsg = uniPushInfo.shopName;
                        break;
                }

                NotificationRecordManager notificationRecordMan = new NotificationRecordManager();
                NotificationRecord notificationRecord = new NotificationRecord();
                notificationRecord.addTime = DateTime.Now;
                notificationRecord.appType = (int)customerPushInfo.appType;
                notificationRecord.isLocked = false;
                notificationRecord.pushToken = customerPushInfo.pushToken;
                notificationRecord.message = pushMsg;
                notificationRecord.customType = (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_ORDERDETAIL;
                // 添加orderID 补差价 新版本preOrder19dianId为空 add by zhujinlei 2015/06/30
                if (uniPushInfo.preOrder19dianId == 0)
                {
                    notificationRecord.customValue = Convert.ToString(uniPushInfo.orderId);
                }
                else
                {
                    notificationRecord.customValue = Convert.ToString(uniPushInfo.preOrder19dianId);
                }

                //IOS设备，直接提交到友盟
                switch (customerPushInfo.appType)
                {
                    case VAAppType.IPAD:
                    case VAAppType.IPHONE:
                        IOSPush IOSPush = new IOSPush();

                        if (Common.CheckLatestBuild_January(customerPushInfo.appType, uniPushInfo.clientBuild))
                        {
                            bool iosResult;
                            if (uniPushInfo.orderId != Guid.Empty)
                            {
                                iosResult = IOSPush.IOSUnicast(customerPushInfo.pushToken, pushMsg, uniPushInfo.orderId.ToString(), UmengPush.VANotificationsCustomType.NOTIFICATIONS_ORDERDETAIL);
                            }
                            else
                            {
                                iosResult = IOSPush.IOSUnicast(customerPushInfo.pushToken, pushMsg, uniPushInfo.preOrder19dianId.ToString(), UmengPush.VANotificationsCustomType.NOTIFICATIONS_ORDERDETAIL);
                            }
                            if (iosResult)
                            {
                                //插入一条已经提交成功的推送记录
                                notificationRecord.isSent = true;
                                notificationRecord.sendCount = 1;
                                notificationRecordMan.InsertNotificationRecord(notificationRecord);
                            }
                            else
                            {
                                //插入一条已经待推送记录
                                notificationRecord.isSent = false;
                                notificationRecord.sendCount = 0;
                                notificationRecordMan.InsertNotificationRecord(notificationRecord);
                            }
                        }
                        else
                        {
                            //老版本只发入座的推送
                            if (uniPushInfo.pushMessage == VAPushMessage.入座成功)
                            {
                                //插入待提交的推送记录                            
                                notificationRecord.isSent = false;
                                notificationRecord.sendCount = 0;
                                notificationRecord.customType = (int)VA.AllNotifications.VANotificationsCustomType.NOTIFICATIONS_EVALUATION;
                                notificationRecord.message = ConfigurationManager.AppSettings["evaluationNotificationMsg"];
                                notificationRecordMan.InsertNotificationRecord(notificationRecord);
                            }
                        }
                        //LogManager.WriteLog(LogFile.Error, DateTime.Now + "---iosResult:" + iosResult + "---preOrder19dianId:" + uniPushInfo.preOrder19dianId.ToString() + "---pushMsg:" + pushMsg);
                        break;
                    case VAAppType.ANDROID:
                        if (customerPushInfo.pushToken.Contains("um"))//已经有了友盟的推送码
                        {
                            AndroidPush AndroidPush = new AndroidPush();
                            bool androidResult;
                            if (uniPushInfo.orderId != Guid.Empty)
                            {
                                androidResult = AndroidPush.AndroidCustomizedcast(customerPushInfo.pushToken, "悠先点菜", pushMsg, (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_ORDERDETAIL, uniPushInfo.orderId.ToString());
                            }
                            else
                            {
                                androidResult = AndroidPush.AndroidCustomizedcast(customerPushInfo.pushToken, "悠先点菜", pushMsg, (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_ORDERDETAIL, uniPushInfo.preOrder19dianId.ToString());
                            }
                            if (androidResult)
                            {
                                //插入一条已经提交成功的推送记录
                                notificationRecord.isSent = true;
                                notificationRecord.sendCount = 1;
                                notificationRecordMan.InsertNotificationRecord(notificationRecord);
                            }
                            else
                            {
                                //插入待提交的推送记录                            
                                notificationRecord.isSent = false;
                                notificationRecord.sendCount = 0;
                                notificationRecordMan.InsertNotificationRecord(notificationRecord);
                            }
                            //LogManager.WriteLog(LogFile.Error, DateTime.Now + "---androidResult:" + androidResult + "---preOrder19dianId:" + uniPushInfo.preOrder19dianId.ToString() + "---pushMsg:" + pushMsg);
                        }
                        else//依然保存到DB，用服务提交到极光推送
                        {
                            //老版本只发入座的推送
                            if (uniPushInfo.pushMessage == VAPushMessage.入座成功)
                            {
                                //插入待提交的推送记录                            
                                notificationRecord.isSent = false;
                                notificationRecord.sendCount = 0;
                                notificationRecord.customType = (int)VA.AllNotifications.VANotificationsCustomType.NOTIFICATIONS_EVALUATION;
                                notificationRecord.message = ConfigurationManager.AppSettings["evaluationNotificationMsg"];
                                notificationRecordMan.InsertNotificationRecord(notificationRecord);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        //public void UniPushByPrecedure(object uniPushInfo)
        //{
        //    CustomPushRecordOperate operate = new CustomPushRecordOperate();
        //    operate.UniPush(uniPushInfo)
        //    CustomPushRecordOperate.UniPush(uniPushInfo);
        //}


        public void SendPushAfterGetCoupon(string phoneNumber, int CouponId)
        {
            CustomPushRecordOperate pushOperate = new CustomPushRecordOperate();
            CustomerDevicePushInfo customerPushInfo = pushOperate.QueryCustomerInfo(phoneNumber);
            if (customerPushInfo != null && !string.IsNullOrEmpty(customerPushInfo.pushToken))
            {
                NotificationRecordOperate notificationRecordMan = new NotificationRecordOperate();
                NotificationRecord notificationRecord = new NotificationRecord();
                notificationRecord.addTime = DateTime.Now;
                notificationRecord.appType = (int)customerPushInfo.appType;
                notificationRecord.isLocked = false;
                notificationRecord.pushToken = customerPushInfo.pushToken;
                notificationRecord.message = "";
                notificationRecord.customType = (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_ORDER;

                var couponV = CouponVOperate.GetEntityById(CouponId);
                if (couponV == null)
                {
                    return;
                }

                string pushContent =
                    string.Format(new SystemConfigCacheLogic().GetSystemConfig("CouponPushContent", "{0}{1}"), couponV.ShopName, couponV.ValidityPeriod);

                notificationRecord.customValue = couponV.ShopId.ToString();
                string shopName = couponV.ShopName;
                switch (customerPushInfo.appType)
                {
                    case VAAppType.ANDROID:
                        if (customerPushInfo.pushToken.Contains("um"))//已经有了友盟的推送码
                        {
                            AndroidPush AndroidPush = new AndroidPush();
                            bool androidResult = AndroidPush.AndroidCustomizedcast(customerPushInfo.pushToken, "悠先点菜", pushContent,
                                (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_ORDER, couponV.ShopId.ToString());
                            if (androidResult)
                            {
                                //插入一条已经提交成功的推送记录
                                notificationRecord.isSent = true;
                                notificationRecord.sendCount = 1;
                                notificationRecordMan.AddNotificationRecord(notificationRecord);
                            }
                        }
                        else//依然保存到DB，用服务提交到极光推送
                        {
                            //插入待提交的推送记录                            
                            notificationRecord.isSent = false;
                            notificationRecord.sendCount = 0;
                            notificationRecord.customType = (int)VA.AllNotifications.VANotificationsCustomType.NOTIFICATIONS_EVALUATION;
                            notificationRecord.message = pushContent;
                            notificationRecordMan.AddNotificationRecord(notificationRecord);

                        }
                        break;
                    case VAAppType.IPHONE:
                        IOSPush IOSPush = new IOSPush();
                        if (VAGastronomistMobileApp.WebPageDll.Common.CheckLatestBuild_January(customerPushInfo.appType, customerPushInfo.appBuild))
                        {
                            bool iosResult = IOSPush.IOSUnicast(customerPushInfo.pushToken, pushContent, couponV.ShopId.ToString(), UmengPush.VANotificationsCustomType.NOTIFICATIONS_ORDER);
                            if (iosResult)
                            {
                                //插入一条已经提交成功的推送记录
                                notificationRecord.isSent = true;
                                notificationRecord.sendCount = 1;
                                notificationRecordMan.AddNotificationRecord(notificationRecord);
                            }
                        }
                        else
                        {
                            //插入待提交的推送记录                            
                            notificationRecord.isSent = false;
                            notificationRecord.sendCount = 0;
                            notificationRecord.customType = (int)VA.AllNotifications.VANotificationsCustomType.NOTIFICATIONS_EVALUATION;
                            notificationRecord.message = pushContent;
                            notificationRecordMan.AddNotificationRecord(notificationRecord);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 中奖后，给客户发推送
        /// </summary>
        /// <param name="uniPushInfo"></param>
        public void UniPushAfterLottery(object objUniPushInfo)
        {
            UniPushAfterLottery uniPushInfo = (UniPushAfterLottery)objUniPushInfo;
            CustomPushConfigManager customPushConfigManager = new CustomPushConfigManager();
            CustomerDevicePushInfo customerPushInfo = customPushConfigManager.SelectCustomerInfo(uniPushInfo.customerPhone);
            if (customerPushInfo != null)
            {
                NotificationRecordManager notificationRecordMan = new NotificationRecordManager();
                NotificationRecord notificationRecord = new NotificationRecord();
                notificationRecord.addTime = DateTime.Now;
                notificationRecord.appType = (int)customerPushInfo.appType;
                notificationRecord.isLocked = false;
                notificationRecord.pushToken = customerPushInfo.pushToken;
                notificationRecord.message = uniPushInfo.pushMessage;
                notificationRecord.customType = (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_ORDERDETAIL;
                notificationRecord.customValue = uniPushInfo.preOrder19dianId.ToString();

                //IOS设备，直接提交到友盟
                switch (customerPushInfo.appType)
                {
                    case VAAppType.IPAD:
                    case VAAppType.IPHONE:
                        IOSPush IOSPush = new IOSPush();
                        bool iosResult = IOSPush.IOSUnicast(customerPushInfo.pushToken, uniPushInfo.pushMessage, uniPushInfo.preOrder19dianId.ToString(), UmengPush.VANotificationsCustomType.NOTIFICATIONS_ORDERDETAIL);
                        if (iosResult)
                        {
                            //插入一条已经提交成功的推送记录
                            notificationRecord.isSent = true;
                            notificationRecord.sendCount = 1;
                            notificationRecordMan.InsertNotificationRecord(notificationRecord);
                        }
                        else
                        {
                            //插入一条已经待推送记录
                            notificationRecord.isSent = false;
                            notificationRecord.sendCount = 0;
                            notificationRecordMan.InsertNotificationRecord(notificationRecord);
                        }

                        break;
                    case VAAppType.ANDROID:
                        if (customerPushInfo.pushToken.Contains("um"))//已经有了友盟的推送码
                        {
                            AndroidPush AndroidPush = new AndroidPush();
                            bool androidResult = AndroidPush.AndroidCustomizedcast(customerPushInfo.pushToken, "悠先点菜", uniPushInfo.pushMessage, (int)UmengPush.VANotificationsCustomType.NOTIFICATIONS_ORDERDETAIL, uniPushInfo.preOrder19dianId.ToString());
                            if (androidResult)
                            {
                                //插入一条已经提交成功的推送记录
                                notificationRecord.isSent = true;
                                notificationRecord.sendCount = 1;
                                notificationRecordMan.InsertNotificationRecord(notificationRecord);
                            }
                            else
                            {
                                //插入待提交的推送记录                            
                                notificationRecord.isSent = false;
                                notificationRecord.sendCount = 0;
                                notificationRecordMan.InsertNotificationRecord(notificationRecord);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
