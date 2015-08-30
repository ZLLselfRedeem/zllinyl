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
    /// 积分商城Html内容数据访问层
    /// 2014-2-21 jinyanni
    /// </summary>
    public class HtmlManage
    {
        /// <summary>
        /// 新增一条Html内容
        /// </summary>
        /// <param name="htmlInfo">htmlInfo Model</param>
        /// <returns></returns>
        public object[] InsertHtml(HtmlInfo htmlInfo)
        {
            object[] objResult = new object[] { false, "" };
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into PointRankingHtmlInfo");
                strSql.Append(" (cityId,htmlStr,status)");
                strSql.Append(" values (@cityId,@htmlStr,@status)");
                strSql.Append(" select @@identity");

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@cityId", SqlDbType.Int),
                    new SqlParameter("@htmlStr", SqlDbType.NVarChar),
                    new SqlParameter("@status", SqlDbType.Int)
                };

                paras[0].Value = htmlInfo.cityId;
                paras[1].Value = htmlInfo.html;
                paras[2].Value = htmlInfo.status;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), paras);
                    if (i > 0)
                    {
                        objResult[0] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                objResult[1] = ex.Message;
            }
            return objResult;
        }

        /// <summary>
        /// 更新一条Html内容
        /// </summary>
        /// <param name="htmlInfo">htmlInfo Model</param>
        /// <returns></returns>
        public object[] UpdateHtml(HtmlInfo htmlInfo)
        {
            object[] objResult = new object[] { false, "" };
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update PointRankingHtmlInfo set");
                strSql.Append(" htmlStr = @htmlStr");
                strSql.Append(" where cityId = @cityId");

                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@htmlStr", SqlDbType.NVarChar),
                    new SqlParameter("@cityId", SqlDbType.Int)
                };

                paras[0].Value = htmlInfo.html;
                paras[1].Value = htmlInfo.cityId;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), paras);
                    if (i > 0)
                    {
                        objResult[0] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                objResult[1] = ex.Message;
            }
            return objResult;
        }

        /// <summary>
        /// 根据城市ID删除对应的Html内容
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public object[] DeleteHtml(int cityId)
        {
            object[] objResult = new object[] { false, "" };
            try
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("update PointRankingHtmlInfo set");
                strSql.Append(" status = -1");
                strSql.Append(" where cityId = @cityId");

                SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@cityId", SqlDbType.Int)
                };

                para[0].Value = cityId;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                    if (i > 0)
                    {
                        objResult[0] = true;
                    }
                }
            }
            catch (Exception ex)
            {
                objResult[1] = ex.Message;
            }
            return objResult;
        }

        /// <summary>
        /// 根据城市ID查询对应的Html内容
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public HtmlInfo QueryHtmlByCityId(int cityId)
        {
            HtmlInfo htmlInfo = new HtmlInfo();

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select Id,cityId,htmlStr,status");
            strSql.Append(" from PointRankingHtmlInfo");
            strSql.Append(" where cityId = @cityId and status = 1");
            
            SqlParameter[] para = new SqlParameter[] { 
                    new SqlParameter("@cityId", SqlDbType.Int)
                };

            para[0].Value = cityId;

            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para))
            {
                if (sdr.Read())
                {
                    htmlInfo.id = sdr.GetInt32(0);
                    htmlInfo.cityId = sdr.GetInt32(1);
                    htmlInfo.html = sdr.GetString(2);
                    htmlInfo.status = sdr.GetInt32(3);
                }
            }
            return htmlInfo;
        }
    }
}
