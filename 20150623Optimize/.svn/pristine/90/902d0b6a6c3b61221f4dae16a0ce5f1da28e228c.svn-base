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
    /// FileName: MenuShopManager.cs 
    /// CLRVersion: 4.0.30319.269 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限菜谱店铺关系
    /// Description: 
    /// DateTime: 2012-08-17 09:51:32 
    /// </summary>
    public class MenuConnShopManager
    {
        /// <summary>
        /// 新增菜谱店铺关系信息
        /// </summary>
        /// <param name="MenuShop"></param>
        /// <returns></returns>
        public int InsertMenuConnShop(MenuConnShop menuConnShop)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into MenuConnShop(");
                    strSql.Append("menuId,companyId,shopId)");
                    strSql.Append(" values (");
                    strSql.Append("@menuId,@companyId,@shopId)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
                        new SqlParameter("@menuId", SqlDbType.Int,4),
                        new SqlParameter("@companyId", SqlDbType.Int,4),
                        new SqlParameter("@shopId",SqlDbType.Int,4)
                    };
                    parameters[0].Value = menuConnShop.menuId;
                    parameters[1].Value = menuConnShop.companyId;
                    parameters[2].Value = menuConnShop.shopId;
                    obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
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

        /// <summary>
        /// 删除菜谱店铺关系信息
        /// </summary>
        /// <param name="menuId"></param>
        public bool DeleteMenuConnShop(int menuId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("delete from MenuConnShop where menuId=@menuId");
                    SqlParameter[] parameters = {					
					new SqlParameter("@menuId", SqlDbType.Int,4)};
                    parameters[0].Value = menuId;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return false;
                }
                if (result >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 删除菜谱公司关系信息
        /// </summary>
        /// <param name="menuId"></param>
        public bool DeleteMenuConnCompany(int menuId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update MenuConnCompany set status=-1  where menuId=@menuId");
                    SqlParameter[] parameters = {					
					new SqlParameter("@menuId", SqlDbType.Int,4)};
                    parameters[0].Value = menuId;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
                }
                catch (Exception)
                {
                    return false;
                }
                if (result >= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 根据店铺Id删除其与菜谱对应关系 2014-2-14
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public bool DeleteMenuConnShopByShopId(int shopId)
        {
            int result = 0;
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                try
                {
                    conn.Open();
                    string strSql = "delete from MenuConnShop where shopId=@shopId";
                    SqlParameter[] para = new SqlParameter[] { 
                        new SqlParameter("@shopId",SqlDbType.Int)};
                    para[0].Value = shopId;
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql, para);
                }
                catch (Exception)
                {
                    return false;
                }
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

        /// <summary>
        /// 根据菜谱id，查询相应的店铺
        /// </summary>
        /// <returns></returns>
        public DataTable SelectShopsByMenuId(int menuId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [menuShopId],[menuId],[companyId],[shopId]");
            strSql.Append(" from MenuConnShop where menuId=@menuId");
            SqlParameter[] parameters = {					
					new SqlParameter("@menuId", SqlDbType.Int,4)};
            parameters[0].Value = menuId;
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据门店id，查询相应的菜谱
        /// </summary>
        /// <returns></returns>
        public DataTable SelectMenusByShopId(int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A.[menuShopId],A.[menuId],A.[companyId],A.[shopId],");
            strSql.Append(" B.[MenuID],[MenuVersion],[CreateTime],[UpdateTime],[MenuSequence],[MenuStatus],[menuImagePath],");
            strSql.Append(" [MenuI18nID],[LangID],[MenuName],[MenuDesc],[MenuI18nStatus]");
            strSql.Append(" from MenuConnShop A left join MenuInfo B on A.menuId=B.MenuID left join MenuI18n c on A.menuId=c.MenuID ");
            strSql.Append("where shopId=@shopId and MenuI18nStatus>0");
            SqlParameter[] parameters = {					
					new SqlParameter("@shopId", SqlDbType.Int,4)};
            parameters[0].Value = shopId;
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return ds.Tables[0];
        }

        public string GetMeunVersion(string menuid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select MenuVersion from MenuInfo where MenuID='{0}'", menuid);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds.Tables[0].Rows[0]["MenuVersion"].ToString();
            }
            else
            {
                return "";
            }
        }
        /*
         公司菜谱模块
         */
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(MenuConnCompany model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into MenuConnCompany(");
            strSql.Append("menuId,companyId,status)");
            strSql.Append(" values (");
            strSql.Append("@menuId,@companyId,@status)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@menuId", SqlDbType.Int,4),
					new SqlParameter("@companyId", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.Int,4)};
            parameters[0].Value = model.menuId;
            parameters[1].Value = model.companyId;
            parameters[2].Value = model.status;
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 查询当前公司所有菜谱信息
        /// </summary>
        public DataTable SelectMenuConnCompany(int companyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@" select menuCompanyId, MenuConnCompany.menuId,companyId,status ,MenuI18n.MenuName,MenuI18n.MenuDesc,MenuInfo.MenuVersion 
from MenuConnCompany inner join MenuI18n on MenuConnCompany.menuId=MenuI18n.MenuID");
            strSql.Append(" inner join MenuInfo on MenuInfo.MenuID=MenuConnCompany.menuId");
            strSql.Append(" where companyId=@companyId and status=1 and MenuI18n.MenuI18nStatus=1 and MenuInfo.MenuStatus=1");
            SqlParameter[] parameters = {
					new SqlParameter("@companyId", SqlDbType.Int,4)
			};
            parameters[0].Value = companyId;
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询当前公司菜谱信息
        /// </summary>
        public DataTable SelectMenuConnCompanyByMenuCompanyId(int menuCompanyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select menuCompanyId, MenuConnCompany.menuId,CompanyInfo.companyId,status ,MenuI18n.MenuName,MenuI18n.MenuDesc,CompanyInfo.companyName ");
            strSql.Append(" from MenuConnCompany inner join MenuI18n on MenuConnCompany.menuId=MenuI18n.MenuID");
            strSql.Append(" inner join CompanyInfo on CompanyInfo.companyID=MenuConnCompany.companyId");
            strSql.Append(" where menuCompanyId=@menuCompanyId and status=1 and MenuI18n.MenuI18nStatus=1");
            SqlParameter[] parameters = {
					new SqlParameter("@menuCompanyId", SqlDbType.Int,4)
			};
            parameters[0].Value = menuCompanyId;
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return ds.Tables[0];
        }
        public int SelectShopCurrectMenuCompanyId(int shopId)
        {
            string strSql = @"SELECT A.menuCompanyId
from MenuConnCompany A INNER JOIN MenuConnShop B on A.menuId=B.menuId
INNER JOIN MenuInfo C on C.MenuID=A.menuId
where B.shopId=@shopId and A.status=1";
            SqlParameter[] paraneters = new SqlParameter[] { 
            new  SqlParameter("@shopId",SqlDbType.Int,4)
            };
            paraneters[0].Value = shopId;
            int menuCompanyId = 0;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, paraneters))
            {
                if (dr.Read())
                {
                    menuCompanyId = dr[0] == DBNull.Value ? 0 : Convert.ToInt32(dr[0]);
                }
            }
            return menuCompanyId;
        }
    }
}
