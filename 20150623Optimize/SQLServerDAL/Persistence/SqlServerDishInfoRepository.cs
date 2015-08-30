﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using PagedList;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerDishInfoRepository : SqlServerRepositoryBase, IDishInfoRepository
    {
        public SqlServerDishInfoRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }



        public DishInfo GetById(long id)
        {
            const string cmdText = @"SELECT * FROM [dbo].[DishInfo] WHERE [DishID]=@DishID";
            SqlParameter cmdParm = new SqlParameter("@DishID", id);
            DishInfo dishInfo = null;
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    dishInfo = SqlHelper.GetEntity<DishInfo>(dr);
                }
            }
            return dishInfo;
        }


        public IEnumerable<Dish> GetRandomDishInfosByMenu(int top, int menuId, params int[] dishIds)
        {
            var dishIdsXML = new StringBuilder();
            foreach (var dishId in dishIds)
            {
                dishIdsXML.AppendFormat("<row><id>{0}</id></row>", dishId);
            }

            const string cmdTextCount = @"select COUNT(1) from [dbo].[DishInfo] a
INNER JOIN [dbo].[ImageInfo] b on a.DishID=b.DishID
INNER JOIN [dbo].[DishI18n] c on a.DishID=c.DishID
WHERE c.DishI18nStatus=1 AND b.ImageStatus=1 AND b.ImageScale=0 AND a.DishStatus=1 AND a.MenuID=@MenuId AND a.[DishID] NOT IN  (
select d.x.value('./id[1]','int') from @a.nodes('/*') as d(x))";

            SqlParameter[] cmdParmsCount = new SqlParameter[]
            {
                new SqlParameter("@MenuId", menuId),
                new SqlParameter("@a", SqlDbType.Xml) { Value = dishIdsXML.ToString() }
            };
            object obj_count = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdTextCount, cmdParmsCount);

            int count = Convert.ToInt32(obj_count);
            Random random = new Random();
            int index = 1;
            if (count > top)
            {
                index = random.Next(0, count - top + 1) + 1;
            }

            const string cmdText = @"SELECT t.DishID,t.DishName,t.ImageName as [Image0] FROM (
