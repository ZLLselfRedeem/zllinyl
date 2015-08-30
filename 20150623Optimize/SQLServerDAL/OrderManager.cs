﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using System.Reflection;
using System.ComponentModel;
using LogDll;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;


namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class OrderManager
    {
        public Order GetEntityById(Guid Id)
        {
            string sql = "SELECT * FROM [Order] WHERE Id = @Id";
            SqlParameter sqlParameter = new SqlParameter("@Id", Id);
            Order order = null;
            using (var reader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameter))
            {
                if (reader.Read())
                {
                    order = reader.GetEntity<Order>();
                }
            }
            return order;
        }

        public bool Add(Order Entity)
        {
            string sql = @"INSERT INTO [Order] (
                                    [Id],
                                    [ShopId],
                                    [Status],
                                    [CustomerId],
                                    [PreOrderServerSum],
                                    [PreOrderTime],
                                    [IsPaid],
                                    [PrePaidSum],
                                    [PrePayTime],
                                    [RefundMoneySum],
                                    [RefundMoneyClosedSum],
                                    [IsEvaluation],
                                    [ExpireTime],
                                    [VerifiedSaving],
                                    [IsShopConfirmed],
                                    [InvoiceTitle],
                                    [Remark],
                                    [PayDifferenceSum]

                                ) VALUES (
                                    @Id,
                                    @ShopId,
                                    @Status,
                                    @CustomerId,
                                    @PreOrderServerSum,
                                    @PreOrderTime,
                                    @IsPaid,
                                    @PrePaidSum,
                                    @PrePayTime,
                                    @RefundMoneySum,
                                    @RefundMoneyClosedSum,
                                    @IsEvaluation,
                                    @ExpireTime,
                                    @VerifiedSaving,
                                    @IsShopConfirmed,
                                    @InvoiceTitle,
                                    @Remark,
                                    @PayDifferenceSum
                                    ); ";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier, 16) { Value = Entity.Id });
            sqlParameterList.Add(new SqlParameter("@ShopId", SqlDbType.Int, 4) { Value = Entity.ShopId });
            sqlParameterList.Add(new SqlParameter("@Status", SqlDbType.TinyInt, 1) { Value = Entity.Status });
            sqlParameterList.Add(new SqlParameter("@CustomerId", SqlDbType.BigInt, 8) { Value = Entity.CustomerId });
            sqlParameterList.Add(new SqlParameter("@PreOrderServerSum", SqlDbType.Float, 8) { Value = Entity.PreOrderServerSum });
            sqlParameterList.Add(new SqlParameter("@PreOrderTime", SqlDbType.DateTime, 8) { Value = Entity.PreOrderTime });
            sqlParameterList.Add(new SqlParameter("@IsPaid", SqlDbType.TinyInt, 1) { Value = Entity.IsPaid });
            sqlParameterList.Add(new SqlParameter("@PrePaidSum", SqlDbType.Money, 8) { Value = Entity.PrePaidSum });
            sqlParameterList.Add(new SqlParameter("@PrePayTime", SqlDbType.DateTime, 8) { Value = Entity.PrePayTime });
            sqlParameterList.Add(new SqlParameter("@RefundMoneySum", SqlDbType.Money, 8) { Value = Entity.RefundMoneySum });
            sqlParameterList.Add(new SqlParameter("@RefundMoneyClosedSum", SqlDbType.Float, 8) { Value = Entity.RefundMoneyClosedSum });
            sqlParameterList.Add(new SqlParameter("@IsEvaluation", SqlDbType.TinyInt, 1) { Value = Entity.IsEvaluation });
            sqlParameterList.Add(new SqlParameter("@ExpireTime", SqlDbType.DateTime, 8) { Value = Entity.ExpireTime });
            sqlParameterList.Add(new SqlParameter("@VerifiedSaving", SqlDbType.Money, 8) { Value = Entity.VerifiedSaving });
            sqlParameterList.Add(new SqlParameter("@IsShopConfirmed", SqlDbType.TinyInt, 1) { Value = Entity.IsShopConfirmed });
            sqlParameterList.Add(new SqlParameter("@InvoiceTitle", SqlDbType.NVarChar, 50) { Value = Entity.InvoiceTitle });
            sqlParameterList.Add(new SqlParameter("@PayDifferenceSum", SqlDbType.Float, 50) { Value = Entity.PayDifferenceSum });
            sqlParameterList.Add(new SqlParameter("@Remark", SqlDbType.NVarChar, 50) { Value = Entity.Remark == null ? string.Empty : Entity.Remark });
            var returnObject =
                SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());

            return true;

        }

        public bool Update(Order Entity)
        {
            string sql = @"UPDATE [Order] SET
                                 [ShopId] = @ShopId
                                ,[Status] = @Status
                                ,[CustomerId] = @CustomerId
                                ,[PreOrderServerSum] = @PreOrderServerSum
                                ,[PreOrderTime] = @PreOrderTime
                                ,[IsPaid] = @IsPaid
                                ,[PrePaidSum] = @PrePaidSum
                                ,[PrePayTime] = @PrePayTime
                                ,[RefundMoneySum] = @RefundMoneySum
                                ,[RefundMoneyClosedSum] = @RefundMoneyClosedSum
                                ,[IsEvaluation] = @IsEvaluation
                                ,[ExpireTime] = @ExpireTime
                                ,[VerifiedSaving] = @VerifiedSaving
                                ,[IsShopConfirmed] = @IsShopConfirmed
                                ,[InvoiceTitle] = @InvoiceTitle
                                ,[PayDifferenceSum] = @PayDifferenceSum
                           WHERE [Id] =@Id";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@Id", SqlDbType.UniqueIdentifier, 16) { Value = Entity.Id });
            sqlParameterList.Add(new SqlParameter("@ShopId", SqlDbType.Int, 4) { Value = Entity.ShopId });
            sqlParameterList.Add(new SqlParameter("@Status", SqlDbType.TinyInt, 1) { Value = Entity.Status });
            sqlParameterList.Add(new SqlParameter("@CustomerId", SqlDbType.BigInt, 8) { Value = Entity.CustomerId });
            sqlParameterList.Add(new SqlParameter("@PreOrderServerSum", SqlDbType.Float, 8) { Value = Entity.PreOrderServerSum });
            sqlParameterList.Add(new SqlParameter("@PreOrderTime", SqlDbType.DateTime, 8) { Value = Entity.PreOrderTime });
            sqlParameterList.Add(new SqlParameter("@IsPaid", SqlDbType.TinyInt, 1) { Value = Entity.IsPaid });
            sqlParameterList.Add(new SqlParameter("@PrePaidSum", SqlDbType.Money, 8) { Value = Entity.PrePaidSum });
            sqlParameterList.Add(new SqlParameter("@PrePayTime", SqlDbType.DateTime, 8) { Value = Entity.PrePayTime });
            sqlParameterList.Add(new SqlParameter("@RefundMoneySum", SqlDbType.Money, 8) { Value = Entity.RefundMoneySum });
            sqlParameterList.Add(new SqlParameter("@RefundMoneyClosedSum", SqlDbType.Float, 8) { Value = Entity.RefundMoneyClosedSum });
            sqlParameterList.Add(new SqlParameter("@IsEvaluation", SqlDbType.TinyInt, 1) { Value = Entity.IsEvaluation });
            sqlParameterList.Add(new SqlParameter("@ExpireTime", SqlDbType.DateTime, 8) { Value = Entity.ExpireTime });
            sqlParameterList.Add(new SqlParameter("@VerifiedSaving", SqlDbType.Money, 8) { Value = Entity.VerifiedSaving });
            sqlParameterList.Add(new SqlParameter("@IsShopConfirmed", SqlDbType.TinyInt, 1) { Value = Entity.IsShopConfirmed });
            sqlParameterList.Add(new SqlParameter("@InvoiceTitle", SqlDbType.NVarChar, 50) { Value = Entity.InvoiceTitle });
            sqlParameterList.Add(new SqlParameter("@PayDifferenceSum", SqlDbType.Float) { Value = Entity.PayDifferenceSum });
            var count = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            if (count > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 查询某些orderId对应的支付金额信息
        /// </summary>
        /// <param name="orderIds"></param>
        /// <returns></returns>
        public List<OrderPaidDetail> GetOrderPaidList(Guid[] orderIds)
        {
            const string strSql = "select Id,PreOrderServerSum,PrePaidSum,VerifiedSaving,refundMoneySum from [Order] where Id in (select d.x.value('./id[1]','UniqueIdentifier') from @xml.nodes('/*') as d(x)) ";
            var xml = new StringBuilder();
            foreach (var t in orderIds)
            {
                xml.AppendFormat("<row><id>{0}</id></row>", (Guid)t);
            }

            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@xml", SqlDbType.Xml) { Value = xml.ToString() }
            };
            List<OrderPaidDetail> orderPaidDetails = new List<OrderPaidDetail>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    orderPaidDetails.Add(SqlHelper.GetEntity<OrderPaidDetail>(sdr));
                }
            }
            return orderPaidDetails;
        }

        /// <summary>
        /// 查询orderId对应的支付金额信息
        /// </summary>
        /// <param name="orderIds"></param>
        /// <returns></returns>
        public OrderPaidDetail GetOrderPaidDeatial(Guid orderId)
        {
            const string strSql = "select id,PreOrderServerSum,PrePaidSum,VerifiedSaving,refundMoneySum from [Order] where Id =@orderId ";

            SqlParameter[] para = new SqlParameter[]{
                new SqlParameter("@orderId", SqlDbType.UniqueIdentifier) { Value = orderId }
            };
            OrderPaidDetail orderPaidDetail = new OrderPaidDetail();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    orderPaidDetail = SqlHelper.GetEntity<OrderPaidDetail>(sdr);
                }
            }
            return orderPaidDetail;
        }

        public bool ShopConfrim(long preOrder19dianId, int shopConfirmenStatus)
        {
            int status = 0;
            if (shopConfirmenStatus == 1)//审核操作
            {
                status = (int)VAPreorderStatus.Completed;
            }
            else//取消审核
            {
                status = (int)VAPreorderStatus.Prepaid;
            }
            string sql = @"update [order] set isShopConfirmed=@isShopConfirmed,status=@status 
where id=(select OrderId from PreOrder19dian where preOrder19dianId=@preOrder19dianId)";
            List<SqlParameter> sqlParameterList = new List<SqlParameter>();
            sqlParameterList.Add(new SqlParameter("@preOrder19dianId", SqlDbType.Int) { Value = preOrder19dianId });
            sqlParameterList.Add(new SqlParameter("@status", SqlDbType.Int) { Value = status });
            sqlParameterList.Add(new SqlParameter("@IsShopConfirmed", SqlDbType.TinyInt, 1) { Value = shopConfirmenStatus });
            var count = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameterList.ToArray());
            if (count > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// add by dangtao at 20150730 for BUG #2061 优先点菜：通过优先点菜老版本删除的订单，在新版本上仍可见
        /// </summary>
        /// <param name="preOrder19dianIds"></param>
        /// <returns></returns>
        public bool UpdateOrderStatus(string strPreOrder19dianIds)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                string strSql = String.Format(@"update [order] set [status] = @status where [id] in 
                                (select [OrderId] from [PreOrder19dian] where [Preorder19DianId] in {0}) and [status] != {1}",
                                  strPreOrder19dianIds, (int)VAPreorderStatus.Prepaid);
                SqlParameter[] parameters = { new SqlParameter("@status", SqlDbType.SmallInt, 2) };
                parameters[0].Value = VAPreorderStatus.Deleted;
                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                return result > 0;
            }
        }
    }
}
