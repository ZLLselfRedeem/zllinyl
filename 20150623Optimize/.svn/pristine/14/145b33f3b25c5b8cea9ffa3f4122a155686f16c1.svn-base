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
    public partial class ShopStopPaymentLogOperate
    {
        private static ShopStopPaymentLogManager _ShopStopPaymentLogManager = new ShopStopPaymentLogManager();
        
        public static bool Add(IShopStopPaymentLog Entity)
        {
            return  _ShopStopPaymentLogManager.Add(Entity); 
        }
        
        
        
        public static bool Update(IShopStopPaymentLog Entity)
        {
            return _ShopStopPaymentLogManager.Update(Entity); 
        }
         
        
        public static bool DeleteEntity(IShopStopPaymentLog Entity)
        {
            return  _ShopStopPaymentLogManager.DeleteEntity(Entity); 
        }
        
        public static IShopStopPaymentLog GetEntityById(int ShopStopPaymentLogId)
        {
            return _ShopStopPaymentLogManager.GetEntityById(ShopStopPaymentLogId); 
        }
        
        public static List<IShopStopPaymentLog> GetListByQuery( int pageSize, int pageIndex,ShopStopPaymentLogQueryObject queryObject = null,
            ShopStopPaymentLogOrderColumn orderColumn = ShopStopPaymentLogOrderColumn.ShopStopPaymentLogId, SortOrder order = SortOrder.Descending)
        {
            return _ShopStopPaymentLogManager.GetListByQuery( pageSize, pageIndex,queryObject, orderColumn, order);
        }
        
        public static List<IShopStopPaymentLog> GetListByQuery( ShopStopPaymentLogQueryObject queryObject = null,
            ShopStopPaymentLogOrderColumn orderColumn = ShopStopPaymentLogOrderColumn.ShopStopPaymentLogId, SortOrder order = SortOrder.Descending)
        {
            return _ShopStopPaymentLogManager.GetListByQuery( queryObject, orderColumn, order);
        }
        
        public static IShopStopPaymentLog GetFirstByQuery(ShopStopPaymentLogQueryObject queryObject =null, ShopStopPaymentLogOrderColumn orderColumn = ShopStopPaymentLogOrderColumn.ShopStopPaymentLogId, 
            SortOrder order = SortOrder.Descending)            
        {
            return _ShopStopPaymentLogManager.GetListByQuery( 1, 1,queryObject, orderColumn, order).FirstOrDefault();
        }
        
        public static long GetCountByQuery(ShopStopPaymentLogQueryObject queryObject)
        {
            return _ShopStopPaymentLogManager.GetCountByQuery(queryObject);
        }
    }
}