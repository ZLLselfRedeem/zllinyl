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
    public class SqlServerTenpayRefundOrderRepository : SqlServerRepositoryBase, ITenpayRefundOrderRepository
    {
        public SqlServerTenpayRefundOrderRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public void Add(TenpayRefundOrder tenpayRefundOrder)
        {
            const string cmdText = @"INSERT INTO [dbo].[TenpayRefundOrder]
                                           ([OutRefundId]
                                           ,[OutTradeNo]
                                           ,[WechatPrePayId]
                                           ,[RefundId]
                                           ,[RefundFee]
                                           ,[RefundChannel]
                                           ,[OpUserId]
                                           ,[RecvUserId]
                                           ,[ReccvUserName]
                                           ,[Status]
                                           ,[CretaeTime],[preOrder19dianId],[OriginalRoadRefundInfoId])
                                     VALUES
                                           (@OutRefundId
                                           ,@OutTradeNo
                                           ,@WechatPrePayId
                                           ,@RefundId
                                           ,@RefundFee
                                           ,@RefundChannel
                                           ,@OpUserId
                                           ,@RecvUserId
                                           ,@ReccvUserName
                                           ,@Status
                                           ,@CretaeTime,@preOrder19dianId,@OriginalRoadRefundInfoId)";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@OutRefundId", SqlDbType.VarChar,32){Value = tenpayRefundOrder.OutRefundId},
                new SqlParameter("@OutTradeNo", SqlDbType.BigInt){Value = tenpayRefundOrder.OutTradeNo},
                new SqlParameter("@WechatPrePayId", SqlDbType.VarChar,28){Value = tenpayRefundOrder.WechatPrePayId},
                new SqlParameter("@RefundId", SqlDbType.VarChar,28){Value = tenpayRefundOrder.RefundId},
                new SqlParameter("@RefundFee", SqlDbType.Float){Value = tenpayRefundOrder.RefundFee},
                new SqlParameter("@RefundChannel", SqlDbType.Int){Value = tenpayRefundOrder.RefundChannel},
                new SqlParameter("@OpUserId", SqlDbType.VarChar, 20){Value = tenpayRefundOrder.OpUserId},
                new SqlParameter("@RecvUserId",SqlDbType.NVarChar,64){Value = tenpayRefundOrder.RecvUserId}, 
                new SqlParameter("@ReccvUserName",SqlDbType.NVarChar,32){Value = tenpayRefundOrder.ReccvUserName},
                new SqlParameter("@Status", SqlDbType.Int){Value = tenpayRefundOrder.Status},
                new SqlParameter("@CretaeTime",SqlDbType.DateTime){Value = tenpayRefundOrder.CretaeTime}, 
                new SqlParameter("@preOrder19dianId", SqlDbType.BigInt){Value = tenpayRefundOrder.PreOrder19dianId}, 
                new SqlParameter("@OriginalRoadRefundInfoId",SqlDbType.BigInt){Value =tenpayRefundOrder.OriginalRoadRefundInfoId }, 
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);

        }

        public TenpayRefundOrder GetTenpayRefundOrderByOutTradeNo(long orderId)
        {
            const string cmdText = @"SELECT * FROM [dbo].[TenpayRefundOrder] WHERE [OutTradeNo]=@OutTradeNo";
            SqlParameter cmdParm = new SqlParameter("@OutTradeNo", SqlDbType.BigInt) { Value = orderId };

            TenpayRefundOrder tenpayRefundOrder = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    tenpayRefundOrder = SqlHelper.GetEntity<TenpayRefundOrder>(dr);
                }
            }
            return tenpayRefundOrder;
        }

        public TenpayRefundOrder GetTenpayRefundOrderByOrder(long orderId)
        {
            const string cmdText = @"SELECT * FROM [dbo].[TenpayRefundOrder] WHERE [preOrder19dianId]=@preOrder19dianId";
            SqlParameter cmdParm = new SqlParameter("@preOrder19dianId", SqlDbType.BigInt) { Value = orderId };

            TenpayRefundOrder tenpayRefundOrder = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    tenpayRefundOrder = SqlHelper.GetEntity<TenpayRefundOrder>(dr);
                }
            }
            return tenpayRefundOrder;
        }

        public TenpayRefundOrder GeTenpayRefundOrderByOriginalRoadRefundInfo(long originalRoadRefundInfoId)
        {
            const string cmdText = @"SELECT * FROM [dbo].[TenpayRefundOrder] WHERE [OriginalRoadRefundInfoId]=@originalRoadRefundInfoId";
            SqlParameter cmdParm = new SqlParameter("@originalRoadRefundInfoId", SqlDbType.BigInt) { Value = originalRoadRefundInfoId };

            TenpayRefundOrder tenpayRefundOrder = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    tenpayRefundOrder = SqlHelper.GetEntity<TenpayRefundOrder>(dr);
                }
            }
            return tenpayRefundOrder;
        }

        public TenpayRefundOrder GeTenpayRefundOrderByRefundIdP(string refundId)
        {
            const string cmdText = @"SELECT * FROM [dbo].[TenpayRefundOrder] WHERE [OutRefundId]=@OutRefundId";
            SqlParameter cmdParm = new SqlParameter("@OutRefundId", SqlDbType.VarChar, 32) { Value = refundId };

            TenpayRefundOrder tenpayRefundOrder = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    tenpayRefundOrder = SqlHelper.GetEntity<TenpayRefundOrder>(dr);
                }
            }
            return tenpayRefundOrder;
        }

        public void Update(TenpayRefundOrder tenpayRefundOrder)
        {
            const string cmdText = @"UPDATE [dbo].[TenpayRefundOrder]
                                       SET [OutRefundId] = @OutRefundId
                                          ,[OutTradeNo] = @OutTradeNo
                                          ,[WechatPrePayId] = @WechatPrePayId
                                          ,[RefundId] = @RefundId
                                          ,[RefundFee] = @RefundFee
                                          ,[RefundChannel] = @RefundChannel
                                          ,[OpUserId] = @OpUserId
                                          ,[RecvUserId] = @RecvUserId
                                          ,[ReccvUserName] = @ReccvUserName
                                          ,[Status] = @Status      
                                          ,[ChangeStatusTime] = @ChangeStatusTime
                                          ,[preOrder19dianId]=@preOrder19dianId
                                     WHERE [Id]=@Id";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@OutRefundId", SqlDbType.VarChar, 32) {Value = tenpayRefundOrder.OutRefundId},
                new SqlParameter("@OutTradeNo", SqlDbType.BigInt) {Value = tenpayRefundOrder.OutTradeNo},
                new SqlParameter("@WechatPrePayId", SqlDbType.VarChar, 28) {Value = tenpayRefundOrder.WechatPrePayId},
                new SqlParameter("@RefundId", SqlDbType.VarChar, 28) {Value = tenpayRefundOrder.RefundId},
                new SqlParameter("@RefundFee", SqlDbType.Float) {Value = tenpayRefundOrder.RefundFee},
                new SqlParameter("@RefundChannel", SqlDbType.Int) {Value = tenpayRefundOrder.RefundChannel},
                new SqlParameter("@OpUserId", SqlDbType.VarChar,20) {Value = tenpayRefundOrder.OpUserId},
                new SqlParameter("@RecvUserId", SqlDbType.NVarChar, 64) {Value = tenpayRefundOrder.RecvUserId},
                new SqlParameter("@ReccvUserName", SqlDbType.NVarChar, 32) {Value = tenpayRefundOrder.ReccvUserName},
                new SqlParameter("@Status", SqlDbType.Int) {Value = tenpayRefundOrder.Status},
                new SqlParameter("@ChangeStatusTime", SqlDbType.DateTime)
                {
                    Value =
                        tenpayRefundOrder.ChangeStatusTime.HasValue
                            ? (object) tenpayRefundOrder.ChangeStatusTime.Value
                            : null
                },
                new SqlParameter("@Id", SqlDbType.BigInt){Value =tenpayRefundOrder.Id }, 
                new SqlParameter("@preOrder19dianId", SqlDbType.BigInt){Value = tenpayRefundOrder.PreOrder19dianId}, 
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

        public IEnumerable<TenpayRefundOrder> GetProcessingenpayRefundOrders()
        {
            const string cmdText = @"SELECT * FROM [dbo].[TenpayRefundOrder] WHERE [Status]=@Status";
            SqlParameter cmdParm = new SqlParameter("@Status", SqlDbType.Int) { Value = TenpayRefundStatus.退款处理中 };

            List<TenpayRefundOrder> tenpayRefundOrders = new List<TenpayRefundOrder>();
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                while (dr.Read())
                {
                    tenpayRefundOrders.Add(SqlHelper.GetEntity<TenpayRefundOrder>(dr));
                }
            }
            return tenpayRefundOrders;
        }
    }
}
