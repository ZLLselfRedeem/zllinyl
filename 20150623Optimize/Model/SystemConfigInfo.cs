using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 系统配置信息
    /// </summary>
    public class SystemConfigInfo
    {
        /// <summary>
        /// 系统配置编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 系统配置名称
        /// </summary>
        public string configName { get; set; }
        /// <summary>
        /// 系统配置描述内容
        /// </summary>
        public string configDescription { get; set; }
        /// <summary>
        /// 系统配置内容
        /// </summary>
        public string configContent { get; set; }
    }
}
