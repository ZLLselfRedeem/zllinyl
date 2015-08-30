using Autofac;
using Autofac.Integration.Web;
using System;
using System.Web;
using System.Web.Security;
using VA.Cache.Distributed;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

namespace VA
{
    /// <summary>
    /// GlobalApplication 的摘要说明
    /// </summary>
    public partial class GlobalApplication : HttpApplication, IContainerProviderAccessor
    {
        static IContainerProvider _containerProvider;

        // Instance property that will be used by Autofac HttpModules
        // to resolve and inject dependencies.
        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            //在应用程序启动时运行的代码



            // Once you're done registering things, set the container
            // provider up with your registrations.
            _containerProvider = new ContainerProvider(Bootstrapper.Container);
            //初始化Memcached通讯链接池
            SockIOPoolExt.Init();



        }

        void Application_End(object sender, EventArgs e)
        {
            //在应用程序关闭时运行的代码

        }

        void Application_Error(object sender, EventArgs e)
        {
            //在出现未处理的错误时运行的代码
            //add by wangcheng
            Exception ex = Server.GetLastError();
            string strException = ex.InnerException != null ? ex.InnerException.StackTrace : string.Empty;
            if (strException.Contains("CompanyPages"))//判断异常是从收银宝模块过来的
            {
                Server.Transfer("~/CompanyPages/login.aspx");//跳转到收银宝登录页面
            }
        }

        void Session_Start(object sender, EventArgs e)
        {
            //在新会话启动时运行的代码
            Session.Timeout = 300;
        }

        void Session_End(object sender, EventArgs e)
        {
            //在会话结束时运行的代码。 
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式 
            //设置为 StateServer 或 SQLServer，则不会引发该事件。
            //Response.Redirect("Login.aspx");
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            ////防SQL注入代码
            //SqlInject myCheck = new SqlInject(this.Request);
            //myCheck.CheckSqlInject();

            try
            {
                string session_param_name = "ASPSESSID";
                string session_cookie_name = "ASP.NET_SESSIONID";

                if (HttpContext.Current.Request.Form[session_param_name] != null)
                {
                    UpdateCookie(session_cookie_name, HttpContext.Current.Request.Form[session_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[session_param_name] != null)
                {
                    UpdateCookie(session_cookie_name, HttpContext.Current.Request.QueryString[session_param_name]);
                }
            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                Response.Write("Error Initializing Session");
            }

            try
            {
                string auth_param_name = "AUTHID";
                string auth_cookie_name = FormsAuthentication.FormsCookieName;

                if (HttpContext.Current.Request.Form[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
                }
                else if (HttpContext.Current.Request.QueryString[auth_param_name] != null)
                {
                    UpdateCookie(auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name]);
                }

            }
            catch (Exception)
            {
                Response.StatusCode = 500;
                Response.Write("Error Initializing Forms Authentication");
            }
        }

        void UpdateCookie(string cookie_name, string cookie_value)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
            if (cookie == null)
            {
                cookie = new HttpCookie(cookie_name);
                HttpContext.Current.Request.Cookies.Add(cookie);
            }
            cookie.Value = cookie_value;
            HttpContext.Current.Request.Cookies.Set(cookie);
        }
    }
}