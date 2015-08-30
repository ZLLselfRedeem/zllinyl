using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class CustomerCheckedShopManager
    {
        /// <summary>
        /// 新增用户查看门店记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long Add(CustomerCheckedShop model)
        {
            const string strSql = @"insert into CustomerCheckedShop(customerId,companyId,shopId,checkTime,cityId) values (
 @customerId,@companyId,@shopId,@checkTime,@cityId);select @@IDENTITY";
            SqlParameter[] parameters = {
					new SqlParameter("@customerId", SqlDbType.BigInt,8),
					new SqlParameter("@companyId", SqlDbType.Int,4),
					new SqlParameter("@shopId", SqlDbType.Int,4),
					new SqlParameter("@checkTime", SqlDbType.DateTime),
					new SqlParameter("@cityId", SqlDbType.Int,4)};
            parameters[0].Value = model.customerId;
            parameters[1].Value = model.companyId;
            parameters[2].Value = model.shopId;
            parameters[3].Value = model.checkTime;
            parameters[4].Value = model.cityId;
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return obj == null ? 0 : Convert.ToInt64(obj);
        }
        /// <summary>
        /// 查询当前用户最新查看的20家门店编号
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public List<int> SelectTop20CheckedShop(long customerId, int cityId)
        {
            const string strSql = @"select distinct top 20 shopId from CustomerCheckedShop where customerId=@customerId and cityId=@cityId order by checkTime desc";
            SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.BigInt),new SqlParameter("@cityId", SqlDbType.Int)
			};
            parameters[0].Value = customerId;
            parameters[1].Value = cityId;
            var list = new List<int>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameters))
            {
                while (dr.Read())
                {
                    list.Add(dr["shopId"] == DBNull.Value ? 0 : Convert.ToInt32(dr["shopId"]));
                }
            }
            return list;
        }
    }
}
