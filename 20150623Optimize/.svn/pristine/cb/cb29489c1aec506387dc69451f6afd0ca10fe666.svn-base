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
    public partial class CouponGetDetailManager 
    {
        public List<CouponGetDetailV> GetListByCouponId(int couponId)
        {
            string sql = @"SELECT CouponGetDetail.*, PreOrder19dian.preOrderSum
                            FROM CouponGetDetail LEFT OUTER JOIN
                             PreOrder19dian ON PreOrder19dian.preOrder19dianId = CouponGetDetail.PreOrder19DianId WHERE  CouponId = @CouponId";
            SqlParameter sqlParameter = new SqlParameter("@CouponId", SqlDbType.Int, 8) { Value = couponId };
            List<CouponGetDetailV> list = null;
            using (var reader =
                   SqlHelper.ExecuteReader(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameter))
            {
                list = new List<CouponGetDetailV>();
                while (reader.Read())
                {
                    list.Add(reader.GetEntity<CouponGetDetailV>());
                }
            }
            return list;
            
        }
    }
}
