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
    public partial class CouponGetDetailManager
    {
        public bool IsExists(long CouponGetDetailId)
        {
            string sql = "SELECT COUNT(0) FROM CouponGetDetail WHERE CouponGetDetailId = @CouponGetDetailId";
            var retutnValue =
                SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@CouponGetDetailId", SqlDbType.Int, 8) { Value = CouponGetDetailId });
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

        public CouponGetDetail GetEntityById(long CouponGetDetailId)
        {
            string sql = @"SELECT [CouponGetDetailId]
                                ,[CouponDetailNumber]
                                ,[GetTime]
                                ,[ValidityEnd]
                                ,[RequirementMoney]
                                ,[DeductibleAmount]
                                ,[State]
                                ,[MobilePhoneNumber]
                                ,[CouponId]
                                ,[UseTime]
                                ,[PreOrder19DianId]
                                ,[IsCorrected]
                                ,[CorrectTime]
                                ,[OriginalNumber]
                                ,[SharePreOrder19DianId]
                                ,[CouponSendDetailID]
                            FROM  [CouponGetDetail] 
                            WHERE [CouponGetDetailId] = @CouponGetDetailId ";
            using (SqlDataReader reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@CouponGetDetailId", SqlDbType.BigInt, 8) { Value = CouponGetDetailId }))
            {
                if (reader.Read())
                {
                    var entity = reader.GetEntity<CouponGetDetail>();
                    return entity;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool Add(CouponGetDetail Entity)
        {
            string sql = @"INSERT INTO CouponGetDetail ( 
                                    [CouponDetailNumber],
                                    [GetTime],
                                    [ValidityEnd],
                                    [RequirementMoney],
                                    [DeductibleAmount],
                                    [State],
                                    [MobilePhoneNumber],
                                    [CouponId],
                                    [UseTime],
                                    [PreOrder19DianId],
                                    [IsCorrected],
                                    [CorrectTime],
                                    [OriginalNumber],
                                    [SharePreOrder19DianId],
                                    [CouponSendDetailID],
                                    [RealDeductibleAmount]
                                ) VALUES ( 
                                    @CouponDetailNumber,
                                    @GetTime,
                                    @ValidityEnd,
                                    @RequirementMoney,
                                    @DeductibleAmount,
                                    @State,
                                    @MobilePhoneNumber,
                                    @CouponId,
                                    @UseTime,
                                    @PreOrder19DianId,
                                    @IsCorrected,
                                    @CorrectTime,
                                    @OriginalNumber,
                                    @SharePreOrder19DianId,
                                    @CouponSendDetailID,
                                    @RealDeductibleAmount
                                    );                                    
                                    SELECT @@IDENTITY";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@CouponDetailNumber", SqlDbType.NVarChar, 100) { Value = Entity.CouponDetailNumber });
            sqlParameterList.Add(new SqlParameter("@GetTime", SqlDbType.DateTime, 8) { Value = Entity.GetTime });
            sqlParameterList.Add(new SqlParameter("@ValidityEnd", SqlDbType.DateTime, 8) { Value = Entity.ValidityEnd });
            sqlParameterList.Add(new SqlParameter("@RequirementMoney", SqlDbType.Float, 8) { Value = Entity.RequirementMoney });
            sqlParameterList.Add(new SqlParameter("@DeductibleAmount", SqlDbType.Float, 8) { Value = Entity.DeductibleAmount });
            sqlParameterList.Add(new SqlParameter("@RealDeductibleAmount", SqlDbType.Float, 8) { Value = Entity.RealDeductibleAmount });
            sqlParameterList.Add(new SqlParameter("@State", SqlDbType.Int, 4) { Value = Entity.State });
            sqlParameterList.Add(new SqlParameter("@MobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = Entity.MobilePhoneNumber });
            sqlParameterList.Add(new SqlParameter("@CouponId", SqlDbType.Int, 4) { Value = Entity.CouponId });
            sqlParameterList.Add(new SqlParameter("@UseTime", SqlDbType.DateTime, 8) { Value = GetDBNullValue(Entity.UseTime) });
            sqlParameterList.Add(new SqlParameter("@PreOrder19DianId", SqlDbType.BigInt, 8) { Value = GetDBNullValue(Entity.PreOrder19DianId) });
            sqlParameterList.Add(new SqlParameter("@IsCorrected", SqlDbType.Bit, 1) { Value = Entity.IsCorrected });
            sqlParameterList.Add(new SqlParameter("@CorrectTime", SqlDbType.DateTime, 8) { Value = GetDBNullValue(Entity.CorrectTime) });
            sqlParameterList.Add(new SqlParameter("@OriginalNumber", SqlDbType.NVarChar, 100) { Value = GetDBNullValue(Entity.OriginalNumber) });
            sqlParameterList.Add(new SqlParameter("@SharePreOrder19DianId", SqlDbType.BigInt, 8) { Value = Entity.SharePreOrder19DianId });
            sqlParameterList.Add(new SqlParameter("@CouponSendDetailID", SqlDbType.BigInt, 8) { Value = Entity.CouponSendDetailID });
            var returnObject =
                SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            int id;
            if (int.TryParse(returnObject.ToString(), out id))
            {
                Entity.CouponGetDetailID = id;
                return true;
            }
            return false;
        }
        public bool Update(CouponGetDetail Entity)
        {
            try
            {
                string sql = @"UPDATE [CouponGetDetail] SET
                                 [CouponDetailNumber] = @CouponDetailNumber
                                ,[GetTime] = @GetTime
                                ,[ValidityEnd] = @ValidityEnd
                                ,[RequirementMoney] = @RequirementMoney
                                ,[DeductibleAmount] = @DeductibleAmount
                                ,[State] = @State
                                ,[MobilePhoneNumber] = @MobilePhoneNumber
                                ,[CouponId] = @CouponId
                                ,[UseTime] = @UseTime
                                ,[PreOrder19DianId] = @PreOrder19DianId
                                ,[IsCorrected] = @IsCorrected
                                ,[CorrectTime] = @CorrectTime
                                ,[OriginalNumber] = @OriginalNumber 
                                ,[SharePreOrder19DianId] = @SharePreOrder19DianId
                                ,[CouponSendDetailID] = @CouponSendDetailID
                                ,[RealDeductibleAmount] = @RealDeductibleAmount
                           WHERE [CouponGetDetailId] =@CouponGetDetailId";
                List<SqlParameter> sqlParameterList = new List<SqlParameter>();
                sqlParameterList.Add(new SqlParameter("@CouponGetDetailId", SqlDbType.BigInt, 8) { Value = Entity.CouponGetDetailID });
                sqlParameterList.Add(new SqlParameter("@CouponDetailNumber", SqlDbType.NVarChar, 100) { Value = Entity.CouponDetailNumber });
                sqlParameterList.Add(new SqlParameter("@GetTime", SqlDbType.DateTime, 8) { Value = Entity.GetTime });
                sqlParameterList.Add(new SqlParameter("@ValidityEnd", SqlDbType.DateTime, 8) { Value = Entity.ValidityEnd });
                sqlParameterList.Add(new SqlParameter("@RequirementMoney", SqlDbType.Float, 8) { Value = Entity.RequirementMoney });
                sqlParameterList.Add(new SqlParameter("@DeductibleAmount", SqlDbType.Float, 8) { Value = Entity.DeductibleAmount });
                sqlParameterList.Add(new SqlParameter("@RealDeductibleAmount", SqlDbType.Float, 8) { Value = Entity.RealDeductibleAmount });
                sqlParameterList.Add(new SqlParameter("@State", SqlDbType.Int, 4) { Value = Entity.State });
                sqlParameterList.Add(new SqlParameter("@MobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = Entity.MobilePhoneNumber });
                sqlParameterList.Add(new SqlParameter("@CouponId", SqlDbType.Int, 4) { Value = Entity.CouponId });
                sqlParameterList.Add(new SqlParameter("@UseTime", SqlDbType.DateTime, 8) { Value = GetDBNullValue(Entity.UseTime) });
                sqlParameterList.Add(new SqlParameter("@PreOrder19DianId", SqlDbType.BigInt, 8) { Value = GetDBNullValue(Entity.PreOrder19DianId) });
                sqlParameterList.Add(new SqlParameter("@IsCorrected", SqlDbType.Bit, 1) { Value = Entity.IsCorrected });
                sqlParameterList.Add(new SqlParameter("@CorrectTime", SqlDbType.DateTime, 8) { Value = GetDBNullValue(Entity.CorrectTime) });
                sqlParameterList.Add(new SqlParameter("@OriginalNumber", SqlDbType.NVarChar, 100) { Value = GetDBNullValue(Entity.OriginalNumber) });
                sqlParameterList.Add(new SqlParameter("@SharePreOrder19DianId", SqlDbType.BigInt, 8) { Value = Entity.SharePreOrder19DianId });
                sqlParameterList.Add(new SqlParameter("@CouponSendDetailID", SqlDbType.BigInt, 8) { Value = Entity.CouponSendDetailID });
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    var count = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteEntity(CouponGetDetail Entity)
        {
            string sql = @"DELETE FROM [CouponGetDetail]
                                 WHERE [PreOrder19DianId] =@PreOrder19DianId";
            var count =
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@PreOrder19DianId", SqlDbType.BigInt, 8) { Value = Entity.PreOrder19DianId });
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public long GetCountByQuery(CouponGetDetailQueryObject queryObject = null)
        {
            string sql = @"SELECT COUNT(0)
                            FROM  [CouponGetDetail] 
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

        public List<CouponGetDetail> GetListByQuery(int pageSize, int pageIndex, CouponGetDetailQueryObject queryObject = null, CouponGetDetailOrderColumn orderColumn = CouponGetDetailOrderColumn.CouponGetDetailId, SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[CouponGetDetailId]
                                ,[CouponDetailNumber]
                                ,[GetTime]
                                ,[ValidityEnd]
                                ,[RequirementMoney]
                                ,[DeductibleAmount]
                                ,[State]
                                ,[MobilePhoneNumber]
                                ,[CouponId]
                                ,[UseTime]
                                ,[PreOrder19DianId]
                                ,[IsCorrected]
                                ,[CorrectTime]
                                ,[OriginalNumber]
                                ,[SharePreOrder19DianId]
                                ,[CouponSendDetailID]
                                ,[RealDeductibleAmount]
                            FROM  [CouponGetDetail] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder, pageSize, pageIndex);
            sql = whereSqlBuilder.Insert(0, sql).ToString();
            sql = string.Format(" SELECT TOP (@PageSize) * FROM ({0}) T WHERE T.ROWNUM > @PageIndex ", sql);
            List<CouponGetDetail> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<CouponGetDetail>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<CouponGetDetail>());
                }
            }
            return list;
        }

        public List<CouponGetDetail> GetListByQuery(CouponGetDetailQueryObject queryObject = null, CouponGetDetailOrderColumn orderColumn = CouponGetDetailOrderColumn.CouponGetDetailId, SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[CouponGetDetailId]
                                ,[CouponDetailNumber]
                                ,[GetTime]
                                ,[ValidityEnd]
                                ,[RequirementMoney]
                                ,[DeductibleAmount]
                                ,[State]
                                ,[MobilePhoneNumber]
                                ,[CouponId]
                                ,[UseTime]
                                ,[PreOrder19DianId]
                                ,[IsCorrected]
                                ,[CorrectTime]
                                ,[OriginalNumber]
                                ,[SharePreOrder19DianId]
                                ,[CouponSendDetailID]
                                ,[RealDeductibleAmount]
                            FROM  [CouponGetDetail] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder);
            sql = whereSqlBuilder.Insert(0, sql).ToString();
            List<CouponGetDetail> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<CouponGetDetail>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<CouponGetDetail>());
                }
            }
            return list;
        }
        private static void GetWhereSqlBuilderAndSqlParameterList(CouponGetDetailQueryObject queryObject, ref SqlParameter[] sqlParameters,
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
            if (queryObject.CouponGetDetailId.HasValue)
            {
                whereSqlBuilder.Append(" AND CouponGetDetailId = @CouponGetDetailId ");
                sqlParameterList.Add(new SqlParameter("@CouponGetDetailId ", SqlDbType.BigInt, 8) { Value = queryObject.CouponGetDetailId });
            }
            if (!string.IsNullOrEmpty(queryObject.CouponDetailNumber))
            {
                whereSqlBuilder.Append(" AND CouponDetailNumber = @CouponDetailNumber ");
                sqlParameterList.Add(new SqlParameter("@CouponDetailNumber", SqlDbType.NVarChar, 100) { Value = queryObject.CouponDetailNumber });
            }
            if (queryObject.GetTime.HasValue)
            {
                whereSqlBuilder.Append(" AND GetTime = @GetTime ");
                sqlParameterList.Add(new SqlParameter("@GetTime ", SqlDbType.DateTime, 8) { Value = queryObject.GetTime });
            }
            if (queryObject.GetTimeFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND GetTime >= @GetTimeFrom ");
                sqlParameterList.Add(new SqlParameter("@GetTimeFrom", SqlDbType.DateTime, 8) { Value = queryObject.GetTimeFrom });
            }
            if (queryObject.GetTimeTo.HasValue)
            {
                whereSqlBuilder.Append(" AND GetTime <= @GetTimeTo ");
                sqlParameterList.Add(new SqlParameter("@GetTimeTo", SqlDbType.DateTime, 8) { Value = queryObject.GetTimeTo });
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
            if (!string.IsNullOrEmpty(queryObject.MobilePhoneNumber))
            {
                whereSqlBuilder.Append(" AND MobilePhoneNumber = @MobilePhoneNumber ");
                sqlParameterList.Add(new SqlParameter("@MobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = queryObject.MobilePhoneNumber });
            }
            if (!string.IsNullOrEmpty(queryObject.MobilePhoneNumberFuzzy))
            {
                whereSqlBuilder.Append(" AND MobilePhoneNumber LIKE @MobilePhoneNumberFuzzy ");
                sqlParameterList.Add(new SqlParameter("@MobilePhoneNumberFuzzy", SqlDbType.NVarChar, 50) { Value = string.Format("%{0}%", queryObject.MobilePhoneNumberFuzzy) });
            }
            if (queryObject.CouponId.HasValue)
            {
                whereSqlBuilder.Append(" AND CouponId = @CouponId ");
                sqlParameterList.Add(new SqlParameter("@CouponId ", SqlDbType.Int, 4) { Value = queryObject.CouponId });
            }
            if (queryObject.SharePreOrder19DianId.HasValue)
            {
                whereSqlBuilder.Append(" AND SharePreOrder19DianId = @SharePreOrder19DianId ");
                sqlParameterList.Add(new SqlParameter("@SharePreOrder19DianId ", SqlDbType.BigInt, 8) { Value = queryObject.SharePreOrder19DianId });
            }
            if (queryObject.CouponSendDetailID.HasValue)
            {
                whereSqlBuilder.Append(" AND CouponSendDetailID = @CouponSendDetailID ");
                sqlParameterList.Add(new SqlParameter("@CouponSendDetailID ", SqlDbType.BigInt, 8) { Value = queryObject.CouponSendDetailID.Value });
            }
            if (queryObject.UseTime.HasValue)
            {
                whereSqlBuilder.Append(" AND UseTime = @UseTime ");
                sqlParameterList.Add(new SqlParameter("@UseTime ", SqlDbType.DateTime, 8) { Value = queryObject.UseTime });
            }
            if (queryObject.UseTimeFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND UseTime >= @UseTimeFrom ");
                sqlParameterList.Add(new SqlParameter("@UseTimeFrom", SqlDbType.DateTime, 8) { Value = queryObject.UseTimeFrom });
            }
            if (queryObject.UseTimeTo.HasValue)
            {
                whereSqlBuilder.Append(" AND UseTime <= @UseTimeTo ");
                sqlParameterList.Add(new SqlParameter("@UseTimeTo", SqlDbType.DateTime, 8) { Value = queryObject.UseTimeTo });
            }
            if (queryObject.PreOrder19DianId.HasValue)
            {
                whereSqlBuilder.Append(" AND PreOrder19DianId = @PreOrder19DianId ");
                sqlParameterList.Add(new SqlParameter("@PreOrder19DianId ", SqlDbType.BigInt, 8) { Value = queryObject.PreOrder19DianId });
            }
            if (queryObject.IsCorrected.HasValue)
            {
                whereSqlBuilder.Append(" AND IsCorrected = @IsCorrected ");
                sqlParameterList.Add(new SqlParameter("@IsCorrected ", SqlDbType.Bit, 1) { Value = queryObject.IsCorrected });
            }
            if (queryObject.CorrectTime.HasValue)
            {
                whereSqlBuilder.Append(" AND CorrectTime = @CorrectTime ");
                sqlParameterList.Add(new SqlParameter("@CorrectTime ", SqlDbType.DateTime, 8) { Value = queryObject.CorrectTime });
            }
            if (queryObject.CorrectTimeFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND CorrectTime >= @CorrectTimeFrom ");
                sqlParameterList.Add(new SqlParameter("@CorrectTimeFrom", SqlDbType.DateTime, 8) { Value = queryObject.CorrectTimeFrom });
            }
            if (queryObject.CorrectTimeTo.HasValue)
            {
                whereSqlBuilder.Append(" AND CorrectTime <= @CorrectTimeTo ");
                sqlParameterList.Add(new SqlParameter("@CorrectTimeTo", SqlDbType.DateTime, 8) { Value = queryObject.CorrectTimeTo });
            }
            if (!string.IsNullOrEmpty(queryObject.OriginalNumber))
            {
                whereSqlBuilder.Append(" AND OriginalNumber = @OriginalNumber ");
                sqlParameterList.Add(new SqlParameter("@OriginalNumber", SqlDbType.NVarChar, 100) { Value = queryObject.OriginalNumber });
            }
            if (!string.IsNullOrEmpty(queryObject.OriginalNumberFuzzy))
            {
                whereSqlBuilder.Append(" AND OriginalNumber LIKE @OriginalNumberFuzzy ");
                sqlParameterList.Add(new SqlParameter("@OriginalNumberFuzzy", SqlDbType.NVarChar, 100) { Value = string.Format("%{0}%", queryObject.OriginalNumberFuzzy) });
            }
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@PageSize", pageSize));
                sqlParameterList.Add(new SqlParameter("@PageIndex", pageSize * (pageIndex - 1)));
            }
            sqlParameters = sqlParameterList.ToArray();
        }

        private static StringBuilder GetOrderSql(CouponGetDetailOrderColumn orderColumn, SortOrder order)
        {
            StringBuilder orderSqlBuilder = new StringBuilder(" ORDER BY ");
            switch (orderColumn)
            {
                case CouponGetDetailOrderColumn.CouponGetDetailId:
                    orderSqlBuilder.Append("CouponGetDetailId");
                    break;
                case CouponGetDetailOrderColumn.CouponDetailNumber:
                    orderSqlBuilder.Append("CouponDetailNumber");
                    break;
                case CouponGetDetailOrderColumn.GetTime:
                    orderSqlBuilder.Append("GetTime");
                    break;
                case CouponGetDetailOrderColumn.ValidityEnd:
                    orderSqlBuilder.Append("ValidityEnd");
                    break;
                case CouponGetDetailOrderColumn.RequirementMoney:
                    orderSqlBuilder.Append("RequirementMoney");
                    break;
                case CouponGetDetailOrderColumn.DeductibleAmount:
                    orderSqlBuilder.Append("DeductibleAmount");
                    break;
                case CouponGetDetailOrderColumn.State:
                    orderSqlBuilder.Append("State");
                    break;
                case CouponGetDetailOrderColumn.MobilePhoneNumber:
                    orderSqlBuilder.Append("MobilePhoneNumber");
                    break;
                case CouponGetDetailOrderColumn.CouponId:
                    orderSqlBuilder.Append("CouponId");
                    break;
                case CouponGetDetailOrderColumn.UseTime:
                    orderSqlBuilder.Append("UseTime");
                    break;
                case CouponGetDetailOrderColumn.PreOrder19DianId:
                    orderSqlBuilder.Append("PreOrder19DianId");
                    break;
                case CouponGetDetailOrderColumn.IsCorrected:
                    orderSqlBuilder.Append("IsCorrected");
                    break;
                case CouponGetDetailOrderColumn.CorrectTime:
                    orderSqlBuilder.Append("CorrectTime");
                    break;
                case CouponGetDetailOrderColumn.OriginalNumber:
                    orderSqlBuilder.Append("OriginalNumber");
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
        /// 支付或退款时更新用户优惠券状态
        /// </summary>
        /// <param name="couponGetDetailID"></param>
        /// <param name="preOrder19DianId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool UpdateCouponState(long couponGetDetailID, long preOrder19DianId, CouponUseStateType type)
        {
            StringBuilder strSql = new StringBuilder();
            // update by zhujinlei 2015/05/26 修改实际抵扣金额RealDeductibleAmount=0
            strSql.Append("update CouponGetDetail set State=@State,UseTime=GETDATE(),PreOrder19DianId=@preOrder19DianId,RealDeductibleAmount=DeductibleAmount WHERE couponGetDetailID=@couponGetDetailID");
            if (type == CouponUseStateType.refund)
            {
                strSql.Append(" and State=" + (int)CouponUseStateType.pay + "");
            }

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@State", SqlDbType.Int) { Value = (int)type },
            new SqlParameter("@preOrder19DianId", SqlDbType.BigInt) { Value = preOrder19DianId },
            new SqlParameter("@couponGetDetailID", SqlDbType.BigInt) { Value = couponGetDetailID }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public CouponGetDetail SelectCouponGetDetail(long preOrder19dianId)
        {
            const string strSql = @"SELECT [CouponGetDetailId],[CouponDetailNumber],[GetTime],[ValidityEnd],[RequirementMoney],[DeductibleAmount],[State],[MobilePhoneNumber]
                                ,[CouponId],[UseTime],[PreOrder19DianId],[IsCorrected],[CorrectTime],[OriginalNumber] FROM  [CouponGetDetail] 
                                where PreOrder19DianId=@preOrder19dianId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt) { Value = preOrder19dianId }           
            };
            CouponGetDetail detail = new CouponGetDetail();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (sdr.Read())
                {
                    detail = SqlHelper.GetEntity<CouponGetDetail>(sdr);
                }
            }
            return detail;
        }

        private static object GetDBNullValue(object o)
        {
            if (o == null)
            {
                return DBNull.Value;
            }
            return o;
        }
    }
}
