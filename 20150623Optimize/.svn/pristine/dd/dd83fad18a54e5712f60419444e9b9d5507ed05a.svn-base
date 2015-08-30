using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Web.Control
{
    public class IPHelper
    {
        /// <summary>
        /// 穿过代理服务器取远程用户真实IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetRemoteIPAddress()
        {
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }

        }
    }
}
