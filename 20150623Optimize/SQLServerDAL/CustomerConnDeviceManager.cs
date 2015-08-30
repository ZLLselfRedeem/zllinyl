using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class CustomerConnDeviceManager
    {
        /// <summary>
        /// 根据设备Id查询当日登陆过的用户
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public List<long> SelectCustomerId(long deviceId)
        {
            string strSql = "select distinct customerId from CustomerConnDevice where deviceId=@deviceId and updateTime>'" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00'";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@deviceId", SqlDbType.BigInt) { Value = deviceId }
            };
            List<long> customerId = new List<long>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    customerId.Add(Convert.ToInt64(sdr["customerId"]));
                }
            }
            return customerId;
        }
    }
}
