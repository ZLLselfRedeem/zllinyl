using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 员工积分操作数据访问层
    /// created by wangcheng 
    /// 20140221
    /// </summary>
    public class EmployeePointManager
    {
        /// <summary>
        /// 更新用户当前已结算积分（当前可用积分）
        /// </summary>
        /// <param name="employeeId">员工编号</param>
        /// <param name="point">积分</param>
        /// <returns></returns>
        public bool UpdateEmployeeSettlementPoint(int employeeId, double point)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update EmployeeInfo set ");
                    strSql.Append(" settlementPoint=isnull(settlementPoint,0)+ @settlementPoint");
                    strSql.Append(" where EmployeeID=@EmployeeID ");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@settlementPoint", SqlDbType.Float),
                    new SqlParameter("@EmployeeID", SqlDbType.Int,4)};
                    parameters[0].Value = point;
                    parameters[1].Value = employeeId;
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
        /// 更新用户当前未结算积分（当前不可用积分）
        /// </summary>
        /// <param name="employeeId">员工编号</param>
        /// <param name="point">积分</param>
        /// <returns></returns>
        public bool UpdateEmployeeNotSettlementPoint(int employeeId, double point)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update EmployeeInfo set ");
                    strSql.Append(" notSettlementPoint=isnull(notSettlementPoint,0)+@notSettlementPoint");
                    strSql.Append(" where EmployeeID=@EmployeeID ");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@notSettlementPoint", SqlDbType.Float),
                    new SqlParameter("@EmployeeID", SqlDbType.Int ,4)};
                    parameters[0].Value = point;
                    parameters[1].Value = employeeId;
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
        /// 查询服务员信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectWaiter(string phoneNum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  [EmployeeID],[UserName] EmployeePhone,[EmployeeFirstName] EmployeeName");
            strSql.Append("  FROM EmployeeInfo");
            strSql.AppendFormat("  where EmployeeStatus>0 and (isViewAllocWorker is null or isViewAllocWorker =0)");
            if (!String.IsNullOrEmpty(phoneNum))
            {
                strSql.Append(" and EmployeePhone like '%" + phoneNum + "%'");
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询服务员在门店服务信息
        /// </summary>
        /// <param name="starTime"></param>
        /// <param name="endTime"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public DataTable SelectWaiterWorkExperience(string starTime, string endTime, int employeeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT EmployeeConnShop.[employeeID],ShopInfo.shopName,[status],[serviceStartTime],[serviceEndTime]");
            strSql.Append("  FROM [EmployeeConnShop] left join ShopInfo on ShopInfo.shopID=EmployeeConnShop.shopID");
            strSql.AppendFormat("  where serviceStartTime between '{0}' and '{1}' and EmployeeConnShop.[employeeID]={2}", starTime, endTime, employeeId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询服务员积分排名
        /// </summary>
        /// <param name="starTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="cityId">城市编号</param>
        /// <param name="orderRule">排序规则</param>
        /// <param name="phoneNum">手机号码</param>
        /// <param name="amount">金额</param>
        /// <returns></returns>
        public DataTable SelectWaiterPointRanking(string starTime, string endTime, int cityId, int orderRule, string phoneNum, double amount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct EmployeeInfo.EmployeeFirstName,EmployeeInfo.UserName,ROUND( EmployeeInfo.settlementPoint,2) settlementPoint,");
            strSql.Append(" ROUND( SUM(PreOrder19dian.prePaidSum-ISNULL(PreOrder19dian.refundMoneySum,0)),2) verifyAmount,EmployeeInfo.EmployeeID");
            strSql.Append(" ,count(PreOrder19dian.preOrder19dianId) verifyCount");
            strSql.Append(" from EmployeeInfo");
            strSql.Append(" left join PreorderShopConfirmedInfo on PreorderShopConfirmedInfo.employeeId=EmployeeInfo.EmployeeID");
            strSql.Append(" left join PreOrder19dian on PreOrder19dian.preOrder19dianId=PreorderShopConfirmedInfo.preOrder19dianId");
            strSql.Append(" left join ShopInfo on ShopInfo.shopID=PreOrder19dian.shopId");
            strSql.Append(" where PreOrder19dian.status<>105 and EmployeeInfo.EmployeeStatus>0 and PreOrder19dian.isApproved=1 and PreOrder19dian.isPaid=1");
            strSql.Append(" and (EmployeeInfo.isViewAllocWorker =0 or  EmployeeInfo.isViewAllocWorker is null)");
            strSql.AppendFormat(" and PreorderShopConfirmedInfo.shopConfirmedTime between '{0}' and '{1}'", starTime, endTime);
            if (cityId > 0)
            {
                strSql.AppendFormat(" and ShopInfo.cityID={0}", cityId);
            }
            if (!String.IsNullOrEmpty(phoneNum))
            {
                strSql.Append(" and EmployeeInfo.UserName like '%" + phoneNum + "%'");
            }
            if (amount != 0)
            {
                strSql.Append(" and PreOrder19dian.prePaidSum>" + amount + "");
            }
            strSql.Append(" group by EmployeeInfo.EmployeeFirstName,EmployeeInfo.UserName,EmployeeInfo.settlementPoint,EmployeeInfo.EmployeeID");
            switch (orderRule)
            {
                case 1://金额高到底
                    strSql.Append(" order by verifyAmount desc");
                    break;
                case 2://金额低到高
                    strSql.Append(" order by verifyAmount asc");
                    break;
                case 3://总积分高到低
                    strSql.Append(" order by settlementPoint desc");
                    break;
                case 4://总积分低到高
                default:
                    strSql.Append(" order by settlementPoint asc");
                    break;
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询服务员可用积分是否正常,true:正常；false:异常
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public bool EmployeePointIsValid(int employeeId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select EmployeeID");
            strSql.Append(" from EmployeeInfo");
            strSql.Append(" where settlementPoint < 0");
            strSql.Append(" and EmployeeStatus = 1");
            strSql.Append(" and EmployeeID = @EmployeeID");

            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@EmployeeID", SqlDbType.Int)
            };
            para[0].Value = employeeId;

            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                if (sdr.Read())
                {
                    return false;//有数据说明积分异常
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
