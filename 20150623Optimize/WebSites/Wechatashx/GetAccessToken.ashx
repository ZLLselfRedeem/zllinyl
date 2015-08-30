﻿<%@ WebHandler Language="C#" Class="GetAccessToken" %>

using System;
using System.Web;
using System.Net;
using System.IO;

public class GetAccessToken : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");

        string AppId = context.Request["AppId"];
        string AppSecret = context.Request["AppSecret"];
        string uriStr = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + AppId + "&secret=" + AppSecret; //请求的地址

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