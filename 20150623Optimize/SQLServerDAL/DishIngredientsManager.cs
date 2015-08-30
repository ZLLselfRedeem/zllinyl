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
    /// 功能描述:菜的配料Id
    /// 创建标识:罗国华 20131030
    /// </summary>
    public class DishIngredientsManager
    {
        #region 通用模块
        public static DishIngredients GetModel(int ingredientsId)
        {
            string strsql = @"select ingredientsId,menuId,ingredientsName,ingredientsPrice,vipDiscountable,backDiscountable,ingredientsRemark,ingredientsSequence
,ingredientsStatus from DishIngredients where ingredientsId=@ingredientsId";
            var parm = new[] { new SqlParameter("@ingredientsId", ingredientsId) };
            DishIngredients model = null;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                if (dr.Read())
                {
                    model = new DishIngredients();
                    model.ingredientsId = !dr.IsDBNull(0) ? dr.GetInt32(0) : 0;
                    model.menuId = !dr.IsDBNull(1) ? dr.GetInt32(1) : 0;
                    model.ingredientsName = !dr.IsDBNull(2) ? dr.GetString(2) : string.Empty;
                    model.ingredientsPrice = !dr.IsDBNull(3) ? dr.GetDouble(3) : 0;
                    model.vipDiscountable = !dr.IsDBNull(4) ? dr.GetBoolean(4) : false;
                    model.backDiscountable = !dr.IsDBNull(5) ? dr.GetBoolean(5) : false;
                    model.ingredientsRemark = !dr.IsDBNull(6) ? dr.GetString(6) : string.Empty;
                    model.ingredientsSequence = !dr.IsDBNull(7) ? dr.GetInt32(7) : 0;
                    model.ingredientsStatus = !dr.IsDBNull(8) ? dr.GetBoolean(8) : false;

                }
            }
            return model;
        }

        public static bool Insert(DishIngredients model, SqlTransaction trans)
        {
            string strsql = @"insert into DishIngredients (menuId,ingredientsName,ingredientsPrice,vipDiscountable,backDiscountable,ingredientsRemark,ingredientsSequence
,ingredientsStatus)
                        values (@menuId,@ingredientsName,@ingredientsPrice,@vipDiscountable,@backDiscountable,@ingredientsRemark,@ingredientsSequence
,@ingredientsStatus)";
            SqlParameter[] parm = {
new SqlParameter("@menuId", SqlDbType.Int),
new SqlParameter("@ingredientsName", SqlDbType.NVarChar,50),
new SqlParameter("@ingredientsPrice", SqlDbType.Float),
new SqlParameter("@vipDiscountable", SqlDbType.Bit),
new SqlParameter("@backDiscountable", SqlDbType.Bit),
new SqlParameter("@ingredientsRemark", SqlDbType.NVarChar,200),
new SqlParameter("@ingredientsSequence", SqlDbType.Int),
new SqlParameter("@ingredientsStatus", SqlDbType.Bit)
                        };
            parm[0].Value = model.menuId;
            parm[1].Value = model.ingredientsName;
            parm[2].Value = model.ingredientsPrice;
            parm[3].Value = model.vipDiscountable;
            parm[4].Value = model.backDiscountable;
            parm[5].Value = model.ingredientsRemark;
            parm[6].Value = model.ingredientsSequence;
            parm[7].Value = model.ingredientsStatus;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) == 1;
        }

        public static bool Del(int ingredientsId, SqlTransaction trans)
        {
            var strsql = "delete DishIngredients where ingredientsId=@ingredientsId";
            var parm = new[] { new SqlParameter("@ingredientsId", ingredientsId) };
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) > 0;
        }

        public static bool Update(DishIngredients model, SqlTransaction trans)
        {
            string strsql = @"update DishIngredients set menuId=@menuId,ingredientsName=@ingredientsName,ingredientsPrice=@ingredientsPrice,vipDiscountable=@vipDiscountable,backDiscountable=@backDiscountable,ingredientsRemark=@ingredientsRemark,ingredientsSequence=@ingredientsSequence,ingredientsStatus=@ingredientsStatus where ingredientsId=@ingredientsId";
            SqlParameter[] parm = {
                        new SqlParameter("@ingredientsId", SqlDbType.Int),
new SqlParameter("@menuId", SqlDbType.Int),
new SqlParameter("@ingredientsName", SqlDbType.NVarChar,50),
new SqlParameter("@ingredientsPrice", SqlDbType.Float),
new SqlParameter("@vipDiscountable", SqlDbType.Bit),
new SqlParameter("@backDiscountable", SqlDbType.Bit),
new SqlParameter("@ingredientsRemark", SqlDbType.NVarChar,200),
new SqlParameter("@ingredientsSequence", SqlDbType.Int),
new SqlParameter("@ingredientsStatus", SqlDbType.Bit)
                        };
            parm[0].Value = model.ingredientsId;
            parm[1].Value = model.menuId;
            parm[2].Value = model.ingredientsName;
            parm[3].Value = model.ingredientsPrice;
            parm[4].Value = model.vipDiscountable;
            parm[5].Value = model.backDiscountable;
            parm[6].Value = model.ingredientsRemark;
            parm[7].Value = model.ingredientsSequence;
            parm[8].Value = model.ingredientsStatus;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) > 0;
        }

        public static List<SybTypeModel> List(int menuId)
        {
            List<SybTypeModel> list = new List<SybTypeModel>();
            string strsql = @"select ingredientsId,ingredientsName from DishIngredients 
where menuId=@menuId and ingredientsStatus=1";
            var parm = new[] { new SqlParameter("@menuId", menuId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                while (dr.Read())
                {
                    list.Add(new SybTypeModel() { Id = dr["ingredientsId"].ToString(), Name = dr["ingredientsName"].ToString() });
                }
            }
            return list;
        }

        #endregion

        public static bool Insert(DishIngredients model)
        {
            bool val = false;
            SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction);
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                Insert(model, trans);

                trans.Commit();
                val = true;
            }
            catch (Exception)
            {
                trans.Rollback();
                val = false;
            }
            finally
            {
                conn.Close();
            }
            return val;
        }

        public static bool Del(int tasteId)
        {
            bool val = false;
            SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction);
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                Del(tasteId);

                trans.Commit();
                val = true;
            }
            catch (Exception)
            {
                trans.Rollback();
                val = false;
            }
            finally
            {
                conn.Close();
            }
            return val;
        }

        public static bool Update(DishIngredients model)
        {
            bool val = false;
            SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction);
            conn.Open();
            SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
            try
            {
                Update(model, trans);

                trans.Commit();
                val = true;
            }
            catch (Exception)
            {
                trans.Rollback();
                val = false;
            }
            finally
            {
                conn.Close();
            }
            return val;
        }


        public static bool UpdateingredientsStatus(int ingredientsId, bool ingredientsStatus, SqlTransaction trans)
        {
            string strsql = @"update DishIngredients set ingredientsStatus=@ingredientsStatus where ingredientsId=@ingredientsId";
            SqlParameter[] parm = {
                   new SqlParameter("@ingredientsId", SqlDbType.Int),
new SqlParameter("@ingredientsStatus", SqlDbType.Bit)
                    };
            parm[0].Value = ingredientsId;
            parm[1].Value = ingredientsStatus;
            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm) == 1;
        }


        public static bool Exit(int menuId, string ingredientsName)
        {
            var val = false;
            string strsql = @"select 1 from DishIngredients where menuId=@menuId and ingredientsName=@ingredientsName and ingredientsStatus=1";
            SqlParameter[] parm = new[] {
                    new SqlParameter("@menuId", menuId),
                     new SqlParameter("@ingredientsName", ingredientsName)
                        };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                if (dr.Read()) val = true;
            }
            return val;
        }

        public static DataTable SelectDishIngredients(string inStr)
        {
            string strsql = @"SELECT [ingredientsId],[menuId],[ingredientsName],[ingredientsPrice],[vipDiscountable],[backDiscountable],[ingredientsRemark],[ingredientsSequence] ,[ingredientsStatus]
                               FROM [VAGastronomistMobileApp].[dbo].[DishIngredients]
                               where [ingredientsId] in " + inStr + " and ingredientsStatus=1";
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, null);
            return ds.Tables[0];
        }
    }
}