using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.DBUtility;
using System.Data.SqlClient;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class ImageInfoManager
    {
        //查询批量传上来的图
        public DataTable GetBatchImageInfo(uint pageIndex)
        {
            StringBuilder strSql = new StringBuilder();
            if (pageIndex == 1)
            {
                strSql.Append("select top 6 DishID,ImageName from ImageInfo");
                strSql.Append(" where ImageStatus = -8");
            }
            else if (pageIndex > 1)
            {
                strSql.AppendFormat("select top {0} DishID,ImageName from ImageInfo where ", 6 * pageIndex);
                strSql.AppendFormat("ImageID not in (select top {0} ImageID from ImageInfo where ImageStatus = -8) ", 6 * (pageIndex - 1));
                strSql.Append("and ImageStatus = -8 ");
            }
            try
            {
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

                return ds.Tables[0];
            }
            catch
            { return null; }
        }

        //查询图片总数量
        public int GetBatchImageCount()
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select count(ImageID) from ImageInfo where ImageStatus = -8");

                    //1、插入市信息表信息
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), null);
                }
                catch
                {
                    return 0;
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

        //删除菜品图片 , 状态置－1
        public bool DeleteDishImage(int DishID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("update ImageInfo set ");
                    strSql.Append("ImageStatus=-1");
                    strSql.Append(" where DishID=@cityID");
                    parameters = new SqlParameter[]{
                    new SqlParameter("@cityID", SqlDbType.Int,4)};
                    parameters[0].Value = DishID;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return false;
                }
                if (result == 1)
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
}
