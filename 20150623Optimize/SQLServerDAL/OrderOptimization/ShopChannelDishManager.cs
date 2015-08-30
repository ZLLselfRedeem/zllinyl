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
    public class ShopChannelDishManager
    {
        /// <summary>
        /// 根据城市ID来获取所有的商户
        /// </summary>
        /// <param name="cityID"></param>
        /// <returns></returns>
        public System.Data.DataTable SelectShopByCityID(int cityID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select shopID,shopName,shopAddress from ShopInfo where cityID={0}", cityID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 添加菜品 
        /// </summary>
        /// <param name="shopChannelID"></param>
        /// <param name="dishID"></param>
        /// <param name="dishPriceID"></param>
        /// <param name="dishIndex"></param>
        /// <param name="dishName"></param>
        /// <param name="dishImageUrl"></param>
        /// <param name="dishContent"></param>
        /// <returns></returns>
        public static string ChannelDishAdd(int shopChannelID, int dishID, int dishPriceID, int dishIndex, string dishName, string dishImageUrl, string dishContent,DateTime time,bool isDelete)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append(@"INSERT INTO [dbo].[ShopChannelDish]
                                       ([shopChannelID]
                                       ,[dishID]
                                       ,[dishPriceID]
                                       ,[dishName]
                                       ,[dishIndex]
                                       ,[dishContent]
                                       ,[dishImageUrl]
                                       ,[createTime]
                                       ,[isDelete]
                                       ,[status])
                                 VALUES(@shopChannelID,@dishID,@dishPriceID,@dishName,@dishIndex,@dishContent,@dishImageUrl,@createTime,@isDelete,@status) ");


                    SqlParameter[] parameters = {
                        new SqlParameter("@shopChannelID", SqlDbType.Int,4),
                        new SqlParameter("@dishID", SqlDbType.Int,4),
					    new SqlParameter("@dishPriceID", SqlDbType.Int,4),
                        new SqlParameter("@dishName", SqlDbType.NVarChar,50),
                        new SqlParameter("@dishIndex", SqlDbType.Int,4),
                        new SqlParameter("@dishContent", SqlDbType.NVarChar,2000),
                        new SqlParameter("@dishImageUrl", SqlDbType.NVarChar,200),
                        new SqlParameter("@createTime", SqlDbType.DateTime),
                        new SqlParameter("@isDelete", SqlDbType.Bit,4),
                        new SqlParameter("@status",SqlDbType.Bit,4)};

                    parameters[0].Value = shopChannelID;
                    parameters[1].Value = dishID;
                    parameters[2].Value = dishPriceID;
                    parameters[3].Value = dishName;
                    parameters[4].Value = dishIndex;
                    parameters[5].Value = dishContent;
                    parameters[6].Value = dishImageUrl;
                    parameters[7].Value = time;
                    parameters[8].Value = isDelete;
                    parameters[9].Value = false;

                    obj = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                if (obj >= 1)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
        }

        /// <summary>
        /// 添加一条删除的菜品
        /// </summary>
        /// <param name="shopChannelID"></param>
        /// <param name="dishID"></param>
        /// <param name="dishPriceID"></param>
        /// <param name="dishIndex"></param>
        /// <param name="dishName"></param>
        /// <param name="dishImageUrl"></param>
        /// <param name="dishContent"></param>
        /// <returns></returns>
        public static string ChannelDishAddDelete(int shopChannelID, int dishID, int dishPriceID, int dishIndex, string dishName, string dishImageUrl, string dishContent,bool isDelete)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append(@"INSERT INTO [dbo].[ShopChannelDish]
                                       ([shopChannelID]
                                       ,[dishID]
                                       ,[dishPriceID]
                                       ,[dishName]
                                       ,[dishIndex]
                                       ,[dishContent]
                                       ,[dishImageUrl]
                                       ,[createTime]
                                       ,[isDelete])
                                 VALUES(@shopChannelID,@dishID,@dishPriceID,@dishName,@dishIndex,@dishContent,@dishImageUrl,@createTime,@isDelete) ");


                    SqlParameter[] parameters = {
                        new SqlParameter("@shopChannelID", SqlDbType.Int,4),
                        new SqlParameter("@dishID", SqlDbType.Int,4),
					    new SqlParameter("@dishPriceID", SqlDbType.Int,4),
                        new SqlParameter("@dishName", SqlDbType.NVarChar,50),
                        new SqlParameter("@dishIndex", SqlDbType.Int,4),
                        new SqlParameter("@dishContent", SqlDbType.NVarChar,2000),
                        new SqlParameter("@dishImageUrl", SqlDbType.NVarChar,200),
                        new SqlParameter("@createTime", SqlDbType.DateTime),
                        new SqlParameter("@isDelete", SqlDbType.Bit,4)};

                    parameters[0].Value = shopChannelID;
                    parameters[1].Value = dishID;
                    parameters[2].Value = dishPriceID;
                    parameters[3].Value = dishName;
                    parameters[4].Value = dishIndex;
                    parameters[5].Value = dishContent;
                    parameters[6].Value = dishImageUrl;
                    parameters[7].Value = DateTime.Now;
                    parameters[8].Value = isDelete;

                    obj = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                if (obj >= 1)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
        }

        public static string ChannelDishUpdate(int id, int dishID, int dishPriceID, int dishIndex, string dishName, string dishImageUrl, string dishContent)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"UPDATE [dbo].[ShopChannelDish]
                               SET [dishID] = @dishID
                                  ,[dishPriceID] =@dishPriceID
                                  ,[dishName] =@dishName
                                  ,[dishIndex] =@dishIndex
                                  ,[dishContent] =@dishContent
                                  ,[dishImageUrl] =@dishImageUrl
                             WHERE id=@id");
            SqlParameter[] parameters = {
                        new SqlParameter("@dishID", SqlDbType.Int,4),
					    new SqlParameter("@dishPriceID", SqlDbType.Int,4),
                        new SqlParameter("@dishName", SqlDbType.NVarChar,50),
                        new SqlParameter("@dishIndex", SqlDbType.Int,4),
                        new SqlParameter("@dishContent", SqlDbType.NVarChar,2000),
                        new SqlParameter("@dishImageUrl", SqlDbType.NVarChar,200),
                        new SqlParameter("@id", SqlDbType.Int,4) };
            parameters[0].Value = dishID;
            parameters[1].Value = dishPriceID;
            parameters[2].Value = dishName;
            parameters[3].Value = dishIndex;
            parameters[4].Value = dishContent;
            parameters[5].Value = dishImageUrl;
            parameters[6].Value = id;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result >= 1)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }

        public static string ChannelDishDelete(string channelDishIDS)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    string strSql = string.Format("update ShopChannelDish set isDelete=1 where id in ({0}) ", channelDishIDS);
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), null);
                }
                catch
                {

                }
                if (result > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
        }

        /// <summary>
        /// 判断菜品是否已添加
        /// </summary>
        /// <param name="shopChannelID"></param>
        /// <param name="dishPriceID"></param>
        /// <returns></returns>
        public static bool CheckHasChannelDish(int shopChannelID, int dishPriceID)
        {
            string strSql = @"select * from ShopChannelDish where isDelete=0 and shopChannelID={0} and dishPriceID={1} ";
            strSql = string.Format(strSql, shopChannelID, dishPriceID);
            bool hasRow = false;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql))
            {
                if (dr.Read())
                {
                    hasRow = true;
                }
            }
            return hasRow;
        }

        public bool UpdateIndex(int dishID, int dishIndex)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("update ShopChannelDish set dishIndex={0} where id={1};", dishIndex, dishID);
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

        public DataTable SelectByShopChannelID(int shopChannelID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select d.id as id,d.dishName as dishName,pi.DishPrice as dishPrice,d.dishIndex from ShopChannelDish d join DishPriceInfo pi on d.dishPriceID=pi.DishPriceID where d.shopChannelID={0} and d.isDelete=0 order by d.dishIndex", shopChannelID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        public DataTable SelectByShopChannelIDBack(int shopChannelID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select ROW_NUMBER()over(Order by d.dishIndex) as rowID,d.id as id,d.dishName as dishName,pi.DishPrice as dishPrice,d.dishIndex as dishIndex from ShopChannelDish d join DishPriceInfo pi on d.dishPriceID=pi.DishPriceID where d.shopChannelID={0} and d.isDelete=0 order by d.dishIndex", shopChannelID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        public static DataTable GetShopChannelDish(int shopChannelDishID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select * from ShopChannelDish where id={0} and isDelete=0 ", shopChannelDishID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        public bool Remove(int dishID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("update ShopChannelDish set isDelete=1 where id={0}",dishID);
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
        /// <summary>
        /// 查询商家频道菜品详情
        /// </summary>
        /// <param name="shopChannelDishID"></param>
        /// <returns></returns>
        public static DataTable SearchShopChannelDish(int shopChannelDishID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select * from ShopChannelDish where id={0} ", shopChannelDishID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        public int NoPublicDelete(int shopChannelID)
        {
            using(SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("delete ShopChannelDish where shopChannelID={0} and status=0;", shopChannelID);
                    strSql.AppendFormat("update ShopChannelDish set isDelete=0 where shopChannelID={0} and isDelete=1", shopChannelID);
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString());
                }
                catch (Exception)
                {
                }
                return result;
            }
        }

       /// <summary>
        /// 发布频道菜品(物理删除isDelete=1的数据,修改所有状态为status=1)
        /// </summary>
        /// <param name="shopChannelDishID"></param>
        /// <returns></returns>
        public static string ShopChannelDishRelease(int shopChannelID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    string strSql = string.Format("update ShopChannelDish set status=1 where shopChannelID={0};delete ShopChannelDish where isDelete=1  and shopChannelID={0}; ", shopChannelID);
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), null);
                }
                catch
                {
                    return "0";
                }
                return "1";
            }
        }

        /// <summary>
        /// 频道菜品发布
        /// </summary>
        /// <param name="channelID"></param>
        /// <returns></returns>
        public bool Public(int channelID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("delete shopChannelDish where shopChannelID={0} and isDelete=1;", channelID);
                    strSql.AppendFormat("update shopChannelDish set status=1 where shopChannelID={0}", channelID);
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

        public ShopChannelDish SelectDishByDishID(int dishID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select * from ShopChannelDish where id={0}", dishID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            DataRow row = ds.Tables[0].Rows[0];
            ShopChannelDish dish = new ShopChannelDish() 
            {
                id=Convert.ToInt32(row["id"]),
                shopChannelID = Convert.ToInt32(row["shopChannelID"]),
                dishID = Convert.ToInt32(row["dishID"]),
                dishName = Convert.ToString(row["dishName"]),
                dishIndex = Convert.ToInt32(row["dishIndex"]),
                dishContent = Convert.ToString(row["dishContent"]),
                dishImageUrl = Convert.ToString(row["dishImageUrl"]),
                createTime = Convert.ToDateTime(row["createTime"]),
                isDelete = Convert.ToBoolean(row["isDelete"]),
                status = Convert.ToBoolean(row["status"])
            };
            return dish;
        }

        public DataTable IndexIsClash(int channelID, int index)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select * from ShopChannelDish where shopChannelID={0} and dishIndex={1}", channelID, index);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
    }
}
