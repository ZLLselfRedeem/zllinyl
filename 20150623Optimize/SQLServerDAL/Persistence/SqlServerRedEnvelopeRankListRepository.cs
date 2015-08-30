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
    public class SqlServerRedEnvelopeRankListRepository : SqlServerRepositoryBase, IRedEnvelopeRankListRepository
    {
        public SqlServerRedEnvelopeRankListRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }


        public RedEnvelopeRankList GetByMobilePhoneNumber(string mobilePhoneNumber)
        {
            const string cmdText = @"SELECT id,mobilePhoneNumber,ranking,createTime,lastUpdateTime,rankState,amount 
FROM [dbo].[RedEnvelopeRankList] WITH(NOLOCK) WHERE [mobilePhoneNumber]=@mobilePhoneNumber";
            SqlParameter cmdParm = new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 20) { Value = mobilePhoneNumber };

            RedEnvelopeRankList ranklist = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    ranklist = dr.GetEntity<RedEnvelopeRankList>();

                }
                return ranklist;
            }

        }

        public void Insert(RedEnvelopeRankList rankList)
        {
            const string cmdText = @"INSERT INTO [dbo].[RedEnvelopeRankList]
           ([mobilePhoneNumber]
           ,[ranking]
           ,[createTime]
           ,[lastUpdateTime]
           ,[rankState],[amount])
     VALUES
           (@mobilePhoneNumber
           ,@ranking
           ,@createTime
           ,@lastUpdateTime
           ,@rankState,@amount)";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar,20){Value = rankList.mobilePhoneNumber},
                new SqlParameter("@ranking", SqlDbType.BigInt){Value = rankList.ranking},
                new SqlParameter("@createTime",SqlDbType.DateTime){Value = rankList.createTime}, 
                new SqlParameter("@lastUpdateTime",SqlDbType.DateTime){Value = rankList.lastUpdateTime},
                new SqlParameter("@rankState",SqlDbType.Int){Value = rankList.rankState}, 
                 new SqlParameter("@amount",SqlDbType.Float){Value = rankList.amount}, 
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

        public void UpdateRanking(long id, long ranking, double totalamount, RankState rankState)
        {
            const string cmdText = @"UPDATE [dbo].[RedEnvelopeRankList] SET [ranking]=@ranking,[rankState]=@rankState,[amount]=@amount WHERE [id]=@id";
            SqlParameter[] cmdParms = new SqlParameter[]
            {
                 new SqlParameter("@ranking", SqlDbType.BigInt){Value = ranking},
                 new SqlParameter("@rankState",SqlDbType.Int){Value = rankState}, 
                 new SqlParameter("@id",SqlDbType.BigInt){Value = id}, 
                 new SqlParameter("@amount",SqlDbType.Float){Value = totalamount}, 
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

    }
}
