using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VA.Cache.Distributed;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VA.CacheLogic.OrderClient
{
    public class AwardCacheLogic
    {
        /// <summary>
        /// 取系统配置
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public string GetAwardConfig(string val, string defaultVal)
        {
            object cache = MemcachedHelper.GetMemcached(val);
            if (cache == null)
            {
                AwardConfigManager manager = new AwardConfigManager();

                cache = manager.GetAwardConfig(val).ConfigCotent;

                if (cache != null && cache.ToString() != "")
                {
                    MemcachedHelper.AddMemcached(val, cache, 60 * 60);//一小时
                }
            }
            return cache != null && cache.ToString() != "" ? cache.ToString() : defaultVal;
        }

        /// <summary>
        /// 可以开启剪刀手入座门店白名单
        /// </summary>
        /// <returns></returns>
        public string[] GetConfirmOrderWhiteShops(int cityId)
        {
            object configCache = MemcachedHelper.GetMemcached(cityId + "confirmOrderWhiteShops");
            if (configCache == null || string.IsNullOrEmpty(configCache.ToString()))
            {
                configCache = SystemConfigManager.GetSystemConfigContent(cityId + "confirmOrderWhiteShops");
                if (configCache != null)
                {
                    MemcachedHelper.AddMemcached(cityId + "confirmOrderWhiteShops", configCache, 60 * 60);
                }
            }
            return configCache.ToString().Split(',').Where(p => !string.IsNullOrEmpty(p.Trim())).ToArray();
        }



        /// <summary>
        /// 可以开启剪刀手入座城市白名单
        /// </summary>
        /// <returns></returns>
        public string[] GetConfirmOrderWhiteCitys()
        {
            object configCache = MemcachedHelper.GetMemcached("confirmOrderWhiteCitys");
            if (configCache == null || string.IsNullOrEmpty(configCache.ToString()))
            {
                configCache = SystemConfigManager.GetSystemConfigContent("confirmOrderWhiteCitys");
                if (configCache != null)
                {
                    MemcachedHelper.AddMemcached("confirmOrderWhiteCitys", configCache, 60 * 60);
                }
            }
            if (configCache == null || string.IsNullOrEmpty(configCache.ToString()))
            {
                return null;
            }
            var list = configCache.ToString().Split(',');
            return list;
        }
    }
}
