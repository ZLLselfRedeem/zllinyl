using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using System.Data.SqlClient;
using System.Data;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 自定义推送（悠先点菜）
    /// 2014-8-11
    /// </summary>
    public class CustomPushConfigManager
    {
        /// <summary>
        /// 新增一条自定义推送
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public long InsertCustomPushRecord(CustomPushRecord record)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CustomPushRecord(");
            strSql.Append("isLocked,pushToken,addTime,customSendTime,isSent,sendCount,appType,message,customType,customValue,mobilePhoneNumber,customerId)");
            strSql.Append(" values(");
            strSql.Append("@isLocked,@pushToken,@addTime,@customSendTime,@isSent,@sendCount,@appType,@message,@customType,@customValue,@mobilePhoneNumber,@customerId)");
            strSql.Append(" select @@identity");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@isLocked", SqlDbType.Bit,1) { Value = record.isLocked },
					new SqlParameter("@pushToken", SqlDbType.NVarChar) { Value = record.pushToken },
					new SqlParameter("@addTime", SqlDbType.DateTime) { Value = DateTime.Now },
                    new SqlParameter("@customSendTime", SqlDbType.DateTime) { Value = record.customSendTime },
					new SqlParameter("@isSent", SqlDbType.Bit,1) { Value = record.isSent },
					new SqlParameter("@sendCount", SqlDbType.Int,4) { Value = record.sendCount },
					new SqlParameter("@appType", SqlDbType.Int,4) { Value = record.appType },
                    new SqlParameter("@message", SqlDbType.NVarChar,100) { Value = record.message },
                    new SqlParameter("@customType", SqlDbType.NVarChar,50) { Value = record.customType },
                    new SqlParameter("@customValue", SqlDbType.NVarChar,50) { Value = record.customValue },
                    new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar) { Value = record.mobilePhoneNumber },
                    new SqlParameter("@customerId", SqlDbType.NVarChar) { Value = record.customerId }
                };
                using (SqlConnection conn = new SqlConnection(SqlHelper.NotificationConnectionStringLocalTransaction))
                {
                    conn.Open();
                    object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
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
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 批量新增自定义推送记录
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool BatchInsertCustomPushRecord(DataTable dt)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.NotificationConnectionStringLocalTransaction))
            {
                conn.Open();
                SqlTransaction sqlbulkTransaction = conn.BeginTransaction();
                SqlBulkCopy sqlbulkcopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.UseInternalTransaction, null);
                sqlbulkcopy.DestinationTableName = "CustomPushRecord";//数据库中的表名
                sqlbulkcopy.BulkCopyTimeout = 30;
                sqlbulkcopy.ColumnMappings.Add("isLocked", "isLocked");
                sqlbulkcopy.ColumnMappings.Add("pushToken", "pushToken");
                sqlbulkcopy.ColumnMappings.Add("addTime", "addTime");
                sqlbulkcopy.ColumnMappings.Add("customSendTime", "customSendTime");
                sqlbulkcopy.ColumnMappings.Add("isSent", "isSent");
                sqlbulkcopy.ColumnMappings.Add("sendCount", "sendCount");
                sqlbulkcopy.ColumnMappings.Add("appType", "appType");
                sqlbulkcopy.ColumnMappings.Add("message", "message");
                sqlbulkcopy.ColumnMappings.Add("customType", "customType");
                sqlbulkcopy.ColumnMappings.Add("customValue", "customValue");
                sqlbulkcopy.ColumnMappings.Add("mobilePhoneNumber", "mobilePhoneNumber");
                sqlbulkcopy.ColumnMappings.Add("customerId", "customerId");
                try
                {
                    sqlbulkcopy.WriteToServer(dt);
                    sqlbulkTransaction.Commit();
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 更新推送记录的发送相关信息
        /// isLocked, sendTime, isSent, sendCount（增量）
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        public bool UpdateCustomPushRecord(CustomPushRecord record)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CustomPushRecord set ");
            strSql.Append("isLocked=@isLocked,");
            strSql.Append("sendTime=@sendTime,");
            strSql.Append("isSent=@isSent,");
            strSql.Append("sendCount=sendCount+@sendCountValue");
            strSql.Append(" where id=@id");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@isLocked", SqlDbType.Bit,1) { Value = record.isLocked },
					new SqlParameter("@sendTime", SqlDbType.DateTime) { Value = record.sendTime },
					new SqlParameter("@isSent", SqlDbType.Bit,1) { Value = record.isSent },
					new SqlParameter("@sendCountValue", SqlDbType.Int,4) { Value = record.sendCount },
					new SqlParameter("@id", SqlDbType.BigInt,8) { Value = record.id }
                };
                using (SqlConnection conn = new SqlConnection(SqlHelper.NotificationConnectionStringLocalTransaction))
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
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateRecordLock(long id)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.NotificationConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CustomPushRecord set ");
                    strSql.Append("isLocked='true'");
                    strSql.Append(" where id=@id");
                    SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.BigInt,8)};
                    parameters[0].Value = id;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (System.Exception)
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
        /// 删除一条未发送的自定义推送
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteCustomPushRecord(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from CustomPushRecord where id = @id and isSent = 0");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@id", SqlDbType.BigInt) { Value = id }
                };
                using (SqlConnection conn = new SqlConnection(SqlHelper.NotificationConnectionStringLocalTransaction))
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
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 查询推送列表
        /// </summary>
        /// <returns></returns>
        public List<CustomPushRecord> SelectAllCustomPushRecord(SQLServerDAL.Persistence.Infrastructure.Page page, int customType, bool isSent, string beginTime, string endTime, out int cnt, long id = 0)
        {
            List<CustomPushRecord> records = new List<CustomPushRecord>();
            StringBuilder strSql = new StringBuilder();
            StringBuilder strCnt = new StringBuilder();
            strCnt.Append("select count(1) from CustomPushRecord where 1=1");

            strSql.Append("select a.id,a.isLocked,a.pushToken,a.addTime,a.customSendTime,isnull(a.sendTime,'1970-01-01') sendTime,a.isSent,a.sendCount,a.appType,a.message,a.customType,a.customValue,a.mobilePhoneNumber,a.customerId from");
            strSql.Append("(select ROW_NUMBER() over(order by id desc) rownum,id,isLocked,pushToken,addTime,customSendTime,sendTime,");
            strSql.Append("isSent,sendCount,appType,message,customType,customValue,mobilePhoneNumber,customerId");
            strSql.Append(" from CustomPushRecord where 1=1");
            if (id > 0)
            {
                strSql.Append(" and id = @id");
                strCnt.Append(" and id = @id");
            }
            if (customType > 0)
            {
                strSql.Append(" and customType = @customType");
                strCnt.Append(" and customType = @customType");
            }
            strSql.Append(" and isSent = @isSent");
            strCnt.Append(" and isSent = @isSent");
            strSql.Append(" and customSendTime between @beginTime and @endTime");
            strCnt.Append(" and customSendTime between @beginTime and @endTime");
            strSql.Append(") a");
            strSql.Append(" where a.rownum between @startIndex and @endIndex");

            List<SqlParameter> paraList = new List<SqlParameter>();
            SqlParameter[] paraCnt = new SqlParameter[] { };
            if (id > 0)
            {
                paraList.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });
            }
            if (customType > 0)
            {
                paraList.Add(new SqlParameter("@customType", SqlDbType.NVarChar, 50) { Value = customType });
            }
            paraList.Add(new SqlParameter("@isSent", SqlDbType.Bit) { Value = isSent });
            paraList.Add(new SqlParameter("@beginTime", SqlDbType.DateTime) { Value = beginTime });
            paraList.Add(new SqlParameter("@endTime", SqlDbType.DateTime) { Value = endTime });

            paraCnt = paraList.ToArray();
            paraList.Add(new SqlParameter("@startIndex", SqlDbType.Int) { Value = page.Skip + 1 });
            paraList.Add(new SqlParameter("@endIndex", SqlDbType.Int) { Value = page.Skip + page.PageSize });

            SqlParameter[] para = paraList.ToArray();
            object objCnt = SqlHelper.ExecuteScalar(SqlHelper.NotificationConnectionStringLocalTransaction, CommandType.Text, strCnt.ToString(), paraCnt);
            if (objCnt == null)
            {
                cnt = 0;
            }
            else
            {
                cnt = Convert.ToInt32(objCnt);
            }
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.NotificationConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                while (sdr.Read())
                {
                    CustomPushRecord record = new CustomPushRecord();
                    record.id = Convert.ToInt64(sdr["id"]);
                    record.isLocked = Convert.ToBoolean(sdr["isLocked"]);
                    record.pushToken = sdr["pushToken"].ToString();
                    record.addTime = Convert.ToDateTime(sdr["addTime"]);
                    record.customSendTime = Convert.ToDateTime(sdr["customSendTime"]);
                    record.sendTime = Convert.ToDateTime(sdr["sendTime"]);
                    record.isSent = Convert.ToBoolean(sdr["isSent"]);
                    record.sendCount = Convert.ToInt32(sdr["sendCount"]);
                    record.appType = Convert.ToInt32(sdr["appType"]);
                    record.message = sdr["message"].ToString();
                    record.customType = sdr["customType"].ToString();
                    record.customValue = sdr["customValue"].ToString();
                    record.mobilePhoneNumber = sdr["mobilePhoneNumber"].ToString();
                    record.customerId = sdr["customerId"].ToString();
                    records.Add(record);
                }
            }
            return records;
        }

        /// <summary>
        /// 查询推送列表
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAllCustomPush(long id = 0, int customType = 0, string mobilePhone = "")
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,isLocked,pushToken,addTime,customSendTime,sendTime,");
            strSql.Append("isSent,sendCount,appType,message,customType,customValue,mobilePhoneNumber,customerId");
            strSql.Append(" from CustomPushRecord where 1=1");
            if (id > 0)
            {
                strSql.Append(" and id = @id");
            }
            if (customType > 0)
            {
                strSql.Append(" and customType=@customType");
            }
            if (mobilePhone != "")
            {
                strSql.Append(" and mobilePhoneNumber like @mobilePhoneNumber");
            }
            List<SqlParameter> paraList = new List<SqlParameter>();
            if (id > 0)
            {
                paraList.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });
            }
            if (customType > 0)
            {
                paraList.Add(new SqlParameter("@customType", SqlDbType.Int) { Value = customType });
            }
            if (mobilePhone != "")
            {
                paraList.Add(new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar) { Value = "%" + mobilePhone + "%" });
            }
            SqlParameter[] para = paraList.ToArray();
            DataSet ds = new DataSet();
            if (para.Length > 0)
            {
                ds = SqlHelper.ExecuteDataSet(SqlHelper.NotificationConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            }
            else
            {
                ds = SqlHelper.ExecuteDataSet(SqlHelper.NotificationConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            }
            return ds.Tables[0];
        }
        /// <summary>
        /// 抓取一条待发送记录
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCustomPushRecord()
        {
            DataTable result = new DataTable();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select top 1 [id],[pushToken],[message],[appType],[customType],[customValue]  FROM [CustomPushRecord] as A ");
                strSql.Append(" where A.isSent = 'false' and A.isLocked = 'false' and A.sendCount < 3 ");
                strSql.Append(" and CONVERT(varchar(10),customSendTime,120)=CONVERT(varchar(10),GETDATE(),120)");//2014-08-13
                strSql.Append(" and DATEPART(HOUR,customSendTime) = DATEPART(HOUR,GETDATE())");
                strSql.Append(" and DATEPART(MINUTE,customSendTime) = DATEPART(MINUTE,GETDATE())");//自定义发送时间=当前时间（精确到分钟）

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.NotificationConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

                result = ds.Tables[0];
            }
            catch (System.Exception)
            {

            }
            return result;
        }

        /// <summary>
        /// 根据用户手机号码检查其最新登录的设备信息
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public CustomerDevicePushInfo SelectCustomerInfo(string mobilePhoneNumber)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Top 1 A.CustomerID,pushToken,appType,appBuild,A.mobilePhoneNumber");
            strSql.Append(" from CustomerInfo as A");
            strSql.Append(" inner join CustomerConnDevice as B on A.CustomerID = B.customerId");
            strSql.Append(" inner join DeviceInfo as C on C.deviceId = B.deviceId");
            strSql.Append(" where A.mobilePhoneNumber = @mobilePhoneNumber order by B.updateTime desc");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = mobilePhoneNumber }
            };
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                if (sdr.Read())
                {
                    CustomerDevicePushInfo customerDevice = new CustomerDevicePushInfo();
                    customerDevice.CustomerID = Convert.ToInt64(sdr["CustomerID"]);
                    customerDevice.pushToken = sdr["pushToken"].ToString();
                    customerDevice.appType = (VAAppType)sdr["appType"];
                    customerDevice.appBuild = sdr["appBuild"].ToString();
                    return customerDevice;
                }
                else
                {
                    return null;
                }
            }
        }
        /// <summary>
        /// 根据拼接好的手机号码组查询用户设备信息
        /// </summary>
        /// <param name="phoneArray">手机号码用英文逗号隔开</param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<CustomerDevicePushInfo> SelectCustomerInfoList(SQLServerDAL.Persistence.Infrastructure.Page page, string phoneArray, out int cnt)
        {
            StringBuilder strSql = new StringBuilder();
            StringBuilder strCnt = new StringBuilder();

            strCnt.Append("select count(1) from ");
            strCnt.Append(" (select ROW_NUMBER() over(order by E.mobilePhoneNumber) rownum");
            strCnt.Append(" from (select ROW_NUMBER() over(partition by uuid order by D.updateTime desc) repeat,");
            strCnt.Append(" D.CustomerID,D.UserName,D.mobilePhoneNumber,D.pushToken,D.uuid,D.appType");
            strCnt.Append(" from (select ROW_NUMBER() over(partition by A.customerId order by B.updateTime desc) as lev,");
            strCnt.Append(" A.CustomerID,A.UserName,A.mobilePhoneNumber,pushToken,C.uuid,appType,B.updateTime");
            strCnt.Append(" from CustomerInfo as A");
            strCnt.Append(" inner join CustomerConnDevice as B on A.CustomerID = B.customerId");
            strCnt.Append(" inner join DeviceInfo as C on C.deviceId = B.deviceId");
            strCnt.AppendFormat(" where A.mobilePhoneNumber in ({0})", phoneArray);
            strCnt.Append(" and isnull(C.pushToken, '') != '' and ISNULL(C.appType, '') != '') D");
            strCnt.Append(" where D.lev = 1) E");
            strCnt.Append(" where E.repeat = 1) F");

            strSql.Append("select F.CustomerID,F.UserName, F.mobilePhoneNumber, F.pushToken, F.appType from ");
            strSql.Append(" (select ROW_NUMBER() over(order by E.mobilePhoneNumber) rownum,");
            strSql.Append(" E.CustomerID,E.UserName, E.mobilePhoneNumber, E.pushToken, E.appType");
            strSql.Append(" from (select ROW_NUMBER() over(partition by uuid order by D.updateTime desc) repeat,");
            strSql.Append(" D.CustomerID,D.UserName,D.mobilePhoneNumber,D.pushToken,D.uuid,D.appType");
            strSql.Append(" from (select ROW_NUMBER() over(partition by A.customerId order by B.updateTime desc) as lev,");
            strSql.Append(" A.CustomerID,A.UserName,A.mobilePhoneNumber,pushToken,C.uuid,appType,B.updateTime");
            strSql.Append(" from CustomerInfo as A");
            strSql.Append(" inner join CustomerConnDevice as B on A.CustomerID = B.customerId");
            strSql.Append(" inner join DeviceInfo as C on C.deviceId = B.deviceId");
            strSql.AppendFormat(" where A.mobilePhoneNumber in ({0})", phoneArray);
            strSql.Append(" and ISNULL(C.appType, '') != '') D");
            strSql.Append(" where D.lev = 1) E");
            strSql.Append(" where E.repeat = 1) F");
            strSql.AppendFormat(" where isnull(F.pushToken, '') != ''  and F.rownum between {0} and {1}", page.Skip + 1, page.Skip + page.PageSize);

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object objCnt = SqlHelper.ExecuteScalar(conn, CommandType.Text, strCnt.ToString());
                if (objCnt == null)
                {
                    cnt = 0;
                }
                else
                {
                    cnt = Convert.ToInt32(objCnt);
                }
            }
            List<CustomerDevicePushInfo> list = new List<CustomerDevicePushInfo>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()))
            {
                while (sdr.Read())
                {
                    CustomerDevicePushInfo customer = new CustomerDevicePushInfo();
                    customer.CustomerID = Convert.ToInt64(sdr["CustomerID"]);
                    customer.UserName = sdr["UserName"].ToString();
                    customer.mobilePhoneNumber = sdr["mobilePhoneNumber"].ToString();
                    customer.pushToken = sdr["pushToken"].ToString();
                    customer.appType = (VAAppType)sdr["appType"];
                    list.Add(customer);
                }
            }
            return list;
        }

        /// <summary>
        /// 根据用户ID查询其所有未支付的订单
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable SelectNotPaidOrders(long customerId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select p.preOrder19dianId,s.shopName,p.preOrderSum,p.preOrderTime,p.orderInJson");
            strSql.Append(" from PreOrder19dian p inner join ShopInfo s on p.shopId = s.shopID and s.shopStatus = 1");
            strSql.Append(" where p.customerId = @customerId");
            strSql.Append(" and isnull(isPaid,'')='' order by p.preOrderTime desc");
            SqlParameter[] para = new SqlParameter[]{
            new SqlParameter("@customerId", SqlDbType.BigInt) { Value = customerId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据店铺名称查询店铺Id
        /// </summary>
        /// <param name="shopName"></param>
        /// <returns></returns>
        public string SelectShopId(string shopName)
        {
            string strSql = "select shopId from shopInfo where shopName = @shopName";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@shopName", SqlDbType.NVarChar, 500) { Value = @shopName }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
                if (obj == null)
                {
                    return "";
                }
                else
                {
                    return obj.ToString();
                }
            }
        }
    }
}
