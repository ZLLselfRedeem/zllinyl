using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using System.Data.SqlClient;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 用户投诉相关
    /// add by wangc 
    /// 20140320
    /// </summary>
    public class ComplaintManager
    {
        /// <summary>
        /// 客户端查询投诉点单和服务员相关信息
        /// </summary>
        /// <param name="preOrder19dianId"></param>
        /// <returns></returns>
        public DataTable SelectComplaintDish(long preOrder19dianId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select top 1  PreOrder19dian.orderInJson,EmployeeInfo.UserName as waiterPhone,EmployeeInfo.EmployeeFirstName as waiterName");
            strSql.Append(" from PreOrder19dian");
            strSql.Append(" inner join CustomerInfo on PreOrder19dian.customerId=CustomerInfo.CustomerID");
            strSql.Append(" left join PreorderShopConfirmedInfo on PreorderShopConfirmedInfo.preOrder19dianId=PreOrder19dian.preOrder19dianId");
            strSql.Append(" left join EmployeeInfo on EmployeeInfo.EmployeeID=PreorderShopConfirmedInfo.employeeId");
            strSql.AppendFormat(" where PreOrder19dian.preOrder19dianId={0}", preOrder19dianId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询用户投诉信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCustomerComplaint()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select distinct CustomerComplaint.complaintTime,CustomerComplaint.complaintMsg,");
            strSql.Append(" CustomerComplaint.preOrder19dianId,EmployeeInfo.EmployeeFirstName,EmployeeInfo.UserName EmployeePhone,");
            strSql.Append(" CustomerInfo.UserName,CustomerInfo.mobilePhoneNumber");
            strSql.Append(" from CustomerComplaint");
            strSql.Append(" inner join PreOrder19dian on CustomerComplaint.preOrder19dianId=PreOrder19dian.preOrder19dianId");
            strSql.Append(" inner join CustomerInfo on CustomerInfo.CustomerID=PreOrder19dian.customerId");
            strSql.Append(" inner join PreorderShopConfirmedInfo on PreorderShopConfirmedInfo.preOrder19dianId=PreOrder19dian.preOrder19dianId");
            strSql.Append(" inner join EmployeeInfo on EmployeeInfo.EmployeeID=PreorderShopConfirmedInfo.employeeId");
            strSql.Append(" where PreorderShopConfirmedInfo.status=1");
            strSql.Append(" order by CustomerComplaint.complaintTime desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 判断当前用户是否对当前点单评价
        /// </summary>
        /// <param name="preOrder19DianId"></param>
        /// <returns></returns>
        public bool IsComplaint(long preOrder19DianId)
        {
            const string strSql =
                @"SELECT TOP 1 [id] FROM [CustomerComplaint] where CustomerComplaint.preOrder19dianId=@preOrder19dianId";
            SqlParameter parameter = new SqlParameter("@preOrder19dianId", preOrder19DianId);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                if (dr.Read())
                {
                    return Convert.ToInt32(dr[0]) > 0;
                }
            }
            return false;
        }
        /// <summary>
        /// 新增投诉信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long InsertCustomerComplaint(CustomerComplaint model)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into CustomerComplaint(");
                    strSql.Append("preOrder19dianId,complaintMsg,complaintTime)");
                    strSql.Append(" values (");
                    strSql.Append("@preOrder19dianId,@complaintMsg,@complaintTime)");
                    strSql.Append(";select @@IDENTITY");
                    SqlParameter[] parameters = {
					new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8),
					new SqlParameter("@complaintMsg", SqlDbType.NVarChar,-1),
					new SqlParameter("@complaintTime", SqlDbType.DateTime)};
                    parameters[0].Value = model.preOrder19dianId;
                    parameters[1].Value = model.complaintMsg;
                    parameters[2].Value = model.complaintTime;
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
    }
}
