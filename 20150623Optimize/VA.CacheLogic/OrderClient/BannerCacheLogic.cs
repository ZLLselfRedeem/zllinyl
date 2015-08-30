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
    public class BannerCacheLogic
    {
        /// <summary>
        /// 城市对应的首页Banner信息，缓存5分钟
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable GeBannerByCityId(int cityId)
        {
            object IndexBannerByCityIdCache = MemcachedHelper.GetMemcached("IndexBanner_" + cityId);
            if (IndexBannerByCityIdCache == null)
            {
                CompanyManager companyMan = new CompanyManager();
                DataTable dtCityCompanyBannerold = companyMan.SelectCompanyBannerByCityId(cityId, VAAdvertisementClassify.INDEX_AD);
                IndexBannerByCityIdCache = dtCityCompanyBannerold;
                if (IndexBannerByCityIdCache != null)
                {
                    MemcachedHelper.AddMemcached("IndexBanner_" + cityId, IndexBannerByCityIdCache, 60 * 5);
                }
            }
            return (DataTable)IndexBannerByCityIdCache;
        }

        /// <summary>
        /// 从Cache中读取美食广场Banner信息，缓存5分钟
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable GetFoodPlazaBannerByCityId(int cityId)
        {
            object foodPlazaBannerCache = MemcachedHelper.GetMemcached("foodPlazaBanner_" + cityId);
            if (foodPlazaBannerCache == null)
            {
                foodPlazaBannerCache = new CompanyManager().SelectCompanyBannerByCityId(cityId, VAAdvertisementClassify.FOODPLAZA_AD);//广告处理
                if (foodPlazaBannerCache != null)
                {
                    MemcachedHelper.AddMemcached("foodPlazaBanner_" + cityId, foodPlazaBannerCache, 60 * 5);//五分钟
                }
            }
            return (DataTable)foodPlazaBannerCache;
        }
    }
}
