using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using System.Data;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class WechatReceivedMsgManager
    {
        //记录下微信客户端发来的信息，包括文字和语音信息
        public int InsertReceivedMsg(WechatReceivedMsg receivedMsg)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("insert into WechatReceivedMsg values(@msgContent,@contentType, ");
                    strSql.Append("@senderWechatID,@media_id,@receiveDateTime,@status) ");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@msgContent",SqlDbType.NVarChar),
                        new SqlParameter("@contentType",SqlDbType.Int),
                        new SqlParameter("@senderWechatID",SqlDbType.NVarChar),
                        new SqlParameter("@media_id",SqlDbType.NVarChar),
                        new SqlParameter("@receiveDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@status",SqlDbType.Int)
                    };
                    parameters[0].Value = receivedMsg.msgContent;
                    parameters[1].Value = receivedMsg.contentType;
                    parameters[2].Value = receivedMsg.senderWechatID;
                    parameters[3].Value = receivedMsg.media_id;
                    parameters[4].Value = receivedMsg.receiveDateTime;
                    parameters[5].Value = receivedMsg.status;

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
