using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;


public partial class Customer_Money19dianDetail : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBox_startTime.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
            TextBox_endTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            QueryMoney19dianDetailList(0, 10);
        }
    }
    protected void Button_Check_Click(object sender, EventArgs e)
    {
        QueryMoney19dianDetailList(0, 10);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="str"></param>
    /// <param name="end"></param>
    protected void QueryMoney19dianDetailList(int str, int end)
    {
        string strAddTime = TextBox_startTime.Text;
        string endAddTime = TextBox_endTime.Text;
        Money19dianDetailOperate money19dianDetailOperate = new Money19dianDetailOperate();
        DataTable dtMoney19dianDetail = money19dianDetailOperate.QueryMoney19dianDetail(strAddTime, endAddTime);
        DataView dvMoney19dianDetail = dtMoney19dianDetail.DefaultView;
        string filter = string.Empty;
        if (Request.QueryString["customerId"] != null)
        {
            filter += "customerId=" + Request.QueryString["customerId"];
        }
        dvMoney19dianDetail.RowFilter = filter;
        AspNetPager1.RecordCount = dvMoney19dianDetail.Count;
        DataTable dtMoney19dianDetailList = Common.GetPageDataTable(dvMoney19dianDetail.ToTable(), str, end);
        GridView_Money19dianDetailList.DataSource = dtMoney19dianDetailList;
        GridView_Money19dianDetailList.DataBind();
        CustomerOperate customerOperate = new CustomerOperate();
        CustomerInfo customerInfo = new CustomerInfo();
        long customerId = 0;
        for (int i = 0; i < GridView_Money19dianDetailList.Rows.Count; i++)
        {
            //获取用户名
            customerId = Common.ToInt64(GridView_Money19dianDetailList.DataKeys[i].Values["customerId"]);
            customerInfo = customerOperate.QueryCustomer(customerId);
            Label Label_customerId = GridView_Money19dianDetailList.Rows[i].FindControl("Label_customerId") as Label;
            Label_customerId.Text = customerInfo.UserName;

            Label Label_mobilePhoneNumber = GridView_Money19dianDetailList.Rows[i].FindControl("Label_mobilePhoneNumber") as Label;
            Label_mobilePhoneNumber.Text = customerInfo.mobilePhoneNumber;

            Label Label_customerEmail = GridView_Money19dianDetailList.Rows[i].FindControl("Label_customerEmail") as Label;
            Label_customerEmail.Text = customerInfo.customerEmail;

            Label Label_cooke = GridView_Money19dianDetailList.Rows[i].FindControl("Label_cooke") as Label;
            Label_cooke.Text = customerInfo.cookie;

            Label Label_changeReason = GridView_Money19dianDetailList.Rows[i].FindControl("Label_changeReason") as Label;
            Label_changeReason.Text = GridView_Money19dianDetailList.DataKeys[i].Values["changeReason"].ToString();
        }

    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        QueryMoney19dianDetailList(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
}