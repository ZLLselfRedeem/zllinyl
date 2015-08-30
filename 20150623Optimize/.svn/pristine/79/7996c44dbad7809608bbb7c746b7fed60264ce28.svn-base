using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using System.Data;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class WechatCustomerManager
    {
        /// <summary>
        /// Add WechatCustomerInfo
        /// </summary>
        /// <param name="freeCaseInfo"></param>
        /// <returns></returns>
        public int Insert(WechatCustomerInfo customerInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder sqlStr = new StringBuilder();
                    SqlParameter[] parameters;

                    sqlStr.Append("insert into WechatCustomerInfo values(@subscribe,@openId,@nickName,@sex, ");
                    sqlStr.Append("@language,@city,@province,@country) ");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@subscribe",SqlDbType.NVarChar),
                        new SqlParameter("@openId",SqlDbType.NVarChar),
                        new SqlParameter("@nickName",SqlDbType.NVarChar),
                        new SqlParameter("@sex",SqlDbType.NVarChar),
                        new SqlParameter("@language",SqlDbType.NVarChar),
                        new SqlParameter("@city",SqlDbType.NVarChar),
                        new SqlParameter("@province",SqlDbType.NVarChar),
                        new SqlParameter("@country",SqlDbType.NVarChar)
                    };
                    parameters[0].Value = customerInfo.subscribe;
                    parameters[1].Value = customerInfo.openid;
                    parameters[2].Value = customerInfo.nickname;
                    parameters[3].Value = customerInfo.sex;
                    parameters[4].Value = customerInfo.language;
                    parameters[5].Value = customerInfo.city;
                    parameters[6].Value = customerInfo.province;
                    parameters[7].Value = customerInfo.country;

                    obj = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sqlStr.ToString(), parameters);
                }
                catch
                {
                    return 0;
                }

                return obj;
            }
        }
    }
}
