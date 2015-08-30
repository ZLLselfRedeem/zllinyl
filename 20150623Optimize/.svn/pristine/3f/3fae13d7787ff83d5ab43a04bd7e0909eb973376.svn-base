using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
//
//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2012-04-10.
//
namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 客户端出错日志数据库操作类
    /// </summary>
    public class ClientErrorManager
    {
        /// <summary>
        /// 新增客户端出错日志
        /// </summary>
        /// <param name="clientError"></param>
        /// <returns></returns>
        public long InsertClientError(ClientErrorInfo clientError)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.MobileAppLogConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into ClientErrorInfo(");
                    strSql.Append("time,errorMessage,clientBuild,clientType,appType)");
                    strSql.Append(" values (");
                    strSql.Append("@time,@errorMessage,@clientBuild,@clientType,@appType)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					    new SqlParameter("@time", SqlDbType.DateTime),
                        new SqlParameter("@errorMessage", SqlDbType.NVarChar),
                        new SqlParameter("@clientBuild", SqlDbType.NVarChar),
                        new SqlParameter("@clientType",SqlDbType.Int),
                        new SqlParameter("@appType",SqlDbType.Int)
                    };
                    parameters[0].Value = clientError.time;
                    parameters[1].Value = clientError.errorMessage;
                    parameters[2].Value = clientError.clientBuild;
                    parameters[3].Value = (int)clientError.clientType;
                    parameters[4].Value = clientError.appType;

                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return 0;
                }
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
