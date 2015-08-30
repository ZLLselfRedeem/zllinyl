using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerActivityRepository : SqlServerRepositoryBase, IActivityRepository
    {
        public SqlServerActivityRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }


        public Activity GetById(int id)
        {
            const string cmdText = @"SELECT  activityId,name,beginTime,endTime,enabled,status,createTime,createdBy,updateTime,updatedBy,expirationTimeRule,
                                            ruleValue,activityRule,activityType,redEnvelopeEffectiveBeginTime,redEnvelopeEffectiveEndTime
                                            FROM [dbo].[Activity] WHERE [activityId]=@id AND [status]=1";

            SqlParameter cmdParm = new SqlParameter("@id", SqlDbType.Int) { Value = id };

            Activity activity = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    activity = dr.GetEntity<Activity>();
                }
            }
            return activity;
        }
    }
}