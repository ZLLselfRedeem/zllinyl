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
    public partial class ShopReportVManager
    {
        public bool IsExists(int ShopReportId)
        {
            string sql = "SELECT COUNT(0) FROM ShopReportV WHERE ShopReportId = @ShopReportId";
            var retutnValue =
                SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@ShopReportId", SqlDbType.Int, 4) { Value = ShopReportId });
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

        public ShopReportV GetEntityById(int ShopReportId)
        {
            string sql = @"SELECT [ShopReportId]
                                ,[UserName]
                                ,[mobilePhoneNumber]
                                ,[CustomId]
                                ,[ReportTime]
                                ,[ShopId]
                                ,[ReportValue]
                                ,[shopName]
                            FROM  [ShopReportV] 
                            WHERE [ShopReportId] = @ShopReportId ";
            using (var reader =
                   SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@ShopReportId", SqlDbType.Int, 4) { Value = ShopReportId }))
            {
                if (reader.Read())
                {
                    var entity = reader.GetEntity<ShopReportV>();
                    return entity;
                }
            }
            return null;
        }

        public long GetCountByQuery(ShopReportVQueryObject queryObject = null)
        {
            string sql = @"SELECT COUNT(0)
                            FROM  [ShopReportV] 
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

        public List<ShopReportV> GetListByQuery(int pageSize, int pageIndex, ShopReportVQueryObject queryObject = null, ShopReportVOrderColumn orderColumn = ShopReportVOrderColumn.ShopReportId, SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[UserName]
                                ,[mobilePhoneNumber]
                                ,[ShopReportId]
                                ,[CustomId]
                                ,[ReportTime]
                                ,[ShopId]
                                ,[ReportValue]
                                ,[shopName]
                            FROM  [ShopReportV] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder, pageSize, pageIndex);
            sql = whereSqlBuilder.Insert(0, sql).ToString();
            sql = string.Format(" SELECT TOP (@PageSize) * FROM ({0}) T WHERE T.ROWNUM > @PageIndex ", sql);
            List<ShopReportV> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<ShopReportV>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<ShopReportV>());
                }
            }
            return list;
        }

        public List<ShopReportV> GetListByQuery(ShopReportVQueryObject queryObject = null, ShopReportVOrderColumn orderColumn = ShopReportVOrderColumn.ShopReportId, SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[UserName]
                                ,[mobilePhoneNumber]
                                ,[ShopReportId]
                                ,[CustomId]
                                ,[ReportTime]
                                ,[ShopId]
                                ,[ReportValue]
                                ,[shopName]
                            FROM  [ShopReportV] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder);
            sql = whereSqlBuilder.Insert(0, sql).ToString();
            List<ShopReportV> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<ShopReportV>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<ShopReportV>());
                }
            }
            return list;
        }
        private static void GetWhereSqlBuilderAndSqlParameterList(ShopReportVQueryObject queryObject, ref SqlParameter[] sqlParameters,
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
            if (!string.IsNullOrEmpty(queryObject.UserName))
            {
                whereSqlBuilder.Append(" AND UserName = @UserName ");
                sqlParameterList.Add(new SqlParameter("@UserName", SqlDbType.NVarChar, 100) { Value = queryObject.UserName });
            }
            if (!string.IsNullOrEmpty(queryObject.MobilePhoneNumber))
            {
                whereSqlBuilder.Append(" AND mobilePhoneNumber = @mobilePhoneNumber ");
                sqlParameterList.Add(new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = queryObject.MobilePhoneNumber });
            }
            if (queryObject.ShopReportId.HasValue)
            {
                whereSqlBuilder.Append(" AND ShopReportId = @ShopReportId ");
                sqlParameterList.Add(new SqlParameter("@ShopReportId ", SqlDbType.Int, 4) { Value = queryObject.ShopReportId });
            }
            if (queryObject.CustomId.HasValue)
            {
                whereSqlBuilder.Append(" AND CustomId = @CustomId ");
                sqlParameterList.Add(new SqlParameter("@CustomId ", SqlDbType.Int, 4) { Value = queryObject.CustomId });
            }
            if (queryObject.ReportTime.HasValue)
            {
                whereSqlBuilder.Append(" AND ReportTime = @ReportTime ");
                sqlParameterList.Add(new SqlParameter("@ReportTime ", SqlDbType.DateTime, 8) { Value = queryObject.ReportTime });
            }
            if (queryObject.ShopId.HasValue)
            {
                whereSqlBuilder.Append(" AND ShopId = @ShopId ");
                sqlParameterList.Add(new SqlParameter("@ShopId ", SqlDbType.Int, 4) { Value = queryObject.ShopId });
            }
            if (queryObject.ReportValue.HasValue)
            {
                whereSqlBuilder.Append(" AND ReportValue = @ReportValue ");
                sqlParameterList.Add(new SqlParameter("@ReportValue ", SqlDbType.Int, 4) { Value = queryObject.ReportValue });
            }
            if (!string.IsNullOrEmpty(queryObject.ShopName))
            {
                whereSqlBuilder.Append(" AND shopName = @shopName ");
                sqlParameterList.Add(new SqlParameter("@shopName", SqlDbType.NVarChar, 500) { Value = queryObject.ShopName });
            }
            if (!string.IsNullOrEmpty(queryObject.ShopNameFuzzy))
            {
                whereSqlBuilder.Append(" AND shopName LIKE @ShopNameFuzzy ");
                sqlParameterList.Add(new SqlParameter("@ShopNameFuzzy", SqlDbType.NVarChar, 500) { Value = string.Format("%{0}%",queryObject.ShopNameFuzzy) });
            }
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@PageSize", pageSize));
                sqlParameterList.Add(new SqlParameter("@PageIndex", pageSize * (pageIndex - 1)));
            }
            sqlParameters = sqlParameterList.ToArray();
        }

        private static StringBuilder GetOrderSql(ShopReportVOrderColumn orderColumn, SortOrder order)
        {
            StringBuilder orderSqlBuilder = new StringBuilder(" ORDER BY ");
            switch (orderColumn)
            {
                case ShopReportVOrderColumn.UserName:
                    orderSqlBuilder.Append("UserName");
                    break;
                case ShopReportVOrderColumn.MobilePhoneNumber:
                    orderSqlBuilder.Append("mobilePhoneNumber");
                    break;
                case ShopReportVOrderColumn.ShopReportId:
                    orderSqlBuilder.Append("ShopReportId");
                    break;
                case ShopReportVOrderColumn.CustomId:
                    orderSqlBuilder.Append("CustomId");
                    break;
                case ShopReportVOrderColumn.ReportTime:
                    orderSqlBuilder.Append("ReportTime");
                    break;
                case ShopReportVOrderColumn.ShopId:
                    orderSqlBuilder.Append("ShopId");
                    break;
                case ShopReportVOrderColumn.ReportValue:
                    orderSqlBuilder.Append("ReportValue");
                    break;
                case ShopReportVOrderColumn.ShopName:
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
