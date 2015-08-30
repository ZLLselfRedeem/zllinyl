using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class ShopChannelManager
    {
        /// <summary>
        /// 根据城市ID来获取所有的商户
        /// </summary>
        /// <param name="cityID"></param>
        /// <returns></returns>
        public System.Data.DataTable SelectShopByCityID(int cityID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select shopID,shopName,shopAddress,cityID from ShopInfo where cityID={0} and shopStatus=1 and isHandle=1", cityID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取该商铺及其对应的频道
        /// </summary>
        /// <returns></returns>
        public DataTable SelectChannel()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select channel.channelName as channelName,shop.isAuthorization as auth,shop.shopID as ID from Channel channel left join ShopChannel shop on channel.id= shop.channelID where channel.isDelete=0 order by shopID");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 在商铺进入设置时，自动在shopChannel表中插入频道
        /// </summary>
        /// <param name="cityID"></param>
        /// <param name="shopID"></param>
        public void DefaultInsert(int shopID)
        {
            StringBuilder strChannel = new StringBuilder();
            strChannel.AppendFormat("select id from Channel where isDelete=0 and cityID in(select cityID from ShopInfo where shopID={0}) and id not in (select channelID as id from ShopChannel where isDelete=0 and shopID={0})", shopID);
            DataTable dtCity = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strChannel.ToString()).Tables[0];
            int resultIndex = 1;
            string indexSearch = string.Format("select MAX(channelIndex) as maxIndex from ShopChannel where isDelete=0 and shopID={0}", shopID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, indexSearch);
            try
            {
                resultIndex = Convert.ToInt32(ds.Tables[0].Rows[0]["maxIndex"]);
            }
            catch (Exception)
            {
                
            }
            //StringBuilder strShopChannel = new StringBuilder();
            //strShopChannel.AppendFormat("select channelID as id from ShopChannel where isDelete=0 and shopID={0}", shopID);
            //DataTable dtShop = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strShopChannel.ToString()).Tables[0];
            if (dtCity.Rows.Count > 0)
            {
                foreach (DataRow row in dtCity.Rows)
                {
                    resultIndex++;
                    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                    {
                        int result = 0;
                        try
                        {
                            StringBuilder strInsert = new StringBuilder();

                            strInsert.AppendFormat("Insert into ShopChannel values ({0},{1},{2},0,0,'{3}',0)", Convert.ToInt32(row["id"]), shopID, resultIndex, DateTime.Now);
                            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strInsert.ToString());
                        }
                        catch (Exception)
                        { 
                        }
                    }

                }

                //StringBuilder strDelete = new StringBuilder();
                //foreach(DataRow row in resutlDelete)
                //{
                //    strDelete.AppendFormat("Update ShopChannel set isDelete=1 where shopID={0} and channelID={1}", shopID, Convert.ToInt32(row["id"]));
                //}
            }
            else
            {
                return;
            }
        }

        public DataTable SelectShopChannelList(int shopID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select s.id as shopChannelID,c.channelName as channelName,s.isAuthorization as isAuthorization from Channel c join ShopChannel s on c.id=s.channelID where c.cityID in (select cityID from ShopInfo where shopID={0}) and s.shopID={0} and c.isDelete=0 order by s.id",shopID);
            DataTable dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()).Tables[0];
            return dt;
        }

        /// <summary>
        /// 更新是否授权
        /// </summary>
        /// <param name="shopChannelID"></param>
        /// <param name="auth"></param>
        /// <returns></returns>
        public bool UpdateAuth(int shopChannelID, int auth)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("update ShopChannel set isAuthorization={0} where id={1}", auth, shopChannelID);
                    if (auth == 0)
                    {
                        strSql.AppendFormat("; update ShopChannel set status=0 where id={0}", shopChannelID);
                    }
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

        public DataTable SelectShopRequestByCityID(int cityID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select request.id as id,shop.shopID,shop.shopName as shopName, shop.shopAddress as address,request.createTime as createTime,channel.channelName as channelName from ShopChannelRequest request join Channel channel ON request.channelID = channel.id join ShopInfo shop ON shop.shopID=request.shopID where shop.cityID={0} and request.isDelete=0 Order by request.createTime desc", cityID);
            DataTable dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()).Tables[0];
            return dt;
        }

        /// <summary>
        /// 移除商户请求页面的记录
        /// </summary>
        /// <param name="requestID"></param>
        /// <returns></returns>
        public bool RemoveRequest(int requestID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("update ShopChannelRequest set isDelete=1 where id={0}", requestID);
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
        /// 商户增值页面列表
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public DataTable SelectShopChannel(int shopID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select sc.id as id,c.channelName as name,c.sign as sign,sc.status as status,c.content as content from ShopChannel sc join Channel c on sc.channelID=c.id where sc.shopID={0} and c.isDelete=0 and sc.isDelete=0 and c.status=1 order by sc.channelIndex", shopID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 后台网站用的商户增值页面列表
        /// </summary>
        /// <param name="shopID"></param>
        /// <param name="pageType"></param>
        /// <returns></returns>
        public DataTable SelectShopChannel(int shopID,string pageType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select ROW_NUMBER()over(Order by sc.status desc)as rowID,sc.id as id,c.channelName as name,(case c.sign when 0 then '无' when 1 then '新品' when 2 then '限时' when 3 then '特价' end) as sign,sc.status as status,sc.channelIndex as channelIndex from ShopChannel sc join Channel c on sc.channelID=c.id where sc.shopID={0} and c.isDelete=0 and sc.isDelete=0 and c.status=1 order by sc.status desc", shopID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据shopChannelID值查找该shopChannel,查看是否已授权
        /// </summary>
        /// <param name="shopChannelID"></param>
        /// <returns></returns>
        public DataTable SelectChannel(int shopChannelID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select * from ShopChannel where id={0}", shopChannelID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        public bool UpdateStatus(int shopChannelID, int status)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("update ShopChannel set status={0} where id={1}", status, shopChannelID);
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

        public bool InsertRecord(int channelID, int shopID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("insert into ShopChannelRequest values({0},{1},'{2}',0)", channelID, shopID, DateTime.Now);
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString());
                }
                catch (Exception)
                {
                    return false;
                }
                if (result > 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool UpdateIndex(int shopChannelID, int shopChannelIndex)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try 
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("update ShopChannel set channelIndex={0} where id={1}",shopChannelIndex, shopChannelID);
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

        public bool IndexIsClash(int shopID, int channelIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select * from ShopChannel where shopID={0} and channelIndex={1} and status=1", shopID, channelIndex);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 根据商铺ID查询其所在的城市ID
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public DataTable SearchCityID(int shopID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select * from ShopInfo where shopID={0}", shopID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        public DataTable SelectAllShopChanne()
        {
            string sql = @"select channelID,isAuthorization,shopID from ShopChannel where isDelete=0";
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql);
            return ds.Tables[0];
        }
    }
}
