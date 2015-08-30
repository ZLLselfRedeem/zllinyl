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
    public partial class PreOrder19dianManager
    {
        /// <summary>
        /// 更新点单PreOrder19dianExtend 的 OrderInJson
        /// </summary>
        /// <param name="orderJson"></param>
        /// <param name="preOrderId"></param>
        /// <returns></returns>
        public bool UpdateOrderJson(string orderJson, long preOrderId)
        {
            const string strSql = @"update PreOrder19dianExtend set orderInJson=@orderJson where preOrder19DianId=@preOrder19dianId";
            SqlParameter[] parameters = { new SqlParameter("@orderJson", orderJson), new SqlParameter("@preOrder19dianId", preOrderId) };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, parameters) > 0;
            }
        }
        /// <summary>
        /// 查询点单扩展表OrderJson信息
        /// </summary>
        /// <param name="preOrderId"></param>
        /// <returns></returns>
        public string GetPreOrder19DianExtendOrderJson(long preOrderId)
        {
            const string strSql = @"select orderInJson from PreOrder19dianExtend where preOrder19DianId=@preOrder19dianId";
            SqlParameter[] parameters = { new SqlParameter("@preOrder19dianId", preOrderId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameters))
            {
                if (dr.Read())
                {
                    return dr[0] == DBNull.Value ? "" : Convert.ToString(dr[0]);
                }
            }
            return "";
        }

        public double GetExtendPayByPreOrder19DianId(long preOrderId)
        {
            const string strSql = @"SELECT isnull(ExtendPay,0) ExtendPay FROM PreOrder19dianExtend WHERE preOrder19DianId=@preOrder19dianId";
            SqlParameter[] parameters = { new SqlParameter("@preOrder19dianId", preOrderId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameters))
            {
                if (dr.Read())
                {
                    return dr[0] == DBNull.Value ? 0 : Convert.ToDouble(dr[0]);
                }
            }
            return 0;
        }

        public double GetExtendPayByOrderId(Guid orderId)
        {
            string strSql = @"SELECT SUM(isnull(ExtendPay,0)) FROM PreOrder19dianExtend WHERE preOrder19DianId IN (SELECT preOrder19dianId FROM PreOrder19dian WHERE OrderId=@OrderId)";
            SqlParameter[] parameters = { new SqlParameter("@OrderId", orderId) };
            return (double)SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameters);
        }

        /// <summary>
        /// 更新点单OrderInJson和SundryJson
        /// </summary>
        /// <param name="orderJson"></param>
        /// <param name="sundryJson"></param>
        /// <param name="preOrderId"></param>
        /// <returns></returns>
        public bool UpdateOrderJsonAndSundryJson(string orderJson, string sundryJson, long preOrderId)
        {
            const string strSql = @"update PreOrder19dianExtend set orderInJson=@orderJson,sundryJson=@sundryJson where preOrder19DianId=@preOrder19dianId;";
            SqlParameter[] parameters = { 
                                          new SqlParameter("@orderJson", orderJson), 
                                          new SqlParameter("@sundryJson", sundryJson), 
                                          new SqlParameter("@preOrder19dianId", preOrderId) 
                                        };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, parameters) > 0;
            }
        }
        /// <summary>
        /// 更新菜品点赞次数
        /// </summary>
        /// <param name="dishIds"></param>
        /// <returns></returns>
        public bool UpdateDishInfoPraiseNum(string dishIds)
        {
            string strSql = String.Format("update DishInfo set dishPraiseNum=dishPraiseNum+1 where DishID in {0}", dishIds);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql) > 0;
        }

        /// <summary>
        /// 更新菜品点赞次数
        /// </summary>
        /// <param name="dishIds"></param>
        /// <returns></returns>
        public bool UpdateExtendPay(double payAmount, long preOrder19dianId)
        {
            string strSql = " UPDATE PreOrder19dianExtend SET ExtendPay = @ExtendPay WHERE PreOrder19dianId  = @PreOrder19dianId  ";
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@ExtendPay", payAmount);
            sqlParameters[1] = new SqlParameter("@PreOrder19dianId", preOrder19dianId);
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, sqlParameters) > 0;
        }
        /// <summary>
        /// 新增点单扩展表信息
        /// </summary>
        /// <param name="orderInJson"></param>
        /// <param name="sundryJson"></param>
        /// <param name="preOrder19DianId"></param>
        /// <returns></returns>
        public long InsertPreOrder19DianExtend(string orderInJson, string sundryJson, long preOrder19DianId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                const string strSql = @"insert into PreOrder19dianExtend(orderInJson,sundryJson,preOrder19dianId) 
 values (@orderInJson,@sundryJson,@preOrder19dianId) select @@identity";
                SqlParameter[] parameters = new SqlParameter[]{
                        new SqlParameter("@orderInJson", SqlDbType.NVarChar) { Value = orderInJson },
                        new SqlParameter("@sundryJson",SqlDbType.NVarChar) { Value = sundryJson },
                        new SqlParameter("@preOrder19dianId", SqlDbType.BigInt) { Value = preOrder19DianId }
                    };
                Object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, parameters);
                return obj == null ? 0 : Convert.ToInt64(obj);
            }
        }

        public DataTable SelectOrder(long preOrder19DianId)
        {
            const string strSql = @"select orderInJson,isnull(discount,0) discount,preOrderSum,sundryJson,B.isSupportAccountsRound
 from PreOrder19dian A
inner join ShopInfo B on A.shopId=B.shopID
where preOrder19dianId=@preOrder19dianId";
            SqlParameter[] parameters = new SqlParameter[]{
                        new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8) { Value = preOrder19DianId }
                    };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameters);
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询某订单是否有扩展支付金额
        /// </summary>
        /// <param name="preOrder19DianId"></param>
        /// <returns></returns>
        public double SelectExtendPay(long preOrder19DianId)
        {
            string strSql = "select isnull(ExtendPay,0) ExtendPay from PreOrder19dianExtend where preOrder19DianId=@preOrder19DianId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19DianId", SqlDbType.BigInt) { Value = preOrder19DianId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDouble(obj);
                }
            }
        }

        /// <summary>
        /// 按用户id返回订单数
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns></returns>
        public Tuple<int, decimal> GetMobileOrdersCount(string mobile)
        {
            string sql = "SELECT COUNT(0),SUM(prePaidSum) FROM PreOrder19dian WHERE customerId=(SELECT Customerid FROM CustomerInfo WHERE mobilePhoneNumber=@mobile)";
            SqlParameter[] parameters = { new SqlParameter("@mobile", mobile) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, parameters))
            {
                if (dr.Read())
                    return new Tuple<int, decimal>((int)dr[0], dr[1] is DBNull ? 0 : Convert.ToDecimal(dr[1]));
            }
            return null;
        }

        public DataTable GetFinancial(int shopid, string beginDT, string endDT, int status, int cityid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select b.shopName,c.companyName,sum(a.prePaidSum-ISNULL(a.refundMoneySum,0)) prePaidSum,");
            //strSql.Append(" SUM(a.viewallocCommission) viewallocCommission,");
            strSql.Append(" SUM(g.accountMoney*-1) viewallocCommission,");
            strSql.Append(" ISNULL(MAX(b.viewallocCommissionValue),0) viewallocCommissionValue,");
            strSql.Append(" case when SUM(a.prePaidSum-ISNULL(a.refundMoneySum,0))>0 then round(ISNULL(SUM(g.accountMoney*-1),0)/SUM(a.prePaidSum-ISNULL(a.refundMoneySum,0)),2) else 0 end viewalloc,");
            strSql.Append(" sum(d.balance) balance,");
            strSql.Append(" sum(d.alipay) alipay,");
            strSql.Append(" sum(d.wechat) wechat,");
            strSql.Append(" sum(d.redenvelope) redenvelope,");
            strSql.Append(" SUM(ISNULL(f.ExtendPay,0)) ExtendPay");
            strSql.Append(" from preOrder19dian a");
            strSql.Append(" inner join ShopInfo b on a.shopId=b.shopID");
            strSql.Append(" inner join CompanyInfo c on a.companyId=c.companyID");
            strSql.Append(" inner join View_PreOrderPayType d on d.Preorder19DianId=a.preOrder19dianId");
            strSql.Append(" inner join PreorderCheckInfo e on a.preOrder19dianId=e.preOrder19dianId");
            strSql.Append(" inner join PreOrder19dianExtend f on a.preOrder19dianId=f.preOrder19DianId");
            strSql.Append(" left join MoneyMerchantAccountDetail g on g.accountTypeConnId=a.preOrder19dianId and accountType=13");
            strSql.AppendFormat(" where e.checkTime>='{0}' and e.checkTime<='{1}' ", beginDT, endDT);
            if (shopid > 0)
            {
                strSql.AppendFormat(" and b.shopid={0}", shopid);
            }
            if (cityid > 0)
            {
                strSql.AppendFormat(" and b.cityId={0}", cityid);
            }
            strSql.Append(" group by shopName,companyName");
            if (status == 1)
            {
                strSql.Append(" having SUM(isnull(g.accountMoney,0)*-1)>0");
            }
            if (status == 0)
            {
                strSql.Append(" having SUM(isnull(g.accountMoney,0)*-1)=0");
            }

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询新的订单表详情
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public DataTable SybSelectOrder(Guid orderId)
        {
            const string strSql = @"SELECT a.preOrder19dianId,
                                           a.discount,
                                           a.[orderInJson],
                                           a.[sundryJson],
                                           o.[preOrderServerSum] preOrderSum,
                                           o.[prePaidSum],
                                           Round(o.[preOrderServerSum] - o.[verifiedSaving],2) [afterDiscountAmount],
                                           o.[isPaid],
                                           o.[prePayTime],
                                           o.refundMoneySum,
                                           o.invoiceTitle,
                                           o.verifiedSaving,
                                           o.PayDifferenceSum
                                    FROM   [order] o
                                           INNER JOIN preorder19dian a
                                             ON o.id = a.orderid
                                    WHERE  a.OrderType=1 and o.id =@orderId";

            SqlParameter para = new SqlParameter("@orderId", SqlDbType.UniqueIdentifier) { Value = orderId };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }
   


        //-------------------------------------------------------------------------------------------------
        /// <summary>
        /// 检查订单是否符合已支付为入座且支付时间是当日
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public bool PreOrderIsToday(long preOrder19dianId)
        {
            string strSql = @"select 1 from PreOrder19dian where preOrder19dianId=@preOrder19dianId and isPaid=1 and ISNULL(isShopConfirmed,0)=0 and ISNULL(refundMoneySum,0)=0
and prePayTime between '" + DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00' and '" + DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59'";

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt) { Value = preOrder19dianId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// 检查用户是否只有一个点单
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public bool IsOnePaidOrder(long customerId)
        {
            const string strSql = "select COUNT(1) from PreOrder19dian where customerId=@customerId and isPaid=1";

           SqlParameter[] para = new SqlParameter[] { 
           new SqlParameter("@customerId", SqlDbType.BigInt) { Value = customerId }
           };

           using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
           {
               object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
               if (obj != null && Convert.ToInt32(obj) == 0)
               {
                   return true;
               }
               else
               {
                   return false;
               }
           }
        }
        //-------------------------------------------------------------------------------------------------
    }
}
