using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class ClientOrderDetailConfigManager
    {
        public List<ClientOrderDetailConfig> GetClientOrderDetailConfig()
        {
            const string strSql = @"SELECT [Id]
      ,[Description]
      ,[Type]
      ,[Status]
  FROM [VAGastronomistMobileApp].[dbo].[ClientOrderDetailConfig]
  WHERE [Status]=1";
            var list = new List<ClientOrderDetailConfig>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql))
            {
                while (dr.Read())
                {
                    list.Add(new ClientOrderDetailConfig()
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Description = dr["Description"] == DBNull.Value ? "" : Convert.ToString(dr["Description"]),
                        Type = dr["Type"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Type"]),
                        Status = dr["Status"] == DBNull.Value ? 0 : Convert.ToInt32(dr["Status"])
                    });
                }
            }
            return list;
        }
    }
}
