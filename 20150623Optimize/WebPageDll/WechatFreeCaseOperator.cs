using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class WechatFreeCaseOperator
    {
        private WechatFreeCaseManager freeCaseManager;
        public WechatFreeCaseOperator()
        {
            if (freeCaseManager == null)
                freeCaseManager = new WechatFreeCaseManager();
        }

        /// <summary>
        /// 获取所有 本期免单信息 (包括历史数据)  发布日期时间(str起始时间   end截止时间)
        /// </summary>
        /// <returns></returns>
        public DataTable GetFreeCaseInfo(string str, string end)
        {
            return freeCaseManager.GetFreeCaseInfo(str, end);
        }

        public string GetTopOneFreeCaseInfo()
        {
            return freeCaseManager.GetTopOneFreeCaseInfo();
        }

        public int Insert(WechatFreeCaseInfo freeCaseInfo)
        {
            return freeCaseManager.Insert(freeCaseInfo);
        }

        public int Update(WechatFreeCaseInfo freeCaseInfo)
        {
            return freeCaseManager.Update(freeCaseInfo);
        }

        public bool Delete(int id)
        {
            return freeCaseManager.Delete(id);
        }
    }
}
