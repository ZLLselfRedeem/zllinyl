using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;

public partial class OtherStatisticalStatement_ConsumptionWeekStatistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
        if (Session["UserInfo"] != null)
        {
            EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
            List<VAEmployeeCompany> employeeCompany = new List<VAEmployeeCompany>();
            VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
            int employeeID = vAEmployeeLoginResponse.employeeID;
            employeeCompany = employeeConnShopOperate.QueryEmployeeCompany(employeeID);//查询所有的上线的门店
            DropDownList_Company.DataSource = employeeCompany;
            DropDownList_Company.DataTextField = "companyName";
            DropDownList_Company.DataValueField = "companyID";
            DropDownList_Company.DataBind();
            DropDownList_Company.Items.Add(new ListItem("所有公司", "0"));
            DropDownList_Company.SelectedValue = "0";
        }
    }
    /// <summary>
    /// 获取门店信息
    /// </summary>
    protected void GetShopInfo()
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
            //if (DropDownList_Company.SelectedValue == "0")//表示选择的是所有公司
            //{
            //    if (DropDownList_Shop.SelectedValue == "0" || DropDownList_Shop.SelectedItem.Text == "")//表示选择所有门店
            //    {
            //    }
            //    else
            //    {
            //        //这种情况不会出现
            //    }
            //}
            //else//表示选择了某家具体的公司
            //{
            //    if (DropDownList_Shop.SelectedValue == "0" || DropDownList_Shop.SelectedItem.Text == "")//表示选择所有门店
            //    {

            //    }
            //    else//表示选择了某家具体门店
            //    {
            //        //根据公司和门店插叙对应的信息
            //    }
            //}
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
}