using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.Interface;
using VAGastronomistMobileApp.Model.QueryObject;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public partial  class CouponVManager
    {
        public List<ICouponV> GetListByDistanceOrder(int pageSize, int pageIndex, CouponVQueryObject queryObject)
        {
            if (queryObject.Longitude == null | queryObject.Latitude == null)
            {
                return null;
            }
            double longitude = queryObject.Longitude.Value;
            double latitude = queryObject.Latitude.Value;

            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlStringBuilder  = new StringBuilder(@"SELECT ROW_NUMBER() 
                                OVER( ORDER BY ( 6371 * ACOS( COS( RADIANS(@Latitude) ) * COS( RADIANS( Latitude )) * COS( RADIANS( Longitude ) 
                                    - RADIANS(@Longitude) ) + SIN( RADIANS(@Latitude) ) * SIN( RADIANS( Latitude ) ) ) ) ) AS ROWNUM
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
                                ,[ShopAddress] ,PublicityPhotoPath
                            FROM  [CouponV] 
                            WHERE 1 =1 ");;

            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlStringBuilder, pageSize, pageIndex);
            

            SqlParameter sqlParameter1 = new SqlParameter();
            SqlParameter sqlParameter2 = new SqlParameter();
            string sql = string.Format(" SELECT TOP (@PageSize) * FROM ({0}) T WHERE T.ROWNUM > @PageIndex ", whereSqlStringBuilder.ToString());

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
    }
}
