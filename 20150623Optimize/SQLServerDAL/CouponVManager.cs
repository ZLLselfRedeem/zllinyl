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
    public partial class CouponVManager
    {
        public bool IsExists(int CouponId)
        {
            string sql = "SELECT COUNT(0) FROM CouponV WHERE CouponId = @CouponId";
            var retutnValue =
                SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@CouponId",SqlDbType.Int,4) { Value = CouponId });
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
        
        public CouponV GetEntityById(int CouponId)
        {
            string sql  = @"SELECT [CouponId]
                                ,[CouponName]
                                ,[ValidityPeriod]
                                ,[StartDate]
                                ,[EndDate]
                                ,[SheetNumber]
                                ,[SendCount]
                                ,[ShopId]
                                ,[RequirementMoney]
                                ,[SortOrder]
                                ,[DeductibleAmount]
                                ,[State]
                                ,[CreatedBy]
                                ,[CreateTime]
                                ,[LastUpdatedBy]
                                ,[LastUpdatedTime]
                                ,[Remark]
                                ,[shopName]
                                ,[cityID]
                                ,[countyID]
                                ,[companyID]
                                ,[RefuseReason]
                                ,[cityName]
                                ,[AuditEmployee]
                                ,[AuditTime]
                                ,[shopStatus]
                                ,[isHandle]
                                ,[DeductibleProportion]
                                ,[longitude]
                                ,[latitude]
                                ,[ShopLogo]
                                ,[ShopImagePath]
                                ,[ShopAddress],PublicityPhotoPath
                            FROM  [CouponV] 
                            WHERE [CouponId] = @CouponId ";
             using(var reader = 
                    SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@CouponId", SqlDbType.Int, 4){ Value = CouponId }))
            {
                if(reader.Read())
                {
                  var entity = reader.GetEntity<CouponV>();
                  return entity;
                }
            }
            return null;
        }
        
        public long GetCountByQuery(CouponVQueryObject queryObject = null)
        {
            string sql = @"SELECT COUNT(0)
                            FROM  [CouponV] 
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

        public List<ICouponV> GetListByQuery(int pageSize, int pageIndex, CouponVQueryObject queryObject = null, CouponVOrderColumn orderColumn = CouponVOrderColumn.CouponId, SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql  = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[CouponId]
                                ,[CouponName]
                                ,[ValidityPeriod]
                                ,[StartDate]
                                ,[EndDate]
                                ,[SheetNumber]
                                ,[SendCount]
                                ,[ShopId]
                                ,[RequirementMoney]
                                ,[SortOrder]
                                ,[DeductibleAmount]
                                ,[State]
                                ,[CreatedBy]
                                ,[CreateTime]
                                ,[LastUpdatedBy]
                                ,[LastUpdatedTime]
                                ,[Remark]
                                ,[shopName]
                                ,[cityID]
                                ,[countyID]
                                ,[companyID]
                                ,[RefuseReason]
                                ,[cityName]
                                ,[AuditEmployee]
                                ,[AuditTime]
                                ,[shopStatus]
                                ,[isHandle]
                                ,[DeductibleProportion]
                                ,[longitude]
                                ,[latitude]
                                ,[ShopLogo]
                                ,[ShopImagePath]
                                ,[ShopAddress],PublicityPhotoPath
                            FROM  [CouponV] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject,ref sqlParameters, whereSqlBuilder,pageSize,pageIndex);
            sql = whereSqlBuilder.Insert(0, sql).ToString(); 
            sql =  string.Format( " SELECT TOP (@PageSize) * FROM ({0}) T WHERE T.ROWNUM > @PageIndex ",sql);
            List<ICouponV> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<ICouponV>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<CouponV>());
                }
            }
            return list;
        }
        
        public List<ICouponV> GetListByQuery(CouponVQueryObject queryObject = null,CouponVOrderColumn orderColumn = CouponVOrderColumn.CouponId,SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql  = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[CouponId]
                                ,[CouponName]
                                ,[ValidityPeriod]
                                ,[StartDate]
                                ,[EndDate]
                                ,[SheetNumber]
                                ,[SendCount]
                                ,[ShopId]
                                ,[RequirementMoney]
                                ,[SortOrder]
                                ,[DeductibleAmount]
                                ,[State]
                                ,[CreatedBy]
                                ,[CreateTime]
                                ,[LastUpdatedBy]
                                ,[LastUpdatedTime]
                                ,[Remark]
                                ,[shopName]
                                ,[cityID]
                                ,[countyID]
                                ,[companyID]
                                ,[RefuseReason]
                                ,[cityName]
                                ,[AuditEmployee]
                                ,[AuditTime]
                                ,[shopStatus]
                                ,[isHandle]
                                ,[DeductibleProportion]
                                ,[longitude]
                                ,[latitude]
                                ,[ShopLogo]
                                ,[ShopImagePath]
                                ,[ShopAddress],PublicityPhotoPath
                            FROM  [CouponV] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject,ref sqlParameters, whereSqlBuilder);
            sql = whereSqlBuilder.Insert(0, sql).ToString(); 
            List<ICouponV> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<ICouponV>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<CouponV>());
                }
            }
            return list;
        }
        private static void GetWhereSqlBuilderAndSqlParameterList(CouponVQueryObject queryObject, ref SqlParameter[] sqlParameters,
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
            if (!string.IsNullOrEmpty(queryObject.ShopName))
            {
                whereSqlBuilder.Append(" AND shopName = @shopName ");
                sqlParameterList.Add(new SqlParameter("@shopName", SqlDbType.NVarChar, 500) { Value = queryObject.ShopName });
            }
            if (queryObject.CityId.HasValue)
            {
                whereSqlBuilder.Append(" AND cityID = @cityID ");
                sqlParameterList.Add(new SqlParameter("@cityID ", SqlDbType.Int, 4) { Value = queryObject.CityId });
            }
            if (queryObject.IsGot.HasValue)
            {
                whereSqlBuilder.Append(" AND IsGot = @IsGot ");
                sqlParameterList.Add(new SqlParameter("@IsGot ", SqlDbType.Bit) { Value = queryObject.IsGot.Value });
            }
            if (queryObject.CountyId.HasValue)
            {
                whereSqlBuilder.Append(" AND countyID = @countyID ");
                sqlParameterList.Add(new SqlParameter("@countyID ", SqlDbType.Int, 4) { Value = queryObject.CountyId });
            }
            if (queryObject.CompanyId.HasValue)
            {
                whereSqlBuilder.Append(" AND companyID = @companyID ");
                sqlParameterList.Add(new SqlParameter("@companyID ", SqlDbType.Int, 4) { Value = queryObject.CompanyId });
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
            if (!string.IsNullOrEmpty(queryObject.CityName))
            {
                whereSqlBuilder.Append(" AND cityName = @cityName ");
                sqlParameterList.Add(new SqlParameter("@cityName", SqlDbType.NVarChar, 50) { Value = queryObject.CityName });
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
            if (queryObject.ShopStatus.HasValue)
            {
                whereSqlBuilder.Append(" AND shopStatus = @shopStatus ");
                sqlParameterList.Add(new SqlParameter("@shopStatus ", SqlDbType.Int, 4) { Value = queryObject.ShopStatus });
            }
            if (queryObject.IsHandle.HasValue)
            {
                whereSqlBuilder.Append(" AND isHandle = @isHandle ");
                sqlParameterList.Add(new SqlParameter("@isHandle ", SqlDbType.Int, 4) { Value = queryObject.IsHandle });
            }
            if (queryObject.DeductibleProportion.HasValue)
            {
                whereSqlBuilder.Append(" AND DeductibleProportion = @DeductibleProportion ");
                sqlParameterList.Add(new SqlParameter("@DeductibleProportion ", SqlDbType.Float, 8) { Value = queryObject.DeductibleProportion });
            }
            if (queryObject.Longitude.HasValue)
            {
                //whereSqlBuilder.Append(" AND longitude =  longitude ");
                sqlParameterList.Add(new SqlParameter("@longitude ", SqlDbType.Float, 8) { Value = queryObject.Longitude });
            }
            if (queryObject.Latitude.HasValue)
            {
                //whereSqlBuilder.Append(" AND latitude =  latitude ");
                sqlParameterList.Add(new SqlParameter("@latitude ", SqlDbType.Float, 8) { Value = queryObject.Latitude });
            }
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@PageSize", pageSize));
                sqlParameterList.Add(new SqlParameter("@PageIndex", pageSize * (pageIndex - 1)));
            }
            sqlParameters = sqlParameterList.ToArray();
        }
        
        private static StringBuilder GetOrderSql(CouponVOrderColumn orderColumn, SortOrder order)
        {
            StringBuilder orderSqlBuilder = new StringBuilder(" ORDER BY ");
            switch (orderColumn)
            {
                case CouponVOrderColumn.CouponId:
                    orderSqlBuilder.Append("CouponId");
                    break;
                case CouponVOrderColumn.CouponName:
                    orderSqlBuilder.Append("CouponName");
                    break;
                case CouponVOrderColumn.ValidityPeriod:
                    orderSqlBuilder.Append("ValidityPeriod");
                    break;
                case CouponVOrderColumn.StartDate:
                    orderSqlBuilder.Append("StartDate");
                    break;
                case CouponVOrderColumn.EndDate:
                    orderSqlBuilder.Append("EndDate");
                    break;
                case CouponVOrderColumn.SheetNumber:
                    orderSqlBuilder.Append("SheetNumber");
                    break;
                case CouponVOrderColumn.SendCount:
                    orderSqlBuilder.Append("SendCount");
                    break;
                case CouponVOrderColumn.ShopId:
                    orderSqlBuilder.Append("ShopId");
                    break;
                case CouponVOrderColumn.RequirementMoney:
                    orderSqlBuilder.Append("RequirementMoney");
                    break;
                case CouponVOrderColumn.SortOrder:
                    orderSqlBuilder.Append("SortOrder");
                    break;
                case CouponVOrderColumn.DeductibleAmount:
                    orderSqlBuilder.Append("DeductibleAmount");
                    break;
                case CouponVOrderColumn.State:
                    orderSqlBuilder.Append("State");
                    break;
                case CouponVOrderColumn.CreatedBy:
                    orderSqlBuilder.Append("CreatedBy");
                    break;
                case CouponVOrderColumn.CreateTime:
                    orderSqlBuilder.Append("CreateTime");
                    break;
                case CouponVOrderColumn.LastUpdatedBy:
                    orderSqlBuilder.Append("LastUpdatedBy");
                    break;
                case CouponVOrderColumn.LastUpdatedTime:
                    orderSqlBuilder.Append("LastUpdatedTime");
                    break;
                case CouponVOrderColumn.Remark:
                    orderSqlBuilder.Append("Remark");
                    break;
                case CouponVOrderColumn.ShopName:
                    orderSqlBuilder.Append("shopName");
                    break;
                case CouponVOrderColumn.CityId:
                    orderSqlBuilder.Append("cityID");
                    break;
                case CouponVOrderColumn.CountyId:
                    orderSqlBuilder.Append("countyID");
                    break;
                case CouponVOrderColumn.CompanyId:
                    orderSqlBuilder.Append("companyID");
                    break;
                case CouponVOrderColumn.RefuseReason:
                    orderSqlBuilder.Append("RefuseReason");
                    break;
                case CouponVOrderColumn.CityName:
                    orderSqlBuilder.Append("cityName");
                    break;
                case CouponVOrderColumn.AuditEmployee:
                    orderSqlBuilder.Append("AuditEmployee");
                    break;
                case CouponVOrderColumn.AuditTime:
                    orderSqlBuilder.Append("AuditTime");
                    break;
                case CouponVOrderColumn.ShopStatus:
                    orderSqlBuilder.Append("shopStatus");
                    break;
                case CouponVOrderColumn.IsHandle:
                    orderSqlBuilder.Append("isHandle");
                    break;
                case CouponVOrderColumn.DeductibleProportion:
                    orderSqlBuilder.Append("DeductibleProportion");
                    break;
                case CouponVOrderColumn.Longitude:
                    orderSqlBuilder.Append("longitude");
                    break;
                case CouponVOrderColumn.Latitude:
                    orderSqlBuilder.Append("latitude");
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