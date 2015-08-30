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
    public class WechatLandladyVoiceManager
    {
        /// <summary>
        /// 获取 聆听老板娘info
        /// </summary>
        /// <returns></returns>
        public DataTable GetLandladysVoiceInfo()
        {
            string sqlStr = "select * from WechatLandladysVoiceInfo a inner join EmployeeInfo b on a.operaterID = b.EmployeeID order by a.pubDateTime desc";

            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);
            return dt.Tables[0];
        }

        /// <summary>
        /// 先删 后增
        /// </summary>
        /// <param name="landyVoiceInfo"></param>
        /// <returns></returns>
        public int Insert(WechatLandladyVoiceInfo landyVoiceInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("truncate table WechatLandladysVoiceInfo;");
                    strSql.Append("insert into WechatLandladysVoiceInfo(fileName,remark,pubDateTime,operaterID) values(@fileName, ");
                    strSql.Append("@remark,@pubDateTime,@operaterID) ");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@fileName",SqlDbType.NVarChar),
                        new SqlParameter("@remark",SqlDbType.NVarChar),
                        new SqlParameter("@pubDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@operaterID",SqlDbType.Int)
                    };
                    parameters[0].Value = landyVoiceInfo.fileName;
                    parameters[1].Value = landyVoiceInfo.remark;
                    parameters[2].Value = landyVoiceInfo.pubDateTime;
                    parameters[3].Value = landyVoiceInfo.operaterID;

                    obj = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

                }
                catch
                {
                    return 0;
                }

                return obj;
            }
        }

        //更新media_id,status
        public int UpdateMediaIDAndStatus(string media_id, int rowId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("update WechatLandladysVoiceInfo set media_id=@media_id, ");
                    strSql.Append("status=@status where ID=@ID) ");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@media_id",SqlDbType.NVarChar),
                        new SqlParameter("@status",SqlDbType.NVarChar),
                        new SqlParameter("@ID",SqlDbType.Int)
                    };
                    parameters[0].Value = media_id;
                    parameters[1].Value = "1";
                    parameters[2].Value = rowId;

                    obj = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

                }
                catch
                {
                    return 0;
                }

                return obj;
            }
        }

        //获取出语音文件的media_id,供微信客户端调用
        public string GetMediaId()
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj;
                try
                {
                    conn.Open();
                    string sqlstr = "select top 1 media_id from WechatLandladysVoiceInfo ";
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, sqlstr, null);
                }
                catch
                {
                    return null;
                }

                return obj.ToString();
            }
        }

        public bool Delete()
        {
            string sqlStr = "truncate table WechatLandladysVoiceInfo";

            int iret = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);
            if (iret == 0)
                return false;
            else
                return true;
        }
    }
}
