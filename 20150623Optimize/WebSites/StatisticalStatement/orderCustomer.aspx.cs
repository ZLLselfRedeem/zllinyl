using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;

public partial class StatisticalStatement_orderCustomer : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Button_day_Click(Button_7day, null);
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
            case "7":
                Button_7day.CssClass = "tabButtonBlueClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_preOrderTimeStr.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                TextBox_preOrderTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "14":
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_preOrderTimeStr.Text = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd");
                TextBox_preOrderTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "30":
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_preOrderTimeStr.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                TextBox_preOrderTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "self":
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
        GR_BindData(0, 10);
    }
    /// <summary>
    /// 绑定GridView数据
    /// </summary>
    /// <param name="str"></param>
    /// <param name="end"></param>
    protected void GR_BindData(int str, int end)
    {
        StatisticalStatementOperate statisticalStatementOperate = new StatisticalStatementOperate();
        string timeFrom = TextBox_preOrderTimeStr.Text + " 00:00:00";
        string timeTo = TextBox_preOrderTimeEnd.Text + " 23:59:59";
        DataTable dt = statisticalStatementOperate.QueryOrderCustomer(timeFrom, timeTo);
        GridView_OrderCustomer.DataSource = dt;
        GridView_OrderCustomer.DataBind();
    }
    /// <summary>
    /// 选择时间刷新页面信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TextBox_preOrderTimeStr_TextChanged(object sender, EventArgs e)
    {
        GR_BindData(0, 10);
    }
}