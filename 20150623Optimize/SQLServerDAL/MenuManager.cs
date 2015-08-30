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
    public class MenuManager
    {
        public MenuInfo GetMenuInfoByMenuId(int menuId)
        {
            string cmdText = "SELECT [MenuID],[MenuVersion],[CreateTime],[UpdateTime],[MenuSequence],[MenuStatus],[menuImagePath] FROM [dbo].[MenuInfo] WHERE [MenuID]=@MenuID";
            SqlParameter cmdParm = new SqlParameter("@MenuID", SqlDbType.Int);
            cmdParm.Value = menuId;
            try
            {
                using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.Clear();
                    cmd.Parameters.Add(cmdParm);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            MenuInfo menuInfo = new MenuInfo
                            {
                                MenuID = dr.GetInt32(0),
                                MenuVersion = dr.GetInt32(1),
                                CreateTime = dr.GetDateTime(2),
                                UpdateTime = dr.GetDateTime(3),
                                MenuSequence = dr.GetInt32(4),
                                MenuStatus = dr.GetInt32(5),
                                menuImagePath = dr.GetString(6)
                            };

                            return menuInfo;
                        }
                    }
                }
            }
            catch
            {

            }
            return null;
        }


        /// <summary>
        /// 新增菜单信息
        /// </summary>
        /// <param name="menu"></param>
        public int InsertMenu(MenuInfo menu)
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
                    strSql.Append("insert into MenuInfo(");
                    strSql.Append("MenuVersion,CreateTime,UpdateTime,MenuStatus,MenuSequence,menuImagePath)");
                    strSql.Append(" values (");
                    strSql.Append("@MenuVersion,@CreateTime,@UpdateTime,@MenuStatus,@MenuSequence,@menuImagePath)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					new SqlParameter("@MenuVersion", SqlDbType.Int,4),
					new SqlParameter("@CreateTime", SqlDbType.DateTime),
					new SqlParameter("@UpdateTime",SqlDbType.DateTime),
                    new SqlParameter("@MenuStatus",SqlDbType.Int,4),
                    new SqlParameter("@MenuSequence",SqlDbType.Int,4),
                    new SqlParameter("@menuImagePath",SqlDbType.NVarChar,500)
                    };
                    parameters[0].Value = 1;
                    parameters[1].Value = menu.CreateTime;
                    parameters[2].Value = menu.UpdateTime;
                    parameters[3].Value = menu.MenuStatus;
                    parameters[4].Value = menu.MenuSequence;
                    parameters[5].Value = menu.menuImagePath;
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
        /// 新增菜单多语言信息
        /// </summary>
        /// <param name="menu"></param>
        public int InsertMenuI18n(MenuI18n menu)
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
                    strSql.Append("insert into MenuI18n(");
                    strSql.Append("MenuID,LangID,MenuName,MenuDesc,MenuI18nStatus)");
                    strSql.Append(" values (");
                    strSql.Append("@MenuID,@LangID,@MenuName,@MenuDesc,@MenuI18nStatus)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					new SqlParameter("@MenuID", SqlDbType.Int,4),
					new SqlParameter("@LangID", SqlDbType.Int,4),
					new SqlParameter("@MenuName",SqlDbType.NVarChar,50),
                    new SqlParameter("@MenuDesc",SqlDbType.NVarChar,500),
                    new SqlParameter("@MenuI18nStatus",SqlDbType.Int,4)};
                    parameters[0].Value = menu.MenuID;
                    parameters[1].Value = menu.LangID;
                    parameters[2].Value = menu.MenuName;
                    parameters[3].Value = menu.MenuDesc;
                    parameters[4].Value = menu.MenuI18nStatus;

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
        /// 删除菜单信息（及对应的多语言信息）
        /// </summary>
        /// <param name="menuID"></param>
        public bool DeleteMenuByID(int menuID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {

                SqlTransaction tran = null;
                int result = 0;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("update MenuInfo set MenuStatus = '-1' where MenuID=@menuID;");
                    strSql.Append("update MenuI18n set MenuI18nStatus = '-1' where MenuID=@menuID;");

                    SqlParameter[] parameters = {					
					new SqlParameter("@menuID", SqlDbType.Int,4)};
                    parameters[0].Value = menuID;

                    result = SqlHelper.ExecuteNonQuery(tran, CommandType.Text, strSql.ToString(), parameters);

                    tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
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
        /// 删除菜单多语言信息
        /// </summary>
        /// <param name="menuI18nID"></param>
        public void DeleteMenuI18nByID(int menuI18nID)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {

                SqlTransaction tran = null;
                try
                {
                    conn.Open();
                    tran = conn.BeginTransaction();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("update MenuI18n set MenuI18nStatus = '-1' where MenuI18nID=@menuI18nID;");

                    SqlParameter[] parameters = {					
					new SqlParameter("@menuI18nID", SqlDbType.Int,4)};
                    parameters[0].Value = menuI18nID;

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
        /// 修改菜单信息
        /// </summary>
        /// <param name="menu"></param>
        public bool UpdateMenu(MenuInfo menu)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update MenuInfo set ");
                    strSql.Append("UpdateTime=@UpdateTime,");
                    strSql.Append("MenuStatus=@MenuStatus,");
                    strSql.Append("MenuSequence=@MenuSequence");
                    strSql.Append(" where MenuID=@MenuID ");
                    SqlParameter[] parameters = {
					new SqlParameter("@UpdateTime",SqlDbType.DateTime),
					new SqlParameter("@MenuStatus", SqlDbType.Int,4),
                    new SqlParameter("@MenuSequence", SqlDbType.Int,4),
                    new SqlParameter("@MenuID",SqlDbType.Int,4)};

                    parameters[0].Value = menu.UpdateTime;
                    parameters[1].Value = menu.MenuStatus;
                    parameters[2].Value = menu.MenuSequence;
                    parameters[3].Value = menu.MenuID;

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
        /// <summary>
        /// new根据菜谱编号修改菜谱版本和修改时间
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public bool UpdateMenuVersionAndTime(int menuId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update MenuInfo set ");
                    strSql.Append("UpdateTime = @UpdateTime,");
                    strSql.Append("MenuVersion = isnull(MenuVersion, 0) + 1");
                    strSql.Append(" where MenuID = @MenuID");
                    SqlParameter[] parameters = {                   
                    new SqlParameter("@UpdateTime", SqlDbType.DateTime),
					new SqlParameter("@MenuID", SqlDbType.Int,4)};

                    parameters[0].Value = System.DateTime.Now;
                    parameters[1].Value = menuId;

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
        /// <summary>
        /// 修改菜单多语言信息
        /// </summary>
        /// <param name="menu"></param>
        public bool UpdateMenuI18n(MenuI18n menu)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update MenuI18n set ");
                    strSql.Append("MenuID=@MenuID,");
                    strSql.Append("LangID=@LangID,");
                    strSql.Append("MenuName=@MenuName,");
                    strSql.Append("MenuDesc=@MenuDesc,");
                    strSql.Append("MenuI18nStatus=@MenuI18nStatus");
                    strSql.Append(" where MenuI18nID=@MenuI18nID ");
                    SqlParameter[] parameters = {
					new SqlParameter("@MenuID", SqlDbType.Int,4),
					new SqlParameter("@LangID", SqlDbType.Int,4),
					new SqlParameter("@MenuName",SqlDbType.NVarChar,50),
					new SqlParameter("@MenuDesc", SqlDbType.NVarChar,500),
                    new SqlParameter("@MenuI18nStatus", SqlDbType.Int,4),
                    new SqlParameter("@MenuI18nID",SqlDbType.Int,4)};
                    parameters[0].Value = menu.MenuID;
                    parameters[1].Value = menu.LangID;
                    parameters[2].Value = menu.MenuName;
                    parameters[3].Value = menu.MenuDesc;
                    parameters[4].Value = menu.MenuI18nStatus;
                    parameters[5].Value = menu.MenuI18nID;

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
        /// <summary>
        /// 修改版本信息（VersionInfo表）
        /// </summary>
        /// <param name="version"></param>
        public void UpdateVersion(VersionInfo version)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update VersionInfo set ");
            strSql.Append("POSLiteVersion=@POSLiteVersion,");
            strSql.Append("SoftwareVersion=@SoftwareVersion,");
            strSql.Append("UpdateTime=@UpdateTime");
            strSql.Append(" where 1=1");
            SqlParameter[] parameters = {
					new SqlParameter("@POSLiteVersion", SqlDbType.NVarChar,50),
					new SqlParameter("@SoftwareVersion", SqlDbType.NVarChar,50),
                    new SqlParameter("@UpdateTime",SqlDbType.DateTime)};
            parameters[0].Value = version.POSLiteVersion;
            parameters[1].Value = version.SoftwareVersion;
            parameters[2].Value = version.UpdateTime;

            SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
        }
        /// <summary>
        /// 修改注册信息（VersionInfo表）
        /// </summary>
        public bool UpdateRegisterInfo(string currentTime, string activationCode)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update VersionInfo set ");
            strSql.Append("currentTime=@currentTime,");
            strSql.Append("activationCode=@activationCode");
            strSql.Append(" where 1=1");
            SqlParameter[] parameters = {
                    new SqlParameter("@currentTime", SqlDbType.NVarChar,500),
					new SqlParameter("@activationCode", SqlDbType.NVarChar,500)};
            parameters[0].Value = currentTime;
            parameters[1].Value = activationCode;


            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改当前时间（VersionInfo表）
        /// </summary>
        public bool UpdateCurrentTime(string currentTime)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update VersionInfo set ");
            strSql.Append("currentTime=@currentTime");
            strSql.Append(" where 1=1");
            SqlParameter[] parameters = {
                    new SqlParameter("@currentTime", SqlDbType.NVarChar,500)};
            parameters[0].Value = currentTime;


            int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 查询菜单信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryMenu()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [MenuInfo].[MenuID],[MenuVersion],[CreateTime],[UpdateTime],[MenuSequence],[MenuStatus],[menuImagePath],");
            strSql.Append("[MenuI18nID],[LangID],[MenuName],[MenuDesc],[MenuI18nStatus]");
            strSql.Append(" from [MenuInfo],[MenuI18n]");
            strSql.Append(" where [MenuInfo].MenuID = [MenuI18n].MenuID ");
            //strSql.Append(" (select LanguageInfo.LangID from LanguageInfo where LanguageInfo.IsDefaultLang = 'true')");and [MenuI18n].LangID =
            //xiaoyu 2011-9-2 13:52  不限制查询的语言，由使用时外部自己在datable中过滤
            strSql.Append(" and [MenuInfo].MenuStatus > '0'");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 查询有审核门店在使用的菜单信息
        /// </summary>
        /// <returns></returns>
        public DataTable SelectMenuForShopHandled()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DISTINCT(A.[MenuID]),A.[menuImagePath]");
            strSql.Append(" from [MenuInfo] as A");
            strSql.Append(" inner join [MenuConnShop] as B on B.[menuId] = A.[MenuID]");
            strSql.Append(" inner join [ShopInfo] as C on C.[shopID] = B.[shopId]");
            strSql.AppendFormat(" where C.isHandle ={0} and A.MenuStatus > 0 and C.shopStatus > 0", (int)VAShopHandleStatus.SHOP_Pass);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 根据菜单编号查询菜单信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryMenu(int menuId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [MenuInfo].[MenuID],[MenuVersion],[CreateTime],[UpdateTime],[MenuSequence],[MenuStatus],[menuImagePath],");
            strSql.Append("[MenuI18nID],[LangID],[MenuName],[MenuDesc],[MenuI18nStatus]");
            strSql.Append(" from [MenuInfo],[MenuI18n]");
            strSql.Append(" where [MenuInfo].MenuID = [MenuI18n].MenuID and [MenuInfo].MenuID = @menuId");
            strSql.Append(" and [MenuInfo].MenuStatus > 0");
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@menuId", SqlDbType.Int) { Value = menuId }
            };

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), para);

            return ds.Tables[0];
        }
        /// <summary>
        /// 根据菜单编号查询菜单信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public DataTable SelectMenu(int menuId)
        {
            const string strSql = @"select A.MenuID,A.MenuVersion,A.menuImagePath,B.MenuName 
 from MenuInfo as A inner join MenuI18n as B on A.MenuID = B.MenuID inner join LanguageInfo as C on B.LangID = C.LangID
 where C.IsDefaultLang =1 and A.MenuStatus =1 and A.MenuID = @MenuID";
            SqlParameter[] parameter = new SqlParameter[] 
            {
                new SqlParameter("@MenuID",SqlDbType.Int,4)
            };
            parameter[0].Value = menuId;
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter);
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据菜单编号查询菜单版本号
        /// </summary>
        /// <returns></returns>
        public int SelectMenuVersion(int menuId)
        {
            const string strSql = "select A.MenuVersion from [MenuInfo] as A  where A.MenuID =@menuId and A.MenuStatus > 0";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@menuId", SqlDbType.Int) { Value = menuId }
            };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);

            if (ds.Tables[0].Rows.Count == 1)
            {
                return Convert.ToInt32(ds.Tables[0].Rows[0]["MenuVersion"]);
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// 根据菜谱编号查询对应的公司信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public DataTable SelectCompanyByMenu(int menuId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Top 1 B.companyID,B.companyName");
            strSql.Append(" from [MenuConnShop] as A inner join [CompanyInfo] as B on A.companyId  =B.companyID");
            strSql.AppendFormat(" where B.companyStatus > '0' and A.menuId = {0} order by A.shopId asc", menuId);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 根据菜谱编号查询对应门店信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public DataTable SelectShopByMenu(int menuId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select A.shopId,B.shopName");
            strSql.Append(" from [MenuConnShop] as A inner join [ShopInfo] as B on A.shopId  =B.shopID");
            strSql.AppendFormat(" where B.shopStatus > '0' and A.menuId = {0} and B.isHandle='" + (int)VAShopHandleStatus.SHOP_Pass + "' order by A.shopId asc", menuId);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 根据店铺编号查询菜单信息
        /// </summary>
        /// <param name="shopId">门店编号</param>
        /// <returns></returns>
        public DataTable SelectMenuByShop(int shopId)
        {
            const string strSql = @"select A.MenuID,A.MenuVersion,A.menuImagePath,B.MenuName from MenuInfo as A inner join MenuI18n as B on A.MenuID = B.MenuID
 inner join LanguageInfo as C on B.LangID = C.LangID inner join MenuConnShop as D on D.menuId = A.MenuID inner join ShopInfo as E on E.shopID = D.shopId
 where E.shopID = @shopID and C.IsDefaultLang = 1 and A.MenuStatus > 0 and E.shopStatus > 0";
            SqlParameter[] paramater = new SqlParameter[] { new SqlParameter("@shopID", shopId) };
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), paramater);
            return ds.Tables[0];
        }
        /// <summary>
        /// 查询当前菜谱菜品点赞次数
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public List<DishPraiseInfo> SelectDishPraiseNumByMenu(int menuId)
        {
            const string strSql = @"select dishPraiseNum,DishID,isnull(dishSalesIn19dian,0) dishSalesIn19dian from DishInfo 
where MenuID=@menuId and DishStatus=1 and dishPraiseNum>0 and isnull(dishSalesIn19dian,0)>0";
            var list = new List<DishPraiseInfo>();
            SqlParameter[] paramater = new SqlParameter[] { new SqlParameter("@menuId", menuId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, paramater))
            {
                while (dr.Read())
                {
                    list.Add(new DishPraiseInfo()
                    {
                        dishId = Convert.ToInt32(dr["DishID"]),
                        orderDishPraiseNum = Convert.ToInt32(dr["dishPraiseNum"]),
                        orderDishSaleCount = Convert.ToInt32(dr["dishSalesIn19dian"])
                    });
                }
            }
            return list;
        }

        /// <summary>
        /// 根据菜单多语言编号查询菜单编号
        /// </summary>
        /// <param name="menuI18nID"></param>
        /// <returns></returns>
        public int QueryMenuID(int menuI18nID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MenuID");
            strSql.Append(" from MenuI18n");
            strSql.AppendFormat(" where MenuI18nID = '{0}' and MenuI18nStatus > '0'", menuI18nID);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }
        /// <summary>
        /// 查询总的菜单版本号
        /// </summary>
        /// <returns></returns>
        //public int QueryVersion()
        //{
        //    StringBuilder strSql = new StringBuilder();
        //    strSql.Append("select RestVersion");
        //    strSql.Append(" from VersionInfo");

        //    DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
        //    return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        //}
        /// <summary>
        /// 查询注册信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryRegisterInfo()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [POSLiteVersion],[SoftwareVersion],[UpdateTime],[downloadURL],[POSLiteUpdatePackageName]");
            strSql.Append(" from VersionInfo");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 查询某个菜谱下面菜的所有图片
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="imageScale">0,大图；1，小图</param>
        /// <returns></returns>
        public DataTable SelectMenuImages(int menuId, int imageScale)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select [ImageID],A.[DishID],[ImageName],[ImageSequence],[ImageScale],[ImageStatus] from ImageInfo A left join  DishInfo B  on A.DishID=B.DishID where B.MenuID={0} and ImageStatus>0 and ImageScale={1}", menuId, imageScale);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 根据员工编号查询对应的菜谱信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public DataTable SelectMenuByEmployee(int employeeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select DISTINCT(D.MenuID),D.MenuName from EmployeeConnShop as A");
            strSql.Append(" inner join dbo.ShopInfo as B on A.shopID = B.shopID");
            strSql.Append(" inner join dbo.MenuConnShop as C on C.shopId = B.shopID");
            strSql.Append(" inner join dbo.MenuI18n as D on C.menuId = D.MenuID");
            strSql.AppendFormat(" where A.employeeID = {0} and B.isHandle = '" + (int)VAShopHandleStatus.SHOP_Pass + "' and B.shopStatus > 0", employeeId);
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return ds.Tables[0];
        }
        /// <summary>
        /// 删除菜谱以及对应的菜和分类信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public bool DeleteMenuAndDish(int menuId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();

                    strSql.Append("update DishConnType set DishConnTypeStatus = -1 where DishID in(select DishID from  dbo.DishInfo where MenuID = @menuID);");

                    strSql.Append("update DishTypeI18n set DishTypeI18nStatus = -1 where DishTypeID in(select DishTypeID from dbo.DishTypeInfo where MenuID = @menuID);");

                    strSql.Append("update DishTypeInfo set DishTypeStatus = -1 where MenuID = @menuID;");

                    strSql.Append("update DishPriceI18n set DishPriceI18nStatus = -1 where DishPriceID in(select DishPriceID from dbo.DishPriceInfo where DishID in( select DishID from dbo.DishInfo where MenuID = @menuID));");

                    strSql.Append("update DishPriceInfo set DishPriceStatus = -1 where DishID in( select DishID from dbo.DishInfo where MenuID = @menuID);");

                    strSql.Append("update DishI18n set DishI18nStatus = -1 where DishID in( select DishID from dbo.DishInfo where MenuID = @menuID);");

                    strSql.Append("update ImageInfo set ImageStatus = -1 where DishID in( select DishID from dbo.DishInfo where MenuID = @menuID);");

                    strSql.Append("update DishInfo set DishStatus = -1 where MenuID = @menuID;");

                    strSql.Append("update MenuInfo set MenuStatus = -1 where MenuID =@menuID;");

                    strSql.Append("update MenuI18n set MenuI18nStatus = -1 where MenuID =@menuID;");

                    strSql.Append("delete from dbo.MenuConnShop where MenuID =@menuID;");

                    SqlParameter[] parameters = {					
					new SqlParameter("@menuID", SqlDbType.Int,4)};
                    parameters[0].Value = menuId;

                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);

                }
                catch
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
        /// 收银宝后台检测当前公司的菜谱名称是否存在
        /// </summary>
        /// <param name="menuName"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public bool BoolCheckShopMenuName(string menuName, int companyId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT COUNT(A.MenuI18nID) from MenuI18n A");
            strSql.Append(" INNER JOIN MenuConnCompany B on A.MenuID=B.menuId");
            strSql.Append(" where A.MenuI18nStatus=1 and B.status=1 and A.MenuName=@menuName and B.companyId=@companyId");
            SqlParameter[] parameter = { 
                                       new  SqlParameter("@menuName",menuName),
                                        new  SqlParameter("@companyId",companyId)
                                       };
            bool result = false;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameter))
            {
                if (dr.Read())
                {
                    result = Convert.ToInt32(dr[0]) > 0 ? true : false;
                }
            }
            return result;
        }
    }
}
