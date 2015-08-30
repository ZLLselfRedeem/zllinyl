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
    public class ClientStartImgConfigManager
    {
        public List<ClientStartImgConfig> GetClientStartImgConfigInfos(int type)
        {
            const string strSql = @"SELECT [Id]
      ,[ImgUrl]
      ,[Type]
      ,[Status]
      ,[ScaleType]
      ,[Sequence]
      ,[AppType]
  FROM [VAGastronomistMobileApp].[dbo].[ClientStartImgConfig]
  WHERE [Status]=1 and [Type]=@type";
            SqlParameter[] parameters = { 
                                          new SqlParameter("@type", type)
                                        };
            var list = new List<ClientStartImgConfig>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameters))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<ClientStartImgConfig>(dr));
                }
            }
            return list;
        }
    }
}
