using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL.Persistence
{
    public class SqlServerRedEnvelopeRepository : SqlServerRepositoryBase, IRedEnvelopeRepository
    {
        public SqlServerRedEnvelopeRepository(ISqlConnectionFactory connectionFactory)
            : base(connectionFactory)
        {
        }

        public IEnumerable<RedEnvelope> GetManyByTreasureChest(long treasureChestId)
        {
            const string cmdText = @"SELECT redEnvelopeId,treasureChestId,Amount,mobilePhoneNumber,status,lastUpdateTime,getTime,isExecuted,
                                            isExpire,expireTime,unusedAmount,activityId,isOwner,isOverflow,cookie,isChange,uuid 
                                            FROM [dbo].[RedEnvelope] WITH(NOLOCK) WHERE [treasureChestId]=@treasureChestId AND [status]=1 ORDER BY [redEnvelopeId] DESC";

            SqlParameter cmdParm = new SqlParameter("@treasureChestId", SqlDbType.BigInt) { Value = treasureChestId };

            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                while (dr.Read())
                {
                    yield return dr.GetEntity<RedEnvelope>();
                }
            }
        }

        public RedEnvelope GetByTreasureChestAndMobilePhone(long treasureChestId, string mobilePhone)
        {
            const string cmdText = @"SELECT  redEnvelopeId,treasureChestId,Amount,mobilePhoneNumber,status,lastUpdateTime,getTime,isExecuted,
                                            isExpire,expireTime,unusedAmount,activityId,isOwner,isOverflow,cookie,isChange,uuid 
                                             FROM [dbo].[RedEnvelope] WITH(NOLOCK) WHERE [treasureChestId]=@treasureChestId AND [mobilePhoneNumber]=@mobilePhone AND [status]=1";

            SqlParameter[] cmdParms = new SqlParameter[]
			{
				new SqlParameter("@treasureChestId",SqlDbType.BigInt){Value = treasureChestId},
				new SqlParameter("@mobilePhone",SqlDbType.NVarChar,20){Value = mobilePhone}, 
			};

            RedEnvelope redEnvelope = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                if (dr.Read())
                {
                    redEnvelope = dr.GetEntity<RedEnvelope>();
                }
            }

            return redEnvelope;
        }

        public RedEnvelope GetByActivityAndMobilePhone(int activityId, string mobilePhone, string weChatUserId)
        {
            string cmdText = @"SELECT  redEnvelopeId,Amount,mobilePhoneNumber,status,lastUpdateTime,getTime,isExecuted,
                                            isExpire,expireTime,unusedAmount,activityId,isOwner,isOverflow,cookie,isChange,uuid 
                                             FROM [dbo].[RedEnvelope] WITH(NOLOCK) WHERE [activityId]=@activityId AND [status]=1";

            SqlParameter[] cmdParms = new SqlParameter[]
			{
				new SqlParameter("@activityId",SqlDbType.Int){Value = activityId},
				new SqlParameter("@mobilePhone",SqlDbType.NVarChar,20){Value = mobilePhone}
			};

            //if (string.IsNullOrEmpty(weChatUserId))
            cmdText += " AND mobilePhoneNumber=@mobilePhone";
            //else
            //cmdText += " AND [mobilePhoneNumber]=@mobilePhone OR WeChatUser_Id='" + weChatUserId + "'";

            RedEnvelope redEnvelope = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                if (dr.Read())
                {
                    redEnvelope = dr.GetEntity<RedEnvelope>();
                }
            }

            return redEnvelope;
        }

        public bool AnyByTreasureChestAndUser(long treasureChestId, string mobilePhone)
        {
            const string cmdText = @"SELECT COUNT(1) FROM [dbo].[RedEnvelope] WITH(NOLOCK) WHERE [treasureChestId]=@treasureChestId AND [mobilePhoneNumber]=@mobilePhone AND [status]=1";

            SqlParameter[] cmdParms = new SqlParameter[]
			{
				new SqlParameter("@treasureChestId",SqlDbType.BigInt){Value = treasureChestId},
				new SqlParameter("@mobilePhone",SqlDbType.NVarChar,20){Value = mobilePhone}, 
			};

            object o = SqlHelper.ExecuteScalar(Connection, CommandType.Text, cmdText, cmdParms);

            int count = Convert.ToInt32(o);

            return count > 0;
        }

        public int CountByUserToday(string mobilePhone, DateTime today)
        {
            const string cmdText = @"SELECT COUNT(1) FROM [dbo].[RedEnvelope] WITH(NOLOCK) WHERE [mobilePhoneNumber]=@mobilePhone AND [getTime]>=@today AND [status]=1";

            SqlParameter[] cmdParms = new SqlParameter[]
			{
				new SqlParameter("@mobilePhone",SqlDbType.NVarChar,20){Value = mobilePhone}, 
				new SqlParameter("@today", SqlDbType.Date){Value = today}, 
			};
            object o = SqlHelper.ExecuteScalar(Connection, CommandType.Text, cmdText, cmdParms);

            int count = Convert.ToInt32(o);

            return count;
        }

        public double SumUserAmount(string mobilePhone)
        {
            const string cmdText = @"SELECT ISNULL(SUM([unusedAmount]),0) FROM [dbo].[RedEnvelope] WITH(NOLOCK) WHERE [mobilePhoneNumber]=@mobilePhone AND [status]=1 AND [isExpire]=0 AND [expireTime]>GETDATE()";

            SqlParameter cmdParm = new SqlParameter("@mobilePhone", SqlDbType.NVarChar, 20) { Value = mobilePhone };

            object o = SqlHelper.ExecuteScalar(Connection, CommandType.Text, cmdText, cmdParm);

            double amount = Convert.ToDouble(o);

            return amount;
        }

        public void Add(RedEnvelope redEnvelope)
        {
            const string cmdText = @"INSERT INTO [dbo].[RedEnvelope]
		   ([treasureChestId]
		   ,[Amount]
		   ,[mobilePhoneNumber]
		   ,[status]           
		   ,[getTime]
		   ,[isExecuted]
		   ,[isExpire]
		   ,[expireTime]
		   ,[unusedAmount],[activityId],[isOwner],[isOverflow],[cookie],[uuid],[effectTime],[from])
	 VALUES
		   (@treasureChestId
		   ,@Amount
		   ,@mobilePhoneNumber
		   ,@status
		   ,@getTime
		   ,@isExecuted
		   ,@isExpire
		   ,@expireTime
		   ,@unusedAmount,@activityId,@isOwner,@isOverflow,@cookie,@uuid,@effectTime,@from); select @@identity;";

            SqlParameter[] cmdParms = new SqlParameter[]
			{
				new SqlParameter("@treasureChestId",SqlDbType.BigInt){Value = redEnvelope.treasureChestId},
				new SqlParameter("@Amount", SqlDbType.Float){Value = redEnvelope.Amount},
				new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar,20){Value = redEnvelope.mobilePhoneNumber},
				new SqlParameter("@status",SqlDbType.Int){Value = redEnvelope.status},
				new SqlParameter("@getTime",SqlDbType.DateTime){Value = redEnvelope.getTime},
				new SqlParameter("@isExecuted",SqlDbType.Int){Value = redEnvelope.isExecuted},
				new SqlParameter("@isExpire",SqlDbType.Bit){Value = redEnvelope.isExpire},
				new SqlParameter("@expireTime",SqlDbType.DateTime){Value = redEnvelope.expireTime}, 
				new SqlParameter("@unusedAmount",SqlDbType.Float){Value = redEnvelope.unusedAmount}, 
				new SqlParameter("@activityId", SqlDbType.Int){Value = redEnvelope.activityId},
				new SqlParameter("@isOwner", SqlDbType.Bit){Value = redEnvelope.isOwner}, 
				new SqlParameter("@isOverflow", SqlDbType.Bit){Value = redEnvelope.isOverflow}, 
                new SqlParameter("@cookie",SqlDbType.NVarChar,100){Value = string.IsNullOrEmpty(redEnvelope.cookie)?"":redEnvelope.cookie}, 
                new SqlParameter("@uuid", SqlDbType.NVarChar,20){Value = redEnvelope.uuid}, 
                new SqlParameter("@effectTime", SqlDbType.DateTime){Value = redEnvelope.effectTime}, 
                new SqlParameter("@from", SqlDbType.NVarChar,50){Value = redEnvelope.from}, 
			};

            object o = SqlHelper.ExecuteScalar(Connection, CommandType.Text, cmdText, cmdParms);

            redEnvelope.redEnvelopeId = Convert.ToInt64(o);
        }

        public void UpdateTreasureChestExecuted(long treasureChestId, VARedEnvelopeStateType isExecuted)
        {
            const string cmdText = @"UPDATE [dbo].[RedEnvelope] SET [isExecuted]=@isExecuted WHERE [treasureChestId]=@treasureChestId AND [isOverflow]=0";

            SqlParameter[] cmdParms = new SqlParameter[]
			{
				new SqlParameter("@isExecuted",SqlDbType.Int){Value = isExecuted},
				new SqlParameter("@treasureChestId",SqlDbType.BigInt){Value = treasureChestId},
			};

            SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        }

        public IEnumerable<RedEnvelope> GetPageByActivity(Page page, int activityId)
        {
            const string cmdText = @"SELECT  rowId,redEnvelopeId,treasureChestId,Amount,mobilePhoneNumber,status,lastUpdateTime,getTime,isExecuted,
isExpire,expireTime,unusedAmount,activityId,isOwner,isOverflow,cookie,isChange,uuid
 FROM (SELECT ROW_NUMBER() Over(order by [Amount] DESC) as rowId,
redEnvelopeId,treasureChestId,Amount,mobilePhoneNumber,status,lastUpdateTime,getTime,isExecuted,
isExpire,expireTime,unusedAmount,activityId,isOwner,isOverflow,cookie,isChange,uuid
FROM [dbo].[RedEnvelope] WITH(NOLOCK) WHERE [status]=1 AND  [isOverflow]=0 AND [isExecuted]=1 AND [ActivityId]=@activityId) AS t
WHERE t.rowId BETWEEN @BeginIndex AND @EndIndex";

            SqlParameter[] cmdParms = new SqlParameter[]
			{
				new SqlParameter("@BeginIndex", SqlDbType.Int) {Value = page.Skip + 1},
				new SqlParameter("@EndIndex", SqlDbType.Int) {Value = page.Skip + page.PageSize},
                new SqlParameter("@activityId", SqlDbType.Int){Value=activityId}
			};


            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                while (dr.Read())
                {
                    yield return dr.GetEntity<RedEnvelope>();
                }
            }
        }

        public bool AnyRedEnvelopeOwnerInActivity(string mobilePhone, int activityId)
        {
            const string cmdText = @"SELECT COUNT(1) FROM [dbo].[RedEnvelope] WITH(NOLOCK) WHERE [mobilePhoneNumber]=@mobilePhone AND [activityId]=@activityId AND [isOwner]=1";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@mobilePhone", SqlDbType.NVarChar,20){Value = mobilePhone},
                new SqlParameter("@activityId", SqlDbType.Int){Value = activityId}, 
            };

            object o = SqlHelper.ExecuteScalar(Connection, CommandType.Text, cmdText, cmdParms);

            return Convert.ToInt32(o) > 0;
        }

        public RedEnvelope GetByTreasureChestAndCookie(long treasureChestId, string cookie)
        {
            const string cmdText = @"SELECT  redEnvelopeId,treasureChestId,Amount,mobilePhoneNumber,status,lastUpdateTime,getTime,isExecuted,
                                            isExpire,expireTime,unusedAmount,activityId,isOwner,isOverflow,cookie,isChange,uuid 
                                             FROM [dbo].[RedEnvelope] WITH(NOLOCK) WHERE [treasureChestId]=@treasureChestId AND [cookie]=@cookie AND [status]=1";

            SqlParameter[] cmdParms = new SqlParameter[]
			{
				new SqlParameter("@treasureChestId",SqlDbType.BigInt){Value = treasureChestId},
				new SqlParameter("@cookie",SqlDbType.NVarChar,100){Value = cookie}, 
			};

            RedEnvelope redEnvelope = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                if (dr.Read())
                {
                    redEnvelope = dr.GetEntity<RedEnvelope>();
                }
            }

            return redEnvelope;
        }

        public RedEnvelope GetByActivityAndCookie(int activityId, string cookie)
        {
            const string cmdText = @"SELECT  redEnvelopeId,Amount,mobilePhoneNumber,status,lastUpdateTime,getTime,isExecuted,
                                            isExpire,expireTime,unusedAmount,activityId,isOwner,isOverflow,cookie,isChange,uuid 
                                             FROM [dbo].[RedEnvelope] WITH(NOLOCK) WHERE [activityId]=@activityId AND [cookie]=@cookie AND [status]=1";

            SqlParameter[] cmdParms = new SqlParameter[]
			{
				new SqlParameter("@activityId",SqlDbType.Int){Value = activityId},
				new SqlParameter("@cookie",SqlDbType.NVarChar,100){Value = cookie}, 
			};

            RedEnvelope redEnvelope = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                if (dr.Read())
                {
                    redEnvelope = dr.GetEntity<RedEnvelope>();
                }
            }

            return redEnvelope;
        }

        public RedEnvelope GetById(long id)
        {
            const string cmdText = @"SELECT  redEnvelopeId,treasureChestId,Amount,mobilePhoneNumber,status,lastUpdateTime,getTime,isExecuted,
                                            isExpire,expireTime,unusedAmount,activityId,isOwner,isOverflow,cookie,isChange,uuid 
                                             FROM [dbo].[RedEnvelope] WITH(NOLOCK) WHERE [redEnvelopeId]=@id AND [status]=1";

            SqlParameter[] cmdParms = new SqlParameter[]
			{
				new SqlParameter("@id",SqlDbType.BigInt){Value = id},
			};

            RedEnvelope redEnvelope = null;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                if (dr.Read())
                {
                    redEnvelope = dr.GetEntity<RedEnvelope>();
                }
            }

            return redEnvelope;
        }

        public int UpdateMobilePhoneNumberAndisExecuted(long redEnvelopeId, string mobilePhoneNumber,
            VARedEnvelopeStateType vARedEnvelopeStateType, string uuid, double amount, string weChatUserId)
        {
            string cmdText = @"UPDATE [dbo].[RedEnvelope] SET [isExecuted]=@isExecuted,[mobilePhoneNumber]=@mobilePhoneNumber,[uuid]=@uuid,[Amount]=@Amount,[unusedAmount]=@Amount";

            if (!string.IsNullOrEmpty(weChatUserId))
                cmdText += " , WeChatUser_Id='" + weChatUserId + "' ";
            cmdText += " WHERE [redEnvelopeId]=@redEnvelopeId AND [isExecuted]=0";

            SqlParameter[] cmdParms = new SqlParameter[]
			{
				new SqlParameter("@isExecuted",SqlDbType.Int){Value = vARedEnvelopeStateType},
				new SqlParameter("@mobilePhoneNumber",SqlDbType.NVarChar,20){Value = mobilePhoneNumber},
                new SqlParameter("@uuid",SqlDbType.NVarChar,100){Value = uuid},
                new SqlParameter("@Amount",SqlDbType.Float){Value = amount},
                new SqlParameter("@redEnvelopeId",SqlDbType.BigInt){Value = redEnvelopeId}
			};

            int c = SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
            return c;
        }

        public void Sum(long treasureChestId, out int count, out double amount)
        {
            const string cmdText = @"SELECT COUNT(1) AS [C], ISNULL(SUM([Amount]),0) AS [A] FROM [dbo].[RedEnvelope] WITH(NOLOCK) WHERE [treasureChestId]=@treasureChestId AND [status]=1";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@treasureChestId", SqlDbType.BigInt){Value = treasureChestId}, 
            };
            count = 0;
            amount = 0.0;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                if (dr.Read())
                {

                    count = dr.GetInt32(0);
                    amount = dr.GetDouble(1);
                }

            }
        }

        public void Sum(int activityId, out double amount)
        {
            const string cmdText = @"SELECT ISNULL(ROUND(SUM([Amount]),2),0) AS [A] FROM [dbo].[RedEnvelope] WITH(NOLOCK) WHERE [activityId]=@activityId AND [status]=1";

            SqlParameter[] cmdParms = new SqlParameter[]
            {
                new SqlParameter("@activityId", SqlDbType.BigInt){Value = activityId}, 
            };
            amount = 0.0;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParms))
            {
                if (dr.Read())
                {
                    amount = dr.GetDouble(0);
                }

            }
        }

        public IEnumerable<RedEnvelope> Ranklist()
        {
            const string cmdText = @"SELECT TOP 5 [mobilePhoneNumber],[Amount]=SUM([Amount]) FROM [dbo].[RedEnvelope] WITH(NOLOCK) 
                                        WHERE [isExecuted]>0 AND [isOverflow]=0
                                        GROUP BY [mobilePhoneNumber]
                                        ORDER BY [Amount] DESC";

            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText))
            {
                while (dr.Read())
                {
                    yield return dr.GetEntity<RedEnvelope>();
                }
            }
        }

        public bool RankLiskNumber(string mobilePhoneNumber, out long ranking, out double amount)
        {
            const string cmdText = @"WITH Ranklist ([mobilePhoneNumber],[Amount])AS (
SELECT [mobilePhoneNumber],[Amount]=SUM([Amount]) FROM [dbo].[RedEnvelope] WITH(NOLOCK)
WHERE [isExecuted]>0 AND [isOverflow]=0
GROUP BY [mobilePhoneNumber]
)
SELECT [rowId],[Amount] FROM (SELECT [rowId]=ROW_NUMBER() OVER(ORDER BY [Amount] DESC),[mobilePhoneNumber],[Amount] FROM Ranklist) AS t
WHERE t.[mobilePhoneNumber]=@mobilePhoneNumber";

            SqlParameter cmdParm = new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 20) { Value = mobilePhoneNumber };
            ranking = 0;
            amount = 0;
            using (var dr = SqlHelper.ExecuteReader(Connection, CommandType.Text, cmdText, cmdParm))
            {
                if (dr.Read())
                {
                    ranking = dr.GetInt64(0);
                    amount = dr.GetDouble(1);
                    return true;
                }
                return false;
            }
        }

        public int UpdateMobilePhoneNumberAndIsChange(long redEnvelopeId, string mobilePhoneNumber, string uuid)
        {
            const string cmdText = @"UPDATE [dbo].[RedEnvelope] SET [mobilePhoneNumber]=@mobilePhoneNumber,[isChange]=1,[lastUpdateTime]=@lastUpdateTime,[uuid]=@uuid WHERE [redEnvelopeId]=@redEnvelopeId";

            SqlParameter[] cmdParms = new SqlParameter[]
			{
				new SqlParameter("@mobilePhoneNumber",SqlDbType.NVarChar,20){Value = mobilePhoneNumber},
                new SqlParameter("@redEnvelopeId",SqlDbType.BigInt){Value = redEnvelopeId}, 
                new SqlParameter("@lastUpdateTime",SqlDbType.DateTime){Value = DateTime.Now}, 
                new SqlParameter("@uuid",SqlDbType.NVarChar,20){Value =uuid }
			};

            return SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);

        }

        //        public int UpdateCustomerRedEnvelopeAmount(string oldPhone, string newPhone, double amount, bool effect)
        //        {
        //            string cmdText = "";
        //            if (effect == true)
        //            {
        //                cmdText = @"UPDATE [dbo].[CustomerInfo] 
        // SET [executedRedEnvelopeAmount]=isnull(executedRedEnvelopeAmount,0)-@amount 
        // WHERE [mobilePhoneNumber]=@oldPhone and CustomerStatus=1 and (isnull(executedRedEnvelopeAmount,0)-@amount) >-0.01;
        //
        // UPDATE [dbo].[CustomerInfo] 
        // SET [executedRedEnvelopeAmount]=isnull(executedRedEnvelopeAmount,0)+@amount 
        // WHERE [mobilePhoneNumber]=@newPhone and CustomerStatus=1 and (isnull(executedRedEnvelopeAmount,0)+@amount) >-0.01";
        //            }
        //            else
        //            {
        //                cmdText = @"UPDATE [dbo].[CustomerInfo] 
        // SET [notExecutedRedEnvelopeAmount]=isnull(notExecutedRedEnvelopeAmount,0)-@amount 
        // WHERE [mobilePhoneNumber]=@oldPhone and CustomerStatus=1 and (isnull(notExecutedRedEnvelopeAmount,0)-@amount) >-0.01;
        //
        // UPDATE [dbo].[CustomerInfo] 
        // SET [notExecutedRedEnvelopeAmount]=isnull(notExecutedRedEnvelopeAmount,0)+@amount 
        // WHERE [mobilePhoneNumber]=@newPhone and CustomerStatus=1 and (isnull(notExecutedRedEnvelopeAmount,0)+@amount) >-0.01";
        //            }
        //            SqlParameter[] cmdParms = new SqlParameter[]
        //            {
        //                new SqlParameter("@amount",SqlDbType.Float){Value = amount},
        //                new SqlParameter("@oldPhone",SqlDbType.NVarChar,20){Value = oldPhone},
        //                new SqlParameter("@newPhone",SqlDbType.NVarChar,20){Value = newPhone}, 
        //            };
        //            return SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        //        }

        //        public int AddCustomerRedEnvelopeAmount(string Phone, double amount, bool effect)
        //        {
        //            string cmdText = "";
        //            if (effect == true)
        //            {
        //                cmdText = @"UPDATE [dbo].[CustomerInfo] 
        // SET [executedRedEnvelopeAmount]=isnull(executedRedEnvelopeAmount,0)+@amount 
        // WHERE [mobilePhoneNumber]=@Phone and CustomerStatus=1 and (isnull(executedRedEnvelopeAmount,0) + @amount) >-0.01;";
        //            }
        //            else
        //            {
        //                cmdText = @"UPDATE [dbo].[CustomerInfo] 
        // SET [notExecutedRedEnvelopeAmount]=isnull(notExecutedRedEnvelopeAmount,0)+@amount 
        // WHERE [mobilePhoneNumber]=@Phone and CustomerStatus=1 and (isnull(notExecutedRedEnvelopeAmount,0) + @amount) >-0.01;";
        //            }
        //            SqlParameter[] cmdParms = new SqlParameter[]
        //            {
        //                new SqlParameter("@amount",SqlDbType.Float){Value = amount},
        //                new SqlParameter("@Phone",SqlDbType.NVarChar,20){Value = Phone},
        //            };
        //            return SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, cmdText, cmdParms);
        //        }

        public List<AppUUIDModel> GetCustomerDeviceUUID(string Phone, int activityId)
        {
//            const string cmdText = @"select A.uuid,A.updateTime from DeviceInfo A 
//left join CustomerConnDevice B with(nolock) on A.deviceId=B.deviceId
//left join CustomerInfo C with(nolock) on C.CustomerID=B.customerId
//where C.mobilePhoneNumber=@mobilePhoneNumber and C.CustomerStatus=1
//and A.uuid not in (select isnull(uuid,'') from RedEnvelope with(nolock) where activityId=@activityId and ISNULL(uuid,'')!='')";

             const string cmdText = @"select * from (
 select uuid,updateTime from DeviceInfo a where exists(select 1 
 from CustomerConnDevice b where a.deviceId=b.deviceId 
and EXISTS(select 1 from CustomerInfo c where c.mobilePhoneNumber=@mobilePhoneNumber
 and c.CustomerID=b.customerId))) dd
