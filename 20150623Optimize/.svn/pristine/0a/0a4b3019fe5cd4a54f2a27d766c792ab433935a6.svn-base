<%@ WebHandler Language="C#" Class="GetCurrentMenu" %>

using System;
using System.Web;
using System.Net;
using System.IO;

public class GetCurrentMenu : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        string Token = context.Request["Token"];

        string uriStr = "https://api.weixin.qq.com/cgi-bin/menu/get?access_token=" + Token;

        try
        {
            HttpWebRequest request = WebRequest.Create(uriStr) as HttpWebRequest;
            request.Method = "GET";
            request.ServicePoint.Expect100Continue = false;
            request.Timeout = 1000 * 60;
            request.ContentType = "application/x-www-form-urlencoded";

            WebResponse webResponse = request.GetResponse();

            StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());

            string responseData = responseReader.ReadToEnd();

            context.Response.Write("(" + responseData + ")");
        }
        catch
        {
            context.Response.Write("('access_token':'请求出错，稍后重试.')");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}