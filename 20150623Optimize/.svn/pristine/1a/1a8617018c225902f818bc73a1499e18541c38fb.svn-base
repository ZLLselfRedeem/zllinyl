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
    public partial class PreOrder19dianVManager
    {
        public bool IsExists(long PreOrder19dianId)
        {
            string sql = "SELECT COUNT(0) FROM PreOrder19dianV WHERE preOrder19dianId = @preOrder19dianId";
            var retutnValue =
                SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@preOrder19dianId", SqlDbType.Int, 8) { Value = PreOrder19dianId });
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

        public PreOrder19dianV GetEntityById(long PreOrder19dianId)
        {
            string sql = @"SELECT  *
                            FROM  [PreOrder19dianV] 
                            WHERE [preOrder19dianId] = @preOrder19dianId ";
            using (var reader =
                   SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@preOrder19dianId", SqlDbType.BigInt, 8) { Value = PreOrder19dianId }))
            {
                if (reader.Read())
                {
                    var entity = reader.GetEntity<PreOrder19dianV>();
                    return entity;
                }
            }
            return null;
        }

        public long GetCountByQuery(PreOrder19dianVQueryObject queryObject = null)
        {
            string sql = @"SELECT COUNT(0)
                            FROM  [PreOrder19dianV] 
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

        public List<PreOrder19dianV> GetListByQuery(int pageSize, int pageIndex, PreOrder19dianVQueryObject queryObject = null, PreOrder19dianVOrderColumn orderColumn = PreOrder19dianVOrderColumn.PreOrder19dianId, SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,  *
                            FROM  [PreOrder19dianV] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder, pageSize, pageIndex);
            sql = whereSqlBuilder.Insert(0, sql).ToString();
            sql = string.Format(" SELECT TOP (@PageSize) * FROM ({0}) T WHERE T.ROWNUM > @PageIndex ", sql);
            List<PreOrder19dianV> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<PreOrder19dianV>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<PreOrder19dianV>());
                }
            }
            return list;
        }

        public List<PreOrder19dianV> GetListByQuery(PreOrder19dianVQueryObject queryObject = null, PreOrder19dianVOrderColumn orderColumn = PreOrder19dianVOrderColumn.PreOrder19dianId, SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,  *
                            FROM  [PreOrder19dianV] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder);
            sql = whereSqlBuilder.Insert(0, sql).ToString();
            List<PreOrder19dianV> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<PreOrder19dianV>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<PreOrder19dianV>());
                }
            }
            return list;
        }
        private static void GetWhereSqlBuilderAndSqlParameterList(PreOrder19dianVQueryObject queryObject, ref SqlParameter[] sqlParameters,
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
            if (queryObject.PreOrder19dianId.HasValue)
            {
                whereSqlBuilder.Append(" AND preOrder19dianId = @preOrder19dianId ");
                sqlParameterList.Add(new SqlParameter("@preOrder19dianId ", SqlDbType.BigInt, 8) { Value = queryObject.PreOrder19dianId });
            }
            if (!string.IsNullOrEmpty(queryObject.ECardNumber))
            {
                whereSqlBuilder.Append(" AND eCardNumber = @eCardNumber ");
                sqlParameterList.Add(new SqlParameter("@eCardNumber", SqlDbType.NVarChar, 50) { Value = queryObject.ECardNumber });
            }
            if (queryObject.MenuId.HasValue)
            {
                whereSqlBuilder.Append(" AND menuId = @menuId ");
                sqlParameterList.Add(new SqlParameter("@menuId ", SqlDbType.Int, 4) { Value = queryObject.MenuId });
            }
            if (queryObject.CompanyId.HasValue)
            {
                whereSqlBuilder.Append(" AND companyId = @companyId ");
                sqlParameterList.Add(new SqlParameter("@companyId ", SqlDbType.Int, 4) { Value = queryObject.CompanyId });
            }
            if (queryObject.ShopId.HasValue)
            {
                whereSqlBuilder.Append(" AND shopId = @shopId ");
                sqlParameterList.Add(new SqlParameter("@shopId ", SqlDbType.Int, 4) { Value = queryObject.ShopId });
            }
            if (queryObject.CityId.HasValue)
            {
                whereSqlBuilder.Append(" AND CityId = @CityId ");
                sqlParameterList.Add(new SqlParameter("@CityId ", SqlDbType.Int, 4) { Value = queryObject.CityId });
            }
            if (queryObject.CustomerId.HasValue)
            {
                whereSqlBuilder.Append(" AND customerId = @customerId ");
                sqlParameterList.Add(new SqlParameter("@customerId ", SqlDbType.BigInt, 8) { Value = queryObject.CustomerId });
            }
            if (!string.IsNullOrEmpty(queryObject.OrderInJson))
            {
                whereSqlBuilder.Append(" AND orderInJson = @orderInJson ");
                sqlParameterList.Add(new SqlParameter("@orderInJson", SqlDbType.NVarChar, -1) { Value = queryObject.OrderInJson });
            }
            if (!string.IsNullOrEmpty(queryObject.CustomerCookie))
            {
                whereSqlBuilder.Append(" AND customerCookie = @customerCookie ");
                sqlParameterList.Add(new SqlParameter("@customerCookie", SqlDbType.NVarChar, 100) { Value = queryObject.CustomerCookie });
            }
            if (!string.IsNullOrEmpty(queryObject.CustomerUUID))
            {
                whereSqlBuilder.Append(" AND customerUUID = @customerUUID ");
                sqlParameterList.Add(new SqlParameter("@customerUUID", SqlDbType.NVarChar, 100) { Value = queryObject.CustomerUUID });
            }
            if (queryObject.Status.HasValue)
            {
                whereSqlBuilder.Append(" AND status = @status ");
                sqlParameterList.Add(new SqlParameter("@status ", SqlDbType.TinyInt, 1) { Value = queryObject.Status });
            }
            if (queryObject.PreOrderSum.HasValue)
            {
                whereSqlBuilder.Append(" AND preOrderSum = @preOrderSum ");
                sqlParameterList.Add(new SqlParameter("@preOrderSum ", SqlDbType.Float, 8) { Value = queryObject.PreOrderSum });
            } 
            if (queryObject.PreOrderTime.HasValue)
            {
                whereSqlBuilder.Append(" AND preOrderTime = @preOrderTime ");
                sqlParameterList.Add(new SqlParameter("@preOrderTime ", SqlDbType.DateTime, 8) { Value = queryObject.PreOrderTime });
            }
            if (queryObject.IsShopQueried.HasValue)
            {
                whereSqlBuilder.Append(" AND isShopQueried = @isShopQueried ");
                sqlParameterList.Add(new SqlParameter("@isShopQueried ", SqlDbType.TinyInt, 1) { Value = queryObject.IsShopQueried });
            }
            if (queryObject.IsWeiboShared.HasValue)
            {
                whereSqlBuilder.Append(" AND isWeiboShared = @isWeiboShared ");
                sqlParameterList.Add(new SqlParameter("@isWeiboShared ", SqlDbType.TinyInt, 1) { Value = queryObject.IsWeiboShared });
            }
            if (queryObject.IsShopVerified.HasValue)
            {
                whereSqlBuilder.Append(" AND isShopVerified = @isShopVerified ");
                sqlParameterList.Add(new SqlParameter("@isShopVerified ", SqlDbType.TinyInt, 1) { Value = queryObject.IsShopVerified });
            }
            if (queryObject.IsPaid.HasValue)
            {
                whereSqlBuilder.Append(" AND isPaid = @isPaid ");
                sqlParameterList.Add(new SqlParameter("@isPaid ", SqlDbType.TinyInt, 1) { Value = queryObject.IsPaid });
            }
            if (queryObject.PrePaidSum.HasValue)
            {
                whereSqlBuilder.Append(" AND prePaidSum = @prePaidSum ");
                sqlParameterList.Add(new SqlParameter("@prePaidSum ", SqlDbType.Float, 8) { Value = queryObject.PrePaidSum });
            }
            if (queryObject.PrePayPrivilegeId.HasValue)
            {
                whereSqlBuilder.Append(" AND prePayPrivilegeId = @prePayPrivilegeId ");
                sqlParameterList.Add(new SqlParameter("@prePayPrivilegeId ", SqlDbType.Int, 4) { Value = queryObject.PrePayPrivilegeId });
            }
            if (!string.IsNullOrEmpty(queryObject.PrePayPrivilegeStr))
            {
                whereSqlBuilder.Append(" AND prePayPrivilegeStr = @prePayPrivilegeStr ");
                sqlParameterList.Add(new SqlParameter("@prePayPrivilegeStr", SqlDbType.NVarChar, 200) { Value = queryObject.PrePayPrivilegeStr });
            }
            if (queryObject.NumberOfHoursAhead.HasValue)
            {
                whereSqlBuilder.Append(" AND numberOfHoursAhead = @numberOfHoursAhead ");
                sqlParameterList.Add(new SqlParameter("@numberOfHoursAhead ", SqlDbType.Float, 8) { Value = queryObject.NumberOfHoursAhead });
            }
            if (queryObject.PrePayTime.HasValue)
            {
                whereSqlBuilder.Append(" AND prePayTime = @prePayTime ");
                sqlParameterList.Add(new SqlParameter("@prePayTime ", SqlDbType.DateTime, 8) { Value = queryObject.PrePayTime });
            }
            if (queryObject.PrePayCashBack.HasValue)
            {
                whereSqlBuilder.Append(" AND prePayCashBack = @prePayCashBack ");
                sqlParameterList.Add(new SqlParameter("@prePayCashBack ", SqlDbType.Float, 8) { Value = queryObject.PrePayCashBack });
            }
            if (queryObject.ValidFromDate.HasValue)
            {
                whereSqlBuilder.Append(" AND validFromDate = @validFromDate ");
                sqlParameterList.Add(new SqlParameter("@validFromDate ", SqlDbType.DateTime, 8) { Value = queryObject.ValidFromDate });
            }
            if (queryObject.ViewallocCommission.HasValue)
            {
                whereSqlBuilder.Append(" AND viewallocCommission = @viewallocCommission ");
                sqlParameterList.Add(new SqlParameter("@viewallocCommission ", SqlDbType.Float, 8) { Value = queryObject.ViewallocCommission });
            }
            if (queryObject.TransactionFee.HasValue)
            {
                whereSqlBuilder.Append(" AND transactionFee = @transactionFee ");
                sqlParameterList.Add(new SqlParameter("@transactionFee ", SqlDbType.Float, 8) { Value = queryObject.TransactionFee });
            }
            if (queryObject.ViewallocNeedsToPayToShop.HasValue)
            {
                whereSqlBuilder.Append(" AND viewallocNeedsToPayToShop = @viewallocNeedsToPayToShop ");
                sqlParameterList.Add(new SqlParameter("@viewallocNeedsToPayToShop ", SqlDbType.Float, 8) { Value = queryObject.ViewallocNeedsToPayToShop });
            }
            if (queryObject.ViewallocPaidToShopSum.HasValue)
            {
                whereSqlBuilder.Append(" AND viewallocPaidToShopSum = @viewallocPaidToShopSum ");
                sqlParameterList.Add(new SqlParameter("@viewallocPaidToShopSum ", SqlDbType.Float, 8) { Value = queryObject.ViewallocPaidToShopSum });
            }
            if (queryObject.ViewallocTransactionCompleted.HasValue)
            {
                whereSqlBuilder.Append(" AND viewallocTransactionCompleted = @viewallocTransactionCompleted ");
                sqlParameterList.Add(new SqlParameter("@viewallocTransactionCompleted ", SqlDbType.TinyInt, 1) { Value = queryObject.ViewallocTransactionCompleted });
            }
            if (queryObject.CashbackReceived.HasValue)
            {
                whereSqlBuilder.Append(" AND cashbackReceived = @cashbackReceived ");
                sqlParameterList.Add(new SqlParameter("@cashbackReceived ", SqlDbType.TinyInt, 1) { Value = queryObject.CashbackReceived });
            }
            if (!string.IsNullOrEmpty(queryObject.VerificationCode))
            {
                whereSqlBuilder.Append(" AND verificationCode = @verificationCode ");
                sqlParameterList.Add(new SqlParameter("@verificationCode", SqlDbType.NVarChar, 50) { Value = queryObject.VerificationCode });
            }
            if (queryObject.RefundDeadline.HasValue)
            {
                whereSqlBuilder.Append(" AND refundDeadline = @refundDeadline ");
                sqlParameterList.Add(new SqlParameter("@refundDeadline ", SqlDbType.DateTime, 8) { Value = queryObject.RefundDeadline });
            }
            if (queryObject.IsApproved.HasValue)
            {
                whereSqlBuilder.Append(" AND isApproved = @isApproved ");
                sqlParameterList.Add(new SqlParameter("@isApproved ", SqlDbType.TinyInt, 1) { Value = queryObject.IsApproved });
            }
            if (queryObject.IsApplied.HasValue)
            {
                whereSqlBuilder.Append(" AND isApplied = @isApplied ");
                sqlParameterList.Add(new SqlParameter("@isApplied ", SqlDbType.TinyInt, 1) { Value = queryObject.IsApplied });
            }
            if (queryObject.PreorderSupportCount.HasValue)
            {
                whereSqlBuilder.Append(" AND preorderSupportCount = @preorderSupportCount ");
                sqlParameterList.Add(new SqlParameter("@preorderSupportCount ", SqlDbType.Int, 4) { Value = queryObject.PreorderSupportCount });
            }
            if (!string.IsNullOrEmpty(queryObject.SnsMessageJson))
            {
                whereSqlBuilder.Append(" AND snsMessageJson = @snsMessageJson ");
                sqlParameterList.Add(new SqlParameter("@snsMessageJson", SqlDbType.NVarChar, -1) { Value = queryObject.SnsMessageJson });
            }
            if (!string.IsNullOrEmpty(queryObject.SnsShareImageUrl))
            {
                whereSqlBuilder.Append(" AND snsShareImageUrl = @snsShareImageUrl ");
                sqlParameterList.Add(new SqlParameter("@snsShareImageUrl", SqlDbType.NVarChar, 500) { Value = queryObject.SnsShareImageUrl });
            }
            if (queryObject.VerifiedSaving.HasValue)
            {
                whereSqlBuilder.Append(" AND verifiedSaving = @verifiedSaving ");
                sqlParameterList.Add(new SqlParameter("@verifiedSaving ", SqlDbType.Float, 8) { Value = queryObject.VerifiedSaving });
            }
            if (queryObject.IsShopConfirmed.HasValue)
            {
                whereSqlBuilder.Append(" AND isShopConfirmed = @isShopConfirmed ");
                sqlParameterList.Add(new SqlParameter("@isShopConfirmed ", SqlDbType.TinyInt, 1) { Value = queryObject.IsShopConfirmed });
            }
            if (!string.IsNullOrEmpty(queryObject.InvoiceTitle))
            {
                whereSqlBuilder.Append(" AND invoiceTitle = @invoiceTitle ");
                sqlParameterList.Add(new SqlParameter("@invoiceTitle", SqlDbType.NVarChar, 50) { Value = queryObject.InvoiceTitle });
            }
            if (!string.IsNullOrEmpty(queryObject.RemoteOrder))
            {
                whereSqlBuilder.Append(" AND remoteOrder = @remoteOrder ");
                sqlParameterList.Add(new SqlParameter("@remoteOrder", SqlDbType.VarChar, 10) { Value = queryObject.RemoteOrder });
            }
            if (!string.IsNullOrEmpty(queryObject.SundryJson))
            {
                whereSqlBuilder.Append(" AND sundryJson = @sundryJson ");
                sqlParameterList.Add(new SqlParameter("@sundryJson", SqlDbType.NVarChar, -1) { Value = queryObject.SundryJson });
            }
            if (queryObject.RefundMoneySum.HasValue)
            {
                whereSqlBuilder.Append(" AND refundMoneySum = @refundMoneySum ");
                sqlParameterList.Add(new SqlParameter("@refundMoneySum ", SqlDbType.Float, 8) { Value = queryObject.RefundMoneySum });
            }
            if (queryObject.Discount.HasValue)
            {
                whereSqlBuilder.Append(" AND discount = @discount ");
                sqlParameterList.Add(new SqlParameter("@discount ", SqlDbType.Float, 8) { Value = queryObject.Discount });
            }
            if (queryObject.RefundMoneyClosedSum.HasValue)
            {
                whereSqlBuilder.Append(" AND refundMoneyClosedSum = @refundMoneyClosedSum ");
                sqlParameterList.Add(new SqlParameter("@refundMoneyClosedSum ", SqlDbType.Float, 8) { Value = queryObject.RefundMoneyClosedSum });
            }
            if (queryObject.EvaluationValue.HasValue)
            {
                whereSqlBuilder.Append(" AND evaluationValue = @evaluationValue ");
                sqlParameterList.Add(new SqlParameter("@evaluationValue ", SqlDbType.SmallInt, 2) { Value = queryObject.EvaluationValue });
            }
            if (!string.IsNullOrEmpty(queryObject.DeskNumber))
            {
                whereSqlBuilder.Append(" AND deskNumber = @deskNumber ");
                sqlParameterList.Add(new SqlParameter("@deskNumber", SqlDbType.NVarChar, 50) { Value = queryObject.DeskNumber });
            }
            if (queryObject.RefundRedEnvelope.HasValue)
            {
                whereSqlBuilder.Append(" AND refundRedEnvelope = @refundRedEnvelope ");
                sqlParameterList.Add(new SqlParameter("@refundRedEnvelope ", SqlDbType.Float, 8) { Value = queryObject.RefundRedEnvelope });
            }
            if (queryObject.AppType.HasValue)
            {
                whereSqlBuilder.Append(" AND appType = @appType ");
                sqlParameterList.Add(new SqlParameter("@appType ", SqlDbType.Int, 4) { Value = queryObject.AppType });
            }
            if (!string.IsNullOrEmpty(queryObject.AppBuild))
            {
                whereSqlBuilder.Append(" AND appBuild = @appBuild ");
                sqlParameterList.Add(new SqlParameter("@appBuild", SqlDbType.NVarChar, 100) { Value = queryObject.AppBuild });
            }
            if (!string.IsNullOrEmpty(queryObject.EvaluationContent))
            {
                whereSqlBuilder.Append(" AND evaluationContent = @evaluationContent ");
                sqlParameterList.Add(new SqlParameter("@evaluationContent", SqlDbType.NVarChar, 400) { Value = queryObject.EvaluationContent });
            }
            if (queryObject.EvaluationTime.HasValue)
            {
                whereSqlBuilder.Append(" AND evaluationTime = @evaluationTime ");
                sqlParameterList.Add(new SqlParameter("@evaluationTime ", SqlDbType.DateTime, 8) { Value = queryObject.EvaluationTime });
            }
            if (queryObject.EvaluationTimeFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND evaluationTime >= @evaluationTimeFrom ");
                sqlParameterList.Add(new SqlParameter("@evaluationTimeFrom", SqlDbType.DateTime, 8) { Value = queryObject.EvaluationTimeFrom });
            }
            if (queryObject.EvaluationTimeTo.HasValue)
            {
                whereSqlBuilder.Append(" AND evaluationTime <= @evaluationTimeTo ");
                sqlParameterList.Add(new SqlParameter("@evaluationTimeTo", SqlDbType.DateTime, 8) { Value = queryObject.EvaluationTimeTo });
            }
            if (queryObject.IsEvaluation.HasValue)
            {
                whereSqlBuilder.Append(" AND isEvaluation = @isEvaluation ");
                sqlParameterList.Add(new SqlParameter("@isEvaluation ", SqlDbType.TinyInt, 1) { Value = queryObject.IsEvaluation });
            }
            if (queryObject.EvaluationLevel.HasValue)
            {
                whereSqlBuilder.Append(" AND evaluationLevel = @evaluationLevel ");
                sqlParameterList.Add(new SqlParameter("@evaluationLevel ", SqlDbType.Int, 4) { Value = queryObject.EvaluationLevel });
            }
            if (queryObject.ExpireTime.HasValue)
            {
                whereSqlBuilder.Append(" AND expireTime = @expireTime ");
                sqlParameterList.Add(new SqlParameter("@expireTime ", SqlDbType.DateTime, 8) { Value = queryObject.ExpireTime });
            }
            if (!string.IsNullOrEmpty(queryObject.UserName))
            {
                whereSqlBuilder.Append(" AND UserName = @UserName ");
                sqlParameterList.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 100) { Value = queryObject.UserName });
            }
            if (!string.IsNullOrEmpty(queryObject.UserNameFuzzy))
            {
                whereSqlBuilder.Append(" AND UserName LIKE @UserNameFuzzy ");
                sqlParameterList.Add(new SqlParameter("@UserNameFuzzy", SqlDbType.NVarChar, 100) { Value = string.Format("%{0}%", queryObject.UserNameFuzzy) });
            }
            if (!string.IsNullOrEmpty(queryObject.MobilePhoneNumber))
            {
                whereSqlBuilder.Append(" AND mobilePhoneNumber = @mobilePhoneNumber ");
                sqlParameterList.Add(new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = queryObject.MobilePhoneNumber });
            }
            if (!string.IsNullOrEmpty(queryObject.MobilePhoneNumberFuzzy))
            {
                whereSqlBuilder.Append(" AND mobilePhoneNumber LIKE @mobilePhoneNumberFuzzy ");
                sqlParameterList.Add(new SqlParameter("@mobilePhoneNumberFuzzy", SqlDbType.NVarChar, 50) { Value = string.Format("%{0}%", queryObject.MobilePhoneNumberFuzzy) });
            }
            if (!string.IsNullOrEmpty(queryObject.ShopName))
            {
                whereSqlBuilder.Append(" AND shopName = @shopName ");
                sqlParameterList.Add(new SqlParameter("@shopName", SqlDbType.NVarChar, 500) { Value = queryObject.ShopName });
            }
            if (!string.IsNullOrEmpty(queryObject.ShopNameFuzzy))
            {
                whereSqlBuilder.Append(" AND shopName LIKE @shopNameFuzzy ");
                sqlParameterList.Add(new SqlParameter("@shopNameFuzzy", SqlDbType.NVarChar, 500) { Value = string.Format("%{0}%", queryObject.ShopNameFuzzy) });
            }
            if (queryObject.PreOrderTime.HasValue)
            {
                whereSqlBuilder.Append(" AND PreOrderTime = @PreOrderTime ");
                sqlParameterList.Add(new SqlParameter("@PreOrderTime ", SqlDbType.DateTime, 8) { Value = queryObject.PreOrderTime });
            }
            if (queryObject.PreOrderTimeFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND PreOrderTime >= @PreOrderTimeFrom ");
                sqlParameterList.Add(new SqlParameter("@PreOrderTimeFrom ", SqlDbType.DateTime, 8) { Value = queryObject.PreOrderTimeFrom });
            }
            if (queryObject.PreOrderTimeTo.HasValue)
            {
                whereSqlBuilder.Append(" AND PreOrderTime <= @PreOrderTimeTo ");
                sqlParameterList.Add(new SqlParameter("@PreOrderTimeTo ", SqlDbType.DateTime, 8) { Value = queryObject.PreOrderTimeTo });
            }
            if (queryObject.PrePayTime.HasValue)
            {
                whereSqlBuilder.Append(" AND PrePayTime = @PrePayTime ");
                sqlParameterList.Add(new SqlParameter("@PrePayTime ", SqlDbType.DateTime, 8) { Value = queryObject.PrePayTimeTo });
            }
            if (queryObject.PrePayTimeFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND PrePayTime >= @PrePayTimeFrom ");
                sqlParameterList.Add(new SqlParameter("@PrePayTimeFrom ", SqlDbType.DateTime, 8) { Value = queryObject.PrePayTimeFrom });
            }
            if (queryObject.PrePayTimeTo.HasValue)
            {
                whereSqlBuilder.Append(" AND PrePayTime <= @PrePayTimeTo ");
                sqlParameterList.Add(new SqlParameter("@PrePayTimeTo ", SqlDbType.DateTime, 8) { Value = queryObject.PrePayTimeTo });
            }
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@PageSize", pageSize));
                sqlParameterList.Add(new SqlParameter("@PageIndex", pageSize * (pageIndex - 1)));
            }
            sqlParameters = sqlParameterList.ToArray();
        }

        private static StringBuilder GetOrderSql(PreOrder19dianVOrderColumn orderColumn, SortOrder order)
        {
            StringBuilder orderSqlBuilder = new StringBuilder(" ORDER BY ");
            switch (orderColumn)
            {
                case PreOrder19dianVOrderColumn.PreOrder19dianId:
                    orderSqlBuilder.Append("preOrder19dianId");
                    break;
                case PreOrder19dianVOrderColumn.ECardNumber:
                    orderSqlBuilder.Append("eCardNumber");
                    break;
                case PreOrder19dianVOrderColumn.MenuId:
                    orderSqlBuilder.Append("menuId");
                    break;
                case PreOrder19dianVOrderColumn.CompanyId:
                    orderSqlBuilder.Append("companyId");
                    break;
                case PreOrder19dianVOrderColumn.ShopId:
                    orderSqlBuilder.Append("shopId");
                    break;
                case PreOrder19dianVOrderColumn.CustomerId:
                    orderSqlBuilder.Append("customerId");
                    break;
                case PreOrder19dianVOrderColumn.OrderInJson:
                    orderSqlBuilder.Append("orderInJson");
                    break;
                case PreOrder19dianVOrderColumn.CustomerCookie:
                    orderSqlBuilder.Append("customerCookie");
                    break;
                case PreOrder19dianVOrderColumn.CustomerUUID:
                    orderSqlBuilder.Append("customerUUID");
                    break;
                case PreOrder19dianVOrderColumn.Status:
                    orderSqlBuilder.Append("status");
                    break;
                case PreOrder19dianVOrderColumn.PreOrderSum:
                    orderSqlBuilder.Append("preOrderSum");
                    break;
                case PreOrder19dianVOrderColumn.PreOrderServerSum:
                    orderSqlBuilder.Append("preOrderServerSum");
                    break;
                case PreOrder19dianVOrderColumn.PreOrderTime:
                    orderSqlBuilder.Append("preOrderTime");
                    break;
                case PreOrder19dianVOrderColumn.IsShopQueried:
                    orderSqlBuilder.Append("isShopQueried");
                    break;
                case PreOrder19dianVOrderColumn.IsWeiboShared:
                    orderSqlBuilder.Append("isWeiboShared");
                    break;
                case PreOrder19dianVOrderColumn.IsShopVerified:
                    orderSqlBuilder.Append("isShopVerified");
                    break;
                case PreOrder19dianVOrderColumn.IsPaid:
                    orderSqlBuilder.Append("isPaid");
                    break;
                case PreOrder19dianVOrderColumn.PrePaidSum:
                    orderSqlBuilder.Append("prePaidSum");
                    break;
                case PreOrder19dianVOrderColumn.PrePayPrivilegeId:
                    orderSqlBuilder.Append("prePayPrivilegeId");
                    break;
                case PreOrder19dianVOrderColumn.PrePayPrivilegeStr:
                    orderSqlBuilder.Append("prePayPrivilegeStr");
                    break;
                case PreOrder19dianVOrderColumn.NumberOfHoursAhead:
                    orderSqlBuilder.Append("numberOfHoursAhead");
                    break;
                case PreOrder19dianVOrderColumn.PrePayTime:
                    orderSqlBuilder.Append("prePayTime");
                    break;
                case PreOrder19dianVOrderColumn.PrePayCashBack:
                    orderSqlBuilder.Append("prePayCashBack");
                    break;
                case PreOrder19dianVOrderColumn.ValidFromDate:
                    orderSqlBuilder.Append("validFromDate");
                    break;
                case PreOrder19dianVOrderColumn.ViewallocCommission:
                    orderSqlBuilder.Append("viewallocCommission");
                    break;
                case PreOrder19dianVOrderColumn.TransactionFee:
                    orderSqlBuilder.Append("transactionFee");
                    break;
                case PreOrder19dianVOrderColumn.ViewallocNeedsToPayToShop:
                    orderSqlBuilder.Append("viewallocNeedsToPayToShop");
                    break;
                case PreOrder19dianVOrderColumn.ViewallocPaidToShopSum:
                    orderSqlBuilder.Append("viewallocPaidToShopSum");
                    break;
                case PreOrder19dianVOrderColumn.ViewallocTransactionCompleted:
                    orderSqlBuilder.Append("viewallocTransactionCompleted");
                    break;
                case PreOrder19dianVOrderColumn.CashbackReceived:
                    orderSqlBuilder.Append("cashbackReceived");
                    break;
                case PreOrder19dianVOrderColumn.VerificationCode:
                    orderSqlBuilder.Append("verificationCode");
                    break;
                case PreOrder19dianVOrderColumn.RefundDeadline:
                    orderSqlBuilder.Append("refundDeadline");
                    break;
                case PreOrder19dianVOrderColumn.IsApproved:
                    orderSqlBuilder.Append("isApproved");
                    break;
                case PreOrder19dianVOrderColumn.IsApplied:
                    orderSqlBuilder.Append("isApplied");
                    break;
                case PreOrder19dianVOrderColumn.PreorderSupportCount:
                    orderSqlBuilder.Append("preorderSupportCount");
                    break;
                case PreOrder19dianVOrderColumn.SnsMessageJson:
                    orderSqlBuilder.Append("snsMessageJson");
                    break;
                case PreOrder19dianVOrderColumn.SnsShareImageUrl:
                    orderSqlBuilder.Append("snsShareImageUrl");
                    break;
                case PreOrder19dianVOrderColumn.VerifiedSaving:
                    orderSqlBuilder.Append("verifiedSaving");
                    break;
                case PreOrder19dianVOrderColumn.IsShopConfirmed:
                    orderSqlBuilder.Append("isShopConfirmed");
                    break;
                case PreOrder19dianVOrderColumn.InvoiceTitle:
                    orderSqlBuilder.Append("invoiceTitle");
                    break;
                case PreOrder19dianVOrderColumn.RemoteOrder:
                    orderSqlBuilder.Append("remoteOrder");
                    break;
                case PreOrder19dianVOrderColumn.SundryJson:
                    orderSqlBuilder.Append("sundryJson");
                    break;
                case PreOrder19dianVOrderColumn.RefundMoneySum:
                    orderSqlBuilder.Append("refundMoneySum");
                    break;
                case PreOrder19dianVOrderColumn.Discount:
                    orderSqlBuilder.Append("discount");
                    break;
                case PreOrder19dianVOrderColumn.RefundMoneyClosedSum:
                    orderSqlBuilder.Append("refundMoneyClosedSum");
                    break;
                case PreOrder19dianVOrderColumn.EvaluationValue:
                    orderSqlBuilder.Append("evaluationValue");
                    break;
                case PreOrder19dianVOrderColumn.DeskNumber:
                    orderSqlBuilder.Append("deskNumber");
                    break;
                case PreOrder19dianVOrderColumn.RefundRedEnvelope:
                    orderSqlBuilder.Append("refundRedEnvelope");
                    break;
                case PreOrder19dianVOrderColumn.AppType:
                    orderSqlBuilder.Append("appType");
                    break;
                case PreOrder19dianVOrderColumn.AppBuild:
                    orderSqlBuilder.Append("appBuild");
                    break;
                case PreOrder19dianVOrderColumn.EvaluationContent:
                    orderSqlBuilder.Append("evaluationContent");
                    break;
                case PreOrder19dianVOrderColumn.EvaluationTime:
                    orderSqlBuilder.Append("evaluationTime");
                    break;
                case PreOrder19dianVOrderColumn.IsEvaluation:
                    orderSqlBuilder.Append("isEvaluation");
                    break;
                case PreOrder19dianVOrderColumn.EvaluationLevel:
                    orderSqlBuilder.Append("evaluationLevel");
                    break;
                case PreOrder19dianVOrderColumn.ExpireTime:
                    orderSqlBuilder.Append("expireTime");
                    break;
                case PreOrder19dianVOrderColumn.UserName:
                    orderSqlBuilder.Append("UserName");
                    break;
                case PreOrder19dianVOrderColumn.MobilePhoneNumber:
                    orderSqlBuilder.Append("mobilePhoneNumber");
                    break;
                case PreOrder19dianVOrderColumn.ShopName:
                    orderSqlBuilder.Append("shopName");
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
