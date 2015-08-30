using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VA.Cache.Distributed
{
    /// <summary>
    /// 分布式缓存Memcached辅助类
    /// </summary>
    public class MemcachedHelper
    {
        /// <summary>
        /// Memcached实例字段
        /// </summary>
        private static readonly MemcachedClient _MemcachedClient = Singleton<MemcachedClient>.Instance;

        #region 读
        /// <summary>
        /// 获取缓存对象
        /// </summary>
        /// <param name="key">指向缓存Key</param>
        /// <returns>返回object对象</returns>
        public static object GetMemcached(string key)
        {
            return _MemcachedClient.Get(key);
        }

        /// <summary>
        /// 获取转换为T类型缓存对象
        /// </summary>
        /// <typeparam name="T">缓存对象类型</typeparam>
        /// <param name="key">指向缓存Key</param>
        /// <returns>返回T类型对象</returns>
        public static T GetMemcached<T>(string key)
        {
            object data = _MemcachedClient.Get(key);
            if (data is T)
            {
                return (T)data;
            }
            else
            {
                return default(T);//null
            }
        }
        #endregion

        #region 写
        /// <summary>
        /// 添加指定Key缓存值Value（没有则插入，有则修改）
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        public static bool AddMemcached(string key, object value)
        {
            return _MemcachedClient.Set(key, value);
        }

        /// <summary>
        /// 添加指定Key缓存值Value，并设置过期时间（没有则插入，有则修改）
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="value">Value</param>
        /// <param name="seconds">过期时间，单位秒</param>
        /// <returns></returns>
        public static bool AddMemcached(string key, object value, double seconds)
        {
            return _MemcachedClient.Set(key, value, DateTime.Now.AddSeconds(seconds));
        }
        #endregion

        #region 删
        /// <summary>
        /// 删除指定Key缓存
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns></returns>
        public static bool DeleteMemcached(string key)
        {
            return _MemcachedClient.Delete(key);
        }

        /// <summary>
        /// 删除全部缓存
        /// </summary>
        public static void DeleteAllMemcached()
        {
            _MemcachedClient.FlushAll();
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新指定Key缓存数据
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static bool UpdateMemcached(string key, object value)
        {
            return _MemcachedClient.Replace(key, value);
        }

        /// <summary>
        /// 更新指定Key缓存数据，并指定过期时间
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="seconds">过期时间，单位秒</param>
        public static bool UpdateMemcached(string key, object value, double seconds)
        {
            return _MemcachedClient.Replace(key, value, DateTime.Now.AddSeconds(seconds));
        }
        #endregion

        #region 判断
        /// <summary>
        /// 判断缓存对象Key是否存在
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns></returns>
        public static bool IsKeyExists(string key)
        {
            return _MemcachedClient.KeyExists(key);
        }
        #endregion
    }
}
