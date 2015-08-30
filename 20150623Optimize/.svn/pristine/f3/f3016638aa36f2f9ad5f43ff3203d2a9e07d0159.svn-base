<%@ WebHandler Language="C#" Class="UpdateMenuToServer" %>

using System;
using System.Web;
using System.Net;
using System.IO;
using System.Text;

public class UpdateMenuToServer : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        
        context.Request.GetType();
        //context.Response.Write("Hello World");
        string jsonStr = context.Request.Params["MenuJson"];
        string Token = context.Request.Params["Token"];
        jsonStr = jsonStr.Substring(9, jsonStr.Length - 11);

        //jsonStr = "{\"button\":[{\"name\":\"推荐餐厅\",\"sub_button\":[{\"type\":\"click\",\"name\":\"北京\",\"key\":\"VA_WECHAT_BEIJING\",\"sub_button\":[]},{\"type\":\"click\",\"name\":\"上海\",\"key\":\"VA_WECHAT_SHANGHAI\",\"sub_button\":[]},{\"type\":\"click\",\"name\":\"广州\",\"key\":\"VA_WECHAT_GUANGZHOU\",\"sub_button\":[]},{\"type\":\"click\",\"name\":\"杭州\",\"key\":\"VA_WECHAT_HANGZHOU\",\"sub_button\":[]},{\"type\":\"click\",\"name\":\"深圳\",\"key\":\"VA_WECHAT_SHENZHEN\",\"sub_button\":[]}]},{\"name\":\"悠先资讯\",\"sub_button\":[{\"type\":\"click\",\"name\":\"本期大奖\",\"key\":\"VA_WECHAT_TOPPRICE\",\"sub_button\":[]},{\"type\":\"click\",\"name\":\"本期免单\",\"key\":\"VA_WECHAT_FREECASE\",\"sub_button\":[]},{\"type\":\"click\",\"name\":\"本期热菜\",\"key\":\"VA_WECHAT_HOTMENU\",\"sub_button\":[]},{\"type\":\"click\",\"name\":\"亲聆老板娘\",\"key\":\"VA_WECHAT_LANDLADY\",\"sub_button\":[]}]},{\"name\":\"悠先服务\",\"sub_button\":[{\"type\":\"click\",\"name\":\"悠先下载\",\"key\":\"VA_WECHAT_DOWNLOAD\",\"sub_button\":[]},{\"type\":\"click\",\"name\":\"悠先合作\",\"key\":\"VA_WECHAT_COOPERATION\",\"sub_button\":[]},{\"type\":\"click\",\"name\":\"常见问答\",\"key\":\"VA_WECHAT_Q_AND_A\",\"sub_button\":[]},{\"type\":\"click\",\"name\":\"意见建议\",\"key\":\"VA_WECHAT_PROPOSAL\",\"sub_button\":[]},{\"type\":\"click\",\"name\":\"投诉处理\",\"key\":\"VA_WECHAT_COMPLAINT\",\"sub_button\":[]}]}]}";

        string uriStr = "https://api.weixin.qq.com/cgi-bin/menu/create?access_token=" + Token;

        try
        {
            HttpWebRequest request = WebRequest.Create(uriStr) as HttpWebRequest;
            request.Method = "POST";
            request.ServicePoint.Expect100Continue = false;
            request.Timeout = 1000 * 60;
            request.ContentType = "application/x-www-form-urlencoded";
            Stream reqstream = request.GetRequestStream();
            byte[] b = Encoding.UTF8.GetBytes(jsonStr);
            reqstream.Write(b, 0, b.Length);

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