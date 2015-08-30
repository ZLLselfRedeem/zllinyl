using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public partial class PreOrder19dianVOperate
    {
        private static PreOrder19dianVManager _PreOrder19dianVManager = new PreOrder19dianVManager();

        public static PreOrder19dianV GetEntityById(long PreOrder19dianId)
        {
            return _PreOrder19dianVManager.GetEntityById(PreOrder19dianId);
        }

        public static List<PreOrder19dianV> GetListByQuery(int pageSize, int pageIndex, PreOrder19dianVQueryObject queryObject = null,
            PreOrder19dianVOrderColumn orderColumn = PreOrder19dianVOrderColumn.PreOrder19dianId, SortOrder order = SortOrder.Descending)
        {
            return _PreOrder19dianVManager.GetListByQuery(pageSize, pageIndex, queryObject, orderColumn, order);
        }

        public static List<PreOrder19dianV> GetListByQuery(PreOrder19dianVQueryObject queryObject = null,
            PreOrder19dianVOrderColumn orderColumn = PreOrder19dianVOrderColumn.PreOrder19dianId, SortOrder order = SortOrder.Descending)
        {
            return _PreOrder19dianVManager.GetListByQuery(queryObject, orderColumn, order);
        }

        public static PreOrder19dianV GetFirstByQuery(PreOrder19dianVQueryObject queryObject = null, PreOrder19dianVOrderColumn orderColumn = PreOrder19dianVOrderColumn.PreOrder19dianId,
            SortOrder order = SortOrder.Descending)
        {
            var list = _PreOrder19dianVManager.GetListByQuery(1, 1, queryObject, orderColumn, order);
            if (list != null)
            {
                return list.FirstOrDefault();
            }
            return null;
        }

        public static long GetCountByQuery(PreOrder19dianVQueryObject queryObject = null)
        {
            return _PreOrder19dianVManager.GetCountByQuery(queryObject);
        }
    }
}
