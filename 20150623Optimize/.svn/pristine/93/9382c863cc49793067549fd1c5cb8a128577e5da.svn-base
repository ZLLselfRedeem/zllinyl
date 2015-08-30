using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VA.Cache;
using VA.CacheLogic.SybWeb;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public partial class MerchantInfoOperate
    {
        /// <summary>
        /// 收银宝查询所有入座订单
        /// </summary>
        /// <returns></returns>
        public static DataTable GetSybConfirmedOrder(int shopId)
        {
            //SybPreOrderCacheLogic cacheLogic = Singleton<SybPreOrderCacheLogic>.Instance;//获取单例对象
            return new SybPreOrderCacheLogic().GetSybConfirmedOrder(shopId);
        }

        /// <summary>
        /// 收银宝查询所有需要对账点单（对账列表）
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="inputTextStr"></param>
        /// <param name="approvedStatus"></param>
        /// <param name="preOrderTimeStr"></param>
        /// <param name="preOrderTimeEnd"></param>
        /// <returns></returns>
        public static DataTable GetSybVerifiedOrder(int shopId, string inputTextStr, int approvedStatus, string preOrderTimeStr, string preOrderTimeEnd,int CouponType)
        {
            return new SybPreOrderManager().GetSybVerifiedOrder(shopId, inputTextStr, approvedStatus, preOrderTimeStr, preOrderTimeEnd, CouponType);
        }
    }
}
