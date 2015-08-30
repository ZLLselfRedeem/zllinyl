<%@ WebHandler Language="C#" Class="doSybUploadImg" %>

using System;
using System.IO;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;

public class doSybUploadImg : IHttpHandler
{
    /// <summary>
    /// created by wangc 20140618
    /// 功能：收银宝后台图片上传处理程序
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        context.Response.Charset = "utf-8";
        int shopId = Common.ToInt32(context.Request["shopId"]);
        string type = Common.ToString(context.Request["type"]);//操作模块
        try
        {
            HttpPostedFile image_upload = context.Request.Files["Filedata"];//获取图片流文件
            Stream img_Stream = image_upload.InputStream;
            string resultWrite = string.Empty;
            switch (type)
            {
                case "shoprevelationimg"://门店环境图片
                    resultWrite = MerchantInfoOperate.SaveShopRevelationImage(shopId, img_Stream);
                    break;
                case "shoplogo"://门店LOGO图片
                    resultWrite = MerchantInfoOperate.SaveShopLogoImage(shopId, img_Stream);
                    break;
                case "shoppublicityphoto"://门店背景图片上传
                    resultWrite = MerchantInfoOperate.SaveShopPublicityPhotoImage(shopId, img_Stream);
                    break;
                default:
                    break;
            }
            context.Response.StatusCode = 200;
            context.Response.Write(resultWrite);
        }
        catch
        {
            //出现异常，上传图片失败
            context.Response.StatusCode = 500;
            context.Response.Write("An error occured");
        }
        finally
        {
            context.Response.End();
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