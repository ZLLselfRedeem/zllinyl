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
    public partial class QrCodeManager
    {
        public bool IsExists(int Id)
        {
            string sql = "SELECT COUNT(0) FROM QrCode WHERE Id = @Id";
            var retutnValue =
                SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@Id",SqlDbType.Int,4) { Value = Id });
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
        
        public QrCode GetEntityById(int Id)
        {
            string sql  =  @"SELECT [Id]
                                ,[Name]
                                ,[Type]
                                ,[State]
                                ,[Remark]
                                ,[CreatedBy]
                                ,[CreateTime]
                                ,[LastUpdatedBy]
                                ,[LastUpdateTime]
                                ,[LinkKey]
                                ,[CityId]
                            FROM  [QrCode] 
                            WHERE [Id] = @Id ";
             using(var reader = 
                    SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@Id", SqlDbType.Int, 4){ Value = Id }))
            {
                if(reader.Read())
                {
                  var entity = reader.GetEntity<QrCode>();
                  return entity;
                }
            }
            return null;
        }
        
        public bool Add(IQrCode Entity)
        {
            string sql = @"INSERT INTO QrCode (
                                    [Name],
                                    [Type],
                                    [State],
                                    [Remark],
                                    [CreatedBy],
                                    [CreateTime],
                                    [LastUpdatedBy],
                                    [LastUpdateTime],
                                    [LinkKey],
                                    [CityId]
                                ) VALUES (
                                    @Name,
                                    @Type,
                                    @State,
                                    @Remark,
                                    @CreatedBy,
                                    @CreateTime,
                                    @LastUpdatedBy,
                                    @LastUpdateTime,
                                    @LinkKey,
                                    @CityId
                                    );                                    
                                    SELECT @@IDENTITY";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = Entity.Name });
            sqlParameterList.Add(new SqlParameter("@Type", SqlDbType.Int, 4) { Value = Entity.Type });
            sqlParameterList.Add(new SqlParameter("@State", SqlDbType.Int, 4) { Value = Entity.State });
            sqlParameterList.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 200) { Value = Entity.Remark });
            sqlParameterList.Add(new SqlParameter("@CreatedBy", SqlDbType.BigInt, 8) { Value = Entity.CreatedBy });
            sqlParameterList.Add(new SqlParameter("@CreateTime", SqlDbType.DateTime, 8) { Value = Entity.CreateTime });
            sqlParameterList.Add(new SqlParameter("@LastUpdatedBy", SqlDbType.BigInt, 8) { Value = Entity.LastUpdatedBy });
            sqlParameterList.Add(new SqlParameter("@LastUpdateTime", SqlDbType.DateTime, 8) { Value = Entity.LastUpdateTime });
            sqlParameterList.Add(new SqlParameter("@LinkKey", SqlDbType.BigInt, 8) { Value = Entity.LinkKey });
            sqlParameterList.Add(new SqlParameter("@CityId", SqlDbType.Int, 4) { Value = Entity.CityId });
            var returnObject =
                SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            int id;
            if(int.TryParse(returnObject.ToString(),out id))
            {
                Entity.Id = id;
                return true;
            }
            return false;
        }
        public bool Update(IQrCode Entity)
        {
            string sql =@"UPDATE [QrCode] SET
                                 [Name] = @Name
                                ,[Type] = @Type
                                ,[State] = @State
                                ,[Remark] = @Remark
                                ,[CreatedBy] = @CreatedBy
                                ,[CreateTime] = @CreateTime
                                ,[LastUpdatedBy] = @LastUpdatedBy
                                ,[LastUpdateTime] = @LastUpdateTime
                                ,[LinkKey] = @LinkKey
                                ,[CityId] = @CityId
                           WHERE [Id] =@Id";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@Id", SqlDbType.Int, 4) { Value = Entity.Id });
            sqlParameterList.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = Entity.Name });
            sqlParameterList.Add(new SqlParameter("@Type", SqlDbType.Int, 4) { Value = Entity.Type });
            sqlParameterList.Add(new SqlParameter("@State", SqlDbType.Int, 4) { Value = Entity.State });
            sqlParameterList.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 200) { Value = Entity.Remark });
            sqlParameterList.Add(new SqlParameter("@CreatedBy", SqlDbType.BigInt, 8) { Value = Entity.CreatedBy });
            sqlParameterList.Add(new SqlParameter("@CreateTime", SqlDbType.DateTime, 8) { Value = Entity.CreateTime });
            sqlParameterList.Add(new SqlParameter("@LastUpdatedBy", SqlDbType.BigInt, 8) { Value = Entity.LastUpdatedBy });
            sqlParameterList.Add(new SqlParameter("@LastUpdateTime", SqlDbType.DateTime, 8) { Value = Entity.LastUpdateTime });
            sqlParameterList.Add(new SqlParameter("@LinkKey", SqlDbType.BigInt, 8) { Value = Entity.LinkKey });
            sqlParameterList.Add(new SqlParameter("@CityId", SqlDbType.Int, 4) { Value = Entity.CityId });
            var count = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        
        public bool DeleteEntity(IQrCode Entity)
        {
            string sql = @"DELETE FROM [QrCode]
                                 WHERE [Id] =@Id";
            var count = 
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@Id", SqlDbType.Int, 4){ Value = Entity.Id });
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        
        public long GetCountByQuery(QrCodeQueryObject queryObject = null)
        {
            string sql = @"SELECT COUNT(0)
                            FROM  [QrCode] 
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

        public List<IQrCode> GetListByQuery(int pageSize,int pageIndex,QrCodeQueryObject queryObject = null,QrCodeOrderColumn orderColumn = QrCodeOrderColumn.Id,SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql  =  @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[Id]
                                ,[Name]
                                ,[Type]
                                ,[State]
                                ,[Remark]
                                ,[CreatedBy]
                                ,[CreateTime]
                                ,[LastUpdatedBy]
                                ,[LastUpdateTime]
                                ,[LinkKey]
                                ,[CityId]
                            FROM  [QrCode] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject,ref sqlParameters, whereSqlBuilder,pageSize,pageIndex);
            sql = whereSqlBuilder.Insert(0, sql).ToString(); 
            sql =  string.Format( " SELECT TOP (@PageSize) * FROM ({0}) T WHERE T.ROWNUM > @PageIndex ",sql);
            List<IQrCode> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<IQrCode>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<QrCode>());
                }
            }
            return list;
        }
        
        public List<IQrCode> GetListByQuery(QrCodeQueryObject queryObject = null,QrCodeOrderColumn orderColumn = QrCodeOrderColumn.Id,SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql  =  @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[Id]
                                ,[Name]
                                ,[Type]
                                ,[State]
                                ,[Remark]
                                ,[CreatedBy]
                                ,[CreateTime]
                                ,[LastUpdatedBy]
                                ,[LastUpdateTime]
                                ,[LinkKey]
                                ,[CityId]
                            FROM  [QrCode] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject,ref sqlParameters, whereSqlBuilder);
            sql = whereSqlBuilder.Insert(0, sql).ToString(); 
            List<IQrCode> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<IQrCode>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<QrCode>());
                }
            }
            return list;
        }
        private static void GetWhereSqlBuilderAndSqlParameterList(QrCodeQueryObject queryObject, ref SqlParameter[] sqlParameters,
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
                sqlParameterList.Add(new SqlParameter("@Id ", SqlDbType.Int, 4) { Value = queryObject.Id });
            }
            if (!string.IsNullOrEmpty(queryObject.Name))
            {
                whereSqlBuilder.Append(" AND Name = @Name ");
                sqlParameterList.Add(new SqlParameter("@Name", SqlDbType.NVarChar, 50) { Value = queryObject.Name });
            }
            if (!string.IsNullOrEmpty(queryObject.NameFuzzy))
            {
                whereSqlBuilder.Append(" AND Name LIKE @NameFuzzy ");
                sqlParameterList.Add(new SqlParameter("@NameFuzzy", SqlDbType.NVarChar, 50) { Value = string.Format("%{0}%", queryObject.NameFuzzy) });
            }
            if (queryObject.Type.HasValue)
            {
                whereSqlBuilder.Append(" AND Type = @Type ");
                sqlParameterList.Add(new SqlParameter("@Type ", SqlDbType.Int, 4) { Value = queryObject.Type });
            }
            if (queryObject.TypeFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND Type >= @TypeFrom ");
                sqlParameterList.Add(new SqlParameter("@TypeFrom", SqlDbType.Int, 4) { Value = queryObject.TypeFrom });
            }
            if (queryObject.TypeTo.HasValue)
            {
                whereSqlBuilder.Append(" AND Type <= @TypeTo ");
                sqlParameterList.Add(new SqlParameter("@TypeTo", SqlDbType.Int, 4) { Value = queryObject.TypeTo });
            }
            if (queryObject.State.HasValue)
            {
                whereSqlBuilder.Append(" AND State = @State ");
                sqlParameterList.Add(new SqlParameter("@State ", SqlDbType.Int, 4) { Value = queryObject.State });
            }
            if (queryObject.StateFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND State >= @StateFrom ");
                sqlParameterList.Add(new SqlParameter("@StateFrom", SqlDbType.Int, 4) { Value = queryObject.StateFrom });
            }
            if (queryObject.StateTo.HasValue)
            {
                whereSqlBuilder.Append(" AND State <= @StateTo ");
                sqlParameterList.Add(new SqlParameter("@StateTo", SqlDbType.Int, 4) { Value = queryObject.StateTo });
            }
            if (!string.IsNullOrEmpty(queryObject.Remark))
            {
                whereSqlBuilder.Append(" AND Remark = @Remark ");
                sqlParameterList.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 200) { Value = queryObject.Remark });
            }
            if (!string.IsNullOrEmpty(queryObject.RemarkFuzzy))
            {
                whereSqlBuilder.Append(" AND Remark LIKE @RemarkFuzzy ");
                sqlParameterList.Add(new SqlParameter("@RemarkFuzzy", SqlDbType.NVarChar, 200) { Value = string.Format("%{0}%", queryObject.RemarkFuzzy) });
            }
            if (queryObject.CreatedBy.HasValue)
            {
                whereSqlBuilder.Append(" AND CreatedBy = @CreatedBy ");
                sqlParameterList.Add(new SqlParameter("@CreatedBy ", SqlDbType.BigInt, 8) { Value = queryObject.CreatedBy });
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
            if (queryObject.LastUpdatedBy.HasValue)
            {
                whereSqlBuilder.Append(" AND LastUpdatedBy = @LastUpdatedBy ");
                sqlParameterList.Add(new SqlParameter("@LastUpdatedBy ", SqlDbType.BigInt, 8) { Value = queryObject.LastUpdatedBy });
            }
            if (queryObject.LastUpdateTime.HasValue)
            {
                whereSqlBuilder.Append(" AND LastUpdateTime = @LastUpdateTime ");
                sqlParameterList.Add(new SqlParameter("@LastUpdateTime ", SqlDbType.DateTime, 8) { Value = queryObject.LastUpdateTime });
            }
            if (queryObject.LastUpdateTimeFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND LastUpdateTime >= @LastUpdateTimeFrom ");
                sqlParameterList.Add(new SqlParameter("@LastUpdateTimeFrom", SqlDbType.DateTime, 8) { Value = queryObject.LastUpdateTimeFrom });
            }
            if (queryObject.LastUpdateTimeTo.HasValue)
            {
                whereSqlBuilder.Append(" AND LastUpdateTime <= @LastUpdateTimeTo ");
                sqlParameterList.Add(new SqlParameter("@LastUpdateTimeTo", SqlDbType.DateTime, 8) { Value = queryObject.LastUpdateTimeTo });
            }
            if (queryObject.LinkKey.HasValue)
            {
                whereSqlBuilder.Append(" AND LinkKey = @LinkKey ");
                sqlParameterList.Add(new SqlParameter("@LinkKey ", SqlDbType.BigInt, 8) { Value = queryObject.LinkKey });
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
        
        private static StringBuilder GetOrderSql(QrCodeOrderColumn orderColumn, SortOrder order)
        {
            StringBuilder orderSqlBuilder = new StringBuilder(" ORDER BY ");
            switch (orderColumn)
            {
                case QrCodeOrderColumn.Id:
                    orderSqlBuilder.Append("Id");
                    break;
                case QrCodeOrderColumn.Name:
                    orderSqlBuilder.Append("Name");
                    break;
                case QrCodeOrderColumn.Type:
                    orderSqlBuilder.Append("Type");
                    break;
                case QrCodeOrderColumn.State:
                    orderSqlBuilder.Append("State");
                    break;
                case QrCodeOrderColumn.Remark:
                    orderSqlBuilder.Append("Remark");
                    break;
                case QrCodeOrderColumn.CreatedBy:
                    orderSqlBuilder.Append("CreatedBy");
                    break;
                case QrCodeOrderColumn.CreateTime:
                    orderSqlBuilder.Append("CreateTime");
                    break;
                case QrCodeOrderColumn.LastUpdatedBy:
                    orderSqlBuilder.Append("LastUpdatedBy");
                    break;
                case QrCodeOrderColumn.LastUpdateTime:
                    orderSqlBuilder.Append("LastUpdateTime");
                    break;
                case QrCodeOrderColumn.LinkKey:
                    orderSqlBuilder.Append("LinkKey");
                    break;
                case QrCodeOrderColumn.CityId:
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