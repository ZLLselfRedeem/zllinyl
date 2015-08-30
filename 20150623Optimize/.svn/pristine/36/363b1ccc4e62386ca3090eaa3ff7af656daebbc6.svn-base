using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VA.Cache.Distributed;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VA.CacheLogic.OrderClient
{
    public class PreOrderCacheLogic
    {

        /// <summary>
        /// 从Cache中读取客户未评价点单数量，缓存10分钟
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public int GetCustomerPreorderCountForNotEvaluated(long customerId)
        {
            object CustomerPreorderCountForNotEvaluatedCache = MemcachedHelper.GetMemcached("CustomerPreorderCountForNotEvaluated_" + customerId);
            if (CustomerPreorderCountForNotEvaluatedCache == null)
            {
                CustomerManager customerMan = new CustomerManager();
                int notEvaluatedCount = customerMan.SelectCustomerPreorderCountForNotEvaluated(customerId);//未评价点单数量
                CustomerPreorderCountForNotEvaluatedCache = notEvaluatedCount;
                if (CustomerPreorderCountForNotEvaluatedCache != null)
                {
                    MemcachedHelper.AddMemcached("CustomerPreorderCountForNotEvaluated_" + customerId, CustomerPreorderCountForNotEvaluatedCache, 60 * 10);
                }
            }
            return (int)CustomerPreorderCountForNotEvaluatedCache;
        }
    }
}
