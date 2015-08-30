using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
public partial class ShopManage_ShopManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        text.Value = Common.ToString(Request.QueryString["name"]);//回传页面显示公司名称
        if (!IsPostBack)
        {
            init_date.InnerHtml = "";
            GetShop();
        }
    }
    /// <summary>
    /// 获取门店信息
    /// </summary>
    protected void GetShop()
    {
        EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
        VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
        int employeeID = vAEmployeeLoginResponse.employeeID;
        // int companyID = Common.ToInt32(Request.QueryString["id"]);
        int shopId = Common.ToInt32(Request.QueryString["id"]);
        DataTable dtShop = employeeConnShopOperate.QueryEmployeeShopByEmplyee(employeeID);
        if (dtShop.Rows.Count > 0)
        {
            DataView dvShop = dtShop.DefaultView;
            dvShop.RowFilter = "shopID=" + shopId;
            dtShop = dvShop.ToTable();
        }
        GridView1.DataSource = dtShop;
        GridView1.DataBind();
    }
    /// <summary>
    /// 删除某行
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ShopOperate shopOperate = new ShopOperate();
        int ShopID = Common.ToInt32(GridView1.DataKeys[e.RowIndex].Values["shopID"].ToString());
        int isHandle = Common.ToInt32(GridView1.DataKeys[e.RowIndex].Values["isHandle"].ToString());
        if (isHandle != (int)VAShopHandleStatus.SHOP_Pass)
        {
            bool i = shopOperate.RemoveShop(ShopID);
            if (i == true)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('删除成功！');</script>");
                GetShop();
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('删除失败！');</script>");
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('当前门店已上线，请先下线再执行删除操作！');</script>");
        }
    }
    /// <summary>
    /// 编辑项
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        int isHandle = Common.ToInt32(GridView1.DataKeys[index].Values["isHandle"].ToString());
        string shopId = GridView1.DataKeys[index].Values["shopID"].ToString();
        if (Common.ToString(e.CommandName) == "Select")//修改不需要具备特殊权限
        {
            Response.Redirect("ShopUpdate.aspx?shopId=" + shopId);
        }
        else
        {
            RoleOperate roleOperate = new RoleOperate();
            DataTable specialAuthority = roleOperate.QuerySpecialAuthorityInfoByEmployeeID(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID, 0);//获得特殊权限
            if (specialAuthority.Rows.Count > 0)
            {
                for (int i = 0; i < specialAuthority.Rows.Count; i++)
                {
                    switch (Common.ToInt32(specialAuthority.Rows[i]["specialAuthorityId"]))
                    {
                        case (int)VASpecialAuthority.SHOP_SHOPIMAGEREVELATION://门店图片权限
                            if (Common.ToString(e.CommandName) == "ShopImage")
                            {
                                Response.Redirect("ShopImageRevelation .aspx?shopId=" + shopId);
                            }
                            break;
                        case (int)VASpecialAuthority.SHOP_SHOPSUNDRYMANAGE://餐厅杂项
                            if (Common.ToString(e.CommandName) == "TablewareAndRice")
                            {
                                Response.Redirect("ShopSundryManage.aspx?shopId=" + shopId);
                            }
                            break;
                        case (int)VASpecialAuthority.SHOP_VIPDISCOUNT://店铺VIP折扣管理
                            if (Common.ToString(e.CommandName) == "ShopVipDiscount")
                            {
                                Response.Redirect("ShopVipDicount.aspx?shopId=" + shopId);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}