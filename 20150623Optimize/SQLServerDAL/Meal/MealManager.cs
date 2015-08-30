using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class MealManager
    {
        /// <summary>
        /// 处理用户未在规定时间内支付年夜饭点单服务
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool UpdateNotPayMealOrderManager(double value)
        {
            const string strSql = @"update PreOrder19dian  
set status=104 where preOrder19dianId>270000 and status!=104 and ISNULL(isPaid,0)=0 
and preOrderTime<dateadd(mi,@value,getdate()) 
and preOrder19dianId in (select preOrder19dianId from MealConnPreOrder) ";
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                SqlParameter[] parameters = {					
					new SqlParameter("@value", value)};
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, parameters) > 0;
            }
        }
        /// <summary>
        /// 处理用户未在规定时间内支付，修改套餐剩余份数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool UpdateMealScheduleCanBuy(int mealScheduleID, int count)
        {
            const string strSql = @"update MealSchedule  
set SoldCount=Isnull(SoldCount,0)-@count1 
where MealScheduleID =@mealScheduleID and (Isnull(SoldCount,0)-@count2 )>=0";
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                SqlParameter[] parameters = {					
					new SqlParameter("@mealScheduleID", mealScheduleID),
                    new SqlParameter("@count1", count),                                            
                    new SqlParameter("@count2", count)
                };
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, parameters) > 0;
            }
        }

        public DataTable SelectBackPreOrder(double value)
        {
            const string strSql = @"select A.MealScheduleID,COUNT(1) cnt from MealConnPreOrder A
inner join PreOrder19dian C on C.preOrder19dianId=A.preOrder19dianId
where C.preOrder19dianId>270000 and C.status!=104 and ISNULL(C.isPaid,0)=0 
and C.preOrderTime<dateadd(mi,@value,getdate()) 
group by A.MealScheduleID";
            SqlParameter[] parameters = { new SqlParameter("@value", value) };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameters);
            return ds.Tables[0];
        }

        public Meal GetEntityByID(int mealID)
        {
            string strSql =
               " SELECT  [MealID],[ShopID]  ,[Price] ,[MealName]  ,[Menu]  ,[Suggestion] ,[ImageURL]  ,[OriginalPrice] ,[IsActive],OrderNumber  FROM  [Meal] " +
               " WHERE [MealID] = @MealID";
            SqlParameter[] parameters = {					
					new SqlParameter("@MealID", mealID)};
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                var reader = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql, parameters);
                if (reader.Read())
                {
                    return reader.GetEntity<Meal>();
                }
            }
            return null;
        }

        public bool AddEntity(Meal entity)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" INSERT INTO  [Meal] ");
            sqlBuilder.Append(" ([ShopID] ");
            sqlBuilder.Append(" ,[Price] ");
            sqlBuilder.Append(" ,[MealName] ");
            sqlBuilder.Append(" ,[Menu] ");
            sqlBuilder.Append(",[Suggestion] ");
            sqlBuilder.Append(" ,[CreatedBy] ");
            sqlBuilder.Append(" ,[CreationDate] ");
            sqlBuilder.Append("  ,[ImageURL] ");
            sqlBuilder.Append("  ,[IsActive] ");
            sqlBuilder.Append(" ,[OriginalPrice]  ");
            sqlBuilder.Append(" ,[OrderNumber] ) ");
            sqlBuilder.Append(" VALUES ");
            sqlBuilder.Append(" (@ShopID ");
            sqlBuilder.Append(" ,@Price ");
            sqlBuilder.Append(" ,@MealName ");
            sqlBuilder.Append(" ,@Menu ");
            sqlBuilder.Append(" ,@Suggestion ");
            sqlBuilder.Append(" ,@CreatedBy ");
            sqlBuilder.Append(" ,@CreationDate ");
            sqlBuilder.Append("  ,@ImageURL ");
            sqlBuilder.Append("  ,@IsActive ");
            sqlBuilder.Append(" ,@OriginalPrice  ");
            sqlBuilder.Append(" ,@OrderNumber ) ");
            sqlBuilder.Append(" SELECT @@identity ");
            SqlParameter[] parameters = {					
					new SqlParameter("@ShopID", entity.ShopID),					
					new SqlParameter("@Price", entity.Price),					
					new SqlParameter("@MealName", entity.MealName),					
					new SqlParameter("@Menu", entity.Menu),					
					new SqlParameter("@Suggestion", entity.Suggestion),					
					new SqlParameter("@ImageURL", entity.ImageURL),							
					new SqlParameter("@CreatedBy", entity.CreatedBy),							
					new SqlParameter("@CreationDate", entity.CreationDate),					
					new SqlParameter("@OriginalPrice", entity.OriginalPrice),						
					new SqlParameter("@OrderNumber", entity.OrderNumber),					
					new SqlParameter("@IsActive", entity.IsActive) };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                var returnValue = SqlHelper.ExecuteScalar(conn, CommandType.Text, sqlBuilder.ToString(), parameters);
                int mealID;
                if (int.TryParse(returnValue.ToString(), out mealID))
                {
                    entity.MealID = mealID;
                    return true;
                }
                return false;
            }
        }

        public bool UpdateEntity(Meal entity)
        {
            StringBuilder sqlBuilder = new StringBuilder();
            sqlBuilder.Append(" UPDATE  Meal ");
            sqlBuilder.Append(" SET [ShopID] = @ShopID");
            sqlBuilder.Append("  ,[Price] = @Price");
            sqlBuilder.Append("  ,[MealName] = @MealName");
            sqlBuilder.Append(" ,[Menu] = @Menu");
            sqlBuilder.Append(" ,[Suggestion] = @Suggestion");
            sqlBuilder.Append("  ,[ImageURL] = @ImageURL");
            sqlBuilder.Append("  ,[OriginalPrice] = @OriginalPrice");
            sqlBuilder.Append("  ,[IsActive] = @IsActive");
            sqlBuilder.Append("  ,[OrderNumber] = @OrderNumber");
            sqlBuilder.Append(" WHERE   [MealID] = @MealID ");
            SqlParameter[] parameters = {					
					new SqlParameter("@ShopID", entity.ShopID),					
					new SqlParameter("@Price", entity.Price),					
					new SqlParameter("@MealName", entity.MealName),					
					new SqlParameter("@Menu", entity.Menu),					
					new SqlParameter("@Suggestion", entity.Suggestion),					
					new SqlParameter("@ImageURL", entity.ImageURL),					
					new SqlParameter("@OriginalPrice", entity.OriginalPrice),					
					new SqlParameter("@IsActive", entity.IsActive),	
			        new SqlParameter("@OrderNumber", entity.OrderNumber),	
					new SqlParameter("@MealID", entity.MealID) };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sqlBuilder.ToString(), parameters) > 0;
            }
        }

        public int GetCountByQuery(MealQueryObject queryObject)
        {
            StringBuilder sqlBuilder = new StringBuilder(" SELECT COUNT(0) FROM Meal WHERE 1=1 ");
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (queryObject.ShopID.HasValue)
            {
                sqlBuilder.Append(" AND ShopID=@ShopID ");
                parameters.Add(new SqlParameter("@ShopID", queryObject.ShopID.Value));
            }
            if (queryObject.IsActive.HasValue)
            {
                sqlBuilder.Append(" AND IsActive=@IsActive ");
                parameters.Add(new SqlParameter("@IsActive", queryObject.IsActive.Value));
            }
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                var returnValue = SqlHelper.ExecuteScalar(conn, CommandType.Text, sqlBuilder.ToString(), parameters.ToArray());
                int count;
                if (int.TryParse(returnValue.ToString(), out count))
                {
                    return count;
                }
                return 0;
            }
        }

        public List<Meal> GetListByQuery(MealQueryObject queryObject, int pageIndex, int pageSize)
        {
            StringBuilder sqlBuilder =
             new StringBuilder("SELECT * FROM ( SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) NUM, [MealID],[ShopID],[Price] ,[MealName],[Menu]," +
                           " [Suggestion],[ImageURL],[OriginalPrice] , [IsActive],OrderNumber  FROM  [Meal] WHERE 1=1 ");
            List<SqlParameter> parameters = new List<SqlParameter>();
            List<Meal> list = null;
            if (queryObject.ShopID.HasValue)
            {
                sqlBuilder.Append(" AND ShopID=@ShopID ");
                parameters.Add(new SqlParameter("@ShopID", queryObject.ShopID.Value));
            }
            if (queryObject.IsActive.HasValue)
            {
                sqlBuilder.Append(" AND IsActive=@IsActive ");
                parameters.Add(new SqlParameter("@IsActive", queryObject.IsActive.Value));
            }
            sqlBuilder.Append(" ) T WHERE T.NUM > @PageStart AND T.NUM <= @PageEnd");
            parameters.Add(new SqlParameter("@PageStart", (pageIndex - 1) * pageSize));
            parameters.Add(new SqlParameter("@PageEnd", pageIndex * pageSize));
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                list = new List<Meal>();
                var reader = SqlHelper.ExecuteReader(conn, CommandType.Text, sqlBuilder.ToString(), parameters.ToArray());
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<Meal>());
                }
            }
            return list;
        }

        public List<Meal> GetListByQuery(MealQueryObject queryObject)
        {
            StringBuilder sqlBuilder =
             new StringBuilder(" SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) NUM, [MealID],[ShopID],[Price] ,[MealName],[Menu]," +
                           " [Suggestion],[ImageURL],[OriginalPrice] , [IsActive],OrderNumber  FROM  [Meal] WHERE 1=1 ");
            List<SqlParameter> parameters = new List<SqlParameter>();
            List<Meal> list = null;
            if (queryObject.ShopID.HasValue)
            {
                sqlBuilder.Append(" AND ShopID=@ShopID ");
                parameters.Add(new SqlParameter("@ShopID", queryObject.ShopID.Value));
            }
            if (queryObject.IsActive.HasValue)
            {
                sqlBuilder.Append(" AND IsActive=@IsActive ");
                parameters.Add(new SqlParameter("@IsActive", queryObject.IsActive.Value));
            }
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                list = new List<Meal>();
                var reader = SqlHelper.ExecuteReader(conn, CommandType.Text, sqlBuilder.ToString(), parameters.ToArray());
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<Meal>());
                }
            }
            return list;
        }

        public DataTable GetMealTableByQuery(MealQueryObject queryObject, int pageIndex, int pageSize)
        {
            StringBuilder sqlBuilder = new StringBuilder("SELECT * FROM( SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) NUM ");
            sqlBuilder.Append(" ,A.[MealID]");
            sqlBuilder.Append(" ,A.[shopID]");
            sqlBuilder.Append(" ,A.[Price]");
            sqlBuilder.Append(" ,A.[MealName]");
            sqlBuilder.Append(" ,A.[Menu]");
            sqlBuilder.Append(" ,A.[Suggestion]");
            sqlBuilder.Append(" ,A.[ImageURL]");
            sqlBuilder.Append(" ,A.[OriginalPrice]");
            sqlBuilder.Append(" ,A.[IsActive]");
            sqlBuilder.Append(" ,A.[OrderNumber]");
            sqlBuilder.Append(" ,A.[CreatedBy]");
            sqlBuilder.Append(" ,A.[CreationDate]");
            sqlBuilder.Append(" ,A.[LastUpdatedBy]");
            sqlBuilder.Append(" ,A.[LastUpdateDate]");
            sqlBuilder.Append(" ,B.shopName");
            sqlBuilder.Append(" ,C.companyName");
            sqlBuilder.Append(" FROM  [Meal] A INNER JOIN  ShopInfo B ON A.ShopID = B.shopID INNER JOIN CompanyInfo C ON B.companyID = C.companyID ");
            sqlBuilder.Append(" WHERE 1=1 ");
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (queryObject.MealID.HasValue)
            {
                sqlBuilder.Append(" AND A.MealID=@MealID ");
                parameters.Add(new SqlParameter("@MealID", queryObject.MealID.Value));
            }
            if (queryObject.ShopID.HasValue)
            {
                sqlBuilder.Append(" AND A.ShopID=@ShopID ");
                parameters.Add(new SqlParameter("@ShopID", queryObject.ShopID.Value));
            }
            if (queryObject.IsActive.HasValue)
            {
                sqlBuilder.Append(" AND A.IsActive=@IsActive ");
                parameters.Add(new SqlParameter("@IsActive", queryObject.IsActive.Value));
            }
            if (queryObject.CompanyID.HasValue)
            {
                sqlBuilder.Append(" AND B.CompanyID=@CompanyID ");
                parameters.Add(new SqlParameter("@CompanyID", queryObject.CompanyID.Value));
            }
            if (queryObject.CityID.HasValue)
            {
                sqlBuilder.Append(" AND B.CityID=@CityID ");
                parameters.Add(new SqlParameter("@CityID", queryObject.CityID.Value));
            }
            if (!string.IsNullOrEmpty(queryObject.MealName))
            {
                sqlBuilder.Append(" AND A.MealName like @MealName ");
                parameters.Add(new SqlParameter("@MealName", string.Format("%{0}%", queryObject.MealName)));
            }
            if (queryObject.CreationDateFrom.HasValue)
            {
                sqlBuilder.Append(" AND A.CreationDate >= @CreationDateFrom ");
                parameters.Add(new SqlParameter("@CreationDateFrom", queryObject.CreationDateFrom.Value));
            }
            if (queryObject.CreationDateTo.HasValue)
            {
                sqlBuilder.Append(" AND A.CreationDate <= @CreationDateTo ");
                parameters.Add(new SqlParameter("@CreationDateTo", queryObject.CreationDateTo.Value));
            }
            sqlBuilder.Append(" ) T WHERE T.NUM > @PageStart AND T.NUM <= @PageEnd");
            parameters.Add(new SqlParameter("@PageStart", (pageIndex - 1) * pageSize));
            parameters.Add(new SqlParameter("@PageEnd", pageIndex * pageSize));
            var dataset = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlBuilder.ToString(), parameters.ToArray());
            if (dataset != null && dataset.Tables.Count > 0)
            {
                return dataset.Tables[0];
            }
            return null;
        }
        public int GetMealTableCountByQuery(MealQueryObject queryObject)
        {
            StringBuilder sqlBuilder = new StringBuilder(" SELECT COUNT(0) ");
            sqlBuilder.Append(" FROM  [Meal] A INNER JOIN  ShopInfo B ON A.ShopID = B.shopID INNER JOIN CompanyInfo C ON B.companyID = C.companyID ");
            sqlBuilder.Append(" WHERE 1=1 ");
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (queryObject.MealID.HasValue)
            {
                sqlBuilder.Append(" AND A.MealID=@MealID ");
                parameters.Add(new SqlParameter("@MealID", queryObject.MealID.Value));
            }
            if (queryObject.ShopID.HasValue)
            {
                sqlBuilder.Append(" AND A.ShopID=@ShopID ");
                parameters.Add(new SqlParameter("@ShopID", queryObject.ShopID.Value));
            }
            if (queryObject.IsActive.HasValue)
            {
                sqlBuilder.Append(" AND A.IsActive=@IsActive ");
                parameters.Add(new SqlParameter("@IsActive", queryObject.IsActive.Value));
            }
            if (queryObject.CompanyID.HasValue)
            {
                sqlBuilder.Append(" AND B.CompanyID=@CompanyID ");
                parameters.Add(new SqlParameter("@CompanyID", queryObject.CompanyID.Value));
            }
            if (queryObject.CityID.HasValue)
            {
                sqlBuilder.Append(" AND B.CityID=@CityID ");
                parameters.Add(new SqlParameter("@CityID", queryObject.CityID.Value));
            }
            if (!string.IsNullOrEmpty(queryObject.MealName))
            {
                sqlBuilder.Append(" AND A.MealName like @MealName ");
                parameters.Add(new SqlParameter("@MealName", string.Format("%{0}%", queryObject.MealName)));
            }
            if (queryObject.CreationDateFrom.HasValue)
            {
                sqlBuilder.Append(" AND A.CreationDate >= @CreationDateFrom ");
                parameters.Add(new SqlParameter("@CreationDateFrom", queryObject.CreationDateFrom.Value));
            }
            if (queryObject.CreationDateTo.HasValue)
            {
                sqlBuilder.Append(" AND A.CreationDate <= @CreationDateTo ");
                parameters.Add(new SqlParameter("@CreationDateTo", queryObject.CreationDateTo.Value));
            }
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                var returnValue = SqlHelper.ExecuteScalar(conn, CommandType.Text, sqlBuilder.ToString(), parameters.ToArray());
                int count;
                if (int.TryParse(returnValue.ToString(), out count))
                {
                    return count;
                }
                return 0;
            }

        }
        public DataTable GetMealTableByQuery(MealQueryObject queryObject)
        {
            StringBuilder sqlBuilder = new StringBuilder(" SELECT COUNT(0) ");
            sqlBuilder.Append(" FROM  [Meal] A INNER JOIN  ShopInfo B ON A.ShopID = B.shopID INNER JOIN CompanyInfo C ON B.companyID = C.companyID ");
            sqlBuilder.Append(" WHERE 1=1 ");
            List<SqlParameter> parameters = new List<SqlParameter>();
            if (queryObject.ShopID.HasValue)
            {
                sqlBuilder.Append(" AND A.ShopID=@ShopID ");
                parameters.Add(new SqlParameter("@ShopID", queryObject.ShopID.Value));
            }
            if (queryObject.IsActive.HasValue)
            {
                sqlBuilder.Append(" AND A.IsActive=@IsActive ");
                parameters.Add(new SqlParameter("@IsActive", queryObject.IsActive.Value));
            }
            if (queryObject.CityID.HasValue)
            {
                sqlBuilder.Append(" AND B.CityID=@CityID ");
                parameters.Add(new SqlParameter("@CityID", queryObject.CityID.Value));
            }
            if (queryObject.CompanyID.HasValue)
            {
                sqlBuilder.Append(" AND B.CompanyID=@CompanyID ");
                parameters.Add(new SqlParameter("@CompanyID", queryObject.CompanyID.Value));
            }
            if (!string.IsNullOrEmpty(queryObject.MealName))
            {
                sqlBuilder.Append(" AND A.MealName like @MealName ");
                parameters.Add(new SqlParameter("@MealName", string.Format("%{0}%", queryObject.MealName)));
            }
            if (queryObject.CreationDateFrom.HasValue)
            {
                sqlBuilder.Append(" AND A.CreationDate >= @CreationDateFrom ");
                parameters.Add(new SqlParameter("@CreationDateFrom", queryObject.CreationDateFrom.Value));
            }
            if (queryObject.CreationDateTo.HasValue)
            {
                sqlBuilder.Append(" AND A.CreationDate <= @CreationDateTo ");
                parameters.Add(new SqlParameter("@CreationDateTo", queryObject.CreationDateTo.Value));
            }
            var dataset = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlBuilder.ToString(), parameters.ToArray());
            if (dataset != null && dataset.Tables.Count > 0)
            {
                return dataset.Tables[0];
            }
            return null;
        }

        public static List<MealShopList> SelectMealShopList(Page page, int cityId, int tagId, out bool isMore)
        {
            List<MealShopList> shopList = new List<MealShopList>();

            string strCnt = @"select count(1)
from dbo.ShopWithTag a
inner join dbo.ShopTag b on a.TagNode.IsDescendantOf(b.TagNode)=1 and b.Enable=1 and b.TagLevel=1
inner join Meal m on m.ShopID=a.ShopId and m.IsActive=1
inner join ShopInfo s on m.ShopID = s.shopID and s.shopStatus=1 and s.isHandle=1
left join shopSequence ss on m.shopID=ss.shopId";

            string strSql = @"select a.ShopID,a.shopName from(
select row_number() OVER (ORDER BY ss.sequenceNumber) rownum,m.ShopID,s.shopName
from dbo.ShopWithTag a
inner join dbo.ShopTag b on a.TagNode.IsDescendantOf(b.TagNode)=1 and b.Enable=1 and b.TagLevel=1
inner join Meal m on m.ShopID=a.ShopId and m.IsActive=1
inner join ShopInfo s on m.ShopID = s.shopID and s.shopStatus=1 and s.isHandle=1";

            List<SqlParameter> paraList = new List<SqlParameter>();
            if (cityId > 0)
            {
                strSql += " and s.cityID=@cityId";
                strCnt += " and s.cityID=@cityId";
                paraList.Add(new SqlParameter("@cityId", SqlDbType.Int) { Value = cityId });
            }
            if (tagId > 0)
            {
                strSql += " and b.TagId=@tagId";
                strCnt += " and b.TagId=@tagId";
                paraList.Add(new SqlParameter("@tagId", SqlDbType.Int) { Value = tagId });
            }

            strSql += " left join shopSequence ss on m.shopID=ss.shopId group by m.ShopID,s.shopName,ss.sequenceNumber) a where a.rownum between @startIndex and @endIndex";

            SqlParameter[] paraCnt = paraList.ToArray();

            int cnt = 0;
            object objCnt = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strCnt, paraCnt);
            if (objCnt == null)
            {
                cnt = 0;
            }
            else
            {
                cnt = Convert.ToInt32(objCnt);
            }
            if (page.PageNumber * page.PageSize > cnt)
            {
                isMore = false;
            }
            else
            {
                isMore = true;
            }

            paraList.Add(new SqlParameter("@startIndex", SqlDbType.Int) { Value = page.Skip + 1 });
            paraList.Add(new SqlParameter("@endIndex", SqlDbType.Int) { Value = page.Skip + page.PageSize });

            SqlParameter[] para = paraList.ToArray();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    shopList.Add(SqlHelper.GetEntity<MealShopList>(sdr));
                }
            }
            return shopList;
        }

        public static List<MealList> SelectMealList(params int[] arrShopId)
        {
            string strSql = @"select m.MealID,m.shopId,m.ImageURL,round(m.Price,2) Price,round(m.OriginalPrice,2) OriginalPrice,m.Suggestion from Meal m 
                                    where m.IsActive=1 and m.ShopID in (select d.x.value('./id[1]','int') from @xml.nodes('/*') as d(x)) order by OrderNumber";
            var xml = new StringBuilder();
            foreach (var t in arrShopId)
            {
                xml.AppendFormat("<row><id>{0}</id></row>", (int)t);
            }

            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@xml", SqlDbType.Xml) { Value = xml.ToString() }
            };

            List<MealList> mealList = new List<MealList>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    mealList.Add(SqlHelper.GetEntity<MealList>(sdr));
                }
            }
            return mealList;
        }

        /// <summary>
        /// 分页查询某城市套餐列表
        /// </summary>
        /// <param name="page">分页信息</param>
        /// <param name="cityId">城市ID</param>
        /// <param name="tagId">商圈ID</param>
        /// <param name="cnt"></param>
        /// <returns></returns>
        public static List<MealList> SelectMealList(Page page, int cityId, int tagId)//, out int cnt
        {
            List<MealList> mealList = new List<MealList>();
            string strSql = "";
            //string strCnt = "";
            //            strCnt = @"select count(1) from dbo.ShopWithTag a
            //inner join dbo.ShopTag b on a.TagNode.IsDescendantOf(b.TagNode)=1 and b.Enable=1
            //inner join Meal m on m.ShopID=a.ShopId
            //inner join ShopInfo s on m.ShopID = s.shopID and s.shopStatus=1 and s.isHandle=1";

            strSql = @"select rownum,a.MealID,a.shopName,a.ImageURL,round(a.Price,2) Price,round(a.OriginalPrice,2) OriginalPrice,a.Suggestion from(
select row_number() OVER (ORDER BY m.ShopID desc) rownum,m.MealID,s.shopName,m.ImageURL,m.Price,m.OriginalPrice,m.Suggestion
from dbo.ShopWithTag a
inner join dbo.ShopTag b on a.TagNode.IsDescendantOf(b.TagNode)=1 and b.Enable=1 and b.TagLevel=1
inner join Meal m on m.ShopID=a.ShopId
inner join ShopInfo s on m.ShopID = s.shopID and s.shopStatus=1 and s.isHandle=1";

            List<SqlParameter> paraList = new List<SqlParameter>();
            if (cityId > 0)
            {
                strSql += " and s.cityID=@cityId";
                //strCnt += " and s.cityID=@cityId";
                paraList.Add(new SqlParameter("@cityId", SqlDbType.Int) { Value = cityId });
            }
            if (tagId > 0)
            {
                strSql += " and b.TagId=@tagId";
                //strCnt += " and b.TagId=@tagId";
                paraList.Add(new SqlParameter("@tagId", SqlDbType.Int) { Value = tagId });
            }
            strSql += ") a where a.rownum between @startIndex and @endIndex";

            SqlParameter[] paraCnt = paraList.ToArray();

            //object objCnt = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strCnt.ToString(), paraCnt);
            //if (objCnt == null)
            //{
            //    cnt = 0;
            //}
            //else
            //{
            //    cnt = Convert.ToInt32(objCnt);
            //}

            paraList.Add(new SqlParameter("@startIndex", SqlDbType.Int) { Value = page.Skip + 1 });
            paraList.Add(new SqlParameter("@endIndex", SqlDbType.Int) { Value = page.Skip + page.PageSize });

            SqlParameter[] para = paraList.ToArray();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                while (sdr.Read())
                {
                    mealList.Add(SqlHelper.GetEntity<MealList>(sdr));
                }
            }
            return mealList;
        }

        /// <summary>
        /// 查询该城市已合作年夜饭的餐厅所在区域
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public static List<ShopTag> SelectMealShopTag(int cityId)
        {
            const string strSql = @"select distinct b.TagId,b.Name from dbo.ShopWithTag a
                                            inner join dbo.ShopTag b on a.TagNode.IsDescendantOf(b.TagNode)=1
                                            where a.ShopId in (select m.ShopId from Meal m inner join ShopInfo s on m.ShopID=s.shopID and m.IsActive=1 and s.cityID=@cityId)
                                            and b.TagLevel=1";
            SqlParameter[] para = new SqlParameter[] { 
           new SqlParameter("@cityId", SqlDbType.Int) { Value = cityId }
           };
            List<ShopTag> shopTag = new List<ShopTag>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    shopTag.Add(SqlHelper.GetEntity<ShopTag>(sdr));
                }
            }
            return shopTag;
        }

        /// <summary>
        /// 查询客户所有的年夜饭订单
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public static List<MealOrder> SelectMealOrder(string mobilePhoneNumber)
        {
            const string strSql = @"select p.preOrder19dianId,p.status,p.preOrderTime,s.shopName,round(p.preOrderSum,2) price,ms.DinnerTime,ms.DinnerType
                                            from PreOrder19dian p inner join MealConnPreOrder conn on p.preOrder19dianId=conn.preOrder19dianId and p.status!=104 and p.preOrder19dianId>200000
                                            inner join MealSchedule ms on conn.MealScheduleID=ms.MealScheduleID
                                            inner join CustomerInfo c on p.customerId=c.CustomerID and c.mobilePhoneNumber=@mobilePhoneNumber
                                            inner join ShopInfo s on p.shopId=s.shopID order by preOrderTime desc";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = mobilePhoneNumber }
            };
            List<MealOrder> mealOrder = new List<MealOrder>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    mealOrder.Add(SqlHelper.GetEntity<MealOrder>(sdr));
                }
            }
            return mealOrder;
        }

        /// <summary>
        /// 查询所有套餐剩余可售份数
        /// </summary>
        /// <returns></returns>
        public static List<MealListStatus> SelectMealListStatus(int mealId)
        {
            const string str = "select d.MealID,SUM(d.TotalCount-d.SoldCount) remainCount from MealSchedule d where d.MealID=@mealId group by d.MealID";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mealId", SqlDbType.Int) { Value = mealId }
            };
            List<MealListStatus> status = new List<MealListStatus>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, str, para))
            {
                while (sdr.Read())
                {
                    status.Add(SqlHelper.GetEntity<MealListStatus>(sdr));
                }
            }
            return status;
        }

        public static List<int> SelectMealStatus()
        {
            const string strSql = "select distinct d.MealID from MealSchedule d where ValidTo>GETDATE()  and d.IsActive=1";
            List<int> mealID = new List<int>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql))
            {
                while (sdr.Read())
                {
                    mealID.Add(Convert.ToInt32(sdr[0]));
                }
            }
            return mealID;
        }

        /// <summary>
        /// 根据套餐ID查询套餐信息
        /// </summary>
        /// <param name="mealId"></param>
        /// <returns></returns>
        public static MealList SelectMealList(int mealId)
        {
            const string strSql = @"select m.MealID,m.ShopID,s.shopName,s.shopTelephone contactPhone,s.shopAddress,
m.ImageURL,m.Menu,round(m.Price,2) Price,round(m.OriginalPrice,2) OriginalPrice,m.Suggestion
 from Meal m inner join ShopInfo s on m.ShopID=s.shopID and m.IsActive=1
and m.MealID=@mealId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mealId", SqlDbType.Int) { Value = mealId }
            };
            MealList mealList = new MealList();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (sdr.Read())
                {
                    mealList = SqlHelper.GetEntity<MealList>(sdr);
                }
            }
            return mealList;
        }
        /// <summary>
        /// 根据套餐排期Id查询所属门店的基本信息
        /// </summary>
        /// <param name="mealScheduleId"></param>
        /// <returns></returns>
        public static DataTable SelectMealConnShop(int mealScheduleId)
        {
            const string strSql = @"select m.MealID,round(m.Price,2) Price,m.ShopID,s.isHandle,s.isSupportPayment,s.notPaymentReason,s.isSupportRedEnvelopePayment,s.companyID,conn.menuId,(d.TotalCount-d.SoldCount) remainCount
from Meal m inner join MealSchedule d on m.MealID=d.MealID and d.MealScheduleID=@mealScheduleId
inner join ShopInfo s on m.ShopID=s.shopID
inner join MenuConnShop conn on s.shopID = conn.shopId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mealScheduleId", SqlDbType.Int) { Value = mealScheduleId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 年夜饭订单报表
        /// </summary>
        /// <returns></returns>
        public List<MealOrderReport> SelectMealOrderReport()
        {
            const string strSql = @"select A.preOrder19dianId,D.shopName,E.UserName,E.mobilePhoneNumber,G.DinnerTime as preOrderTime,'￥'+cast(C.Price as varchar)+'/'+C.Suggestion as mealName,
B.prePayTime,F.EmployeeFirstName,G.DinnerType,D.cityID,

(case when ISNULL(B.isPaid,0)=0 and B.status!=104 then 1
      when ISNULL(B.isPaid,0)=0 and B.status=104 then 5
      when B.prePaidSum=B.refundMoneySum and B.refundMoneySum>0 and ISNULL(B.isPaid,0)=1 then 4
      when ISNULL(B.isShopConfirmed,0)=0 and ISNULL(B.isPaid,0)=1 then 2
      when ISNULL(B.isShopConfirmed,0)=1 and ISNULL(B.isPaid,0)=1 then 3 end) as orderStatus
      
from dbo.MealConnPreOrder A
inner join PreOrder19dian B on A.preOrder19dianId=B.preOrder19dianId
inner join ShopInfo D on D.shopID=B.shopId
inner join CustomerInfo E on E.CustomerID=B.customerId
inner join EmployeeInfo F on F.EmployeeID=D.accountManager
inner join dbo.MealSchedule G on G.MealScheduleID=A.MealScheduleID
inner join dbo.Meal C on C.MealID=G.MealID
where B.preOrder19dianId>200000 and D.isHandle=1 and D.shopStatus=1
and E.CustomerStatus=1 and E.mobilePhoneNumber!='' and F.EmployeeStatus=1 order by B.preOrderTime desc ";
            List<MealOrderReport> list = new List<MealOrderReport>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql))
            {
                while (sdr.Read())
                {
                    var model = new MealOrderReport();
                    model.shopName = sdr["shopName"] == DBNull.Value ? "未知门店" : Convert.ToString(sdr["shopName"]);
                    model.customerName = sdr["shopName"] == DBNull.Value ? "匿名" : Convert.ToString(sdr["UserName"]);
                    model.customerPhone = sdr["mobilePhoneNumber"] == DBNull.Value ? "" : Convert.ToString(sdr["mobilePhoneNumber"]);
                    model.orderTime = sdr["preOrderTime"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(sdr["preOrderTime"]);
                    model.orderTimeFrame = sdr["DinnerType"] == DBNull.Value ? "" : Enum.Parse(typeof(DinnerType), Convert.ToString(sdr["DinnerType"])).ToString();
                    model.mealName = sdr["mealName"] == DBNull.Value ? "未知" : Convert.ToString(sdr["mealName"]);
                    model.orderPayTime = sdr["prePayTime"] == DBNull.Value ? "" : Convert.ToDateTime(sdr["prePayTime"]).ToString();

                    model.orderStatusDesc = sdr["orderStatus"] == DBNull.Value ? "未知状态" : Enum.Parse(typeof(OrderStatus), Convert.ToString(sdr["orderStatus"])).ToString();

                    model.orderStatus = sdr["orderStatus"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["orderStatus"]);
                    model.employeeName = sdr["EmployeeFirstName"] == DBNull.Value ? "未知" : Convert.ToString(sdr["EmployeeFirstName"]);
                    model.cityId = sdr["cityID"] == DBNull.Value ? 0 : Convert.ToInt32(sdr["cityID"]);

                    list.Add(model);
                }
            }
            return list;
        }
        /// <summary>
        /// 查询当前用户所有年夜饭点单编号
        /// </summary>
        /// <param name="customerId">用户编号</param>
        /// <returns></returns>
        public List<long> GetMealOrderIds(long customerId)
        {
            const string strSql = @"select A.preOrder19dianId from MealConnPreOrder A
left join PreOrder19dian B on A.preOrder19dianId=B.preOrder19dianId
where B.status!=104 and B.customerId=@customerId";
            SqlParameter parameter = new SqlParameter("@customerId", customerId);
            var list = new List<long>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                while (dr.Read())
                {
                    list.Add(dr[0] == DBNull.Value ? 0 : Convert.ToInt64(dr[0]));
                }
            }
            return list;
        }

        public static int InsertIntoMealConnPreOrder(long preOrder19dianId, int mealScheduleId)
        {
            const string strSql = @"insert into MealConnPreOrder(preOrder19dianId,MealScheduleID) values (@preOrder19dianId,@mealScheduleId) select @@identity";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt) { Value = preOrder19dianId },
            new SqlParameter("@mealScheduleId", SqlDbType.Int) { Value = mealScheduleId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
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

        public string GetMealScheduleByOrderId(long preOrder19dianId)
        {
            const string strSql = @"select B.DinnerTime,B.DinnerType from MealConnPreOrder A
left join MealSchedule B on A.MealScheduleID=B.MealScheduleID
where A.preOrder19dianId=@preOrder19dianId";
            SqlParameter parameter = new SqlParameter("@preOrder19dianId", preOrder19dianId);
            string result = "年夜饭套餐";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                if (dr.Read())
                {
                    result = dr["DinnerTime"] != DBNull.Value && dr["DinnerType"] != DBNull.Value ?
                        Convert.ToDateTime(dr["DinnerTime"]).ToString("MM月dd日").TrimStart('0') + Enum.Parse(typeof(DinnerType), Convert.ToString(dr["DinnerType"])).ToString() : "年夜饭套餐";
                }
            }
            return result;
        }
    }
}
