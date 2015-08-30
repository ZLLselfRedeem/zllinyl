using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 特殊权限
    /// </summary>
   public class SpecialAuthorityInfo
    {
        /// <summary>
        /// 对应角色的ID
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 特殊权限的枚举值
        /// </summary>
        public int specialAuthorityId { get; set; }
        /// <summary>
        /// 是否具备该特殊权限
        /// </summary>
        public bool status { get; set; }
       
    }
}
