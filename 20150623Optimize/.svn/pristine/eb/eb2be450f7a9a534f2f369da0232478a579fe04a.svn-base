using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 支付宝退款数据处理层
    /// 创建日期：2014-6-20
    /// </summary>
    public class AliRefundManager
    {
        /// <summary>
        /// 新增支付宝退款记录
        /// </summary>
        /// <param name="refundOrder"></param>
        /// <returns></returns>
        public long InsertAliRefundOrder(AliRefundOrderInfo refundOrder)
        {
            object obj = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into AliRefundOrderInfo");
            strSql.Append(" (batchNo,refundDate,batchNum,aliTradeNo,refundSum,refundReason,refundStatus,connId,originalId,customerId,notifyStatus)");
            strSql.Append(" values (@batchNo,@refundDate,@batchNum,@aliTradeNo,@refundSum,@refundReason,@refundStatus,@connId,@originalId,@customerId,@notifyStatus)");
            strSql.Append(" select @@identity");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@batchNo",SqlDbType.NVarChar,100),
                new SqlParameter("@refundDate",SqlDbType.DateTime),
                new SqlParameter("@batchNum",SqlDbType.Int),
                new SqlParameter("@aliTradeNo",SqlDbType.NVarChar,400),
                new SqlParameter("@refundSum",SqlDbType.Float),
                new SqlParameter("@refundReason",SqlDbType.NVarChar,100),
                new SqlParameter("@refundStatus",SqlDbType.Int),
                new SqlParameter("@connId",SqlDbType.BigInt),
                new SqlParameter("@originalId",SqlDbType.BigInt),
                new SqlParameter("@customerId",SqlDbType.BigInt),
                new SqlParameter("@notifyStatus",SqlDbType.Int)
                };
                para[0].Value = refundOrder.batchNo;
                para[1].Value = refundOrder.refundDate;
                para[2].Value = refundOrder.batchNum;
                para[3].Value = refundOrder.aliTradeNo;
                para[4].Value = refundOrder.refundSum;
                para[5].Value = refundOrder.refundReason;
                para[6].Value = refundOrder.refundStatus;
                para[7].Value = refundOrder.connId;
                para[8].Value = refundOrder.originalId;
                para[9].Value = refundOrder.customerId;
                para[10].Value = refundOrder.notifyStatus;

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
        /// 收到异步通知时，更新支付宝退款记录
        /// </summary>
        /// <param name="refundOrder"></param>
        /// <returns></returns>
        public bool UpdateAliRefundOrder(AliRefundOrderInfo refundOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AliRefundOrderInfo set ");
            strSql.Append("notifyTime = @notifyTime,");
            strSql.Append("notifyType = @notifyType,");
            strSql.Append("notifyId = @notifyId,");
            strSql.Append("successNum = @successNum,");
            strSql.Append("lastUpdateTime = @lastUpdateTime,");
            strSql.Append("notifyStatus = @notifyStatus");
            strSql.Append(" where batchNo = @batchNo");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@notifyTime",SqlDbType.DateTime),
                new SqlParameter("@notifyType",SqlDbType.NVarChar, 50),
                new SqlParameter("@notifyId",SqlDbType.NVarChar, 50),
                new SqlParameter("@successNum",SqlDbType.Int),
                new SqlParameter("@lastUpdateTime",SqlDbType.DateTime),
                new SqlParameter("@notifyStatus",SqlDbType.Int),
                new SqlParameter("@batchNo",SqlDbType.NVarChar, 100)
                };
                para[0].Value = refundOrder.notifyTime;
                para[1].Value = refundOrder.notifyType;
                para[2].Value = refundOrder.notifyId;
                para[3].Value = refundOrder.successNum;
                para[4].Value = refundOrder.lastUpdateTime;
                para[5].Value = refundOrder.notifyStatus;
                para[6].Value = refundOrder.batchNo;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
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
        /// 更新退款单状态为退款成功
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public bool UpdateAliRefundStatus(string batchNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AliRefundOrderInfo set ");
            strSql.Append("refundStatus = @refundStatus,");
            strSql.Append("lastUpdateTime = @lastUpdateTime");
            strSql.Append(" where batchNo = @batchNo");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
             
                new SqlParameter("@refundStatus",SqlDbType.Int),
                new SqlParameter("@lastUpdateTime",SqlDbType.DateTime),
                new SqlParameter("@batchNo",SqlDbType.NVarChar, 100)
                };
                para[0].Value = (int)VAAliRefundStatus.REFUND_SUCCESS;
                para[1].Value = DateTime.Now;
                para[2].Value = batchNo;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
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
        /// 根据batchNo查询还未收到通知的退款单据
        /// </summary>
        /// <param name="batchNo"></param>
        /// <returns></returns>
        public DataTable QueryAliRefund(string batchNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select batchNo,refundDate,batchNum,aliTradeNo,refundSum,refundReason,refundStatus,connId,originalId,customerId,notifyStatus");
            strSql.Append(" from AliRefundOrderInfo");
            strSql.AppendFormat(" where batchNo = '{0}' and notifyStatus = 0", batchNo);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据支付宝交易号查询所有处于退款中的退款单据
        /// </summary>
        /// <param name="tradeNo"></param>
        /// <returns></returns>
        public DataTable QueryAliRefundByTradeNo(string tradeNo)
        {
            string strSql = @"select batchNo,refundDate,batchNum,aliTradeNo,refundSum,refundReason,refundStatus,connId,originalId,customerId,notifyStatus
from AliRefundOrderInfo
where aliTradeNo = @tradeNo and refundStatus = " + (int)VAAliRefundStatus.REFUNDING + " and notifyStatus=1 order by refundSum desc,refundDate asc";

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@tradeNo", SqlDbType.NVarChar,400) { Value = tradeNo }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据原路退款流水号查询支付宝退款记录
        /// </summary>
        /// <param name="originalId"></param>
        /// <returns></returns>
        public DataTable QueryAliRefundByOriginalId(long originalId)
        {
            const string strSql = "select * from AliRefundOrderInfo where originalId =@originalId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@originalId", SqlDbType.BigInt) { Value = originalId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询所有支付宝退款单据（已通知，未打款）
        /// </summary>
        /// <returns></returns>
        public DataTable QueryAllRefundOrder()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select batchNo,refundDate,batchNum,aliTradeNo,refundSum,refundReason,connId,originalId,customerId");
            strSql.AppendFormat(" from AliRefundOrderInfo where refundStatus='{0}' and notifyStatus=1", (int)VAAliRefundStatus.REFUNDING);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据原路退款流水号查询改订单我方交易号
        /// </summary>
        /// <param name="originalId"></param>
        /// <returns></returns>
        public string QueryTradeNo(long originalId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT PAY.alipayOrderId");
            strSql.Append(" FROM AliRefundOrderInfo REFUND INNER JOIN AlipayOrderInfo PAY");
            strSql.Append(" ON REFUND.aliTradeNo = PAY.aliTradeNo");
            strSql.AppendFormat(" AND REFUND.originalId = '{0}'", originalId);
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                conn.Open();
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString());
                if (obj == null)
                {
                    return "";
                }
                else
                {
                    return obj.ToString();
                }
            }
        }
    }
}
