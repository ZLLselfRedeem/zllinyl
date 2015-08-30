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
    /// 功能描述: 
    /// 创建标识:XXX 20131104
    /// </summary>
    public class DishPriceI18nManager
    {
        #region 通用模块

        public static bool Insert(DishPriceI18n model, SqlTransaction trans)
        {
            string strsql = @"insert into DishPriceI18n (DishPriceI18nID,DishPriceID,LangID,ScaleName,DishPriceI18nStatus,markName)
                        values (@DishPriceI18nID,@DishPriceID,@LangID,@ScaleName,@DishPriceI18nStatus,@markName)";
            var parm = new[] { 
                 new SqlParameter("@DishPriceI18nID" , model.DishPriceI18nID),
                new SqlParameter("@DishPriceID" , model.DishPriceID),
                new SqlParameter("@LangID",model.LangID),
                new SqlParameter("@ScaleName",model.ScaleName),
                new SqlParameter("@DishPriceI18nStatus",model.DishPriceI18nStatus),
                new SqlParameter("@markName",model.markName)
            };
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) == 1;
        }

        public static bool Del(int dishPriceID, SqlTransaction trans)
        {
            var strsql = "update DishPriceI18n set DishPriceI18nStatus=0 where DishPriceID=@DishPriceID";
            var parm = new[] { new SqlParameter("@DishPriceID", dishPriceID) };
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) > 0;
        }

        public static bool Update(DishPriceI18n model, SqlTransaction trans)
        {
            string strsql = @"update DishPriceI18n set DishPriceID=@DishPriceID,LangID=@LangID,ScaleName=@ScaleName,markName=@markName where DishPriceI18nID=@DishPriceI18nID";
            SqlParameter[] parm = {
                        new SqlParameter("@DishPriceI18nID", SqlDbType.Int),
new SqlParameter("@DishPriceID", SqlDbType.Int),
new SqlParameter("@LangID", SqlDbType.Int),
new SqlParameter("@ScaleName", SqlDbType.NVarChar,50),
new SqlParameter("@markName", SqlDbType.NVarChar,50)
                        };
            parm[0].Value = model.DishPriceI18nID;
            parm[1].Value = model.DishPriceID;
            parm[2].Value = model.LangID;
            parm[3].Value = model.ScaleName;
            parm[4].Value = model.markName;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, strsql, parm) > 0;
        }

        public static int GetDishPriceI18nID(int dishPriceID, int langID)
        {
            int val = 0;
            string strsql = @"select DishPriceI18nID from DishPriceI18n where DishPriceID=@DishPriceID and LangID=@LangID";
            SqlParameter[] parm = new[] {
                   new SqlParameter("@DishPriceID",dishPriceID ),
                   new SqlParameter("@LangID",langID )
                    };
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strsql, parm))
            {
                if (dr.Read())
                {
                    val = !dr.IsDBNull(0) ? dr.GetInt32(0) : 0;
                }
            }
            return val;
        }
        #endregion
    }
}