using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using System.Data;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 客服操作数据访问
    /// </summary>
    public class CustomerServiceOperateLogManager
    {
        /// <summary>
        /// 客服执行在CustomerServiceOperateLog中添加一条日志记录
        /// </summary>
        /// <returns></returns>
        public int InsertCustomerServiceOperateLogInfo(CustomerServiceOperateLogInfo customerServiceOperateLogInfo)
        {
            const string strSql = @"insert into CustomerServiceOperateLog(
employeeId,employeeName,employeeOperate,employeeOperateTime,employeeOperateType)
values (@employeeId,@employeeName,@employeeOperate,@employeeOperateTime,@employeeOperateType)
select @@identity";
            SqlParameter[] parameters = new SqlParameter[]{
					    new SqlParameter("@employeeId", SqlDbType.Int,4),
                        new SqlParameter("@employeeName", SqlDbType.VarChar,50),
                        new SqlParameter("@employeeOperate", SqlDbType.VarChar,200),
                        new SqlParameter("@employeeOperateTime",SqlDbType.DateTime),
                         new SqlParameter("@employeeOperateType",SqlDbType.Int,4)
                    };
            parameters[0].Value = customerServiceOperateLogInfo.employeeId;
            parameters[1].Value = customerServiceOperateLogInfo.employeeName;
            parameters[2].Value = customerServiceOperateLogInfo.employeeOperate;
            parameters[3].Value = customerServiceOperateLogInfo.employeeOperateTime;
            parameters[4].Value = customerServiceOperateLogInfo.employeeOperateType;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
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
        /// 查询客服操作日志信息
        /// </summary>
        /// <param name="preOrderId"></param>
        /// <param name="operateName"></param>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable SelectCustomerServiceOperateLog(int preOrderId, string operateName, string strTime, string endTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select  [id],[employeeName],[employeeOperate] ,[employeeOperateTime] ,[employeeOperateType] FROM CustomerServiceOperateLog");
            strSql.Append(" where 1=1");
            if (preOrderId != 0)
            {
                strSql.AppendFormat(" and employeeOperate like '%{0}%'", preOrderId);
            }
            if (operateName != "")
            {
                strSql.AppendFormat(" and employeeName like '%{0}%'", operateName);
            }
            if (strTime != "" && endTime != "")
            {
                strSql.AppendFormat(" and employeeOperateTime between '{0}' and '{1}'", strTime, endTime);
            }
            strSql.Append(" order by employeeOperateTime desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        public DataTable SelectCustomerServiceOperateLog(int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select  [id],[employeeName],[employeeOperate] ,[employeeOperateTime] ,[employeeOperateType] FROM CustomerServiceOperateLog");
            if (type != 0)
            {
                strSql.AppendFormat(" where employeeOperateType='{0}'", type);
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询所有原路返回申请信息（wangcheng）
        /// </summary>
        /// <param name="connId"></param>
        /// <param name="customerMobilephone"></param>
        /// <param name="strTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataTable SelectOriginalRoadRefundApply(long connId, string customerMobilephone, string strTime, string endTime, int status, int type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT id,type,refund.connId,ROUND(refundAmount,2) refundAmount,applicationTime,remittanceTime,status,remitEmployee remitEmployee,
                            customerMobilephone,note,customerUserName,EmployeeInfo.UserName employeeId,ali.aliBuyerEmail,RefundPayType
                            FROM OriginalRoadRefundInfo refund
                            left join EmployeeInfo on EmployeeInfo.EmployeeID=refund.employeeId
                            left join AlipayOrderInfo ali on refund.connId = ali.connId");
            strSql.AppendFormat(" and ali.orderStatus = '{0}'", (int)VAAlipayOrderStatus.PAID);
            strSql.Append(" where 1=1");
            if (connId > 0)
            {
                strSql.AppendFormat(" and refund.connId='{0}'", connId);
            }
            else
            {
                if (customerMobilephone != "")
                {
                    strSql.AppendFormat(" and customerMobilephone='{0}'", customerMobilephone);
                }
                else
                {
                    if (strTime != "" && endTime != "")
                    {
                        strSql.AppendFormat(" and applicationTime between '{0}' and '{1}' ", strTime, endTime);
                    }
                    else
                    {
                        //不做任何处理    
                    }
                }
            }
            switch (status)
            {
                case 1://未打款
                    strSql.Append(" and refund.status=1");
                    break;
                case 2://已打款
                    strSql.Append(" and refund.status=2");
                    break;
                case 4://打款中
                    strSql.Append(" and refund.status=3");
                    break;
                case 5:
                    strSql.Append(" and (refund.status=4 or refund.status=1)");
                    break;
                case 3://全部
                default:
                    break;
            }
            switch (type)
            {
                case (int)VAOriginalRefundType.PREORDER:
                    strSql.AppendFormat(" and type={0}", (int)VAOriginalRefundType.PREORDER);
                    break;
                case (int)VAOriginalRefundType.REPEAT_PREORDER:
                    strSql.AppendFormat(" and type={0}", (int)VAOriginalRefundType.REPEAT_PREORDER);
                    break;
                default:
                    break;
            }
            strSql.Append(" order by applicationTime desc");//返回数据按照请求原路退款时间降序排列
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 更新原路退款日志表信息（wangcheng）
        /// </summary>
        /// <param name="remitEmployee"></param>
        /// <param name="note"></param>
        /// <returns></returns>
        public bool UpdateOriginalRoadRefundApply(int remitEmployee, string note, long id, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update OriginalRoadRefundInfo set ");
            strSql.Append(" remittanceTime = @remittanceTime,");
            strSql.Append(" remitEmployee = @remitEmployee,");
            strSql.Append(" note = @note,");
            strSql.Append(" status = @status");
            strSql.Append(" where id = @id");
            SqlParameter[] parameters = {
			            new SqlParameter("@remittanceTime", SqlDbType.DateTime),
                        new SqlParameter("@remitEmployee", SqlDbType.Int,4),
                        new SqlParameter("@note", SqlDbType.NVarChar,500),
                        new SqlParameter("@status", SqlDbType.Int,4),
                        new SqlParameter("@id", SqlDbType.BigInt,8)
            };
            parameters[0].Value = DateTime.Now;
            parameters[1].Value = remitEmployee;
            parameters[2].Value = note;
            parameters[3].Value = status;//标记为当前记录已被处理
            parameters[4].Value = id;//标记为当前记录已被处理
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
    }
}

