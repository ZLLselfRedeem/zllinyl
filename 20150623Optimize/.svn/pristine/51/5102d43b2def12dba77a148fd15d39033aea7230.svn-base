using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VA.Cache.Distributed;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VA.CacheLogic.OrderClient
{
    public class CouponCacheLogic
    {
        /// <summary>
        /// 抵扣券链接对应券数量，缓存30分钟
        /// </summary>
        /// <returns></returns>
        public int GetCouponCount()
        {
            object configCache = MemcachedHelper.GetMemcached("CouponCountConfig");
            if (configCache == null)
            {
                configCache = SystemConfigManager.GetCouponCount();

                if (configCache != null)
                {
                    MemcachedHelper.AddMemcached("CouponCountConfig", configCache, 60 * 30);
                }
            }
            return (int)configCache;
        }
        /// <summary>
        /// 抵扣券链接有效期，缓存30分钟
        /// </summary>
        /// <returns></returns>
        public double GetCouponValidDate()
        {
            object configCache = MemcachedHelper.GetMemcached("couponValidDateConfig");
            if (configCache == null)
            {
                configCache = SystemConfigManager.GetCouponValidDate();

                if (configCache != null)
                {
                    MemcachedHelper.AddMemcached("couponValidDateConfig", configCache, 60 * 30);
                }
            }
            return (double)configCache;
        }

        /// <summary>
        /// 抵扣券分享图片，缓存30分钟
        /// </summary>
        /// <returns></returns>
        public string GetCouponShareImage()
        {
            object configCache = MemcachedHelper.GetMemcached("couponShareImageConfig");
            if (configCache == null)
            {
                configCache = SystemConfigManager.GetSystemConfigContent("couponShareImage");
                if (configCache != null)
                {
                    MemcachedHelper.AddMemcached("couponShareImageConfig", configCache, 60 * 30);
                }
            }
            return configCache.ToString();
        }

        /// <summary>
        /// 抵扣券分享文字，缓存30分钟
        /// </summary>
        /// <returns></returns>
        public string GetCouponShareText()
        {
            object configCache = MemcachedHelper.GetMemcached("couponShareTextConfig");
            if (configCache == null)
            {
                configCache = SystemConfigManager.GetSystemConfigContent("couponShareText");
                if (configCache != null)
                {
                    MemcachedHelper.AddMemcached("couponShareTextConfig", configCache, 60 * 30);
                }
            }
            return configCache.ToString();
        }
        /// <summary>
        /// 设置优惠券缓存，缓存时间：24小时
        /// </summary>
        /// <param name="key"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool SetCouponList(string key, object value)
        {
            if (MemcachedHelper.IsKeyExists(key))
            {
                return MemcachedHelper.UpdateMemcached(key, value, 86400);
            }
            else
            {
                return MemcachedHelper.AddMemcached(key, value, 86400);
            }
        }

        public static List<CouponV> SetCouponList(string key)
        {
            var list = MemcachedHelper.GetMemcached<List<CouponV>>(key);
            return list;
        }

        /// <summary>
        /// 可以分享优惠券门店白名单
        /// </summary>
        /// <returns></returns>
        public string[] GetShareCouponsWhiteShops(int cityId)
        {
            object configCache = MemcachedHelper.GetMemcached(cityId + "shareCouponsWhiteShops");
            if (configCache == null)
            {
                configCache = SystemConfigManager.GetSystemConfigContent(cityId + "shareCouponsWhiteShops");
                if (configCache != null)
                {
                    MemcachedHelper.AddMemcached(cityId + "shareCouponsWhiteShops", configCache, 60 * 60);
                }
            }
            return configCache.ToString().Split(',').Where(p => !string.IsNullOrEmpty(p.Trim())).ToArray();
        }



        /// <summary>
        /// 可以分享优惠券门店白名单
        /// </summary>
        /// <returns></returns>
        public string[] GetShareCouponsWhiteCitys()
        {
            object configCache = MemcachedHelper.GetMemcached("shareCouponsWhiteCitys");
            if (configCache == null || string.IsNullOrEmpty(configCache.ToString()))
            {
                configCache = SystemConfigManager.GetSystemConfigContent("shareCouponsWhiteCitys");
                if (configCache != null)
                {
                    MemcachedHelper.AddMemcached("shareCouponsWhiteCitys", configCache, 60 * 60);
                }
            }
            if (configCache == null || string.IsNullOrEmpty(configCache.ToString()))
            {
                return null;
            }
            var list =  configCache.ToString().Split(',');
            return list;
        }

        public bool GetCouponIsShowOnIndexOfCache()
        {
            object configCache = MemcachedHelper.GetMemcached("couponShowIndex");
            if (configCache == null)
            {
                configCache = SystemConfigManager.GetSystemConfigContent("couponShowIndex");
                if (configCache != null)
                {
                    MemcachedHelper.AddMemcached("couponShowIndex", configCache, 60 * 60);
                }
            }
            if (Convert.ToInt32(configCache) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
