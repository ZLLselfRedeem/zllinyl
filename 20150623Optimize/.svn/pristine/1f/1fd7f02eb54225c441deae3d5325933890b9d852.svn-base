using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 客户端App，悠先服务版本信息
    /// created by wangc 20140421
    /// </summary>
    public class InstallVersionOperate
    {
        readonly AppBuildManager appManager = new AppBuildManager();
        readonly ServiceBuildManager serviceManager = new ServiceBuildManager();

        #region 客户端App
        /// <summary>
        /// 查询悠先点菜app版本信息（Android，IOS）
        /// </summary>
        /// <returns></returns>
        public DataTable QueryAppLatestBuild()
        {
            return appManager.SelectAppLatestBuild();
        }
        /// <summary>
        /// 更新app版本信息
        /// </summary>
        public bool ModifyAppBuildInfo(AppBuildInfo model)
        {
            return appManager.UpdateAppBuildInfo(model);
        }
        #endregion

        #region 悠先服务模块
        /// <summary>
        /// 查询悠先服务版本信息（pc版本，Android版本，IOS版本）
        /// </summary>
        /// <returns></returns>
        public DataTable QueryServiceLatestBuild()
        {
            return serviceManager.SelectServiceLatestBuild();
        }
        /// <summary>
        /// 更新悠先服务版本信息
        /// </summary>
        public bool ModifyServiceLatestBuild(ServiceBuildInfo model)
        {
            return serviceManager.UpdateServiceLatestBuild(model);
        }
        #endregion
    }
}
