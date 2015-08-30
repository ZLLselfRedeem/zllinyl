﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class MoneyMerchantAccountDetailManager
    {
        /// <summary>
        /// 基本添加定义
        /// </summary>
        private const string SQL_INSERT = @"insert into MoneyMerchantAccountDetail (flowNumber,accountMoney,remainMoney,accountType,accountTypeConnId,inoutcomeType,operUser
,operTime,companyId,shopId,remark)
                        values (@flowNumber,@accountMoney,@remainMoney,@accountType,@accountTypeConnId,@inoutcomeType,@operUser
,@operTime,@companyId,@shopId,@remark) 
select @@IDENTITY";

        public static double GetaccountMoneySum(string strWhere)
        {
            double val = 0;
            string strsql = @"select sum(total) from [dbo].[View_MoneyMerchantAccountDetail34]" + (strWhere != "" ? " where " + strWhere : "");
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, null))
            {
                if (dr.Read())
                {
                    val = !dr.IsDBNull(0) ? dr.GetDouble(0) : 0;
                }
            }
            return val;
        }

        public static long Insert(MoneyMerchantAccountDetail model)
        {
            var parm = new[] {
                        new SqlParameter("@flowNumber", model.flowNumber),
new SqlParameter("@accountMoney", model.accountMoney),
new SqlParameter("@remainMoney", model.remainMoney),
new SqlParameter("@accountType", model.accountType),
new SqlParameter("@accountTypeConnId", model.accountTypeConnId),
new SqlParameter("@inoutcomeType", model.inoutcomeType),
new SqlParameter("@operUser", model.operUser),
new SqlParameter("@operTime", model.operTime),
new SqlParameter("@companyId", model.companyId),
new SqlParameter("@shopId", model.shopId),
new SqlParameter("@remark", model.remark)
                        };
            long result = Convert.ToInt64(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, SQL_INSERT, parm));
            return result;
        }

        public static long InsertBalanceAccount(MoneyMerchantAccountDetail model)
        {
            string SQL_INSERT = @"insert into MoneyMerchantAccountDetail (flowNumber,accountMoney,remainMoney,accountType,accountTypeConnId,inoutcomeType,operUser
,operTime,companyId,shopId,remark,status";
            if (model.confirmTime != null)
            {
                SQL_INSERT += ",confirmTime";
            }
            SQL_INSERT += ")";
            SQL_INSERT += @" values (@flowNumber,@accountMoney,@remainMoney,@accountType,@accountTypeConnId,@inoutcomeType,@operUser
,@operTime,@companyId,@shopId,@remark,@status";
            if (model.confirmTime != null)
            {
                SQL_INSERT += ",@confirmTime";
            }
            SQL_INSERT += " )select @@IDENTITY";

            List<SqlParameter> paraList = new List<SqlParameter>();
            paraList.Add(new SqlParameter("@flowNumber", model.flowNumber));
            paraList.Add(new SqlParameter("@accountMoney", model.accountMoney));
            paraList.Add(new SqlParameter("@remainMoney", model.remainMoney));
            paraList.Add(new SqlParameter("@accountType", model.accountType));
            paraList.Add(new SqlParameter("@accountTypeConnId", model.accountTypeConnId));
            paraList.Add(new SqlParameter("@inoutcomeType", model.inoutcomeType));
            paraList.Add(new SqlParameter("@operUser", model.operUser));
            paraList.Add(new SqlParameter("@operTime", model.operTime));
            paraList.Add(new SqlParameter("@companyId", model.companyId));
            paraList.Add(new SqlParameter("@shopId", model.shopId));
            paraList.Add(new SqlParameter("@remark", model.remark));
            paraList.Add(new SqlParameter("@status", model.status));
            if (model.confirmTime != null)
            {
                paraList.Add(new SqlParameter("@confirmTime", model.confirmTime));
            }
            SqlParameter[] parm = paraList.ToArray();
            long result = Convert.ToInt64(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, SQL_INSERT, parm));
            return result;
        }

        /// <summary>
        /// 更新备注 20140416 add by wangc
        /// </summary>
        /// <param name="accountId"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool UpdateRemark(long accountId, string remark)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update MoneyMerchantAccountDetail set ");
            strSql.Append("remark=@remark");
            strSql.Append(" where accountId=@accountId");
            SqlParameter[] parameters = {
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@accountId", SqlDbType.BigInt,8)};
            parameters[0].Value = remark;
            parameters[1].Value = accountId;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public static int GetOrderCount(string strWhere,int CouponType)
        {
            int val = 0;
            string strsql = @"select sum([count]) from [dbo].[View_MoneyMerchantAccountDetail34] " + (strWhere != "" ? " where " + strWhere : "");
            if (CouponType == 0)
            {
                strsql = @"select sum([count]) from [dbo].[View_MoneyMerchantAccountDetail33] " + (strWhere != "" ? " where " + strWhere : "");
            }
            
            SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, null);
            if (dr.Read())
            {
                val = !dr.IsDBNull(0) ? dr.GetInt32(0) : 0;
            }
            dr.Close();
            return val;
        }

        public static List<MoneyMerchantAccountSumResponse> GetAccountTotal(int shopId, DateTime startDate,
            DateTime endDate,int CouponType)
        {
            string strWhere = (CouponType == 0) ? string.Empty : " and CouponType=@CouponType";
            StringBuilder strSqlBuilder = new StringBuilder(@"select [date],[total],[count] from [dbo].[View_MoneyMerchantAccountDetail34] where shopId=@shopId" + strWhere + " and [date]>=@startDate and [date]<@endDate order by [date] desc");

            List<SqlParameter> para = new List<SqlParameter>();
            para.Add(new SqlParameter("@shopId", shopId));
            para.Add(new SqlParameter("@startDate", startDate));
            para.Add(new SqlParameter("@endDate", endDate));
            //SqlParameter[] cmdParameters = new SqlParameter[]
            //{
            //    new SqlParameter("@shopId",shopId), new SqlParameter("@startDate",startDate),new SqlParameter("@endDate",endDate)
            //};

            List<MoneyMerchantAccountSumResponse> list = null;
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSqlBuilder.ToString(), para.ToArray()))
            {
                list = CommonManager.GetEntityList<MoneyMerchantAccountSumResponse>(dr);
            }

            return list;

        }

        /// <summary>
        /// 更新平账单据信息
        /// </summary>
        /// <param name="accountId">单号</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public bool UpdateMoneyMerchantAccountDetail(MoneyMerchantAccountDetail accountDetail, bool updateRemainMoney = false, double remainMoney = 0)
        {
            string strSql = @"update dbo.MoneyMerchantAccountDetail set remark=@remark,
                                            status=@status";
            if (accountDetail.confirmTime != null && accountDetail.confirmTime != Convert.ToDateTime("0001/1/1 0:00:00"))
            {
                strSql += " ,confirmTime=@confirmTime";
            }
            if (updateRemainMoney)//处理主管平账时的余额
            {
                strSql += ",remainMoney = @remainMoney";
            }
            strSql += " where accountId=@accountId";
            List<SqlParameter> paraList = new List<SqlParameter>();
            paraList.Add(new SqlParameter("@remark", SqlDbType.NVarChar, 500) { Value = accountDetail.remark });
            paraList.Add(new SqlParameter("@status", SqlDbType.Int) { Value = (int)accountDetail.status });
            if (accountDetail.confirmTime != null && accountDetail.confirmTime != Convert.ToDateTime("0001/1/1 0:00:00"))
            {
                paraList.Add(new SqlParameter("@confirmTime", SqlDbType.DateTime) { Value = accountDetail.confirmTime });
            }
            if (updateRemainMoney)
            {
                paraList.Add(new SqlParameter("@remainMoney", SqlDbType.Float) { Value = remainMoney });
            }
            paraList.Add(new SqlParameter("@accountId", SqlDbType.BigInt) { Value = accountDetail.accountId });

            SqlParameter[] para = paraList.ToArray();
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
        /// 根据单号查询平账信息
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        public MoneyMerchantAccountDetail QueryMoneyMerchantAccountDetail(long accountId)
        {
            const string strSql = @"select accountId,flowNumber,accountMoney,remainMoney,accountType,accountTypeConnId,inoutcomeType,
                                            operUser,operTime,companyId,shopId,remark,status,confirmTime
                                            from MoneyMerchantAccountDetail 
                                            where accountId=@accountId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@accountId", SqlDbType.BigInt) { Value = accountId }
            };
            MoneyMerchantAccountDetail detail = new MoneyMerchantAccountDetail();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (sdr.Read())
                {
                    detail = SqlHelper.GetEntity<MoneyMerchantAccountDetail>(sdr);
                }
            }
            return detail;
        }
    }
}
