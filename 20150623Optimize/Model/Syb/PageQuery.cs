using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 功能描述:分页查询(目前用于后台的分页数据的查询获取)
    /// 创建标识:罗国华 20131105
    /// </summary>
    public class Paging
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int recordCount { get; set; }
        public int pageCount { get; set; }
    }

    /// <summary>
    /// 查询内容
    /// </summary>
    public class PageQuery
    {
        public string tableName { get; set; }
        public string fields { get; set; }
        public string orderField { get; set; }
        public string sqlWhere { get; set; }
    }
    /// <summary>
    /// list 分页 created by wangc 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagingUtil
    {
        public int recordCount { get; set; }//总记录数
        public int pageCount { get; set; }//总页数
        public int pageIndex { get; set; }//页码
        public int pageSize { get; set; }//页面尺寸
        public string data { get; set; }//数据源
    }
}
