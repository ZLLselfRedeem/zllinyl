using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerShopVIPSpeedConfigRepository : SqlServerRepositoryBase, IShopVIPSpeedConfigRepository
    {
        public SqlServerShopVIPSpeedConfigRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public ShopVIPSpeedConfig GetShopVipSpeedConfigByCityAndHour(int cityId, int hour)
        {
            const string cmdText = @"SELECT * FROM [dbo].[ShopVIPSpeedConfig] WHERE [City]=@cityId AND [StartHour]<=@hour AND [EndHour]>@hour";
            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@hour", SqlDbType.SmallInt) { Value = hour },
                new SqlParameter("@cityId",SqlDbType.Int){Value = cityId}
            };

            ShopVIPSpeedConfig config = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                if (dr.Read())
                {
                    config = SqlHelper.GetEntity<ShopVIPSpeedConfig>(dr);
                }
            }
            return config;
        }

        public int[] GetCityForShopVipSpeedConfig()
        {
            const string cmdText = @"SELECT DISTINCT [City] FROM [dbo].[ShopVIPSpeedConfig] ";
            List<int> cityIds = new List<int>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText))
            {
                while (dr.Read())
                {
                    cityIds.Add(dr.GetInt32(0));
                }
            }
            return cityIds.ToArray();
        }

        public int InsertShopVipSpeedConfig(ShopVIPSpeedConfig config)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ShopVIPSpeedConfig(City,StartHour,EndHour,PreUnit,Unit,MinSpeed,MaxSpeed)");
            strSql.Append(" values (@City,@StartHour,@EndHour,@PreUnit,@Unit,@MinSpeed,@MaxSpeed)");
            strSql.Append(" select @@identity");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@City", SqlDbType.Int),
                new SqlParameter("@StartHour", SqlDbType.SmallInt),
                new SqlParameter("@EndHour", SqlDbType.SmallInt),
                new SqlParameter("@PreUnit", SqlDbType.Int),
                new SqlParameter("@Unit", SqlDbType.SmallInt),
                new SqlParameter("@MinSpeed", SqlDbType.Int),
                new SqlParameter("@MaxSpeed", SqlDbType.Int),
                };
                para[0].Value = config.City;
                para[1].Value = config.StartHour;
                para[2].Value = config.EndHour;
                para[3].Value = config.PreUnit;
                para[4].Value = config.Unit;
                para[5].Value = config.MinSpeed;
                para[6].Value = config.MaxSpeed;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
                    if (obj == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return Convert.ToInt32(obj);
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public bool DeleteShopVipSpeedConfig(int cityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from ShopVIPSpeedConfig");
            strSql.Append(" where City=@City");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@City", SqlDbType.Int)
                };
                para[0].Value = cityId;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                    if (i > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 查询所有/指定城市对应的增速因子展示给客户
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable GetCityVipSpeed(int cityId = 0)
        {   
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select City.cityID,City.cityName,SUM(MinSpeed) MinSpeed,SUM(MaxSpeed) MaxSpeed");
            strSql.Append(" from ShopVIPSpeedConfig vip");
            strSql.Append(" inner join city on City.cityID = vip.City");
            if (cityId > 0)
            {
                strSql.Append(" where City=@City");
            }
            strSql.Append(" group by City.cityID,City.cityName");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@City", SqlDbType.Int)
                };
                para[0].Value = cityId;

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
