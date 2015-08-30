using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using VA.Cache.Distributed;
using VA.Cache.HttpRuntime;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VA.CacheLogic.SybWeb
{
    public class SybPreOrderCacheLogic
    {
        private const string Key = "syb_AllOrder_list_key";
        private const string Value = "syb_AllOrder_list_value";
        /// <summary>
        /// 缓存收银宝入座订单列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetSybConfirmedOrder(int shopId)
        {
            //object value = MemcachedHelper.GetMemcached(Key);//key缓存
            //if (null == value)//key缓存失效，则判定真实数据缓存也失效。
            //{
            //    if (MemcachedHelper.AddMemcached(Key, Value, 3))
            //    {
            //        value = new SybPreOrderManager().GetSybConfirmedOrder();
            //        MemcachedHelper.AddMemcached(Value, value, 3);//key的value作为真实数据缓存key
            //    }
            //    else
            //    {
            //        while (true)
            //        {
            //            Thread.Sleep(2);
            //            value = MemcachedHelper.GetMemcached(Value);
            //            if (null != value)
            //            {
            //                break;
            //            }
            //        }
            //    }
            //}
            //else//key缓存有效
            //{
            //    value = MemcachedHelper.GetMemcached(Value);
            //}
            //return (DataTable)value;

            #region 单步缓存
            //var orders1 = CacheHelper.GetCache("syb_AllOrder_list");
            //if (orders1 == null)
            //{
            //    orders1 = new SybPreOrderManager().GetSybConfirmedOrder();
            //    CacheHelper.AddCache("syb_AllOrder_list", orders1, 3);
            //}
            //return (DataTable)orders1;
            #endregion

            #region 普通memcached缓存（bug：无法处理缓存失效高并发问题）
            var orders = MemcachedHelper.GetMemcached<DataTable>("syb_AllOrder_list_" + shopId);
            if (orders == null || orders.Rows.Count <= 0)
            {
                orders = new SybPreOrderManager().GetSybConfirmedOrder(shopId);
                MemcachedHelper.AddMemcached("syb_AllOrder_list_" + shopId, orders, 3);//缓存3秒
            }
            return orders;
            #endregion
        }
    }
}
