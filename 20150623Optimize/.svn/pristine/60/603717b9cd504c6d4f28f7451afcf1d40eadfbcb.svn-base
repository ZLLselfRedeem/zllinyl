using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;
using System.Data;
using VAGastronomistMobileApp.Model.HomeNew;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 商家广告管理
    /// </summary>
    public class AdvertManager
    {
        /// <summary>
        /// 新增商铺广告信息
        /// </summary>
        /// <param name="County"></param>
        /// <returns></returns>
        public int InsertAdvertShop(AdvertShop advertShop)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append(@"INSERT INTO [dbo].[AdvertShop]
                                       ([cityID]
                                       ,[shopID]
                                       ,[firstTitleID]
                                       ,[secondTitleID]
                                       ,[index]
                                       ,[title]
                                       ,[subtitle]
                                       ,[yuanImageUrl]
                                       ,[status]
                                       ,[createTime]
                                       ,[createBy]
                                       ,[isDelete])
                                 VALUES(@cityID,@shopID,@firstTitleID,@secondTitleID,@index,@title,@subtitle,@yuanImageUrl,@status,@createTime,@createBy,@isDelete) ");


                    SqlParameter[] parameters = {
                        new SqlParameter("@cityID", SqlDbType.Int,4),
                        new SqlParameter("@shopID", SqlDbType.Int,4),
					    new SqlParameter("@firstTitleID", SqlDbType.Int,4),
                        new SqlParameter("@secondTitleID", SqlDbType.Int,4),
                        new SqlParameter("@index", SqlDbType.Int,4),
                        new SqlParameter("@title", SqlDbType.NVarChar,50),
                        new SqlParameter("@subtitle", SqlDbType.NVarChar,200),
                        new SqlParameter("@yuanImageUrl", SqlDbType.NVarChar,100),
                        new SqlParameter("@status", SqlDbType.Int,4),
                        new SqlParameter("@createTime", SqlDbType.DateTime),
                        new SqlParameter("@createBy", SqlDbType.NVarChar,30),
                        new SqlParameter("@isDelete", SqlDbType.Bit)};
                    parameters[0].Value = advertShop.cityID;
                    parameters[1].Value = advertShop.shopID;
                    parameters[2].Value = advertShop.firstTitleID;
                    parameters[3].Value = advertShop.secondTitleID;
                    parameters[4].Value = advertShop.index;
                    parameters[5].Value = advertShop.title;
                    parameters[6].Value = advertShop.subtitle;
                    parameters[7].Value = advertShop.yuanImageUrl;
                    parameters[8].Value = advertShop.status;
                    parameters[9].Value = advertShop.createTime;
                    parameters[10].Value = advertShop.createBy;
                    parameters[11].Value = advertShop.isDelete;

                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(obj);
                }
            }
        }

        /// <summary>
        /// 删除商店广告
        /// </summary>
        /// <param name="CountyID"></param>
        public bool DeleteAdvert(int advertID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update AdvertShop set isDelete=1 where ID=@id");
                    SqlParameter[] parameters = {					
					new SqlParameter("@id", SqlDbType.Int,4)};
                    parameters[0].Value = advertID;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch
                {

                }
                if (result == 1)
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
        /// 修改商铺广告信息
        /// </summary>
        /// <param name="advertShop"></param>
        /// <returns></returns>
        public bool UpdateAdvertShop(AdvertShop advertShop)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"UPDATE [dbo].[AdvertShop]
                               SET [cityID] = @cityID
                                  ,[shopID] = @shopID
                                  ,[firstTitleID] = @firstTitleID
                                  ,[secondTitleID] = @secondTitleID
                                  ,[index] = @index
                                  ,[title] =@title
                                  ,[subtitle] = @subtitle
                                  ,[yuanImageUrl]=@yuanImageUrl
                                  ,[status] = @status
                                  ,[createTime] =@createTime
                                  ,[createBy] =@createBy
                                  ,[isDelete] =@isDelete
                             WHERE id=@id");
            SqlParameter[] parameters = {
                        new SqlParameter("@cityID", SqlDbType.Int,4),
                        new SqlParameter("@shopID", SqlDbType.Int,4),
					    new SqlParameter("@firstTitleID", SqlDbType.Int,4),
                        new SqlParameter("@secondTitleID", SqlDbType.Int,4),
                        new SqlParameter("@index", SqlDbType.Int,4),
                        new SqlParameter("@title", SqlDbType.NVarChar,50),
                        new SqlParameter("@subtitle", SqlDbType.NVarChar,200),
                        new SqlParameter("@yuanImageUrl", SqlDbType.NVarChar,100),
                        new SqlParameter("@status", SqlDbType.Int,4),
                        new SqlParameter("@createTime", SqlDbType.DateTime),
                        new SqlParameter("@createBy", SqlDbType.NVarChar,30),
                        new SqlParameter("@isDelete", SqlDbType.Bit),
                        new SqlParameter("@id", SqlDbType.Int,4) };
            parameters[0].Value = advertShop.cityID;
            parameters[1].Value = advertShop.shopID;
            parameters[2].Value = advertShop.firstTitleID;
            parameters[3].Value = advertShop.secondTitleID;
            parameters[4].Value = advertShop.index;
            parameters[5].Value = advertShop.title;
            parameters[6].Value = advertShop.subtitle;
            parameters[7].Value = advertShop.yuanImageUrl;
            parameters[8].Value = advertShop.status;
            parameters[9].Value = advertShop.createTime;
            parameters[10].Value = advertShop.createBy;
            parameters[11].Value = advertShop.isDelete;
            parameters[12].Value = advertShop.id;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool UpdateAdvertShopStatus(int advertID,int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"UPDATE [dbo].[AdvertShop]
                               SET [status] = @status
                             WHERE id=@id");
            SqlParameter[] parameters = {
                        new SqlParameter("@status", SqlDbType.Int,4),
                        new SqlParameter("@id", SqlDbType.Int,4) };
            parameters[0].Value = status;
            parameters[1].Value = advertID;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查询商户广告信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAdvert(int advertID=0)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id],[cityID],[shopID],[firstTitleID],[secondTitleID],[index],[title] ,[subtitle],[yuanImageUrl],[status],[createTime],[createBy],[isDelete]");
            strSql.Append(" from AdvertShop where 1=1");
            if(advertID>0)
            {
                strSql.Append(string.Format(" and id={0} ",advertID));
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据条件查询商户广告信息
        /// </summary>
        /// <param name="cityID"></param>
        /// <param name="firstTitleID"></param>
        /// <param name="secondTitleID"></param>
        /// <param name="shopName"></param>
        /// <returns></returns>
        public List<AdvertShop> SelectAdvertByKey(int cityID,int firstTitleID,int secondTitleID,string shopName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.[id],a.[cityID],a.[shopID],a.[firstTitleID],f.titleName as firstTitleName,a.[secondTitleID],s.titleName as secondTitleName,[index],[title] ,[subtitle],[yuanImageUrl],a.[status] ,a.[createTime],a.[createBy],a.[isDelete]");
            strSql.Append(" from AdvertShop a left join ShopInfo b on a.shopID=b.shopID join HomeFirstTitle f on f.id = a.firstTitleID join HomeSecondTitle s on a.secondTitleID = s.id where a.isdelete=0");
            if(cityID>0)
            {
                strSql.Append(string.Format(" and a.cityID={0} ",cityID));
            }
            if (firstTitleID > 0)
            {
                strSql.Append(string.Format(" and a.firstTitleID={0} ",firstTitleID));
            }
            if (secondTitleID > 0)
            {
                strSql.Append(string.Format(" and a.secondTitleID={0} ",secondTitleID));
            }
            if (!string.IsNullOrEmpty(shopName))
            {
                strSql.Append(string.Format(" and b.shopName like '%{0}%' ",shopName));
            }
            strSql.Append(" order by a.createTime desc,a.[index] asc ");
            var list = new List<AdvertShop>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()))
            {
                while (dr.Read())
                {
                    list.Add(new AdvertShop()
                    {
                        id=Convert.ToInt32(dr["id"]),
                        cityID = Convert.ToInt32(dr["cityID"]),
                        shopID = Convert.ToInt32(dr["shopID"]),
                        firstTitleID = Convert.ToInt32(dr["firstTitleID"]),
                        firstTitleName = Convert.ToString(dr["firstTitleName"]),
                        secondTitleID = Convert.ToInt32(dr["secondTitleID"]),
                        secondTitleName = Convert.ToString(dr["secondTitleName"]),
                        index = Convert.ToInt32(dr["index"]),
                        title = Convert.ToString(dr["title"]),
                        subtitle = Convert.ToString(dr["subtitle"]),
                        yuanImageUrl = Convert.ToString(dr["yuanImageUrl"]),
                        status = Convert.ToInt32(dr["status"]),
                        createTime = Convert.ToDateTime(dr["createTime"]),
                        createBy = Convert.ToString(dr["createBy"])
                    });
                }
            }
            return list;
        }
        /// <summary>
        /// 检查对应广告的排序是否存在
        /// </summary>
        /// <param name="newIndex"></param>
        /// <param name="advertID"></param>
        /// <param name="cityID"></param>
        /// <returns></returns>
        public bool CheckHasAdvertIndex(int newIndex, string secondTitleID, int cityID)
        {
            string strSql = @"select [id]  from AdvertShop where isdelete=0  and cityID={0} and [index]={1} and status=1 and secondTitleID={2}";
            strSql = string.Format(strSql, cityID, newIndex,secondTitleID);
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
    }
}
