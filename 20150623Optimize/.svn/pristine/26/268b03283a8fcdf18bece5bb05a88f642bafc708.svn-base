using LogDll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;

public partial class AppPages_weChat_callbackAddress : System.Web.UI.Page
{
    protected string message = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string state = Request.QueryString["state"];
            if (string.IsNullOrEmpty(state) || Session["weChatState"] == null)
            {
                message = "state为空";
                Response.End();
            }
            if (state != Session["weChatState"].ToString())
            {
                message = "state不对，请不要非法操作";
                Response.End();
            }
            Session["weChatState"] = null;
            //取code
            string code = Request.QueryString["code"];
            if (string.IsNullOrEmpty(code))
            {
                message = "code为空";
                Response.End();
            }
            //取令牌
            Tuple<string, string> accessTokenAndOpenId = WeChatLogin.GetCodeOfAccessToken(code);
            if (accessTokenAndOpenId == null)
            {
                message = "令牌为空";
                Response.End();
            }
            Tuple<int, string> addAndId = WeChatLogin.AddWeChatUser(accessTokenAndOpenId.Item1, accessTokenAndOpenId.Item2);
            if (addAndId.Item1 < 1)
            {
                message = "添加用户失败";
                Response.End();
            }

            HttpCookie ck = new HttpCookie("weChatUserId");//写入cookies
            ck.Value = addAndId.Item2;
            ck.Expires = DateTime.Now.AddYears(1);
            //ck.Expires = DateTime.Now.AddMinutes(2);
            Response.AppendCookie(ck);

            string url = Request.QueryString["from"];
            //url += "&weChatUserId=" + addAndId.Item2;

            Response.Redirect(url);
        }
        catch (Exception ex)
        {
            LogManager.WriteLog(LogFile.Trace, ex.ToString());
        }
    }
}