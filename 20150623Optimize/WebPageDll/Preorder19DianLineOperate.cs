using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.Interface;
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public partial class Preorder19DianLineOperate
    {
        private static Preorder19DianLineManager _Preorder19DianLineManager = new Preorder19DianLineManager();

        public static bool Add(IPreorder19DianLine Entity)
        {
            return _Preorder19DianLineManager.Add(Entity);
        }

        public static bool Update(IPreorder19DianLine Entity)
        {
            return _Preorder19DianLineManager.Update(Entity);
        }

        public static bool DeleteEntity(IPreorder19DianLine Entity)
        {
            return _Preorder19DianLineManager.DeleteEntity(Entity);
        }

        public static IPreorder19DianLine GetEntityById(long Preorder19DianLineId)
        {
            return _Preorder19DianLineManager.GetEntityById(Preorder19DianLineId);
        }

        public static List<IPreorder19DianLine> GetListByQuery(int pageSize, int pageIndex, Preorder19DianLineQueryObject queryObject = null,
            Preorder19DianLineOrderColumn orderColumn = Preorder19DianLineOrderColumn.Preorder19DianLineId, SortOrder order = SortOrder.Descending)
        {
            return _Preorder19DianLineManager.GetListByQuery(pageSize, pageIndex, queryObject, orderColumn, order);
        }

        public static List<IPreorder19DianLine> GetListByQuery(Preorder19DianLineQueryObject queryObject = null,
            Preorder19DianLineOrderColumn orderColumn = Preorder19DianLineOrderColumn.Preorder19DianLineId, SortOrder order = SortOrder.Descending)
        {
            return _Preorder19DianLineManager.GetListByQuery(queryObject, orderColumn, order);
        }

        public static IPreorder19DianLine GetFirstByQuery(Preorder19DianLineQueryObject queryObject = null, Preorder19DianLineOrderColumn orderColumn = Preorder19DianLineOrderColumn.Preorder19DianLineId,
            SortOrder order = SortOrder.Descending)
        {
            return _Preorder19DianLineManager.GetListByQuery(1, 1, queryObject, orderColumn, order).FirstOrDefault();
        }

        public static long GetCountByQuery(Preorder19DianLineQueryObject queryObject)
        {
            return _Preorder19DianLineManager.GetCountByQuery(queryObject);
        }

        

        /// <summary>
        /// 按订单id返回列表
        /// </summary>
        /// <param name="preorder19DianId">订单id</param>
        /// <returns></returns>
        public IList<Preorder19DianLine> GetListOfPreorder19DianId(long preorder19DianId)
        {
            return new Preorder19DianLineManager().GetListOfPreorder19DianId(preorder19DianId).ToList();
        }
    }
}