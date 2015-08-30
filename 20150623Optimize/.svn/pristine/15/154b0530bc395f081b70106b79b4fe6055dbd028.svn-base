using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control.DDL;

public partial class CustomerServiceProcessing_batchMoneyManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(ddlCity);
            Button_Time_Click(Button_1day, null);//默认选择第一天
            btnStatus_Click(btnAll, null);
        }
    }
    /// <summary>
    /// 设置点击时间Button的样式
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Time_Click(object sender, EventArgs e)
    {
        Button Button = (Button)sender;
        switch (Button.CommandName)
        {
            case "1":
                Button_1day.CssClass = "tabButtonBlueClick";
                Button_7day.CssClass = "tabButtonBlueUnClick";
                Button_14day.CssClass = "tabButtonBlueUnClick";
                Button_30day.CssClass = "tabButtonBlueUnClick";
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//
                TextBox_preOrderTimeStr.Text = DateTime.Now.ToString("yyyy-MM-dd");
                TextBox_preOrderTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "yesterday":
                Button_1day.CssClass = "tabButtonBlueUnClick";
                Button_7day.CssClass = "tabButtonBlueUnClick";
                Button_14day.CssClass = "tabButtonBlueUnClick";
                Button_30day.CssClass = "tabButtonBlueUnClick";
                Button_yesterday.CssClass = "tabButtonBlueClick";//
                TextBox_preOrderTimeStr.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                TextBox_preOrderTimeEnd.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
                break;
            case "7":
                Button_1day.CssClass = "tabButtonBlueUnClick";
                Button_7day.CssClass = "tabButtonBlueClick";
                Button_14day.CssClass = "tabButtonBlueUnClick";
                Button_30day.CssClass = "tabButtonBlueUnClick";
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//
                TextBox_preOrderTimeStr.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                TextBox_preOrderTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "14":
                Button_1day.CssClass = "tabButtonBlueUnClick";
                Button_7day.CssClass = "tabButtonBlueUnClick";
                Button_14day.CssClass = "tabButtonBlueClick";
                Button_30day.CssClass = "tabButtonBlueUnClick";
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//
                TextBox_preOrderTimeStr.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
                TextBox_preOrderTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            case "30":
                Button_1day.CssClass = "tabButtonBlueUnClick";
                Button_7day.CssClass = "tabButtonBlueUnClick";
                Button_30day.CssClass = "tabButtonBlueClick";
                Button_14day.CssClass = "tabButtonBlueUnClick";
                Button_yesterday.CssClass = "tabButtonBlueUnClick";//
                TextBox_preOrderTimeStr.Text = DateTime.Now.AddDays(-30).ToString("yyyy-MM-dd");
                TextBox_preOrderTimeEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
                break;
            default:
                break;
        }
        BindGr(0, 10);
    }
    /// <summary>
    /// 绑定列表数据
    /// </summary>
    /// <param name="str"></param>
    /// <param name="end"></param>
    void BindGr(int str, int end)
    {
        string strTime = TextBox_preOrderTimeStr.Text.Trim() + " 00:00:00";
        string endTime = TextBox_preOrderTimeEnd.Text.Trim() + " 23:59:59";
        int cityId = Common.ToInt32(ddlCity.SelectedValue);
        string status = Common.ToString(hiddenField.Value);
        BatchMoneyOperate operate = new BatchMoneyOperate();
        DataTable dt = operate.QueryBatchMoneyApply(strTime, endTime, cityId, status);
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);
            GridView_batchMoneyManage.DataSource = dt_page;
        }
        GridView_batchMoneyManage.DataBind();
        if (GridView_batchMoneyManage.Rows.Count > 0)
        {
            for (int i = 0; i < GridView_batchMoneyManage.Rows.Count; i++)
            {
                LinkButton queryText = GridView_batchMoneyManage.Rows[i].FindControl("queryText") as LinkButton;
                int batchMoneyApplyId = Common.ToInt32(GridView_batchMoneyManage.DataKeys[i].Values["batchMoneyApplyId"].ToString());
                queryText.PostBackUrl = "~/CustomerServiceProcessing/batchMoneyDetail.aspx?batchMoneyApplyId=" + batchMoneyApplyId.ToString();
            }
        }
    }
    /// <summary>
    /// 切换时间
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void TextBox_TextChanged(object sender, EventArgs e)
    {
        BindGr(0, 10);
    }
    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindGr(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 切换城市刷新数据
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindGr(0, 10);
    }
    protected void btnStatus_Click(object sender, EventArgs e)
    {
        Button btn = (Button)sender;
        switch (btn.CommandName)
        {
            case "All":
                btnAll.CssClass = "tabButtonBlueClick";
                btnYes.CssClass = "tabButtonBlueUnClick";
                btnNot.CssClass = "tabButtonBlueUnClick";
                hiddenField.Value = "all";
                break;
            case "Yes":
                btnYes.CssClass = "tabButtonBlueClick";
                btnAll.CssClass = "tabButtonBlueUnClick";
                btnNot.CssClass = "tabButtonBlueUnClick";
                hiddenField.Value = "yes";
                break;
            case "Not":
            default:
                btnNot.CssClass = "tabButtonBlueClick";
                btnAll.CssClass = "tabButtonBlueUnClick";
                btnYes.CssClass = "tabButtonBlueUnClick";
                hiddenField.Value = "not";
                break;
        }
        BindGr(0, 10);
    }
}