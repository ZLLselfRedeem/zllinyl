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
    public class SqlServerOriginalRoadRefundInfoRepository : SqlServerRepositoryBase, IOriginalRoadRefundInfoRepository
    {
        public SqlServerOriginalRoadRefundInfoRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public OriginalRoadRefundInfo GetOriginalRoadRefundInfoById(long id)
        {
            const string cmdText = @"SELECT * FROM [dbo].[OriginalRoadRefundInfo] WHERE [id]=@Id";

            SqlParameter cmdParm = new SqlParameter("@Id", SqlDbType.BigInt) { Value = id };

            OriginalRoadRefundInfo originalRoadRefundInfo = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    originalRoadRefundInfo = SqlHelper.GetEntity<OriginalRoadRefundInfo>(dr);
                }
            }

            return originalRoadRefundInfo;

        }

        public void Update(OriginalRoadRefundInfo originalRoadRefundInfo)
        {
            const string cmdText = @"UPDATE [dbo].[OriginalRoadRefundInfo]
                                       SET [type] = @type
                                          ,[connId] = @connId
                                          ,[refundAmount] = @refundAmount                                          
                                          ,[remittanceTime] = @remittanceTime
                                          ,[status] = @status
                                          ,[remitEmployee] = @remitEmployee
                                          ,[customerMobilephone] = @customerMobilephone
                                          ,[note] = @note
                                          ,[customerUserName] = @customerUserName
                                          ,[employeeId] = @employeeId
                                          ,[RefundPayType] = @RefundPayType
                                     WHERE [id]=@id";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@type", SqlDbType.Int){Value = originalRoadRefundInfo.type}, 
                new SqlParameter("@connId",SqlDbType.BigInt){Value = originalRoadRefundInfo.connId}, 
                new SqlParameter("@refundAmount", SqlDbType.Float){Value = originalRoadRefundInfo.refundAmount},
                new SqlParameter("@remittanceTime",SqlDbType.DateTime){Value = originalRoadRefundInfo.remittanceTime},
                new SqlParameter("@status",SqlDbType.Int){Value = originalRoadRefundInfo.status},
                new SqlParameter("@remitEmployee",SqlDbType.Int){Value = originalRoadRefundInfo.remitEmployee},
                new SqlParameter("@customerMobilephone",SqlDbType.NVarChar,50){Value = originalRoadRefundInfo.customerMobilephone},
                new SqlParameter("@note", SqlDbType.NVarChar,500){Value = originalRoadRefundInfo.note},
                new SqlParameter("@customerUserName",SqlDbType.NVarChar,100){Value = originalRoadRefundInfo.customerUserName},
                new SqlParameter("@employeeId", SqlDbType.Int){Value = originalRoadRefundInfo.employeeId},
                new SqlParameter("@RefundPayType",SqlDbType.Int){Value = originalRoadRefundInfo.RefundPayType}, 
                new SqlParameter("@id",SqlDbType.BigInt){Value = originalRoadRefundInfo.id}, 
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

        public OriginalRoadRefundInfo GetOriginalRoadRefundInfoByOrderId(long orderId)
        {
            const string cmdText = @"SELECT * FROM [dbo].[OriginalRoadRefundInfo] WHERE [connId]=@Id";

            SqlParameter cmdParm = new SqlParameter("@Id", SqlDbType.BigInt) { Value = orderId };

            OriginalRoadRefundInfo originalRoadRefundInfo = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    originalRoadRefundInfo = SqlHelper.GetEntity<OriginalRoadRefundInfo>(dr);
                }
            }

            return originalRoadRefundInfo;
        }
    }
}
