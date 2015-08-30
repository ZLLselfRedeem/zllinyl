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
    public partial class CouponVOperate
    {
        private static CouponVManager _CouponVManager = new CouponVManager();
        
        public static ICouponV GetEntityById(int CouponId)
        {
            return _CouponVManager.GetEntityById(CouponId); 
        }
        
        public static List<ICouponV> GetListByQuery( int pageSize, int pageIndex,CouponVQueryObject queryObject = null,
            CouponVOrderColumn orderColumn = CouponVOrderColumn.CouponId, SortOrder order = SortOrder.Descending)
        {
            return _CouponVManager.GetListByQuery( pageSize, pageIndex,queryObject, orderColumn, order);
        }
        
        public static List<ICouponV> GetListByQuery(CouponVQueryObject queryObject = null,
            CouponVOrderColumn orderColumn = CouponVOrderColumn.CouponId, SortOrder order = SortOrder.Descending)
        {
            return _CouponVManager.GetListByQuery(queryObject, orderColumn, order);
        }
        
        public static ICouponV GetFirstByQuery(CouponVQueryObject queryObject =null, CouponVOrderColumn orderColumn = CouponVOrderColumn.CouponId, 
            SortOrder order = SortOrder.Descending)            
        {
            var list = _CouponVManager.GetListByQuery( 1, 1,queryObject, orderColumn, order);
            if (list != null)
            {
                return list.FirstOrDefault();
            }
            return null;
        }
        
        public static long GetCountByQuery(CouponVQueryObject queryObject = null)
        {
            return _CouponVManager.GetCountByQuery(queryObject);
        }
    }
}