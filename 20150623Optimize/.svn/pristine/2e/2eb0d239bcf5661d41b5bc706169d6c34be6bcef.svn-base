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
    public class WechatUxianProposalManager
    {
        /// <summary>
        /// 获取WechatUxianProposalInfo
        /// </summary>
        /// <returns></returns>
        public DataTable GetUxianProposalInfo()
        {
            string sqlStr = "select * from WechatUxianProposalInfo a inner join EmployeeInfo b on a.operaterID = b.EmployeeID order by a.pubDateTime desc";

            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);
            return dt.Tables[0];
        }
        // 获取意见建议 微信客户端显示用
        public string GetUxianProposal()
        {
            string sqlStr = "select top 1 msgContent from WechatUxianProposalInfo order by pubDateTime desc";

            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);
            return obj.ToString();
        }
        /// <summary>
        /// 新增WechatUxianProposalInfo
        /// </summary>
        /// <param name="uxianProposalInfo"></param>
        /// <returns></returns>
        public int Insert(WechatUxianProposalInfo uxianProposalInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("insert into WechatUxianProposalInfo values(@msgContent, ");
                    strSql.Append("@pubDateTime,@operaterID) ");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@msgContent",SqlDbType.NVarChar),
                        new SqlParameter("@pubDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@operaterID",SqlDbType.Int)
                    };
                    parameters[0].Value = uxianProposalInfo.msgContent;
                    parameters[1].Value = uxianProposalInfo.pubDateTime;
                    parameters[2].Value = uxianProposalInfo.operaterID;

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
        /// 更新WechatUxianProposalInfo
        /// </summary>
        /// <param name="uxianProposalInfo"></param>
        /// <returns></returns>
        public int Update(WechatUxianProposalInfo uxianProposalInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("update WechatUxianProposalInfo set msgContent=@msgContent, ");
                    strSql.Append("pubDateTime=@pubDateTime,operaterID=@operaterID ");
                    strSql.Append("where ID=@id");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@msgContent",SqlDbType.NVarChar),
                        new SqlParameter("@pubDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@operaterID",SqlDbType.Int),
                        new SqlParameter("@id",SqlDbType.Int)
                    };
                    parameters[0].Value = uxianProposalInfo.msgContent;
                    parameters[1].Value = uxianProposalInfo.pubDateTime;
                    parameters[2].Value = uxianProposalInfo.operaterID;
                    parameters[3].Value = uxianProposalInfo.ID;

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
                    sqlStr.Append("delete from WechatUxianProposalInfo ");
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

        /// <summary>
        /// 获取 微信客户端发送的 意见建议信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetProposalReceiveInfo()
        {
            string sqlStr = @"select a.*,b.UserName as operater from WechatProposalReceiveInfo a left join EmployeeInfo b 
            on a.operaterID = b.EmployeeID order by receiveDateTime desc";

            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);
            return dt.Tables[0];
        }
        /// <summary>
        /// 新增 微信客户端发送的意见建议记录
        /// </summary>
        /// <param name="proposalReceiveInfo"></param>
        /// <returns></returns>
        public int InsertProposalReceiveInfo(WechatProposalReceiveInfo proposalReceiveInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("insert into WechatProposalReceiveInfo(msgContent,contentType,senderWechatID,status,receiveDateTime,voicefileName) values(@msgContent,@contentType, ");
                    strSql.Append("@senderWechatID,@status,@receiveDateTime,@voicefileName) ");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@msgContent",SqlDbType.NVarChar),
                        new SqlParameter("@contentType",SqlDbType.Int),
                        new SqlParameter("@senderWechatID",SqlDbType.NVarChar),
                        new SqlParameter("@status",SqlDbType.NVarChar),
                        new SqlParameter("@receiveDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@voicefileName",SqlDbType.NVarChar)
                    };
                    parameters[0].Value = proposalReceiveInfo.msgContent;
                    parameters[1].Value = proposalReceiveInfo.contentType;
                    parameters[2].Value = proposalReceiveInfo.senderWechatID;
                    parameters[3].Value = proposalReceiveInfo.status;
                    parameters[4].Value = proposalReceiveInfo.receiveDateTime;
                    parameters[5].Value = proposalReceiveInfo.voicefileName;

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
        /// 更新WechatProposalReceiveInfo 处理人,处理时间
        /// </summary>
        /// <param name="proposalReceiveInfo"></param>
        /// <returns></returns>
        public int UpdateProposalReceiveInfo(WechatProposalReceiveInfo proposalReceiveInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("update WechatProposalReceiveInfo set operaterID=@operaterID,operateDateTime=@operateDateTime,status=@status ");
                    strSql.Append("where ID=@id ");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@operaterID",SqlDbType.Int),
                        new SqlParameter("@operateDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@status",SqlDbType.NVarChar),
                        new SqlParameter("@id",SqlDbType.Int)
                    };
                    parameters[0].Value = proposalReceiveInfo.operaterID;
                    parameters[1].Value = proposalReceiveInfo.operateDateTime;
                    parameters[2].Value = proposalReceiveInfo.status;
                    parameters[3].Value = proposalReceiveInfo.ID;

                    obj = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

                }
                catch
                {
                    return 0;
                }

                return obj;
            }
        }
    }
}
