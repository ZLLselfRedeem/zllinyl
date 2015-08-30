using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;
using System.Data.SqlClient;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 2013-09-03 微信管理平台 推荐餐厅
    /// </summary>
    public class WechatRecommandShopInfoManager
    {
        /// <summary>
        /// 获取所有推荐餐厅设置信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetRecommandShopInfo()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select b.cityName,a.cityID,a.shopID,c.shopName,c.shopAddress,");
            strSql.Append(" c.shopTelephone,a.recommandType,(case a.recommandType when 0 then '主推荐餐厅' ");
            strSql.Append(" when 1 then '次推荐餐厅' when 2 then '特色餐厅' end) as recommandTypeName from ");
            strSql.Append(" dbo.WechatRecommandShopInfo a inner join dbo.City b on a.cityID=b.cityID ");
            strSql.Append(" inner join dbo.ShopInfo c on a.shopID=c.shopID ");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取指定城市所有推荐餐厅设置信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetRecommandShopInfo(int cityID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select b.cityName,a.cityID,a.shopID,c.shopName,c.shopAddress,");
            strSql.Append(" c.shopTelephone,a.recommandType,(case a.recommandType when 0 then '主推荐餐厅' ");
            strSql.Append(" when 1 then '次推荐餐厅' when 2 then '特色餐厅' end) as recommandTypeName from ");
            strSql.Append(" dbo.WechatRecommandShopInfo a inner join dbo.City b on a.cityID=b.cityID ");
            strSql.Append(" inner join dbo.ShopInfo c on a.shopID=c.shopID ");
            strSql.AppendFormat("where a.cityID ={0} ", cityID);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取指定城市所有推荐餐厅设置信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetRecommandShopInfo(string cityName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select b.cityName,a.cityID,a.shopID,c.shopName,c.shopAddress,c.shopLogo,c.shopImagePath,");
            strSql.Append(" c.shopTelephone,a.recommandType,(case a.recommandType when 0 then '主推荐餐厅' ");
            strSql.Append(" when 1 then '次推荐餐厅' when 2 then '特色餐厅' end) as recommandTypeName from ");
            strSql.Append(" dbo.WechatRecommandShopInfo a inner join dbo.City b on a.cityID=b.cityID ");
            strSql.Append(" inner join dbo.ShopInfo c on a.shopID=c.shopID ");
            strSql.AppendFormat("where b.cityName = '{0}' order by a.recommandType", cityName);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 获取指定城市,指定店铺的推荐餐厅设置信息
        /// </summary>
        /// <returns></returns>
        public DataTable GetRecommandShopInfo(int cityID, int shopID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select b.cityName,a.cityID,a.shopID,c.shopName,c.shopAddress,c.shopLogo,c.shopImagePath,");
            strSql.Append(" c.shopTelephone,a.recommandType,(case a.recommandType when 0 then '主推荐餐厅' ");
            strSql.Append(" when 1 then '次推荐餐厅' when 2 then '特色餐厅' end) as recommandTypeName from ");
            strSql.Append(" dbo.WechatRecommandShopInfo a inner join dbo.City b on a.cityID=b.cityID ");
            strSql.Append(" inner join dbo.ShopInfo c on a.shopID=c.shopID ");
            strSql.AppendFormat("where a.cityID = {0} and a.shopID = {1}", cityID, shopID);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 添加推荐餐厅信息
        /// </summary>
        /// <param name="recommandShopInfo"></param>
        /// <returns></returns>
        public int Insert(WechatRecommandShopInfo recommandShopInfo)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters;

                    //假如存在当前记录，则进行更新操作


                    strSql.Append("insert into dbo.WechatRecommandShopInfo values(@shopID, ");
                    strSql.Append("@cityID,@recommandType,@operateDate,@operatorID) ");
                    parameters = new SqlParameter[] { 
                        new SqlParameter("@shopID",SqlDbType.Int),
                        new SqlParameter("@cityID",SqlDbType.Int),
                        new SqlParameter("@recommandType",SqlDbType.Int),
                        new SqlParameter("@operateDate",SqlDbType.DateTime),
                        new SqlParameter("@operatorID",SqlDbType.Int)
                    };
                    parameters[0].Value = recommandShopInfo.shopID;
                    parameters[1].Value = recommandShopInfo.cityID;
                    parameters[2].Value = (int)recommandShopInfo.recommandType;
                    parameters[3].Value = recommandShopInfo.operateDate;
                    parameters[4].Value = recommandShopInfo.operatorID;
                    
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);

                    

                }
                catch
                {
                    return 0;
                }

                if (obj == null)
                    return 0;
                else
                    return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 删除推荐餐厅信息
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public bool Delete(int shopID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("delete from dbo.WechatRecommandShopInfo ");
                    sqlStr.AppendFormat("where shopID={0}", shopID);

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, sqlStr.ToString(), null);
                }
                catch
                {
                    return false;
                }
                if (result == 1)
                    return true;
                else
                    return false;
            }
        }

        // 根据城市名称获取城市ID -暂用
        public int GetCityID(string cityName)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object result = null;
                try
                {
                    conn.Open();
                    StringBuilder sqlStr = new StringBuilder();
                    sqlStr.Append("select cityID from City ");
                    sqlStr.AppendFormat("where cityName='{0}'", cityName);

                    result = SqlHelper.ExecuteScalar(conn, CommandType.Text, sqlStr.ToString(), null);
                }
                catch
                {
                    return 0;
                }
                if (result == null)
                    return 0;
                else
                    return Convert.ToInt32(result);
            }
        }

        //根据店铺ID查询 店铺的环境图片
        public DataTable GetShopRevealImage(int shopID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.shopImagePath,b.revealImageName ");
            strSql.Append("from ShopInfo a inner join ShopRevealImage b on a.shopID = b.shopId ");
            strSql.AppendFormat("where shopId = {0} and status = 1", shopID);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
    }
}
