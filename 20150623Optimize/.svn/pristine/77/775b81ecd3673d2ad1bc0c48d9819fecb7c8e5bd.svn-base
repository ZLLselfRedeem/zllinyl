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

public partial class Messages_ActivityMessageManage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(ddlCity);
            ddlCity.Items.Remove(ddlCity.Items[ddlCity.Items.Count - 1]);
            ddlCity.SelectedValue = "87";
            tbEndTime.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59";
            tbBeginTime.Text = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd") + " 00:00:00";
            new MessageFirstTitleDropDownList().BindMessageFirstTitle(ddlMessageFirstTitle, Common.ToInt32(ddlCity.SelectedValue), true);
            
            Button_LargePageCount_Click(Button_10, null);//分页选中10
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        
        AspNetPager1.CurrentPageIndex = 1;
        LoadData(0, Common.ToInt32(Label_LargePageCount.Text));
    }

    protected void btn_New_Click(object sender, EventArgs e)
    {
        Response.Redirect("ActivityMessageDetail.aspx");
    }

    private void LoadData(int str, int end)
    {
        ActivityMessageOperate amo = new ActivityMessageOperate();
        ActivityMessage model = new ActivityMessage();
        model.Name = tbName.Text;
        model.MsgType = Common.ToInt32(ddlMsgType.SelectedValue);
        model.MessageFirstTitleID = Common.ToInt32(ddlMessageFirstTitle.SelectedValue);
       
        model.CityID = Common.ToInt32(ddlCity.SelectedValue);
        DateTime BeginDate = Common.ToDateTime(tbBeginTime.Text);
        DateTime EndDate = Common.ToDateTime(tbEndTime.Text);
        DataTable dt = amo.ActivityMessages(model, BeginDate, EndDate);
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
            foreach (MsgType msgType in Enum.GetValues(typeof(MsgType)))
            {
                if ((int)msgType == Common.ToInt32(gdList.DataKeys[i].Values["MsgType"]))
                {
                    gdList.Rows[i].Cells[3].Text = Common.GetEnumDescription(msgType);
                }
            } 

            LinkButton lbUpdate = (LinkButton)gdList.Rows[i].FindControl("lbtnUpdate");
            lbUpdate.PostBackUrl = "ActivityMessageDetail.aspx?ID=" + gdList.DataKeys[i].Values["ID"].ToString();
            
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        ActivityMessage model = new ActivityMessage();
        DateTime BeginDate = Common.ToDateTime(tbBeginTime.Text);
        DateTime EndDate = Common.ToDateTime(tbEndTime.Text);
        LoadData(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
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
        ActivityMessage model = new ActivityMessage();
        DateTime BeginDate = Common.ToDateTime(tbBeginTime.Text);
        DateTime EndDate = Common.ToDateTime(tbEndTime.Text);
        LoadData(0, Common.ToInt32(Label_LargePageCount.Text));
    }
    protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        new MessageFirstTitleDropDownList().BindMessageFirstTitle(ddlMessageFirstTitle, Common.ToInt32(ddlCity.SelectedValue), true);
    }
}