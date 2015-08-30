using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 红包相关统计
    /// 2014-10-30
    /// </summary>
    public class RedEnvelopeStatisticalManager
    {
        /// <summary>
        /// 按照条件查询所有用户红包信息
        /// </summary>
        /// <param name="name">活动名称</param>
        /// <param name="getTimeBegin">获取红包开始时间</param>
        /// <param name="getTimeEnd">获取红包结束时间</param>
        /// <param name="mobilePhoneNumber">电话号码</param>
        /// <param name="isReal">是否抓取真实抢红包数据</param>
        /// <param name="isUsed">是否已经使用</param>
        /// <returns></returns>
        public DataTable SelectData(string name, DateTime getTimeBegin, DateTime getTimeEnd, bool isReal = false, bool isUsed = false)
        {
            StringBuilder strSql = new StringBuilder();
            string select = "select COUNT(1) Count,SUM(Amount) Amount,COUNT(distinct mobilePhoneNumber) usedCount";
            strSql.Append(" from RedEnvelope r inner join Activity a");
            strSql.Append(" on r.activityId=a.activityId");
            strSql.Append(" and getTime between @getTimeBegin and @getTimeEnd");

            List<SqlParameter> paraList = new List<SqlParameter>();
            paraList.Add(new SqlParameter("@getTimeBegin", SqlDbType.DateTime) { Value = getTimeBegin });
            paraList.Add(new SqlParameter("@getTimeEnd", SqlDbType.DateTime) { Value = getTimeEnd });

            if (isUsed)//实际使用
            {
                select += ",SUM(currectUsedAmount) usedAmount";
                strSql.Append(" inner join RedEnvelopeConnPreOrder conn on r.redEnvelopeId=conn.redEnvelopeId and unusedAmount!=Amount");
                strSql.Append(" and conn.currectUsedTime between @usedTimeBegin and @usedTimeEnd");
                paraList.Add(new SqlParameter("@usedTimeBegin", SqlDbType.DateTime) { Value = getTimeBegin });
                paraList.Add(new SqlParameter("@usedTimeEnd", SqlDbType.DateTime) { Value = getTimeEnd });
            }
            if (!string.IsNullOrEmpty(name))
            {
                strSql.Append(" and a.name = @name");
                paraList.Add(new SqlParameter("@name", SqlDbType.NVarChar, 30) { Value = name });
            }
            if (isReal)//实际红包数据
            {
                strSql.Append(" and ISNULL(mobilePhoneNumber,'') !=''");
            }
            SqlParameter[] para = paraList.ToArray();

            select += strSql.ToString();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, select, para);
            return ds.Tables[0];
        }

        //DateTime registerTimeBegin, DateTime registerTimeEnd, DateTime payTimeBegin, DateTime payTimeEnd,

        /// <summary>
        /// 按照条件查询新用户红包信息
        /// </summary>
        /// <param name="name">活动名称</param>
        /// <param name="getTimeBegin">获取红包开始时间</param>
        /// <param name="getTimeEnd">获取红包结束时间</param>
        /// <param name="registerTimeBegin">注册时间起</param>
        /// <param name="registerTimeEnd">注册时间止</param>
        /// <returns></returns>
        public DataTable SelectNewCustomer(string name, DateTime getTimeBegin, DateTime getTimeEnd, DateTime registerTimeBegin, DateTime registerTimeEnd, bool isUsed = false)
        {
            StringBuilder strSql = new StringBuilder();

            string select = "select COUNT(1) Count,SUM(Amount) Amount,COUNT(distinct r.mobilePhoneNumber) usedCount";
            strSql.Append(" from RedEnvelope r inner join Activity a");
            strSql.Append(" on r.activityId=a.activityId");
            strSql.Append(" inner join CustomerInfo c on r.mobilePhoneNumber=c.mobilePhoneNumber");
            strSql.Append(" and r.getTime between @getTimeBegin and @getTimeEnd");
            strSql.Append(" and c.RegisterDate between @registerTimeBegin and @registerTimeEnd");

            List<SqlParameter> paraList = new List<SqlParameter>();
            paraList.Add(new SqlParameter("@getTimeBegin", SqlDbType.DateTime) { Value = getTimeBegin });
            paraList.Add(new SqlParameter("@getTimeEnd", SqlDbType.DateTime) { Value = getTimeEnd });
            paraList.Add(new SqlParameter("@registerTimeBegin", SqlDbType.DateTime) { Value = registerTimeBegin });
            paraList.Add(new SqlParameter("@registerTimeEnd", SqlDbType.DateTime) { Value = registerTimeEnd });

            if (isUsed)//实际使用
            {
                select += ",SUM(currectUsedAmount) usedAmount";
                strSql.Append(" inner join RedEnvelopeConnPreOrder conn on r.redEnvelopeId=conn.redEnvelopeId and unusedAmount!=Amount");
                strSql.Append(" and conn.currectUsedTime between @usedTimeBegin and @usedTimeEnd");
                paraList.Add(new SqlParameter("@usedTimeBegin", SqlDbType.DateTime) { Value = getTimeBegin });
                paraList.Add(new SqlParameter("@usedTimeEnd", SqlDbType.DateTime) { Value = getTimeEnd });
            }
            if (!string.IsNullOrEmpty(name))
            {
                strSql.Append(" and a.name = @name");
                paraList.Add(new SqlParameter("@name", SqlDbType.NVarChar, 30) { Value = name });
            }
            SqlParameter[] para = paraList.ToArray();
            
            select += strSql.ToString();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, select, para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询红包订单相关信息
        /// </summary>
        /// <param name="name">活动名称</param>
        /// <param name="getTimeBegin">获取红包时间起</param>
        /// <param name="getTimeEnd">获取红包时间止</param>
        /// <param name="registerTimeBegin">注册时间起</param>
        /// <param name="registerTimeEnd">注册时间止</param>
        /// <param name="payTimeBegin">支付时间起</param>
        /// <param name="payTimeEnd">支付时间止</param>
        /// <param name="cityId">城市ID</param>
        /// <param name="activityType">活动类别</param>
        /// <param name="isRegister">是否参考注册时间</param>
        /// <param name="isPay">是否参考支付时间</param>
        /// <param name="isAll">是否查询全部活动</param>
        /// <returns></returns>
        public DataTable SelectOrder(string name, DateTime getTimeBegin, DateTime getTimeEnd, DateTime registerTimeBegin, DateTime registerTimeEnd, DateTime payTimeBegin, DateTime payTimeEnd, int cityId, int activityType, bool isRegister = false, bool isAll = false)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COUNT(1) orderCount, SUM(currectUsedAmount) UsedAmount,SUM(prePaidSum) prePaidSum ,SUM(refundMoneySum)  refundMoneySum from RedEnvelopeConnPreOrder conn");
            strSql.Append(" inner join RedEnvelope r on conn.redEnvelopeId=r.redEnvelopeId");
            strSql.Append(" inner join Activity a on a.activityId = r.activityId");
            strSql.Append(" inner join PreOrder19dian p on conn.preOrder19dianId=p.preOrder19dianId");
            strSql.Append(" and p.isPaid=1 and isApproved=1");
            strSql.Append(" and r.getTime between @getTimeBegin and @getTimeEnd");
            strSql.Append(" and p.prePayTime between @payTimeBegin and @payTimeEnd");

            List<SqlParameter> paraList = new List<SqlParameter>();
            paraList.Add(new SqlParameter("@getTimeBegin", SqlDbType.DateTime) { Value = getTimeBegin });
            paraList.Add(new SqlParameter("@getTimeEnd", SqlDbType.DateTime) { Value = getTimeEnd });
            paraList.Add(new SqlParameter("@payTimeBegin", SqlDbType.DateTime) { Value = payTimeBegin });
            paraList.Add(new SqlParameter("@payTimeEnd", SqlDbType.DateTime) { Value = payTimeEnd });

            if (!string.IsNullOrEmpty(name))
            {
                strSql.Append(" and a.name = @name");
                paraList.Add(new SqlParameter("@name", SqlDbType.NVarChar, 30) { Value = name });
            }
            if (isRegister)
            {
                strSql.Append(" inner join customerInfo c on r.mobilePhoneNumber=c.mobilePhoneNumber and c.RegisterDate between @registerTimeBegin and @registerTimeEnd");
                paraList.Add(new SqlParameter("@registerTimeBegin", SqlDbType.DateTime) { Value = registerTimeBegin });
                paraList.Add(new SqlParameter("@registerTimeEnd", SqlDbType.DateTime) { Value = registerTimeEnd });
            }
            if (!isAll)
            {
                strSql.Append(" and not exists (");
                strSql.Append(" select 1 from RedEnvelopeConnPreOrder connn");
                strSql.Append(" inner join RedEnvelope rr on connn.redEnvelopeId = rr.redEnvelopeId and connn.preOrder19dianId=conn.preOrder19dianId");
                strSql.Append(" inner join Activity aa on rr.activityId=aa.activityId and aa.activityType!=" + activityType + ")");
            }
            if (cityId > 0)
            {
                strSql.Append(" inner join ShopInfo s on p.shopId=s.shopID and s.cityID=@cityId");
                paraList.Add(new SqlParameter("@cityId", SqlDbType.Int) { Value = cityId });
            }
            SqlParameter[] para = paraList.ToArray();

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询最高纪录门店流水
        /// </summary>
        /// <param name="name">活动名称</param>
        /// <param name="getTimeBegin">获取红包时间起</param>
        /// <param name="getTimeEnd">获取红包时间止</param>
        /// <param name="registerTimeBegin">注册时间起</param>
        /// <param name="registerTimeEnd">注册时间止</param>
        /// <param name="payTimeBegin">支付时间起</param>
        /// <param name="payTimeEnd">支付时间止</param>
        /// <param name="cityId">城市ID</param>
        /// <param name="activityType">活动类别</param>
        /// <param name="isRegister">是否参考注册时间</param>
        /// <param name="isPay">是否参考支付时间</param>
        /// <param name="isAll">是否查询全部活动</param>
        /// <returns></returns>
        public DataTable SelectTopShop(string name, DateTime getTimeBegin, DateTime getTimeEnd, DateTime registerTimeBegin, DateTime registerTimeEnd, DateTime payTimeBegin, DateTime payTimeEnd, int cityId, int activityType, bool isRegister = false, bool isAll = false)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 s.shopName,COUNT(distinct p.preOrder19dianId) orderCount,SUM(prePaidSum)prePaidSum,SUM(refundMoneySum)  refundMoneySum from RedEnvelopeConnPreOrder conn");
            strSql.Append(" inner join RedEnvelope r on conn.redEnvelopeId=r.redEnvelopeId");
            strSql.Append(" inner join Activity a on a.activityId = r.activityId");
            strSql.Append(" inner join PreOrder19dian p on conn.preOrder19dianId=p.preOrder19dianId");
            strSql.Append(" inner join ShopInfo s on p.shopId=s.shopID ");
            strSql.Append(" and p.isPaid=1 and isApproved=1");
            strSql.Append(" and r.getTime between @getTimeBegin and @getTimeEnd");
            strSql.Append(" and p.prePayTime between @payTimeBegin and @payTimeEnd");

            List<SqlParameter> paraList = new List<SqlParameter>();
            paraList.Add(new SqlParameter("@getTimeBegin", SqlDbType.DateTime) { Value = getTimeBegin });
            paraList.Add(new SqlParameter("@getTimeEnd", SqlDbType.DateTime) { Value = getTimeEnd });
            paraList.Add(new SqlParameter("@payTimeBegin", SqlDbType.DateTime) { Value = payTimeBegin });
            paraList.Add(new SqlParameter("@payTimeEnd", SqlDbType.DateTime) { Value = payTimeEnd });

            if (!string.IsNullOrEmpty(name))
            {
                strSql.Append(" and a.name = @name");
                paraList.Add(new SqlParameter("@name", SqlDbType.NVarChar, 30) { Value = name });
            }
            if (isRegister)
            {
                strSql.Append(" inner join customerInfo c on r.mobilePhoneNumber=c.mobilePhoneNumber and c.RegisterDate between @registerTimeBegin and @registerTimeEnd");
                paraList.Add(new SqlParameter("@registerTimeBegin", SqlDbType.DateTime) { Value = registerTimeBegin });
                paraList.Add(new SqlParameter("@registerTimeEnd", SqlDbType.DateTime) { Value = registerTimeEnd });
            }
            if (!isAll)
            {
                strSql.Append(" and not exists (");
                strSql.Append(" select 1 from RedEnvelopeConnPreOrder connn");
                strSql.Append(" inner join RedEnvelope rr on connn.redEnvelopeId = rr.redEnvelopeId and connn.preOrder19dianId=conn.preOrder19dianId");
                strSql.Append(" inner join Activity aa on rr.activityId=aa.activityId and aa.activityType!=" + activityType + ")");
            }
            if (cityId > 0)
            {
                strSql.Append(" and s.cityID=@cityId");
                paraList.Add(new SqlParameter("@cityId", SqlDbType.Int) { Value = cityId });
            }
            strSql.Append(" group by p.shopID,s.shopName order by COUNT(p.preOrder19dianId) desc");
            SqlParameter[] para = paraList.ToArray();

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询指定时间段内的N次消费次数
        /// </summary>
        /// <param name="dayNum"></param>
        /// <param name="paidNum"></param>
        /// <param name="payTimeBegin"></param>
        /// <param name="payTimeEnd"></param>
        /// <returns></returns>
        public int SelectPrePaidCount(int dayNum,int paidNum,DateTime payTimeBegin,DateTime payTimeEnd)
        {
            StringBuilder strSql = new StringBuilder();     
            strSql.Append("select COUNT(a.customerId) from (");
            strSql.Append(" select customerId from PreOrder19dian");
            strSql.Append(" where prePayTime between DATEADD(day,@dayNum,@payTimeBegin) and @payTimeEnd");
            strSql.Append(" and isPaid=1 and isApproved=1");
            strSql.Append(" group by customerId having COUNT(customerId)>=@paidNum) a");

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@dayNum",SqlDbType.Int) { Value = dayNum },
            new SqlParameter("@payTimeBegin", SqlDbType.DateTime) { Value = payTimeBegin },
            new SqlParameter("@payTimeEnd", SqlDbType.DateTime) { Value = payTimeEnd },
            new SqlParameter("@paidNum",SqlDbType.Int) { Value = paidNum }
            };
           object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
           return Convert.ToInt32(obj);
        }
    }
}
