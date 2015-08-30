<%@ WebHandler Language="C#" Class="UploadVoiceFile" %>

using System;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

public class UploadVoiceFile : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        //context.Response.Write("Hello World");
        string token = context.Request["token"];
        int selectID = Convert.ToInt32(context.Request["selectID"]); //取出语音文件用
        string uriStr = "http://file.api.weixin.qq.com/cgi-bin/media/upload?access_token=" + token + "&type=voice";

        try
        {
            HttpWebRequest request = WebRequest.Create(uriStr) as HttpWebRequest;
            request.Method = "POST";
            request.ServicePoint.Expect100Continue = false;
            request.Timeout = 1000 * 60;
            request.ContentType = "application/x-www-form-urlencoded";
            
            //取出文件流，写入request
            Stream requestStream = request.GetRequestStream();
            string[] files = Directory.GetFiles(context.Server.MapPath("~/UploadFiles/LandladyVoice/"));
            StreamReader sr = new StreamReader(files[0]);
            string str = sr.ReadToEnd();
            byte[] postData = Encoding.Default.GetBytes(str);
            requestStream.Write(postData, 0, postData.Length);
            
            
            WebResponse webResponse = request.GetResponse();

            StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());
            
            //{"type":"TYPE","media_id":"MEDIA_ID","created_at":123456789}正确情况下返回
            //{"errcode":40004,"errmsg":"invalid media type"}错误情况
            string responseData = responseReader.ReadToEnd();
            string[] split = responseData.Split(':', ',');
            if (responseData.Contains("errmsg"))
            {
                string sret = "({'status':'出错了.." + split[3].TrimEnd('}') + "'})";
                context.Response.Write(sret);
            }
            else
            {
                //返回media_id 记录下来这个media_id,更新到数据库
                VAGastronomistMobileApp.WebPageDll.WechatLandladyVoiceOperate wvo = new VAGastronomistMobileApp.WebPageDll.WechatLandladyVoiceOperate();
                if (wvo.UpdateMediaIDAndStatus(split[3], selectID) > 0)
                    context.Response.Write("({\"status\":\"OK.\"})");
                else
                    context.Response.Write("({\"status\":\"更新media_id失败.\"})");
            }
        }
        catch
        {
            context.Response.Write("('status':'请求出错，稍后重试.')");
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}