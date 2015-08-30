using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 微信支付--AccessToken
    /// </summary>
    public class WechatAccessTokenManager
    {
        public long InsertWechatAccessToken(string accessToken, DateTime createTime, DateTime expireTime)
        {
            object obj = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into WechatAccessToken(");
                strSql.Append("accessToken,createtime,expireTime)");
                strSql.Append(" values(");
                strSql.Append("@accessToken,@createtime,@expireTime)");
                strSql.Append(" select @@identity");

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@accessToken", SqlDbType.NVarChar, 500),
                    new SqlParameter("@createtime", SqlDbType.DateTime),
                    new SqlParameter("@expireTime", SqlDbType.DateTime)
                };
                para[0].Value = accessToken;
                para[1].Value = createTime;
                para[2].Value = expireTime;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
                }
            }
            catch (Exception)
            {
                return 0;
            }
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt64(obj);
            }
        }

        /// <summary>
        /// 将最新获取的AccessToken更新至DB
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="createTime"></param>
        /// <param name="expireTime"></param>
        /// <returns></returns>
        public bool UpdateWechatAccessToken(string accessToken, DateTime createTime, DateTime expireTime, VAAccessTokenType accessTokenType)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update WechatAccessToken");
                strSql.Append(" set accessToken = @accessToken,");
                strSql.Append(" createtime=@createtime,");
                strSql.Append(" expireTime=@expireTime");
                strSql.Append(" where id = " + (int)accessTokenType);

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@accessToken", SqlDbType.NVarChar, 500),
                    new SqlParameter("@createtime", SqlDbType.DateTime),
                    new SqlParameter("@expireTime", SqlDbType.DateTime)
                };
                para[0].Value = accessToken;
                para[1].Value = createTime;
                para[2].Value = expireTime;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
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
        /// 获取数据库中最新的AccessToken
        /// DB中只存一条数据，valid标识是否要从DB中读取未过期的AccessToken
        /// </summary>
        /// <returns></returns>
        public DataTable QueryNewestAccessToken(VAAccessTokenType accessTokenType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select accessToken,createtime,expireTime");
            strSql.Append(" from WechatAccessToken");
            strSql.Append(" where valid = 1 and id = " + (int)accessTokenType);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
    }
}
