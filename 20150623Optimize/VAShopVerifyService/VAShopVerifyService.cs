using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using VAGastronomistMobileApp.WebPageDll;
using System.Configuration;
using LogDll;
using SMSYimei;
using SMSGuoDu;
using System.Threading;
using SMS;

namespace VAShopVerifyService
{
    public partial class VAShopVerifyService : ServiceBase
    {
        string logName = ConfigurationManager.AppSettings["logFileName"].ToString();
        bool verifyFlag = false;//标记对账是否全部完成

        public VAShopVerifyService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            verifyFlag = true;
            this.timer1.Enabled = true;
            LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "VAShopVerifyService启动");
            VerifyNew();//服务启动时直接调用一次，避免定时器会等设置的间隔时间后才执行
            //CheckBalanceAndSendEmail();
        }

        protected override void OnStop()
        {
            this.timer1.Enabled = false;
            LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "VAShopVerifyService停止");
        }

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Timers.Timer tt = (System.Timers.Timer)sender;
            try
            {
                int intHour = e.SignalTime.Hour;
                int startTime = Common.ToInt32(ConfigurationManager.AppSettings["startTime"]);
                int endTime = Common.ToInt32(ConfigurationManager.AppSettings["endTime"]);
                int intSmsExcuteTime = Common.ToInt32(ConfigurationManager.AppSettings["smsExcuteTime"]);
                int logTime = Common.ToInt32(ConfigurationManager.AppSettings["logTime"]);

                tt.Enabled = false;
                #region 执行任务
                if (intHour >= startTime && intHour <= endTime)
                {
                    // new DishOperate().DeleteAllCurrentSellOffInfo();//
                    VerifyNew();
                }
                if (intHour == intSmsExcuteTime)
                {
                    CheckBalanceAndSendEmail();
                }
                if (intHour == logTime)
                {
                    ShopOperate so = new ShopOperate();
                    so.CreateShopAmountLog();
                    LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--商户余额日志已经记录");
                }
                #endregion
                tt.Enabled = true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--timer1_Elapsed--Exception:" + ex.Message);
            }
            finally
            {
                tt.Enabled = true;
            }
        }
        private void VerifyNew()
        {
            PreOrder19dianOperate orderOperate = new PreOrder19dianOperate();
            try
            {
                if (verifyFlag)//对账完成状态
                {
                    DataTable dtShopId = orderOperate.QueryShopIdNotVerified();
                    if (dtShopId != null && dtShopId.Rows.Count > 0)
                    {
                        verifyFlag = false;//标记此次对账开始
                        int shopId = 0;
                        int threadCount = Common.ToInt32(ConfigurationManager.AppSettings["threadCount"]);
                        ThreadPool.SetMaxThreads(threadCount, threadCount);
                        for (int i = 0; i < dtShopId.Rows.Count; i++)
                        {
                            shopId = Common.ToInt32(dtShopId.Rows[i]["shopId"]);
                            try
                            {
                                ThreadPool.QueueUserWorkItem(new WaitCallback(VerifyThreadMethod), shopId);
                                //VerifyThreadMethod(shopId);
                            }
                            catch (Exception ex)
                            {
                                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--ThreadPool.QueueUserWorkItem--Exception：" + ex.Message);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                verifyFlag = true;
                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--VerifyNew--Exception：" + ex.Message);
            }
            finally
            {
                verifyFlag = true;
            }
        }

        private void VerifyThreadMethod(object shopId)
        {
            PreOrder19dianOperate orderOperate = new PreOrder19dianOperate();
            SybMoneyMerchantOperate sybOperate = new SybMoneyMerchantOperate();
            string result = "";
            try
            {
                //先找出该店铺所有已审核未对账的单据
                DataTable dtOrder = orderOperate.QueryPreOrder19dianId(Common.ToInt32(shopId));
                if (dtOrder.Rows.Count > 0)
                {
                    int employeeId = Common.ToInt32(ConfigurationManager.AppSettings["employeeId"]);//自动对账账号
                    long preOrder19dianId = 0;
                    int cnt = dtOrder.Rows.Count;
                    //Guid orderId = new Guid();
                    for (int i = 0; i < cnt; i++)
                    {
                        preOrder19dianId = Common.ToInt64(dtOrder.Rows[i]["preOrder19dianId"]);

                        result = sybOperate.ApproveMoneyMerchantNew(preOrder19dianId, employeeId);

                        //orderId = Guid.Parse(dtOrder.Rows[i]["orderId"].ToString());
                        //result = sybOperate.ApproveMoneyMerchantByOrderId(orderId, employeeId);

                        #region
                        switch (result)
                        {
                            case "0"://对账失败
                                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--" + shopId + "---" + preOrder19dianId + "--对账失败");
                                string againApprove = sybOperate.ApproveMoneyMerchantNew(preOrder19dianId, employeeId);
                                //string againApprove = sybOperate.ApproveMoneyMerchantByOrderId(preOrder19dianId, employeeId);
                                switch (againApprove)
                                {
                                    case "0"://对账失败
                                        LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--" + shopId + "---" + preOrder19dianId + "--再次对账:失败");
                                        break;
                                    case "-1"://未找到该点单
                                        LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--" + shopId + "---" + preOrder19dianId + "--再次对账:未找到该订单");
                                        break;
                                    case "-2"://点单不符合对账条件
                                        LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--" + shopId + "---" + preOrder19dianId + "--再次对账:点单不符合对账条件");
                                        break;
                                    default:
                                        if (Common.ToInt32(result) > 0)
                                        {
                                            LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--" + shopId + "---" + preOrder19dianId + "--再次对账:对账成功");
                                        }
                                        else
                                        {
                                            LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--" + shopId + "---" + preOrder19dianId + "--再次对账:异常" + result);
                                        }
                                        break;
                                }
                                break;
                            case "-1"://未找到该点单
                                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--" + shopId + "---" + preOrder19dianId + "--未找到该订单");
                                break;
                            case "-2"://点单不符合对账条件
                                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--" + shopId + "---" + preOrder19dianId + "--点单不符合对账条件");
                                break;
                            default:
                                if (Common.ToInt32(result) > 0)
                                {
                                    LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--" + shopId + "---" + preOrder19dianId + "--对账成功");
                                }
                                else
                                {
                                    LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--" + shopId + "---" + preOrder19dianId + "---对账:异常,result:" + result);
                                }
                                break;
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--对账Exception：店铺Id：" + shopId + "--result：" + result + "--exMsg：" + ex.Message);
            }
        }

        /// <summary>
        /// 检查短信平台余额并适时发邮件提醒相关人员
        /// </summary>
        private void CheckBalanceAndSendEmail()
        {
            try
            {
                //亿美短信类
                SMSYimei.SMSYimeiVoice smsYimei = new SMSYimeiVoice();
                double yimeiBalance = smsYimei.getBalance();
                int yimeiCount = (int)(yimeiBalance * 10);//亿美短信剩余条数
                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "亿美短信剩余" + yimeiCount + "条");

                //国都短信
                string smsUid = ConfigurationManager.AppSettings["SmsUid"].Trim();
                string smsPwd = ConfigurationManager.AppSettings["SmsPwd"].Trim();
                int guoduCount = Common.ToInt32(SMSGuoDu.SMSGuoDu.CheckBalance(smsUid, smsPwd));//国都短信剩余条数
                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "国都短信剩余" + guoduCount + "条");

                //建周短信
                SMSJianzhou sMSJianzhou = new SMSJianzhou();
                int jianzhouCount = Common.ToInt32(sMSJianzhou.GetBalance());
                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "建周短信剩余" + jianzhouCount + "条");

                //漫道短信
                SMSMandao sMSMandao = new SMSMandao();
                int mandaoCount = Common.ToInt32(sMSMandao.GetMandaoInfo());
                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "漫道短信剩余" + mandaoCount + "条");

                string subject = "短信平台余额提醒";
                string to = ConfigurationManager.AppSettings["addressTo"].Trim();
                StringBuilder content = new StringBuilder();

                //配置的提醒区间
                int firstNode = Common.ToInt32(ConfigurationManager.AppSettings["firstNode"].Trim());
                int secondNode = Common.ToInt32(ConfigurationManager.AppSettings["secondNode"].Trim());
                int thirdNode = Common.ToInt32(ConfigurationManager.AppSettings["thirdNode"].Trim());

                if (yimeiCount <= 0)
                {
                    content.Append(GetMailBody(yimeiCount, "亿美短信"));
                }
                else if (yimeiCount > 0 && yimeiCount < firstNode)
                {
                    content.Append(GetMailBody(yimeiCount, "亿美短信"));
                }
                else if (yimeiCount > secondNode && yimeiCount < thirdNode)
                {
                    content.Append(GetMailBody(yimeiCount, "亿美短信"));
                }

                if (guoduCount <= 0)
                {
                    content.Append(GetMailBody(guoduCount, "国都短信"));
                }
                else if (guoduCount > 0 && guoduCount < firstNode)
                {
                    content.Append(GetMailBody(guoduCount, "国都短信"));
                }
                else if (guoduCount > secondNode && guoduCount < thirdNode)
                {
                    content.Append(GetMailBody(guoduCount, "国都短信"));
                }

                content.Append(GetMailBody(jianzhouCount, "建周短信"));
                content.Append(GetMailBody(mandaoCount, "漫道短信"));

                if (!string.IsNullOrEmpty(to) && !string.IsNullOrEmpty(content.ToString()) && !string.IsNullOrEmpty(subject))
                {
                    VAEmailInfo emailInfo = new VAEmailInfo();
                    emailInfo.emailAddressTo = to;
                    emailInfo.messageBody = content.ToString();
                    emailInfo.subject = subject;
                    Common.SendNEmailFrom19dianService(emailInfo);
                }
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "--【短信余额提醒】Exception：" + ex.Message);
            }
        }

        /// <summary>
        /// 组装邮件内容
        /// </summary>
        /// <param name="count">短信可用剩余条数</param>
        /// <param name="platform">短信平台</param>
        /// <returns></returns>
        private StringBuilder GetMailBody(int count, string platform)
        {
            StringBuilder content = new StringBuilder();

            content.Append("<table>");
            content.Append("<tr><td>温馨提示：</td></tr>");
            content.Append("<tr><td>&nbsp;&nbsp;&nbsp;截至" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "，" + platform + "可用短信剩余" + count + "条。</td></tr>");
            content.Append("<tr><td>&nbsp;&nbsp;&nbsp;请及时充值，感谢！</td></tr>");
            content.Append("</table>");
            return content;
        }
    }
}
