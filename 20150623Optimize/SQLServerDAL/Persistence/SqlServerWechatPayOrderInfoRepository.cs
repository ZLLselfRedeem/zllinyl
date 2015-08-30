using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerWechatPayOrderInfoRepository : SqlServerRepositoryBase, IWechatPayOrderInfoRepository
    {
        public SqlServerWechatPayOrderInfoRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public WechatPayOrderInfo GetWechatPayOrderInfoByOrderId(long orderId)
        {
            const string cmdText = @"SELECT TOP 1 * FROM [dbo].[WechatPayOrderInfo] WHERE [connId]=@OrderId AND orderStatus=2 ORDER BY orderPayTime DESC";

            SqlParameter cmdParm = new SqlParameter("@OrderId", SqlDbType.BigInt) { Value = orderId };

            WechatPayOrderInfo wechatPayOrderInfo = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    wechatPayOrderInfo = SqlHelper.GetEntity<WechatPayOrderInfo>(dr);
                }
            }
            return wechatPayOrderInfo;
        }

        public WechatPayOrderInfo GetWechatPayOrderInfoByOutTradeNo(long outTradeNo)
        {
            const string cmdText = @"select top 1 * from WechatPayOrderInfo where outTradeNo = @outTradeNo";

            SqlParameter[] para = new SqlParameter[] { 
             new SqlParameter("@outTradeNo",SqlDbType.BigInt)
            };
            para[0].Value = outTradeNo;

            WechatPayOrderInfo wechatPayOrderInfo = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, para))
            {
                if (dr.Read())
                {
                    wechatPayOrderInfo = SqlHelper.GetEntity<WechatPayOrderInfo>(dr);
                }
            }
            return wechatPayOrderInfo;
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

                int i = SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, strSql.ToString(), para);
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
