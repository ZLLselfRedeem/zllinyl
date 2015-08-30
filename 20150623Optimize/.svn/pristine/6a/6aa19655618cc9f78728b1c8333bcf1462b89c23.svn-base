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

public partial class MenuManage_MenuAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SqlInject myCheck = new SqlInject(this.Request);
        myCheck.CheckAllPageInject();
        text.Value = Common.ToString(Request.QueryString["name"]);//回传页面显示公司名称
        if (!IsPostBack)
        {
            init_date.InnerHtml = "";
            QueryShop();
            GetLang();
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
    /// <summary>
    /// 获取所有门店
    /// </summary>
    protected void QueryShop()
    {
        if (Session["UserInfo"] != null)
        {
            EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
            List<VAEmployeeShop> employeeShop = new List<VAEmployeeShop>();
            VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
            int employeeID = vAEmployeeLoginResponse.employeeID;
            int companyID = Common.ToInt32(Request.QueryString["id"]);
            employeeShop = employeeConnShopOperate.QueryEmployeeShopByCompanyAndEmplyee(employeeID, companyID);
            GridView_Shop.DataSource = employeeShop;
            GridView_Shop.DataBind();
            if (GridView_Shop.Rows.Count > 0)
            {
                CheckBox CheckBox_Shop = GridView_Shop.Rows[0].FindControl("CheckBox_Shop") as CheckBox;
                CheckBox_Shop.Checked = true;
            }
        }
    }

    protected void DropDownList_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        QueryShop();
    }
    /// <summary>
    /// 写入菜单信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Add_Click(object sender, EventArgs e)
    {
        // string imagePath = @"../" + ConfigurationManager.AppSettings["ImagePath"].ToString();
        VAMenu vAMenu = new VAMenu();
        vAMenu.menuDesc = TextBox_MenuDesc.Text;
        vAMenu.menuName = TextBox_MenuName.Text;
        vAMenu.langID = Common.ToInt32(DropDownList_LangID.SelectedValue);
        vAMenu.menuSequence = Common.ToInt32(TextBox_MenuSequence.Text);
        List<VAEmployeeShop> listEmployeeShop = new List<VAEmployeeShop>();
        for (int i = 0; i < GridView_Shop.Rows.Count; i++)
        {
            //菜谱的默认路径是：公司路径+第一个店铺路径+菜谱名+时间戳
            CheckBox CheckBox_Shop = GridView_Shop.Rows[i].FindControl("CheckBox_Shop") as CheckBox;
            if (CheckBox_Shop.Checked)
            {
                VAEmployeeShop vAEmployeeShop = new VAEmployeeShop();
                vAEmployeeShop.shopID = Common.ToInt32(GridView_Shop.DataKeys[i].Values["shopID"]);
                vAEmployeeShop.shopImagePath = GridView_Shop.DataKeys[i].Values["shopImagePath"].ToString();
                vAEmployeeShop.shopName = GridView_Shop.DataKeys[i].Values["shopName"].ToString();
                listEmployeeShop.Add(vAEmployeeShop);
            }
        }
        if (listEmployeeShop.Count > 0)
        {//菜谱图片路径
            vAMenu.menuImagePath = listEmployeeShop[0].shopImagePath + ChineseCharacterToPinyin.CharacterToPinyin.GetAllPYLetters(Common.ToClearSpecialCharString(vAMenu.menuName)) + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "/";
            // Common.FolderCreate(Server.MapPath(imagePath + vAMenu.menuImagePath));OOS自动生成文件夹
            List<MenuConnShop> listMenuConnShop = new List<MenuConnShop>();
            for (int i = 0; i < listEmployeeShop.Count; i++)
            {
                MenuConnShop menuConnShop = new MenuConnShop();
                menuConnShop.menuId = 0;
                menuConnShop.shopId = listEmployeeShop[i].shopID;
                menuConnShop.companyId = Common.ToInt32(Request.QueryString["id"]);
                listMenuConnShop.Add(menuConnShop);
            }
            MenuOperate menuOperate = new MenuOperate();
            FunctionResult functionResult = menuOperate.AddMenuAndMenuShop(vAMenu, listMenuConnShop);
            if (functionResult.returnResult > 0)
            {
                int companyId = Common.ToInt32(Request.QueryString["id"]);
                int menuId = functionResult.returnResult;
                CompanyOperate companyOperate = new CompanyOperate();
                if (companyOperate.SetCompanyDefaultMenu(companyId, menuId))
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('添加成功');</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('设置默认菜谱失败');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + functionResult.message + "');</script>");
            }
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请选择店铺！')</script>");
        }
    }
}