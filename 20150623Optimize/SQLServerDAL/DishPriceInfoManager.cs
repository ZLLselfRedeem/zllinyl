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
    public class DishPriceInfoManager
    {
          #region 通用模块
        public static DishPriceInfo GetModel(int dishPriceID)
        {
            string strsql = @"select DishPriceID,DishPrice,DishID,DishSoldout,DishPriceStatus,DishNeedWeigh,vipDiscountable,backDiscountable
,dishIngredientsMinAmount,dishIngredientsMaxAmount from DishPriceInfo where DishPriceID=@DishPriceID";
            var parm = new[] {new SqlParameter("@DishPriceID", dishPriceID)};
            DishPriceInfo model = null;
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                if (dr.Read())
                {
                    model = new DishPriceInfo();
                    model.DishPriceID = !dr.IsDBNull(0) ? dr.GetInt32(0) : 0;
                    model.DishPrice = !dr.IsDBNull(1) ? dr.GetDouble(1) : 0;
                    model.DishID = !dr.IsDBNull(2) ? dr.GetInt32(2) : 0;
                    model.DishSoldout = !dr.IsDBNull(3) ? dr.GetBoolean(3) : false;
                    model.DishPriceStatus = !dr.IsDBNull(4) ? dr.GetInt32(4) : 0;
                    model.DishNeedWeigh = !dr.IsDBNull(5) ? dr.GetBoolean(5) : false;
                    model.vipDiscountable = !dr.IsDBNull(6) ? dr.GetBoolean(6) : false;
                    model.backDiscountable = !dr.IsDBNull(7) ? dr.GetBoolean(7) : false;
                    model.dishIngredientsMinAmount = !dr.IsDBNull(8) ? dr.GetInt32(8) : 0;
                    model.dishIngredientsMaxAmount = !dr.IsDBNull(9) ? dr.GetInt32(9) : 0;

                }
            }
            return model;
        }

        public static int InsertDishPriceInfo(DishPriceInfo model)
        {
            int val = 0;
            try
            {
                string strsql = @"insert into DishPriceInfo (DishPrice,DishID,DishSoldout,DishPriceStatus,DishNeedWeigh,vipDiscountable,backDiscountable
,dishIngredientsMinAmount,dishIngredientsMaxAmount)
                        values (@DishPrice,@DishID,@DishSoldout,@DishPriceStatus,@DishNeedWeigh,@vipDiscountable,@backDiscountable
,@dishIngredientsMinAmount,@dishIngredientsMaxAmount)
select @@IDENTITY";
                SqlParameter[] parm = {
new SqlParameter("@DishPrice", SqlDbType.Float),
new SqlParameter("@DishID", SqlDbType.Int),
new SqlParameter("@DishSoldout", SqlDbType.Bit),
new SqlParameter("@DishPriceStatus", SqlDbType.Int),
new SqlParameter("@DishNeedWeigh", SqlDbType.Bit),
new SqlParameter("@vipDiscountable", SqlDbType.Bit),
new SqlParameter("@backDiscountable", SqlDbType.Bit),
new SqlParameter("@dishIngredientsMinAmount", SqlDbType.Int),
new SqlParameter("@dishIngredientsMaxAmount", SqlDbType.Int)
                        };
                parm[0].Value = model.DishPrice;
                parm[1].Value = model.DishID;
                parm[2].Value = model.DishSoldout;
                parm[3].Value = model.DishPriceStatus;
                parm[4].Value = model.DishNeedWeigh;
                parm[5].Value = model.vipDiscountable;
                parm[6].Value = model.backDiscountable;
                parm[7].Value = model.dishIngredientsMinAmount;
                parm[8].Value = model.dishIngredientsMaxAmount;
                object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm);
                val = Convert.ToInt32(obj);
            }
            catch (Exception) { }
            return val;
        }

        public static bool Update(DishPriceInfo model,SqlTransaction trans)
        {
            string strsql = @"update DishPriceInfo set DishPrice=@DishPrice,DishID=@DishID,DishSoldout=@DishSoldout,DishNeedWeigh=@DishNeedWeigh,vipDiscountable=@vipDiscountable,backDiscountable=@backDiscountable,dishIngredientsMinAmount=@dishIngredientsMinAmount,dishIngredientsMaxAmount=@dishIngredientsMaxAmount where DishPriceID=@DishPriceID";
            SqlParameter[] parm = {
                        new SqlParameter("@DishPriceID", SqlDbType.Int),
new SqlParameter("@DishPrice", SqlDbType.Float),
new SqlParameter("@DishID", SqlDbType.Int),
new SqlParameter("@DishSoldout", SqlDbType.Bit),
new SqlParameter("@DishNeedWeigh", SqlDbType.Bit),
new SqlParameter("@vipDiscountable", SqlDbType.Bit),
new SqlParameter("@backDiscountable", SqlDbType.Bit),
new SqlParameter("@dishIngredientsMinAmount", SqlDbType.Int),
new SqlParameter("@dishIngredientsMaxAmount", SqlDbType.Int)
                        };
            parm[0].Value = model.DishPriceID;
parm[1].Value = model.DishPrice;
parm[2].Value = model.DishID;
parm[3].Value = model.DishSoldout;
parm[4].Value = model.DishNeedWeigh;
parm[5].Value = model.vipDiscountable;
parm[6].Value = model.backDiscountable;
parm[7].Value = model.dishIngredientsMinAmount;
parm[8].Value = model.dishIngredientsMaxAmount;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) > 0;
        }
        #endregion


        public static bool Update(DishPriceInfo model)
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

        public static bool Del(int dishPriceID, SqlTransaction trans)
        {
            var strsql = "update DishPriceInfo set DishPriceStatus=0 where DishPriceID=@DishPriceID";
            var parm = new[] { new SqlParameter("@DishPriceID", dishPriceID) };
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) > 0;
        }
    }
    
}
