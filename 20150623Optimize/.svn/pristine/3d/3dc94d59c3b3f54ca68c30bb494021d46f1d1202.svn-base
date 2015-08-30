using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Reflection;
using System.Configuration;
using VAGastronomistMobileApp.Model;
using System.Transactions;
using VAGastronomistMobileApp.WebPageDll;
using VA.AllNotifications;
using LogDll;
using System.Web;
//
//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2012-04-10.
//
namespace NotificationService
{
    public partial class NotificationService : ServiceBase
    {
        private string p12File { get; set; }
        private string p12FileName { get; set; }
        private string p12FilePassword { get; set; }
        private string servicep12File { get; set; }
        private string servicep12FileName { get; set; }
        private string servicep12FilePassword { get; set; }
        private int sleepBetweenNotifications { get; set; }
        private bool sandbox { get; set; }
        private static int trueInteval = 10000;
        private bool stopping = false;
        private int threadNumber = 5;
        private static Object notificationLock = new Object();
        private static Object uxianServiceLock = new Object();

        public NotificationService()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString() + "--NotificationService启动" );
            //Thread.Sleep(15000);
            #region 从配置文件读取优先点菜及优先服务的p12文件相关信息

            p12File = ConfigurationManager.AppSettings["p12File"].Trim();
            p12FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + p12File;
            p12FilePassword = ConfigurationManager.AppSettings["p12FilePassword"].Trim();

            servicep12File = ConfigurationManager.AppSettings["servicep12File"].Trim();
            servicep12FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + servicep12File;
            servicep12FilePassword = ConfigurationManager.AppSettings["servicep12FilePassword"].Trim();

