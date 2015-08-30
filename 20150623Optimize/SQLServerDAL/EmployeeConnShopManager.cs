using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class EmployeeConnShopManager
    {
        /// <summary>
        /// 获取员工在门店的配置
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public EmployeeConnShop GetEmployeeInShopConn(int shopId, int employeeId)
        {
            StringBuilder strSqlBuilder = new StringBuilder(@"SELECT * FROM [dbo].[EmployeeConnShop] where employeeID=@employeeID and shopID=@shopID and status=1");

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@employeeID", employeeId),
                new SqlParameter("@shopID", shopId) 
            };

            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSqlBuilder.ToString(), cmdParameters))
            {
                EmployeeConnShop employeeConnShop = null;
                if (dr.Read())
                {
                    employeeConnShop = new EmployeeConnShop()
                    {
                        companyID = Convert.ToInt32(dr["companyID"]),
                        employeeID = Convert.ToInt32(dr["employeeID"]),
                        employeeShopID = Convert.ToInt32(dr["employeeShopID"]),
                        status = Convert.ToInt32(dr["status"]),
                        serviceEndTime = dr["serviceEndTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["serviceEndTime"]),
                        serviceStartTime = dr["serviceStartTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(dr["serviceStartTime"]),
                        shopID = Convert.ToInt32(dr["shopID"])
                        //isSupportEnterSyb = Convert.ToBoolean(dr["isSupportEnterSyb"]),
                        //isSupportReceiveMsg = Convert.ToBoolean(dr["isSupportReceiveMsg"])
                    };
                }

                return employeeConnShop;
            }
        }
    }
}
