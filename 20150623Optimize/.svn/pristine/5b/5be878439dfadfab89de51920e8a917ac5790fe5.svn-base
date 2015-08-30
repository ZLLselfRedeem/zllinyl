using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
//
//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2012-04-10.
//
namespace VAGastronomistMobileApp.SQLServerDAL
{
   public class VAEncourageConfigManager
    {
        /// <summary>
        /// 新增系统设置
        /// </summary>
        /// <param name="vAEncourageConfig"></param>
        public int InsertVAEncourageConfig(VAEncourageConfig vAEncourageConfig)
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
                    strSql.Append("insert into VAEncourageConfig(");
                    strSql.Append("configName,configDescription,configContent)");
                    strSql.Append(" values (");
                    strSql.Append("@configName,@configDescription,@configContent)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					    new SqlParameter("@configName", SqlDbType.NVarChar,500),
                        new SqlParameter("@configDescription", SqlDbType.NVarChar,500),
                        new SqlParameter("@configContent", SqlDbType.NVarChar,500)
                    };
                    parameters[0].Value = vAEncourageConfig.configName;
                    parameters[1].Value = vAEncourageConfig.configDescription;
                    parameters[2].Value = vAEncourageConfig.configContent;
                    //1、插入系统设置表信息
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
        /// 修改系统设置
        /// </summary>
        /// <param name="vAEncourageConfig"></param>
        public bool UpdateVAEncourageConfig(VAEncourageConfig vAEncourageConfig)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters = null;
            strSql.Append("update VAEncourageConfig set ");
            strSql.Append("configContent=@configContent");
            strSql.Append(" where id=@id ");
            parameters = new SqlParameter[]{
                        new SqlParameter("@configContent", SqlDbType.NVarChar,500),
                        new SqlParameter("@id", SqlDbType.Int,4)
                    };
            parameters[0].Value = vAEncourageConfig.configContent;
            parameters[1].Value = vAEncourageConfig.id;
            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result >= 1) //
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 查询所有配置信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectVAEncourageConfig()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * FROM VAEncourageConfig");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
    }
}