select a.DishID, c.DishName,b.ImageName,ROW_NUMBER() OVER(ORDER BY a.DishDisplaySequence) as RowNum from [dbo].[DishInfo] a
INNER JOIN [dbo].[ImageInfo] b on a.DishID=b.DishID
INNER JOIN [dbo].[DishI18n] c on a.DishID=c.DishID
WHERE c.DishI18nStatus=1 AND b.ImageStatus=1 AND b.ImageScale=0 AND a.DishStatus=1 AND a.MenuID=@MenuId AND a.[DishID] NOT IN  (
select d.x.value('./id[1]','int') from @a.nodes('/*') as d(x))
) AS t
WHERE t.RowNum BETWEEN @StartIndex AND @EndIndex";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@MenuId", menuId),
                new SqlParameter("@a", SqlDbType.Xml) { Value = dishIdsXML.ToString() },
                new SqlParameter("@StartIndex", index),
                new SqlParameter("@EndIndex", index + top-1),
            };

            List<Dish> list = new List<Dish>();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParms))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<Dish>(dr));
                }
            }
            return list;
        }

        public IPagedList<DishDetails> GetPageShopSellOffDishDetailses(Page page, int shopId)
        {
            StringBuilder strSqlBuilder = new StringBuilder();
            strSqlBuilder.Append(
                @"select  t.companyId,t.shopId,t.menuId,t.DishI18nID,t.DishPriceI18nID,t.DishName,t.dishQuanPin,t.dishJianPin,t.DishPrice,
t.ScaleName,t.DishDisplaySequence,t.SellOff 
from (SELECT ROW_NUMBER() Over(order by  b.DishDisplaySequence asc) as rowId,
f.companyId,f.shopId,f.menuId,a.DishI18nID,e.DishPriceI18nID, a.DishName,a.dishQuanPin,a.dishJianPin,d.[DishPrice],
e.ScaleName,b.DishDisplaySequence,1 as SellOff
 from CurrentSellOffInfo f
inner join dbo.DishI18n a on f.DishI18nID=a.DishI18nID
inner join dbo.DishInfo b on a.DishID=b.DishID
inner join dbo.DishPriceI18n e on e.DishPriceI18nID=f.DishPriceI18nID
inner join dbo.[DishPriceInfo] d on b.DishID=d.DishID and e.DishPriceID=d.DishPriceID
where f.shopId=@ShopId and f.[status]=1 and a.DishI18nStatus=1 and b.DishStatus=1 and e.DishPriceI18nStatus=1
and d.DishPriceStatus=1 and exists(select 1 from MenuConnShop where shopid=@ShopId and [menuId]=f.menuId)) as t 
where t.rowId between @StartIndex and @EndIndex");

            StringBuilder strCountSqlBuilder = new StringBuilder();
            strCountSqlBuilder.Append(@"SELECT COUNT(1)
from CurrentSellOffInfo f
inner join dbo.DishI18n a on f.DishI18nID=a.DishI18nID
inner join dbo.DishInfo b on a.DishID=b.DishID
inner join dbo.DishPriceI18n e on e.DishPriceI18nID=f.DishPriceI18nID
inner join dbo.[DishPriceInfo] d on b.DishID=d.DishID and e.DishPriceID=d.DishPriceID
where f.shopId=@ShopId and f.[status]=1 and a.DishI18nStatus=1 and b.DishStatus=1 and e.DishPriceI18nStatus=1 
and d.DishPriceStatus=1 and exists(select 1 from MenuConnShop where shopid=@ShopId and [menuId]=f.menuId)");

            SqlParameter[] cmdCountParameters = new[] { new SqlParameter("@ShopId", shopId) };
            List<SqlParameter> cmdParameters = new List<SqlParameter>(cmdCountParameters);
            cmdParameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("@StartIndex", page.Skip+1),
                new SqlParameter("@EndIndex", page.Skip+page.PageSize)
            });

            List<DishDetails> list = new List<DishDetails>();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text,
                strSqlBuilder.ToString(), cmdParameters.ToArray()))
            {
                while (dr.Read())
                {
                    list.Add(new DishDetails
                    {
                        CompanyId = Convert.ToInt32(dr["companyId"]),
                        ShopId = Convert.ToInt32(dr["shopId"]),
                        MenuId = Convert.ToInt32(dr["menuId"]),
                        DishId = Convert.ToInt32(dr["DishI18nID"]),
                        DishPriceId = Convert.ToInt32(dr["DishPriceI18nID"]),
                        DishDisplaySequence = dr["DishDisplaySequence"] == DBNull.Value
                            ? int.MaxValue
                            : Convert.ToInt32(dr["DishDisplaySequence"]),
                        DishJianPin = dr["dishJianPin"] == DBNull.Value ? "" : dr["dishJianPin"].ToString(),
                        DishName = dr["DishName"] == DBNull.Value ? "" : dr["DishName"].ToString(),
                        DishQuanPin = dr["dishQuanPin"] == DBNull.Value ? "" : dr["dishQuanPin"].ToString(),
                        ScaleName = dr["ScaleName"] == DBNull.Value ? "" : dr["ScaleName"].ToString(),
                        DishPrice = Convert.ToSingle(dr["DishPrice"]),
                        SellOff = Convert.ToBoolean(dr["SellOff"])
                    });
                }
            }

            object v = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text,
               strCountSqlBuilder.ToString(), cmdCountParameters);
            int count = Convert.ToInt32(v);

            return new StaticPagedList<DishDetails>(list, page.PageNumber, page.PageSize, count);
        }

        public IPagedList<DishDetails> GetPageShopSellOnDishDetailses(Page page, int shopId)
        {
            const string cmdTextCount = @"select count(1)
 from dbo.DishI18n a
inner join dbo.DishInfo b on a.DishID=b.DishID
inner join dbo.MenuConnShop c on b.MenuID=c.menuId
inner join dbo.[DishPriceInfo] d on a.DishID=d.DishID
inner join dbo.DishPriceI18n e on e.DishPriceID=d.DishPriceID 
where b.DishStatus=1  and d.DishPriceStatus=1 and e.DishPriceI18nStatus=1 and a.DishI18nStatus=1 and c.shopId=@shopId
and NOT exists(SELECT 1 FROM [dbo].[CurrentSellOffInfo] f where f.DishI18nID=a.DishI18nID and f.DishPriceI18nID=e.DishPriceI18nID and f.status=1 and f.shopid=c.shopId)";

            const string cmdText = @"select * from (
select ROW_NUMBER() Over(order by  f.[currentSellOffCount] desc) as rowId,c.companyId,c.shopId,c.menuId,a.DishI18nID,e.DishPriceI18nID, a.DishName,a.dishQuanPin,a.dishJianPin,d.[DishPrice],e.ScaleName,b.DishDisplaySequence
 from dbo.DishI18n a
inner join dbo.DishInfo b on a.DishID=b.DishID
inner join dbo.MenuConnShop c on b.MenuID=c.menuId
inner join dbo.[DishPriceInfo] d on a.DishID=d.DishID
inner join dbo.DishPriceI18n e on e.DishPriceID=d.DishPriceID 
left join [dbo].[CommonCurrentSellOffInfo] f on f.DishPriceI18nID=e.DishPriceI18nID and f.menuId=c.menuId and f.DishI18nID=a.DishI18nID and f.[status]=1
where b.DishStatus=1  and d.DishPriceStatus=1 and e.DishPriceI18nStatus=1 and a.DishI18nStatus=1 and c.shopId=@shopId 
and NOT exists(SELECT 1 FROM [dbo].[CurrentSellOffInfo] where DishI18nID=a.DishI18nID and DishPriceI18nID=e.DishPriceI18nID and status=1 and shopid=c.shopId)
) as t
where t.rowId between @StartIndex and @EndIndex";

            SqlParameter cmdParmCount = new SqlParameter("@shopId", SqlDbType.Int) { Value = shopId };

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                cmdParmCount,
                new SqlParameter("@StartIndex", SqlDbType.Int) { Value = page.Skip+1 },
                new SqlParameter("@EndIndex", SqlDbType.Int) { Value = page.Skip+page.PageSize },
            };


            object o = SqlHelper.ExecuteScalar(Connection, CommandType.Text, cmdTextCount, cmdParmCount);
            int count = Convert.ToInt32(o);

            List<DishDetails> list = new List<DishDetails>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                while (dr.Read())
                {
                    list.Add(new DishDetails
                    {
                        CompanyId = Convert.ToInt32(dr["companyId"]),
                        ShopId = Convert.ToInt32(dr["shopId"]),
                        MenuId = Convert.ToInt32(dr["menuId"]),
                        DishId = Convert.ToInt32(dr["DishI18nID"]),
                        DishPriceId = Convert.ToInt32(dr["DishPriceI18nID"]),
                        DishDisplaySequence = dr["DishDisplaySequence"] == DBNull.Value
                            ? int.MaxValue
                            : Convert.ToInt32(dr["DishDisplaySequence"]),
                        DishJianPin = dr["dishJianPin"] == DBNull.Value ? "" : dr["dishJianPin"].ToString(),
                        DishName = dr["DishName"] == DBNull.Value ? "" : dr["DishName"].ToString(),
                        DishQuanPin = dr["dishQuanPin"] == DBNull.Value ? "" : dr["dishQuanPin"].ToString(),
                        ScaleName = dr["ScaleName"] == DBNull.Value ? "" : dr["ScaleName"].ToString(),
                        DishPrice = Convert.ToSingle(dr["DishPrice"]),
                        SellOff = false
                    });
                }
            }

            return new StaticPagedList<DishDetails>(list, page.PageNumber, page.PageSize, count);
        }

        public IPagedList<DishDetails> GetPageShopAllDishDetailses(Page page, int shopId, string keyword)
        {
            StringBuilder strSqlBuilder = new StringBuilder();
            strSqlBuilder.Append(
                "select t.companyId,t.shopId,t.menuId,t.DishI18nID,t.DishPriceI18nID,t.DishName,t.dishQuanPin,t.dishJianPin,t.DishPrice,t.ScaleName,t.DishDisplaySequence,t.SellOff from (SELECT ROW_NUMBER() Over(order by  b.DishDisplaySequence asc) as rowId,c.companyId,c.shopId,c.menuId,a.DishI18nID,e.DishPriceI18nID, a.DishName,a.dishQuanPin,a.dishJianPin,d.[DishPrice],e.ScaleName,b.DishDisplaySequence,");
            strSqlBuilder.Append(
                "case when exists (SELECT 1 FROM [dbo].[CurrentSellOffInfo] f where f.DishI18nID=a.DishI18nID and f.DishPriceI18nID=e.DishPriceI18nID and f.status=1 and f.shopid=@ShopId) then 1 else 0 end as SellOff ");
            strSqlBuilder.Append("FROM [dbo].DishI18n a ");
            strSqlBuilder.Append("inner join dbo.DishInfo b on a.DishID=b.DishID and b.DishStatus=1 ");
            strSqlBuilder.Append("inner join dbo.MenuConnShop c on b.MenuID=c.menuId ");
            strSqlBuilder.Append("inner join dbo.[DishPriceInfo] d on a.DishID=d.DishID and d.DishPriceStatus=1 ");
            strSqlBuilder.Append(
                "inner join dbo.DishPriceI18n e on e.DishPriceID=d.DishPriceID and e.DishPriceI18nStatus=1 ");
            strSqlBuilder.Append("where a.DishI18nStatus=1 and c.shopId=@ShopId ");
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                strSqlBuilder.Append("and (a.DishName like @Key or a.dishQuanPin like @Key or a.dishJianPin like @Key) ");
            }
            strSqlBuilder.Append(") as t ");
            strSqlBuilder.Append("where t.rowId between @StartIndex and @EndIndex");

            Console.WriteLine(strSqlBuilder.ToString());

            List<SqlParameter> cmdCountParameters = new List<SqlParameter>();
            cmdCountParameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("@ShopId", shopId),
            });
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                cmdCountParameters.Add(new SqlParameter("@Key", string.Format("%{0}%", keyword)));
            }

            List<SqlParameter> cmdParameters = new List<SqlParameter>(cmdCountParameters);
            cmdParameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("@StartIndex", page.Skip+1),
                new SqlParameter("@EndIndex", page.Skip+page.PageSize)
            });

            List<DishDetails> list = new List<DishDetails>();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text,
                strSqlBuilder.ToString(), cmdParameters.ToArray()))
            {
                while (dr.Read())
                {
                    list.Add(new DishDetails
                    {
                        CompanyId = Convert.ToInt32(dr["companyId"]),
                        ShopId = Convert.ToInt32(dr["shopId"]),
                        MenuId = Convert.ToInt32(dr["menuId"]),
                        DishId = Convert.ToInt32(dr["DishI18nID"]),
                        DishPriceId = Convert.ToInt32(dr["DishPriceI18nID"]),
                        DishDisplaySequence = dr["DishDisplaySequence"] == DBNull.Value
                            ? int.MaxValue
                            : Convert.ToInt32(dr["DishDisplaySequence"]),
                        DishJianPin = dr["dishJianPin"] == DBNull.Value ? "" : dr["dishJianPin"].ToString(),
                        DishName = dr["DishName"] == DBNull.Value ? "" : dr["DishName"].ToString(),
                        DishQuanPin = dr["dishQuanPin"] == DBNull.Value ? "" : dr["dishQuanPin"].ToString(),
                        ScaleName = dr["ScaleName"] == DBNull.Value ? "" : dr["ScaleName"].ToString(),
                        DishPrice = Convert.ToSingle(dr["DishPrice"]),
                        SellOff = Convert.ToBoolean(dr["SellOff"])
                    });
                }
            }

            StringBuilder strCountSqlBuilder = new StringBuilder();
            strCountSqlBuilder.Append("SELECT COUNT(1) FROM [dbo].DishI18n a ");
            strCountSqlBuilder.Append("inner join dbo.DishInfo b on a.DishID=b.DishID and b.DishStatus=1 ");
            strCountSqlBuilder.Append("inner join dbo.MenuConnShop c on b.MenuID=c.menuId ");
            strCountSqlBuilder.Append("inner join dbo.[DishPriceInfo] d on a.DishID=d.DishID and d.DishPriceStatus=1 ");
            strCountSqlBuilder.Append(
                "inner join dbo.DishPriceI18n e on e.DishPriceID=d.DishPriceID and e.DishPriceI18nStatus=1 ");
            strCountSqlBuilder.Append("where c.shopId=@ShopId ");
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                strCountSqlBuilder.Append("and (a.DishName like @Key or a.dishQuanPin like @Key or a.dishJianPin like @Key) ");
            }
            object v = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text,
                strCountSqlBuilder.ToString(), cmdCountParameters.ToArray());

            return new StaticPagedList<DishDetails>(list, page.PageNumber, page.PageSize, Convert.ToInt32(v));
        }

        public IPagedList<DishDetails> GetPageShopAllDishDetailsForAward(Page page, int shopId, string keyword)
        {
            StringBuilder strSqlBuilder = new StringBuilder();
            strSqlBuilder.Append(
                "select t.companyId,t.shopId,t.menuId,t.DishID,t.DishPriceI18nID,t.DishName,t.dishQuanPin,t.dishJianPin,t.DishPrice,t.ScaleName,t.DishDisplaySequence,t.SellOff from (SELECT ROW_NUMBER() Over(order by  b.DishDisplaySequence asc) as rowId,c.companyId,c.shopId,c.menuId,a.DishID,e.DishPriceI18nID, a.DishName,a.dishQuanPin,a.dishJianPin,d.[DishPrice],e.ScaleName,b.DishDisplaySequence,");
            strSqlBuilder.Append(
                "case when exists (SELECT 1 FROM [dbo].[CurrentSellOffInfo] f where f.DishI18nID=a.DishI18nID and f.DishPriceI18nID=e.DishPriceI18nID and f.status=1 and f.shopid=@ShopId) then 1 else 0 end as SellOff ");
            strSqlBuilder.Append("FROM [dbo].DishI18n a ");
            strSqlBuilder.Append("inner join dbo.DishInfo b on a.DishID=b.DishID and b.DishStatus=1 ");
            strSqlBuilder.Append("inner join dbo.MenuConnShop c on b.MenuID=c.menuId ");
            strSqlBuilder.Append("inner join dbo.[DishPriceInfo] d on a.DishID=d.DishID and d.DishPriceStatus=1 and d.DishSoldout=0 ");
            strSqlBuilder.Append(
                "inner join dbo.DishPriceI18n e on e.DishPriceID=d.DishPriceID and e.DishPriceI18nStatus=1 ");
            strSqlBuilder.Append("where a.DishI18nStatus=1 and c.shopId=@ShopId ");
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                strSqlBuilder.Append("and (a.DishName like @Key or a.dishQuanPin like @Key or a.dishJianPin like @Key) ");
            }
            strSqlBuilder.Append(") as t ");
            strSqlBuilder.Append("where t.rowId between @StartIndex and @EndIndex");

            Console.WriteLine(strSqlBuilder.ToString());

            List<SqlParameter> cmdCountParameters = new List<SqlParameter>();
            cmdCountParameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("@ShopId", shopId),
            });
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                cmdCountParameters.Add(new SqlParameter("@Key", string.Format("%{0}%", keyword)));
            }

            List<SqlParameter> cmdParameters = new List<SqlParameter>(cmdCountParameters);
            cmdParameters.AddRange(new SqlParameter[]
            {
                new SqlParameter("@StartIndex", page.Skip+1),
                new SqlParameter("@EndIndex", page.Skip+page.PageSize)
            });

            List<DishDetails> list = new List<DishDetails>();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text,
                strSqlBuilder.ToString(), cmdParameters.ToArray()))
            {
                while (dr.Read())
                {
                    list.Add(new DishDetails
                    {
                        CompanyId = Convert.ToInt32(dr["companyId"]),
                        ShopId = Convert.ToInt32(dr["shopId"]),
                        MenuId = Convert.ToInt32(dr["menuId"]),
                        DishId = Convert.ToInt32(dr["DishId"]),
                        DishPriceId = Convert.ToInt32(dr["DishPriceI18nID"]),
                        DishDisplaySequence = dr["DishDisplaySequence"] == DBNull.Value
                            ? int.MaxValue
                            : Convert.ToInt32(dr["DishDisplaySequence"]),
                        DishJianPin = dr["dishJianPin"] == DBNull.Value ? "" : dr["dishJianPin"].ToString(),
                        DishName = dr["DishName"] == DBNull.Value ? "" : dr["DishName"].ToString(),
                        DishQuanPin = dr["dishQuanPin"] == DBNull.Value ? "" : dr["dishQuanPin"].ToString(),
                        ScaleName = dr["ScaleName"] == DBNull.Value ? "" : dr["ScaleName"].ToString(),
                        DishPrice = Convert.ToSingle(dr["DishPrice"]),
                        SellOff = Convert.ToBoolean(dr["SellOff"])
                    });
                }
            }

            StringBuilder strCountSqlBuilder = new StringBuilder();
            strCountSqlBuilder.Append("SELECT COUNT(1) FROM [dbo].DishI18n a ");
            strCountSqlBuilder.Append("inner join dbo.DishInfo b on a.DishID=b.DishID and b.DishStatus=1 ");
            strCountSqlBuilder.Append("inner join dbo.MenuConnShop c on b.MenuID=c.menuId ");
            strCountSqlBuilder.Append("inner join dbo.[DishPriceInfo] d on a.DishID=d.DishID and d.DishPriceStatus=1  and d.DishSoldout=0 ");
            strCountSqlBuilder.Append(
                "inner join dbo.DishPriceI18n e on e.DishPriceID=d.DishPriceID and e.DishPriceI18nStatus=1 ");
            strCountSqlBuilder.Append("where c.shopId=@ShopId ");
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                strCountSqlBuilder.Append("and (a.DishName like @Key or a.dishQuanPin like @Key or a.dishJianPin like @Key) ");
            }
            object v = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text,
                strCountSqlBuilder.ToString(), cmdCountParameters.ToArray());

            return new StaticPagedList<DishDetails>(list, page.PageNumber, page.PageSize, Convert.ToInt32(v));
        }
    }
}