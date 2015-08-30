using CloudStorage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class SystemConfig_uploadImageConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    /// <summary>
    /// 上传广告图片到阿里云
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Upload_Click(object sender, EventArgs e)
    {
        string fileName = Server.HtmlEncode(fileUpload.FileName);
        string extension = System.IO.Path.GetExtension(fileName);
        if (fileName != "")
        {
            if (Common.ValidateExtension(extension) && (extension.Replace(".", "").ToLower() == "gif" || extension.Replace(".", "").ToLower() == "png" || extension.Replace(".", "").ToLower() == "jpg"))
            {
                if (fileUpload.PostedFile.ContentLength / 1024 < 1024)
                {
                    string virtualPath = string.Empty;
                    string imageName = string.Empty;
                    virtualPath = WebConfig.Advertisement + "common/"+DateTime.Now.Year+"/"+DateTime.Now.Month+"/";
                    imageName = DateTime.Now.ToString("ddHHmmssfff") + extension;
                    
                    string objectKey = WebConfig.ImagePath + virtualPath + imageName;//图片在数据库中的实际位置
                    CloudStorageResult result = CloudStorageOperate.PutObject(objectKey, fileUpload, imageName);

                    if (result.code)
                    {
                        this.Big_Img.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + virtualPath + imageName;//阿里云服务器读取显示图片信息
                        // 上传到阿里云后对应的图片地址
                        lblImgUrl.Text = this.Big_Img.ImageUrl;
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('上传失败');</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片必须小于1024KB');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请上传gif,png,jpg格式的图片！');</script>");
            }
        }
        
    }
}