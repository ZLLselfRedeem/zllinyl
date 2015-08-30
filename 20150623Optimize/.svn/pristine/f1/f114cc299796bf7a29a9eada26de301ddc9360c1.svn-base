using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;

public partial class AuthorizationManagement_ShopAuthorityList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Bind();
        }
    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void Bind()
    {
        IShopAuthorityService shopAuthorityService = ServiceFactory.Resolve<IShopAuthorityService>();
        var list = shopAuthorityService.GetAllShopAuthorities();
        GridView1.DataSource = list;
        GridView1.DataBind();
    }

    protected void GridView1_OnRowEditing(object sender, GridViewEditEventArgs e)
    {
        var dataKey = ((GridView)sender).DataKeys[e.NewEditIndex];
        if (dataKey != null)
        {
            if (dataKey.Values != null)
            {
                var shopAuthorityId = (int)dataKey.Values["ShopAuthorityId"];
                Response.Redirect("~/AuthorizationManagement/ShopAuthorityEdit.aspx?shopAuthorityId=" + shopAuthorityId.ToString());
                return;
            }
        }
        e.Cancel = true;
    }

    protected void GridView1_OnRowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        var dataKey = ((GridView)sender).DataKeys[e.RowIndex];
        if (dataKey != null)
        {
            if (dataKey.Values != null)
            {
                var shopAuthorityId = (int)dataKey.Values["ShopAuthorityId"];
                //Response.Redirect("~/AuthorizationManagement/ShopAuthorityEdit.aspx?shopAuthorityId=" + shopAuthorityId.ToString());
                IShopAuthorityService shopAuthorityService = ServiceFactory.Resolve<IShopAuthorityService>();
                shopAuthorityService.Delete(new ShopAuthority() { ShopAuthorityId = shopAuthorityId });
                Page.ClientScript.RegisterStartupScript(this.GetType(), "del", "alert('删除成功！');window.location='ShopAuthorityList.aspx';", true);
                return;
            }
        }
        e.Cancel = true;
    }
}