using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using Web.Control;
using Web.Control.DDL;

public partial class StatisticalStatement_dataStatistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(DropDownList_City);
            QueryCompany();
            QueryShop();
            if (!string.IsNullOrEmpty(Request.QueryString["str"]) && !string.IsNullOrEmpty(Request.QueryString["end"]))
            {
                Button_day_Click(Button_self, null);//分页选中自定义
            }
            else
            {
                Button_day_Click(Button_1day, null);//默认选中今天
            }
            Button_LargePageCount_Click(Button_10, null);//分页选中10
        }
    }
    /// <summary>
    /// 获取所有上线公司
    /// </summary>
    protected void QueryCompany()
    {
        CompanyOperate operate = new CompanyOperate();
        int companyId = operate.GetCompanyId(Common.ToInt32(Request.QueryString["id"]));
        List<CompanyViewModel> employeeCompany = new EmployeeConnShopOperate().QueryEmployeeCompany(SessionHelper.GetCurrectSessionEmployeeId(), Common.ToInt32(DropDownList_City.SelectedValue));
        DropDownList_Company.DataSource = employeeCompany;
        DropDownList_Company.DataTextField = "CompanyName";
        DropDownList_Company.DataValueField = "CompanyID";
        DropDownList_Company.DataBind();
        DropDownList_Company.Items.Add(new ListItem("所有公司", "0"));
        if (employeeCompany.Any(c => c.companyID == companyId))
        {
            DropDownList_Company.SelectedValue = companyId.ToString();
        }
        else
        {
            DropDownList_Company.SelectedValue = "0";//默认选择所有公司
        }
    }
    protected void QueryShop(bool flag = false)
    {
        if (DropDownList_Company.Items.Count <= 0)
        {
            return;
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
            if (flag == false)//店家下拉列表刷新页面信息，不再获取地址栏参数
            {
                DropDownList_Shop.SelectedValue = Common.ToString(Request.QueryString["id"]);//选中所有门店
            }
            else
            {
                DropDownList_Shop.SelectedValue = "0";//选中所有门店
            }
        }
    }
    /// <summary>
    /// 选择统计日期
    /// </summary>
    protected void Button_day_Click(object sender, EventArgs e)
    {
        TextBox_preOrderTimeStr.Text = "";
        TextBox_preOrderTimeEnd.Text = "";
        Button Button = (Button)sender;
        switch (Button.CommandName)
        {
            case "1"://
                Button_1day.CssClass = "tabButtonBlueClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                TextBox_preOrderTimeStr.Text = DateTime.Now.ToString("yyyy-MM-dd");//日期控件显示当前时间
                TextBox_preOrderTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "yesterday":
                Button_1day.CssClass = "tabButtonBlueUnClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueClick";//昨天
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_preOrderTimeStr.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                TextBox_preOrderTimeEnd.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                break;
            case "7":
                Button_1day.CssClass = "tabButtonBlueUnClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
                Button_7day.CssClass = "tabButtonBlueClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_preOrderTimeStr.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                TextBox_preOrderTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "14":
                Button_1day.CssClass = "tabButtonBlueUnClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_preOrderTimeStr.Text = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd");
                TextBox_preOrderTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "30":
                Button_1day.CssClass = "tabButtonBlueUnClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_preOrderTimeStr.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                TextBox_preOrderTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "self":
                Button_1day.CssClass = "tabButtonBlueUnClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                Button_self.CssClass = "tabButtonBlueClick";//自定义
                if (!string.IsNullOrEmpty(Request.QueryString["str"]) && !string.IsNullOrEmpty(Request.QueryString["end"]))
                {
                    TextBox_preOrderTimeStr.Text = Common.ToString(Request.QueryString["str"]);
                    TextBox_preOrderTimeEnd.Text = Common.ToString(Request.QueryString["end"]);
                }
                else
                {
                    TextBox_preOrderTimeStr.Text = "";
                    TextBox_preOrderTimeEnd.Text = "";
                }
                break;
            default:
                break;
        }
        CommonFunctuion();
    }
    //公司
    protected void DropDownList_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        QueryShop(true);
        CommonFunctuion();
    }
    //门店
    protected void DropDownList_Shop_SelectedIndexChanged(object sender, EventArgs e)
    {
        CommonFunctuion();
    }
    /// <summary>
    /// 绑定显示数据表信息
    /// </summary>
    protected void ShowGridView_OrderStatisticsInfo(int str, int end)
    {
        AspNetPager1.PageSize = Common.ToInt32(Label_LargePageCount.Text);
        StatisticalStatementOperate statisticalStatementOperate = new StatisticalStatementOperate();
        DateTime timeFrom = Common.ToDateTime(TextBox_preOrderTimeStr.Text + " 00:00:00");
        DateTime timeTo = Common.ToDateTime(TextBox_preOrderTimeEnd.Text + " 23:59:59");
        int companyId = Common.ToInt32(DropDownList_Company.SelectedValue);//默认这个值为0
        int shopId = Common.ToInt32(DropDownList_Shop.SelectedValue);
        int cityId = Common.ToInt32(DropDownList_City.SelectedValue);
        DataTable dt = statisticalStatementOperate.ComprehensiveStatisticalQuery(timeFrom, timeTo, companyId, shopId, cityId);
        DataView dv = dt.DefaultView;
        string sortedField = "";
        if (ViewState["SortedField"] != null)
        {
            Dictionary<string, string> sorted = (Dictionary<string, string>)ViewState["SortedField"];
            foreach (KeyValuePair<string, string> kvp in sorted)
            {
                sortedField = kvp.Key + "  " + kvp.Value;
            }
            dv.Sort = sortedField;
        }
        dt = dv.ToTable();
        #region 总计
        Label_preOrderServerSumSum.Text = Common.ToDouble(dt.Compute("sum(orderSumAmount)", "1=1")).ToString("0.00");
        Label_OrderCount.Text = Common.ToInt32(dt.Compute("sum(orderCount)", "1=1")).ToString();
        Label_payOrderCount.Text = Common.ToInt32(dt.Compute("sum(isPaidOrderCount)", "1=1")).ToString();
        Label_prePaidSumSum.Text = Common.ToDouble(dt.Compute("sum(isPaidOrderAmount)", "1=1")).ToString("0.00");
        Label_refundAmount.Text = Common.ToDouble(dt.Compute("sum(refundOrderAmount)", "1=1")).ToString("0.00");
        Label_refundCount.Text = Common.ToInt32(dt.Compute("sum(refundOrderCount)", "1=1")).ToString();
        #endregion
        if (dt.Rows.Count > 0)
        {
            Label_massage.Text = "";
            int tableCount = dt.Rows.Count;//
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
            GridView_OrderStatistics.DataSource = dt_page;
        }
        else
        {
            Label_massage.Text = "暂无符合条件的数据";
            GridView_OrderStatistics.DataSource = dt;//绑定显示的是空数据，目的为清空显示
        }
        GridView_OrderStatistics.DataBind();
        //datatable 中String无法正确排序
        //解决方案
        for (int i = 0; i < GridView_OrderStatistics.Rows.Count; i++)
        {
            Label Label_payRate = GridView_OrderStatistics.Rows[i].FindControl("Label_payRate") as Label;
            Label_payRate.Text = Common.ToDouble(GridView_OrderStatistics.DataKeys[i].Values["payRate"]) != 0 ? Common.ToDouble(GridView_OrderStatistics.DataKeys[i].Values["payRate"]) + "%" : "0";
            // Label Label_timersPayRate = GridView_OrderStatistics.Rows[i].FindControl("Label_timersPayRate") as Label;
            // Label_timersPayRate.Text = Common.ToDouble(GridView_OrderStatistics.DataKeys[i].Values["timersPayRate"]) != 0 ? Common.ToDouble(GridView_OrderStatistics.DataKeys[i].Values["timersPayRate"]) + "%" : "0";
            // Label Label_timersOrderRate = GridView_OrderStatistics.Rows[i].FindControl("Label_timersOrderRate") as Label;
            // Label_timersOrderRate.Text = Common.ToDouble(GridView_OrderStatistics.DataKeys[i].Values["timersOrderRate"]) != 0 ? Common.ToDouble(GridView_OrderStatistics.DataKeys[i].Values["timersOrderRate"]) + "%" : "0";
        }
    }
    //分页
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        ShowGridView_OrderStatisticsInfo(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    public void CommonFunctuion()
    {
        if (TextBox_preOrderTimeEnd.Text.Trim() != "" && TextBox_preOrderTimeStr.Text.Trim() != ""
            && !String.IsNullOrEmpty(Label_LargePageCount.Text))
        {
            if (CommonPageOperate.GetDiffDay(Common.ToDateTime(TextBox_preOrderTimeEnd.Text), Common.ToDateTime(TextBox_preOrderTimeStr.Text)) > 30)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('查询的时间范围不能超过30天，请重新选择自定义时间');</script>");
                return;
            }
            ShowGridView_OrderStatisticsInfo(0, Common.ToInt32(Label_LargePageCount.Text));
        }
    }
    //分页数目控制
    protected void Button_LargePageCount_Click(object sender, EventArgs e)
    {
        Button Button = (Button)sender;
        Label_LargePageCount.Text = "";
        switch (Button.CommandName)
        {
            case "button_10":
                Button_10.CssClass = "tabButtonBlueClick";
                Button_50.CssClass = "tabButtonBlueUnClick";
                Button_100.CssClass = "tabButtonBlueUnClick";
                Label_LargePageCount.Text = "10";
                break;
            case "button_50":
                Button_10.CssClass = "tabButtonBlueUnClick";
                Button_50.CssClass = "tabButtonBlueClick";
                Button_100.CssClass = "tabButtonBlueUnClick";
                Label_LargePageCount.Text = "50";
                break;
            case "button_100":
                Button_10.CssClass = "tabButtonBlueUnClick";
                Button_50.CssClass = "tabButtonBlueUnClick";
                Button_100.CssClass = "tabButtonBlueClick";
                Label_LargePageCount.Text = "100";
                break;
            default:
                break;
        }
        CommonFunctuion();
    }
    protected void GridView_OrderStatistics_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (string.IsNullOrEmpty(e.CommandName) || e.CommandName == "Sort")
        {
            return;
        }
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        string orderTime = Common.ToString(GridView_OrderStatistics.DataKeys[index].Values["orderTime"].ToString());
        switch (Common.ToString(e.CommandName))
        {
            case "Order":
                Response.Redirect("shopOrderListStatistics.aspx?d=" + orderTime
                                                            + "&s=" + Common.ToInt32(DropDownList_Shop.SelectedValue)
                                                            + "&c=" + Common.ToInt32(DropDownList_Company.SelectedValue)
                                                            + "&cityId=" + Common.ToInt32(DropDownList_City.SelectedValue));
                break;
            case "SecondProportion":
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>ConfirmWindow('Panel_Detail');</script>");
                StatisticalStatementOperate statisticalStatementOperate = new StatisticalStatementOperate();
                string timeFrom = orderTime + " 00:00:00";
                string timeTo = orderTime + " 23:59:59";
                int companyId = Common.ToInt32(DropDownList_Company.SelectedValue);//默认这个值为0
                int shopId = Common.ToInt32(DropDownList_Shop.SelectedValue);
                int cityId = Common.ToInt32(DropDownList_City.SelectedValue);
                DataTable dt = statisticalStatementOperate.ComprehensiveStatisticalQueryDetail(timeFrom, timeTo, companyId, shopId, cityId);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                break;
            default:
                break;
        }
    }
    protected void GridView_Sorting(object sender, GridViewSortEventArgs e)
    {
        Dictionary<string, string> sorted = new Dictionary<string, string>();
        if (ViewState["SortedField"] == null)
        {
            sorted.Add(e.SortExpression, "ASC");
            ViewState["SortedField"] = sorted;
        }
        else
        {
            sorted = (Dictionary<string, string>)ViewState["SortedField"];
            if (sorted.ContainsKey(e.SortExpression))
            {
                if (sorted[e.SortExpression] == "ASC")
                {
                    sorted[e.SortExpression] = "DESC";
                }
                else
                {
                    sorted[e.SortExpression] = "ASC";
                }
            }
            else
            {
                sorted.Clear();
                sorted.Add(e.SortExpression, "ASC");
                ViewState["SortedField"] = sorted;
            }
        }
        ShowGridView_OrderStatisticsInfo(0, Common.ToInt32(Label_LargePageCount.Text));
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        CommonFunctuion();
    }
    protected void DropDownList_City_SelectedIndexChanged(object sender, EventArgs e)
    {
        QueryCompany();
        if (TextBox_preOrderTimeEnd.Text.Trim() != "" && TextBox_preOrderTimeStr.Text.Trim() != "")
        {
            ShowGridView_OrderStatisticsInfo(0, Common.ToInt32(Label_LargePageCount.Text));
        }
    }
}