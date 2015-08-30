using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 积分商城：兑换记录操作日志 数据处理层
    /// 2014-2-26 jinyanni
    /// </summary>
    public class PointManageLogManager
    {
        /// <summary>
        /// 积分商城：新增一条操作积分兑换表的记录
        /// </summary>
        /// <param name="Log"></param>
        /// <returns></returns>
        public int InsertPointManageLog(PointManageLog Log)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into PointManageLog(");
            strSql.Append("pointLogId,remark,createTime,createdBy,status)");
            strSql.Append(" values(@pointLogId,@remark,@createTime,@createdBy,@status)");
            strSql.Append(" select @@identity");
            
            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@pointLogId",SqlDbType.BigInt),
                new SqlParameter("@remark",SqlDbType.NVarChar,200),
                new SqlParameter("@createTime",SqlDbType.DateTime),
                new SqlParameter("@createdBy",SqlDbType.Int),
                new SqlParameter("@status",SqlDbType.Int)
            };
            para[0].Value = Log.pointLogId;
            para[1].Value = Log.remark;
            para[2].Value = Log.createTime;
            para[3].Value = Log.createdBy;
            para[4].Value = Log.status;

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);

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
        /// 积分商城：根据积分变动ID查询其对应的操作日志
        /// </summary>
        /// <param name="pointLogId"></param>
        /// <returns></returns>
        public DataTable QueryPointManageLog(int pointLogId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select P.id,P.createdBy,E.EmployeeFirstName,P.createTime,P.remark");
            strSql.Append(" from PointManageLog P inner join EmployeeInfo E");
            strSql.Append(" on P.createdBy = E.EmployeeID");
            strSql.Append(" and P.status = 1 and E.EmployeeStatus = 1");
            strSql.Append(" and P.pointLogId = @pointLogId");

            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@pointLogId",SqlDbType.BigInt)
            };
            para[0].Value = pointLogId;

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }
    }
}
