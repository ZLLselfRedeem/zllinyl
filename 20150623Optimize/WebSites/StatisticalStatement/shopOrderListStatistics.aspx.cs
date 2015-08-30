using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using Web.Control.DDL;
using Web.Control;

public partial class StatisticalStatement_shopOrderListStatistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(ddlCity);
            if (!String.IsNullOrEmpty(Request.QueryString["cityId"]))
            {
                ddlCity.SelectedValue = Common.ToString(Request.QueryString["cityId"]);
                ViewState["cityId"] = ddlCity.SelectedValue;
            }
            ViewState["cityId"] = ddlCity.SelectedValue;
            Button_LargePageCount_Click(Button_10, null);
            if (!string.IsNullOrEmpty(Request.QueryString["d"]))
            {
                Button_day_Click(Button_self, null);//默认选中自定义
            }
            else
            {
                Button_day_Click(Button_1day, null);
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
                string initData = Common.ToString(Request.QueryString["d"]);
                if (!string.IsNullOrEmpty(initData))
                {
                    TextBox_preOrderTimeStr.Text = initData;
                    TextBox_preOrderTimeEnd.Text = initData;
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
        GR_BindData(0, Common.ToInt32(Label_LargePageCount.Text));
    }
    /// <summary>
    /// 绑定GridView数据
    /// </summary>
    /// <param name="str"></param>
    /// <param name="end"></param>
    protected void GR_BindData(int str, int end)
    {
        ddlCity.SelectedValue = String.IsNullOrEmpty(ViewState["cityId"].ToString()) ? "0" : Common.ToString(ViewState["cityId"]);
        AspNetPager1.PageSize = Common.ToInt32(Label_LargePageCount.Text);
        StatisticalStatementOperate statisticalStatementOperate = new StatisticalStatementOperate();
        string timeFrom = TextBox_preOrderTimeStr.Text + " 00:00:00";
        string timeTo = TextBox_preOrderTimeEnd.Text + " 23:59:59";
        int shopId = 0;
        int companyId = 0;
        int cityId = 0;
        if (!string.IsNullOrEmpty(Request.QueryString["s"]))
        {
            shopId = Common.ToInt32(Request.QueryString["s"]);
        }
        if (!string.IsNullOrEmpty(Request.QueryString["c"]))
        {
            companyId = Common.ToInt32(Request.QueryString["c"]);
        }
        if (!String.IsNullOrEmpty(ViewState["cityId"].ToString()))
        {
            cityId = Common.ToInt32(ViewState["cityId"]);
        }
        DataTable dt = statisticalStatementOperate.QueryShopOrderList(timeFrom, timeTo, companyId, shopId, cityId);
        

        ViewState["data"] = dt;
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
        Label_preOrderServerSumSum.Text = Common.ToDouble(dt.Compute("sum(OrderAmount)", "1=1")).ToString("0.00");
        Label_OrderCount.Text = dt.Compute("sum(OrderCount)", "1=1").ToString();
        Label_payOrderCount.Text = dt.Compute("sum(PayCount)", "1=1").ToString();
        Label_prePaidSumSum.Text = Common.ToDouble(dt.Compute("sum(PayAmount)", "1=1")).ToString("0.00");

        lbYuE.Text = Common.ToDouble(dt.Compute("sum(yuePay)", "1=1")).ToString("0.00");
        lbTotalAliPay.Text = Common.ToDouble(dt.Compute("sum(aliPay)", "1=1")).ToString("0.00");
        lbTotalWechatPay.Text = Common.ToDouble(dt.Compute("sum(wechatPay)", "1=1")).ToString("0.00");
        lbAther.Text = Common.ToDouble(dt.Compute("sum(redEnvelopePay)", "1=1")).ToString("0.00");
        #endregion
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;//
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
            GridView_shopOrderList.DataSource = dt_page;
        }
        else
        {
            GridView_shopOrderList.DataSource = dt;//绑定显示的是空数据，目的为清空显示
        }
        GridView_shopOrderList.DataBind();
        if (GridView_shopOrderList.Rows.Count > 0)
        {
            for (int i = 0; i < GridView_shopOrderList.Rows.Count; i++)
            {
                ((LinkButton)(GridView_shopOrderList.Rows[i].FindControl("lnk_ShopName"))).Text = GridView_shopOrderList.DataKeys[i].Values["shopName"].ToString();
                ((LinkButton)GridView_shopOrderList.Rows[i].FindControl("lnk_Order")).Text = GridView_shopOrderList.DataKeys[i].Values["OrderCount"].ToString();
            }
        }
    }
    /// <summary>
    /// 分页显示
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GR_BindData(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 列表样式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_shopOrderList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (string.IsNullOrEmpty(e.CommandName) || e.CommandName == "Sort")
        {
            return;
        }
        int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
        int shopId = Common.ToInt32(GridView_shopOrderList.DataKeys[index].Values["shopID"].ToString());
        switch (Common.ToString(e.CommandName))
        {
            case "ShopName":
                //Response.Redirect("comprehensiveStatistics.aspx?id=" + shopId + "&str=" + TextBox_preOrderTimeStr.Text + "&end=" + TextBox_preOrderTimeEnd.Text + "");
                Response.Redirect("dataStatistics.aspx?id=" + shopId + "&str=" + TextBox_preOrderTimeStr.Text + "&end=" + TextBox_preOrderTimeEnd.Text + "");
                break;
            case "Order":
                Response.Redirect("singleShopDayOrderList.aspx?id=" + shopId + "&str=" + TextBox_preOrderTimeStr.Text + "&end=" + TextBox_preOrderTimeEnd.Text + "");
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 选择时间控件刷新列表信息
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TextBox_preOrderTimeStr_TextChanged(object sender, EventArgs e)
    {
        GR_BindData(0, Common.ToInt32(Label_LargePageCount.Text));
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
        GR_BindData(0, Common.ToInt32(Label_LargePageCount.Text));
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
        GR_BindData(0, Common.ToInt32(Label_LargePageCount.Text));
    }
    protected void Button_query_Click(object sender, EventArgs e)
    {
        GR_BindData(0, Common.ToInt32(Label_LargePageCount.Text));
    }
    /// <summary>
    /// 筛选城市刷新页面数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["cityId"] = Common.ToString(ddlCity.SelectedValue);
        GR_BindData(0, Common.ToInt32(Label_LargePageCount.Text));
    }
    /// <summary>
    /// 数据导出excel
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExportExcel_Click(object sender, EventArgs e)
    {
        DataTable dt = ViewState["data"] as DataTable;
        if (dt != null)
        {
            if (dt.Rows.Count > 0)
            {
                ExcelHelper.ExportExcel(dt, this, "ShopOrder" + DateTime.Now);
                return;
            }
        }
        CommonPageOperate.AlterMsg(this, "无数据");
    }

    protected void GridView_shopOrderList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //ShopOperate shopOperate = new ShopOperate();
        //EmployeeOperate employeeOperate = new EmployeeOperate();
        //Label LabelAreaManager = e.Row.FindControl("LabelAreaManager") as Label;
        //if (LabelAreaManager != null)
        //{
        //    int shopId = int.Parse( this.GridView_shopOrderList.DataKeys[e.Row.RowIndex]["shopID"].ToString());
        //    var shopInfo = shopOperate.QueryShop(shopId);
        //    if (shopInfo == null ||  shopInfo.AreaManager.HasValue == false)
        //    {
        //        return;
        //    }

        //    var areaManager = employeeOperate.QueryEmployee(shopInfo.AreaManager.Value);

        //    if (areaManager == null)
        //    {
        //        return;
        //    }

        //    LabelAreaManager.Text = areaManager.EmployeeFirstName;
        //}
    }
}