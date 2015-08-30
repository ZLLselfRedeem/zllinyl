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
    public class DishPriceConnIngredientsManager
    {
        public static List<int> ListIngredients(int dishPriceId)
        {
            List<int> list = new List<int>();
            string strsql = @"select ingredientsId from DishPriceConnIngredients where dishPriceId=@dishPriceId";
            var parm = new[] { new SqlParameter("@dishPriceId", dishPriceId) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                while (dr.Read())
                {
                    list.Add(!dr.IsDBNull(0) ? dr.GetInt32(0) : 0);
                }
            }
            return list;
        }

        public static bool Insert(DishPriceConnIngredients model, SqlTransaction trans)
        {
            string strsql = @"insert into DishPriceConnIngredients (dishPriceId,ingredientsId)
                        values (@dishPriceId,@ingredientsId)";
            SqlParameter[] parm = {
                       new SqlParameter("@dishPriceId", SqlDbType.Int),
new SqlParameter("@ingredientsId", SqlDbType.Int)
                        };
            parm[0].Value = model.dishPriceId;
            parm[1].Value = model.ingredientsId;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) == 1;
        }

        public static bool Del(int dishPriceId,SqlTransaction trans)
        {
            var strsql = "delete DishPriceConnIngredients where dishPriceId=@dishPriceId";
            var parm = new[] { new SqlParameter("@dishPriceId", dishPriceId) };
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) > 0;
        }

        public static bool DelIngredients(int ingredientsId, SqlTransaction trans)
        {
            var strsql = "delete DishPriceConnIngredients where ingredientsId=@ingredientsId";
            var parm = new[] { new SqlParameter("@ingredientsId", ingredientsId) };
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) > 0;
        }

        public static bool Exit(int ingredientsId)
        {
            var val = false;
            string strsql = @"select 1 from DishPriceConnIngredients where ingredientsId=@ingredientsId";
            SqlParameter[] parm = new[] {
                        new SqlParameter("@ingredientsId", ingredientsId)
                        };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                if (dr.Read()) val = true;
            }
            return val;
        }
    }
}
