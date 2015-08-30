using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using VAGastronomistMobileApp.WebPageDll;

namespace Web.Control
{
    public class SessionHelper
    {
        /// <summary>
        /// 获取后台系统session对象
        /// </summary>
        /// <returns></returns>
        public static VAEmployeeLoginResponse GetCurrectSession()
        {
            if (HttpContext.Current.Session["UserInfo"] == null)
            {
                return null;
            }
            return HttpContext.Current.Session["UserInfo"] as VAEmployeeLoginResponse;
        }

        /// <summary>
        /// 获取后台系统session对象员工编号
        /// </summary>
        /// <returns></returns>
        public static int GetCurrectSessionEmployeeId()
        {
            var session = GetCurrectSession();
            if (session == null)
            {
                return 0;
            }
            return session.employeeID;
        }
    }
}
