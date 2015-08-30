using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;

public partial class StatisticalStatement_UserAmountStatistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Button_Users_Click(Button_Users, null);//默认选中订单量统计按钮
            Button_day_Click(Button_1day, null);//默认选中今天
            CommonFunction();

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
        CommonFunction();
    }

    #region 切换两个按钮隐藏不同控件
    protected void Button_Users_Click(object sender, EventArgs e)
    {
        Button_UsersDetail.CssClass = "tabButtonBlueUnClick";
        Button_Users.CssClass = "couponButtonSubmit";
        Button_ActityUser.CssClass = "tabButtonBlueUnClick";
        Panel_DayUsersStatistics.Visible = true;
        Panel_UserAmountStatisticsDetail_Gridview.Visible = false;
        Panel_ActityUser.Visible = false;
        CommonFunction();
    }
    protected void Button_UsersDetail_Click(object sender, EventArgs e)
    {
        Button_UsersDetail.CssClass = "couponButtonSubmit";
        Button_Users.CssClass = "tabButtonBlueUnClick";
        Button_ActityUser.CssClass = "tabButtonBlueUnClick";
        Panel_DayUsersStatistics.Visible = false;
        Panel_UserAmountStatisticsDetail_Gridview.Visible = true;
        Panel_ActityUser.Visible = false;
        CommonFunction();
    }
    protected void Button_ActityUser_Click(object sender, EventArgs e)
    {
        Button_UsersDetail.CssClass = "tabButtonBlueUnClick";
        Button_Users.CssClass = "tabButtonBlueUnClick";
        Button_ActityUser.CssClass = "couponButtonSubmit";
        Panel_DayUsersStatistics.Visible = false;
        Panel_UserAmountStatisticsDetail_Gridview.Visible = false;
        Panel_ActityUser.Visible = true;
        CommonFunction();
    }
    #endregion

    #region 绑定GridView
    /// <summary>
    /// 绑定显示第一个GridView
    /// DateTime strTime, DateTime endTime, string strHour, string endHour
    /// </summary>
    protected void BindGridView_DayUsersStatistics(int str, int end)
    {
        DateTime strTime = Common.ToDateTime(TextBox_preOrderTimeStr.Text + " 00:00:00");//格式？
        DateTime endTime = Common.ToDateTime(TextBox_preOrderTimeEnd.Text + " 23:59:59");
        string strHour = "00:00:00";
        string endHour = "23:59:59";
        StatisticalStatementOperate statisticalStatementOperate = new StatisticalStatementOperate();
        DataTable dt = statisticalStatementOperate.QueryUsersAmountByDiffCity(strTime, endTime, strHour, endHour);
        if (dt.Rows.Count > 0)
        {
            Label_massage.Text = "";
            int tableCount = dt.Rows.Count;//
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
            GridView_DayUsersStatistics.DataSource = dt_page;
        }
        else
        {
            Label_massage.Text = "暂无符合条件的数据";
            GridView_DayUsersStatistics.DataSource = dt;//绑定显示
        }
        GridView_DayUsersStatistics.DataBind();
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)//分页操作
    {
        BindGridView_DayUsersStatistics(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 绑定显示第二个GridView
    /// DateTime strTime, DateTime endTime, string strHour, string endHour
    /// </summary>
    /// <param name="str"></param>
    /// <param name="end"></param>
    protected void BindGridView_UserAmountStatisticsDetail()
    {
        DateTime strTime = Common.ToDateTime(TextBox_preOrderTimeStr.Text + " 00:00:00");//格式？
        DateTime endTime = Common.ToDateTime(TextBox_preOrderTimeEnd.Text + " 23:59:59");
        string strHour = "00:00:00";
        string endHour = "23:59:59";
        StatisticalStatementOperate statisticalStatementOperate = new StatisticalStatementOperate();
        DataTable dt = statisticalStatementOperate.QueryUserAmountByDiffHour(strTime, endTime, strHour, endHour);
        if (dt.Rows.Count > 0)
        {
            Label_errorMessage.Text = "";
        }
        else
        {
            Label_errorMessage.Text = "暂无符合条件的数据";
        }
        GridView_UserAmountStatisticsDetail.DataSource = dt;//绑定显示
        GridView_UserAmountStatisticsDetail.DataBind();
    }
    #endregion

    /// <summary>
    /// 显示绑定信息
    /// </summary>
    protected void CommonFunction()
    {
        if (TextBox_preOrderTimeEnd.Text != "" && TextBox_preOrderTimeStr.Text != "")
        {
            if (CommonPageOperate.GetDiffDay(Common.ToDateTime(TextBox_preOrderTimeEnd.Text), Common.ToDateTime(TextBox_preOrderTimeStr.Text)) > 30)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('查询的时间范围不能超过30天，请重新选择自定义时间');</script>");
                return;
            }
            if (Panel_DayUsersStatistics.Visible == true)//用户量统计
            {
                BindGridView_DayUsersStatistics(0, 10);//显示绑定信息
            }
            else if (Panel_UserAmountStatisticsDetail_Gridview.Visible == true)
            {
                BindGridView_UserAmountStatisticsDetail();//显示明细绑定信息
            }
            else
            {
                BindGridView_ActityUsersStatistics(0, 10);
            }
        }
    }
    #region 选择日期和时间
    /// <summary>
    /// 选择日期
    /// </summary>
    protected void TextBox_preOrderTimeStr_TextChanged(object sender, EventArgs e)
    {
        CommonFunction();
        Button_1day.CssClass = "tabButtonBlueUnClick";//今天
        Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
        Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
        Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
        Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
        Button_self.CssClass = "tabButtonBlueClick";//自定义
    }
    /// <summary>
    /// 选择时间
    /// </summary>
    protected void TextBox_hourTimerStr_TextChanged(object sender, EventArgs e)
    {
        CommonFunction();
    }
    #endregion
    protected void BindGridView_ActityUsersStatistics(int str, int end)
    {
        DateTime strTime = Common.ToDateTime(TextBox_preOrderTimeStr.Text + " 00:00:00");
        DateTime endTime = Common.ToDateTime(TextBox_preOrderTimeEnd.Text + " 23:59:59");
        StatisticalStatementOperate statisticalStatementOperate = new StatisticalStatementOperate();
        DataTable dt = statisticalStatementOperate.GetActityUser(strTime, endTime);
        if (dt.Rows.Count > 0)
        {
            Label1.Text = "";
            int tableCount = dt.Rows.Count;//
            AspNetPager2.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
            GridView_ActityUser.DataSource = dt_page;
        }
        else
        {
            Label1.Text = "暂无符合条件的数据";
            GridView_ActityUser.DataSource = dt;//绑定显示
        }
        GridView_ActityUser.DataBind();
        lb_totalCount.Text = statisticalStatementOperate.GetActityUserCount(strTime, endTime).ToString();
    }
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        BindGridView_ActityUsersStatistics(AspNetPager2.StartRecordIndex - 1, AspNetPager2.EndRecordIndex);
    }
    protected void Button_query_Click(object sender, EventArgs e)
    {
        CommonFunction();
    }
}