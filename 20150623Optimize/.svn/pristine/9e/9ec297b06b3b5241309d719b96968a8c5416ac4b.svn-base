using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using System.Transactions;
using Web.Control.DDL;
using System.Net;
using System.Net.Sockets;
using CloudStorage;
using System.Drawing;
using VAEncryptDecrypt;

public partial class Messages_ActivityMessageDetail : System.Web.UI.Page
{
    private static DataTable dt = new DataTable();
    private static int ID = 0;
    private static List<CouponList> list = new List<CouponList>();
    private static List<Activity> listActivity = new List<Activity>();
    private BatchMoneyOperate bmo = new BatchMoneyOperate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(ddlCity);
            ddlCity.Items.Remove(ddlCity.Items[ddlCity.Items.Count - 1]);
            new MessageFirstTitleDropDownList().BindMessageFirstTitle(ddlMessageFirstTitle, Common.ToInt32(ddlCity.SelectedValue), false);
            ID = Common.ToInt32(Request.QueryString["id"]);
            if (ID != 0)
            {
                //加载数据
                ActivityMessageOperate amo = new ActivityMessageOperate();
                DataTable dt = amo.ActivityMessageDetail(ID);
                if (dt.Rows.Count > 0)
                {
                    tbName.Text = dt.Rows[0]["Name"].ToString();
                }
                else
                {
                    return;
                }
                ddlCity.SelectedValue = dt.Rows[0]["CityID"].ToString();
                ddlCity_SelectedIndexChanged(sender, e);
                ddlMessageFirstTitle.SelectedValue = dt.Rows[0]["MessageFirstTitleID"].ToString();
                btnUpdate.Text = "修改活动";
                if (Common.ToInt32(dt.Rows[0]["MsgType"]) == (int)MsgType.PureText)
                {
                    rbPureText.Checked = true;
                    rbPureText_CheckedChanged(sender, e);
                    Big_Img_Log.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + dt.Rows[0]["ActivityLogo"].ToString();
                    HiddenField_Image_Log.Value = dt.Rows[0]["ActivityLogo"].ToString();
                    tbActivityExplain.Text = dt.Rows[0]["ActivityExplain"].ToString();
                }
                else if (Common.ToInt32(dt.Rows[0]["MsgType"]) == (int)MsgType.SpecialAdvertisement)
                {
                    rbSpecialAdvertisement.Checked = true;
                    rbSpecialAdvertisement_CheckedChanged(sender, e);
                    tbAdvertisementURL.Text = dt.Rows[0]["AdvertisementURL"].ToString();
                    Big_Img_Log.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + dt.Rows[0]["ActivityLogo"].ToString();
                    HiddenField_Image_Log.Value = dt.Rows[0]["ActivityLogo"].ToString();
                    HiddenField_Image.Value = dt.Rows[0]["AdvertisementAddress"].ToString();
                    Big_Img.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + dt.Rows[0]["AdvertisementAddress"].ToString();
                }
                else if (Common.ToInt32(dt.Rows[0]["MsgType"]) == (int)MsgType.RedEnvelopeAdvertisement)
                {
                    rbRedEnvelopeAdvertisement.Checked = true;
                    rbRedEnvelopeAdvertisement_CheckedChanged(sender, e);
                    foreach (ListItem item in rblActivity.Items)
                    {
                        if (item.Value == dt.Rows[0]["ActivityID"].ToString())
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                    Big_Img_Log.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + dt.Rows[0]["ActivityLogo"].ToString();
                    HiddenField_Image_Log.Value = dt.Rows[0]["ActivityLogo"].ToString();
                    HiddenField_Image.Value = dt.Rows[0]["AdvertisementAddress"].ToString();
                    Big_Img.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + dt.Rows[0]["AdvertisementAddress"].ToString();
                }
                else if (Common.ToInt32(dt.Rows[0]["MsgType"]) == (int)MsgType.CommercialTenantPackage)
                {
                    rbCommercialTenantPackage.Checked = true;
                    tbShopName.Text = dt.Rows[0]["ShopName"].ToString();
                    rbCommercialTenantPackage_CheckedChanged(sender, e);
                    SearchCoupon();
                    Big_Img_Log.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + dt.Rows[0]["ActivityLogo"].ToString();
                    HiddenField_Image_Log.Value = dt.Rows[0]["ActivityLogo"].ToString();
                    HiddenField_Image.Value = dt.Rows[0]["AdvertisementAddress"].ToString();
                    Big_Img.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + dt.Rows[0]["AdvertisementAddress"].ToString();
                    foreach (ListItem item in rblCoupon.Items)
                    {
                        if (item.Value == dt.Rows[0]["CouponID"].ToString())
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }
            else
            {
                btnUpdate.Text = "发布活动";
                rbPureText.Checked = true;
                rbPureText_CheckedChanged(sender, e);

                //ShopOperate so = new ShopOperate();
                string Path = "Common/viewalloc.png";// so.getDefaultLogPath();
                HiddenField_Image_Log.Value = Path;
                Big_Img_Log.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + Path;
            }
        }
        else
        {
            
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PackageManager.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //验证
        if(tbName.Text.Trim().Equals(string.Empty))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('活动名称不能为空')</script>");
            return;
        }

        if (Common.ToInt32(ddlMessageFirstTitle.SelectedValue) == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('活动标签不能为空')</script>");
            return;
        }

        ActivityMessage model = new ActivityMessage();
        model.AdvertisementAddress = string.Empty;
        model.AdvertisementURL = string.Empty;
        model.ActivityExplain = string.Empty;
        model.ActivityID = 0;
        model.CouponID = 0;
        model.ShopID = 0;

        model.Name = tbName.Text;
        if (HiddenField_Image_Log.Value.Equals(string.Empty))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('活动LOGO不能为空')</script>");
            return;
        }
        model.ActivityLogo = HiddenField_Image_Log.Value;
        model.CityID = Common.ToInt32(ddlCity.SelectedValue);
        model.MessageFirstTitleID = Common.ToInt32(ddlMessageFirstTitle.SelectedValue);
        if (rbPureText.Checked)
        {
            if (tbActivityExplain.Text.Trim().Equals(string.Empty))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('活动说明不能为空')</script>");
                return;
            }
            model.MsgType = (int)MsgType.PureText;
            model.ActivityExplain = tbActivityExplain.Text;
        }
        else if (rbSpecialAdvertisement.Checked)
        {
            if (tbAdvertisementURL.Text.Trim().Equals(string.Empty))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('广告地址不能为空')</script>");
                return;
            }
            model.MsgType = (int)MsgType.SpecialAdvertisement;
            if (!IsURL(tbAdvertisementURL.Text.Trim()))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('广告地址无法访问')</script>");
                return;
            }
            model.AdvertisementURL = tbAdvertisementURL.Text.Trim();
            if (HiddenField_Image.Value.Equals(string.Empty))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('活动广告图不能为空')</script>");
                return;
            }
            model.AdvertisementAddress = HiddenField_Image.Value;
        }
        else if (rbRedEnvelopeAdvertisement.Checked)
        {
            ActivityType activityType = new ActivityType();
            foreach (ListItem item in rblActivity.Items)
            {
                if (item.Selected == true)
                {
                    model.ActivityID=Common.ToInt32(item.Value);
                    break;
                }
            }
            if (rblActivity.Items.Count == 0 || model.ActivityID==null)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选中一条红包活动')</script>");
                return;
            }

            foreach (Activity ac in listActivity)
            {
                if (ac.activityId == model.ActivityID)
                {
                    activityType = ac.activityType;
                    break;
                }
            }

            string activityId = HttpUtility.UrlEncode(EncryptDecrypt.Encrypt(rblActivity.SelectedValue.ToString(), "va"));

            if (activityType == ActivityType.节日免单红包)
            {
                model.AdvertisementURL = WebConfig.ServerDomain +
                    "AppPages/christmasEnvelope/?activityId=" + activityId + "&cookie={1}&mobilephone={2}&type=app";// BigRedEnvelope/Index.html?activityId=
            }
            else
            {
                model.AdvertisementURL = WebConfig.ServerDomain +
                    "AppPages/RedEnvelope/TreasureChest.aspx?activityId=" + activityId + "&cookie={1}&mobilephone={2}&type=app";
            }

            model.MsgType = (int)MsgType.RedEnvelopeAdvertisement;
            if (HiddenField_Image.Value.Equals(string.Empty))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('活动广告图不能为空')</script>");
                return;
            }
            model.AdvertisementAddress = HiddenField_Image.Value;
        }
        else if (rbCommercialTenantPackage.Checked)
        {
            foreach (ListItem item in rblCoupon.Items)
            {
                if (item.Selected == true)
                {
                    model.CouponID = Common.ToInt32(item.Value);
                }
            }
            if (rblCoupon.Items.Count == 0 || model.CouponID == null)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选中一条礼券')</script>");
                return;
            }
            model.MsgType = (int)MsgType.CommercialTenantPackage;
            if (HiddenField_Image.Value.Equals(string.Empty))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('活动广告图不能为空')</script>");
                return;
            }
            model.AdvertisementAddress = HiddenField_Image.Value;

            foreach (CouponList c in list)
            {
                if (c.CouponId == model.CouponID)
                {
                    model.ShopID = c.ShopID;
                    break;
                }
            }
        }
        model.CreateUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
        model.CreateDate = DateTime.Now;
        model.UpdateUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
        model.UpdateDate = model.CreateDate;

        ActivityMessageOperate amo = new ActivityMessageOperate();
        if (ID == 0)
        {
            //insert
            if (amo.Insert(model) == 1)
            {
                Response.Redirect("ActivityMessageManage.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('添加失败')</script>");
                return;
            }
        }
        else
        {
            //update
            model.ID = ID;
            if (amo.Update(model) == 1)
            {
                Response.Redirect("ActivityMessageManage.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败')</script>");
                return;
            }
        }
    }

    protected void Button_Upload_Click(object sender, EventArgs e)
    {
        try
        {
            string fileName = Server.HtmlEncode(fileUpload.FileName);
            string extension = System.IO.Path.GetExtension(fileName);
            if (fileName != "")
            {
                if (Common.ValidateExtension(extension) && (extension.Replace(".", "").ToLower() == "png" || extension.Replace(".", "").ToLower() == "jpg"))
                {
                    if (fileUpload.PostedFile.ContentLength / 1024 < WebConfig.BannerSpace)
                    {
                        string virtualPath = string.Empty;
                        string imageName = string.Empty;

                        virtualPath = "ActivityMessage/";
                        if (rbPureText.Checked)
                        {
                            virtualPath += "PureText/";
                            imageName = "PureText" + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                        }
                        else if (rbSpecialAdvertisement.Checked)
                        {
                            virtualPath += "SpecialAdvertisement/";
                            imageName = "SpecialAdvertisement" + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                        }
                        else if (rbRedEnvelopeAdvertisement.Checked)
                        {
                            virtualPath += "RedEnvelopeAdvertisement/";
                            imageName = "RedEnvelopeAdvertisement" + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                        }
                        else if (rbCommercialTenantPackage.Checked)
                        {
                            virtualPath += "CommercialTenantPackage/";
                            imageName = "CommercialTenantPackage" + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                        }

                        #region 判断上传图片的信息
                        Bitmap originalBMP = new Bitmap(fileUpload.FileContent);//该上传图片文件
                        int width = originalBMP.Width;
                        int height = originalBMP.Height;
                        if (Math.Floor(Common.ToDouble(width * 3 / 5)) == height)
                        {
                            if (width >= 640)
                            {
                                string objectKey = WebConfig.ImagePath + virtualPath + imageName;//图片在数据库中的实际位置
                                CloudStorageResult result = CloudStorageOperate.PutObject(objectKey, fileUpload, imageName);

                                if (result.code)
                                {
                                    HiddenField_Image.Value = virtualPath + imageName;
                                    this.Big_Img.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + virtualPath + imageName;//阿里云服务器读取显示图片信息
                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('上传失败');</script>");
                                }
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片最小尺寸应为640*384！');</script>");
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片比例应为5:3！');</script>");
                        }
                        #endregion
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片必须小于" + WebConfig.BannerSpace + "KB');</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请上传png格式的图片！');</script>");
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            HeadControlDataBind(sender, e);
        }
    }
    protected void Button_Upload_Log_Click(object sender, EventArgs e)
    {
        try
        {
            string fileName = Server.HtmlEncode(fileUploadLog.FileName);
            string extension = System.IO.Path.GetExtension(fileName);
            if (fileName != "")
            {
                if (Common.ValidateExtension(extension) && (extension.Replace(".", "").ToLower() == "png" || extension.Replace(".", "").ToLower() == "jpg"))
                {
                    if (fileUploadLog.PostedFile.ContentLength / 100 < WebConfig.BannerSpace)
                    {
                        string virtualPath = string.Empty;
                        string imageName = string.Empty;

                        virtualPath = "ActivityMessageLog/";
                        if (rbPureText.Checked)
                        {
                            virtualPath += "PureText/";
                            imageName = "PureText" + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                        }
                        else if (rbSpecialAdvertisement.Checked)
                        {
                            virtualPath += "SpecialAdvertisement/";
                            imageName = "SpecialAdvertisement" + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                        }
                        else if (rbRedEnvelopeAdvertisement.Checked)
                        {
                            virtualPath += "RedEnvelopeAdvertisement/";
                            imageName = "RedEnvelopeAdvertisement" + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                        }
                        else if (rbCommercialTenantPackage.Checked)
                        {
                            virtualPath += "CommercialTenantPackage/";
                            imageName = "CommercialTenantPackage" + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                        }

                        #region 判断上传图片的信息
                        Bitmap originalBMP = new Bitmap(fileUploadLog.FileContent);//该上传图片文件
                        int width = originalBMP.Width;
                        int height = originalBMP.Height;
                        if (Math.Floor(Common.ToDouble(width * 1 / 1)) == height)
                        {
                            if (width >= 68)
                            {
                                string objectKey = WebConfig.ImagePath + virtualPath + imageName;//图片在数据库中的实际位置
                                CloudStorageResult result = CloudStorageOperate.PutObject(objectKey, fileUploadLog, imageName);

                                if (result.code)
                                {
                                    HiddenField_Image_Log.Value = virtualPath + imageName;
                                    this.Big_Img_Log.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + virtualPath + imageName;//阿里云服务器读取显示图片信息
                                }
                                else
                                {
                                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('上传失败');</script>");
                                }
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片最小尺寸应为68*68！');</script>");
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片比例应为1:1！');</script>");
                        }
                        #endregion
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片必须小于100KB');</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请上传png格式的图片！');</script>");
                }
            }
        }
        catch (Exception ex)
        {

        }
        finally
        {
            HeadControlDataBind(sender,e);
        }
    }
    protected void rbPureText_CheckedChanged(object sender, EventArgs e)
    {
        if (rbPureText.Checked)
        {
            this.Title = "发布"+rbPureText.Text;
            HyperLink hk = (HyperLink)HeadControl1.FindControl("HyperLink_NavigationUrl");
            Label lbName = (Label)HeadControl1.FindControl("Label_HeadName");
            Label lbName2 = (Label)HeadControl1.FindControl("Label_HeadName2");
            lbName.Text = "发布" + rbPureText.Text;
            lbName2.Text = "发布" + rbPureText.Text;
            hk.Text = "发布" + rbPureText.Text;
            AdvertisementURL.Visible = false;
            ShopName.Visible = false;
            Activity.Visible = false;
            Coupon.Visible = false;
            AdvertisementAddress.Visible = false;
            ActivityExplain.Visible = true;
        }
    }
    protected void rbSpecialAdvertisement_CheckedChanged(object sender, EventArgs e)
    {
        if (rbSpecialAdvertisement.Checked)
        {
            this.Title = "发布" + rbSpecialAdvertisement.Text;
            HyperLink hk = (HyperLink)HeadControl1.FindControl("HyperLink_NavigationUrl");
            Label lbName = (Label)HeadControl1.FindControl("Label_HeadName");
            Label lbName2 = (Label)HeadControl1.FindControl("Label_HeadName2");
            lbName.Text = "发布" + rbSpecialAdvertisement.Text;
            lbName2.Text = "发布" + rbSpecialAdvertisement.Text;
            hk.Text = "发布" + rbSpecialAdvertisement.Text;
            AdvertisementURL.Visible = true;
            ShopName.Visible = false;
            Activity.Visible = false;
            Coupon.Visible = false;
            AdvertisementAddress.Visible = true;
            ActivityExplain.Visible = false;
        }
    }
    protected void rbRedEnvelopeAdvertisement_CheckedChanged(object sender, EventArgs e)
    {
        if (rbRedEnvelopeAdvertisement.Checked)
        {
            this.Title = "发布" + rbRedEnvelopeAdvertisement.Text;
            HyperLink hk = (HyperLink)HeadControl1.FindControl("HyperLink_NavigationUrl");
            Label lbName = (Label)HeadControl1.FindControl("Label_HeadName");
            Label lbName2 = (Label)HeadControl1.FindControl("Label_HeadName2");
            lbName.Text = "发布" + rbRedEnvelopeAdvertisement.Text;
            lbName2.Text = "发布" + rbRedEnvelopeAdvertisement.Text;
            hk.Text = "发布" + rbRedEnvelopeAdvertisement.Text;
            AdvertisementURL.Visible = false;
            ShopName.Visible = false;
            Activity.Visible = true;
            Coupon.Visible = false;
            AdvertisementAddress.Visible = true;
            ActivityExplain.Visible = false;
            SearchActivity();
        }
    }
    protected void rbCommercialTenantPackage_CheckedChanged(object sender, EventArgs e)
    {
        if (rbCommercialTenantPackage.Checked)
        {
            this.Title = "发布" + rbCommercialTenantPackage.Text;
            HyperLink hk = (HyperLink)HeadControl1.FindControl("HyperLink_NavigationUrl");
            Label lbName = (Label)HeadControl1.FindControl("Label_HeadName");
            Label lbName2 = (Label)HeadControl1.FindControl("Label_HeadName2");
            lbName.Text = "发布" + rbCommercialTenantPackage.Text;
            lbName2.Text = "发布" + rbCommercialTenantPackage.Text;
            hk.Text = "发布" + rbCommercialTenantPackage.Text;
            AdvertisementURL.Visible = false;
            ShopName.Visible = true;
            Activity.Visible = false;
            Coupon.Visible = true;
            AdvertisementAddress.Visible = true;
            ActivityExplain.Visible = false;
        }
    }
    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        SearchCoupon();
    }

    private void SearchCoupon()
    {
        CouponOperate co = new CouponOperate();
        list = co.GetCouponListByShopName(tbShopName.Text);
        rblCoupon.DataSource = list;
        rblCoupon.DataValueField = "CouponId";
        rblCoupon.DataTextField = "CouponName";
        if (list == null || list.Count == 0)
        {
            CommonPageOperate.AlterMsg(this, "门店不存在,或该门店下不存在礼券!");
            return;
        }
        this.rblCoupon.DataBind();
        this.rblCoupon.SelectedIndex = 0;

        ShopOperate so = new ShopOperate();
        string Path = so.getDefaultLogPath(list[0].ShopID);
        HiddenField_Image_Log.Value = Path;
        Big_Img_Log.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + Path;

    }

    private void SearchActivity()
    {
        ActivityOperate ao=new ActivityOperate();
        listActivity = ao.QueryAllActivityNew();
       
        if (listActivity == null || listActivity.Count == 0)
        {
            CommonPageOperate.AlterMsg(this, "没有有效的红包活动!");
            return;
        }
        rblActivity.DataSource = listActivity;
        rblActivity.DataTextField = "Name";
        rblActivity.DataValueField = "activityId";
        rblActivity.DataBind();
        rblActivity.SelectedIndex = 0;
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        new MessageFirstTitleDropDownList().BindMessageFirstTitle(ddlMessageFirstTitle, Common.ToInt32(ddlCity.SelectedValue), false);
    }

    private bool IsURL(String url)
    {
        try
        {
            // Creates an HttpWebRequest for the specified URL.
            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            // 有些网站会阻止程序访问，需要加入下面这句
            myHttpWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko";
            myHttpWebRequest.Method = "GET";
            // Sends the HttpWebRequest and waits for a response.
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
                Console.WriteLine("\r\nResponse Status Code is OK and StatusDescription is: {0}", myHttpWebResponse.StatusDescription);
            // Releases the resources of the response.
            myHttpWebResponse.Close();

        }
        catch (WebException e)
        {
            return false;
        }
        catch (Exception e)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// 重新绑定头控件的文本信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void HeadControlDataBind(object sender, EventArgs e)
    {
        if (rbPureText.Checked)
        {
            rbPureText_CheckedChanged(sender, e);
        }
        else if (rbCommercialTenantPackage.Checked)
        {
            rbCommercialTenantPackage_CheckedChanged(sender, e);
        }
        else if (rbSpecialAdvertisement.Checked)
        {
            rbSpecialAdvertisement_CheckedChanged(sender, e);
        }
        else if (rbRedEnvelopeAdvertisement.Checked)
        {
            rbRedEnvelopeAdvertisement_CheckedChanged(sender, e);
        }
    }
}