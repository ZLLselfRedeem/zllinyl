using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;

namespace VAGastronomistMobileApp.WebPageDll
{
   public class VAEncourageConfigOperate
    {
        VAEncourageConfigManager VAEncourageConfigManager = new VAEncourageConfigManager();
        /// <summary>
        /// 添加系统设置
        /// </summary>
        /// <param name="VAEncourageConfigInfo"></param>
        public void AddVAEncourageConfig(VAEncourageConfig vAEncourageConfigInfo)
        {
            VAEncourageConfigManager.InsertVAEncourageConfig(vAEncourageConfigInfo);
        }
        /// <summary>
        /// 修改系统设置
        /// </summary>
        /// <param name="VAEncourageConfigInfo"></param>
        public void ModifyVAEncourageConfig(VAEncourageConfig vAEncourageConfigInfo)
        {
            VAEncourageConfigManager.UpdateVAEncourageConfig(vAEncourageConfigInfo);
        }
        /// <summary>
        /// 查询系统设置信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryVAEncourageConfig()
        {
            return VAEncourageConfigManager.SelectVAEncourageConfig();
        }
    }
}
