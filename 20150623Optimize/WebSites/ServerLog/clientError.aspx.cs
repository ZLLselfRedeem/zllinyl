using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VAGastronomistMobileApp.WebPageDll;

public partial class ServerLog_clientError : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBox_endTime.Text = DateTime.Now.ToString();
            TextBox_startTime.Text = DateTime.Now.AddDays(-1).ToString();
            BindGr(0, 10);
        }
    }
    void BindGr(int str, int end)
    {
        ClientErrorInfoOperate operate = new ClientErrorInfoOperate();
        string strTime = TextBox_startTime.Text;
        string endTime = TextBox_endTime.Text;
        DataTable dt = operate.QueryClientErrorInfo(strTime, endTime);
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);
            gr_ClientError.DataSource = dt_page;
        }
        else
        {
            gr_ClientError.DataSource = dt;
        }
        gr_ClientError.DataBind();
    }
    protected void gr_ClientError_SelectedIndexChanged(object sender, EventArgs e)
    {
        long id = Common.ToInt64(gr_ClientError.DataKeys[gr_ClientError.SelectedIndex].Values["id"]);
        Response.Redirect("clientErrorDetail.aspx?errorId=" + id);
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindGr(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    protected void btn_Search_Click(object sender, EventArgs e)
    {
        BindGr(0, 10);
    }
}