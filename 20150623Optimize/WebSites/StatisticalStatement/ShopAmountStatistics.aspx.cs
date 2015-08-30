using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using Web.Control.DDL;

public partial class StatisticalStatement_ShopAmountStatistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(ddlCity);
            Button_LargePageCount_Click(Button_10, null);//分页选中10
            Button_day_Click(Button_1day, null);//默认选中今天
        }
    }
    /// <summary>
    /// 设置选择日期的样式
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
        BindGridView_DayShopsStatistics(0, Common.ToInt32(Label_LargePageCount.Text));
    }
    /// <summary>
    /// 绑定显示GridView
    /// </summary>
    /// <param name="str"></param>
    /// <param name="end"></param>
    protected void BindGridView_DayShopsStatistics(int str, int end)
    {
        AspNetPager1.PageSize = Common.ToInt32(Label_LargePageCount.Text);
        if (TextBox_preOrderTimeStr.Text != "" && TextBox_preOrderTimeEnd.Text != "")
        {
            Label_Message.Text = "";
            int ishandle = ReturnRadioButtonCheckedStatus();
            int cityId = Common.ToInt32(ddlCity.SelectedValue);
            string strTime = Common.ToDateTime(TextBox_preOrderTimeStr.Text).ToString("yyyy-MM-dd");
            string endTime = Common.ToDateTime(TextBox_preOrderTimeEnd.Text).ToString("yyyy-MM-dd");
            StatisticalStatementOperate statisticalStatementOperate = new StatisticalStatementOperate();
            DataTable dt = statisticalStatementOperate.QueryShopAmountByDiffCity(strTime, endTime, ishandle, cityId);
            if (dt.Rows.Count > 0)
            {
                Label_massage.Text = "";
                int tableCount = dt.Rows.Count;//
                AspNetPager1.RecordCount = tableCount;
                DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
                GridView_DayShopsStatistics.DataSource = dt_page;
            }
            else
            {
                Label_massage.Text = "暂无符合条件的数据";
                GridView_DayShopsStatistics.DataSource = dt;//绑定显示
            }
        }
        else
        {
            Label_Message.Text = "查询时间不能为空";
        }
        GridView_DayShopsStatistics.DataBind();
    }
    /// <summary>
    /// 审核，未审核，未审核通过和所有门店四个不同的状态
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void RadioButton_CheckedChanged(object sender, EventArgs e)
    {
        BindGridView_DayShopsStatistics(0, Common.ToInt32(Label_LargePageCount.Text));
    }
    /// <summary>
    /// 获得查询门店的过滤条件（审核，未审核，未审核通过和所有门店四个不同的状态）
    /// </summary>
    /// <returns></returns>
    protected int ReturnRadioButtonCheckedStatus()
    {
        int handleStatus = 0;
        if (RadioButton_All.Checked)//所有的
        {
            handleStatus = 2;//自定义值
        }
        else if (RadioButton_Confirmed.Checked)//审核过
        {
            handleStatus = (int)VAShopHandleStatus.SHOP_Pass;
        }
        else if (RadioButton_Not_Confirmed.Checked)//未审核
        {
            handleStatus = (int)VAShopHandleStatus.SHOP_UnHandle;
        }
        else if (RadioButton_NotPass_Confrimed.Checked)//审核未通过
        {
            handleStatus = (int)VAShopHandleStatus.SHOP_UnPass;
        }
        return handleStatus;
    }
    protected void TextBox_preOrderTimeStr_TextChanged(object sender, EventArgs e)
    {
        BindGridView_DayShopsStatistics(0, Common.ToInt32(Label_LargePageCount.Text));
        Button_1day.CssClass = "tabButtonBlueUnClick";//今天
        Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
        Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
        Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
        Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
        Button_self.CssClass = "tabButtonBlueClick";//自定义
    }
    /// <summary>
    /// 分页
    /// </summary>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindGridView_DayShopsStatistics(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
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
        BindGridView_DayShopsStatistics(0, Common.ToInt32(Label_LargePageCount.Text));
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridView_DayShopsStatistics(0, Common.ToInt32(Label_LargePageCount.Text));
    }
}
