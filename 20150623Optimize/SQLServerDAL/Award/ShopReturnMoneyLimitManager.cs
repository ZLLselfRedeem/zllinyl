﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class ShopReturnMoneyLimitManager
    {
        /// <summary>
        /// 查询某家门店返现限制
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public ShopReturnMoneyLimit SelectShopReturnMoneyLimit(int shopId)
        {
            const string strSql = "select * from ShopReturnMoneyLimit where shopId = @shopId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@shopId", SqlDbType.Int) { Value = shopId }
            };

            using(SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction,CommandType.Text,strSql,para))
            {
                if (sdr.Read())
                {
                    return SqlHelper.GetEntity<ShopReturnMoneyLimit>(sdr);
                }
                else
                {
                    return null; 
                }
            }
        }
    }
}
