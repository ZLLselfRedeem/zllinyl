using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
//
//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2012-04-10.
//
namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 支付宝相关数据库操作类
    /// </summary>
    public class AlipayManager
    {
        /// <summary>
        /// 新增支付宝订单信息
        /// </summary>
        /// <param name="alipayOrder"></param>
        /// <returns></returns>
        public long InsertAlipayOrder(AlipayOrderInfo alipayOrder)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into AlipayOrderInfo(");
                    strSql.Append("totalFee,orderCreatTime,orderStatus,conn19dianOrderType,connId,subject,customerId)");
                    strSql.Append(" values (");
                    strSql.Append("@totalFee,@orderCreatTime,@orderStatus,@conn19dianOrderType,@connId,@subject,@customerId)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					    new SqlParameter("@totalFee", SqlDbType.Float),
                        new SqlParameter("@orderCreatTime", SqlDbType.DateTime),
                        new SqlParameter("@orderStatus", SqlDbType.Int,4),
                        new SqlParameter("@conn19dianOrderType", SqlDbType.Int,4),
                        new SqlParameter("@connId", SqlDbType.BigInt,8),
                        new SqlParameter("@subject",SqlDbType.NVarChar,500),
                        new SqlParameter("@customerId",SqlDbType.BigInt,8)
                    };
                    parameters[0].Value = alipayOrder.totalFee;
                    parameters[1].Value = alipayOrder.orderCreatTime;
                    parameters[2].Value = alipayOrder.orderStatus;
                    parameters[3].Value = alipayOrder.conn19dianOrderType;
                    parameters[4].Value = alipayOrder.connId;
                    parameters[5].Value = alipayOrder.subject;
                    parameters[6].Value = alipayOrder.customerId;

                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return 0;
                }
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt64(obj);
                }
            }
        }
        /// <summary>
        /// 修改支付宝订单信息
        /// orderPayTime,orderStatus,totalFee,aliTreadNo,aliBuyerEmail
        /// </summary>
        /// <param name="alipayOrder"></param>
        /// <returns></returns>
        public bool UpdateAlipayOrder(AlipayOrderInfo alipayOrder)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("update AlipayOrderInfo set ");
                    strSql.Append("totalFee=@totalFee,orderPayTime=@orderPayTime,orderStatus=@orderStatus,");
                    strSql.Append(" aliTradeNo=@aliTradeNo,aliBuyerEmail=@aliBuyerEmail");
                    strSql.Append(" where alipayOrderId=@alipayOrderId ");

                    SqlParameter[] parameters = {
                        new SqlParameter("@totalFee",SqlDbType.Float),
                        new SqlParameter("@orderPayTime",SqlDbType.DateTime),
                        new SqlParameter("@orderStatus",SqlDbType.Int,4),
                        new SqlParameter("@aliTradeNo",SqlDbType.NVarChar,32),
                        new SqlParameter("@aliBuyerEmail",SqlDbType.NVarChar,100),
                        new SqlParameter("@alipayOrderId",SqlDbType.BigInt,8)};
                    parameters[0].Value = alipayOrder.totalFee;
                    parameters[1].Value = alipayOrder.orderPayTime;
                    parameters[2].Value = alipayOrder.orderStatus;
                    parameters[3].Value = alipayOrder.aliTradeNo;
                    parameters[4].Value = alipayOrder.aliBuyerEmail;
                    parameters[5].Value = alipayOrder.alipayOrderId;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (System.Exception)
                {
                    return false;
                }
                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 更新第三方订单的状态
        /// </summary>
        /// <param name="outTradeNo"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateOrderStatus(string outTradeNo, VAAlipayOrderStatus status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AlipayOrderInfo");
            strSql.Append(" set orderStatus = @orderStatus");
            strSql.Append(" where alipayOrderId=@alipayOrderId");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@orderStatus", SqlDbType.Int),
                new SqlParameter("@alipayOrderId", SqlDbType.BigInt)
                };
                para[0].Value = (int)status;
                para[1].Value = outTradeNo;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                    if (i > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 根据支付宝订单查询对应的未支付的订单信息
        /// </summary>
        /// <param name="alipayOrderId"></param>
        /// <returns></returns>
        public DataTable SelectAlipayOrder(long alipayOrderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A.alipayOrderId,A.totalFee,A.orderCreatTime,A.orderPayTime,A.orderStatus,A.conn19dianOrderType,");
            strSql.Append("A.connId,A.subject,A.aliTradeNo,A.aliBuyerEmail,A.customerId from AlipayOrderInfo as A where");
            strSql.Append(" A.alipayOrderId = @alipayOrderId");
            strSql.AppendFormat(" and  A.orderStatus = {0}", (int)VAAlipayOrderStatus.NOT_PAID);
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@alipayOrderId", SqlDbType.BigInt) { Value = alipayOrderId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据点单Id查询相应的支付宝订单信息
        /// </summary>
        /// <param name="connId">点单Id</param>
        /// <returns></returns>
        public DataTable SelectAlipayOrderByConnId(long connId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A.alipayOrderId,A.totalFee,A.orderCreatTime,A.orderPayTime,A.orderStatus,A.conn19dianOrderType,");
            strSql.Append("A.connId,A.subject,A.aliTradeNo,A.aliBuyerEmail,A.customerId from AlipayOrderInfo as A where");
            strSql.AppendFormat(" A.connId = {0} and A.orderStatus = '{1}'", connId, (int)VAAlipayOrderStatus.PAID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据我方交易号查询支付订单
        /// </summary>
        /// <param name="alipayOrderId"></param>
        /// <returns></returns>
        public DataTable SelectAlipayOrderById(long alipayOrderId)
        {
            const string strSql = @"select A.alipayOrderId,A.totalFee,A.orderCreatTime,A.orderPayTime,A.orderStatus,A.conn19dianOrderType,
            A.connId,A.subject,A.aliTradeNo,A.aliBuyerEmail,A.customerId from AlipayOrderInfo as A where A.alipayOrderId =@alipayOrderId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@alipayOrderId",SqlDbType.BigInt){ Value = alipayOrderId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }
    }
}
