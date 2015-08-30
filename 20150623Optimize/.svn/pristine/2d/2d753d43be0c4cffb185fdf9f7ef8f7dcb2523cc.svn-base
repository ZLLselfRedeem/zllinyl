using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
//
//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by TDQ on 2012-04-10.
//
namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 权限数据库操作类
    /// </summary>
    public class AuthorityManager
    {
        /// <summary>
        /// 存储过程新增服务员用户信息 add by wangc 20142417
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public int InsertEmployeeBySp(EmployeeInfo employee)
        {
            using (SqlConnection connection = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                connection.Open();
                SqlCommand comm = new SqlCommand();
                comm.Connection = connection;
                comm.CommandType = CommandType.StoredProcedure;
                comm.CommandText = "yxfw_Insert_Employee";
                comm.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = employee.UserName;
                comm.Parameters.Add("@cookie", SqlDbType.NVarChar).Value = employee.cookie;
                comm.Parameters.Add("@EmployeeStatus", SqlDbType.Int).Value = employee.EmployeeStatus;
                comm.Parameters.Add("@EmployeePhone", SqlDbType.NVarChar).Value = employee.EmployeePhone;
                comm.Parameters.Add("@EmployeeAge", SqlDbType.Int).Value = employee.EmployeeAge;
                comm.Parameters.Add("@EmployeeSequence", SqlDbType.Int).Value = employee.EmployeeSequence;
                comm.Parameters.Add("@EmployeeSex", SqlDbType.Int).Value = employee.EmployeeSex;
                comm.Parameters.Add("@Password", SqlDbType.NVarChar).Value = employee.Password;
                comm.Parameters.Add("@isViewAllocWorker", SqlDbType.Bit).Value = employee.isViewAllocWorker;
                comm.Parameters.Add("@registerTime", SqlDbType.DateTime).Value = employee.registerTime;
                comm.Parameters.Add("@isSupportLoginBgSYS", SqlDbType.Bit).Value = employee.isSupportLoginBgSYS;
                comm.Parameters.Add("@EmployeeFirstName", SqlDbType.NVarChar).Value = employee.EmployeeFirstName;
                comm.Parameters.Add("@position", SqlDbType.NVarChar, 100).Value = employee.position;
                comm.Parameters.Add("@defaultPage", SqlDbType.NVarChar, 500).Value = employee.defaultPage;
                comm.Parameters.Add("@rtn", SqlDbType.Int).Value = 0;
                int result = comm.ExecuteNonQuery();
                // int sdsds = Convert.ToInt32(comm.Parameters["@rtn"].Value);
                comm.Parameters.Clear();
                return result;
            }
        }
        /// <summary>
        /// 删除员工信息
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public bool DeleteEmployee(int employeeID)
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

                    strSql.Append("update EmployeeInfo set EmployeeStatus = '-1' where EmployeeID=@EmployeeID;");

                    SqlParameter[] parameters = {					
					new SqlParameter("@EmployeeID", SqlDbType.Int,4)};
                    parameters[0].Value = employeeID;

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
        /// 修改员工信息
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool UpdateEmployee(EmployeeInfo employee)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("update EmployeeInfo set ");
                    strSql.Append("EmployeeFirstName=@EmployeeFirstName,");//EmployeeLastName=@EmployeeLastName,
                    strSql.Append("EmployeeSex=@EmployeeSex,EmployeeAge=@EmployeeAge,EmployeePhone=@EmployeePhone,");
                    strSql.Append("EmployeeSequence=@EmployeeSequence,EmployeeStatus=@EmployeeStatus,");
                    //strSql.Append(" removeChangeMaxValue=@removeChangeMaxValue,canQuickCheckout=@canQuickCheckout,canClearTable=@canClearTable,canWeigh=@canWeigh,");
                    strSql.Append(" position=@position,defaultPage=@defaultPage,");
                    strSql.Append("isViewAllocWorker=@isViewAllocWorker,birthday=@birthday,remark=@remark,");
                    strSql.Append("isSupportLoginBgSYS=@isSupportLoginBgSYS");
                    strSql.Append(" where EmployeeID=@EmployeeID");
                    parameters = new SqlParameter[]{
                    new SqlParameter("@EmployeeFirstName", SqlDbType.NVarChar,50),
                    //new SqlParameter("@EmployeeLastName", SqlDbType.NVarChar,50),//2014-2-23 取消LastName
                    new SqlParameter("@EmployeeSex", SqlDbType.Int,4),
                    new SqlParameter("@EmployeeAge", SqlDbType.Int,4),
                    new SqlParameter("@EmployeePhone", SqlDbType.NVarChar,50),
                    new SqlParameter("@EmployeeSequence", SqlDbType.Int,4),
                    new SqlParameter("@EmployeeStatus",SqlDbType.Int,4),
                    //2014-2-23 取消 员工抹零金额最大值，快速结账权限，清台权限，称重权限
                    //new SqlParameter("@removeChangeMaxValue",SqlDbType.Float,8),
                    //new SqlParameter("@canQuickCheckout",SqlDbType.Bit,1),
                    //new SqlParameter("@canClearTable",SqlDbType.Bit,1),
                    //new SqlParameter("@canWeigh",SqlDbType.Bit,1),
                    new SqlParameter("@EmployeeID",SqlDbType.Int,4),
                    new SqlParameter("@position",SqlDbType.NVarChar,100),
                    new SqlParameter("@defaultPage",SqlDbType.NVarChar,500),
                    new SqlParameter("@isViewAllocWorker",SqlDbType.Bit,1),
                    new SqlParameter("@birthday",SqlDbType.DateTime),
                    new SqlParameter("@remark",SqlDbType.NVarChar),
                    new SqlParameter("@isSupportLoginBgSYS",SqlDbType.Bit,1)
                    };
                    parameters[0].Value = employee.EmployeeFirstName;
                    //parameters[1].Value = employee.EmployeeLastName;
                    parameters[1].Value = employee.EmployeeSex;
                    parameters[2].Value = employee.EmployeeAge;
                    parameters[3].Value = employee.EmployeePhone;
                    parameters[4].Value = employee.EmployeeSequence;
                    parameters[5].Value = employee.EmployeeStatus;
                    //parameters[7].Value = employee.removeChangeMaxValue;
                    //parameters[8].Value = employee.canQuickCheckout;
                    //parameters[9].Value = employee.canClearTable;
                    //parameters[10].Value = employee.canWeigh;
                    parameters[6].Value = employee.EmployeeID;
                    parameters[7].Value = employee.position;
                    parameters[8].Value = employee.defaultPage;
                    parameters[9].Value = employee.isViewAllocWorker;
                    parameters[10].Value = employee.birthday;
                    parameters[11].Value = employee.remark;
                    //add by wangc 20140324
                    parameters[12].Value = employee.isSupportLoginBgSYS;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch
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
        /// 修改员工登录密码
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UpdateEmployeePwd(int employeeID, string password)
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
                    SqlParameter[] parameters = null;
                    strSql.Append("update EmployeeInfo set ");
                    strSql.Append("Password=@Password");
                    strSql.Append(" where EmployeeID=@EmployeeID");
                    parameters = new SqlParameter[]{
                    new SqlParameter("@Password", SqlDbType.NVarChar,50),
                    new SqlParameter("@EmployeeID",SqlDbType.Int,4)};
                    parameters[0].Value = password;
                    parameters[1].Value = employeeID;

                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();

                }
                catch (Exception ex)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }

                    throw ex;
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
        /// 查询所有员工信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectEmployee()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [EmployeeID],[UserName],[Password],[EmployeeFirstName],");//[EmployeeLastName],
            strSql.Append("[EmployeeSex],[EmployeeAge],[EmployeePhone],[EmployeeSequence],[EmployeeStatus],[removeChangeMaxValue],");
            strSql.Append("[canQuickCheckout],[canClearTable],[canWeigh],[position],[defaultPage],[cookie],[verificationCode],[verificationCodeTime],[employeeNumber],[isViewAllocWorker]");
            strSql.Append(" ,[settlementPoint],[notSettlementPoint],registerTime,birthday,remark,isSupportLoginBgSYS");
            strSql.Append(" FROM EmployeeInfo where EmployeeInfo.EmployeeStatus > 0");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询员工信息 add by wangc 20140325
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public DataTable SelectEmployee(int employeeId)
        {
            const string strSql = @"select [EmployeeID],[UserName],[Password],[EmployeeFirstName],[EmployeeSex],[EmployeeAge],[EmployeePhone],[EmployeeSequence],[EmployeeStatus],[removeChangeMaxValue],
                                     [canQuickCheckout],[canClearTable],[canWeigh],[position],[defaultPage],[cookie],[verificationCode],[verificationCodeTime],[employeeNumber],[isViewAllocWorker]
                                     ,[settlementPoint],[notSettlementPoint],registerTime,birthday,remark,isSupportLoginBgSYS
                                     FROM EmployeeInfo where EmployeeInfo.EmployeeID =@employeeId";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@employeeId",SqlDbType.Int){ Value = employeeId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据Cookie查询员工信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectEmployeeByCookie(string cookie)
        {
            const string strSql = @"select [EmployeeID],[UserName],[Password],[EmployeeFirstName],[isVCSendByVoice],[EmployeeSex],[EmployeeAge],[EmployeePhone],[EmployeeSequence],[EmployeeStatus],[removeChangeMaxValue],
 [canQuickCheckout],[canClearTable],[canWeigh],[position],[defaultPage],[cookie],[verificationCode],[verificationCodeTime],[employeeNumber],[isViewAllocWorker]
 FROM EmployeeInfo where cookie = @cookie and EmployeeInfo.EmployeeStatus =1 ";
            SqlParameter[] parameter = { new SqlParameter("@cookie", SqlDbType.NVarChar, 100) { Value = cookie } };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据电话号码查询员工信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectEmployeeByMobilephone(string mobilephone, bool flag = false)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [EmployeeID],[UserName],[Password],[EmployeeFirstName],[isVCSendByVoice],");//[EmployeeLastName],
            strSql.Append("[EmployeeSex],[EmployeeAge],[EmployeePhone],[EmployeeSequence],[EmployeeStatus],[removeChangeMaxValue],");
            strSql.Append("[canQuickCheckout],[canClearTable],[canWeigh],[position],[defaultPage],[cookie],[verificationCode],[verificationCodeTime],[employeeNumber],");
            strSql.Append("[settlementPoint],[notSettlementPoint]");
            strSql.Append(" FROM EmployeeInfo where  UserName = @mobilephone");
            if (flag == false)
            {
                strSql.Append(" and EmployeeInfo.EmployeeStatus >0");
            }
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@mobilephone",SqlDbType.NVarChar,50){ Value = mobilephone }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据cookie修改服务员短信验证码和验证码时间
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="verificationCode"></param>
        /// <returns></returns>
        public bool UpdateEmployeeVerificationCode(string cookie, string verificationCode)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update EmployeeInfo set ");
                    strSql.Append("verificationCode=@verificationCode,");
                    strSql.Append("verificationCodeTime=@verificationCodeTime");
                    strSql.Append(" where cookie=@cookie ");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@verificationCode", SqlDbType.NVarChar,50),
                    new SqlParameter("@verificationCodeTime", SqlDbType.DateTime),
					new SqlParameter("@cookie", SqlDbType.NVarChar,100)};

                    parameters[0].Value = verificationCode;
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
        /// 根据手机号码修改用户短信验证码和验证码时间
        /// </summary>
        /// <param name="mobilePhoneNumber"></param>
        /// <param name="verificationCode"></param>
        /// <param name="updateVerificationCodeTime"></param>
        /// <returns></returns>
        public bool UpdateEmployeeVerificationCodeByMobilephoneNumber(string mobilePhoneNumber, string verificationCode, bool updateVerificationCodeTime, bool isVCSendByVoice)
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
                        strSql.Append("update EmployeeInfo set ");
                        strSql.Append("verificationCode=@verificationCode,");
                        strSql.Append("verificationCodeTime=@verificationCodeTime,");
                        strSql.Append("isVCSendByVoice=@isVCSendByVoice");
                        strSql.Append(" where UserName=@UserName and EmployeeStatus>0");
                        SqlParameter[] parameters = {                   
                        new SqlParameter("@verificationCode", SqlDbType.NVarChar,50),
                        new SqlParameter("@verificationCodeTime", SqlDbType.DateTime),
                        new SqlParameter("@isVCSendByVoice", SqlDbType.Bit),
					    new SqlParameter("@UserName", SqlDbType.NVarChar,50)};

                        parameters[0].Value = verificationCode;
                        parameters[1].Value = System.DateTime.Now;
                        parameters[2].Value = isVCSendByVoice;
                        parameters[3].Value = mobilePhoneNumber;

                        result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                    }
                    else
                    {
                        if (isVCSendByVoice)
                        {
                            strSql.Append("update EmployeeInfo set ");
                            strSql.Append("isVCSendByVoice=@isVCSendByVoice");
                            strSql.Append(" where UserName=@UserName  and EmployeeStatus>0");
                            SqlParameter[] parameters =
                            {
                                new SqlParameter("@isVCSendByVoice", SqlDbType.Bit),
                                new SqlParameter("@UserName", SqlDbType.NVarChar, 50)
                            };

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

        /// <summary>
        /// 根据cookies修改用户短信验证码和验证码时间
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="verificationCode"></param>
        /// <param name="updateVerificationCodeTime"></param>
        /// <returns></returns>
        public bool UpdateEmployeeVerificationCode(string cookie, string verificationCode, bool updateVerificationCodeTime)
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
                        strSql.Append("update EmployeeInfo set ");
                        strSql.Append("verificationCode=@verificationCode,");
                        strSql.Append("verificationCodeTime=@verificationCodeTime");
                        strSql.Append(" where cookie=@Cookie and EmployeeStatus>0");
                        SqlParameter[] parameters = {                   
                        new SqlParameter("@verificationCode", SqlDbType.NVarChar,50),
                        new SqlParameter("@verificationCodeTime", SqlDbType.DateTime),
					    new SqlParameter("@Cookie", SqlDbType.NVarChar,100)};

                        parameters[0].Value = verificationCode;
                        parameters[1].Value = System.DateTime.Now;
                        parameters[2].Value = cookie;

                        result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                    }
                    else
                    {
                        strSql.Append("update EmployeeInfo set ");
                        strSql.Append("verificationCode=@verificationCode");
                        strSql.Append(" where cookie=@Cookie  and EmployeeStatus>0");
                        SqlParameter[] parameters = {                   
                        new SqlParameter("@verificationCode", SqlDbType.NVarChar,50),
					    new SqlParameter("@Cookie", SqlDbType.NVarChar,100)};

                        parameters[0].Value = verificationCode;
                        parameters[1].Value = cookie;

                        result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
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
        /// 根据cookie修改服务员的姓名和员工号码
        /// </summary>
        /// <param name="name"></param>
        /// <param name="employeeNumber"></param>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public bool UpdateEmployeeNameAndNumber(string name, string employeeNumber, string cookie)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update EmployeeInfo set ");
                    strSql.Append("EmployeeFirstName=@EmployeeFirstName,");
                    strSql.Append("employeeNumber=@employeeNumber");
                    strSql.Append(" where cookie=@cookie ");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@EmployeeFirstName", SqlDbType.NVarChar,50),
                    new SqlParameter("@employeeNumber", SqlDbType.NVarChar,50),
					new SqlParameter("@cookie", SqlDbType.NVarChar,100)};

                    parameters[0].Value = name;
                    parameters[1].Value = employeeNumber;
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
        /// 根据员工姓名和用户名查询员工信息（wangcheng）
        /// </summary>
        /// <returns></returns>
        public DataTable SelectEmployeeByName(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM EmployeeInfo where EmployeeInfo.EmployeeStatus > '0'");
            if (name != "")
            {
                strSql.AppendFormat(" and ( UserName like '%{0}%' or EmployeeFirstName like '%{0}%')", name);//+EmployeeLastName
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 新增角色信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public int InsertRole(RoleInfo role)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                SqlTransaction tran = null;
                Object obj = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into RoleInfo(");
                    strSql.Append("RoleName,RoleDescription,RoleStatus)");
                    strSql.Append(" values (");
                    strSql.Append("@RoleName,@RoleDescription,@RoleStatus)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					new SqlParameter("@RoleName", SqlDbType.NVarChar,50),
                    new SqlParameter("@RoleDescription", SqlDbType.NVarChar,500),
                    new SqlParameter("@RoleStatus",SqlDbType.Int,4)};
                    parameters[0].Value = role.RoleName;
                    parameters[1].Value = role.RoleDescription;
                    parameters[2].Value = role.RoleStatus;

                    obj = SqlHelper.ExecuteScalar(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    throw ex;
                }
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
        /// 删除角色信息
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public bool DeleteRole(int roleID)
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

                    strSql.Append("update RoleInfo set RoleStatus = '-1' where RoleID=@RoleID;");

                    SqlParameter[] parameters = {					
					new SqlParameter("@RoleID", SqlDbType.Int,4)};
                    parameters[0].Value = roleID;

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
        /// 修改角色信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool UpdateRole(RoleInfo role)
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
                    SqlParameter[] parameters = null;
                    strSql.Append("update RoleInfo set ");
                    strSql.Append("RoleName=@RoleName,RoleDescription=@RoleDescription,");
                    strSql.Append("RoleStatus=@RoleStatus");
                    strSql.Append(" where RoleID=@RoleID");
                    parameters = new SqlParameter[]{
					new SqlParameter("@RoleName", SqlDbType.NVarChar,50),
                    new SqlParameter("@RoleDescription", SqlDbType.NVarChar,500),
                    new SqlParameter("@RoleStatus", SqlDbType.Int,4),
                    new SqlParameter("@RoleID",SqlDbType.Int,4)};
                    parameters[0].Value = role.RoleName;
                    parameters[1].Value = role.RoleDescription;
                    parameters[2].Value = role.RoleStatus;
                    parameters[3].Value = role.RoleID;

                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();

                }
                catch (Exception ex)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }

                    throw ex;
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
        /// 查询所有角色信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectRole()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM RoleInfo where RoleInfo.RoleStatus > '0'");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 新增权限信息
        /// </summary>
        /// <param name="authority"></param>
        /// <returns></returns>
        public int InsertAuthority(AuthorityInfo authority)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                SqlTransaction tran = null;
                Object obj = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into AuthorityInfo(");
                    strSql.Append("AuthorityName,AuthorityURL,AuthorityRank,");
                    strSql.Append("AuthorityDescription,AuthoritySequence,AuthorityStatus,AuthorityType)");
                    strSql.Append(" values (");
                    strSql.Append("@AuthorityName,@AuthorityURL,@AuthorityRank,");
                    strSql.Append("@AuthorityDescription,@AuthoritySequence,@AuthorityStatus,@AuthorityType)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					new SqlParameter("@AuthorityName", SqlDbType.NVarChar,50),
                    new SqlParameter("@AuthorityURL", SqlDbType.NVarChar,500),
                    new SqlParameter("@AuthorityRank", SqlDbType.Int,4),
                    new SqlParameter("@AuthorityDescription", SqlDbType.NVarChar,500),
                    new SqlParameter("@AuthoritySequence", SqlDbType.Int,4),
                    new SqlParameter("@AuthorityStatus", SqlDbType.Int,4),
                    new SqlParameter("@AuthorityType", SqlDbType.NVarChar,50)
                    };
                    parameters[0].Value = authority.AuthorityName;
                    parameters[1].Value = authority.AuthorityURL;
                    parameters[2].Value = authority.AuthorityRank;
                    parameters[3].Value = authority.AuthorityDescription;
                    parameters[4].Value = authority.AuthoritySequence;
                    parameters[5].Value = authority.AuthorityStatus;
                    parameters[6].Value = authority.AuthorityType;

                    obj = SqlHelper.ExecuteScalar(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    throw ex;
                }
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
        /// 删除权限信息
        /// </summary>
        /// <param name="authorityID"></param>
        /// <returns></returns>
        public bool DeleteAuthority(int authorityID)
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

                    strSql.Append("update AuthorityInfo set AuthorityStatus = '-1' where AuthorityID=@AuthorityID;");

                    SqlParameter[] parameters = {					
					new SqlParameter("@AuthorityID", SqlDbType.Int,4)};
                    parameters[0].Value = authorityID;

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
        /// 修改权限信息
        /// </summary>
        /// <param name="authority"></param>
        /// <returns></returns>
        public bool UpdateAuthority(AuthorityInfo authority)
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
                    SqlParameter[] parameters = null;
                    strSql.Append("update AuthorityInfo set ");
                    strSql.Append("AuthorityName=@AuthorityName,AuthorityURL=@AuthorityURL,");
                    strSql.Append("AuthorityRank=@AuthorityRank,AuthorityDescription=@AuthorityDescription,");
                    strSql.Append("AuthoritySequence=@AuthoritySequence,AuthorityStatus=@AuthorityStatus,AuthorityType=@AuthorityType");
                    strSql.Append(" where AuthorityID=@AuthorityID");
                    parameters = new SqlParameter[]{
					new SqlParameter("@AuthorityName", SqlDbType.NVarChar,50),
                    new SqlParameter("@AuthorityURL", SqlDbType.NVarChar,500),
                    new SqlParameter("@AuthorityRank", SqlDbType.Int,4),
                    new SqlParameter("@AuthorityDescription", SqlDbType.NVarChar,500),
                    new SqlParameter("@AuthoritySequence", SqlDbType.Int,4),
                    new SqlParameter("@AuthorityStatus", SqlDbType.Int,4),
                    new SqlParameter("@AuthorityID",SqlDbType.Int,4),
                    new SqlParameter("@AuthorityType",SqlDbType.NVarChar,50)
                    };
                    parameters[0].Value = authority.AuthorityName;
                    parameters[1].Value = authority.AuthorityURL;
                    parameters[2].Value = authority.AuthorityRank;
                    parameters[3].Value = authority.AuthorityDescription;
                    parameters[4].Value = authority.AuthoritySequence;
                    parameters[5].Value = authority.AuthorityStatus;
                    parameters[6].Value = authority.AuthorityID;
                    parameters[7].Value = authority.AuthorityType;
                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();

                }
                catch (Exception ex)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }

                    throw ex;
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
        /// 查询所有权限信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAuthority()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM AuthorityInfo where AuthorityInfo.AuthorityStatus > '0'");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 新增员工与角色关系信息
        /// </summary>
        /// <param name="employeeRole"></param>
        /// <returns></returns>
        public int InsertEmployeeRole(EmployeeRole employeeRole)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                //SqlTransaction tran = null;
                Object obj = null;
                try
                {
                    conn.Open();
                    //tran = conn.BeginTransaction();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into EmployeeRole(");
                    strSql.Append("EmployeeID,RoleID,EmployeeRoleStatus)");
                    strSql.Append(" values (");
                    strSql.Append("@EmployeeID,@RoleID,@EmployeeRoleStatus)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					new SqlParameter("@EmployeeID", SqlDbType.Int,4),
                    new SqlParameter("@RoleID", SqlDbType.Int,4),
                    new SqlParameter("@EmployeeRoleStatus",SqlDbType.Int,4),
                    //new SqlParameter("@ShopId",employeeRole.ShopId), 
                    };
                    parameters[0].Value = employeeRole.EmployeeID;
                    parameters[1].Value = employeeRole.RoleID;
                    parameters[2].Value = employeeRole.EmployeeRoleStatus;
                    //parameters[3].Value = employeeRole.ShopId;

                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);

                    //tran.Commit();
                }
                catch (Exception)
                {
                    //if (tran != null)
                    //{
                    //    tran.Rollback();
                    //}
                    //throw ex;
                    return 0;
                }
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
        /// 删除员工与角色关系信息
        /// </summary>
        /// <param name="employeeRoleID"></param>
        /// <returns></returns>
        public bool DeleteEmployeeRole(int employeeRoleID)
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

                    strSql.Append("update EmployeeRole set EmployeeRoleStatus = '-1' where EmployeeRoleID=@EmployeeRoleID;");

                    SqlParameter[] parameters = {					
					new SqlParameter("@EmployeeRoleID", SqlDbType.Int,4)};
                    parameters[0].Value = employeeRoleID;

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
        /// 当角色删除时，删除所有与该角色有关的信息 add by wangc 20140414
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool DeleteEmployeeRoleByRoleID(int roleId)
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
                    strSql.Append("update EmployeeRole set EmployeeRoleStatus = '-1' where RoleID=@RoleID;");
                    SqlParameter[] parameters = {					
					new SqlParameter("@RoleID", SqlDbType.Int,4)};
                    parameters[0].Value = roleId;
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
                if (result >= 1)
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
        /// 收银宝删除服务员角色
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public bool DeleteEmployeeRole(int employeeID, int roleId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update EmployeeRole set EmployeeRoleStatus = '-1' where EmployeeID=@EmployeeID and RoleID=@RoleID;");
                    SqlParameter[] parameters = {					
					new SqlParameter("@EmployeeID", SqlDbType.Int,4),
                    new SqlParameter("@RoleID", SqlDbType.Int,4)};
                    parameters[0].Value = employeeID;
                    parameters[1].Value = roleId;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch
                {
                    return true;
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
        /// 修改员工与角色关系信息
        /// </summary>
        /// <param name="employeeRole"></param>
        /// <returns></returns>
        public bool UpdateEmployeeRole(EmployeeRole employeeRole)
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
                    SqlParameter[] parameters = null;
                    strSql.Append("update EmployeeRole set ");
                    strSql.Append("EmployeeID=@EmployeeID,RoleID=@RoleID,");
                    strSql.Append("EmployeeRoleStatus=@EmployeeRoleStatus");
                    strSql.Append(" where EmployeeRoleID=@EmployeeRoleID");
                    parameters = new SqlParameter[]{
					new SqlParameter("@EmployeeID", SqlDbType.Int,4),
                    new SqlParameter("@RoleID", SqlDbType.Int,4),
                    new SqlParameter("@EmployeeRoleStatus",SqlDbType.Int,4),
                    new SqlParameter("@EmployeeRoleID",SqlDbType.Int,4)};
                    parameters[0].Value = employeeRole.EmployeeID;
                    parameters[1].Value = employeeRole.RoleID;
                    parameters[2].Value = employeeRole.EmployeeRoleStatus;
                    parameters[3].Value = employeeRole.EmployeeRoleID;

                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();

                }
                catch (Exception ex)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }

                    throw ex;
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
        /// 查询所有员工与角色关系信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectEmployeeRole()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM EmployeeRole INNER JOIN");
            strSql.Append(" EmployeeInfo ON EmployeeRole.EmployeeID = EmployeeInfo.EmPloyeeID INNER JOIN");
            strSql.Append(" RoleInfo ON EmployeeRole.RoleID = RoleInfo.RoleID");
            strSql.Append(" where EmployeeRoleStatus > '0'");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 新增角色与权限关系信息
        /// </summary>
        /// <param name="roleAuthority"></param>
        /// <returns></returns>
        public int InsertRoleAuthority(RoleAuthority roleAuthority)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                SqlTransaction tran = null;
                Object obj = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into RoleAuthority(");
                    strSql.Append("RoleID,AuthorityID,RoleAuthorityStatus)");
                    strSql.Append(" values (");
                    strSql.Append("@RoleID,@AuthorityID,@RoleAuthorityStatus)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					new SqlParameter("@RoleID", SqlDbType.Int,4),
                    new SqlParameter("@AuthorityID", SqlDbType.Int,4),
                    new SqlParameter("@RoleAuthorityStatus",SqlDbType.Int,4)};
                    parameters[0].Value = roleAuthority.RoleID;
                    parameters[1].Value = roleAuthority.AuthorityID;
                    parameters[2].Value = roleAuthority.RoleAuthorityStatus;

                    obj = SqlHelper.ExecuteScalar(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    throw ex;
                }
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
        /// 根据角色编号删除角色与权限关系信息
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public bool DeleteRoleAuthority(int roleID)
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

                    strSql.Append("delete from RoleAuthority where RoleID=@RoleID;");

                    SqlParameter[] parameters = {					
					new SqlParameter("@RoleID", SqlDbType.Int,4)};
                    parameters[0].Value = roleID;

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
                if (result > 1)
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
        /// 修改角色与权限关系信息
        /// </summary>
        /// <param name="roleAuthority"></param>
        /// <returns></returns>
        public bool UpdateRoleAuthority(RoleAuthority roleAuthority)
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
                    SqlParameter[] parameters = null;
                    strSql.Append("update RoleAuthority set ");
                    strSql.Append("RoleID=@RoleID,AuthorityID=@AuthorityID,");
                    strSql.Append("RoleAuthorityStatus=@RoleAuthorityStatus");
                    strSql.Append(" where RoleAuthorityID=@RoleAuthorityID");
                    parameters = new SqlParameter[]{
					new SqlParameter("@RoleID", SqlDbType.Int,4),
                    new SqlParameter("@AuthorityID", SqlDbType.Int,4),
                    new SqlParameter("@RoleAuthorityStatus",SqlDbType.Int,4),
                    new SqlParameter("@RoleAuthorityID",SqlDbType.Int,4)};
                    parameters[0].Value = roleAuthority.RoleID;
                    parameters[1].Value = roleAuthority.AuthorityID;
                    parameters[2].Value = roleAuthority.RoleAuthorityStatus;
                    parameters[3].Value = roleAuthority.RoleAuthorityID;

                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();

                }
                catch (Exception ex)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }

                    throw ex;
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
        /// 查询所有角色与权限关系信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectRoleAuthority()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A.AuthorityID,A.RoleID,RoleAuthorityID,AuthorityName");
            strSql.Append(" FROM RoleAuthority as A INNER JOIN");
            strSql.Append(" RoleInfo as B ON A.RoleID = B.RoleID INNER JOIN");
            strSql.Append(" AuthorityInfo as C ON A.AuthorityID = C.AuthorityID");
            strSql.Append(" where RoleAuthorityStatus > '0'");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 新增员工登录信息
        /// </summary>
        /// <param name="employeeLogin"></param>
        /// <returns></returns>
        public int InsertEmployeeLogin(EmployeeLoginInfo employeeLogin)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                SqlTransaction tran = null;
                Object obj = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into EmployeeLoginInfo(");
                    strSql.Append("LoginTime,UserName,StatusGUID)");
                    strSql.Append(" values (");
                    strSql.Append("@LoginTime,@UserName,@StatusGUID)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					new SqlParameter("@LoginTime", SqlDbType.DateTime),
                    new SqlParameter("@UserName", SqlDbType.NVarChar,50),
                    new SqlParameter("@StatusGUID",SqlDbType.NVarChar,50)};
                    parameters[0].Value = employeeLogin.LoginTime;
                    parameters[1].Value = employeeLogin.UserName;
                    parameters[2].Value = employeeLogin.StatusGUID;

                    obj = SqlHelper.ExecuteScalar(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                    throw ex;
                }
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
        /// 清空员工登录状态
        /// </summary>
        /// <param name="statusGUID"></param>
        /// <returns></returns>
        public bool UpdateLoginStatus(string statusGUID)
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

                    strSql.Append("delete from EmployeeLoginInfo where StatusGUID=@StatusGUID;");

                    SqlParameter[] parameters = {					
					new SqlParameter("@StatusGUID", SqlDbType.NVarChar,50)};
                    parameters[0].Value = statusGUID;

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
                if (result >= 1)
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
        /// 根据登录名和状态码查询登录信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="statusGUID"></param>
        /// <returns></returns>
        public DataTable SelectEmployeeLogin(string userName, string statusGUID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select UserName,StatusGUID");
            strSql.Append(" FROM EmployeeLoginInfo");
            strSql.AppendFormat(" where EmployeeLoginInfo.UserName = '{0}' and EmployeeLoginInfo.StatusGUID = '{1}'", userName, statusGUID);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 根据员工登录名查询对应权限
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public DataTable SelectAuthority(string userName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DISTINCT RoleAuthority.AuthorityID,EmployeeInfo.EmployeeID,");
            strSql.Append("AuthorityInfo.AuthorityName,AuthorityInfo.AuthorityURL,AuthorityInfo.AuthoritySequence,EmployeeInfo.UserName,");
            strSql.Append("AuthorityInfo.AuthorityRank,AuthorityInfo.AuthorityType,EmployeeInfo.EmployeeFirstName,EmployeeInfo.EmployeeLastName");
            strSql.Append(" FROM AuthorityInfo INNER JOIN");
            strSql.Append(" RoleAuthority ON AuthorityInfo.AuthorityID = RoleAuthority.AuthorityID INNER JOIN");
            strSql.Append(" EmployeeInfo INNER JOIN EmployeeRole ON EmployeeInfo.EmployeeID = EmployeeRole.EmployeeID");
            strSql.Append(" ON RoleAuthority.RoleID = EmployeeRole.RoleID");
            strSql.AppendFormat(" where EmployeeInfo.UserName = '{0}' and EmployeeRoleStatus >0 and EmployeeStatus>0", userName);//2011-11-30 xiaoyu 增加EmployeeRoleStatus>0
            strSql.Append(" and AuthorityStatus>0");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 收银宝判断某个用户是否具备某个页面权限 add by wangc 20140515
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="authortiyUrl"></param>
        /// <returns></returns>
        public bool CheckEmployeeAuthortiy(string userName, string authortiyUrl)
        {
            bool result = false;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select DISTINCT RoleAuthority.AuthorityID,AuthorityInfo.AuthorityName,AuthorityInfo.AuthorityURL");
            strSql.Append(" FROM AuthorityInfo INNER JOIN RoleAuthority ON AuthorityInfo.AuthorityID = RoleAuthority.AuthorityID ");
            strSql.Append(" INNER JOIN EmployeeInfo INNER JOIN EmployeeRole ON EmployeeInfo.EmployeeID = EmployeeRole.EmployeeID");
            strSql.Append(" ON RoleAuthority.RoleID = EmployeeRole.RoleID");
            strSql.AppendFormat(" where EmployeeInfo.UserName = '{0}' and EmployeeRoleStatus >0 and EmployeeStatus>0", userName);
            strSql.AppendFormat(" and AuthorityStatus>0 and AuthorityInfo.AuthorityUrl='{0}'", authortiyUrl);
            using (IDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()))
            {
                if (dr.Read())
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary>
        /// 判断用户名是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsEmployeeUserNameExit(string userName)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select *");
                strSql.Append(" FROM EmployeeInfo");
                strSql.AppendFormat(" where EmployeeInfo.UserName = '{0}' and EmployeeInfo.EmployeeStatus > 0", userName);

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
                if (ds.Tables[0].Rows.Count == 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 新增店铺管理员与店铺关系信息
        /// </summary>
        /// <param name="EmployeeConnShop"></param>
        /// <returns></returns>
        public int InsertEmployeeShop(EmployeeConnShop EmployeeConnShop)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into EmployeeConnShop(");
                    strSql.Append("employeeID,shopID,companyId,status,serviceStartTime)");
                    strSql.Append(" values (");
                    strSql.Append("@employeeID,@shopID,@companyId,@status,@serviceStartTime)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					new SqlParameter("@employeeID", SqlDbType.Int,4),
                    new SqlParameter("@shopID", SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.Int,4),
                    new SqlParameter("@status",SqlDbType.Int,4),
                    new SqlParameter ("@serviceStartTime",SqlDbType.DateTime)
                    };
                    parameters[0].Value = EmployeeConnShop.employeeID;
                    parameters[1].Value = EmployeeConnShop.shopID;
                    parameters[2].Value = EmployeeConnShop.companyID;
                    parameters[3].Value = EmployeeConnShop.status;
                    parameters[4].Value = DateTime.Now;
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
                    return Convert.ToInt32(obj);
                }
            }
        }
        /// <summary>
        /// 根据employeeID和shopID将其对应的门店关系数据重置status(wangcheng)
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public int UpdateEmployeeShopStatus(int employeeID, int shopID, int status)
        {
            try
            {
                int result = 0;
                string strSql = "update EmployeeConnShop set status = @status , serviceEndTime=@serviceEndTime where employeeID=@employeeID and  shopID=@shopID";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@status", SqlDbType.Int,4),
                    new SqlParameter("@employeeID", SqlDbType.Int,4),
                    new SqlParameter("@shopID", SqlDbType.Int,4),
                    new SqlParameter ("@serviceEndTime",SqlDbType.DateTime)
                };
                para[0].Value = status;
                para[1].Value = employeeID;
                para[2].Value = shopID;
                para[3].Value = DateTime.Now;//更新服务员服务结束时间
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
                }
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// 根据employeeID将其对应的所有门店关系数据置-1
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public int UpdateEmployeeShopStatus(int employeeID)
        {
            try
            {
                int result = 0;
                string strSql = "update EmployeeConnShop set status = -1,serviceEndTime = @serviceEndTime where employeeID=@employeeID";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@employeeID", SqlDbType.Int,4),
                    new SqlParameter ("@serviceEndTime",SqlDbType.DateTime)
                };
                para[0].Value = employeeID;
                para[1].Value = DateTime.Now;//更新服务员服务结束时间
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
                }
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// 根据employeeID将其对应的所有门店关系数据置-1
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public int UpdateEmployeeShopStatusByemployeeShopID(int employeeShopID)
        {
            try
            {
                int result = 0;
                string strSql = "update EmployeeConnShop set status = -1,serviceEndTime = @serviceEndTime where employeeShopID=@employeeShopID";
                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@employeeShopID", SqlDbType.Int,4),
                    new SqlParameter ("@serviceEndTime",SqlDbType.DateTime)
                };
                para[0].Value = employeeShopID;
                para[1].Value = DateTime.Now;//更新服务员服务结束时间
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
                }
                return result;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// 根据店铺管理员编号删除对应的所有店铺管理员与店铺关系信息
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public bool DeleteEmployeeShop(int employeeID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("delete from EmployeeConnShop where employeeID=@employeeID;");

                    SqlParameter[] parameters = {					
					new SqlParameter("@employeeID", SqlDbType.Int,4)};
                    parameters[0].Value = employeeID;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

                }
                catch (Exception)
                {
                    return false;
                }
                if (result >= 0)
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
        /// 查询店铺管理员与店铺关系信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectEmployeeShop(bool isControlSyb = false)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ShopInfo.shopID,ShopInfo.shopName,ShopInfo.shopImagePath,ShopInfo.cityID,EmployeeConnShop.employeeID,CompanyInfo.companyId,CompanyInfo.companyName");
            strSql.Append(",ShopInfo.isHandle,EmployeeConnShop.status,EmployeeConnShop.serviceStartTime,EmployeeConnShop.serviceEndTime,EmployeeConnShop.employeeShopID FROM EmployeeConnShop INNER JOIN");
            strSql.Append(" EmployeeInfo ON EmployeeConnShop.employeeID = EmployeeInfo.EmPloyeeID INNER JOIN");
            strSql.Append(" ShopInfo ON EmployeeConnShop.shopID = ShopInfo.shopID");
            strSql.Append(" INNER JOIN CompanyInfo ON ShopInfo.companyID = CompanyInfo.companyID");
            strSql.Append(" where ShopInfo.shopStatus > 0 and CompanyInfo.companyStatus > 0 and EmployeeInfo.EmployeeStatus > 0");

            strSql.Append(" and  EmployeeConnShop.status>0");

            strSql.Append(" and ShopInfo.isHandle='" + (int)VAShopHandleStatus.SHOP_Pass + "'");

            if (isControlSyb == true)
            {
                // strSql.Append(" and  EmployeeConnShop.isSupportEnterSyb=1");//权限过滤
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }

        /// <summary>
        /// 查询店铺管理员与店铺关系信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectEmployeeShop(int employeeID, bool onlyHandled)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ShopInfo.shopID,ShopInfo.shopName,ShopInfo.shopImagePath,ShopInfo.cityID,EmployeeConnShop.employeeID,CompanyInfo.companyId,CompanyInfo.companyName");
            strSql.Append(",ShopInfo.isHandle,EmployeeConnShop.status,EmployeeConnShop.serviceStartTime,EmployeeConnShop.serviceEndTime,EmployeeConnShop.employeeShopID FROM EmployeeConnShop INNER JOIN");
            strSql.Append(" EmployeeInfo ON EmployeeConnShop.employeeID = EmployeeInfo.EmPloyeeID INNER JOIN");
            strSql.Append(" ShopInfo ON EmployeeConnShop.shopID = ShopInfo.shopID");
            strSql.Append(" INNER JOIN CompanyInfo ON ShopInfo.companyID = CompanyInfo.companyID");
            strSql.Append(" where ShopInfo.shopStatus > 0 and CompanyInfo.companyStatus > 0 and EmployeeInfo.EmployeeStatus > 0");
            strSql.Append(" and  EmployeeConnShop.status>0");
            if (onlyHandled)
            {
                strSql.Append(" and isHandle ='" + (int)VAShopHandleStatus.SHOP_Pass + "'");
            }

            strSql.Append(" and EmployeeInfo.employeeID = @employeeID");

            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@employeeID", SqlDbType.Int) { Value = employeeID }
            };

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);

            return ds.Tables[0];
        }

        /// <summary>
        /// 查询所有上线门店
        /// </summary>
        /// <returns></returns>
        public DataTable SelectHandleShop()
        {
            const string strSql = @"select A.companyName,B.shopID,B.shopName,B.cityID
 from CompanyInfo A inner join ShopInfo B on A.companyID=B.CompanyID
 where A.companyStatus=1 and B.shopStatus=1 and B.isHandle=1";
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询店铺管理员与店铺关系信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectEmployeeShop(int employeeId, int shopID)
        {
            const string strSql = "SELECT [employeeShopID],[employeeID],[shopID],[companyId],[status] FROM [VAGastronomistMobileApp].[dbo].[EmployeeConnShop] where status > 0 and employeeId = @employeeId and  shopID = @shopID";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@employeeId", SqlDbType.Int) { Value = employeeId },
            new SqlParameter("@shopID", SqlDbType.Int) { Value = shopID }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询店铺管理员与店铺关系信息，包括所有状态
        /// </summary>
        /// <returns></returns>
        public DataTable SelectEmployeeShopAll(int employeeId, int shopID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [employeeShopID],[employeeID],[shopID],[companyId],[status]");
            strSql.Append(" FROM [VAGastronomistMobileApp].[dbo].[EmployeeConnShop]");
            strSql.AppendFormat(" where employeeId = {0} and shopID = {1}", employeeId, shopID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据员工编号查询审核店铺信息和公司信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectEmployeeShopInfoAndCompanyInfo(int employeeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ShopInfo.shopID,ShopInfo.shopName,EmployeeConnShop.employeeID,CompanyInfo.companyId,CompanyInfo.companyName");
            strSql.Append(" FROM EmployeeConnShop INNER JOIN");
            strSql.Append(" EmployeeInfo ON EmployeeConnShop.employeeID = EmployeeInfo.EmPloyeeID INNER JOIN");
            strSql.Append(" ShopInfo ON EmployeeConnShop.shopID = ShopInfo.shopID");
            strSql.Append(" INNER JOIN CompanyInfo ON ShopInfo.companyID = CompanyInfo.companyID");
            strSql.Append(" where ShopInfo.shopStatus > '0' and CompanyInfo.companyStatus > '0' and EmployeeInfo.EmployeeStatus > '0' and EmployeeInfo.EmployeeID='" + employeeID + "' and ShopInfo.[isHandle]='" + (int)VAShopHandleStatus.SHOP_Pass + "'");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 根据管理员编号查询对应公司信息(wangcheng加companyImagePath字段)
        /// </summary>
        /// <returns></returns>
        public DataTable SelectEmployeeCompany(int employeeID, bool isControlSyb = false)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select distinct CompanyInfo.companyID,CompanyInfo.companyName");
            //strSql.Append(" FROM EmployeeConnShop INNER JOIN");
            //strSql.Append(" EmployeeInfo ON EmployeeConnShop.employeeID = EmployeeInfo.EmPloyeeID INNER JOIN");
            //strSql.Append(" ShopInfo ON EmployeeConnShop.shopID = ShopInfo.shopID");
            //strSql.Append(" INNER JOIN CompanyInfo ON ShopInfo.companyID = CompanyInfo.companyID");
            //strSql.Append(" where ShopInfo.shopStatus > '0' and CompanyInfo.companyStatus > '0' and EmployeeInfo.EmployeeStatus > '0'");
            //strSql.AppendFormat(" and EmployeeInfo.EmployeeID = {0}", employeeID);
            strSql.Append("select distinct CompanyInfo.companyID,CompanyInfo.companyName,CompanyInfo.companyImagePath");
            strSql.Append(" FROM EmployeeConnShop INNER JOIN");
            strSql.Append(" EmployeeInfo ON EmployeeConnShop.employeeID = EmployeeInfo.EmPloyeeID INNER JOIN");
            strSql.Append(" CompanyInfo ON EmployeeConnShop.companyID = CompanyInfo.companyID");
            strSql.Append(" where CompanyInfo.companyStatus > '0' and EmployeeInfo.EmployeeStatus > '0'");
            strSql.AppendFormat(" and EmployeeInfo.EmployeeID = {0}", employeeID);
            strSql.Append(" and EmployeeConnShop.status>0 order by CompanyInfo.companyID desc");
            if (isControlSyb == true)
            {
                // strSql.Append(" and EmployeeConnShop.isSupportEnterSyb=1");//收银宝
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 收银宝查询对应公司信息
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="isControlSyb"></param>
        /// <returns></returns>
        public DataTable SelectEmployeeCompanyRemoveNotShop(int employeeID, bool isControlSyb = false)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct CompanyInfo.companyID,CompanyInfo.companyName,CompanyInfo.companyImagePath");
            strSql.Append(" FROM EmployeeConnShop INNER JOIN");
            strSql.Append(" EmployeeInfo ON EmployeeConnShop.employeeID = EmployeeInfo.EmPloyeeID INNER JOIN");
            strSql.Append(" CompanyInfo ON EmployeeConnShop.companyID = CompanyInfo.companyID");
            strSql.Append(" inner join ShopInfo on ShopInfo.shopID=EmployeeConnShop.shopID");
            strSql.Append(" where CompanyInfo.companyStatus > '0' and EmployeeInfo.EmployeeStatus > '0'");
            strSql.AppendFormat(" and EmployeeInfo.EmployeeID = {0}", employeeID);
            strSql.Append(" and EmployeeConnShop.status>0 and ShopInfo.shopStatus>'0'");
            if (isControlSyb == true)
            {

            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据管理员编号查询对应被审核过的公司信息（旗下有一家店被审核过，视为审核过公司）（wangcheng）
        /// </summary>
        /// <returns></returns>
        public DataTable SelectEmployeeCompanyIsHandle(int employeeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct CompanyInfo.companyID,CompanyInfo.companyName,CompanyInfo.companyImagePath");
            strSql.Append(" FROM CompanyInfo INNER JOIN ShopInfo on");
            strSql.Append(" ShopInfo.companyID = CompanyInfo.companyID");
            strSql.Append(" where CompanyInfo.companyStatus > '0'");
            strSql.Append(" and ShopInfo.isHandle= '1'");
            //strSql.Append("select distinct CompanyInfo.companyID,CompanyInfo.companyName,CompanyInfo.companyImagePath");
            //strSql.Append(" FROM EmployeeConnShop INNER JOIN");
            //strSql.Append(" EmployeeInfo ON EmployeeConnShop.employeeID = EmployeeInfo.EmPloyeeID INNER JOIN");

            //strSql.Append(" ShopInfo ON ShopInfo.companyID = EmployeeConnShop.companyId INNER JOIN");

            //strSql.Append(" CompanyInfo ON ShopInfo.companyID = CompanyInfo.companyID");
            //strSql.Append(" where CompanyInfo.companyStatus > '0' and EmployeeInfo.EmployeeStatus > '0'");
            //strSql.Append(" and ShopInfo.isHandle= '1'");
            //strSql.AppendFormat(" and EmployeeInfo.EmployeeID = {0}", employeeID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 所有上线的公司（wangcheng）
        /// </summary>
        /// <returns></returns>
        public DataTable SelectCompanyIsHandle()
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select distinct CompanyInfo.companyID,CompanyInfo.companyName,CompanyInfo.companyImagePath");
            strSql.Append(" FROM CompanyInfo INNER JOIN");
            strSql.Append(" ShopInfo ON ShopInfo.companyID = CompanyInfo.companyID");
            strSql.Append(" where CompanyInfo.companyStatus > '0'  and ShopInfo.shopStatus>'0'");
            strSql.Append(" and ShopInfo.isHandle= '1' order by CompanyInfo.companyID desc");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询当前城市所有上线门店
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable SelectCompanyIsHandle(int cityId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select distinct CompanyInfo.companyID,CompanyInfo.companyName");
            strSql.Append(" FROM CompanyInfo INNER JOIN");
            strSql.Append(" ShopInfo ON ShopInfo.companyID = CompanyInfo.companyID");
            strSql.Append(" where CompanyInfo.companyStatus > '0'  and ShopInfo.shopStatus>'0'");
            strSql.AppendFormat(" and ShopInfo.isHandle= '1' and ShopInfo.cityID={0} order by CompanyInfo.companyID desc", cityId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据员工姓名查询defaultPage页面信息（wangcheng）
        /// </summary>
        /// <returns></returns>
        public string SelectDefaultPage(string userName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select defaultPage from EmployeeInfo where UserName = '{0}' and EmployeeStatus>0 ", userName);//保证username查询出来的数据是唯一的
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            if (ds.Tables[0] != null)
            {
                if (ds.Tables[0].Rows.Count == 1)
                {
                    return ds.Tables[0].Rows[0]["defaultPage"].ToString();
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// （wangcheng）新增特殊权限信息
        /// (2013-8-2)
        /// </summary>
        /// <param name="EmployeeConnShop"></param>
        /// <returns></returns>
        public int InsertSpecialAuthority(SpecialAuthorityInfo specialAuthorityInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into SpecialAuthority(");
                    strSql.Append(" RoleId,specialAuthorityId,status)");
                    strSql.Append(" values (");
                    strSql.Append(" @RoleId,@specialAuthorityId,@status)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					new SqlParameter("@RoleId", SqlDbType.Int,4),
                    new SqlParameter("@specialAuthorityId", SqlDbType.Int,4),
                    new SqlParameter("@status", SqlDbType.Bit)};
                    parameters[0].Value = specialAuthorityInfo.RoleId;
                    parameters[1].Value = specialAuthorityInfo.specialAuthorityId;
                    parameters[2].Value = specialAuthorityInfo.status; ;
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
                    return Convert.ToInt32(obj);
                }
            }
        }
        /// <summary>
        /// 根据角色id查询特殊权限表信息（wangcheng）
        /// (2013-8-2)
        /// </summary>
        /// <returns></returns>
        public DataTable SelectSpecialAuthorityInfo(int roleId)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append("select RoleId ,specialAuthorityId,status");
            strSql.Append(" FROM SpecialAuthority ");
            strSql.AppendFormat(" where RoleId='{0}'", roleId);
            strSql.Append(" and status=1");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// （wangcheng）更新特殊权限开启状态
        /// //(2013-8-2 wangcheng)
        /// </summary>
        /// <param name="EmployeeConnShop"></param>
        /// <returns></returns>
        public int UpdateSpecialAuthorityStatus(int roleId, int specialAuthorityId, bool status)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("update  SpecialAuthority");
                    strSql.Append(" set status=@status ");
                    strSql.Append(" where specialAuthorityId=@specialAuthorityId and RoleId=@RoleId");
                    parameters = new SqlParameter[]{
                    new SqlParameter("@status", SqlDbType.Bit),
                    new SqlParameter("@specialAuthorityId", SqlDbType.Int,4),
					new SqlParameter("@RoleId", SqlDbType.Int,4)
                  };
                    parameters[0].Value = status;
                    parameters[1].Value = specialAuthorityId;
                    parameters[2].Value = roleId;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch
                {
                    return 0;
                }
                if (result == 0)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(result);
                }
            }
        }
        /// <summary>
        /// 删除该角色所有特殊权限
        /// add by wangc 20140414
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int UpdateSpecialAuthorityStatus(int roleId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("update  SpecialAuthority");
                    strSql.Append(" set status=0 ");
                    strSql.Append(" where RoleId=@RoleId");
                    parameters = new SqlParameter[]{
					new SqlParameter("@RoleId", SqlDbType.Int,4) };
                    parameters[0].Value = roleId;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch
                {
                    return 0;
                }
                if (result == 0)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(result);
                }
            }
        }
        /// <summary>
        /// 根据employeeID查询对应特殊权限信息（wangcheng）
        /// </summary>
        /// <returns></returns>
        public DataTable SelectSpecialAuthorityInfoByEmployeeID(int employeeID, int specialAuthorityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select distinct SpecialAuthority.RoleId,SpecialAuthority.specialAuthorityId,SpecialAuthority.status");
            strSql.Append(" FROM SpecialAuthority INNER JOIN");
            strSql.Append(" EmployeeRole ON EmployeeRole.RoleID = SpecialAuthority.RoleId ");

            List<SqlParameter> paraList = new List<SqlParameter>();
            if (specialAuthorityId == 0)
            {
                //查询当前employeeID下所有的开启的权限信息
                strSql.AppendFormat(" where SpecialAuthority.status=1  and EmployeeRole.EmployeeRoleStatus=1 and EmployeeRole.EmployeeID =@employeeID");
                paraList.Add(new SqlParameter("@employeeID", SqlDbType.Int) { Value = employeeID });
            }
            else
            {
                //查询当前employeeID下对应的specialAuthorityId的开启的权限信息
                strSql.AppendFormat(" where SpecialAuthority.status=1 and EmployeeRole.EmployeeRoleStatus=1 and EmployeeRole.EmployeeID = @employeeID and SpecialAuthority.specialAuthorityId=@specialAuthorityId");
                paraList.Add(new SqlParameter("@employeeID", SqlDbType.Int) { Value = employeeID });
                paraList.Add(new SqlParameter("@specialAuthorityId", SqlDbType.TinyInt) { Value = specialAuthorityId });
            }
            SqlParameter[] para = paraList.ToArray();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }

        /// <summary>
        /// 插入特殊权限和城市官僚表信息
        /// </summary>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="connSpecialAuthorityId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int InsertSpecialAuthorityConnCityInfo(int provinceId, int cityId, int connSpecialAuthorityId, int status)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();

                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into SpecialAuthorityConnCity(");
                    strSql.Append(" provinceId,cityId,connSpecialAuthorityId,status)");
                    strSql.Append(" values (");
                    strSql.Append(" @provinceId,@cityId,@connSpecialAuthorityId,@status)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
                    new SqlParameter("@provinceId", SqlDbType.Int,4),
                    new SqlParameter("@cityId", SqlDbType.Int,4),
                     new SqlParameter("@connSpecialAuthorityId", SqlDbType.Int,4),
                    new SqlParameter("@status", SqlDbType.Int,4)};
                    parameters[0].Value = provinceId;
                    parameters[1].Value = cityId;
                    parameters[2].Value = connSpecialAuthorityId;
                    parameters[3].Value = status;
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
                    return Convert.ToInt32(obj);
                }
            }
        }
        public DataTable SelectSpecialAuthorityConnCityInfo(int provinceId, int cityId, int connSpecialAuthorityId, int status)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select *");
            strSql.Append(" FROM SpecialAuthorityConnCity");
            strSql.AppendFormat(" where provinceId='{0}' and cityId='{1}' and connSpecialAuthorityId = '{2}' and status='{3}'", provinceId, cityId, connSpecialAuthorityId, status);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        public int DeleteSpecialAuthorityConnCityInfo(int provinceId, int cityId, int connSpecialAuthorityId, int status)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat("delete from SpecialAuthorityConnCity  where provinceId='{0}' and cityId='{1}' and connSpecialAuthorityId = '{2}' and status='{3}'", provinceId, cityId, connSpecialAuthorityId, status);
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString());
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
                    return Convert.ToInt32(obj);
                }
            }
        }
        /// <summary>
        /// 查询当前门店关联员工信息(wangcheng)
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public SybEmployeeShopInfoList SYBSelectEmployeeShop(int pageIndex, int pageSize, int shopId, bool isViewAllocWorker)
        {
            //过滤掉友络员工的部分帐号and (EmployeeInfo.isViewAllocWorker is null or EmployeeInfo.isViewAllocWorker=0)
            string sqlWhere = "EmployeeInfo.EmployeeStatus>0 and  EmployeeConnShop.shopID='" + shopId + "' and EmployeeConnShop.status>0 ";//该存储过程后附带group by 语句会导致返回数据行数有误
            if (isViewAllocWorker == false)
            {
                sqlWhere += " and (EmployeeInfo.isViewAllocWorker is null or EmployeeInfo.isViewAllocWorker=0) ";
            }
            PageQuery pageQuery = new PageQuery()
            {
                tableName = "EmployeeConnShop  inner join EmployeeInfo  on EmployeeConnShop.employeeID=EmployeeInfo.EmployeeID",
                fields = "EmployeeConnShop.employeeID,EmployeeInfo.UserName,EmployeeInfo.EmployeeFirstName",
                orderField = "EmployeeConnShop.employeeID",
                sqlWhere = sqlWhere
            };
            Paging paging = new Paging()
            {
                pageIndex = pageIndex,
                pageSize = pageSize,
                recordCount = 0,
                pageCount = 0
            };
            SybEmployeeShopInfoList data = new SybEmployeeShopInfoList();
            data.list = CommonManager.GetPageData<SybEmployeeShopInfo>(SqlHelper.ConnectionStringLocalTransaction, pageQuery, paging);
            data.page = new PageNav() { pageIndex = pageIndex, pageSize = pageSize, totalCount = paging.recordCount };
            return data;
        }
        /// <summary>
        /// 根据电话号码查询员工编号
        /// </summary>
        /// <param name="phoneNum"></param>
        /// <returns></returns>
        public DataTable SelectEmployeeIdByEmployeePhone(string phoneNum)
        {
            const string strSql = "SELECT  EmployeeID,UserName,EmployeeFirstName FROM EmployeeInfo where UserName=@phoneNum and EmployeeStatus>0 ";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@phoneNum", SqlDbType.NVarChar, 50) { Value = phoneNum }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }
        /// <summary>
        /// 收银宝查询服务员角色
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public DataTable SelectWaiterRole(int employeeId, int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [RoleInfo].[RoleID],[RoleName],[EmployeeRole].EmployeeID");
            strSql.Append(" FROM [RoleInfo] left join [EmployeeRole] on [EmployeeRole].RoleID=[RoleInfo].RoleID");
            strSql.AppendFormat(" where  RoleInfo.RoleStatus>0 and [EmployeeRole].EmployeeID={0}", employeeId);
            strSql.AppendFormat(" and [ShopId]={0}", shopId);
            strSql.Append(" and RoleInfo.RoleName in ('入座管理','财务对账','菜品管理','沽清管理','账户明细','增值管理')");//待处理
            strSql.Append("  and EmployeeRoleStatus > '0'");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 收银宝查询服务员默认角色
        /// </summary>
        /// <returns></returns>
        public DataTable SelectWaiterDefautRole()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [RoleInfo].[RoleID],RoleName");
            strSql.Append("  FROM [RoleInfo]");
            strSql.AppendFormat("  where  RoleInfo.RoleStatus>0");
            strSql.Append(" and RoleInfo.RoleName in ('入座管理','财务对账','菜品管理','沽清管理','账户明细','增值管理')");//待处理
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询当前员工可进入收银宝的门店
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public DataTable SelectEmployeeConnShopIsSupportEnterSyb(int employeeID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT  [employeeID],[shopID]");
            strSql.Append(" FROM [EmployeeConnShop]");
            strSql.AppendFormat("  where status=1 and employeeID={0} ", employeeID);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 积分商城：根据员工cookie信息查询基本信息
        /// </summary>
        /// <param name="cookie"></param>
        /// <returns></returns>
        public DataTable SelectEmployeePointByCookie(string cookie)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" select e.EmployeeID,e.UserName phoneNumber,e.EmployeeFirstName,ROUND( e.settlementPoint,2) settlementPoint,ROUND( e.notSettlementPoint,2) notSettlementPoint");
            strSql.Append(" from EmployeeInfo e");
            strSql.Append(" where e.EmployeeStatus = 1");
            strSql.Append(" and e.cookie = @cookie");

            SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@cookie", SqlDbType.NVarChar, 100)
            };
            para[0].Value = cookie;

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }
        /// <summary>
        /// 用户注册修改员工状态和密码（wangc 20140117 注册模块）
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public int UpdateEmployeeStatus(int employeeId, string password)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("update  EmployeeInfo");
                    strSql.Append(" set EmployeeStatus=1,");
                    strSql.Append(" Password=@Password");
                    strSql.Append(" where EmployeeID=@EmployeeID");
                    parameters = new SqlParameter[]{
                    new SqlParameter("@Password", SqlDbType.NVarChar,50),
					new SqlParameter("@EmployeeID", SqlDbType.Int,4)
                  };
                    parameters[0].Value = password;
                    parameters[1].Value = employeeId;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch
                {
                    return 0;
                }
                if (result == 0)
                {
                    return 0;
                }
                else
                {
                    return Convert.ToInt32(result);
                }
            }
        }
        /// <summary>
        /// 查询客户经理信息 add by wangc 20140328
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public List<PartEmployee> GetPartEmployeeInfo(string str)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [EmployeeID],[UserName],[EmployeeFirstName]  FROM [EmployeeInfo]");
            strSql.AppendFormat(" where (UserName like '%{0}%' or [EmployeeFirstName] like '%{0}%')", str);
            strSql.Append(" and isViewAllocWorker=1 and EmployeeStatus=1");
            List<PartEmployee> list = new List<PartEmployee>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), null))
            {
                while (dr.Read())
                {
                    PartEmployee model = new PartEmployee();
                    model.employeeId = dr["EmployeeID"] == DBNull.Value ? 0 : Convert.ToInt32(dr["EmployeeID"]);
                    model.employeeName = dr["EmployeeFirstName"] == DBNull.Value ? "未知" : Convert.ToString(dr["EmployeeFirstName"]);
                    model.employeePhone = dr["UserName"] == DBNull.Value ? "未知" : Convert.ToString(dr["UserName"]);
                    list.Add(model);
                }
            }
            return list;
        }

        /// <summary>
        /// <para>获取员工在该店的模块权限</para>
        /// <para>bruke</para>
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="employeeId"></param>
        /// <param name="shopAuthorityType">模块类型(1-收银宝,2-悠先服务)</param>
        /// <returns></returns>
        public string[] GetEmployeeInShopAuthorityNames(int shopId, int employeeId, short shopAuthorityType)
        {
            var strSqlBuilder = new StringBuilder(@"SELECT a.[shopAuthorityName]   
  FROM [dbo].[ShopAuthority] a
  inner join [dbo].[EmployeeShopAuthority] b on a.shopAuthorityId=b.shopAuthorityId 
  inner join [dbo].[EmployeeConnShop] c on c.employeeShopID=b.employeeConnShopId 
  where a.shopAuthorityStatus=1 and c.shopID=@ShopID and c.employeeID=@EmployeeID and a.shopAuthorityType=@shopAuthorityType and b.employeeShopAuthorityStatus=1 and c.[status]=1 and a.isClientShow=1");

            //StringBuilder strSqlBuilder = new StringBuilder();
            //strSqlBuilder.Append("SELECT d.AuthorityName ");
            //strSqlBuilder.Append("FROM [dbo].[EmployeeRole] a ");
            //strSqlBuilder.Append("inner join dbo.RoleInfo b on a.RoleID=b.RoleID ");
            //strSqlBuilder.Append("inner join dbo.RoleAuthority c on b.RoleID=c.RoleID ");
            //strSqlBuilder.Append("inner join dbo.AuthorityInfo d on c.AuthorityID=d.AuthorityID ");
            //strSqlBuilder.Append("where a.[EmployeeID]=@EmployeeID and a.[ShopID]=@ShopID and b.RoleStatus=1 and d.AuthorityStatus=1 and d.AuthorityType=@AuthorityType");
            SqlParameter[] cmdParms = new[]
            {
                new SqlParameter("@EmployeeID", employeeId),
                new SqlParameter("@ShopID", shopId),
                new SqlParameter("@shopAuthorityType", shopAuthorityType)
            };

            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSqlBuilder.ToString(), cmdParms))
            {
                List<string> authorityNames = new List<string>();
                while (dr.Read())
                {
                    authorityNames.Add(dr.GetString(0));
                }
                return authorityNames.ToArray();
            }

        }

        /// <summary>
        /// 获取店的权限
        /// </summary>
        /// <param name="shopAuthorityType"></param>
        /// <returns></returns>
        public string[] GetShopAuthorityNames(short shopAuthorityType)
        {
            StringBuilder strSqlBuilder = new StringBuilder(@"SELECT a.[shopAuthorityName]   
              FROM [dbo].[ShopAuthority] a
              where a.shopAuthorityType=@shopAuthorityType and a.isShow=1");

            SqlParameter[] cmdParms = new[]
            {
                //new SqlParameter("@shopId", shopId),
                new SqlParameter("@shopAuthorityType", shopAuthorityType)
            };

            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSqlBuilder.ToString(), cmdParms))
            {
                List<string> authorityNames = new List<string>();
                while (dr.Read())
                {
                    authorityNames.Add(dr.GetString(0));
                }
                return authorityNames.ToArray();
            }
        }

        /// <summary>
        /// 获取员工门店权限列表值
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="employeeId"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<WaiterRoleInfo> GetShopRoleListWithEmployee(int shopId, int employeeId, int type)
        {
            #region ---------------------------------------------------------update
            StringBuilder strSqlBuilder = new StringBuilder(@"SELECT a.[shopAuthorityName],a.shopAuthorityId, 
                                        case when EXISTS(
                                        select 1 from [EmployeeConnShop] c
                                        inner join [EmployeeShopAuthority] b on c.employeeShopID=b.employeeConnShopId
                                        where  b.employeeShopAuthorityStatus=1  and c.[status]=1 and c.shopID=@ShopID and c.employeeID=@EmployeeID and b.shopAuthorityId=a.shopAuthorityId 
                                        )
                                        then 1 else 0 end as IsHave
                                          FROM [dbo].[ShopAuthority] a
                                            where a.shopAuthorityStatus=1 and a.shopAuthorityType=@AuthorityType
                                          order by a.shopAuthoritySequence asc");

            SqlParameter[] cmdpParameters = new SqlParameter[]
            {
                new SqlParameter("@EmployeeID",employeeId), 
                new SqlParameter("@ShopID",shopId), 
                new SqlParameter("@AuthorityType",type)
            };

            if(type==2)
            {
                strSqlBuilder = new StringBuilder(@"SELECT a.[shopAuthorityName],a.shopAuthorityId, 
                                        case when EXISTS(
                                        select 1 from [EmployeeConnShop] c
                                        inner join [EmployeeShopAuthority] b on c.employeeShopID=b.employeeConnShopId
                                        where  b.employeeShopAuthorityStatus=1  and c.[status]=1 and c.shopID=@ShopID and c.employeeID=@EmployeeID and b.shopAuthorityId=a.shopAuthorityId 
                                        )
                                        then 1 else 0 end as IsHave
                                          FROM [dbo].[ShopAuthority] a
                                            where a.shopAuthorityStatus=1 and (a.shopAuthorityType=@AuthorityType or a.shopAuthorityType=@AuthorityType1)
                                          order by a.shopAuthoritySequence asc");
                cmdpParameters = new SqlParameter[]
                {
                    new SqlParameter("@EmployeeID",employeeId), 
                    new SqlParameter("@ShopID",shopId), 
                    new SqlParameter("@AuthorityType",type),
                    new SqlParameter("@AuthorityType1",4)
                };
            }

            #endregion

            List<WaiterRoleInfo> roles = new List<WaiterRoleInfo>();
            using (var dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSqlBuilder.ToString(), cmdpParameters))
            {
                while (dr.Read())
                {
                    roles.Add(new WaiterRoleInfo()
                    {
                        isHave = Convert.ToBoolean(dr["IsHave"]),
                        roleId = Convert.ToInt32(dr["shopAuthorityId"]),
                        roleName = Convert.ToString(dr["shopAuthorityName"])
                    });
                }
            }
            return roles;
        }

        /// <summary>
        /// 查询店铺管理员与店铺关系信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectEmployeeShopNew(int employeeId, string searchKeyWords)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.shopID,a.shopName,b.companyID,b.companyName");
            strSql.Append(" FROM EmployeeConnShop ");
            strSql.Append(" INNER JOIN EmployeeInfo ON EmployeeConnShop.employeeID = EmployeeInfo.EmPloyeeID ");
            strSql.Append(" INNER JOIN ShopInfo a ON EmployeeConnShop.shopID = a.shopID");
            strSql.Append(" INNER JOIN CompanyInfo b ON a.companyID = b.companyID");
            strSql.Append(" where a.shopStatus > 0 and b.companyStatus > 0 and EmployeeInfo.EmployeeStatus > 0");
            strSql.Append(" and  EmployeeConnShop.status>0");
            strSql.AppendFormat(" and EmployeeInfo.EmPloyeeID={0}", employeeId);
            if (searchKeyWords.Trim().Length > 0)
            {
                strSql.AppendFormat(" and shopName like '%{0}%'", searchKeyWords.Trim());
            }
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }

        /// <summary>
        /// 搜索数个人员信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public DataTable SelectEmployeeInfoByemployeeIds(string employeeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select EmployeeID,UserName,EmployeeFirstName,EmployeePhone from EmployeeInfo");
            strSql.AppendFormat(" where EmployeeID in ({0})", employeeId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
    }
}
