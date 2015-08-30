using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Data;
using VAGastronomistMobileApp.DBUtility;

namespace VAGastronomistMobileApp.SQLServerDAL
{
    public class ShopAuthorityManager
    {
        /// <summary>
        /// 收银宝查询用户在某家门店是否有某个权限
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="shopId"></param>
        /// <param name="shopAuthorityName"></param>
        /// <returns></returns>
        public bool SelectSybAuthority(int employeeId, int shopId, string shopAuthorityName)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select Count(A.employeeShopID) isHaveAuthority");
            strSql.Append(" from EmployeeConnShop A");
            strSql.Append(" inner join EmployeeShopAuthority B on A.employeeShopID=B.employeeConnShopId");
            strSql.Append(" inner join ShopAuthority C on C.shopAuthorityId=B.shopAuthorityId");
            strSql.AppendFormat(" where A.status=1 and C.shopAuthorityName='{0}'",shopAuthorityName);
            strSql.AppendFormat(" and A.employeeID={0} and A.shopID={1}", employeeId, shopId);
            strSql.Append(" and B.employeeShopAuthorityStatus=1 and C.shopAuthorityStatus=1");
            DataSet ds = SqlHelper.ExecuteDataSet(SqlHelper.ConnectionStringLocalTransaction, CommandType.Text, strSql.ToString());
            if (ds == null)
            {
                return false;
            }
            else
            {
                if (Convert.ToInt32(ds.Tables[0].Rows[0][0]) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
