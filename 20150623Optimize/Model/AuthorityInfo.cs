using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 后台管理权限信息
    /// </summary>
    public class AuthorityInfo
    {
        /// <summary>
        /// 权限编号
        /// </summary>
        public int AuthorityID { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string AuthorityName { get; set; }
        /// <summary>
        /// 权限URL地址
        /// </summary>
        public string AuthorityURL { get; set; }
        /// <summary>
        /// 权限级别
        /// </summary>
        public int AuthorityRank { get; set; }
        /// <summary>
        /// 权限描述
        /// </summary>
        public string AuthorityDescription { get; set; }
        /// <summary>
        /// 权限序号
        /// </summary>
        public int AuthoritySequence { get; set; }
        /// <summary>
        /// 权限状态
        /// </summary>
        public int AuthorityStatus { get; set; }
        /// <summary>
        /// 权限类型(页面权限还是按钮权限)
        /// </summary>
        public string AuthorityType { get; set; }
    }
}
