using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Senparc.Weixin.MP;
using System.IO;
using WechatService;

public partial class wechatapi : System.Web.UI.Page
{
    private readonly string Token = "youxiandiancai";//与微信公众账号后台的Token设置保持一致，区分大小写。
    //private readonly string Token = "wpjluyu";//与微信公众账号后台的Token设置保持一致，区分大小写。
    protected void Page_Load(object sender, EventArgs e)
    {
        string signature = Request.QueryString["signature"];
        string timestamp = Request.QueryString["timestamp"];
        string nonce = Request.QueryString["nonce"];
        string echostr = Request.QueryString["echostr"];
        if (Request.HttpMethod == "GET")
        {
            //get method - 仅在微信后台填写URL验证时触发
            if (CheckSignature.Check(signature, timestamp, nonce, Token))
            {
                WriteContent(echostr); //返回字符串echostr表示验证通过
            }
            else
            {
                WriteContent("failed!");
            }
            Response.End();
        }
        else
        {
            //post method - 当有用户想公众账号发送消息时触发
            if (!CheckSignature.Check(signature, timestamp, nonce, Token))
            {
                WriteContent("参数错误!");
                Response.End();
                return;
            }

            //自定义MessageHandler，对微信请求的详细判断操作都在这里面。
            var messageHandler = new CustomMessageHandler(Request.InputStream);

            try
            {
                //测试时可开启此记录，帮助跟踪数据
                //messageHandler.RequestDocument.Save(Server.MapPath("~/Logs/wechatapi.txt"));
                //执行微信处理过程
                messageHandler.Execute();
                //测试时可开启，帮助跟踪数据
                //messageHandler.ResponseDocument.Save(Server.MapPath("~/Logs/wechatapi.txt"));
                WriteContent(messageHandler.ResponseDocument.ToString());
                return;
            }
            catch (Exception ex)
            {
                using (TextWriter tw = new StreamWriter(Server.MapPath("~/Logs/wechatapi.txt")))
                {
                    tw.WriteLine(ex.Message);
                    tw.WriteLine(ex.InnerException.Message);
                    if (messageHandler.ResponseDocument != null)
                    {
                        tw.WriteLine(messageHandler.ResponseDocument.ToString());
                    }
                    tw.Flush();
                    tw.Close();
                }
                WriteContent("");
            }
            finally
            {
                Response.End();
            }
        }
        
    }
    private void WriteContent(string str)
    {
        Response.Write(str);
    }
}