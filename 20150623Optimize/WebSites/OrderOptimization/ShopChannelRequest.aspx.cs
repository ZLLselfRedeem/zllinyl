using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control.DDL;

public partial class OrderOptimization_ShopChannelRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(ddlCity,"87");
            BindShopChannel(Convert.ToInt32(ddlCity.SelectedValue), 0, 10);
        }
    }

    private void BindShopChannel(int cityID, int str, int end)
    {
        DataTable dt = new ShopChannelManager().SelectShopRequestByCityID(cityID);
        int tableCount = dt.Rows.Count;
        AspNetPager1.RecordCount = tableCount;
        if (dt.Rows.Count > 0)
        {
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);
            GridView_ShopChannelRequest.DataSource = dt_page;
        }
        else
        {
            GridView_ShopChannelRequest.DataSource = dt;
        }
        lbCount.InnerText = "共计:" + dt.Rows.Count + "条";
        GridView_ShopChannelRequest.DataBind();
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindShopChannel(Common.ToInt32(ddlCity.SelectedValue), AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }

    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindShopChannel(Convert.ToInt32(ddlCity.SelectedValue), 0, 10);
    }

    protected void GridView_ShopChannelRequest_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        int requestID = Common.ToInt32(GridView_ShopChannelRequest.DataKeys[index].Values["id"]);
        int indexOfPage = AspNetPager1.CurrentPageIndex;
        switch (e.CommandName.ToString())
        {
            case "delete":
                new ShopChannelManager().RemoveRequest(requestID);
                BindShopChannel(Convert.ToInt32(ddlCity.SelectedValue), 0, 10);
                AspNetPager1_PageChanged(null, null);
                AspNetPager1.CurrentPageIndex = indexOfPage;
                break;
            default:
                break;
        }
    }

    protected void GridView_ShopChannelRequest_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void GridView_ShopChannelRequest_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}