using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerTreasureChestAccessInfoRepositoryRepository : SqlServerRepositoryBase, ITreasureChestAccessInfoRepository
    {
        public SqlServerTreasureChestAccessInfoRepositoryRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }



        public void Add(TreasureChestAccessInfo treasureChestAccessInfo)
        {
            const string cmdText = @"INSERT INTO [dbo].[TreasureChestAccessInfo]
                                       ([activityId]
                                       ,[sourceType]
                                       ,[appType]
                                       ,[mobilePhoneNumber]
                                       ,[cookie]
                                       ,[ip]
                                       ,[url]
                                       ,[accessTime])
                                 VALUES
                                       (@activityId
                                       ,@sourceType
                                       ,@appType
                                       ,@mobilePhoneNumber
                                       ,@cookie
                                       ,@ip
                                       ,@url
                                       ,@accessTime)";


            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@activityId",SqlDbType.Int){Value = treasureChestAccessInfo.activityId}, 
                new SqlParameter("@sourceType",SqlDbType.NVarChar,50){Value = treasureChestAccessInfo.sourceType}, 
                new SqlParameter("@appType",SqlDbType.Int){Value = treasureChestAccessInfo.appType},
                new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar,50){Value = treasureChestAccessInfo.mobilePhoneNumber},
                new SqlParameter("@cookie",SqlDbType.NVarChar,100){Value = treasureChestAccessInfo.cookie}, 
                new SqlParameter("@ip",SqlDbType.NVarChar,20){Value = treasureChestAccessInfo.ip}, 
                new SqlParameter("@url",SqlDbType.NVarChar,100){Value = treasureChestAccessInfo.url},
                new SqlParameter("@accessTime",SqlDbType.DateTime){Value = treasureChestAccessInfo.accessTime}, 
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }
    }
}