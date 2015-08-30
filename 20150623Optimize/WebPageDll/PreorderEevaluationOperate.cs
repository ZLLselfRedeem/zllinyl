using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAGastronomistMobileApp.Model; 
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.SQLServerDAL;

namespace VAGastronomistMobileApp.WebPageDll
{
    public partial class PreorderEvaluationOperate
    {
        private static PreorderEvaluationManager _PreorderEvaluationManager = new PreorderEvaluationManager();
        
        public static bool Add(PreorderEvaluation Entity)
        {
            return  _PreorderEvaluationManager.Add(Entity); 
        } 
        
        public static bool Update(PreorderEvaluation Entity)
        {
            return _PreorderEvaluationManager.Update(Entity); 
        } 
        
        public static bool DeleteEntity(PreorderEvaluation Entity)
        {
            return  _PreorderEvaluationManager.DeleteEntity(Entity); 
        }
        
        public static PreorderEvaluation GetEntityById(long PreorderEvaluationId)
        {
            return _PreorderEvaluationManager.GetEntityById(PreorderEvaluationId); 
        }
        
        public static List<PreorderEvaluation> GetListByQuery( int pageSize, int pageIndex,PreorderEvaluationQueryObject queryObject = null,
            PreorderEvaluationOrderColumn orderColumn = PreorderEvaluationOrderColumn.PreorderEvaluationId, SortOrder order = SortOrder.Descending)
        {
            return _PreorderEvaluationManager.GetListByQuery( pageSize, pageIndex,queryObject, orderColumn, order);
        }
        
        public static PreorderEvaluation GetFirstByQuery(PreorderEvaluationQueryObject queryObject =null, PreorderEvaluationOrderColumn orderColumn = PreorderEvaluationOrderColumn.PreorderEvaluationId, 
            SortOrder order = SortOrder.Descending)            
        {
            return _PreorderEvaluationManager.GetListByQuery( 1, 1,queryObject, orderColumn, order).FirstOrDefault();
        }
        
        public static long GetCountByQuery(PreorderEvaluationQueryObject queryObject)
        {
            return _PreorderEvaluationManager.GetCountByQuery(queryObject);
        }
    }
}