where 
 not exists(select 1 from RedEnvelope R with(nolock) WHERE activityId=@activityId AND R.uuid=dd.uuid)";

            SqlParameter[] cmdParms = new SqlParameter[]
			{
                new SqlParameter("@mobilePhoneNumber",SqlDbType.NVarChar,20){Value = Phone},
                 new SqlParameter("@activityId",SqlDbType.Int,4){Value = activityId}
			};
            var list = new List<AppUUIDModel>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParms))
            {
                while (dr.Read())
                {
                    list.Add(new AppUUIDModel()
                    {
                        uuid = dr["uuid"] == DBNull.Value ? "" : Convert.ToString(dr["uuid"]),
                        updateTime = dr["updateTime"] == DBNull.Value ? Convert.ToDateTime("2014/01/01 00:00:00") : Convert.ToDateTime(dr["updateTime"])
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 判断微信用户是否抢过
        /// </summary>
        /// <param name="activityId">活动号</param>
        /// <param name="weChatUserId">微信用户id</param>
        /// <returns></returns>
        public RedEnvelope GetWeChatModel(int activityId, Guid weChatUserId)
        {
            string sql = "SELECT * FROM RedEnvelope WHERE activityId=@activityId AND WeChatUser_Id=@WeChatUserId";
            SqlParameter[] parameters = { 
                                        new SqlParameter("@activityId", activityId),
                                        new SqlParameter("@WeChatUserId", weChatUserId)};
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader drea = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString(), parameters))
            {
                if (drea.Read())
                    return SqlHelper.GetEntity<RedEnvelope>(drea);
                return null;
            }
        }

        /// <summary>
        /// 按活动号跟uuid返回model
        /// </summary>
        /// <param name="activityId">活动id</param>
        /// <param name="mobile">设备号</param>
        /// <returns></returns>
        public RedEnvelope GetAcitvityIdAndUuidModel(int activityId, string mobile)
        {
            string sql = "SELECT * FROM RedEnvelope WHERE activityId=@activityId AND uuid=(select top 1 uuid from DeviceInfo where deviceId=(select top 1 deviceId from CustomerConnDevice where customerId=(select top 1 customerId from CustomerInfo where mobilePhoneNumber=@mobile) order by updateTime desc))";
            SqlParameter[] parameters = { 
                                        new SqlParameter("@activityId", activityId),
                                        new SqlParameter("@mobile", mobile)};
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader drea = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString(), parameters))
            {
                if (drea.Read())
                    return SqlHelper.GetEntity<RedEnvelope>(drea);
                return null;
            }
        }

        /// <summary>
        /// 更新微信用户id
        /// </summary>
        /// <param name="redEnvelopeId">id</param>
        /// <param name="weChatUserId">微信用户id</param>
        /// <returns></returns>
        public int UpdateWeChatUserId(long redEnvelopeId, Guid weChatUserId)
        {
            string sql = "UPDATE RedEnvelope SET WeChatUser_Id=@WeChatUserId WHERE redEnvelopeId=@redEnvelopeId AND WeChatUser_Id IS NULL";
            SqlParameter[] parameters = { 
                                        new SqlParameter("@WeChatUserId", weChatUserId),
                                        new SqlParameter("@redEnvelopeId", redEnvelopeId)};
            return SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, sql, parameters);
        }

        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="redEnvelopeId">id</param>
        /// <returns></returns>
        public int DelSingleData(long redEnvelopeId)
        {
            string sql = "DELETE FROM RedEnvelope WHERE redEnvelopeId=@redEnvelopeId AND mobilePhoneNumber!=''";
            SqlParameter[] parameters = { 
                                        new SqlParameter("@redEnvelopeId", redEnvelopeId)};
            return SqlHelper.ExecuteNonQuery(Connection, CommandType.Text, sql, parameters);
        }
    }
}