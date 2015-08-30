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
    public interface IRedEnvelopeConnPreOrderManager
    {
        double GetPayOrderConsumeRedEnvelopeAmount(long preOrder19DianId);
        DataTable SelectRedEnvelopeConnPreOrder(long redEnvelopeId);
        bool BatchInsertRedEnvelopeConnPreOrder(DataTable dt);
        bool CheckPaidOrderRedEnvelopeStatus(long preOrder19DianId);
        List<long> QueryExpirePreOrder(string cookie);
    }

    /// <summary>
    /// 红包与点单关联关系数据访问
    /// </summary>
    public class RedEnvelopeConnPreOrderManager : IRedEnvelopeConnPreOrderManager
    {
        /// <summary>
        /// 查询当前点单使用红包总金额
        /// </summary>
        /// <param name="preOrder19DianId"></param>
        /// <returns></returns>
        public double GetPayOrderConsumeRedEnvelopeAmount(long preOrder19DianId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ROUND(ISNULL(sum(currectUsedAmount),0),2) consumeRedEnvelopeAmount FROM RedEnvelopeConnPreOrder where preOrder19dianId=@preOrder19dianId");
            SqlParameter[] parameters = new[] { new SqlParameter("@preOrder19dianId", preOrder19DianId) };
            double result = 0;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters))
            {
                if (dr.Read())
                {
                    result = dr[0] == DBNull.Value ? 0 : Convert.ToDouble(dr[0]);
                }
            }
            return result;
        }
        /// <summary>
        /// 根据红包Id查看其使用情况（支付过哪些点单）
        /// </summary>
        /// <param name="redEnvelopeId"></param>
        /// <returns></returns>
        public DataTable SelectRedEnvelopeConnPreOrder(long redEnvelopeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select conn.Id,conn.preOrder19dianId,conn.redEnvelopeId,conn.currectUsedAmount,conn.currectUsedTime,");
            strSql.Append(" pre.prePaidSum,pre.prePayTime,pre.refundMoneySum,shop.shopName");
            strSql.Append(" from RedEnvelopeConnPreOrder conn inner join PreOrder19dian pre");
            strSql.Append(" on conn.preOrder19dianId = pre.preOrder19dianId");
            strSql.Append(" inner join ShopInfo shop on pre.shopId = shop.shopID");
            strSql.Append(" and conn.redEnvelopeId = @redEnvelopeId");

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@redEnvelopeId", SqlDbType.BigInt) { Value = redEnvelopeId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }
        /// <summary>
        /// 批量插入点单使用红包关联
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool BatchInsertRedEnvelopeConnPreOrder(DataTable dt)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                //SqlTransaction sqlbulkTransaction = conn.BeginTransaction();
                SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(conn);
                sqlbulkcopy.DestinationTableName = "RedEnvelopeConnPreOrder";//数据库中的表名
                sqlbulkcopy.BulkCopyTimeout = 30;
                sqlbulkcopy.ColumnMappings.Add("preOrder19dianId", "preOrder19dianId");
                sqlbulkcopy.ColumnMappings.Add("redEnvelopeId", "redEnvelopeId");
                sqlbulkcopy.ColumnMappings.Add("currectUsedAmount", "currectUsedAmount");
                sqlbulkcopy.ColumnMappings.Add("currectUsedTime", "currectUsedTime");
                sqlbulkcopy.WriteToServer(dt);
                //sqlbulkTransaction.Commit();
                return true;
            }
        }

        /// <summary>
        /// 检查支付未入座点单是否有参与支付过期的红包
        /// </summary>
        /// <param name="preOrder19DianId"></param>
        /// <returns></returns>
        public bool CheckPaidOrderRedEnvelopeStatus(long preOrder19DianId)
        {
            const string strSql = @"select COUNT(Id) from PreOrder19dian A 
inner join RedEnvelopeConnPreOrder B on A.preOrder19dianId=B.preOrder19dianId
inner join RedEnvelope C on C.redEnvelopeId=B.redEnvelopeId
where A.preOrder19dianId=@preOrder19dianId and A.isPaid=1 and isnull(A.isShopConfirmed,0)=0 and C.expireTime<GETDATE()";
            SqlParameter[] parameters = new[] { new SqlParameter("@preOrder19dianId", SqlDbType.BigInt, 8) { Value = preOrder19DianId } };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameters))
            {
                if (dr.Read())
                {
                    return Convert.ToDouble(dr[0]) > 0;
                }
            }
            return false;
        }
        /// <summary>
        /// 根据用户cookie查询红包过期导致订单过期的订单号
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public List<long> QueryExpirePreOrder(string cookie)
        {
            const string strSql = @"select A.preOrder19dianId from PreOrder19dian A 
inner join RedEnvelopeConnPreOrder B on A.preOrder19dianId=B.preOrder19dianId
inner join RedEnvelope C on C.redEnvelopeId=B.redEnvelopeId
inner join customerInfo cus on A.customerId = cus.customerId
where cus.cookie=@cookie
 and A.isPaid=1 
 and isnull(A.isShopConfirmed,0)=0 and C.expireTime<GETDATE()
 and ISNULL(A.refundMoneySum,0)=0";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@cookie", SqlDbType.NVarChar, 100) { Value = cookie }
            };
            List<long> orderId = new List<long>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    orderId.Add(Convert.ToInt64(sdr[0]));
                }
            }
            return orderId;
        }
    }
}
