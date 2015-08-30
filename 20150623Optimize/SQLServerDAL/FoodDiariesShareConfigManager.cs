using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Data;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 美食分享客户端webview描述配置项数据访问层
    /// created by wangc
    /// 20140616
    /// </summary>
    public class FoodDiariesShareConfigManager
    {
        /// <summary>
        /// 查询所有美食日记分享配置信息
        /// </summary>
        /// <returns></returns>
        public List<FoodDiariesShareConfig> GetAllFoodDiariesShareConfig()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT [id] ,[foodDiariesShareInfo],[type],[status]");
            strSql.Append(" FROM [VAGastronomistMobileApp].[dbo].[FoodDiariesShareConfig]");
            strSql.Append(" where status=1");
            List<FoodDiariesShareConfig> list = new List<FoodDiariesShareConfig>();
            using (IDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()))
            {
                while (dr.Read())
                {
                    list.Add(new FoodDiariesShareConfig()
                    {
                        id = Convert.ToInt32(dr["id"]),
                        foodDiariesShareInfo = dr["foodDiariesShareInfo"] == DBNull.Value ? "" : Convert.ToString(dr["foodDiariesShareInfo"]),
                        status = dr["status"] == DBNull.Value ? 0 : Convert.ToInt32(dr["status"]),
                        type = dr["type"] == DBNull.Value ? (byte)0 : Convert.ToByte(dr["type"])
                    });
                }
            }
            return list;
        }
        /// <summary>
        /// 新增美食日记分享配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool InsertFoodDiariesShareConfig(FoodDiariesShareConfig model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into FoodDiariesShareConfig(");
            strSql.Append("foodDiariesShareInfo,type,status)");
            strSql.Append(" values (");
            strSql.Append("@foodDiariesShareInfo,@type,@status)");
            SqlParameter[] parameters = {
					new SqlParameter("@foodDiariesShareInfo", SqlDbType.NVarChar,500),
					new SqlParameter("@type", SqlDbType.TinyInt,1),
					new SqlParameter("@status", SqlDbType.Int,4)};
            parameters[0].Value = model.foodDiariesShareInfo;
            parameters[1].Value = model.type;
            parameters[2].Value = model.status;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新美食日记分享配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateFoodDiariesShareConfig(FoodDiariesShareConfig model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FoodDiariesShareConfig set ");
            strSql.Append("foodDiariesShareInfo=@foodDiariesShareInfo,");
            strSql.Append("type=@type,");
            strSql.Append("status=@status");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@foodDiariesShareInfo", SqlDbType.NVarChar,500),
					new SqlParameter("@type", SqlDbType.TinyInt,1),
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.foodDiariesShareInfo;
            parameters[1].Value = model.type;
            parameters[2].Value = model.status;
            parameters[3].Value = model.id;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 删除美食日记分享配置信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteFoodDiariesShareConfig(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update FoodDiariesShareConfig set ");
            strSql.Append("status=@status");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@status", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = -1;
            parameters[1].Value = id;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
