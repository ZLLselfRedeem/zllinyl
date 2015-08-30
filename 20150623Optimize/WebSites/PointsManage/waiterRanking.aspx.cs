using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;

public partial class PointsManage_waiterRanking : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Button_Time_Click(Button_Months, null);
        }
    }
    /// <summary>
    /// 设置排名周期的样式
    /// </summary>
    protected void Button_Time_Click(object sender, EventArgs e)
    {
        Panel_WaiterRanking.Visible = true;
        Panel_Detail.Visible = false;
        Button Button = (Button)sender;
        switch (Button.CommandName)
        {
            case "months"://上一月
                {
                    Button_Months.CssClass = "tabButtonBlueClick";
                    Button_Week.CssClass = "tabButtonBlueUnClick";
                    Button_UserDefined.CssClass = "tabButtonBlueUnClick";
                    TextBox_TimeStr.Text = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                    TextBox_TimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    TextBox_TimeStr.Enabled = false;
                    TextBox_TimeEnd.Enabled = false;
                    break;
                }
            case "week"://上一周
                {
                    Button_Months.CssClass = "tabButtonBlueUnClick";
                    Button_Week.CssClass = "tabButtonBlueClick";
                    Button_UserDefined.CssClass = "tabButtonBlueUnClick";
                    TextBox_TimeStr.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                    TextBox_TimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    TextBox_TimeStr.Enabled = false;
                    TextBox_TimeEnd.Enabled = false;
                    break;
                }
            case "userDefined"://自定义
            default:
                {
                    Button_Months.CssClass = "tabButtonBlueUnClick";
                    Button_Week.CssClass = "tabButtonBlueUnClick";
                    Button_UserDefined.CssClass = "tabButtonBlueClick";
                    TextBox_TimeStr.Enabled = true;
                    TextBox_TimeEnd.Enabled = true;
                    break;
                }
        }
        BindGridView(0, 10);
    }
    /// <summary>
    /// 绑定列表数据（查询条件：对账点单，过滤已退款点单，非友络员工）
    /// </summary>
    void BindGridView(int str, int end)
    {
        string starTime = Common.ToDateTime(TextBox_TimeStr.Text).ToString("yyyy-MM-dd") + " 00:00:00";
        string endTime = Common.ToDateTime(TextBox_TimeEnd.Text.Trim()).ToString("yyyy-MM-dd") + " 23:59:59";
        int cityId = Common.ToInt32(DropDownList_City.SelectedValue);
        int orderRule = Common.ToInt32(RadioButtonList_OrderBy.SelectedValue);
        EmployeePointOperate pointOper = new EmployeePointOperate();
        string phoneNum = txt_phoneNum.Text.Trim();
        double amount = Common.ToDouble(txt_amount.Text.Trim());
        DataTable dt = pointOper.QueryWaiterPointRanking(starTime, endTime, cityId, orderRule, phoneNum, amount);
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;
            AspNetPager_WaiterRanking.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
            GridView_WaiterRanking.DataSource = dt_page;
        }
        GridView_WaiterRanking.DataBind();
        if (GridView_WaiterRanking.Rows.Count > 0)
        {
            for (int i = 0; i < GridView_WaiterRanking.Rows.Count; i++)
            {
                string EmployeeFirstName = GridView_WaiterRanking.DataKeys[i].Values["EmployeeFirstName"].ToString();
                LinkButton linkBtn = GridView_WaiterRanking.Rows[i].FindControl("LinkButton_Name") as LinkButton;
                linkBtn.Text = EmployeeFirstName;
            }
        }
    }
    /// <summary>
    /// 分页
    /// </summary>
    protected void AspNetPager_WaiterRanking_PageChanged(object sender, EventArgs e)
    {
        BindGridView(AspNetPager_WaiterRanking.StartRecordIndex - 1, AspNetPager_WaiterRanking.EndRecordIndex);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_WaiterRanking_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Panel_WaiterRanking.Visible = false;
        Panel_Detail.Visible = true;
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        string employeeId = Common.ToString(GridView_WaiterRanking.DataKeys[index].Values["EmployeeID"].ToString());
        mainFrame.Attributes.Add("src", "waiterQuery.aspx?employeeID=" + employeeId);
    }
    /// <summary>
    /// 执行返回操作
    /// </summary>
    protected void Button_back_Click(object sender, EventArgs e)
    {
        Panel_WaiterRanking.Visible = true;
        Panel_Detail.Visible = false;
    }
    /// <summary>
    /// 切换选择筛选条件（城市）
    /// </summary>
    protected void DropDownList_City_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridView(0, 10);
    }
    /// <summary>
    /// 排序条件选择查询列表
    /// </summary>
    protected void RadioButtonList_OrderBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGridView(0, 10);
    }
    /// <summary>
    /// 日期时间选择查询列表
    /// </summary>
    protected void TextBox_Time_TextChanged(object sender, EventArgs e)
    {
        BindGridView(0, 10);
    }
    /// <summary>
    /// 点击按钮查询
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        BindGridView(0, 10);
    }
}