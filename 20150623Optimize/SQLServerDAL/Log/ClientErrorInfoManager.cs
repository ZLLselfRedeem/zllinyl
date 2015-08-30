using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// created by wangc 
    /// 20140512
    /// 客户端发送到服务器错误日志 数据访问层
    /// </summary>
    public class ClientErrorInfoManager
    {
        /// <summary>
        /// 查询客户端错误日志
        /// </summary>
        /// <returns></returns>
        public DataTable SelectClientErrorInfo(string strTime, string endTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [id],[time],[clientBuild],");
            strSql.Append(" case when  [appType]=3 then '安卓' when [appType]=4 then '微信' when [appType]=1 then 'iPhone' when [appType]=2 then 'iPad' end appType,");
            strSql.Append(" case when  [clientType]=1 then '悠先点菜' when [clientType]=2 then '悠先服务' end clientType");
            strSql.Append(" FROM [VAGastronomistMobileAppLog].[dbo].[ClientErrorInfo]");
            strSql.AppendFormat(" where time between '{0}' and '{1}'", strTime, endTime);
            strSql.Append(" order by time desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询错误消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string SelectErrorMessage(long id)
        {
            string result = String.Empty;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [id],[errorMessage]");
            strSql.Append(" FROM [VAGastronomistMobileAppLog].[dbo].[ClientErrorInfo]");
            strSql.AppendFormat(" where id={0}", id);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()))
            {
                if (dr.Read())
                {
                    result = dr["errorMessage"] == DBNull.Value ? "" : Convert.ToString(dr["errorMessage"]);
                }
            }
            return result;
        }
    }
}
