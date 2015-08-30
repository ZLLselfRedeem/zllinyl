using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;

public partial class OtherStatisticalStatement_PayAmountStatistics : System.Web.UI.Page
{
    /// <summary>
    /// 初始化加载信息
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Button_LargePageCount_Click(Button_10, null);//分页选中10
            QueryCompany();
            QueryShop();
            Button_Pay_Click(Button_Pay, null);
            Button_day_Click(Button_1day, null);//默认选中今天
            Button_allDay_Click(Button_allDay, null);//默认选中时间段为全天
        }
    }
    /// <summary>
    /// 获取所有上线公司
    /// </summary>
    protected void QueryCompany()
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
        DropDownList_Company.Items.Add(new ListItem("所有公司", "0"));
        DropDownList_Company.SelectedValue = "0";//默认选择所有公司
    }
    /// <summary>
    /// 选择统计日期，添加样式
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
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
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
                break;
            default:
                break;
        }
        SelectFunction();
    }
    /// <summary>
    /// 根据时间段查询
    /// </summary>
    protected void Button_allDay_Click(object sender, EventArgs e)
    {
        //设置点击各个按钮的样式和事件
        Button Button = (Button)sender;
        switch (Button.CommandName)
        {
            case "allDay"://
                Button_allDay.CssClass = "tabButtonBlueClick";//全天
                HiddenField_DinnerStarTime.Value = "00:00:00";//开始时间
                HiddenField_DinnerEndTime.Value = "23:59:59";//结束时间
                TextBox_hourTimerStr.Text = "00:00:00";
                TextBox_hourTimerEnd.Text = "23:59:59";
                Button_afternoon_soon.CssClass = "tabButtonBlueUnClick";//午市(10时~14时)
                Button_afternoon.CssClass = "tabButtonBlueUnClick";//下午(14~17时)
                Button_night_soon.CssClass = "tabButtonBlueUnClick";//晚市(16时~22时) 
                Button_night.CssClass = "tabButtonBlueUnClick";//夜市(22时~4时)
                // Button_customSettings.CssClass = "tabButtonBlueUnClick";//自定义查询
                break;
            case "afternoon_soon":
                Button_allDay.CssClass = "tabButtonBlueUnClick";//全天
                Button_afternoon_soon.CssClass = "tabButtonBlueClick";//午市(10时~14时)
                HiddenField_DinnerStarTime.Value = "10:00:00";//开始时间
                HiddenField_DinnerEndTime.Value = "13:59:59";//结束时间
                TextBox_hourTimerStr.Text = "10:00:00";
                TextBox_hourTimerEnd.Text = "13:59:59";
                Button_afternoon.CssClass = "tabButtonBlueUnClick";//下午(14~17时)
                Button_night_soon.CssClass = "tabButtonBlueUnClick";//晚市(16时~22时) 
                Button_night.CssClass = "tabButtonBlueUnClick";//夜市(22时~4时)
                // Button_customSettings.CssClass = "tabButtonBlueUnClick";//自定义查询
                break;
            case "afternoon":
                Button_allDay.CssClass = "tabButtonBlueUnClick";//全天
                Button_afternoon_soon.CssClass = "tabButtonBlueUnClick";//午市(10时~14时)
                Button_afternoon.CssClass = "tabButtonBlueClick";//下午(14~17时)
                HiddenField_DinnerStarTime.Value = "14:00:00";//开始时间
                HiddenField_DinnerEndTime.Value = "16:59:59";//结束时间
                TextBox_hourTimerStr.Text = "14:00:00";
                TextBox_hourTimerEnd.Text = "16:59:59";
                Button_night_soon.CssClass = "tabButtonBlueUnClick";//晚市(16时~22时) 
                Button_night.CssClass = "tabButtonBlueUnClick";//夜市(22时~4时)
                // Button_customSettings.CssClass = "tabButtonBlueUnClick";//自定义查询
                break;
            case "night_soon":
                Button_allDay.CssClass = "tabButtonBlueUnClick";//全天
                Button_afternoon_soon.CssClass = "tabButtonBlueUnClick";//午市(10时~14时)
                Button_afternoon.CssClass = "tabButtonBlueUnClick";//下午(14~17时)
                Button_night_soon.CssClass = "tabButtonBlueClick";//晚市(16时~22时) 
                HiddenField_DinnerStarTime.Value = "16:00:00";//开始时间
                HiddenField_DinnerEndTime.Value = "21:59:59";//结束时间
                TextBox_hourTimerStr.Text = "16:00:00";
                TextBox_hourTimerEnd.Text = "21:59:59";
                Button_night.CssClass = "tabButtonBlueUnClick";//夜市(22时~4时)
                // Button_customSettings.CssClass = "tabButtonBlueUnClick";//自定义查询
                break;
            case "night":
                Button_allDay.CssClass = "tabButtonBlueUnClick";//全天
                Button_afternoon_soon.CssClass = "tabButtonBlueUnClick";//午市(10时~14时)
                Button_afternoon.CssClass = "tabButtonBlueUnClick";//下午(14~17时)
                Button_night_soon.CssClass = "tabButtonBlueUnClick";//晚市(16时~22时) 
                Button_night.CssClass = "tabButtonBlueClick";//夜市(22时~4时)
                HiddenField_DinnerStarTime.Value = "22:00:00";//开始时间
                HiddenField_DinnerEndTime.Value = "03:59:59";//结束时间，这个时间要切换到第二天
                TextBox_hourTimerStr.Text = "22:00:00";
                TextBox_hourTimerEnd.Text = "03:59:59";
                //TextBox_preOrderTimeEnd.Text = Common.ToDateTime(TextBox_preOrderTimeEnd.Text).AddDays(1).ToString("yyyy-MM-dd");
                // Button_customSettings.CssClass = "tabButtonBlueUnClick";//自定义查询
                break;
            case "customSettings":
                Button_allDay.CssClass = "tabButtonBlueUnClick";//全天
                Button_afternoon_soon.CssClass = "tabButtonBlueUnClick";//午市(10时~14时)
                Button_afternoon.CssClass = "tabButtonBlueUnClick";//下午(14~17时)
                Button_night_soon.CssClass = "tabButtonBlueUnClick";//晚市(16时~22时) 
                Button_night.CssClass = "tabButtonBlueUnClick";//夜市(22时~4时)
                // Button_customSettings.CssClass = "tabButtonBlueClick";//自定义查询
                break;
            default:
                break;
        }
        SelectFunction();
    }
    /// <summary>
    /// 切换按钮开关查看信息
    /// </summary>
    protected void Button_Pay_Click(object sender, EventArgs e)
    {
        Button_PayDetail.CssClass = "tabButtonBlueUnClick";
        Button_Pay.CssClass = "couponButtonSubmit";
        Panel_PayStatistics.Visible = true;
        Panel_PayDetailGridview.Visible = false;
        SelectFunction();
    }
    protected void Button_PayDetail_Click(object sender, EventArgs e)
    {
        Button_PayDetail.CssClass = "couponButtonSubmit";
        Button_Pay.CssClass = "tabButtonBlueUnClick";
        Panel_PayStatistics.Visible = false;
        Panel_PayDetailGridview.Visible = true;
        SelectFunction();
    }
    /// <summary>
    /// 绑定显示数据表信息
    /// </summary>
    protected void ShowGridView_PayOrderStatisticsInfo(int str, int end)
    {
        AspNetPager1.PageSize = Common.ToInt32(Label_LargePageCount.Text);
        StatisticalStatementOperate statisticalStatementOperate = new StatisticalStatementOperate();
        DateTime timeFrom = Common.ToDateTime(TextBox_preOrderTimeStr.Text + " 00:00:00");
        DateTime timeTo = Common.ToDateTime(TextBox_preOrderTimeEnd.Text + " 23:59:59");
        //当选择为夜市
        if (Button_night.CssClass == "tabButtonBlueClick")//
        {
            timeTo = Common.ToDateTime(Common.ToDateTime(TextBox_preOrderTimeEnd.Text).AddDays(1).ToString("yyyy-MM-dd") + " 00:00:00");
        }
        else
        {
            timeTo = Common.ToDateTime(Common.ToDateTime(TextBox_preOrderTimeEnd.Text).ToString("yyyy-MM-dd") + " 23:59:59");
        }
        //List<StatisticalStatementOperate.PayAmountStatisticsInfo> list = new List<StatisticalStatementOperate.PayAmountStatisticsInfo>();
        int companyId = Common.ToInt32(DropDownList_Company.SelectedValue);//默认这个值为0
        int shopId = Common.ToInt32(DropDownList_Shop.SelectedValue);
        DataTable dt = statisticalStatementOperate.GetPayOrderStatisticsInfo(timeFrom, timeTo, companyId, shopId,0, HiddenField_DinnerStarTime.Value, HiddenField_DinnerEndTime.Value);
        //list = statisticalStatementOperate.GetPayOrderStatisticsInfo(timeFrom, timeTo, companyId, HiddenField_DinnerStarTime.Value, HiddenField_DinnerEndTime.Value);
        Label_OrderCount.Text = Common.ToInt32(dt.Compute("sum(orderNumber)", "1=1")).ToString();
        Label_prePaidSumSum.Text = Common.ToDouble(dt.Compute("sum(payOrderAmount)", "1=1")).ToString("0.00");
        Label_payOrderCount.Text = Common.ToInt32(dt.Compute("sum(payOrderNumber)", "1=1")).ToString();
        Label_refundCount.Text = Common.ToInt32(dt.Compute("sum(refundOrderCount)", "1=1")).ToString();
        Label_refundAmount.Text = Common.ToDouble(dt.Compute("sum(refundOrderAmount)", "1=1")).ToString("0.00");
        if (dt.Rows.Count > 0)
        {
            Label_massage.Text = "";
            // DataTable listToDataTable = statisticalStatementOperate.ToDataTable(list);
            int tableCount = dt.Rows.Count;//
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
            GridView_PayStatistics.DataSource = dt_page;
        }
        else
        {
            Label_massage.Text = "暂无符合条件的数据";
            GridView_PayStatistics.DataSource = dt;//绑定显示的是空数据，目的为清空显示
        }
        GridView_PayStatistics.DataBind();
    }
    /// <summary>
    /// 根据公司查询订单信息
    /// </summary>
    protected void DropDownList_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        //默认进来选中的是所有门店
        int companyId = Common.ToInt32(DropDownList_Company.SelectedValue);
        QueryShop();
        SelectFunction();
    }
    /// <summary>
    /// 显示根据时间段查询的信息
    /// </summary>
    protected void ShowGridView_PayDetailInfo()
    {
        DateTime strTime = Common.ToDateTime(TextBox_preOrderTimeStr.Text + " 00:00:00");
        DateTime endTime = Common.ToDateTime(TextBox_preOrderTimeEnd.Text + " 23:59:59");
        //当选择为夜市
        if (Button_night.CssClass == "tabButtonBlueClick")//
        {
            endTime = Common.ToDateTime(Common.ToDateTime(TextBox_preOrderTimeEnd.Text).AddDays(1).ToString("yyyy-MM-dd") + " 00:00:00");
        }
        else
        {
            endTime = Common.ToDateTime(Common.ToDateTime(TextBox_preOrderTimeEnd.Text).ToString("yyyy-MM-dd") + " 23:59:59");
        }
        int company = Common.ToInt32(DropDownList_Company.SelectedValue);
        int shopId = Common.ToInt32(DropDownList_Shop.SelectedValue);
        string strHour = HiddenField_DinnerStarTime.Value;
        string endHour = HiddenField_DinnerEndTime.Value;
        string condition = "PayOrder";//表示查询的是已支付的详细订单信息
        StatisticalStatementOperate statisticalStatementOperate = new StatisticalStatementOperate();
        DataTable dt = statisticalStatementOperate.QueryOrderDetailStatisticsInfo(strTime, endTime, company, shopId, strHour, endHour, condition, 0);
        if (dt.Rows.Count == 0)
        {
            Label_errorMessage.Text = "暂无符合条件的数据";
        }
        else
        {
            Label_errorMessage.Text = "";
        }
        GridView_PayDetailGridView.DataSource = dt;//
        GridView_PayDetailGridView.DataBind();
    }
    /// <summary>
    /// 自定义选择时间日期
    /// </summary>
    protected void TextBox_preOrderTimeStr_TextChanged(object sender, EventArgs e)
    {
        SelectFunction();
        Button_1day.CssClass = "tabButtonBlueUnClick";//今天
        Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
        Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
        Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
        Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
        Button_self.CssClass = "tabButtonBlueClick";//自定义
    }
    /// <summary>
    /// 分页操作
    /// </summary>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        ShowGridView_PayOrderStatisticsInfo(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 查询时间段
    /// </summary>
    protected void TextBox_hourTimerStr_TextChanged(object sender, EventArgs e)
    {
        HiddenField_DinnerStarTime.Value = TextBox_hourTimerStr.Text;
        HiddenField_DinnerEndTime.Value = TextBox_hourTimerEnd.Text;
        SelectFunction();
    }
    /// <summary>
    ///触发查询显示信息方法
    /// </summary>
    protected void SelectFunction()
    {
        if (TextBox_preOrderTimeStr.Text != "" && TextBox_preOrderTimeEnd.Text != "" && TextBox_hourTimerStr.Text != "" && TextBox_hourTimerEnd.Text != "")
        {
            if (Panel_PayStatistics.Visible == true)
            {
                ShowGridView_PayOrderStatisticsInfo(0, Common.ToInt32(Label_LargePageCount.Text));
            }
            else
            {
                ShowGridView_PayDetailInfo();
            }
        }
    }
    protected void QueryShop()
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
        DropDownList_Shop.SelectedValue = "0";//选中所有门店
        SelectFunction();
    }
    protected void DropDownList_Shop_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectFunction();
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
        }
        SelectFunction();
    }
}