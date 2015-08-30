using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class NotificationRecordManager
    {
        /// <summary>
        /// new增加一条数据
        /// </summary>
        public long InsertNotificationRecord(NotificationRecord model)
        {
            Object obj = null;
            try
            {
                if (!string.IsNullOrEmpty(model.pushToken))
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into NotificationRecord(");
                    strSql.Append("isLocked,pushToken,addTime,isSent,sendCount,appType,message,customType,customValue)");
                    strSql.Append(" values (");
                    strSql.Append("@isLocked,@pushToken,@addTime,@isSent,@sendCount,@appType,@message,@customType,@customValue)");
                    strSql.Append(";select @@IDENTITY");
                    SqlParameter[] parameters = {
					new SqlParameter("@isLocked", SqlDbType.Bit,1),
					new SqlParameter("@pushToken", SqlDbType.NVarChar),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@isSent", SqlDbType.Bit,1),
					new SqlParameter("@sendCount", SqlDbType.Int,4),
					new SqlParameter("@appType", SqlDbType.Int,4),
                    new SqlParameter("@message", SqlDbType.NVarChar,100),
                    new SqlParameter("@customType", SqlDbType.Int,4),
                    new SqlParameter("@customValue", SqlDbType.NVarChar,50)};
                    parameters[0].Value = model.isLocked;
                    parameters[1].Value = model.pushToken;
                    parameters[2].Value = model.addTime;
                    parameters[3].Value = model.isSent;
                    parameters[4].Value = model.sendCount;
                    parameters[5].Value = model.appType;
                    parameters[6].Value = model.message;
                    parameters[7].Value = model.customType;
                    parameters[8].Value = model.customValue;

                    obj = SqlHelper.ExecuteScalar(SqlHelper.NotificationConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
                } 
                else
                {
                    return 0;
                }
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
        /// 更新悠先点菜推送记录的发送相关信息
        /// isLocked, sendTime, isSent, sendCount（增量）
        /// </summary>
        public bool UpdateNotificationRecord(NotificationRecord model)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.NotificationConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update NotificationRecord set ");
                    strSql.Append("isLocked=@isLocked,");
                    strSql.Append("sendTime=@sendTime,");
                    strSql.Append("isSent=@isSent,");
                    strSql.Append("sendCount=sendCount+@sendCountValue");
                    strSql.Append(" where id=@id");
                    SqlParameter[] parameters = {
					new SqlParameter("@isLocked", SqlDbType.Bit,1),
					new SqlParameter("@sendTime", SqlDbType.DateTime),
					new SqlParameter("@isSent", SqlDbType.Bit,1),
					new SqlParameter("@sendCountValue", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.BigInt,8)};
                    parameters[0].Value = model.isLocked;
                    parameters[1].Value = model.sendTime;
                    parameters[2].Value = model.isSent;
                    parameters[3].Value = model.sendCount;
                    parameters[4].Value = model.id;

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
        /// 删除一条数据
        /// </summary>
        public bool DeleteNotificationRecord(long id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from NotificationRecord ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.BigInt,8)
			};
            parameters[0].Value = id;

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.NotificationConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
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
        /// 锁定推送记录里的第一条未发送的消息，并返回该消息的编号，推送码，推送内容，设备类型
        /// </summary>
        /// <returns></returns>
        public DataTable LockNotification()
        {
            DataTable result = new DataTable();
            try
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("select top 1 [id],[pushToken],[message],[appType],[customType],[customValue]  FROM [NotificationRecord] as A where A.isSent = 'false' and A.isLocked = 'false' and A.sendCount < 3 ;");
                strSql.Append("update [NotificationRecord] set [isLocked] = 'true' where id in (select top 1 id FROM [NotificationRecord] as A where A.isSent = 'false' and A.isLocked = 'false' and A.sendCount < 3)");

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.NotificationConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

                result = ds.Tables[0];
            }
            catch (System.Exception)
            {

            }
            return result;
        }

        public DataTable SelectNotificationRecord()
        {
            DataTable result = new DataTable();
            try
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("select top 1 [id],[pushToken],[message],[appType],[customType],[customValue]  FROM [NotificationRecord] as A where A.isSent = 'false' and A.isLocked = 'false' and A.sendCount < 3 ;");

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.NotificationConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

                result = ds.Tables[0];
            }
            catch (System.Exception)
            {

            }
            return result;
        }

        public bool UpdateNotificationRecordLock(long id)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.NotificationConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update NotificationRecord set ");
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
        /// 根据添加时间，查询悠先点菜推送表数据
        /// </summary>
        /// <param name="strAddTime"></param>
        /// <param name="endAddTime"></param>
        /// <returns></returns>
        public DataTable SelectNotification(string strAddTime, string endAddTime)
        {
            DataTable result = new DataTable();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select [id],[isLocked],[pushToken],[addTime],[sendTime],");
                strSql.Append("[isSent],[sendCount],[appType],[message],[customType],[customValue]");
                strSql.Append(" from NotificationRecord where addTime between @strAddTime and @endAddTime");
                SqlParameter[] parameters = {
					new SqlParameter("@strAddTime",strAddTime),
					new SqlParameter("@endAddTime",endAddTime)};
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.NotificationConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
                result = ds.Tables[0];
            }
            catch (System.Exception)
            {

            }
            return result;
        }
        /// <summary>
        /// 根据customerID查询该用户最新手机设备信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectNewDeviceInfo(int customerId)
        {
            DataTable result = new DataTable();
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select top 1 A.pushToken,A.appType,MAX(A.updateTime)");
            strSql.Append(" from DeviceInfo A inner join CustomerConnDevice B on B.deviceId=A.deviceId ");
            strSql.Append(" where customerId=@customerId group by A.pushToken,A.appType order by max(A.updateTime) desc");
            SqlParameter[] parameters = {
					new SqlParameter("@customerId",customerId)};
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            result = ds.Tables[0];
            return result;
        }

        #region 悠先服务
        
        /// <summary>
        /// 增加一条数据至悠先服务推送表
        /// </summary>
        public long InsertUxianServiceNotificationRecord(NotificationRecord model)
        {
            Object obj = null;
            try
            {
                if (!string.IsNullOrEmpty(model.pushToken))
                {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into UxianServiceNotificationRecord(");
                    strSql.Append("isLocked,pushToken,addTime,isSent,sendCount,appType,message,customType,customValue)");
                    strSql.Append(" values (");
                    strSql.Append("@isLocked,@pushToken,@addTime,@isSent,@sendCount,@appType,@message,@customType,@customValue)");
                    strSql.Append(";select @@IDENTITY");
                    SqlParameter[] parameters = {
					new SqlParameter("@isLocked", SqlDbType.Bit,1),
					new SqlParameter("@pushToken", SqlDbType.NVarChar),
					new SqlParameter("@addTime", SqlDbType.DateTime),
					new SqlParameter("@isSent", SqlDbType.Bit,1),
					new SqlParameter("@sendCount", SqlDbType.Int,4),
					new SqlParameter("@appType", SqlDbType.Int,4),
                    new SqlParameter("@message", SqlDbType.NVarChar,100),
                    new SqlParameter("@customType", SqlDbType.Int,4),
                    new SqlParameter("@customValue", SqlDbType.NVarChar,50)};
                    parameters[0].Value = model.isLocked;
                    parameters[1].Value = model.pushToken;
                    parameters[2].Value = model.addTime;
                    parameters[3].Value = model.isSent;
                    parameters[4].Value = model.sendCount;
                    parameters[5].Value = model.appType;
                    parameters[6].Value = model.message;
                    parameters[7].Value = model.customType;
                    parameters[8].Value = model.customValue;

                    obj = SqlHelper.ExecuteScalar(SqlHelper.NotificationConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
                }
                else
                {
                    return 0;
                }
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
        /// 更新悠先服务推送记录的发送相关信息
        /// isLocked, sendTime, isSent, sendCount（增量）
        /// </summary>
        public bool UpdateUxianServiceNotificationRecord(NotificationRecord model)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.NotificationConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update UxianServiceNotificationRecord set ");
                    strSql.Append("isLocked=@isLocked,");
                    strSql.Append("sendTime=@sendTime,");
                    strSql.Append("isSent=@isSent,");
                    strSql.Append("sendCount=sendCount+@sendCountValue");
                    strSql.Append(" where id=@id");
                    SqlParameter[] parameters = {
					new SqlParameter("@isLocked", SqlDbType.Bit,1),
					new SqlParameter("@sendTime", SqlDbType.DateTime),
					new SqlParameter("@isSent", SqlDbType.Bit,1),
					new SqlParameter("@sendCountValue", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.BigInt,8)};
                    parameters[0].Value = model.isLocked;
                    parameters[1].Value = model.sendTime;
                    parameters[2].Value = model.isSent;
                    parameters[3].Value = model.sendCount;
                    parameters[4].Value = model.id;

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
        /// 删除一条数据
        /// </summary>
        public bool DeleteUxianServiceNotificationRecord(long id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from UxianServiceNotificationRecord ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.BigInt,8)
			};
            parameters[0].Value = id;

            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.NotificationConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       
        public DataTable SelectUxianServiceNotificationRecord()
        {
            DataTable result = new DataTable();
            try
            {

                StringBuilder strSql = new StringBuilder();
                strSql.Append("select top 1 [id],[pushToken],[message],[appType],[customType],[customValue]  FROM [UxianServiceNotificationRecord] as A where A.isSent = 'false' and A.isLocked = 'false' and A.sendCount < 3 ;");

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.NotificationConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

                result = ds.Tables[0];
            }
            catch (System.Exception)
            {

            }
            return result;
        }

        public bool UpdateUxianServiceNotificationRecordLock(long id)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.NotificationConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update UxianServiceNotificationRecord set ");
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
        /// 根据添加时间，查询悠先服务推送表数据
        /// </summary>
        /// <param name="strAddTime"></param>
        /// <param name="endAddTime"></param>
        /// <returns></returns>
        public DataTable SelectUxianServiceNotification(string strAddTime, string endAddTime)
        {
            DataTable result = new DataTable();
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select [id],[isLocked],[pushToken],[addTime],[sendTime],");
                strSql.Append("[isSent],[sendCount],[appType],[message],[customType],[customValue]");
                strSql.Append(" from UxianServiceNotificationRecord where addTime between @strAddTime and @endAddTime");
                SqlParameter[] parameters = {
					new SqlParameter("@strAddTime",strAddTime),
					new SqlParameter("@endAddTime",endAddTime)};
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.NotificationConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
                result = ds.Tables[0];
            }
            catch (System.Exception)
            {

            }
            return result;
        }

        #endregion
    }
}
