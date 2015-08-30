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
    public class DishPriceConnTasteManager
    {
        public static List<int> ListTaste(int dishPriceId)
        {
            List<int> list = new List<int>();
            string strsql = @"select tasteId from DishPriceConnTaste where dishPriceId=@dishPriceId";
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

        public static bool Insert(DishPriceConnTaste model, SqlTransaction trans)
        {
            string strsql = @"insert into DishPriceConnTaste (dishPriceId,tasteId)
                        values (@dishPriceId,@tasteId)";
            SqlParameter[] parm = {
                       new SqlParameter("@dishPriceId", SqlDbType.Int),
new SqlParameter("@tasteId", SqlDbType.Int)
                        };
            parm[0].Value = model.dishPriceId;
            parm[1].Value = model.tasteId;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) == 1;
        }

        public static bool Del(int dishPriceId, SqlTransaction trans)
        {
            var strsql = "delete DishPriceConnTaste where dishPriceId=@dishPriceId";
            var parm = new[] { new SqlParameter("@dishPriceId", dishPriceId) };
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) > 0;
        }


        public static bool DelTaste(int tasteId, SqlTransaction trans)
        {
            var strsql = "delete DishPriceConnTaste where tasteId=@tasteId";
            var parm = new[] { new SqlParameter("@tasteId", tasteId) };
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) > 0;
        }

        public static bool Exit(int tasteId)
        {
            var val = false;
            string strsql = @"select 1 from DishPriceConnTaste where tasteId=@tasteId";
            SqlParameter[] parm = new[] {
                    new SqlParameter("@tasteId", tasteId)
                        };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                if (dr.Read()) val = true;
            }
            return val;
        }
    }
}
