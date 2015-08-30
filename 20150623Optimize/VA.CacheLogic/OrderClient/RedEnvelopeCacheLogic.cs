using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VA.Cache.Distributed;

namespace VA.CacheLogic.OrderClient
{
    public class RedEnvelopeCacheLogic
    {
        /// <summary>
        /// 宝箱配置文件，缓存10分钟 
        /// </summary>
        /// <param name="configId"></param>
        /// <returns></returns>
        public TreasureChestConfig GetTreasureChestConfigOfCache(int configId)
        {
            object configCache = MemcachedHelper.GetMemcached("redEnveope_configCache_" + configId);
            if (configCache == null)
            {
                TreasureChestConfigManager treasureChestConfigManager = new TreasureChestConfigManager();
                configCache = treasureChestConfigManager.QueryTreasureChest(configId);
                if (configCache != null)
                {
                    MemcachedHelper.AddMemcached("redEnveope_configCache_" + configId, configCache, 60 * 10);
                }
            }
            return (TreasureChestConfig)configCache;
        }

        public TreasureChestConfig GetConfigOfActivityOfCache(int activityId)
        {
            object configCache = MemcachedHelper.GetMemcached("redEnveope_configCache_activity_" + activityId);
            if (configCache == null)
            {
                TreasureChestConfigManager treasureChestConfigManager = new TreasureChestConfigManager();
                configCache = treasureChestConfigManager.QueryConfigOfActivity(activityId);
                if (configCache != null)
                {
                    MemcachedHelper.AddMemcached("redEnveope_configCache_activity_" + activityId, configCache, 60 * 10);
                }
            }
            return (TreasureChestConfig)configCache;
        }

        /// <summary>
        /// 宝箱信息，缓存1分钟
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public TreasureChest GetTreasureChestOfCache(int activityId)
        {
            object treasureChestCache = MemcachedHelper.GetMemcached("redEnvelope_treasureChest_" + activityId);
            if (treasureChestCache == null)
            {
                TreasureChestManager treasureChestManager = new TreasureChestManager();
                treasureChestCache = treasureChestManager.GetByActivity(activityId);
                if (treasureChestCache != null)
                {
                    MemcachedHelper.AddMemcached("redEnvelope_treasureChest_" + activityId, treasureChestCache, 60);
                }
            }
            return (TreasureChest)treasureChestCache;
        }
        /// <summary>
        /// 红包领取总额排行榜，缓存20分钟
        /// </summary>
        /// <returns></returns>
        public object GetRankListOfCache()
        {
            List<TopRedEnvelopeRankList> rankListCache = MemcachedHelper.GetMemcached<List<TopRedEnvelopeRankList>>("redEnvelope_rankList");
            if (rankListCache == null || !rankListCache.Any())
            {
                RedEnvelopeManager redEnvelopeManager = new RedEnvelopeManager();
                rankListCache = redEnvelopeManager.GetTopRankList();
                if (rankListCache != null && rankListCache.Any())
                {
                    MemcachedHelper.AddMemcached("redEnvelope_rankList", rankListCache, 60 * 20);

                    return rankListCache.Select(p => new
                    {
                        mobilePhoneNumber = p.mobilePhoneNumber.Remove(3, 4).Insert(3, "xxxx"),
                        amount = Convert.ToDouble(p.amount).ToString("F1"),
                        context = "分享拿钱,以后点菜就用悠先了."
                    });
                }
            }
            else
            {
                return rankListCache.Select(p => new
                {
                    mobilePhoneNumber = p.mobilePhoneNumber.Remove(3, 4).Insert(3, "xxxx"),
                    amount = Convert.ToDouble(p.amount).ToString("F1"),
                    context = "分享拿钱,以后点菜就用悠先了."
                });
            }
            return rankListCache;
        }
        /// <summary>
        /// 活动信息，缓存10分钟
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public Activity GetActivityOfCache(int activityId)
        {
            object activityInfoCache = MemcachedHelper.GetMemcached("activity_" + activityId);
            var activity = new Activity();
            if (activityInfoCache == null)
            {
                ActivityManager activityManager = new ActivityManager();
                activity = activityManager.QueryActivity(activityId);//查询活动信息
                if (activity != null)
                {
                    MemcachedHelper.AddMemcached("activity_" + activityId, activity, 60 * 10);
                }
            }
            else
            {
                activity = (Activity)activityInfoCache;
            }
            return activity;
        }
        /// <summary>
        /// 活动分享信息，缓存10分钟
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public List<ActivityShareInfo> GetActivityShareInfoOfCache(int activityId)
        {
            object activityShareInfoCache = MemcachedHelper.GetMemcached("activity_share_" + activityId);

            if (activityShareInfoCache == null)
            {
                ActivityShareManager activityShareManager = new ActivityShareManager();
                List<ActivityShareInfo> activityShare = activityShareManager.GetManyByActivity(activityId);//查询活动分享信息
                if (activityShare != null && activityShare.Any())
                {
                    MemcachedHelper.AddMemcached("activity_share_" + activityId, activityShare, 60 * 10);
                }
                return activityShare;
            }
            else
            {
                return (List<ActivityShareInfo>)activityShareInfoCache;
            }
        }
    }
}
