using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public partial class CounponManager
    {
        /// <summary>
        /// 查询当前城市有有效优惠券的门店编号
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public List<int> SelectHadCouponShopId(int cityId)
        {
            const string strSql = @"select A.ShopId from Coupon A 
inner join ShopInfo B on A.ShopId=B.shopID
where B.cityID=@cityId and A.EndDate<GETDATE() and A.State=1
and B.shopStatus=1 and B.isHandle=1";
            SqlParameter parameter = new SqlParameter("@cityId", SqlDbType.Int, 4) { Value = cityId };
            var list = new List<int>();
            using (SqlDataReader drReader = SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql, parameter))
            {
                list.Add(Convert.ToInt32(drReader[0]));
            }
            return list;
        }
    }
}
