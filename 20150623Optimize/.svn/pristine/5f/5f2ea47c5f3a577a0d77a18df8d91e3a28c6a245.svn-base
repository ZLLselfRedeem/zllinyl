using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 日志数据库操作类
    /// </summary>
    public class OrderLogManager
    {
        /// <summary>
        /// 新增日志信息
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public int InsertLog(LogInfo log)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                //SqlTransaction tran = null;
                Object obj = null;
                try
                {
                    conn.Open();
                    //tran = conn.BeginTransaction();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into LogInfo(");
                    strSql.Append("LogTime,LogContent)");
                    strSql.Append(" values (");
                    strSql.Append("@LogTime,@LogContent)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					new SqlParameter("@LogTime", SqlDbType.DateTime),
                    new SqlParameter("@LogContent", SqlDbType.NVarChar)};
                    parameters[0].Value = log.LogTime;
                    parameters[1].Value = log.LogContent;

                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);

                    //tran.Commit();
                }
                catch (Exception)
                {
                    //if (tran != null)
                    //{
                    //    tran.Rollback();
                    //}
                    return 0;
                }
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
}
