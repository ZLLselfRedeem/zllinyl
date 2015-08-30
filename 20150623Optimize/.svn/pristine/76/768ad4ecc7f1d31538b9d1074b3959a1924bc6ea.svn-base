using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public partial class ShopReportVOperate
    {
        private static ShopReportVManager _ShopReportVManager = new ShopReportVManager();

        public static ShopReportV GetEntityById(int ShopReportId)
        {
            return _ShopReportVManager.GetEntityById(ShopReportId);
        } 

        public static List<ShopReportV> GetListByQuery(int pageSize, int pageIndex, ShopReportVQueryObject queryObject = null,
            ShopReportVOrderColumn orderColumn = ShopReportVOrderColumn.ShopReportId, SortOrder order = SortOrder.Descending)
        {
            return _ShopReportVManager.GetListByQuery(pageSize, pageIndex, queryObject, orderColumn, order);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryObject"></param>
        /// <param name="orderColumn"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static ShopReportV GetFirstByQuery(ShopReportVQueryObject queryObject = null, ShopReportVOrderColumn orderColumn = ShopReportVOrderColumn.ShopReportId,
            SortOrder order = SortOrder.Descending)
        {
            var list = _ShopReportVManager.GetListByQuery(1, 1, queryObject, orderColumn, order);
            if (list != null)
            {
                return list.FirstOrDefault();
            }
            return null;
        }

        public static long GetCountByQuery(ShopReportVQueryObject queryObject = null)
        {
            return _ShopReportVManager.GetCountByQuery(queryObject);
        }
    }
}
