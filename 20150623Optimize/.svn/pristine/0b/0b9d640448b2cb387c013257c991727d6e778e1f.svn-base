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
    public class WechatUxianCooperationManager
    {
        /// <summary>
        /// 获取发布的信息 
        /// </summary>
        /// <returns></returns>
        public DataTable GetCooperationInfo()
        {
            string sqlStr = "select * from WechatUxianCooperationInfo a inner join EmployeeInfo b on a.operaterID = b.EmployeeID order by a.pubDateTime desc";

            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);
            return dt.Tables[0];
        }

        public string GetCooperation()
        {
            string sqlStr = "select top 1 msgContent from WechatUxianCooperationInfo order by pubDateTime desc";
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);
            return obj.ToString();
        }

        /// <summary>
        /// 新增发布信息 UxianCooperationInfo表
        /// </summary>
        /// <param name="uxianCooperationInfo"></param>
        /// <returns></returns>
        public int Insert(WechatUxianCooperationInfo uxianCooperationInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("insert into WechatUxianCooperationInfo values(@msgContent, ");
                    strSql.Append("@pubDateTime,@operaterID) ");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@msgContent",SqlDbType.NVarChar),
                        new SqlParameter("@pubDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@operaterID",SqlDbType.Int)
                    };
                    parameters[0].Value = uxianCooperationInfo.msgContent;
                    parameters[1].Value = uxianCooperationInfo.pubDateTime;
                    parameters[2].Value = uxianCooperationInfo.operaterID;

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
        /// 更新UxianCooperationInfo表
        /// </summary>
        /// <param name="uxianCooperationInfo"></param>
        /// <returns></returns>
        public int Update(WechatUxianCooperationInfo uxianCooperationInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("update WechatUxianCooperationInfo set msgContent=@msgContent, ");
                    strSql.Append("pubDateTime=@pubDateTime,operaterID=@operaterID ");
                    strSql.Append("where ID=@id");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@msgContent",SqlDbType.NVarChar),
                        new SqlParameter("@pubDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@operaterID",SqlDbType.Int),
                        new SqlParameter("@id",SqlDbType.Int)
                    };
                    parameters[0].Value = uxianCooperationInfo.msgContent;
                    parameters[1].Value = uxianCooperationInfo.pubDateTime;
                    parameters[2].Value = uxianCooperationInfo.operaterID;
                    parameters[3].Value = uxianCooperationInfo.ID;

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
        /// 删除 UxianCooperationInfo表中一条数据
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
                    sqlStr.Append("delete from WechatUxianCooperationInfo ");
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
        /// 获取 商户发送的 合作信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetMerchantSendInfo()
        {
            string sqlStr = @"select a.*,b.UserName operater from WechatMerchantSendInfo a left join EmployeeInfo b
                            on a.operaterID = b.EmployeeID  order by receiveDateTime desc";

            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);
            return dt.Tables[0];
        }

        /// <summary>
        /// 向商户发送信息 表中增加一条记录 (由微信公众平台接收)
        /// </summary>
        /// <param name="merchantSendInfo"></param>
        /// <returns></returns>
        public int InsertMerchantSendInfo(WechatMerchantSendInfo merchantSendInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("insert into WechatMerchantSendInfo(msgContent,senderWechatID,receiveDateTime,status) values(@msgContent, ");
                    strSql.Append("@senderWechatID,@receiveDateTime,@status) ");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@msgContent",SqlDbType.NVarChar),
                        new SqlParameter("@senderWechatID",SqlDbType.NVarChar),
                        new SqlParameter("@receiveDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@status",SqlDbType.NVarChar)
                    };
                    parameters[0].Value = merchantSendInfo.msgContent;
                    parameters[1].Value = merchantSendInfo.senderWechatID;
                    parameters[2].Value = merchantSendInfo.receiveDateTime;
                    parameters[3].Value = merchantSendInfo.status;

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
        /// 更新 WechatMerchantSendInfo 商户发送信息 表
        /// </summary>
        /// <param name="merchantSendInfo"></param>
        /// <returns></returns>
        public int UpdateMerchantSendInf(WechatMerchantSendInfo merchantSendInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("update WechatMerchantSendInfo set status=@status, ");
                    strSql.Append("operaterID=@operaterID,operateDateTime=@operateDateTime ");
                    strSql.Append("where ID=@id");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@status",SqlDbType.NVarChar),
                        new SqlParameter("@operaterID",SqlDbType.Int),
                        new SqlParameter("@operateDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@id",SqlDbType.Int)
                    };
                    parameters[0].Value = merchantSendInfo.status;
                    parameters[1].Value = merchantSendInfo.operaterID;
                    parameters[2].Value = merchantSendInfo.operateDateTime;
                    parameters[3].Value = merchantSendInfo.ID;

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
