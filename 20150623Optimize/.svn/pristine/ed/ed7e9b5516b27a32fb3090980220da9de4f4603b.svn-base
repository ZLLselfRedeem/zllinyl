using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Configuration;

public partial class MenuManage_MenuList : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlInject myCheck = new SqlInject(this.Request);
        myCheck.CheckAllPageInject();
        text.Value = Common.ToString(Request.QueryString["name"]);//回传页面显示公司名称
        if (!IsPostBack)
        {
            init_date.InnerHtml = "";
            GetMenu();//获取菜谱信息
        }
    }
    /// <summary>
    /// 获取菜单信息
    /// </summary>
    protected void GetMenu()
    {
        int shopId = Common.ToInt32(Request.QueryString["id"]);
        ShopOperate operate = new ShopOperate();
        ShopInfo shopInfo = operate.QueryShop(shopId);
        int companyID = shopInfo.companyID;
        CompanyOperate companyOperate = new CompanyOperate();
        CompanyInfo companyInfo = companyOperate.QueryCompany(companyID);
        Label_companyName.Text = companyInfo.companyName;
        MenuConnShopOperate menuConnShopOperate = new MenuConnShopOperate();
        DataTable dtMenus = menuConnShopOperate.QueryMenusByShopId(shopId);
        GridView1.DataSource = dtMenus;
        GridView1.DataBind();
    }
    /// <summary>
    /// 修改菜谱
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Redirect("MenuUpdate.aspx?menuId=" + GridView1.DataKeys[GridView1.SelectedIndex].Values["MenuID"]);
    }
    /// <summary>
    /// 更新菜谱
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        MenuOperate menuOperate = new MenuOperate();
        int menuId = Common.ToInt32(GridView1.DataKeys[e.RowIndex].Values["MenuID"].ToString());
        int result = menuOperate.UpdateMenu(menuId);
        if (result > 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('已成功更新到服务器！')</script>");
            GetMenu();
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('更新失败，请重试！')</script>");
        }
    }
}