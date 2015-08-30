using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 用户相关信息
    /// </summary>
    public class CustomerManager
    {
        private static Object update19dianMoenyRemainLock = new Object();

        /// <summary>
        /// new新增用户收藏公司信息
        /// </summary>
        /// <param name="customerFavoriteCompany"></param>
        /// <returns></returns>
        public long InsertCustomerFavoriteCompany(CustomerFavoriteCompany customerFavoriteCompany)
        {

            Object obj = null;
            try
            {
                StringBuilder strSql = new StringBuilder();
                SqlParameter[] parameters = null;
                strSql.Append("insert into CustomerFavoriteCompany(");
                strSql.Append("customerId,companyId,");
                strSql.Append("collectTime,shopId)");
                strSql.Append(" values (");
                strSql.Append("@customerId,@companyId,");
                strSql.Append("@collectTime,@shopId)");
                strSql.Append(" select @@identity");
                parameters = new SqlParameter[]{
					        new SqlParameter("@customerId", SqlDbType.BigInt,8),
                            new SqlParameter("@companyId", SqlDbType.Int,4),
                            new SqlParameter("@collectTime",SqlDbType.DateTime),
                            new SqlParameter("@shopId",SqlDbType.Int,4)};
                parameters[0].Value = customerFavoriteCompany.customerId;
                parameters[1].Value = customerFavoriteCompany.companyId;
                parameters[2].Value = System.DateTime.Now;
                parameters[3].Value = customerFavoriteCompany.shopId;
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                }
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
        /// <summary>
        /// new新增用户信息20140313
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public long InsertCustomer(CustomerInfo customer)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into CustomerInfo(");
                    strSql.Append("CustomerRankID,RegisterDate,");
                    strSql.Append("CustomerStatus,cookie,wechatId,registerCityId)");
                    strSql.Append(" values (");
                    strSql.Append("@CustomerRankID,@RegisterDate,");
                    strSql.Append("@CustomerStatus,@cookie,@wechatId,@registerCityId)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					        new SqlParameter("@CustomerRankID", SqlDbType.Int,4),
                            new SqlParameter("@RegisterDate", SqlDbType.DateTime),
                            new SqlParameter("@CustomerStatus",SqlDbType.Int,4),
                            new SqlParameter("@cookie",SqlDbType.NVarChar,100),
                            new SqlParameter("@wechatId",SqlDbType.NVarChar,100),
                            new SqlParameter("@registerCityId",SqlDbType.Int,4)};
                    parameters[0].Value = customer.CustomerRankID;
                    parameters[1].Value = customer.RegisterDate;
                    parameters[2].Value = customer.CustomerStatus;
                    parameters[3].Value = customer.cookie;
                    parameters[4].Value = customer.wechatId;
                    parameters[5].Value = customer.registerCityId;
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
        /// <summary>
        /// new新增用户邀请记录信息
        /// </summary>
        /// <param name="customerInviteRecord"></param>
        /// <returns></returns>
        public long InsertCustomerInviteRecord(CustomerInviteRecord customerInviteRecord)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                const string strSql = @"insert into CustomerInviteRecord( customerId,phoneNumberInvite,phoneNumbeInvited,inviteTime) values (@customerId,@phoneNumberInvite,
 @phoneNumbeInvited,@inviteTime) select @@identity";
                SqlParameter[] parameters = new SqlParameter[]{
					        new SqlParameter("@customerId", SqlDbType.BigInt,8),
                            new SqlParameter("@phoneNumberInvite",SqlDbType.NVarChar,50),
                            new SqlParameter("@phoneNumberInvited",SqlDbType.NVarChar,50),
                            new SqlParameter("@inviteTime", SqlDbType.DateTime)};
                parameters[0].Value = customerInviteRecord.customerId;
                parameters[1].Value = customerInviteRecord.phoneNumberInvite;
                parameters[2].Value = customerInviteRecord.phoneNumberInvited;
                parameters[3].Value = customerInviteRecord.inviteTime;
                Object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                return Convert.ToInt64(obj);
            }
        }
        /// <summary>
        /// new新增用户忘记密码信息
        /// </summary>
        /// <param name="customerForgetPassword"></param>
        /// <returns></returns>
        public long InsertCustomerForgetPassword(CustomerForgetPassword customerForgetPassword)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into CustomerForgetPassword(");
                    strSql.Append("customerId,sendEmailTime,");
                    strSql.Append("status,verifyCode)");
                    strSql.Append(" values (");
                    strSql.Append("@customerId,@sendEmailTime,");
                    strSql.Append("@status,@verifyCode)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					        new SqlParameter("@customerId", SqlDbType.BigInt,8),
                            new SqlParameter("@sendEmailTime", SqlDbType.DateTime),
                            new SqlParameter("@status",SqlDbType.Int,4),
                            new SqlParameter("@verifyCode",SqlDbType.NVarChar,500)};
                    parameters[0].Value = customerForgetPassword.customerId;
                    parameters[1].Value = customerForgetPassword.sendEmailTime;
                    parameters[2].Value = 1;
                    parameters[3].Value = customerForgetPassword.verifyCode;

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
        ///<summary>
        /// new新增设备信息20140313
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        public long InsertDevice(DeviceInfo device)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into DeviceInfo(");
                    strSql.Append("uuid,pushToken,updateTime,appType)");
                    strSql.Append(" values (");
                    strSql.Append("@uuid,@pushToken,@updateTime,@appType)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					        new SqlParameter("@uuid", SqlDbType.NVarChar,100),
                            new SqlParameter("@pushToken", SqlDbType.NVarChar,100),
                            new SqlParameter("@updateTime", SqlDbType.DateTime),
                            new SqlParameter("@appType",SqlDbType.Int,4)};
                    parameters[0].Value = device.uuid;
                    parameters[1].Value = device.pushToken;
                    parameters[2].Value = device.updateTime;
                    parameters[3].Value = device.type;

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
        /// <summary>
        /// new新增用户与设备关系信息20140313
        /// </summary>
        /// <param name="customerConnDevice"></param>
        /// <returns></returns>
        public long InsertCustomerConnDevice(CustomerConnDevice customerConnDevice)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into CustomerConnDevice(");
                    strSql.Append("customerId,deviceId,updateTime)");
                    strSql.Append(" values (");
                    strSql.Append("@customerId,@deviceId,@updateTime)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					        new SqlParameter("@customerId", SqlDbType.BigInt,8),
                            new SqlParameter("@deviceId", SqlDbType.BigInt,8),
                            new SqlParameter("@updateTime",SqlDbType.DateTime)};
                    parameters[0].Value = customerConnDevice.customerId;
                    parameters[1].Value = customerConnDevice.deviceId;
                    parameters[2].Value = customerConnDevice.updateTime;

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
        ///// <summary>
        ///// new新增用户登录信息
        ///// </summary>
        ///// <param name="customerLogin"></param>
        ///// <returns></returns>
        //public long InsertCustomerLogin(CustomerLoginInfo customerLogin)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        Object obj = null;
        //        try
        //        {
        //            conn.Open();

        //            StringBuilder strSql = new StringBuilder();
        //            SqlParameter[] parameters = null;
        //            strSql.Append("insert into CustomerLoginInfo(");
        //            strSql.Append("customerId,uuid,loginTime,loginCityId)");
        //            strSql.Append(" values (");
        //            strSql.Append("@customerId,@uuid,@loginTime,@loginCityId)");
        //            strSql.Append(" select @@identity");
        //            parameters = new SqlParameter[]{
        //                    new SqlParameter("@customerId", SqlDbType.BigInt,8),
        //                    new SqlParameter("@uuid", SqlDbType.NVarChar,100),
        //                    new SqlParameter("@loginTime", SqlDbType.DateTime),
        //                    new SqlParameter("@loginCityId", SqlDbType.Int,4)};
        //            parameters[0].Value = customerLogin.customerId;
        //            parameters[1].Value = customerLogin.uuid;
        //            parameters[2].Value = customerLogin.loginTime;
        //            parameters[3].Value = customerLogin.loginCityId;

        //            obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);

        //        }
        //        catch (Exception)
        //        {
        //            return 0;
        //        }
        //        if (obj == null)
        //        {
        //            return 0;
        //        }
        //        else
        //        {
        //            return Convert.ToInt64(obj);
        //        }
        //    }
        //}
        /// <summary>
        /// new新增OpenId信息
        /// xiaoyu 20140319
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        public long InsertOpenId(OpenIdInfo openId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into OpenIdInfo(");
                    strSql.Append("customerId,openIdUid,openIdBindTime,expirationDate,openIdType,openIdUpdateTime)");
                    strSql.Append(" values (");
                    strSql.Append("@customerId,@openIdUid,@openIdBindTime,@expirationDate,@openIdType,@openIdUpdateTime)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					        new SqlParameter("@customerId", SqlDbType.BigInt,8),
                            new SqlParameter("@openIdUid", SqlDbType.NVarChar,100),
                            new SqlParameter("@openIdBindTime", SqlDbType.DateTime),
                            new SqlParameter("@expirationDate", SqlDbType.DateTime),
                            new SqlParameter("@openIdType", SqlDbType.Int),
                            new SqlParameter("@openIdUpdateTime", SqlDbType.DateTime)
                    };
                    parameters[0].Value = openId.customerId;
                    parameters[1].Value = openId.openIdUid;
                    parameters[2].Value = openId.openIdBindTime;
                    parameters[3].Value = openId.expirationDate;
                    parameters[4].Value = (int)openId.openIdType;
                    parameters[5].Value = openId.openIdUpdateTime;

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
        /// <summary>
        /// 插入用户的充值订单信息
        /// </summary>
        /// <param name="customerChargeOrder"></param>
        /// <returns></returns>
        public long InsertCustomerChargeOrder(CustomerChargeOrder customerChargeOrder)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into CustomerChargeOrder(");
                    strSql.Append("createTime,status,customerCookie,customerUUID,priceSum,customerId,subjectName)");
                    strSql.Append(" values (");
                    strSql.Append("@createTime,@status,@customerCookie,@customerUUID,@priceSum,@customerId,@subjectName)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					    new SqlParameter("@createTime", SqlDbType.DateTime),
                        new SqlParameter("@status",SqlDbType.TinyInt,2),
                        new SqlParameter("@customerCookie", SqlDbType.NVarChar,100),
                        new SqlParameter("@customerUUID", SqlDbType.NVarChar,100),
                        new SqlParameter("@priceSum",SqlDbType.Float),
                        new SqlParameter("@customerId",SqlDbType.BigInt,8),
                        new SqlParameter("@subjectName",SqlDbType.NVarChar,500)
                    };
                    parameters[0].Value = customerChargeOrder.createTime;
                    parameters[1].Value = customerChargeOrder.status;
                    parameters[2].Value = customerChargeOrder.customerCookie;
                    parameters[3].Value = customerChargeOrder.customerUUID;
                    parameters[4].Value = customerChargeOrder.priceSum;
                    parameters[5].Value = customerChargeOrder.customerId;
                    parameters[6].Value = customerChargeOrder.subjectName;

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
        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public bool DeleteCustomer(long customerID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {

                SqlTransaction tran = null;
                int result = 0;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("update CustomerInfo set CustomerStatus = '-1' where CustomerID=@CustomerID;");

                    SqlParameter[] parameters = {					
					new SqlParameter("@CustomerID", SqlDbType.BigInt,8)};
                    parameters[0].Value = customerID;

                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                }
                if (result > 0)
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
        /// new删除用户收藏公司信息
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public bool DeleteCustomerFavoriteCompany(long customerFavoriteCompanyId)
        {
            int result = 0;
            try
            {
                StringBuilder strSql = new StringBuilder();

                strSql.Append("delete from CustomerFavoriteCompany where id=@id;");

                SqlParameter[] parameters = {					
					new SqlParameter("@id", SqlDbType.BigInt,8)};
                parameters[0].Value = customerFavoriteCompanyId;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
            }
            catch
            {

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
        /// <summary>
        /// 删除用户等级信息（及对应的与折扣分类的关系信息）
        /// </summary>
        /// <param name="cumtomerRankID"></param>
        public bool DeleteCustomerRankByID(int customerRankID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {

                SqlTransaction tran = null;
                int result = 0;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("update CustomerRank set CustomerRankStatus = '-1' where CustomerRankID=@CustomerRankID;");//用户等级表
                    strSql.Append("update RankConnDiscount set RankConnDisStatus = '-1' where CustomerRankID=@CustomerRankID;");//用户等级与折扣分类关系表

                    SqlParameter[] parameters = {					
					new SqlParameter("@CustomerRankID", SqlDbType.Int,4)};
                    parameters[0].Value = customerRankID;

                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                }
                if (result > 0)
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
        /// 删除用户等级与折扣分类关系表信息
        /// </summary>
        /// <param name="rankConnDisID"></param>
        public bool DeleteRankConnDisByID(int rankConnDisID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {

                SqlTransaction tran = null;
                int result = 0;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("update RankConnDiscount set RankConnDisStatus = '-1' where RankConnDisID=@RankConnDisID;");

                    SqlParameter[] parameters = {					
					new SqlParameter("@RankConnDisID", SqlDbType.Int,4)};
                    parameters[0].Value = rankConnDisID;

                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                }
                if (result > 0)
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
        /// new根据用户编号和设备编号更新用户与设备关系表时间20140313
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public bool UpdateCustomerConnDeviceTime(long customerId, long deviceId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CustomerConnDevice set ");
            strSql.Append("updateTime=@updateTime");
            strSql.Append(" where customerId=@customerId and deviceId=@deviceId");
            SqlParameter[] parameters = {                   
                    new SqlParameter("@updateTime", SqlDbType.DateTime),
                    new SqlParameter("@customerId", SqlDbType.BigInt,8),
					new SqlParameter("@deviceId", SqlDbType.BigInt,8)};

            parameters[0].Value = System.DateTime.Now;
            parameters[1].Value = customerId;
            parameters[2].Value = deviceId;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                if (result > 0)
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
        /// new根据uuid更新pushToken和时间20140313
        /// </summary>
        /// <param name="uuid"></param>
        /// <param name="pushToken"></param>
        /// <returns></returns>
        public bool UpdateDeviceToken(string uuid, string pushToken, string appBuild)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                const string strSql = @"update DeviceInfo set pushToken=@pushToken,updateTime=@updateTime,appBuild=@appBuild where uuid=@uuid";
                SqlParameter[] parameters = new SqlParameter[]{
                    new SqlParameter("@pushToken", SqlDbType.NVarChar,100),
                    new SqlParameter("@updateTime", SqlDbType.DateTime),
                    new SqlParameter("@appBuild", SqlDbType.NVarChar, 50),
                    new SqlParameter("@uuid",SqlDbType.NVarChar,100)};
                parameters[0].Value = pushToken;
                parameters[1].Value = System.DateTime.Now;
                parameters[2].Value = appBuild;
                parameters[3].Value = uuid;
                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                return result > 0;
            }
        }
        /// <summary>
        /// 修改客户的基本信息
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool UpdateCustomerBaseInfo(CustomerInfo customer)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CustomerInfo set ");
                    strSql.Append("UserName=@UserName,");
                    strSql.Append("CustomerSex=@CustomerSex,");
                    strSql.Append("customerEmail=@customerEmail,");
                    strSql.Append("CustomerBirthday=@CustomerBirthday,");
                    strSql.Append("CustomerAddress=@CustomerAddress");
                    strSql.Append(" where CustomerID=@CustomerID ");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
					new SqlParameter("@CustomerSex", SqlDbType.Int,4),
                    new SqlParameter("@customerEmail", SqlDbType.NVarChar,500),
                    new SqlParameter("@CustomerBirthday", SqlDbType.DateTime),
                    new SqlParameter("@CustomerAddress", SqlDbType.NVarChar,500),
                    new SqlParameter("@CustomerID", SqlDbType.BigInt,8)
                    };
                    parameters[0].Value = customer.UserName;
                    parameters[1].Value = customer.CustomerSex;
                    parameters[2].Value = customer.customerEmail;
                    parameters[3].Value = customer.CustomerBirthday;
                    parameters[4].Value = customer.CustomerAddress;
                    parameters[5].Value = customer.CustomerID;
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
        /// new修改用户基本信息
        /// 根据cookie修改邮箱
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool UpdateCustomer(CustomerInfo customer)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CustomerInfo set ");
                    strSql.Append("UserName=@UserName,");
                    strSql.Append("titleName=@titleName,");
                    //strSql.Append("localAlarm=@localAlarm,");
                    //strSql.Append("localAlarmMinute=@localAlarmMinute,");
                    //strSql.Append("localAlarmHour=@localAlarmHour,");
                    strSql.Append("customerEmail=@customerEmail");
                    strSql.Append(" where cookie=@cookie ");
                    SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,100),
                    new SqlParameter("@titleName", SqlDbType.NVarChar,100),
                    new SqlParameter("@customerEmail", SqlDbType.NVarChar,500),
					new SqlParameter("@cookie", SqlDbType.NVarChar,100)};

                    parameters[0].Value = customer.UserName;
                    parameters[1].Value = customer.titleName;
                    parameters[2].Value = customer.customerEmail;
                    parameters[3].Value = customer.cookie;

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
        /// 修改用户基本信息new20140313
        /// 20140126 xiaoyu
        /// modify by wangc 20140512 增加defaultPayment修改
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool UpdateCustomerInfo(CustomerInfo customer)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                const string strSql = @"update CustomerInfo set UserName=@UserName,CustomerSex=@CustomerSex,personalImgInfo=@personalImgInfo,defaultPayment=@defaultPayment where cookie=@cookie";
                SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,100),
                    new SqlParameter("@CustomerSex", SqlDbType.Int,4),
                    new SqlParameter("@personalImgInfo", SqlDbType.NVarChar),
                    new SqlParameter("@defaultPayment",SqlDbType.Int,4),
					new SqlParameter("@cookie", SqlDbType.NVarChar,100)};
                parameters[0].Value = customer.UserName;
                parameters[1].Value = customer.CustomerSex;
                parameters[2].Value = customer.personalImgInfo;
                parameters[3].Value = customer.defaultPayment;
                parameters[4].Value = customer.cookie;
                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                return result > 0;
            }
        }
        /// <summary>
        /// （悠先点菜）修改用户个人信息接口不修改用户个人头像
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool ClientUpdateCustomerInfo(CustomerInfo customer)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                const string strSql = @"update CustomerInfo set UserName=@UserName,CustomerSex=@CustomerSex,defaultPayment=@defaultPayment where cookie=@cookie";
                SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,100),
                    new SqlParameter("@CustomerSex", SqlDbType.Int,4),
                    new SqlParameter("@defaultPayment",SqlDbType.Int,4),
					new SqlParameter("@cookie", SqlDbType.NVarChar,100)};

                parameters[0].Value = customer.UserName;
                parameters[1].Value = customer.CustomerSex;
                parameters[2].Value = customer.defaultPayment;
                parameters[3].Value = customer.cookie;
                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                return result > 0;
            }
        }
        /// <summary>
        /// （悠先点菜）修改用户个人用户名和性别
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool ClientUpdateCustomerInfo1(CustomerInfo customer)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                const string strSql = @"update CustomerInfo set UserName=@UserName,CustomerSex=@CustomerSex where cookie=@cookie";
                SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,100),
                    new SqlParameter("@CustomerSex", SqlDbType.Int,4),
					new SqlParameter("@cookie", SqlDbType.NVarChar,100)};

                parameters[0].Value = customer.UserName;
                parameters[1].Value = customer.CustomerSex;
                parameters[2].Value = customer.cookie;
                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                return result > 0;
            }
        }
        /// <summary>
        /// 修改用户基本信息new20140313
        /// 20140126 xiaoyu
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool UpdateCustomerInfoByMobile(CustomerInfo customer)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CustomerInfo set ");
                    strSql.Append("UserName=@UserName,");
                    strSql.Append("CustomerSex=@CustomerSex,");
                    strSql.Append("personalImgInfo=@personalImgInfo");
                    strSql.Append(" where mobilePhoneNumber=@mobilePhoneNumber ");
                    SqlParameter[] parameters = {
                    new SqlParameter("@UserName", SqlDbType.NVarChar,100),
                    new SqlParameter("@CustomerSex", SqlDbType.Int,4),
                    new SqlParameter("@personalImgInfo", SqlDbType.NVarChar),
					new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar,50)};

                    parameters[0].Value = customer.UserName;
                    parameters[1].Value = customer.CustomerSex;
                    parameters[2].Value = customer.personalImgInfo;
                    parameters[3].Value = customer.mobilePhoneNumber;
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
        /// 根据cookie修改用户短信验证码和验证码时间20140313
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="verificationCode"></param>
        /// <param name="updateVerificationCodeTime"></param>
        /// <param name="isVCSendByVoice"></param>
        /// <returns></returns>
        public bool UpdateCustomerVerificationCode(string cookie, string verificationCode, string verificationCodeMobile, bool updateVerificationCodeTime, bool isVCSendByVoice, bool clearErrorCount)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    if (updateVerificationCodeTime)
                    {
                        strSql.Append("update CustomerInfo set ");
                        strSql.Append("verificationCode=@verificationCode,");
                        strSql.Append("verificationCodeTime=@verificationCodeTime,");
                        strSql.Append("isVCSendByVoice=@isVCSendByVoice,");
                        strSql.Append("verificationCodeMobile=@verificationCodeMobile");
                        if (clearErrorCount)
                        {
                            strSql.Append(",verificationCodeErrCnt=0");
                        }
                        strSql.Append(" where cookie=@cookie ");
                        SqlParameter[] parameters = {                
                        new SqlParameter("@verificationCode", SqlDbType.NVarChar,50),
                        new SqlParameter("@verificationCodeTime", SqlDbType.DateTime),
                        new SqlParameter("@isVCSendByVoice", SqlDbType.Bit),
                        new SqlParameter("@verificationCodeMobile", SqlDbType.NVarChar,50),
					    new SqlParameter("@cookie", SqlDbType.NVarChar,100)};

                        parameters[0].Value = verificationCode;
                        parameters[1].Value = System.DateTime.Now;
                        parameters[2].Value = isVCSendByVoice;
                        parameters[3].Value = verificationCodeMobile;
                        parameters[4].Value = cookie;

                        result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                    }
                    else
                    {
                        if (isVCSendByVoice)
                        {
                            strSql.Append("update CustomerInfo set ");
                            strSql.Append("isVCSendByVoice=@isVCSendByVoice");

                            if (clearErrorCount)
                            {
                                strSql.Append(",verificationCodeErrCnt=0");
                            }
                            strSql.Append(" where cookie=@cookie ");
                            SqlParameter[] parameters = {
                        new SqlParameter("@isVCSendByVoice", SqlDbType.Bit),
					    new SqlParameter("@cookie", SqlDbType.NVarChar,100)};

                            parameters[0].Value = isVCSendByVoice;
                            parameters[1].Value = cookie;

                            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                        }
                        else
                        {
                            result = 1;
                        }
                    }
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
        /// 根据手机号码修改用户短信验证码和验证码时间20140313
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="verificationCode"></param>
        /// <returns></returns>
        public bool UpdateCustomerVerificationCodeByMobilephoneNumber(string mobilePhoneNumber, string verificationCode, bool updateVerificationCodeTime, bool isVCSendByVoice, bool clearErrorCount)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    if (updateVerificationCodeTime)
                    {
                        strSql.Append("update CustomerInfo set ");
                        strSql.Append("verificationCode=@verificationCode,");
                        strSql.Append("verificationCodeTime=@verificationCodeTime,");
                        strSql.Append("isVCSendByVoice=@isVCSendByVoice");
                        if (clearErrorCount)
                        {
                            strSql.Append(",verificationCodeErrCnt=0");
                        }
                        strSql.Append(" where mobilePhoneNumber=@mobilePhoneNumber ");
                        SqlParameter[] parameters = {                   
                        new SqlParameter("@verificationCode", SqlDbType.NVarChar,50),
                        new SqlParameter("@verificationCodeTime", SqlDbType.DateTime),
                        new SqlParameter("@isVCSendByVoice", SqlDbType.Bit),
					    new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar,50)};

                        parameters[0].Value = verificationCode;
                        parameters[1].Value = System.DateTime.Now;
                        parameters[2].Value = isVCSendByVoice;
                        parameters[3].Value = mobilePhoneNumber;

                        result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                    }
                    else
                    {
                        if (clearErrorCount)
                        {
                            strSql.Append("update CustomerInfo set verificationCodeErrCnt=0");
                            strSql.Append(" where mobilePhoneNumber=@mobilePhoneNumber ");
                            SqlParameter[] parameters = {        
					    new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar,50)};
                            parameters[0].Value = mobilePhoneNumber;
                            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                        }
                        if (isVCSendByVoice)
                        {
                            strSql.Append("update CustomerInfo set ");
                            strSql.Append("isVCSendByVoice=@isVCSendByVoice");
                            if (clearErrorCount)
                            {
                                strSql.Append(",verificationCodeErrCnt=0");
                            }
                            strSql.Append(" where mobilePhoneNumber=@mobilePhoneNumber ");
                            SqlParameter[] parameters = {
                        new SqlParameter("@isVCSendByVoice", SqlDbType.Bit),
					    new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar,50)};

                            parameters[0].Value = isVCSendByVoice;
                            parameters[1].Value = mobilePhoneNumber;

                            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                        }
                        else
                        {
                            result = 1;
                        }
                    }
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

        //public bool UpdateCustomerEmail(string cookie, string email)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update CustomerInfo set ");
        //            strSql.Append("verificationCode=@verificationCode,");
        //            strSql.Append("verificationCodeTime=@verificationCodeTime");
        //            strSql.Append(" where cookie=@cookie ");
        //            SqlParameter[] parameters = {                   
        //            new SqlParameter("@verificationCode", SqlDbType.NVarChar,50),
        //            new SqlParameter("@verificationCodeTime", SqlDbType.DateTime),
        //            new SqlParameter("@cookie", SqlDbType.NVarChar,100)};

        //            parameters[0].Value = email;
        //            parameters[1].Value = System.DateTime.Now;
        //            parameters[2].Value = cookie;

        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        /// <summary>
        /// 根据用户忘记密码的验证码修改对应的重置密码时间以及该验证码状态
        /// </summary>
        /// <param name="verifyCode"></param>
        /// <returns></returns>
        public bool UpdateCustomerForgetPassword(string verifyCode)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CustomerForgetPassword set ");
                    strSql.Append("resetPasswordTime=@resetPasswordTime,");
                    strSql.Append("status=@status");
                    strSql.Append(" where verifyCode=@verifyCode");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@resetPasswordTime", SqlDbType.DateTime),
					new SqlParameter("@status", SqlDbType.Int,4),
                    new SqlParameter("@verifyCode", SqlDbType.NVarChar,500)};

                    parameters[0].Value = System.DateTime.Now;
                    parameters[1].Value = -1;
                    parameters[2].Value = verifyCode;

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
        ///// <summary>
        ///// 根据手机号码修改用户验证码和验证码时间
        ///// </summary>
        ///// <param name="mobilePhoneNumber"></param>
        ///// <param name="verificationCode"></param>
        ///// <returns></returns>
        //public bool UpdateCustomerVerificationCodeByMobilephoneNumber(string mobilePhoneNumber, string verificationCode)
        //{
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int result = 0;
        //        try
        //        {
        //            conn.Open();
        //            StringBuilder strSql = new StringBuilder();
        //            strSql.Append("update CustomerInfo set ");
        //            strSql.Append("verificationCode=@verificationCode,");
        //            strSql.Append("verificationCodeTime=@verificationCodeTime");
        //            strSql.Append(" where mobilePhoneNumber=@mobilePhoneNumber ");
        //            SqlParameter[] parameters = {                   
        //            new SqlParameter("@verificationCode", SqlDbType.NVarChar,50),
        //            new SqlParameter("@verificationCodeTime", SqlDbType.DateTime),
        //            new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar,100)};

        //            parameters[0].Value = verificationCode;
        //            parameters[1].Value = System.DateTime.Now;
        //            parameters[2].Value = mobilePhoneNumber;

        //            result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //        if (result == 1)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        /// <summary>
        /// 根据cookie修改用户手机号码和认证时间20140313
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="mobilephone"></param>
        /// <returns></returns>
        public bool UpdateCustomerMobilephone(string cookie, string mobilePhoneNumber)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CustomerInfo set ");
                    strSql.Append("mobilePhoneNumber=@mobilePhoneNumber,");
                    strSql.Append("verificationTime=@verificationTime");
                    strSql.Append(" where cookie=@cookie and @mobilePhoneNumber not in (Select mobilePhoneNumber from CustomerInfo where mobilePhoneNumber=@mobilePhoneNumber)");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar,50),
                    new SqlParameter("@verificationTime", SqlDbType.DateTime),
					new SqlParameter("@cookie", SqlDbType.NVarChar,100)};

                    parameters[0].Value = mobilePhoneNumber;
                    parameters[1].Value = System.DateTime.Now;
                    parameters[2].Value = cookie;

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
        /// 根据cookie修改用户信息，包括手机号码
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool UpdateCustomerInfoAndphone(CustomerInfo customer)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CustomerInfo set ");
                    strSql.Append("mobilePhoneNumber=@mobilePhoneNumber,");
                    strSql.Append("verificationTime=@verificationTime,");
                    strSql.Append("CustomerSex=@CustomerSex,");
                    strSql.Append("personalImgInfo=@personalImgInfo,");
                    strSql.Append("UserName=@UserName,");
                    strSql.Append("registerCityId=@registerCityId");
                    strSql.Append(" where cookie=@cookie and @mobilePhoneNumber not in (Select mobilePhoneNumber from CustomerInfo where mobilePhoneNumber=@mobilePhoneNumber)");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar,50),
                    new SqlParameter("@verificationTime", SqlDbType.DateTime),
                    new SqlParameter("@CustomerSex", SqlDbType.Int),
                    new SqlParameter("@personalImgInfo", SqlDbType.NVarChar),
                    new SqlParameter("@UserName", SqlDbType.NVarChar),
                    new SqlParameter("@registerCityId", SqlDbType.Int),
					new SqlParameter("@cookie", SqlDbType.NVarChar,100)};

                    parameters[0].Value = customer.mobilePhoneNumber;
                    parameters[1].Value = System.DateTime.Now;
                    parameters[2].Value = customer.CustomerSex;
                    parameters[3].Value = customer.personalImgInfo;
                    parameters[4].Value = customer.UserName;
                    parameters[5].Value = customer.registerCityId;
                    parameters[6].Value = customer.cookie;

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
        /// 按id更新用户登录信息
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public int UpdateCustomerInfoAndphoneOfId(CustomerInfo customer)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CustomerInfo set ");
            strSql.Append("mobilePhoneNumber=@mobilePhoneNumber,");
            strSql.Append("verificationTime=@verificationTime,");
            strSql.Append("CustomerSex=@CustomerSex,");
            strSql.Append("personalImgInfo=@personalImgInfo,");
            strSql.Append("UserName=@UserName,");
            strSql.Append("registerCityId=@registerCityId");
            strSql.Append(" where Customerid=@Customerid ");
            SqlParameter[] parameters = {                   
                    new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar,50),
                    new SqlParameter("@verificationTime", SqlDbType.DateTime),
                    new SqlParameter("@CustomerSex", SqlDbType.Int),
                    new SqlParameter("@personalImgInfo", SqlDbType.NVarChar),
                    new SqlParameter("@UserName", SqlDbType.NVarChar),
                    new SqlParameter("@registerCityId", SqlDbType.Int),
					new SqlParameter("@Customerid", SqlDbType.BigInt)};

            parameters[0].Value = customer.mobilePhoneNumber;
            parameters[1].Value = System.DateTime.Now;
            parameters[2].Value = customer.CustomerSex;
            parameters[3].Value = customer.personalImgInfo;
            parameters[4].Value = customer.UserName;
            parameters[5].Value = customer.registerCityId;
            parameters[6].Value = customer.CustomerID;

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据cookie修改用户信息，包括手机号码(不修改用户头像)
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool ClientUpdateCustomerInfoAndphone(CustomerInfo customer)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                conn.Open();
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update CustomerInfo set ");
                strSql.Append("mobilePhoneNumber=@mobilePhoneNumber,");
                strSql.Append("verificationTime=@verificationTime,");
                strSql.Append("CustomerSex=@CustomerSex,");
                strSql.Append("UserName=@UserName");
                strSql.Append(" where cookie=@cookie and @mobilePhoneNumber not in (Select mobilePhoneNumber from CustomerInfo where mobilePhoneNumber=@mobilePhoneNumber)");
                SqlParameter[] parameters = {                   
                    new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar,50),
                    new SqlParameter("@verificationTime", SqlDbType.DateTime),
                    new SqlParameter("@CustomerSex", SqlDbType.Int),
                    new SqlParameter("@UserName", SqlDbType.NVarChar),
					new SqlParameter("@cookie", SqlDbType.NVarChar,100)};
                parameters[0].Value = customer.mobilePhoneNumber;
                parameters[1].Value = System.DateTime.Now;
                parameters[2].Value = customer.CustomerSex;
                parameters[3].Value = customer.UserName;
                parameters[4].Value = customer.cookie;
                result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                return result > 0;
            }
        }
        /// <summary>
        /// 根据cookie修改用户密码
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="passwordMD5"></param>
        /// <returns></returns>
        public bool UpdateCustomerPasswordByCookie(string cookie, string passwordMD5)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CustomerInfo set ");
                    strSql.Append("Password=@Password");
                    strSql.Append(" where cookie=@cookie ");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@Password", SqlDbType.NVarChar,500),
					new SqlParameter("@cookie", SqlDbType.NVarChar,100)};

                    parameters[0].Value = passwordMD5;
                    parameters[1].Value = cookie;

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
        /// new根据用户的OpenIdUid和OpenId类型更新OpenIdSession
        /// </summary>
        /// <param name="openIdUid"></param>
        /// <param name="openIdSession"></param>
        /// <param name="openIdVendor"></param>
        /// <returns></returns>
        public bool UpdateCustomerOpenId(string openIdUid, DateTime expirationDate)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update OpenIdInfo set ");
                    strSql.Append("expirationDate=@expirationDate,openIdUpdateTime=@openIdUpdateTime");
                    strSql.Append(" where openIdUid=@openIdUid");
                    SqlParameter[] parameters = 
                            {
                                new SqlParameter("@expirationDate", SqlDbType.DateTime),
                                new SqlParameter("@openIdUpdateTime", SqlDbType.DateTime),
                                new SqlParameter("@openIdUid", SqlDbType.NVarChar,100)
                            };
                    parameters[0].Value = expirationDate;
                    parameters[1].Value = System.DateTime.Now;
                    parameters[2].Value = openIdUid;

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
        /// 修改用户密码
        /// </summary>
        /// <param name="customerID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UpdateCustomerPassword(long customerID, string password)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("update CustomerInfo set ");
                    strSql.Append("Password=@Password");
                    strSql.Append(" where CustomerID=@CustomerID");
                    parameters = new SqlParameter[]{
                    new SqlParameter("@Password", SqlDbType.NVarChar,500),
                    new SqlParameter("@CustomerID",SqlDbType.BigInt,8)};
                    parameters[0].Value = password;
                    parameters[1].Value = customerID;

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
        /// 修改用户Cookie
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public bool UpdateCustomerCookie(long customerId, string cookie)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("update CustomerInfo set ");
                    strSql.Append("cookie=@cookie");
                    strSql.Append(" where CustomerID=@CustomerID");
                    parameters = new SqlParameter[]{
                    new SqlParameter("@cookie", SqlDbType.NVarChar,500),
                    new SqlParameter("@CustomerID",SqlDbType.BigInt,8)};
                    parameters[0].Value = cookie;
                    parameters[1].Value = customerId;

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
        /// 修改用户等级信息
        /// </summary>
        /// <param name="cumtomerRank"></param>
        public bool UpdateCustomerRank(CustomerRank cumtomerRank)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update CustomerRank set ");
            strSql.Append("CustomerRankName=@CustomerRankName,");
            strSql.Append("CustomerRankSequence=@CustomerRankSequence,");
            strSql.Append("CustomerRankStatus=@CustomerRankStatus");
            strSql.Append(" where CustomerRankID=@CustomerRankID ");
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerRankName", SqlDbType.NVarChar,50),
                    new SqlParameter("@CustomerRankSequence", SqlDbType.Int,4),
					new SqlParameter("@CustomerRankStatus", SqlDbType.Int,4),
                    new SqlParameter("@CustomerRankID",SqlDbType.Int,4)};
            parameters[0].Value = cumtomerRank.CustomerRankName;
            parameters[1].Value = cumtomerRank.CustomerRankSequence;
            parameters[2].Value = cumtomerRank.CustomerRankStatus;
            parameters[3].Value = cumtomerRank.CustomerRankID;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改用户等级与折扣分类关系表信息
        /// </summary>
        /// <param name="rankConnDis"></param>
        public bool UpdateRankConnDis(RankConnDiscount rankConnDis)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update RankConnDiscount set ");
            strSql.Append("CustomerRankID=@CustomerRankID,");
            strSql.Append("DiscountTypeID=@DiscountTypeID,");
            strSql.Append("DiscountValue=@DiscountValue,");
            strSql.Append("RankConnDisStatus=@RankConnDisStatus");
            strSql.Append(" where RankConnDisID=@RankConnDisID ");
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerRankID", SqlDbType.NVarChar,50),
                    new SqlParameter("@DiscountTypeID",SqlDbType.Int,4),
                    new SqlParameter("@DiscountValue",SqlDbType.Float,8),
                    new SqlParameter("@RankConnDisStatus",SqlDbType.Int,4),
                    new SqlParameter("@RankConnDisID",SqlDbType.Int,4)};
            parameters[0].Value = rankConnDis.CustomerRankID;
            parameters[1].Value = rankConnDis.DiscountTypeID;
            parameters[2].Value = rankConnDis.DiscountValue;
            parameters[3].Value = rankConnDis.RankConnDisStatus;
            parameters[4].Value = rankConnDis.RankConnDisID;

            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// new根据用户编号修改登录奖励时间
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public bool UpdateLoginRewardTime(long customerId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CustomerInfo set ");
                    strSql.Append("loginRewardTime=@loginRewardTime");
                    strSql.Append(" where CustomerID=@CustomerID ");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@loginRewardTime", SqlDbType.DateTime),
					new SqlParameter("@CustomerID", SqlDbType.BigInt,8)};

                    parameters[0].Value = System.DateTime.Now;
                    parameters[1].Value = customerId;

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
        /// new根据用户编号修改注册奖励时间
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public bool UpdateRegisterRewardTime(long customerId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CustomerInfo set ");
                    strSql.Append("registerRewardTime=@registerRewardTime");
                    strSql.Append(" where CustomerID=@CustomerID ");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@registerRewardTime", SqlDbType.DateTime),
					new SqlParameter("@CustomerID", SqlDbType.BigInt,8)};

                    parameters[0].Value = System.DateTime.Now;
                    parameters[1].Value = customerId;

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
        /// new根据用户编号修改验证手机奖励时间
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public bool UpdateVerifyMobileRewardTime(long customerId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CustomerInfo set ");
                    strSql.Append("verifyMobileRewardTime=@verifyMobileRewardTime");
                    strSql.Append(" where CustomerID=@CustomerID ");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@verifyMobileRewardTime", SqlDbType.DateTime),
					new SqlParameter("@CustomerID", SqlDbType.BigInt,8)};

                    parameters[0].Value = System.DateTime.Now;
                    parameters[1].Value = customerId;

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
        /// new根据用户编号修改用户余额
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="encourageValue"></param>
        /// <returns></returns>
        public bool UpdateMoney19dianRemained(long customerId, double encourageValue)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                const string strSql = @"update CustomerInfo set money19dianRemained = isnull(money19dianRemained, 0) + @encourageValue
 where CustomerID=@CustomerID and (isnull(money19dianRemained, 0) + @encourageValue) >-0.01"; //保证更新后的余额大于零（负的一分钱）
                SqlParameter[] parameters = {                   
                        new SqlParameter("@encourageValue", SqlDbType.Float,8),
					    new SqlParameter("@CustomerID", SqlDbType.BigInt,8)};
                parameters[0].Value = encourageValue;
                parameters[1].Value = customerId;
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters) > 0;
            }
        }
        /// <summary>
        /// new根据用户编号修改用户连续登录天数
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="clearContinuousLoginNumber">是否清零</param>
        /// <returns></returns>
        public bool UpdateCustomerContinuousLoginNumber(long customerId, bool clearContinuousLoginNumber)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CustomerInfo set ");
                    if (clearContinuousLoginNumber)
                    {
                        strSql.Append("continuousLoginNumber = 0");
                    }
                    else
                    {
                        strSql.Append("continuousLoginNumber = isnull(continuousLoginNumber, 0) + 1");
                    }
                    //保证更新后的余额大于零
                    strSql.Append(" where CustomerID=@CustomerID");
                    SqlParameter[] parameters = {
					new SqlParameter("@CustomerID", SqlDbType.BigInt,8)};

                    parameters[0].Value = customerId;

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
        /// new根据用户邀请编号修改邀请记录的奖励时间和金额
        /// </summary>
        /// <param name="inviteRecordId"></param>
        /// <param name="inviteRewardValue"></param>
        /// <returns></returns>
        public bool UpdateInviteRewardTimeAndValue(long inviteRecordId, double inviteRewardValue)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CustomerInviteRecord set ");
                    strSql.Append("inviteRewardTime=@inviteRewardTime,inviteRewardValue=@inviteRewardValue");
                    strSql.Append(" where id=@id");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@inviteRewardTime", SqlDbType.DateTime),
                    new SqlParameter("@inviteRewardValue", SqlDbType.Float),
					new SqlParameter("@id", SqlDbType.BigInt,8)};

                    parameters[0].Value = System.DateTime.Now;
                    parameters[1].Value = inviteRewardValue;
                    parameters[2].Value = inviteRecordId;

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
        /// 更新用户充值订单的支付时间并将状态为修改为已支付
        /// </summary>
        /// <param name="customerChargeOrderId"></param>
        /// <returns></returns>
        public bool UpdateCustomerChargeStatus(long customerChargeOrderId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CustomerChargeOrder set ");
                    strSql.Append("status=@status,payTime=@payTime");
                    strSql.Append(" where id=@id ");
                    SqlParameter[] parameters = {
                        new SqlParameter("@status",SqlDbType.TinyInt,2),
                        new SqlParameter("@payTime",SqlDbType.DateTime),
                        new SqlParameter("@id",SqlDbType.BigInt,8)};
                    parameters[0].Value = (int)VACustomerCouponOrderStatus.PAID;
                    parameters[1].Value = System.DateTime.Now;
                    parameters[2].Value = customerChargeOrderId;

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
        /// 更新用户默认支付方式 add by wangc 20140512
        /// </summary>
        /// <param name="defaultPayment"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public bool UpdateCustomerDefaultPayment(int defaultPayment, string cookie)
        {
            const string strSql = @"update CustomerInfo set defaultPayment =@defaultPayment where cookie=@cookie";
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                SqlParameter[] parameters = {
                    new SqlParameter("@defaultPayment", SqlDbType.Int,4),
					new SqlParameter("@cookie", SqlDbType.NVarChar,100)};
                parameters[0].Value = defaultPayment;
                parameters[1].Value = cookie;
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters) > 0;
            }
        }

        /// <summary>
        /// 更新用户的当前城市
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public bool UpdateCustomerCurrentCityId(long customerId, int cityId)
        {
            string strSql = "update CustomerInfo set currentCityId=@cityId where CustomerID=@customerId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@cityId", SqlDbType.Int) { Value = cityId },
            new SqlParameter("@customerId", SqlDbType.BigInt) { Value = customerId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                return i > 0;
            }
        }

        /// <summary>
        /// 更新用户的已生效红包金额及未生效红包金额
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="executedAmount"></param>
        /// <param name="notExecutedAmount"></param>
        /// <returns></returns>
        //public bool UpdateCustomerRedEnvelope(string mobilePhoneNumber, double executedAmount, double notExecutedAmount)
        //{
        //    string strSql = "update CustomerInfo set executedRedEnvelopeAmount=isnull(executedRedEnvelopeAmount,0)+@executedAmount,notExecutedRedEnvelopeAmount=isnull(notExecutedRedEnvelopeAmount,0)+@notExecutedAmount where mobilePhoneNumber = @mobilePhoneNumber";
        //    SqlParameter[] para = new SqlParameter[] { 
        //    new SqlParameter("@executedAmount", SqlDbType.Float) { Value = executedAmount },
        //    new SqlParameter("@notExecutedAmount", SqlDbType.Float) { Value = notExecutedAmount },
        //    new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = mobilePhoneNumber }
        //    };
        //    using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
        //    {
        //        int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
        //        if (i > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}

        /// <summary>
        /// new查询用户信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCustomer()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [CustomerID],[CustomerRankID],[UserName],[Password]");
            strSql.Append(",[RegisterDate],[CustomerFirstName],[CustomerLastName],[CustomerSex]");
            strSql.Append(",[CustomerBirthday],[mobilePhoneNumber],[CustomerAddress],[customerEmail]");
            strSql.Append(",[CustomerStatus],[cookie],[securityQuestion],[securityAnswer],[localAlarm]");
            strSql.Append(",[localAlarmHour],[localAlarmMinute],[receivePushForFavoriteRestaurants]");
            strSql.Append(",[isVIP],[vipExpireDate],[verificationCode],[verificationCodeTime],[verificationTime]");
            strSql.Append(",[money19dianRemained],[loginRewardTime],[registerRewardTime],[verifyMobileRewardTime]");
            strSql.Append(",[continuousLoginNumber],[eCardNumber]");
            strSql.Append(" from CustomerInfo");

            strSql.Append(" where CustomerStatus > 0");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }

        //public DataTable SelectCustomer(long customerId)
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select [CustomerID],[CustomerRankID],[UserName],[Password]");
        //    strSql.Append(",[RegisterDate],[CustomerFirstName],[CustomerLastName],[CustomerSex]");
        //    strSql.Append(",[CustomerBirthday],[mobilePhoneNumber],[CustomerAddress],[customerEmail]");
        //    strSql.Append(",[CustomerStatus],[cookie],[securityQuestion],[securityAnswer],[localAlarm]");
        //    strSql.Append(",[localAlarmHour],[localAlarmMinute],[receivePushForFavoriteRestaurants]");
        //    strSql.Append(",[isVIP],[vipExpireDate],[verificationCode],[verificationCodeTime],[verificationTime]");
        //    strSql.Append(",[money19dianRemained],[loginRewardTime],[registerRewardTime],[verifyMobileRewardTime]");
        //    strSql.Append(",[continuousLoginNumber],[eCardNumber]");
        //    strSql.Append(" from CustomerInfo");

        //    strSql.AppendFormat(" where CustomerStatus > 0 AND CustomerID={0}", customerId);

        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

        //    return ds.Tables[0];
        //}
        /// <summary>
        /// 通过菜均价查询用户信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCustomerByDishAverageAmount(double dishAverageAmount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [CustomerID]");
            strSql.Append(" from CustomerInfo");
            strSql.AppendFormat(" where CustomerStatus > '0' and preOrderTotalQuantity>0 and preOrderTotalQuantity is not null and  preOrderTotalAmount/preOrderTotalQuantity>='{0}'", dishAverageAmount);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 通过回头次数查询用户信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCustomerByBackShopCountAndDate(DateTime strDate, DateTime endDate, int companyId, int returnCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select COUNT(A.preOrder19dianId) as 'returnCount',A.customerId");
            strSql.Append(" FROM [PreOrder19dian] as A");
            strSql.Append(" left join PreOrderQueryInfo as B on B.preorder19dianId = A.preOrder19dianId");
            strSql.AppendFormat(" where B.queryTime between '{0}' and '{1}'", strDate, endDate);
            strSql.AppendFormat(" and A.companyId='{0}'", companyId);
            strSql.AppendFormat(" group by A.customerId HAVING COUNT(A.preOrder19dianId)>='{0}'", returnCount);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// new查询指定用户信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCustomer(long customerId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [CustomerID],[CustomerRankID],[UserName],[Password]");
            strSql.Append(",[RegisterDate],[CustomerFirstName],[CustomerLastName],[CustomerSex]");
            strSql.Append(",[CustomerBirthday],[mobilePhoneNumber],[CustomerAddress],[customerEmail]");
            strSql.Append(",[CustomerStatus],[cookie],[securityQuestion],[securityAnswer],[localAlarm]");
            strSql.Append(",[localAlarmHour],[localAlarmMinute],[receivePushForFavoriteRestaurants]");
            strSql.Append(",[isVIP],[vipExpireDate],[verificationCode],[verificationCodeTime],[verificationTime]");
            strSql.Append(",[money19dianRemained],[loginRewardTime],[registerRewardTime],[verifyMobileRewardTime],[cookie]");
            strSql.Append(",[continuousLoginNumber],[eCardNumber], [preOrderTotalAmount],[preOrderTotalQuantity],[currentPlatformVipGrade]");
            strSql.Append(" from CustomerInfo");
            strSql.Append(" where CustomerID =@customerId and CustomerStatus =1");
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@customerId", SqlDbType.BigInt) { Value = customerId } };

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);

            return ds.Tables[0];
        }
        /// <summary>
        /// new查询指定用户的余额变动信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCustomerMoneyDetail(long customerId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id],[customerId],[changeReason],[changeValue],[changeTime]");
            strSql.Append(" from [Money19dianDetail]");
            strSql.Append(" where [customerId]  =@customerId order by changeTime desc");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@customerId",SqlDbType.BigInt){ Value = customerId }
            };

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);

            return ds.Tables[0];
        }
        /// <summary>
        /// new根据被邀请的手机号码查询对应的没有奖励过的用户邀请信息
        /// 返回datatable中按照邀请时间升序排列
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCustomerInviteRecord(string phoneNumbeInvited)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id],[customerId],[phoneNumberInvite],[phoneNumberInvited],[inviteTime],[inviteRewardTime],[inviteRewardValue]");
            strSql.Append(" from CustomerInviteRecord");
            strSql.AppendFormat(" where [inviteRewardTime] is null and [phoneNumberInvited] = '{0}' order by inviteTime asc", phoneNumbeInvited);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        ///  new根据证书查询用户基本信息
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public DataTable SelectCustomer(string cookie, string uuid)
        {
            const string strSql = @"select A.CustomerID,A.mobilePhoneNumber,A.UserName,A.eCardNumber,A.CustomerSex,A.currentPlatformVipGrade,A.personalImgInfo,
A.money19dianRemained,A.CustomerID,C.appType,C.deviceId,C.pushToken,A.verificationCodeTime,A.preOrderTotalAmount,A.preOrderTotalQuantity,
A.verificationCode,A.isVCSendByVoice,A.defaultPayment,0 executedRedEnvelopeAmount,A.Picture,A.registerDate,A.currentCityId,A.verificationCodeErrCnt from CustomerInfo as A
 inner join CustomerConnDevice as B on B.customerId = A.CustomerID
 inner join DeviceInfo as C on C.deviceId = B.deviceId
 where A.CustomerStatus > 0 and A.cookie = @cookie and C.uuid = @uuid";
            SqlParameter[] parameter = new SqlParameter[]
            {
           new SqlParameter("@cookie",cookie),
           new SqlParameter("@uuid",uuid)
           };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            return ds.Tables[0];
        }
        /// <summary>
        /// new根据手机号码查询用户基本信息20140313
        /// </summary>
        /// <param name="mobilephone"></param>
        /// <returns></returns>
        public DataTable SelectCustomerByMobilephone(string mobilephone)
        {
            const string strSql = @"select CustomerID,CustomerRankID,UserName,Password,isVCSendByVoice,executedRedEnvelopeAmount
,RegisterDate,CustomerFirstName,CustomerLastName,CustomerSex,CustomerBirthday,mobilePhoneNumber,CustomerAddress,customerEmail
,CustomerStatus,cookie,securityQuestion,securityAnswer,localAlarm,localAlarmHour,localAlarmMinute,receivePushForFavoriteRestaurants
,isVIP,vipExpireDate,verificationCode,verificationCodeTime,verificationTime,money19dianRemained,loginRewardTime,registerRewardTime,verifyMobileRewardTime
,continuousLoginNumber,eCardNumber,currentPlatformVipGrade,personalImgInfo,Picture,verificationCodeErrCnt,IsUpdateUserName from CustomerInfo 
where mobilePhoneNumber = @mobilePhoneNumber and CustomerStatus =1";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = mobilephone } };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }
        /// <summary>
        /// 检测用户手机号码是否已注册
        /// </summary>
        /// <param name="mobilephone"></param>
        /// <returns></returns>
        public bool SelectIsExistsCustomerByMobilephone(string mobilephone)
        {
            const string strSql = @"select CustomerID from CustomerInfo where mobilePhoneNumber = @mobilePhoneNumber and CustomerStatus > 0";
            SqlParameter[] para = new SqlParameter[] { new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = mobilephone } };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (dr.Read())
                {
                    return Convert.ToInt64(dr[0]) > 0;
                }
            }
            return false;
        }
        /// <summary>
        /// 根据手机号码查询用户昵称和手机号码
        /// </summary>
        /// <param name="phones"></param>
        /// <returns></returns>
        public List<string[]> SelectCustomerNameAndPhoneByMobilephone(string phones)
        {
            string strSql = String.Format(@"select UserName,mobilePhoneNumber
from CustomerInfo
where mobilePhoneNumber in {0}", phones);
            var list = new List<string[]>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql))
            {
                while (dr.Read())
                {
                    string[] str = new string[2] { "", "" };
                    str[0] = dr["UserName"] == DBNull.Value ? "" : Convert.ToString(dr["UserName"]);
                    str[1] = dr["mobilePhoneNumber"] == DBNull.Value ? "" : Convert.ToString(dr["mobilePhoneNumber"]);
                    list.Add(str);
                }
            }
            return list;
        }
        /// <summary>
        /// new根据手机号码查询用户基本信息20140313
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public DataTable SelectCustomerTableByCookie(string cookie)
        {
            const string strSql = @" select A.[CustomerID],[CustomerRankID],[UserName],[Password]
,[RegisterDate],[CustomerFirstName],[CustomerLastName],[CustomerSex],[CustomerBirthday],[mobilePhoneNumber],[CustomerAddress],[customerEmail]
,[CustomerStatus],[cookie],[securityQuestion],[securityAnswer],[localAlarm],[localAlarmHour],[localAlarmMinute],[receivePushForFavoriteRestaurants]
,[isVIP],[vipExpireDate],[verificationCode],[verificationCodeTime],[verificationTime],[money19dianRemained],[loginRewardTime],[registerRewardTime],[verifyMobileRewardTime]
,[continuousLoginNumber],[eCardNumber],[currentPlatformVipGrade] from CustomerInfo as A where A.CustomerStatus=1 and A.cookie = @cookie";
            SqlParameter parameter = new SqlParameter("@cookie", cookie);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            return ds.Tables[0];
        }
        /// <summary>
        /// new根据uuid查询设备信息20140313
        /// </summary>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public DataTable SelectDevice(string uuid)
        {
            const string strSql = @"select [deviceId],[uuid],[pushToken],[updateTime],[appType],[appBuild],isnull(unlockTime,DATEADD(DAY,-1,GETDATE()))[unlockTime]  from DeviceInfo where uuid = @uuid";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@uuid", uuid) };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            return ds.Tables[0];
        }
        /// <summary>
        /// new根据customerId和deviceId查询用户与设备对应表信息20140313
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public DataTable SelectCustomerConnDevice(long customerId, long deviceId)
        {
            string strSql = @"select [customerDeviceId],[customerId],[deviceId],[updateTime] from CustomerConnDevice
                                 where customerId = @customerId and deviceId =@deviceId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@customerId", SqlDbType.BigInt) { Value = customerId },
            new SqlParameter("@deviceId", SqlDbType.BigInt) { Value = deviceId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);

            return ds.Tables[0];
        }
        /// <summary>
        /// new根据customerId查询用户信息(只包括用户最近登录的设备的信息)
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable SelectCustomerTopDevice(long customerId)
        {
            const string strSql = @"select Top 1 A.CustomerID,pushToken,appType,uuid from CustomerInfo as A
            inner join CustomerConnDevice as B on A.CustomerID = B.customerId inner join DeviceInfo as C on C.deviceId = B.deviceId where A.CustomerID =@customerId order by B.updateTime desc";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@customerId", SqlDbType.BigInt) { Value = customerId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }
        /// <summary>
        /// new查询所有用户的信息(只包括用户最近登录的设备的信息)
        /// 如果不传customerIdList则查询全部的
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAllCustomerTopDevice(List<long> customerIdList = null)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select appType,A.CustomerID,pushToken,eCardNumber,max(B.updateTime)");   //添加eCardNumber 用作android推送用 woody 2013－10－15
            strSql.Append(" from CustomerInfo as A");
            strSql.Append(" inner join CustomerConnDevice as B on A.CustomerID = B.customerId");
            strSql.Append(" inner join DeviceInfo as D on D.deviceId = B.deviceId");
            strSql.Append(" where B.updateTime=(select max(C.updateTime) from CustomerConnDevice as C where C.customerId=B.customerId)");
            strSql.Append("  and pushToken is not null and pushToken !=''");//过滤掉pushToken为空的用户
            if (customerIdList != null)
            {
                if (customerIdList.Count > 0)
                {
                    string abc = string.Empty;
                    for (int i = 0; i < customerIdList.Count; i++)
                    {
                        abc += customerIdList[i].ToString();
                        if (i != customerIdList.Count - 1)
                        {
                            abc += ",";
                        }
                    }
                    strSql.Append(" and A.CustomerID in(" + abc + ")");
                }
            }
            strSql.Append("  group by appType,A.CustomerID,pushToken,eCardNumber order by max(B.updateTime) desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 查询用户等级信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryCustomerRank()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [CustomerRankID],[CustomerRankName],[CustomerRankSequence],[CustomerRankStatus]");
            strSql.Append(" from CustomerRank");
            strSql.Append(" where CustomerRankStatus > '0'");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 查询用户等级与对应的折扣信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryCustomerRankDetail()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CustomerRank.CustomerRankID,CustomerRankName,CustomerRankSequence,CustomerRankStatus,");
            strSql.Append("RankConnDisID,DiscountType.DiscountTypeID,DiscountValue,RankConnDisStatus,DiscountTypeName");
            strSql.Append(" from CustomerRank,RankConnDiscount,DiscountType");
            strSql.Append(" where CustomerRank.CustomerRankID = RankConnDiscount.CustomerRankID");
            strSql.Append(" and DiscountType.DiscountTypeID = RankConnDiscount.DiscountTypeID");
            strSql.Append(" and CustomerRank.CustomerRankStatus > 0 and RankConnDiscount.RankConnDisStatus > 0 and DiscountType.DiscountTypeStatus > 0");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }

        /// <summary>
        /// 查询客人类型信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCustomerType()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [customerTypeId],[customerTypeName]");
            strSql.Append(" from CustomerType");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// new根据OpenId类型和OpenIdUid查询该Uid是否存在
        /// xiaoyu20140319
        /// </summary>
        /// <param name="openIdUid"></param>
        /// <param name="openIdVendor"></param>
        /// <returns></returns>
        public bool IsOpenIdUidExist(string openIdUid, VAOpenIdVendor openIdVendor)
        {
            bool result = false;
            DataSet ds = new DataSet();
            const string strSql = "select [customerId],[openIdUid],[openIdBindTime] from OpenIdInfo as A where A.openIdUid = @openIdUid and A.openIdType =@openIdType";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@openIdUid",SqlDbType.NVarChar,100){ Value = openIdUid },
            new SqlParameter("@openIdType",SqlDbType.Int){ Value = (int)openIdVendor }
            };
            ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);

            if (ds.Tables[0].Rows.Count == 1)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 根据用户编号以及需要查询的类型查询对应的用户优惠券
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="currentTime"></param>
        /// <param name="couponType"></param>
        /// <returns></returns>
        public DataTable SelectCustomerCoupon(long customerId, DateTime currentTime, VAUserCouponType couponType)
        {
            StringBuilder strSql = new StringBuilder();
            DataSet ds = new DataSet();
            strSql.Append("select A.couponID,couponType,couponName,couponDesc,couponRequirementType,");
            strSql.Append("couponDisplayStartTime,couponDisplayEndTime,B.couponValidStartTime,B.couponValidEndTime,");
            strSql.Append("originaQuantity,currentQuantity,originalPrice,currentPrice,discount,discountedAmount,");
            strSql.Append("isVIPOnly,canDownloadOnlyOnce,B.couponDownloadPrice,downloadQuantity,limitedSerial,");
            strSql.Append("customerConnCouponID,verificationCode,status,prompt");
            strSql.Append(" from CouponInfo as A");
            strSql.Append(" inner join CustomerConnCoupon as B on A.couponID = B.couponID");
            strSql.AppendFormat(" where B.customerID = '{0}'", customerId);
            switch (couponType)
            {

                case VAUserCouponType.USER_COUPON_USED:
                    {
                        strSql.Append(" and B.status = '" + (int)VACustomerCouponStatus.USED + "'");
                    }
                    break;
                case VAUserCouponType.USER_COUPON_EXPIRED_UNUSED:
                    {
                        strSql.Append(" and ((B.status = '" + (int)VACustomerCouponStatus.NOT_USED + "'");
                        strSql.AppendFormat(" and B.couponValidEndTime < '{0}')", currentTime);
                        strSql.Append(" or B.status = '" + (int)VACustomerCouponStatus.OVERDUE + "')");
                    }
                    break;
                case VAUserCouponType.USER_COUPON_VALID_AND_UNUSED:
                case VAUserCouponType.USER_COUPON_USED_OK:
                    {
                        strSql.Append(" and B.status = '" + (int)VACustomerCouponStatus.NOT_USED + "'");
                        //strSql.AppendFormat(" and '{0}' between A.couponValidStartTime and A.couponValidEndTime", currentTime);
                        strSql.AppendFormat(" and '{0}' < B.couponValidEndTime and '{1}' >B.couponValidStartTime", currentTime, currentTime);//将未能开始验证的券也返回 xiaoyu 20130727
                    }
                    break;
                case VAUserCouponType.USER_COUPON_USED_UNEFFECT:
                    {
                        strSql.Append(" and B.status = '" + (int)VACustomerCouponStatus.NOT_USED + "'");
                        //strSql.AppendFormat(" and '{0}' between A.couponValidStartTime and A.couponValidEndTime", currentTime);
                        strSql.AppendFormat(" and '{0}' < B.couponValidStartTime ", currentTime);//
                    }
                    break;
                case VAUserCouponType.USER_COUPON_NOTUSED:
                    {
                        strSql.Append(" and B.status = '" + (int)VACustomerCouponStatus.NOT_USED + "'");
                        strSql.AppendFormat(" and '{0}' < B.couponValidEndTime ", currentTime);//
                    }
                    break;
            }
            strSql.Append(" order by B.downloadTime desc");
            ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询所有的已过期且状态为未使用的用户优惠券
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCustomerCouponOverdue()
        {
            StringBuilder strSql = new StringBuilder();
            DataSet ds = new DataSet();
            strSql.Append("select verificationCode,customerConnCouponID");
            strSql.Append(" from CouponInfo as A");
            strSql.Append(" inner join CustomerConnCoupon as B on A.couponID = B.couponID");
            strSql.AppendFormat(" where B.status = '" + (int)VACustomerCouponStatus.NOT_USED + "'");
            strSql.AppendFormat(" and A.couponValidEndTime < '{0}'", System.DateTime.Now);
            strSql.Append(" order by B.downloadTime desc");
            ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// new根据忘记密码的验证码查询对应信息
        /// </summary>
        /// <param name="mobilephone"></param>
        /// <returns></returns>
        public DataTable SelectCustomerForgetPassword(string verifyCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id],[customerId],[sendEmailTime],[resetPasswordTime],[status],[verifyCode]");
            strSql.Append(" from CustomerForgetPassword");
            strSql.AppendFormat(" where status > '0' and verifyCode = '{0}'", verifyCode);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 根据用户编号查询对应收藏的公司信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable SelectCustomerFavoriteCompany(long customerId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DISTINCT(A.[companyId]), A.[id],B.[companyName],B.[companyLogo],B.[companyImagePath],B.[companyDescription],");
            strSql.Append(" B.defaultMenuId,B.prePayCashBackCount,B.prePayVIPCount,B.prePaySendGiftCount,B.acpp,B.preorderCount,B.prepayOrderCount");
            strSql.Append(" from CustomerFavoriteCompany as A");
            strSql.Append(" inner join CompanyInfo as B on A.companyId = B.companyID");
            strSql.AppendFormat(" inner join ShopInfo as C on C.[companyID] = A.[companyId]");
            strSql.AppendFormat(" where B.companyStatus > '0' and A.customerId = '{0}' and C.[isHandle] = '" + (int)VAShopHandleStatus.SHOP_Pass + "'", customerId);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 201401查询当前用户所有收藏门店
        /// created by wangcheng
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public List<int> SelectCustomerFavoriteShop(long customerId)
        {
            List<int> list = new List<int>();
            int val = 0;
            string strsql = @"SELECT [shopId] FROM [CustomerFavoriteCompany] where shopId is not null and  customerId='" + customerId + "'";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, null))
            {
                while (dr.Read())
                {
                    val = dr.GetInt32(0);
                    list.Add(val);
                }
            }
            return list;
        }
        /// <summary>
        /// 查询当前用户平台VIP等级 add by wangc 20140320
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public int SelectCustomerCurrentPlatformVipGrade(long customerId)
        {
            int val = 0;
            string strsql = @"SELECT [currentPlatformVipGrade] FROM [CustomerInfo] where customerId='" + customerId + "'";
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, null))
            {
                if (dr.Read())
                {
                    val = dr.GetInt32(0);
                }
            }
            return val;
        }
        /// <summary>
        /// 查询用户的有效预点单数量20140313
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public int SelectCustomerValidPreorderCount(long customerId)
        {
            StringBuilder strSql = new StringBuilder();
            DataSet ds = new DataSet();
            strSql.Append("select COUNT(preOrder19dianId)");
            strSql.Append(" from PreOrder19dian as A");
            strSql.Append(" where A.[customerId] = @customerId and (A.status = '" + (int)VAPreorderStatus.Initial
                + "' or A.status = '" + (int)VAPreorderStatus.Uploaded
                + "' or A.status = '" + (int)VAPreorderStatus.Prepaid + "')");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@customerId",SqlDbType.BigInt){ Value = customerId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object result = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
                return Convert.ToInt32(result);
            }
        }
        /// <summary>
        /// 根据微信号码查询对应的信息
        /// </summary>
        /// <param name="wechatId"></param>
        /// <returns></returns>
        public DataTable SelectCustomerInfoByWechatId(string wechatId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Top 1 A.CustomerID,pushToken,appType,uuid,cookie,eCardNumber");
            strSql.Append(" from CustomerInfo as A");
            strSql.Append(" inner join CustomerConnDevice as B on A.CustomerID = B.customerId");
            strSql.Append(" inner join DeviceInfo as C on C.deviceId = B.deviceId");
            strSql.AppendFormat(" where A.wechatId = '{0}' order by B.updateTime desc", wechatId);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 判断用户是否收藏了特定的公司
        /// 请将Favorite当动词理解
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public bool IsCustomerFavoriteCompany(long customerId, int companyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id");
            strSql.Append(" from CustomerFavoriteCompany as A");
            strSql.AppendFormat(" where A.customerId = '{0}' and A.companyId='{1}'", customerId, companyId);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            if (ds.Tables[0].Rows.Count == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 查询用户充值订单信息
        /// </summary>
        /// <param name="customerChargeOrderId"></param>
        /// <returns></returns>
        public DataTable SelectCustomerChargeOrder(long customerChargeOrderId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [id],[createTime],[payTime],[status]");
            strSql.Append(",[customerCookie],[customerUUID],[priceSum],[customerId]");
            strSql.Append(",[subjectName]");
            strSql.Append(" from CustomerChargeOrder");
            strSql.AppendFormat(" where status = {0} and id = {1}", (int)VACustomerChargeOrderStatus.NOT_PAID, customerChargeOrderId);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        public void UpdateCustomerCountAmount(long customerId, double preOrderTotalAmount, int preOrderTotalQuantity)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CustomerInfo set ");
                    strSql.Append("preOrderTotalAmount=isnull(preOrderTotalAmount,0)+@preOrderTotalAmount,");
                    strSql.Append("preOrderTotalQuantity=isnull(preOrderTotalQuantity,0)+@preOrderTotalQuantity");
                    strSql.Append(" where customerId=@customerId");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@preOrderTotalAmount", SqlDbType.BigInt),
					new SqlParameter("@preOrderTotalQuantity", SqlDbType.Int,4),
                    new SqlParameter("@customerId", SqlDbType.BigInt)};

                    parameters[0].Value = preOrderTotalAmount;
                    parameters[1].Value = preOrderTotalQuantity;
                    parameters[2].Value = customerId;

                    SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {

                }
            }
        }
        /// <summary>
        /// 查询用户当时间在平台等级和消费次数以及消费总金额（wangcheng）
        /// </summary>
        /// <param name="customerId">用户编号</param>
        /// <returns></returns>
        public DataTable SelectCustomerConsumptionInformation(long customerId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [preOrderTotalAmount] ,[preOrderTotalQuantity] ,[currentPlatformVipGrade]");
            strSql.Append(" from [CustomerInfo]");
            strSql.AppendFormat(" where CustomerID='{0}'", customerId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 更新用户平台消费总金额，消费总次数，vip等级（wangcheng）
        /// </summary>
        /// <param name="preOrderTotalAmount"></param>
        /// <param name="preOrderTotalQuantity"></param>
        /// <param name="currentPlatformVipGrade"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public bool UpdateCustomerPartInfo(double preOrderAmount, int preOrderQuantity, int currentPlatformVipGrade, long customerId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CustomerInfo set ");
                    strSql.Append("preOrderTotalAmount=@preOrderAmount+isnull(preOrderTotalAmount,0),");
                    strSql.Append("preOrderTotalQuantity=@preOrderQuantity+isnull(preOrderTotalQuantity,0),");
                    strSql.Append("currentPlatformVipGrade=@currentPlatformVipGrade");
                    strSql.Append(" where customerId=@customerId ");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@preOrderAmount", SqlDbType.Float),
                    new SqlParameter("@preOrderQuantity", SqlDbType.Int,4),
                    new SqlParameter("@currentPlatformVipGrade", SqlDbType.Int,4),
					new SqlParameter("@customerId", SqlDbType.BigInt,8)};
                    parameters[0].Value = preOrderAmount;
                    parameters[1].Value = preOrderQuantity;
                    parameters[2].Value = currentPlatformVipGrade;
                    parameters[3].Value = customerId;
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
        /// 查询用户的部分信息和平台VIP信息
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public DataTable SelectCustomerPartInfoAndVIPInfo(long customerId)
        {
            const string sqlStr = @"select name,UserName,mobilePhoneNumber,currentPlatformVipGrade,CustomerSex,money19dianRemained,vipImg,
executedRedEnvelopeAmount,notExecutedRedEnvelopeAmount,CustomerInfo.Picture,CustomerInfo.RegisterDate from CustomerInfo
 left join ViewAllocPlatformVipInfo on CustomerInfo.currentPlatformVipGrade=ViewAllocPlatformVipInfo.id  where CustomerID=@CustomerID";
            SqlParameter[] parameter = new SqlParameter[] { new SqlParameter("@CustomerID", customerId) };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlStr, parameter);
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据点单流水号查询客人部分信息（wangcheng 收银宝）
        /// </summary>
        /// <param name="preOrder19dianId">点单流水号</param>
        /// <returns></returns>
        public DataTable SelectPartCustomerInfo(long preOrder19dianId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserName,mobilePhoneNumber,CustomerSex,CustomerInfo.titleName");
            strSql.Append(" from  CustomerInfo inner join PreOrder19dian on PreOrder19dian.customerId=CustomerInfo.CustomerID");
            strSql.Append(" where preOrder19dianId=@preOrder19dianId");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@preOrder19dianId", SqlDbType.BigInt) { Value = preOrder19dianId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询用户的未评价点单数量
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public int SelectCustomerPreorderCountForNotEvaluated(long customerId)
        {
            const string strSql = @"select count(preOrder19dianId) as count from PreOrder19dian
where customerId= @customerId and isEvaluation =0 and status !=@status and isnull(isShopConfirmed,0) = 1
and (isnull(refundMoneyClosedSum,0)=0 or refundMoneyClosedSum != prePaidSum)";
            SqlParameter[] para = new SqlParameter[] 
            { 
                new SqlParameter("@customerId", SqlDbType.BigInt, 8) { Value = customerId },
                new SqlParameter("@status", SqlDbType.Int, 4) { Value = (int)VAPreorderStatus.Deleted }
            };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (dr.Read())
                {
                    return Convert.ToInt32(dr[0]);
                }
            }
            return 0;
        }
        /// <summary>
        /// 根据当前用户Cookie查询当前用户编号（wangcheng 20140312）
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public long SelectCustomerByCookie(string cookie)
        {
            string strSql = "SELECT TOP 1 [CustomerID]   FROM [CustomerInfo] where cookie=@cookie and [CustomerStatus]=1";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@cookie", SqlDbType.NVarChar, 100){ Value = cookie}
            };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                long customerId = 0;
                if (dr.Read())
                {
                    customerId = Convert.ToInt64(dr[0]);
                }
                return customerId;
            }
        }
        /// <summary>
        /// 插入客户端用户反馈信息（wangcheng 20140312）
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="feedbackMsg"></param>
        /// <returns></returns>
        public long InsertCustomerFeedback(long customerId, string feedbackMsg)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into CustomerFeedback(");
            strSql.Append("customerId,feedbackMsg,feedbackTime)");
            strSql.Append(" values (");
            strSql.Append("@customerId,@feedbackMsg,@feedbackTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@customerId", SqlDbType.BigInt,8),
					new SqlParameter("@feedbackMsg", SqlDbType.NVarChar),
                    new SqlParameter("@feedbackTime",SqlDbType.DateTime)};
            parameters[0].Value = customerId;
            parameters[1].Value = feedbackMsg;
            parameters[2].Value = DateTime.Now;
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
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
        /// 查询客户端用户反馈信息（wangcheng 20140312）
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCustomerFeedbackInfo()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select CustomerInfo.UserName,mobilePhoneNumber,CustomerFeedback.feedbackMsg,CustomerFeedback.feedbackTime");
            strSql.AppendFormat(" from CustomerFeedback inner join  CustomerInfo on CustomerFeedback.customerId=CustomerInfo.CustomerID order by CustomerFeedback.feedbackTime desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据当前用户Cookie查询当前用户信息（wangcheng 20140318）
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public DataTable SelectCustomerInfoByCookie(string cookie)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT TOP 1 [CustomerID],[mobilePhoneNumber]  FROM [CustomerInfo]");
            strSql.AppendFormat("   where cookie='{0}' and [CustomerStatus]=1", cookie);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 给悠先点菜用户充值 add by wangc 20140326
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CustomerRecharge(string phoneStr, double amount, string cookie)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CustomerInfo set ");
                    strSql.AppendFormat("money19dianRemained = isnull(money19dianRemained, 0) + {0}", amount);
                    //strSql.AppendFormat(" where mobilePhoneNumber in ({0}) and (isnull(money19dianRemained, 0) + {1}) >-0.01", phoneStr, amount);
                    strSql.AppendFormat(" where (isnull(money19dianRemained, 0) + {0}) >-0.01", amount);
                    if (phoneStr != "")
                    {
                        strSql.AppendFormat(" and mobilePhoneNumber in ({0}) ", phoneStr);
                    }
                    if (cookie != "")
                    {
                        strSql.AppendFormat(" and cookie = '{0}'", cookie);
                    }
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), null);
                }
                catch (Exception)
                {
                    return false;
                }
                if (result > 0)
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
        /// 用户追溯，根据用户手机号码查找用户信息
        /// </summary>
        /// <param name="phoneNumber">电话号码</param>
        /// <returns></returns>
        public DataTable SelectCustomerInfoByPhone(string phoneNumber)
        {
            const string strSql = @"select top 1 c.CustomerID,c.UserName,c.mobilePhoneNumber,c.RegisterDate,
round(c.money19dianRemained,2) money19dianRemained,c.cookie
 from CustomerInfo c where c.CustomerStatus = 1 and c.mobilePhoneNumber = @mobilePhoneNumber";
            SqlParameter parameter = new SqlParameter("@mobilePhoneNumber", phoneNumber);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据用户手机号码查询对应cookie
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public string SelectCustomerCookieByMobile(string mobilePhoneNumber)
        {
            const string strSql = "select cookie from CustomerInfo where mobilePhoneNumber=@mobilePhoneNumber";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = mobilePhoneNumber }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
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

        /// <summary>
        /// 根据用户手机号码查询最后一次登录的设备等相关信息
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <returns></returns>
        public static DataTable SelectCustomerForMeal(string mobilePhoneNumber)
        {
            const string strSql = @"select D.CustomerID,D.uuid,D.appType
from (select ROW_NUMBER() over(partition by A.customerId order by B.updateTime desc) as lev, A.CustomerID,C.uuid,appType
from CustomerInfo as A inner join CustomerConnDevice as B on A.CustomerID = B.customerId
inner join DeviceInfo as C on C.deviceId = B.deviceId
where A.mobilePhoneNumber=@mobilePhoneNumber
and isnull(C.pushToken, '') != '' and ISNULL(C.appType, '') != '') D where D.lev = 1";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mobilePhoneNumber", SqlDbType.NVarChar, 50) { Value = mobilePhoneNumber }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }

        public string SelectVerificationCodeMobile(string cookie)
        {
            const string strSql = "select verificationCodeMobile from CustomerInfo where cookie=@cookie";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@cookie", SqlDbType.NVarChar, 100){ Value = cookie }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
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

        public DataTable SelectCustomerRegisterInfo(string cookie)
        {
            const string strSql = "select customerId,verificationCodeMobile,verificationCodeErrCnt from CustomerInfo where cookie=@cookie";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@cookie", SqlDbType.NVarChar, 100){ Value = cookie }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }

        public string SelectCustomerCookieById(long customerId)
        {
            const string strSql = "select cookie from customerInfo where customerId=@customerId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@customerId", SqlDbType.BigInt) { Value = customerId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
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

        public bool UpdateVerificationCodeErrorCount(long customerId)
        {
            const string strSql = "update CustomerInfo set verificationCodeErrCnt=isnull(verificationCodeErrCnt,0)+1 where CustomerID=@customerId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@customerId", SqlDbType.BigInt) { Value = customerId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
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

        public bool UpdateDeviceUnlockTime(long deviceId, DateTime unlockTime)
        {
            const string strSql = "update DeviceInfo set unlockTime = @unlockTime where deviceId = @deviceId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@unlockTime", SqlDbType.DateTime) { Value = unlockTime },
            new SqlParameter("@deviceId", SqlDbType.BigInt) { Value = deviceId }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
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
        /// 按手机号返回model
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public CustomerInfo GetMobileOfModel(string mobile)
        {
            string sql = "SELECT * FROM CustomerInfo WHERE mobilePhoneNumber=@mobilePhoneNumber AND CustomerStatus=1";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("mobilePhoneNumber", mobile) };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader drea = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString(), parameters))
            {
                if (drea.Read())
                    return SqlHelper.GetEntity<CustomerInfo>(drea);
                return null;
            }
        }

        /// <summary>
        /// 按手机号返回model
        /// </summary>
        /// <param name="long"></param>
        /// <returns></returns>
        public CustomerInfo GetModelOfId(long id)
        {
            string sql = "SELECT * FROM CustomerInfo WHERE CustomerID=@CustomerID AND CustomerStatus=1";
            SqlParameter[] parameters = new SqlParameter[] { new SqlParameter("CustomerID", id) };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader drea = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString(), parameters))
            {
                if (drea.Read())
                    return SqlHelper.GetEntity<CustomerInfo>(drea);
                return null;
            }
        }

        public bool UpdateDeviceUUID(long deviceId, string uuid)
        {
            const string strSql = "update DeviceInfo set uuid=@uuid where deviceId=@deviceId and uuid!=@uuid2";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@uuid", SqlDbType.NVarChar, 100) { Value = uuid },
            new SqlParameter("@deviceId", SqlDbType.BigInt) { Value = deviceId },
            new SqlParameter("@uuid2", SqlDbType.NVarChar, 100) { Value = uuid }
            };

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
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
        /// 微信用户注册更新昵称
        /// </summary>
        /// <param name="customerID">id</param>
        /// <param name="userName">昵称</param>
        /// <param name="customerSex">性别</param>
        /// <param name="personalImgInfo">头像</param>
        /// <returns></returns>
        public int UpdateWeChatUserRegistered(long customerID, string userName, int customerSex, string personalImgInfo)
        {
            string sql = "UPDATE CustomerInfo SET UserName=@UserName,CustomerSex=@CustomerSex,personalImgInfo=@personalImgInfo WHERE CustomerID=@customerID";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = userName },
            new SqlParameter("@CustomerSex", SqlDbType.Int) { Value = customerSex },
            new SqlParameter("@personalImgInfo", SqlDbType.NVarChar) { Value = personalImgInfo },
            new SqlParameter("@customerID", SqlDbType.BigInt) { Value = customerID }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql, para);
        }

        /// <summary>
        /// 微信用户注册更新昵称
        /// </summary>
        /// <param name="customerID">id</param>
        /// <param name="userName">昵称</param>
        /// <param name="customerSex">性别</param>
        /// <param name="personalImgInfo">头像</param>
        /// <param name="picture">新头像</param>
        /// <param name="registerDate">新头像日期</param>
        /// <returns></returns>
        public int UpdateWeChatUserRegistered(long customerID, string userName, int customerSex, string personalImgInfo, int isUpdateUserName, string picture, DateTime? registerDate)
        {
            string sql = "UPDATE CustomerInfo SET UserName=@UserName,CustomerSex=@CustomerSex,personalImgInfo=@personalImgInfo,IsUpdateUserName=@IsUpdateUserName,Picture=@Picture,registerDate=@registerDate WHERE CustomerID=@customerID";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@UserName", SqlDbType.NVarChar) { Value = userName },
            new SqlParameter("@CustomerSex", SqlDbType.Int) { Value = customerSex },
            new SqlParameter("@personalImgInfo", SqlDbType.NVarChar) { Value = personalImgInfo },
            new SqlParameter("@IsUpdateUserName", SqlDbType.Int) { Value = isUpdateUserName },
            new SqlParameter("@Picture", SqlDbType.NVarChar) { Value = picture },
            new SqlParameter("@registerDate", SqlDbType.DateTime) { Value = registerDate },
            new SqlParameter("@customerID", SqlDbType.BigInt) { Value = customerID }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql, para);
        }

        /// <summary>
        /// 按微信id返回
        /// </summary>
        /// <param name="unionId"></param>
        /// <returns></returns>
        public CustomerInfo GetModelOfUnionId(string unionId)
        {
            string sql = "SELECT * FROM CustomerInfo WHERE CustomerID=(SELECT TOP 1 CustomerInfo_CustomerID FROM Tb_WeChatUser WHERE UnionId=@UnionId ORDER BY Id DESC)";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@UnionId", unionId)};
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader drea = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString(), parameters))
            {
                if (drea.Read())
                    return SqlHelper.GetEntity<CustomerInfo>(drea);
                return null;
            }
        }

        /// <summary>
        /// 按cookieId返回
        /// </summary>
        /// <param name="cookieId"></param>
        /// <returns></returns>
        public CustomerInfo GetModelOfCookieId(string cookieId)
        {
            string sql = "SELECT * FROM CustomerInfo WHERE cookie=@cookie";
            SqlParameter[] parameters = new SqlParameter[] { 
                new SqlParameter("@cookie", cookieId)};
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader drea = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString(), parameters))
            {
                if (drea.Read())
                    return SqlHelper.GetEntity<CustomerInfo>(drea);
                return null;
            }
        }

        /// <summary>
        /// 按id更新手机号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public int UpdateMobileOfId(long id, string mobile)
        {
            string sql = "UPDATE CustomerInfo SET mobilePhoneNumber=@mobilePhoneNumber,verificationCodeErrCnt=0,verificationCodeMobile='' WHERE CustomerID=@CustomerID";
            SqlParameter[] parameters = new SqlParameter[] { 
                    new SqlParameter("@mobilePhoneNumber", mobile),
                    new SqlParameter("@CustomerID", id)};
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql, parameters);
        }

        /// <summary>
        /// 判断cookie跟uuid是否存在
        /// </summary>
        /// <returns></returns>
        public int IsExistUuid(string cookie, string uuid)
        {
            string sql = "SELECT COUNT(0) FROM CustomerConnDevice C WHERE C.customerId=(SELECT customerId FROM customerInfo WHERE cookie=@cookie) AND C.deviceId=(SELECT deviceId FROM DeviceInfo WHERE uuid=@uuid)";
            SqlParameter[] parameters = new SqlParameter[] { 
                    new SqlParameter("@cookie", cookie),
                    new SqlParameter("@uuid", uuid)};
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                return Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.Text, sql, parameters));
        }

        /// <summary>
        /// 添加设备号
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public int InsertUuid(string cookie, string uuid, int appType, string appBuild)
        {
            string uuidSql = "SELECT COUNT(0) FROM DeviceInfo WHERE uuid=@uuid";
            SqlParameter[] uuidParameters = new SqlParameter[] { 
                    new SqlParameter("@uuid", uuid)};
            int s = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                s = Convert.ToInt32(SqlHelper.ExecuteScalar(conn, CommandType.Text, uuidSql, uuidParameters));

            if (s == 0)
            {
                uuidSql = "INSERT INTO DeviceInfo(uuid,pushToken,updateTime,appType,appBuild) VALUES(@uuid,'',getdate(),@appType,@appBuild)";
                uuidParameters = new SqlParameter[] { 
                    new SqlParameter("@uuid", uuid),
                    new SqlParameter("@appType", uuid),
                    new SqlParameter("@appBuild", uuid)};
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                    SqlHelper.ExecuteNonQuery(conn, CommandType.Text, uuidSql, uuidParameters);
            }

            string sql = "INSERT INTO CustomerConnDevice(customerId,deviceId,updateTime) VALUES((SELECT customerId FROM customerInfo WHERE cookie=@cookie),(SELECT deviceId FROM DeviceInfo WHERE uuid=@uuid),getdate())";
            SqlParameter[] parameters = new SqlParameter[] { 
                    new SqlParameter("@cookie", cookie),
                    new SqlParameter("@uuid", uuid)};
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql, parameters);
        }

        /// <summary>
        /// 修改更新状态
        /// </summary>
        /// <param name="CustomerID">id</param>
        /// <param name="IsUpdateUserName">状态</param>
        /// <returns></returns>
        public int UpdateIsUpdateUserName(long CustomerID, int IsUpdateUserName)
        {
            string sql = "UPDATE [CustomerInfo] SET IsUpdateUserName=@IsUpdateUserName WHERE CustomerID=@CustomerID AND EXISTS (SELECT 1 FROM Tb_WeChatUser WHERE CustomerInfo_CustomerID=@CustomerID)";
            SqlParameter[] parameters = new SqlParameter[] { 
                    new SqlParameter("@IsUpdateUserName", IsUpdateUserName),
                    new SqlParameter("@CustomerID", CustomerID)};
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql, parameters);
        }

        /// <summary>
        /// 查询用户在指定城市关注的店
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public List<int> SelectCustomerFavoriteShop(long customerId, int cityId)
        {
            const string strSql = @"select isnull(CustomerFavoriteCompany.shopId,0) shopId from CustomerFavoriteCompany
inner join ShopInfo on CustomerFavoriteCompany.shopId=ShopInfo.shopID
where CustomerFavoriteCompany.customerId=@customerId and cityID=@cityId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@customerId", SqlDbType.BigInt) { Value = customerId },
            new SqlParameter("@cityId", SqlDbType.Int) { Value = cityId }
            };
            List<int> shopIds = new List<int>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    shopIds.Add(Convert.ToInt32(sdr[0]));
                }
            }
            return shopIds;
        }

        public DataTable SelectCustomer(string customerIds)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [CustomerID],[CustomerRankID],[UserName],[Password]");
            strSql.Append(",[RegisterDate],[CustomerFirstName],[CustomerLastName],[CustomerSex]");
            strSql.Append(",[CustomerBirthday],[mobilePhoneNumber],[CustomerAddress],[customerEmail]");
            strSql.Append(",[CustomerStatus],[cookie],[securityQuestion],[securityAnswer],[localAlarm]");
            strSql.Append(",[localAlarmHour],[localAlarmMinute],[receivePushForFavoriteRestaurants]");
            strSql.Append(",[isVIP],[vipExpireDate],[verificationCode],[verificationCodeTime],[verificationTime]");
            strSql.Append(",[money19dianRemained],[loginRewardTime],[registerRewardTime],[verifyMobileRewardTime]");
            strSql.Append(",[continuousLoginNumber],[eCardNumber], [preOrderTotalAmount],[preOrderTotalQuantity],[currentPlatformVipGrade]");
            strSql.Append(" from CustomerInfo");
            strSql.Append(" where CustomerID  in ({0}) and CustomerStatus =1");
            string sql = string.Format(strSql.ToString(), customerIds);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, null);

            return ds.Tables[0];
        }

        /// <summary>
        /// 修改用户在平台的消费次数和消费金额
        /// </summary>
        /// <param name="preOrderAmount">金额</param>
        /// <param name="preOrderQuantity">数量</param>
        /// <param name="customerId">客户ID</param>
        /// <returns></returns>
        public bool UpdateCustomerPartInfo(double preOrderAmount, int preOrderQuantity, long customerId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update CustomerInfo set ");
                    strSql.Append("preOrderTotalAmount=@preOrderAmount+isnull(preOrderTotalAmount,0),");
                    strSql.Append("preOrderTotalQuantity=@preOrderQuantity+isnull(preOrderTotalQuantity,0) ");
                    strSql.Append(" where customerId=@customerId ");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@preOrderAmount", SqlDbType.Float),
                    new SqlParameter("@preOrderQuantity", SqlDbType.Int,4),
					new SqlParameter("@customerId", SqlDbType.BigInt,8)};
                    parameters[0].Value = preOrderAmount;
                    parameters[1].Value = preOrderQuantity;
                    parameters[2].Value = customerId;
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

    }
}
