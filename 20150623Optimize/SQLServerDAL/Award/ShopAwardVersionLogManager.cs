using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class ShopAwardVersionLogManager
    {
        /// <summary>
        /// 获取门店奖品变更信息
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public DataTable SelectAwardVersionLog(int shopID)
        {
            const string strSql = @"select 
	                                    CreateTime as changeTime,
	                                    Content as changeContent,
	                                    ChangeSource as changeSource,
	                                    CreateBy as mobilePhone
                                     from dbo.ShopAwardVersionLog  where ShopId=@shopID  order by CreateTime desc ";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@shopID", SqlDbType.Int) { Value = shopID }
            };
            DataTable dt = new DataTable();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            dt = ds.Tables[0];
            return dt; ;
        }

        /// <summary>
        /// 添加门店奖品变更记录
        /// </summary>
        /// <param name="shopAwardVersionLog"></param>
        /// <returns></returns>
        public bool InsertShopAwardVersionLog(ShopAwardVersionLog shopAwardVersionLog)
        {
            const string strSql = @"INSERT INTO [VAAward].[dbo].[ShopAwardVersionLog]
                                           ([Id]
                                           ,[ShopAwardId]
                                           ,[ShopAwardVersionId]
                                           ,[ShopId]
                                           ,[Content]
                                           ,[ChangeSource]
                                           ,[CreateTime]
                                           ,[CreateBy])
                                     VALUES(@Id,@ShopAwardId,@ShopAwardVersionId,@ShopId,@Content,@ChangeSource,@CreateTime,@CreateBy)";

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@Id", SqlDbType.UniqueIdentifier){ Value = shopAwardVersionLog.Id },
            new SqlParameter("@ShopAwardId", SqlDbType.UniqueIdentifier){ Value = shopAwardVersionLog.ShopAwardId },
            new SqlParameter("@ShopAwardVersionId", SqlDbType.Int){ Value = shopAwardVersionLog.ShopAwardVersionId },
            new SqlParameter("@ShopId", SqlDbType.Int){ Value = shopAwardVersionLog.ShopId },
            new SqlParameter("@Content", SqlDbType.NVarChar,300){ Value = shopAwardVersionLog.Content },
            new SqlParameter("@ChangeSource", SqlDbType.NVarChar,100){ Value = shopAwardVersionLog.ChangeSource },
            new SqlParameter("@CreateTime", SqlDbType.DateTime){ Value = shopAwardVersionLog.CreateTime },
            new SqlParameter("@CreateBy", SqlDbType.NVarChar,50){ Value = shopAwardVersionLog.CreateBy }
            };

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
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

    }
}
