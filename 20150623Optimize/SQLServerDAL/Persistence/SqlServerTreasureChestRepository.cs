using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerTreasureChestRepository : SqlServerRepositoryBase, ITreasureChestRepository
    {
        public SqlServerTreasureChestRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public TreasureChest GetById(long id)
        {
            const string cmdText = @"SELECT treasureChestId,treasureChestConfigId,activityId,amount,remainAmount,count,lockCount,
                                            mobilePhoneNumber,cookie,createTime,url,executedTime,isExpire,expireTime,status
                                            FROM [dbo].[TreasureChest] WITH(NOLOCK) WHERE [treasureChestId]=@id AND [status]=1";

            SqlParameter cmdParm = new SqlParameter("@id", SqlDbType.BigInt) { Value = id };

            TreasureChest treasureChest = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    treasureChest = dr.GetEntity<TreasureChest>();
                }
            }
            return treasureChest;
        }

        public TreasureChest GetByActivityCookie(string cookie, int activityId)
        {
            const string cmdText = @"SELECT treasureChestId,treasureChestConfigId,activityId,amount,remainAmount,count,lockCount,
                                            mobilePhoneNumber,cookie,createTime,url,executedTime,isExpire,expireTime,status
                                            FROM [dbo].[TreasureChest] WHERE [cookie]=@cookie AND [activityId]=@activityId AND [status]=1";
            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@cookie", SqlDbType.NVarChar, 100) { Value = string.IsNullOrEmpty(cookie)?"":cookie },
                new SqlParameter("@activityId",SqlDbType.Int){Value = activityId}, 
            };

            TreasureChest treasureChest = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                if (dr.Read())
                {
                    treasureChest = dr.GetEntity<TreasureChest>();
                }
            }
            return treasureChest;
        }

        public TreasureChest GetByActivityMobilePhone(string mobilePhone, int activityId)
        {
            const string cmdText = @"SELECT  treasureChestId,treasureChestConfigId,activityId,amount,remainAmount,count,lockCount,
                                            mobilePhoneNumber,cookie,createTime,url,executedTime,isExpire,expireTime,status
                                            FROM [dbo].[TreasureChest] WHERE [mobilePhoneNumber]=@mobilePhone AND [activityId]=@activityId AND [status]=1";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@mobilePhone", SqlDbType.NVarChar, 20) { Value = string.IsNullOrEmpty(mobilePhone)?"":mobilePhone },
                new SqlParameter("@activityId",SqlDbType.Int){Value = activityId}, 
            };


            TreasureChest treasureChest = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                if (dr.Read())
                {
                    treasureChest = dr.GetEntity<TreasureChest>();
                }
            }

            return treasureChest;
        }

        public TreasureChest GetByActivity(int activityId)
        {
            const string cmdText = @"SELECT  treasureChestId,treasureChestConfigId,activityId,amount,remainAmount,count,lockCount,
                                            mobilePhoneNumber,cookie,createTime,url,executedTime,isExpire,expireTime,status
                                             FROM [dbo].[TreasureChest] WITH(NOLOCK) WHERE [activityId]=@activityId AND [status]=1";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@activityId",SqlDbType.Int){Value = activityId}, 
            };


            TreasureChest treasureChest = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                if (dr.Read())
                {
                    treasureChest = dr.GetEntity<TreasureChest>();
                }
            }

            return treasureChest;
        }

        public void Add(TreasureChest treasureChest)
        {
            const string cmdText = @"INSERT INTO [dbo].[TreasureChest]
           ([treasureChestConfigId]
           ,[activityId]
           ,[amount]
           ,[remainAmount]
           ,[count]
           ,[lockCount]
           ,[mobilePhoneNumber]
           ,[cookie]
           ,[createTime]
           ,[isExpire]
           ,[expireTime]
           ,[status])
     VALUES
           (@treasureChestConfigId
           ,@activityId
           ,@amount
           ,@remainAmount
           ,@count
           ,@lockCount
           ,@mobilePhoneNumber
           ,@cookie
           ,@createTime
           ,@isExpire
           ,@expireTime
           ,@status); select @@identity;";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@treasureChestConfigId", SqlDbType.Int) {Value = treasureChest.treasureChestConfigId},
                new SqlParameter("@activityId", SqlDbType.Int) {Value = treasureChest.activityId},
                new SqlParameter("@amount", SqlDbType.Float) {Value = treasureChest.amount},
                new SqlParameter("@remainAmount",SqlDbType.Float){Value = treasureChest.remainAmount}, 
                new SqlParameter("@count", SqlDbType.Int) {Value = treasureChest.count},
                new SqlParameter("@lockCount", SqlDbType.Int) {Value = treasureChest.lockCount},
                new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 20)
                {
                    Value = string.IsNullOrEmpty(treasureChest.mobilePhoneNumber) ? "" : treasureChest.mobilePhoneNumber
                },
                new SqlParameter("@cookie", SqlDbType.NVarChar, 100) {Value = treasureChest.cookie},
                new SqlParameter("@createTime", SqlDbType.DateTime) {Value = treasureChest.createTime},
                new SqlParameter("@isExpire", SqlDbType.Bit) {Value = treasureChest.isExpire},
                new SqlParameter("@expireTime", SqlDbType.DateTime) {Value = treasureChest.expireTime},
                new SqlParameter("@status", SqlDbType.Bit) {Value = treasureChest.status},
            };

            object o = SqlHelper.ExecuteScalar(Connection, CommandType.Text, cmdText, cmdParms);

            treasureChest.treasureChestId = Convert.ToInt64(o);
        }

        public void UpdateLockAndAmount(long treasureChestId, double remainAmount, int lockCount)
        {
            const string cmdText = @"UPDATE [dbo].[TreasureChest] SET [remainAmount]=@remainAmount, [lockCount]=@lockCount WHERE [treasureChestId]=@treasureChestId";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@remainAmount",SqlDbType.Float){Value = remainAmount},
                new SqlParameter("@lockCount",SqlDbType.Int){Value = lockCount},
                new SqlParameter("@treasureChestId",SqlDbType.BigInt){Value = treasureChestId}, 
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

        public int UpdateLockAndAmount(long treasureChestId, double originRemainAmount, double remainAmount, int originLockCount, int lockCount)
        {
            //const string cmdText = @"UPDATE [dbo].[TreasureChest] SET [remainAmount]=@remainAmount, [lockCount]=@lockCount WHERE [treasureChestId]=@treasureChestId AND [remainAmount]=@originRemainAmount AND [lockCount]=@originLockCount";
            const string cmdText = @"UPDATE [dbo].[TreasureChest] SET [remainAmount]=@remainAmount, [lockCount]=@lockCount WHERE [treasureChestId]=@treasureChestId";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@remainAmount",SqlDbType.Float){Value = remainAmount},
                new SqlParameter("@lockCount",SqlDbType.Int){Value = lockCount},
                //new SqlParameter("@originRemainAmount",SqlDbType.Float){Value = originRemainAmount},
                //new SqlParameter("@originLockCount",SqlDbType.Int){Value = originLockCount},
                new SqlParameter("@treasureChestId",SqlDbType.BigInt){Value = treasureChestId}, 
            };

            int c = SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
            return c;
        }

        public void UpdateLockAndAmountAndExecutedTime(long treasureChestId, double remainAmount, int lockCount, DateTime executedTime)
        {
            const string cmdText = @"UPDATE [dbo].[TreasureChest] SET [remainAmount]=@remainAmount, [lockCount]=@lockCount,[executedTime]=@executedTime WHERE [treasureChestId]=@treasureChestId";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@remainAmount",SqlDbType.Float){Value = remainAmount},
                new SqlParameter("@lockCount",SqlDbType.Int){Value = lockCount},
                new SqlParameter("@treasureChestId",SqlDbType.BigInt){Value = treasureChestId}, 
                new SqlParameter("@executedTime",SqlDbType.DateTime){Value = executedTime}, 
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

        public void UpdateMobilePhoneLockAndAmount(long treasureChestId, string mobilePhone, double remainAmount, int lockCount)
        {
            const string cmdText = @"UPDATE [dbo].[TreasureChest] SET [mobilePhoneNumber]=@mobilePhone,[remainAmount]=@remainAmount, [lockCount]=@lockCount WHERE [treasureChestId]=@treasureChestId";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@remainAmount",SqlDbType.Float){Value = remainAmount},
                new SqlParameter("@lockCount",SqlDbType.Int){Value = lockCount},
                new SqlParameter("@treasureChestId",SqlDbType.BigInt){Value = treasureChestId}, 
                new SqlParameter("@mobilePhone",SqlDbType.NVarChar,20){Value = mobilePhone}, 
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

        public void UpdateCookie(long treasureChestId, string cookie)
        {
            const string cmdText = @"UPDATE [dbo].[TreasureChest] SET [cookie]=@cookie WHERE [treasureChestId]=@treasureChestId";
            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@cookie",SqlDbType.NVarChar,100){Value = cookie},
                new SqlParameter("@treasureChestId",SqlDbType.BigInt){Value = treasureChestId}, 
                 
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

        public void UpdateStatus(long treasureChestId, bool status)
        {
            const string cmdText = @"UPDATE [dbo].[TreasureChest] SET [status]=@status WHERE [treasureChestId]=@treasureChestId";
            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@status",SqlDbType.Bit){Value = status},
                new SqlParameter("@treasureChestId",SqlDbType.BigInt){Value = treasureChestId}, 
                 
            };

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

        public bool AnyMobilePhone(string mobilePhone)
        {
            const string cmdText = @"SELECT COUNT(1) FROM [dbo].[TreasureChest] WHERE [mobilePhoneNumber]=@mobilePhone AND [status]=1";

            SqlParameter cmdParm = new SqlParameter("@mobilePhone", SqlDbType.NVarChar, 20) { Value = mobilePhone };

            object o = SqlHelper.ExecuteScalar(Connection, CommandType.Text, cmdText, cmdParm);

            return Convert.ToInt32(o) > 0;
        }
    }
}
