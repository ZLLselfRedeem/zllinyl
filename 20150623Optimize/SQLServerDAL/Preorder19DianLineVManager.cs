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
    public partial class Preorder19DianLineVManager
    {
        public bool IsExists(long Preorder19DianLineId)
        {
            string sql = "SELECT COUNT(0) FROM Preorder19DianLineV WHERE Preorder19DianLineId = @Preorder19DianLineId";
            var retutnValue =
                SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@Preorder19DianLineId",SqlDbType.Int,8) { Value = Preorder19DianLineId });
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
        
        public Preorder19DianLineV GetEntityById(long Preorder19DianLineId)
        {
            string sql  = @"SELECT [Preorder19DianLineId]
                                ,[Preorder19DianId]
                                ,[CustomerId]
                                ,[PayType]
                                ,[PayAccount]
                                ,[Amount]
                                ,[CreateTime]
                                ,[Remark]
                                ,[State]
                                ,[Uuid]
                                ,[RefundAmount]
                                ,[preOrderTime]
                                ,[status]
                                ,[cityID] 
                                ,[ShopID] 
                            FROM  [Preorder19DianLineV] 
                            WHERE [Preorder19DianLineId] = @Preorder19DianLineId ";
             using(var reader = 
                    SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@Preorder19DianLineId", SqlDbType.BigInt, 8){ Value = Preorder19DianLineId }))
            {
                if(reader.Read())
                {
                  var entity = reader.GetEntity<Preorder19DianLineV>();
                  return entity;
                }
            }
            return null;
        }
        
        public long GetCountByQuery(Preorder19DianLineVQueryObject queryObject = null)
        {
            string sql = @"SELECT COUNT(0)
                            FROM  [Preorder19DianLineV] 
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

        public List<IPreorder19DianLineV> GetListByQuery(int pageSize,int pageIndex,Preorder19DianLineVQueryObject queryObject = null,Preorder19DianLineVOrderColumn orderColumn = Preorder19DianLineVOrderColumn.Preorder19DianLineId,SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql  = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[Preorder19DianLineId]
                                ,[Preorder19DianId]
                                ,[CustomerId]
                                ,[PayType]
                                ,[PayAccount]
                                ,[Amount]
                                ,[CreateTime]
                                ,[Remark]
                                ,[State]
                                ,[Uuid]
                                ,[RefundAmount]
                                ,[preOrderTime]
                                ,[status]
                                ,[cityID]
                                ,[ShopID] 
                            FROM  [Preorder19DianLineV] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject,ref sqlParameters, whereSqlBuilder,pageSize,pageIndex);
            sql = whereSqlBuilder.Insert(0, sql).ToString(); 
            sql =  string.Format( " SELECT TOP (@PageSize) * FROM ({0}) T WHERE T.ROWNUM > @PageIndex ",sql);
            List<IPreorder19DianLineV> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<IPreorder19DianLineV>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<Preorder19DianLineV>());
                }
            }
            return list;
        }
        
        public List<IPreorder19DianLineV> GetListByQuery(Preorder19DianLineVQueryObject queryObject = null,Preorder19DianLineVOrderColumn orderColumn = Preorder19DianLineVOrderColumn.Preorder19DianLineId,SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql  = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
                                ,[Preorder19DianLineId]
                                ,[Preorder19DianId]
                                ,[CustomerId]
                                ,[PayType]
                                ,[PayAccount]
                                ,[Amount]
                                ,[CreateTime]
                                ,[Remark]
                                ,[State]
                                ,[Uuid]
                                ,[RefundAmount]
                                ,[preOrderTime]
                                ,[status]
                                ,[cityID]
                                ,[ShopID] 
                            FROM  [Preorder19DianLineV] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject,ref sqlParameters, whereSqlBuilder);
            sql = whereSqlBuilder.Insert(0, sql).ToString(); 
            List<IPreorder19DianLineV> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<IPreorder19DianLineV>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<Preorder19DianLineV>());
                }
            }
            return list;
        }
        private static void GetWhereSqlBuilderAndSqlParameterList(Preorder19DianLineVQueryObject queryObject, ref SqlParameter[] sqlParameters,
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
            if (queryObject.Preorder19DianLineId.HasValue)
            {
                whereSqlBuilder.Append(" AND Preorder19DianLineId = @Preorder19DianLineId ");
                sqlParameterList.Add(new SqlParameter("@Preorder19DianLineId ", SqlDbType.BigInt, 8) { Value = queryObject.Preorder19DianLineId });
            }
            if (queryObject.Preorder19DianId.HasValue)
            {
                whereSqlBuilder.Append(" AND Preorder19DianId = @Preorder19DianId ");
                sqlParameterList.Add(new SqlParameter("@Preorder19DianId ", SqlDbType.BigInt, 8) { Value = queryObject.Preorder19DianId });
            }
            if (queryObject.CustomerId.HasValue)
            {
                whereSqlBuilder.Append(" AND CustomerId = @CustomerId ");
                sqlParameterList.Add(new SqlParameter("@CustomerId ", SqlDbType.BigInt, 8) { Value = queryObject.CustomerId });
            }
            if (queryObject.PayType.HasValue)
            {
                whereSqlBuilder.Append(" AND PayType = @PayType ");
                sqlParameterList.Add(new SqlParameter("@PayType ", SqlDbType.Int, 4) { Value = queryObject.PayType });
            }
            if (!string.IsNullOrEmpty(queryObject.PayAccount))
            {
                whereSqlBuilder.Append(" AND PayAccount = @PayAccount ");
                sqlParameterList.Add(new SqlParameter("@PayAccount", SqlDbType.NChar, 200) { Value = queryObject.PayAccount });
            }
            if (!string.IsNullOrEmpty(queryObject.PayAccountFuzzy))
            {
                whereSqlBuilder.Append(" AND PayAccount LIKE @PayAccountFuzzy ");
                sqlParameterList.Add(new SqlParameter("@PayAccountFuzzy", SqlDbType.NChar, 200) { Value = string.Format("%{0}%", queryObject.PayAccountFuzzy) });
            }
            if (queryObject.Amount.HasValue)
            {
                whereSqlBuilder.Append(" AND Amount = @Amount ");
                sqlParameterList.Add(new SqlParameter("@Amount ", SqlDbType.Float, 8) { Value = queryObject.Amount });
            }
            if (queryObject.AmountFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND Amount >= @AmountFrom ");
                sqlParameterList.Add(new SqlParameter("@AmountFrom", SqlDbType.Float, 8) { Value = queryObject.AmountFrom });
            }
            if (queryObject.AmountTo.HasValue)
            {
                whereSqlBuilder.Append(" AND Amount <= @AmountTo ");
                sqlParameterList.Add(new SqlParameter("@AmountTo", SqlDbType.Float, 8) { Value = queryObject.AmountTo });
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
            if (!string.IsNullOrEmpty(queryObject.Remark))
            {
                whereSqlBuilder.Append(" AND Remark = @Remark ");
                sqlParameterList.Add(new SqlParameter("@Remark", SqlDbType.NChar, 400) { Value = queryObject.Remark });
            }
            if (!string.IsNullOrEmpty(queryObject.RemarkFuzzy))
            {
                whereSqlBuilder.Append(" AND Remark LIKE @RemarkFuzzy ");
                sqlParameterList.Add(new SqlParameter("@RemarkFuzzy", SqlDbType.NChar, 400) { Value = string.Format("%{0}%", queryObject.RemarkFuzzy) });
            }
            if (queryObject.State.HasValue)
            {
                whereSqlBuilder.Append(" AND State = @State ");
                sqlParameterList.Add(new SqlParameter("@State ", SqlDbType.Int, 4) { Value = queryObject.State });
            }
            if (!string.IsNullOrEmpty(queryObject.Uuid))
            {
                whereSqlBuilder.Append(" AND Uuid = @Uuid ");
                sqlParameterList.Add(new SqlParameter("@Uuid", SqlDbType.NChar, 200) { Value = queryObject.Uuid });
            }
            if (queryObject.RefundAmount.HasValue)
            {
                whereSqlBuilder.Append(" AND RefundAmount = @RefundAmount ");
                sqlParameterList.Add(new SqlParameter("@RefundAmount ", SqlDbType.Float, 8) { Value = queryObject.RefundAmount });
            }
            if (queryObject.PreOrderTime.HasValue)
            {
                whereSqlBuilder.Append(" AND preOrderTime = @preOrderTime ");
                sqlParameterList.Add(new SqlParameter("@preOrderTime ", SqlDbType.DateTime, 8) { Value = queryObject.PreOrderTime });
            }
            if (queryObject.PreOrderTimeFrom.HasValue)
            {
                whereSqlBuilder.Append(" AND preOrderTime >= @preOrderTimeFrom ");
                sqlParameterList.Add(new SqlParameter("@preOrderTimeFrom", SqlDbType.DateTime, 8) { Value = queryObject.PreOrderTimeFrom });
            }
            if (queryObject.PreOrderTimeTo.HasValue)
            {
                whereSqlBuilder.Append(" AND preOrderTime <= @preOrderTimeTo ");
                sqlParameterList.Add(new SqlParameter("@preOrderTimeTo", SqlDbType.DateTime, 8) { Value = queryObject.PreOrderTimeTo });
            }
            if (queryObject.Status.HasValue)
            {
                whereSqlBuilder.Append(" AND status = @status ");
                sqlParameterList.Add(new SqlParameter("@status ", SqlDbType.TinyInt, 1) { Value = queryObject.Status });
            }
            if (queryObject.CityID.HasValue)
            {
                whereSqlBuilder.Append(" AND cityID = @cityID ");
                sqlParameterList.Add(new SqlParameter("@cityID ", SqlDbType.Int, 4) { Value = queryObject.CityID });
            } 
            if (queryObject.ShopID.HasValue)
            {
                whereSqlBuilder.Append(" AND ShopID = @ShopID ");
                sqlParameterList.Add(new SqlParameter("@ShopID ", SqlDbType.Int, 4) { Value = queryObject.ShopID });
            } 
            if (pageIndex.HasValue && pageSize.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@PageSize", pageSize));
                sqlParameterList.Add(new SqlParameter("@PageIndex", pageSize * (pageIndex - 1)));
            }
            sqlParameters = sqlParameterList.ToArray();
        }
        
        private static StringBuilder GetOrderSql(Preorder19DianLineVOrderColumn orderColumn, SortOrder order)
        {
            StringBuilder orderSqlBuilder = new StringBuilder(" ORDER BY ");
            switch (orderColumn)
            {
                case Preorder19DianLineVOrderColumn.Preorder19DianLineId:
                    orderSqlBuilder.Append("Preorder19DianLineId");
                    break;
                case Preorder19DianLineVOrderColumn.Preorder19DianId:
                    orderSqlBuilder.Append("Preorder19DianId");
                    break;
                case Preorder19DianLineVOrderColumn.CustomerId:
                    orderSqlBuilder.Append("CustomerId");
                    break;
                case Preorder19DianLineVOrderColumn.PayType:
                    orderSqlBuilder.Append("PayType");
                    break;
                case Preorder19DianLineVOrderColumn.PayAccount:
                    orderSqlBuilder.Append("PayAccount");
                    break;
                case Preorder19DianLineVOrderColumn.Amount:
                    orderSqlBuilder.Append("Amount");
                    break;
                case Preorder19DianLineVOrderColumn.CreateTime:
                    orderSqlBuilder.Append("CreateTime");
                    break;
                case Preorder19DianLineVOrderColumn.Remark:
                    orderSqlBuilder.Append("Remark");
                    break;
                case Preorder19DianLineVOrderColumn.State:
                    orderSqlBuilder.Append("State");
                    break;
                case Preorder19DianLineVOrderColumn.Uuid:
                    orderSqlBuilder.Append("Uuid");
                    break;
                case Preorder19DianLineVOrderColumn.RefundAmount:
                    orderSqlBuilder.Append("RefundAmount");
                    break;
                case Preorder19DianLineVOrderColumn.PreOrderTime:
                    orderSqlBuilder.Append("preOrderTime");
                    break;
                case Preorder19DianLineVOrderColumn.Status:
                    orderSqlBuilder.Append("status");
                    break;
                case Preorder19DianLineVOrderColumn.CityID:
                    orderSqlBuilder.Append("cityID");
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