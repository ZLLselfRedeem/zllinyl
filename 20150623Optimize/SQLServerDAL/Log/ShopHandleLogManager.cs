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
    public partial class ShopHandleLogManager
    {
        /// <summary>
        /// 新增审核日志记录
        /// </summary>
        /// <param name="shopHandleLog"></param>
        /// <returns></returns>
        public long InsertShopHandleLog(ShopHandleLog shopHandleLog)
        {
            const string strSql = @"insert into [VAGastronomistMobileAppLog].[dbo].[ShopHandleLog](
[ShopName]
      ,[ShopId]
      ,[EmployeeId]
      ,[EmployeeName]
      ,[HandleStatus]
      ,[HandleDesc]
      ,[OperateTime]
      ,[CityId])  values (@ShopName
      ,@ShopId
      ,@EmployeeId
      ,@EmployeeName
      ,@HandleStatus
      ,@HandleDesc
      ,@OperateTime
      ,@CityId);select @@identity";
            SqlParameter[] parameters = new SqlParameter[]{
					    new SqlParameter("@ShopName", SqlDbType.NVarChar,500),
                        new SqlParameter("@ShopId", SqlDbType.Int,4),
                        new SqlParameter("@EmployeeId", SqlDbType.Int,4),
                        new SqlParameter("@EmployeeName",SqlDbType.NVarChar,500),
                        new SqlParameter("@HandleStatus", SqlDbType.Int,4),
                        new SqlParameter("@OperateTime",SqlDbType.DateTime),
                        new SqlParameter("@HandleDesc",SqlDbType.NVarChar,500),
                        new SqlParameter("@CityId",SqlDbType.Int,4),
                    };
            parameters[0].Value = shopHandleLog.ShopName;
            parameters[1].Value = shopHandleLog.ShopId;
            parameters[2].Value = shopHandleLog.EmployeeId;
            parameters[3].Value = shopHandleLog.EmployeeName;
            parameters[4].Value = shopHandleLog.HandleStatus;
            parameters[5].Value = shopHandleLog.OperateTime;
            parameters[6].Value = shopHandleLog.ToString();
            parameters[7].Value = shopHandleLog.CityId;
            using (SqlConnection conn = new SqlConnection(SqlHelper.MobileAppLogConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, parameters);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt64(obj);
                }
            }
        }
    }
}
