﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class ZZBPreOrderManager
    {
        ///// <summary>
        ///// 根据预点单编号查询预点单信息和对应的用户信息
        ///// </summary>
        ///// <param name="startTime"></param>
        ///// <param name="endTime"></param>
        ///// <param name="shopId"></param>
        ///// <param name="flag"></param>
        ///// <returns></returns>
        //public DataTable ZZB_SelectPreOrderList(string startTime, string endTime, int shopId, int flag)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select  A.preOrder19dianId,A.prePaidSum,B.UserName customerName,B.eCardNumber");
        //    strSql.Append(" from CustomerInfo as B inner join PreOrder19dian as A on B.eCardNumber = A.eCardNumber ");
        //    strSql.Append(" inner join PreOrderQueryInfo as C on C.preorder19dianId=A.preOrder19dianId");
        //    strSql.AppendFormat(" where A.isShopVerified=1 and A.isPaid=1 and A.shopId={0}", shopId);//支付，验证
        //    switch (flag)
        //    {
        //        case 2://待审核（条件：已支付，已验证，未审核，未对账）
        //            strSql.Append(" and (A.isShopConfirmed=0 or A.isShopConfirmed is null) and (A.isApproved=0 or A.isApproved is null) ");
        //            break;
        //        case 3://待对账（条件：已支付，已验证，已审核，未对账）
        //            strSql.Append(" and A.isShopConfirmed=1 and (A.isApproved=0 or A.isApproved is null) ");
        //            break;
        //        case 4://全部（条件：已支付，已验证，已审核，已对账）
        //            strSql.Append(" and A.isShopConfirmed=1 and A.isApproved=1");
        //            break; ;
        //    }
        //    strSql.Append(" group by A.preOrder19dianId,A.prePaidSum,B.UserName,B.eCardNumber");
        //    strSql.AppendFormat(" having MAX(C.queryTime) between '{0}' and '{1}'", startTime, endTime);
        //    strSql.Append(" order by MAX(C.queryTime) desc");
        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
        //    return ds.Tables[0];
        //}
//        /// <summary>
//        /// 根据预点单编号查询预点单信息和对应的用户信息
//        /// </summary>
//        /// <param name="startTime"></param>
//        /// <param name="endTime"></param>
//        /// <param name="shopId"></param>
//        /// <returns></returns>
//        public DataTable ZZB_SelectPreOrderList(string startTime, string endTime, int shopId)
//        {
//            StringBuilder strSql = new StringBuilder();
//            strSql.Append("select  A.preOrder19dianId,A.prePaidSum,B.UserName customerName,B.eCardNumber");
//            strSql.Append("  from CustomerInfo as B inner join PreOrder19dian as A on B.eCardNumber = A.eCardNumber");
//            strSql.AppendFormat(" where (A.isShopVerified=0 or A.isShopVerified is null)  and A.isPaid=1 and A.shopId={0}", shopId);//支付，未验证
//            strSql.AppendFormat(" and A.preOrderTime between '{0}' and '{1}'", startTime, endTime);
//            strSql.Append(" order by A.preOrderTime desc");
//            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
//            return ds.Tables[0];
//        }
//        /// <summary>
//        /// 悠先服务：根据店铺ID查询两种单据
//        /// 已支付，未审核，非退款中，非已退款，非已删除
//        /// 已支付，已审核，未对账
//        /// 若用户无电话号码，则带出 VIP号码-验证码
//        /// </summary>
//        /// <param name="shopId"></param>
//        /// <returns></returns>
//        public DataTable ZZB_SelectPreOrderList(int shopId)
//        {
//            const string strSql = @"select O.preOrder19dianId,O.prePaidSum,O.prePayTime,O.status,O.isShopConfirmed,O.refundMoneyClosedSum,O.refundMoneySum,
//                                     isnull(isnull(C.mobilePhoneNumber,O.eCardNumber+'-'+O.verificationCode),'') number,isnull(C.UserName,'') UserName
//                                      from PreOrder19dian O inner join CustomerInfo C
//                                      on O.customerId = C.CustomerID
//                                      and O.shopId = @shopId
//                                      and ((O.isPaid = 1 and isnull(O.isShopConfirmed, 0) <> 1 and O.status <> 105 and O.status <> 107 and O.status <> 104)
//                                      or (O.isPaid = 1 and isnull(O.isShopConfirmed, 0) = 1 and isnull(O.isApproved, 0) <> 1))  
//                                      and C.CustomerStatus > 0
//                                      order by O.preOrderTime desc,O.isShopConfirmed";
//            SqlParameter[] para = new SqlParameter[] { 
//            new SqlParameter("@shopId", SqlDbType.Int) { Value = @shopId }
//            };
//            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
//            return ds.Tables[0];
//        }
        /// <summary>
        /// 掌中宝查询点单详情
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        //public DataTable ZZB_SelectPreOrderListDetail(int preOrder19dianId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select A.preOrder19dianId,preOrderTime,");
        //    strSql.Append("  prePaidSum,refundMoneySum,(prePaidSum-isnull(refundMoneySum,0)) as canRefundAccount,sundryJson,orderInJson,A.isShopVerified,A.isShopConfirmed,A.isApproved");//Add canRefundAccount at 20131230 by jinyanni
        //    strSql.AppendFormat("  from PreOrder19dian A  where A.preOrder19dianId={0}", preOrder19dianId);
        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
        //    return ds.Tables[0];
        //}
        ///// <summary>
        ///// 查询点单的最大验证时间
        ///// </summary>
        ///// <param name="preOrder19dianId"></param>
        ///// <returns></returns>
        //public string ZZB_SelectMaxPreOrderQueryTime(int preOrder19dianId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.AppendFormat("select MAX(queryTime) from PreOrderQueryInfo where preorder19dianId={0} and isShopVerified=1", preOrder19dianId);
        //    string maxQueryTime = string.Empty;
        //    using (IDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()))
        //    {
        //        if (dr.Read())
        //        {
        //            maxQueryTime = dr[0] == DBNull.Value ? "" : Convert.ToString(dr[0]);
        //        }
        //    }
        //    return maxQueryTime;
        //}
        /// <summary>
        /// 悠先服务：查询点单详情
        /// </summary>
        /// <param name="preOder19dianId"></param>
        /// <returns></returns>
        public DataTable ZZB_SelectPreOrderListDetail(long preOder19dianId)
        {
            //const string strSql = @"select O.customerId,O.preOrder19dianId,O.prePayTime,O.discount,O.preOrderServerSum orginalPrice,O.isEvaluation,
                                       // (O.preOrderServerSum-O.prePaidSum) savedAmount,O.invoiceTitle,O.status,
//                                        O.prePaidSum,O.refundMoneySum,O.deskNumber, (prePaidSum-isnull(refundMoneySum,0)) as canRefundAccount,
//                                        O.sundryJson,O.orderInJson,O.isShopConfirmed,O.isApproved, C.mobilePhoneNumber,C.UserName
//                                        from PreOrder19dian O inner join CustomerInfo C  on O.customerId = C.CustomerID and O.preOrder19dianId =@preOder19dianId
//                                        and O.isPaid = 1 and isnull(O.isApproved,0) <> 1 and O.status > 0 and C.CustomerStatus > 0";
            const string strSql = @"select O.customerId,O.preOrder19dianId,O.prePayTime,O.discount,O.preOrderSum orginalPrice,O.isEvaluation,
                                        (O.preOrderSum-O.prePaidSum) savedAmount,O.invoiceTitle,O.status,
                                        O.prePaidSum,O.refundMoneySum,O.deskNumber, (prePaidSum-isnull(refundMoneySum,0)) as canRefundAccount,
                                        O.sundryJson,O.orderInJson,O.isShopConfirmed,O.isApproved, C.mobilePhoneNumber,C.UserName,O.verifiedSaving,O.orderId
                                        from PreOrder19dian O inner join CustomerInfo C  on O.customerId = C.CustomerID and O.preOrder19dianId =@preOder19dianId
                                        and O.isPaid = 1 and O.status > 0 and C.CustomerStatus > 0";
            
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOder19dianId", SqlDbType.BigInt) { Value = preOder19dianId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }

        public DataTable ZZB_SelectOrderListDetail(Guid orderID)
        {
            const string strSql = @"SELECT o.customerid,
                                           o.preorder19dianid,
                                           d.prepaytime,
                                           o.discount,
                                           d.preOrderServerSum orginalprice,
                                           d.isevaluation,
                                           (d.preOrderServerSum - d.prepaidsum) savedamount,
                                           d.invoicetitle,
                                           d.status,
                                           d.prepaidsum,
                                           d.refundmoneysum,
                                           o.desknumber,
                                           (d.prepaidsum - Isnull(d.refundmoneysum,0)) AS canrefundaccount,
                                           o.sundryjson,
                                           o.orderinjson,
                                           d.isshopconfirmed,
                                           o.isapproved,
                                           c.mobilephonenumber,
                                           c.username,
                                           d.PayDifferenceSum,
                                           d.VerifiedSaving
                                    FROM   [Order] d
	                                       INNER JOIN preorder19dian o
		                                    ON d.Id=o.OrderId
                                           INNER JOIN customerinfo c
                                             ON o.customerid = c.customerid
                                                AND d.id = @orderId
                                                AND o.ispaid = 1
                                                AND o.status > 0
                                                AND c.customerstatus > 0
                                                AND o.OrderType=1
                                            order by o.preOrderTime";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@orderId", SqlDbType.UniqueIdentifier) { Value = orderID }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取补差价金额
        /// </summary>
        /// <param name="orderID">订单ID</param>
        /// <returns>补差价的总额</returns>
        public double ZZB_SelectFillPostAmount(Guid orderID)
        {
            const string strSql = @"select sum(prePaidSum) as fillpostAmount from PreOrder19dian where ordertype=2 and ispaid = 1 and status > 0 orderid= @orderId ";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@orderId", SqlDbType.UniqueIdentifier) { Value = orderID }
            };
            double result = 0;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                if (dr.Read())
                {
                    result = dr[0] == DBNull.Value ? 0 : Convert.ToDouble(dr[0]);
                }
            }
            return result;
        }
    }
}
