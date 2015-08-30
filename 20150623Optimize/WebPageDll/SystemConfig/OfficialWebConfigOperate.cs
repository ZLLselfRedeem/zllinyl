using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;
namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 官网配置文件BLL
    /// 创建日期：2014-4-22
    /// </summary>
    public class OfficialWebConfigOperate
    {
        OfficialWebConfigManager webManager = new OfficialWebConfigManager();
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="web"></param>
        /// <returns></returns>
        public int InsertOfficialWebConfig(OfficialWebConfig web)
        {
            return webManager.InsertOfficialWebConfig(web);
        }
        public int InsertOfficialWebConfigWithID(OfficialWebConfig web)
        {
            return webManager.InsertOfficialWebConfigWithID(web);
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="web"></param>
        /// <returns></returns>
        public bool UpdateOfficialWebConfig(OfficialWebConfig web)
        {
            return webManager.UpdateOfficialWebConfig(web);
        }
        /// <summary>
        /// 根据ID删除指定信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteOfficialWebConfig(long id)
        {
            return webManager.DeleteOfficialWebConfig(id);
        }
        /// <summary>
        /// 根据类别获取相应数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable QueryOfficialWebConfig(VAOfficialWebType type)
        {
            return webManager.QueryOfficialWebConfig(type);
        }
        /// <summary>
        /// 根据ID获取相应数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable QueryOfficialWebConfig(long id)
        {
            return webManager.QueryOfficialWebConfig(id);
        }
        /// <summary>
        /// 获取指定类型的最新动态
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable QueryRencetNewWithType(string type)
        {
            return webManager.QueryRencetNewWithType(type);
        }
        /// <summary>
        /// 获取指定type的当前最大sequence
        /// </summary>
        /// <returns></returns>
        public int QueryMaxSquence(VAOfficialWebType type)
        {
            return webManager.QueryMaxSquence(type);
        }
        /// <summary>
        /// 最新动态类别：根据类别名称找其排序号
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public string GetSequenceByType(string title)
        {
            return webManager.GetSequenceByType(title);
        }
         /// <summary>
        /// 获取最新动态的所有类别
        /// </summary>
        /// <returns></returns>
        public DataTable GetRecentNewsType()
        {
            return webManager.GetRecentNewsType();
        }
    }
}
