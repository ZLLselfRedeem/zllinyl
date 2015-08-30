using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class ShopAwardVersionOperate
    {
        /// <summary>
        /// 获取门店奖品变更版本信息
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public DataTable SelectAwardVersion(int shopID)
        {
            ShopAwardVersionManager versionManager = new ShopAwardVersionManager();
            return versionManager.SelectAwardVersion(shopID);
        }

        /// <summary>
        /// 添加门店奖品变更版本记录
        /// </summary>
        /// <param name="shopAwardVersionLog"></param>
        /// <returns></returns>
        public int InsertShopAwardVersion(ShopAwardVersion shopAwardVersion)
        {
            ShopAwardVersionManager versionManager = new ShopAwardVersionManager();
            return versionManager.InsertShopAwardVersion(shopAwardVersion);
        }

        /// <summary>
        /// 添加奖品版本变更记录和日志
        /// </summary>
        /// <param name="employeeID">操作员工ID</param>
        /// <param name="shopID">商家ID</param>
        /// <param name="content">操作内容</param>
        /// <param name="changeSource">操作平台</param>
        /// <param name="shopAwardId">奖品ID</param>
        /// <returns></returns>
        public bool InsertShopAwardVersionAndLog(int employeeID, int shopID, string content, string changeSource, Guid shopAwardId)
        {
            bool result = false;
            try
            {
                // 奖品版本更新记录
                ShopAwardVersion objVersion = new ShopAwardVersion()
                {
                    ShopId = shopID,
                    CreateTime = DateTime.Now,
                    CreateBy = employeeID.ToString()
                };
                int shopAwardVersionID = InsertShopAwardVersion(objVersion);

                // 日志操作记录
                ShopAwardVersionLogOperate operateLog = new ShopAwardVersionLogOperate();
                ShopAwardVersionLog objLog = new ShopAwardVersionLog()
                {
                    Id = Guid.NewGuid(),
                    ShopAwardId = shopAwardId,
                    ShopAwardVersionId = shopAwardVersionID,
                    ShopId = shopID,
                    Content = content,
                    ChangeSource = changeSource,
                    CreateTime = DateTime.Now,
                    CreateBy = employeeID.ToString()
                };
                result=operateLog.InsertShopAwardVersionLog(objLog);
            }
            catch
            {
                result = false;
            }
            return result;
        }
    }
}
