using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
//
//  Copyright 2013 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2013-06-06.
//
namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// Vip政策及用户Vip信息数据操作类
    /// </summary>
    public class VipManager
    {
        /// <summary>
        /// new新增公司Vip政策信息
        /// </summary>
        /// <param name="companyVip"></param>
        /// <returns></returns>
        public long InsertCompanyVip(CompanyVipInfo companyVip)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into CompanyVipInfo(");
                    strSql.Append("name,companyId,nextRequirement,");
                    strSql.Append("sequence,discount,status)");
                    strSql.Append(" values (");
                    strSql.Append("@name,@companyId,@nextRequirement,");
                    strSql.Append("@sequence,@discount,@status)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					        new SqlParameter("@name", SqlDbType.NVarChar,50),
                            new SqlParameter("@companyId", SqlDbType.Int,4),
                            new SqlParameter("@nextRequirement",SqlDbType.Int,4),
                            new SqlParameter("@sequence",SqlDbType.Int,4),
                            new SqlParameter("@discount",SqlDbType.Float,8),
                            new SqlParameter("@status",SqlDbType.Int,4)};
                    parameters[0].Value = companyVip.name;
                    parameters[1].Value = companyVip.companyId;
                    parameters[2].Value = companyVip.nextRequirement;
                    parameters[3].Value = companyVip.sequence;
                    parameters[4].Value = companyVip.discount;
                    parameters[5].Value = 1;

                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);

                }
                catch (Exception)
                {
                    return 0;
                }
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
        /// new新增用户Vip信息
        /// </summary>
        /// <param name="customerVip"></param>
        /// <returns></returns>
        public long InsertCustomerVip(CustomerVipInfo customerVip)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into CustomerVipInfo(");
                    strSql.Append("customerId,companyId,companyVipId,lastOrderTime,currentDayCount,");
                    strSql.Append("userCompletedOrderCount,userTotalOrderAmount,status)");
                    strSql.Append(" values (");
                    strSql.Append("@customerId,@companyId,@companyVipId,@lastOrderTime,@currentDayCount,");
                    strSql.Append("@userCompletedOrderCount,@userTotalOrderAmount,@status)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					        new SqlParameter("@customerId", SqlDbType.BigInt,8),
                            new SqlParameter("@companyId", SqlDbType.Int,4),
                            new SqlParameter("@companyVipId",SqlDbType.BigInt,8),
                            new SqlParameter("@lastOrderTime",SqlDbType.DateTime),
                            new SqlParameter("@currentDayCount",SqlDbType.Int,4),
                            new SqlParameter("@userCompletedOrderCount",SqlDbType.Int,4),
                            new SqlParameter("@userTotalOrderAmount",SqlDbType.Float,8),
                            new SqlParameter("@status",SqlDbType.Int,4)};
                    parameters[0].Value = customerVip.customerId;
                    parameters[1].Value = customerVip.companyId;
                    parameters[2].Value = customerVip.companyVipId;
                    parameters[3].Value = customerVip.lastOrderTime;
                    parameters[4].Value = 1;
                    parameters[5].Value = 1;
                    parameters[6].Value = customerVip.userTotalOrderAmount;
                    parameters[7].Value = 1;

                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);

                }
                catch (Exception)
                {
                    return 0;
                }
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
        /// new新增用户Vip记录信息
        /// </summary>
        /// <param name="customerVipLog"></param>
        /// <returns></returns>
        public bool InsertCustomerVipLog(CustomerVipLog customerVipLog)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into CustomerVipLog(");
                    strSql.Append("customerId,companyId,companyVipId,time,isLevelUp)");
                    strSql.Append(" values (");
                    strSql.Append("@customerId,@companyId,@companyVipId,@time,@isLevelUp)");
                    parameters = new SqlParameter[]{
					        new SqlParameter("@customerId", SqlDbType.BigInt,8),
                            new SqlParameter("@companyId", SqlDbType.Int,4),
                            new SqlParameter("@companyVipId",SqlDbType.BigInt,8),
                            new SqlParameter("@time",SqlDbType.DateTime),
                            new SqlParameter("@isLevelUp",SqlDbType.Bit)};
                    parameters[0].Value = customerVipLog.customerId;
                    parameters[1].Value = customerVipLog.companyId;
                    parameters[2].Value = customerVipLog.companyVipId;
                    parameters[3].Value = customerVipLog.time;
                    parameters[4].Value = customerVipLog.isLevelUp;

                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);

                }
                catch (Exception)
                {
                    return false;
                }
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
        /// 更新公司Vip政策信息
        /// </summary>
        /// <param name="companyVip"></param>
        /// <returns></returns>
        public bool UpdateCompanyVip(CompanyVipInfo companyVip)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CompanyVipInfo set ");
                    strSql.Append("name=@name,");
                    strSql.Append("nextRequirement=@nextRequirement,");
                    strSql.Append("discount=@discount");
                    strSql.Append(" where id=@id ");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@name", SqlDbType.NVarChar,50),
                    new SqlParameter("@nextRequirement", SqlDbType.Int,4),
                    new SqlParameter("@discount", SqlDbType.Float),
					new SqlParameter("@id", SqlDbType.BigInt,8)};
                    parameters[0].Value = companyVip.name;
                    parameters[1].Value = companyVip.nextRequirement;
                    parameters[2].Value = companyVip.discount;
                    parameters[3].Value = companyVip.id;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return false;
                }
                if (result == 1)
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
        /// 关闭公司Vip政策信息
        /// </summary>
        /// <param name="companyVip"></param>
        /// <returns></returns>
        public bool DeleteCompanyVipStatus(int companyId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("Delete CompanyVipInfo");
                    strSql.Append(" where companyId=@companyId ");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@companyId", SqlDbType.Int,4)};
                    parameters[0].Value = companyId;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return false;
                }
                if (result > 0)
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
        /// 更新用户Vip信息
        /// </summary>
        /// <param name="companyVipId"></param>
        /// <returns></returns>
        public bool UpdateCustomerCompanyVip(bool add, double amount, long companyVipId, long customerCompanyVipId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update dbo.CustomerVipInfo set ");
                    strSql.Append("companyVipId=@companyVipId,");
                    if (add)
                    {
                        strSql.Append("lastOrderTime=GETDATE(),");
                    }
                    strSql.Append("userCompletedOrderCount=isnull(userCompletedOrderCount, 0) + @count,");
                    strSql.Append("userTotalOrderAmount=isnull(userTotalOrderAmount, 0) + @amount,");
                    strSql.Append("currentDayCount=case when (isnull(currentDayCount, 0) + @currentDayCount) > 2");
                    strSql.Append(" or DateDiff(day,GetDate(),lastOrderTime) <> 0 then 1");
                    strSql.Append(" else (isnull(currentDayCount, 0) + @currentDayCount) end");
                    strSql.Append(" where id=@id and (isnull(userCompletedOrderCount, 0) + @count) > -1");
                    strSql.Append(" and (isnull(userTotalOrderAmount, 0) + @amount) > -0.01");
                    strSql.Append(" and (isnull(currentDayCount, 0) + @currentDayCount) > -1");
                    strSql.Append(" and ((DateDiff(day,GetDate(),lastOrderTime) = 0  and (isnull(currentDayCount, 0) + @currentDayCount) < 3) or DateDiff(day,GetDate(),lastOrderTime) <> 0)");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@companyVipId", SqlDbType.BigInt,8),
                    new SqlParameter("@count", SqlDbType.Int,4),
                    new SqlParameter("@amount", SqlDbType.Float),
                    new SqlParameter("@currentDayCount", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.BigInt,8)};
                    parameters[0].Value = companyVipId;
                    if (add)
                    {
                        parameters[1].Value = 1;
                        parameters[2].Value = amount;
                        parameters[3].Value = 1;
                    }
                    else
                    {
                        parameters[1].Value = -1;
                        parameters[2].Value = -amount;
                        parameters[3].Value = -1;
                    }
                    parameters[4].Value = customerCompanyVipId;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return false;
                }
                if (result == 1)
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
        /// 查询指定公司的Vip政策
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public DataTable SelectCompanyVip(int companyId, int sequence = 0, bool orderAsc = false)
        {
            string strSql = @"select [id],[name],[companyId],[nextRequirement],[sequence],[discount],[status]
 from CompanyVipInfo where companyId = @companyId and status > 0";
            if (sequence > 0)
            {
                if (orderAsc)
                {
                    strSql += " and sequence > @sequence order by sequence asc";
                }
                else
                {
                    strSql += " and sequence < @sequence order by sequence desc";
                }
            }
            else
            {
                strSql += " order by sequence asc";
            }
            SqlParameter[] parameter = { new SqlParameter("@companyId", SqlDbType.Int, 4) { Value = companyId },
                                       new SqlParameter("@sequence", SqlDbType.Int, 4) { Value = sequence }};
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询指定用户在指定公司的Vip信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public DataTable SelectCustomerCompanyVip(long customerId, int companyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A.id as customerCompanyVipId,A.[customerId],A.[companyVipId],A.[userCompletedOrderCount],");
            strSql.Append("A.[companyId],A.[userTotalOrderAmount],B.name,B.nextRequirement,B.discount,B.sequence,A.lastOrderTime,A.currentDayCount");
            strSql.Append(" from CustomerVipInfo as A inner join CompanyVipInfo as B on A.companyVipId = B.id");
            strSql.Append(" where A.companyId = @companyId and A.customerId=@customerId and A.status > 0 and B.status > 0");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@companyId", SqlDbType.Int) { Value = companyId },
            new SqlParameter("@customerId", SqlDbType.BigInt) { Value = companyId },
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }
    }
}
