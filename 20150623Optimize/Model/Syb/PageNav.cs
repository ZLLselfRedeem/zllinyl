using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 功能描述:分页导航 (目前用于前台的页面导航条)
    /// 创建标识:罗国华 20131104
    /// </summary>
    public class PageNav
    {
        /// <summary>
        /// 总记录
        /// </summary>
        public int totalCount { get; set; }
        /// <summary>
        /// 当前页码
        /// </summary>
        public int pageIndex { get; set; }
        /// <summary>
        /// 每页大小
        /// </summary>
        public int pageSize { get; set; }
    }
}
