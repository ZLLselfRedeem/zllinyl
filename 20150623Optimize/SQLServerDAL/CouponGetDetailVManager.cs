using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.Interface;
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public partial class CouponGetDetailVManager
    {
        public bool IsExists(int CouponGetDetailId)
        {
            string sql = "SELECT COUNT(0) FROM CouponGetDetailV WHERE CouponGetDetailID = @CouponGetDetailID";
            var retutnValue =
                SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@CouponGetDetailID",SqlDbType.Int,4) { Value = CouponGetDetailId });
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
        
        public CouponGetDetailV GetEntityById(int CouponGetDetailId)
        {
            string sql  = @"SELECT [CouponGetDetailID]
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
                                ,[SharePreOrder19DianId]
                                ,[OriginalNumber]
                                ,[CheckTime]
                                ,[CouponSendDetailID]
                                ,[ShopId]
                                ,[CityId]
                                ,[ShopName],PreOrderSum,IsApproved
                            FROM  [CouponGetDetailV] 
                            WHERE [CouponGetDetailID] = @CouponGetDetailID ";
             using(var reader = 
                    SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@CouponGetDetailID", SqlDbType.Int, 4){ Value = CouponGetDetailId }))
            {
                if(reader.Read())
                {
                  var entity = reader.GetEntity<CouponGetDetailV>();
                  return entity;
                }
            }
            return null;
        }
        
        public long GetCountByQuery(CouponGetDetailVQueryObject queryObject = null)
        {
            string sql = @"SELECT COUNT(0)
                            FROM  [CouponGetDetailV] 
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

        public List<ICouponGetDetailV> GetListByQuery(int pageSize, int pageIndex, CouponGetDetailVQueryObject queryObject = null, CouponGetDetailVOrderColumn orderColumn = CouponGetDetailVOrderColumn.CouponGetDetailId, SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql  = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[CouponGetDetailID]
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
                                ,[SharePreOrder19DianId]
                                ,[OriginalNumber]
                                ,[CheckTime]
                                ,[CouponSendDetailID]
                                ,[ShopId]
                                ,[CityId]
                                ,[ShopName],PreOrderSum,IsApproved
                            FROM  [CouponGetDetailV] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject,ref sqlParameters, whereSqlBuilder,pageSize,pageIndex);
            sql = whereSqlBuilder.Insert(0, sql).ToString(); 
            sql =  string.Format( " SELECT TOP (@PageSize) * FROM ({0}) T WHERE T.ROWNUM > @PageIndex ",sql);
            List<ICouponGetDetailV> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<ICouponGetDetailV>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<CouponGetDetailV>());
                }
            }
            return list;
        }
        
        public List<ICouponGetDetailV> GetListByQuery(CouponGetDetailVQueryObject queryObject = null,CouponGetDetailVOrderColumn orderColumn = CouponGetDetailVOrderColumn.CouponGetDetailId,SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql  = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[CouponGetDetailID]
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
                                ,[SharePreOrder19DianId]
                                ,[OriginalNumber]
                                ,[CheckTime]
                                ,[CouponSendDetailID]
                                ,[ShopId]
                                ,[CityId]
                                ,[ShopName],PreOrderSum,IsApproved,[MaxAmount]
                            FROM  [CouponGetDetailV] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject,ref sqlParameters, whereSqlBuilder);
            sql = whereSqlBuilder.Insert(0, sql).ToString(); 
            List<ICouponGetDetailV> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<ICouponGetDetailV>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<CouponGetDetailV>());
                }
            }
            return list;
        }
        private static void GetWhereSqlBuilderAndSqlParameterList(CouponGetDetailVQueryObject queryObject, ref SqlParameter[] sqlParameters,
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
                whereSqlBuilder.Append(" AND CouponGetDetailID = @CouponGetDetailID ");
                sqlParameterList.Add(new SqlParameter("@CouponGetDetailID ", SqlDbType.Int, 4) { Value = queryObject.CouponGetDetailId });
            }
            if (!string.IsNullOrEmpty(queryObject.CouponDetailNumber))
            {
                whereSqlBuilder.Append(" AND CouponDetailNumber = @CouponDetailNumber ");
                sqlParameterList.Add(new SqlParameter("@CouponDetailNumber", SqlDbType.NVarChar, 100) { Value = queryObject.CouponDetailNumber });
            }
            if (!string.IsNullOrEmpty(queryObject.CouponDetailNumberFuzzy))
            {
                whereSqlBuilder.Append(" AND CouponDetailNumber LIKE @CouponDetailNumberFuzzy ");
                sqlParameterList.Add(new SqlParameter("@CouponDetailNumberFuzzy", SqlDbType.NVarChar, 100) { Value = string.Format("%{0}%", queryObject.CouponDetailNumberFuzzy) });
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
            if (queryObject.ReconciliationTimeFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND ReconciliationTime >= @ReconciliationTimeFrom ");
                sqlParameterList.Add(new SqlParameter("@ReconciliationTimeFrom", SqlDbType.DateTime, 8) { Value = queryObject.ReconciliationTimeFrom });
            }
            if (queryObject.ReconciliationTimeTo.HasValue)
            {
                whereSqlBuilder.Append(" AND ReconciliationTime  <= @ReconciliationTimeTo ");
                sqlParameterList.Add(new SqlParameter("@ReconciliationTimeTo", SqlDbType.DateTime, 8) { Value = queryObject.ReconciliationTimeTo });
            }
            if (queryObject.CorrectTimeTo.HasValue)
            {
                whereSqlBuilder.Append(" AND CorrectTime <= @CorrectTimeTo ");
                sqlParameterList.Add(new SqlParameter("@CorrectTimeTo", SqlDbType.DateTime, 8) { Value = queryObject.CorrectTimeTo });
            }
            if (queryObject.SharePreOrder19DianId.HasValue)
            {
                whereSqlBuilder.Append(" AND SharePreOrder19DianId = @SharePreOrder19DianId ");
                sqlParameterList.Add(new SqlParameter("@SharePreOrder19DianId ", SqlDbType.BigInt, 8) { Value = queryObject.SharePreOrder19DianId });
            }
            if (!string.IsNullOrEmpty(queryObject.OriginalNumber))
            {
                whereSqlBuilder.Append(" AND OriginalNumber = @OriginalNumber ");
                sqlParameterList.Add(new SqlParameter("@OriginalNumber", SqlDbType.NVarChar, 50) { Value = queryObject.OriginalNumber });
            }
            if (!string.IsNullOrEmpty(queryObject.OriginalNumberFuzzy))
            {
                whereSqlBuilder.Append(" AND OriginalNumber LIKE @OriginalNumberFuzzy ");
                sqlParameterList.Add(new SqlParameter("@OriginalNumberFuzzy", SqlDbType.NVarChar, 50) { Value = string.Format("%{0}%", queryObject.OriginalNumberFuzzy) });
            }
            if (queryObject.CheckTime.HasValue)
            {
                whereSqlBuilder.Append(" AND CheckTime = @CheckTime ");
                sqlParameterList.Add(new SqlParameter("@CheckTime ", SqlDbType.DateTime, 8) { Value = queryObject.CheckTime });
            }
            if (queryObject.CouponSendDetailID.HasValue)
            {
                whereSqlBuilder.Append(" AND CouponSendDetailID = @CouponSendDetailID ");
                sqlParameterList.Add(new SqlParameter("@CouponSendDetailID ", SqlDbType.BigInt, 8) { Value = queryObject.CouponSendDetailID });
            }
            if (queryObject.ShopId.HasValue)
            {
                whereSqlBuilder.Append(" AND ShopId = @ShopId ");
                sqlParameterList.Add(new SqlParameter("@ShopId ", SqlDbType.Int, 4) { Value = queryObject.ShopId });
            }
            if (queryObject.IsApproved.HasValue)
            {
                whereSqlBuilder.Append(" AND IsApproved = @IsApproved ");
                sqlParameterList.Add(new SqlParameter("@IsApproved ", SqlDbType.SmallInt) { Value = queryObject.IsApproved });
            }
            if (queryObject.CityId.HasValue)
            {
                whereSqlBuilder.Append(" AND CityId = @CityId ");
                sqlParameterList.Add(new SqlParameter("@CityId ", SqlDbType.Int, 4) { Value = queryObject.CityId });
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
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@PageSize", pageSize));
                sqlParameterList.Add(new SqlParameter("@PageIndex", pageSize * (pageIndex - 1)));
            }
            sqlParameters = sqlParameterList.ToArray();
        }
        
        private static StringBuilder GetOrderSql(CouponGetDetailVOrderColumn orderColumn, SortOrder order)
        {
            StringBuilder orderSqlBuilder = new StringBuilder(" ORDER BY ");
            switch (orderColumn)
            {
                case CouponGetDetailVOrderColumn.CouponGetDetailId:
                    orderSqlBuilder.Append("CouponGetDetailID");
                    break;
                case CouponGetDetailVOrderColumn.CouponDetailNumber:
                    orderSqlBuilder.Append("CouponDetailNumber");
                    break;
                case CouponGetDetailVOrderColumn.GetTime:
                    orderSqlBuilder.Append("GetTime");
                    break;
                case CouponGetDetailVOrderColumn.ValidityEnd:
                    orderSqlBuilder.Append("ValidityEnd");
                    break;
                case CouponGetDetailVOrderColumn.RequirementMoney:
                    orderSqlBuilder.Append("RequirementMoney");
                    break;
                case CouponGetDetailVOrderColumn.DeductibleAmount:
                    orderSqlBuilder.Append("DeductibleAmount");
                    break;
                case CouponGetDetailVOrderColumn.State:
                    orderSqlBuilder.Append("State");
                    break;
                case CouponGetDetailVOrderColumn.MobilePhoneNumber:
                    orderSqlBuilder.Append("MobilePhoneNumber");
                    break;
                case CouponGetDetailVOrderColumn.CouponId:
                    orderSqlBuilder.Append("CouponId");
                    break;
                case CouponGetDetailVOrderColumn.UseTime:
                    orderSqlBuilder.Append("UseTime");
                    break;
                case CouponGetDetailVOrderColumn.PreOrder19DianId:
                    orderSqlBuilder.Append("PreOrder19DianId");
                    break;
                case CouponGetDetailVOrderColumn.IsCorrected:
                    orderSqlBuilder.Append("IsCorrected");
                    break;
                case CouponGetDetailVOrderColumn.CorrectTime:
                    orderSqlBuilder.Append("CorrectTime");
                    break;
                case CouponGetDetailVOrderColumn.SharePreOrder19DianId:
                    orderSqlBuilder.Append("SharePreOrder19DianId");
                    break;
                case CouponGetDetailVOrderColumn.OriginalNumber:
                    orderSqlBuilder.Append("OriginalNumber");
                    break;
                case CouponGetDetailVOrderColumn.CheckTime:
                    orderSqlBuilder.Append("CheckTime");
                    break;
                case CouponGetDetailVOrderColumn.CouponSendDetailId:
                    orderSqlBuilder.Append("CouponSendDetailID");
                    break;
                case CouponGetDetailVOrderColumn.ShopId:
                    orderSqlBuilder.Append("ShopId");
                    break;
                case CouponGetDetailVOrderColumn.CityId:
                    orderSqlBuilder.Append("CityId");
                    break;
                case CouponGetDetailVOrderColumn.ShopName:
                    orderSqlBuilder.Append("ShopName");
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
        /// 获取指定店铺抵扣卷列表 add by zhujinlei 2015/06/16
        /// </summary>
        /// <param name="shopid">店铺ID</param>
        /// <returns>返回指定店铺抵扣卷列表</returns>
        public List<CouponGetDetailV> GetOrderPaymentCouponDetail(int shopid)
        {
            string sql = @"SELECT couponid as CouponGetDetailID, 
                                  requirementmoney,
                                  deductibleamount,
                                  maxamount,
                                  enddate as ValidityEnd
                            FROM  coupon 
                            WHERE  State=1 AND CouponType=1
                                   AND SendCount<SheetNumber
                                   AND EndDate>GETDATE()
                                   AND StartDate<GETDATE()
                                   AND ShopId =@ShopID ";

            var list = new List<CouponGetDetailV>();
            using (var reader =
                   SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@ShopID", SqlDbType.Int, 6) { Value = shopid }))
            {
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<CouponGetDetailV>());
                }
            }
            return list;
        }
    }
}