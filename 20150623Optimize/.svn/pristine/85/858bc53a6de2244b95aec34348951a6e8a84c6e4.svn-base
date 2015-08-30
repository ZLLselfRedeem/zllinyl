using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace VA.Cache.Distributed
{
    public class MemcachedConfiguration
    {
        /// <summary>
        /// 获取配置文件中缓存服务器Ip和端口地址字符串
        /// </summary>
        private static readonly string MemcachedServiceAddress = ConfigurationManager.AppSettings["MemcachedServiceAddress"];

        /// <summary>
        /// 获取配置文件中缓存服务器Ip和端口地址数组
        /// </summary>
        /// <returns></returns>
        public static string[] GetServiceAddress()
        {
            if (String.IsNullOrWhiteSpace(MemcachedServiceAddress))
            {
                return null;
            }
            string[] str = MemcachedServiceAddress.Split(',');
            return str;
        }
    }
}
