using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Transactions;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using CloudStorage;

public partial class ShopManage_ShopImageRevelation_ : System.Web.UI.Page
{
    static string saveImagePath = string.Empty;//上传图片路径
    static string queryImagePath = string.Empty;//读取图片路径
    private static string validExtension = WebConfig.Extension;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["shopId"] != null)
            {
                QueryShopImagePath();
                GetImageInfo();
                lbShopEnvironmentSpace.Text = WebConfig.ShopEnvironmentSpace.ToString() + "KB";
            }
        }
    }
    /// <summary>
    /// 获得上传图片的路径
    /// </summary>
    protected void QueryShopImagePath()
    {
        int shopId = Common.ToInt32(Request.QueryString["shopId"]);
        ShopOperate shopOperate = new ShopOperate();
        ShopInfo shopInfo = shopOperate.QueryShop(shopId);
        if (shopInfo != null)
        {
            saveImagePath = WebConfig.ImagePath + shopInfo.shopImagePath.ToString() + WebConfig.ShopImg;
            queryImagePath = WebConfig.CdnDomain + WebConfig.ImagePath + shopInfo.shopImagePath.ToString() + WebConfig.ShopImg;
        }
    }
    /// <summary>
    /// 上传图片
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_ShopImage_Click(object sender, EventArgs e)
    {
        UploadImage(FileUpload_ShopImage);
    }
    /// <summary>
    /// 上传
    /// </summary>
    /// <param name="fileUpload"></param>
    protected void UploadImage(FileUpload fileUpload)
    {
        label_message.Text = "";
        int shopId = Common.ToInt32(Request.QueryString["shopId"]);
        string fileName = Server.HtmlEncode(fileUpload.FileName);
        string extension = System.IO.Path.GetExtension(fileName);
        using (TransactionScope scope = new TransactionScope())
        {
            if (Common.ValidateExtension(extension) && (extension.Replace(".", "").ToLower() == "jpg" || extension.Replace(".", "").ToLower() == "png"))
            {
                string uploadImageName = shopId + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                if (saveImagePath != "")
                {
                    saveImagePath = saveImagePath.Trim().ToString();
                    ShopOperate shopOperate = new ShopOperate();
                    DataTable dt = shopOperate.QueryShopRevealImageInfo(shopId);
                    if (dt.Rows.Count < 4)
                    {
                        #region 判断上传图片的信息
                        if (fileUpload.PostedFile.ContentLength / 1024 < WebConfig.ShopEnvironmentSpace)//获得的是文件的大小，单位kb
                        {
                            Bitmap originalBMP = new Bitmap(fileUpload.FileContent);//该上传图片文件
                            if (Math.Floor(Common.ToDouble(originalBMP.Width * 3 / 4)) == originalBMP.Height)
                            {
                                if (originalBMP.Width >= 652)
                                {
                                    #region 上传图片信息
                                    CloudStorageResult result = CloudStorageOperate.PutObject(saveImagePath + uploadImageName, fileUpload, uploadImageName);
                                    int resultInsert = shopOperate.InsertShopRevealImage(shopId, uploadImageName, 1);//记录插入数据库
                                    if (resultInsert > 0 && result.code)
                                    {
                                        label_message.Text = "上传成功";
                                        GetImageInfo();
                                        scope.Complete();
                                    }
                                    else
                                    {
                                        label_message.Text = "上传失败，请重试";
                                    }
                                    #endregion
                                }
                                else
                                {
                                    label_message.Text = "图片最小尺寸应为652*489";
                                }
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('上传失败，图片比例应为4*3！');</script>");
                            }
                        }
                        else
                        {
                            label_message.Text = "图片必须小于" + WebConfig.ShopEnvironmentSpace + "KB";
                        }

                        #endregion
                    }
                    else
                    {
                        label_message.Text = "最多只能上传4张图片";
                    }
                }
                else
                {
                    label_message.Text = "上传失败，请重试";
                }
            }
            else
            {
                label_message.Text = "请上传jpg格式的图片";
            }
        }
    }

    /// <summary>
    /// 显示图片
    /// </summary>
    protected void GetImageInfo()
    {
        int shopId = Common.ToInt32(Request.QueryString["shopId"].ToString());
        ShopOperate shopOperate = new ShopOperate();
        DataTable dt = shopOperate.QueryShopRevealImageInfo(shopId);
        Repeater_ShopImage.DataSource = dt;
        Repeater_ShopImage.DataBind();
        for (int i = 0; i < Repeater_ShopImage.Items.Count; i++)
        {
            string imagePath = queryImagePath + dt.Rows[i]["revealImageName"].ToString();
            try
            {
                //显示图片
                System.Web.UI.WebControls.Image Image_ShopImage = Repeater_ShopImage.Items[i].FindControl("Image_ShopImage") as System.Web.UI.WebControls.Image;
                Image_ShopImage.ImageUrl = imagePath + "@180w_135h_50Q";
                //图片规格
                Label Label_ShopImage = Repeater_ShopImage.Items[i].FindControl("Label_ShopImage") as Label;
            }
            catch (Exception)
            {
            }
            //删除按钮
            LinkButton Button_ShopImage = Repeater_ShopImage.Items[i].FindControl("Button_ShopImage") as LinkButton;
            Button_ShopImage.CommandName = dt.Rows[i]["revealImageName"].ToString();
        }
    }
    /// <summary>
    /// 删除图片
    /// </summary>
    protected void Repeater_ShopImage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        ShopOperate shopOperate = new ShopOperate();
        bool result = shopOperate.DeleteShopRevealImage(e.CommandName, Common.ToInt32(Request.QueryString["shopId"].ToString()));
        ShopInfo shopInfo = shopOperate.QueryShop(Common.ToInt32(Request.QueryString["shopId"].ToString()));
        if (result == false)
        {
            label_message.Text = "删除失败";
        }
        else
        {
            CloudStorageOperate.DeleteObject(saveImagePath + e.CommandName);//删除原有的图片
            GetImageInfo();
        }
    }
}