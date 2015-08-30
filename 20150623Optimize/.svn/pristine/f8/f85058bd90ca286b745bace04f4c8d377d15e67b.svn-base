using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VAGastronomistMobileApp.WebPageDll;

public partial class ClientRecharge_customerRechargeStatistic : System.Web.UI.Page
{
    ClientRechargeOperate recharge = new ClientRechargeOperate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Initail();
        }
    }
    private void Initail()
    {
        this.txbBeginTime.Text = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
        this.txbEndTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
        DropDownListBind();
        BindList(0, 10);
    }
    private void BindList(int str, int end)
    {
        DateTime beginTime = Common.ToDateTime(txbBeginTime.Text.Trim() + " 00:00:00");
        DateTime endTime = Common.ToDateTime(txbEndTime.Text.Trim() + " 23:00:00");
        int rechargeId = Common.ToInt32(ddlRecharge.SelectedValue);

        DataTable dt = recharge.QueryRechargeStatistics(beginTime, endTime, rechargeId);
        if (dt.Rows.Count > 0)
        {
            int cnt = dt.Rows.Count;
            this.AspNetPager1.RecordCount = cnt;
            DataTable dtPage = Common.GetPageDataTable(dt, str, end);
            this.gdvRechageList.DataSource = dtPage;
            this.gdvRechageList.DataBind();
            this.lbRechargeCount.Text = dt.Compute("sum(rechargeCount)", "1=1").ToString();
            this.lbRechargeAmount.Text = dt.Compute("sum(rechargeAmount)", "1=1").ToString();
        }
        else
        {
            this.gdvRechageList.DataSource = null;
            this.gdvRechageList.DataBind();
            this.lbRechargeCount.Text = "0";
            this.lbRechargeAmount.Text = "0";
        }
    }
    protected void ddlRecharge_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindList(0, 10);
    }
    protected void txbBeginTime_TextChanged(object sender, EventArgs e)
    {
        BindList(0, 10);
    }
    protected void txbEndTime_TextChanged(object sender, EventArgs e)
    {
        BindList(0, 10);
    }
    private void DropDownListBind()
    {
        DataTable dt = recharge.QueryRecharge("");
        if (dt.Rows.Count > 0)
        {
            foreach (DataRow dr in dt.Rows)
            {
                ddlRecharge.Items.Add(new ListItem(dr["name"].ToString(), dr["id"].ToString()));
            }
        }
    }
    /// <summary>
    /// 分页操作
    /// </summary>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        BindList(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    protected void lnkbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        string date = e.CommandArgument.ToString().Replace("0:00:00", "");
        string rechargeId = ddlRecharge.SelectedValue;

        switch (e.CommandName)
        {
            case "detail":
                Response.Redirect("customerRechargeDetail.aspx?d=" + date + "&r=" + rechargeId + "");
                break;
        }
    }
}