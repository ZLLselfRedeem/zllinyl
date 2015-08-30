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
    public class DishConnTypeManager
    {
        public static List<int> ListType(int dishID)
        {
            List<int> list = new List<int>();
            string strsql = @"select DishTypeID from DishConnType where DishID=@DishID and DishConnTypeStatus=1";
            var parm = new[] { new SqlParameter("@DishID", dishID) };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                while (dr.Read())
                {
                    list.Add(!dr.IsDBNull(0) ? dr.GetInt32(0) : 0);
                }
            }
            return list;
        }


        public static bool Insert(DishConnType model, SqlTransaction trans)
        {
            string strsql = @"insert into DishConnType (DishID,DishTypeID,DishConnTypeStatus)
                        values (@DishID,@DishTypeID,@DishConnTypeStatus)";
            SqlParameter[] parm = {
new SqlParameter("@DishID", SqlDbType.Int),
new SqlParameter("@DishTypeID", SqlDbType.Int),
new SqlParameter("@DishConnTypeStatus", SqlDbType.Int)
                        };
            parm[0].Value = model.DishID;
            parm[1].Value = model.DishTypeID;
            parm[2].Value = model.DishConnTypeStatus;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) == 1;
        }

        public static bool Del(int dishID, SqlTransaction trans)
        {
            var strsql = "update DishConnType set DishConnTypeStatus=0 where DishID=@DishID";
            var parm = new[] { new SqlParameter("@DishID", dishID) };
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) > 0;
        }
    }
}
