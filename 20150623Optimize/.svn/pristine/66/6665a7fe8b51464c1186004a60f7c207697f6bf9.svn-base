using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using System.Data;

public partial class ViewAllocVip_shopVIPSpeedConfigManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetProvince();
            this.ddlProvinceID.SelectedValue = "11";
            GetCity();

            BindCityVipSpeed(0, 10, Common.ToInt32(ddlCityID.SelectedValue));
        }
    }
    private void BindCityVipSpeed(int str, int end, int cityId = 0)
    {
        IShopVipSpeedConfigService shopVipSpeedConfig = ServiceFactory.Resolve<IShopVipSpeedConfigService>();
        DataTable cityVIPSpeed = shopVipSpeedConfig.GetCityVipSpeed(cityId);
        if (cityVIPSpeed != null && cityVIPSpeed.Rows.Count > 0)
        {
            int cnt = cityVIPSpeed.Rows.Count;
            AspNetPager1.RecordCount = cnt;
            DataTable dt_page = Common.GetPageDataTable(cityVIPSpeed, str, end);

            gdvVipSpeed.DataSource = cityVIPSpeed;
            gdvVipSpeed.DataBind();
        }
        else
        {
            gdvVipSpeed.DataSource = null;
            gdvVipSpeed.DataBind();
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        int cityId = Common.ToInt32(ddlCityID.SelectedValue);
        BindCityVipSpeed(0, 10, cityId);
    }
    /// <summary>
    /// 获取省份信息
    /// </summary>
    protected void GetProvince()
    {
        ProvinceOperate provinceOperate = new ProvinceOperate();
        DataTable dtProvince = provinceOperate.QueryProvince();
        ddlProvinceID.DataSource = dtProvince;
        ddlProvinceID.DataTextField = "ProvinceName";
        ddlProvinceID.DataValueField = "ProvinceID";
        ddlProvinceID.DataBind();
    }
    protected void GetDetailProvince()
    {
        ProvinceOperate provinceOperate = new ProvinceOperate();
        DataTable dtProvince = provinceOperate.QueryProvince();
        ddlProvinceDetail.DataSource = dtProvince;
        ddlProvinceDetail.DataTextField = "ProvinceName";
        ddlProvinceDetail.DataValueField = "ProvinceID";
        ddlProvinceDetail.DataBind();
    }
    /// <summary>
    /// 获取市信息
    /// </summary>
    protected void GetCity()
    {
        int provinceID = Common.ToInt32(ddlProvinceID.SelectedValue);
        CityOperate cityOperate = new CityOperate();
        DataTable dtCity = cityOperate.QueryCity(provinceID);
        ddlCityID.DataSource = dtCity;
        ddlCityID.DataTextField = "CityName";
        ddlCityID.DataValueField = "CityID";
        ddlCityID.DataBind();
    }
    /// <summary>
    /// 获取市信息
    /// </summary>
    protected void GetDetailCity()
    {
        int provinceID = Common.ToInt32(ddlProvinceDetail.SelectedValue);
        CityOperate cityOperate = new CityOperate();
        DataTable dtCity = cityOperate.QueryCity(provinceID);
        ddlCityDetail.DataSource = dtCity;
        ddlCityDetail.DataTextField = "CityName";
        ddlCityDetail.DataValueField = "CityID";
        ddlCityDetail.DataBind();
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        this.divList.Attributes.Add("style", "display:none");
        this.divDetail.Attributes.Add("style", "display:''");
        GetDetailProvince();
        this.ddlProvinceDetail.SelectedValue = "11";
        GetDetailCity();
    }

    protected void lnkbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        int cityId = Common.ToInt32(e.CommandArgument);

        switch (e.CommandName)
        {
            case "del":
                IShopVipSpeedConfigService shopVipSpeedConfig = ServiceFactory.Resolve<IShopVipSpeedConfigService>();
                bool delete = shopVipSpeedConfig.DeleteShopVipSpeed(cityId);
                if (delete)
                {
                    BindCityVipSpeed(0, 10, Common.ToInt32(ddlCityID.SelectedValue));
                    CommonPageOperate.AlterMsg(this, "删除成功！");
                }
                else
                {
                    CommonPageOperate.AlterMsg(this, "删除失败！");
                }
                break;
            default:
                break;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int cityId = Common.ToInt32(ddlCityDetail.SelectedValue);
        int min = Common.ToInt32(txbMinSpeed.Text.Trim());
        int max = Common.ToInt32(txbMaxSpeed.Text.Trim());

        IShopVipSpeedConfigService shopVipSpeedConfig = ServiceFactory.Resolve<IShopVipSpeedConfigService>();

        bool result = shopVipSpeedConfig.CreateShopVipSpeed(cityId, min, max);
        if (result)
        {   
            this.txbMinSpeed.Text = "";
            this.txbMaxSpeed.Text = "";
         
            CommonPageOperate.AlterMsg(this, "数据生成成功！");
         
        }
        else
        {
            CommonPageOperate.AlterMsg(this, "数据生成失败！");
        }
    }

    /// <summary>
    /// 分页操作
    /// </summary>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindCityVipSpeed(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }

    protected void ddlProvinceID_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCity();
    }
    protected void ddlProvinceDetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDetailCity();
    }
    protected void btnCancle_Click(object sender, EventArgs e)
    {
        this.divList.Attributes.Add("style", "display:''");
        this.divDetail.Attributes.Add("style", "display:none");
        this.txbMinSpeed.Text = "";
        this.txbMaxSpeed.Text = "";
    }
}