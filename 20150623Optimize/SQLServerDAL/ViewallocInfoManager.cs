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
    public class ViewallocInfoManager
    {
        /// <summary>
        /// 查询VIewAlloc平台VIP等级信息（jyn）
        /// </summary>
        /// <returns></returns>
        public DataTable SelectViewAllocPlatformVipInfo()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select v.id,v.name,v.consumptionLevel,v.isMonetary,v.status,v.vipImg");
            strSql.Append(" from ViewAllocPlatformVipInfo v");
            strSql.Append(" where v.status > 0 order by consumptionLevel ASC");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(string name, double consumptionLevel, string vipImgName)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("insert into ViewAllocPlatformVipInfo(");
                    strSql.Append("name,isMonetary,consumptionLevel,status,vipImg)");
                    strSql.Append(" values (");
                    strSql.Append("@name,@isMonetary,@consumptionLevel,@status,@vipImg)");
                    strSql.Append(";select @@IDENTITY");
                    SqlParameter[] parameters = {
					new SqlParameter("@name", name),
					new SqlParameter("@isMonetary", true),
					new SqlParameter("@consumptionLevel", consumptionLevel),
					new SqlParameter("@status", 1),
                    new SqlParameter("@vipImg", vipImgName)};
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
                catch (Exception)
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(int id, string name, double consumptionLevel, string vipImgName)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update ViewAllocPlatformVipInfo set ");
                    strSql.Append("name=@name,");
                    strSql.Append("isMonetary=@isMonetary,");
                    strSql.Append("consumptionLevel=@consumptionLevel,");
                    strSql.Append("status=@status,");
                    strSql.Append("vipImg=@vipImgName");
                    strSql.Append(" where id=@id");
                    SqlParameter[] parameters = {
					new SqlParameter("@name", name),
					new SqlParameter("@isMonetary", true),
					new SqlParameter("@consumptionLevel", consumptionLevel),
					new SqlParameter("@status", 1),
                    new SqlParameter("@vipImgName", vipImgName),
					new SqlParameter("@id", id)};
                    int rows = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                    if (rows > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update ViewAllocPlatformVipInfo set ");
                    strSql.Append("status=@status");
                    strSql.Append(" where id=@id");
                    SqlParameter[] parameters = {
					new SqlParameter("@status", -1),
					new SqlParameter("@id", id)};
                    int rows = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                    if (rows > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string name)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("select * from ViewAllocPlatformVipInfo");
                strSql.Append(" where name=@name and status>0");
                SqlParameter[] parameters = { new SqlParameter("@name", name) };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return true;//表示存在当前记录
            }
        }
    }
}
