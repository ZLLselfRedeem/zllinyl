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
    public class WechatUxianQandAManager
    {
        /// <summary>
        /// 获取常见问答信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetUxianQandAInfo()
        {
            string sqlStr = "select * from WechatUxianQandAInfo a inner join EmployeeInfo b on a.operaterID = b.EmployeeID order by a.pubDateTime desc";

            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);
            return dt.Tables[0];
        }
        //获取问答信息 微信端显示用
        public DataTable GetUxianQandA()
        {
            string sqlStr = "select question,answer from WechatUxianQandAInfo order by pubDateTime desc";

            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);
            return dt.Tables[0];
        }

        /// <summary>
        /// 新增常见问答
        /// </summary>
        /// <param name="uxianQandAInfo"></param>
        /// <returns></returns>
        public int Insert(WechatUxianQandAInfo uxianQandAInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("insert into WechatUxianQandAInfo values(@question, ");
                    strSql.Append("@answer,@pubDateTime,@operaterID) ");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@question",SqlDbType.NVarChar),
                        new SqlParameter("@answer",SqlDbType.NVarChar),
                        new SqlParameter("@pubDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@operaterID",SqlDbType.Int)
                    };
                    parameters[0].Value = uxianQandAInfo.question;
                    parameters[1].Value = uxianQandAInfo.answer;
                    parameters[2].Value = uxianQandAInfo.pubDateTime;
                    parameters[3].Value = uxianQandAInfo.operaterID;

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
        /// 更新
        /// </summary>
        /// <param name="uxianQandAInfo"></param>
        /// <returns></returns>
        public int Update(WechatUxianQandAInfo uxianQandAInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("update WechatUxianQandAInfo set question=@question, ");
                    strSql.Append("answer=@answer,pubDateTime=@pubDateTime,operaterID=@operaterID ");
                    strSql.Append("where ID=@id");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@question",SqlDbType.NVarChar),
                        new SqlParameter("@answer",SqlDbType.NVarChar),
                        new SqlParameter("@pubDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@operaterID",SqlDbType.Int),
                        new SqlParameter("@id",SqlDbType.Int)
                    };
                    parameters[0].Value = uxianQandAInfo.question;
                    parameters[1].Value = uxianQandAInfo.answer;
                    parameters[2].Value = uxianQandAInfo.pubDateTime;
                    parameters[3].Value = uxianQandAInfo.operaterID;
                    parameters[4].Value = uxianQandAInfo.ID;

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
        /// 删除
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
                    sqlStr.Append("delete from WechatUxianQandAInfo ");
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
