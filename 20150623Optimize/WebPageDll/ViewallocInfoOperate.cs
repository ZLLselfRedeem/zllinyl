using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 友络平台配置信息
    /// created by wangcheng
    /// 20140210
    /// </summary>
    public class ViewallocInfoOperate
    {
        private readonly ViewallocInfoManager manager = new ViewallocInfoManager();
        /// <summary>
        /// 查询VIewAlloc平台VIP等级信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryViewAllocPlatformVipInfo()
        {
            return manager.SelectViewAllocPlatformVipInfo();
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            return manager.Delete(id);
        }
        /// <summary>
        ///  更新一条数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="consumptionLevel"></param>
        /// <returns></returns>
        public bool Update(int id, string name, double consumptionLevel, string vipImgName)
        {
            return manager.Update(id, name, consumptionLevel, vipImgName);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="name"></param>
        /// <param name="consumptionLevel"></param>
        /// <returns></returns>
        public int Add(string name, double consumptionLevel, string vipImgName)
        {
            return manager.Add(name, consumptionLevel, vipImgName);
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exists(string name)
        {
            return manager.Exists(name);
        }
    }
}
