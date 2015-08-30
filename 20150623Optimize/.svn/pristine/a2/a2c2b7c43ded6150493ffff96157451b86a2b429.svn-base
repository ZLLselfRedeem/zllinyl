using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class WechatTopPriceOperator
    {
        private WechatTopPriceManager topPriceManager;

        public WechatTopPriceOperator()
        {
            topPriceManager = new WechatTopPriceManager();    
        }

        //获取所有 本期大奖信息 (包括历史数据)
        public DataTable GetTopPriceInfo()
        {
            return topPriceManager.GetTopPriceInfo();
        }

        //获取一条最新的未过期数据
        public string GetTopOneOfTopPriceInfo()
        {
            return topPriceManager.GetTopOneOfTopPriceInfo();
        }

        /// <summary>
        /// 获取所有 本期大奖信息 (包括历史数据)  发布日期时间(str起始时间   end截止时间)
        /// </summary>
        /// <returns></returns>
        public DataTable GetTopPriceInfo(string str, string end)
        {
            return topPriceManager.GetTopPriceInfo(str, end);
        }
        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="topPriceInfo"></param>
        /// <returns></returns>
        public int Insert(WechatTopPriceInfo topPriceInfo)
        {
            return topPriceManager.Insert(topPriceInfo);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="topPriceInfo"></param>
        /// <returns></returns>
        public int Update(WechatTopPriceInfo topPriceInfo)
        {
            return topPriceManager.Update(topPriceInfo);
        }

        /// <summary>
        /// 删除数据  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            return topPriceManager.Delete(id);
        }

        /// <summary>
        /// 根据vip号查询 联系方式etc.
        /// </summary>
        /// <param name="eCardNum"></param>
        /// <returns></returns>
        public DataTable GetVIPContectInfo(string eCardNum)
        {
            return topPriceManager.GetVIPContectInfo(eCardNum);
        }
    }
}
