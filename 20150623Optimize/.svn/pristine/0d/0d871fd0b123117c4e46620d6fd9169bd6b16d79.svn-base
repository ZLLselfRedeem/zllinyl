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
    /// 服务员积分日志数据访问层
    /// created by wangcheng 
    /// 20140222
    /// </summary>
    public class EmployeePointLogManager
    {
        /// <summary>
        /// 增加一条服务员积分
        /// </summary>
        public int Add(EmployeePointLog model)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into EmployeePointLog(");
                strSql.Append("operateTime,employeeId,preOrder19dianId,shopId,customerId,viewallocEmployeeId,pointVariation,pointVariationMethods,monetary,remark,status)");
                strSql.Append(" values (");
                strSql.Append("@operateTime,@employeeId,@preOrder19dianId,@shopId,@customerId,@viewallocEmployeeId,@pointVariation,@pointVariationMethods,@monetary,@remark,@status)");
                strSql.Append(";select @@IDENTITY");
                SqlParameter[] parameters = {
					new SqlParameter("@operateTime", SqlDbType.DateTime),
					new SqlParameter("@employeeId", SqlDbType.Int,4),
					new SqlParameter("@preOrder19dianId", SqlDbType.BigInt,8),
					new SqlParameter("@shopId", SqlDbType.Int,4),
					new SqlParameter("@customerId", SqlDbType.BigInt,8),
					new SqlParameter("@viewallocEmployeeId", SqlDbType.Int,4),
					new SqlParameter("@pointVariation", SqlDbType.Float,8),
					new SqlParameter("@pointVariationMethods", SqlDbType.Int,4),
					new SqlParameter("@monetary", SqlDbType.Float,8),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@status", SqlDbType.Int,4)};
                parameters[0].Value = model.operateTime;
                parameters[1].Value = model.employeeId;
                parameters[2].Value = model.preOrder19dianId;
                parameters[3].Value = model.shopId;
                parameters[4].Value = model.customerId;
                parameters[5].Value = model.viewallocEmployeeId;
                parameters[6].Value = model.pointVariation;
                parameters[7].Value = model.pointVariationMethods;
                parameters[8].Value = model.monetary;
                parameters[9].Value = model.remark;
                parameters[10].Value = model.status;
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
        /// 查询服务员兑换记录条数
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public int SelectEmployeeExchangeCount(int employeeID)
        {
            int val = 0;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select COUNT (id) count ");
            strSql.Append(" from [EmployeePointLog]");
            strSql.AppendFormat(" where employeeId={0} and status >0 and pointVariationMethods=" + (int)PointVariationMethods.GOODS_EXCHANGE + "", employeeID);
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null))
            {
                if (dr.Read())
                {
                    val = dr[0] == DBNull.Value ? 0 : Convert.ToInt32(dr[0]);
                }
            }
            return val;
        }
        /// <summary>
        /// 查询服务员在某个时间段内积分变动日志
        /// </summary>
        /// <param name="starTime"></param>
        /// <param name="endTime"></param>
        /// <param name="employeeId"></param>
        /// <param name="showAdd"></param>
        /// <param name="showReduce"></param>
        /// <returns></returns>
        public DataTable SelectWaiterPointLog(string starTime, string endTime, int employeeId, bool showAdd, bool showReduce)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [operateTime],[pointVariation],[monetary],EmployeePointLog.remark,CustomerInfo.UserName,CustomerInfo.mobilePhoneNumber,ShopInfo.shopName,");
            strSql.Append(" EmployeeInfo.EmployeeFirstName");
            strSql.Append(" FROM EmployeePointLog");
            strSql.Append(" left join CustomerInfo on CustomerInfo.CustomerID=EmployeePointLog.customerId");
            strSql.Append(" left join ShopInfo on ShopInfo.shopID=EmployeePointLog.shopId");
            strSql.Append(" left join EmployeeInfo on EmployeeInfo.EmployeeID=EmployeePointLog.viewallocEmployeeId");
            strSql.AppendFormat(" where EmployeePointLog.employeeId={0}", employeeId);
            strSql.AppendFormat(" and operateTime between '{0}' and '{1}'", starTime, endTime);
            if (showAdd == true && showReduce == false)//显示增加
            {
                strSql.Append(" and EmployeePointLog.pointVariation>=0");
            }
            if (showReduce == true && showAdd == false)//显示减少
            {
                strSql.Append(" and EmployeePointLog.pointVariation<0 ");
            }
            //其他情况，全选全不选都显示所有记录信息
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 兑换商品时，新增一条记录
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public int ExchangeAdd(EmployeePointLog point)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("insert into EmployeePointLog(");
            strSql.Append(" operateTime,employeeId,pointVariation,pointVariationMethods,");
            strSql.Append(" status,goodsId,exchangeStatus,confirmStatus,shipStatus,remark)");
            strSql.Append(" values(");
            strSql.Append(" @operateTime,@employeeId,@pointVariation,@pointVariationMethods,");
            strSql.Append(" @status,@goodsId,@exchangeStatus,@confirmStatus,@shipStatus,@remark)");
            strSql.Append(";select @@identity");

            SqlParameter[] paras = new SqlParameter[] { 
                new SqlParameter("@operateTime",SqlDbType.DateTime),
                new SqlParameter("@employeeId",SqlDbType.Int),
                new SqlParameter("@pointVariation",SqlDbType.Float),
                new SqlParameter("@pointVariationMethods",SqlDbType.Int),
                new SqlParameter("@status",SqlDbType.Int),
                new SqlParameter("@goodsId",SqlDbType.Int),
                new SqlParameter("@exchangeStatus",SqlDbType.Int),
                new SqlParameter("@confirmStatus",SqlDbType.Int),
                new SqlParameter("@shipStatus",SqlDbType.Int),
                new SqlParameter("@remark", SqlDbType.NVarChar, 500)
            };
            paras[0].Value = point.operateTime;
            paras[1].Value = point.employeeId;
            paras[2].Value = point.pointVariation;
            paras[3].Value = point.pointVariationMethods;
            paras[4].Value = point.status;
            paras[5].Value = point.goodsId;
            paras[6].Value = point.exchangeStatus;
            paras[7].Value = point.confirmStatus;
            paras[8].Value = point.shipStatus;
            paras[9].Value = point.remark;

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), paras);
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
        ///  更新兑换记录 
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public bool UpdateExchangeLog(EmployeePointLog point)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update EmployeePointLog");
            strSql.Append(" set address = @address,");
            strSql.Append(" confirmTime = @confirmTime,");
            strSql.Append(" confirmBy = @confirmBy,");
            strSql.Append(" exchangeRemark = @exchangeRemark,");
            strSql.Append(" shipStatus = @shipStatus,");
            strSql.Append(" shipBy = @shipBy,");
            strSql.Append(" platform = @platform,");
            strSql.Append(" serialNumber = @serialNumber");
            strSql.Append(" where id = @id");

            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@address",SqlDbType.NVarChar, 500),
                new SqlParameter("@confirmTime",SqlDbType.DateTime),
                new SqlParameter("@confirmBy",SqlDbType.Int),
                new SqlParameter("@exchangeRemark",SqlDbType.NVarChar, 500),
                new SqlParameter("@shipStatus",SqlDbType.Int),
                new SqlParameter("@shipBy",SqlDbType.Int),
                new SqlParameter("@platform",SqlDbType.NVarChar, 50),
                new SqlParameter("@serialNumber",SqlDbType.NVarChar, 50),
                new SqlParameter("@id",SqlDbType.Int),
            };
            para[0].Value = point.address;
            para[1].Value = point.confirmTime;
            para[2].Value = point.confirmBy;
            para[3].Value = point.exchangeRemark;
            para[4].Value = point.shipStatus;
            para[5].Value = point.shipBy;
            para[6].Value = point.platform;
            para[7].Value = point.serialNumber;
            para[8].Value = point.id;

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
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
        /// 积分商城：后台查询兑换记录
        /// </summary>
        /// <returns></returns>
        public DataTable QueryExchangeLog(string startTime, string endTime, string shipStatus, string phoneNumber, int id)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select * from (");
            strSql.Append("select P.id,G.name GoodsName,E.EmployeeID,E.EmployeeFirstName,E.UserName,P.address,");
            strSql.Append("(case P.confirmStatus when 1 then '已确认' when -1 then '未确认' end) confirmStatus,");
            strSql.Append(" (case P.shipStatus  when 1 then '已发货/已充值'  when -1 then '未发货/未充值' end) shipStatus,");
            strSql.Append(" con.EmployeeFirstName shipBy,P.exchangeRemark,P.exchangeStatus,P.operateTime,P.pointVariation,");
            strSql.Append(" P.confirmTime,sh.EmployeeFirstName confirmBy,P.platform,P.serialNumber");
            strSql.Append(" from EmployeePointLog P");
            strSql.Append(" inner join EmployeeInfo E on P.employeeId = E.EmployeeID and P.status = 1  and E.EmployeeStatus = 1");

            if (!string.IsNullOrEmpty(startTime))
            {
                strSql.AppendFormat(" and P.operateTime >= '{0}'", startTime);
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                strSql.AppendFormat(" and P.operateTime <= '{0}'", endTime);
            }
            if (!string.IsNullOrEmpty(shipStatus) && shipStatus != "0")
            {
                strSql.AppendFormat(" and P.shipStatus = '{0}'", shipStatus);
            }
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                strSql.AppendFormat(" and UserName like '%{0}%'", phoneNumber);
            }
            if (id != 0)
            {
                strSql.AppendFormat(" and id = '{0}'", id);
            }
            strSql.Append(" inner join Goods G on P.goodsId = G.Id and G.status = 1");

            strSql.Append(" left join EmployeeInfo con on P.confirmBy = con.EmployeeID and con.EmployeeStatus = 1");

            strSql.Append(" left join EmployeeInfo sh on P.shipBy = sh.EmployeeID and sh.EmployeeStatus = 1");

            strSql.Append(" order by operateTime desc");
           
            //strSql.Append(" ) a where 1=1");
           

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 后台查询员工的兑换记录
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public DataTable QueryEmployeeExchangeLog(int employeeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select P.id,G.name GoodsName,E.EmployeeID,E.EmployeeFirstName,E.UserName,P.address,");
            strSql.Append("(case P.confirmStatus when 1 then '已确认' when -1 then '未确认' end) confirmStatus,");
            strSql.Append("(case P.shipStatus  when 1 then '已发货/已充值'  when -1 then '未发货/未充值' end) shipStatus,");
            strSql.Append(" P.exchangeRemark,P.exchangeStatus,P.operateTime,P.pointVariation,P.confirmTime,P.confirmBy,P.platform,P.serialNumber");
            strSql.Append(" from EmployeePointLog P");
            strSql.Append(" inner join EmployeeInfo E on P.employeeId = E.EmployeeID");
            strSql.Append(" inner join Goods G on P.goodsId = G.Id");
            strSql.Append(" and P.status = 1 and E.EmployeeStatus = 1 and G.status = 1");
            strSql.Append(" and P.employeeId = @employeeId");
            strSql.Append(" order by P.operateTime desc");

            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@employeeId", SqlDbType.Int)
            };
            para[0].Value = employeeId;
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }
        /// <summary>
        /// 积分商城：根据兑换记录ID查询兑换记录
        /// </summary>
        /// <param name="id">兑换记录ID</param>
        /// <returns></returns>
        public EmployeePointLog QueryExchangeLog(int id)
        {
            EmployeePointLog PointLog = new EmployeePointLog();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select P.address,");
            strSql.Append("P.confirmStatus,");
            strSql.Append("P.shipStatus,");
            strSql.Append("P.exchangeRemark,P.platform,P.serialNumber");
            strSql.Append(" from EmployeePointLog P");
            strSql.Append(" where P.status = 1");
            strSql.Append(" and P.id = @id");

            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@id",SqlDbType.BigInt)
            };
            para[0].Value = id;

            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                if (sdr.Read())
                {
                    PointLog.address = sdr["address"].ToString();
                    PointLog.confirmStatus = sdr.GetInt32(1);
                    PointLog.shipStatus = sdr.GetInt32(2);
                    PointLog.remark = sdr["exchangeRemark"].ToString();
                    PointLog.platform = sdr["platform"].ToString();
                    PointLog.serialNumber = sdr["serialNumber"].ToString();
                }
            }
            return PointLog;
        }

        /// <summary>
        /// 积分商城：客户端查询服务员的积分兑换记录
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public DataTable QueryExchangeLogForClient(int employeeId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select P.operateTime,G.name,");
            strSql.Append("(case P.exchangeStatus when -1 then '积分异常'");
            strSql.Append(" when 1 then '处理中' when 2 then '已兑换' end) exchangeStatus");
            strSql.Append(" from EmployeePointLog P");
            strSql.Append(" inner join EmployeeInfo E on P.employeeId = E.EmployeeID");
            strSql.Append(" inner join Goods G on P.goodsId = G.Id");
            strSql.Append(" and P.status = 1 and E.EmployeeStatus = 1 and G.status = 1");
            strSql.Append(" and E.EmployeeID = @EmployeeID");
            strSql.Append(" order by P.operateTime desc");

            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@EmployeeID",SqlDbType.Int)
            };
            para[0].Value = employeeId;

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 检查此兑换单是否已经发货
        /// </summary>
        /// <param name="pointLogId">兑换单号</param>
        /// <returns>true:已发货；false:未发货</returns>
        public bool IsShip(int pointLogId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select shipStatus");
            strSql.Append(" from EmployeePointLog");
            strSql.Append(" where id = @id and shipStatus = 1;");

            SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@id", SqlDbType.BigInt)
                };
            para[0].Value = pointLogId;

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
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
        /// 更改兑换单的兑换状态，确认状态
        /// </summary>
        /// <param name="exchangeStatus"></param>
        /// <param name="pointLogId"></param>
        /// <returns></returns>
        public bool UpdateExchangeStatus(int exchangeStatus, int confirmStatus, long pointLogId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update EmployeePointLog");
            strSql.Append(" set exchangeStatus = @exchangeStatus,");
            strSql.Append(" confirmStatus = @confirmStatus");
            strSql.Append(" where id = @id");

            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@exchangeStatus", SqlDbType.Int),
                new SqlParameter("@confirmStatus", SqlDbType.Int),
                new SqlParameter("@id", SqlDbType.BigInt)
            };
            para[0].Value = exchangeStatus;
            para[1].Value = confirmStatus;
            para[2].Value = pointLogId;

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
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
        /// 根据用户ID查询其所有的积分变动记录
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns></returns>
        public DataTable QueryEmployeePoint(int employeeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select operateTime,pointVariationMethods,pointVariation,remark");
            strSql.Append(" from EmployeePointLog ");
            strSql.Append(" where status = 1 ");
            strSql.Append(" and employeeId = @employeeId");
            strSql.Append(" order by operateTime desc");

            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@employeeId", SqlDbType.Int)
            };
            para[0].Value = employeeId;

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据用户ID查询最近一次积分变动
        /// </summary>
        /// <param name="employeeId">员工ID</param>
        /// <returns></returns>
        public DataTable QueryEmployeeLastPointLog(int employeeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 *");
            strSql.Append(" from (select operateTime,");
            strSql.Append(" c.UserName name, p.pointVariation,p.remark");
            strSql.Append(" from EmployeePointLog p");
            strSql.Append(" inner join EmployeeInfo e on p.employeeId = e.EmployeeID");
            strSql.Append(" inner join CustomerInfo c on p.customerId = c.CustomerID");
            strSql.Append(" and p.status = 1 and e.EmployeeStatus = 1 and c.CustomerStatus = 1");
            strSql.Append(" and p.employeeId = @employeeIdA");
            strSql.Append(" and p.pointVariationMethods in (" + (int)PointVariationMethods.CUSTOMER_EXPENSE_GET + "," + (int)PointVariationMethods.CLIENT_VALIDATION + ")");
            strSql.Append(" union");
            strSql.Append(" select operateTime,e.EmployeeFirstName name,p.pointVariation,p.remark");
            strSql.Append(" from EmployeePointLog p");
            strSql.Append(" inner join EmployeeInfo e on p.employeeId = e.EmployeeID");
            strSql.Append(" and p.status = 1 and e.EmployeeStatus = 1 and p.employeeId = @employeeIdB");
            strSql.Append(" and p.pointVariationMethods not in (" + (int)PointVariationMethods.CUSTOMER_EXPENSE_GET + "," + (int)PointVariationMethods.CLIENT_VALIDATION + ")) a");
            strSql.Append(" order by operateTime desc");

            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@employeeIdA", SqlDbType.Int),
                new SqlParameter("@employeeIdB", SqlDbType.Int)
            };
            para[0].Value = employeeId;
            para[1].Value = employeeId;

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询确认状态
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int QueryConfirmStatus(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select confirmStatus from EmployeePointLog where status = 1 and id = '{0}'", id);
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj =  SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString());

                if (obj != null)
                {
                    return Convert.ToInt32(obj);
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// 根据兑换单号查询员工ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int QueryEmployeeID(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select employeeId from EmployeePointLog where status = 1 and id = '{0}'", id);
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString());

                if (obj != null)
                {
                    return Convert.ToInt32(obj);
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
