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
    /// <summary>
    /// 微信 本期大奖
    /// </summary>
    public class WechatTopPriceManager
    {
        /// <summary>
        /// 获取所有 本期大奖信息 (包括历史数据)
        /// </summary>
        /// <returns></returns>
        public DataTable GetTopPriceInfo()
        {
            string sqlStr = "select * from WechatTopPriceInfo order by pubDateTime desc";

            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);
            return dt.Tables[0];
        }
        //获取最新的一条记录
        public string GetTopOneOfTopPriceInfo()
        {
            string sqlStr = "select top 1 msgContent from WechatTopPriceInfo where status='未过期' order by pubDateTime desc";

            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr);
            return obj.ToString();
        }

        /// <summary>
        /// 获取所有 本期大奖信息 (包括历史数据)  发布日期时间(str起始时间   end截止时间)
        /// </summary>
        /// <returns></returns>
        public DataTable GetTopPriceInfo(string str, string end)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select * from WechatTopPriceInfo ");
            sqlStr.AppendFormat("where pubDateTime >= '{0}' ",str);
            sqlStr.AppendFormat("and pubDateTime <= '{0}' ",end);
            sqlStr.Append("order by pubDateTime desc" );

            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr.ToString());
            return dt.Tables[0];
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <param name="topPriceInfo"></param>
        /// <returns></returns>
        public int Insert(WechatTopPriceInfo topPriceInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("insert into dbo.WechatTopPriceInfo values(@msgContent, ");
                    strSql.Append("@pubDateTime,@operaterID,@status) ");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@msgContent",SqlDbType.NVarChar),
                        new SqlParameter("@pubDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@operaterID",SqlDbType.Int),
                        new SqlParameter("@status",SqlDbType.NVarChar)
                    };
                    parameters[0].Value = topPriceInfo.MsgContent;
                    parameters[1].Value = topPriceInfo.PubDateTime;
                    parameters[2].Value = topPriceInfo.OperaterID;
                    parameters[3].Value = topPriceInfo.Status;

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
        /// 更新信息
        /// </summary>
        /// <param name="topPriceInfo"></param>
        /// <returns></returns>
        public int Update(WechatTopPriceInfo topPriceInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    strSql.Append("update dbo.WechatTopPriceInfo set msgContent=@msgContent, ");
                    strSql.Append("pubDateTime=@pubDateTime,operaterID=@operaterID,status=@status ");
                    strSql.Append("where ID=@id" );
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@msgContent",SqlDbType.NVarChar),
                        new SqlParameter("@pubDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@operaterID",SqlDbType.Int),
                        new SqlParameter("@status",SqlDbType.NVarChar),
                        new SqlParameter("@id",SqlDbType.Int)
                    };
                    parameters[0].Value = topPriceInfo.MsgContent;
                    parameters[1].Value = topPriceInfo.PubDateTime;
                    parameters[2].Value = topPriceInfo.OperaterID;
                    parameters[3].Value = topPriceInfo.Status;
                    parameters[4].Value = topPriceInfo.ID;

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
                    sqlStr.Append("delete from dbo.WechatTopPriceInfo ");
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
        /// 根据vip号查询 联系方式etc.
        /// </summary>
        /// <param name="eCardNum"></param>
        /// <returns></returns>
        public DataTable GetVIPContectInfo(string eCardNum)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select b.eCardNumber,b.mobilePhoneNumber,b.customerEmail,b.wechatId,a.cityName ");
            sqlStr.Append("from dbo.City a inner join dbo.CustomerInfo b ");
            sqlStr.Append("on a.cityID = b.registerCityId ");
            sqlStr.AppendFormat("where b.eCardNumber = '{0}' ", eCardNum);

            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr.ToString());
            return dt.Tables[0];
        }
    }
}
