using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VA.Cache.Distributed;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VA.CacheLogic.ServiceClient
{
    public class PreOrderCacheLogic
    {
        /// <summary>
        /// 查询悠先服务所有未入座点单
        /// </summary>
        /// <returns></returns>
        public static List<ShopPreOrder> GetVAServiceAllOrderList(Page page, int shopId)
        {
            var orders = MemcachedHelper.GetMemcached<List<ShopPreOrder>>("yxfw_AllOrder_list_" + shopId);
            if (orders == null || !orders.Any())
            {
                orders = new SqlServerPreOrder19DianInfoManager().GetPageNoApprovedShopOrders(page, shopId);
                MemcachedHelper.AddMemcached("yxfw_AllOrder_list_" + shopId, orders, 3);//缓存3秒
            }
            return orders;
        }
        /// <summary>
        /// 查询悠先服务点单列表主要信息，缓存3秒钟
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static List<PreOrderList> GetVAServiceOrderList(int shopId, bool isNew)
        {
            var orders = MemcachedHelper.GetMemcached<List<PreOrderList>>("yxfw_order_list_" + shopId);
            if (orders == null || !orders.Any())
            {
                orders = new SqlServerPreOrder19DianInfoManager().GetUnApprovedShopOrders(shopId, isNew);
                MemcachedHelper.AddMemcached("yxfw_order_list_" + shopId, orders, 3);
            }
            return orders;
        }
    }
}
