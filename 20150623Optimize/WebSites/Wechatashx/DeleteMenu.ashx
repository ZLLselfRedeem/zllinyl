<%@ WebHandler Language="C#" Class="DeleteMenu" %>

using System;
using System.Web;
using System.Net;
using System.IO;

public class DeleteMenu : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        string Token = context.Request["Token"];

        string uriStr = "https://api.weixin.qq.com/cgi-bin/menu/delete?access_token=" + Token;

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
            //{"errcode":0,"errmsg":"ok"}
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