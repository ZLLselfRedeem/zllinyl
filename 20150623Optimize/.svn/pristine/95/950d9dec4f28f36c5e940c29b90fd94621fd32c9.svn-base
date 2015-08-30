using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class ShopEvaluationDetailManager
    {
        public ShopEvaluationDetail GetShopEvaluationDetailByID(int shopEvaluationDetailID)
        {
            string selectSQL = "SELECT ShopEvaluationDetailId, EvaluationValue  ,  EvaluationCount ,  ShopId      FROM ShopEvaluationDetail"
                + " WHERE ShopEvaluationDetailId = @ShopEvaluationDetailId";
            SqlParameter[] parameters = {
                    new SqlParameter("@ShopEvaluationDetailId", shopEvaluationDetailID)};
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, selectSQL,parameters);
            if(ds.Tables.Count >0 && ds.Tables[0].Rows.Count>0)
            {
                ShopEvaluationDetail shopEvaluationDetail = new ShopEvaluationDetail();
                shopEvaluationDetail.ShopEvaluationDetailId = Convert.ToInt32(ds.Tables[0].Rows[0]["ShopEvaluationDetailId"]);
                shopEvaluationDetail.EvaluationValue = Convert.ToInt32(ds.Tables[0].Rows[0]["EvaluationValue"]);
                shopEvaluationDetail.EvaluationCount = Convert.ToInt32(ds.Tables[0].Rows[0]["EvaluationCount"]);
                shopEvaluationDetail.ShopId = Convert.ToInt32(ds.Tables[0].Rows[0]["ShopId"]);
            }
            return null;
        }

        public List<ShopEvaluationDetail> GetShopEvaluationDetailByQuery(ShopEvaluationDetailQueryObject queryObject)
        {
            StringBuilder selectSQL = new StringBuilder("SELECT ShopEvaluationDetailId, EvaluationValue  ,  EvaluationCount ,  ShopId      FROM ShopEvaluationDetail"
                + " WHERE  1= 1");
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (queryObject.EvaluationValue.HasValue)
            {
                selectSQL.Append(" AND EvaluationValue = @EvaluationValue");
                parameters.Add(new SqlParameter("@EvaluationValue", queryObject.EvaluationValue.Value));
            }
            if (queryObject.ShopId.HasValue)
            {
                selectSQL.Append(" AND ShopId = @ShopId");
                parameters.Add(new SqlParameter("@ShopId", queryObject.ShopId.Value));
            }

            List<ShopEvaluationDetail> list = new List<ShopEvaluationDetail>();
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, selectSQL.ToString(), parameters.ToArray()))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<ShopEvaluationDetail>());
                }
            }
            return list;
        }

        public bool AddShopEvaluationDetail(ShopEvaluationDetail shopEvaluationDetail)
        {
            string selectSQL = "INSERT INTO [ShopEvaluationDetail] "
                              +" ([EvaluationValue] "
                              +"  ,[EvaluationCount] "
                              +"  ,[ShopId]) "
                              +"  VALUES "
                              +"   (@EvaluationValue ,"
                              + "  @EvaluationCount,  "
                              +"   @ShopId)" 
                              +"  SELECT @@identity";
            SqlParameter[] parameters = {
                    new SqlParameter("@EvaluationValue", shopEvaluationDetail.EvaluationValue),
                    new SqlParameter("@EvaluationCount", shopEvaluationDetail.EvaluationCount),
                   new SqlParameter("@ShopId", shopEvaluationDetail.ShopId)};
            object result = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, selectSQL, parameters);
            int shopEvaluationDetailId = Convert.ToInt32(result);
            if (shopEvaluationDetailId > 0)
            {
                shopEvaluationDetail.ShopEvaluationDetailId = shopEvaluationDetailId;
                return true;
            } 
            return false;
        }
        public bool UpdateShopEvaluationDetail(ShopEvaluationDetail shopEvaluationDetail)
        {
            string selectSQL = "UPDATE  [ShopEvaluationDetail] "
                             + " SET [EvaluationValue] = @EvaluationValue"
                             + " ,[EvaluationCount] = @EvaluationCount"
                             + "  ,[ShopId] = @ShopId "
                             + " WHERE  ShopEvaluationDetailId = @ShopEvaluationDetailId";
            SqlParameter[] parameters = {
                     new SqlParameter("@EvaluationValue", shopEvaluationDetail.EvaluationValue),
                     new SqlParameter("@EvaluationCount", shopEvaluationDetail.EvaluationCount),
                     new SqlParameter("@ShopId", shopEvaluationDetail.ShopId),
                     new SqlParameter("@ShopEvaluationDetailId", shopEvaluationDetail.ShopEvaluationDetailId)    
                                        };
            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, selectSQL, parameters);
            if (result > 0)
            {
                shopEvaluationDetail.ShopEvaluationDetailId = result;
                return true;
            }
            return false;
        }
    }
       
}
