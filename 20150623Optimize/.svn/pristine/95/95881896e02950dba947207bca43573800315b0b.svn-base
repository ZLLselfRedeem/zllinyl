<%@ WebHandler Language="C#" Class="wechatGongzhong" %>

using System;
using System.Web;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public class wechatGongzhong : IHttpHandler
{

    /// <summary>
    /// 使用JS-SDK的页面必须先注入配置信息
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        string response = "";
        string module = context.Request["m"] == null ? "" : Common.ToString(context.Request["m"]);
        if (!string.IsNullOrEmpty(module))
        {
            switch (module)
            {
                case "config"://获取权限验证配置信息
                    string url = context.Request["url"];
                    if (!string.IsNullOrEmpty(url))
                    {
                        WechatGongzhongOperate operate = new WechatGongzhongOperate();
                        WechatJsApiConfig config = operate.GetWechatJsApiConfig(url);
                        response = SysJson.JsonSerializer(config);
                    }
                    else
                    {
                        response = "{\"error\":\"-1\",\"msg\":\"参数传递错误\"}";
                    }
                    break;
                default:
                    break;
            }
            context.Response.Write(response);
        }
        else
        {
            response = "{\"error\":\"-1\",\"msg\":\"参数传递错误\"}";
            context.Response.Write(response); 
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}