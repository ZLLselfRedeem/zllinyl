using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class PaginationPager
    {
        public PaginationPager()
        {

        }
        public string tblName { get; set; }//表名(注意：可以多表链接) 
        public string strGetFields { get; set; }//需要返回的列 
        public string OrderfldName { get; set; }//排序的字段名 
        public int PageSize { get; set; }//页尺寸 
        public int PageIndex { get; set; }//页码 
        public int doCount { get; set; }//查询到的记录数 
        public byte OrderType { get; set; }//设置排序类型, 非 0 值则降序 
        public string strWhere { get; set; }//查询条件 (注意: 不要加 where) 
        public string realOrderfldName { get; set; }//真·排序名
    }
}
