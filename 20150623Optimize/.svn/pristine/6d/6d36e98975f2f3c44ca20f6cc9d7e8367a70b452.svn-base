using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class WeChatAuthorizationManager
    {
        /// <summary>
        /// 插入微信授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(WeChatAuthorization model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("INSERT INTO Tb_WeChatAuthorization(");
            sql.Append("Id,OpenId,AccessToken,ExpiresIn,RefreshToken,Scope,AddTime,ModifyTime,ModifyUser,ModifyIP)");
            sql.Append("VALUES (");
            sql.Append("@Id,@OpenId,@AccessToken,@ExpiresIn,@RefreshToken,@Scope,getdate(),getdate(),@ModifyUser,@ModifyIP)");
            SqlParameter[] parameters = {
					new SqlParameter("@Id", model.Id),
					new SqlParameter("@OpenId", model.OpenId),
					new SqlParameter("@AccessToken", model.AccessToken),
					new SqlParameter("@ExpiresIn", model.ExpiresIn),
					new SqlParameter("@RefreshToken", model.RefreshToken),
                    new SqlParameter("@Scope", model.Scope),
                    new SqlParameter("@ModifyUser", model.ModifyUser),
                    new SqlParameter("@ModifyIP", model.ModifyIP)};
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString(), parameters);
        }

        /// <summary>
        /// 更新微信授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Update(WeChatAuthorization model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE Tb_WeChatAuthorization SET ");
            sql.Append("OpenId=@OpenId,");
            sql.Append("AccessToken=@AccessToken,");
            sql.Append("ExpiresIn=@ExpiresIn,");
            sql.Append("RefreshToken=@RefreshToken,");
            sql.Append("Scope=@Scope,");
            sql.Append("ModifyTime=getdate(),");
            sql.Append("ModifyUser=@ModifyUser,");
            sql.Append("ModifyIP=@ModifyIP ");
            sql.Append("WHERE Id=@Id");
            SqlParameter[] parameters = {
					new SqlParameter("@OpenId", model.OpenId),
					new SqlParameter("@AccessToken", model.AccessToken),
					new SqlParameter("@ExpiresIn", model.ExpiresIn),
					new SqlParameter("@RefreshToken", model.RefreshToken),
                    new SqlParameter("@Scope", model.Scope),
                    new SqlParameter("@ModifyUser", model.ModifyUser),
					new SqlParameter("@ModifyIP", model.ModifyIP),
                    new SqlParameter("@Id", model.Id)};
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString(), parameters);
        }

        /// <summary>
        /// 按OpenId更新微信授权
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateOfOpenId(WeChatAuthorization model)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("UPDATE Tb_WeChatAuthorization SET ");
            sql.Append("AccessToken=@AccessToken,");
            sql.Append("ExpiresIn=@ExpiresIn,");
            sql.Append("RefreshToken=@RefreshToken,");
            sql.Append("Scope=@Scope,");
            sql.Append("ModifyTime=getdate(),");
            sql.Append("ModifyUser=@ModifyUser,");
            sql.Append("ModifyIP=@ModifyIP ");
            sql.Append("WHERE OpenId=@OpenId");
            SqlParameter[] parameters = {
					new SqlParameter("@AccessToken", model.AccessToken),
					new SqlParameter("@ExpiresIn", model.ExpiresIn),
					new SqlParameter("@RefreshToken", model.RefreshToken),
                    new SqlParameter("@Scope", model.Scope),
                    new SqlParameter("@ModifyUser", model.ModifyUser),
					new SqlParameter("@ModifyIP", model.ModifyIP),
                    new SqlParameter("@OpenId", model.OpenId)};
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                return SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sql.ToString(), parameters);
        }

        /// <summary>
        /// 返回model
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WeChatAuthorization GetModel(Guid id)
        {
            string sql = "SELECT * FROM Tb_WeChatAuthorization WHERE Id=@Id";
            SqlParameter[] parameters = { new SqlParameter("@Id", id) };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader drea = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString(), parameters))
            {
                if (drea.Read())
                    return SqlHelper.GetEntity<WeChatAuthorization>(drea);
                return null;
            }
        }

        /// <summary>
        /// 按openId返回model
        /// </summary>
        /// <param name="openId">openid</param>
        /// <returns></returns>
        public WeChatAuthorization GetOpenIdOfModel(string openId)
        {
            string sql = "SELECT * FROM Tb_WeChatAuthorization WHERE OpenId=@OpenId";
            SqlParameter[] parameters = { new SqlParameter("@OpenId", openId) };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            using (SqlDataReader drea = SqlHelper.ExecuteReader(conn, CommandType.Text, sql.ToString(), parameters))
            {
                if (drea.Read())
                    return SqlHelper.GetEntity<WeChatAuthorization>(drea);
                return null;
            }
        }
    }
}
