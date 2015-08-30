using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using System.Transactions;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp.SQLServerDAL;

public partial class MenuManage_MenuUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlInject myCheck = new SqlInject(this.Request);
        myCheck.CheckAllPageInject();
        if (!IsPostBack)
        {
            GetLang();
            QueryCompanyByMenu();
            QueryShop();
            QueryMenuInfo();
        }
    }
    /// <summary>
    /// 获取语言信息
    /// </summary>
    protected void GetLang()
    {
        LangOperate lo = new LangOperate();
        DataTable dt = lo.SearchLang();
        DropDownList_LangID.DataSource = dt;
        DropDownList_LangID.DataTextField = "LangName";
        DropDownList_LangID.DataValueField = "LangID";
        DropDownList_LangID.DataBind();
    }
    protected void QueryCompanyByMenu()
    {
        int menuId = Common.ToInt32(Request.QueryString["menuID"]);
        MenuOperate menuOpe = new MenuOperate();
        DataTable dtCompany = menuOpe.QueryCompanyByMenu(menuId);
        if (dtCompany.Rows.Count == 1)
        {
            DropDownList_Company.DataSource = dtCompany;
            DropDownList_Company.DataTextField = "companyName";
            DropDownList_Company.DataValueField = "companyID";
            DropDownList_Company.DataBind();
        }
    }
    /// <summary>
    /// 获取所有公司
    /// </summary>
    protected void QueryCompany()
    {
        if (Session["UserInfo"] != null)
        {
            EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
            List<VAEmployeeCompany> employeeCompany = new List<VAEmployeeCompany>();
            VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
            if (vAEmployeeLoginResponse != null)
            {
                int employeeID = vAEmployeeLoginResponse.employeeID;
                employeeCompany = employeeConnShopOperate.QueryEmployeeCompany(employeeID);
                DropDownList_Company.DataSource = employeeCompany;
                DropDownList_Company.DataTextField = "CompanyName";
                DropDownList_Company.DataValueField = "CompanyID";
                DropDownList_Company.DataBind();
            }
        }
    }
    /// <summary>
    /// 获取所有门店
    /// </summary>
    protected void QueryShop()
    {
        EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
        List<VAEmployeeShop> employeeShop = new List<VAEmployeeShop>();
        VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
        if (vAEmployeeLoginResponse != null)
        {
            int employeeID = vAEmployeeLoginResponse.employeeID;
            int companyID = Common.ToInt32(DropDownList_Company.SelectedValue);
            employeeShop = employeeConnShopOperate.QueryEmployeeShopByCompanyAndEmplyee(employeeID, companyID);
            GridView_Shop.DataSource = employeeShop;
            GridView_Shop.DataBind();
        }
    }
    protected void QueryMenuInfo()
    {
        int menuId = Common.ToInt32(Request.QueryString["menuID"]);
        MenuManager man = new MenuManager();
        DataTable dtMenu = man.QueryMenu(menuId);
        if (dtMenu.Rows.Count > 0)
        {
            HiddenField_MenuI18nID.Value = dtMenu.Rows[0]["MenuI18nID"].ToString();
            HiddenField_MenuID.Value = dtMenu.Rows[0]["MenuID"].ToString();
            TextBox_MenuDesc.Text = dtMenu.Rows[0]["MenuDesc"].ToString();
            TextBox_MenuName.Text = dtMenu.Rows[0]["MenuName"].ToString();
            TextBox_MenuSequence.Text = dtMenu.Rows[0]["MenuSequence"].ToString();
            DropDownList_LangID.Text = dtMenu.Rows[0]["LangID"].ToString();
            //将这个菜谱所选的门店打钩
            MenuConnShopOperate menuConnShopOperate = new MenuConnShopOperate();
            DataTable dtMenuShop = menuConnShopOperate.QueryShopsByMenuId(menuId);
            for (int i = 0; i < dtMenuShop.Rows.Count; i++)
            {
                int shopIdMenu = Common.ToInt32(dtMenuShop.Rows[i]["shopId"]);
                for (int j = 0; j < GridView_Shop.Rows.Count; j++)
                {
                    int shopId = Common.ToInt32(GridView_Shop.DataKeys[j]["shopId"]);
                    if (shopIdMenu == shopId)
                    {
                        CheckBox CheckBox_Shop = GridView_Shop.Rows[j].FindControl("CheckBox_Shop") as CheckBox;//2013-7-22
                        // CheckBox CheckBox_Shop = GridView_Shop.Rows[i].FindControl("CheckBox_Shop") as CheckBox;源代码,bug
                        CheckBox_Shop.Checked = true;
                    }
                }
            }
        }
    }
    /// <summary>
    /// 写入菜单信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Update_Click(object sender, EventArgs e)
    {
        MenuOperate mo = new MenuOperate();
        VAMenu vAMenu = new VAMenu();
        vAMenu.menuDesc = TextBox_MenuDesc.Text;
        vAMenu.menuName = TextBox_MenuName.Text;
        vAMenu.langID = Common.ToInt32(DropDownList_LangID.SelectedValue);
        vAMenu.menuSequence = Common.ToInt32(TextBox_MenuSequence.Text);
        vAMenu.menuI18nID = Common.ToInt32(HiddenField_MenuI18nID.Value);
        vAMenu.menuID = Common.ToInt32(HiddenField_MenuID.Value);
        using (TransactionScope scope = new TransactionScope())
        {
            bool resultTag = mo.ModifyMenu(vAMenu);
            if (resultTag == true)
            {
                MenuConnShopOperate menuConnShopOperate = new MenuConnShopOperate();
                menuConnShopOperate.RemoveMenuConnShop(vAMenu.menuID);
                int returnTag = 0;
                int count = 0;
                for (int i = 0; i < GridView_Shop.Rows.Count; i++)
                {
                    CheckBox cd = (CheckBox)GridView_Shop.Rows[i].FindControl("CheckBox_shop");
                    if (cd.Checked == true)
                    {
                        MenuConnShop menuConnShop = new MenuConnShop();
                        menuConnShop.menuId = Common.ToInt32(HiddenField_MenuID.Value);
                        menuConnShop.shopId = Common.ToInt32(GridView_Shop.DataKeys[i].Values["shopId"]);
                        menuConnShop.companyId = Common.ToInt32(DropDownList_Company.SelectedValue);
                        count++;
                        if (menuConnShopOperate.AddMenuShop(menuConnShop) > 0)
                        {
                            returnTag++;
                        }
                    }
                }
                if (returnTag == count)
                {
                    scope.Complete();
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改成功！')</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败！')</script>");
            }
        }
    }
    protected void Button_Cancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("MenuList.aspx");
    }
}