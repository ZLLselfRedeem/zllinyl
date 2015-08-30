using Memcached.ClientLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VA.Cache.Distributed
{
    public class SockIOPoolExt : IDisposable
    {
        /// <summary>
        /// 初始化通讯池
        /// </summary>
        public static void Init()
        {
            //测试服务器信息
            //11211为memcached服务默认端口
            //string[] serverlist = { "192.168.5.21:11211", "127.0.0.1:11211", "192.168.1.11:11211" };
            //string[] serverlist = { "192.168.1.11:11211" };
            //string[] serverlist = { "127.0.0.1:11211" };
            string[] serverlist = MemcachedConfiguration.GetServiceAddress();
            int[] weights = new int[] { 1, 1, 1 };//各个缓存服务器的权重
            try
            {
                SockIOPool pool = SockIOPool.GetInstance();
                pool.SetServers(serverlist);
                pool.InitConnections = 500;
                pool.MinConnections = 100;
                pool.MaxConnections = 1000;
                pool.SocketConnectTimeout = 300;
                pool.SocketTimeout = 300;
                pool.MaintenanceSleep = 200;
                pool.Failover = true;
                pool.Nagle = false;
                pool.SetWeights(weights);//设配各个服务器负载作用的系数，系数值越大，其负载作用也就越大　　　
                pool.Initialize();
            }
            catch (Exception)
            {
                //错误日志记录
            }
        }

        public void Dispose()
        {

        }
    }
}
