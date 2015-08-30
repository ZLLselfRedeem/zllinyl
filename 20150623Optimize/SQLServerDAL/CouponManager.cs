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
    public partial class CouponManager
    {
        public bool IsExists(int CouponId)
        {
            string sql = "SELECT COUNT(0) FROM Coupon WHERE CouponId = @CouponId";
            var retutnValue =
                SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@CouponId", SqlDbType.Int, 4) { Value = CouponId });
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

        public Coupon GetEntityById(int CouponId)
        {
            string sql = @"SELECT [CouponId]
                                ,[CouponName]
                                ,[ValidityPeriod]
                                ,[StartDate]
                                ,[EndDate]
                                ,[SheetNumber]
                                ,[SendCount]
                                ,[ShopId]
                                ,[CityId]
                                ,[RequirementMoney]
                                ,[SortOrder]
                                ,[DeductibleAmount]
                                ,[State]
                                ,[CreatedBy]
                                ,[CreateTime]
                                ,[LastUpdatedBy]
                                ,[LastUpdatedTime]
                                ,[Remark]
                                ,[RefuseReason]
                                ,[AuditEmployee]
                                ,[AuditTime]
                                ,[ShopName]
                                ,[ShopAddress]
                                ,[IsGot]
                                ,[Longitude]
                                ,[Latitude]
                                ,[DeductibleProportion]
                                ,[Image]
                                ,[CouponType]
                                ,[IsDisplay]
                                ,[SubsidyAmount]
                                ,[MaxAmount]
                            FROM  [Coupon] 
                            WHERE [CouponId] = @CouponId ";
            using (var reader =
                   SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@CouponId", SqlDbType.Int, 4) { Value = CouponId }))
            {
                if (reader.Read())
                {
                    var entity = reader.GetEntity<Coupon>();
                    return entity;
                }
            }
            return null;
        }

        public Coupon GetEntityByIdQuery(int CouponId)
        {
            string sql = @"SELECT [CouponId]
                                ,[CouponName]
                                ,[ValidityPeriod]
                                ,[StartDate]
                                ,[EndDate]
                                ,[SheetNumber]
                                ,[SendCount]
                                ,[ShopId]
                                ,[CityId]
                                ,[RequirementMoney]
                                ,[SortOrder]
                                ,[DeductibleAmount]
                                ,[State]
                                ,[CreatedBy]
                                ,[CreateTime]
                                ,[LastUpdatedBy]
                                ,[LastUpdatedTime]
                                ,[Remark]
                                ,[RefuseReason]
                                ,[AuditEmployee]
                                ,[AuditTime]
                                ,[ShopName]
                                ,[ShopAddress]
                                ,[IsGot]
                                ,[Longitude]
                                ,[Latitude]
                                ,[DeductibleProportion]
                                ,[Image]
                                ,[CouponType]
                                ,[IsDisplay]
                                ,[SubsidyAmount]
                                ,[MaxAmount]
                            FROM  [Coupon] 
                            WHERE State=1
                                   AND SendCount<SheetNumber
                                   AND EndDate>GETDATE()
                                   AND [CouponId] = @CouponId ";
            using (var reader =
                   SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@CouponId", SqlDbType.Int, 4) { Value = CouponId }))
            {
                if (reader.Read())
                {
                    var entity = reader.GetEntity<Coupon>();
                    return entity;
                }
            }
            return null;
        }

        public bool Add(ICoupon Entity)
        {
            string sql = @"INSERT INTO Coupon ( 
                                    [CouponName],
                                    [ValidityPeriod],
                                    [StartDate],
                                    [EndDate],
                                    [SheetNumber],
                                    [SendCount],
                                    [ShopId],
                                    [CityId],
                                    [RequirementMoney],
                                    [SortOrder],
                                    [DeductibleAmount],
                                    [State],
                                    [CreatedBy],
                                    [CreateTime],
                                    [LastUpdatedBy],
                                    [LastUpdatedTime],
                                    [Remark],
                                    [RefuseReason],
                                    [AuditEmployee],
                                    [AuditTime],
                                    [ShopName],
                                    [ShopAddress],
                                    [IsGot],
                                    [Longitude],
                                    [Latitude], 
                                    [Image],
                                    [CouponType],
                                    [IsDisplay],
                                    [SubsidyAmount],
                                    [MaxAmount]
                                ) VALUES ( 
                                    @CouponName,
                                    @ValidityPeriod,
                                    @StartDate,
                                    @EndDate,
                                    @SheetNumber,
                                    @SendCount,
                                    @ShopId,
                                    @CityId,
                                    @RequirementMoney,
                                    @SortOrder,
                                    @DeductibleAmount,
                                    @State,
                                    @CreatedBy,
                                    @CreateTime,
                                    @LastUpdatedBy,
                                    @LastUpdatedTime,
                                    @Remark,
                                    @RefuseReason,
                                    @AuditEmployee,
                                    @AuditTime,
                                    @ShopName,
                                    @ShopAddress,
                                    @IsGot,
                                    @Longitude,
                                    @Latitude, 
                                    @Image,
                                    @CouponType,
                                    @IsDisplay,
                                    @SubsidyAmount,
                                    @MaxAmount
                                    );                                    
                                    SELECT @@IDENTITY";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>(); 
            sqlParameterList.Add(new SqlParameter("@CouponName", SqlDbType.NVarChar, 100) { Value = Entity.CouponName });
            sqlParameterList.Add(new SqlParameter("@ValidityPeriod", SqlDbType.Int, 4) { Value = Entity.ValidityPeriod });
            sqlParameterList.Add(new SqlParameter("@StartDate", SqlDbType.DateTime, 8) { Value = Entity.StartDate });
            sqlParameterList.Add(new SqlParameter("@EndDate", SqlDbType.DateTime, 8) { Value = Entity.EndDate });
            sqlParameterList.Add(new SqlParameter("@SheetNumber", SqlDbType.Int, 4) { Value = Entity.SheetNumber });
            sqlParameterList.Add(new SqlParameter("@SendCount", SqlDbType.Int, 4) { Value = Entity.SendCount });
            sqlParameterList.Add(new SqlParameter("@ShopId", SqlDbType.Int, 4) { Value = SqlHelper.GetDbNullValue( Entity.ShopId )});
            sqlParameterList.Add(new SqlParameter("@CityId", SqlDbType.Int, 4) { Value = Entity.CityId});
            sqlParameterList.Add(new SqlParameter("@RequirementMoney", SqlDbType.Float, 8) { Value = Entity.RequirementMoney });
            sqlParameterList.Add(new SqlParameter("@SortOrder", SqlDbType.Int, 4) { Value = Entity.SortOrder });
            sqlParameterList.Add(new SqlParameter("@DeductibleAmount", SqlDbType.Float, 8) { Value = Entity.DeductibleAmount });
            sqlParameterList.Add(new SqlParameter("@State", SqlDbType.Int, 4) { Value = Entity.State });
            sqlParameterList.Add(new SqlParameter("@CreatedBy", SqlDbType.Int, 4) { Value = Entity.CreatedBy });
            sqlParameterList.Add(new SqlParameter("@CreateTime", SqlDbType.DateTime, 8) { Value = Entity.CreateTime });
            sqlParameterList.Add(new SqlParameter("@LastUpdatedBy", SqlDbType.Int, 4) { Value = Entity.LastUpdatedBy });
            sqlParameterList.Add(new SqlParameter("@LastUpdatedTime", SqlDbType.DateTime, 8) { Value = Entity.LastUpdatedTime });
            sqlParameterList.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 200) { Value = Entity.Remark });
            sqlParameterList.Add(new SqlParameter("@RefuseReason", SqlDbType.NVarChar, 200) { Value = SqlHelper.GetDbNullValue( Entity.RefuseReason) });
            sqlParameterList.Add(new SqlParameter("@AuditEmployee", SqlDbType.Int, 4) { Value = SqlHelper.GetDbNullValue( Entity.AuditEmployee) });
            sqlParameterList.Add(new SqlParameter("@AuditTime", SqlDbType.DateTime, 8) { Value = SqlHelper.GetDbNullValue( Entity.AuditTime) });
            sqlParameterList.Add(new SqlParameter("@ShopName", SqlDbType.NVarChar, 400) { Value = Entity.ShopName });
            sqlParameterList.Add(new SqlParameter("@ShopAddress", SqlDbType.NVarChar, 400) { Value = Entity.ShopAddress });
            sqlParameterList.Add(new SqlParameter("@IsGot", SqlDbType.Bit, 1) { Value = Entity.IsGot });
            sqlParameterList.Add(new SqlParameter("@Longitude", SqlDbType.Float, 8) { Value = Entity.Longitude });
            sqlParameterList.Add(new SqlParameter("@Latitude", SqlDbType.Float, 8) { Value = Entity.Latitude });
            //sqlParameterList.Add(new SqlParameter("@DeductibleProportion", SqlDbType.Float, 8) { Value = Entity.DeductibleProportion });
            sqlParameterList.Add(new SqlParameter("@Image", SqlDbType.NVarChar, 400) { Value = Entity.Image });
            sqlParameterList.Add(new SqlParameter("@CouponType", SqlDbType.Int, 4) { Value = Entity.CouponType });
            sqlParameterList.Add(new SqlParameter("@IsDisplay", SqlDbType.Bit, 1) { Value = Entity.IsDisplay });
            sqlParameterList.Add(new SqlParameter("@SubsidyAmount", SqlDbType.Float, 8) { Value = Entity.SubsidyAmount });
            sqlParameterList.Add(new SqlParameter("@MaxAmount", SqlDbType.Float, 8) { Value = Entity.MaxAmount });
            sqlParameterList.Add(new SqlParameter("@SubsidyType", SqlDbType.SmallInt, 8) { Value = Entity.SubsidyType });
            //sqlParameterList.Add(new SqlParameter("@BeginTime", SqlDbType.Time) { Value = Entity.BeginTime });
            //sqlParameterList.Add(new SqlParameter("@EndTime", SqlDbType.Time) { Value = Entity.EndTime });
            //sqlParameterList.Add(new SqlParameter("@IsGeneralHolidays", SqlDbType.Bit, 1) { Value = Entity.IsGeneralHolidays });
            var returnObject =
                SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            int id;
            if (int.TryParse(returnObject.ToString(), out id))
            {
                Entity.CouponId = id;
                return true;
            }
            return false;
        }
        public bool Update(ICoupon Entity)
        {
            string sql = @"UPDATE [Coupon] SET
                                 [CouponName] = @CouponName
                                ,[ValidityPeriod] = @ValidityPeriod
                                ,[StartDate] = @StartDate
                                ,[EndDate] = @EndDate
                                ,[SheetNumber] = @SheetNumber
                                ,[SendCount] = @SendCount
                                ,[ShopId] = @ShopId
                                ,[CityId] = @CityId
                                ,[RequirementMoney] = @RequirementMoney
                                ,[SortOrder] = @SortOrder
                                ,[DeductibleAmount] = @DeductibleAmount
                                ,[State] = @State
                                ,[CreatedBy] = @CreatedBy
                                ,[CreateTime] = @CreateTime
                                ,[LastUpdatedBy] = @LastUpdatedBy
                                ,[LastUpdatedTime] = @LastUpdatedTime
                                ,[Remark] = @Remark
                                ,[RefuseReason] = @RefuseReason
                                ,[AuditEmployee] = @AuditEmployee
                                ,[AuditTime] = @AuditTime
                                ,[ShopName] = @ShopName
                                ,[ShopAddress] = @ShopAddress
                                ,[IsGot] = @IsGot
                                ,[Longitude] = @Longitude
                                ,[Latitude] = @Latitude
                                ,[Image] = @Image
                                ,[CouponType] = @CouponType
                                ,[IsDisplay] = @IsDisplay
                                ,[SubsidyAmount] = @SubsidyAmount
                                ,[SubsidyType] = @SubsidyType
                           WHERE [CouponId] =@CouponId";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@CouponId", SqlDbType.Int, 4) { Value = Entity.CouponId });
            sqlParameterList.Add(new SqlParameter("@CouponName", SqlDbType.NVarChar, 100) { Value = Entity.CouponName });
            sqlParameterList.Add(new SqlParameter("@ValidityPeriod", SqlDbType.Int, 4) { Value = Entity.ValidityPeriod });
            sqlParameterList.Add(new SqlParameter("@StartDate", SqlDbType.DateTime, 8) { Value = Entity.StartDate });
            sqlParameterList.Add(new SqlParameter("@EndDate", SqlDbType.DateTime, 8) { Value = Entity.EndDate });
            sqlParameterList.Add(new SqlParameter("@SheetNumber", SqlDbType.Int, 4) { Value = Entity.SheetNumber });
            sqlParameterList.Add(new SqlParameter("@SendCount", SqlDbType.Int, 4) { Value = Entity.SendCount });
            sqlParameterList.Add(new SqlParameter("@ShopId", SqlDbType.Int, 4) { Value = SqlHelper.GetDbNullValue( Entity.ShopId) });
            sqlParameterList.Add(new SqlParameter("@CityId", SqlDbType.Int, 4) { Value = Entity.CityId });
            sqlParameterList.Add(new SqlParameter("@RequirementMoney", SqlDbType.Float, 8) { Value = Entity.RequirementMoney });
            sqlParameterList.Add(new SqlParameter("@SortOrder", SqlDbType.Int, 4) { Value = Entity.SortOrder });
            sqlParameterList.Add(new SqlParameter("@DeductibleAmount", SqlDbType.Float, 8) { Value = Entity.DeductibleAmount });
            sqlParameterList.Add(new SqlParameter("@State", SqlDbType.Int, 4) { Value = Entity.State });
            sqlParameterList.Add(new SqlParameter("@CreatedBy", SqlDbType.Int, 4) { Value = Entity.CreatedBy });
            sqlParameterList.Add(new SqlParameter("@CreateTime", SqlDbType.DateTime, 8) { Value = Entity.CreateTime });
            sqlParameterList.Add(new SqlParameter("@LastUpdatedBy", SqlDbType.Int, 4) { Value = Entity.LastUpdatedBy });
            sqlParameterList.Add(new SqlParameter("@LastUpdatedTime", SqlDbType.DateTime, 8) { Value = Entity.LastUpdatedTime });
            sqlParameterList.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 200) { Value = Entity.Remark });
            sqlParameterList.Add(new SqlParameter("@RefuseReason", SqlDbType.NVarChar, 200) { Value = SqlHelper.GetDbNullValue(Entity.RefuseReason) });
            sqlParameterList.Add(new SqlParameter("@AuditEmployee", SqlDbType.Int, 4) { Value = SqlHelper.GetDbNullValue(Entity.AuditEmployee) });
            sqlParameterList.Add(new SqlParameter("@AuditTime", SqlDbType.DateTime, 8) { Value = SqlHelper.GetDbNullValue(Entity.AuditTime) });
            sqlParameterList.Add(new SqlParameter("@ShopName", SqlDbType.NVarChar, 400) { Value = Entity.ShopName });
            sqlParameterList.Add(new SqlParameter("@ShopAddress", SqlDbType.NVarChar, 400) { Value = Entity.ShopAddress });
            sqlParameterList.Add(new SqlParameter("@IsGot", SqlDbType.Bit, 1) { Value = Entity.IsGot });
            sqlParameterList.Add(new SqlParameter("@Longitude", SqlDbType.Float, 8) { Value = Entity.Longitude });
            sqlParameterList.Add(new SqlParameter("@Latitude", SqlDbType.Float, 8) { Value = Entity.Latitude });
            //sqlParameterList.Add(new SqlParameter("@DeductibleProportion", SqlDbType.Float, 8) { Value = Entity.DeductibleProportion });
            sqlParameterList.Add(new SqlParameter("@Image", SqlDbType.NVarChar, 400) { Value = Entity.Image });
            sqlParameterList.Add(new SqlParameter("@CouponType", SqlDbType.Int, 4) { Value = Entity.CouponType });
            sqlParameterList.Add(new SqlParameter("@IsDisplay", SqlDbType.Bit, 1) { Value = Entity.IsDisplay });
            sqlParameterList.Add(new SqlParameter("@SubsidyAmount", SqlDbType.Float, 8) { Value = Entity.SubsidyAmount });
            sqlParameterList.Add(new SqlParameter("@SubsidyType", SqlDbType.SmallInt) { Value = Entity.SubsidyType });
            var count = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 修改抵扣券表中的isGot
        /// </summary>
        /// <param name="couponID"></param>
        /// <param name="isGot"></param>
        /// <returns></returns>
        public bool Update(int couponID,bool isGot)
        {
            string sql = @"UPDATE [Coupon] SET
                                [IsGot] = @IsGot
                           WHERE [CouponId] =@CouponId";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@CouponId", SqlDbType.Int, 4) { Value = couponID });
            sqlParameterList.Add(new SqlParameter("@IsGot", SqlDbType.Bit, 1) { Value = isGot });
            var count = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteEntity(ICoupon Entity)
        {
            string sql = @"DELETE FROM [Coupon]
                                 WHERE [CouponId] =@CouponId";
            var count =
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@CouponId", SqlDbType.Int, 4) { Value = Entity.CouponId });
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public long GetCountByQuery(CouponQueryObject queryObject = null)
        {
            string sql = @"SELECT COUNT(0)
                            FROM  [Coupon] 
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

        public List<ICoupon> GetListByQuery(int pageSize, int pageIndex, CouponQueryObject queryObject = null, CouponOrderColumn orderColumn = CouponOrderColumn.CouponId, SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[CouponId]
                                ,[CouponName]
                                ,[ValidityPeriod]
                                ,[StartDate]
                                ,[EndDate]
                                ,[SheetNumber]
                                ,[SendCount]
                                ,[ShopId]
                                ,[CityId]
                                ,[RequirementMoney]
                                ,[SortOrder]
                                ,[DeductibleAmount]
                                ,[State]
                                ,[CreatedBy]
                                ,[CreateTime]
                                ,[LastUpdatedBy]
                                ,[LastUpdatedTime]
                                ,[Remark]
                                ,[RefuseReason]
                                ,[AuditEmployee]
                                ,[AuditTime]
                                ,[ShopName]
                                ,[ShopAddress]
                                ,[IsGot]
                                ,[Longitude]
                                ,[Latitude]
                                ,[DeductibleProportion]
                                ,[Image]
                                ,[CouponType]
                                ,[IsDisplay]
                                ,[SubsidyAmount]
                            FROM  [Coupon] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder, pageSize, pageIndex);
            sql = whereSqlBuilder.Insert(0, sql).ToString();
            sql = string.Format(" SELECT TOP (@PageSize) * FROM ({0}) T WHERE T.ROWNUM > @PageIndex ", sql);
            List<ICoupon> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<ICoupon>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<Coupon>());
                }
            }
            return list;
        }

        public List<ICoupon> GetListByQuery(CouponQueryObject queryObject = null, CouponOrderColumn orderColumn = CouponOrderColumn.CouponId, SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[CouponId]
                                ,[CouponName]
                                ,[ValidityPeriod]
                                ,[StartDate]
                                ,[EndDate]
                                ,[SheetNumber]
                                ,[SendCount]
                                ,[ShopId]
                                ,[CityId]
                                ,[RequirementMoney]
                                ,[SortOrder]
                                ,[DeductibleAmount]
                                ,[State]
                                ,[CreatedBy]
                                ,[CreateTime]
                                ,[LastUpdatedBy]
                                ,[LastUpdatedTime]
                                ,[Remark]
                                ,[RefuseReason]
                                ,[AuditEmployee]
                                ,[AuditTime]
                                ,[ShopName]
                                ,[ShopAddress]
                                ,[IsGot]
                                ,[Longitude]
                                ,[Latitude]
                                ,[DeductibleProportion]
                                ,[Image]
                                ,[CouponType]
                                ,[IsDisplay]
                                ,[SubsidyAmount]
                                ,[MaxAmount]
                            FROM  [Coupon] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder);
            sql = whereSqlBuilder.Insert(0, sql).ToString();
            List<ICoupon> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<ICoupon>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<Coupon>());
                }
            }
            return list;
        }
        private static void GetWhereSqlBuilderAndSqlParameterList(CouponQueryObject queryObject, ref SqlParameter[] sqlParameters,
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
            if (queryObject.CouponId.HasValue)
            {
                whereSqlBuilder.Append(" AND CouponId = @CouponId ");
                sqlParameterList.Add(new SqlParameter("@CouponId ", SqlDbType.Int, 4) { Value = queryObject.CouponId });
            }
            if (!string.IsNullOrEmpty(queryObject.CouponName))
            {
                whereSqlBuilder.Append(" AND CouponName = @CouponName ");
                sqlParameterList.Add(new SqlParameter("@CouponName", SqlDbType.NVarChar, 100) { Value = queryObject.CouponName });
            }
            if (!string.IsNullOrEmpty(queryObject.CouponNameFuzzy))
            {
                whereSqlBuilder.Append(" AND CouponName LIKE @CouponNameFuzzy ");
                sqlParameterList.Add(new SqlParameter("@CouponNameFuzzy", SqlDbType.NVarChar, 100) { Value = string.Format("%{0}%", queryObject.CouponNameFuzzy) });
            }
            if (queryObject.ValidityPeriod.HasValue)
            {
                whereSqlBuilder.Append(" AND ValidityPeriod = @ValidityPeriod ");
                sqlParameterList.Add(new SqlParameter("@ValidityPeriod ", SqlDbType.Int, 4) { Value = queryObject.ValidityPeriod });
            }
            if (queryObject.ValidityPeriodFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND ValidityPeriod >= @ValidityPeriodFrom ");
                sqlParameterList.Add(new SqlParameter("@ValidityPeriodFrom", SqlDbType.Int, 4) { Value = queryObject.ValidityPeriodFrom });
            }
            if (queryObject.ValidityPeriodTo.HasValue)
            {
                whereSqlBuilder.Append(" AND ValidityPeriod <= @ValidityPeriodTo ");
                sqlParameterList.Add(new SqlParameter("@ValidityPeriodTo", SqlDbType.Int, 4) { Value = queryObject.ValidityPeriodTo });
            }
            if (queryObject.StartDate.HasValue)
            {
                whereSqlBuilder.Append(" AND StartDate = @StartDate ");
                sqlParameterList.Add(new SqlParameter("@StartDate ", SqlDbType.DateTime, 8) { Value = queryObject.StartDate });
            }
            if (queryObject.StartDateFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND StartDate >= @StartDateFrom ");
                sqlParameterList.Add(new SqlParameter("@StartDateFrom", SqlDbType.DateTime, 8) { Value = queryObject.StartDateFrom });
            }
            if (queryObject.StartDateTo.HasValue)
            {
                whereSqlBuilder.Append(" AND StartDate <= @StartDateTo ");
                sqlParameterList.Add(new SqlParameter("@StartDateTo", SqlDbType.DateTime, 8) { Value = queryObject.StartDateTo });
            }
            if (queryObject.EndDate.HasValue)
            {
                whereSqlBuilder.Append(" AND EndDate = @EndDate ");
                sqlParameterList.Add(new SqlParameter("@EndDate ", SqlDbType.DateTime, 8) { Value = queryObject.EndDate });
            }
            if (queryObject.EndDateFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND EndDate >= @EndDateFrom ");
                sqlParameterList.Add(new SqlParameter("@EndDateFrom", SqlDbType.DateTime, 8) { Value = queryObject.EndDateFrom });
            }
            if (queryObject.EndDateTo.HasValue)
            {
                whereSqlBuilder.Append(" AND EndDate <= @EndDateTo ");
                sqlParameterList.Add(new SqlParameter("@EndDateTo", SqlDbType.DateTime, 8) { Value = queryObject.EndDateTo });
            }
            if (queryObject.SheetNumber.HasValue)
            {
                whereSqlBuilder.Append(" AND SheetNumber = @SheetNumber ");
                sqlParameterList.Add(new SqlParameter("@SheetNumber ", SqlDbType.Int, 4) { Value = queryObject.SheetNumber });
            }
            if (queryObject.SheetNumberFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND SheetNumber >= @SheetNumberFrom ");
                sqlParameterList.Add(new SqlParameter("@SheetNumberFrom", SqlDbType.Int, 4) { Value = queryObject.SheetNumberFrom });
            }
            if (queryObject.SheetNumberTo.HasValue)
            {
                whereSqlBuilder.Append(" AND SheetNumber <= @SheetNumberTo ");
                sqlParameterList.Add(new SqlParameter("@SheetNumberTo", SqlDbType.Int, 4) { Value = queryObject.SheetNumberTo });
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
            if (queryObject.ShopId.HasValue)
            {
                whereSqlBuilder.Append(" AND ShopId = @ShopId ");
                sqlParameterList.Add(new SqlParameter("@ShopId ", SqlDbType.Int, 4) { Value = queryObject.ShopId });
            }
            if (queryObject.CityId.HasValue)
            {
                whereSqlBuilder.Append(" AND CityId = @CityId ");
                sqlParameterList.Add(new SqlParameter("@CityId ", SqlDbType.Int, 4) { Value = queryObject.CityId });
            }
            if (queryObject.RequirementMoney.HasValue)
            {
                whereSqlBuilder.Append(" AND RequirementMoney = @RequirementMoney ");
                sqlParameterList.Add(new SqlParameter("@RequirementMoney ", SqlDbType.Float, 8) { Value = queryObject.RequirementMoney });
            }
            if (queryObject.RequirementMoneyFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND RequirementMoney >= @RequirementMoneyFrom ");
                sqlParameterList.Add(new SqlParameter("@RequirementMoneyFrom", SqlDbType.Float, 8) { Value = queryObject.RequirementMoneyFrom });
            }
            if (queryObject.RequirementMoneyTo.HasValue)
            {
                whereSqlBuilder.Append(" AND RequirementMoney <= @RequirementMoneyTo ");
                sqlParameterList.Add(new SqlParameter("@RequirementMoneyTo", SqlDbType.Float, 8) { Value = queryObject.RequirementMoneyTo });
            }
            if (queryObject.SortOrder.HasValue)
            {
                whereSqlBuilder.Append(" AND SortOrder = @SortOrder ");
                sqlParameterList.Add(new SqlParameter("@SortOrder ", SqlDbType.Int, 4) { Value = queryObject.SortOrder });
            }
            if (queryObject.SortOrderFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND SortOrder >= @SortOrderFrom ");
                sqlParameterList.Add(new SqlParameter("@SortOrderFrom", SqlDbType.Int, 4) { Value = queryObject.SortOrderFrom });
            }
            if (queryObject.SortOrderTo.HasValue)
            {
                whereSqlBuilder.Append(" AND SortOrder <= @SortOrderTo ");
                sqlParameterList.Add(new SqlParameter("@SortOrderTo", SqlDbType.Int, 4) { Value = queryObject.SortOrderTo });
            }
            if (queryObject.DeductibleAmount.HasValue)
            {
                whereSqlBuilder.Append(" AND DeductibleAmount = @DeductibleAmount ");
                sqlParameterList.Add(new SqlParameter("@DeductibleAmount ", SqlDbType.Float, 8) { Value = queryObject.DeductibleAmount });
            }
            if (queryObject.DeductibleAmountFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND DeductibleAmount >= @DeductibleAmountFrom ");
                sqlParameterList.Add(new SqlParameter("@DeductibleAmountFrom", SqlDbType.Float, 8) { Value = queryObject.DeductibleAmountFrom });
            }
            if (queryObject.DeductibleAmountTo.HasValue)
            {
                whereSqlBuilder.Append(" AND DeductibleAmount <= @DeductibleAmountTo ");
                sqlParameterList.Add(new SqlParameter("@DeductibleAmountTo", SqlDbType.Float, 8) { Value = queryObject.DeductibleAmountTo });
            }
            if (queryObject.State.HasValue)
            {
                whereSqlBuilder.Append(" AND State = @State ");
                sqlParameterList.Add(new SqlParameter("@State ", SqlDbType.Int, 4) { Value = queryObject.State });
            }
            if (queryObject.CreatedBy.HasValue)
            {
                whereSqlBuilder.Append(" AND CreatedBy = @CreatedBy ");
                sqlParameterList.Add(new SqlParameter("@CreatedBy ", SqlDbType.Int, 4) { Value = queryObject.CreatedBy });
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
                sqlParameterList.Add(new SqlParameter("@LastUpdatedBy ", SqlDbType.Int, 4) { Value = queryObject.LastUpdatedBy });
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
                sqlParameterList.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 200) { Value = queryObject.Remark });
            }
            if (!string.IsNullOrEmpty(queryObject.RemarkFuzzy))
            {
                whereSqlBuilder.Append(" AND Remark LIKE @RemarkFuzzy ");
                sqlParameterList.Add(new SqlParameter("@RemarkFuzzy", SqlDbType.NVarChar, 200) { Value = string.Format("%{0}%", queryObject.RemarkFuzzy) });
            }
            if (!string.IsNullOrEmpty(queryObject.RefuseReason))
            {
                whereSqlBuilder.Append(" AND RefuseReason = @RefuseReason ");
                sqlParameterList.Add(new SqlParameter("@RefuseReason", SqlDbType.NVarChar, 200) { Value = queryObject.RefuseReason });
            }
            if (!string.IsNullOrEmpty(queryObject.RefuseReasonFuzzy))
            {
                whereSqlBuilder.Append(" AND RefuseReason LIKE @RefuseReasonFuzzy ");
                sqlParameterList.Add(new SqlParameter("@RefuseReasonFuzzy", SqlDbType.NVarChar, 200) { Value = string.Format("%{0}%", queryObject.RefuseReasonFuzzy) });
            }
            if (queryObject.AuditEmployee.HasValue)
            {
                whereSqlBuilder.Append(" AND AuditEmployee = @AuditEmployee ");
                sqlParameterList.Add(new SqlParameter("@AuditEmployee ", SqlDbType.Int, 4) { Value = queryObject.AuditEmployee });
            }
            if (queryObject.AuditTime.HasValue)
            {
                whereSqlBuilder.Append(" AND AuditTime = @AuditTime ");
                sqlParameterList.Add(new SqlParameter("@AuditTime ", SqlDbType.DateTime, 8) { Value = queryObject.AuditTime });
            }
            if (queryObject.AuditTimeFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND AuditTime >= @AuditTimeFrom ");
                sqlParameterList.Add(new SqlParameter("@AuditTimeFrom", SqlDbType.DateTime, 8) { Value = queryObject.AuditTimeFrom });
            }
            if (queryObject.AuditTimeTo.HasValue)
            {
                whereSqlBuilder.Append(" AND AuditTime <= @AuditTimeTo ");
                sqlParameterList.Add(new SqlParameter("@AuditTimeTo", SqlDbType.DateTime, 8) { Value = queryObject.AuditTimeTo });
            }
            if (!string.IsNullOrEmpty(queryObject.ShopName))
            {
                whereSqlBuilder.Append(" AND ShopName = @ShopName ");
                sqlParameterList.Add(new SqlParameter("@ShopName", SqlDbType.NVarChar, 400) { Value = queryObject.ShopName });
            }
            if (!string.IsNullOrEmpty(queryObject.ShopNameFuzzy))
            {
                whereSqlBuilder.Append(" AND ShopName LIKE @ShopNameFuzzy ");
                sqlParameterList.Add(new SqlParameter("@ShopNameFuzzy", SqlDbType.NVarChar, 400) { Value = string.Format("%{0}%", queryObject.ShopNameFuzzy) });
            }
            if (!string.IsNullOrEmpty(queryObject.ShopAddress))
            {
                whereSqlBuilder.Append(" AND ShopAddress = @ShopAddress ");
                sqlParameterList.Add(new SqlParameter("@ShopAddress", SqlDbType.NVarChar, 400) { Value = queryObject.ShopAddress });
            }
            if (!string.IsNullOrEmpty(queryObject.ShopAddressFuzzy))
            {
                whereSqlBuilder.Append(" AND ShopAddress LIKE @ShopAddressFuzzy ");
                sqlParameterList.Add(new SqlParameter("@ShopAddressFuzzy", SqlDbType.NVarChar, 400) { Value = string.Format("%{0}%", queryObject.ShopAddressFuzzy) });
            }
            if (queryObject.IsGot.HasValue)
            {
                whereSqlBuilder.Append(" AND IsGot = @IsGot ");
                sqlParameterList.Add(new SqlParameter("@IsGot ", SqlDbType.Bit, 1) { Value = queryObject.IsGot });
            }
            if (queryObject.Longitude.HasValue)
            {
                //whereSqlBuilder.Append(" AND Longitude = @Longitude ");
                sqlParameterList.Add(new SqlParameter("@Longitude ", SqlDbType.Float, 8) { Value = queryObject.Longitude });
            }
            if (queryObject.LongitudeFrom.HasValue)
            {
                //whereSqlBuilder.Append(" AND Longitude >= @LongitudeFrom ");
                sqlParameterList.Add(new SqlParameter("@LongitudeFrom", SqlDbType.Float, 8) { Value = queryObject.LongitudeFrom });
            }
            if (queryObject.LongitudeTo.HasValue)
            {
                //whereSqlBuilder.Append(" AND Longitude <= @LongitudeTo ");
                sqlParameterList.Add(new SqlParameter("@LongitudeTo", SqlDbType.Float, 8) { Value = queryObject.LongitudeTo });
            }
            if (queryObject.Latitude.HasValue)
            {
                //whereSqlBuilder.Append(" AND Latitude = @Latitude ");
                sqlParameterList.Add(new SqlParameter("@Latitude ", SqlDbType.Float, 8) { Value = queryObject.Latitude });
            }
            if (queryObject.LatitudeFrom.HasValue)
            {
                //whereSqlBuilder.Append(" AND Latitude >= @LatitudeFrom ");
                sqlParameterList.Add(new SqlParameter("@LatitudeFrom", SqlDbType.Float, 8) { Value = queryObject.LatitudeFrom });
            }
            if (queryObject.LatitudeTo.HasValue)
            {
               // whereSqlBuilder.Append(" AND Latitude <= @LatitudeTo ");
                sqlParameterList.Add(new SqlParameter("@LatitudeTo", SqlDbType.Float, 8) { Value = queryObject.LatitudeTo });
            }
            if (queryObject.DeductibleProportion.HasValue)
            {
                whereSqlBuilder.Append(" AND DeductibleProportion = @DeductibleProportion ");
                sqlParameterList.Add(new SqlParameter("@DeductibleProportion ", SqlDbType.Float, 8) { Value = queryObject.DeductibleProportion });
            }
            if (queryObject.DeductibleProportionFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND DeductibleProportion >= @DeductibleProportionFrom ");
                sqlParameterList.Add(new SqlParameter("@DeductibleProportionFrom", SqlDbType.Float, 8) { Value = queryObject.DeductibleProportionFrom });
            }
            if (queryObject.DeductibleProportionTo.HasValue)
            {
                whereSqlBuilder.Append(" AND DeductibleProportion <= @DeductibleProportionTo ");
                sqlParameterList.Add(new SqlParameter("@DeductibleProportionTo", SqlDbType.Float, 8) { Value = queryObject.DeductibleProportionTo });
            }
            if (!string.IsNullOrEmpty(queryObject.Image))
            {
                whereSqlBuilder.Append(" AND Image = @Image ");
                sqlParameterList.Add(new SqlParameter("@Image", SqlDbType.NChar, 400) { Value = queryObject.Image });
            }
            if (!string.IsNullOrEmpty(queryObject.ImageFuzzy))
            {
                whereSqlBuilder.Append(" AND Image LIKE @ImageFuzzy ");
                sqlParameterList.Add(new SqlParameter("@ImageFuzzy", SqlDbType.NChar, 400) { Value = string.Format("%{0}%", queryObject.ImageFuzzy) });
            }
            if (queryObject.CouponType.HasValue)
            {
                whereSqlBuilder.Append(" AND CouponType = @CouponType ");
                sqlParameterList.Add(new SqlParameter("@CouponType ", SqlDbType.Int, 4) { Value = queryObject.CouponType });
            }
            if (queryObject.IsGiftCoupon)
            {
                whereSqlBuilder.Append(" AND CouponType != 3  ");
            }
            if (queryObject.IsDisplay.HasValue)
            {
                whereSqlBuilder.Append(" AND IsDisplay = @IsDisplay ");
                sqlParameterList.Add(new SqlParameter("@IsDisplay ", SqlDbType.Bit, 1) { Value = queryObject.IsDisplay });
            }
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@PageSize", pageSize));
                sqlParameterList.Add(new SqlParameter("@PageIndex", pageSize * (pageIndex - 1)));
            }
            sqlParameters = sqlParameterList.ToArray();
        }

        private static StringBuilder GetOrderSql(CouponOrderColumn orderColumn, SortOrder order)
        {
            StringBuilder orderSqlBuilder = new StringBuilder(" ORDER BY ");
            switch (orderColumn)
            {
                case CouponOrderColumn.CouponId:
                    orderSqlBuilder.Append("CouponId");
                    break;
                case CouponOrderColumn.CouponName:
                    orderSqlBuilder.Append("CouponName");
                    break;
                case CouponOrderColumn.ValidityPeriod:
                    orderSqlBuilder.Append("ValidityPeriod");
                    break;
                case CouponOrderColumn.StartDate:
                    orderSqlBuilder.Append("StartDate");
                    break;
                case CouponOrderColumn.EndDate:
                    orderSqlBuilder.Append("EndDate");
                    break;
                case CouponOrderColumn.SheetNumber:
                    orderSqlBuilder.Append("SheetNumber");
                    break;
                case CouponOrderColumn.SendCount:
                    orderSqlBuilder.Append("SendCount");
                    break;
                case CouponOrderColumn.ShopId:
                    orderSqlBuilder.Append("ShopId");
                    break;
                case CouponOrderColumn.RequirementMoney:
                    orderSqlBuilder.Append("RequirementMoney");
                    break;
                case CouponOrderColumn.SortOrder:
                    orderSqlBuilder.Append("SortOrder");
                    break;
                case CouponOrderColumn.DeductibleAmount:
                    orderSqlBuilder.Append("DeductibleAmount");
                    break;
                case CouponOrderColumn.State:
                    orderSqlBuilder.Append("State");
                    break;
                case CouponOrderColumn.CreatedBy:
                    orderSqlBuilder.Append("CreatedBy");
                    break;
                case CouponOrderColumn.CreateTime:
                    orderSqlBuilder.Append("CreateTime");
                    break;
                case CouponOrderColumn.LastUpdatedBy:
                    orderSqlBuilder.Append("LastUpdatedBy");
                    break;
                case CouponOrderColumn.LastUpdatedTime:
                    orderSqlBuilder.Append("LastUpdatedTime");
                    break;
                case CouponOrderColumn.Remark:
                    orderSqlBuilder.Append("Remark");
                    break;
                case CouponOrderColumn.RefuseReason:
                    orderSqlBuilder.Append("RefuseReason");
                    break;
                case CouponOrderColumn.AuditEmployee:
                    orderSqlBuilder.Append("AuditEmployee");
                    break;
                case CouponOrderColumn.AuditTime:
                    orderSqlBuilder.Append("AuditTime");
                    break;
                case CouponOrderColumn.ShopName:
                    orderSqlBuilder.Append("ShopName");
                    break;
                case CouponOrderColumn.ShopAddress:
                    orderSqlBuilder.Append("ShopAddress");
                    break;
                case CouponOrderColumn.IsGot:
                    orderSqlBuilder.Append("IsGot");
                    break;
                case CouponOrderColumn.Longitude:
                    orderSqlBuilder.Append("Longitude");
                    break;
                case CouponOrderColumn.Latitude:
                    orderSqlBuilder.Append("Latitude");
                    break;
                case CouponOrderColumn.DeductibleProportion:
                    orderSqlBuilder.Append("DeductibleProportion");
                    break;
                case CouponOrderColumn.Image:
                    orderSqlBuilder.Append("Image");
                    break;
                case CouponOrderColumn.CouponType:
                    orderSqlBuilder.Append("CouponType");
                    break;
                case CouponOrderColumn.IsDisplay:
                    orderSqlBuilder.Append("IsDisplay");
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

        /// <summary>
        /// 修改抵扣券已发送数量,并且检查是否还有可用的抵扣券 add by zhujinlei 2015/06/17
        /// </summary>
        /// <param name="CouponId"></param>
        /// <returns></returns>
        public bool CheckHasCouponAndUpdate(int CouponId)
        {
            string sql = @"update Coupon set SendCount=SendCount+1 where 
                            SheetNumber>SendCount 
                            AND State=1
                            AND EndDate>GETDATE()
                            AND CouponId=@CouponId ";
            var count =SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@CouponId", SqlDbType.Int, 4) { Value = CouponId });
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 券支付情况统计
        /// </summary>
        /// <param name="cityid"></param>
        /// <param name="OperateBeginTime"></param>
        /// <param name="OperateEndTime"></param>
        /// <returns></returns>
        public DataTable GetCouponCount(int cityid, string OperateBeginTime, string OperateEndTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select case s.cityID when 87 then '杭州' when 73 then '上海' end 城市,");
            strSql.Append(" s.shopName 门店,c.mobilePhoneNumber 用户手机,c.RegisterDate 注册时间,p.prePayTime 支付时间,");
            strSql.Append(" p.prePaidSum 支付金额,d.CouponSendDetailID 抵扣券来源,d.GetTime 抵扣券领取时间,d.DeductibleAmount 抵扣金额 ");
            strSql.Append(" from PreOrder19dian p ");
            strSql.Append(" inner join CustomerInfo c on p.customerId=c.CustomerID");
            strSql.Append(" inner join ShopInfo s on p.shopId=s.shopID");
            strSql.Append(" left join CouponGetDetail d on p.preOrder19dianId=d.PreOrder19DianId");
            strSql.Append(" where p.isPaid=1");
            strSql.Append(" and p.prePaidSum>0");
            if (!string.IsNullOrEmpty(OperateBeginTime))
            {
                strSql.AppendFormat(" and p.prePayTime >= '{0}'", OperateBeginTime);
            }
            if (!string.IsNullOrEmpty(OperateEndTime))
            {
                strSql.AppendFormat(" and p.prePayTime <= '{0}'", OperateEndTime);
            }
            if (cityid > 0)
            {
                strSql.AppendFormat(" and s.cityId={0}", cityid);
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 某张券的领取情况
        /// </summary>
        /// <param name="CouponId">券ID</param>
        /// <param name="Used">是否使用过</param>
        /// <returns></returns>
        public long GetCountByCouponId(int CouponId,bool Used)
        {
            string sql = @"select Count(*) from CouponGetDetail a
left join PreOrder19dian b on a.PreOrder19DianId=b.preOrder19dianId
where CouponId=@CouponId ";
            if (Used)
            {
                sql += " and prePaidSum>isnull(refundMoneySum,0) and State=2 and isApproved=1 and isPaid=1";
            }
            object retutnValue;
           
            SqlParameter[] sqlParameters = new SqlParameter[]{
                new SqlParameter("@CouponId",SqlDbType.Int){Value=CouponId}
            };

            retutnValue = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters);
            long count;
            if (long.TryParse(retutnValue.ToString(), out count))
            {
                return count;
            }
            return 0;
        }
    }
}