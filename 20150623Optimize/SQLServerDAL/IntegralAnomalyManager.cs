using System;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class IntegralAnomalyManager
    {
        public int Add(IntegralAnomaly entity)
        {
            string sql = "INSERT INTO [IntegralAnomaly] ([ID],[Address],[parameter],[CreateDate]) VALUES(@ID,@Address,@parameter,GETDATE())";
            var parameters = new SqlParameter[]{
					    new SqlParameter("@ID",entity.Id),
                        new SqlParameter("@Address",entity.Address),
                        new SqlParameter("@parameter",entity.Parameters)
                    };
            return (int)SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, parameters);
        }
    }
}
