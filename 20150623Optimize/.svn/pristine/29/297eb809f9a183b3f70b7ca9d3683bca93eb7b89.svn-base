using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class ShopReturnMoneyLimitOperate
    {
        private readonly ShopReturnMoneyLimitManager manager = new ShopReturnMoneyLimitManager();

        /// <summary>
        /// 查询某家门店返现限制
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public ShopReturnMoneyLimit SelectShopReturnMoneyLimit(int shopId)
        {
            return manager.SelectShopReturnMoneyLimit(shopId);
        }
    }
}
