using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;
using ChineseCharacterToPinyin;
using System.Configuration;
using System.Web.Services;
using System.Net;
using System.IO;
using CloudStorage;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using System.Collections;
using Web.Control.Enum;

public partial class ShopManage_ShopUpdate : System.Web.UI.Page
{
    static string savaLogoPath = string.Empty;
    static string queryLogoPath = string.Empty;
    static string savePublicityPhoto = string.Empty;
    static string queryPublicityPhoto = string.Empty;
    ArrayList alTagId
    {
        set { ViewState["TagId"] = value; }
        get { return (ArrayList)ViewState["TagId"]; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["shopID"] != null)
            {
                int shopID = Common.ToInt32(Request.QueryString["shopID"]);
                GetShopInfo(shopID);
                GetShopTag(shopID);
            }
            if (Request.UrlReferrer != null)
            {
                ViewState["UrlReferrer"] = Request.UrlReferrer.ToString();
            }
        }
    }
    [WebMethod]
    public static string GetShopCoordinate(string shopDetailAddress, string cityName)
    {
        //获取店铺的经纬度
        string shopLocation = "";
        //MapLocation googleMapLocation = Common.GetGoogleMapCoordinate(shopDetailAddress);
        MapLocation baiduMapLocation = Common.GetBaiduMapCoordinate(shopDetailAddress, cityName);
        //if (googleMapLocation != null && baiduMapLocation != null)
        if (baiduMapLocation != null)
        {
            shopLocation = "{\"lat\":" + 0 + ",\"lng\":" + 0 + ",\"latBaidu\":" + baiduMapLocation.lat + ",\"lngBaidu\":" + baiduMapLocation.lng + "}";
        }
        return shopLocation;
    }
    /// <summary>
    /// 获取某门店信息
    /// </summary>
    /// <param name="shopID"></param>
    protected void GetShopInfo(int shopID)
    {
        ShopInfo shopInfo = new ShopInfo();
        ShopOperate shopOperate = new ShopOperate();
        shopInfo = shopOperate.QueryShop(shopID);
        //省
        GetProvince();
        DropDownList_provinceID.Text = shopInfo.provinceID.ToString();
        //市
        GetCity();
        DropDownList_cityID.Text = shopInfo.cityID.ToString();
        //县
        GetCounty();
        DropDownList_countyID.Text = shopInfo.countyID.ToString();
        //公司
        GetCompany();
        DropDownList_Company.Text = shopInfo.companyID.ToString();
        //门店状态
        QueryShopStatus();
        //门店审批状态
        QueryShopHandleStatus();
        //二维码类型 2014-1-16 jinyanni
        GetQRCodeShopType();
        BindQRCodeByShopID(shopID);

        lbShopFaceSpace.Text = WebConfig.ShopFaceSpace.ToString() + "KB";
        lbShopLogoSpace.Text = WebConfig.ShopLogoSpace.ToString() + "KB";

        DropDownList_shopStatus.Text = shopInfo.shopStatus.ToString();
        TextBox_contactPerson.Text = shopInfo.contactPerson;
        TextBox_contactPhone.Text = shopInfo.contactPhone;
        TextBox_shopAddress.Text = shopInfo.shopAddress;
        TextBox_shopBusinessLicense.Text = shopInfo.shopBusinessLicense;
        TextBox_shopHygieneLicense.Text = shopInfo.shopHygieneLicense;
        TextBox_shopName.Text = shopInfo.shopName;
        TextBox_shopTelePhone.Text = shopInfo.shopTelephone;
        Label_imageName.Text = shopInfo.shopLogo;
        DropDownList_IsHandle.Text = shopInfo.isHandle.ToString();
        TextBox_shopDescription.Text = shopInfo.shopDescription;
        TextBox_sinaWeibo.Text = shopInfo.sinaWeiboName;
        TextBox_qqWeibo.Text = shopInfo.qqWeiboName;
        TextBox_wechatPublicName.Text = shopInfo.wechatPublicName;
        TextBox_openTime.Text = shopInfo.openTimes;
        TextBox_shopRating.Text = shopInfo.shopRating.ToString();//店铺评分 2014-1-3 jinyanni
        EmployeeOperate operate = new EmployeeOperate();
        DataTable dt = operate.QueryEmployeeByEmployeeId(shopInfo.accountManager.HasValue ? shopInfo.accountManager.Value : 0);
        if (dt.Rows.Count == 1)
        {
            hidden_init.Value = Common.ToString(dt.Rows[0]["EmployeeFirstName"]) == "" ? Common.ToString(dt.Rows[0]["UserName"]) : Common.ToString(dt.Rows[0]["EmployeeFirstName"]);
        }
        hidden.Value = Common.ToString(shopInfo.accountManager);
        queryLogoPath = WebConfig.CdnDomain + WebConfig.ImagePath + shopInfo.shopImagePath;
        savaLogoPath = WebConfig.ImagePath + shopInfo.shopImagePath;
        Big_Img.ImageUrl = queryLogoPath + shopInfo.shopLogo;
        queryPublicityPhoto = WebConfig.CdnDomain + WebConfig.ImagePath;
        savePublicityPhoto = WebConfig.ImagePath;
        if (!string.IsNullOrEmpty(shopInfo.publicityPhotoPath))
        {
            publicityPhoto.ImageUrl = queryPublicityPhoto + shopInfo.publicityPhotoPath;
        }
        TextBox_accp.Text = shopInfo.acpp.ToString();//人均消费 2014-1-7 jinyanni
        ShopCoordinate shopCoordinateBaidu = shopOperate.QueryShopCoordinate(2, shopID);//百度经纬度
        TextBox_LatitudeBaidu.Text = shopCoordinateBaidu.latitude.ToString();
        TextBox_LongitudeBaidu.Text = shopCoordinateBaidu.longitude.ToString();
        TextBox_orderDishDes.Text = shopInfo.orderDishDesc;
        if (shopInfo.isSupportPayment == true)
        {
            RadioButton_Support.Checked = true;
        }
        else
        {
            RadioButton_NotSupport.Checked = true;
        }
        if (shopInfo.isSupportRedEnvelopePayment == true)
        {
            rb_SupportRedEnvelopePayment.Checked = true;
        }
        else
        {
            rb_notSupportRedEnvelopePayment.Checked = true;
        }
        tb_notPaymentReason.Text = shopInfo.notPaymentReason;
        BindShopAccount(shopInfo.bankAccount.HasValue ? shopInfo.bankAccount.Value : 0);
        //if (shopInfo.isSupportAccountsRound.HasValue && shopInfo.isSupportAccountsRound.Value == true)
        //{
        //    rb_isSupportAccountsRound.Checked = true;
        //}
        //else
        //{
        //    rb_isSupportAccountsRound.Checked = false;
        //}
        GetLevel1(shopInfo.cityID);
    }
    /// <summary>
    /// 修改店铺信息
    /// </summary>
    protected void ModifyShop()
    {
        if (RadioButton_NotSupport.Checked == true && tb_notPaymentReason.Text.Trim() == "")//选中暂不支持付款，并且没有填写不支持付款原因
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请填写暂不支持付款原因');</script>");
            return;
        }
        ShopOperate shopOperate = new ShopOperate();
        ShopInfo ShopInfo = new ShopInfo();
        int shopID = Common.ToInt32(Request.QueryString["shopID"]);
        ShopInfo shopInfo = new ShopInfo();
        shopInfo.shopID = shopID;
        shopInfo.canEatInShop = true;
        shopInfo.canTakeout = true;
        shopInfo.cityID = Common.ToInt32(DropDownList_cityID.SelectedValue);
        shopInfo.companyID = Common.ToInt32(DropDownList_Company.SelectedValue);
        shopInfo.contactPerson = TextBox_contactPerson.Text;
        shopInfo.contactPhone = TextBox_contactPhone.Text;
        shopInfo.countyID = Common.ToInt32(DropDownList_countyID.SelectedValue);
        shopInfo.provinceID = Common.ToInt32(DropDownList_provinceID.SelectedValue);
        shopInfo.shopAddress = TextBox_shopAddress.Text;
        shopInfo.shopBusinessLicense = TextBox_shopBusinessLicense.Text;
        shopInfo.shopHygieneLicense = TextBox_shopHygieneLicense.Text;
        shopInfo.shopLogo = Label_imageName.Text;
        shopInfo.shopName = TextBox_shopName.Text;
        shopInfo.shopStatus = Common.ToInt32(DropDownList_shopStatus.SelectedValue);
        shopInfo.shopTelephone = TextBox_shopTelePhone.Text;
        shopInfo.shopDescription = TextBox_shopDescription.Text;
        shopInfo.sinaWeiboName = TextBox_sinaWeibo.Text;
        shopInfo.qqWeiboName = TextBox_qqWeibo.Text;
        shopInfo.openTimes = TextBox_openTime.Text;
        shopInfo.shopRating = Common.ToDouble(TextBox_shopRating.Text);//店铺评分 2014-1-3 jinyanni
        shopInfo.wechatPublicName = TextBox_wechatPublicName.Text;
        shopInfo.accountManager = Common.ToInt32(hidden.Value);
        shopInfo.isSupportAccountsRound = false; //Common.ToBool(rb_isSupportAccountsRound.Checked);
        shopInfo.isSupportRedEnvelopePayment = Common.ToBool(rb_SupportRedEnvelopePayment.Checked);
        //店铺形象展示照片
        if (!string.IsNullOrEmpty(publicityPhoto.ImageUrl))
        {
            shopInfo.publicityPhotoPath = publicityPhoto.ImageUrl.Replace(WebConfig.CdnDomain + WebConfig.ImagePath, "").Replace("@320w_106h_50Q", "");
        }
        else
        {
            shopInfo.publicityPhotoPath = "";
        }
        shopInfo.bankAccount = Common.ToInt32(ddl_account.SelectedValue);//门店银行帐号
        shopInfo.acpp = Common.ToDouble(TextBox_accp.Text);//人均消费
        shopInfo.isSupportPayment = Common.ToBool(RadioButton_Support.Checked);
        shopInfo.orderDishDesc = TextBox_orderDishDes.Text.Trim();
        shopInfo.notPaymentReason = tb_notPaymentReason.Text.Trim();
        bool result = shopOperate.ModifyShop(shopInfo);
        if (result)
        {
            List<ShopCoordinate> shopCoordinateList = new List<ShopCoordinate>();
            ShopCoordinate shopCoordinate = new ShopCoordinate();
            shopCoordinate.shopId = shopID;
            shopCoordinate.latitude = Common.ToDouble11Digit(TextBox_LatitudeBaidu.Text);
            shopCoordinate.longitude = Common.ToDouble11Digit(TextBox_LongitudeBaidu.Text);
            shopCoordinate.mapId = 2;//百度地图编号，暂时固定
            shopCoordinateList.Add(shopCoordinate);
            bool coordinate = shopOperate.UpdateShopCoordinate(shopCoordinateList);

            string tagIds = "";
            object[] objShopTag = new object[] { false, "" };
            foreach (ListItem li in ckbExistShopTag.Items)
            {
                if (!li.Selected)
                {
                    tagIds = tagIds + li.Value + ",";
                }
            }
            if (tagIds.Length > 0 && tagIds.Substring(tagIds.Length - 1, 1) == ",")
            {
                tagIds = tagIds.Remove(tagIds.Length - 1, 1);

                IShopTagService shopTagService = ServiceFactory.Resolve<IShopTagService>();
                objShopTag = shopTagService.MaintainShopTag(tagIds, shopID, true);
            }
            else
            {
                objShopTag[0] = true;
            }
            if (coordinate && Common.ToBool(objShopTag[0]))
            {
                GetShopTag(shopID);
                Common.RecordEmployeeOperateLog((int)VAEmployeeOperateLogOperatePageType.SHOPINFO, (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE,
                   "城市编号：" + DropDownList_cityID.SelectedItem.Text + "，公司编号：" + DropDownList_Company.SelectedItem.Text + "，门店联系人："
                   + shopInfo.contactPerson + "，门店联系人电话：" + shopInfo.contactPhone + "，门店所属区编号：" + shopInfo.countyID
                   + "，店铺结算是否支持四舍五入：" + shopInfo.isSupportAccountsRound + "，营业时间：" + shopInfo.openTimes + "，门店所属省份编号：" + DropDownList_provinceID.SelectedItem.Text
                   + "，店铺腾讯微博：" + shopInfo.qqWeiboName + "，门店详细地址：" + shopInfo.shopAddress + "，门店营业执照：" + shopInfo.shopBusinessLicense
                   + "，店铺描述：" + shopInfo.shopDescription + "，门店卫生许可证：" + shopInfo.shopHygieneLicense + "，门店编号：" + shopInfo.shopID + "，门店名称："
                   + shopInfo.shopName + "，门店注册时间：" + shopInfo.shopRegisterTime + "，门店状态：" + DropDownList_shopStatus.SelectedItem.Text + "，门店电话：" + shopInfo.shopTelephone
                   + "，门店验证时间：" + shopInfo.shopVerifyTime + "，门店新浪微博：" + shopInfo.sinaWeiboName + "，门店微信公共帐号：" + shopInfo.wechatPublicName
                   + "，谷歌纬度：" + 0 + "，谷歌经度：" + 0 + "，店铺评分：" + TextBox_shopRating.Text + "，店铺形象展示照片：" + shopInfo.publicityPhotoPath + "，人均消费：" + TextBox_accp.Text
                   + ",门店银行帐号：" + ddl_account.SelectedItem.Text + ",客户经理：" + hidden.Value + "，支持红包支付：" + shopInfo.isSupportRedEnvelopePayment);
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改成功！');</script>");
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败！');</script>");
        }
    }
    /// <summary>
    /// 修改门店
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_ShopUpdate_Click(object sender, EventArgs e)
    {
        ModifyShop();
    }
    /// <summary>
    /// 上传图片
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ButtonBig_Click(object sender, EventArgs e)
    {
        UploadImage(Big_File, Big_Img);
        ModifyShop();
    }
    protected void UploadImage(FileUpload fileUpload, Image image)
    {
        string imageName = string.Empty;
        //上传图片
        string fileName = Server.HtmlEncode(fileUpload.FileName);
        string extension = System.IO.Path.GetExtension(fileName);
        int shopID = Common.ToInt32(Request.QueryString["shopID"]);
        ShopOperate shopOper = new ShopOperate();
        ShopInfo shopInfo = shopOper.QueryShop(shopID);
        if (fileName != "")
        {
            if (extension == ".png" || extension == ".jpg")
            {
                System.Drawing.Bitmap originalBMP = new System.Drawing.Bitmap(fileUpload.FileContent);//该上传图片文件
                if (originalBMP.Width == originalBMP.Height)
                {
                    if (originalBMP.Width >= 300)
                    {
                        if (fileUpload.PostedFile.ContentLength / 1024 < WebConfig.ShopLogoSpace)//获得的是文件的大小，单位kb
                        {
                            CloudStorageOperate.DeleteObject(savaLogoPath + shopInfo.shopLogo);//删除原有的图片

                            imageName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                            CloudStorageResult result = CloudStorageOperate.PutObject(savaLogoPath + imageName, fileUpload, imageName);
                            image.ImageUrl = queryLogoPath + imageName + "@136w_136h_50Q";
                            Label_imageName.Text = imageName;
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片必须小于" + WebConfig.ShopLogoSpace + "KB');</script>");
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片最小尺寸应为300*300');</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('上传失败，图片比例应为1*1！');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片格式必须是png或者jpg');</script>");
            }
        }
    }
    /// <summary>
    /// 获取所有门店
    /// </summary>
    protected void GetCompany()
    {
        if (Session["UserInfo"] != null)
        {
            EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
            List<VAEmployeeCompany> employeeCompany = new List<VAEmployeeCompany>();
            VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
            int employeeID = vAEmployeeLoginResponse.employeeID;
            employeeCompany = employeeConnShopOperate.QueryEmployeeCompany(employeeID);
            DropDownList_Company.DataSource = employeeCompany;
            DropDownList_Company.DataTextField = "CompanyName";
            DropDownList_Company.DataValueField = "CompanyID";
            DropDownList_Company.DataBind();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>window.open('../Login.aspx',target='_top')</script>");
        }
    }
    /// <summary>
    /// 获取省份信息
    /// </summary>
    protected void GetProvince()
    {
        ProvinceOperate provinceOperate = new ProvinceOperate();
        DataTable dtProvince = provinceOperate.QueryProvince();
        DropDownList_provinceID.DataSource = dtProvince;
        DropDownList_provinceID.DataTextField = "ProvinceName";
        DropDownList_provinceID.DataValueField = "ProvinceID";
        DropDownList_provinceID.DataBind();
    }
    /// <summary>
    /// 获取市信息
    /// </summary>
    protected void GetCity()
    {
        int provinceID = Common.ToInt32(DropDownList_provinceID.SelectedValue);
        CityOperate cityOperate = new CityOperate();
        DataTable dtCity = cityOperate.QueryCity(provinceID);
        DropDownList_cityID.DataSource = dtCity;
        DropDownList_cityID.DataTextField = "CityName";
        DropDownList_cityID.DataValueField = "CityID";
        DropDownList_cityID.DataBind();
    }
    /// <summary>
    /// 获取县信息
    /// </summary>
    protected void GetCounty()
    {
        int cityID = Common.ToInt32(DropDownList_cityID.SelectedValue);
        CountyOperate countyOperate = new CountyOperate();
        DataTable dtCity = countyOperate.QueryCounty(cityID);
        DropDownList_countyID.DataSource = dtCity;
        DropDownList_countyID.DataTextField = "CountyName";
        DropDownList_countyID.DataValueField = "CountyID";
        DropDownList_countyID.DataBind();
        GetLevel1(cityID);

    }
    /// <summary>
    /// 获取店铺状态
    /// </summary>
    protected void QueryShopStatus()
    {
        DropDownList_shopStatus.DataSource = EnumHelper.EnumToList(typeof(VAShopStatus));
        DropDownList_shopStatus.DataTextField = "Text";
        DropDownList_shopStatus.DataValueField = "Value";
        DropDownList_shopStatus.DataBind();
    }
    /// <summary>
    /// 获取店铺审批状态
    /// </summary>
    protected void QueryShopHandleStatus()
    {
        DropDownList_IsHandle.DataSource = EnumHelper.EnumToList(typeof(VAShopHandleStatus));
        DropDownList_IsHandle.DataTextField = "Text";
        DropDownList_IsHandle.DataValueField = "Value";
        DropDownList_IsHandle.DataBind();
    }
    protected void DropDownList_provinceID_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCity();
        GetCounty();
    }
    protected void DropDownList_cityID_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCounty();
    }
    protected void ButtonPublicityPhoto_Click(object sender, EventArgs e)
    {
        object[] result = UploadPublicityPhoto(FileUpload_PublicityPhoto, publicityPhoto);
        if (result[1].ToString() != "")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + result[1].ToString() + "');</script>");
            return;
        }
        ModifyShop();
    }
    /// <summary>
    /// 上传门店的形象宣传照
    /// </summary>
    /// <param name="fileUpload"></param>
    /// <param name="image"></param>
    protected object[] UploadPublicityPhoto(FileUpload fileUpload, Image image)
    {
        object[] objResult = new object[] { false, "" };
        try
        {
            int shopID = Common.ToInt32(Request.QueryString["shopID"]);
            ShopOperate shopOper = new ShopOperate();
            ShopInfo shopInfo = shopOper.QueryShop(shopID);
            string photoName = string.Empty;//照片名称
            string fileName = Server.HtmlEncode(fileUpload.FileName);//上传的文件
            string extension = System.IO.Path.GetExtension(fileName);//获取扩展名
            if (!string.IsNullOrEmpty(fileName))
            {
                if (extension == ".png" || extension == ".jpg")
                {
                    System.Drawing.Bitmap originalBMP = new System.Drawing.Bitmap(fileUpload.FileContent);
                    if (Math.Floor(Common.ToDouble(originalBMP.Width * 53 / 160)) == originalBMP.Height)
                    {
                        if (originalBMP.Width >= 1440)
                        {
                            if (fileUpload.PostedFile.ContentLength / 1024 < WebConfig.ShopFaceSpace)//获得的是文件的大小，单位kb
                            {
                                CloudStorageOperate.DeleteObject(savePublicityPhoto + shopInfo.publicityPhotoPath.Replace("@320w_106h_50Q", ""));//删除原有的图片
                                photoName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                                CloudStorageResult result = CloudStorageOperate.PutObject(savePublicityPhoto + shopInfo.shopImagePath + "shopPublicityPhoto/" + photoName, fileUpload, photoName);
                                if (result.code)
                                {
                                    objResult[0] = true;
                                    image.ImageUrl = queryPublicityPhoto + shopInfo.shopImagePath + "shopPublicityPhoto/" + photoName + "@320w_106h_50Q";
                                    Label_PublicityPhotoName.Text = photoName;
                                }
                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片必须小于" + WebConfig.ShopFaceSpace + "KB');</script>");
                            }
                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片最小尺寸应为1440*477');</script>");
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('上传失败，图片比例应为160*53！');</script>");
                    }
                }
                else
                {
                    objResult[1] = "图片格式必须是png或者是jpg";
                }
            }
            else
            {
                objResult[1] = "请先选择图片";
            }
        }
        catch (Exception ex)
        {
            objResult[1] = ex.Message;
        }
        return objResult;
    }
    #region 二维码
    /// <summary>
    /// 绑定店铺可用的二维码类型
    /// </summary>
    protected void GetQRCodeShopType()
    {
        QRCodeOperate _QRCode = new QRCodeOperate();
        DataTable dtQRCode = _QRCode.QueryQRCodeShopType();
        DropDownList_QRCodeType.DataSource = dtQRCode;
        DropDownList_QRCodeType.DataTextField = "name";
        DropDownList_QRCodeType.DataValueField = "id";
        DropDownList_QRCodeType.DataBind();
    }

    /// <summary>
    /// 根据店铺Id绑定相应的二维码图片
    /// </summary>
    /// <param name="shopId"></param>
    protected void BindQRCodeByShopID(int shopId)
    {
        QRCodeOperate _QRCode = new QRCodeOperate();
        List<QRCodeConnShop> QRCodeConnShops = new List<QRCodeConnShop>();
        QRCodeConnShops = _QRCode.QueryQRByShopId(shopId);

        foreach (QRCodeConnShop qr in QRCodeConnShops)
        {
            switch (qr.typeId)
            {
                case 1://易拉宝
                    //this.imgYLB.ImageUrl = qr.QRCodeImage;
                    this.imgYLB.ImageUrl = WebConfig.CdnDomain + qr.QRCodeImage.Replace("../", "");
                    this.imgYLB.AlternateText = qr.imageName;
                    break;
                case 2://卡台
                    //this.ImgKT.ImageUrl = qr.QRCodeImage;
                    this.ImgKT.ImageUrl = WebConfig.CdnDomain + qr.QRCodeImage.Replace("../", "");
                    this.ImgKT.AlternateText = qr.imageName;
                    break;
                default:
                    break;
            }
        }
    }

    /// <summary>
    /// 生成二维码图片
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        QRCodeOperate _QRCode = new QRCodeOperate();
        ShopInfo shopInfo = new ShopInfo();
        ShopOperate shopOperate = new ShopOperate();
        QRCodeConnShop qrCodeConnShop = new QRCodeConnShop();

        try
        {
            string typeId = DropDownList_QRCodeType.SelectedItem.Value;//二维码类型ID
            string typeName = DropDownList_QRCodeType.SelectedItem.Text;//二维码类型名称
            int shopId = Common.ToInt32(Request.QueryString["shopID"]);

            shopInfo = shopOperate.QueryShop(shopId);

            string sourcePath = @"../" + WebConfig.ImagePath + "icon.png";//悠先标志路径
            string imageName = typeId + "-" + shopId + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";//二维码名称

            Common.FolderCreate(Server.MapPath(WebConfig.Temp));
            string imagePath = WebConfig.Temp + imageName;//生成的二维码暂时存放在UploadFiles/temp/中，等上传到阿里云后删除
            string objectKey = WebConfig.ImagePath + shopInfo.shopImagePath + WebConfig.QRCodeImage + imageName;//路径+名称
            object[] createResult = _QRCode.CreateQRCode(typeId, shopId, Server.MapPath(imagePath), Server.MapPath(sourcePath));//生成二维码
            if (Common.ToBool(createResult[0]))
            {
                //生成成功后上传到阿里云                
                CloudStorageResult UploadResult = CloudStorageOperate.PutObject(objectKey, Server.MapPath(imagePath));
                if (UploadResult.code)
                {
                    File.Delete(Server.MapPath(imagePath));//删除temp文件夹中暂存的二维码
                    if (typeId == "1")//易拉宝
                    {
                        imgYLB.ImageUrl = WebConfig.CdnDomain + objectKey;
                        if (!string.IsNullOrEmpty(imgYLB.AlternateText))
                        {
                            CloudStorageOperate.DeleteObject(WebConfig.ImagePath + shopInfo.shopImagePath + WebConfig.QRCodeImage + imgYLB.AlternateText);
                        }
                        imgYLB.AlternateText = imageName;
                    }
                    else//卡台
                    {
                        ImgKT.ImageUrl = WebConfig.CdnDomain + objectKey;
                        if (!string.IsNullOrEmpty(ImgKT.AlternateText))
                        {
                            CloudStorageOperate.DeleteObject(WebConfig.ImagePath + shopInfo.shopImagePath + WebConfig.QRCodeImage + ImgKT.AlternateText);
                        }
                        ImgKT.AlternateText = imageName;
                    }

                    qrCodeConnShop.shopId = shopId;
                    qrCodeConnShop.typeId = Common.ToInt32(typeId);
                    qrCodeConnShop.QRCodeImage = objectKey;
                    qrCodeConnShop.status = 1;
                    qrCodeConnShop.imageName = imageName;

                    bool result = _QRCode.SaveQRCodeConnShop(qrCodeConnShop);//保存该店铺的二维码信息

                    if (result)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "exc", "<script>alert('二维码生成成功！')</script>");
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "exc", "<script>alert('二维码生成失败！')</script>");
                    }
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "exc", "<script>alert('二维码生成失败：+" + createResult[1].ToString() + "！')</script>");
            }
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "exc", "<script>alert('" + ex.Message + "')</script>");
        }
    }

    /// <summary>
    /// 下载店铺易拉宝二维码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDownloadYLB_Click(object sender, EventArgs e)
    {
        //string path = Server.MapPath(this.imgYLB.ImageUrl);
        string path = this.imgYLB.ImageUrl.Replace(WebConfig.CdnDomain, "");
        string name = this.imgYLB.AlternateText;
        DownloadQRCode(path, name);
    }

    /// <summary>
    /// 下载店铺卡台二维码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDownloadKT_Click(object sender, EventArgs e)
    {
        //string path = Server.MapPath(this.ImgKT.ImageUrl);
        string path = this.ImgKT.ImageUrl.Replace(WebConfig.CdnDomain, "");
        string name = this.ImgKT.AlternateText;
        DownloadQRCode(path, name);
    }

    /// <summary>
    /// 下载二维码
    /// </summary>
    /// <param name="path">文件存放路径</param>
    /// <param name="name">文件名</param>
    public void DownloadQRCode(string path, string name)
    {
        try
        {
            CloudStorageObject cloudStorageObject = CloudStorageOperate.GetObject(path);
            if (cloudStorageObject.Key != null)
            {
                WebClient wc = new WebClient();
                byte[] imgData = wc.DownloadData(WebConfig.CdnDomain + cloudStorageObject.Key);
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;FileName=" + HttpUtility.UrlEncode(name));
                Response.BinaryWrite(imgData);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "exc", "<script>alert('二维码不存在！')</script>");
            }
        }
        catch (Exception ex)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "exc", "<script>alert('" + ex.Message + "')</script>");
        }
    }
    #endregion
    [WebMethod]
    public static string GetDate(string str)
    {
        EmployeeOperate operate = new EmployeeOperate();
        List<PartEmployee> list = operate.GetPartEmployeeInfo(str);
        return JsonOperate.JsonSerializer<List<PartEmployee>>(list);
    }
    /// <summary>
    /// 绑定门店银行帐号信息
    /// </summary>
    void BindShopAccount(int bankAccount)
    {
        int companyId = Common.ToInt32(DropDownList_Company.SelectedValue);
        CompanyAccountOprate operate = new CompanyAccountOprate();
        DataTable dtAccount = operate.QueryAccountNameAndAccountNumByCompanyId(companyId);
        if (dtAccount.Rows.Count > 0)
        {
            ddl_account.DataSource = dtAccount;
            ddl_account.DataTextField = "accountStr";
            ddl_account.DataValueField = "identity_Id";
        }
        ddl_account.DataBind();
        ddl_account.Items.Add(new ListItem("请选择银行帐号", "0"));
        if (bankAccount > 0)
        {
            ddl_account.SelectedValue = bankAccount.ToString();
        }
        else
        {
            ddl_account.SelectedValue = "0";
        }
    }
    #region 商圈
    /// <summary>
    /// 根据城市Id获取一级商圈
    /// </summary>
    private void GetLevel1(int cityId)
    {
        IShopTagService shopTagService = ServiceFactory.Resolve<IShopTagService>();
        List<ShopTag> shopLevel1 = shopTagService.GetFirstGradeShopTagByCityId(cityId);
        if (shopLevel1 != null && shopLevel1.Count > 0)
        {
            ddlLevel1.DataSource = shopLevel1;
            ddlLevel1.DataTextField = "Name";
            ddlLevel1.DataValueField = "TagId";
            ddlLevel1.DataBind();

            GetLevel2(Common.ToInt32(ddlLevel1.SelectedValue));
        }
        else
        {
            ddlLevel1.Items.Clear();
            ddlLevel2.Items.Clear();
        }
    }
    /// <summary>
    /// 根据一级商圈获取其二级商圈
    /// </summary>
    private void GetLevel2(int tagId)
    {
        IShopTagService shopTagService = ServiceFactory.Resolve<IShopTagService>();
        List<ShopTag> shopLevel2 = shopTagService.GetSecondGradeShopTagByFirstGrade(tagId);
        if (shopLevel2 != null && shopLevel2.Count > 0)
        {
            ddlLevel2.DataSource = shopLevel2;
            ddlLevel2.DataTextField = "Name";
            ddlLevel2.DataValueField = "TagId";
            ddlLevel2.DataBind();
        }
        else
        {
            ddlLevel2.Items.Clear();
        }
    }
    /// <summary>
    /// 根据ShopId获取其所有的商圈标记
    /// </summary>
    /// <param name="shopId"></param>
    private void GetShopTag(int shopId)
    {
        ckbExistShopTag.Items.Clear();
        IShopTagService shopTagService = ServiceFactory.Resolve<IShopTagService>();
        List<ShopTag> shopTags = shopTagService.GetShopTagByShopId(shopId);
        if (shopTags != null && shopTags.Count > 0)
        {
            ckbExistShopTag.DataSource = shopTags;
            ckbExistShopTag.DataTextField = "Name";
            ckbExistShopTag.DataValueField = "TagId";
            ckbExistShopTag.DataBind();

        }
        else
        {
            ckbExistShopTag.Items.Clear();
        }
    }

    protected void ddlLevel1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLevel2(Common.ToInt32(ddlLevel1.SelectedValue));
    }
    protected void txbAddShopTag_Click(object sender, EventArgs e)
    {
        string tagId = ddlLevel2.SelectedValue;
        int shopId = Common.ToInt32(Request.QueryString["shopID"]);
        IShopTagService shopTagService = ServiceFactory.Resolve<IShopTagService>();
        object[] shopWithTag = shopTagService.MaintainShopTag(tagId, shopId, false);
        if (Common.ToBool(shopWithTag[0]))
        {
            GetShopTag(shopId);
        }
        else
        {
            CommonPageOperate.AlterMsg(this, shopWithTag[1].ToString());
        }
    }
    #endregion

}