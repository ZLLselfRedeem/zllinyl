using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using Web.Control.DDL;
using VAGastronomistMobileApp.Model;
using Web.Control;

public partial class StatisticalStatement_ConsumptionWeekStatistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(DropDownList_City);
            TextBox_preOrderTime.Text = Common.ToString(DateTime.Now.ToString("yyyy/MM/dd"));
            GetCompanyInfo();
            GetShopInfo();
        }
    }
    /// <summary>
    /// 获取公司信息
    /// </summary>
    protected void GetCompanyInfo()
    {
        CompanyOperate operate = new CompanyOperate();
        int companyId = operate.GetCompanyId(Common.ToInt32(Request.QueryString["id"]));
        List<CompanyViewModel> employeeCompany = new EmployeeConnShopOperate().QueryEmployeeCompany(SessionHelper.GetCurrectSessionEmployeeId(), Common.ToInt32(DropDownList_City.SelectedValue));
        DropDownList_Company.DataSource = employeeCompany;
        DropDownList_Company.DataTextField = "CompanyName";
        DropDownList_Company.DataValueField = "CompanyID";
        DropDownList_Company.DataBind();
        DropDownList_Company.Items.Add(new ListItem("所有公司", "0"));
        DropDownList_Company.SelectedValue = companyId.ToString();//默认选择所有公司
    }
    /// <summary>
    /// 获取门店信息
    /// </summary>
    protected void GetShopInfo()
    {
        if (DropDownList_Company.Items.Count <= 0)
        {
            return;
        }
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
        DropDownList_Shop.SelectedValue = "0";//默认选择所有门店
        BindConsumptionWeekStatistics();
        BindGridView_All();
    }
    /// <summary>
    /// 选择公司
    /// </summary>
    protected void DropDownList_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetShopInfo();//动态选择门店
        BindConsumptionWeekStatistics();
    }
    /// <summary>
    /// 绑定显示GridView_ConsumptionWeekStatistics
    /// </summary>
    public void BindConsumptionWeekStatistics()
    {
        if (TextBox_preOrderTime.Text != "")
        {
            Label_Message.Text = "";
            StatisticalStatementOperate statisticalStatementOperate = new StatisticalStatementOperate();
            int companyId = Common.ToInt32(DropDownList_Company.SelectedValue);
            int shopId = Common.ToInt32(DropDownList_Shop.SelectedValue);
            DateTime dTime = Common.ToDateTime(TextBox_preOrderTime.Text);
            DataTable dtable = statisticalStatementOperate.QueryConsumptionWeekStatistics(companyId, shopId, dTime);
            if (dtable.Rows.Count > 0)
            {
                Label_massage.Text = "";
                GridView_ConsumptionWeekStatistics.DataSource = dtable;
            }
            else
            {
                Label_massage.Text = "暂无消息";
            }
        }
        else
        {
            Label_Message.Text = "统计时间不能为空";
        }
        GridView_ConsumptionWeekStatistics.DataBind();
    }
    public void BindGridView_All()
    {
        StatisticalStatementOperate statisticalStatementOperate = new StatisticalStatementOperate();
        int companyId = Common.ToInt32(DropDownList_Company.SelectedValue);
        int shopId = Common.ToInt32(DropDownList_Shop.SelectedValue);
        DataTable dtable = statisticalStatementOperate.QueryAllConsumptionWeekStatistics(companyId, shopId);
        if (dtable.Rows.Count > 0)
        {
            GridView_All.DataSource = dtable;
            Label1.Text = "";
        }
        else
        {
            Label1.Text = "暂无消息";
        }
        GridView_All.DataBind();
    }
    /// <summary>
    /// 选择日期改变
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TextBox_preOrderTime_TextChanged(object sender, EventArgs e)
    {
        BindConsumptionWeekStatistics();
    }
    protected void DropDownList_Shop_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindConsumptionWeekStatistics();
        BindGridView_All();
    }
    protected void DropDownList_City_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCompanyInfo();
        BindConsumptionWeekStatistics();
        BindGridView_All();
    }
}