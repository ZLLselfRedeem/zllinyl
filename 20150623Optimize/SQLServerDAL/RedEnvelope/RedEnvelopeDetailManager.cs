using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public interface IRedEnvelopeDetailManager
    {
        //List<RedEnvelopeDetail> GetClientRedEnvelopeDetail(int pageIndex, int pageSize, string mobilePhoneNumber);
        List<WebRedEnvelopeDetailModel> GetWebRedEnvelopeDetail(int pageIndex, int pageSize, string phoneNum, string type, ref bool isHaveMore);
        //List<long> GetCustomerUnusedRedEnvelope(string mobilePhoneNumber, bool partUsed);
        //bool UpdateExpirationRedEnvelopeStatus(string mobilePhoneNumber, int stateType);
        //long InsertRedEnvelopeDetail(RedEnvelopeDetail model);
        //long InsertRedEnvelopeDetail_1(RedEnvelopeDetail model);
        //double[] GetCustomerRedEnvelope(string mobilePhoneNumber, bool flag = false);
        bool DoUpdateExpirationRedEnvelope(string mobilePhoneNumber);
        void DoUpdateExpirationTreasureChest();
        double GetCustomerExpirationRedEnvelopeAmount(string mobilePhoneNumber, bool flag);
        //bool UpdateCustomerRedEnvelope(string mobilePhoneNumber, double expirationEffectRedEnvelopeAmount, double expirationNotEffectRedEnvelopeAmount);
        /// <summary>
        /// 查询用户所有未生效的红包
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        List<NotEffectiveRedEnvelope> GetNotEffectiveRedEnvelope(string phone);
        //bool UpdateNotEffectiveRedEnvelopeAndDetail(string rIds);
        //int AddCustomerRedEnvelopeAmount(string Phone, double amount);
        //bool UpdateRedEnvelopeDetailStatus(CustomerRedEnvelope redEnvelope);
    }

    /// <summary>
    /// 红包领用记录数据访问
    /// </summary>
    public class RedEnvelopeDetailManager : IRedEnvelopeDetailManager
    {
        /// <summary>
        /// 分页查询红包领用记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        //public List<RedEnvelopeDetail> GetClientRedEnvelopeDetail(int pageIndex, int pageSize, string mobilePhoneNumber)
        //{
        //    PageQuery pageQuery = new PageQuery()
        //    {
        //        tableName = "RedEnvelopeDetails",
        //        fields =
        //            "Id,mobilePhoneNumber,redEnvelopeId,redEnvelopeAmount,redEnvelopeExpirationTime,operationTime,stateType,usedAmount,preOrder19dianId,shopName",
        //        orderField = "Id",
        //        sqlWhere = string.Format("mobilePhoneNumber={0}", mobilePhoneNumber)
        //    };
        //    Paging paging = new Paging()
        //    {
        //        pageIndex = pageIndex,
        //        pageSize = pageSize,
        //        recordCount = 0,
        //        pageCount = 0
        //    };
        //    List<RedEnvelopeDetail> data =
        //        CommonManager.GetPageData<RedEnvelopeDetail>(SqlHelper.ConnectionStringLocalTransaction, pageQuery,
        //            paging);
        //    return data;
        //}
        /// <summary>
        /// 分页web view查询红包领用记录
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="phoneNum"></param>
        /// <param name="isHaveMore"></param>
        /// <returns></returns>
        //        public List<WebRedEnvelopeViewModel> GetWebRedEnvelopeDetail(int pageIndex, int pageSize, string phoneNum, ref bool isHaveMore)
        //        {
        //            string sqlWhere = "A.mobilePhoneNumber='" + phoneNum + "' and A.stateType <>" + (int)VARedEnvelopeStateType.红包满 + " and ((stateType=2 and CAST(usedAmount as numeric(18,2))!=0) or (stateType!=2))";
        //            PageQuery pageQuery = new PageQuery()
        //            {
        //                tableName = @"RedEnvelopeDetails A with(NOLOCK)
        //  left join RedEnvelope E with(NOLOCK) on E.redEnvelopeId=A.redEnvelopeId
        //  left join PreOrder19dian B with(NOLOCK) on A.preOrder19dianId=B.preOrder19dianId
        //  left join ShopInfo C with(NOLOCK) on C.shopID=B.shopId
        //  left join Activity H with(NOLOCK) on H.activityId=E.activityId",
        //                fields = @"A.[redEnvelopeAmount],A.[redEnvelopeExpirationTime],A.[operationTime]
        //      ,A.[stateType],A.[usedAmount],C.shopName
        //      ,case when E.unusedAmount=0 then 1 else 0 end as flag,H.redEnvelopeEffectiveBeginTime",
        //                orderField = "operationTime desc",
        //                sqlWhere = sqlWhere
        //            };
        //            Paging paging = new Paging()
        //            {
        //                pageIndex = pageIndex,
        //                pageSize = pageSize,
        //                recordCount = 0,
        //                pageCount = 0
        //            };
        //            List<WebRedEnvelopeViewModel> data = CommonManager.GetPageData<WebRedEnvelopeViewModel>(SqlHelper.ConnectionStringLocalTransaction, pageQuery, paging);
        //            isHaveMore = paging.pageCount > pageIndex;
        //            return data;
        //        }
        public List<WebRedEnvelopeDetailModel> GetWebRedEnvelopeDetail(int pageIndex, int pageSize, string phoneNum, string type, ref bool isHaveMore)
        {
            string sqlWhere = "";
            PageQuery pageQuery = null;


            if (type == "present")
            {
                sqlWhere = "mobilePhoneNumber='" + phoneNum + "' and unusedAmount>0 and expireTime>GETDATE()";
                pageQuery = new PageQuery()
                {
                    tableName = @"RedEnvelope",
                    fields = @"Amount,round((Amount-unusedAmount),2) usedAmount,expireTime,effectTime,
statusType=case 
when isExecuted=6 then 6
when effectTime>GETDATE() then 1
when effectTime<=GETDATE() and Amount>=unusedAmount then 0
end",
                    orderField = "getTime desc",
                    sqlWhere = sqlWhere
                };
            }
            else
            {
                sqlWhere = "mobilePhoneNumber='" + phoneNum + "' and (unusedAmount=0 or expireTime<GETDATE())";
                pageQuery = new PageQuery()
                {
                    tableName = @"RedEnvelope",
                    fields = @"Amount,round((Amount-unusedAmount),2) usedAmount,expireTime,effectTime,
statusType=case
when isExecuted=6 then 6
when unusedAmount=0 then 2
when expireTime<GETDATE() then 3
end",
                    orderField = "getTime desc",
                    sqlWhere = sqlWhere
                }; 
            }

            
            Paging paging = new Paging()
            {
                pageIndex = pageIndex,
                pageSize = pageSize,
                recordCount = 0,
                pageCount = 0
            };
            List<WebRedEnvelopeDetailModel> data = CommonManager.GetPageData<WebRedEnvelopeDetailModel>(SqlHelper.ConnectionStringLocalTransaction, pageQuery, paging);
            isHaveMore = paging.pageCount > pageIndex;
            return data;
        }

        /// <summary>
        /// 查询用户红包编号
        /// </summary>
        /// <param name="mobilePhoneNumber">当前用户手机号码</param>
        /// <param name="partUserd">标记是否查询部分使用的红包</param>
        /// <returns></returns>
//        public List<long> GetCustomerUnusedRedEnvelope(string mobilePhoneNumber, bool partUserd)
//        {
//            string strSql = @"select A.redEnvelopeId
//                               from RedEnvelopeDetails A
//                               inner join RedEnvelopeConnPreOrder B on A.redEnvelopeId=B.redEnvelopeId
//                               where A.mobilePhoneNumber=@mobilePhoneNumber";
//            if (partUserd)
//            {
//                strSql += "A.stateType=1 group by A.redEnvelopeId,A.redEnvelopeAmount having SUM(B.currectUsedAmount)<A.redEnvelopeAmount";
//            }
//            SqlParameter parameter = new SqlParameter("@mobilePhoneNumber", mobilePhoneNumber);
//            List<long> list = new List<long>();
//            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
//            {
//                while (dr.Read())
//                {
//                    list.Add(Convert.ToInt64(dr[0]));
//                }
//            }
//            return list;
//        }
        /// <summary>
        /// 更新过期红包状态
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="stateType"></param>
        /// <returns></returns>
//        public bool UpdateExpirationRedEnvelopeStatus(string mobilePhoneNumber, int stateType)
//        {
//            StringBuilder strSql = new StringBuilder();
//            strSql.Append("update RedEnvelopeDetails set ");
//            strSql.Append("stateType=@stateType");
//            strSql.Append(@" where redEnvelopeExpirationTime<=GETDATE()
//                                   and (stateType=1 
//                                   or (stateType=2 and redEnvelopeId in (
//                                   select A.redEnvelopeId
//                                   from RedEnvelopeDetails A
//                                   inner join RedEnvelopeConnPreOrder B on A.redEnvelopeId=B.redEnvelopeId
//                                   where A.mobilePhoneNumber=@mobilePhoneNumber)))");
//            SqlParameter[] parameters =
//            {
//                new SqlParameter("@stateType", SqlDbType.Int, 1),
//                 new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 20)
//            };
//            parameters[0].Value = stateType;
//            parameters[1].Value = mobilePhoneNumber;

//            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text,
//                 strSql.ToString(), parameters) > 0;
//        }
        /// <summary>
        /// 添加红包使用详情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //        public long InsertRedEnvelopeDetail(RedEnvelopeDetail model)
        //        {
        //            const string strSql = @"insert into RedEnvelopeDetails(mobilePhoneNumber,operationTime,stateType,usedAmount,cookie,preOrder19dianId)
        //  values (@mobilePhoneNumber,@operationTime,@stateType,@usedAmount,@cookie,@preOrder19dianId) ;select @@IDENTITY";
        //            SqlParameter[] parameters = {
        //                    new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar,50),
        //                    new SqlParameter("@operationTime", SqlDbType.DateTime),
        //                    new SqlParameter("@stateType", SqlDbType.Int,4),
        //                    new SqlParameter("@usedAmount", SqlDbType.Float,8),
        //                    new SqlParameter("@cookie", SqlDbType.NVarChar,100),
        //                    new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8)};
        //            parameters[0].Value = model.mobilePhoneNumber;
        //            parameters[1].Value = model.operationTime;
        //            parameters[2].Value = model.stateType;
        //            parameters[3].Value = model.usedAmount;
        //            parameters[4].Value = model.cookie;
        //            parameters[5].Value = model.preOrder19dianId;
        //            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //            {
        //                if (conn.State != ConnectionState.Open)
        //                {
        //                    conn.Open();
        //                }
        //                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
        //                if (obj == null)
        //                {
        //                    return 0;
        //                }
        //                else
        //                {
        //                    return Convert.ToInt64(obj);
        //                }
        //            }
        //        }
        /// <summary>
        /// 添加红包过期详情
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        //        public long InsertRedEnvelopeDetail_1(RedEnvelopeDetail model)
        //        {
        //            const string strSql = @"insert into RedEnvelopeDetails(mobilePhoneNumber,operationTime,stateType,usedAmount)
        // values (@mobilePhoneNumber,@operationTime,@stateType,@usedAmount) ;select @@IDENTITY";
        //            SqlParameter[] parameters = {
        //                    new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar,50),
        //                    new SqlParameter("@operationTime", SqlDbType.DateTime),
        //                    new SqlParameter("@stateType", SqlDbType.Int,4),
        //                    new SqlParameter("@usedAmount", SqlDbType.Float,8)};
        //            parameters[0].Value = model.mobilePhoneNumber;
        //            parameters[1].Value = model.operationTime;
        //            parameters[2].Value = model.stateType;
        //            parameters[3].Value = model.usedAmount;
        //            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //            {
        //                if (conn.State != ConnectionState.Open)
        //                {
        //                    conn.Open();
        //                }
        //                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
        //                if (obj == null)
        //                {
        //                    return 0;
        //                }
        //                else
        //                {
        //                    return Convert.ToInt64(obj);
        //                }
        //            }
        //        }
        /// <summary>
        /// 根据用户cookie更新其领用记录中的电话号码
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        //public bool UpdateRedEnvelopeDetail(string mobilePhoneNumber, string cookie)
        //{
        //    string strSql = "update RedEnvelopeDetails set mobilePhoneNumber = @mobilePhoneNumber where cookie = @cookie";
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
        /// 查看当前手机号码的红包总额
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
//        public double[] GetCustomerRedEnvelope(string mobilePhoneNumber, bool flag = false)
//        {
//            string strSql = @"SELECT [executedRedEnvelopeAmount],[notExecutedRedEnvelopeAmount]
//                                            FROM [VAGastronomistMobileApp].[dbo].[CustomerInfo]
//                                           where mobilePhoneNumber=@mobilePhoneNumber";
//            if (flag)
//            {
//                strSql = String.Format(@"select (select SUM(Amount) 
// from RedEnvelope
// where isExecuted=1 and  mobilePhoneNumber=@mobilePhoneNumber
//  and isExpire=0 and isOverflow=0 and status =1
// group by isExpire,mobilePhoneNumber) as executedRedEnvelopeAmount,
// 
// (select SUM(Amount)
// from RedEnvelope
// where isExecuted=0 and  mobilePhoneNumber=@mobilePhoneNumber
// and isExpire=0 and isOverflow=0 and status=1
// group by isExpire,mobilePhoneNumber) as  notExecutedRedEnvelopeAmount");
//            }
//            SqlParameter parameter = new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 20) { Value = mobilePhoneNumber };
//            double[] list = new double[2];
//            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
//            {
//                if (dr.Read())
//                {
//                    list[0] = dr[0] == DBNull.Value ? 0 : Convert.ToDouble(dr[0]);
//                    list[1] = dr[1] == DBNull.Value ? 0 : Convert.ToDouble(dr[1]);
//                }
//            }
//            return list;
//        }

        /*
         处理红包过期业务
         */
        /// <summary>
        /// 处理过期红包
        /// </summary>
        public bool DoUpdateExpirationRedEnvelope(string mobilePhoneNumber)
        {
            const string strSql = @"update RedEnvelope set isExpire=@isExpire where expireTime<=GETDATE() and isExpire=0 and mobilePhoneNumber=@mobilePhoneNumber";
            SqlParameter[] parameters =
            {
                new SqlParameter("@isExpire", SqlDbType.Bit, 1),
                 new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 20),
            };
            parameters[0].Value = true;
            parameters[1].Value = mobilePhoneNumber;
            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);//可能没有数据处理操作，如果程序没有异常发生，则视为处理成功
            return result > 0;
        }
        /// <summary>
        /// 处理过期宝箱
        /// </summary>
        public void DoUpdateExpirationTreasureChest()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update TreasureChest set ");
            strSql.Append(" isExpire=@isExpire");
            strSql.Append(@" where expireTime<=GETDATE() and isExpire=0 and status=1");
            SqlParameter[] parameters =
            {
                new SqlParameter("@isExpire", SqlDbType.Bit, 1),
            };
            parameters[0].Value = true;
            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);//可能没有数据处理操作，如果程序没有异常发生，则视为处理成功
        }
        /// <summary>
        /// 查询当前用户过期红包的金额
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        public double GetCustomerExpirationRedEnvelopeAmount(string mobilePhoneNumber, bool flag)
        {
            string strSql = @"SELECT SUM(unusedAmount) Amount
  FROM [VAGastronomistMobileApp].[dbo].[RedEnvelope]
  where [mobilePhoneNumber]=@mobilePhoneNumber and status =1 and isExpire=0 and [expireTime]<= GETDATE() and isOverflow=0 and isExecuted=@isExecuted ";
            SqlParameter[] parameter = {new SqlParameter("@mobilePhoneNumber",SqlDbType.NVarChar,20) { Value = mobilePhoneNumber },
                                     new SqlParameter("@isExecuted",SqlDbType.Int,4){Value=flag==true?(int)VARedEnvelopeStateType.已生效:(int)VARedEnvelopeStateType.未生效}
                                       };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                if (dr.Read())
                {
                    return dr[0] == DBNull.Value ? 0 : Convert.ToDouble(dr[0]);
                }
            }
            return 0;
        }
        /// <summary>
        /// 更新用户红包余额
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
//        public bool UpdateCustomerRedEnvelope(string mobilePhoneNumber, double expirationEffectRedEnvelopeAmount, double expirationNotEffectRedEnvelopeAmount)
//        {
//            const string strSql = @" update [VAGastronomistMobileApp].[dbo].[CustomerInfo] set 
// executedRedEnvelopeAmount=@executedRedEnvelopeAmount+isnull(executedRedEnvelopeAmount,0),notExecutedRedEnvelopeAmount=@notExecutedRedEnvelopeAmount+isnull(notExecutedRedEnvelopeAmount,0)
// where mobilePhoneNumber=@mobilePhoneNumber and (isnull(executedRedEnvelopeAmount, 0) + @executedRedEnvelopeAmount) >-0.01
// and (isnull(notExecutedRedEnvelopeAmount, 0) + @notExecutedRedEnvelopeAmount) >-0.01";//确保更新红包后不能小于0
//            SqlParameter[] parameters =
//            {
//                new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 20),
//                new SqlParameter("@executedRedEnvelopeAmount", SqlDbType.Float),
//                new SqlParameter("@notExecutedRedEnvelopeAmount", SqlDbType.Float)
//            };
//            parameters[0].Value = mobilePhoneNumber;
//            parameters[1].Value = expirationEffectRedEnvelopeAmount;
//            parameters[2].Value = expirationNotEffectRedEnvelopeAmount;
//            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);//可能没有数据处理操作，如果程序没有异常发生，则视为处理成功
//            return result > 0;
//        }
        /// <summary>
        /// 查询用户所有未生效的红包
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        public List<NotEffectiveRedEnvelope> GetNotEffectiveRedEnvelope(string phone)
        {
            const string strSql = @"select RedEnvelope.redEnvelopeId,RedEnvelope.Amount
        from RedEnvelope inner join Activity on RedEnvelope.activityId=Activity.activityId
        where Activity.activityType=@activityType and Activity.redEnvelopeEffectiveBeginTime<=GETDATE()
        and isExecuted=0 and RedEnvelope.status=1 and isOverflow=0 and RedEnvelope.mobilePhoneNumber=@mobilePhoneNumber";
            SqlParameter[] parameter = 
            {
                new SqlParameter("@activityType",SqlDbType.Int,4){Value=(int)ActivityType.节日免单红包},
                new SqlParameter("@mobilePhoneNumber",SqlDbType.NVarChar,20){Value=phone}
            };
            var list = new List<NotEffectiveRedEnvelope>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<NotEffectiveRedEnvelope>(dr));
                }
            }
            return list;
        }

        //public bool UpdateNotEffectiveRedEnvelopeAndDetail(string rIds)
        //{
        //    string strSql = String.Format("update RedEnvelope set isExecuted=1 where redEnvelopeId in {0};update RedEnvelopeDetails set stateType=1 where redEnvelopeId in {0};", rIds);
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql);
        //        return i > 0;
        //    }
        //}
