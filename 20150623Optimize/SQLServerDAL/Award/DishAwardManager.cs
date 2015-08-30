using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Data;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class DishAwardManager
    {
        /// <summary>
        /// 菜主要信息
        /// </summary>
        /// <param name="DishID"></param>
        /// <param name="DishPriceID"></param>
        /// <returns></returns>
        public DishAward SelectDishInfo(int DishID, int DishPriceID)
        {
            const string strSql = @"select C.DishID,C.DishName,
B.DishPrice unitPrice,1 quantity,A.ScaleName dishPriceName,A.dishPriceI18nId,
B.vipDiscountable,B.DishPriceID,isnull(A.markName,'') markName,D.MenuID
from DishPriceI18n as A
inner join DishPriceInfo as B on A.DishPriceID=B.DishPriceID
inner join DishI18n as C on C.DishID=B.DishID
inner join DishInfo D on C.DishID = D.DishID
where C.DishID=@DishID and A.DishPriceID=@DishPriceID
and A.DishPriceI18nStatus=1 and B.DishPriceStatus=1 and C.DishI18nStatus=1";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@DishID", SqlDbType.Int) { Value = DishID },
            new SqlParameter("@DishPriceID", SqlDbType.Int) { Value = DishPriceID }
            };
            DishAward dishAward = new DishAward();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (sdr.Read())
                {
                    dishAward = SqlHelper.GetEntity<DishAward>(sdr);
                }
            }
            return dishAward;
        }

        /// <summary>
        /// 分类
        /// </summary>
        /// <param name="MenuID"></param>
        /// <returns></returns>
        public string SelectDishTypeName(int MenuID)
        {
            const string strSql = @"select A.DishTypeName from DishTypeI18n A inner join DishTypeInfo B
on A.DishTypeID = B.DishTypeID
and B.MenuID = @MenuID and B.DishTypeSequence = 1";
            SqlParameter[] para = new SqlParameter[]{
            new SqlParameter("@MenuID", SqlDbType.Int) { Value = MenuID }
            };
            using (SqlConnection conn = new SqlConnection(SqlHelper.ConnectionStringLocalTransaction))
            {
                object obj = SqlHelper.ExecuteScalar(conn, CommandType.Text, strSql, para);
                if (obj == null)
                {
                    return "";
                }
                else
                {
                    return obj.ToString();
                }
            }
        }

        /// <summary>
        /// 口味
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="dishPriceId"></param>
        /// <returns></returns>
        public DishTasteAward SelectDishTaste(int menuId, int dishPriceId)
        {
            const string strSql = @"select t.tasteId,t.tasteName from DishTaste t inner join DishPriceConnTaste c
on t.tasteId = c.tasteId  and menuId=@menuId and c.dishPriceId=@dishPriceId and tasteStatus=1";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@menuId", SqlDbType.Int) { Value = menuId },
            new SqlParameter("@dishPriceId", SqlDbType.Int) { Value = dishPriceId }
            };
            DishTasteAward taste = new DishTasteAward();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                if (sdr.Read())
                {
                    taste = SqlHelper.GetEntity<DishTasteAward>(sdr);
                }
            }
            return taste;
        }

        /// <summary>
        /// 配菜
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="dishPriceId"></param>
        /// <returns></returns>
        public List<DishIngredientsAward> SelectIngredients(int menuId, int dishPriceId)
        {
            const string strSql = @"select d.ingredientsId,d.ingredientsName,d.ingredientsPrice,d.vipDiscountable
 from DishIngredients d inner join DishPriceConnIngredients conn
on d.ingredientsId = conn.ingredientsId
 and d.menuId=1125 and conn.dishPriceId=102431 and d.ingredientsStatus=1";
            SqlParameter[] para = new SqlParameter[] { 
            new SqlParameter("@menuId", SqlDbType.Int) { Value = menuId },
            new SqlParameter("@dishPriceId", SqlDbType.Int) { Value = dishPriceId }
            };
            List<DishIngredientsAward> ingredients = new List<DishIngredientsAward>();
            using (SqlDataReader sdr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, para))
            {
                while (sdr.Read())
                {
                    ingredients.Add(SqlHelper.GetEntity<DishIngredientsAward>(sdr));
                }
            }
            return ingredients;
        }
    }
}
