using System;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public  partial class CouponSendDetailOperate
    {
       private static CouponSendDetailManager _CouponSendDetailManager = new CouponSendDetailManager();
        
        public static bool Add(CouponSendDetail Entity)
        {
            return  _CouponSendDetailManager.Add(Entity); 
        } 
        
        public static bool Update(CouponSendDetail Entity)
        {
            return _CouponSendDetailManager.Update(Entity); 
        } 
        public static bool DeleteEntity(CouponSendDetail Entity)
        {
            return  _CouponSendDetailManager.Add(Entity); 
        }
        public static CouponSendDetail GetEntityById(int couponSendDetailId)
        {
            return _CouponSendDetailManager.GetEntityById(couponSendDetailId); 
        }
        public static List<CouponSendDetail> GetListByQuery( int pageSize, int pageIndex,CouponSendDetailQueryObject queryObject = null,
            CouponSendDetailOrderColumn orderColumn = CouponSendDetailOrderColumn.CouponSendDetailId, SortOrder order = SortOrder.Descending)
        {
            return _CouponSendDetailManager.GetListByQuery( pageSize, pageIndex,queryObject, orderColumn, order);
        }
        public static List<CouponSendDetail> GetListByQuery(CouponSendDetailQueryObject queryObject =null, CouponSendDetailOrderColumn orderColumn = CouponSendDetailOrderColumn.CouponSendDetailId, 
            SortOrder order = SortOrder.Descending)            
        {
            return _CouponSendDetailManager.GetListByQuery(queryObject, orderColumn, order); 
        }
        public static long GetCountByQuery(CouponSendDetailQueryObject queryObject)
        {
            return _CouponSendDetailManager.GetCountByQuery(queryObject);
        }
    
    }
}