            if (ConfigurationManager.AppSettings["sandbox"].Trim() == "true")
            {
                sandbox = true;
            }
            else
            {
                sandbox = false;
            }
            try
            {
                sleepBetweenNotifications = Convert.ToInt32(ConfigurationManager.AppSettings["sleepBetweenNotifications"].Trim());
            }
            catch (System.Exception)
            {
                sleepBetweenNotifications = 100;
            }
            try
            {
                threadNumber = Convert.ToInt32(ConfigurationManager.AppSettings["threadNumber"].Trim());
            }
            catch (System.Exception)
            {
                threadNumber = 5;
            }
            #endregion
            //按照配置文件中的线程数量启动相应数量的线程
            //优先点菜
            Thread[] notificationThread = new Thread[threadNumber];
            for (int i = 0; i < threadNumber; i++)
            {
                try
                {
                    notificationThread[i] = new Thread(new ThreadStart(CallInThread));
                    notificationThread[i].Start();
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString() + "--创建notificationThread--Exception：" + ex.Message);
                }
            }
            //优先服务
            Thread[] uxainServiceThread = new Thread[threadNumber];
            for (int i = 0; i < threadNumber; i++)
            {
                try
                {
                    uxainServiceThread[i] = new Thread(new ThreadStart(CallInUxianServiceThread));
                    uxainServiceThread[i].Start();
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString() + "--创建uxainServiceThread--Exception：" + ex.Message);  
                }
            }
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        protected override void OnStop()
        {
            stopping = true;
            LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString() + "--NotificationService停止"); 
        }

        #region 优先点菜推送

        private void CallInThread()
        {
            while (true)
            {
                SendNotification();
                if (stopping)
                    break;
                Thread.Sleep(trueInteval);//线程之间的间隔时间
            }
        }

        /// <summary>
        /// 发送推送（优先点菜）
        /// </summary>
        private void SendNotification()
        {
            NotificationRecordOperate notificationRecordOpe = new NotificationRecordOperate();
            NotificationRecord notificationRecord = new NotificationRecord();
            long id = 0;
            try
            {
                DataTable currentLockedNotification = new DataTable();
                lock (notificationLock)
                {
                    currentLockedNotification = notificationRecordOpe.LockNotification();
                }
                if (currentLockedNotification.Rows.Count == 1)
                {
                    LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "找到该记录");
                    id = Common.ToInt64(currentLockedNotification.Rows[0]["id"]);
                    int messageAppType = Common.ToInt32(currentLockedNotification.Rows[0]["appType"]);
                    string pushToken = Common.ToString(currentLockedNotification.Rows[0]["pushToken"]);
                    string message = Common.ToString(currentLockedNotification.Rows[0]["message"]);
                    int customType = Common.ToInt32(currentLockedNotification.Rows[0]["customType"]);
                    string customValue = Common.ToString(currentLockedNotification.Rows[0]["customValue"]);
                    if (!string.IsNullOrEmpty(pushToken) && !string.IsNullOrEmpty(message) && customType != 0)
                    {
                        LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "推送信息合法");
                        switch (messageAppType)
                        {
                            case (int)VAAppType.IPHONE:
                            case (int)VAAppType.IPAD:
                                {
                                    VASendApnsResponse sendApnsResponse = new VASendApnsResponse();
                                    VASendApnsRequest sendApnsRequest = new VASendApnsRequest();
                                    ApnsOperate apnsOperate = new ApnsOperate();
                                    sendApnsRequest.p12FileName = p12FileName;
                                    sendApnsRequest.p12FilePassword = p12FilePassword;
                                    sendApnsRequest.sleepBetweenNotifications = sleepBetweenNotifications;
                                    sendApnsRequest.sandbox = sandbox;
                                    string[] pushTokenArray = pushToken.Split(new string[1] { "*" }, StringSplitOptions.RemoveEmptyEntries);
                                    List<string> pushTokenList = new List<string>();
                                    for (int i = 0; i < pushTokenArray.Length; i++)
                                    {
                                        pushTokenList.Add(pushTokenArray[i]);
                                    }
                                    sendApnsRequest.pushToken = pushTokenList;
                                    sendApnsRequest.message = message;
                                    sendApnsRequest.notificationsContentType = (VANotificationsCustomType)customType;
                                    sendApnsRequest.notificationsContentValue = customValue;
                                    sendApnsResponse = apnsOperate.SendApnsNotification(sendApnsRequest);
                                    //NotificationRecord notificationRecord = new NotificationRecord();
                                    if (sendApnsResponse.result == VANotificationsResult.VA_OK)
                                    {
                                        LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "OK");
                                        notificationRecord.isLocked = false;
                                        notificationRecord.sendTime = System.DateTime.Now;
                                        notificationRecord.isSent = true;
                                        notificationRecord.sendCount = 1;//增量
                                        notificationRecord.id = id;
                                    }
                                    else
                                    {
                                        LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "Fail：" + sendApnsResponse.result);
                                        notificationRecord.isLocked = false;
                                        notificationRecord.sendTime = System.DateTime.Now;
                                        notificationRecord.isSent = false;
                                        notificationRecord.sendCount = 1;//增量
                                        notificationRecord.id = id;
                                    }
                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        if (notificationRecordOpe.ModifyNotificationRecord(notificationRecord))
                                        {
                                            scope.Complete();
                                            LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "Complete");
                                        }
                                    }
                                }
                                break;
                            case (int)VAAppType.ANDROID:
                                //安桌推送 woody 2013-10-15  result : {"sendno":"1","msg_id":"474944259","errcode":0,"errmsg":"Succeed"}
                                //string extras = "{\\\"type\\\":" + customType + ",\\\"value\\\":\\\"" + customValue + "\\\"}";//自定义类型的extras格式
                                string extras = "{\"type\":" + customType + ",\"value\":\"" + customValue + "\"}";//通知类型
                                //string jPushResult = Jpush.DoSend(id, pushToken, message, extras, "1");

                                string username = ConfigurationManager.AppSettings["username"].ToString();
                                string appkeys = ConfigurationManager.AppSettings["appkeys"].ToString();
                                string master_secret = ConfigurationManager.AppSettings["master_secret"].ToString();

                                string jPushResult = Jpush.DoSend(id, pushToken, message, extras, "1", username, appkeys, master_secret);

                                LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + jPushResult);
                                if (jPushResult.Contains("Succeed"))
                                {
                                    LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "OK");
                                    notificationRecord.isLocked = false;
                                    notificationRecord.sendTime = System.DateTime.Now;
                                    notificationRecord.isSent = true;
                                    notificationRecord.sendCount = 1;//增量
                                    notificationRecord.id = id;
                                }
                                else
                                {
                                    LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "Fail");
                                    notificationRecord.isLocked = false;
                                    notificationRecord.sendTime = System.DateTime.Now;
                                    notificationRecord.isSent = false;
                                    notificationRecord.sendCount = 1;//增量
                                    notificationRecord.id = id;
                                }
                                using (TransactionScope scope = new TransactionScope())
                                {
                                    if (notificationRecordOpe.ModifyNotificationRecord(notificationRecord))
                                    {
                                        scope.Complete();
                                        LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "Complete");
                                    }
                                }

                                break;
                            default:
                                LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "不是iOS推送");
                                break;
                        }
                    }
                    else
                    {
                        LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "Token空message空type错");
                        notificationRecord.isLocked = false;
                        notificationRecord.sendTime = System.DateTime.Now;
                        notificationRecord.isSent = false;
                        notificationRecord.sendCount = 1;//增量
                        notificationRecord.id = id;
                        using (TransactionScope scope = new TransactionScope())
                        {
                            if (notificationRecordOpe.ModifyNotificationRecord(notificationRecord))
                            {
                                scope.Complete();
                                LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "Complete");
                            }
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "abc" + e.ToString());
                if (id > 0)
                {
                    notificationRecord.isLocked = false;
                    notificationRecord.sendTime = System.DateTime.Now;
                    notificationRecord.isSent = false;
                    notificationRecord.sendCount = 1;//增量
                    notificationRecord.id = id;
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (notificationRecordOpe.ModifyNotificationRecord(notificationRecord))
                        {
                            scope.Complete();
                            LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "Complete2");
                        }
                    }
                }
            }
        }

        #endregion

        #region 优先服务推送

        private void CallInUxianServiceThread()
        {
            while (true)
            {
                SendUxianServiceNotification();
                if (stopping)
                    break;
                Thread.Sleep(trueInteval);//线程之间的间隔时间
            }
        }

        /// <summary>
        /// 发送优先服务的推送
        /// </summary>
        private void SendUxianServiceNotification()
        {
            NotificationRecordOperate notificationRecordOpe = new NotificationRecordOperate();
            NotificationRecord notificationRecord = new NotificationRecord();
            long id = 0;
            try
            {
                DataTable currentLockedNotification = new DataTable();
                lock (uxianServiceLock)
                {
                    currentLockedNotification = notificationRecordOpe.LockUxianServiceNotification();
                }
                if (currentLockedNotification.Rows.Count == 1)
                {
                    LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "找到该记录");
                    id = Common.ToInt64(currentLockedNotification.Rows[0]["id"]);
                    int messageAppType = Common.ToInt32(currentLockedNotification.Rows[0]["appType"]);
                    string pushToken = Common.ToString(currentLockedNotification.Rows[0]["pushToken"]);
                    string message = Common.ToString(currentLockedNotification.Rows[0]["message"]);
                    int customType = Common.ToInt32(currentLockedNotification.Rows[0]["customType"]);
                    string customValue = Common.ToString(currentLockedNotification.Rows[0]["customValue"]);
                    if (!string.IsNullOrEmpty(pushToken) && !string.IsNullOrEmpty(message) && customType != 0)
                    {
                        LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "推送信息合法");
                        switch (messageAppType)
                        {
                            case (int)VAServiceType.IPHONE:
                            case (int)VAServiceType.IPAD:
                                {
                                    #region
                                    VASendApnsResponse sendApnsResponse = new VASendApnsResponse();
                                    VASendApnsRequest sendApnsRequest = new VASendApnsRequest();
                                    ApnsOperate apnsOperate = new ApnsOperate();
                                    sendApnsRequest.p12FileName = servicep12FileName;
                                    sendApnsRequest.p12FilePassword = servicep12FilePassword;
                                    sendApnsRequest.sleepBetweenNotifications = sleepBetweenNotifications;
                                    sendApnsRequest.sandbox = sandbox;
                                    string[] pushTokenArray = pushToken.Split(new string[1] { "*" }, StringSplitOptions.RemoveEmptyEntries);
                                    List<string> pushTokenList = new List<string>();
                                    for (int i = 0; i < pushTokenArray.Length; i++)
                                    {
                                        pushTokenList.Add(pushTokenArray[i]);
                                    }
                                    sendApnsRequest.pushToken = pushTokenList;
                                    sendApnsRequest.message = message;
                                    sendApnsRequest.notificationsContentType = (VANotificationsCustomType)customType;
                                    sendApnsRequest.notificationsContentValue = customValue;
                                    sendApnsResponse = apnsOperate.SendApnsNotification(sendApnsRequest);
                                    //NotificationRecord notificationRecord = new NotificationRecord();
                                    if (sendApnsResponse.result == VANotificationsResult.VA_OK)
                                    {
                                        LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "OK");
                                        notificationRecord.isLocked = false;
                                        notificationRecord.sendTime = System.DateTime.Now;
                                        notificationRecord.isSent = true;
                                        notificationRecord.sendCount = 1;//增量
                                        notificationRecord.id = id;
                                    }
                                    else
                                    {
                                        LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "Fail");
                                        notificationRecord.isLocked = false;
                                        notificationRecord.sendTime = System.DateTime.Now;
                                        notificationRecord.isSent = false;
                                        notificationRecord.sendCount = 1;//增量
                                        notificationRecord.id = id;
                                    }
                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        if (notificationRecordOpe.ModifyUxianServiceNotificationRecord(notificationRecord))
                                        {
                                            scope.Complete();
                                            LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "Complete");
                                        }
                                    }
                                }
                                    #endregion
                                break;
                            case (int)VAServiceType.ANDROID:
                                #region
                                //安桌推送 woody 2013-10-15  result : {"sendno":"1","msg_id":"474944259","errcode":0,"errmsg":"Succeed"}
                                //string extras = "{\\\"type\\\":" + customType + ",\\\"value\\\":\\\"" + customValue + "\\\"}";//自定义类型的extras格式
                                string extras = "{\"type\":" + customType + ",\"value\":\"" + customValue + "\"}";//通知类型
                                //string jPushResult = Jpush.DoSend(id, pushToken, message, extras, "1");

                                string username = ConfigurationManager.AppSettings["username"].ToString();
                                string appkeys = ConfigurationManager.AppSettings["serviceappkeys"].ToString();
                                string master_secret = ConfigurationManager.AppSettings["servicemaster_secret"].ToString();

                                string jPushResult = Jpush.DoSend(id, pushToken, message, extras, "1", username, appkeys, master_secret);

                                LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + jPushResult);
                                if (jPushResult.Contains("Succeed"))
                                {
                                    LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "OK");
                                    notificationRecord.isLocked = false;
                                    notificationRecord.sendTime = System.DateTime.Now;
                                    notificationRecord.isSent = true;
                                    notificationRecord.sendCount = 1;//增量
                                    notificationRecord.id = id;
                                }
                                else
                                {
                                    LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "Fail");
                                    notificationRecord.isLocked = false;
                                    notificationRecord.sendTime = System.DateTime.Now;
                                    notificationRecord.isSent = false;
                                    notificationRecord.sendCount = 1;//增量
                                    notificationRecord.id = id;
                                }
                                using (TransactionScope scope = new TransactionScope())
                                {
                                    if (notificationRecordOpe.ModifyUxianServiceNotificationRecord(notificationRecord))
                                    {
                                        scope.Complete();
                                        LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "Complete");
                                    }
                                }
                                #endregion
                                break;
                            default:
                                LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "不是iOS推送");
                                break;
                        }
                    }
                    else
                    {
                        LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "Token空message空type错");
                        notificationRecord.isLocked = false;
                        notificationRecord.sendTime = System.DateTime.Now;
                        notificationRecord.isSent = false;
                        notificationRecord.sendCount = 1;//增量
                        notificationRecord.id = id;
                        using (TransactionScope scope = new TransactionScope())
                        {
                            if (notificationRecordOpe.ModifyUxianServiceNotificationRecord(notificationRecord))
                            {
                                scope.Complete();
                                LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "Complete");
                            }
                        }
                    }
                }
            }
            catch (System.Exception e)
            {
                LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "abc" + e.ToString());
                if (id > 0)
                {
                    notificationRecord.isLocked = false;
                    notificationRecord.sendTime = System.DateTime.Now;
                    notificationRecord.isSent = false;
                    notificationRecord.sendCount = 1;//增量
                    notificationRecord.id = id;
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (notificationRecordOpe.ModifyUxianServiceNotificationRecord(notificationRecord))
                        {
                            scope.Complete();
                            LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "Complete2");
                        }
                    }
                }
            }
        }

        #endregion
    }
}
