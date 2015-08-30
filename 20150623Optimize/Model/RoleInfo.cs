using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 后台管理角色信息
    /// </summary>
    public class RoleInfo
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public int RoleID { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }
        /// <summary>
        /// 角色描述
        /// </summary>
        public string RoleDescription { get; set; }
        /// <summary>
        /// 角色状态
        /// </summary>
        public int RoleStatus { get; set; }
    }
}
