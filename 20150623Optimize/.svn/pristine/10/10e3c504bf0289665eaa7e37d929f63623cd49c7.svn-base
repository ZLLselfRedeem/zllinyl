using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;
using System.Data;
//
//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2012-04-10.
//
namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 客户端版本数据库操作类
    /// </summary>
    public class AppBuildManager
    {
        /// <summary>
        /// 根据客户端类型查询对应最新版本信息20140313
        /// </summary>
        /// <returns></returns>
        public DataTable SelectLatestBuild(VAAppType apptype)
        {
            const string strSql = @"SELECT [id],[latestBuild] ,[latestUpdateDescription],[latestUpdateUrl],[appType],[oldBuildSupport],[updateTime] FROM [AppBuildInfo] where appType =@appType";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@appType",SqlDbType.TinyInt){ Value = (int)apptype }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询悠先点菜app版本信息（Android，IOS）
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAppLatestBuild()
        {
            const string strSql = "SELECT [id],[latestBuild] ,[latestUpdateDescription],[latestUpdateUrl],[appType] type,[oldBuildSupport],[updateTime] FROM [AppBuildInfo]";
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql);
            return ds.Tables[0];
        }
        /// <summary>
        /// 更新app版本信息
        /// </summary>
        public bool UpdateAppBuildInfo(AppBuildInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update AppBuildInfo set ");
            strSql.Append("latestBuild=@latestBuild,");
            strSql.Append("latestUpdateDescription=@latestUpdateDescription,");
            strSql.Append("latestUpdateUrl=@latestUpdateUrl,");
            strSql.Append("appType=@appType,");
            strSql.Append("oldBuildSupport=@oldBuildSupport,");
            strSql.Append("updateTime=@updateTime");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@latestBuild", SqlDbType.NVarChar,50),
                    new SqlParameter("@latestUpdateDescription", SqlDbType.NVarChar,100),
                    new SqlParameter("@latestUpdateUrl", SqlDbType.NVarChar,500),
                    new SqlParameter("@appType", SqlDbType.TinyInt,1),
                    new SqlParameter("@oldBuildSupport", SqlDbType.NVarChar,50),
                    new SqlParameter("@updateTime", SqlDbType.DateTime),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.latestBuild;
            parameters[1].Value = model.latestUpdateDescription;
            parameters[2].Value = model.latestUpdateUrl;
            parameters[3].Value = model.type;
            parameters[4].Value = model.oldBuildSupport;
            parameters[5].Value = model.updateTime;
            parameters[6].Value = model.id;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
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
    /// 悠先服务版本信息类 add by wangc 20140421 
    /// （pc版本，Android版本，IOS版本）
    /// </summary>
    public class ServiceBuildManager
    {
        /// <summary>
        /// 根据客户端类型查询对应最新版本信息20140401
        /// </summary>
        /// <returns></returns>
        public DataTable SelectLatestBuild(int serviceType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [id],[latestBuild] ,[latestUpdateDescription],[latestUpdateUrl],[type],");
            strSql.Append("[oldBuildSupport],[updateTime] FROM [ServiceBuildInfo]");
            strSql.AppendFormat(" where type = '{0}'", serviceType);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询悠先服务版本信息（pc版本，Android版本，IOS版本）
        /// </summary>
        /// <returns></returns>
        public DataTable SelectServiceLatestBuild()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT [id] ,[latestBuild] ,[latestUpdateDescription] ,[latestUpdateUrl] ,[type] ,[oldBuildSupport] ,[updateTime]");
            strSql.Append(" FROM [ServiceBuildInfo]");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 更新悠先服务版本信息
        /// </summary>
        public bool UpdateServiceLatestBuild(ServiceBuildInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ServiceBuildInfo set ");
            strSql.Append("latestBuild=@latestBuild,");
            strSql.Append("latestUpdateDescription=@latestUpdateDescription,");
            strSql.Append("latestUpdateUrl=@latestUpdateUrl,");
            strSql.Append("type=@type,");
            strSql.Append("oldBuildSupport=@oldBuildSupport,");
            strSql.Append("updateTime=@updateTime");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@latestBuild", SqlDbType.NVarChar,50),
					new SqlParameter("@latestUpdateDescription", SqlDbType.NVarChar,100),
					new SqlParameter("@latestUpdateUrl", SqlDbType.NVarChar,500),
					new SqlParameter("@type", SqlDbType.TinyInt,1),
					new SqlParameter("@oldBuildSupport", SqlDbType.NVarChar,50),
					new SqlParameter("@updateTime", SqlDbType.DateTime),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.latestBuild;
            parameters[1].Value = model.latestUpdateDescription;
            parameters[2].Value = model.latestUpdateUrl;
            parameters[3].Value = model.type;
            parameters[4].Value = model.oldBuildSupport;
            parameters[5].Value = model.updateTime;
            parameters[6].Value = model.id;
            int rows = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (rows > 0)
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
