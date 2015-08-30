using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public partial class ShopStaticsReportOperate
    {
        private static ShopStaticsReportManager _ShopStaticsReportManager = new ShopStaticsReportManager();

        public static bool Add(ShopStaticsReport Entity)
        {
            return _ShopStaticsReportManager.Add(Entity);
        }
        public static bool AddList(List<ShopStaticsReport> list)
        {
            return _ShopStaticsReportManager.AddList(list);
        }

        public static bool Update(ShopStaticsReport Entity)
        {
            return _ShopStaticsReportManager.Update(Entity);
        }

        public static bool DeleteEntity(ShopStaticsReport Entity)
        {
            return _ShopStaticsReportManager.DeleteEntity(Entity);
        }

        public static ShopStaticsReport GetEntityById(long ShopStaticsReportId)
        {
            return _ShopStaticsReportManager.GetEntityById(ShopStaticsReportId);
        } 

        public static List<ShopStaticsReport> GetListByQuery(int pageSize, int pageIndex, ShopStaticsReportQueryObject queryObject = null,
            ShopStaticsReportOrderColumn orderColumn = ShopStaticsReportOrderColumn.ShopStaticsReportId, SortOrder order = SortOrder.Descending)
        {
            return _ShopStaticsReportManager.GetListByQuery(pageSize, pageIndex, queryObject, orderColumn, order);
        }

        public static List<ShopStaticsReport> GetListByQuery(ShopStaticsReportQueryObject queryObject = null,
            ShopStaticsReportOrderColumn orderColumn = ShopStaticsReportOrderColumn.ShopStaticsReportId, SortOrder order = SortOrder.Descending)
        {
            return _ShopStaticsReportManager.GetListByQuery(queryObject, orderColumn, order);
        }

        public static ShopStaticsReport GetFirstByQuery(ShopStaticsReportQueryObject queryObject = null, ShopStaticsReportOrderColumn orderColumn = ShopStaticsReportOrderColumn.ShopStaticsReportId,
            SortOrder order = SortOrder.Descending)
        {
            return _ShopStaticsReportManager.GetListByQuery(1, 1, queryObject, orderColumn, order).FirstOrDefault();
        }

        public static long GetCountByQuery(ShopStaticsReportQueryObject queryObject = null)
        {
            return _ShopStaticsReportManager.GetCountByQuery(queryObject);
        }
    }
}
