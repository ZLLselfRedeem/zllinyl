using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using System.Transactions;
using Web.Control.DDL;
using System.Net;
using System.Net.Sockets;
using Web.Control;
using System.Reflection;
using System.EnterpriseServices;
using System.ServiceModel.Activities;

public partial class Package_PackageStatistics : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(ddlCity);
            loadPackage();
            tbEndTime.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59";
            tbBeginTime.Text = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd") + " 00:00:00";

            Button_LargePageCount_Click(Button_10, null);//分页选中10
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        LoadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlCity.SelectedValue), Common.ToInt32(ddlPackage.SelectedValue));
    }

    private void LoadData(int str,int end,int cityId,int status)
    {
        PackageOperate po = new PackageOperate();
        DataTable dt = po.PackageStatistics(tbName.Text, Common.ToDateTime(tbBeginTime.Text), Common.ToDateTime(tbEndTime.Text), Common.ToInt32(ddlCity.SelectedValue), Common.ToInt32(ddlPackage.SelectedValue));
        DataTable dtPage = new DataTable();

        AspNetPager1.PageSize = Common.ToInt32(Label_LargePageCount.Text);
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            dtPage = Common.GetPageDataTable(dt, str, end);
        }
        else
        {
            AspNetPager1.RecordCount = 0;
        }

        gdList.DataSource = dtPage;
        gdList.DataBind();

        for (int i = 0; i < gdList.Rows.Count; i++)
        {
            LinkButton lbtnView = (LinkButton)gdList.Rows[i].FindControl("lbtnView");
            lbtnView.PostBackUrl = "PackageStatisticsView.aspx?ID=" + gdList.DataKeys[i].Values["CouponId"].ToString();
            if (gdList.DataKeys[i].Values["Name"].ToString().Equals(string.Empty))
            {
                gdList.Rows[i].Cells[3].Text = "由平台发送";
            }
            gdList.Rows[i].Cells[4].Text = killZero(gdList.Rows[i].Cells[4].Text);
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        LoadData(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex, Common.ToInt32(ddlCity.SelectedValue), Common.ToInt32(ddlPackage.SelectedValue));
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
        AspNetPager1.CurrentPageIndex = 0;
        LoadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlCity.SelectedValue), Common.ToInt32(ddlPackage.SelectedValue));
    }

    private void loadPackage()
    {
        PackageOperate po = new PackageOperate();
        DataTable dt = po.Packages(Common.ToInt32(ddlCity.SelectedValue), (int)PackageStatus.Enable);
        ddlPackage.DataSource = dt;
        ddlPackage.DataTextField = "Name";
        ddlPackage.DataValueField = "ID";
        ddlPackage.DataBind();
        ddlPackage.Items.Add(new ListItem("所有", "0"));
        ddlPackage.Items.Add(new ListItem("由平台发送", "-1"));
        ddlPackage.SelectedValue = "0";//
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadPackage();
    }

    private string killZero(string str)
    {
        if (str.IndexOf('.') != -1)
        {
            return str.TrimEnd('0').TrimEnd('.');
        }
        else
        {
            return str;
        }

    }
}