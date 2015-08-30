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
    public class WechatUxianComplaintManager
    {
        /// <summary>
        /// 获取WechatUxianComplaintInfo
        /// </summary>
        /// <returns></returns>
        public DataTable GetUxianComplaintInfo()
        {
            string sqlStr = "select * from WechatUxianComplaintInfo a inner join EmployeeInfo b on a.operaterID = b.EmployeeID order by a.pubDateTime desc";

            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);
            return dt.Tables[0];
        }
        //获取投诉处理 微信客户端显示
        public string GetUxianComplaint()
        {
            string sqlStr = "select top 1 msgContent from WechatUxianComplaintInfo order by pubDateTime desc";

            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);
            return obj.ToString();
        }
        /// <summary>
        /// 新增WechatUxianComplaintInfo
        /// </summary>
        /// <param name="uxianCompaintInfo"></param>
        /// <returns></returns>
        public int Insert(WechatUxianComplaintInfo uxianCompaintInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("insert into WechatUxianComplaintInfo values(@msgContent, ");
                    strSql.Append("@pubDateTime,@operaterID) ");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@msgContent",SqlDbType.NVarChar),
                        new SqlParameter("@pubDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@operaterID",SqlDbType.Int)
                    };
                    parameters[0].Value = uxianCompaintInfo.msgContent;
                    parameters[1].Value = uxianCompaintInfo.pubDateTime;
                    parameters[2].Value = uxianCompaintInfo.operaterID;

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
        /// 更新WechatUxianComplaintInfo
        /// </summary>
        /// <param name="uxianCompaintInfo"></param>
        /// <returns></returns>
        public int Update(WechatUxianComplaintInfo uxianCompaintInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("update WechatUxianComplaintInfo set msgContent=@msgContent, ");
                    strSql.Append("pubDateTime=@pubDateTime,operaterID=@operaterID ");
                    strSql.Append("where ID=@id");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@msgContent",SqlDbType.NVarChar),
                        new SqlParameter("@pubDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@operaterID",SqlDbType.Int),
                        new SqlParameter("@id",SqlDbType.Int)
                    };
                    parameters[0].Value = uxianCompaintInfo.msgContent;
                    parameters[1].Value = uxianCompaintInfo.pubDateTime;
                    parameters[2].Value = uxianCompaintInfo.operaterID;
                    parameters[3].Value = uxianCompaintInfo.ID;

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
        /// 删除WechatUxianComplaintInfo
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
                    sqlStr.Append("delete from WechatUxianComplaintInfo ");
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
        /// 获取微信平台 客户端发送的投诉信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetComplaintReceiveInfo()
        {
            string sqlStr = @"select a.*,b.UserName as operater from WechatComplaintReceiveInfo a left join EmployeeInfo b 
                            on a.operaterID = b.EmployeeID order by receiveDateTime desc";

            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);
            return dt.Tables[0];
        }
        /// <summary>
        /// 新增 微信平台 客户端发送的投诉信息
        /// </summary>
        /// <param name="complaintReceiverInfo"></param>
        /// <returns></returns>
        public int InsertComplaintReceiveInfo(WechatComplaintReceiveInfo complaintReceiverInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("insert into WechatComplaintReceiveInfo(msgContent,contentType,senderWechatID,status,receiveDateTime,voicefileName) values(@msgContent,@contentType, ");
                    strSql.Append("@senderWechatID,@status,@receiveDateTime,@voicefileName ) ");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@msgContent",SqlDbType.NVarChar),
                        new SqlParameter("@contentType",SqlDbType.Int),
                        new SqlParameter("@senderWechatID",SqlDbType.NVarChar),
                        new SqlParameter("@status",SqlDbType.NVarChar),
                        new SqlParameter("@receiveDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@voicefileName",SqlDbType.NVarChar)
                    };
                    parameters[0].Value = complaintReceiverInfo.msgContent;
                    parameters[1].Value = complaintReceiverInfo.contentType;
                    parameters[2].Value = complaintReceiverInfo.senderWechatID;
                    parameters[3].Value = complaintReceiverInfo.status;
                    parameters[4].Value = complaintReceiverInfo.receiveDateTime;
                    parameters[5].Value = complaintReceiverInfo.voicefileName;

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
        /// 更新 微信平台 客户端发送的投诉信息 (处理人,处理时间)
        /// </summary>
        /// <param name="complaintReceiveInfo"></param>
        /// <returns></returns>
        public int UpdateComplaintReceiveInfo(WechatComplaintReceiveInfo complaintReceiveInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("update WechatComplaintReceiveInfo set operaterID=@operaterID, ");
                    strSql.Append("operateDateTime=@operateDateTime,status=@status ");
                    strSql.Append("where ID=@id");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@operaterID",SqlDbType.Int),
                        new SqlParameter("@operateDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@status", SqlDbType.NVarChar),
                        new SqlParameter("@id",SqlDbType.Int)
                    };
                    parameters[0].Value = complaintReceiveInfo.operaterID;
                    parameters[1].Value = complaintReceiveInfo.operateDateTime;
                    parameters[2].Value = complaintReceiveInfo.status;
                    parameters[3].Value = complaintReceiveInfo.ID;

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
