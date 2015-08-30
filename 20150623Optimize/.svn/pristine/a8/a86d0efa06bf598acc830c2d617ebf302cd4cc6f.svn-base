using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.ComponentModel;
using System.Reflection;
using ChineseCharacterToPinyin;
using System.Configuration;
using System.Web.Services;
using System.Text.RegularExpressions;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using System.Transactions;
using Web.Control.Enum;

public partial class ShopManage_ShopAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            company_init_data.InnerHtml = "";
            GetProvince();
            GetCity();
            GetCounty();
            QueryShopStatus();
        }
    }
    [WebMethod]
    public static string GetShopCoordinate(string shopDetailAddress, string cityName)
    {
        //获取店铺的经纬度
        string shopLocation = "";
        MapLocation baiduMapLocation = Common.GetBaiduMapCoordinate(shopDetailAddress, cityName);
        if (baiduMapLocation != null)
        {
            shopLocation = "{\"lat\":" + 0 + ",\"lng\":" + 0 + ",\"latBaidu\":" + baiduMapLocation.lat + ",\"lngBaidu\":" + baiduMapLocation.lng + "}";
        }
        return shopLocation;
    }
    /// <summary>
    ///验证店铺名称是否已经存在
    /// </summary>
    /// <param name="shopName"></param>
    /// <returns></returns>
    [WebMethod]
    public static bool QueryShopName(string TextBox_shopName)
    {
        ShopOperate shopOperate = new ShopOperate();
        DataTable dtShop = shopOperate.QueryShop();
        DataRow[] drShop = dtShop.Select("shopName='" + TextBox_shopName + "'");
        if (drShop.Length > 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    /// <summary>
    /// 添加一条门店数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_ShopAdd_Click(object sender, EventArgs e)
    {
        if (Common.ToInt32(hidden_companyId.Value) <= 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择公司');</script>");
            return;
        }
        if (RadioButton_NotSupport.Checked == true && tb_notPaymentReason.Text.Trim() == "")//选中暂不支持付款，并且没有填写不支持付款原因
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请填写暂不支持付款原因');</script>");
            return;
        }
        if (Common.ToInt32(ddlLevel2.SelectedValue) < 0)
        {
            CommonPageOperate.AlterMsg(this, "请配置" + ddlLevel1.SelectedItem.Text + "的二级商圈或选择其他商圈");
            return;
        }
        ShopOperate shopOperate = new ShopOperate();
        ShopInfo shopInfo = new ShopInfo();
        shopInfo.canEatInShop = true;
        shopInfo.canTakeout = true;
        shopInfo.cityID = Common.ToInt32(DropDownList_cityID.SelectedValue);
        shopInfo.companyID = Common.ToInt32(hidden_companyId.Value);
        shopInfo.contactPerson = TextBox_contactPerson.Text;
        shopInfo.contactPhone = TextBox_contactPhone.Text;
        shopInfo.countyID = Common.ToInt32(DropDownList_countyID.SelectedValue);
        shopInfo.provinceID = Common.ToInt32(DropDownList_provinceID.SelectedValue);
        shopInfo.shopAddress = TextBox_shopAddress.Text;
        shopInfo.shopBusinessLicense = TextBox_shopBusinessLicense.Text;
        shopInfo.shopHygieneLicense = TextBox_shopHygieneLicense.Text;
        shopInfo.shopLogo = "";
        shopInfo.shopName = TextBox_shopName.Text;
        shopInfo.shopStatus = Common.ToInt32(DropDownList_shopStatus.SelectedValue);
        shopInfo.shopTelephone = TextBox_shopTelePhone.Text;
        shopInfo.isHandle = (int)VAShopHandleStatus.SHOP_UnHandle;
        shopInfo.shopDescription = TextBox_shopDescription.Text;
        shopInfo.sinaWeiboName = TextBox_sinaWeibo.Text;
        shopInfo.qqWeiboName = TextBox_qqWeibo.Text;
        shopInfo.wechatPublicName = TextBox_wechatPublicName.Text;//wangcheng
        shopInfo.openTimes = TextBox_openTime.Text;
        shopInfo.acpp = Common.ToDouble(TextBox_accp.Text.Trim());
        shopInfo.shopVerifyTime = DateTime.Now;
        shopInfo.shopRegisterTime = DateTime.Now;
        shopInfo.isSupportAccountsRound = false;// Common.ToBool(rb_isSupportAccountsRound.Checked);
        shopInfo.shopRating = Common.ToDouble(TextBox_shopRating.Text);//店铺评分 add at 2014-1-3 by jinyanni
        shopInfo.isSupportPayment = Common.ToBool(RadioButton_Support.Checked);
        shopInfo.orderDishDesc = TextBox_orderDishDes.Text.Trim();
        shopInfo.notPaymentReason = tb_notPaymentReason.Text.Trim();
        shopInfo.accountManager = Common.ToInt32(hidden.Value);//添加客户经理编号
        //获取公司图片路径
        CompanyOperate companyOperate = new CompanyOperate();
        CompanyInfo companyInfo = companyOperate.QueryCompany(shopInfo.companyID);
        string companyImagePath = companyInfo.companyImagePath;
        shopInfo.shopImagePath = companyImagePath + CharacterToPinyin.GetAllPYLetters(Common.ToClearSpecialCharString(shopInfo.shopName)) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "/";
        shopInfo.publicityPhotoPath = shopInfo.shopImagePath + "shopPublicityPhoto" + "/";
        //获取店铺的经纬度
        List<ShopCoordinate> shopCoordinateList = new List<ShopCoordinate>();
        ShopCoordinate shopCoordinate = new ShopCoordinate();
        shopCoordinate.latitude = Common.ToDouble11Digit(TextBox_LatitudeBaidu.Text);
        shopCoordinate.longitude = Common.ToDouble11Digit(TextBox_LongitudeBaidu.Text);
        shopCoordinate.mapId = 2;//百度地图编号，暂时固定
        shopCoordinateList.Add(shopCoordinate);
        shopInfo.isSupportRedEnvelopePayment = Common.ToBool(rb_SupportRedEnvelopePayment.Checked);//wangc 门店是否支持红包支付

        using (TransactionScope ts = new TransactionScope())
        {
            int shopId = shopOperate.AddShop(shopInfo, shopCoordinateList);
            if (shopId > 0)
            {
                //将这个店铺管理权限对应给这个用户
                EmployeeConnShopOperate employeeShopOperate = new EmployeeConnShopOperate();
                EmployeeConnShop employeeShop = new EmployeeConnShop();
                employeeShop.shopID = shopId;
                employeeShop.status = 1;
                employeeShop.companyID = shopInfo.companyID;
                VAEmployeeLoginResponse vAEmployeeLoginResponse = ((VAEmployeeLoginResponse)Session["UserInfo"]);//根据session获取用户信息
                employeeShop.employeeID = vAEmployeeLoginResponse.employeeID;

                bool conn = employeeShopOperate.AddEmployeeShop(employeeShop);

                IShopTagService shopTagService = ServiceFactory.Resolve<IShopTagService>();
                object[] shopWithTag = shopTagService.MaintainShopTag(ddlLevel2.SelectedValue, shopId, false);

                if (conn && Common.ToBool(shopWithTag[0]))
                {
                    ts.Complete();
                    Common.RecordEmployeeOperateLog((int)VAEmployeeOperateLogOperatePageType.SHOPINFO, (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE,
                        "城市编号：" + DropDownList_cityID.SelectedItem.Text + "，公司编号：" + hidden_companyId.Value + "，门店联系人："
                       + shopInfo.contactPerson + "，门店联系人电话：" + shopInfo.contactPhone + "，门店所属区编号：" + shopInfo.countyID
                       + "，店铺结算是否支持四舍五入：" + shopInfo.isSupportAccountsRound + "，营业时间：" + shopInfo.openTimes + "，门店所属省份编号：" + DropDownList_provinceID.SelectedItem.Text
                       + "，店铺腾讯微博：" + shopInfo.qqWeiboName + "，门店详细地址：" + shopInfo.shopAddress + "，门店营业执照：" + shopInfo.shopBusinessLicense
                       + "，店铺描述：" + shopInfo.shopDescription + "，门店卫生许可证：" + shopInfo.shopHygieneLicense + "，门店编号：" + shopInfo.shopID + "，门店名称："
                       + shopInfo.shopName + "，门店注册时间：" + shopInfo.shopRegisterTime + "，门店状态：" + DropDownList_shopStatus.SelectedItem.Text + "，门店电话：" + shopInfo.shopTelephone
                       + "，门店验证时间：" + shopInfo.shopVerifyTime + "，门店新浪微博：" + shopInfo.sinaWeiboName + "，门店微信公共帐号：" + shopInfo.wechatPublicName
                       + ",百度纬度：" + TextBox_LatitudeBaidu.Text + "，百度经度：" + TextBox_LongitudeBaidu.Text + "，店铺评分：" + TextBox_shopRating.Text + ",客户经理：" + shopInfo.accountManager.ToString()
                       + "，支持红包支付：" + shopInfo.isSupportRedEnvelopePayment);
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('添加成功！');</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('添加失败！');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('门店信息未找到！');</script>");
            }
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

        GetLevel1(Common.ToInt32(DropDownList_cityID.SelectedValue));
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
    protected void DropDownList_provinceID_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCity();
        GetCounty();
    }
    protected void DropDownList_cityID_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCounty();
        GetLevel1(Common.ToInt32(DropDownList_cityID.SelectedValue));
    }
    [WebMethod]
    public static string GetDate(string str)
    {
        EmployeeOperate operate = new EmployeeOperate();
        List<PartEmployee> list = operate.GetPartEmployeeInfo(str);
        return JsonOperate.JsonSerializer<List<PartEmployee>>(list);
    }

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

    protected void ddlLevel1_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLevel2(Common.ToInt32(ddlLevel1.SelectedValue));
    }

}