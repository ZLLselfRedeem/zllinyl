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
    public class ShopSequenceManager
    {
        /// <summary>
        /// 新增年夜饭套餐门店排序
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool AddShopSequence(ShopSequence entity)
        {
            const string strSql = @"INSERT INTO [VAGastronomistMobileApp].[dbo].[shopSequence] ([type],[shopId],[sequenceNumber]) VALUES (@type,@shopId,@sequenceNumber);SELECT @@identity";
            SqlParameter[] parameters = {					
					new SqlParameter("@type", entity.type),					
					new SqlParameter("@shopId", entity.shopId),					
					new SqlParameter("@sequenceNumber", entity.sequenceNumber) };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                var returnValue = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, parameters);
                return Convert.ToInt32(returnValue) > 0;
            }
        }
        /// <summary>
        /// 判断是否门店排序表是否有当前门店数据
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool ExistShopSequence(int shopId)
        {
            const string strSql = @"select COUNT(id) from shopSequence where shopId=@shopId";
            SqlParameter[] parameters = {					
					new SqlParameter("@shopId", shopId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameters))
            {
                if (dr.Read())
                {
                    return dr[0] == DBNull.Value ? false : Convert.ToInt32(dr[0]) > 0;
                }
            }
            return false;
        }

        /// <summary>
        /// 查询年夜饭门店排序信息
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<ShopSequenceMedel> GetShopSequence(int type)
        {
            const string strSql = @"select distinct A.ShopID,B.shopName,C.sequenceNumber from Meal A
left join ShopInfo B on A.ShopID=B.shopID
left join ShopSequence C on A.ShopID=C.shopId
where B.shopStatus=1 and B.isHandle=1 and C.type=@type ";
            SqlParameter[] parameters = {					
					new SqlParameter("@type", type) };
            var list = new List<ShopSequenceMedel>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameters))
            {
                while (dr.Read())
                {
                    list.Add(new ShopSequenceMedel()
                    {
                        sequenceNumber = dr["sequenceNumber"] == DBNull.Value ? 99999 : Convert.ToInt32(dr["sequenceNumber"]),
                        shopId = dr["ShopID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["ShopID"]),
                        shopName = dr["shopName"] == DBNull.Value ? "未知门店" : Convert.ToString(dr["shopName"]),
                    });
                }
            }
            return list;
        }
        /// <summary>
        /// 批量新增年夜饭套餐门店排序
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool BatchAddShopSequence(DataTable dt)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    SqlTransaction sqlbulkTransaction = conn.BeginTransaction();
                    SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.UseInternalTransaction, null);
                    sqlbulkcopy.DestinationTableName = "ShopSequence";
                    sqlbulkcopy.BulkCopyTimeout = 30;
                    sqlbulkcopy.ColumnMappings.Add("type", "type");
                    sqlbulkcopy.ColumnMappings.Add("shopId", "shopId");
                    sqlbulkcopy.ColumnMappings.Add("sequenceNumber", "sequenceNumber");
                    sqlbulkcopy.WriteToServer(dt);
                    sqlbulkTransaction.Commit();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 批量删除年夜饭门店排序
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool DeleteShopSequenceByType(int type)
        {
            const string strSql = @"delete from [VAGastronomistMobileApp].[dbo].[shopSequence] where type=@type;";
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                SqlParameter[] parameters = { new SqlParameter("@type", type) };
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters) > 0;
            }
        }
    }
}
