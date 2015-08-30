using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 微信平台 推荐餐厅 2013-09-03
    /// </summary>
    public class WechatRecommandShopOperator
    {
        private static WechatRecommandShopInfoManager rm;
        public WechatRecommandShopOperator()
        {
            lock (this)
            {
                if (rm == null)
                    rm = new WechatRecommandShopInfoManager();
            }
        }
        /// <summary>
        /// 获取所有推荐餐厅设置信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetRecommandShopInfo()
        {
            return rm.GetRecommandShopInfo();
        }
        /// <summary>
        /// 获取指定城市所有推荐餐厅设置信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetRecommandShopInfo(int cityID)
        {
            return rm.GetRecommandShopInfo(cityID);
        }
        /// <summary>
        /// 获取指定城市所有推荐餐厅设置信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetRecommandShopInfo(string cityName)
        { 
            return rm.GetRecommandShopInfo(cityName);
        }
        /// <summary>
        /// 获取指定城市,指定店铺的推荐餐厅设置信息
        /// </summary>
        public DataTable GetRecommandShopInfo(int cityID, int shopID)
        {
            return rm.GetRecommandShopInfo(cityID, shopID);
        }
        /// <summary>
        /// 添加推荐餐厅信息
        /// </summary>
        /// <param name="recommandShopInfo"></param>
        /// <returns></returns>
        public int Insert(WechatRecommandShopInfo recommandShopInfo)
        {
            return rm.Insert(recommandShopInfo);
        }
        /// <summary>
        /// 删除推荐餐厅信息
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public bool Delete(int shopID)
        {
            return rm.Delete(shopID);
        }

        //获取城市ID
        public int GetCityID(string cityName)
        {
            return rm.GetCityID(cityName);
        }

        //根据店铺 ID 获取店铺的环境图片
        public DataTable GetShopRevealImage(int shopID)
        {
            return rm.GetShopRevealImage(shopID);
        }
    }
    
}
