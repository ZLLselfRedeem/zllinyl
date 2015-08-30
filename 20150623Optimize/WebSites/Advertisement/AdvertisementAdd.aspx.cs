using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using System.Configuration;
using VAGastronomistMobileApp.Model;
using System.Collections;
using System.Drawing;
using CloudStorage;
using VAEncryptDecrypt;

public partial class Advertisement_AdvertisementAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbBannerSpace.Text = WebConfig.BannerSpace.ToString() + "KB";
            DropDownList_ADType_SelectedIndexChanged(null, null);
            this.BindShopCheckBoxList();
            if (Request["ID"] != null)
            {
                int adID = Common.ToInt32(Request["ID"]);
                if (adID == 0)
                {
                    return;
                }
                this.ViewState["AdvertisementID"] = adID;
                AdvertisementOperate oprate = new AdvertisementOperate();
                AdvertisementInfo advertisementInfo = oprate.GetAdvertisementByID(adID);
                if (advertisementInfo == null)
                {
                    return;
                }
                if (advertisementInfo.advertisementClassify.HasValue)
                {
                    this.rblAdClassify.SelectedValue = advertisementInfo.advertisementClassify.Value.ToString();
                }
                if (advertisementInfo.advertisementClassify.HasValue)
                {
                    this.DropDownList_ADType.SelectedValue = advertisementInfo.advertisementType.Value.ToString();
                }
                this.TextBox_Description.Text = advertisementInfo.advertisementDescription;
                this.TextBox_ADName.Text = advertisementInfo.name;
                this.TextBox_ADName.Enabled = false;
                string virtualPath = string.Empty;
                //if (string.IsNullOrEmpty(DropDownList_Companys.SelectedValue))
                //{
                //    virtualPath = WebConfig.ImagePath + WebConfig.Advertisement +    "common/";
                //}
                //else
                //{
                //   virtualPath= WebConfig.ImagePath + WebConfig.Advertisement + DropDownList_Companys.SelectedValue + "/";
                //}
                this.Big_Img.ImageUrl = WebConfig.CdnDomain + WebConfig.ImagePath + advertisementInfo.imageURL;
                this.HiddenField_Image.Value = advertisementInfo.imageURL;
                this.Button_Add.Text = "更新广告";
                this.ButtonCancel.Visible = true;
                this.Title = "更新广告-" + advertisementInfo.name;
                HeadControl1.headName = "更新广告-" + advertisementInfo.name;
                this.HeadControl1.navigationUrl = "AdvertisementManagement.aspx";
                switch (advertisementInfo.advertisementType)
                {
                    case 1:
                        this.trComapny.Visible = true;
                        this.trStore.Visible = true;
                        this.trURL.Visible = false;
                        this.trActivity.Visible = false;
                        TextBox_url.Enabled = false;
                        rblActivity.Items.Clear();
                        SetCompanyInfo();
                        this.DropDownList_Companys.SelectedValue = advertisementInfo.value;
                        BindShopCheckBoxList();//显示对应的门店列表
                        int shopID = oprate.GetSopIDByAdvertisement((int)advertisementInfo.id);
                        if (shopID > 0)
                        {
                            this.CheckBoxList_ShopId.SelectedValue = shopID.ToString();
                        }
                        break;
                    case 4:
                        this.trComapny.Visible = false;
                        this.trStore.Visible = false;
                        this.trURL.Visible = false;
                        this.trActivity.Visible = true;
                        TextBox_url.Text = "http://";
                        TextBox_url.Enabled = true;
                        BindActivity();
                        this.rblActivity.SelectedValue = advertisementInfo.value;
                        break;
                    case 3:
                    case 5:
                    case 6:
                        this.trComapny.Visible = false;
                        this.trStore.Visible = false;
                        this.trURL.Visible = true;
                        this.trActivity.Visible = false;
                        TextBox_url.Text = advertisementInfo.webAdvertisementUrl;
                        TextBox_url.Enabled = true;
                        rblActivity.Items.Clear();
                        break;
                    default:
                        break;
                }
            }
        }
    }
    /// <summary>
    /// 绑定公司列表
    /// </summary>
    protected void SetCompanyInfo()
    {
        if (Session["UserInfo"] != null)
        {
            VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
            int employeeID = vAEmployeeLoginResponse.employeeID;
            EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
            List<VAEmployeeCompany> employeeCompany = new List<VAEmployeeCompany>();
            //VAEmployeeCompany comp = new VAEmployeeCompany();
            //comp.companyID = 0;
            //comp.companyName = "--请选择--";
            //employeeCompany.Add(comp);
            employeeCompany.AddRange(employeeConnShopOperate.QueryEmployeeCompany(employeeID));
            DropDownList_Companys.DataSource = employeeCompany;
            DropDownList_Companys.DataValueField = "companyID";
            DropDownList_Companys.DataTextField = "companyName";
            DropDownList_Companys.DataBind();
            DropDownList_Companys.SelectedIndex = 0;
        }
    }
    protected void DropDownList_Conpamys_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindShopCheckBoxList();//显示对应的门店列表
        DropDownList_Coupon.Items.Clear();
    }
    protected void DropDownList_ADType_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (DropDownList_ADType.SelectedValue)
        {
            case "1":
                this.trComapny.Visible = true;
                this.trStore.Visible = true;
                this.trURL.Visible = false;
                this.trActivity.Visible = false;
                TextBox_url.Enabled = false;
                TextBox_url.Text = "";
                rblActivity.Items.Clear();
                SetCompanyInfo();
                break;
            case "3":
            case "5":
                this.trComapny.Visible = false;
                this.trStore.Visible = false;
                this.trURL.Visible = true;
                this.trActivity.Visible = false;
                TextBox_url.Text = "http://";
                TextBox_url.Enabled = true;
                rblActivity.Items.Clear();
                break;
            case "4":
                this.trComapny.Visible = false;
                this.trStore.Visible = false;
                this.trURL.Visible = false;
                this.trActivity.Visible = true;
                TextBox_url.Text = "http://";
                TextBox_url.Enabled = true;
                BindActivity();
                break;
            case "6":
                this.trComapny.Visible = false;
                this.trStore.Visible = false;
                this.trURL.Visible = true;
                this.trActivity.Visible = false;
                TextBox_url.Text = "http://";
                TextBox_url.Enabled = true;
                rblActivity.Items.Clear();
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 上传图片
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Upload_Click(object sender, EventArgs e)
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
                    //门店广告为1
                    if (DropDownList_ADType.SelectedValue == "1")
                    {
                        virtualPath = WebConfig.Advertisement + DropDownList_Companys.SelectedValue + "/";
                        imageName = DropDownList_Companys.SelectedValue + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                    }
                    else
                    {
                        virtualPath = WebConfig.Advertisement + "common/";
                        imageName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + extension;
                    }
                    #region 判断上传图片的信息
                    Bitmap originalBMP = new Bitmap(fileUpload.FileContent);//该上传图片文件
                    int width = originalBMP.Width;
                    int height = originalBMP.Height;
                    if (Math.Floor(Common.ToDouble(width * 11 / 32)) == height)
                    {
                        if (width >= 1350)
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
                            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片最小尺寸应为1350*495！');</script>");
                        }
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('图片比例应为32*11！');</script>");
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
    /// <summary>e
    /// 保存表单
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Add_Click(object sender, EventArgs e)
    {
        bool boo = InsertNewAD();
        if (boo)
        {
            Page.ClientScript.RegisterStartupScript(GetType(),
                "message", "<script language='javascript' defer>alert('添加成功！');window.location.href = 'AdvertisementManagement.aspx';</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('添加失败！');</script>");
        }
    }
    /// <summary>
    /// 新增广告函数
    /// </summary>
    public bool InsertNewAD()
    {

        AdvertisementOperate oprate = new AdvertisementOperate();
        AdvertisementInfo adver = null;
        if (this.ViewState["AdvertisementID"] != null)
        {
            adver = oprate.GetAdvertisementByID(Common.ToInt32(this.ViewState["AdvertisementID"]));
        }
        if (adver == null)
        {
            adver = new AdvertisementInfo();
            DataTable dtTmp = oprate.QueryAdvertisement(new AdvertisementInfoQueryObject() { nameEqual = this.TextBox_ADName.Text, status = 1 });
            if (dtTmp != null && dtTmp.Rows.Count > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('广告名称已经存在,请重新输入！');</script>");
                this.TextBox_ADName.Focus();
                return false;
            }
        }
        bool flag = false;

        if (string.IsNullOrEmpty(this.TextBox_ADName.Text))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('广告名称不能为空！');</script>");
            return false;
        }
        if (HiddenField_Image.Value == "" || rblAdClassify.SelectedValue == "")
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请设定广告分类、广告类型、所属公司及上传图片！');</script>");
        }

        else
        {
            //if (Common.ToInt32(rblAdClassify.SelectedValue) == (int)VAAdvertisementClassify.INDEX_AD && DropDownList_ADType.SelectedValue == "0"
            //    || Common.ToInt32(rblAdClassify.SelectedValue) == (int)VAAdvertisementClassify.FOODPLAZA_AD )
            //{
            //    CommonPageOperate.AlterMsg(this, "请选择广告类型");
            //    return false;
            //}

            //if (Common.ToInt32(rblAdClassify.SelectedValue) == (int)VAAdvertisementClassify.INDEX_AD
            //    && Common.ToInt32(DropDownList_ADType.SelectedValue) == (int)VAAdvertisementArea.REDENVELOPE_BANNER
            //    && rblActivity.SelectedValue == "")
            //{
            //    CommonPageOperate.AlterMsg(this, "红包广告必须选择活动");
            //    return false;
            //}
            adver.name = TextBox_ADName.Text;
            adver.imageURL = HiddenField_Image.Value;
            adver.status = 1;
            adver.advertisementDescription = TextBox_Description.Text;//
            adver.webAdvertisementUrl = TextBox_url.Text.Trim().ToString();
            //2015-1-8 临时解决方案
            //如果是套餐广告，将广告类别设置为红包类别，这样客户端会帮忙替换 mobile 及 cookie 参数
            if (DropDownList_ADType.SelectedValue == "6")
            {
                adver.advertisementType = 4;
            }
            else
            {
                adver.advertisementType = Common.ToInt32(DropDownList_ADType.SelectedValue);
            }
            switch (Common.ToInt32(DropDownList_ADType.SelectedValue))
            {
                case 1://店铺
                    adver.value = DropDownList_Companys.SelectedValue;
                    break;
                case 3://宣传
                case 5://专题
                case 6://套餐
                    adver.value = DropDownList_Companys.SelectedValue;
                    break;
                case 2://优惠券
                    adver.value = DropDownList_Coupon.SelectedValue;
                    break;
                case 4://红包
                    adver.value = rblActivity.SelectedValue;
                    ActivityOperate activityOperate = new ActivityOperate();
                    Activity activity = activityOperate.QueryActivity(Common.ToInt32(rblActivity.SelectedValue));
                    string activityId = HttpUtility.UrlEncode(EncryptDecrypt.Encrypt(rblActivity.SelectedValue.ToString(), "va"));

                    if (activity.activityType == ActivityType.节日免单红包)
                    {
                        adver.webAdvertisementUrl = WebConfig.ServerDomain +
                            "AppPages/christmasEnvelope/?activityId=" + activityId + "&cookie={1}&mobilephone={2}&type=app";// BigRedEnvelope/Index.html?activityId=
                    }
                    else
                    {
                        adver.webAdvertisementUrl = WebConfig.ServerDomain +
                            "AppPages/RedEnvelope/TreasureChest.aspx?activityId=" + activityId + "&cookie={1}&mobilephone={2}&type=app";
                    }
                    break;
                default:
                    break;
            }
            adver.advertisementClassify = Common.ToInt32(this.rblAdClassify.SelectedValue);

            if (adver.id <= 0)
            {
                flag = oprate.AddNewAD(adver, GetShopId());//2013/8/15 wangcheng添加参数
            }
            else
            {
                flag = oprate.UpdateAD(adver, GetShopId());//2013/8/15 wangcheng添加参数
            }
        }
        return flag;
    }
    /// <summary>
    /// 动态显示绑定店铺信息
    /// </summary>
    protected void BindShopCheckBoxList()
    {
        if (Session["UserInfo"] != null)
        {
            EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
            List<VAEmployeeShop> employeeShop = new List<VAEmployeeShop>();
            VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
            int employeeID = vAEmployeeLoginResponse.employeeID;
            int companyID = Common.ToInt32(DropDownList_Companys.SelectedValue);
            employeeShop = employeeConnShopOperate.QueryEmployeeShopByCompanyAndEmplyee(employeeID, companyID);
            CheckBoxList_ShopId.DataSource = employeeShop;
            CheckBoxList_ShopId.DataTextField = "shopName";
            CheckBoxList_ShopId.DataValueField = "shopID";
            CheckBoxList_ShopId.DataBind();
            if (employeeShop != null && employeeShop.Count > 0)
            {
                CheckBoxList_ShopId.SelectedIndex = 0;
            }
            else
            {
                CheckBoxList_ShopId.Items.Clear();
            }
        }
    }
    /// <summary>
    /// 获取选择的显示门店Id
    /// </summary>
    protected ArrayList GetShopId()
    {
        ArrayList al = new ArrayList();
        for (int i = 0; i < CheckBoxList_ShopId.Items.Count; i++)
        {
            if (CheckBoxList_ShopId.Items[i].Selected == true)
            {
                al.Add(CheckBoxList_ShopId.Items[i].Value);
            }
        }
        return al;
    }
    //广告分类切换
    protected void rblAdClassify_SelectedIndexChanged(object sender, EventArgs e)
    {
        //switch (rblAdClassify.SelectedValue)
        //{
        //    case "1":
        //        DropDownList_ADType.Attributes.Add("style", "display:''");
        //        DropDownList_ADTypeFoodPlaza.Attributes.Add("style", "display:none");
        //        break;
        //    case "2":
        //        DropDownList_ADType.Attributes.Add("style", "display:none");
        //        DropDownList_ADTypeFoodPlaza.Attributes.Add("style", "display:''");
        //        break;
        //    default:
        //        break;
        //}
        //DropDownList_ADType.SelectedValue = "0";
        //DropDownList_ADTypeFoodPlaza.SelectedValue = "0";
        rblActivity.Items.Clear();
    }
    protected void DropDownList_ADTypeFoodPlaza_SelectedIndexChanged(object sender, EventArgs e)
    {
        //switch (DropDownList_ADTypeFoodPlaza.SelectedValue)
        //{
        //    case "1":
        //        CouponSelect.Attributes.Add("style", "display:none");
        //        TextBox_url.Enabled = false;
        //        TextBox_url.Text = "";
        //        break;
        //    case "3":
        //    case "5":
        //        CouponSelect.Attributes.Add("style", "display:none");
        //        TextBox_url.Text = "http://";
        //        TextBox_url.Enabled = true;
        //        break;
        //    default:
        //        break;
        //}
    }
    /// <summary>
    /// 绑定所有的有效活动
    /// </summary>
    private void BindActivity()
    {
        ActivityOperate activityOperate = new ActivityOperate();
        List<Activity> activities = activityOperate.QueryActivity();
        rblActivity.DataSource = activities;
        rblActivity.DataTextField = "name";
        rblActivity.DataValueField = "activityId";
        rblActivity.DataBind();
        if (rblActivity.Items.Count > 0)
        {
            rblActivity.SelectedIndex = 0;
        }
    }
}