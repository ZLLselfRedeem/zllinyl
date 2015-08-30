using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.QueryObject;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public partial class PreorderEvaluationManager
    {
        public bool IsExists(long PreorderEvaluationId)
        {
            string sql = "SELECT COUNT(0) FROM PreorderEvaluation WHERE PreorderEvaluationId = @PreorderEvaluationId";
            var retutnValue =
                SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@PreorderEvaluationId",SqlDbType.Int,8) { Value = PreorderEvaluationId });
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
        
        public PreorderEvaluation GetEntityById(long PreorderEvaluationId)
        {
            string sql  =  @"SELECT [PreorderEvaluationId]
                                ,[PreOrder19dianId]
                                ,[ShopId]
                                ,[CustomerId]
                                ,[EvaluationValue]
                                ,[EvaluationContent]
                                ,[EvaluationTime]
                                ,[EvaluationLevel]
                            FROM  [PreorderEvaluation] 
                            WHERE [PreorderEvaluationId] = @PreorderEvaluationId ";
             using(var reader = 
                    SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@PreorderEvaluationId", SqlDbType.BigInt, 8){ Value = PreorderEvaluationId }))
            {
                if(reader.Read())
                {
                  var entity = reader.GetEntity<PreorderEvaluation>();
                  return entity;
                }
            }
            return null;
        }
        
        public bool Add(PreorderEvaluation Entity)
        {
            string sql = @"INSERT INTO PreorderEvaluation (
                                    [PreOrder19dianId],
                                    [ShopId],
                                    [CustomerId],
                                    [EvaluationValue],
                                    [EvaluationContent],
                                    [EvaluationTime],
                                    [EvaluationLevel]
                                ) VALUES (
                                    @PreOrder19dianId,
                                    @ShopId,
                                    @CustomerId,
                                    @EvaluationValue,
                                    @EvaluationContent,
                                    @EvaluationTime,
                                    @EvaluationLevel
                                    );                                    
                                    SELECT @@IDENTITY";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@PreOrder19dianId", SqlDbType.BigInt, 8) { Value = Entity.PreOrder19dianId });
            sqlParameterList.Add(new SqlParameter("@ShopId", SqlDbType.Int, 4) { Value = Entity.ShopId });
            sqlParameterList.Add(new SqlParameter("@CustomerId", SqlDbType.BigInt, 8) { Value = Entity.CustomerId });
            sqlParameterList.Add(new SqlParameter("@EvaluationValue", SqlDbType.Int, 4) { Value = Entity.EvaluationValue });
            sqlParameterList.Add(new SqlParameter("@EvaluationContent", SqlDbType.NVarChar, 400) { Value = Entity.EvaluationContent });
            sqlParameterList.Add(new SqlParameter("@EvaluationTime", SqlDbType.DateTime, 8) { Value = Entity.EvaluationTime });
            sqlParameterList.Add(new SqlParameter("@EvaluationLevel", SqlDbType.Int, 4) { Value = Entity.EvaluationLevel });
            var returnObject =
                SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            int id;
            if(int.TryParse(returnObject.ToString(),out id))
            {
                Entity.PreorderEvaluationId = id;
                return true;
            }
            return false;
        }
        public bool Update(PreorderEvaluation Entity)
        {
            string sql =@"UPDATE [PreorderEvaluation] SET
                                 [PreOrder19dianId] = @PreOrder19dianId
                                ,[ShopId] = @ShopId
                                ,[CustomerId] = @CustomerId
                                ,[EvaluationValue] = @EvaluationValue
                                ,[EvaluationContent] = @EvaluationContent
                                ,[EvaluationTime] = @EvaluationTime
                                ,[EvaluationLevel] = @EvaluationLevel
                           WHERE [PreorderEvaluationId] =@PreorderEvaluationId";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@PreorderEvaluationId", SqlDbType.BigInt, 8) { Value = Entity.PreorderEvaluationId });
            sqlParameterList.Add(new SqlParameter("@PreOrder19dianId", SqlDbType.BigInt, 8) { Value = Entity.PreOrder19dianId });
            sqlParameterList.Add(new SqlParameter("@ShopId", SqlDbType.Int, 4) { Value = Entity.ShopId });
            sqlParameterList.Add(new SqlParameter("@CustomerId", SqlDbType.BigInt, 8) { Value = Entity.CustomerId });
            sqlParameterList.Add(new SqlParameter("@EvaluationValue", SqlDbType.Int, 4) { Value = Entity.EvaluationValue });
            sqlParameterList.Add(new SqlParameter("@EvaluationContent", SqlDbType.NVarChar, 400) { Value = Entity.EvaluationContent });
            sqlParameterList.Add(new SqlParameter("@EvaluationTime", SqlDbType.DateTime, 8) { Value = Entity.EvaluationTime });
            sqlParameterList.Add(new SqlParameter("@EvaluationLevel", SqlDbType.Int, 4) { Value = Entity.EvaluationLevel });
            var count = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        
        public bool DeleteEntity(PreorderEvaluation Entity)
        {
            string sql = @"DELETE FROM [PreorderEvaluation]
                                 WHERE [PreorderEvaluationId] =@PreorderEvaluationId";
            var count = 
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@PreorderEvaluationId", SqlDbType.BigInt, 8){ Value = Entity.PreorderEvaluationId });
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        
        public long GetCountByQuery(PreorderEvaluationQueryObject queryObject = null)
        {
            string sql = @"SELECT COUNT(0)
                            FROM  [PreorderEvaluation] 
                            WHERE 1 =1 ";
            object retutnValue;
            SqlParameter[] sqlParameters = null;
            if (queryObject != null)
            {
                StringBuilder whereSqlBuilder = new StringBuilder();
                GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder);
                sql = whereSqlBuilder.Insert(0, sql).ToString();
            }

            retutnValue = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters);
            long count;
            if (long.TryParse(retutnValue.ToString(), out count))
            {
                return count;
            }
            return 0;
        }

        public List<PreorderEvaluation> GetListByQuery(int pageSize,int pageIndex,PreorderEvaluationQueryObject queryObject = null,PreorderEvaluationOrderColumn orderColumn = PreorderEvaluationOrderColumn.PreorderEvaluationId,SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql  =  @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[PreorderEvaluationId]
                                ,[PreOrder19dianId]
                                ,[ShopId]
                                ,[CustomerId]
                                ,[EvaluationValue]
                                ,[EvaluationContent]
                                ,[EvaluationTime]
                                ,[EvaluationLevel]
                            FROM  [PreorderEvaluation] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject,ref sqlParameters, whereSqlBuilder,pageSize,pageIndex);
            sql = whereSqlBuilder.Insert(0, sql).ToString(); 
            sql =  string.Format( " SELECT TOP (@PageSize) * FROM ({0}) T WHERE T.ROWNUM > @PageIndex ",sql);
            List<PreorderEvaluation> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<PreorderEvaluation>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<PreorderEvaluation>());
                }
            }
            return list;
        }
        
        public List<PreorderEvaluation> GetListByQuery(PreorderEvaluationQueryObject queryObject = null,PreorderEvaluationOrderColumn orderColumn = PreorderEvaluationOrderColumn.PreorderEvaluationId,SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql  =  @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[PreorderEvaluationId]
                                ,[PreOrder19dianId]
                                ,[ShopId]
                                ,[CustomerId]
                                ,[EvaluationValue]
                                ,[EvaluationContent]
                                ,[EvaluationTime]
                                ,[EvaluationLevel]
                            FROM  [PreorderEvaluation] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject,ref sqlParameters, whereSqlBuilder);
            sql = whereSqlBuilder.Insert(0, sql).ToString(); 
            List<PreorderEvaluation> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<PreorderEvaluation>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<PreorderEvaluation>());
                }
            }
            return list;
        }
        private static void GetWhereSqlBuilderAndSqlParameterList(PreorderEvaluationQueryObject queryObject, ref SqlParameter[] sqlParameters,
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
            if (queryObject.PreorderEvaluationId.HasValue)
            {
                whereSqlBuilder.Append(" AND PreorderEvaluationId = @PreorderEvaluationId ");
                sqlParameterList.Add(new SqlParameter("@PreorderEvaluationId ", SqlDbType.BigInt, 8) { Value = queryObject.PreorderEvaluationId });
            }
            if (queryObject.PreOrder19dianId.HasValue)
            {
                whereSqlBuilder.Append(" AND PreOrder19dianId = @PreOrder19dianId ");
                sqlParameterList.Add(new SqlParameter("@PreOrder19dianId ", SqlDbType.BigInt, 8) { Value = queryObject.PreOrder19dianId });
            }
            if (queryObject.ShopId.HasValue)
            {
                whereSqlBuilder.Append(" AND ShopId = @ShopId ");
                sqlParameterList.Add(new SqlParameter("@ShopId ", SqlDbType.Int, 4) { Value = queryObject.ShopId });
            }
            if (queryObject.CustomerId.HasValue)
            {
                whereSqlBuilder.Append(" AND CustomerId = @CustomerId ");
                sqlParameterList.Add(new SqlParameter("@CustomerId ", SqlDbType.BigInt, 8) { Value = queryObject.CustomerId });
            }
            if (queryObject.EvaluationValue.HasValue)
            {
                whereSqlBuilder.Append(" AND EvaluationValue = @EvaluationValue ");
                sqlParameterList.Add(new SqlParameter("@EvaluationValue ", SqlDbType.Int, 4) { Value = queryObject.EvaluationValue });
            }
            if (queryObject.EvaluationValueFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND EvaluationValue >= @EvaluationValueFrom ");
                sqlParameterList.Add(new SqlParameter("@EvaluationValueFrom", SqlDbType.Int, 4) { Value = queryObject.EvaluationValueFrom });
            }
            if (queryObject.EvaluationValueTo.HasValue)
            {
                whereSqlBuilder.Append(" AND EvaluationValue <= @EvaluationValueTo ");
                sqlParameterList.Add(new SqlParameter("@EvaluationValueTo", SqlDbType.Int, 4) { Value = queryObject.EvaluationValueTo });
            }
            if (!string.IsNullOrEmpty(queryObject.EvaluationContent))
            {
                whereSqlBuilder.Append(" AND EvaluationContent = @EvaluationContent ");
                sqlParameterList.Add(new SqlParameter("@EvaluationContent", SqlDbType.NVarChar, 400) { Value = queryObject.EvaluationContent });
            }
            if (!string.IsNullOrEmpty(queryObject.EvaluationContentFuzzy))
            {
                whereSqlBuilder.Append(" AND EvaluationContent LIKE @EvaluationContentFuzzy ");
                sqlParameterList.Add(new SqlParameter("@EvaluationContentFuzzy", SqlDbType.NVarChar, 400) { Value = string.Format("%{0}%", queryObject.EvaluationContentFuzzy) });
            }
            if (queryObject.EvaluationTime.HasValue)
            {
                whereSqlBuilder.Append(" AND EvaluationTime = @EvaluationTime ");
                sqlParameterList.Add(new SqlParameter("@EvaluationTime ", SqlDbType.DateTime, 8) { Value = queryObject.EvaluationTime });
            }
            if (queryObject.EvaluationTimeFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND EvaluationTime >= @EvaluationTimeFrom ");
                sqlParameterList.Add(new SqlParameter("@EvaluationTimeFrom", SqlDbType.DateTime, 8) { Value = queryObject.EvaluationTimeFrom });
            }
            if (queryObject.EvaluationTimeTo.HasValue)
            {
                whereSqlBuilder.Append(" AND EvaluationTime <= @EvaluationTimeTo ");
                sqlParameterList.Add(new SqlParameter("@EvaluationTimeTo", SqlDbType.DateTime, 8) { Value = queryObject.EvaluationTimeTo });
            }
            if (queryObject.EvaluationLevel.HasValue)
            {
                whereSqlBuilder.Append(" AND EvaluationLevel = @EvaluationLevel ");
                sqlParameterList.Add(new SqlParameter("@EvaluationLevel ", SqlDbType.Int, 4) { Value = queryObject.EvaluationLevel });
            }
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@PageSize", pageSize));
                sqlParameterList.Add(new SqlParameter("@PageIndex", pageSize * (pageIndex - 1)));
            }
            sqlParameters = sqlParameterList.ToArray();
        }
        
        private static StringBuilder GetOrderSql(PreorderEvaluationOrderColumn orderColumn, SortOrder order)
        {
            StringBuilder orderSqlBuilder = new StringBuilder(" ORDER BY ");
            switch (orderColumn)
            {
                case PreorderEvaluationOrderColumn.PreorderEvaluationId:
                    orderSqlBuilder.Append("PreorderEvaluationId");
                    break;
                case PreorderEvaluationOrderColumn.PreOrder19dianId:
                    orderSqlBuilder.Append("PreOrder19dianId");
                    break;
                case PreorderEvaluationOrderColumn.ShopId:
                    orderSqlBuilder.Append("ShopId");
                    break;
                case PreorderEvaluationOrderColumn.CustomerId:
                    orderSqlBuilder.Append("CustomerId");
                    break;
                case PreorderEvaluationOrderColumn.EvaluationValue:
                    orderSqlBuilder.Append("EvaluationValue");
                    break;
                case PreorderEvaluationOrderColumn.EvaluationContent:
                    orderSqlBuilder.Append("EvaluationContent");
                    break;
                case PreorderEvaluationOrderColumn.EvaluationTime:
                    orderSqlBuilder.Append("EvaluationTime");
                    break;
                case PreorderEvaluationOrderColumn.EvaluationLevel:
                    orderSqlBuilder.Append("EvaluationLevel");
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