using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.Interface;
using VAGastronomistMobileApp.Model.QueryObject;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public partial class ShopStaticsManager
    {
        public bool IsExists(int ShopStaticsId)
        {
            string sql = "SELECT COUNT(0) FROM ShopStatics WHERE ShopStaticsId = @ShopStaticsId";
            var retutnValue =
                SqlHelper.ExecuteScalar(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@ShopStaticsId",SqlDbType.Int,4) { Value = ShopStaticsId });
            int count;
            if (int.TryParse(retutnValue.ToString(), out count))
            {
                if (count > 0)
                {
                    return true;
                }
            }
            return false;
        }
        
        public ShopStatics GetEntityById(int ShopStaticsId)
        {
            string sql  = @"SELECT [ShopStaticsId]
                                ,[CityId]
                                ,[AddedCount]
                                ,[OnlineCount]
                                ,[StopPaymentCount]
                                ,[StaticsStart]
                                ,[StaticsEnd]
                                ,[ReportType]
                                ,[CreateTime]
                                ,[TotalCount]
                            FROM  [ShopStatics] 
                            WHERE [ShopStaticsId] = @ShopStaticsId ";
             using(var reader = 
                    SqlHelper.ExecuteReader(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@ShopStaticsId", SqlDbType.Int, 4){ Value = ShopStaticsId }))
            {
                if(reader.Read())
                {
                  var entity = reader.GetEntity<ShopStatics>();
                  return entity;
                }
            }
            return null;
        }
        
        public bool Add(IShopStatics Entity)
        {
            string sql = @"INSERT INTO ShopStatics (
                                    [CityId],
                                    [AddedCount],
                                    [OnlineCount],
                                    [StopPaymentCount],
                                    [StaticsStart],
                                    [StaticsEnd],
                                    [ReportType],
                                    [CreateTime],
                                    [TotalCount]
                                ) VALUES (
                                    @CityId,
                                    @AddedCount,
                                    @OnlineCount,
                                    @StopPaymentCount,
                                    @StaticsStart,
                                    @StaticsEnd,
                                    @ReportType,
                                    @CreateTime,
                                    @TotalCount
                                    );                                    
                                    SELECT @@IDENTITY";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@CityId", SqlDbType.Int, 4) { Value = Entity.CityId });
            sqlParameterList.Add(new SqlParameter("@AddedCount", SqlDbType.Int, 4) { Value = Entity.AddedCount });
            sqlParameterList.Add(new SqlParameter("@OnlineCount", SqlDbType.Int, 4) { Value = Entity.OnlineCount });
            sqlParameterList.Add(new SqlParameter("@StopPaymentCount", SqlDbType.Int, 4) { Value = Entity.StopPaymentCount });
            sqlParameterList.Add(new SqlParameter("@StaticsStart", SqlDbType.DateTime, 8) { Value = Entity.StaticsStart });
            sqlParameterList.Add(new SqlParameter("@StaticsEnd", SqlDbType.DateTime, 8) { Value = Entity.StaticsEnd });
            sqlParameterList.Add(new SqlParameter("@ReportType", SqlDbType.Int, 4) { Value = Entity.ReportType });
            sqlParameterList.Add(new SqlParameter("@CreateTime", SqlDbType.DateTime, 8) { Value = Entity.CreateTime });
            sqlParameterList.Add(new SqlParameter("@TotalCount", SqlDbType.Int, 4) { Value = Entity.TotalCount });
            var returnObject =
                SqlHelper.ExecuteScalar(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            int id;
            if(int.TryParse(returnObject.ToString(),out id))
            {
                Entity.ShopStaticsId = id;
                return true;
            }
            return false;
        }
        public bool Update(IShopStatics Entity)
        {
            string sql = @"UPDATE [ShopStatics] SET
                                 [CityId] = @CityId
                                ,[AddedCount] = @AddedCount
                                ,[OnlineCount] = @OnlineCount
                                ,[StopPaymentCount] = @StopPaymentCount
                                ,[StaticsStart] = @StaticsStart
                                ,[StaticsEnd] = @StaticsEnd
                                ,[ReportType] = @ReportType
                                ,[CreateTime] = @CreateTime
                                ,[TotalCount] = @TotalCount
                           WHERE [ShopStaticsId] =@ShopStaticsId";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@ShopStaticsId", SqlDbType.Int, 4) { Value = Entity.ShopStaticsId });
            sqlParameterList.Add(new SqlParameter("@CityId", SqlDbType.Int, 4) { Value = Entity.CityId });
            sqlParameterList.Add(new SqlParameter("@AddedCount", SqlDbType.Int, 4) { Value = Entity.AddedCount });
            sqlParameterList.Add(new SqlParameter("@OnlineCount", SqlDbType.Int, 4) { Value = Entity.OnlineCount });
            sqlParameterList.Add(new SqlParameter("@StopPaymentCount", SqlDbType.Int, 4) { Value = Entity.StopPaymentCount });
            sqlParameterList.Add(new SqlParameter("@StaticsStart", SqlDbType.DateTime, 8) { Value = Entity.StaticsStart });
            sqlParameterList.Add(new SqlParameter("@StaticsEnd", SqlDbType.DateTime, 8) { Value = Entity.StaticsEnd });
            sqlParameterList.Add(new SqlParameter("@ReportType", SqlDbType.Int, 4) { Value = Entity.ReportType });
            sqlParameterList.Add(new SqlParameter("@CreateTime", SqlDbType.DateTime, 8) { Value = Entity.CreateTime });
            sqlParameterList.Add(new SqlParameter("@TotalCount", SqlDbType.Int, 4) { Value = Entity.TotalCount });
            var count = SqlHelper.ExecuteNonQuery(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        
        public bool DeleteEntity(IShopStatics Entity)
        {
            string sql = @"DELETE FROM [ShopStatics]
                                 WHERE [ShopStaticsId] =@ShopStaticsId";
            var count = 
                    SqlHelper.ExecuteNonQuery(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@ShopStaticsId", SqlDbType.Int, 4){ Value = Entity.ShopStaticsId });
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        
        public long GetCountByQuery(ShopStaticsQueryObject queryObject = null)
        {
            string sql = @"SELECT COUNT(0)
                            FROM  [ShopStatics] 
                            WHERE 1 =1 ";
            object retutnValue;
            SqlParameter[] sqlParameters = null;
            if (queryObject != null)
            {
                StringBuilder whereSqlBuilder = new StringBuilder();
                GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder);
                sql = whereSqlBuilder.Insert(0, sql).ToString();
            }

            retutnValue = SqlHelper.ExecuteScalar(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters);
            long count;
            if (long.TryParse(retutnValue.ToString(), out count))
            {
                return count;
            }
            return 0;
        }

        public List<IShopStatics> GetListByQuery(int pageSize,int pageIndex,ShopStaticsQueryObject queryObject = null,ShopStaticsOrderColumn orderColumn = ShopStaticsOrderColumn.ShopStaticsId,SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql  =  @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[ShopStaticsId]
                                ,[CityId]
                                ,[AddedCount]
                                ,[OnlineCount]
                                ,[StopPaymentCount]
                                ,[StaticsStart]
                                ,[StaticsEnd]
                                ,[ReportType]
                                ,[CreateTime]
                                ,[TotalCount]
                            FROM  [ShopStatics] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject,ref sqlParameters, whereSqlBuilder,pageSize,pageIndex);
            sql = whereSqlBuilder.Insert(0, sql).ToString(); 
            sql =  string.Format( " SELECT TOP (@PageSize) * FROM ({0}) T WHERE T.ROWNUM > @PageIndex ",sql);
            List<IShopStatics> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<IShopStatics>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<ShopStatics>());
                }
            }
            return list;
        }
        
        public List<IShopStatics> GetListByQuery(ShopStaticsQueryObject queryObject = null,ShopStaticsOrderColumn orderColumn = ShopStaticsOrderColumn.ShopStaticsId,SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql  = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[ShopStaticsId]
                                ,[CityId]
                                ,[AddedCount]
                                ,[OnlineCount]
                                ,[StopPaymentCount]
                                ,[StaticsStart]
                                ,[StaticsEnd]
                                ,[ReportType]
                                ,[CreateTime]
                                ,[TotalCount]
                            FROM  [ShopStatics] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject,ref sqlParameters, whereSqlBuilder);
            sql = whereSqlBuilder.Insert(0, sql).ToString(); 
            List<IShopStatics> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<IShopStatics>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<ShopStatics>());
                }
            }
            return list;
        }
        private static void GetWhereSqlBuilderAndSqlParameterList(ShopStaticsQueryObject queryObject, ref SqlParameter[] sqlParameters,
            StringBuilder whereSqlBuilder, int? pageSize = null, int? pageIndex = null)
        {

           
            if (queryObject == null)
            {
                if (pageIndex.HasValue && pageSize.HasValue)
                {
                    sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter("@PageSize", pageSize.Value),
                        new SqlParameter("@PageIndex", pageSize.Value * (pageIndex.Value - 1))
                    };
                }
                return;
            }
            var sqlParameterList = new List<SqlParameter>(); 
            if (queryObject.ShopStaticsId.HasValue)
            {
                whereSqlBuilder.Append(" AND ShopStaticsId = @ShopStaticsId ");
                sqlParameterList.Add(new SqlParameter("@ShopStaticsId ", SqlDbType.Int, 4) { Value = queryObject.ShopStaticsId });
            }
            if (queryObject.CityId.HasValue)
            {
                whereSqlBuilder.Append(" AND CityId = @CityId ");
                sqlParameterList.Add(new SqlParameter("@CityId ", SqlDbType.Int, 4) { Value = queryObject.CityId });
            }
            if (queryObject.AddedCount.HasValue)
            {
                whereSqlBuilder.Append(" AND AddedCount = @AddedCount ");
                sqlParameterList.Add(new SqlParameter("@AddedCount ", SqlDbType.Int, 4) { Value = queryObject.AddedCount });
            }
            if (queryObject.AddedCountFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND AddedCount >= @AddedCountFrom ");
                sqlParameterList.Add(new SqlParameter("@AddedCountFrom", SqlDbType.Int, 4) { Value = queryObject.AddedCountFrom });
            }
            if (queryObject.AddedCountTo.HasValue)
            {
                whereSqlBuilder.Append(" AND AddedCount <= @AddedCountTo ");
                sqlParameterList.Add(new SqlParameter("@AddedCountTo", SqlDbType.Int, 4) { Value = queryObject.AddedCountTo });
            }
            if (queryObject.OnlineCount.HasValue)
            {
                whereSqlBuilder.Append(" AND OnlineCount = @OnlineCount ");
                sqlParameterList.Add(new SqlParameter("@OnlineCount ", SqlDbType.Int, 4) { Value = queryObject.OnlineCount });
            }
            if (queryObject.OnlineCountFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND OnlineCount >= @OnlineCountFrom ");
                sqlParameterList.Add(new SqlParameter("@OnlineCountFrom", SqlDbType.Int, 4) { Value = queryObject.OnlineCountFrom });
            }
            if (queryObject.OnlineCountTo.HasValue)
            {
                whereSqlBuilder.Append(" AND OnlineCount <= @OnlineCountTo ");
                sqlParameterList.Add(new SqlParameter("@OnlineCountTo", SqlDbType.Int, 4) { Value = queryObject.OnlineCountTo });
            }
            if (queryObject.StopPaymentCount.HasValue)
            {
                whereSqlBuilder.Append(" AND StopPaymentCount = @StopPaymentCount ");
                sqlParameterList.Add(new SqlParameter("@StopPaymentCount ", SqlDbType.Int, 4) { Value = queryObject.StopPaymentCount });
            }
            if (queryObject.StaticsStart.HasValue)
            {
                whereSqlBuilder.Append(" AND StaticsStart = @StaticsStart ");
                sqlParameterList.Add(new SqlParameter("@StaticsStart ", SqlDbType.DateTime, 8) { Value = queryObject.StaticsStart });
            }
            if (queryObject.StaticsStartFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND StaticsStart >= @StaticsStartFrom ");
                sqlParameterList.Add(new SqlParameter("@StaticsStartFrom", SqlDbType.DateTime, 8) { Value = queryObject.StaticsStartFrom });
            }
            if (queryObject.StaticsStartTo.HasValue)
            {
                whereSqlBuilder.Append(" AND StaticsStart <= @StaticsStartTo ");
                sqlParameterList.Add(new SqlParameter("@StaticsStartTo", SqlDbType.DateTime, 8) { Value = queryObject.StaticsStartTo });
            }
            if (queryObject.StaticsEnd.HasValue)
            {
                whereSqlBuilder.Append(" AND StaticsEnd = @StaticsEnd ");
                sqlParameterList.Add(new SqlParameter("@StaticsEnd ", SqlDbType.DateTime, 8) { Value = queryObject.StaticsEnd });
            }
            if (queryObject.StaticsEndFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND StaticsEnd >= @StaticsEndFrom ");
                sqlParameterList.Add(new SqlParameter("@StaticsEndFrom", SqlDbType.DateTime, 8) { Value = queryObject.StaticsEndFrom });
            }
            if (queryObject.StaticsEndTo.HasValue)
            {
                whereSqlBuilder.Append(" AND StaticsEnd <= @StaticsEndTo ");
                sqlParameterList.Add(new SqlParameter("@StaticsEndTo", SqlDbType.DateTime, 8) { Value = queryObject.StaticsEndTo });
            }
            if (queryObject.ReportType.HasValue)
            {
                whereSqlBuilder.Append(" AND ReportType = @ReportType ");
                sqlParameterList.Add(new SqlParameter("@ReportType ", SqlDbType.Int, 4) { Value = queryObject.ReportType });
            }
            if (queryObject.CreateTime.HasValue)
            {
                whereSqlBuilder.Append(" AND CreateTime = @CreateTime ");
                sqlParameterList.Add(new SqlParameter("@CreateTime ", SqlDbType.DateTime, 8) { Value = queryObject.CreateTime });
            }
            if (queryObject.CreateTimeFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND CreateTime >= @CreateTimeFrom ");
                sqlParameterList.Add(new SqlParameter("@CreateTimeFrom", SqlDbType.DateTime, 8) { Value = queryObject.CreateTimeFrom });
            }
            if (queryObject.CreateTimeTo.HasValue)
            {
                whereSqlBuilder.Append(" AND CreateTime <= @CreateTimeTo ");
                sqlParameterList.Add(new SqlParameter("@CreateTimeTo", SqlDbType.DateTime, 8) { Value = queryObject.CreateTimeTo });
            }
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@PageSize", pageSize));
                sqlParameterList.Add(new SqlParameter("@PageIndex", pageSize * (pageIndex - 1)));
            }
            sqlParameters = sqlParameterList.ToArray();
        }
        
        private static StringBuilder GetOrderSql(ShopStaticsOrderColumn orderColumn, SortOrder order)
        {
            StringBuilder orderSqlBuilder = new StringBuilder(" ORDER BY ");
            switch (orderColumn)
            {
                case ShopStaticsOrderColumn.ShopStaticsId:
                    orderSqlBuilder.Append("ShopStaticsId");
                    break;
                case ShopStaticsOrderColumn.CityId:
                    orderSqlBuilder.Append("CityId");
                    break;
                case ShopStaticsOrderColumn.AddedCount:
                    orderSqlBuilder.Append("AddedCount");
                    break;
                case ShopStaticsOrderColumn.OnlineCount:
                    orderSqlBuilder.Append("OnlineCount");
                    break;
                case ShopStaticsOrderColumn.StopPaymentCount:
                    orderSqlBuilder.Append("StopPaymentCount");
                    break;
                case ShopStaticsOrderColumn.StaticsStart:
                    orderSqlBuilder.Append("StaticsStart");
                    break;
                case ShopStaticsOrderColumn.StaticsEnd:
                    orderSqlBuilder.Append("StaticsEnd");
                    break;
                case ShopStaticsOrderColumn.ReportType:
                    orderSqlBuilder.Append("ReportType");
                    break;
                case ShopStaticsOrderColumn.CreateTime:
                    orderSqlBuilder.Append("CreateTime");
                    break;
            }
            if (order == SortOrder.Ascending)
            {
                orderSqlBuilder.Append(" ASC ");
            }
            else
            {
                orderSqlBuilder.Append(" DESC ");
            }
            return orderSqlBuilder;
        }
    }
}