using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.WebPageDll;

public partial class OrderOptimization_ShopChannelDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                TextBox_MerchantName.InnerText = Common.ToString(Request.QueryString["shopName"]) + "增值页权限管理";
                //TextBox_MerchantName.Font.Size = 14;
                DataTable dt = new ShopChannelManager().SelectShopChannelList(Common.ToInt32(Request.QueryString["id"]));
                GridView_ShopChannel.DataSource = dt;
                GridView_ShopChannel.DataBind();
                for (int i = 0; i < this.GridView_ShopChannel.Rows.Count; i++)
                {
                    int shopChannelID = Common.ToInt32(GridView_ShopChannel.DataKeys[i].Values["shopChannelID"]);
                    RadioButtonList auth = (RadioButtonList)GridView_ShopChannel.Rows[i].FindControl("radListAuthorization");
                    DataView dv = dt.AsDataView();
                    dv.RowFilter = "shopChannelID=" + shopChannelID;
                    if (Common.ToString(dv[0]["isAuthorization"]) == "False")
                    {
                        auth.SelectedValue = "0";
                    }
                    else
                    {
                        auth.SelectedValue = "1";
                    }
                }
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        int shopID = Common.ToInt32(Request.QueryString["id"]);
        int cityID = ShopChannelOperate.SearchCityID(shopID);
        Response.Redirect("ShopChannelManager.aspx?cityID=" + cityID + "&pageIndex=" + Convert.ToString(Request.QueryString["pageIndex"]) + "&query=" + Convert.ToString(Request.QueryString["query"]));
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        bool flag = true;
        for (int i = 0; i < this.GridView_ShopChannel.Rows.Count; i++)
        {
            int shopChannelID = Common.ToInt32(GridView_ShopChannel.DataKeys[i].Values["shopChannelID"]);
            RadioButtonList auth = (RadioButtonList)GridView_ShopChannel.Rows[i].FindControl("radListAuthorization");
            bool result = new ShopChannelManager().UpdateAuth(shopChannelID, Convert.ToInt32(auth.SelectedValue));
            if (result == false)
            {
                flag = false;
                break;
            }
        }

        if (flag == true)
        {
            int shopID = Common.ToInt32(Request.QueryString["id"]);
            int cityID = ShopChannelOperate.SearchCityID(shopID);
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('设置成功！');window.location.href='ShopChannelManager.aspx?cityID=" + cityID + "&pageIndex=" + Convert.ToString(Request.QueryString["pageIndex"]) + "&query=" + Convert.ToString(Request.QueryString["query"]) + "'</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('设置失败！');</script>");
        }
        //Response.Redirect("ShopChannelManager.aspx?cityID=" + Convert.ToString(Request.QueryString["cityID"]));
    }
    protected void GridView_ShopChannel_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void GridView_ShopChannel_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void GridView_ShopChannel_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView_ShopChannel_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
}