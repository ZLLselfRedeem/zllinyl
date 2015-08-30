using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 宝箱操作数据访问层接口
    /// </summary>
    public interface ITreasureChestManager
    {
        List<TreasureChest> SelectTreasureChest(Page page, int activityId);
        int GetNotLockTreasureChestCount(long redEnvelopeId);
        bool UpdateTreasureChestOwner(string mobilePhoneNumber, string cookie);
        bool UpdateTreasureChestAmount(int ActivityId, double amount);
        long InsertTreasureChest(TreasureChest treasureChest);
        TreasureChest GetByActivity(int activityId);
    }
    /// <summary>
    /// 宝箱操作数据访问层
    /// </summary>
    public class TreasureChestManager : ITreasureChestManager
    {
        /// <summary>
        /// 查询宝箱列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public List<TreasureChest> SelectTreasureChest(Page page, int activityId)
        {
            List<TreasureChest> treasureChests = new List<TreasureChest>();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select ROW_NUMBER() over (order by treasureChestId) rownum,");
            strSql.Append(" [treasureChestId],[treasureChestConfigId],[mobilePhoneNumber],[cookie],[createTime],[url],[executedTime],[isExpire],[expireTime],[status] ");
            strSql.Append(" from TreasureChestConfig ");
            strSql.Append(" where rownum between @startIndex and @endIndex");
            List<SqlParameter> paraList = new List<SqlParameter>();
            paraList.Add(new SqlParameter("@startIndex", SqlDbType.Int) { Value = page.Skip + 1 });
            paraList.Add(new SqlParameter("@endIndex", SqlDbType.Int) { Value = page.Skip + page.PageSize });
            SqlParameter[] para = paraList.ToArray();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()))
            {
                while (sdr.Read())
                {
                    treasureChests.Add(SqlHelper.GetEntity<TreasureChest>(sdr));
                }
            }
            return treasureChests;
        }
        /// <summary>
        /// 查询某个红包对应宝箱开锁人数
        /// </summary>
        /// <param name="redEnvelopeId"></param>
        /// <returns></returns>
        public int GetNotLockTreasureChestCount(long redEnvelopeId)
        {
            string strSql = @"select (A.count-A.lockCount) as notLockCount
from TreasureChest A inner join RedEnvelope B on A.treasureChestId=B.treasureChestId
where B.redEnvelopeId=@redEnvelopeId ";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@redEnvelopeId",SqlDbType.BigInt,8){Value = redEnvelopeId}
            };
            var count = 0;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameters))
            {
                if (dr.Read())
                {
                    count = Convert.ToInt32(dr[0]);
                }
            }
            return count;
        }

        /// <summary>
        /// 根据用户cookie更新其手机号码
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public bool UpdateTreasureChestOwner(string mobilePhoneNumber, string cookie)
        {
            string strSql = @"update TreasureChest set mobilePhoneNumber = @mobiePhoneNumber
                                      where cookie = @cookie";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mobiePhoneNumber", SqlDbType.NVarChar, 50) { Value = mobilePhoneNumber },
            new SqlParameter("@cookie", SqlDbType.NVarChar, 100) { Value = cookie }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public TreasureChest GetByActivity(int activityId)
        {
            const string cmdText = @"SELECT  treasureChestId,treasureChestConfigId,activityId,amount,remainAmount,count,lockCount,
                                            mobilePhoneNumber,cookie,createTime,url,executedTime,isExpire,expireTime,status
                                             FROM [dbo].[TreasureChest] WITH(NOLOCK) WHERE [activityId]=@activityId AND [status]=1";

            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@activityId",SqlDbType.Int){Value = activityId}, 
            };


            TreasureChest treasureChest = null;
            using (SqlDataReader odr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, para))
            {
                if (odr.Read())
                {
                    treasureChest = odr.GetEntity<TreasureChest>();
                }
            }
            return treasureChest;
        }
        /// <summary>
        /// 活动开始
        /// </summary>
        /// <param name="ActivityId"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool UpdateTreasureChestAmount(int ActivityId, double amount)
        {
            const string strSql = @"update TreasureChest set amount = @amount where activityId = @activityId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@amount", SqlDbType.Float) { Value = amount },
            new SqlParameter("@activityId", SqlDbType.Int) { Value = ActivityId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public long InsertTreasureChest(TreasureChest treasureChest)
        {
            const string strSql = @"INSERT INTO [dbo].[TreasureChest]
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

            SqlParameter[] para = new SqlParameter[]
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
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                conn.Open();
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt64(obj);
                }
            }
        }
    }
}
