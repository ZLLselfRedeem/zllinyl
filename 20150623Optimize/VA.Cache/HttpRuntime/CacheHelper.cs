using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;

namespace VA.Cache.HttpRuntime
{
    public class CacheHelper
    {
        private static System.Web.Caching.Cache httpRuntimeCache = System.Web.HttpRuntime.Cache;

        /// <summary>
        /// 新增缓存（覆盖原有，永不过期）
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        public static void AddCache(string key, object value)
        {
            httpRuntimeCache.Insert(key, value);
        }

        /// <summary>
        /// 新增缓存（覆盖原有，<Seconds>秒后过期）
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="seconds">Seconds 秒后过期</param>
        public static void AddCache(string key, object value, int seconds)
        {
            httpRuntimeCache.Insert(key, value, null, DateTime.Now.AddSeconds(seconds), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// 新增缓存，设置滑动<Seconds>秒后过期
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="seconds">滑动时间</param>
        public static void AddCache(string key, object value, double seconds)
        {
            httpRuntimeCache.Insert(key, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(seconds));
        }

        /// <summary>
        /// 新增缓存（覆盖原有）
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="seconds">Seconds 秒后过期</param>
        /// <param name="enumItem">服务器回收缓存优先等级</param>
        public static void AddCache(string key, object value, int seconds, CacheItemPriorityExt enumItem)
        {
            httpRuntimeCache.Insert(key, value, null, DateTime.Now.AddSeconds(seconds), System.Web.Caching.Cache.NoSlidingExpiration, (CacheItemPriority)enumItem, null);
        }

        /// <summary>
        /// 新增缓存，并设置<Seconds>秒的滑过过期时间，并指定移除通知委托方法delegateFun
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="seconds">滑动过期时间</param>
        /// <param name="delegateFun">缓存移除，委托方法调用</param>
        public static void AddCache(string key, object value, double seconds, CacheItemRemovedCallback delegateFun)
        {
            httpRuntimeCache.Insert(key, value, null, System.Web.Caching.Cache.NoAbsoluteExpiration, TimeSpan.FromSeconds(seconds), CacheItemPriority.NotRemovable, delegateFun);
        }

        /// <summary>
        /// 移除指定Key缓存Value
        /// </summary>
        /// <param name="key">Key</param>
        public static void RemoveCache(string key)
        {
            httpRuntimeCache.Remove(key);
        }

        /// <summary>
        /// 移除所有缓存
        /// </summary>
        public static void RemoveAllCache()
        {
            IDictionaryEnumerator dicCache = System.Web.HttpRuntime.Cache.GetEnumerator();
            int count = System.Web.HttpRuntime.Cache.Count;
            while (dicCache.MoveNext())
            {
                httpRuntimeCache.Remove(dicCache.Key.ToString());
            }
        }

        /// <summary>
        /// 获取指定Key的缓存Value
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns></returns>
        public static object GetCache(string key)
        {
            object obj = httpRuntimeCache.Get(key);
            return obj;
        }
    }
}
