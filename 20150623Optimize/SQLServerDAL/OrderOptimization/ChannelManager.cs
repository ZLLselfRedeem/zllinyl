using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class ChannelManager
    {
        /// <summary>
        /// 根据cityID来获取增值页(频道)数据源
        /// </summary>
        /// <param name="cityID"></param>
        /// <returns></returns>
        public DataTable SelectChannel(int cityID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ROW_NUMBER()over(Order by channel.status desc)as rowID,channel.id,channel.channelName,channel.price,(case channel.sign when 0 then '无' when 1 then '新品' when 2 then '限时' when 3 then '特价' end) signStr,channel.firstTitleID as firstID,firstTitle.titleName,channel.status as status,channel.modelType as modelType"); 
            strSql.Append(" from Channel channel");
            strSql.Append(" left JOIN HomeFirstTitle firstTitle"); 
            strSql.Append(" ON firstTitle.id=channel.firstTitleID");
            strSql.AppendFormat(" where channel.isDelete=0 and channel.cityID={0}", cityID);
            strSql.Append(" order by channel.status desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据id来删除增值页（频道）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveChannel(int id)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("Update Channel set isDelete=1 where id={0}",id);
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString());
                }
                catch (System.Exception)
                {
                    return false;
                }
                if (result >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 该频道是否开启
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int MerchantUpdate(int id, int status)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("update Channel set status={0} where id={1};", status, id);
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString());
                }
                catch (Exception)
                {
                    return result;
                }
            }
            return result;
        }

        /// <summary>
        /// 插入增值页面
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        public int Insert(Channel channel)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("Insert into Channel");
                    strSql.Append(" values(@channelName,@cityID,@price,@firstTitleID,@status,@sign,@content,@createTime,@isDelete,@modelType)");

                    SqlParameter[] param = new SqlParameter[] { 
                        new SqlParameter("@channelName",SqlDbType.NVarChar),
                        new SqlParameter("@cityID", SqlDbType.Int,4),
                        new SqlParameter("@price",SqlDbType.Money),
                        new SqlParameter("@firstTitleID",SqlDbType.Int, 4),
                        new SqlParameter("@status", SqlDbType.Int,4),
                        new SqlParameter("@sign", SqlDbType.Int,4),
                        new SqlParameter("@content",SqlDbType.NVarChar),
                        new SqlParameter("@createTime",SqlDbType.DateTime),
                        new SqlParameter("@isDelete",SqlDbType.Bit),
                        new SqlParameter("@modelType",SqlDbType.Int,4)
                        };

                    param[0].Value = channel.channelName;
                    param[1].Value = channel.cityID;
                    param[2].Value = channel.price;
                    param[3].Value = channel.firstTitleID;
                    param[4].Value = channel.status;
                    param[5].Value = channel.sign;
                    param[6].Value = channel.content;
                    param[7].Value = channel.createTime;
                    param[8].Value = channel.isDelete;
                    param[9].Value = channel.modelType;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), param);
                }
                catch (Exception)
                {
                }
            }
            return result;
        }

        /// <summary>
        /// 根据id获取对应的channel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Channel Select(int channelID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select channelName,cityID,price,firstTitleID,status,sign,content,modelType from Channel where id={0}", channelID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            DataRow row = ds.Tables[0].Rows[0];
            Channel channel = new Channel()
            {
                id = channelID,
                channelName = Convert.ToString(row["channelName"]),
                cityID = Convert.ToInt32(row["cityID"]),
                price = Convert.ToDouble(row["price"]),
                firstTitleID = Convert.ToInt32(row["firstTitleID"]),
                sign = Convert.ToInt32(row["sign"]),
                content = Convert.ToString(row["content"]),
                modelType = Convert.ToInt32(row["modelType"])
            };

            if (Convert.ToString(row["status"]) == "False")
            {
                channel.status = false;
            }
            else
            {
                channel.status = true;
            }

            return channel;
        }

        /// <summary>
        /// 更新增值页面
        /// </summary>
        /// <param name="channel"></param>
        /// <returns></returns>
        public int Update(Channel channel)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update Channel set");
                    strSql.Append(" channelName=@channelName,price=@price,firstTitleID=@firstTitleID,status=@status,sign=@sign,content=@content,modelType=@modelType where id=@channelID");

                    SqlParameter[] param = new SqlParameter[] { 
                        new SqlParameter("@channelName",SqlDbType.NVarChar),
                        new SqlParameter("@price",SqlDbType.Money),
                        new SqlParameter("@firstTitleID",SqlDbType.Int, 4),
                        new SqlParameter("@status", SqlDbType.Int,4),
                        new SqlParameter("@sign",SqlDbType.Int,4),
                        new SqlParameter("@content",SqlDbType.NVarChar),
                        new SqlParameter("@modelType",SqlDbType.Int,4),
                        new SqlParameter("@channelID",SqlDbType.Int,4)
                        };

                    param[0].Value = channel.channelName;
                    param[1].Value = channel.price;
                    param[2].Value = channel.firstTitleID;
                    param[3].Value = channel.status;
                    param[4].Value = channel.sign;
                    param[5].Value = channel.content;
                    param[6].Value = channel.modelType;
                    param[7].Value = channel.id;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), param);
                }
                catch (Exception)
                {
                }
            }
            return result;
        }

        public DataTable SelectAllChannel()
        {
            string sql= @"select id,channelName,cityID from Channel where isDelete=0";
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql);
            return ds.Tables[0];
        }

        public bool DeteteRelation(int id)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            { 
                int result = 0;
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("update shopChannel set isAuthorization=0,status=0 where channelID={0};", id);
                    strSql.AppendFormat(" delete shopChannelDish where shopChannelID in (select id from ShopChannel where channelID={0})", id);
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString());
                }
                catch (Exception)
                {
                    return false;
                }

                if (result >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }
    }
}
