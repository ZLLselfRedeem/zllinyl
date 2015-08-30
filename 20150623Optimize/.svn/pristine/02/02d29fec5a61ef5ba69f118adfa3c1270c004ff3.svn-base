using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using System.Web.Services;
using System.Configuration;
using Web.Control.Enum;
using Web.Control;

public partial class ShopManage_ShopHandle : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        text.Value = Common.ToString(Request.QueryString["name"]);//回传页面显示公司名称
        if (!IsPostBack)
        {
            init_date.InnerHtml = "";
            GetShopInfo();
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
    /// <summary>
    /// 获取某门店信息
    /// </summary>
    /// <param name="ShopID"></param>
    protected void GetShopInfo()
    {
        int shopID = Common.ToInt32(Request.QueryString["id"]);
        ShopInfo shopInfo = new ShopInfo();
        ShopOperate shopOperate = new ShopOperate();
        shopInfo = shopOperate.QueryShop(shopID);
        if (shopInfo.countyID > 0)//表示当前是有店铺
        {
            GetProvince(); //省
            DropDownList_provinceID.Text = shopInfo.provinceID.ToString();
            GetCity(); //市
            DropDownList_cityID.Text = shopInfo.cityID.ToString();
            GetCounty();//县
            DropDownList_countyID.Text = shopInfo.countyID.ToString();
            QueryShopStatus(); //门店状态
            QueryShopHandleStatus(); //门店审批状态
            DropDownList_shopStatus.Text = shopInfo.shopStatus.ToString();
            TextBox_contactPerson.Text = shopInfo.contactPerson;
            TextBox_contactPhone.Text = shopInfo.contactPhone;
            TextBox_shopAddress.Text = shopInfo.shopAddress;
            TextBox_shopBusinessLicense.Text = shopInfo.shopBusinessLicense;
            TextBox_shopHygieneLicense.Text = shopInfo.shopHygieneLicense;
            TextBox_shopTelePhone.Text = shopInfo.shopTelephone;
            DropDownList_IsHandle.Text = shopInfo.isHandle.ToString();
            TextBox_shopDescription.Text = shopInfo.shopDescription;
            TextBox_sinaWeibo.Text = shopInfo.sinaWeiboName;
            TextBox_qqWeibo.Text = shopInfo.qqWeiboName;
            TextBox_wechatPublicName.Text = shopInfo.wechatPublicName;
            TextBox_openTime.Text = shopInfo.openTimes;
            TextBox_shopRating.Text = shopInfo.shopRating.ToString();
            EmployeeOperate operate = new EmployeeOperate();
            DataTable dt = operate.QueryEmployeeByEmployeeId(shopInfo.accountManager.HasValue ? shopInfo.accountManager.Value : 0);
            if (dt.Rows.Count == 1)
            {
                TextBox_manager.Text = Common.ToString(dt.Rows[0]["EmployeeFirstName"]) == "" ? Common.ToString(dt.Rows[0]["UserName"]) : Common.ToString(dt.Rows[0]["EmployeeFirstName"]);
            }
            TextBox_accp.Text = shopInfo.acpp.ToString();
            ShopCoordinate shopCoordinateBaidu = shopOperate.QueryShopCoordinate(2, shopID);
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
            tb_notPaymentReason.Text = shopInfo.notPaymentReason;
            CompanyOperate companyOper = new CompanyOperate();
            CompanyInfo companyInfo = companyOper.QueryCompany(shopInfo.companyID);
            if (companyInfo != null)
            {
                TextBox_companyName.Text = companyInfo.companyName;
            }
            string imagePath = WebConfig.CdnDomain + WebConfig.ImagePath;
            img_ShopLogo.ImageUrl = imagePath + shopInfo.shopImagePath + shopInfo.shopLogo;
            img_ShopBgImg.ImageUrl = imagePath + shopInfo.publicityPhotoPath;
        }
        else
        {
            GetProvince();
            GetCity();
            GetCounty();
            QueryShopStatus();
            QueryShopHandleStatus();
            TextBox_contactPerson.Text = "";
            TextBox_contactPhone.Text = "";
            TextBox_shopAddress.Text = "";
            TextBox_shopBusinessLicense.Text = "";
            TextBox_shopHygieneLicense.Text = "";
            TextBox_shopTelePhone.Text = "";
        }
    }
    protected void Button_ShopHandle_Click(object sender, EventArgs e)
    {
        ShopOperate shopOperate = new ShopOperate();
        int shopId = Common.ToInt32(Request.QueryString["id"]);
        int handle = Common.ToInt32(DropDownList_IsHandle.SelectedValue);
        if (shopOperate.ModifyShopIsHandle(shopId, handle))
        {
            GetShopInfo();
            string desc = String.Format("门店名称：{0}，审核状态：{1}，公司名称：{2}", text.Value, DropDownList_IsHandle.SelectedItem.Text.ToString(), TextBox_companyName.Text);
            Common.RecordEmployeeOperateLog((int)VAEmployeeOperateLogOperatePageType.SHOP_HANDLE, (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE, desc);

            var shopInfo = new ShopOperate().QueryShop(shopId);
            int employeeId = ((VAEmployeeLoginResponse)HttpContext.Current.Session["UserInfo"]).employeeID;
            ShopHandleLogOperate.InsertShopHandleLog(handle, shopId, text.Value, shopInfo.cityID, employeeId);
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('审核成功')</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('审核失败')</script>");
        }
    }
}