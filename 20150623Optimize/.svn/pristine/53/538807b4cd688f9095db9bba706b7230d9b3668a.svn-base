using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VA.Cache.Distributed;

namespace VA.CacheLogic.OrderClient
{
    /// <summary>
    /// 年夜饭套餐相关缓存
    /// </summary>
    public class MealCacheLogic
    {
        /// <summary>
        /// 年夜饭套餐活动规则，缓存10分钟 
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        public MealActivity GetMealActivityRuleOfCache()
        {
            object configCache = MemcachedHelper.GetMemcached("mealActivityRule");
            if (configCache == null)
            {
                MealActivity mealActivity = new MealActivity();
                mealActivity.activityRule = SystemConfigManager.GetVAMealActivityRule();
                mealActivity.activityRuleMini = SystemConfigManager.GetVAMealActivityRuleMini();

                configCache = mealActivity;
                if (configCache != null)
                {
                    MemcachedHelper.AddMemcached("mealActivityRule", configCache, 60 * 10);
                }
            }
            return (MealActivity)configCache;
        }

        /// <summary>
        /// 年夜饭套餐有效周期(单位:分钟)，缓存10分钟 
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        public double GetMealValidPeriodOfCache()
        {
            object configCache = MemcachedHelper.GetMemcached("mealValidPeriod");
            if (configCache == null)
            {
                double period = SystemConfigManager.GetVAMealValidPeriod();

                configCache = period;
                if (configCache != null)
                {
                    MemcachedHelper.AddMemcached("mealValidPeriod", configCache, 60 * 10);
                }
            }
            return (double)configCache;
        }

        /// <summary>
        /// 查询该城市已合作年夜饭的餐厅所在区域，缓存1分钟
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public List<ShopTag> GetMealShopTagOfCache(int cityId)
        {
            object mealShopTagOfCache = MemcachedHelper.GetMemcached("mealShopTag_" + cityId);

            if (mealShopTagOfCache == null)
            {
                List<ShopTag> shopTag = MealManager.SelectMealShopTag(cityId);
                if (shopTag != null && shopTag.Any())
                {
                    mealShopTagOfCache = shopTag;
                    MemcachedHelper.AddMemcached("mealShopTag_" + cityId, mealShopTagOfCache, 60);
                }
            }
            return (List<ShopTag>)mealShopTagOfCache;
        }

        /// <summary>
        /// 根据套餐ID查询套餐信息，缓存1分钟
        /// </summary>
        /// <param name="mealId"></param>
        /// <returns></returns>
        public MealList GetMealListOfCache(int mealId)
        {
            object mealListOfCache = MemcachedHelper.GetMemcached("mealList_" + mealId);
            if (mealListOfCache == null)
            {
                MealList mealList = MealManager.SelectMealList(mealId);
                if (mealList != null)
                {
                    mealList.menu.Replace(" ", "");
                    mealListOfCache = mealList;
                    MemcachedHelper.AddMemcached("mealList_" + mealId, mealListOfCache, 60 * 1);
                }
            }
            return (MealList)mealListOfCache;
        }
    }
}
