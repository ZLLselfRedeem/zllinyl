using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 美食分享客户端webview描述配置项业务逻辑层
    /// created by wangc
    /// 20140616
    /// </summary>
    public class FoodDiariesShareConfigOperate
    {
        readonly FoodDiariesShareConfigManager man = new FoodDiariesShareConfigManager();
        /// <summary>
        /// 查询所有美食日记分享配置信息
        /// </summary>
        /// <returns></returns>
        public List<FoodDiariesShareConfig> GetAllFoodDiariesShareConfig()
        {
            return man.GetAllFoodDiariesShareConfig();
        }
        /// <summary>
        /// 新增美食日记分享配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InsertFoodDiariesShareConfig(FoodDiariesShareConfig model)
        {
            return man.InsertFoodDiariesShareConfig(model);
        }
        /// <summary>
        /// 更新美食日记分享配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateFoodDiariesShareConfig(FoodDiariesShareConfig model)
        {
            return man.UpdateFoodDiariesShareConfig(model);
        }
        /// <summary>
        /// 删除美食日记分享配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteFoodDiariesShareConfig(int id)
        {
            return man.DeleteFoodDiariesShareConfig(id);
        }
    }
}
