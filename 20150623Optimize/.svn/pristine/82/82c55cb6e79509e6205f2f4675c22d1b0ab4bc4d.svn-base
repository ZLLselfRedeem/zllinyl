using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Text;
using System.Data;
using VAGastronomistMobileApp.DBUtility;
using System.Reflection;
using System.Collections;
using System.IO;

public partial class OtherStatisticalStatement_OrderStatistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Button_LargePageCount_Click(Button_10, null);//分页选中10
            Button_Change_Click(Button_Change, null);//默认选中订单量统计按钮
            QueryCompany();
            QueryShop();
            Button_allDay_Click(Button_allDay, null);//默认选中时间段为全天
            Button_day_Click(Button_1day, null);//默认选中今天
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
    /// 绑定显示数据表信息
    /// </summary>
    protected void ShowGridView_OrderStatisticsInfo(int str, int end)
    {
        AspNetPager1.PageSize = Common.ToInt32(Label_LargePageCount.Text);
        int orderStatus = 0;//获取页面radiobutton选中情况
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
        if (RadioButton_AllOrder.Checked == true)//所有订单选中
        {
            orderStatus = 1;
        }
        else//已验证订单选中
        {
            orderStatus = 2;
        }
        // List<StatisticalStatementOperate.OrderStatisticsInfo> list = new List<StatisticalStatementOperate.OrderStatisticsInfo>();
        int companyId = Common.ToInt32(DropDownList_Company.SelectedValue);//默认这个值为0
        int shopId = Common.ToInt32(DropDownList_Shop.SelectedValue);
        // list = statisticalStatementOperate.GetOrderStatisticsInfo(timeFrom, timeTo, companyId, HiddenField_DinnerStarTime.Value, HiddenField_DinnerEndTime.Value, orderStatus);
        DataTable dt = statisticalStatementOperate.GetOrderStatisticsInfo(timeFrom, timeTo, companyId, shopId, 0, HiddenField_DinnerStarTime.Value, HiddenField_DinnerEndTime.Value, orderStatus);
        #region 总计
        Label_preOrderServerSumSum.Text = Common.ToDouble(dt.Compute("sum(orderAmount)", "1=1")).ToString("0.00");
        Label_OrderCount.Text = dt.Compute("sum(orderNumber)", "1=1").ToString();
        Label_payOrderCount.Text = dt.Compute("sum(payOrderCount)", "1=1").ToString();
        Label_prePaidSumSum.Text = Common.ToDouble(dt.Compute("sum(payOrderAmount)", "1=1")).ToString("0.00");
        Label_refundAmount.Text = Common.ToDouble(dt.Compute("sum(refundOrderAmount)", "1=1")).ToString("0.00");
        Label_refundCount.Text = dt.Compute("sum(refundOrderCount)", "1=1").ToString();
        #endregion
        if (dt.Rows.Count > 0)
        {
            Label_massage.Text = "";
            // DataTable listToDataTable = statisticalStatementOperate.ToDataTable(list);
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
    }
    /// <summary>
    /// 分页显示
    /// </summary>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        ShowGridView_OrderStatisticsInfo(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
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
                TextBox_preOrderTimeStr.Text = "";
                TextBox_preOrderTimeEnd.Text = "";
                break;
            default:
                break;
        }
        SelectFunction();
    }
    /// <summary>
    /// 自定义选择时间
    /// </summary>
    protected void TextBox_preOrderTimeStr_TextChanged(object sender, EventArgs e)
    {
        ShowGridView_OrderStatisticsInfo(0, 10);
        Button_1day.CssClass = "tabButtonBlueUnClick";//今天
        Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
        Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
        Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
        Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
        Button_self.CssClass = "tabButtonBlueClick";//自定义
    }
    /// <summary>
    /// 根据上线的公司查询当前的信息
    /// </summary>
    protected void DropDownList_Company_SelectedIndexChanged(object sender, EventArgs e)
    {
        //默认进来选中的是所有门店
        int companyId = Common.ToInt32(DropDownList_Company.SelectedValue);
        ShowGridView_OrderStatisticsInfo(0, 10);
        QueryShop();
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
                TextBox_hourTimerStr.Text = "00:00:00";
                HiddenField_DinnerEndTime.Value = "23:59:59";//结束时间
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
                //  Button_customSettings.CssClass = "tabButtonBlueUnClick";//自定义查询
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
                //  Button_customSettings.CssClass = "tabButtonBlueUnClick";//自定义查询
                break;
            case "customSettings":
                Button_allDay.CssClass = "tabButtonBlueUnClick";//全天
                Button_afternoon_soon.CssClass = "tabButtonBlueUnClick";//午市(10时~14时)
                Button_afternoon.CssClass = "tabButtonBlueUnClick";//下午(14~17时)
                Button_night_soon.CssClass = "tabButtonBlueUnClick";//晚市(16时~22时) 
                Button_night.CssClass = "tabButtonBlueUnClick";//夜市(22时~4时)
                //  Button_customSettings.CssClass = "tabButtonBlueClick";//自定义查询
                break;
            default:
                break;
        }
        SelectFunction();
    }
    /// <summary>
    /// 绑定显示订单量时段明细
    /// </summary>
    protected void ShowGridView_othergridviewInfo()
    {
        int orderStatus = 0;//获取页面radiobutton选中情况
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
        if (RadioButton_AllOrder.Checked == true)//所有订单选中
        {
            orderStatus = 1;
        }
        else//已验证订单选中
        {
            orderStatus = 2;
        }
        int company = Common.ToInt32(DropDownList_Company.SelectedValue);
        int shopId = Common.ToInt32(DropDownList_Shop.SelectedValue);
        string strHour = HiddenField_DinnerStarTime.Value;
        string endHour = HiddenField_DinnerEndTime.Value;
        string condition = "Order";//表示查询的是普通订单的详细订单信息
        StatisticalStatementOperate statisticalStatementOperate = new StatisticalStatementOperate();
        DataTable dt = statisticalStatementOperate.QueryOrderDetailStatisticsInfo(strTime, endTime, company, shopId, strHour, endHour, condition, orderStatus);
        if (dt.Rows.Count == 0)//表示无返回信息
        {
            Label_errorMessage.Text = "暂无符合条件的数据";
        }
        else
        {
            Label_errorMessage.Text = "";
        }
        GridView_othergridview.DataSource = dt;//没有数据显示为空
        GridView_othergridview.DataBind();
    }
    /// <summary>
    /// 切换查看按钮
    /// </summary>
    protected void Button_Change_Click(object sender, EventArgs e)
    {
        Button_ChangeDetail.CssClass = "tabButtonBlueUnClick";
        Button_Change.CssClass = "couponButtonSubmit";
        Panel_OrderStatistics.Visible = true;
        Panel_othergridview.Visible = false;
        SelectFunction();
    }
    protected void Button_ChangeDetail_Click(object sender, EventArgs e)
    {
        Button_ChangeDetail.CssClass = "couponButtonSubmit";
        Button_Change.CssClass = "tabButtonBlueUnClick";
        Panel_OrderStatistics.Visible = false;
        Panel_othergridview.Visible = true;
        SelectFunction();
    }
    /// <summary>
    /// 订单类型改变事件
    /// </summary>
    protected void RadioButton_AllOrder_CheckedChanged(object sender, EventArgs e)
    {
        SelectFunction();
    }
    /// <summary>
    /// 查询时间变化
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
        //限定4个时间不能为空
        if (TextBox_preOrderTimeStr.Text != "" && TextBox_preOrderTimeEnd.Text != "" && TextBox_hourTimerStr.Text != "" && TextBox_hourTimerEnd.Text != "")
        {
            if (Panel_OrderStatistics.Visible == true)
            {
                ShowGridView_OrderStatisticsInfo(0, Common.ToInt32(Label_LargePageCount.Text));
            }
            else
            {
                ShowGridView_othergridviewInfo();
            }
        }
    }
    protected void DropDownList_Shop_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectFunction();
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
        SelectFunction();
    }
}