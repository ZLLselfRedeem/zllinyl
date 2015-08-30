using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class WechatHotMenuOperate
    {
        private WechatHotMenuManager hotMenuManager;
        public WechatHotMenuOperate()
        {
            if (hotMenuManager == null)
                hotMenuManager = new WechatHotMenuManager();
        }

        /// <summary>
        /// 获取热菜 按地区统计信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetHotMenuStatisticInfo()
        {
            return hotMenuManager.GetHotMenuStatisticInfo();
        }

        /// <summary>
        /// 插入热菜设置 信息
        /// </summary>
        /// <param name="hotMenuInfo"></param>
        /// <returns></returns>
        public int InsertHotMenu(WechatHotMenuInfo hotMenuInfo)
        {
            return hotMenuManager.InsertHotMenu(hotMenuInfo);
        }

        /// <summary>
        /// 按城市名获取设置 的dishid
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public int GetDishIDByCityName(string cityName)
        {
            return hotMenuManager.GetDishIDByCityName(cityName);
        }

        public DataTable GetHopMenuInfo()
        {
            return hotMenuManager.GetHopMenuInfo();
        }

        public DataTable GetHopMenuInfo(int DishID)
        {
            return hotMenuManager.GetHopMenuInfo(DishID);
        }
    }
}
