using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class DishTypeManager
    {
        /// <summary>
        /// 新增菜显示分类（SYB共用）
        /// </summary>
        /// <param name="dishType"></param>
        public int InsertDishType(DishTypeInfo dishType)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into DishTypeInfo(");
                    strSql.Append("DishTypeSequence,MenuID,DishTypeStatus)");
                    strSql.Append(" values (");
                    strSql.Append("@DishTypeSequence,@MenuID,@DishTypeStatus)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					    new SqlParameter("@DishTypeSequence", SqlDbType.Int,4),
					    new SqlParameter("@MenuID", SqlDbType.Int,4),
					    new SqlParameter("@DishTypeStatus", SqlDbType.Int,4)};
                    parameters[0].Value = dishType.DishTypeSequence;
                    parameters[1].Value = dishType.MenuID;
                    parameters[2].Value = dishType.DishTypeStatus;
                    //1、插入菜分类表信息
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
        /// 新增折扣分类
        /// </summary>
        /// <param name="discountType"></param>
        public int InsertDiscountType(DiscountType discountType)
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
                    strSql.Append("insert into DiscountType(");
                    strSql.Append("DiscountTypeName,DiscountTypeStatus,financialTypeID,PrinterName,discountTypeOrderPrinter,discountTypeOrderCopy,printdiscountTypeOrder,cookOrderStyle,cookOrderCopy)");
                    strSql.Append(" values (");
                    strSql.Append("@DiscountTypeName,@DiscountTypeStatus,@financialTypeID,@PrinterName,@discountTypeOrderPrinter,@discountTypeOrderCopy,@printdiscountTypeOrder,@cookOrderStyle,@cookOrderCopy)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					    new SqlParameter("@DiscountTypeName", SqlDbType.NVarChar,50),
					    new SqlParameter("@DiscountTypeStatus", SqlDbType.Int,4),
                        new SqlParameter("@financialTypeID", SqlDbType.Int,4),
                        new SqlParameter("@PrinterName", SqlDbType.NVarChar,50),
                        new SqlParameter("@discountTypeOrderPrinter", SqlDbType.NVarChar,50),
                        new SqlParameter("@discountTypeOrderCopy", SqlDbType.Int,4),
                        new SqlParameter("@printdiscountTypeOrder", SqlDbType.Int,4),
                        new SqlParameter("@cookOrderStyle", SqlDbType.Int,4),
                        new SqlParameter("@cookOrderCopy", SqlDbType.Int,4)
                    };
                    parameters[0].Value = discountType.DiscountTypeName;
                    parameters[1].Value = discountType.DiscountTypeStatus;
                    parameters[2].Value = discountType.financialTypeID;
                    parameters[3].Value = discountType.PrinterName;
                    parameters[4].Value = discountType.discountTypeOrderPrinter;
                    parameters[5].Value = discountType.discountTypeOrderCopy;
                    parameters[6].Value = discountType.printdiscountTypeOrder;
                    parameters[7].Value = discountType.cookOrderStyle;
                    parameters[8].Value = discountType.cookOrderCopy;
                    //1、插入折扣分类表信息
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
        /// 新增菜分类多语言（SYB共用）
        /// </summary>
        /// <param name="dishType"></param>
        public int InsertDishTypeI18n(DishTypeI18n dishType)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                Object obj = null;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    SqlParameter[] parameters = null;
                    strSql.Append("insert into DishTypeI18n(");
                    strSql.Append("LangID,DishTypeID,DishTypeName,DishTypeI18nStatus)");
                    strSql.Append(" values (");
                    strSql.Append("@LangID,@DishTypeID,@DishTypeName,@DishTypeI18nStatus)");
                    strSql.Append(" select @@identity");
                    parameters = new SqlParameter[]{
					    new SqlParameter("@LangID", SqlDbType.Int,4),
					    new SqlParameter("@DishTypeID", SqlDbType.Int,4),
					    new SqlParameter("@DishTypeName", SqlDbType.NVarChar,50),
                        new SqlParameter("@DishTypeI18nStatus",SqlDbType.Int,4)};
                    parameters[0].Value = dishType.LangID;
                    parameters[1].Value = dishType.DishTypeID;
                    parameters[2].Value = dishType.DishTypeName;
                    parameters[3].Value = dishType.DishTypeI18nStatus;
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
        /// 删除菜分类
        /// </summary>
        /// <param name="dishTypeID"></param>
        public bool DeleteDishType(int dishTypeID)
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

                    strSql.Append("update DishTypeInfo set DishTypeStatus = '-1' where DishTypeID=@dishTypeID;");
                    strSql.Append("update DishTypeI18n set DishTypeI18nStatus = '-1' where DishTypeID=@dishTypeID;");

                    SqlParameter[] parameters = {					
					new SqlParameter("@dishTypeID", SqlDbType.Int,4)};
                    parameters[0].Value = dishTypeID;

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
        /// 删除折扣分类
        /// </summary>
        /// <param name="discountType"></param>
        public bool DeleteDiscountType(int discountTypeID, ref string error)
        {
            //参数
            SqlParameter[] parameters = {					
					new SqlParameter("@DiscountTypeID", SqlDbType.Int,4)};
            parameters[0].Value = discountTypeID;

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DiscountTypeID from DishInfo  where DiscountTypeID=@DiscountTypeID");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                error = "当前已经有菜品选择了该分类，请先处理菜品，再来删除此分类";
                return false;
            }
            else
            {

                strSql.Clear();
                strSql.Append("update DiscountType set DiscountTypeStatus = '-1' where DiscountTypeID=@DiscountTypeID");
                int result = SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
                if (result > 0)
                {
                    error = "";
                    return true;

                }
                else
                {
                    error = "删除失败";
                    return false;
                }
            }
        }
        /// <summary>
        /// 修改菜分类（SYB共用）
        /// </summary>
        /// <param name="dishType"></param>
        public bool UpdateDishType(DishTypeInfo dishType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DishTypeInfo set ");
            strSql.Append("DishTypeSequence=@DishTypeSequence,");
            strSql.Append("MenuID=@MenuID,");
            strSql.Append("DishTypeStatus=@DishTypeStatus");
            strSql.Append(" where DishTypeID=@DishTypeID ");
            SqlParameter[] parameters = {
                        new SqlParameter("@DishTypeSequence", SqlDbType.Int,4),
					    new SqlParameter("@MenuID", SqlDbType.Int,4),
					    new SqlParameter("@DishTypeStatus", SqlDbType.Int,4),
                        new SqlParameter("@DishTypeID",SqlDbType.Int,4)};
            parameters[0].Value = dishType.DishTypeSequence;
            parameters[1].Value = dishType.MenuID;
            parameters[2].Value = dishType.DishTypeStatus;
            parameters[3].Value = dishType.DishTypeID;

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
        /// 修改折扣分类
        /// </summary>
        /// <param name="discountType"></param>
        public bool UpdateDiscountType(DiscountType discountType)
        {
            StringBuilder strSql = new StringBuilder();
            SqlParameter[] parameters = null;
            strSql.Append("update DiscountType set ");
            strSql.Append("DiscountTypeName=@DiscountTypeName,");
            strSql.Append("DiscountTypeStatus=@DiscountTypeStatus,");
            strSql.Append("financialTypeID=@financialTypeID,");
            strSql.Append("PrinterName=@PrinterName,");
            strSql.Append("discountTypeOrderPrinter=@discountTypeOrderPrinter,");
            strSql.Append("discountTypeOrderCopy=@discountTypeOrderCopy,");
            strSql.Append("printdiscountTypeOrder=@printdiscountTypeOrder,");
            strSql.Append("cookOrderStyle=@cookOrderStyle,");
            strSql.Append("cookOrderCopy=@cookOrderCopy");
            strSql.Append(" where DiscountTypeID=@DiscountTypeID ");
            parameters = new SqlParameter[]{
					    new SqlParameter("@DiscountTypeName", SqlDbType.NVarChar,50),
					    new SqlParameter("@DiscountTypeStatus", SqlDbType.Int,4),
                        new SqlParameter("@financialTypeID", SqlDbType.Int,4),
                        new SqlParameter("@PrinterName", SqlDbType.NVarChar,50),
                        new SqlParameter("@discountTypeOrderPrinter", SqlDbType.NVarChar,50),
                        new SqlParameter("@discountTypeOrderCopy", SqlDbType.Bit),
                        new SqlParameter("@printdiscountTypeOrder", SqlDbType.Int,4),
                        new SqlParameter("@cookOrderStyle", SqlDbType.Int,4),
                        new SqlParameter("@cookOrderCopy", SqlDbType.Int,4),
                        new SqlParameter("@DiscountTypeID", SqlDbType.Int,4)
                    };
            parameters[0].Value = discountType.DiscountTypeName;
            parameters[1].Value = discountType.DiscountTypeStatus;
            parameters[2].Value = discountType.financialTypeID;
            parameters[3].Value = discountType.PrinterName;
            parameters[4].Value = discountType.discountTypeOrderPrinter;
            parameters[5].Value = discountType.discountTypeOrderCopy;
            parameters[6].Value = discountType.printdiscountTypeOrder;
            parameters[7].Value = discountType.cookOrderStyle;
            parameters[8].Value = discountType.cookOrderCopy;
            parameters[9].Value = discountType.DiscountTypeID;
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
        /// 修改菜分类多语言（SYB共用）
        /// </summary>
        /// <param name="dishType"></param>
        public bool UpdateDishTypeI18n(DishTypeI18n dishType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DishTypeI18n set ");
            strSql.Append("LangID=@LangID,");
            strSql.Append("DishTypeID=@DishTypeID,");
            strSql.Append("DishTypeName=@DishTypeName,");
            strSql.Append("DishTypeI18nStatus=@DishTypeI18nStatus");
            strSql.Append(" where DishTypeI18nID=@DishTypeI18nID ");
            SqlParameter[] parameters = {
                        new SqlParameter("@LangID", SqlDbType.Int,4),
					    new SqlParameter("@DishTypeID", SqlDbType.Int,4),
					    new SqlParameter("@DishTypeName", SqlDbType.NVarChar,50),
                        new SqlParameter("@DishTypeI18nStatus", SqlDbType.Int,4),
                        new SqlParameter("@DishTypeI18nID",SqlDbType.Int,4)};
            parameters[0].Value = dishType.LangID;
            parameters[1].Value = dishType.DishTypeID;
            parameters[2].Value = dishType.DishTypeName;
            parameters[3].Value = dishType.DishTypeI18nStatus;
            parameters[4].Value = dishType.DishTypeI18nID;

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
        /// 根据菜分类多语言编号查询分类编号
        /// </summary>
        /// <param name="dishTypeI18nID"></param>
        public int QueryDishTypeID(int dishTypeI18nID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DishTypeID");
            strSql.Append(" from DishTypeI18n");
            strSql.AppendFormat(" where DishTypeI18nID = '{0}' and DishTypeI18nStatus > '0'", dishTypeI18nID);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
        }
        /// <summary>
        /// 查询所有菜分类信息
        /// </summary>
        public DataTable QueryDishType()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DishTypeInfo.[DishTypeID],[MenuID],[DishTypeSequence],[DishTypeStatus],");
            strSql.Append("[DishTypeI18nID],[LangID],[DishTypeName],[DishTypeI18nStatus]");
            strSql.Append(" FROM DishTypeInfo,DishTypeI18n");
            strSql.Append(" where DishTypeInfo.DishTypeID = DishTypeI18n.DishTypeID and DishTypeInfo.DishTypeStatus > '0'");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 根据菜谱编号查询菜分类信息
        /// </summary>
        public DataTable QueryDishType(int menuId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select DishTypeInfo.[DishTypeID],[MenuID],[DishTypeSequence],[DishTypeStatus],");
            strSql.Append("[DishTypeI18nID],[LangID],[DishTypeName],[DishTypeI18nStatus]");
            strSql.Append(" FROM DishTypeInfo,DishTypeI18n");
            strSql.Append(" where DishTypeInfo.DishTypeID = DishTypeI18n.DishTypeID and DishTypeInfo.DishTypeStatus > '0'");
            strSql.AppendFormat(" and DishTypeInfo.MenuID = {0}", menuId);

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }
        /// <summary>
        /// 查询所有折扣分类信息
        /// </summary>
        public DataTable QueryDiscountType()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select [DiscountTypeID],[DiscountTypeName],[DiscountTypeStatus],[PrinterName],");
            strSql.Append("[discountTypeOrderPrinter],[discountTypeOrderCopy],[printdiscountTypeOrder],");
            strSql.Append("[cookOrderStyle],[cookOrderCopy],[financialTypeID]");
            strSql.Append(" FROM DiscountType");
            strSql.Append(" where DiscountTypeStatus > '0'");

            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());

            return ds.Tables[0];
        }

        public static List<SybTypeinfoModel> List(int menuID)
        {
            List<SybTypeinfoModel> list = new List<SybTypeinfoModel>();
            string strsql = @"select DishTypeInfo.DishTypeID,DishTypeSequence,DishTypeName from DishTypeInfo 
                left join DishTypeI18n on DishTypeInfo.DishTypeID=DishTypeI18n.DishTypeID and LangID=1
                where MenuID=@MenuID and DishTypeStatus=1";
            var parm = new[] { new SqlParameter("@MenuID", menuID) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                while (dr.Read())
                {
                    SybTypeinfoModel model = new SybTypeinfoModel();

                    model.Id = dr["DishTypeID"].ToString();
                    model.Name = dr["DishTypeName"].ToString();
                    model.Sequence = dr["DishTypeSequence"].ToString();
                    list.Add(model);
                }
            }
            return list;
        }
        #region SYB模块
        /// <summary>
        /// 查询添加的菜谱分类名称是否重复
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="dishTypeName"></param>
        /// <returns></returns>
        public static bool CheckDishTypeName(int menuId, string dishTypeName)
        {
            List<DishTypeInfo> list = new List<DishTypeInfo>();
            string strsql = @"select DishTypeI18nID from DishTypeI18n  inner join DishTypeInfo on DishTypeInfo.DishTypeID=DishTypeI18n.DishTypeID
                where MenuID=@MenuID and DishTypeName=@DishTypeName and DishTypeI18n.DishTypeI18nStatus=1 and DishTypeInfo.DishTypeStatus=1";
            SqlParameter[] parm = { 
                new SqlParameter("@MenuID", menuId) ,
                new SqlParameter("@DishTypeName", dishTypeName)};
            SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm);
            string result = String.Empty;
            while (dr.Read())
            {
                result = dr["DishTypeI18nID"].ToString();
            }
            // bool result = dr.FieldCount > 0 ? false : true;//TODO 待测试
            dr.Close();
            if (!String.IsNullOrEmpty(result))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 查询修改的菜谱分类名称是否重复
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="dishTypeName"></param>
        /// <returns></returns>
        public static bool CheckUpdateDishTypeName(int menuId, string dishTypeName, int dishTypeID)
        {
            List<DishTypeInfo> list = new List<DishTypeInfo>();
            string strsql = @"select DishTypeI18nID from DishTypeI18n  inner join DishTypeInfo on DishTypeInfo.DishTypeID=DishTypeI18n.DishTypeID
                where MenuID=@MenuID and DishTypeName=@DishTypeName 
                and DishTypeI18n.DishTypeI18nStatus=1 and DishTypeInfo.DishTypeStatus=1 
                and DishTypeInfo.DishTypeID <> @DishTypeID";
            SqlParameter[] parm = { 
                new SqlParameter("@MenuID", menuId) ,
                new SqlParameter("@DishTypeName", dishTypeName),
                new SqlParameter("@DishTypeID",dishTypeID)};

            string result = String.Empty;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                while (dr.Read())
                {
                    result = dr["DishTypeI18nID"].ToString();
                }
                // bool result = dr.FieldCount > 0 ? false : true;//TODO 待测试
            }
            if (!String.IsNullOrEmpty(result))
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        /// <summary>
        /// 判断该类型是否可以删除
        /// </summary>
        /// <param name="dishTypeID"></param>
        /// <returns></returns>
        public static bool CheckDishTypeCanDel(int dishTypeID)
        {
            string strsql = @"select DishID from DishConnType where DishTypeID=@DishTypeID and DishConnTypeStatus>0";
            SqlParameter[] parm = { 
                new SqlParameter("@DishTypeID", dishTypeID)};
            
            string result = String.Empty;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                while (dr.Read())
                {
                    result = dr["DishID"].ToString();
                }
            }
            if (!String.IsNullOrEmpty(result))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 根据DishTypeID修改菜谱分类名称
        /// </summary>
        /// <param name="dishType"></param>
        /// <returns></returns>
        public bool UpdateDishName(DishTypeI18n dishType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update DishTypeI18n set ");
            strSql.Append("DishTypeName=@DishTypeName");
            strSql.Append(" where DishTypeID=@DishTypeID and DishTypeI18nStatus > 0");
            SqlParameter[] parameters = {
					    new SqlParameter("@DishTypeName", SqlDbType.NVarChar,50),
                        new SqlParameter("@DishTypeID",SqlDbType.Int,4)};
            parameters[0].Value = dishType.DishTypeName;
            parameters[1].Value = dishType.DishTypeID;

            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), parameters);
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
        /// 根据DishTypeID查找对应的Sequence
        /// </summary>
        /// <param name="dishTypeID"></param>
        /// <returns></returns>
        public int QueryDishSequence(int dishTypeID)
        {
            try
            {
                const string strSql = "select DishTypeSequence from DishTypeInfo where DishTypeID =@dishTypeID and DishTypeStatus > 0";
                SqlParameter[] para = new SqlParameter[] { 
                new SqlParameter("@dishTypeID",SqlDbType.Int) { Value = dishTypeID }
                };
                DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return Convert.ToInt32(ds.Tables[0].Rows[0][0]);
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
        /// <summary>
        /// 删除菜品分类关联表关联信息 add by wangc 20140416
        /// </summary>
        /// <param name="dishTypeId">菜品分类Id，为0，表示选择的是全部，则删除该菜品的全部关联关系</param>
        /// <param name="dishId">菜品编号</param>
        /// <returns></returns>
        public bool UpdateDishConnTypeStatus(int dishId)
        {
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                int result = 0;
                try
                {
                    conn.Open();
                    StringBuilder strSql = new StringBuilder();
                    strSql.AppendFormat(" update DishConnType set  DishConnTypeStatus = '-1'  where DishID={0}", dishId);
                    result = SqlHelper.ExecuteNonQuery(conn, CommandType.Text, strSql.ToString(), null);
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
        #endregion
    }
}
