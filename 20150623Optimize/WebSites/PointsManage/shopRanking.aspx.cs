using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VAGastronomistMobileApp.WebPageDll;

public partial class PointsManage_shopRanking : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            QueryShopRanking(0, 10);
        }
    }

    private void QueryShopRanking(int str, int end)
    {
        string startTime = "", endTime = "", cityId = "", orderBy = "";

        ShopOperate _ShopOperate = new ShopOperate();

        cityId = this.ddlArea.SelectedValue;

        switch (this.rblPeriod.SelectedValue)
        {
            case "month":
                startTime = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                endTime = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "week":
                //上周第一天  
                startTime = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                //上周最后一天  
                endTime = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            default:
                startTime = this.TextBox_TimeStr.Text.Trim();
                endTime = this.TextBox_TimeEnd.Text.Trim();
                break;
        }
        TextBox_TimeStr.Text = startTime;
        TextBox_TimeEnd.Text = endTime;
        orderBy = this.rblOrder.SelectedValue;

        DataTable dtShopRanking = _ShopOperate.QueryShopRanking(startTime + " 00:00:00", endTime + " 23:59:59", cityId, orderBy);

        if (dtShopRanking != null && dtShopRanking.Rows.Count > 0)
        {
            int cnt = dtShopRanking.Rows.Count;//总数
            AspNetPager1.RecordCount = cnt;
            DataTable dt_page = Common.GetPageDataTable(dtShopRanking, str, end);
            this.gdvShopRankingList.DataSource = dt_page;
            this.gdvShopRankingList.DataBind();
        }
        else
        {
            this.gdvShopRankingList.DataSource = null;
            this.gdvShopRankingList.DataBind();
        }
    }

    protected void TextBox_TimeStr_TextChanged(object sender, EventArgs e)
    {
        this.rblPeriod.ClearSelection();
        QueryShopRanking(0, 10);
    }

    /// <summary>
    /// 分页操作
    /// </summary>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        QueryShopRanking(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    //排序方式
    protected void rblOrder_SelectedIndexChanged(object sender, EventArgs e)
    {
        QueryShopRanking(0, 10);
    }
    //地区
    protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        QueryShopRanking(0, 10);
    }
    //周期
    protected void rblPeriod_SelectedIndexChanged(object sender, EventArgs e)
    {
        QueryShopRanking(0, 10);
    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        this.ddlArea.SelectedValue = "";
        this.rblPeriod.SelectedValue = "month";
        this.rblOrder.SelectedValue = "desc";
        DateTime lastMonth = DateTime.Now.AddMonths(-1);
        this.TextBox_TimeStr.Text = lastMonth.AddDays(1 - lastMonth.Day).ToString("yyyy-MM-dd");//上月第一天
        this.TextBox_TimeEnd.Text = lastMonth.AddDays(1 - lastMonth.Day).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");//上月最后一天
        QueryShopRanking(0, 10);
    }
}