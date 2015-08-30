﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using VA.Cache.Distributed;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VA.CacheLogic
{
    public class SystemConfigCacheLogic
    {
        /// <summary>
        /// 有效的支付方式，缓存3分钟
        /// </summary>
        /// <returns></returns>
        public object GetServerPayModel()
        {
            object ServerPayModelCache = MemcachedHelper.GetMemcached("serverPayModel");
            if (ServerPayModelCache == null)
            {
                SystemConfigManager systemConfigManager = new SystemConfigManager();
                ServerPayModelCache = systemConfigManager.SelectPayModeList();

                if (ServerPayModelCache != null)
                {
                    MemcachedHelper.AddMemcached("serverPayModel", ServerPayModelCache, 60 * 3);//3分钟
                }
            }
            return ServerPayModelCache;
        }

        /// <summary>
        /// 缓存系统配置手续费比例
        /// </summary>
        /// <returns></returns>
        public double GetVATransactionProportion()
        {
            object transactionProportion = MemcachedHelper.GetMemcached("transactionProportion");
            if (transactionProportion == null)
            {
                transactionProportion = SystemConfigManager.GetVATransactionProportion();
                if (transactionProportion != null)
                {
                    MemcachedHelper.AddMemcached("transactionProportion", transactionProportion, 60 * 60 * 12);//12小时
                }
            }
            return (double)transactionProportion;
        }
        /// <summary>
        /// 悠先点菜最新版本，缓存5分钟
        /// </summary>
        /// <param name="appType"></param>
        /// <returns></returns>
        public DataTable GetAppLatestBuild(VAAppType appType)
        {
            DataTable appLatestBuildCache = MemcachedHelper.GetMemcached<DataTable>("appLatestBuild_" + (int)appType);
            if (appLatestBuildCache == null || appLatestBuildCache.Rows.Count <= 0)
            {
                AppBuildManager appBuildMan = new AppBuildManager();
                appLatestBuildCache = appBuildMan.SelectLatestBuild(appType);
                if (appLatestBuildCache != null)
                {
                    MemcachedHelper.AddMemcached("appLatestBuild_" + (int)appType, appLatestBuildCache, 60 * 5);//五分钟
                }
            }
            return (DataTable)appLatestBuildCache;
        }

        /// <summary>
        /// 优先服务的版本信息，缓存5分钟
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public DataTable GetUxianServiceLatestBuild(VAServiceType serviceType)
        {
            DataTable uxianServiceLatestBuildCache = MemcachedHelper.GetMemcached<DataTable>("uxianServiceLatestBuild_" + (int)serviceType);
            if (uxianServiceLatestBuildCache == null || uxianServiceLatestBuildCache.Rows.Count <= 0)
            {
                ServiceBuildManager manager = new ServiceBuildManager();
                uxianServiceLatestBuildCache = manager.SelectLatestBuild((int)serviceType);
                if (uxianServiceLatestBuildCache != null)
                {
                    MemcachedHelper.AddMemcached("uxianServiceLatestBuild_" + (int)serviceType, uxianServiceLatestBuildCache, 60 * 5);//五分钟
                }
            }
            return (DataTable)uxianServiceLatestBuildCache;
        }

        /// <summary>
        /// 客户端图片处理参数，缓存10分钟
        /// </summary>
        /// <returns></returns>
        public List<ClientImageSize> GetClientImageSize()
        {
            List<ClientImageSize> clientImageSize = MemcachedHelper.GetMemcached<List<ClientImageSize>>("clientImageSize");
            if (clientImageSize == null || !clientImageSize.Any())
            {
                ClientImageSizeManager clientImageSizeManager = new ClientImageSizeManager();
                clientImageSize = clientImageSizeManager.QueryClientImageSize();

                if (clientImageSize != null)
                {
                    MemcachedHelper.AddMemcached("clientImageSize", clientImageSize, 60 * 10);
                }
            }
            return clientImageSize;
        }
        /// <summary>
        /// 获取已经上线的省份，缓存10分钟
        /// </summary>
        /// <returns></returns>
        public List<Province> GetOnLineProvince()
        {
            var OnLineProvinceCache = MemcachedHelper.GetMemcached<List<Province>>("OnLineProvince");
            if (OnLineProvinceCache == null)
            {
                ProvinceManager provinceMan = new ProvinceManager();
                List<Province> onlineProvince = provinceMan.SelectOnLineProvince();
                OnLineProvinceCache = onlineProvince;
                if (OnLineProvinceCache != null && OnLineProvinceCache.Any())
                {
                    MemcachedHelper.AddMemcached("OnLineProvince", OnLineProvinceCache, 60 * 10);//10分钟
                }
            }
            return OnLineProvinceCache;
        }

        /// <summary>
        /// 获取某省份对应城市信息，缓存10分钟
        /// </summary>
        /// <param name="provinceId"></param>
        /// <returns></returns>
        public DataTable GetCityOfProvince(int provinceId)
        {
            DataTable CityOfProvinceCache = MemcachedHelper.GetMemcached<DataTable>("CityOfProvince_" + provinceId);
            if (CityOfProvinceCache == null || CityOfProvinceCache.Rows.Count <= 0)
            {
                CityManager cityMan = new CityManager();
                DataTable dtCity = cityMan.SelectProvinceCity(provinceId);//上线城市省份不多，暂不优化此处
                CityOfProvinceCache = dtCity;
                if (CityOfProvinceCache != null)
                {
                    MemcachedHelper.AddMemcached("CityOfProvince_" + provinceId, CityOfProvinceCache, 60 * 10);//10分钟
                }
            }
            return (DataTable)CityOfProvinceCache;
        }

        /// <summary>
        /// 从Cache中读取客服热线
        /// </summary>
        /// <returns></returns>
        public string GetVAServicePhone()
        {
            string VAServicePhoneCache = MemcachedHelper.GetMemcached<string>("VAServicePhone");
            if (VAServicePhoneCache == null)
            {
                VAServicePhoneCache = SystemConfigManager.GetServicePhone();
                if (VAServicePhoneCache != null)
                {
                    MemcachedHelper.AddMemcached("VAServicePhone", VAServicePhoneCache, 60 * 60);//一小时
                }
            }
            return (string)VAServicePhoneCache;
        }
        /// <summary>
        /// 缓存悠先点菜客户端启动图地址
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<ClientStartImgConfig> ClientStartImgConfigInfosOfCache(int type)
        {
            object clinetStartImgConfigInfos = MemcachedHelper.GetMemcached("ClientStartImgConfigInfos");
            if (clinetStartImgConfigInfos == null)
            {
                clinetStartImgConfigInfos = new ClientStartImgConfigManager().GetClientStartImgConfigInfos(type);
                if (clinetStartImgConfigInfos != null)
                {
                    MemcachedHelper.AddMemcached("ClientStartImgConfigInfos", clinetStartImgConfigInfos, 60 * 60);//一小时
                }
            }
            return (List<ClientStartImgConfig>)clinetStartImgConfigInfos;
        }

        /// <summary>
        /// 缓存悠先点菜客户端启动图地址
        /// </summary>
        /// <returns></returns>
        public string ClientDishSortAlgorithmBaseOfCache()
        {
            object cache = MemcachedHelper.GetMemcached("dishSortAlgorithmBase");
            if (cache == null)
            {
                cache = SystemConfigManager.GetDishSortAlgorithmBase();
                if (cache != null)
                {
                    MemcachedHelper.AddMemcached("dishSortAlgorithmBase", cache, 60 * 60);//一小时
                }
            }
            return (string)cache;
        }

        /// <summary>
        /// 缓存悠先点菜客户端可用红包最低版本号
        /// </summary>
        /// <returns></returns>
        public string[] GetAvailableRedEnvelopeBuildOfCache()
        {
            object cache = MemcachedHelper.GetMemcached("availableRedEnvelopeBuild");
            if (cache == null)
            {
                cache = SystemConfigManager.GetAvailableRedEnvelopeBuild();
                if (cache != null)
                {
                    MemcachedHelper.AddMemcached("availableRedEnvelopeBuild", cache, 60 * 60);//一小时
                }
            }
            return ((string)cache).Split('|');
        }
        /// <summary>
        /// 红包过期版本号，缓存1小时
        /// </summary>
        /// <returns></returns>
        public string[] GetExpireRedEnvelopeBuildOfCahce()
        {
            object cache = MemcachedHelper.GetMemcached("expireRedEnvelopeBuild");
            if (cache == null)
            {
                cache = SystemConfigManager.GetSystemConfigContent("expireRedEnvelopeBuild");
                if (cache != null)
                {
                    MemcachedHelper.AddMemcached("expireRedEnvelopeBuild", cache, 60 * 60);
                }
            }
            return ((string)cache).Split('|');
        }

        /// <summary>
        /// 红包过期期限（几日），缓存1小时
        /// </summary>
        /// <returns></returns>
        public int GetRedEnvelopeExpireDayOfCache()
        {
            object cache = MemcachedHelper.GetMemcached("redEnvelopeExpireDay");
            if (cache == null)
            {
                cache = SystemConfigManager.GetSystemConfigContent("redEnvelopeExpireDay");
                if (cache != null)
                {
                    MemcachedHelper.AddMemcached("redEnvelopeExpireDay", cache, 60 * 60);//一小时
                }
            }
            return Convert.ToInt32(cache);
        }

        /// <summary>
        /// 验证码错误次数，缓存1小时
        /// </summary>
        /// <returns></returns>
        public int GetVerificationCodeErrorCountOfCache()
        {
            object cache = MemcachedHelper.GetMemcached("verificationCodeErrorCount");
            if (cache == null)
            {
                cache = SystemConfigManager.GetSystemConfigContent("verificationCodeErrorCount");
                if (cache != null)
                {
                    MemcachedHelper.AddMemcached("verificationCodeErrorCount", cache, 60 * 60);//一小时
                }
            }
            return Convert.ToInt32(cache);
        }
        /// <summary>
        /// 设备封锁时间（分钟），缓存1小时
        /// </summary>
        /// <returns></returns>
        public double GetDeviceLockTimeOfCache()
        {
            object cache = MemcachedHelper.GetMemcached("deviceLockTime");
            if (cache == null)
            {
                cache = SystemConfigManager.GetSystemConfigContent("deviceLockTime");
                if (cache != null)
                {
                    MemcachedHelper.AddMemcached("deviceLockTime", cache, 60 * 60);//一小时
                }
            }
            return Convert.ToDouble(cache);
        }

        /// <summary>
        /// 注册前红包个数，缓存1小时
        /// </summary>
        /// <returns></returns>
        public int GetMaxRedEnvelopeCountOfCache()
        {
            object cache = MemcachedHelper.GetMemcached("maxRedEnvelopeCount");
            if (cache == null)
            {
                cache = SystemConfigManager.GetSystemConfigContent("maxRedEnvelopeCount");
                if (cache != null)
                {
                    MemcachedHelper.AddMemcached("maxRedEnvelopeCount", cache, 60 * 60);//一小时
                }
            }
            return Convert.ToInt32(cache);
        }

        /// <summary>
        /// 取系统配置
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public string GetSystemConfig(string val, string defaultVal)
        {
            object cache = MemcachedHelper.GetMemcached(val);
            if (cache == null)
            {
                cache = SystemConfigManager.GetSystemConfigContent(val);
                if (cache != null && cache.ToString() != "")
                {
                    MemcachedHelper.AddMemcached(val, cache, 60 * 60);//一小时
                }
            }
            return cache != null && cache.ToString() != "" ? cache.ToString() : defaultVal;
        }

        public static T GetSystemConfig<T>(string key)
        {
            object cache = MemcachedHelper.GetMemcached(key);
            if (cache == null || (cache is T) == false)
            {
                return default(T);
            }
            return (T)cache;
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="seconds">默认300秒</param>
        /// <returns></returns>
        public static bool SetSystemConfig(string key, object value, double seconds = 300)
        {
            return MemcachedHelper.AddMemcached(key, value, seconds);
        }
    }
}