//        public int AddCustomerRedEnvelopeAmount(string Phone, double amount)
//        {
//            const string cmdText = @"UPDATE [dbo].[CustomerInfo] 
// SET [executedRedEnvelopeAmount]=isnull(executedRedEnvelopeAmount,0)+@amount 
// WHERE [mobilePhoneNumber]=@Phone and CustomerStatus=1 and (isnull(executedRedEnvelopeAmount,0) + @amount) >-0.01;
// UPDATE [dbo].[CustomerInfo] 
// SET [notExecutedRedEnvelopeAmount]=isnull(notExecutedRedEnvelopeAmount,0)+@amount1 
// WHERE [mobilePhoneNumber]=@Phone and CustomerStatus=1 and (isnull(notExecutedRedEnvelopeAmount,0) + @amount1) >-0.01;";
//            SqlParameter[] cmdParms = new SqlParameter[]
//            {
//                new SqlParameter("@amount",SqlDbType.Float){Value = amount},
//                new SqlParameter("@amount1",SqlDbType.Float){Value = (-1)*amount},
//                new SqlParameter("@Phone",SqlDbType.NVarChar,20){Value = Phone},
//            };
//            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, cmdText, cmdParms);
//        }

        /// <summary>
        /// 作废用户的红包
        /// </summary>
        /// <param name="redEnvelope"></param>
        /// <returns></returns>
        //public bool UpdateRedEnvelopeDetailStatus(CustomerRedEnvelope redEnvelope)
        //{
        //    StringBuilder strSql = new StringBuilder("update RedEnvelopeDetails set stateType=" + (int)VARedEnvelopeStateType.已作废 + ",operationTime=GETDATE() from RedEnvelopeDetails d inner join RedEnvelope r on d.redEnvelopeId=r.redEnvelopeId");
        //    strSql.Append(" inner join Activity a on r.activityId=a.activityId where r.mobilePhoneNumber=@mobilePhoneNumber and isExecuted=" + (int)VARedEnvelopeStateType.已作废 + " and a.activityType=" + (int)ActivityType.节日免单红包);
        //    strSql.Append(" and exists (select 1 from RedEnvelope rr where rr.activityId=r.activityId and rr.uuid=@uuid)");
        //    SqlParameter[] para = new SqlParameter[] { 
        //    new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 20) { Value = redEnvelope.mobilePhoneNumber },
        //    new SqlParameter("@uuid", SqlDbType.NVarChar, 100) { Value = redEnvelope.uuid }
        //    };
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
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
    }
}
