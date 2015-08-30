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
    public partial class Preorder19DianLineVOperate
    {
        private static Preorder19DianLineVManager _Preorder19DianLineVManager = new Preorder19DianLineVManager();
        
        public static IPreorder19DianLineV GetEntityById(long Preorder19DianLineId)
        {
            return _Preorder19DianLineVManager.GetEntityById(Preorder19DianLineId); 
        }
        
        public static List<IPreorder19DianLineV> GetListByQuery( int pageSize, int pageIndex,Preorder19DianLineVQueryObject queryObject = null,
            Preorder19DianLineVOrderColumn orderColumn = Preorder19DianLineVOrderColumn.Preorder19DianLineId, SortOrder order = SortOrder.Descending)
        {
            return _Preorder19DianLineVManager.GetListByQuery( pageSize, pageIndex,queryObject, orderColumn, order);
        }
        
        public static List<IPreorder19DianLineV> GetListByQuery(Preorder19DianLineVQueryObject queryObject = null,
            Preorder19DianLineVOrderColumn orderColumn = Preorder19DianLineVOrderColumn.Preorder19DianLineId, SortOrder order = SortOrder.Descending)
        {
            return _Preorder19DianLineVManager.GetListByQuery(queryObject, orderColumn, order);
        }
        
        public static IPreorder19DianLineV GetFirstByQuery(Preorder19DianLineVQueryObject queryObject =null, Preorder19DianLineVOrderColumn orderColumn = Preorder19DianLineVOrderColumn.Preorder19DianLineId, 
            SortOrder order = SortOrder.Descending)            
        {
            var list = _Preorder19DianLineVManager.GetListByQuery( 1, 1,queryObject, orderColumn, order);
            if (list != null)
            {
                return list.FirstOrDefault();
            }
            return null;
        }
        
        public static long GetCountByQuery(Preorder19DianLineVQueryObject queryObject = null)
        {
            return _Preorder19DianLineVManager.GetCountByQuery(queryObject);
        }
    }
}