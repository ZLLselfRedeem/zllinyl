using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using System.Transactions;
using Web.Control.DDL;
using System.Net;
using System.Net.Sockets;
using CloudStorage;
using System.Drawing;
using VAEncryptDecrypt;

public partial class Integration_IntegrationStatistics : System.Web.UI.Page
{
    private static DataTable dt = new DataTable();
    private static Guid ID;
    private IntegrationRuleOperate iro = new IntegrationRuleOperate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(ddlCity);
            new RuleDropDownList().BindRule(ddlBussnessEventType);
            ddlBussnessEventType.Items.Insert(ddlBussnessEventType.Items.Count, new ListItem("运营填写", new Guid().ToString()));
            ddlBussnessEventType.SelectedIndex = 0;
            new RuleDropDownList().BindIntegrationRule(ddlIntegrationRule, new Guid(ddlBussnessEventType.SelectedValue));

            tbEndTime.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59";
            tbBeginTime.Text = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd") + " 00:00:00";
        }
    }
   
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataTable dt = iro.SumIntegration(Common.ToInt32(ddlCity.SelectedValue), new Guid(ddlIntegrationRule.SelectedValue), new Guid(ddlBussnessEventType.SelectedValue), Common.ToDateTime(tbBeginTime.Text), Common.ToDateTime(tbEndTime.Text));
        tbIntegration.Text = dt.Rows[0][0].ToString();
    }
    protected void ddlBussnessEventType_SelectedIndexChanged(object sender, EventArgs e)
    {
        new RuleDropDownList().BindIntegrationRule(ddlIntegrationRule, new Guid(ddlBussnessEventType.SelectedValue));
    }
}