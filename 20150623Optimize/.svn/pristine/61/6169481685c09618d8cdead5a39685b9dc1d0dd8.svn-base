using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    class SqlServerActivityShareInfoRepository : SqlServerRepositoryBase, IActivityShareInfoRepository
    {
        public SqlServerActivityShareInfoRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public IEnumerable<ActivityShareInfo> GetManyByActivity(int activity)
        {
            const string cmdText = @"SELECT id,activityId,type,remark,status FROM [dbo].[ActivityShareInfo] WITH(NOLOCK) WHERE [activityId]=@activityId AND [status]=1";

            SqlParameter cmdParm = new SqlParameter("@activityId", SqlDbType.Int) { Value = activity };
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                while (dr.Read())
                {
                    yield return dr.GetEntity<ActivityShareInfo>();
                }
                
            }

        }
    }
}
