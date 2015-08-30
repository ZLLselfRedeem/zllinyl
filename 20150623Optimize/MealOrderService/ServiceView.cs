using LogDll;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using VAGastronomistMobileApp.WebPageDll;

namespace MealOrderService
{
    public partial class ServiceView : ServiceBase
    {
        public ServiceView()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.Timer.Enabled = true;
            LogManager.WriteLog(LogFile.MealOrderService, DateTime.Now.ToString() + "处理用户未在规定时间内支付年夜饭点单服务（MealOrderService）开启。");
        }

        protected override void OnStop()
        {
            this.Timer.Enabled = false;
            LogManager.WriteLog(LogFile.MealOrderService, DateTime.Now.ToString() + "处理用户未在规定时间内支付年夜饭点单服务（MealOrderService）停止。");
        }

        /// <summary>
        /// 定时器时间到，触发事件执行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Timers.Timer tt = (System.Timers.Timer)sender;
            try
            {
                int intHour = e.SignalTime.Hour;

                ConfigurationProperties configurationProperty = new ConfigurationProperties();
                int startTime = configurationProperty.StartTime;
                int endTime = configurationProperty.EndTime;
                double mealValidPeriod = configurationProperty.MealValidPeriod;

                if (intHour >= startTime && intHour <= endTime)
                {
                    tt.Enabled = false;
                    //执行逻辑处理
                    var operate = new MealOperate();

                    DataTable dt = operate.SelectBackPreOrder(mealValidPeriod);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        int mealScheduleId = 0;
                        int count = 0;
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            mealScheduleId = Common.ToInt32(dt.Rows[i]["MealScheduleID"]);
                            count = Common.ToInt32(dt.Rows[i]["cnt"]);

                            bool flag2 = operate.UpdateMealScheduleCanBuy(mealScheduleId, count);//注意方法执行顺序

                            if (flag2 == false)
                            {
                                LogManager.WriteLog(LogFile.MealOrderService, DateTime.Now.ToString() + "MealOrderService服务执行失败。--修改套餐剩余份数失败");
                                return;
                            }
                            else
                            {
                                LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "修改套餐剩余份数成功，mealScheduleId：" + mealScheduleId + "，count：" + count);
                            }
                        }
                        bool flag1 = operate.UpdateNotPayMealOrderOperate(mealValidPeriod);
                        if (flag1 == false)
                        {
                            LogManager.WriteLog(LogFile.MealOrderService, DateTime.Now.ToString() + "MealOrderService服务执行失败。--更新PreOrder19dian失败");
                            return;
                        }
                        else
                        {
                            LogManager.WriteLog(LogFile.Trace, DateTime.Now.ToString() + "修改PreOrder19dian成功");
                        }
                    }
                }

                tt.Enabled = true;
            }
            catch (Exception ex)
            {
                LogManager.WriteLog(LogFile.MealOrderService, DateTime.Now.ToString() + "MealOrderService服务发生异常信息:" + ex.Message);
            }
            finally
            {
                tt.Enabled = true;
            }
        }
    }
}
