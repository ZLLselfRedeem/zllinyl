using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class ShopAwardVersionLogOperate
    {
        /// <summary>
        /// 获取门店奖品变更信息
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public DataTable SelectAwardVersionLog(int shopID)
        {
            ShopAwardVersionLogManager logManager = new ShopAwardVersionLogManager();
            return logManager.SelectAwardVersionLog(shopID);
        }

        /// <summary>
        /// 添加门店奖品变更记录
        /// </summary>
        /// <param name="shopAwardVersionLog"></param>
        /// <returns></returns>
        public bool InsertShopAwardVersionLog(ShopAwardVersionLog shopAwardVersionLog)
        {
            ShopAwardVersionLogManager logManager = new ShopAwardVersionLogManager();
            return logManager.InsertShopAwardVersionLog(shopAwardVersionLog);
        }
    }
}
