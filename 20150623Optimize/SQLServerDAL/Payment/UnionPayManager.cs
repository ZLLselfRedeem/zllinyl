using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.IO;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 银联支付相关数据库操作类
    /// </summary>
    public class UnionPayManager
    {
        /// <summary>
        /// 新增银联订单信息
        /// </summary>
        /// <param name="UnionPayOrder"></param>
        /// <returns></returns>
        public long InsertUnionpayOrder(UnionPayInfo UnionPayOrder)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into UnionPayOrderInfo(");
                    strSql.Append("merchantOrderTime,orderStatus,conn19dianOrderType,connId,merchantOrderAmt,merchantOrderDesc,customerId)");
                    strSql.Append(" values (");
                    strSql.Append("@merchantOrderTime,@orderStatus,@conn19dianOrderType,@connId,@merchantOrderAmt,@merchantOrderDesc,@customerId)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					    new SqlParameter("@merchantOrderTime", SqlDbType.DateTime),
                        new SqlParameter("@orderStatus", SqlDbType.Int),
                        new SqlParameter("@conn19dianOrderType", SqlDbType.Int),
                        new SqlParameter("@connId", SqlDbType.BigInt),
                        new SqlParameter("@merchantOrderAmt",SqlDbType.Float),
                        new SqlParameter("@merchantOrderDesc",SqlDbType.NVarChar),
                        new SqlParameter("@customerId",SqlDbType.BigInt)
                    };
                    parameters[0].Value = UnionPayOrder.merchantOrderTime;
                    parameters[1].Value = (int)UnionPayOrder.orderStatus;
                    parameters[2].Value = (int)UnionPayOrder.conn19dianOrderType;
                    parameters[3].Value = UnionPayOrder.connId;
                    parameters[4].Value = UnionPayOrder.merchantOrderAmt;
                    parameters[5].Value = UnionPayOrder.merchantOrderDesc;
                    parameters[6].Value = UnionPayOrder.customerId;

                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch //(Exception ex)
                {
                    //StreamWriter sw = new StreamWriter("D:\\log.txt", true);
                    //sw.WriteLine("数据库插入数据出错:"+ex.Message);
                    //sw.Close();
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
        /// 修改订单状态
        /// orderPayTime,orderStatus,totalFee,aliTreadNo,aliBuyerEmail
        /// </summary>
        /// <param name="alipayOrder"></param>
        /// <returns></returns>
        public bool UpdateUnionpayOrderStatus(int orderStatus, DateTime orderPayTime, long merchantOrderId, string cupsQid)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("update UnionPayOrderInfo set ");
                    strSql.Append("orderPayTime=@orderPayTime,orderStatus=@orderStatus,cupsQid=@cupsQid");
                    strSql.Append(" where UnionPayOrderInfoID=@merchantOrderId ");

                    SqlParameter[] parameters = {
                        new SqlParameter("@orderPayTime", SqlDbType.DateTime),
                        new SqlParameter("@orderStatus", SqlDbType.Int,4),
                        new SqlParameter("@merchantOrderId", SqlDbType.BigInt,8),
                        new SqlParameter("@cupsQid",SqlDbType.NVarChar,100)};
                    parameters[0].Value = orderPayTime;
                    parameters[1].Value = orderStatus;
                    parameters[2].Value = merchantOrderId;
                    parameters[3].Value = cupsQid;

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
        /// 根据银联订单查询对应的未支付的订单信息
        /// </summary>
        /// <param name="unionPayOrderId"></param>
        /// <returns></returns>
        public DataTable SelectunionPayOrder(long unionPayOrderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [UnionPayOrderInfoID],[merchantOrderTime],[orderStatus] ,[conn19dianOrderType],[connId] ,[merchantOrderAmt],[merchantOrderDesc],[customerId] ");
            strSql.Append("from UnionPayOrderInfo where");
            strSql.AppendFormat(" UnionPayOrderInfoID = {0}", unionPayOrderId);
            strSql.AppendFormat(" and orderStatus = {0}", (int)VAAlipayOrderStatus.NOT_PAID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }


    }
}
