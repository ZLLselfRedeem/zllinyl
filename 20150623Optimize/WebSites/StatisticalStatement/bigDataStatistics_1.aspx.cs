using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;

public partial class StatisticalStatement_bigDataStatistics_1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataBind();
        }
    }

    /// <summary>
    /// 绑定列表数据
    /// </summary>
    private void DataBind()
    {
        string flag = Common.ToBool(rbWeek.Checked) ? "week" : "month";//true:查询周；false:查询月
        int year = Common.ToInt32(ddlYear.SelectedValue);//选择当前的年份
        var operate = new StatisticalStatementOperate();
        DataTable dt = operate.GetBigData_1(flag, year);
        gvDigDataStatistics_1.DataSource = dt;
        gvDigDataStatistics_1.DataBind();
    }

    /// <summary>
    /// 切换日期选择
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rbMonth_CheckedChanged(object sender, EventArgs e)
    {
        DataBind();
    }

    /// <summary>
    /// 切换年份
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataBind();
    }
}