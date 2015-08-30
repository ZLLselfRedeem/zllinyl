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
    public partial class CouponGetDetailVOperate
    {
        private static CouponGetDetailVManager _CouponGetDetailVManager = new CouponGetDetailVManager();
        
        public static ICouponGetDetailV GetEntityById(int CouponGetDetailId)
        {
            return _CouponGetDetailVManager.GetEntityById(CouponGetDetailId); 
        }
        
        public static List<ICouponGetDetailV> GetListByQuery( int pageSize, int pageIndex,CouponGetDetailVQueryObject queryObject = null,
            CouponGetDetailVOrderColumn orderColumn = CouponGetDetailVOrderColumn.CouponGetDetailId, SortOrder order = SortOrder.Descending)
        {
            return _CouponGetDetailVManager.GetListByQuery( pageSize, pageIndex,queryObject, orderColumn, order);
        }
        
        public static List<ICouponGetDetailV> GetListByQuery(CouponGetDetailVQueryObject queryObject = null,
            CouponGetDetailVOrderColumn orderColumn = CouponGetDetailVOrderColumn.CouponGetDetailId, SortOrder order = SortOrder.Descending)
        {
            return _CouponGetDetailVManager.GetListByQuery(queryObject, orderColumn, order);
        }

        public static ICouponGetDetailV GetFirstByQuery(CouponGetDetailVQueryObject queryObject = null, CouponGetDetailVOrderColumn orderColumn = CouponGetDetailVOrderColumn.CouponGetDetailId, 
            SortOrder order = SortOrder.Descending)            
        {
            var list = _CouponGetDetailVManager.GetListByQuery( 1, 1,queryObject, orderColumn, order);
            if (list != null)
            {
                return list.FirstOrDefault();
            }
            return null;
        }
        
        public static long GetCountByQuery(CouponGetDetailVQueryObject queryObject = null)
        {
            return _CouponGetDetailVManager.GetCountByQuery(queryObject);
        }

        /// <summary>
        /// 获取指定店铺抵扣卷列表
        /// </summary>
        /// <param name="shopid">店铺ID</param>
        /// <returns>返回指定店铺抵扣卷列表</returns>
        public static List<CouponGetDetailV> GetOrderPaymentCouponDetail(int shopid)
        {
            return _CouponGetDetailVManager.GetOrderPaymentCouponDetail(shopid);
        }
    }
}