﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class ShopAwardVersionManager
    {
        /// <summary>
        /// 获取门店奖品变更信息
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public DataTable SelectAwardVersion(int shopID)
        {
            const string strSql = @"select *
                                     from dbo.ShopAwardVersion  where ShopId=@shopID  order by CreateTime desc ";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@shopID", SqlDbType.Int) { Value = shopID }
            };
            DataTable dt = new DataTable();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            dt = ds.Tables[0];
            return dt; ;
        }

        /// <summary>
        /// 添加门店奖品变更版本记录
        /// </summary>
        /// <param name="ShopAwardVersion"></param>
        /// <returns></returns>
        public int InsertShopAwardVersion(ShopAwardVersion shopAwardVersion)
        {
            const string strSql = @"INSERT INTO [ShopAwardVersion]
                                   ([ShopId]
                                   ,[CreateTime]
                                   ,[CreatedBy])
                                     VALUES(@ShopId,@CreateTime,@CreateBy)  select @@identity";

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@ShopId", SqlDbType.Int){ Value = shopAwardVersion.ShopId },
            new SqlParameter("@CreateTime", SqlDbType.DateTime){ Value = shopAwardVersion.CreateTime },
            new SqlParameter("@CreateBy", SqlDbType.NVarChar,50){ Value = shopAwardVersion.CreateBy }
            };
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
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
}
