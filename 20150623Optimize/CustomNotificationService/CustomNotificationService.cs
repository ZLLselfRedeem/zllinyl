using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Threading;
using VA.AllNotifications;
using System.Transactions;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using LogDll;

namespace CustomNotificationService
{
    public partial class CustomNotificationService : ServiceBase
    {
        private string p12File { get; set; }
        private string p12FileName { get; set; }
        private string p12FilePassword { get; set; }
        private int sleepBetweenNotifications { get; set; }
        private bool sandbox { get; set; }
        private static int trueInteval = 10000;
        private bool stopping = false;
        private int threadNumber = 5;
        private static Object customNotificationLock = new Object();

        public CustomNotificationService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString() + "--CustomNotificationService启动");
            //Thread.Sleep(15000);
            #region 从配置文件读取悠闲点菜p12文件相关信息

            p12File = ConfigurationManager.AppSettings["p12File"].Trim();
            p12FileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + p12File;
            p12FilePassword = ConfigurationManager.AppSettings["p12FilePassword"].Trim();

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
            //悠先点菜自定义服务
            Thread[] CustomNotificationThread = new Thread[threadNumber];
            for (int i = 0; i < threadNumber; i++)
            {
                try
                {
                    CustomNotificationThread[i] = new Thread(new ThreadStart(CallInThread));
                    CustomNotificationThread[i].Start();
                }
                catch (Exception ex)
                {
                    LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString() + "--创建notificationThread--Exception：" + ex.Message);
                }
            }


            //ThreadPool.SetMaxThreads(threadNumber, threadNumber);
            //for (int i = 0; i < threadNumber; i++)
            //{
            //    try
            //    {
            //        ThreadPool.QueueUserWorkItem(new WaitCallback(CallInThread), i);
            //        LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--ThreadPool.QueueUserWorkItem第" + i + "个线程");
            //    }
            //    catch (Exception ex)
            //    {
            //        LogManager.WriteLog(LogFile.Error, DateTime.Now.ToString() + "--ThreadPool.QueueUserWorkItem--Exception：" + ex.Message);
            //    }
            //}

        }

        protected override void OnStop()
        {
            stopping = true;
            LogManager.WriteLog(LogFile.Error, System.DateTime.Now.ToString() + "--CustomNotificationService停止");
        }

        //private void CallInThread(object number)
        private void CallInThread()
        {
            while (true)
            {
                SendNotification();
                if (stopping)
                    break;
                Thread.Sleep(trueInteval);//线程之间的间隔时间
                //LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString() + "第" + number + "个线程");
            }
        }

        /// <summary>
        /// 发送推送（优先点菜）
        /// </summary>
        private void SendNotification()
        {
            CustomPushRecordOperate operate = new CustomPushRecordOperate();
            CustomPushRecord notificationRecord = new CustomPushRecord();
            long id = 0;
            try
            {
                DataTable currentLockedNotification = new DataTable();
                lock (customNotificationLock)
                {
                    currentLockedNotification = operate.LockNotification();
                }
                if (currentLockedNotification.Rows.Count == 1)
                {
                    id = Convert.ToInt64(currentLockedNotification.Rows[0]["id"]);
                    LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "找到记录:" + id);
                    int messageAppType = Convert.ToInt32(currentLockedNotification.Rows[0]["appType"]);
                    string pushToken = Convert.ToString(currentLockedNotification.Rows[0]["pushToken"]);
                    string message = Convert.ToString(currentLockedNotification.Rows[0]["message"]);
                    int customType = Convert.ToInt32(currentLockedNotification.Rows[0]["customType"]);
                    string customValue = Convert.ToString(currentLockedNotification.Rows[0]["customValue"]);
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
                                        LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "Fail");
                                        notificationRecord.isLocked = false;
                                        notificationRecord.sendTime = System.DateTime.Now;
                                        notificationRecord.isSent = false;
                                        notificationRecord.sendCount = 1;//增量
                                        notificationRecord.id = id;
                                    }
                                    using (TransactionScope scope = new TransactionScope())
                                    {
                                        if (operate.UpdateCustomPushRecord(notificationRecord))
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
                                    if (operate.UpdateCustomPushRecord(notificationRecord))
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
                            if (operate.UpdateCustomPushRecord(notificationRecord))
                            {
                                scope.Complete();
                                LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "Complete");
                            }
                        }
                    }
                }
                //else
                //{
                //    LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "空操作"); 
                //}
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
                        if (operate.UpdateCustomPushRecord(notificationRecord))
                        {
                            scope.Complete();
                            LogManager.WriteLog(LogFile.Trace, System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "Complete2");
                        }
                    }
                }
            }
        }
    }
}
