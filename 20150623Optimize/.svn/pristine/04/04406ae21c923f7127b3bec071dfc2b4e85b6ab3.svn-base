using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using System.Data;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class WechatHotMenuManager
    {
        /// <summary>
        /// 按城市名获取设置 的dishid
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        public int GetDishIDByCityName(string cityName)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("select DishID from WechatHotMenuInfo ");
                    strSql.AppendFormat("where cityName='{0}'",cityName);

                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), null);
                }
                catch (Exception ex)
                {
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
        /// 获取热菜 按地区统计信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetHotMenuStatisticInfo()
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("select tb.*,g.DishName,e.DishPrice,f.ImageName ");
            sqlstr.Append("from (select top 5 a.DishID,a.dishSalesIn19dian as saleAmount,d.cityName,c.shopName,c.shopAddress,m.menuImagePath ");
            sqlstr.Append("from dbo.DishInfo a inner join MenuConnShop b ");
            sqlstr.Append("on a.MenuID = b.menuId inner join ShopInfo c ");
            sqlstr.Append("on b.shopId = c.shopID inner join City d ");
            sqlstr.Append("on c.cityID = d.cityID inner join MenuInfo m ");
            sqlstr.Append("on a.MenuID = m.MenuID ");
            sqlstr.Append("group by d.cityName,a.DishID,a.dishSalesIn19dian,c.shopName,c.shopAddress,m.menuImagePath ");
            sqlstr.Append("order by saleAmount desc ) as tb inner join DishPriceInfo e ");
            sqlstr.Append("on tb.DishID = e.DishID inner join DishI18n g ");
            sqlstr.Append("on tb.DishID = g.DishID inner join ImageInfo f ");
            sqlstr.Append("on tb.DishID = f.DishID where f.ImageScale = 0 ");
            sqlstr.Append("order by tb.saleAmount desc ");

            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlstr.ToString());
            return dt.Tables[0];
        }
        /// <summary>
        /// 插入热菜设置 信息
        /// </summary>
        /// <param name="hotMenuInfo"></param>
        /// <returns></returns>
        public int InsertHotMenu(WechatHotMenuInfo hotMenuInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int obj = 0;
                try
                {
                    conn.Open();
                    StringBuilder sqlStr = new StringBuilder();
                    SqlParameter[] parameters;

                    sqlStr.Append("truncate table WechatHotMenuInfo; ");
                    sqlStr.Append("insert into dbo.WechatHotMenuInfo values(@DishID, @saleAmount,");
                    sqlStr.Append("@cityName,@shopName,@shopAddress,@DishName,@DishPrice,@ImageFolder,@ImageName,@setDateTime,@operaterID) ");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@DishID",SqlDbType.Int),
                        new SqlParameter("@saleAmount",SqlDbType.BigInt),
                        new SqlParameter("@cityName",SqlDbType.NVarChar),
                        new SqlParameter("@shopName",SqlDbType.NVarChar),
                        new SqlParameter("@shopAddress",SqlDbType.NVarChar),
                        new SqlParameter("@DishName",SqlDbType.NVarChar),
                        new SqlParameter("@DishPrice",SqlDbType.NVarChar),
                        new SqlParameter("@ImageFolder",SqlDbType.NVarChar),
                        new SqlParameter("@ImageName",SqlDbType.NVarChar),
                        new SqlParameter("@setDateTime",SqlDbType.NVarChar),
                        new SqlParameter("@operaterID",SqlDbType.Int)
                    };
                    parameters[0].Value = hotMenuInfo.DishID;
                    parameters[1].Value = hotMenuInfo.saleAmount;
                    parameters[2].Value = hotMenuInfo.cityName;
                    parameters[3].Value = hotMenuInfo.shopName;
                    parameters[4].Value = hotMenuInfo.shopAddress;
                    parameters[5].Value = hotMenuInfo.DishName;
                    parameters[6].Value = hotMenuInfo.DishPrice;
                    parameters[7].Value = hotMenuInfo.ImageFolder;
                    parameters[8].Value = hotMenuInfo.ImageName;
                    parameters[9].Value = hotMenuInfo.setDateTime;
                    parameters[10].Value = hotMenuInfo.operaterID;

                    obj = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sqlStr.ToString(), parameters);
                }
                catch
                {
                    return 0;
                }

                return obj;
            }
        }

        //获取设置的热菜信息 微信客户端用
        public DataTable GetHopMenuInfo()
        {
            string sqlstr = "select cityName,shopName,shopAddress,DishName,DishPrice,ImageFolder,ImageName,DishID,saleAmount from WechatHotMenuInfo";

            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlstr);
            return dt.Tables[0];
        }

        //获取设置的热菜信息 微信客户端用
        public DataTable GetHopMenuInfo(int DishID)
        {
            string sqlstr = "select cityName,shopName,shopAddress,DishName,DishPrice,ImageFolder,ImageName,DishID,saleAmount from WechatHotMenuInfo where DishID=" + DishID;

            DataSet dt = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sqlstr);
            return dt.Tables[0];
        }
    }
}
