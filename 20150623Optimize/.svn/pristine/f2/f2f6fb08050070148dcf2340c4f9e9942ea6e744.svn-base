using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Data.SqlClient;
using System.Data;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// add by wangc 20140326
    /// 用户充值日志
    /// </summary>
    public class RechargeLogManager
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public long Add(RechargeLog model)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                const string strSql = @"insert into RechargeLog(employeeId,operateTime,remark,customerPhone,amount,cookie)
 values (@employeeId,@operateTime,@remark,@customerPhone,@amount,@cookie);select @@IDENTITY";
                SqlParameter[] parameters = {
					new SqlParameter("@employeeId", SqlDbType.Int,4),
					new SqlParameter("@operateTime", SqlDbType.DateTime),
					new SqlParameter("@remark", SqlDbType.NVarChar,300),
					new SqlParameter("@customerPhone", SqlDbType.NVarChar,-1),
                    new SqlParameter("@amount",SqlDbType.Float),
                    new SqlParameter("@cookie",SqlDbType.NVarChar,300) };
                parameters[0].Value = model.employeeId;
                parameters[1].Value = model.operateTime;
                parameters[2].Value = model.remark;
                parameters[3].Value = model.customerPhone;
                parameters[4].Value = model.amount;
                parameters[5].Value = model.cookie == null ? "" : model.cookie;
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
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
        public bool BatchInsertRechargeLog(DataTable dt)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    SqlTransaction sqlbulkTransaction = conn.BeginTransaction();
                    SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.UseInternalTransaction, null);
                    sqlbulkcopy.DestinationTableName = "RechargeLog";//数据库中的表名
                    sqlbulkcopy.BulkCopyTimeout = 30;
                    sqlbulkcopy.ColumnMappings.Add("employeeId", "employeeId");
                    sqlbulkcopy.ColumnMappings.Add("operateTime", "operateTime");
                    sqlbulkcopy.ColumnMappings.Add("remark", "remark");
                    sqlbulkcopy.ColumnMappings.Add("customerPhone", "customerPhone");
                    sqlbulkcopy.ColumnMappings.Add("amount", "amount");
                    sqlbulkcopy.ColumnMappings.Add("cookie", "cookie");
                    sqlbulkcopy.ColumnMappings.Add("status", "status");
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
        /// <summary>
        /// 查询打款信息
        /// </summary>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public DataTable SelectRechargeLog(string strTime, string endTime, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select id,RechargeLog.employeeId,amount,operateTime,RechargeLog.remark,customerPhone,EmployeeInfo.EmployeeFirstName,
      case when [status]=1 then '审批申请'
      when  [status]=2 then '审批通过'
      when [status]=-1 then '审批不通过'
      else '历史数据' end as status,
[approvalTime],(select EmployeeFirstName from EmployeeInfo where approvalEmployeeId=EmployeeID) as approvalEmployee");
            strSql.Append(" from RechargeLog ");
            strSql.Append(" inner join EmployeeInfo on RechargeLog.employeeId=EmployeeInfo.EmployeeID");
            if (!string.IsNullOrEmpty(strTime) && !string.IsNullOrEmpty(endTime))
            {
                strSql.AppendFormat(" where operateTime between '{0}' and '{1}'", strTime, endTime);
            }
            switch (status)
            {
                case 1:
                case 2:
                case -1:
                    strSql.AppendFormat(" and status={0}", status);
                    break;
                case 0:
                default:
                    break;
            }
            strSql.Append(" order by operateTime desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询申请充值记录手机号码
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<RechargeLogPartMolde> SelectRechargeCustomerPhone(string ids)
        {
            List<RechargeLogPartMolde> list = new List<RechargeLogPartMolde>();
            string strSql = String.Format(@"select customerPhone,amount from RechargeLog where id in ({0})", ids);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()))
            {
                while (dr.Read())
                {
                    list.Add(new RechargeLogPartMolde()
                    {
                        amount = dr["amount"] == DBNull.Value ? 0 : Convert.ToDouble(dr["amount"]),
                        customerPhone = dr["customerPhone"] == DBNull.Value ? "" : "\'" + Convert.ToString(dr["customerPhone"]) + "\'"
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 更新充值申请审批状态
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public bool UpdateRechargeLogStatus(string ids, int status, int employeeId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                string strSql = String.Format(@"update RechargeLog set status={0},approvalEmployeeId={1},approvalTime='{2}' where id in ({3}) ", status, employeeId, DateTime.Now, ids);
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString()) > 0;
            }
        }
    }
}
