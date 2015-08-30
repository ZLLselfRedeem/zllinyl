﻿using System;
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
    public partial class Preorder19DianLineManager
    {
        public bool IsExists(long Preorder19DianLineId)
        {
            string sql = "SELECT COUNT(0) FROM Preorder19DianLine WHERE Preorder19DianLineId = @Preorder19DianLineId";
            var retutnValue =
                SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@Preorder19DianLineId", SqlDbType.Int, 8) { Value = Preorder19DianLineId });
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

        public Preorder19DianLine GetEntityById(long Preorder19DianLineId)
        {
            string sql = @"SELECT [Preorder19DianLineId]
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
                            FROM  [Preorder19DianLine] 
                            WHERE [Preorder19DianLineId] = @Preorder19DianLineId ";
            using (var reader =
                   SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@Preorder19DianLineId", SqlDbType.BigInt, 8) { Value = Preorder19DianLineId }))
            {
                if (reader.Read())
                {
                    var entity = reader.GetEntity<Preorder19DianLine>();
                    return entity;
                }
            }
            return null;
        }

        public bool Add(IPreorder19DianLine Entity)
        {
            string sql = @"INSERT INTO Preorder19DianLine (
                                    [Preorder19DianId],
                                    [CustomerId],
                                    [PayType],
                                    [PayAccount],
                                    [Amount],
                                    [CreateTime],
                                    [Remark],
                                    [State],
                                    [Uuid],
                                    [RefundAmount]
                                ) VALUES (
                                    @Preorder19DianId,
                                    @CustomerId,
                                    @PayType,
                                    @PayAccount,
                                    @Amount,
                                    @CreateTime,
                                    @Remark,
                                    @State,
                                    @Uuid,
                                    @RefundAmount
                                    );                                    
                                    SELECT @@IDENTITY";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@Preorder19DianId", SqlDbType.BigInt, 8) { Value = Entity.Preorder19DianId });
            sqlParameterList.Add(new SqlParameter("@CustomerId", SqlDbType.BigInt, 8) { Value = Entity.CustomerId });
            sqlParameterList.Add(new SqlParameter("@PayType", SqlDbType.Int, 4) { Value = Entity.PayType });
            sqlParameterList.Add(new SqlParameter("@PayAccount", SqlDbType.NChar, 200) { Value = Entity.PayAccount });
            sqlParameterList.Add(new SqlParameter("@Amount", SqlDbType.Float, 8) { Value = Entity.Amount });
            sqlParameterList.Add(new SqlParameter("@CreateTime", SqlDbType.DateTime, 8) { Value = Entity.CreateTime });
            sqlParameterList.Add(new SqlParameter("@Remark", SqlDbType.NChar, 400) { Value = SqlHelper.ConvertDbNullValue(Entity.Remark) });
            sqlParameterList.Add(new SqlParameter("@State", SqlDbType.Int, 4) { Value = Entity.State });
            sqlParameterList.Add(new SqlParameter("@Uuid", SqlDbType.NChar, 200) { Value = Entity.Uuid });
            sqlParameterList.Add(new SqlParameter("@RefundAmount", SqlDbType.Float, 8) { Value = Entity.RefundAmount });
            var returnObject =
                SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            int id;
            if (int.TryParse(returnObject.ToString(), out id))
            {
                Entity.Preorder19DianLineId = id;
                return true;
            }
            return false;
        }
        public bool Update(IPreorder19DianLine Entity)
        {
            string sql = @"UPDATE [Preorder19DianLine] SET
                                 [Preorder19DianId] = @Preorder19DianId
                                ,[CustomerId] = @CustomerId
                                ,[PayType] = @PayType
                                ,[PayAccount] = @PayAccount
                                ,[Amount] = @Amount
                                ,[CreateTime] = @CreateTime
                                ,[Remark] = @Remark
                                ,[State] = @State
                                ,[RefundAmount] = @RefundAmount
                           WHERE [Preorder19DianLineId] =@Preorder19DianLineId";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@Preorder19DianLineId", SqlDbType.BigInt, 8) { Value = Entity.Preorder19DianLineId });
            sqlParameterList.Add(new SqlParameter("@Preorder19DianId", SqlDbType.BigInt, 8) { Value = Entity.Preorder19DianId });
            sqlParameterList.Add(new SqlParameter("@CustomerId", SqlDbType.BigInt, 8) { Value = Entity.CustomerId });
            sqlParameterList.Add(new SqlParameter("@PayType", SqlDbType.Int, 4) { Value = Entity.PayType });
            sqlParameterList.Add(new SqlParameter("@PayAccount", SqlDbType.NChar, 200) { Value = Entity.PayAccount });
            sqlParameterList.Add(new SqlParameter("@Amount", SqlDbType.Float, 8) { Value = Entity.Amount });
            sqlParameterList.Add(new SqlParameter("@CreateTime", SqlDbType.DateTime, 8) { Value = Entity.CreateTime });
            sqlParameterList.Add(new SqlParameter("@Remark", SqlDbType.NChar, 400) { Value = SqlHelper.ConvertDbNullValue(Entity.Remark) });
            sqlParameterList.Add(new SqlParameter("@State", SqlDbType.Int, 4) { Value = Entity.State });
            sqlParameterList.Add(new SqlParameter("@RefundAmount", SqlDbType.Float, 8) { Value = Entity.RefundAmount });
            var count = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public bool DeleteEntity(IPreorder19DianLine Entity)
        {
            string sql = @"DELETE FROM [Preorder19DianLine]
                                 WHERE [Preorder19DianLineId] =@Preorder19DianLineId";
            var count =
                    SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, new SqlParameter("@Preorder19DianLineId", SqlDbType.BigInt, 8) { Value = Entity.Preorder19DianLineId });
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        public long GetCountByQuery(Preorder19DianLineQueryObject queryObject = null)
        {
            string sql = @"SELECT COUNT(0)
                            FROM  [Preorder19DianLine] 
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

        public List<IPreorder19DianLine> GetListByQuery(int pageSize, int pageIndex, Preorder19DianLineQueryObject queryObject = null, Preorder19DianLineOrderColumn orderColumn = Preorder19DianLineOrderColumn.Preorder19DianLineId, SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
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
                            FROM  [Preorder19DianLine] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder, pageSize, pageIndex);
            sql = whereSqlBuilder.Insert(0, sql).ToString();
            sql = string.Format(" SELECT TOP (@PageSize) * FROM ({0}) T WHERE T.ROWNUM > @PageIndex ", sql);
            List<IPreorder19DianLine> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<IPreorder19DianLine>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<Preorder19DianLine>());
                }
            }
            return list;
        }

        public List<IPreorder19DianLine> GetListByQuery(Preorder19DianLineQueryObject queryObject = null, Preorder19DianLineOrderColumn orderColumn = Preorder19DianLineOrderColumn.Preorder19DianLineId, SortOrder order = SortOrder.Descending)
        {
            StringBuilder orderSqlBuilder = GetOrderSql(orderColumn, order);
            string sql = @"SELECT ROW_NUMBER() OVER( {0} ) AS ROWNUM
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
                            FROM  [Preorder19DianLine] 
                            WHERE 1 =1 ";
            sql = string.Format(sql, orderSqlBuilder.ToString());
            SqlParameter[] sqlParameters = null;
            StringBuilder whereSqlBuilder = new StringBuilder();
            GetWhereSqlBuilderAndSqlParameterList(queryObject, ref sqlParameters, whereSqlBuilder);
            sql = whereSqlBuilder.Insert(0, sql).ToString();
            List<IPreorder19DianLine> list = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters))
            {
                list = new List<IPreorder19DianLine>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<Preorder19DianLine>());
                }
            }
            return list;
        }
        private static void GetWhereSqlBuilderAndSqlParameterList(Preorder19DianLineQueryObject queryObject, ref SqlParameter[] sqlParameters,
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
            if (!string.IsNullOrEmpty(queryObject.Uuid))
            {
                whereSqlBuilder.Append(" AND Uuid = @Uuid ");
                sqlParameterList.Add(new SqlParameter("@Uuid", SqlDbType.NChar, 400) { Value = queryObject.Uuid });
            }
            if (queryObject.State.HasValue)
            {
                whereSqlBuilder.Append(" AND State = @State ");
                sqlParameterList.Add(new SqlParameter("@State ", SqlDbType.Int, 4) { Value = queryObject.State });
            }
            if (queryObject.IsRefoundOut.HasValue && queryObject.IsRefoundOut.Value)
            {
                whereSqlBuilder.Append(" AND (Amount - RefundAmount) > 0.001 ");
            }

            if (pageIndex.HasValue && pageSize.HasValue)
            {
                sqlParameterList.Add(new SqlParameter("@PageSize", pageSize));
                sqlParameterList.Add(new SqlParameter("@PageIndex", pageSize * (pageIndex - 1)));
            }
            sqlParameters = sqlParameterList.ToArray();
        }

        private static StringBuilder GetOrderSql(Preorder19DianLineOrderColumn orderColumn, SortOrder order)
        {
            StringBuilder orderSqlBuilder = new StringBuilder(" ORDER BY ");
            switch (orderColumn)
            {
                case Preorder19DianLineOrderColumn.Preorder19DianLineId:
                    orderSqlBuilder.Append("Preorder19DianLineId");
                    break;
                case Preorder19DianLineOrderColumn.Preorder19DianId:
                    orderSqlBuilder.Append("Preorder19DianId");
                    break;
                case Preorder19DianLineOrderColumn.CustomerId:
                    orderSqlBuilder.Append("CustomerId");
                    break;
                case Preorder19DianLineOrderColumn.PayType:
                    orderSqlBuilder.Append("PayType");
                    break;
                case Preorder19DianLineOrderColumn.PayAccount:
                    orderSqlBuilder.Append("PayAccount");
                    break;
                case Preorder19DianLineOrderColumn.Amount:
                    orderSqlBuilder.Append("Amount");
                    break;
                case Preorder19DianLineOrderColumn.CreateTime:
                    orderSqlBuilder.Append("CreateTime");
                    break;
                case Preorder19DianLineOrderColumn.Remark:
                    orderSqlBuilder.Append("Remark");
                    break;
                case Preorder19DianLineOrderColumn.State:
                    orderSqlBuilder.Append("State");
                    break;
                case Preorder19DianLineOrderColumn.RefundAmount:
                    orderSqlBuilder.Append("RefundAmount");
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
        /// 财务查询指定时期粮票退款金额
        /// </summary>
        /// <returns></returns>
        public DataTable SelectFoodPreOrder19dian(DateTime beginDT, DateTime endDT, int cityID)
        {
            StringBuilder strSql = new StringBuilder();
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@beginDT", SqlDbType.DateTime) { Value = beginDT });
            para.Add(new SqlParameter("@endDT", SqlDbType.DateTime) { Value = endDT });
            para.Add(new SqlParameter("@PayType", SqlDbType.Int) { Value = (int)VAOrderUsedPayMode.BALANCE });
            strSql.Append("select CAST(SUM(Amount) as numeric(18,2)) Amount,");
            strSql.Append(" CAST(SUM(RefundAmount) as numeric(18,2)) RefundAmount ");
            strSql.Append(" from Preorder19DianLine a");
            strSql.Append(" inner join Preorder19Dian p on a.Preorder19DianId=p.preOrder19dianId");
            if (cityID > 0)
            {
                strSql.Append(" inner join ShopInfo s on p.shopId=s.shopID");
            }
            strSql.Append(" where PayType=@PayType and p.preOrderTime between @beginDT and @endDT");
            if (cityID > 0)
            {
                strSql.Append(" and s.cityID=@cityID");
                para.Add(new SqlParameter("@cityID", SqlDbType.Int) { Value = cityID });
            }

            SqlParameter[] pa = para.ToArray();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), pa);
            return ds.Tables[0];
        }

        /// <summary>
        /// 按订单id返回列表
        /// </summary>
        /// <param name="preorder19DianId">订单id</param>
        /// <returns></returns>
        public IEnumerable<Preorder19DianLine> GetListOfPreorder19DianId(long preorder19DianId)
        {
            string sql = "SELECT * FROM Preorder19DianLine WHERE [Preorder19DianId]=@Preorder19DianId";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@Preorder19DianId ", preorder19DianId) };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader drea = SqlHelper.ExecuteReader(conn, CommandType.Text, sql, parameters))
            {
                while (drea.Read())
                    yield return SqlHelper.GetEntity<Preorder19DianLine>(drea);
            }
        }

        /// <summary>
        /// 按订单id返回价钱列表
        /// </summary>
        /// <param name="orderId">订单id</param>
        /// <returns></returns>
        public IEnumerable<Preorder19DianLine> GetOrderToPrices(Guid orderId)
        {
            string sql = "SELECT [State],PayType,sum(Amount)as Amount,sum(RefundAmount) as RefundAmount  FROM Preorder19DianLine WHERE Preorder19DianId IN (SELECT preOrder19dianId FROM PreOrder19dian WHERE OrderId=@orderId)group by [State],PayType";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("@orderId ", orderId) };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader drea = SqlHelper.ExecuteReader(conn, CommandType.Text, sql, parameters))
            {
                while (drea.Read())
                    yield return SqlHelper.GetEntity<Preorder19DianLine>(drea);
            }
        }

        /// <summary>
        /// 判断支付详情是否有第三方支付
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public bool IsMoneyPaymentOfOrderId(Guid orderId)
        {
            string sql = "SELECT COUNT(0) FROM Preorder19DianLine WHERE Preorder19DianId IN (SELECT Preorder19DianId FROM PreOrder19dian WHERE OrderId=@OrderId) AND PayType IN (3,4)";
            SqlParameter[] sqlParameters = new SqlParameter[] { new SqlParameter("@OrderId", orderId) };
            return (int)SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameters) != 0;
        }
    }
}