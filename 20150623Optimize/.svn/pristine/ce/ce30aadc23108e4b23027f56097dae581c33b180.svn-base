using LogDll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class WeChatUserManager
    {
        /// <summary>
        /// 插入微信授权
        /// </summary>
        /// <param name="model">微信用户模型</param>
        /// <param name="privilegeList">微信用户特权列表</param>
        /// <returns></returns>
        public int Insert(WeChatUser model, IList<WeChatUserPrivilege> privilegeList)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO Tb_WeChatUser(");
            sql.Append("Id,CustomerInfo_CustomerID,OpenId,UnionId,NickName,Sex,Province,City,Country,HeadImgUrl,HeadImgSize,AddTime,ModifyTime,ModifyUser,ModifyIP)");
            sql.Append("VALUES (");
            sql.Append("@Id,@CustomerInfo_CustomerID,@OpenId,@UnionId,@NickName,@Sex,@Province,@City,@Country,@HeadImgUrl,@HeadImgSize,getdate(),getdate(),@ModifyUser,@ModifyIP)");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", model.Id),
					new SqlParameter("@CustomerInfo_CustomerID", model.CustomerInfo_CustomerID),
					new SqlParameter("@OpenId", model.OpenId),
					new SqlParameter("@UnionId", model.UnionId),
					new SqlParameter("@NickName", model.NickName),
					new SqlParameter("@Sex", model.Sex),
                    new SqlParameter("@Province", model.Province),
                    new SqlParameter("@City", model.City),
                    new SqlParameter("@Country", model.Country),
                    new SqlParameter("@HeadImgUrl", model.HeadImgUrl),
                    new SqlParameter("@HeadImgSize", model.HeadImgSize),
                    new SqlParameter("@ModifyUser", model.ModifyUser),
                    new SqlParameter("@ModifyIP", model.ModifyIP)};
            string sqlPrivilege = "INSERT INTO Tb_WeChatUser_Privilege (Id,WeChatUser_Id,Privilege,AddTime) VALUES (@Id,@WeChatUser_Id,@Privilege,getdate())";

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int s, count = 0;
                        count += s = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql.ToString(), parameters);
                        if (s == 0)
                            throw new Exception("添加微信用户失败!");

                        foreach (var item in privilegeList)
                        {
                            SqlParameter[] parametersPrivilege = {
                            new SqlParameter("@Id", item.Id),
                            new SqlParameter("@WeChatUser_Id", model.Id),
                            new SqlParameter("@Privilege", item.Privilege)};
                            count += s = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sqlPrivilege, parametersPrivilege);
                            if (s == 0)
                                throw new Exception("添加微信用户特权失败");
                        }
                        tran.Commit();
                        return count;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        LogManager.WriteLog(LogFile.Trace, ex.ToString());
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// 更新微信用户
        /// </summary>
        /// <param name="model">微信用户模型</param>
        /// <param name="privilegeList">微信用户特权列表</param>
        /// <returns></returns>
        public int Update(WeChatUser model, IList<WeChatUserPrivilege> privilegeList)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE Tb_WeChatUser SET ");
            sql.Append("CustomerInfo_CustomerID=@CustomerInfo_CustomerID,");
            sql.Append("OpenId=@OpenId,");
            sql.Append("UnionId=@UnionId,");
            sql.Append("NickName=@NickName,");
            sql.Append("Sex=@Sex,");
            sql.Append("Province=@Province,");
            sql.Append("City=@City,");
            sql.Append("Country=@Country,");
            sql.Append("HeadImgUrl=@HeadImgUrl,");
            sql.Append("HeadImgSize=@HeadImgSize,");
            sql.Append("ModifyTime=getdate(),");
            sql.Append("ModifyUser=@ModifyUser,");
            sql.Append("ModifyIP=@ModifyIP ");
            sql.Append("WHERE Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerInfo_CustomerID", model.CustomerInfo_CustomerID),
					new SqlParameter("@OpenId", model.OpenId),
					new SqlParameter("@UnionId", model.UnionId),
					new SqlParameter("@NickName", model.NickName),
					new SqlParameter("@Sex", model.Sex),
                    new SqlParameter("@Province", model.Province),
                    new SqlParameter("@City", model.City),
                    new SqlParameter("@Country", model.Country),
                    new SqlParameter("@HeadImgUrl", model.HeadImgUrl),
                    new SqlParameter("@HeadImgSize", model.HeadImgSize),
                    new SqlParameter("@ModifyUser", model.ModifyUser),
                    new SqlParameter("@ModifyIP", model.ModifyIP),
					new SqlParameter("@Id", model.Id)};
            string sqlPrivilege = "INSERT INTO Tb_WeChatUser_Privilege (Id,WeChatUser_Id,Privilege,AddTime) VALUES (@Id,@WeChatUser_Id,@Privilege,getdate())";
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int s, count = 0;
                        count += s = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql.ToString(), parameters);
                        if (s == 0)
                            throw new Exception("更新微信用户失败!");

                        if (privilegeList != null && privilegeList.Count != 0)
                        {
                            string sqlDelPrivilege = "DELETE FROM Tb_WeChatUser_Privilege WHERE WeChatUser_Id=@WeChatUserId";
                            SqlParameter[] parametersDelPrivilege = { new SqlParameter("@WeChatUserId", model.Id) };
                            count += s = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sqlDelPrivilege, parametersDelPrivilege);
                            if (s == 0)
                                throw new Exception("删除微信用户特权失败!");

                            foreach (var item in privilegeList)
                            {
                                SqlParameter[] parametersPrivilege = {
                                new SqlParameter("@Id", item.Id),
                                new SqlParameter("@WeChatUser_Id", model.Id),
                                new SqlParameter("@Privilege", item.Privilege)};
                                count += s = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sqlPrivilege, parameters);
                                if (s == 0)
                                    throw new Exception("添加微信用户特权失败");
                            }
                        }
                        tran.Commit();
                        return count;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        LogManager.WriteLog(LogFile.Trace, ex.Message);
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// 更新用户id
        /// </summary>
        /// <param name="unionId"></param>
        /// <param name="customerInfoCustomerID"></param>
        /// <param name="modifyUser"></param>
        /// <param name="modifyIP"></param>
        /// <returns></returns>
        public int UpdateCustomerInfoCustomerID(string unionId, long customerInfoCustomerID, string modifyUser, string modifyIP)
        {
            string sql = "UPDATE Tb_WeChatUser SET CustomerInfo_CustomerID=@CustomerInfoCustomerID,ModifyTime=getdate(),ModifyUser=@ModifyUser,ModifyIP=@ModifyIP WHERE UnionId=@UnionId";
            SqlParameter[] parameters = {
					new SqlParameter("@CustomerInfoCustomerID",customerInfoCustomerID),
					new SqlParameter("@ModifyUser", modifyUser),
					new SqlParameter("@ModifyIP", modifyIP),
					new SqlParameter("@UnionId", unionId)};
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString(), parameters);
        }

        /// <summary>
        /// 更新手机号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mobile"></param>
        /// <param name="modifyUser"></param>
        /// <param name="modifyIP"></param>
        /// <returns></returns>
        public int UpdateMobile(string id, string mobile, string modifyUser, string modifyIP)
        {
            string sql = "UPDATE Tb_WeChatUser SET MobilePhoneNumber=@MobilePhoneNumber,ModifyTime=getdate(),ModifyUser=@ModifyUser,ModifyIP=@ModifyIP WHERE Id=@Id";
            SqlParameter[] parameters = {
					new SqlParameter("@MobilePhoneNumber",mobile),
					new SqlParameter("@ModifyUser", modifyUser),
					new SqlParameter("@ModifyIP", modifyIP),
					new SqlParameter("@Id", id)};
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString(), parameters);
        }

        /// <summary>
        /// 更新手机号
        /// </summary>
        /// <param name="mobile">旧手机号</param>
        /// <param name="newMobile">新手机号</param>
        /// <param name="modifyUser">修改人</param>
        /// <param name="modifyIP">修改IP</param>
        /// <returns></returns>
        public int UpdateNewMobile(string mobile, string newMobile, string modifyUser, string modifyIP)
        {
            string sql = "UPDATE Tb_WeChatUser SET MobilePhoneNumber=@MobilePhoneNumber,ModifyTime=getdate(),ModifyUser=@ModifyUser,ModifyIP=@ModifyIP WHERE MobilePhoneNumber=@Mobile";
            SqlParameter[] parameters = {
					new SqlParameter("@MobilePhoneNumber",newMobile),
					new SqlParameter("@ModifyUser", modifyUser),
					new SqlParameter("@ModifyIP", modifyIP),
					new SqlParameter("@Mobile", mobile)};
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString(), parameters);
        }

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="customerInfoCustomerID"></param>
        /// <returns></returns>
        public bool IsExistUser(long customerInfoCustomerID)
        {
            string sql = "SELECT * FROM Tb_WeChatUser WHERE CustomerInfo_CustomerID=@CustomerInfoCustomerID";
            SqlParameter[] parameters = { new SqlParameter("@CustomerInfoCustomerID", customerInfoCustomerID) };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader drea = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString(), parameters))
            {
                if (drea.Read())
                    return SqlHelper.GetEntity<WeChatUser>(drea).OpenId != null ? true : false;
                return false;
            }
        }

        /// <summary>
        /// 判断手机号是否存在
        /// </summary>
        /// <param name="mobile"></param>
        /// <returns></returns>
        public bool IsExistMombile(string mobile)
        {
            string sql = "SELECT * FROM Tb_WeChatUser WHERE MobilePhoneNumber=@MobilePhoneNumber";
            SqlParameter[] parameters = { new SqlParameter("@MobilePhoneNumber", mobile) };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader drea = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString(), parameters))
            {
                if (drea.Read())
                    return SqlHelper.GetEntity<WeChatUser>(drea).OpenId != null ? true : false;
                return false;
            }
        }

        /// <summary>
        /// 返回微信用户model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WeChatUser GetModel(Guid id)
        {
            string sql = "SELECT * FROM Tb_WeChatUser WHERE Id=@Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", id) };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader drea = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString(), parameters))
            {
                if (drea.Read())
                    return SqlHelper.GetEntity<WeChatUser>(drea);
                return null;
            }
        }

        /// <summary>
        /// 按openId返回微信用户model
        /// </summary>
        /// <param name="openId">openid</param>
        /// <returns></returns>
        public WeChatUser GetOpenIdOfModel(string openId)
        {
            string sql = "SELECT * FROM Tb_WeChatUser WHERE OpenId=@OpenId";
            SqlParameter[] parameters = { new SqlParameter("@OpenId", openId) };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader drea = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString(), parameters))
            {
                if (drea.Read())
                    return SqlHelper.GetEntity<WeChatUser>(drea);
                return null;
            }
        }

        /// <summary>
        /// 按unionId返回微信用户model
        /// </summary>
        /// <param name="unionId">unionId</param>
        /// <returns></returns>
        public WeChatUser GetUnionIdOfModel(string unionId)
        {
            string sql = "SELECT * FROM Tb_WeChatUser WHERE UnionId=@UnionId";
            SqlParameter[] parameters = { new SqlParameter("@UnionId", unionId) };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader drea = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString(), parameters))
            {
                if (drea.Read())
                    return SqlHelper.GetEntity<WeChatUser>(drea);
                return null;
            }
        }

        /// <summary>
        /// 按mobile返回微信用户model
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns></returns>
        public WeChatUser GetMobileOfModel(string mobile)
        {
            string sql = "SELECT * FROM Tb_WeChatUser WHERE MobilePhoneNumber=@MobilePhoneNumber";
            SqlParameter[] parameters = { new SqlParameter("@MobilePhoneNumber", mobile) };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader drea = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString(), parameters))
            {
                if (drea.Read())
                    return SqlHelper.GetEntity<WeChatUser>(drea);
                return null;
            }
        }

        /// <summary>
        /// 按微信用户id返回特权列表
        /// </summary>
        /// <param name="weChatUserId"></param>
        /// <returns></returns>
        public IEnumerable<WeChatUserPrivilege> GetPrivilegeList(Guid weChatUserId)
        {
            string sql = "SELECT * FROM Tb_WeChatUser_Privilege WHERE WeChatUser_Id=@WeChatUserId";
            SqlParameter[] parameters = { new SqlParameter("@WeChatUserId", weChatUserId) };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader rd = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString(), parameters))
            {
                while (rd.Read())
                    yield return SqlHelper.GetEntity<WeChatUserPrivilege>(rd);
            }
        }

        /// <summary>
        /// 按用户id返回entity
        /// </summary>
        /// <param name="customerId">用户id</param>
        /// <returns></returns>
        public WeChatUser GetCustomerIDToEntity(long customerId)
        {
            string sql = "SELECT * FROM Tb_WeChatUser WHERE CustomerInfo_CustomerID=@CustomerInfoCustomerID";
            SqlParameter[] parameters = { new SqlParameter("@CustomerInfoCustomerID", customerId) };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader drea = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString(), parameters))
            {
                if (drea.Read())
                    return SqlHelper.GetEntity<WeChatUser>(drea);
                return null;
            }
        }

        /// <summary>
        /// 重新绑定微信号
        /// </summary>
        /// <param name="customerId">原用户id</param>
        /// <param name="unionId">微信号</param>
        /// <returns></returns>
        public int UpdateCustomerIdBinding(long customerId, string unionId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                conn.Open();
                using (SqlTransaction tran = conn.BeginTransaction())
                {
                    try
                    {
                        int s, count = 0;
                        string sql = "SELECT COUNT(0) FROM Tb_WeChatUser WHERE CustomerInfo_CustomerID=@CustomerInfoCustomerID";
                        SqlParameter[] parameters = { new SqlParameter("@CustomerInfoCustomerID", customerId) };
                        int isRemoveBinding = Convert.ToInt32(SqlHelper.ExecuteScalar(tran, CommandType.Text, sql.ToString(), parameters));
                        if (isRemoveBinding != 0)
                        {
                            sql = "UPDATE Tb_WeChatUser SET CustomerInfo_CustomerID=0 WHERE CustomerInfo_CustomerID=@CustomerInfoCustomerID";
                            count += s = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql.ToString(), parameters);
                            if (s == 0)
                                throw new Exception("解除微信绑定失败");
                        }

                        sql = "UPDATE Tb_WeChatUser SET CustomerInfo_CustomerID=@CustomerInfoCustomerID WHERE UnionId=@UnionId";
                        SqlParameter[] parameters1 = { new SqlParameter("@CustomerInfoCustomerID", customerId), new SqlParameter("@UnionId", unionId) };

                        count += s = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, sql.ToString(), parameters1);
                        if (s == 0)
                            throw new Exception("绑定微信失败");

                        tran.Commit();
                        return count;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        LogManager.WriteLog(LogFile.Trace, ex.Message);
                        return 0;
                    }
                }
            }
        }

        /// <summary>
        /// 用户解除绑定
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateRemoveCustomerInfoCustomerID(Guid id)
        {
            string sql = "UPDATE Tb_WeChatUser SET CustomerInfo_CustomerID=0 WHERE Id=@Id";
            SqlParameter[] parameters = {
					new SqlParameter("@Id",id)};
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString(), parameters);
        }
    }
}
