using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class ShopNoticeManager
    {
        /// <summary>
        /// 返回商家对应的活动版本号
        /// </summary>
        public DateTime GetShopAwardVersion(int shopID)
        {
            const string strSql = @"select top 1 CreateTime from ShopAwardVersion where ShopId=@shopID order by CreateTime desc ";
            SqlParameter parameter = new SqlParameter("@shopID", shopID);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                try
                {
                    if (dr.Read())
                    {
                        return Convert.ToDateTime(dr[0]);
                    }
                }
                catch
                {
                    return Convert.ToDateTime("1970-01-02");
                }
            }
            return Convert.ToDateTime("1970-01-02");
        }

        /// <summary>
        /// 查询VAGastronomistMobileApp表里面对应的创建时间
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public DateTime GetShopNoticeVersion(int shopID)
        {
            const string strSql = @"select top 1 CreateTime from ShopAwardVersion where ShopId=@shopID order by CreateTime desc ";
            SqlParameter parameter = new SqlParameter("@shopID", shopID);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                try
                {
                    if (dr.Read())
                    {
                        return Convert.ToDateTime(dr[0]);
                    }
                }
                catch
                {
                    return Convert.ToDateTime("1970-01-02");
                }
            }
            return Convert.ToDateTime("1970-01-02");
        }

        /// <summary>
        /// 返回强制弹出时间戳
        /// </summary>
        public string GetAwardResetTimeValue()
        {
            const string strSql = @"select top 1 changeTime from AwardResetConfig order by changeTime desc";

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, null))
            {
                try
                {
                    if (dr.Read())
                    {
                        return Convert.ToString(dr[0]);
                    }
                }
                catch
                {
                    return "1970-01-02";
                }
            }
            return "1970-01-02";
        }
    }
}
