using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerRedEnvelopeDetaiRepository : SqlServerRepositoryBase, IRedEnvelopeDetailRepository
    {
        public SqlServerRedEnvelopeDetaiRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }


//        public int Add(RedEnvelopeDetail redEnvelopeDetail)
//        {
//            //const string cmdText = @"if not exists (select Id from [dbo].[RedEnvelopeDetails] where redEnvelopeId=@redEnvelopeId and mobilePhoneNumber=@mobilePhoneNumber and stateType=0) 
//           const string cmdText = @"INSERT INTO [dbo].[RedEnvelopeDetails]
//           ([mobilePhoneNumber]
//           ,[treasureChestId]
//           ,[redEnvelopeId]
//           ,[redEnvelopeAmount]
//           ,[redEnvelopeExpirationTime]
//           ,[operationTime]
//           ,[stateType]
//           ,[usedAmount])
//     VALUES
//           (@mobilePhoneNumber
//           ,@treasureChestId
//           ,@redEnvelopeId
//           ,@redEnvelopeAmount
//           ,@redEnvelopeExpirationTime
//           ,@operationTime
//           ,@stateType
//           ,@usedAmount)";

//            SqlParameter[] cmdParms = new SqlParameter[]
//            {
//                new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar,50){Value = redEnvelopeDetail.mobilePhoneNumber},
//                new SqlParameter("@treasureChestId",SqlDbType.BigInt){Value = redEnvelopeDetail.treasureChestId},
//                new SqlParameter("@redEnvelopeId",SqlDbType.BigInt){Value = redEnvelopeDetail.redEnvelopeId},
//                new SqlParameter("@redEnvelopeAmount",SqlDbType.Float){Value = redEnvelopeDetail.redEnvelopeAmount},
//                new SqlParameter("@redEnvelopeExpirationTime", SqlDbType.DateTime){Value = redEnvelopeDetail.redEnvelopeExpirationTime},
//                new SqlParameter("@operationTime",SqlDbType.DateTime){Value = redEnvelopeDetail.operationTime},
//                new SqlParameter("@stateType",SqlDbType.TinyInt){Value = redEnvelopeDetail.stateType},
//                new SqlParameter("@usedAmount",SqlDbType.Float){Value = redEnvelopeDetail.usedAmount}, 
//            };

//            return SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
//        }

        //public void UpdateTreasureChestExecuted(long treasureChestId, VARedEnvelopeStateType stateType)
        //{
        //    const string cmdText = @"UPDATE [dbo].[RedEnvelopeDetails] SET [stateType]=@stateType WHERE [treasureChestId]=@treasureChestId AND [stateType]=@savagery";

        //    SqlParameter[] cmdParms = new SqlParameter[]
        //    {
        //        new SqlParameter("@stateType",SqlDbType.TinyInt){Value = stateType},
        //        new SqlParameter("@treasureChestId",SqlDbType.BigInt){Value = treasureChestId},
        //        new SqlParameter("@savagery", SqlDbType.TinyInt){Value = VARedEnvelopeStateType.未生效}, 
        //    };

        //    SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        //}

        //public int UpdateMobilePhoneNumberByRedEnvelope(long redEnvelopeId, string mobilePhoneNumber)
        //{
        //    const string cmdText = @"UPDATE [dbo].[RedEnvelopeDetails] SET [mobilePhoneNumber]=@mobilePhoneNumber WHERE [redEnvelopeId]=@redEnvelopeId";

        //    SqlParameter[] cmdParms = new SqlParameter[]
        //    {
        //        new SqlParameter("@mobilePhoneNumber",SqlDbType.NVarChar,50){Value = mobilePhoneNumber},
        //        new SqlParameter("@redEnvelopeId",SqlDbType.BigInt){Value =redEnvelopeId }, 
        //    };

        //    return SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        //}
    }
}