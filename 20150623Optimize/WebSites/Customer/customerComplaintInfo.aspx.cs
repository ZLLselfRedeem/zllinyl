using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;

public partial class Customer_customerComplaintInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GR_DataBind(0, 10);
        }
    }
    /// <summary>
    /// 绑定反馈列表信息显示
    /// </summary>
    /// <param name="str"></param>
    /// <param name="end"></param>
    void GR_DataBind(int str, int end)
    {
        ComplaintOperate operate = new ComplaintOperate();
        DataTable dt = operate.QueryCustomerComplaint();
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);//分页的DataTable
            GridViewCustomerComplaint.DataSource = dt_page;
        }
        else
        {
            GridViewCustomerComplaint.DataSource = dt;
        }
        GridViewCustomerComplaint.DataBind();
    }
    /// <summary>
    /// 分页显示
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GR_DataBind(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
}