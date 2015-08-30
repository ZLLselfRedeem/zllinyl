using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerTreasureChestConfigRepository : SqlServerRepositoryBase, ITreasureChestConfigRepository
    {
        public SqlServerTreasureChestConfigRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }


        public IEnumerable<TreasureChestConfig> GetManyByActivity(int activityId)
        {
            const string cmdText = @"SELECT treasureChestConfigId,activityId,amount,count,min,max,quantity,remainQuantity,status,createTime,createdBy,updateTime,updatedBy,
                                                amountRule,defaultAmountRange,defaultRateRange,newAmountRange,newRateRange,isPreventCheat
                                                FROM [dbo].[TreasureChestConfig] WITH(NOLOCK) WHERE [activityId]=@activityId AND [status]=1";
            SqlParameter cmdParm = new SqlParameter("@activityId", SqlDbType.Int) { Value = activityId };

            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                while (dr.Read())
                {
                    yield return dr.GetEntity<TreasureChestConfig>();
                }
            }
        }

        public void UpdateRemainQuantity(long id, int remainQuantity)
        {
            const string cmdText = @"UPDATE [dbo].[TreasureChestConfig] SET [remainQuantity]=@remainQuantity WHERE [treasureChestConfigId]=@id";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@remainQuantity", SqlDbType.Int){Value = remainQuantity},
                new SqlParameter("@id",SqlDbType.BigInt){Value = id}, 
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

        public int UpdateRemainQuantity(long id, int originRemainQuantity, int remainQuantity)
        {
            const string cmdText = @"UPDATE [dbo].[TreasureChestConfig] SET [remainQuantity]=@remainQuantity WHERE [treasureChestConfigId]=@id AND [remainQuantity]=@originRemainQuantity";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@remainQuantity", SqlDbType.Int){Value = remainQuantity},
                new SqlParameter("@originRemainQuantity", SqlDbType.Int){Value = originRemainQuantity},
                new SqlParameter("@id",SqlDbType.BigInt){Value = id}, 
            };

            int c = SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);

            return c;
        }

        public TreasureChestConfig GetById(int id)
        {
            const string cmdText = @"SELECT treasureChestConfigId,activityId,amount,count,min,max,quantity,remainQuantity,status,createTime,createdBy,updateTime,updatedBy,
                                            amountRule,defaultAmountRange,defaultRateRange,newAmountRange,newRateRange,isPreventCheat 
                                            FROM [dbo].[TreasureChestConfig] WITH(NOLOCK) WHERE [treasureChestConfigId]=@id AND [status]=1";

            SqlParameter cmdParm = new SqlParameter("@id", SqlDbType.BigInt) { Value = id };

            TreasureChestConfig treasureChestConfig = null;

            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    treasureChestConfig = dr.GetEntity<TreasureChestConfig>();
                }
            }

            return treasureChestConfig;
        }

        public double SumActivityRemainAmount(int activityId)
        {
            const string cmdText = @"SELECT ISNULL(SUM([remainQuantity]*[amount]),0) FROM [dbo].[TreasureChestConfig] WHERE [activityId]=@activityId AND [status]=1";

            SqlParameter cmdParm = new SqlParameter("@activityId", SqlDbType.Int) { Value = activityId };

            object o = SqlHelper.ExecuteScalar(Connection, CommandType.Text, cmdText, cmdParm);

            return Convert.ToDouble(o);
        }
    }

}