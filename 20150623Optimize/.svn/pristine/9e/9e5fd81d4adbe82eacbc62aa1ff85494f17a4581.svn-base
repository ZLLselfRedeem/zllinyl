using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public partial class CouponSendDetailManager
    {
        public bool IsExists(long CouponSendDetailId)
        {
            string sql = "SELECT COUNT(0) FROM CouponSendDetail WHERE CouponSendDetailID = @CouponSendDetailID";
            var retutnValue =
                SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@CouponSendDetailID", SqlDbType.Int, 8) { Value = CouponSendDetailId });
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

        public CouponSendDetail GetEntityById(long CouponSendDetailId)
        {
            string sql = @"SELECT [CouponSendDetailID]
                                ,[TotalCount]
                                ,[SendCount]
                                ,[ValidityEnd]
                                ,[PreOrder19DianId]
                                  ,[ShareType]
                                  ,[CreatedBy]
                                  ,[LastUpdatedBy]
                                  ,[LastUpdateTime]
                                  ,[CreateTime]
                            FROM  [CouponSendDetail] 
                            WHERE [CouponSendDetailID] = @CouponSendDetailID ";
            using (var reader =
                   SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@CouponSendDetailID", SqlDbType.BigInt, 8) { Value = CouponSendDetailId }))
            {
                if (reader.Read())
                {
                    var entity = reader.GetEntity<CouponSendDetail>();
                    return entity;
                }
            }
            return null;
        }

        public bool Add(CouponSendDetail Entity)
        {
            string sql = @"INSERT INTO CouponSendDetail (
                                    [TotalCount],
                                    [SendCount],
                                    [ValidityEnd],
                                    [PreOrder19DianId]
                                  ,[ShareType]
                                  ,[CreatedBy]
                                  ,[LastUpdatedBy]
                                  ,[LastUpdateTime]
                                  ,[CreateTime]
                                ) VALUES (
                                    @TotalCount,
                                    @SendCount,
                                    @ValidityEnd,
                                    @PreOrder19DianId
                                  ,@ShareType
                                  ,@CreatedBy
                                  ,@LastUpdatedBy
                                  ,@LastUpdateTime
                                  ,@CreateTime
                                    );                                    
                                    SELECT @@IDENTITY";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@TotalCount", SqlDbType.Int, 4) { Value = Entity.TotalCount });
            sqlParameterList.Add(new SqlParameter("@SendCount", SqlDbType.Int, 4) { Value = Entity.SendCount });
            sqlParameterList.Add(new SqlParameter("@ValidityEnd", SqlDbType.DateTime, 8) { Value = Entity.ValidityEnd });
            sqlParameterList.Add(new SqlParameter("@PreOrder19DianId", SqlDbType.BigInt, 8) { Value = Entity.PreOrder19DianId });
            sqlParameterList.Add(new SqlParameter("@ShareType", SqlDbType.Int, 4) { Value = Entity.ShareType });
            sqlParameterList.Add(new SqlParameter("@CreatedBy", SqlDbType.Int, 4) { Value =  SqlHelper.GetDbNullValue(Entity.CreatedBy) });
            sqlParameterList.Add(new SqlParameter("@LastUpdateTime", SqlDbType.DateTime, 8) { Value = DateTime.Now });
            sqlParameterList.Add(new SqlParameter("@CreateTime", SqlDbType.DateTime, 8) { Value = SqlHelper.GetDbNullValue( Entity.CreateTime) });
            sqlParameterList.Add(new SqlParameter("@LastUpdatedBy", SqlDbType.Int, 4) { Value =  SqlHelper.GetDbNullValue(Entity.LastUpdatedBy )});
            var returnObject =
                SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            int id;
            if (int.TryParse(returnObject.ToString(), out id))
            {
                Entity.CouponSendDetailID = id;
                return true;
            }
            return false;
        }
        public bool Update(CouponSendDetail Entity)
        {
                string sql = @"UPDATE [CouponSendDetail] SET
                                     [TotalCount] = @TotalCount
                                    ,[SendCount] = @SendCount
                                    ,[ValidityEnd] = @ValidityEnd
                                    ,[PreOrder19DianId] = @PreOrder19DianId
                                  ,[ShareType] = @ShareType
                                  ,[CreatedBy] = @CreatedBy
                                  ,[LastUpdatedBy] = @LastUpdatedBy
                                  ,[LastUpdateTime] = @LastUpdateTime
                                  ,[CreateTime] = @CreateTime
                           WHERE [CouponSendDetailID] =@CouponSendDetailID";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@CouponSendDetailID", SqlDbType.BigInt, 8) { Value = Entity.CouponSendDetailID });
            sqlParameterList.Add(new SqlParameter("@TotalCount", SqlDbType.Int, 4) { Value = Entity.TotalCount });
            sqlParameterList.Add(new SqlParameter("@SendCount", SqlDbType.Int, 4) { Value = Entity.SendCount });
            sqlParameterList.Add(new SqlParameter("@ValidityEnd", SqlDbType.DateTime, 8) { Value = Entity.ValidityEnd });
            sqlParameterList.Add(new SqlParameter("@PreOrder19DianId", SqlDbType.BigInt, 8) { Value = Entity.PreOrder19DianId });
            sqlParameterList.Add(new SqlParameter("@ShareType", SqlDbType.Int, 4) { Value = Entity.ShareType });
            sqlParameterList.Add(new SqlParameter("@CreatedBy", SqlDbType.Int, 4) { Value = SqlHelper.GetDbNullValue( Entity.CreatedBy) });
            sqlParameterList.Add(new SqlParameter("@LastUpdateTime", SqlDbType.DateTime, 8) { Value = DateTime.Now });
            sqlParameterList.Add(new SqlParameter("@CreateTime", SqlDbType.DateTime, 8) { Value = SqlHelper.GetDbNullValue(Entity.CreateTime) });
            sqlParameterList.Add(new SqlParameter("@LastUpdatedBy", SqlDbType.Int, 4) { Value = SqlHelper.GetDbNullValue(Entity.LastUpdatedBy) });
            var count = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteEntity(CouponSendDetail Entity)
        {
            string sql = @"DELETE FROM [CouponSendDetail]
                                 WHERE [CouponSendDetailID] =@CouponSendDetailID";
            var count =
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@CouponSendDetailID", SqlDbType.BigInt, 8) { Value = Entity.CouponSendDetailID });
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public long GetCountByQuery(CouponSendDetailQueryObject queryObject = null)
        {
            string sql = @"SELECT COUNT(0)
                            FROM  [CouponSendDetail] 
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

        public List<CouponSendDetail> GetListByQuery(int pageSize, int pageIndex, CouponSendDetailQueryObject queryObject = null, CouponSendDetailOrderColumn orderColumn = CouponSendDetailOrderColumn.CouponSendDetailId, SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[CouponSendDetailID]
                                ,[TotalCount]
                                ,[SendCount]
                                ,[ValidityEnd]
                                ,[PreOrder19DianId]
                                  ,[ShareType]
                                  ,[CreatedBy]
                                  ,[LastUpdatedBy]
                                  ,[LastUpdateTime]
                                  ,[CreateTime]
                            FROM  [CouponSendDetail] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder, pageSize, pageIndex);
            sql = whereSqlBuilder.Insert(0, sql).ToString();
            sql = string.Format(" SELECT TOP (@PageSize) * FROM ({0}) T WHERE T.ROWNUM > @PageIndex ", sql);
            List<CouponSendDetail> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<CouponSendDetail>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<CouponSendDetail>());
                }
            }
            return list;
        }

        public List<CouponSendDetail> GetListByQuery(CouponSendDetailQueryObject queryObject = null, CouponSendDetailOrderColumn orderColumn = CouponSendDetailOrderColumn.CouponSendDetailId, SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[CouponSendDetailID]
                                ,[TotalCount]
                                ,[SendCount]
                                ,[ValidityEnd]
                                ,[PreOrder19DianId]
                                  ,[ShareType]
                                  ,[CreatedBy]
                                  ,[LastUpdatedBy]
                                  ,[LastUpdateTime]
                                  ,[CreateTime]
                            FROM  [CouponSendDetail] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder);
            sql = whereSqlBuilder.Insert(0, sql).ToString();
            List<CouponSendDetail> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<CouponSendDetail>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<CouponSendDetail>());
                }
            }
            return list;
        }
        private static void GetWhereSqlBuilderAndSqlParameterList(CouponSendDetailQueryObject queryObject, ref SqlParameter[] sqlParameters,
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
            if (queryObject.CouponSendDetailId.HasValue)
            {
                whereSqlBuilder.Append(" AND CouponSendDetailID = @CouponSendDetailID ");
                sqlParameterList.Add(new SqlParameter("@CouponSendDetailID ", SqlDbType.BigInt, 8) { Value = queryObject.CouponSendDetailId });
            }
            if (queryObject.TotalCount.HasValue)
            {
                whereSqlBuilder.Append(" AND TotalCount = @TotalCount ");
                sqlParameterList.Add(new SqlParameter("@TotalCount ", SqlDbType.Int, 4) { Value = queryObject.TotalCount });
            }
            if (queryObject.TotalCountFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND TotalCount >= @TotalCountFrom ");
                sqlParameterList.Add(new SqlParameter("@TotalCountFrom", SqlDbType.Int, 4) { Value = queryObject.TotalCountFrom });
            }
            if (queryObject.TotalCountTo.HasValue)
            {
                whereSqlBuilder.Append(" AND TotalCount <= @TotalCountTo ");
                sqlParameterList.Add(new SqlParameter("@TotalCountTo", SqlDbType.Int, 4) { Value = queryObject.TotalCountTo });
            }
            if (queryObject.SendCount.HasValue)
            {
                whereSqlBuilder.Append(" AND SendCount = @SendCount ");
                sqlParameterList.Add(new SqlParameter("@SendCount ", SqlDbType.Int, 4) { Value = queryObject.SendCount });
            }
            if (queryObject.SendCountFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND SendCount >= @SendCountFrom ");
                sqlParameterList.Add(new SqlParameter("@SendCountFrom", SqlDbType.Int, 4) { Value = queryObject.SendCountFrom });
            }
            if (queryObject.SendCountTo.HasValue)
            {
                whereSqlBuilder.Append(" AND SendCount <= @SendCountTo ");
                sqlParameterList.Add(new SqlParameter("@SendCountTo", SqlDbType.Int, 4) { Value = queryObject.SendCountTo });
            }
            if (queryObject.ValidityEnd.HasValue)
            {
                whereSqlBuilder.Append(" AND ValidityEnd = @ValidityEnd ");
                sqlParameterList.Add(new SqlParameter("@ValidityEnd ", SqlDbType.DateTime, 8) { Value = queryObject.ValidityEnd });
            }
            if (queryObject.ValidityEndFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND ValidityEnd >= @ValidityEndFrom ");
                sqlParameterList.Add(new SqlParameter("@ValidityEndFrom", SqlDbType.DateTime, 8) { Value = queryObject.ValidityEndFrom });
            }
            if (queryObject.ValidityEndTo.HasValue)
            {
                whereSqlBuilder.Append(" AND ValidityEnd <= @ValidityEndTo ");
                sqlParameterList.Add(new SqlParameter("@ValidityEndTo", SqlDbType.DateTime, 8) { Value = queryObject.ValidityEndTo });
            }
            if (queryObject.PreOrder19DianId.HasValue)
            {
                whereSqlBuilder.Append(" AND PreOrder19DianId = @PreOrder19DianId ");
                sqlParameterList.Add(new SqlParameter("@PreOrder19DianId ", SqlDbType.BigInt, 8) { Value = queryObject.PreOrder19DianId });
            }
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@PageSize", pageSize));
                sqlParameterList.Add(new SqlParameter("@PageIndex", pageSize * (pageIndex - 1)));
            }
            sqlParameters = sqlParameterList.ToArray();
        }

        private static StringBuilder GetOrderSql(CouponSendDetailOrderColumn orderColumn, SortOrder order)
        {
            StringBuilder orderSqlBuilder = new StringBuilder(" ORDER BY ");
            switch (orderColumn)
            {
                case CouponSendDetailOrderColumn.CouponSendDetailId:
                    orderSqlBuilder.Append("CouponSendDetailID");
                    break;
                case CouponSendDetailOrderColumn.TotalCount:
                    orderSqlBuilder.Append("TotalCount");
                    break;
                case CouponSendDetailOrderColumn.SendCount:
                    orderSqlBuilder.Append("SendCount");
                    break;
                case CouponSendDetailOrderColumn.ValidityEnd:
                    orderSqlBuilder.Append("ValidityEnd");
                    break;
                case CouponSendDetailOrderColumn.PreOrder19DianId:
                    orderSqlBuilder.Append("PreOrder19DianId");
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
