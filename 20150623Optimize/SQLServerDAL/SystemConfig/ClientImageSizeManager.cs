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
    /// <summary>
    /// 客户端图片处理参数：数据处理层
    /// 创建日期：2014-5-29
    /// </summary>
    public class ClientImageSizeManager
    {
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="clientImageSize"></param>
        /// <returns></returns>
        public int InsertClientImageSize(ClientImageSize clientImageSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into ClientImageSize(");
            strSql.Append("appType,screenWidth,imageType,value,status)");
            strSql.Append(" values (@appType,@screenWidth,@imageType,@value,@status)");
            strSql.Append(" select @@identity");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@appType",SqlDbType.Int),
                new SqlParameter("@screenWidth",SqlDbType.NVarChar,50),
                new SqlParameter("@imageType",SqlDbType.Int,4),
                new SqlParameter("@value",SqlDbType.NVarChar,200),
                new SqlParameter("@status",SqlDbType.Int)
                };
                para[0].Value = clientImageSize.apptype;
                para[1].Value = clientImageSize.screenWidth;
                para[2].Value = clientImageSize.imageType;
                para[3].Value = clientImageSize.value;
                para[4].Value = clientImageSize.status;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    object result = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), para);
                    if (result == null)
                    {
                        return 0;
                    }
                    else
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// 更新某图片尺寸相关设置
        /// </summary>
        /// <param name="clientImageSize"></param>
        /// <returns></returns>
        public bool UpdateClientImageSize(ClientImageSize clientImageSize)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update ClientImageSize set ");
            strSql.Append("appType=@appType,");
            strSql.Append("screenWidth=@screenWidth,");
            strSql.Append("imageType=@imageType,");
            strSql.Append("value=@value");
            strSql.Append(" where id=@id");
            try
            {
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@appType",SqlDbType.Int),
                new SqlParameter("@screenWidth",SqlDbType.NVarChar,50),
                new SqlParameter("@imageType",SqlDbType.Int,4),
                new SqlParameter("@value",SqlDbType.NVarChar,200),
                new SqlParameter("@id",SqlDbType.Int)
                };
                para[0].Value = clientImageSize.apptype;
                para[1].Value = clientImageSize.screenWidth;
                para[2].Value = clientImageSize.imageType;
                para[3].Value = clientImageSize.value;
                para[4].Value = clientImageSize.id;

                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), para);
                    if (i == 1)
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
        /// 删除某个图片尺寸的设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeleteClientImageSize(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("update ClientImageSize set status = -1 where id='{0}' ", id);
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    int i = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString());
                    if (i == 1)
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
        /// 根据条件查询对应的图片处理参数
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="appType">设备类别</param>
        /// <param name="imageType">图片类别</param>
        /// <param name="screenWidth">屏幕宽度</param>
        /// <returns></returns>
        public DataTable QueryClientImageSize(int id, int appType, string imageType, string screenWidth)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,appType,screenWidth,imageType,value,status");
            strSql.Append(" from ClientImageSize");
            strSql.Append(" where status = 1");
            List<SqlParameter> paraList = new List<SqlParameter>();
            if (id > 0)
            {
                strSql.Append(" and id =@id");
                paraList.Add(new SqlParameter("@id", SqlDbType.Int) { Value = id });
            }
            if (appType > 0)
            {
                strSql.Append(" and appType =@appType");
                paraList.Add(new SqlParameter("@appType", SqlDbType.Int) { Value = appType });
            }
            if (!string.IsNullOrEmpty(imageType))
            {
                strSql.Append(" and imageType =@imageType");
                paraList.Add(new SqlParameter("@imageType", SqlDbType.Int) { Value = Convert.ToInt32(imageType) });
            }
            if (!string.IsNullOrEmpty(screenWidth))
            {
                strSql.Append(" and screenWidth =@screenWidth");
                paraList.Add(new SqlParameter("@screenWidth", SqlDbType.NVarChar, 50) { Value = screenWidth });
            }
            SqlParameter[] para = paraList.ToArray();
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);
            return ds.Tables[0];
        }

        public List<ClientImageSize> QueryClientImageSize()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,appType,screenWidth,imageType,value,status");
            strSql.Append(" from ClientImageSize");
            strSql.Append(" where status = 1");
            List<ClientImageSize> imageSize = new List<ClientImageSize>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString()))
            {
                while (sdr.Read())
                {
                    imageSize.Add(sdr.GetEntity<ClientImageSize>());
                }
            }
            return imageSize;
        }
    }
}
