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

public partial class ShopManage_shopWeChatConfig : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCompanyInfo();
            GetShopInfo();
        }
    }
    /// <summary>
    /// 获取公司列表信息
    /// </summary>
    protected void GetCompanyInfo()
    {
        if (Session["UserInfo"] != null)
        {
            EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
            List<VAEmployeeCompany> employeeCompany = new List<VAEmployeeCompany>();
            VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
            int employeeID = vAEmployeeLoginResponse.employeeID;
            employeeCompany = employeeConnShopOperate.QueryEmployeeCompany(employeeID);
            DropDownList_Company.DataSource = employeeCompany;
            DropDownList_Company.DataTextField = "CompanyName";
            DropDownList_Company.DataValueField = "CompanyID";
            DropDownList_Company.DataBind();
        }
    }
    /// <summary>
    /// 获取门店列表信息
    /// </summary>
    protected void GetShopInfo()
    {
        if (DropDownList_Company.Items.Count == 0)
        {
            return;//没有公司信息，直接不显示门店信息
        }
        else
        {
            EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
            List<VAEmployeeShop> employeeShop = new List<VAEmployeeShop>();
            VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
            int employeeID = vAEmployeeLoginResponse.employeeID;
            int companyID = Common.ToInt32(DropDownList_Company.SelectedValue);
            employeeShop = employeeConnShopOperate.QueryEmployeeShopByCompanyAndEmplyee(employeeID, companyID);
            DropDownList_Shop.DataSource = employeeShop;
            DropDownList_Shop.DataTextField = "shopName";
            DropDownList_Shop.DataValueField = "shopID";
            DropDownList_Shop.DataBind();
            DropDownList_Shop.Items.Add(new ListItem("所有门店", "0"));
            DropDownList_Shop.SelectedValue = "0";//选中所有门店k
            BindData(0, 10);
        }
    }
    /// <summary>
    /// 选择公司刷新门店列表
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DropDownList_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetShopInfo();
    }
    /// <summary>
    /// 选择门店加载数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DropDownList_Shop_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindData(0, 10);
    }
    /// <summary>
    /// 绑定列表数据
    /// </summary>
    /// <param name="str"></param>
    /// <param name="end"></param>
    void BindData(int str, int end)
    {
        int shopId = Common.ToInt32(DropDownList_Shop.SelectedValue);
        int companyId = Common.ToInt32(DropDownList_Company.SelectedValue);
        ShopOperate shopOper = new ShopOperate();
        DataTable dt = shopOper.QueryShopWechatOrderConfig(shopId, companyId);
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);
            GridView_ShopWechatConfig.DataSource = dt_page;
            GridView_ShopWechatConfig.DataBind();
            for (int i = 0; i < dt_page.Rows.Count; i++)
            {
                if (Common.ToInt32(dt_page.Rows[i]["status"]) == 1)//开启
                {
                    ((LinkButton)GridView_ShopWechatConfig.Rows[i].FindControl("LinkButton1")).Text = "开启";
                }
                else if (Common.ToInt32(dt_page.Rows[i]["status"]) != 1)//关闭
                {
                    ((LinkButton)GridView_ShopWechatConfig.Rows[i].FindControl("LinkButton1")).Text = "关闭";
                }
            }
        }
        else
        {
            GridView_ShopWechatConfig.DataSource = null;
            GridView_ShopWechatConfig.DataBind();
        }
    }
    protected void GridView_ShopWechatConfig_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//获得点击的当前行
        long id = Common.ToInt64(GridView_ShopWechatConfig.DataKeys[index].Values["id"].ToString());//获得点击的按钮
        ShopOperate shopOper = new ShopOperate();
        if (e.CommandName.ToString() == "SetIsValid")
        {
            if (shopOper.ModifyShopWechatOrderConfigStatus(id) == false)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('开启失败！');</script>");
            }
            else
            {
                if (((LinkButton)e.CommandSource).Text == "关闭")
                {
                    ((LinkButton)e.CommandSource).Text = "开启";
                }
                else
                {
                    ((LinkButton)e.CommandSource).Text = "关闭";
                }
            }
        }
    }
    /// <summary>
    /// 生成微信点菜Url
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Query_Click(object sender, EventArgs e)
    {
        string cookie = Common.ToString(Guid.NewGuid()) + Common.ToString(System.DateTime.Now.Ticks);
        int shopId = Common.ToInt32(DropDownList_Shop.SelectedValue);
        if (shopId == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择门店');</script>");
        }
        else
        {
            ShopOperate shopMan = new ShopOperate();
            DataTable dt = shopMan.QueryShopWechatOrderConfig(shopId, 0);
            if (dt.Rows.Count > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('当前门店已有数据');</script>");
            }
            else
            {
                ShopInfo shopInfo = shopMan.QueryShop(shopId);
                string wechatOrderUrl = ConfigurationManager.AppSettings["Server"].ToString() + "/AppPages/wechatOrder/menu.aspx?sc=" + cookie + "&cid=" + shopInfo.cityID + "";
                if (shopMan.AddShopWechatOrderConfig(cookie, shopId, wechatOrderUrl) == true)
                {
                    BindData(0, 10);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('操作失败');</script>");
                }
            }
        }
    }
    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindData(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
}