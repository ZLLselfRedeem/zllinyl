using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VA.Cache.Distributed;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VA.CacheLogic.OrderClient
{
    /// <summary>
    /// 店铺相关缓存
    /// </summary>
    public class ShopInfoCacheLogic
    {
        /// <summary>
        /// 门店折扣信息，缓存五分钟
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable GetShopVipInfo(int shopId)
        {
            var shopVipInfoCache = MemcachedHelper.GetMemcached<DataTable>("shopVipInfo_" + shopId);
            if (shopVipInfoCache == null || shopVipInfoCache.Rows.Count <= 0)
            {
                ShopManager shopMan = new ShopManager();
                shopVipInfoCache = shopMan.SelectShopVipInfo(shopId);//查询当前门店的VIP等级信息
                if (shopVipInfoCache != null)
                {
                    MemcachedHelper.AddMemcached("shopVipInfo_" + shopId, shopVipInfoCache, 60 * 5);//五分钟
                }
            }
            return shopVipInfoCache;
        }

        /// <summary>
        /// 缓存门店勋章信息，缓存10分钟
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<string> GetShopMedalInfo(int shopId, string path)
        {
            var shopMedalInfoCache = MemcachedHelper.GetMemcached<List<string>>("shopMedalInfo_" + shopId);
            if (shopMedalInfoCache == null || !shopMedalInfoCache.Any())
            {
                MedalManager medalMan = new MedalManager();
                shopMedalInfoCache = medalMan.SelectShopMedalUrl(shopId, path);
                if (shopMedalInfoCache != null)
                {
                    MemcachedHelper.AddMemcached("shopMedalInfo_" + shopId, shopMedalInfoCache, 60 * 10);//十分钟
                }
            }
            return shopMedalInfoCache;
        }

        /// <summary>
        /// 缓存门店菜谱信息，缓存5分钟
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable GetShopMenuInfo(int menuId)
        {
            var shopMenuInfoCache = MemcachedHelper.GetMemcached<DataTable>("shopMenuInfo_" + menuId);
            if (shopMenuInfoCache == null || shopMenuInfoCache.Rows.Count <= 0)
            {
                MenuManager menuMan = new MenuManager();
                shopMenuInfoCache = menuMan.SelectMenu(menuId);
                if (shopMenuInfoCache != null)
                {
                    MemcachedHelper.AddMemcached("shopMenuInfo_" + menuId, shopMenuInfoCache, 60 * 5);//五分钟
                }
            }
            return shopMenuInfoCache;
        }

        /// <summary>
        /// 缓存门店杂项信息，缓存10分钟
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable GetShopSundryInfo(int shopId)
        {
            var shopSundryInfoCache = MemcachedHelper.GetMemcached<DataTable>("shopSundryInfo_" + shopId);
            if (shopSundryInfoCache == null || shopSundryInfoCache.Rows.Count <= 0)
            {
                SundryManager sundryMan = new SundryManager();
                shopSundryInfoCache = sundryMan.SelectSundryInfoByShop(shopId);
                if (shopSundryInfoCache != null)
                {
                    MemcachedHelper.AddMemcached("shopSundryInfo_" + shopId, shopSundryInfoCache, 60 * 10);
                }
            }
            return shopSundryInfoCache;
        }

        /// <summary>
        /// 门店的菜谱信息，缓存60分钟
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable GetShopOfMenu(int shopId)
        {
            var ShopOfMenuCache = MemcachedHelper.GetMemcached<DataTable>("shopOfMenu_" + shopId);
            if (ShopOfMenuCache == null || ShopOfMenuCache.Rows.Count <= 0)
            {
                MenuManager menuMan = new MenuManager();
                ShopOfMenuCache = menuMan.SelectMenuByShop(shopId);
                if (ShopOfMenuCache != null)
                {
                    MemcachedHelper.AddMemcached("shopOfMenu_" + shopId, ShopOfMenuCache, 60 * 60);//60分钟
                }
            }
            return ShopOfMenuCache;
        }

        /// <summary>
        /// 门店主菜沽清信息，缓存30秒
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public List<int> GetDishSellOffOfShop(int shopId)
        {
            List<int> DishSellOffOfShopCache = MemcachedHelper.GetMemcached<List<int>>("dishSellOff_" + shopId);
            if (DishSellOffOfShopCache == null || !DishSellOffOfShopCache.Any())
            {
                DishSellOffOfShopCache = new DishManager().SelectCurrentSellOffInfoByShopId(shopId);
                if (DishSellOffOfShopCache != null)
                {
                    MemcachedHelper.AddMemcached("dishSellOff_" + shopId, DishSellOffOfShopCache, 30);//30秒
                }
            }
            return DishSellOffOfShopCache;
        }

        /// <summary>
        /// 店铺基本信息，缓存2分钟
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public ShopInfo GetShopInfo(int shopId)
        {
            ShopInfo shopInfoCache = MemcachedHelper.GetMemcached<ShopInfo>("shopInfo_" + shopId);
            if (shopInfoCache == null)
            {
                ShopManager shopManager = new ShopManager();
                shopInfoCache = shopManager.SelectShopInfo(shopId);//查询门店基本信息
                if (shopInfoCache != null)
                {
                    MemcachedHelper.AddMemcached("shopInfo_" + shopId, shopInfoCache, 60 * 2);//2分钟
                }
            }
            return shopInfoCache;
        }

        /// <summary>
        /// 店铺百度经纬度信息，缓存五分钟
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public ShopCoordinate GetShopCoordinate(int shopId)
        {
            ShopCoordinate shopCoordinateCache = MemcachedHelper.GetMemcached<ShopCoordinate>("shopCoordinate_" + shopId);
            if (shopCoordinateCache == null)
            {
                ShopManager shopManager = new ShopManager();

                shopCoordinateCache = shopManager.SelectShopCoordinate(2, shopId);//百度经纬度
                if (shopCoordinateCache != null)
                {
                    MemcachedHelper.AddMemcached("shopCoordinate_" + shopId, shopCoordinateCache, 60 * 5);//五分钟
                }
            }
            return (ShopCoordinate)shopCoordinateCache;
        }

        /// <summary>
        /// 缓存支付接口查询门店部分信息，缓存1分钟
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable GetPartShopInfoCache(int shopId)
        {
            DataTable shopPartCache = MemcachedHelper.GetMemcached<DataTable>("dtPartShopInfo_" + shopId);
            if (shopPartCache == null || shopPartCache.Rows.Count <= 0)
            {
                ShopManager shopManager = new ShopManager();
                shopPartCache = shopManager.GetPartShopInfo(shopId);
                if (shopPartCache != null)
                {
                    MemcachedHelper.AddMemcached("dtPartShopInfo_" + shopId, shopPartCache, 60);
                }
            }
            return (DataTable)shopPartCache;
        }

        /// <summary>
        /// 缓存10个小时
        /// </summary>
        /// <returns></returns>
        public string GetMostEffectiveCouponStr()
        {
            return MemcachedHelper.GetMemcached<string>("MostEffectiveCouponStr");
        }

        public bool SetMostEffectiveCouponStr(string mostEffectiveCouponStr)
        {
            return MemcachedHelper.AddMemcached("MostEffectiveCouponStr", mostEffectiveCouponStr,36000);
        }

        /// <summary>
        /// 缓存10个小时
        /// </summary>
        /// <returns></returns>
        public string GetBestCouponStr()
        {
            return MemcachedHelper.GetMemcached<string>("BestCouponStr");
        }

        public bool SetBestCouponStr(string BestCouponStr)
        {
            return MemcachedHelper.AddMemcached("BestCouponStr", BestCouponStr, 36000);
        }
    }
}
