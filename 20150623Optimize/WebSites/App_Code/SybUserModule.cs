using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
/// <summary>
///SybUserModule 的摘要说明
/// </summary>
public class SybUserModule : IHttpModule, IRequiresSessionState
{
    public SybUserModule()
    {
    }

    public void Init(HttpApplication application)
    {
        application.BeginRequest += (new EventHandler(this.Application_BeginRequest));
        application.PreRequestHandlerExecute += new EventHandler(application_PreRequestHandlerExecute);
    }

    private void Application_BeginRequest(Object source, EventArgs e)
    {
       
    }

    private void application_PreRequestHandlerExecute(object sender, EventArgs e)
    {
        //HttpContext context = HttpContext.Current;
        //var path = context.Request.Url.AbsolutePath.ToLower();
        //var isExit = path.Contains("companypages");
        //if (isExit && (path.Contains(".aspx") || path.Contains(".ashx"))
        //    && !path.Contains("login.aspx") && context.Session["MerchantsTreasureUserInfo"] == null)
        //{
        //    context.Response.Redirect("~/CompanyPages/ToLogin.htm");
        //}
    }

    public void Dispose()
    {
    }

}