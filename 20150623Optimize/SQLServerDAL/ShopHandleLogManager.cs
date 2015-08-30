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
    public partial class ShopHandleLogManager
    {
        public bool IsExists(long Id)
        {
            string sql = "SELECT COUNT(0) FROM ShopHandleLog WHERE Id = @Id";
            var retutnValue =
                SqlHelper.ExecuteScalar(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@Id",SqlDbType.Int,8) { Value = Id });
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
        
        public ShopHandleLog GetEntityById(long Id)
        {
            string sql  =  @"SELECT [Id]
                                ,[ShopName]
                                ,[ShopId]
                                ,[EmployeeId]
                                ,[EmployeeName]
                                ,[HandleStatus]
                                ,[HandleDesc]
                                ,[OperateTime]
                                ,[CityId]
                            FROM  [ShopHandleLog] 
                            WHERE [Id] = @Id ";
             using(var reader = 
                    SqlHelper.ExecuteReader(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@Id", SqlDbType.BigInt, 8){ Value = Id }))
            {
                if(reader.Read())
                {
                  var entity = reader.GetEntity<ShopHandleLog>();
                  return entity;
                }
            }
            return null;
        }
        
        public bool Add(IShopHandleLog Entity)
        {
            string sql = @"INSERT INTO ShopHandleLog (
                                    [ShopName],
                                    [ShopId],
                                    [EmployeeId],
                                    [EmployeeName],
                                    [HandleStatus],
                                    [HandleDesc],
                                    [OperateTime],
                                    [CityId]
                                ) VALUES (
                                    @ShopName,
                                    @ShopId,
                                    @EmployeeId,
                                    @EmployeeName,
                                    @HandleStatus,
                                    @HandleDesc,
                                    @OperateTime,
                                    @CityId
                                    );                                    
                                    SELECT @@IDENTITY";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@ShopName", SqlDbType.NVarChar, 500) { Value = Entity.ShopName });
            sqlParameterList.Add(new SqlParameter("@ShopId", SqlDbType.Int, 4) { Value = Entity.ShopId });
            sqlParameterList.Add(new SqlParameter("@EmployeeId", SqlDbType.Int, 4) { Value = Entity.EmployeeId });
            sqlParameterList.Add(new SqlParameter("@EmployeeName", SqlDbType.NVarChar, 500) { Value = Entity.EmployeeName });
            sqlParameterList.Add(new SqlParameter("@HandleStatus", SqlDbType.Int, 4) { Value = Entity.HandleStatus });
            sqlParameterList.Add(new SqlParameter("@HandleDesc", SqlDbType.NVarChar, 500) { Value = Entity.HandleDesc });
            sqlParameterList.Add(new SqlParameter("@OperateTime", SqlDbType.DateTime, 8) { Value = Entity.OperateTime });
            sqlParameterList.Add(new SqlParameter("@CityId", SqlDbType.Int, 4) { Value = Entity.CityId });
            var returnObject =
                SqlHelper.ExecuteScalar(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            int id;
            if(int.TryParse(returnObject.ToString(),out id))
            {
                Entity.Id = id;
                return true;
            }
            return false;
        }
        public bool Update(IShopHandleLog Entity)
        {
            string sql =@"UPDATE [ShopHandleLog] SET
                                 [ShopName] = @ShopName
                                ,[ShopId] = @ShopId
                                ,[EmployeeId] = @EmployeeId
                                ,[EmployeeName] = @EmployeeName
                                ,[HandleStatus] = @HandleStatus
                                ,[HandleDesc] = @HandleDesc
                                ,[OperateTime] = @OperateTime
                                ,[CityId] = @CityId
                           WHERE [Id] =@Id";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@Id", SqlDbType.BigInt, 8) { Value = Entity.Id });
            sqlParameterList.Add(new SqlParameter("@ShopName", SqlDbType.NVarChar, 500) { Value = Entity.ShopName });
            sqlParameterList.Add(new SqlParameter("@ShopId", SqlDbType.Int, 4) { Value = Entity.ShopId });
            sqlParameterList.Add(new SqlParameter("@EmployeeId", SqlDbType.Int, 4) { Value = Entity.EmployeeId });
            sqlParameterList.Add(new SqlParameter("@EmployeeName", SqlDbType.NVarChar, 500) { Value = Entity.EmployeeName });
            sqlParameterList.Add(new SqlParameter("@HandleStatus", SqlDbType.Int, 4) { Value = Entity.HandleStatus });
            sqlParameterList.Add(new SqlParameter("@HandleDesc", SqlDbType.NVarChar, 500) { Value = Entity.HandleDesc });
            sqlParameterList.Add(new SqlParameter("@OperateTime", SqlDbType.DateTime, 8) { Value = Entity.OperateTime });
            sqlParameterList.Add(new SqlParameter("@CityId", SqlDbType.Int, 4) { Value = Entity.CityId });
            var count = SqlHelper.ExecuteNonQuery(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        
        public bool DeleteEntity(IShopHandleLog Entity)
        {
            string sql = @"DELETE FROM [ShopHandleLog]
                                 WHERE [Id] =@Id";
            var count = 
                    SqlHelper.ExecuteNonQuery(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@Id", SqlDbType.BigInt, 8){ Value = Entity.Id });
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        
        public long GetCountByQuery(ShopHandleLogQueryObject queryObject = null)
        {
            string sql = @"SELECT COUNT(0)
                            FROM  [ShopHandleLog] 
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

        public List<IShopHandleLog> GetListByQuery(int pageSize,int pageIndex,ShopHandleLogQueryObject queryObject = null,ShopHandleLogOrderColumn orderColumn = ShopHandleLogOrderColumn.Id,SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql  =  @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[Id]
                                ,[ShopName]
                                ,[ShopId]
                                ,[EmployeeId]
                                ,[EmployeeName]
                                ,[HandleStatus]
                                ,[HandleDesc]
                                ,[OperateTime]
                                ,[CityId]
                            FROM  [ShopHandleLog] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject,ref sqlParameters, whereSqlBuilder,pageSize,pageIndex);
            sql = whereSqlBuilder.Insert(0, sql).ToString(); 
            sql =  string.Format( " SELECT TOP (@PageSize) * FROM ({0}) T WHERE T.ROWNUM > @PageIndex ",sql);
            List<IShopHandleLog> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<IShopHandleLog>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<ShopHandleLog>());
                }
            }
            return list;
        }
        
        public List<IShopHandleLog> GetListByQuery(ShopHandleLogQueryObject queryObject = null,ShopHandleLogOrderColumn orderColumn = ShopHandleLogOrderColumn.Id,SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql  =  @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[Id]
                                ,[ShopName]
                                ,[ShopId]
                                ,[EmployeeId]
                                ,[EmployeeName]
                                ,[HandleStatus]
                                ,[HandleDesc]
                                ,[OperateTime]
                                ,[CityId]
                            FROM  [ShopHandleLog] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject,ref sqlParameters, whereSqlBuilder);
            sql = whereSqlBuilder.Insert(0, sql).ToString(); 
            List<IShopHandleLog> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.MobileAppLogConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<IShopHandleLog>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<ShopHandleLog>());
                }
            }
            return list;
        }
        private static void GetWhereSqlBuilderAndSqlParameterList(ShopHandleLogQueryObject queryObject, ref SqlParameter[] sqlParameters,
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
            if (queryObject.Id.HasValue)
            {
                whereSqlBuilder.Append(" AND Id = @Id ");
                sqlParameterList.Add(new SqlParameter("@Id ", SqlDbType.BigInt, 8) { Value = queryObject.Id });
            }
            if (!string.IsNullOrEmpty(queryObject.ShopName))
            {
                whereSqlBuilder.Append(" AND ShopName = @ShopName ");
                sqlParameterList.Add(new SqlParameter("@ShopName", SqlDbType.NVarChar, 500) { Value = queryObject.ShopName });
            }
            if (!string.IsNullOrEmpty(queryObject.ShopNameFuzzy))
            {
                whereSqlBuilder.Append(" AND ShopName LIKE @ShopNameFuzzy ");
                sqlParameterList.Add(new SqlParameter("@ShopNameFuzzy", SqlDbType.NVarChar, 500) { Value = string.Format("%{0}%", queryObject.ShopNameFuzzy) });
            }
            if (queryObject.ShopId.HasValue)
            {
                whereSqlBuilder.Append(" AND ShopId = @ShopId ");
                sqlParameterList.Add(new SqlParameter("@ShopId ", SqlDbType.Int, 4) { Value = queryObject.ShopId });
            }
            if (queryObject.EmployeeId.HasValue)
            {
                whereSqlBuilder.Append(" AND EmployeeId = @EmployeeId ");
                sqlParameterList.Add(new SqlParameter("@EmployeeId ", SqlDbType.Int, 4) { Value = queryObject.EmployeeId });
            }
            if (!string.IsNullOrEmpty(queryObject.EmployeeName))
            {
                whereSqlBuilder.Append(" AND EmployeeName = @EmployeeName ");
                sqlParameterList.Add(new SqlParameter("@EmployeeName", SqlDbType.NVarChar, 500) { Value = queryObject.EmployeeName });
            }
            if (!string.IsNullOrEmpty(queryObject.EmployeeNameFuzzy))
            {
                whereSqlBuilder.Append(" AND EmployeeName LIKE @EmployeeNameFuzzy ");
                sqlParameterList.Add(new SqlParameter("@EmployeeNameFuzzy", SqlDbType.NVarChar, 500) { Value = string.Format("%{0}%", queryObject.EmployeeNameFuzzy) });
            }
            if (queryObject.HandleStatus.HasValue)
            {
                whereSqlBuilder.Append(" AND HandleStatus = @HandleStatus ");
                sqlParameterList.Add(new SqlParameter("@HandleStatus ", SqlDbType.Int, 4) { Value = queryObject.HandleStatus });
            }
            if (!string.IsNullOrEmpty(queryObject.HandleDesc))
            {
                whereSqlBuilder.Append(" AND HandleDesc = @HandleDesc ");
                sqlParameterList.Add(new SqlParameter("@HandleDesc", SqlDbType.NVarChar, 500) { Value = queryObject.HandleDesc });
            }
            if (!string.IsNullOrEmpty(queryObject.HandleDescFuzzy))
            {
                whereSqlBuilder.Append(" AND HandleDesc LIKE @HandleDescFuzzy ");
                sqlParameterList.Add(new SqlParameter("@HandleDescFuzzy", SqlDbType.NVarChar, 500) { Value = string.Format("%{0}%", queryObject.HandleDescFuzzy) });
            }
            if (queryObject.OperateTime.HasValue)
            {
                whereSqlBuilder.Append(" AND OperateTime = @OperateTime ");
                sqlParameterList.Add(new SqlParameter("@OperateTime ", SqlDbType.DateTime, 8) { Value = queryObject.OperateTime });
            }
            if (queryObject.OperateTimeFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND OperateTime >= @OperateTimeFrom ");
                sqlParameterList.Add(new SqlParameter("@OperateTimeFrom", SqlDbType.DateTime, 8) { Value = queryObject.OperateTimeFrom });
            }
            if (queryObject.OperateTimeTo.HasValue)
            {
                whereSqlBuilder.Append(" AND OperateTime <= @OperateTimeTo ");
                sqlParameterList.Add(new SqlParameter("@OperateTimeTo", SqlDbType.DateTime, 8) { Value = queryObject.OperateTimeTo });
            }
            if (queryObject.CityId.HasValue)
            {
                whereSqlBuilder.Append(" AND CityId = @CityId ");
                sqlParameterList.Add(new SqlParameter("@CityId ", SqlDbType.Int, 4) { Value = queryObject.CityId });
            }
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@PageSize", pageSize));
                sqlParameterList.Add(new SqlParameter("@PageIndex", pageSize * (pageIndex - 1)));
            }
            sqlParameters = sqlParameterList.ToArray();
        }
        
        private static StringBuilder GetOrderSql(ShopHandleLogOrderColumn orderColumn, SortOrder order)
        {
            StringBuilder orderSqlBuilder = new StringBuilder(" ORDER BY ");
            switch (orderColumn)
            {
                case ShopHandleLogOrderColumn.Id:
                    orderSqlBuilder.Append("Id");
                    break;
                case ShopHandleLogOrderColumn.ShopName:
                    orderSqlBuilder.Append("ShopName");
                    break;
                case ShopHandleLogOrderColumn.ShopId:
                    orderSqlBuilder.Append("ShopId");
                    break;
                case ShopHandleLogOrderColumn.EmployeeId:
                    orderSqlBuilder.Append("EmployeeId");
                    break;
                case ShopHandleLogOrderColumn.EmployeeName:
                    orderSqlBuilder.Append("EmployeeName");
                    break;
                case ShopHandleLogOrderColumn.HandleStatus:
                    orderSqlBuilder.Append("HandleStatus");
                    break;
                case ShopHandleLogOrderColumn.HandleDesc:
                    orderSqlBuilder.Append("HandleDesc");
                    break;
                case ShopHandleLogOrderColumn.OperateTime:
                    orderSqlBuilder.Append("OperateTime");
                    break;
                case ShopHandleLogOrderColumn.CityId:
                    orderSqlBuilder.Append("CityId");
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