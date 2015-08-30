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
    public partial class QrCodeOperate
    {
        private static QrCodeManager _QrCodeManager = new QrCodeManager();
        
        public static bool Add(IQrCode Entity)
        {
            return  _QrCodeManager.Add(Entity); 
        } 
        
        public static bool Update(IQrCode Entity)
        {
            return _QrCodeManager.Update(Entity); 
        } 
        
        public static bool DeleteEntity(IQrCode Entity)
        {
            return  _QrCodeManager.DeleteEntity(Entity); 
        }
        
        public static IQrCode GetEntityById(int Id)
        {
            return _QrCodeManager.GetEntityById(Id); 
        }
        
        public static List<IQrCode> GetListByQuery( int pageSize, int pageIndex,QrCodeQueryObject queryObject = null,
            QrCodeOrderColumn orderColumn = QrCodeOrderColumn.Id, SortOrder order = SortOrder.Descending)
        {
            return _QrCodeManager.GetListByQuery( pageSize, pageIndex,queryObject, orderColumn, order);
        }
        
        public static List<IQrCode> GetListByQuery(QrCodeQueryObject queryObject = null,
            QrCodeOrderColumn orderColumn = QrCodeOrderColumn.Id, SortOrder order = SortOrder.Descending)
        {
            return _QrCodeManager.GetListByQuery(queryObject, orderColumn, order);
        }
        
        public static IQrCode GetFirstByQuery(QrCodeQueryObject queryObject =null, QrCodeOrderColumn orderColumn = QrCodeOrderColumn.Id, 
            SortOrder order = SortOrder.Descending)            
        {
            return _QrCodeManager.GetListByQuery( 1, 1,queryObject, orderColumn, order).FirstOrDefault();
        }
        
        public static long GetCountByQuery(QrCodeQueryObject queryObject)
        {
            return _QrCodeManager.GetCountByQuery(queryObject);
        }
    }
}