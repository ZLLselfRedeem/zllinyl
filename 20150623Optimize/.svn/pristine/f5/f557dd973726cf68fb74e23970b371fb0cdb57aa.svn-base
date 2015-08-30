using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Configuration;
using VAGastronomistMobileApp.Model;
using System.Transactions;
using System.Data;
using System.Drawing;
using CloudStorage;
using System.IO;

public partial class CompanyManage_MedalManage : System.Web.UI.Page
{
    string saveMedalImagePath = string.Empty;
    string queryMedalImagePath = string.Empty;
    string saveMedalImageShotPath = string.Empty;
    string queryMedalImageShotPath = string.Empty;
    private static string validExtension = WebConfig.Extension.ToString();
    protected void Page_Load(object sender, EventArgs e)
    {
        text.Value = Common.ToString(Request.QueryString["name"]);//回传页面显示公司名称
        if (!IsPostBack)
        {
            init_date.InnerHtml = "";
            QueryShopMedalImagePath();
            GetImageInfo();
        }
    }
    ///<summary>
    /// 获得门店上传勋章标记的路径
    /// </summary>
    protected void QueryShopMedalImagePath()
    {
        int shopId = Common.ToInt32(Request.QueryString["id"]);
        if (shopId != 0)
        {
            ShopOperate shopOperate = new ShopOperate();
            ShopInfo shopInfo = shopOperate.QueryShop(shopId);
            if (shopInfo != null)
            {
                saveMedalImagePath = WebConfig.ImagePath + shopInfo.shopImagePath.ToString() + WebConfig.ShopMedalImage;
                queryMedalImagePath = WebConfig.CdnDomain + saveMedalImagePath;
                saveMedalImageShotPath = WebConfig.ImagePath + shopInfo.shopImagePath.ToString();
                queryMedalImageShotPath = WebConfig.CdnDomain + saveMedalImageShotPath;
            }
        }
    }
    /// <summary>
    /// 显示勋章大小图片
    /// </summary>
    protected void GetImageInfo()
    {
        QueryShopMedalImagePath();
        int companyOrShopId = Common.ToInt32(Request.QueryString["id"]);
        int medalType = (int)VAMedalType.MEDAL_SHOP;//表示门店
        MedalOperate medalOperate = new MedalOperate();
        #region 显示小图
        DataTable dt = medalOperate.QueryMedalInfoTable(companyOrShopId, medalType, (int)VAMedalImageType.MEDAL_IMAGE_SMALL);//查询当前的上传的信息
        Repeater_MedalImage.DataSource = dt;
        Repeater_MedalImage.DataBind();
        for (int i = 0; i < Repeater_MedalImage.Items.Count; i++)
        {
            string imagePath = queryMedalImageShotPath + dt.Rows[i]["medalURL"].ToString();
            try
            {
                System.Web.UI.WebControls.Image Image_Image = Repeater_MedalImage.Items[i].FindControl("Image_MedalImage") as System.Web.UI.WebControls.Image;
                Image_Image.ImageUrl = imagePath;
                Label Label_MedalImage = Repeater_MedalImage.Items[i].FindControl("Label_MedalImage") as Label;
            }
            catch
            {
            }
            LinkButton Button_MedalImage = Repeater_MedalImage.Items[i].FindControl("Button_MedalImage") as LinkButton; //删除按钮
            Button_MedalImage.CommandName = dt.Rows[i]["id"].ToString();
        }
        #endregion
    }
    /// <summary>
    /// 删除小图片
    /// 删除数据库对应的三条数据
    /// </summary>
    protected void Repeater_MedalImage_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if ((string)e.CommandArgument == "delete")//删除
        {
            QueryShopMedalImagePath();
            DeleteOssImg(e.CommandName);
            bool result = DeleteImageFun(Common.ToInt64(e.CommandName));
            if (result == false)
            {
                label_message.Text = "删除失败";
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除失败！');</script>");
            }
            else
            {
                GetImageInfo();
            }
        }
    }
    protected bool DeleteImageFun(long id)
    {
        int companyOrShopId = 0;
        companyOrShopId = Common.ToInt32(Request.QueryString["id"]);
        MedalOperate medalOperate = new MedalOperate();
        bool result = medalOperate.DeleteMedalInfo(id, companyOrShopId);
        return result;
    }
    protected int GetCompanyOrShopId()
    {
        return Common.ToInt32(Request.QueryString["id"]);
    }
    /// <summary>
    /// 删除存储在OSS服务器上图片源文件
    /// </summary>
    /// <param name="commandName"></param>
    void DeleteOssImg(string commandName)
    {
        MedalOperate operate = new MedalOperate();
        DataTable dtMenuImg = operate.QueryMedalInfoByMedalId(Common.ToInt32(commandName));
        foreach (DataRow item in dtMenuImg.Rows)
        {
            CloudStorageOperate.DeleteObject(saveMedalImageShotPath + Common.ToString(item["medalURL"]));//删除原有的图片
        }
    }
    /// <summary>
    /// 上传操作
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_ShopImage_Click(object sender, EventArgs e)
    {
        QueryShopMedalImagePath();
        FileUpload fileUpload = FileUpload_MedalImage;
        label_message.Text = "";
        string extension = Path.GetExtension(Server.HtmlEncode(fileUpload.FileName));
        ImageUploadOperate imageUploadOperate = new ImageUploadOperate();
        int companyOrShopId = GetCompanyOrShopId();
        using (TransactionScope scope = new TransactionScope())
        {
            if (Common.ValidateExtension(extension) && (extension.Replace(".", "").ToLower() == "png" || extension.Replace(".", "").ToLower() == "jpg") || extension.Replace(".", "").ToLower() == "jpeg")
            {
                string uploadImageName = "1_" + companyOrShopId + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;//1_  开头表示小图
                if (saveMedalImagePath != "")
                {
                    saveMedalImagePath = saveMedalImagePath.Trim().ToString();
                    ShopOperate shopOperate = new ShopOperate();
                    #region 判断上传图片的信息
                    if (fileUpload.PostedFile.ContentLength / 1024 < 10)//获得的是文件的大小，单位kb
                    {
                        Bitmap originalBMP = new Bitmap(fileUpload.FileContent);//该上传图片文件
                        if (originalBMP.Width >= 120 && originalBMP.Height >= 120 && originalBMP.Width == originalBMP.Height)
                        {
                            #region 上传图片信息
                            fileUpload.SaveAs(Server.MapPath(WebConfig.Temp + uploadImageName)); //上传到临时目录下
                            string objectKey = saveMedalImagePath + uploadImageName; //图片在数据库中的实际位置                                
                            CloudStorageResult result = CloudStorageOperate.PutObject(objectKey, fileUpload, uploadImageName);
                            if (result.code)
                            {
                                MedalConnShopCompany medalConnShopCompany = new MedalConnShopCompany();
                                MedalOperate medalOperate = new MedalOperate();
                                medalConnShopCompany.companyOrShopId = Common.ToInt32(Request.QueryString["id"]);
                                medalConnShopCompany.medalType = (int)VAMedalType.MEDAL_SHOP;
                                medalConnShopCompany.medalDescription = "";
                                medalConnShopCompany.medalName = "";
                                long insertResultMedalConnShopCompany = medalOperate.InsertMedalConnShopCompany(medalConnShopCompany); //添加的是勋章的信息
                                if (insertResultMedalConnShopCompany > 0)
                                {
                                    MedalImageInfo medalImageInfo = new MedalImageInfo();
                                    medalImageInfo.medalId = Common.ToInt32(insertResultMedalConnShopCompany);
                                    medalImageInfo.medalScale = (int)VAMedalImageType.MEDAL_IMAGE_SMALL;
                                    medalImageInfo.medalURL = WebConfig.ShopMedalImage + uploadImageName;
                                    long medalImageInfoSmallResult = medalOperate.InsertMedalImageInfo(medalImageInfo);
                                    if (medalImageInfoSmallResult > 0)
                                    {
                                        label_message.Text = "上传成功";
                                        GetImageInfo(); //显示图片
                                        scope.Complete();
                                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('添加成功！');</script>");
                                    }
                                    else
                                    {
                                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('添加失败！');</script>");
                                    }
                                }
                            }
                            #endregion
                        }
                        else
                        {
                            label_message.Text = "上传图片尺寸必须大于120*120，比例必须为1：1";
                        }
                    }
                    else
                    {
                        label_message.Text = "上传图片大小必须小于10k";
                    }
                    #endregion
                }
                else
                {
                    label_message.Text = "上传失败，请重试";
                }
            }
        }
    }
}