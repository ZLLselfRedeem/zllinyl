using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using System.Data;
using System.Data.SqlClient;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 官网配置文件数据处理层
    /// 创建日期：2014-4-22
    /// </summary>
    public class OfficialWebConfigManager
    {
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="web"></param>
        /// <returns></returns>
        public int InsertOfficialWebConfig(OfficialWebConfig web)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into WebsiteConfig(");
                strSql.Append("type,title,date,content,sequence,imageName,updateTime,status,remark)");
                strSql.Append(" values (");
                strSql.Append("@type,@title,@date,@content,@sequence,@imageName,@updateTime,@status,@remark)");
                strSql.Append(" select @@identity");

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@type",SqlDbType.Int),
                    new SqlParameter("@title",SqlDbType.NVarChar,100),
                    new SqlParameter("@date",SqlDbType.NVarChar,50),
                    new SqlParameter("@content",SqlDbType.NVarChar,4000),
                    new SqlParameter("@sequence",SqlDbType.NVarChar,50),
                    new SqlParameter("@imageName",SqlDbType.NVarChar,50),
                    new SqlParameter("@updateTime",SqlDbType.NVarChar,50),
                    new SqlParameter("@status",SqlDbType.Int),
                    new SqlParameter("@remark",SqlDbType.NVarChar,500)
                    };
                para[0].Value = web.type;
                if (!string.IsNullOrEmpty(web.title))
                {
                    para[1].Value = web.title;
                }
                else
                {
                    para[1].Value = "";
                }
                if (!string.IsNullOrEmpty(web.date))
                {
                    para[2].Value = web.date;
                }
                else
                {
                    para[2].Value = "";
                }
                if (!string.IsNullOrEmpty(web.content))
                {
                    para[3].Value = web.content;
                }
                else
                {
                    para[3].Value = "";
                }
                para[4].Value = web.sequence;
                if (!string.IsNullOrEmpty(web.imageName))
                {
                    para[5].Value = web.imageName;
                }
                else
                {
                    para[5].Value = "";
                }
                para[6].Value = DateTime.Now;
                para[7].Value = 1;
                if (!string.IsNullOrEmpty(web.remark))
                {
                    para[8].Value = web.remark;
                }
                else
                {
                    para[8].Value = "";
                }
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    object objResult = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
                    if (objResult == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return Convert.ToInt32(objResult);
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int InsertOfficialWebConfigWithID(OfficialWebConfig web)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into WebsiteConfig(");
                strSql.Append("id,type,title,date,content,sequence,imageName,updateTime,status,remark)");
                strSql.Append(" values (");
                strSql.Append("@id,@type,@title,@date,@content,@sequence,@imageName,@updateTime,@status,@remark)");

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@id",SqlDbType.BigInt),
                    new SqlParameter("@type",SqlDbType.Int),
                    new SqlParameter("@title",SqlDbType.NVarChar,100),
                    new SqlParameter("@date",SqlDbType.NVarChar,50),
                    new SqlParameter("@content",SqlDbType.NVarChar,50),
                    new SqlParameter("@sequence",SqlDbType.NVarChar,50),
                    new SqlParameter("@imageName",SqlDbType.NVarChar,50),
                    new SqlParameter("@updateTime",SqlDbType.NVarChar,50),
                    new SqlParameter("@status",SqlDbType.Int),
                    new SqlParameter("@remark",SqlDbType.NVarChar,500)
                    };
                para[0].Value = web.id;
                para[1].Value = web.type;
                para[2].Value = web.title;
                para[3].Value = web.date;
                para[4].Value = web.content;
                para[5].Value = web.sequence;
                para[6].Value = web.imageName;
                para[7].Value = DateTime.Now;
                para[8].Value = 1;
                para[9].Value = web.remark;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int objResult = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                    if (objResult > 0)
                    {
                        return 0;
                    }
                    else
                    {
                        return objResult;
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        /// <param name="web"></param>
        /// <returns></returns>
        public bool UpdateOfficialWebConfig(OfficialWebConfig web)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update WebsiteConfig set ");
                strSql.Append("type=@type,");
                strSql.Append("title=@title,");
                strSql.Append("date=@date,");
                strSql.Append("content=@content,");
                strSql.Append("sequence=@sequence,");
                strSql.Append("imageName=@imageName,");
                strSql.Append("updateTime=@updateTime,");
                strSql.Append("remark=@remark");
                strSql.Append(" where id=@id");

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@type",SqlDbType.Int),
                    new SqlParameter("@title",SqlDbType.NVarChar,100),
                    new SqlParameter("@date",SqlDbType.NVarChar,50),
                    new SqlParameter("@content",SqlDbType.NVarChar,4000),
                    new SqlParameter("@sequence",SqlDbType.NVarChar,50),
                    new SqlParameter("@imageName",SqlDbType.NVarChar,50),
                    new SqlParameter("@updateTime",SqlDbType.NVarChar,50),
                    new SqlParameter("@remark",SqlDbType.NVarChar,500),
                    new SqlParameter("@id",SqlDbType.BigInt)
                };
                para[0].Value = web.type;
                if (!string.IsNullOrEmpty(web.title))
                {
                    para[1].Value = web.title;
                }
                else
                {
                    para[1].Value = "";
                }
                if (!string.IsNullOrEmpty(web.date))
                {
                    para[2].Value = web.date;
                }
                else
                {
                    para[2].Value = "";
                }
                if (!string.IsNullOrEmpty(web.content))
                {
                    para[3].Value = web.content;
                }
                else
                {
                    para[3].Value = "";
                }
                para[4].Value = web.sequence;
                if (!string.IsNullOrEmpty(web.imageName))
                {
                    para[5].Value = web.imageName;
                }
                else
                {
                    para[5].Value = "";
                }
                para[6].Value = DateTime.Now;
                if (!string.IsNullOrEmpty(web.remark))
                {
                    para[7].Value = web.remark;
                }
                else
                {
                    para[7].Value = "";
                }
                para[8].Value = web.id;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
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
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 根据ID删除指定信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteOfficialWebConfig(long id)
        {
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.AppendFormat("update WebsiteConfig set status='-1' where id={0}", id);
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString());
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
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// 根据类别获取相应数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable QueryOfficialWebConfig(VAOfficialWebType type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,type,title,date,content,sequence,imageName,updateTime,status,remark");
            strSql.Append(" from WebsiteConfig");
            strSql.AppendFormat(" where type={0} and status=1", (int)type);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据ID获取相应数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable QueryOfficialWebConfig(long id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,type,title,date,content,sequence,imageName,updateTime,status,remark");
            strSql.Append(" from WebsiteConfig");
            strSql.AppendFormat(" where id={0} and status=1", id);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 获取指定类型的最新动态
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataTable QueryRencetNewWithType(string type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,type,title,date,content,sequence,imageName,updateTime,status,remark");
            strSql.Append(" from WebsiteConfig");
            strSql.AppendFormat(" where type={0} and remark='{1}' and status=1", (int)VAOfficialWebType.RECENT_NEWS, type);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 获取指定type的当前最大sequence
        /// </summary>
        /// <returns></returns>
        public int QueryMaxSquence(VAOfficialWebType type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select isnull(max(sequence),'') sequence");
            strSql.Append(" from WebsiteConfig");
            strSql.AppendFormat(" where type={0} and status=1", (int)type);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 最新动态类别：根据类别名称找其排序号
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public string GetSequenceByType(string title)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select sequence");
            strSql.Append(" from WebsiteConfig");
            strSql.AppendFormat(" where type = {0}", (int)VAOfficialWebType.RECENT_NEWS_TYPE);
            strSql.AppendFormat(" and title = '{0}' and status=1", title);
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                conn.Open();
                object sequence = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString());
                if (sequence != null)
                {
                    return sequence.ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        /// <summary>
        /// 获取最新动态的所有类别
        /// </summary>
        /// <returns></returns>
        public DataTable GetRecentNewsType()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,title");
            strSql.Append(" from WebsiteConfig");
            strSql.AppendFormat(" where type={0} and status=1", (int)VAOfficialWebType.RECENT_NEWS_TYPE);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
    }
}
