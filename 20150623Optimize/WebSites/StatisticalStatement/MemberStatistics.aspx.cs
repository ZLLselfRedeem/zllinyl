using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;

public partial class StatisticalStatement_MemberStatistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Button_LargePageCount_Click(Button_10, null);//分页选中10
            QueryProvince();
            QueryCity();
            //Button_Pay_Click(Button_Pay, null);
            Button_day_Click(Button_1day, null);//默认选中今天
        }
    }
    /// <summary>
    /// 获取省份
    /// </summary>
    protected void QueryProvince()
    {
        ProvinceOperate po = new ProvinceOperate();
        DropDownList_Province.DataSource = po.QueryProvince();
        DropDownList_Province.DataTextField = "provinceName";
        DropDownList_Province.DataValueField = "provinceID";
        DropDownList_Province.DataBind();
        DropDownList_Province.Items.Add(new ListItem("所有省份", "0"));
        DropDownList_Province.SelectedIndex = DropDownList_Province.Items.Count - 1;
    }

    /// <summary>
    /// 获取城市
    /// </summary>
    protected void QueryCity()
    {
        DropDownList_City.Items.Clear();
        if (DropDownList_Province.SelectedItem.Text == "所有省份")
        {
            setCitySelected();
        }
        else
        {
            CityOperate co = new CityOperate();
            DropDownList_City.DataSource = co.QueryCity(int.Parse(DropDownList_Province.SelectedItem.Value));
            DropDownList_City.DataTextField = "cityName";
            DropDownList_City.DataValueField = "cityID";
            DropDownList_City.DataBind();
            setCitySelected();
        }
    }

    private void setCitySelected()
    {
        DropDownList_City.Items.Add(new ListItem("所有城市", "0"));
        DropDownList_City.SelectedIndex = 0;
    }

    /// <summary>
    /// 选择统计日期，添加样式
    /// </summary>
    protected void Button_day_Click(object sender, EventArgs e)
    {
        TextBox_registerTimeStr.Text = "";
        TextBox_registerTimeEnd.Text = "";
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
                TextBox_registerTimeStr.Text = DateTime.Now.ToString("yyyy-MM-dd");//日期控件显示当前时间
                TextBox_registerTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "yesterday":
                Button_1day.CssClass = "tabButtonBlueUnClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueClick";//昨天
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_registerTimeStr.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                TextBox_registerTimeEnd.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                break;
            case "7":
                Button_1day.CssClass = "tabButtonBlueUnClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
                Button_7day.CssClass = "tabButtonBlueClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_registerTimeStr.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                TextBox_registerTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "14":
                Button_1day.CssClass = "tabButtonBlueUnClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueUnClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_registerTimeStr.Text = DateTime.Now.AddDays(-14).ToString("yyyy-MM-dd");
                TextBox_registerTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "30":
                Button_1day.CssClass = "tabButtonBlueUnClick";//今天
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//昨天
                Button_7day.CssClass = "tabButtonBlueUnClick";//最近一周
                Button_14day.CssClass = "tabButtonBlueUnClick";//最近14天
                Button_30day.CssClass = "tabButtonBlueClick";//最近30天
                Button_self.CssClass = "tabButtonBlueUnClick";//自定义
                TextBox_registerTimeStr.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                TextBox_registerTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
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
    /// 自定义选择时间日期
    /// </summary>
    protected void TextBox_registerTimeStr_TextChanged(object sender, EventArgs e)
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
        ShowGridView_MemberDetailInfo(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 绑定显示数据表信息
    /// </summary>
    protected void ShowGridView_MemberDetailInfo(int str, int end)
    {
        AspNetPager1.PageSize = Common.ToInt32(Label_LargePageCount.Text);
        DateTime strTime = Common.ToDateTime(TextBox_registerTimeStr.Text + " 00:00:00");
        DateTime endTime = Common.ToDateTime(TextBox_registerTimeEnd.Text + " 23:59:59");
        int provinceID = int.Parse(DropDownList_Province.SelectedItem.Value);
        int cityID = int.Parse(DropDownList_City.SelectedItem.Value);
        StatisticalStatementOperate sto = new StatisticalStatementOperate();
        DataTable dt = sto.QueryMemberDetailByDiffCity(strTime, endTime, provinceID, cityID);

        if (dt.Rows.Count > 0)
        {
            Label_massage.Text = "";
            int tableCount = dt.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
            GridView_CustomerDetail.DataSource = dt_page;
        }
        else
        {
            Label_massage.Text = "暂无符合条件的数据";
            GridView_CustomerDetail.DataSource = dt;//绑定显示的是空数据，目的为清空显示
        }
        GridView_CustomerDetail.DataBind();
    }
    /// <summary>
    ///触发查询显示信息方法
    /// </summary>
    protected void SelectFunction()
    {
        if (TextBox_registerTimeStr.Text != "" && TextBox_registerTimeEnd.Text != "")
        {
            if (Panel_MemberStatisticGridview.Visible == true)
            {
                ShowGridView_MemberStatistic();
            }
            else
            {
                ShowGridView_MemberDetailInfo(0, Common.ToInt32(Label_LargePageCount.Text));
            }
        }
    }

    private void ShowGridView_MemberStatistic()
    {
        DateTime strTime = Common.ToDateTime(TextBox_registerTimeStr.Text + " 00:00:00");
        DateTime endTime = Common.ToDateTime(TextBox_registerTimeEnd.Text + " 23:59:59");
        int provinceID = int.Parse(DropDownList_Province.SelectedItem.Value);
        int cityID = int.Parse(DropDownList_City.SelectedItem.Value);
        int nTotal;
        StatisticalStatementOperate sto = new StatisticalStatementOperate();
        GridView_MemberStatistic.DataSource = sto.QueryMemberAmountByDiffCity(strTime, endTime, provinceID, cityID, out nTotal);
        GridView_MemberStatistic.DataBind();
        Label_Total.Text = "会员总数：" + nTotal;
    }
    /// <summary>
    /// 显示会员数量统计信息
    /// </summary>
    protected void Button_Member_Click(object sender, EventArgs e)
    {
        Button_Member.CssClass = "couponButtonSubmit";
        Button_CustomerDetail.CssClass = "tabButtonBlueUnClick";

        Panel_MemberStatisticGridview.Visible = true;
        Panel_CustomerDetail.Visible = false;
        SelectFunction();
    }
    protected void DropDownList_Province_SelectedIndexChanged(object sender, EventArgs e)
    {
        QueryCity();
        SelectFunction();
    }
    protected void DropDownList_City_SelectedIndexChanged(object sender, EventArgs e)
    {
        SelectFunction();
    }
    /// <summary>
    /// 显示会员详细信息
    /// </summary>
    protected void Button_CustomerDetail_Click(object sender, EventArgs e)
    {
        Button_Member.CssClass = "tabButtonBlueUnClick";
        Button_CustomerDetail.CssClass = "couponButtonSubmit";

        Panel_MemberStatisticGridview.Visible = false;
        Panel_CustomerDetail.Visible = true;
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