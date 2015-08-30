using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace VAGastronomistMobileApp.SQLServerDAL
{
   public class DeviceManager
    {
       /// <summary>
       /// 根据UUID查询设备号
       /// </summary>
       /// <param name="uuid"></param>
       /// <returns></returns>
       public long SelectDeviceIdByUUID(string uuid)
       {
           const string strSql = "select deviceId from DeviceInfo where uuid=@uuid";
           SqlParameter[] para = new SqlParameter[] { 
           new SqlParameter("@uuid", SqlDbType.NVarChar, 100) { Value = uuid }
           };
           using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
           {
               object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
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
