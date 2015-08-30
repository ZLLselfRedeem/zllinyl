using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VA.Cache.Distributed;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VA.CacheLogic.Dish
{
    public class OrderConfigLogic
    {
        /// <summary>
        /// 缓存悠先点餐订单详情配置信息
        /// </summary>
        /// <returns></returns>
        public List<ClientOrderDetailConfig> GetClientOrderDetailConfigOfCache()
        {
            object cache = MemcachedHelper.GetMemcached("ClientOrderDetailConfig");
            if (cache == null)
            {
                cache = new ClientOrderDetailConfigManager().GetClientOrderDetailConfig();
                if (cache != null)
                {
                    MemcachedHelper.AddMemcached("ClientOrderDetailConfig", cache, 60 * 60);//缓存5分钟
                }
            }
            return (List<ClientOrderDetailConfig>)cache;
        }
    }
}
