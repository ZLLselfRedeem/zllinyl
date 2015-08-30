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
    public class Money19dianDetailManager
    {
        /// <summary>
        /// 增加一条数据
        /// 功能描述：检查用户账户详情使用情况 待处理 select @@IDENTITY
        /// 修改标识：罗国华
        /// </summary>

        public static long InsertMoney19dianDetail(Money19dianDetail model)
        {
            string strsql = @"insert into Money19dianDetail (customerId,changeReason,changeValue,remainMoney,changeTime,flowNumber,accountType
,accountTypeConnId,inoutcomeType,companyId,shopId)
                        values (@customerId,@changeReason,@changeValue,@remainMoney,@changeTime,@flowNumber,@accountType
,@accountTypeConnId,@inoutcomeType,@companyId,@shopId)
select @@IDENTITY";
            var parm = new[] {
new SqlParameter("@customerId", model.customerId),
new SqlParameter("@changeReason", model.changeReason),
new SqlParameter("@changeValue", model.changeValue),
new SqlParameter("@remainMoney", model.remainMoney),
new SqlParameter("@changeTime", model.changeTime),
new SqlParameter("@flowNumber", model.flowNumber),
new SqlParameter("@accountType", model.accountType),
new SqlParameter("@accountTypeConnId", model.accountTypeConnId),
new SqlParameter("@inoutcomeType", model.inoutcomeType),
new SqlParameter("@companyId", model.companyId),
new SqlParameter("@shopId", model.shopId)
                        };
            try
            {
                return Convert.ToInt64(SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm));
            }
            catch
            {
                return 0;
            }
        }
        /// <summary>
        /// 增加一条数据（涉及到老方法待处理，老方法需要替换）
        /// 新table添加了字段，更全面记录信息
        /// </summary>
        /// <param name="model">Money19dianDetail model</param>
        /// <returns></returns>
        public bool Insert(Money19dianDetail model)
        {
            string strsql = @"insert into Money19dianDetail (customerId,changeReason,changeValue,remainMoney,changeTime,flowNumber,accountType
,accountTypeConnId,inoutcomeType,companyId,shopId)
                        values (@customerId,@changeReason,@changeValue,@remainMoney,@changeTime,@flowNumber,@accountType
,@accountTypeConnId,@inoutcomeType,@companyId,@shopId)";
            SqlParameter[] parm = {
new SqlParameter("@customerId", SqlDbType.BigInt),
new SqlParameter("@changeReason", SqlDbType.NVarChar,50),
new SqlParameter("@changeValue", SqlDbType.Float),
new SqlParameter("@remainMoney", SqlDbType.Float),
new SqlParameter("@changeTime", SqlDbType.DateTime),
new SqlParameter("@flowNumber", SqlDbType.VarChar,50),
new SqlParameter("@accountType", SqlDbType.Int),
new SqlParameter("@accountTypeConnId", SqlDbType.VarChar,100),
new SqlParameter("@inoutcomeType", SqlDbType.Int),
new SqlParameter("@companyId", SqlDbType.Int),
new SqlParameter("@shopId", SqlDbType.Int)
                        };
            parm[0].Value = model.customerId;
            parm[1].Value = model.changeReason;
            parm[2].Value = model.changeValue;
            parm[3].Value = model.remainMoney;
            parm[4].Value = model.changeTime;
            parm[5].Value = model.flowNumber;
            parm[6].Value = model.accountType;
            parm[7].Value = model.accountTypeConnId;
            parm[8].Value = model.inoutcomeType;
            parm[9].Value = model.companyId;
            parm[10].Value = model.shopId;
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm) == 1;
        }



        /// <summary>
        /// 根据时间，查询19点金钱操作记录
        /// </summary>
        /// <param name="strChangeTime"></param>
        /// <param name="endChangeTime"></param>
        /// <returns></returns>
        public DataTable SelectMoney19dian(string strChangeTime, string endChangeTime)
        {
            DataTable result = new DataTable();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select [id],[customerId],[changeReason],[changeValue],[changeTime] from Money19dianDetail where changeTime between @strChangeTime and @endChangeTime");
                SqlParameter[] parameters = {
					new SqlParameter("@strChangeTime",strChangeTime),
					new SqlParameter("@endChangeTime",endChangeTime)};
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
                result = ds.Tables[0];
            }
            catch (System.Exception)
            {

            }
            return result;
        }


        #region 公司账户和公司账户明细
        /// <summary>
        /// 更新viewalloc公司账户余额
        /// </summary>
        /// <param name="moneyAccount">变更金额</param>
        /// <returns></returns>
        public bool UpdateViewallocInfo(double moneyAccount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ViewallocInfo set ");
            strSql.Append(" remainMoney = @moneyAccount+isnull(remainMoney,0)");
            SqlParameter[] parameters = {
			            new SqlParameter("@moneyAccount", SqlDbType.Float)
            };
            parameters[0].Value = moneyAccount;
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
        /// <summary>
        /// 增加公司账户明细的记录
        /// </summary>
        public static long InsertMoneyViewallocAccountDetail(MoneyViewallocAccountDetail model)
        {
            Object obj = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into MoneyViewallocAccountDetail(");
                strSql.Append("[accountMoney] ,[remainMoney] ,[accountType]  ,[accountTypeConnId] ,[inoutcomeType] ,[operUser] ,[operTime] ,[outcomeCompanyId] ,[outcomeShopId] ,[remark]");
                strSql.Append(") values (");
                strSql.Append("@accountMoney,@remainMoney,@accountType,@accountTypeConnId,@inoutcomeType,@operUser,@operTime,@outcomeCompanyId,@outcomeShopId,@remark");
                strSql.Append(") ");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
			            new SqlParameter("@accountMoney", SqlDbType.Float) ,            
                        new SqlParameter("@remainMoney", SqlDbType.Float) ,            
                        new SqlParameter("@accountType", SqlDbType.Int,4) ,   
                         new SqlParameter("@accountTypeConnId", SqlDbType.VarChar,100) ,            
                        new SqlParameter("@inoutcomeType", SqlDbType.Int,4) ,            
                        new SqlParameter("@operUser", SqlDbType.VarChar,100) ,       
                         new SqlParameter("@operTime", SqlDbType.DateTime) ,            
                        new SqlParameter("@outcomeCompanyId", SqlDbType.Int,4) ,            
                        new SqlParameter("@outcomeShopId", SqlDbType.Int,4) ,       
                        new SqlParameter("@remark", SqlDbType.NVarChar,100)};
                parameters[0].Value = model.accountMoney;
                parameters[1].Value = model.remainMoney;
                parameters[2].Value = model.accountType;
                parameters[3].Value = model.accountTypeConnId;
                parameters[4].Value = model.inoutcomeType;
                parameters[5].Value = model.operUser;
                parameters[6].Value = model.operTime;
                parameters[7].Value = model.outcomeCompanyId;
                parameters[8].Value = model.outcomeShopId;
                parameters[9].Value = model.remark;
                obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            }
            catch (System.Exception)
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
        /// <summary>
        /// 查询当前时间公司账户余额
        /// </summary>
        /// <returns></returns>
        public static double GetViewAllocRemainMoney()
        {
            double remainMoney = 0;
            string strsql = "select remainMoney from ViewallocInfo";
            SqlParameter[] parm = { };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                if (dr.Read())
                {
                    remainMoney = !dr.IsDBNull(0) ? dr.GetDouble(0) : 0;
                }
            }
            return remainMoney;
        }
        /// <summary>
        /// 查询当前时间用户余额
        /// </summary>
        /// <param name="customerId">用户ID</param>
        /// <returns></returns>
        public static double GetCustomerRemainMoney(long customerId)
        {
            double val = 0;
            string strsql = @"select money19dianRemained from CustomerInfo where CustomerID=@CustomerID";
            SqlParameter[] parm = {
                   new SqlParameter("@CustomerID", SqlDbType.BigInt,8)
                    };
            parm[0].Value = customerId;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                if (dr.Read())
                {
                    val = !dr.IsDBNull(0) ? dr.GetDouble(0) : 0;
                }
            }
            return val;
        }
        /// <summary>
        /// 收银宝查询当前点单的退款信息
        /// </summary>
        /// <param name="preOrder19dianId">点单流水号</param>
        /// <returns></returns>
        public static List<MoneyRefundDetail> GetMoney19dianDetailInfo(long preOrder19dianId, Guid orderID)
        {
            List<MoneyRefundDetail> list = new List<MoneyRefundDetail>();
            //            string strsql = @"select[refundMoney],[preOrder19dianId],[operTime],MoneyRefundDetail.remark ,EmployeeInfo.EmployeeFirstName
            //                              from MoneyRefundDetail inner join EmployeeInfo 
            //                              on MoneyRefundDetail.operUser=EmployeeInfo.EmployeeID where preOrder19dianId=" + preOrder19dianId + "";

            string strsql = @"SELECT   Sum(a.refundmoney) AS [refundMoney]," + preOrder19dianId + " as [preOrder19dianId],";
            strsql += "  a.opertime AS operTime,a.remark,Isnull(b.employeefirstname,'') AS EmployeeFirstName FROM     moneyrefunddetail a ";
            strsql += "   INNER JOIN employeeinfo b ";
            strsql += "  ON a.operuser = b.employeeid ";
            strsql += "  WHERE    a.orderid = '" + orderID + "' ";
            strsql += " GROUP BY a.opertime,a.remark,b.employeefirstname ORDER BY a.opertime DESC ";

            //SqlParameter[] parameters = { new SqlParameter("@orderID", orderId) };

            SqlDataReader dr = null;
            try
            {
                dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, null);//新数据处理，老数据operUser字段存储的不是员工编号，所有有下面异常处理
            }
            catch
            {
                strsql = @"select[refundMoney],[preOrder19dianId],[operTime],MoneyRefundDetail.remark,'' EmployeeFirstName
                              from MoneyRefundDetail 
                              where preOrder19dianId=" + preOrder19dianId + "";
                dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, null);
            }
            while (dr.Read())
            {
                MoneyRefundDetail moneyRefundDetail = new MoneyRefundDetail();
                moneyRefundDetail.refundMoney = Convert.ToDecimal(dr["refundMoney"] != DBNull.Value ? Convert.ToDouble(dr["refundMoney"]) : 0);
                moneyRefundDetail.preOrder19dianId = dr["preOrder19dianId"] != DBNull.Value ? Convert.ToInt64(dr["preOrder19dianId"]) : 0;
                moneyRefundDetail.operTime = dr["operTime"] != DBNull.Value ? Convert.ToDateTime(dr["operTime"]) : System.DateTime.Now;
                moneyRefundDetail.remark = dr["remark"] != DBNull.Value ? Convert.ToString(dr["remark"]) : "";
                moneyRefundDetail.operUser = dr["EmployeeFirstName"] != DBNull.Value ? Convert.ToString(dr["EmployeeFirstName"]) : "";
                list.Add(moneyRefundDetail);
            }
            dr.Close();
            return list;
        }

        public static List<MoneyRefundDetail> GetMoney19dianDetailInfo(long preOrder19dianId)
        {
            List<MoneyRefundDetail> list = new List<MoneyRefundDetail>();
            string strsql = @"select[refundMoney],[preOrder19dianId],[operTime],MoneyRefundDetail.remark ,EmployeeInfo.EmployeeFirstName
                                          from MoneyRefundDetail inner join EmployeeInfo 
                                          on MoneyRefundDetail.operUser=EmployeeInfo.EmployeeID where preOrder19dianId=" + preOrder19dianId + "";

            SqlDataReader dr = null;
            try
            {
                dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, null);//新数据处理，老数据operUser字段存储的不是员工编号，所有有下面异常处理
            }
            catch
            {
                strsql = @"select[refundMoney],[preOrder19dianId],[operTime],MoneyRefundDetail.remark,'' EmployeeFirstName
                              from MoneyRefundDetail 
                              where preOrder19dianId=" + preOrder19dianId + "";
                dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, null);
            }
            while (dr.Read())
            {
                MoneyRefundDetail moneyRefundDetail = new MoneyRefundDetail();
                moneyRefundDetail.refundMoney = Convert.ToDecimal(dr["refundMoney"] != DBNull.Value ? Convert.ToDouble(dr["refundMoney"]) : 0);
                moneyRefundDetail.preOrder19dianId = dr["preOrder19dianId"] != DBNull.Value ? Convert.ToInt64(dr["preOrder19dianId"]) : 0;
                moneyRefundDetail.operTime = dr["operTime"] != DBNull.Value ? Convert.ToDateTime(dr["operTime"]) : System.DateTime.Now;
                moneyRefundDetail.remark = dr["remark"] != DBNull.Value ? Convert.ToString(dr["remark"]) : "";
                moneyRefundDetail.operUser = dr["EmployeeFirstName"] != DBNull.Value ? Convert.ToString(dr["EmployeeFirstName"]) : "";
                list.Add(moneyRefundDetail);
            }
            dr.Close();
            return list;
        }
        /// <summary>
        /// 商户（收银宝）退款，将记录插入临时记录表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool InsertMoneyRefundDetail(MoneyRefundDetail model)
        {
            string strsql = @"insert into MoneyRefundDetail (preOrder19dianId,refundMoney,remark,operUser,operTime,orderID)
                        values (@preOrder19dianId,@refundMoney,@remark,@operUser,@operTime,@orderID)";
            SqlParameter[] parm = {
new SqlParameter("@preOrder19dianId", SqlDbType.BigInt),
new SqlParameter("@refundMoney", SqlDbType.Float),
new SqlParameter("@remark", SqlDbType.NVarChar,500),
new SqlParameter("@operUser", SqlDbType.VarChar,100),
new SqlParameter("@operTime", SqlDbType.DateTime),
new SqlParameter("@orderID", SqlDbType.UniqueIdentifier),
                        };
            parm[0].Value = model.preOrder19dianId;
            parm[1].Value = model.refundMoney;
            parm[2].Value = model.remark;
            parm[3].Value = model.operUser;
            parm[4].Value = model.operTime;
            parm[5].Value = model.orderID;
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm) == 1;
        }
        #endregion

        public List<RefundDetail> SelectRefundDetailByOrderId(long preOrder19dianID)
        {
            const string strSql = @"select 
isnull(B.UserName,'') as operPhone
      ,isnull(B.EmployeeFirstName,'') as operName
      ,A.refundMoney as operAmount
      ,A.operTime as operTime
from MoneyRefundDetail A 
inner join EmployeeInfo B on A.operUser=B.EmployeeID where A.remark!='优先服务原路退款'
and A.preOrder19dianId=@preOrder19dianId order by A.operTime desc ";

            SqlParameter[] parameters = { new SqlParameter("@preOrder19dianId", preOrder19dianID) };
            var list = new List<RefundDetail>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameters))
            {
                while (dr.Read())
                {
                    list.Add(new RefundDetail()
                    {
                        amount = Math.Round(Convert.ToDouble(dr["operAmount"]), 2),
                        name = string.IsNullOrWhiteSpace(Convert.ToString(dr["operName"])) ? Convert.ToString(dr["operPhone"]) : Convert.ToString(dr["operName"]),
                        time = ToSecondFrom1970(Convert.ToDateTime(dr["operTime"]))
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 根据OrderID获取项目退款日志 add by zhujinlei
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<RefundDetail> SelectRefundDetailByOrderIdNew(Guid orderId)
        {
            const string strSql = @"select 
                                    isnull(B.UserName,'') as operPhone
                                          ,isnull(B.EmployeeFirstName,'') as operName
                                          ,sum(A.refundMoney) as operAmount
                                          ,A.operTime as operTime
                                    from MoneyRefundDetail A 
                                    inner join EmployeeInfo B on A.operUser=B.EmployeeID where A.remark!='优先服务原路退款'
                                    and A.orderID=@orderID
                                    group by B.UserName,B.EmployeeFirstName,A.operTime
                                    order by A.operTime desc  ";

            SqlParameter[] parameters = { new SqlParameter("@orderID", orderId) };
            var list = new List<RefundDetail>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameters))
            {
                while (dr.Read())
                {
                    list.Add(new RefundDetail()
                    {
                        amount = Math.Round(Convert.ToDouble(dr["operAmount"]), 2),
                        name = string.IsNullOrWhiteSpace(Convert.ToString(dr["operName"])) ? Convert.ToString(dr["operPhone"]) : Convert.ToString(dr["operName"]),
                        time = ToSecondFrom1970(Convert.ToDateTime(dr["operTime"]))
                    });
                }
            }
            return list;
        }
        private double ToSecondFrom1970(DateTime inputDateTime)
        {
            DateTime beginTime = new DateTime(1970, 1, 1).ToLocalTime();//new DateTime取出来的时间是UTC的，转换为当前时区
            long elapsedTicks = inputDateTime.Ticks - beginTime.Ticks;
            TimeSpan elapsedSpan = new TimeSpan(elapsedTicks);
            return Math.Round(elapsedSpan.TotalSeconds, 0);
        }
    }
}
