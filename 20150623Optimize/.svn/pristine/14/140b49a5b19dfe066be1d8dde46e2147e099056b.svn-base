using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.WebPageDll.OrderOptimization;
using Web.Control.DDL;

public partial class OrderOptimization_ShopChannelManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["cityID"] != null)
            {
                string cityID = Request.QueryString["cityID"];
                new CityDropDownList().BindCity(ddlCity, cityID);
                TextBox_MerchantName.Text = Convert.ToString(Request.QueryString["query"]);
                string merchantName = TextBox_MerchantName.Text.Trim();
                DataTable dtMerchant = new DataTable();
                if (!string.IsNullOrEmpty(merchantName))
                {
                    dtMerchant = GetNewDataTable(merchantName);
                    PageBind(0, 10, dtMerchant);
                }
                else
                {
                    DataTable dt = GetShopByCityID(Convert.ToInt32(ddlCity.SelectedValue));
                    PageBind(0, 10, dt);
                }

                AspNetPager1.CurrentPageIndex = Convert.ToInt32(Request.QueryString["pageIndex"]);
                AspNetPager1_PageChanged(null, null);
            }
            else
            {
                new CityDropDownList().BindCity(ddlCity,"87");
                DataTable dt = GetShopByCityID(Convert.ToInt32(ddlCity.SelectedValue));
                PageBind(0, 10, dt);
                GridView_ShopChannel.DataBind();
                PageList();
            }
        }
    }

    private void PageBind(int str, int end, DataTable dt)
    {
        int tableCount = dt.Rows.Count;
        AspNetPager1.RecordCount = tableCount;
        if (dt.Rows.Count > 0)
        {
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);
            GridView_ShopChannel.DataSource = dt_page;
        }
        else
        {
            GridView_ShopChannel.DataSource = dt;
        }
        lbCount.InnerText = "共计:" + dt.Rows.Count + "条";
    }

    private void PageList()
    {
        // DataTable dtAllShop = new ShopChannelManager().SelectChannel();
        List<Tuple<int, string, int>> channels = ChannelOperate.SelectAllChannel();
        List<Tuple<int, bool, int>> shopChannels = ShopChannelOperate.SelectAllShopChannel();
        for (int i = 0; i < this.GridView_ShopChannel.Rows.Count; i++)
        {
            int shopID = Common.ToInt32(GridView_ShopChannel.DataKeys[i].Values["shopID"]);
            int cityID = Common.ToInt32(GridView_ShopChannel.DataKeys[i].Values["cityID"]);
            List<Tuple<int, string, int>> filterChannel = channels.FindAll(tuple => tuple.Item3 == cityID);
            List<Tuple<int, bool, int>> filterShopChannel = shopChannels.FindAll(tuple => tuple.Item3 == shopID);

            Label page = (Label)GridView_ShopChannel.Rows[i].FindControl("AppreciationPage");
            //DataView dv = dtAllShop.AsDataView();
            //dv.RowFilter = "ID=" + shopID + "Or ID is null";
            foreach (var item in filterChannel)
            {
                var result = filterShopChannel.Find(tuple => tuple.Item1 == item.Item1);
                if (result != null && result.Item2)
                {
                    page.Text += "<a> &nbsp;&nbsp;&nbsp;&nbsp;<font color=blue>" + item.Item2 + "</font></a>";
                }
                else
                {
                    page.Text += "<a> &nbsp;&nbsp;&nbsp;&nbsp;<font color=gray>" + item.Item2 + "</font></a>";
                }
            }
                //for (int j = 0; j < dv.Count; j++)
                //{
                //    if (dv[j]["auth"].ToString() == "True")
                //    {
                //        page.Text += "<a> &nbsp;&nbsp;&nbsp;&nbsp;<font color=blue>" + dv[j]["channelName"].ToString() + "</font></a>";
                //    }
                //    else
                //    {
                //        page.Text += "<a> &nbsp;&nbsp;&nbsp;&nbsp;<font color=gray>" + dv[j]["channelName"].ToString() + "</font></a>";
                //    }
                //}
        }
    }

    private DataTable GetShopByCityID(int cityID)
    {
        DataTable dt = new ShopChannelManager().SelectShopByCityID(cityID);
        return dt;
    }

    protected void Button_QueryMerchant_Click(object sender, EventArgs e)
    {
        string merchantName = TextBox_MerchantName.Text.Trim();
        DataTable dtMerchant = new DataTable();
        if (!string.IsNullOrEmpty(merchantName))
        {
            dtMerchant = GetNewDataTable(merchantName);
            PageBind(0, 10, dtMerchant);
        }
        else
        {
            DataTable dt = GetShopByCityID(Convert.ToInt32(ddlCity.SelectedValue));
            PageBind(0, 10, dt);
        }

        AspNetPager1.CurrentPageIndex = 0;
        GridView_ShopChannel.DataBind();
        PageList();
    }

    private DataTable GetNewDataTable(string condition)
    {
        DataTable dtMerchant = GetShopByCityID(Common.ToInt32(ddlCity.SelectedValue));
        DataTable newdt = new DataTable();
        newdt = dtMerchant.Clone();
        string likecompanyname = "shopName like '%" + condition + "%'";
        DataRow[] dr = dtMerchant.Select(likecompanyname);
        for (int i = 0; i < dr.Length; i++)
        {
            newdt.ImportRow((DataRow)dr[i]);
        }
        return newdt;
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        string merchantName = TextBox_MerchantName.Text.Trim();
        DataTable dtMerchant = new DataTable();
        if (!string.IsNullOrEmpty(merchantName))
        {
            dtMerchant = GetNewDataTable(merchantName);
            PageBind(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex, dtMerchant);
        }
        else
        {
            DataTable dt = GetShopByCityID(Convert.ToInt32(ddlCity.SelectedValue));
            PageBind(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex, dt);
        }
        GridView_ShopChannel.DataBind();
        PageList();
    }

    protected void GridView_ShopChannel_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        int shopID = Common.ToInt32(GridView_ShopChannel.DataKeys[index].Values["shopID"]);
        //int cityID = Common.ToInt32(ddlCity.SelectedValue);
        string shopName = Common.ToString(GridView_ShopChannel.DataKeys[index].Values["shopName"]);
        string queryCondition = TextBox_MerchantName.Text.Trim();
        switch (e.CommandName.ToString())
        {
            case "edit":
                int pageIndex = AspNetPager1.CurrentPageIndex;
                new ShopChannelManager().DefaultInsert(shopID);
                Response.Redirect("ShopChannelDetail.aspx?id=" + shopID + "&pageIndex=" + pageIndex + "&query=" + queryCondition + "&shopName=" + shopName);
                break;
            case "config":
                new ShopChannelManager().DefaultInsert(shopID);
                Response.Redirect("ShopChannelConfig.aspx?shopID=" + shopID + "&shopName=" + shopName);
                break;
            default:
                break;
        }
    }
}