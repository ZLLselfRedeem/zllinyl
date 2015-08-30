using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using VAGastronomistMobileApp.WebPageDll;

namespace Web.Control
{
    public class SybSessionHelper
    {
        /// <summary>
        /// 获取收银宝系统session对象门店编号
        /// </summary>
        /// <returns></returns>
        public static int GetSybCurrectSessionShopId()
        {
            if (HttpContext.Current.Session["loginshop"] == null)
            {
                return 0;
            }
            return Common.ToInt32(HttpContext.Current.Session["loginshop"]);
        }

        /// <summary>
        ///  获取收银宝系统session对象登录员工编号
        /// </summary>
        /// <returns></returns>
        public static int GetCurrectSessionEmployeeId()
        {
            if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
            {
                return 0;
            }
            var session = (VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"];
            if (session == null)
            {
                return 0;
            }
            return session.employeeID;
        }
    }
}
