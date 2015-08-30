using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 微信支付订单数据处理层
    /// </summary>
    public class WechatPayManager
    {
        /// <summary>
        /// 新增微信支付订单信息
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public long InsertWechatPayOrder(WechatPayOrderInfo order)
        {
            object obj = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into dbo.WechatPayOrderInfo(");
                strSql.Append("totalFee,orderCreateTime,orderStatus,conn19dianOrderType,");
                strSql.Append("connId,body,customerId)");
                strSql.Append(" values(");
                strSql.Append("@totalFee,@orderCreateTime,@orderStatus,@conn19dianOrderType,");
                strSql.Append("@connId,@body,@customerId)");
                strSql.Append(" select @@identity");

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@totalFee", SqlDbType.Float),
                    new SqlParameter("@orderCreateTime", SqlDbType.DateTime),
                    new SqlParameter("@orderStatus", SqlDbType.Int),
                    new SqlParameter("@conn19dianOrderType", SqlDbType.Int),
                    new SqlParameter("@connId", SqlDbType.Int),
                    new SqlParameter("@body", SqlDbType.NVarChar, 128),
                    new SqlParameter("@customerId", SqlDbType.Int)
                };
                para[0].Value = order.totalFee;
                para[1].Value = order.orderCreateTime;
                para[2].Value = order.orderStatus;
                para[3].Value = order.conn19dianOrderType;
                para[4].Value = order.connId;
                para[5].Value = order.body;
                para[6].Value = order.customerId;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
                }
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

        /// <summary>
        /// 根据outTradeNo更新prepayid
        /// </summary>
        /// <param name="outTradeNo">我方产生的订单编号</param>
        /// <param name="PrePayId">微信返回的相应的订单编号</param>
        /// <returns></returns>
        public int UpdateWechatPrePayId(long outTradeNo, string PrePayId)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update WechatPayOrderInfo");
                strSql.Append(" set wechatPrePayId = @wechatPrePayId");
                strSql.Append(" where outTradeNo = @outTradeNo");

                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@wechatPrePayId", SqlDbType.NVarChar, 50),
                new SqlParameter("@outTradeNo", SqlDbType.BigInt)
            };
                para[0].Value = PrePayId;
                para[1].Value = outTradeNo;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                    return i;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 根据通知结果更新微信支付订单信息
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int UpdateWechatOrder(WechatPayOrderInfo order)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update WechatPayOrderInfo");
                strSql.Append(" set totalFee = @totalFee,");
                strSql.Append(" orderPayTime = @orderPayTime,");
                strSql.Append(" orderStatus = @orderStatus,");
                strSql.Append(" openId = @openId,");
                strSql.Append(" bankType = @bankType,");
                strSql.Append(" bankBillno = @bankBillno,");
                strSql.Append(" notifyId = @notifyId,");
                strSql.Append(" transactionId = @transactionId");
                strSql.Append(" where outTradeNo = @outTradeNo");

                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@totalFee", SqlDbType.Float),
                new SqlParameter("@orderPayTime", SqlDbType.DateTime),
                new SqlParameter("@orderStatus", SqlDbType.Int),
                new SqlParameter("@outTradeNo", SqlDbType.BigInt),
                new SqlParameter("@openId",SqlDbType.NVarChar, 100),
                new SqlParameter("@bankType",SqlDbType.NVarChar, 16),
                new SqlParameter("@bankBillno",SqlDbType.NVarChar, 32),
                new SqlParameter("@notifyId",SqlDbType.NVarChar, 128),
                new SqlParameter("@transactionId",SqlDbType.NVarChar, 28)
            };
                para[0].Value = order.totalFee;
                para[1].Value = order.orderPayTime;
                para[2].Value = order.orderStatus;
                para[3].Value = order.outTradeNo;
                para[4].Value = order.openId;
                para[5].Value = order.bankType;
                para[6].Value = order.bankBillno;
                para[7].Value = order.notifyId;
                para[8].Value = order.transactionId;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                    return i;
                }
            }
            catch (Exception)
            {
                return 0;
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
            strSql.Append("update WechatPayOrderInfo");
            strSql.Append(" set orderStatus = @orderStatus");
            strSql.Append(" where outTradeNo=@outTradeNo");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@orderStatus", SqlDbType.Int),
                new SqlParameter("@outTradeNo", SqlDbType.BigInt)
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
        /// 根据outTradeNo查询微信支付订单信息
        /// </summary>
        /// <param name="outTradeNo"></param>
        /// <returns></returns>
        public DataTable QueryWechatPayOrder(string outTradeNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select outTradeNo,totalFee,orderCreateTime,orderStatus,conn19dianOrderType,connId,body,wechatPrePayId,customerId");
            strSql.Append(" from WechatPayOrderInfo");
            strSql.AppendFormat(" where outTradeNo = @outTradeNo and orderStatus = {0}", (int)VAAlipayOrderStatus.NOT_PAID);

            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@outTradeNo", SqlDbType.BigInt)
            };
            para[0].Value = outTradeNo;

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据客户电话号码查询其重复支付的点单
        /// </summary>
        /// <param name="customerPhoneNumber"></param>
        /// <returns></returns>
        public DataTable QueryRepeatedPay(string customerPhoneNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select '微信' payType,ww.outTradeNo,UserName,mobilePhoneNumber,ww.totalFee,
            ww.orderCreateTime,ww.orderStatus,ww.orderPayTime,ww.body,ww.connId,ww.outTradeNo tradeNo from
            (select connId,customerId,UserName,mobilePhoneNumber from(
            select wx.connId,wx.customerId,c.UserName,c.mobilePhoneNumber
            from WechatPayOrderInfo wx           
            inner join CustomerInfo c on wx.customerId = c.CustomerID");
            strSql.AppendFormat(" where wx.orderStatus != '1' and c.mobilePhoneNumber like '{0}%'", customerPhoneNumber);
            strSql.Append(@" union all            
            select ali.connId,ali.customerId,UserName,mobilePhoneNumber
            from AlipayOrderInfo ali           
            inner join CustomerInfo c on ali.customerId = c.CustomerID");
            strSql.AppendFormat(" where ali.orderStatus != '1' and c.mobilePhoneNumber like '{0}%') a", customerPhoneNumber);
            strSql.Append(@" group by connId,customerId,UserName,mobilePhoneNumber
            having COUNT(connId)>1) b
            inner join WechatPayOrderInfo ww on b.connId = ww.connId and ww.orderStatus!=1 
            union
            select '支付宝' payType,alip.alipayOrderId outTradeNo,
            UserName,mobilePhoneNumber,alip.totalFee,alip.orderCreatTime orderCreateTime,
            alip.orderStatus,alip.orderPayTime,alip.subject body,alip.connId,alip.alipayOrderId tradeNo
            from
            (select connId,customerId,UserName,mobilePhoneNumber from(
            select wx.connId,wx.customerId,c.UserName,c.mobilePhoneNumber
            from WechatPayOrderInfo wx           
            inner join CustomerInfo c on wx.customerId = c.CustomerID");
            strSql.AppendFormat(" where wx.orderStatus != '1' and c.mobilePhoneNumber like '{0}%'", customerPhoneNumber);
            strSql.Append(@" union all            
            select ali.connId,ali.customerId,UserName,mobilePhoneNumber
            from AlipayOrderInfo ali           
            inner join CustomerInfo c on ali.customerId = c.CustomerID");
            strSql.AppendFormat(" where ali.orderStatus != '1' and c.mobilePhoneNumber like '{0}%') a", customerPhoneNumber);
            strSql.Append(@" group by connId,customerId,UserName,mobilePhoneNumber
            having COUNT(connId)>1) b
            inner join AlipayOrderInfo alip on b.connId = alip.connId
            and alip.orderStatus!=1");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
    }
}
