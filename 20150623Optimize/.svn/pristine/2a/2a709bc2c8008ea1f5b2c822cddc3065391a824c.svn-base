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
    /// 客户端充值活动管理：数据处理层
    /// 创建日期：2014-5-4
    /// </summary>
    public class ClientRechargeManager
    {
        /// <summary>
        /// 新增一个活动
        /// </summary>
        /// <param name="recharge"></param>
        /// <returns></returns>
        public int Insert(ClientRechargeInfo recharge)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ClientRecharge(");
            strSql.Append("name,rechargeCondition,present,beginTime,endTime,externalSold,actualSold,status,createTime,sequence)");
            strSql.Append(" values (@name,@rechargeCondition,@present,@beginTime,@endTime,@externalSold,@actualSold,@status,@createTime,@sequence)");
            strSql.Append(" select @@identity");

            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@name",SqlDbType.NVarChar,100),
                new SqlParameter("@rechargeCondition",SqlDbType.Float),
                new SqlParameter("@present",SqlDbType.Float),
                new SqlParameter("@beginTime",SqlDbType.NVarChar,50),
                new SqlParameter("@endTime",SqlDbType.NVarChar,50),
                new SqlParameter("@externalSold",SqlDbType.Int),
                new SqlParameter("@actualSold",SqlDbType.Int),
                new SqlParameter("@status",SqlDbType.Int),
                new SqlParameter("@createTime",SqlDbType.DateTime),
                new SqlParameter("@sequence",SqlDbType.Int)
                };
                para[0].Value = recharge.name;
                para[1].Value = recharge.rechargeCondition;
                para[2].Value = recharge.present;
                para[3].Value = recharge.beginTime;
                para[4].Value = recharge.endTime;
                para[5].Value = recharge.externalSold;
                para[6].Value = recharge.actualSold;
                para[7].Value = -1;
                para[8].Value = DateTime.Now;
                para[9].Value = recharge.sequence;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    object result = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
                    if (result == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// 开启或关闭充值活动
        /// </summary>
        /// <param name="id">活动编号</param>
        /// <param name="status">状态(1 / -1)</param>
        /// <returns></returns>
        public bool ClientRechargeOnOff(int id, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ClientRecharge set");
            strSql.Append(" status=@status");
            strSql.Append(" where id=@id");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@status",SqlDbType.Int),
                new SqlParameter("@id",SqlDbType.Int)
                };
                para[0].Value = status;
                para[1].Value = id;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                    if (i == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 更新充值活动
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="externalSold">对外已售份数</param>
        /// <returns></returns>
        public bool UpdateRecharge(ClientRechargeInfo recharge)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ClientRecharge set");
            strSql.Append(" beginTime=@beginTime,");
            strSql.Append(" endTime=@endTime,");
            strSql.Append(" externalSold=@externalSold,");
            strSql.Append(" sequence=@sequence");
            strSql.Append(" where id=@id");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@beginTime",SqlDbType.NVarChar,50),
                new SqlParameter("@endTime",SqlDbType.NVarChar,50),
                new SqlParameter("@externalSold",SqlDbType.Int),
                new SqlParameter("@sequence",SqlDbType.Int),
                new SqlParameter("@id",SqlDbType.Int)
                };
                para[0].Value = recharge.beginTime;
                para[1].Value = recharge.endTime;
                para[2].Value = recharge.externalSold;
                para[3].Value = recharge.sequence;
                para[4].Value = recharge.id;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                    if (i == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 用户充值后，更改已售份数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateRechargeSoldCount(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ClientRecharge set");
            strSql.Append(" externalSold=isnull(externalSold,0)+1,");
            strSql.Append(" actualSold=isnull(actualSold,0)+1");
            strSql.AppendFormat(" where id={0} and status=1", id);
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString());
                    if (i == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 客户端查询所有开启的活动
        /// </summary>
        /// <returns></returns>
        public DataTable ClientQueryRecharge()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,rechargeCondition,present,beginTime,endTime,externalSold,actualSold,status,createTime,sequence");
            strSql.Append(" from ClientRecharge");
            strSql.Append(" where status=1");
            strSql.Append(" order by sequence desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询所有活动或指定活动
        /// </summary>
        /// <param name="name">充值活动名称</param>
        /// <returns></returns>
        public DataTable QueryRecharge(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,rechargeCondition,present,beginTime,endTime,externalSold,actualSold,status,createTime,sequence");
            strSql.Append(" from ClientRecharge");
            if (!string.IsNullOrEmpty(name))
            {
                strSql.AppendFormat(" where name like '%{0}%'", name);
            }
            strSql.Append(" order by sequence desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据ID查询活动详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable QueryRecharge(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,rechargeCondition,present,beginTime,endTime,externalSold,actualSold,status,createTime,sequence");
            strSql.Append(" from ClientRecharge");
            strSql.AppendFormat(" where id={0}", id);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 新增客户充值记录
        /// </summary>
        /// <param name="recharge"></param>
        /// <returns></returns>
        public long Insert(CustomerRechargeInfo recharge)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CustomerRecharge(");
            strSql.Append("customerId,customerCookie,customerUUID,rechargeId,shopId,preOrder19dianId,rechargeTime,payStatus,payMode,rechargeCondition,rechargePresent)");
            strSql.Append(" values(@customerId,@customerCookie,@customerUUID,@rechargeId,@shopId,@preOrder19dianId,@rechargeTime,@payStatus,@payMode,@rechargeCondition,@rechargePresent)");
            strSql.Append("select @@identity");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@customerId",SqlDbType.BigInt,8),
                new SqlParameter("@customerCookie",SqlDbType.NVarChar,100),
                new SqlParameter("@customerUUID",SqlDbType.NVarChar,100),
                new SqlParameter("@rechargeId",SqlDbType.Int),
                new SqlParameter("@shopId",SqlDbType.Int),
                new SqlParameter("@preOrder19dianId",SqlDbType.BigInt,8),
                new SqlParameter("@rechargeTime",SqlDbType.DateTime),
                new SqlParameter("@payStatus",SqlDbType.Int),
                new SqlParameter("@payMode",SqlDbType.Int),
                new SqlParameter("@rechargeCondition",SqlDbType.Float),
                new SqlParameter("@rechargePresent",SqlDbType.Float)
                };
                para[0].Value = recharge.customerId;
                para[1].Value = recharge.customerCookie;
                para[2].Value = recharge.customerUUID;
                para[3].Value = recharge.rechargeId;
                para[4].Value = recharge.shopId;
                para[5].Value = recharge.preOrder19dianId;
                para[6].Value = recharge.rechargeTime;
                para[7].Value = recharge.payStatus;
                para[8].Value = recharge.payMode;
                para[9].Value = recharge.rechargeCondition;
                para[10].Value = recharge.rechargePresent;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    object result = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
                    if (result == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// 充值成功后更新客户充值记录
        /// </summary>
        /// <param name="id">充值活动ID</param>
        /// <param name="payStauts">支付状态</param>
        /// <param name="payTime">支付时间</param>
        /// <param name="payMode">支付方式</param>
        /// <returns></returns>
        public bool UpdateCustomerRecharge(long id, int payStauts, DateTime payTime, int payMode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CustomerRecharge set");
            strSql.Append(" payStatus=@payStatus,");
            strSql.Append(" payTime=@payTime,");
            strSql.Append(" payMode=@payMode");
            strSql.Append(" where id=@id");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@payStatus",SqlDbType.Int),
                new SqlParameter("@payTime",SqlDbType.DateTime),
                new SqlParameter("@payMode",SqlDbType.Int),
                new SqlParameter("@id",SqlDbType.Int)
                };
                para[0].Value = payStauts;
                para[1].Value = payTime;
                para[2].Value = payMode;
                para[3].Value = id;
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                    if (i == 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 根据充值记录ID查询用户充值详情
        /// </summary>
        /// <param name="id">用户充值记录ID</param>
        /// <returns></returns>
        public DataTable QueryCustomerRecharge(long id)
        {
            const string strSql = "select customerId,customerCookie,customerUUID,rechargeId,shopId,preOrder19dianId,rechargeTime,payStatus,payTime,payMode from CustomerRecharge where id=@id";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@id", SqlDbType.BigInt) { Value = id }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }
        /// <summary>
        /// 充值活动统计数据概览
        /// </summary>
        /// <param name="beginTime">充值时间起</param>
        /// <param name="endTime">充值时间止</param>
        /// <param name="rechargeId">充值活动ID</param>
        /// <returns></returns>
        public DataTable QueryRechargeStatistics(DateTime beginTime, DateTime endTime, int rechargeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CONVERT(varchar(100), c.payTime, 111) payTime,COUNT(c.rechargeId) rechargeCount,SUM(r.rechargeCondition) rechargeAmount");
            strSql.Append(" from CustomerRecharge c inner join ClientRecharge r");
            strSql.Append(" on c.rechargeId=r.id");
            strSql.AppendFormat(" where c.payStatus='{0}' ", (int)VACustomerChargeOrderStatus.PAID);
            if (rechargeId > 0)
            {
                strSql.AppendFormat(" and c.rechargeId='{0}'", rechargeId);
            }
            strSql.AppendFormat(" and c.payTime between '{0}' and '{1}'", beginTime, endTime);
            strSql.Append(" group by r.id,CONVERT(varchar(100), c.payTime, 111)");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 用户充值数据统计
        /// </summary>
        /// <param name="beginTime">充值时间起</param>
        /// <param name="endTime">充值时间止</param>
        /// <param name="rechargeId">充值活动ID</param>
        /// <returns></returns>
        public DataTable QueryCustomerRechargeStatistics(DateTime beginTime, DateTime endTime, int rechargeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select c.CustomerID,c.mobilePhoneNumber,COUNT(cr.id) rechargeCount,SUM(r.rechargeCondition) rechargeAmount");
            strSql.Append(" from CustomerRecharge cr");
            strSql.Append(" inner join ClientRecharge r on cr.rechargeId = r.id");
            strSql.Append(" inner join CustomerInfo c on cr.customerId = c.CustomerID");
            strSql.Append(" and c.CustomerStatus = 1");
            strSql.AppendFormat(" where cr.payStatus='{0}' ", (int)VACustomerChargeOrderStatus.PAID);
            if (rechargeId > 0)
            {
                strSql.AppendFormat(" and cr.rechargeId='{0}'", rechargeId);
            }
            strSql.AppendFormat(" and cr.payTime between '{0}' and '{1}'", beginTime, endTime);
            strSql.Append(" group by c.CustomerID,c.mobilePhoneNumber");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询用户的充值及消费统计数据
        /// </summary>
        /// <returns></returns>
        public DataTable QueryCustomerConsumeAndRechargeStatistics()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select A.mobilePhoneNumber, A.RegisterDate,A.preOrderTotalAmount,A.preOrderTotalQuantity,");
            strSql.Append(" B.CustomerID,COUNT(B.id) rechargeCount,SUM(B.rechargeCondition) rechargeAmount");
            strSql.Append(" from CustomerRecharge B ");
            strSql.Append(" inner join CustomerInfo A on A.CustomerID=B.customerId");
            strSql.Append(" where B.payStatus=2");
            strSql.Append(" group by  A.mobilePhoneNumber, A.RegisterDate,A.preOrderTotalAmount,A.preOrderTotalQuantity,B.customerId");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询用户的充值及消费详情
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable QueryCustomerConsumeAndRechargeDetail(long customerId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select null preOrder19dianId,c.mobilePhoneNumber,s.shopName,cr.payTime,r.rechargeCondition paySum");
            strSql.Append(" from CustomerRecharge cr");
            strSql.Append(" inner join ClientRecharge r on cr.rechargeId = r.id");
            strSql.Append(" inner join CustomerInfo c on cr.customerId = c.CustomerID");
            //strSql.Append(" and c.CustomerStatus = 1 ");
            strSql.AppendFormat(" and cr.payStatus = '{0}' ", (int)VACustomerChargeOrderStatus.PAID);
            strSql.AppendFormat(" and c.CustomerID = '{0}'", customerId);
            strSql.Append(" left join ShopInfo s on cr.shopId = s.shopID");
            //strSql.Append(" and s.shopStatus = 1");
            strSql.Append(" union all");
            strSql.Append(" select p.preOrder19dianId,c.mobilePhoneNumber,s.shopName,p.prePayTime payTime,-p.prePaidSum paySum");
            strSql.Append(" from PreOrder19dian p");
            strSql.Append(" inner join CustomerInfo c on p.customerId = c.CustomerID");
            //strSql.Append(" and c.CustomerStatus = 1");
            strSql.AppendFormat(" and c.CustomerID = '{0}'", customerId);
            strSql.Append(" and p.isPaid = 1");
            strSql.AppendFormat(" and p.status <> '{0}'", (int)VAPreorderStatus.Deleted);
            strSql.Append(" inner join ShopInfo s on p.shopId = s.shopID");
            // strSql.Append(" and s.shopStatus = 1");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询用户的充值及消费详情
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="rechargeId"></param>
        /// <param name="str"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public DataTable QueryCustomerConsumeAndRechargeDetail(long customerId, int rechargeId, DateTime str, DateTime end)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct null preOrder19dianId,c.mobilePhoneNumber,s.shopName,cr.payTime,r.rechargeCondition paySum");
            strSql.Append(" from CustomerRecharge cr");
            strSql.Append(" inner join ClientRecharge r on cr.rechargeId = r.id");
            strSql.Append(" inner join CustomerInfo c on cr.customerId = c.CustomerID");
            strSql.Append(" left join ShopInfo s on cr.shopId = s.shopID");
            strSql.AppendFormat(" where r.id='{0}' and cr.payTime between '{1}' and  '{2}'", rechargeId, str, end);
            strSql.AppendFormat(" and cr.payStatus = '{0}' ", (int)VACustomerChargeOrderStatus.PAID);
            strSql.AppendFormat(" and c.CustomerID = '{0}'", customerId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据用户充值记录ID查询当笔赠送金额
        /// </summary>
        /// <param name="id">客户充值记录ID</param>
        /// <returns></returns>
        public double QueryPresentMoney(long id)
        {
            const string strSql = @"select r.present from CustomerRecharge cr inner join ClientRecharge r on cr.rechargeId = r.id and cr.id =@id";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@id",SqlDbType.BigInt) { Value = id }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                conn.Open();
                object present = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (present == null)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToDouble(present);
                }
            }
        }
    }
}
