using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public partial class CouponSendDetailManager
    {
        public long GetRegisterUserCountViaCoupon(int couponSendDetailId)
        {
            string sql = @"SELECT COUNT(0) FROM CustomerInfo C INNER JOIN CouponGetDetail G ON C.mobilePhoneNumber = G.MobilePhoneNumber 
                            INNER JOIN CouponSendDetail S ON G.CouponSendDetailID = S.CouponSendDetailID 
                            WHERE S.CouponSendDetailID = @CouponSendDetailID AND C.RegisterDate > S.CreateTime"; 
           SqlParameter sqlParameter  = new SqlParameter("@CouponSendDetailID", couponSendDetailId); 
            var retutnValue =
               SqlHelper.ExecuteScalar(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, sql, sqlParameter);
            long count;
            if (long.TryParse(retutnValue.ToString(), out count))
            {
                return count; 
            }
            return 0;
        }
    }
}
