using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    /// <summary>
    /// 配菜沽清数据访问接口层
    /// </summary>
    public interface ICurrectIngredientsSellOffInfoManager
    {
        /// <summary>
        /// 新增沽清信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Add(CurrectIngredientsSellOffInfo model);

        /// <summary>
        /// 取消沽清
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Update(CurrectIngredientsSellOffInfo model);

        /// <summary>
        /// 判断是否存在已沽清的记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        bool Exists(CurrectIngredientsSellOffInfo model);

        /// <summary>
        /// 查询当前公司当前门店所有沽清配菜编号
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        List<int> Select(int companyId, int shopId);

        /// <summary>
        /// 查询当前门店所有沽清配菜编号
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        List<int> Select(int shopId);

        /// <summary>
        /// 查询当前门店配菜
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        List<DishIngredients> GetShopDishIngredients(int shopId, string key);
    }

    /// <summary>
    /// 配菜沽清数据访问层
    /// </summary>
    public class CurrectIngredientsSellOffInfoManager : ICurrectIngredientsSellOffInfoManager
    {
        /// <summary>
        /// 新增沽清信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(CurrectIngredientsSellOffInfo model)
        {
            const string strSql = @"insert into CurrectIngredientsSellOffInfo(ingredientsId,shopId,companyId,status,expirationTime,operateEmployeeId)
  values (@ingredientsId,@shopId,@companyId,@status,@expirationTime,@operateEmployeeId) ;select @@IDENTITY";
            SqlParameter[] parameters = {
					new SqlParameter("@ingredientsId", SqlDbType.Int,4),
					new SqlParameter("@shopId", SqlDbType.Int,4),
					new SqlParameter("@companyId", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.Bit,1),
                    new SqlParameter("@expirationTime", SqlDbType.DateTime),
					new SqlParameter("@operateEmployeeId", SqlDbType.Int,4)};
            parameters[0].Value = model.ingredientsId;
            parameters[1].Value = model.shopId;
            parameters[2].Value = model.companyId;
            parameters[3].Value = model.status;
            parameters[4].Value = model.expirationTime;
            parameters[5].Value = model.operateEmployeeId;
            object obj = SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters);
            return obj == null ? false : Convert.ToInt64(obj) > 0;
        }

        /// <summary>
        /// 取消沽清
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(CurrectIngredientsSellOffInfo model)
        {
            const string strSql = @"update CurrectIngredientsSellOffInfo set 
 status=@status,operateTime=@operateTime,operateEmployeeId=@operateEmployeeId where ingredientsId=@ingredientsId and shopId=@shopId and companyId=@companyId and status=1";
            SqlParameter[] parameters = {
					new SqlParameter("@shopId", SqlDbType.Int,4),
					new SqlParameter("@companyId", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.Bit,1),
					new SqlParameter("@ingredientsId", SqlDbType.Int,4),
                                        new SqlParameter("@operateTime", SqlDbType.DateTime),
					new SqlParameter("@operateEmployeeId", SqlDbType.Int,4)};
            parameters[0].Value = model.shopId;
            parameters[1].Value = model.companyId;
            parameters[2].Value = model.status;
            parameters[3].Value = model.ingredientsId;
            parameters[4].Value = model.operateTime;
            parameters[5].Value = model.operateEmployeeId;

            return SqlHelper.ExecuteNonQuery(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters) > 0;
        }

        /// <summary>
        /// 判断是否存在已沽清的记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Exists(CurrectIngredientsSellOffInfo model)
        {
            const string strSql = @"select count(1) from CurrectIngredientsSellOffInfo
  where ingredientsId=@ingredientsId and shopId=@shopId and status=1 and companyId=@companyId and expirationTime>getdate()";
            SqlParameter[] parameters = {
					new SqlParameter("@ingredientsId", SqlDbType.Int,4),
                    new SqlParameter("@shopId", SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.Int,4)
			};
            parameters[0].Value = model.ingredientsId;
            parameters[1].Value = model.shopId;
            parameters[2].Value = model.companyId;

            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters))
            {
                if (dr.Read())
                {
                    return dr.IsDBNull(0) ? false : Convert.ToInt32(dr[0]) > 0;
                }
                return false;
            }
        }
        /// <summary>
        /// 查询当前公司当前门店所有沽清配菜编号
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public List<int> Select(int companyId, int shopId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ingredientsId from CurrectIngredientsSellOffInfo");
            strSql.Append(" where  shopId=@shopId and status=1 and companyId=@companyId and [expirationTime]>getdate()");
            SqlParameter[] parameters = {
                    new SqlParameter("@shopId", SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.Int,4)
			};
            parameters[0].Value = shopId;
            parameters[1].Value = companyId;
            var list = new List<int>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters))
            {
                while (dr.Read())
                {
                    list.Add(dr.IsDBNull(0) ? 0 : Convert.ToInt32(dr[0]));
                }
            }
            return list;
        }
        /// <summary>
        /// 查询当前门店所有沽清配菜编号
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public List<int> Select(int shopId)
        {
            const string strSql = @"select ingredientsId from CurrectIngredientsSellOffInfo where [expirationTime]>getdate() and shopId=@shopId and status=1";
            SqlParameter[] parameters = { new SqlParameter("@shopId", SqlDbType.Int, 4) };
            parameters[0].Value = shopId;
            var list = new List<int>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString(), parameters))
            {
                while (dr.Read())
                {
                    list.Add(dr.IsDBNull(0) ? 0 : Convert.ToInt32(dr[0]));
                }
            }
            return list;
        }
        /// <summary>
        /// 查询当前门店配菜
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public List<DishIngredients> GetShopDishIngredients(int shopId, string key)
        {
            const string strSql = @"select d.*
from MenuConnShop m
inner join DishIngredients d on m.menuId=d.menuId
where shopId=@shopId and d.ingredientsName like @key and d.ingredientsStatus=1 order by ingredientsSequence desc";
            SqlParameter[] parameter = { new SqlParameter("@shopId", SqlDbType.Int, 4) { Value = shopId },
                                       new SqlParameter("@key",SqlDbType.NVarChar,50){Value=String.Format("%{0}%",key)}};
            var list = new List<DishIngredients>();
            using (SqlDataReader dr = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                while (dr.Read())
                {
                    list.Add(SqlHelper.GetEntity<DishIngredients>(dr));
                }
            }
            return list;
        }
    }
}
