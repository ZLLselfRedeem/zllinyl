using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;
using System.Data;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 门店会员页面配置的文章
    /// 2014-7-11
    /// </summary>
    public class ShopVipArticleManager
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public int InsertArticle(ShopVipArticle article)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Article(City,Content,CreateTime,Enable)");
            strSql.Append("  values (@City,@Content,@CreateTime,@Enable)");
            strSql.Append(" select @@identity");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@City", SqlDbType.Int),
                new SqlParameter("@Content", SqlDbType.NVarChar, 4000),
                new SqlParameter("@CreateTime", SqlDbType.DateTime),
                new SqlParameter("@Enable", SqlDbType.Bit)
                };
                para[0].Value = article.City;
                para[1].Value = article.Content;
                para[2].Value = article.CreateTime;
                para[3].Value = article.Enable;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
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
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 保存文章中的视频
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public int InsertVideo(string name, string path)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ArticleVideo(name,path,status) values ");
            strSql.Append("(@name,@path,@status)");
            strSql.Append(" select @@identity");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@name", SqlDbType.NVarChar, 50),
                new SqlParameter("@path", SqlDbType.NVarChar, 200),
                new SqlParameter("@status", SqlDbType.Int)
                };
                para[0].Value = name;
                para[1].Value = path;
                para[2].Value = 1;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
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
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        public bool UpdateArticle(ShopVipArticle article)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update article set");
            strSql.Append(" Content = @Content");
            strSql.Append(" where City = @City");
            try
            {
                SqlParameter[] paras = new SqlParameter[] { 
                    new SqlParameter("@Content", SqlDbType.NVarChar, 4000),
                    new SqlParameter("@City", SqlDbType.Int)
                };

                paras[0].Value = article.Content;
                paras[1].Value = article.City;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), paras);
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
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public bool DeleteArtile(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update Article set enable=0");
            strSql.Append(" where City = @City");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("City", SqlDbType.Int)
                };
                para[0].Value = id;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
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
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 删除文章中视频信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteVideo(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ArticleVideo set status=-1");
            strSql.Append(" where id=@id");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("id", SqlDbType.Int)
                };
                para[0].Value = id;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
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
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 客户端根据店铺Id获取相应配置
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable ClientGetArticle(int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select Id,City,Content");
            strSql.Append(" from Article inner join ShopInfo on Article.City = ShopInfo.cityID");
            strSql.Append(" where ShopInfo.shopID=@shopID and Article.Enable=1");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@shopID", SqlDbType.Int)
                };
                para[0].Value = shopId;

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取指定城市配置
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public DataTable GetArticle(int cityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,City,Content");
            strSql.Append(" from Article where Enable=1 and City=@City");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@City", SqlDbType.Int)
                };
                para[0].Value = cityId;

                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取所有有效视频
        /// </summary>
        /// <returns></returns>
        public DataTable GetVideo()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,name,path from ArticleVideo where status = 1");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据id获取视频objectKey
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetVideoPath(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select path from ArticleVideo where status = 1 and id=@id");
            strSql.Append(" select @@identity");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@id", SqlDbType.Int)
                };
                para[0].Value = id;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
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
            catch (Exception)
            {
                return null;
            }
        }
    }
}
