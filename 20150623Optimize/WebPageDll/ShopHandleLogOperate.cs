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
    public partial class ShopHandleLogOperate
    {
        private static ShopHandleLogManager _ShopHandleLogManager = new ShopHandleLogManager();
        
        public static bool Add(IShopHandleLog Entity)
        {
            return  _ShopHandleLogManager.Add(Entity); 
        } 
        
        public static bool Update(IShopHandleLog Entity)
        {
            return _ShopHandleLogManager.Update(Entity); 
        } 
        
        public static bool DeleteEntity(IShopHandleLog Entity)
        {
            return  _ShopHandleLogManager.DeleteEntity(Entity); 
        }
        
        public static IShopHandleLog GetEntityById(long Id)
        {
            return _ShopHandleLogManager.GetEntityById(Id); 
        }
        
        public static List<IShopHandleLog> GetListByQuery( int pageSize, int pageIndex,ShopHandleLogQueryObject queryObject = null,
            ShopHandleLogOrderColumn orderColumn = ShopHandleLogOrderColumn.Id, SortOrder order = SortOrder.Descending)
        {
            return _ShopHandleLogManager.GetListByQuery( pageSize, pageIndex,queryObject, orderColumn, order);
        }
        
        public static List<IShopHandleLog> GetListByQuery(ShopHandleLogQueryObject queryObject = null,
            ShopHandleLogOrderColumn orderColumn = ShopHandleLogOrderColumn.Id, SortOrder order = SortOrder.Descending)
        {
            return _ShopHandleLogManager.GetListByQuery(queryObject, orderColumn, order);
        }
        
        public static IShopHandleLog GetFirstByQuery(ShopHandleLogQueryObject queryObject =null, ShopHandleLogOrderColumn orderColumn = ShopHandleLogOrderColumn.Id, 
            SortOrder order = SortOrder.Descending)            
        {
            return _ShopHandleLogManager.GetListByQuery( 1, 1,queryObject, orderColumn, order).FirstOrDefault();
        }
        
        public static long GetCountByQuery(ShopHandleLogQueryObject queryObject)
        {
            return _ShopHandleLogManager.GetCountByQuery(queryObject);
        }
    }
}