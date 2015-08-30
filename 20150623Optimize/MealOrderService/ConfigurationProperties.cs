using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAGastronomistMobileApp.WebPageDll;

namespace MealOrderService
{
    internal class ConfigurationProperties
    {
        public int StartTime { get { return Common.ToInt32(ConfigurationManager.AppSettings["startTime"].ToString()); } }

        public int EndTime { get { return Common.ToInt32(ConfigurationManager.AppSettings["endTime"].ToString()); } }

        /// <summary>
        /// 订单有效支付时间
        /// </summary>
        public double MealValidPeriod
        {
            get
            {
                System.Web.Caching.Cache httpRuntimeCache = System.Web.HttpRuntime.Cache;

                object mealValidPeriod = httpRuntimeCache.Get("MealValidPeriod");
                if (mealValidPeriod == null)
                {
                    mealValidPeriod = SystemConfigOperate.GetVAMealValidPeriod();
                    httpRuntimeCache.Insert("MealValidPeriod", mealValidPeriod, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(60 * 5));
                }
                return Common.ToDouble(mealValidPeriod) * (-1);
            }
        }
    }
}
