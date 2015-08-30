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
    public class LangManager
    {
        /// <summary>
        /// 新增语言信息
        /// </summary>
        /// <param name="lang"></param>
        public int InsertLang(LanguageInfo lang)
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
                    strSql.Append("insert into LanguageInfo(");
                    strSql.Append("LangName,IsDefaultLang,LangStatus)");
                    strSql.Append(" values (");
                    strSql.Append("@LangName,@IsDefaultLang,@LangStatus)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					new SqlParameter("@LangName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsDefaultLang", SqlDbType.Bit,1),
					new SqlParameter("@LangStatus",SqlDbType.Int,4)};
                    parameters[0].Value = lang.LangName;
                    parameters[1].Value = lang.IsDefaultLang;
                    parameters[2].Value = lang.LangStatus;

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
        /// 删除语言信息
        /// </summary>
        /// <param name="langID"></param>
        public void DeleteLangByID(int langID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {

                SqlTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("update LanguageInfo set LangStatus = '-1' where MenuID=@menuID;");

                    SqlParameter[] parameters = {					
					new SqlParameter("@langID", SqlDbType.Int,4)};
                    parameters[0].Value = langID;

                    SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                }
            }
        }
        /// <summary>
        /// 修改语言信息
        /// </summary>
        /// <param name="lang"></param>
        public void UpdateLang(LanguageInfo lang)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update LanguageInfo set ");
            strSql.Append("LangName=@LangName,");
            strSql.Append("IsDefaultLang=@IsDefaultLang,");
            strSql.Append("LangStatus=@LangStatus,");
            strSql.Append(" where LangID=@LangID ");
            SqlParameter[] parameters = {
					new SqlParameter("@LangName", SqlDbType.NVarChar,50),
					new SqlParameter("@IsDefaultLang", SqlDbType.Bit,1),
					new SqlParameter("@LangStatus",SqlDbType.Int,4),
                    new SqlParameter("@LangID",SqlDbType.Int,4)};
            parameters[0].Value = lang.LangName;
            parameters[1].Value = lang.IsDefaultLang;
            parameters[2].Value = lang.LangStatus;
            parameters[3].Value = lang.LangID;

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 查询菜单信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryLang()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [LangID],[LangName],[IsDefaultLang],[LangStatus],[langNameEn]");
            strSql.Append(" from [LanguageInfo]");
            strSql.Append(" where [LanguageInfo].LangStatus > 0");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
    }
}
