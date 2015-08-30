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
    public partial class ShopStopPaymentLogManager
    {
        public bool IsExists(int ShopStopPaymentLogId)
        {
            string sql = "SELECT COUNT(0) FROM ShopStopPaymentLog WHERE ShopStopPaymentLogId = @ShopStopPaymentLogId";
            var retutnValue =
                SqlHelper.ExecuteScalar(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@ShopStopPaymentLogId",SqlDbType.Int,4) { Value = ShopStopPaymentLogId });
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
        
        public ShopStopPaymentLog GetEntityById(int ShopStopPaymentLogId)
        {
            string sql  =  @"SELECT [ShopStopPaymentLogId]
                                ,[ShopId]
                                ,[StopPaymentTime]
                                ,[StartPaymentTime]
                                ,[State]
                                ,[CreateTime]
                                ,[CreatedBy]
                                ,[LastUpdatedBy]
                                ,[LastUpdatedTime]
                                ,[Remark]
                            FROM  [ShopStopPaymentLog] 
                            WHERE [ShopStopPaymentLogId] = @ShopStopPaymentLogId ";
             using(var reader = 
                    SqlHelper.ExecuteReader(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@ShopStopPaymentLogId", SqlDbType.Int, 4){ Value = ShopStopPaymentLogId }))
            {
                if(reader.Read())
                {
                  var entity = reader.GetEntity<ShopStopPaymentLog>();
                  return entity;
                }
            }
            return null;
        }
        
        public bool Add(IShopStopPaymentLog Entity)
        {
            string sql = @"INSERT INTO ShopStopPaymentLog (
                                    [ShopId],
                                    [StopPaymentTime],
                                    [StartPaymentTime],
                                    [State],
                                    [CreateTime],
                                    [CreatedBy],
                                    [LastUpdatedBy],
                                    [LastUpdatedTime],
                                    [Remark]
                                ) VALUES (
                                    @ShopId,
                                    @StopPaymentTime,
                                    @StartPaymentTime,
                                    @State,
                                    @CreateTime,
                                    @CreatedBy,
                                    @LastUpdatedBy,
                                    @LastUpdatedTime,
                                    @Remark
                                    );                                    
                                    SELECT @@IDENTITY";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@ShopId", SqlDbType.Int, 4) { Value = Entity.ShopId });
            sqlParameterList.Add(new SqlParameter("@StopPaymentTime", SqlDbType.DateTime, 8) { Value = Entity.StopPaymentTime });
            sqlParameterList.Add(new SqlParameter("@StartPaymentTime", SqlDbType.DateTime, 8) { Value = SqlHelper.GetDbNullValue(Entity.StartPaymentTime) });
            sqlParameterList.Add(new SqlParameter("@State", SqlDbType.Int, 4) { Value = Entity.State });
            sqlParameterList.Add(new SqlParameter("@CreateTime", SqlDbType.DateTime, 8) { Value = Entity.CreateTime });
            sqlParameterList.Add(new SqlParameter("@CreatedBy", SqlDbType.BigInt, 8) { Value = Entity.CreatedBy });
            sqlParameterList.Add(new SqlParameter("@LastUpdatedBy", SqlDbType.BigInt, 8) { Value = Entity.LastUpdatedBy });
            sqlParameterList.Add(new SqlParameter("@LastUpdatedTime", SqlDbType.DateTime, 8) { Value = Entity.LastUpdatedTime });
            sqlParameterList.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 400) { Value = Entity.Remark });
            var returnObject =
                SqlHelper.ExecuteScalar(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            int id;
            if(int.TryParse(returnObject.ToString(),out id))
            {
                Entity.ShopStopPaymentLogId = id;
                return true;
            }
            return false;
        }
        public bool Update(IShopStopPaymentLog Entity)
        {
            string sql =@"UPDATE [ShopStopPaymentLog] SET
                                 [ShopId] = @ShopId
                                ,[StopPaymentTime] = @StopPaymentTime
                                ,[StartPaymentTime] = @StartPaymentTime
                                ,[State] = @State
                                ,[CreateTime] = @CreateTime
                                ,[CreatedBy] = @CreatedBy
                                ,[LastUpdatedBy] = @LastUpdatedBy
                                ,[LastUpdatedTime] = @LastUpdatedTime
                                ,[Remark] = @Remark
                           WHERE [ShopStopPaymentLogId] =@ShopStopPaymentLogId";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@ShopStopPaymentLogId", SqlDbType.Int, 4) { Value = Entity.ShopStopPaymentLogId });
            sqlParameterList.Add(new SqlParameter("@ShopId", SqlDbType.Int, 4) { Value = Entity.ShopId });
            sqlParameterList.Add(new SqlParameter("@StopPaymentTime", SqlDbType.DateTime, 8) { Value = Entity.StopPaymentTime });
            sqlParameterList.Add(new SqlParameter("@StartPaymentTime", SqlDbType.DateTime, 8) { Value = SqlHelper.GetDbNullValue(Entity.StartPaymentTime) });
            sqlParameterList.Add(new SqlParameter("@State", SqlDbType.Int, 4) { Value = Entity.State });
            sqlParameterList.Add(new SqlParameter("@CreateTime", SqlDbType.DateTime, 8) { Value = Entity.CreateTime });
            sqlParameterList.Add(new SqlParameter("@CreatedBy", SqlDbType.BigInt, 8) { Value = Entity.CreatedBy });
            sqlParameterList.Add(new SqlParameter("@LastUpdatedBy", SqlDbType.BigInt, 8) { Value = Entity.LastUpdatedBy });
            sqlParameterList.Add(new SqlParameter("@LastUpdatedTime", SqlDbType.DateTime, 8) { Value = Entity.LastUpdatedTime });
            sqlParameterList.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 400) { Value = Entity.Remark });
            var count = SqlHelper.ExecuteNonQuery(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        
        public bool DeleteEntity(IShopStopPaymentLog Entity)
        {
            string sql = @"DELETE FROM [ShopStopPaymentLog]
                                 WHERE [ShopStopPaymentLogId] =@ShopStopPaymentLogId";
            var count = 
                    SqlHelper.ExecuteNonQuery(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@ShopStopPaymentLogId", SqlDbType.Int, 4){ Value = Entity.ShopStopPaymentLogId });
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        
        public long GetCountByQuery(ShopStopPaymentLogQueryObject queryObject = null)
        {
            string sql = @"SELECT COUNT(0)
                            FROM  [ShopStopPaymentLog] 
                            WHERE 1 =1 ";
            object retutnValue;
            SqlParameter[] sqlParameters = null;
            if (queryObject != null)
            {
                StringBuilder whereSqlBuilder = new StringBuilder();
                GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder);
                sql = whereSqlBuilder.Insert(0, sql).ToString();
            }

            retutnValue = SqlHelper.ExecuteScalar(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters);
            long count;
            if (long.TryParse(retutnValue.ToString(), out count))
            {
                return count;
            }
            return 0;
        }

        public List<IShopStopPaymentLog> GetListByQuery(int pageSize,int pageIndex,ShopStopPaymentLogQueryObject queryObject = null,ShopStopPaymentLogOrderColumn orderColumn = ShopStopPaymentLogOrderColumn.ShopStopPaymentLogId,SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql  =  @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[ShopStopPaymentLogId]
                                ,[ShopId]
                                ,[StopPaymentTime]
                                ,[StartPaymentTime]
                                ,[State]
                                ,[CreateTime]
                                ,[CreatedBy]
                                ,[LastUpdatedBy]
                                ,[LastUpdatedTime]
                                ,[Remark]
                            FROM  [ShopStopPaymentLog] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject,ref sqlParameters, whereSqlBuilder,pageSize,pageIndex);
            sql = whereSqlBuilder.Insert(0, sql).ToString(); 
            sql =  string.Format( " SELECT TOP (@PageSize) * FROM ({0}) T WHERE T.ROWNUM > @PageIndex ",sql);
            List<IShopStopPaymentLog> list = null;

            using (var reader = SqlHelper.ExecuteReader(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<IShopStopPaymentLog>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<ShopStopPaymentLog>());
                }
            }
            return list;
        }
        
        public List<IShopStopPaymentLog> GetListByQuery(ShopStopPaymentLogQueryObject queryObject = null,ShopStopPaymentLogOrderColumn orderColumn = ShopStopPaymentLogOrderColumn.ShopStopPaymentLogId,SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql  =  @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[ShopStopPaymentLogId]
                                ,[ShopId]
                                ,[StopPaymentTime]
                                ,[StartPaymentTime]
                                ,[State]
                                ,[CreateTime]
                                ,[CreatedBy]
                                ,[LastUpdatedBy]
                                ,[LastUpdatedTime]
                                ,[Remark]
                            FROM  [ShopStopPaymentLog] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject,ref sqlParameters, whereSqlBuilder);
            sql = whereSqlBuilder.Insert(0, sql).ToString(); 
            List<IShopStopPaymentLog> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<IShopStopPaymentLog>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<ShopStopPaymentLog>());
                }
            }
            return list;
        }
        private static void GetWhereSqlBuilderAndSqlParameterList(ShopStopPaymentLogQueryObject queryObject, ref SqlParameter[] sqlParameters,
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
            if (queryObject.ShopStopPaymentLogId.HasValue)
            {
                whereSqlBuilder.Append(" AND ShopStopPaymentLogId = @ShopStopPaymentLogId ");
                sqlParameterList.Add(new SqlParameter("@ShopStopPaymentLogId ", SqlDbType.Int, 4) { Value = queryObject.ShopStopPaymentLogId });
            }
            if (queryObject.ShopId.HasValue)
            {
                whereSqlBuilder.Append(" AND ShopId = @ShopId ");
                sqlParameterList.Add(new SqlParameter("@ShopId ", SqlDbType.Int, 4) { Value = queryObject.ShopId });
            }
            if (queryObject.StopPaymentTime.HasValue)
            {
                whereSqlBuilder.Append(" AND StopPaymentTime = @StopPaymentTime ");
                sqlParameterList.Add(new SqlParameter("@StopPaymentTime ", SqlDbType.DateTime, 8) { Value = queryObject.StopPaymentTime });
            }
            if (queryObject.StopPaymentTimeFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND StopPaymentTime >= @StopPaymentTimeFrom ");
                sqlParameterList.Add(new SqlParameter("@StopPaymentTimeFrom", SqlDbType.DateTime, 8) { Value = queryObject.StopPaymentTimeFrom });
            }
            if (queryObject.StopPaymentTimeTo.HasValue)
            {
                whereSqlBuilder.Append(" AND StopPaymentTime <= @StopPaymentTimeTo ");
                sqlParameterList.Add(new SqlParameter("@StopPaymentTimeTo", SqlDbType.DateTime, 8) { Value = queryObject.StopPaymentTimeTo });
            }
            if (queryObject.StartPaymentTime.HasValue)
            {
                whereSqlBuilder.Append(" AND StartPaymentTime = @StartPaymentTime ");
                sqlParameterList.Add(new SqlParameter("@StartPaymentTime ", SqlDbType.DateTime, 8) { Value = queryObject.StartPaymentTime });
            }
            if (queryObject.StartPaymentTimeFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND StartPaymentTime >= @StartPaymentTimeFrom ");
                sqlParameterList.Add(new SqlParameter("@StartPaymentTimeFrom", SqlDbType.DateTime, 8) { Value = queryObject.StartPaymentTimeFrom });
            }
            if (queryObject.StartPaymentTimeTo.HasValue)
            {
                whereSqlBuilder.Append(" AND StartPaymentTime <= @StartPaymentTimeTo ");
                sqlParameterList.Add(new SqlParameter("@StartPaymentTimeTo", SqlDbType.DateTime, 8) { Value = queryObject.StartPaymentTimeTo });
            }
            if (queryObject.State.HasValue)
            {
                whereSqlBuilder.Append(" AND State = @State ");
                sqlParameterList.Add(new SqlParameter("@State ", SqlDbType.Int, 4) { Value = queryObject.State });
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
            if (queryObject.CreatedBy.HasValue)
            {
                whereSqlBuilder.Append(" AND CreatedBy = @CreatedBy ");
                sqlParameterList.Add(new SqlParameter("@CreatedBy ", SqlDbType.BigInt, 8) { Value = queryObject.CreatedBy });
            }
            if (queryObject.LastUpdatedBy.HasValue)
            {
                whereSqlBuilder.Append(" AND LastUpdatedBy = @LastUpdatedBy ");
                sqlParameterList.Add(new SqlParameter("@LastUpdatedBy ", SqlDbType.BigInt, 8) { Value = queryObject.LastUpdatedBy });
            }
            if (queryObject.LastUpdatedTime.HasValue)
            {
                whereSqlBuilder.Append(" AND LastUpdatedTime = @LastUpdatedTime ");
                sqlParameterList.Add(new SqlParameter("@LastUpdatedTime ", SqlDbType.DateTime, 8) { Value = queryObject.LastUpdatedTime });
            }
            if (queryObject.LastUpdatedTimeFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND LastUpdatedTime >= @LastUpdatedTimeFrom ");
                sqlParameterList.Add(new SqlParameter("@LastUpdatedTimeFrom", SqlDbType.DateTime, 8) { Value = queryObject.LastUpdatedTimeFrom });
            }
            if (queryObject.LastUpdatedTimeTo.HasValue)
            {
                whereSqlBuilder.Append(" AND LastUpdatedTime <= @LastUpdatedTimeTo ");
                sqlParameterList.Add(new SqlParameter("@LastUpdatedTimeTo", SqlDbType.DateTime, 8) { Value = queryObject.LastUpdatedTimeTo });
            }
            if (!string.IsNullOrEmpty(queryObject.Remark))
            {
                whereSqlBuilder.Append(" AND Remark = @Remark ");
                sqlParameterList.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 400) { Value = queryObject.Remark });
            }
            if (!string.IsNullOrEmpty(queryObject.RemarkFuzzy))
            {
                whereSqlBuilder.Append(" AND Remark LIKE @RemarkFuzzy ");
                sqlParameterList.Add(new SqlParameter("@RemarkFuzzy", SqlDbType.NVarChar, 400) { Value = string.Format("%{0}%", queryObject.RemarkFuzzy) });
            }
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@PageSize", pageSize));
                sqlParameterList.Add(new SqlParameter("@PageIndex", pageSize * (pageIndex - 1)));
            }
            sqlParameters = sqlParameterList.ToArray();
        }
        
        private static StringBuilder GetOrderSql(ShopStopPaymentLogOrderColumn orderColumn, SortOrder order)
        {
            StringBuilder orderSqlBuilder = new StringBuilder(" ORDER BY ");
            switch (orderColumn)
            {
                case ShopStopPaymentLogOrderColumn.ShopStopPaymentLogId:
                    orderSqlBuilder.Append("ShopStopPaymentLogId");
                    break;
                case ShopStopPaymentLogOrderColumn.ShopId:
                    orderSqlBuilder.Append("ShopId");
                    break;
                case ShopStopPaymentLogOrderColumn.StopPaymentTime:
                    orderSqlBuilder.Append("StopPaymentTime");
                    break;
                case ShopStopPaymentLogOrderColumn.StartPaymentTime:
                    orderSqlBuilder.Append("StartPaymentTime");
                    break;
                case ShopStopPaymentLogOrderColumn.State:
                    orderSqlBuilder.Append("State");
                    break;
                case ShopStopPaymentLogOrderColumn.CreateTime:
                    orderSqlBuilder.Append("CreateTime");
                    break;
                case ShopStopPaymentLogOrderColumn.CreatedBy:
                    orderSqlBuilder.Append("CreatedBy");
                    break;
                case ShopStopPaymentLogOrderColumn.LastUpdatedBy:
                    orderSqlBuilder.Append("LastUpdatedBy");
                    break;
                case ShopStopPaymentLogOrderColumn.LastUpdatedTime:
                    orderSqlBuilder.Append("LastUpdatedTime");
                    break;
                case ShopStopPaymentLogOrderColumn.Remark:
                    orderSqlBuilder.Append("Remark");
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