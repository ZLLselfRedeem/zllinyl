
using System.Collections.Generic;
using VA.CacheLogic;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class ClientStartImgConfigOperate
    {
        /// <summary>
        /// 获取悠先点菜启动图地址
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<ClientStartImgConfig> GetClientStartImgConfigInfos(int type)
        {
            return new SystemConfigCacheLogic().ClientStartImgConfigInfosOfCache(type);
        }
    }
}
