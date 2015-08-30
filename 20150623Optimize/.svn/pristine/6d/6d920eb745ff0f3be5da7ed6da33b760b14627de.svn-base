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
    public class RedEnvelopeTestManager
    {

        /// <summary>
        /// 红包压力测试--老用户数据
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCustomerPhone()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 2000 mobilePhoneNumber from CustomerInfo where mobilePhoneNumber is not null and mobilePhoneNumber!=''");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        public DataTable SelectTestCustomer()
        {
            const string str = @" select a.cookie,a.uuid from (
 select ROW_NUMBER() over(partition by c.cookie order by conn.updateTime desc) rowid, 
 cookie,uuid,conn.updateTime
  from CustomerInfo c inner join CustomerConnDevice conn on c.CustomerID=conn.customerId
  inner join DeviceInfo d on conn.deviceId=d.deviceId
  where mobilePhoneNumber is not null and mobilePhoneNumber!='' and isnull(cookie,'')!='' and isnull(uuid,'')!=''
  and RegisterDate>'2014-11-16'
 ) a where a.rowid=1";
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, str);
            return ds.Tables[0];
        }

        public bool BatchInsertTest(DataTable dt)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    SqlTransaction sqlbulkTransaction = conn.BeginTransaction();
                    SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, sqlbulkTransaction);
                    sqlbulkcopy.DestinationTableName = "Test001";//数据库中的表名
                    sqlbulkcopy.BulkCopyTimeout = 30;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        sqlbulkcopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                    }
                    sqlbulkcopy.WriteToServer(dt);
                    sqlbulkTransaction.Commit();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable SelectCustomer(string mobilePhoneNumber)
        {
            string strSql = "select preOrderTotalAmount,preOrderTotalQuantity,currentPlatformVipGrade,mobilePhoneNumber,UserName from CustomerInfo where mobilePhoneNumber=@mobilePhoneNumber";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar,50){ Value = mobilePhoneNumber}
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询感恩节待退款的订单
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public List<RefundOrder> SelectRefundOrder(int activityId)
        {
            List<RefundOrder> refundOrder = new List<RefundOrder>();
//            string strSql = @" select pp.preOrder19dianId,b.mobilePhoneNumber from (
// select connn.preOrder19dianId,a.mobilePhoneNumber, SUM(connn.currectUsedAmount) Amount from (
// select conn.preOrder19dianId,r.mobilePhoneNumber
//  from RedEnvelopeConnPreOrder conn
// inner join RedEnvelope r 
// on conn.redEnvelopeId=r.redEnvelopeId
// and r.activityId=@activityId
// group by conn.preOrder19dianId,r.mobilePhoneNumber) a
// inner join RedEnvelopeConnPreOrder connn on a.preOrder19dianId = connn.preOrder19dianId
// group by connn.preOrder19dianId,a.mobilePhoneNumber
// ) b
// inner join PreOrder19dian pp on b.preOrder19dianId = pp.preOrder19dianId
// and ISNULL(pp.isPaid,0)=1 and ISNULL(pp.isShopConfirmed,0)=0
// and ISNULL(pp.refundMoneySum,0)=0
// and CONVERT(numeric(18,2),pp.prePaidSum)=CONVERT(numeric(18,2), b.Amount)";

            string strSql = @"select conn.preOrder19dianId,r.mobilePhoneNumber,r.activityId
  from RedEnvelopeConnPreOrder conn
 inner join RedEnvelope r 
 on conn.redEnvelopeId=r.redEnvelopeId
 and r.activityId=@activityId
  inner join PreOrder19dian p on conn.preOrder19dianId = p.preOrder19dianId
and ISNULL(p.isPaid,0)=1 and ISNULL(p.isShopConfirmed,0)=0
 and ISNULL(p.refundMoneySum,0)=0
 and r.Amount>2  and p.prePaidSum<50";

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@activityId", SqlDbType.Int){ Value = activityId}
            };
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    refundOrder.Add(sdr.GetEntity<RefundOrder>());
                }
            }
            return refundOrder;
        }
        /// <summary>
        /// 查询某个已支付点单对应的红包关联表
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public List<RedEnvelopeConnOrder> SelectRedEnvelopeConnPreOrder(long preOrder19dianId)
        {
            List<RedEnvelopeConnOrder> connPreOrder = new List<RedEnvelopeConnOrder>();
            string strSql = @"select preOrder19dianId,conn.redEnvelopeId,conn.currectUsedAmount,a.activityType
                                    from RedEnvelopeConnPreOrder conn 
                                    inner join RedEnvelope r on conn.redEnvelopeId=r.redEnvelopeId
                                    inner join Activity a on r.activityId=a.activityId 
                                    where conn.preOrder19dianId=@preOrder19dianId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt){ Value = preOrder19dianId}
            };
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    connPreOrder.Add(sdr.GetEntity<RedEnvelopeConnOrder>());
                }
            }
            return connPreOrder;
        }

        public List<RedEnvelopeConnOrder2> SelectRedEnvelopeConnPreOrder2(long preOrder19dianId)
        {
            List<RedEnvelopeConnOrder2> connPreOrder = new List<RedEnvelopeConnOrder2>();
            string strSql = @"select preOrder19dianId,conn.redEnvelopeId,conn.currectUsedAmount,a.activityType,a.activityId
                                    from RedEnvelopeConnPreOrder conn 
                                    inner join RedEnvelope r on conn.redEnvelopeId=r.redEnvelopeId
                                    inner join Activity a on r.activityId=a.activityId 
                                    where conn.preOrder19dianId=@preOrder19dianId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt){ Value = preOrder19dianId}
            };
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    connPreOrder.Add(sdr.GetEntity<RedEnvelopeConnOrder2>());
                }
            }
            return connPreOrder;
        }

        /// <summary>
        /// 修改红包详情使用金额
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <param name="backAmount"></param>
        /// <returns></returns>
        //public bool updateRedEnvelopePaidAmount(long preOrder19dianId, double backAmount)
        //{
        //    string strSql = "update RedEnvelopeDetails set usedAmount=ISNULL(usedAmount,0)-@backAmount where preOrder19dianId=@preOrder19dianId and stateType=" + (int)VARedEnvelopeStateType.已使用;
        //    SqlParameter[] para = new SqlParameter[] { 
        //    new SqlParameter("@backAmount", SqlDbType.Float){ Value = backAmount},
        //    new SqlParameter("@preOrder19dianId", SqlDbType.BigInt){ Value = preOrder19dianId}
        //    };
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
        //        if (i > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        /// <summary>
        /// 将已使用的红包改为已生效，未使用金额改掉
        /// 将退款的天天红包及大红包还给用户
        /// </summary>
        /// <param name="strRedEnvelopeId"></param>
        /// <returns></returns>
        public bool UpdateRedEnvelopeStatus(long strRedEnvelopeId, double unusedAmount)
        {
            string strSql = "update RedEnvelope set isExecuted=" + (int)VARedEnvelopeStateType.已生效 + ",unusedAmount=isnull(unusedAmount,0)+@unusedAmount where redEnvelopeId =@strRedEnvelopeId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@unusedAmount", SqlDbType.Float ){ Value = unusedAmount},
            new SqlParameter("@strRedEnvelopeId", SqlDbType.BigInt ){ Value = strRedEnvelopeId}
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
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
        /// <summary>
        /// 更新用户红包表，将天天红包和大红包还回去
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="backExecutedRedEnvelopeAmount"></param>
        /// <returns></returns>
        public bool UpdateCustomerRedEnvelope(string mobilePhoneNumber, double backExecutedRedEnvelopeAmount)
        {
            string strSql = "update CustomerInfo set executedRedEnvelopeAmount=ISNULL(executedRedEnvelopeAmount,0)+@backExecutedRedEnvelopeAmount where mobilePhoneNumber=@mobilePhoneNumber";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@backExecutedRedEnvelopeAmount", SqlDbType.Float ){ Value = backExecutedRedEnvelopeAmount},
            new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar,50 ){ Value = mobilePhoneNumber}
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
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
    }
}
