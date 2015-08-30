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
    public partial class ShopStaticsOperate
    {
        private static ShopStaticsManager _ShopStaticsManager = new ShopStaticsManager();
        
        public static bool Add(IShopStatics Entity)
        {
            return  _ShopStaticsManager.Add(Entity); 
        } 
        
        public static bool Update(IShopStatics Entity)
        {
            return _ShopStaticsManager.Update(Entity); 
        } 
        
        public static bool DeleteEntity(IShopStatics Entity)
        {
            return  _ShopStaticsManager.DeleteEntity(Entity); 
        }
        
        public static IShopStatics GetEntityById(int ShopStaticsId)
        {
            return _ShopStaticsManager.GetEntityById(ShopStaticsId); 
        }
        
        public static List<IShopStatics> GetListByQuery( int pageSize, int pageIndex,ShopStaticsQueryObject queryObject = null,
            ShopStaticsOrderColumn orderColumn = ShopStaticsOrderColumn.ShopStaticsId, SortOrder order = SortOrder.Descending)
        {
            return _ShopStaticsManager.GetListByQuery( pageSize, pageIndex,queryObject, orderColumn, order);
        }
        
        public static IShopStatics GetFirstByQuery(ShopStaticsQueryObject queryObject =null, ShopStaticsOrderColumn orderColumn = ShopStaticsOrderColumn.ShopStaticsId, 
            SortOrder order = SortOrder.Descending)            
        {
            return _ShopStaticsManager.GetListByQuery( 1, 1,queryObject, orderColumn, order).FirstOrDefault();
        }
        
        public static long GetCountByQuery(ShopStaticsQueryObject queryObject)
        {
            return _ShopStaticsManager.GetCountByQuery(queryObject);
        }
    }
}