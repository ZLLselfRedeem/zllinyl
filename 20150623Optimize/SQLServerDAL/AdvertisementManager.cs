using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;
using System.Data;
//
//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2012-04-10.
//
namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class AdvertisementManager
    {
        /// <summary>
        /// 根据传入的更新值和需要更新的id更新广告的显示数量
        /// </summary>
        /// <param name="adverttisementConnAdColumnId">(1,2,3)</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool UpdateAdvertisementDisplayCount(string adverttisementConnAdColumnId, int value)
        {
            string strSql = String.Format(@"update AdvertisementConnAdColumn set displayCount = isnull(displayCount, 0) + @value where id in {0}", adverttisementConnAdColumnId);
                SqlParameter[] parameters = { new SqlParameter("@value", SqlDbType.Int, 4) };
                parameters[0].Value = value;
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters) > 0;
            }

        /// <summary>
        /// 新增一条广告记录
        /// </summary>
        /// <returns></returns>
        public long InsertNewAdvertisement(AdvertisementInfo adver)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into [AdvertisementInfo](");
                strSql.Append("[name],[imageURL],[status],[advertisementType],[value],[advertisementDescription],[webAdvertisementUrl],[advertisementClassify])");
                strSql.Append(" values (");
                strSql.Append("@name,@imageURL,@status,@advertisementType,@value,@advertisementDescription,@webAdvertisementUrl,@advertisementClassify)");
                strSql.Append(" select @@identity");//返回的主键就是广告的Id
                SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,500),
					new SqlParameter("@imageURL", SqlDbType.NVarChar,500),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@advertisementType", SqlDbType.TinyInt,1),
					new SqlParameter("@value", SqlDbType.NVarChar,500),
                    new SqlParameter("@advertisementDescription", SqlDbType.NVarChar,300),
                    new SqlParameter("@webAdvertisementUrl", SqlDbType.NVarChar,300),
                    new SqlParameter("@advertisementClassify", SqlDbType.TinyInt,1)};
                parameters[0].Value = adver.name;
                parameters[1].Value = adver.imageURL;
                parameters[2].Value = adver.status;
                parameters[3].Value = adver.advertisementType;
                parameters[4].Value = adver.value;
                parameters[5].Value = adver.advertisementDescription;
                parameters[6].Value = adver.webAdvertisementUrl;
                parameters[7].Value = adver.advertisementClassify;
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt64(obj);
                }
            }
        }

        /// <summary>
        /// 按照公司id查询其所属广告
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAdByCompanyId(int companyId, int adClassify)
        {

            StringBuilder strSql = new StringBuilder();
            SqlParameter[] param ={
                                      new SqlParameter("@companyId",companyId)     
                                  };
            strSql.Append("SELECT [id],[name],[advertisementType]");
            strSql.Append(" from [AdvertisementInfo]");
            strSql.Append("  where value =@companyId and advertisementType !=" + (int)VAAdvertisementType.COUPON + " and [status]=" + (int)VAAdvertisementStatus.AVAILABLE);
            if (adClassify == (int)VAAdvertisementClassify.INDEX_AD)
            {
                strSql.Append(" and advertisementClassify = " + (int)VAAdvertisementClassify.INDEX_AD + "");
            }
            else
            {
                strSql.Append(" and advertisementClassify = " + (int)VAAdvertisementClassify.FOODPLAZA_AD + "");
            }
            strSql.Append(" union");
            strSql.Append(" SELECT [id],[name],[advertisementType]");
            strSql.Append(" from [AdvertisementInfo] as A");
            strSql.Append("  inner join CouponInfo as B on A.value=B.couponID");
            strSql.Append(" where B.companyId =@companyId and advertisementType=" + (int)VAAdvertisementType.COUPON + " and A.[status]=" + (int)VAAdvertisementStatus.AVAILABLE);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
            return ds.Tables[0];
        }

        public AdvertisementInfo GetAdvertisementInfoByID(int AdvertisementID)
        {
            StringBuilder strSql = new StringBuilder();
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@id", AdvertisementID));
            strSql.Append("SELECT [id] ,[name] ,[imageURL] ");
            strSql.Append(",[status] ");
            strSql.Append(",[advertisementType] ");
            strSql.Append(",[value] ");
            strSql.Append(",[advertisementDescription] ");
            strSql.Append(",[webAdvertisementUrl] ");
            strSql.Append(",[advertisementClassify] ");
            strSql.Append("FROM [VAGastronomistMobileApp].[dbo].[AdvertisementInfo] WHERE [id] = @id");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), sqlParameterList.ToArray());
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                AdvertisementInfo advertisementInfo = new AdvertisementInfo()
                {
                    id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString()),
                    name = ds.Tables[0].Rows[0]["name"].ToString(),
                    imageURL = ds.Tables[0].Rows[0]["imageURL"].ToString(),
                    status = int.Parse(ds.Tables[0].Rows[0]["status"].ToString()),
                    advertisementType = int.Parse(ds.Tables[0].Rows[0]["advertisementType"].ToString()),
                    value = ds.Tables[0].Rows[0]["value"].ToString(),
                    advertisementDescription = ds.Tables[0].Rows[0]["advertisementDescription"].ToString(),
                    webAdvertisementUrl = ds.Tables[0].Rows[0]["webAdvertisementUrl"].ToString(),
                    advertisementClassify = int.Parse(ds.Tables[0].Rows[0]["advertisementClassify"].ToString())
                };
                return advertisementInfo;
            }
            return null;
        }
        /*************************************
         * Added by 林东宇 2014-11-12
         * 依据查询条件查询广告信息
         * *************************************/
        /// <summary>
        /// 查询广告
        /// </summary>
        /// <returns>输出列：广告ID,广告名称,广告类型,所属门店，广告分类</returns>
        public DataTable SelectAdvertisement(AdvertisementInfoQueryObject queryObject)
        {

            StringBuilder strSql = new StringBuilder();
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            strSql.Append("SELECT [id],[name],[advertisementType],[value],[advertisementClassify]");
            strSql.Append(" FROM [AdvertisementInfo] info");
            strSql.Append("  WHERE 1=1  ");

            if (queryObject.id.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@id", queryObject.id.Value));
                strSql.Append(" AND info.[id] = @id ");
            }
            if (queryObject.status.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@status", queryObject.status.Value));
                strSql.Append(" AND info.[status] = @status ");
            }
            if (!string.IsNullOrEmpty(queryObject.nameEqual))
            {
                sqlParameterList.Add(new SqlParameter("@nameEqual", queryObject.nameEqual));
                strSql.Append(" AND info.[name] =  @nameEqual  ");
            }
            if (!string.IsNullOrEmpty(queryObject.name))
            {
                sqlParameterList.Add(new SqlParameter("@name","%" + queryObject.name+ "%"  ));
                strSql.Append(" AND info.[name] like  @name  ");
            }
            if (!string.IsNullOrEmpty(queryObject.value))
            {
                sqlParameterList.Add(new SqlParameter("@value", queryObject.value));
                strSql.Append(" AND info.[value] = @value ");
            }
            if ( queryObject.advertisementClassify.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@advertisementClassify", queryObject.advertisementClassify));
                strSql.Append(" AND info.[advertisementClassify] = @advertisementClassify ");
            }
            if (queryObject.advertisementType.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@advertisementType", queryObject.advertisementType));
                strSql.Append(" AND info.[advertisementType] = @advertisementType ");
            }
            if (queryObject.cityID.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@cityID", queryObject.cityID));
                strSql.Append(" AND EXISTS (SELECT NULL FROM  AdvertisementConnAdColumn C WHERE info.id = C.[advertisementId] AND C.cityID = @cityID )");
            }
            strSql.Append(" ORDER BY  info.[id] DESC ");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), sqlParameterList.ToArray());
            return ds.Tables[0];
        }

        /*************************************
        * Added by 林东宇 2014-11-12
        * 依据查询条件查询广告信息
        * *************************************/
        /// <summary>
        /// 查询广告条数
        /// </summary>
        /// <returns>广告条数</returns>
        public int SelectAdvertisementCount(AdvertisementInfoQueryObject queryObject)
        {

            StringBuilder strSql = new StringBuilder();
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();

            strSql.Append("SELECT COUNT(0) ");
            strSql.Append(" FROM [AdvertisementInfo] info");
            strSql.Append("  WHERE  [status]=" + (int)VAAdvertisementStatus.AVAILABLE + " ");
            if (!string.IsNullOrEmpty(queryObject.name))
            {
                sqlParameterList.Add(new SqlParameter("@name", "%" + queryObject.name + "%"));
                strSql.Append(" AND info.[name] like  @name ");
            }
            if (!string.IsNullOrEmpty(queryObject.value))
            {
                sqlParameterList.Add(new SqlParameter("@value", queryObject.value));
                strSql.Append(" AND info.[value] = @value ");
            }
            if (queryObject.advertisementClassify.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@advertisementClassify", queryObject.advertisementClassify));
                strSql.Append(" AND info.[advertisementClassify] = @advertisementClassify ");
            }
            if (queryObject.advertisementType.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@advertisementType", queryObject.advertisementType));
                strSql.Append(" AND info.[advertisementType] = @advertisementType ");
            }
            if (queryObject.cityID.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@cityID", queryObject.cityID));
                strSql.Append(" AND EXISTS (SELECT NULL FROM  AdvertisementConnAdColumn C WHERE info.id = C.[advertisementId] AND C.cityID = @cityID )");
            }
            if (queryObject.id.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@id", queryObject.id.Value));
                strSql.Append(" AND info.[id] = @id ");
            }
            object count = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), sqlParameterList.ToArray());
            return int.Parse(count.ToString());
        }
        /// <summary>
        /// 按照广告类型查询其banner
        /// </summary>
        /// <returns></returns>
        public DataTable SelectBanner(int adType)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] param ={
                                      new SqlParameter("@adType",adType)     
                                  };
            strSql.Append("SELECT [id],[advertisementColumnName]");
            strSql.Append(" from [AdvertisementColumn]");
            strSql.Append("  where advertisementAreaId =@adType");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
            return ds.Tables[0];
        }

        public int GetSopIDByAdvertisement(int AdvertisementID)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] param ={
                                      new SqlParameter("@AdvertisementID",AdvertisementID)     
                                  };
            strSql.Append("SELECT TOP 1  [shopId] ");
            strSql.Append(" from [AdvertisementConnShop]");
            strSql.Append("  where advertisementId =@AdvertisementID ");

            object  id = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), param);
            if (id != null)
            {
                return int.Parse(id.ToString());
            }
            return 0;
        }
        /******************************************
         * Added by 林东宇 2014-11-13
         * 依据查询条件获取广告的排期信息
         * ******************************************/
        /// <summary>
        ///  查询所有广告排期信息 
        /// </summary>
        /// <returns>输出列：广告排期ID、广告名称、排期开始时间、排期截至时间、排期状态、城市名称</returns>
        public DataTable SelectBannerDetailInfo(AdvertisementInfoQueryObject queryObject)
        {
            StringBuilder strSql = new StringBuilder();
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            strSql.Append("SELECT ");
            strSql.Append(" AdvertisementConnAdColumn.id,AdvertisementConnAdColumn.name,[advertisementId],[displayStartTime],[displayEndTime],AdvertisementConnAdColumn.[status],city.cityName");
            strSql.Append(" FROM [AdvertisementConnAdColumn]");
            strSql.Append(" INNER JOIN AdvertisementInfo info ON info.id=AdvertisementConnAdColumn.advertisementId ");
            strSql.Append(" INNER JOIN city ON city.cityid=AdvertisementConnAdColumn.cityID ");
            strSql.Append(" WHERE 1=1 ");
            if (queryObject.id.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@id", queryObject.id.Value));
                strSql.Append(" AND info.[id] = @id ");
            }
            if (queryObject.status.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@status", queryObject.status.Value));
                strSql.Append(" AND AdvertisementConnAdColumn.[status] = @status ");
            }
            if (!string.IsNullOrEmpty(queryObject.name))
            {
                sqlParameterList.Add(new SqlParameter("@name", "%" + queryObject.name + "%"));
                strSql.Append(" AND info.[name] like  @name ");
            }
            if (!string.IsNullOrEmpty(queryObject.value))//门店ID
            {
                sqlParameterList.Add(new SqlParameter("@value", queryObject.value));
                strSql.Append(" AND info.[value] = @value ");
            }
            if (queryObject.advertisementClassify.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@advertisementClassify", queryObject.advertisementClassify.Value));
                strSql.Append(" AND info.[advertisementClassify] = @advertisementClassify ");
            }
            if (queryObject.advertisementType.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@advertisementType", queryObject.advertisementType.Value));
                strSql.Append(" AND info.[advertisementType] = @advertisementType ");
            }
            if (queryObject.cityID.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@cityID", queryObject.cityID.Value));
                strSql.Append(" AND AdvertisementConnAdColumn.cityID = @cityID ");
            }
            if (queryObject.advertisementColumnId.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@advertisementColumnId", queryObject.advertisementColumnId.Value));
                strSql.Append(" AND AdvertisementConnAdColumn.advertisementColumnId = @advertisementColumnId ");
            }
            if (queryObject.IntervalStart.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@displayStartTime", queryObject.IntervalStart.Value));
                strSql.Append(" AND AdvertisementConnAdColumn.displayStartTime <= @displayStartTime ");
                strSql.Append(" AND AdvertisementConnAdColumn.displayEndTime >= @displayStartTime ");
            }
            if (queryObject.IntervalEnd.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@displayEndTime", queryObject.IntervalEnd.Value));
                strSql.Append(" AND AdvertisementConnAdColumn.displayEndTime >= @displayEndTime ");
                strSql.Append(" AND AdvertisementConnAdColumn.displayStartTime <= @displayEndTime ");
            }
            strSql.Append(" order by AdvertisementConnAdColumn.id desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(),sqlParameterList.ToArray());
            return ds.Tables[0];
        }
        /// <summary>
        /// （wangcheng）查询所有广告信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectBannerDetailInfo(int cityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT AdvertisementConnAdColumn.id,AdvertisementConnAdColumn.name,[advertisementId],[displayStartTime],[displayEndTime],AdvertisementConnAdColumn.[status]");
            strSql.Append(" from [AdvertisementConnAdColumn]");
            strSql.Append(" inner join AdvertisementInfo on AdvertisementInfo.id=AdvertisementConnAdColumn.advertisementId");
            if (cityId != 0)
            {
                strSql.AppendFormat(" where AdvertisementConnAdColumn.cityId='{0}'", cityId);
            }
            strSql.Append(" order by AdvertisementConnAdColumn.id desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// （wangcheng）修改当前客户端展示广告状态（开启和关闭客户端广告）
        /// </summary>
        /// <returns></returns>
        public bool UpdateAdvertisementConnAdColumnStatus(long id, int status)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update [AdvertisementConnAdColumn] set ");
                    strSql.AppendFormat("status = {0}", status);
                    strSql.AppendFormat(" where id = {0}", id);
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString());
                }
                catch (System.Exception)
                {
                    return false;
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
        /// （wangcheng）修改当前客户端展示广告展示时间
        /// </summary>
        /// <returns></returns>
        public bool UpdateAdvertisementConnAdColumnTime(long id, DateTime displayStartTime, DateTime displayEndTime)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update [AdvertisementConnAdColumn] set ");
                    strSql.AppendFormat(" displayStartTime = '{0}',displayEndTime='{1}'", displayStartTime, displayEndTime);
                    strSql.AppendFormat(" where id = {0}", id);
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString());
                }
                catch (System.Exception)
                {
                    return false;
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
        /// 新增一条广告展示记录
        /// </summary>
        /// <returns></returns>
        public bool InsertNewAdvertisementConnBanner(AdvertisementConnAdColumnInfo adver)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into AdvertisementConnAdColumn(");
                strSql.Append("name,advertisementColumnId,advertisementId,displayStartTime,displayEndTime,status,cityId)");
                strSql.Append(" values (");
                strSql.Append("@name,@advertisementColumnId,@advertisementId,@displayStartTime,@displayEndTime,@status,@cityId)");

                SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,500),
					new SqlParameter("@advertisementColumnId", SqlDbType.Int,4),
					new SqlParameter("@advertisementId", SqlDbType.BigInt,8),
					new SqlParameter("@displayStartTime", SqlDbType.DateTime),
					new SqlParameter("@displayEndTime", SqlDbType.DateTime),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
                    //插入cityId信息
                    new SqlParameter("@cityId", SqlDbType.Int,4)
					};
                parameters[0].Value = adver.name;
                parameters[1].Value = adver.advertisementColumnId;
                parameters[2].Value = adver.advertisementId;
                parameters[3].Value = adver.displayStartTime;
                parameters[4].Value = adver.displayEndTime;
                parameters[5].Value = adver.status;

                parameters[6].Value = adver.cityId;

                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                if (result > 0)
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
        /// 新增广告id和门店id对应表
        /// </summary>
        /// <returns></returns>
        public bool InsertNewAdvertisementConnShopId(long shopId, long advertisementId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {


                SqlParameter[] parameters = { 
					new SqlParameter("@advertisementId", SqlDbType.BigInt)
					}; 
                parameters[0].Value = advertisementId;
                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, "DELETE FROM AdvertisementConnShop WHERE advertisementId = @advertisementId", parameters);
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into AdvertisementConnShop(");
                strSql.Append("shopId,advertisementId)");
                strSql.Append(" values (");
                strSql.Append("@shopId,@advertisementId)");

               parameters = new []{
					new SqlParameter("@shopId", SqlDbType.BigInt),
					new SqlParameter("@advertisementId", SqlDbType.BigInt)
					};
                parameters[0].Value = shopId;
                parameters[1].Value = advertisementId;
                result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                if (result > 0)
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
        /// 根据广告编号查询SHOP列表 add by jlf on 20130816
        /// </summary>
        /// <param name="advertisementid"></param>
        /// <returns></returns>
        public List<int> GetShopIdbyAdvertisementId(int advertisementid)
        {
            List<int> shopidlist = new List<int>();
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("SELECT  shopId  from AdvertisementConnShop where advertisementId ={0}", advertisementid);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                shopidlist.Add(Convert.ToInt32(ds.Tables[0].Rows[i]["shopId"].ToString()));
            }
            return shopidlist;
        }

        /// <summary>
        /// 获取列表中有该栏位的广告
        /// </summary>
        /// <param name="adver"></param>
        /// <returns></returns>
        public DataTable GetAdvertisementConnToBanner(int adver)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select * from AdvertisementConnAdColumn where advertisementColumnId ={0}", adver);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        public void DelAdvertisementConnToBanner(int advertisementColumnId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                SqlTransaction tran = null;

                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("update AdvertisementConnAdColumn set status ='{0}' where advertisementColumnId= {1}", "0", advertisementColumnId);
                    SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString());

                    tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                }
            }

        }
       /**************************
        * Added By 林东宇 at 2014-11-28
        * 
        * ***********************/
        /// <summary>
        /// 更新一广告条记录
        /// </summary>
        /// <param name="adver"></param>
        public bool UpdateAdvertisement(AdvertisementInfo adver)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("UPDATE [AdvertisementInfo] SET ");
                strSql.Append("[name]=@name,[imageURL]=@imageURL,[status]=@status,[advertisementType]=@advertisementType,[value]=@value," +
                "[advertisementDescription] =@advertisementDescription,[webAdvertisementUrl]=@webAdvertisementUrl,[advertisementClassify]=@advertisementClassify "); 
                strSql.Append(" WHERE [id] = @ID "); 
                SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,500),
					new SqlParameter("@imageURL", SqlDbType.NVarChar,500),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@advertisementType", SqlDbType.TinyInt,1),
					new SqlParameter("@value", SqlDbType.NVarChar,500),
                    new SqlParameter("@advertisementDescription", SqlDbType.NVarChar,300),
                    new SqlParameter("@webAdvertisementUrl", SqlDbType.NVarChar,300),
                    new SqlParameter("@advertisementClassify", SqlDbType.TinyInt,1),
                    new SqlParameter("@ID", SqlDbType.BigInt)};
                parameters[0].Value = adver.name;
                parameters[1].Value = adver.imageURL;
                parameters[2].Value = adver.status;
                parameters[3].Value = adver.advertisementType;
                parameters[4].Value = adver.value;
                parameters[5].Value = adver.advertisementDescription;
                parameters[6].Value = adver.webAdvertisementUrl;
                parameters[7].Value = adver.advertisementClassify;
                parameters[8].Value = adver.id;
                int count = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                if (count > 0)
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
