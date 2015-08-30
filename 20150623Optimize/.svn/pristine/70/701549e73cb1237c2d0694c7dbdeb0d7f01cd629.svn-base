using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using VAGastronomistMobileApp.WebPageDll;
using System.Configuration;

public partial class web_app_index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 官网手机版，给用户手机发送各个安装包下载链接
    /// </summary>
    /// <param name="mobile">用户手机号码</param>
    /// <returns></returns>
    [WebMethod]
    public static string SendMessage(string mobile)
    {
        string strResult = "", strMessage = "";

        if (!string.IsNullOrEmpty(mobile))
        {
            if (mobile.Trim().Length == 11)
            {
                strMessage = ConfigurationManager.AppSettings["SmsDownloadLink"].ToString();

                if (Common.SendMessageBySms(mobile, strMessage))
                {
                    strResult = "1";//短信发送成功
                }
                else
                {
                    strResult = "-3";//短信发送失败，请重试
                }
            }
            else
            {
                strResult = "-2";//目前的手机号码长度必须是11位
            }
        }
        else
        {
            strResult = "-1";//手机号码不能为空
        }
        return strResult;
    }
}