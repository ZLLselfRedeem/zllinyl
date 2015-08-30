using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using System.Data.SqlClient;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class WechatFreeCaseManager
    {
        /// <summary>
        /// 获取所有 本期免单信息 (包括历史数据)
        /// </summary>
        /// <returns></returns>
        public DataTable GetFreeCaseInfo()
        {
            string sqlStr = "select * from WechatFreeCaseInfo order by pubDateTime desc";

            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);
            return dt.Tables[0];
        }

        //获取top1
        public string GetTopOneFreeCaseInfo()
        {
            string sqlStr = "select top 1 msgContent from WechatFreeCaseInfo where status='未过期' order by pubDateTime desc";

            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);
            return obj.ToString();
        }

        /// <summary>
        /// 获取所有 本期免单信息 (包括历史数据)  发布日期时间(str起始时间   end截止时间)
        /// </summary>
        /// <returns></returns>
        public DataTable GetFreeCaseInfo(string str, string end)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select * from WechatFreeCaseInfo ");
            sqlStr.AppendFormat("where pubDateTime >= '{0}' ", str);
            sqlStr.AppendFormat("and pubDateTime <= '{0}' ", end);
            sqlStr.Append("order by pubDateTime desc");

            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr.ToString());
            return dt.Tables[0];
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="freeCaseInfo"></param>
        /// <returns></returns>
        public int Insert(WechatFreeCaseInfo freeCaseInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder sqlStr = new StringBuilder();
                    SqlParameter[] parameters;

                    sqlStr.Append("insert into dbo.WechatFreeCaseInfo values(@msgContent, ");
                    sqlStr.Append("@pubDateTime,@operaterID,@status) ");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@msgContent",SqlDbType.NVarChar),
                        new SqlParameter("@pubDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@operaterID",SqlDbType.Int),
                        new SqlParameter("@status",SqlDbType.NVarChar)
                    };
                    parameters[0].Value = freeCaseInfo.MsgContent;
                    parameters[1].Value = freeCaseInfo.PubDateTime;
                    parameters[2].Value = freeCaseInfo.OperaterID;
                    parameters[3].Value = freeCaseInfo.Status;

                    obj = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sqlStr.ToString(), parameters);
                }
                catch
                {
                    return 0;
                }

                return obj;
            }
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="freeCaseInfo"></param>
        /// <returns></returns>
        public int Update(WechatFreeCaseInfo freeCaseInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("update dbo.WechatFreeCaseInfo set msgContent=@msgContent, ");
                    strSql.Append("pubDateTime=@pubDateTime,operaterID=@operaterID,status=@status ");
                    strSql.Append("where ID=@id");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@msgContent",SqlDbType.NVarChar),
                        new SqlParameter("@pubDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@operaterID",SqlDbType.Int),
                        new SqlParameter("@status",SqlDbType.NVarChar),
                        new SqlParameter("@id",SqlDbType.Int)
                    };
                    parameters[0].Value = freeCaseInfo.MsgContent;
                    parameters[1].Value = freeCaseInfo.PubDateTime;
                    parameters[2].Value = freeCaseInfo.OperaterID;
                    parameters[3].Value = freeCaseInfo.Status;
                    parameters[4].Value = freeCaseInfo.ID;

                    obj = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

                }
                catch
                {
                    return 0;
                }

                return obj;
            }
        }
        /// <summary>
        /// 删除数据  
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("delete from dbo.WechatFreeCaseInfo ");
                    sqlStr.AppendFormat("where ID={0}", id);

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sqlStr.ToString(), null);
                }
                catch
                {
                    return false;
                }
                if (result == 1)
                    return true;
                else
                    return false;
            }
        }

    }
}
