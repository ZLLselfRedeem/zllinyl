using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;

public partial class OtherStatisticalStatement_employeePoint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Button_day_Click(Button_1day, null);//默认选中自定义
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
                TextBox_preOrderTimeStr.Text = "";
                TextBox_preOrderTimeEnd.Text = "";
                break;
            default:
                break;
        }
        GR_BindData_1(0, 10);
        GR_BindData_2(0, 10);
    }
    protected void GR_BindData_1(int str, int end)
    {
        StatisticalStatementOperate statisticalStatementOperate = new StatisticalStatementOperate();
        string timeFrom = TextBox_preOrderTimeStr.Text + " 00:00:00";
        string timeTo = TextBox_preOrderTimeEnd.Text + " 23:59:59";
        DataTable dt = statisticalStatementOperate.QueryActiveEmployee(timeFrom, timeTo);
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;//
            if (tableCount > 10)
            {
                AspNetPager1.Visible = true;
            }
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
            gv_activeEmployee.DataSource = dt_page;
        }
        else
        {
            AspNetPager1.Visible = false;
            gv_activeEmployee.DataSource = dt;//绑定显示的是空数据，目的为清空显示
        }
        gv_activeEmployee.DataBind();
    }
    protected void GR_BindData_2(int str, int end)
    {
        StatisticalStatementOperate statisticalStatementOperate = new StatisticalStatementOperate();
        string timeFrom = TextBox_preOrderTimeStr.Text + " 00:00:00";
        string timeTo = TextBox_preOrderTimeEnd.Text + " 23:59:59";
        DataTable dt = statisticalStatementOperate.QueryPointEmployee(timeFrom, timeTo);
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;//
            if (tableCount > 10)
            {
                AspNetPager2.Visible = true;
            }
            AspNetPager2.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
            gv_point.DataSource = dt_page;
        }
        else
        {
            AspNetPager2.Visible = false;
            gv_point.DataSource = dt;//绑定显示的是空数据，目的为清空显示
        }
        gv_point.DataBind();
    }
    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GR_BindData_1(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        GR_BindData_2(AspNetPager2.StartRecordIndex - 1, AspNetPager2.EndRecordIndex);
    }
    /// <summary>
    /// 选择时间控件，刷新页面
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TextBox_preOrderTimeStr_TextChanged(object sender, EventArgs e)
    {
        GR_BindData_1(0, 10);
        GR_BindData_2(0, 10);
    }
}