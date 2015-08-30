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
    public class EmployeeShopAuthorityManager
    {
        public void Insert(EmployeeShopAuthority employeeShopAuthority)
        {
            StringBuilder strSqlBuilder = new StringBuilder(@"INSERT INTO [dbo].[EmployeeShopAuthority]
           ([employeeConnShopId]
           ,[shopAuthorityId]
           ,[employeeShopAuthorityStatus])
     VALUES
           (@employeeConnShopId,@shopAuthorityId,@employeeShopAuthorityStatus)");

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@employeeConnShopId", employeeShopAuthority.employeeConnShopId), 
                new SqlParameter("@shopAuthorityId", employeeShopAuthority.shopAuthorityId), 
                new SqlParameter("@employeeShopAuthorityStatus", employeeShopAuthority.employeeShopAuthorityStatus), 
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text,
                strSqlBuilder.ToString(), cmdParameters);
        }

        public EmployeeShopAuthority GetEmployeeShopAuthority(int emploryeeShopId, int shopAuthorityId)
        {
            StringBuilder strSqlBuilder = new StringBuilder(@"SELECT *  FROM [dbo].[EmployeeShopAuthority] where employeeConnShopId=@employeeConnShopId and shopAuthorityId=@shopAuthorityId and employeeShopAuthorityStatus=1");

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@employeeConnShopId", emploryeeShopId), new SqlParameter("@shopAuthorityId", shopAuthorityId), 
            };

            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSqlBuilder.ToString(), cmdParameters))
            {
                EmployeeShopAuthority employeeShopAuthority = null;
                if (dr.Read())
                {
                    employeeShopAuthority = new EmployeeShopAuthority()
                    {
                        employeeConnShopId = Convert.ToInt32(dr["employeeConnShopId"]),
                        employeeShopAuthorityId = Convert.ToInt32(dr["employeeShopAuthorityId"]),
                        employeeShopAuthorityStatus = Convert.ToInt32(dr["employeeShopAuthorityStatus"]),
                        shopAuthorityId = Convert.ToInt32(dr["shopAuthorityId"])
                    };
                }
                return employeeShopAuthority;
            }
        }

        public void Delete(int emploryeeShopId, int shopAuthorityId)
        {
            StringBuilder strSqlBuilder = new StringBuilder(@"UPDATE [dbo].[EmployeeShopAuthority] 
   SET [employeeShopAuthorityStatus] = -1
 WHERE employeeConnShopId=@employeeConnShopId and shopAuthorityId=@shopAuthorityId and employeeShopAuthorityStatus=1");

            SqlParameter[] cmdParameters = new SqlParameter[]
            {
                new SqlParameter("@employeeConnShopId", emploryeeShopId), new SqlParameter("@shopAuthorityId", shopAuthorityId), 
            };

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text,
                strSqlBuilder.ToString(), cmdParameters);
        }
    }
}
