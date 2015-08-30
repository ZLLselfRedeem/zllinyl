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
    public partial class ShopStaticsReportManager
    {
        public bool IsExists(long ShopStaticsReportId)
        {
            string sql = "SELECT COUNT(0) FROM ShopStaticsReport WHERE ShopStaticsReportId = @ShopStaticsReportId";
            var retutnValue =
                SqlHelper.ExecuteScalar(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@ShopStaticsReportId", SqlDbType.Int, 8) { Value = ShopStaticsReportId });
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

        public ShopStaticsReport GetEntityById(long ShopStaticsReportId)
        {
            string sql = @"SELECT [ShopStaticsReportId]
                                ,[ShopId]
                                ,[CompanyId]
                                ,[CompanyName]
                                ,[ShopName]
                                ,[PreorderCount]
                                ,[MaxPreorderSum]
                                ,[AveragePreorderSum]
                                ,[TotalPreorderSum]
                                ,[CompareRise]
                                ,[SaveTime]
                                ,[OldCustomerCount]
                                ,[TotalCustomerCount]
                                ,[TotalEvaluationCount]
                                ,[GoodEvaluationCount]
                                ,[OldCustomerGoodEvaluationCount]
                                ,[Ranking]
                                ,[AccountManager]
                                ,[StaticsStart]
                                ,[StaticsEnd]
                                ,[ReportType],RefoundSum,DeductibleAmountSum,RedEnvelopeSum,OldCustomerEvaluationCount
                            FROM  [ShopStaticsReport] 
                            WHERE [ShopStaticsReportId] = @ShopStaticsReportId ";
            using (var reader =
                   SqlHelper.ExecuteReader(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@ShopStaticsReportId", SqlDbType.BigInt, 8) { Value = ShopStaticsReportId }))
            {
                if (reader.Read())
                {
                    var entity = reader.GetEntity<ShopStaticsReport>();
                    return entity;
                }
            }
            return null;
        }

        public bool Add(ShopStaticsReport Entity)
        {
            string sql = @"INSERT INTO ShopStaticsReport (
                                    [ShopId],
                                    [CompanyId],
                                    [CompanyName],
                                    [ShopName],
                                    [PreorderCount],
                                    [MaxPreorderSum],
                                    [AveragePreorderSum],
                                    [TotalPreorderSum],
                                    [CompareRise],
                                    [SaveTime],
                                    [OldCustomerCount],
                                    [TotalCustomerCount],
                                    [TotalEvaluationCount],
                                    [GoodEvaluationCount],
                                    [OldCustomerGoodEvaluationCount],
                                    [Ranking],
                                    [AccountManager],
                                    [StaticsStart],
                                    [StaticsEnd],
                                    [ReportType],RefoundSum,DeductibleAmountSum,RedEnvelopeSum,CityId
                                ) VALUES (
                                    @ShopId,
                                    @CompanyId,
                                    @CompanyName,
                                    @ShopName,
                                    @PreorderCount,
                                    @MaxPreorderSum,
                                    @AveragePreorderSum,
                                    @TotalPreorderSum,
                                    @CompareRise,
                                    @SaveTime,
                                    @OldCustomerCount,
                                    @TotalCustomerCount,
                                    @TotalEvaluationCount,
                                    @GoodEvaluationCount,
                                    @OldCustomerGoodEvaluationCount,
                                    @Ranking,
                                    @AccountManager,
                                    @StaticsStart,
                                    @StaticsEnd,
                                    @ReportType,@RefoundSum,@DeductibleAmountSum,@RedEnvelopeSum,@CityId
                                    );                                    
                                    SELECT @@IDENTITY";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@ShopId", SqlDbType.Int, 4) { Value = Entity.ShopId });
            sqlParameterList.Add(new SqlParameter("@CompanyId", SqlDbType.Int, 4) { Value = Entity.CompanyId });
            sqlParameterList.Add(new SqlParameter("@CompanyName", SqlDbType.NVarChar, 100) { Value = Entity.CompanyName });
            sqlParameterList.Add(new SqlParameter("@ShopName", SqlDbType.NVarChar, 100) { Value = Entity.ShopName });
            sqlParameterList.Add(new SqlParameter("@PreorderCount", SqlDbType.Int, 4) { Value = Entity.PreorderCount });
            sqlParameterList.Add(new SqlParameter("@MaxPreorderSum", SqlDbType.Float, 8) { Value = Entity.MaxPreorderSum });
            sqlParameterList.Add(new SqlParameter("@AveragePreorderSum", SqlDbType.Float, 8) { Value = Entity.AveragePreorderSum });
            sqlParameterList.Add(new SqlParameter("@TotalPreorderSum", SqlDbType.Float, 8) { Value = Entity.TotalPreorderSum });
            sqlParameterList.Add(new SqlParameter("@CompareRise", SqlDbType.Float, 8) { Value = Entity.CompareRise });
            sqlParameterList.Add(new SqlParameter("@SaveTime", SqlDbType.Int, 4) { Value = Entity.SaveTime });
            sqlParameterList.Add(new SqlParameter("@OldCustomerCount", SqlDbType.Int, 4) { Value = Entity.OldCustomerCount });
            sqlParameterList.Add(new SqlParameter("@TotalCustomerCount", SqlDbType.Int, 4) { Value = Entity.TotalCustomerCount });
            sqlParameterList.Add(new SqlParameter("@TotalEvaluationCount", SqlDbType.Int, 4) { Value = Entity.TotalEvaluationCount });
            sqlParameterList.Add(new SqlParameter("@GoodEvaluationCount", SqlDbType.Int, 4) { Value = Entity.GoodEvaluationCount });
            sqlParameterList.Add(new SqlParameter("@OldCustomerGoodEvaluationCount", SqlDbType.Float, 8) { Value = Entity.OldCustomerGoodEvaluationCount });
            sqlParameterList.Add(new SqlParameter("@Ranking", SqlDbType.Int, 4) { Value = Entity.Ranking });
            sqlParameterList.Add(new SqlParameter("@AccountManager", SqlDbType.BigInt, 8) { Value = Entity.AccountManager });
            sqlParameterList.Add(new SqlParameter("@StaticsStart", SqlDbType.DateTime, 8) { Value = Entity.StaticsStart });
            sqlParameterList.Add(new SqlParameter("@StaticsEnd", SqlDbType.DateTime, 8) { Value = Entity.StaticsEnd });
            sqlParameterList.Add(new SqlParameter("@ReportType", SqlDbType.Int, 4) { Value = Entity.ReportType });
            sqlParameterList.Add(new SqlParameter("@RefoundSum", SqlDbType.Float, 8) { Value = Entity.RefoundSum });
            sqlParameterList.Add(new SqlParameter("@DeductibleAmountSum", SqlDbType.Float, 8) { Value = Entity.DeductibleAmountSum });
            sqlParameterList.Add(new SqlParameter("@RedEnvelopeSum", SqlDbType.Float, 8) { Value = Entity.RedEnvelopeSum });
            sqlParameterList.Add(new SqlParameter("@CityId", SqlDbType.Int, 4) { Value = Entity.CityId });
            var returnObject =
                SqlHelper.ExecuteScalar(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            int id;
            if (int.TryParse(returnObject.ToString(), out id))
            {
                Entity.ShopStaticsReportId = id;
                return true;
            }
            return false;
        }

        public bool AddList(List<ShopStaticsReport> list)
        {
            var insertTable = list.GetTableFromList();
            if (insertTable != null && insertTable.Rows.Count > 0)
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction))
                {
                    
                    sqlBulkCopy.DestinationTableName = "ShopStaticsReport";
                    foreach (DataColumn column in insertTable.Columns)
                    {
                        sqlBulkCopy.ColumnMappings.Add(column.ColumnName.Trim(), column.ColumnName.Trim());
                    }
                    sqlBulkCopy.WriteToServer(insertTable);
                    return true;
                }
            }
            return false;
        } 
        public bool Update(ShopStaticsReport Entity)
        {
            string sql = @"UPDATE [ShopStaticsReport] SET
                                 [ShopId] = @ShopId
                                ,[CompanyId] = @CompanyId
                                ,[CompanyName] = @CompanyName
                                ,[ShopName] = @ShopName
                                ,[PreorderCount] = @PreorderCount
                                ,[MaxPreorderSum] = @MaxPreorderSum
                                ,[AveragePreorderSum] = @AveragePreorderSum
                                ,[TotalPreorderSum] = @TotalPreorderSum
                                ,[CompareRise] = @CompareRise
                                ,[SaveTime] = @SaveTime
                                ,[OldCustomerCount] = @OldCustomerCount
                                ,[TotalCustomerCount] = @TotalCustomerCount
                                ,[TotalEvaluationCount] = @TotalEvaluationCount
                                ,[GoodEvaluationCount] = @GoodEvaluationCount
                                ,[OldCustomerGoodEvaluationCount] = @OldCustomerGoodEvaluationCount
                                ,[Ranking] = @Ranking
                                ,[AccountManager] = @AccountManager
                                ,[StaticsStart] = @StaticsStart
                                ,[StaticsEnd] = @StaticsEnd
                                ,[ReportType] = @ReportType
                                ,[CityId] = @CityId
                           WHERE [ShopStaticsReportId] =@ShopStaticsReportId";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@ShopStaticsReportId", SqlDbType.BigInt, 8) { Value = Entity.ShopStaticsReportId });
            sqlParameterList.Add(new SqlParameter("@ShopId", SqlDbType.Int, 4) { Value = Entity.ShopId });
            sqlParameterList.Add(new SqlParameter("@CompanyId", SqlDbType.Int, 4) { Value = Entity.CompanyId });
            sqlParameterList.Add(new SqlParameter("@CompanyName", SqlDbType.NVarChar, 100) { Value = Entity.CompanyName });
            sqlParameterList.Add(new SqlParameter("@ShopName", SqlDbType.NVarChar, 100) { Value = Entity.ShopName });
            sqlParameterList.Add(new SqlParameter("@PreorderCount", SqlDbType.Int, 4) { Value = Entity.PreorderCount });
            sqlParameterList.Add(new SqlParameter("@MaxPreorderSum", SqlDbType.Float, 8) { Value = Entity.MaxPreorderSum });
            sqlParameterList.Add(new SqlParameter("@AveragePreorderSum", SqlDbType.Float, 8) { Value = Entity.AveragePreorderSum });
            sqlParameterList.Add(new SqlParameter("@TotalPreorderSum", SqlDbType.Float, 8) { Value = Entity.TotalPreorderSum });
            sqlParameterList.Add(new SqlParameter("@CompareRise", SqlDbType.Float, 8) { Value = Entity.CompareRise });
            sqlParameterList.Add(new SqlParameter("@SaveTime", SqlDbType.Int, 4) { Value = Entity.SaveTime });
            sqlParameterList.Add(new SqlParameter("@OldCustomerCount", SqlDbType.Int, 4) { Value = Entity.OldCustomerCount });
            sqlParameterList.Add(new SqlParameter("@TotalCustomerCount", SqlDbType.Int, 4) { Value = Entity.TotalCustomerCount });
            sqlParameterList.Add(new SqlParameter("@TotalEvaluationCount", SqlDbType.Int, 4) { Value = Entity.TotalEvaluationCount });
            sqlParameterList.Add(new SqlParameter("@GoodEvaluationCount", SqlDbType.Int, 4) { Value = Entity.GoodEvaluationCount });
            sqlParameterList.Add(new SqlParameter("@OldCustomerGoodEvaluationCount", SqlDbType.Float, 8) { Value = Entity.OldCustomerGoodEvaluationCount });
            sqlParameterList.Add(new SqlParameter("@Ranking", SqlDbType.Int, 4) { Value = Entity.Ranking });
            sqlParameterList.Add(new SqlParameter("@AccountManager", SqlDbType.BigInt, 8) { Value = Entity.AccountManager });
            sqlParameterList.Add(new SqlParameter("@StaticsStart", SqlDbType.DateTime, 8) { Value = Entity.StaticsStart });
            sqlParameterList.Add(new SqlParameter("@StaticsEnd", SqlDbType.DateTime, 8) { Value = Entity.StaticsEnd });
            sqlParameterList.Add(new SqlParameter("@ReportType", SqlDbType.Int, 4) { Value = Entity.ReportType });
            sqlParameterList.Add(new SqlParameter("@CityId", SqlDbType.Int, 4) { Value = Entity.CityId });
            var count = SqlHelper.ExecuteNonQuery(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteEntity(ShopStaticsReport Entity)
        {
            string sql = @"DELETE FROM [ShopStaticsReport]
                                 WHERE [ShopStaticsReportId] =@ShopStaticsReportId";
            var count =
                    SqlHelper.ExecuteNonQuery(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@ShopStaticsReportId", SqlDbType.BigInt, 8) { Value = Entity.ShopStaticsReportId });
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public long GetCountByQuery(ShopStaticsReportQueryObject queryObject = null)
        {
            string sql = @"SELECT COUNT(0)
                            FROM  [ShopStaticsReport] 
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

        public List<ShopStaticsReport> GetListByQuery(int pageSize, int pageIndex, ShopStaticsReportQueryObject queryObject = null, ShopStaticsReportOrderColumn orderColumn = ShopStaticsReportOrderColumn.ShopStaticsReportId, SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[ShopStaticsReportId]
                                ,[ShopId]
                                ,[CompanyId]
                                ,[CompanyName]
                                ,[ShopName]
                                ,[PreorderCount]
                                ,[MaxPreorderSum]
                                ,[AveragePreorderSum]
                                ,[TotalPreorderSum]
                                ,[CompareRise]
                                ,[SaveTime]
                                ,[OldCustomerCount]
                                ,[TotalCustomerCount]
                                ,[TotalEvaluationCount]
                                ,[GoodEvaluationCount]
                                ,[OldCustomerGoodEvaluationCount]
                                ,[Ranking]
                                ,[AccountManager]
                                ,[StaticsStart]
                                ,[StaticsEnd]
                                ,[ReportType],RefoundSum,DeductibleAmountSum,RedEnvelopeSum,OldCustomerEvaluationCount,CityId
                            FROM  [ShopStaticsReport] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder, pageSize, pageIndex);
            sql = whereSqlBuilder.Insert(0, sql).ToString();
            sql = string.Format(" SELECT TOP (@PageSize) * FROM ({0}) T WHERE T.ROWNUM > @PageIndex ", sql);
            List<ShopStaticsReport> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<ShopStaticsReport>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<ShopStaticsReport>());
                }
            }
            return list;
        }

        public List<ShopStaticsReport> GetListByQuery(ShopStaticsReportQueryObject queryObject = null, ShopStaticsReportOrderColumn orderColumn = ShopStaticsReportOrderColumn.ShopStaticsReportId, SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[ShopStaticsReportId]
                                ,[ShopId]
                                ,[CompanyId]
                                ,[CompanyName]
                                ,[ShopName]
                                ,[PreorderCount]
                                ,[MaxPreorderSum]
                                ,[AveragePreorderSum]
                                ,[TotalPreorderSum]
                                ,[CompareRise]
                                ,[SaveTime]
                                ,[OldCustomerCount]
                                ,[TotalCustomerCount]
                                ,[TotalEvaluationCount]
                                ,[GoodEvaluationCount]
                                ,[OldCustomerGoodEvaluationCount]
                                ,[Ranking]
                                ,[AccountManager]
                                ,[StaticsStart]
                                ,[StaticsEnd]
                                ,[ReportType],RefoundSum,DeductibleAmountSum,RedEnvelopeSum,OldCustomerEvaluationCount,CityId
                            FROM  [ShopStaticsReport] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder);
            sql = whereSqlBuilder.Insert(0, sql).ToString();
            List<ShopStaticsReport> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.StatisticsStatementsConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<ShopStaticsReport>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<ShopStaticsReport>());
                }
            }
            return list;
        }
        private static void GetWhereSqlBuilderAndSqlParameterList(ShopStaticsReportQueryObject queryObject, ref SqlParameter[] sqlParameters,
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
            if (queryObject.ShopStaticsReportId.HasValue)
            {
                whereSqlBuilder.Append(" AND ShopStaticsReportId = @ShopStaticsReportId ");
                sqlParameterList.Add(new SqlParameter("@ShopStaticsReportId ", SqlDbType.BigInt, 8) { Value = queryObject.ShopStaticsReportId });
            }
            if (queryObject.ShopId.HasValue)
            {
                whereSqlBuilder.Append(" AND ShopId = @ShopId ");
                sqlParameterList.Add(new SqlParameter("@ShopId ", SqlDbType.Int, 4) { Value = queryObject.ShopId });
            }
            if (queryObject.CompanyId.HasValue)
            {
                whereSqlBuilder.Append(" AND CompanyId = @CompanyId ");
                sqlParameterList.Add(new SqlParameter("@CompanyId ", SqlDbType.Int, 4) { Value = queryObject.CompanyId });
            }
            if (!string.IsNullOrEmpty(queryObject.CompanyName))
            {
                whereSqlBuilder.Append(" AND CompanyName = @CompanyName ");
                sqlParameterList.Add(new SqlParameter("@CompanyName", SqlDbType.NVarChar, 100) { Value = queryObject.CompanyName });
            }
            if (!string.IsNullOrEmpty(queryObject.CompanyNameFuzzy))
            {
                whereSqlBuilder.Append(" AND CompanyName LIKE @CompanyNameFuzzy ");
                sqlParameterList.Add(new SqlParameter("@CompanyNameFuzzy", SqlDbType.NVarChar, 100) { Value = string.Format("%{0}%", queryObject.CompanyNameFuzzy) });
            }
            if (!string.IsNullOrEmpty(queryObject.ShopName))
            {
                whereSqlBuilder.Append(" AND ShopName = @ShopName ");
                sqlParameterList.Add(new SqlParameter("@ShopName", SqlDbType.NVarChar, 100) { Value = queryObject.ShopName });
            }
            if (!string.IsNullOrEmpty(queryObject.ShopNameFuzzy))
            {
                whereSqlBuilder.Append(" AND ShopName LIKE @ShopNameFuzzy ");
                sqlParameterList.Add(new SqlParameter("@ShopNameFuzzy", SqlDbType.NVarChar, 100) { Value = string.Format("%{0}%", queryObject.ShopNameFuzzy) });
            }
            if (queryObject.PreorderCount.HasValue)
            {
                whereSqlBuilder.Append(" AND PreorderCount = @PreorderCount ");
                sqlParameterList.Add(new SqlParameter("@PreorderCount ", SqlDbType.Int, 4) { Value = queryObject.PreorderCount });
            }
            if (queryObject.PreorderCountFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND PreorderCount >= @PreorderCountFrom ");
                sqlParameterList.Add(new SqlParameter("@PreorderCountFrom", SqlDbType.Int, 4) { Value = queryObject.PreorderCountFrom });
            }
            if (queryObject.PreorderCountTo.HasValue)
            {
                whereSqlBuilder.Append(" AND PreorderCount <= @PreorderCountTo ");
                sqlParameterList.Add(new SqlParameter("@PreorderCountTo", SqlDbType.Int, 4) { Value = queryObject.PreorderCountTo });
            }
            if (queryObject.MaxPreorderSum.HasValue)
            {
                whereSqlBuilder.Append(" AND MaxPreorderSum = @MaxPreorderSum ");
                sqlParameterList.Add(new SqlParameter("@MaxPreorderSum ", SqlDbType.Float, 8) { Value = queryObject.MaxPreorderSum });
            }
            if (queryObject.MaxPreorderSumFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND MaxPreorderSum >= @MaxPreorderSumFrom ");
                sqlParameterList.Add(new SqlParameter("@MaxPreorderSumFrom", SqlDbType.Float, 8) { Value = queryObject.MaxPreorderSumFrom });
            }
            if (queryObject.MaxPreorderSumTo.HasValue)
            {
                whereSqlBuilder.Append(" AND MaxPreorderSum <= @MaxPreorderSumTo ");
                sqlParameterList.Add(new SqlParameter("@MaxPreorderSumTo", SqlDbType.Float, 8) { Value = queryObject.MaxPreorderSumTo });
            }
            if (queryObject.AveragePreorderSum.HasValue)
            {
                whereSqlBuilder.Append(" AND AveragePreorderSum = @AveragePreorderSum ");
                sqlParameterList.Add(new SqlParameter("@AveragePreorderSum ", SqlDbType.Float, 8) { Value = queryObject.AveragePreorderSum });
            }
            if (queryObject.AveragePreorderSumFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND AveragePreorderSum >= @AveragePreorderSumFrom ");
                sqlParameterList.Add(new SqlParameter("@AveragePreorderSumFrom", SqlDbType.Float, 8) { Value = queryObject.AveragePreorderSumFrom });
            }
            if (queryObject.AveragePreorderSumTo.HasValue)
            {
                whereSqlBuilder.Append(" AND AveragePreorderSum <= @AveragePreorderSumTo ");
                sqlParameterList.Add(new SqlParameter("@AveragePreorderSumTo", SqlDbType.Float, 8) { Value = queryObject.AveragePreorderSumTo });
            }
            if (queryObject.TotalPreorderSum.HasValue)
            {
                whereSqlBuilder.Append(" AND TotalPreorderSum = @TotalPreorderSum ");
                sqlParameterList.Add(new SqlParameter("@TotalPreorderSum ", SqlDbType.Float, 8) { Value = queryObject.TotalPreorderSum });
            }
            if (queryObject.TotalPreorderSumFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND TotalPreorderSum >= @TotalPreorderSumFrom ");
                sqlParameterList.Add(new SqlParameter("@TotalPreorderSumFrom", SqlDbType.Float, 8) { Value = queryObject.TotalPreorderSumFrom });
            }
            if (queryObject.TotalPreorderSumTo.HasValue)
            {
                whereSqlBuilder.Append(" AND TotalPreorderSum <= @TotalPreorderSumTo ");
                sqlParameterList.Add(new SqlParameter("@TotalPreorderSumTo", SqlDbType.Float, 8) { Value = queryObject.TotalPreorderSumTo });
            }
            if (queryObject.CompareRise.HasValue)
            {
                whereSqlBuilder.Append(" AND CompareRise = @CompareRise ");
                sqlParameterList.Add(new SqlParameter("@CompareRise ", SqlDbType.Float, 8) { Value = queryObject.CompareRise });
            }
            if (queryObject.SaveTime.HasValue)
            {
                whereSqlBuilder.Append(" AND SaveTime = @SaveTime ");
                sqlParameterList.Add(new SqlParameter("@SaveTime ", SqlDbType.Int, 4) { Value = queryObject.SaveTime });
            }
            if (queryObject.OldCustomerCount.HasValue)
            {
                whereSqlBuilder.Append(" AND OldCustomerCount = @OldCustomerCount ");
                sqlParameterList.Add(new SqlParameter("@OldCustomerCount ", SqlDbType.Int, 4) { Value = queryObject.OldCustomerCount });
            }
            if (queryObject.OldCustomerCountFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND OldCustomerCount >= @OldCustomerCountFrom ");
                sqlParameterList.Add(new SqlParameter("@OldCustomerCountFrom", SqlDbType.Int, 4) { Value = queryObject.OldCustomerCountFrom });
            }
            if (queryObject.OldCustomerCountTo.HasValue)
            {
                whereSqlBuilder.Append(" AND OldCustomerCount <= @OldCustomerCountTo ");
                sqlParameterList.Add(new SqlParameter("@OldCustomerCountTo", SqlDbType.Int, 4) { Value = queryObject.OldCustomerCountTo });
            }
            if (queryObject.TotalCustomerCount.HasValue)
            {
                whereSqlBuilder.Append(" AND TotalCustomerCount = @TotalCustomerCount ");
                sqlParameterList.Add(new SqlParameter("@TotalCustomerCount ", SqlDbType.Int, 4) { Value = queryObject.TotalCustomerCount });
            }
            if (queryObject.TotalCustomerCountFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND TotalCustomerCount >= @TotalCustomerCountFrom ");
                sqlParameterList.Add(new SqlParameter("@TotalCustomerCountFrom", SqlDbType.Int, 4) { Value = queryObject.TotalCustomerCountFrom });
            }
            if (queryObject.TotalCustomerCountTo.HasValue)
            {
                whereSqlBuilder.Append(" AND TotalCustomerCount <= @TotalCustomerCountTo ");
                sqlParameterList.Add(new SqlParameter("@TotalCustomerCountTo", SqlDbType.Int, 4) { Value = queryObject.TotalCustomerCountTo });
            }
            if (queryObject.TotalEvaluationCount.HasValue)
            {
                whereSqlBuilder.Append(" AND TotalEvaluationCount = @TotalEvaluationCount ");
                sqlParameterList.Add(new SqlParameter("@TotalEvaluationCount ", SqlDbType.Int, 4) { Value = queryObject.TotalEvaluationCount });
            }
            if (queryObject.TotalEvaluationCountFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND TotalEvaluationCount >= @TotalEvaluationCountFrom ");
                sqlParameterList.Add(new SqlParameter("@TotalEvaluationCountFrom", SqlDbType.Int, 4) { Value = queryObject.TotalEvaluationCountFrom });
            }
            if (queryObject.TotalEvaluationCountTo.HasValue)
            {
                whereSqlBuilder.Append(" AND TotalEvaluationCount <= @TotalEvaluationCountTo ");
                sqlParameterList.Add(new SqlParameter("@TotalEvaluationCountTo", SqlDbType.Int, 4) { Value = queryObject.TotalEvaluationCountTo });
            }
            if (queryObject.GoodEvaluationCount.HasValue)
            {
                whereSqlBuilder.Append(" AND GoodEvaluationCount = @GoodEvaluationCount ");
                sqlParameterList.Add(new SqlParameter("@GoodEvaluationCount ", SqlDbType.Int, 4) { Value = queryObject.GoodEvaluationCount });
            }
            if (queryObject.GoodEvaluationCountFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND GoodEvaluationCount >= @GoodEvaluationCountFrom ");
                sqlParameterList.Add(new SqlParameter("@GoodEvaluationCountFrom", SqlDbType.Int, 4) { Value = queryObject.GoodEvaluationCountFrom });
            }
            if (queryObject.GoodEvaluationCountTo.HasValue)
            {
                whereSqlBuilder.Append(" AND GoodEvaluationCount <= @GoodEvaluationCountTo ");
                sqlParameterList.Add(new SqlParameter("@GoodEvaluationCountTo", SqlDbType.Int, 4) { Value = queryObject.GoodEvaluationCountTo });
            }
            if (queryObject.OldCustomerGoodEvaluationCount.HasValue)
            {
                whereSqlBuilder.Append(" AND OldCustomerGoodEvaluationCount = @OldCustomerGoodEvaluationCount ");
                sqlParameterList.Add(new SqlParameter("@OldCustomerGoodEvaluationCount ", SqlDbType.Float, 8) { Value = queryObject.OldCustomerGoodEvaluationCount });
            }
            if (queryObject.OldCustomerGoodEvaluationCountFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND OldCustomerGoodEvaluationCount >= @OldCustomerGoodEvaluationCountFrom ");
                sqlParameterList.Add(new SqlParameter("@OldCustomerGoodEvaluationCountFrom", SqlDbType.Float, 8) { Value = queryObject.OldCustomerGoodEvaluationCountFrom });
            }
            if (queryObject.OldCustomerGoodEvaluationCountTo.HasValue)
            {
                whereSqlBuilder.Append(" AND OldCustomerGoodEvaluationCount <= @OldCustomerGoodEvaluationCountTo ");
                sqlParameterList.Add(new SqlParameter("@OldCustomerGoodEvaluationCountTo", SqlDbType.Float, 8) { Value = queryObject.OldCustomerGoodEvaluationCountTo });
            }
            if (queryObject.Ranking.HasValue)
            {
                whereSqlBuilder.Append(" AND Ranking = @Ranking ");
                sqlParameterList.Add(new SqlParameter("@Ranking ", SqlDbType.Int, 4) { Value = queryObject.Ranking });
            }
            if (queryObject.AccountManager.HasValue)
            {
                whereSqlBuilder.Append(" AND AccountManager = @AccountManager ");
                sqlParameterList.Add(new SqlParameter("@AccountManager ", SqlDbType.BigInt, 8) { Value = queryObject.AccountManager });
            }
            if (queryObject.StaticsStart.HasValue)
            {
                whereSqlBuilder.Append(" AND StaticsStart = @StaticsStart ");
                sqlParameterList.Add(new SqlParameter("@StaticsStart ", SqlDbType.DateTime, 8) { Value = queryObject.StaticsStart });
            }
            if (queryObject.StaticsEnd.HasValue)
            {
                whereSqlBuilder.Append(" AND StaticsEnd = @StaticsEnd ");
                sqlParameterList.Add(new SqlParameter("@StaticsEnd ", SqlDbType.DateTime, 8) { Value = queryObject.StaticsEnd });
            }
            if (queryObject.ReportType.HasValue)
            {
                whereSqlBuilder.Append(" AND ReportType = @ReportType ");
                sqlParameterList.Add(new SqlParameter("@ReportType ", SqlDbType.Int, 4) { Value = queryObject.ReportType });
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

        private static StringBuilder GetOrderSql(ShopStaticsReportOrderColumn orderColumn, SortOrder order)
        {
            StringBuilder orderSqlBuilder = new StringBuilder(" ORDER BY ");
            switch (orderColumn)
            {
                case ShopStaticsReportOrderColumn.ShopStaticsReportId:
                    orderSqlBuilder.Append("ShopStaticsReportId");
                    break;
                case ShopStaticsReportOrderColumn.ShopId:
                    orderSqlBuilder.Append("ShopId");
                    break;
                case ShopStaticsReportOrderColumn.CompanyId:
                    orderSqlBuilder.Append("CompanyId");
                    break;
                case ShopStaticsReportOrderColumn.CompanyName:
                    orderSqlBuilder.Append("CompanyName");
                    break;
                case ShopStaticsReportOrderColumn.ShopName:
                    orderSqlBuilder.Append("ShopName");
                    break;
                case ShopStaticsReportOrderColumn.PreorderCount:
                    orderSqlBuilder.Append("PreorderCount");
                    break;
                case ShopStaticsReportOrderColumn.MaxPreorderSum:
                    orderSqlBuilder.Append("MaxPreorderSum");
                    break;
                case ShopStaticsReportOrderColumn.AveragePreorderSum:
                    orderSqlBuilder.Append("AveragePreorderSum");
                    break;
                case ShopStaticsReportOrderColumn.TotalPreorderSum:
                    orderSqlBuilder.Append("TotalPreorderSum");
                    break;
                case ShopStaticsReportOrderColumn.CompareRise:
                    orderSqlBuilder.Append("CompareRise");
                    break;
                case ShopStaticsReportOrderColumn.SaveTime:
                    orderSqlBuilder.Append("SaveTime");
                    break;
                case ShopStaticsReportOrderColumn.OldCustomerCount:
                    orderSqlBuilder.Append("OldCustomerCount");
                    break;
                case ShopStaticsReportOrderColumn.TotalCustomerCount:
                    orderSqlBuilder.Append("TotalCustomerCount");
                    break;
                case ShopStaticsReportOrderColumn.TotalEvaluationCount:
                    orderSqlBuilder.Append("TotalEvaluationCount");
                    break;
                case ShopStaticsReportOrderColumn.GoodEvaluationCount:
                    orderSqlBuilder.Append("GoodEvaluationCount");
                    break;
                case ShopStaticsReportOrderColumn.OldCustomerGoodEvaluationCount:
                    orderSqlBuilder.Append("OldCustomerGoodEvaluationCount");
                    break;
                case ShopStaticsReportOrderColumn.Ranking:
                    orderSqlBuilder.Append("Ranking");
                    break;
                case ShopStaticsReportOrderColumn.AccountManager:
                    orderSqlBuilder.Append("AccountManager");
                    break;
                case ShopStaticsReportOrderColumn.StaticsStart:
                    orderSqlBuilder.Append("StaticsStart");
                    break;
                case ShopStaticsReportOrderColumn.StaticsEnd:
                    orderSqlBuilder.Append("StaticsEnd");
                    break;
                case ShopStaticsReportOrderColumn.ReportType:
                    orderSqlBuilder.Append("ReportType");
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
