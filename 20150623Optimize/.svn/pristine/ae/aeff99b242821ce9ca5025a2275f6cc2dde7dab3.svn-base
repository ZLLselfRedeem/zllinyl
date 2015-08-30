using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data;
using System.Data.SqlClient;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 宝箱中的红包
    /// 2014-7-31
    /// </summary>
    public class RedEnvelopeManager
    {
        /// <summary>
        /// 新建宝箱时
        /// </summary>
        /// <param name="redEnvelope"></param>
        /// <returns></returns>
        public long InsertRedEnvelope(RedEnvelope redEnvelope)
        {
            const string strSql = @"INSERT INTO [VAGastronomistMobileApp].[dbo].[RedEnvelope]
           ([treasureChestId],[Amount],[mobilePhoneNumber],[status],[getTime],[isExecuted],[isExpire],[expireTime]
           ,[unusedAmount],[activityId],[isOwner],[isOverflow],[cookie],[isChange],[uuid],[effectTime],[from])
     VALUES
           (0,@Amount,@mobilePhoneNumber,@status,@getTime,@isExecuted,@isExpire,@expireTime
           ,@unusedAmount,@activityId,@isOwner,@isOverflow,@cookie,@isChange,@uuid,@effectTime,@from)
select @@identity";

            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@Amount", SqlDbType.Float) { Value = redEnvelope.Amount },
                new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = redEnvelope.mobilePhoneNumber },
                new SqlParameter("@status", SqlDbType.Bit) { Value = redEnvelope.status },
                new SqlParameter("@getTime", SqlDbType.DateTime) { Value = redEnvelope.getTime },
                new SqlParameter("@isExecuted", SqlDbType.Int) { Value = redEnvelope.isExecuted },
                new SqlParameter("@isExpire", SqlDbType.Bit) { Value = redEnvelope.isExpire },
                new SqlParameter("@expireTime", SqlDbType.DateTime) { Value = redEnvelope.expireTime },
                new SqlParameter("@unusedAmount", SqlDbType.Float) { Value = redEnvelope.unusedAmount },
                new SqlParameter("@activityId", SqlDbType.Int) { Value = redEnvelope.activityId },
                new SqlParameter("@isOwner", SqlDbType.Bit) { Value = redEnvelope.isOwner },
                new SqlParameter("@isOverflow", SqlDbType.Bit) { Value = redEnvelope.isOverflow },
                new SqlParameter("@cookie", SqlDbType.NVarChar, 100) { Value = redEnvelope.cookie },
                new SqlParameter("@isChange", SqlDbType.Bit) { Value = redEnvelope.isChange },                
                new SqlParameter("@uuid", SqlDbType.NVarChar, 100) { Value = redEnvelope.uuid },                
                new SqlParameter("@effectTime", SqlDbType.DateTime) { Value = redEnvelope.effectTime },                
                new SqlParameter("@from", SqlDbType.NVarChar, 50) { Value = redEnvelope.from }
                };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
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

        /// <summary>
        /// 客户领取某宝箱中红包时，要更新此红包数据
        /// </summary>
        /// <param name="redEnvelope"></param>
        /// <returns></returns>
        public bool UpdateRedEnvelope(RedEnvelope redEnvelope)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update RedEnvelope set ");
            strSql.Append("mobilePhoneNumber = @mobilePhoneNumber,");
            strSql.Append("getTime = @getTime");
            strSql.Append("isExecuted = @isExecuted");
            strSql.Append("isExpire = @isExpire");
            strSql.Append("expireTime = @expireTime");
            strSql.Append("unusedAmount = @unusedAmount");
            strSql.Append(" where redEnvelopeId = @redEnvelopeId");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = redEnvelope.mobilePhoneNumber },
                new SqlParameter("@getTime", SqlDbType.DateTime) { Value = redEnvelope.getTime },
                new SqlParameter("@isExecuted", SqlDbType.Int) { Value = redEnvelope.isExecuted },
                new SqlParameter("@isExpire", SqlDbType.Bit) { Value = redEnvelope.isExpire },
                new SqlParameter("@expireTime", SqlDbType.DateTime) { Value = redEnvelope.expireTime },
                new SqlParameter("@unusedAmount", SqlDbType.Float) { Value = redEnvelope.unusedAmount },
                new SqlParameter("@redEnvelopeId", SqlDbType.BigInt) { Value = redEnvelope.redEnvelopeId }
                };
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
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
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 根据用户cookie更新其已领取红包记录中的电话号码
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        //public bool UpdateRedEnvelope(string mobilePhoneNumber, string cookie)
        //{
        //    string strSql = "update RedEnvelope set mobilePhoneNumber = @mobilePhoneNumber where cookie = @cookie";
        //    SqlParameter[] para = new SqlParameter[] {
        //    new SqlParameter("@mobiePhoneNumber", SqlDbType.NVarChar, 50) { Value = mobilePhoneNumber },
        //    new SqlParameter("@cookie", SqlDbType.NVarChar, 100) { Value = cookie }
        //    };
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
        //        if (i > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        /// <summary>
        /// 根据用户手机号码查询其领到的所有红包，包括宝箱信息
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public DataTable SelectRedEnvelope(string mobilePhoneNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select isnull(red.mobilePhoneNumber,'匿名用户') ownerPhone,tre.amount treasureChestAmout,tre.count,");
            strSql.Append("red.redEnvelopeId,red.treasureChestId,red.Amount redEnvelopeAmount,red.isExecuted,red.mobilePhoneNumber,red.getTime,red.unusedAmount,red.expireTime,red.activityId");
            strSql.Append(" from RedEnvelope red inner join TreasureChestConfig tre");
            strSql.Append(" on red.activityId = tre.activityId and tre.status=1");
            strSql.Append(" and red.mobilePhoneNumber = @mobilePhoneNumber order by red.getTime desc");

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = mobilePhoneNumber }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }
        /// <summary>
        /// 查看宝箱红包
        /// </summary>
        /// <param name="treasureChestId"></param>
        /// <returns></returns>
        //        public List<RedEnvelopeViewModel> SelectRedEnvelopeViewModel(long treasureChestId)
        //        {
        //            string strSql = @"select A.Amount,A.getTime,case when A.isExecuted=1 then '是'
        //else '否' end as isExecuted,case when B.mobilePhoneNumber is null 
        //then '匿名用户' else B.UserName end as UserName,
        //
        //case when A.redEnvelopeId in (select RedEnvelopeConnPreOrder.redEnvelopeId from RedEnvelopeConnPreOrder) then '已使用' 
        //when  D.redEnvelopeExpirationTime<GETDATE() then '已过期'  
        //when  D.stateType=1 then '未生效' 
        //when  D.stateType=2 then '已生效'
        //else '已删除' end as stateType,
        //
        //B.mobilePhoneNumber,A.redEnvelopeId
        //from RedEnvelope A 
        //left join CustomerInfo B on A.cookie=B.cookie
        //left join RedEnvelopeDetails D on D.redEnvelopeId=A.redEnvelopeId 
        //left join RedEnvelopeConnPreOrder C on A.redEnvelopeId=C.redEnvelopeId
        //where B.CustomerStatus=1 and A.treasureChestId='" + treasureChestId + "'";
        //            List<RedEnvelopeViewModel> list = new List<RedEnvelopeViewModel>();
        //            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql))
        //            {
        //                while (dr.Read())
        //                {
        //                    list.Add(SqlHelper.GetEntity<RedEnvelopeViewModel>(dr));
        //                }
        //            }
        //            return list;
        //        }
        /// <summary>
        /// 查询红包使用的点单
        /// </summary>
        /// <param name="redEnvelopeId"></param>
        /// <returns></returns>
        public DataTable SelectRedEnvelopeUsedOrder(long redEnvelopeId)
        {
            string strSql = @"select A.prePaidSum ,A.prePayTime,A.refundMoneySum
from PreOrder19dian A
inner join RedEnvelopeConnPreOrder B on A.preOrder19dianId=B.preOrder19dianId
where B.redEnvelopeId=@redEnvelopeId";

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@redEnvelopeId", SqlDbType.BigInt) { Value = redEnvelopeId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询用户所有可使用红包
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public List<RedEnvelope> SelectCustomerAvailableRedEnvelope(string mobilePhoneNumber)
        {
            const string strSql = @"SELECT  [redEnvelopeId] ,[treasureChestId],[Amount] ,[mobilePhoneNumber],[status] ,[lastUpdateTime]
      ,[getTime] ,[isExecuted] ,[isExpire] ,[expireTime] ,[unusedAmount] ,[activityId] ,[isOwner] ,[isOverflow]
FROM [RedEnvelope]
where [expireTime]>GETDATE()
and [mobilePhoneNumber]=@mobilePhoneNumber and status=1
and [unusedAmount]>0 and effectTime<GETDATE()
and [isExecuted]=1 and isOverflow=0 order by [expireTime] asc";//过滤溢出红包
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar,20) { Value = mobilePhoneNumber }
            };
            var list = new List<RedEnvelope>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                while (sdr.Read())
                {
                    list.Add(SqlHelper.GetEntity<RedEnvelope>(sdr));
                }
            }
            return list;
        }
        /// <summary>
        /// 支付点单批量修改红包状态
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="isExecuted"></param>
        /// <param name="unusedAmount"></param>
        /// <param name="redEnvelopeIdStr"></param>
        /// <returns></returns>
        public bool UpdateRedEnvelope(string mobilePhoneNumber, int isExecuted, double unusedAmount, string redEnvelopeIdStr)
        {
            string strSql = String.Format(@"update RedEnvelope set isExecuted = @isExecuted,unusedAmount = @unusedAmount
 where mobilePhoneNumber = @mobilePhoneNumber and redEnvelopeId in {0}", redEnvelopeIdStr);
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 20) { Value = mobilePhoneNumber },
                new SqlParameter("@isExecuted", SqlDbType.Int) { Value = isExecuted },//2 标记为已使用，待处理
                new SqlParameter("@unusedAmount", SqlDbType.Float) { Value = unusedAmount }
                };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                return i > 0;
            }
        }
        /// <summary>
        /// 支付点单批量修改红包状态（半个红包调用）
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="isExecuted"></param>
        /// <param name="unusedAmount"></param>
        /// <param name="redEnvelopeIdStr"></param>
        /// <returns></returns>
        public bool UpdateHrefRedEnvelope(string mobilePhoneNumber, int isExecuted, double unusedAmount, string redEnvelopeIdStr)
        {
            string strSql = String.Format(@"update RedEnvelope set isExecuted = @isExecuted,unusedAmount =unusedAmount+ @unusedAmount
  where mobilePhoneNumber = @mobilePhoneNumber and redEnvelopeId in {0}", redEnvelopeIdStr);
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 20) { Value = mobilePhoneNumber },
                new SqlParameter("@isExecuted", SqlDbType.Int) { Value = isExecuted },//2 标记为已使用，待处理
                new SqlParameter("@unusedAmount", SqlDbType.Float) { Value = unusedAmount }
                };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {

                }
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                return i > 0;
            }
        }
        /// <summary>
        /// 更新用户红包余额
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="executedRedEnvelopeAmount"></param>
        /// <returns></returns>
        public bool UpdateCustomerRedEnvelope(string mobilePhoneNumber, double executedRedEnvelopeAmount)
        {
            const string strSql = @"update CustomerInfo set executedRedEnvelopeAmount = @executedRedEnvelopeAmount+executedRedEnvelopeAmount
 where mobilePhoneNumber = @mobilePhoneNumber ";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 20) { Value = mobilePhoneNumber },
                new SqlParameter("@executedRedEnvelopeAmount", SqlDbType.Float) { Value = executedRedEnvelopeAmount }
                };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                return i > 0;
            }
        }

        /// <summary>
        /// 更新点单过期时间
        /// </summary>
        /// <param name="preOrderId">点单编号</param>
        /// <param name="dtTime">过期时间</param>
        /// <returns></returns>
        public bool UpdatePreOrderExpireTime(long preOrderId, DateTime dtTime)
        {
            const string strSql = @"update PreOrder19dian set expireTime = @expireTime where preOrder19dianId = @preOrder19dianId ";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@expireTime", SqlDbType.DateTime) { Value = dtTime },
                new SqlParameter("@preOrder19dianId", SqlDbType.BigInt, 8) { Value = preOrderId }
                };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
                return i > 0;
            }
        }

        /// <summary>
        /// 根据用户cookie或者手机号码查询其红包总额（可使用+未生效）
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public double SelectCustomerRedEnvelope(string mobilePhoneNumber)
        {
            const string strSql = @"select (isnull(executedRedEnvelopeAmount,0)+isnull(notExecutedRedEnvelopeAmount,0)) amount 
                                      from CustomerInfo
                                      where mobilePhoneNumber = @mobilePhoneNumber";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar,50) { Value = mobilePhoneNumber }
            };
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Math.Round(Convert.ToDouble(obj), 2);
            }
        }

        /// <summary>
        /// 查看用户的已生效或未生效红包总额
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="executedAmount">value：true/false 已生效/未生效</param>
        /// <returns></returns>
        public double SelectCustomerPartRedEnvelope(string mobilePhoneNumber, bool executedAmount, bool inValid = false, string uuid = "")
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select isnull(SUM(Amount),0) from RedEnvelope  r inner join Activity a  on r.activityId=a.activityId
                                      where mobilePhoneNumber = @mobilePhoneNumber and isExpire=0 ");

            List<SqlParameter> paraList = new List<SqlParameter>();
            paraList.Add(new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = mobilePhoneNumber });

            if (executedAmount)//已生效金额
            {
                if (inValid)
                {
                    strSql.Append(" and isExecuted=1 and isOverflow=0 and a.activityType=" + (int)ActivityType.节日免单红包);
                    strSql.Append(" and not exists (select 1 from RedEnvelope rr where rr.activityId=r.activityId and rr.uuid=@uuid)");
                    paraList.Add(new SqlParameter("@uuid", SqlDbType.NVarChar, 100) { Value = uuid });
                }
                else
                {
                    strSql.Append(" and isExecuted=1 and a.activityType!=" + (int)ActivityType.节日免单红包);
                }
            }
            else
            {
                if (inValid)//作废的未生效金额
                {
                    strSql.Append(" and isExecuted=0 and isOverflow=0 and a.activityType=" + (int)ActivityType.节日免单红包);
                    strSql.Append(" and exists (select 1 from RedEnvelope rr where rr.activityId=r.activityId and rr.uuid=@uuid)");
                }
                else//获得的未生效金额
                {
                    strSql.Append(" and isExecuted=0 and isOverflow=0 and a.activityType=" + (int)ActivityType.节日免单红包);
                    strSql.Append(" and not exists (select 1 from RedEnvelope rr where rr.activityId=r.activityId and rr.uuid=@uuid)");
                }
                paraList.Add(new SqlParameter("@uuid", SqlDbType.NVarChar, 100) { Value = uuid });
            }
            SqlParameter[] para = paraList.ToArray();
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(obj);
            }
        }
        /// <summary>
        /// 根据宝箱Id查询用户分到的钱
        /// </summary>
        /// <param name="treasureChestId"></param>
        /// <returns></returns>
        public double SelectCustomerRedEnvelopeAmount(long treasureChestId, string mobilePhoneNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Round(Amount,2) from RedEnvelope where treasureChestId = @treasureChestId and mobilePhoneNumber=@mobilePhoneNumber");
            strSql.Append(" and status=1 and GETDATE()<expireTime and unusedAmount>0 and isOverflow=0 and isExecuted=1");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@treasureChestId", SqlDbType.BigInt) { Value = treasureChestId },
            new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = mobilePhoneNumber },
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDouble(obj);
                }
            }
        }
        /// <summary>
        /// 根据宝箱Id查询抢到红包的手机号码
        /// </summary>
        /// <param name="treasureChestId"></param>
        /// <returns></returns>
        public List<string> SelectCustomerMobilePhone(long treasureChestId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select mobilePhoneNumber from RedEnvelope where status=1 and GETDATE()<expireTime and unusedAmount>0 and isOverflow=0 and isExecuted=1");
            strSql.Append(" and treasureChestId=@treasureChestId");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@treasureChestId", SqlDbType.BigInt) { Value = treasureChestId }
             };
            List<string> str = new List<string>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                while (sdr.Read())
                {
                    str.Add(sdr[0].ToString());
                }
            }
            return str;
        }

        /// <summary>
        /// 查询该UUID是否领过红包
        /// true:领过
        /// false:没领过
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public bool CheckUuidAlreadyUsed(string uuid)
        {
            string strSql = "select uuid from RedEnvelope where uuid=@uuid";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@uuid",SqlDbType.NVarChar,100){ Value = uuid }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 作废用户的红包
        /// </summary>
        /// <param name="redEnvelope"></param>
        /// <returns></returns>
        public bool UpdateRedEnvelopeStatus(CustomerRedEnvelope redEnvelope)
        {
            StringBuilder strSql = new StringBuilder("update RedEnvelope set isExecuted=" + (int)VARedEnvelopeStateType.已作废 + " from RedEnvelope r inner join Activity a on r.activityId=a.activityId");
            strSql.Append(" where mobilePhoneNumber=@mobilePhoneNumber and isExecuted=0 and a.activityType=" + (int)ActivityType.节日免单红包);
            strSql.Append(" and exists (select 1 from RedEnvelope rr where rr.activityId=r.activityId and rr.uuid=@uuid)");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 20) { Value = redEnvelope.mobilePhoneNumber },
            new SqlParameter("@uuid", SqlDbType.NVarChar, 100) { Value = redEnvelope.uuid }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
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
        /// <summary>
        /// 把用户获得到的未生效红包uuid补上
        /// </summary>
        /// <param name="redEnvelope"></param>
        /// <returns></returns>
        public bool UpdateRedEnvelopeUuid(CustomerRedEnvelope redEnvelope)
        {
            StringBuilder strSql = new StringBuilder("update RedEnvelope set uuid=@uuid from RedEnvelope r inner join Activity a on r.activityId=a.activityId");
            strSql.Append(" where mobilePhoneNumber=@mobilePhoneNumber and isExecuted=0 and a.activityType=" + (int)ActivityType.节日免单红包);
            strSql.Append(" and not exists (select 1 from RedEnvelope rr where rr.activityId=r.activityId and rr.uuid=@uuid)");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 20) { Value = redEnvelope.mobilePhoneNumber },
            new SqlParameter("@uuid", SqlDbType.NVarChar, 100) { Value = redEnvelope.uuid }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
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

        public List<TopRedEnvelopeRankList> GetTopRankList()
        {
            const string strSql = @"SELECT TOP 5 [mobilePhoneNumber],[Amount]=SUM([Amount]) FROM [dbo].[RedEnvelope] WITH(NOLOCK) 
                                        WHERE mobilePhoneNumber in (
'15958193591',
'15990111524',
'13018950518',
'13486376512',
'18658159565',
'15155211695',
'15968898567',
'13067833303',
'13868093138',
'18968165965',
'13331991032',
'13867429433',
'13958059992',
'15395710115',
'18817579044',
'15382392583',
'18507385766',
'15397057427',
'15958172007',
'13588794780')
                                         and [isExecuted]>0 AND [isOverflow]=0 GROUP BY [mobilePhoneNumber]
                                        ORDER BY [Amount] DESC";
            var list = new List<TopRedEnvelopeRankList>();

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                using (SqlDataReader sdr = SqlHelper.ExecuteReader(conn, CommandType.Text, strSql))
                {
                    while (sdr.Read())
                    {
                        list.Add(sdr.GetEntity<TopRedEnvelopeRankList>());
                    }
                }
            }
            return list;
        }
        public bool BatchInsert(DataTable dt)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    SqlTransaction sqlbulkTransaction = conn.BeginTransaction();
                    SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, sqlbulkTransaction);
                    sqlbulkcopy.DestinationTableName = dt.TableName;//数据库中的表名
                    sqlbulkcopy.BulkCopyTimeout = 30;
                    foreach (DataColumn dc in dt.Columns)
                    {
                        sqlbulkcopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                    }
                    sqlbulkcopy.WriteToServer(dt);
                    sqlbulkTransaction.Commit();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public DataTable SelectCustomerPhone()
        {
            string strSql = "select mobilePhoneNumber from CustomerInfo where RegisterDate>'2014-11-3' and ISNULL(mobilePhoneNumber,'')!='';";
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql);
            return ds.Tables[0];
        }

        public List<RedEnvelopeConnOrder3> SelectRedEnvelopeConnPreOrder3(long preOrder19dianId)
        {
            List<RedEnvelopeConnOrder3> connPreOrder = new List<RedEnvelopeConnOrder3>();
            string strSql = @"select preOrder19dianId,conn.redEnvelopeId,conn.currectUsedAmount
                                    from RedEnvelopeConnPreOrder conn  inner join RedEnvelope r on conn.redEnvelopeId = r.redEnvelopeId                                 
                                    and conn.preOrder19dianId=@preOrder19dianId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt){ Value = preOrder19dianId}
            };
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    connPreOrder.Add(sdr.GetEntity<RedEnvelopeConnOrder3>());
                }
            }
            return connPreOrder;
        }

        public List<RedEnvelopeRefund> SelectRedEnvelopeRefund(long preOrder19dianId)
        {
            List<RedEnvelopeRefund> refund = new List<RedEnvelopeRefund>();
            string strSql = @"select conn.redEnvelopeId,conn.currectUsedAmount,ISNULL(r.Amount,0)-ISNULL(r.unusedAmount,0) canRefundAmount
                                    from RedEnvelopeConnPreOrder conn  inner join RedEnvelope r on conn.redEnvelopeId = r.redEnvelopeId                                 
                                    and conn.preOrder19dianId=@preOrder19dianId and ISNULL(r.Amount,0)-ISNULL(r.unusedAmount,0)>0 order by r.expireTime desc";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt){ Value = preOrder19dianId}
            };
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    refund.Add(sdr.GetEntity<RedEnvelopeRefund>());
                }
            }
            return refund;
        }

        /// <summary>
        /// 修改红包详情使用金额
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <param name="backAmount"></param>
        /// <returns></returns>
        //public bool updateRedEnvelopePaidAmount(long preOrder19dianId, double backAmount)
        //{
        //    string strSql = "update RedEnvelopeDetails set usedAmount=ISNULL(usedAmount,0)-@backAmount where preOrder19dianId=@preOrder19dianId and stateType=" + (int)VARedEnvelopeStateType.已使用;
        //    SqlParameter[] para = new SqlParameter[] { 
        //    new SqlParameter("@backAmount", SqlDbType.Float){ Value = backAmount},
        //    new SqlParameter("@preOrder19dianId", SqlDbType.BigInt){ Value = preOrder19dianId}
        //    };
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
        //        if (i > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        /// <summary>
        /// 将已使用的红包改为已生效，未使用金额改掉
        /// 将退款红包还给用户
        /// </summary>
        /// <param name="strRedEnvelopeId"></param>
        /// <returns></returns>
        public bool UpdateRedEnvelopeStatus(long strRedEnvelopeId, double unusedAmount)
        {
            string strSql = "update RedEnvelope set isExecuted=" + (int)VARedEnvelopeStateType.已生效 + ",unusedAmount=isnull(unusedAmount,0)+@unusedAmount where redEnvelopeId =@strRedEnvelopeId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@unusedAmount", SqlDbType.Float ){ Value = unusedAmount},
            new SqlParameter("@strRedEnvelopeId", SqlDbType.BigInt ){ Value = strRedEnvelopeId}
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
        /// <summary>
        /// 更新用户红包表，将天天红包和大红包还回去
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="backExecutedRedEnvelopeAmount"></param>
        /// <returns></returns>
        //public bool UpdateCustomerRedEnvelope2(long customerId, double backExecutedRedEnvelopeAmount)
        //{
        //    string strSql = "update CustomerInfo set executedRedEnvelopeAmount=ISNULL(executedRedEnvelopeAmount,0)+@backExecutedRedEnvelopeAmount where customerId=@customerId";
        //    SqlParameter[] para = new SqlParameter[] { 
        //    new SqlParameter("@backExecutedRedEnvelopeAmount", SqlDbType.Float ){ Value = backExecutedRedEnvelopeAmount},
        //    new SqlParameter("@customerId", SqlDbType.BigInt ){ Value = customerId}
        //    };
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
        //        if (i > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        public List<TopRedEnvelopeRankList> GetRedEnvelopeSend(int activityId, string mobilePhoneNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.mobilePhoneNumber,a.Amount from ( select ROW_NUMBER() over(order by mobilePhoneNumber) row,");
            strSql.Append("mobilePhoneNumber,Amount from RedEnvelope where activityId=@activityId and ISNULL(mobilePhoneNumber,'')!=''");
            List<SqlParameter> paraList = new List<SqlParameter>();
            paraList.Add(new SqlParameter("@activityId", SqlDbType.Int) { Value = activityId });
            if (!string.IsNullOrEmpty(mobilePhoneNumber))
            {
                strSql.Append("and mobilePhoneNumber=@mobilePhoneNumber");
                paraList.Add(new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = mobilePhoneNumber });
            }
            strSql.Append(")a where a.row between @startIndex and @endIndex");
            SqlParameter[] para = paraList.ToArray();
            List<TopRedEnvelopeRankList> redEnvelope = new List<TopRedEnvelopeRankList>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                while (sdr.Read())
                {
                    redEnvelope.Add(SqlHelper.GetEntity<TopRedEnvelopeRankList>(sdr));
                }
            }
            return redEnvelope;
        }

        /// <summary>
        /// 查询某个活动已经领取金额
        /// </summary>
        /// <param name="treasureChestId"></param>
        /// <returns></returns>
        public double Sum(int activityId)
        {
            const string strSql = @"SELECT ISNULL(SUM([Amount]),0) AS Amount FROM [dbo].[RedEnvelope] WHERE [activityId]=@activityId AND [status]=1";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@activityId", SqlDbType.Int) { Value =activityId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDouble(obj);
                }
            }
        }

        /// <summary>
        /// 查询用户是否已经领取某个活动的红包
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public bool CheckCustomerHadRedEnvelope(string mobilePhoneNumber, int activityId)
        {
            const string strSql = "select 1 from RedEnvelope where activityId=@activityId and mobilePhoneNumber=@mobilePhoneNumber";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@activityId", SqlDbType.Int) { Value = activityId },
            new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 20) { Value = mobilePhoneNumber }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public long Add(RedEnvelope redEnvelope)
        {
            const string strSql = @"INSERT INTO [dbo].[RedEnvelope]
		   ([treasureChestId]
		   ,[Amount]
		   ,[mobilePhoneNumber]
		   ,[status]           
		   ,[getTime]
		   ,[isExecuted]
		   ,[isExpire]
		   ,[expireTime]
		   ,[unusedAmount],[activityId],[isOwner],[isOverflow],[cookie],[uuid],[effectTime])
	 VALUES
		   (@treasureChestId
		   ,@Amount
		   ,@mobilePhoneNumber
		   ,@status
		   ,@getTime
		   ,@isExecuted
		   ,@isExpire
		   ,@expireTime
		   ,@unusedAmount,@activityId,@isOwner,@isOverflow,@cookie,@uuid,@effectTime); select @@identity;";

            SqlParameter[] para = new SqlParameter[]
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

        /// <summary>
        /// 查询某个用户所有的可用红包
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public double QueryCustomerExcutedRedEnvelope(string mobilePhoneNumber)
        {
            string strSql = @"select ROUND(ISNULL(SUM(unusedAmount),0),2) amount from RedEnvelope where mobilePhoneNumber=@mobilePhoneNumber      
and expireTime>GETDATE()
and unusedAmount>0
and isOverflow=0
and effectTime<=GETDATE()";

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 20) { Value = mobilePhoneNumber }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDouble(obj);
                }
            }
        }

        public double QueryCustomerNotExecutedRedEnvelope(string mobilePhoneNumber)
        {
            string strSql = @"select ROUND(ISNULL(SUM(Amount),0),2) amount from RedEnvelope where mobilePhoneNumber=@mobilePhoneNumber      
and expireTime>GETDATE()
and unusedAmount>0
and isOverflow=0
and effectTime>GETDATE()";

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 20) { Value = mobilePhoneNumber }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDouble(obj);
                }
            }
        }

        /// <summary>
        /// 查询用户即将过期的红包个数
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public int QueryExpireCount(string mobilePhoneNumber, int redEnvelopeExpireDay)
        {
            string strSql = @"select COUNT(1) from RedEnvelope where mobilePhoneNumber=@mobilePhoneNumber
and unusedAmount>0 and effectTime<GETDATE()
and expireTime>GETDATE()
and expireTime-GETDATE()<=@expireDay ";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 20) { Value = mobilePhoneNumber },
            new SqlParameter("@expireDay", SqlDbType.Int) { Value = redEnvelopeExpireDay }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(obj);
                }
            }
        }
        /// <summary>
        /// 财务查询指定时期指定类别红包支付金额
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="activityType"></param>
        /// <returns></returns>
        public double QueryFinanceRedEnvelopePay(DateTime beginTime, DateTime endTime, int activityType)
        {
            string strSql = @"select ROUND(ISNULL(SUM(currectUsedAmount),0),2) UsedAmount from RedEnvelopeConnPreOrder conn inner join 
RedEnvelope r on conn.redEnvelopeId = r.redEnvelopeId
and conn.currectUsedTime between @beginTime and @endTime
inner join Activity a on r.activityId = a.activityId";
            if (activityType > 0)
            {
                strSql += " and a.activityType=@activityType";
            }
            List<SqlParameter> paraList = new List<SqlParameter>();
            paraList.Add(new SqlParameter("@beginTime", SqlDbType.DateTime) { Value = beginTime });
            paraList.Add(new SqlParameter("@endTime", SqlDbType.DateTime) { Value = endTime });
            if (activityType > 0)
            {
                paraList.Add(new SqlParameter("@activityType", SqlDbType.Int) { Value = activityType });
            }
            SqlParameter[] para = paraList.ToArray();
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDouble(obj);
                }
            }
        }

        /// <summary>
        /// 财务查询指定时期红包退款金额
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public double QueryFinanceRedEnvelopeRefund(DateTime beginTime, DateTime endTime)
        {
            string strSql = @"select ROUND(ISNULL(SUM(refundRedEnvelope),0),2) from PreOrder19dian
where isPaid=1 and prePayTime between @beginTime and @endTime";

            SqlParameter[] para = new SqlParameter[] {
            new SqlParameter("@beginTime", SqlDbType.DateTime) { Value = beginTime },
            new SqlParameter("@endTime", SqlDbType.DateTime) { Value = endTime }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDouble(obj);
                }
            }
        }

        /// <summary>
        /// 查询某个用户所有未过期的且无uuid的红包
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public List<RedEnvelopeOfActivity> QueryRedEnvelopeOfActivity(string mobilePhoneNumber)
        {
            const string strSql = "select redEnvelopeId,activityId from RedEnvelope where mobilePhoneNumber=@mobilePhoneNumber and isnull(uuid,'')='' and expireTime > GETDATE()";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mobilePhoneNumber", SqlDbType.BigInt) { Value=  mobilePhoneNumber}
            };
            List<RedEnvelopeOfActivity> redEnvelopeOfActivityList = new List<RedEnvelopeOfActivity>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                while (sdr.Read())
                {
                    redEnvelopeOfActivityList.Add(SqlHelper.GetEntity<RedEnvelopeOfActivity>(sdr));
                }
            }
            return redEnvelopeOfActivityList;
        }

        /// <summary>
        /// 查询某个活动是否在某个设备上领过
        /// </summary>
        /// <param name="activityId">活动ID</param>
        /// <param name="uuid">设备UUID</param>
        /// <returns></returns>
        public bool QueryRedEnvelopeIsOwnUUID(int activityId, string uuid)
        {
            const string strSql = "select 1 from RedEnvelope where activityId=@activityId and uuid=@uuid";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@activityId", SqlDbType.Int) { Value = activityId },
            new SqlParameter("@uuid", SqlDbType.NVarChar,100) { Value = uuid }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        /// <summary>
        /// 将某个红包状态改为已作废
        /// </summary>
        /// <param name="redEnvelopeId"></param>
        /// <returns></returns>
        public bool CancelRedEnvelope(long redEnvelopeId)
        {
            string strSql = "update RedEnvelope set isExecuted=" + (int)VARedEnvelopeStateType.已作废 + ",unusedAmount=0 where redEnvelopeId=@redEnvelopeId";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@redEnvelopeId" ,SqlDbType.BigInt) { Value = redEnvelopeId }
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
        /// <summary>
        /// 更新某个红包的uuid
        /// </summary>
        /// <param name="redEnvelopeId"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public bool UpdateRedEnvelopeUUID(long redEnvelopeId, string uuid)
        {
            string strSql = "update RedEnvelope set uuid=@uuid where redEnvelopeId=@redEnvelopeId and isnull(uuid,'')=''";
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@uuid", SqlDbType.NVarChar, 100) { Value = uuid },
                new SqlParameter("@redEnvelopeId" ,SqlDbType.BigInt) { Value = redEnvelopeId }
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

        /// <summary>
        /// 财务红包统计查询
        /// </summary>
        /// <param name="beginDT"></param>
        /// <param name="endDT"></param>
        /// <returns></returns>
        public DataTable SelectRedEnvelopeFinance(DateTime beginDT, DateTime endDT, int activityType, int cityID)
        {
            StringBuilder strSql = new StringBuilder();
            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@beginDT", SqlDbType.DateTime) { Value = beginDT });
            para.Add(new SqlParameter("@endDT", SqlDbType.DateTime) { Value = endDT });
            strSql.Append("select COUNT(distinct p.preOrder19dianId) preOrder19dianIds,");
            strSql.Append(" CAST(SUM(a.amount) as numeric(18,2)) currectUsedAmounts,");
            strSql.Append(" CAST(SUM(prePaidSum) as numeric(18,2)) prePaidSums,");
            strSql.Append(" CAST(SUM(refundRedEnvelope) as numeric(18,2)) refundRedEnvelopes");
            strSql.Append(" from(");
            strSql.Append(" select conn.preOrder19dianId,SUM(conn.currectUsedAmount) amount");
            strSql.Append(" from RedEnvelopeConnPreOrder conn");
            if (activityType > 0)
            {
                strSql.Append(" inner join RedEnvelope b on b.redEnvelopeId=conn.redEnvelopeId");
                strSql.Append(" inner join Activity c on b.activityId=c.activityId");
            }
            strSql.Append(" where conn.currectUsedTime between @beginDT and @endDT");
            if (activityType > 0)
            {
                strSql.Append(" and c.activityType=@activityType");
                para.Add(new SqlParameter("@activityType", SqlDbType.Int) { Value = activityType });
            }
            strSql.Append(" group by conn.preOrder19dianId) a");
            strSql.Append(" inner join PreOrder19dian p on a.preOrder19dianId=p.preOrder19dianId");
            if (cityID > 0)
            {
                strSql.Append(" inner join ShopInfo s on p.shopId=s.shopID");
                strSql.Append(" where s.cityID=@cityID");
                para.Add(new SqlParameter("@cityID", SqlDbType.Int) { Value = cityID });
            }

            SqlParameter[] pa = para.ToArray();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), pa);
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询指定红包领取情况（0：数量；1：金额）
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public RedEnvelopeConsume SelectRedEnvelopeConsume(int activityId)
        {
            const string strSql = @"select COUNT(1) consumeCount,round(SUM(Amount),2) consumeAmount from RedEnvelope where activityId=@activityId
and ISNULL(mobilePhoneNumber,'')!=''";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@activityId", SqlDbType.Int) { Value = activityId }
            };
            RedEnvelopeConsume redEnvelopeConsume = new RedEnvelopeConsume();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (sdr.Read())
                {
                    redEnvelopeConsume = SqlHelper.GetEntity<RedEnvelopeConsume>(sdr);
                }
            }
            return redEnvelopeConsume;
        }

        /// <summary>
        /// 根据红包Id，查询某用户领取的红包金额
        /// </summary>
        /// <param name="activityId"></param>
        /// <returns></returns>
        public double SelectRedEnvelopeAmount(long redEnvelopeId)
        {
            const string strSql = "select Amount from RedEnvelope where redEnvelopeId=@redEnvelopeId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@redEnvelopeId", SqlDbType.BigInt) { Value = redEnvelopeId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return 0;
                }
                else
                {
                    return Math.Round(Convert.ToDouble(obj), 2);
                }
            }
        }

        /// <summary>
        /// 用户全额退款，作废其中奖红包
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public bool CancelAwardRedEnvelope(long redEnvelopeId)
        {
            string strSql = "update RedEnvelope set isExecuted=" + (int)VARedEnvelopeStateType.已作废;
            strSql += ",expireTime=GETDATE(),effectTime=GETDATE() where redEnvelopeId=@redEnvelopeId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@redEnvelopeId", SqlDbType.BigInt) { Value = redEnvelopeId }
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

        /// <summary>
        /// 用户中奖订单对账后，把中奖红包生效
        /// </summary>
        /// <param name="activityId"></param>
        /// <param name="expireTime"></param>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public bool EffectAwardRedEnvelope(long redEnvelopeId, DateTime expireTime)
        {
            const string strSql = "update RedEnvelope set effectTime=GetDate(),expireTime=@expireTime where redEnvelopeId=@redEnvelopeId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@redEnvelopeId", SqlDbType.BigInt) { Value = redEnvelopeId },
            new SqlParameter("@expireTime", SqlDbType.DateTime) { Value = expireTime }
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
    }
}
