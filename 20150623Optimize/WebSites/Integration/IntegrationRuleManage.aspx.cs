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
using Web.Control.DDL;
using System.Net;
using System.Net.Sockets;
using Web.Control;
using System.Reflection;
using System.EnterpriseServices;
using System.ServiceModel.Activities;

public partial class Integration_IntegrationRuleManage : System.Web.UI.Page
{
    private IntegrationRuleOperate iro = new IntegrationRuleOperate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new RuleDropDownList().BindRule(ddlBussnessEventType);
            ddlBussnessEventType.Items.Insert(0, new ListItem("所有", new Guid().ToString()));
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
        Response.Redirect("IntegrationRuleDetail.aspx");
    }

    private void LoadData(int str, int end)
    {
        DataTable dt = iro.Integrations(new Guid(ddlBussnessEventType.SelectedValue));
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
            //foreach (BussnessEventType type in Enum.GetValues(typeof(BussnessEventType)))
            //{
            //    if ((int)type == Common.ToInt32(gdList.DataKeys[i].Values["RuleTypeID"]))
            //    {
            //        gdList.Rows[i].Cells[0].Text = Common.GetEnumDescription(type);
            //    }
            //}
            foreach (EventType type in Enum.GetValues(typeof(EventType)))
            {
                if ((int)type == Common.ToInt32(gdList.DataKeys[i].Values["EventType"]))
                {
                    gdList.Rows[i].Cells[3].Text = Common.GetEnumDescription(type);
                }
            }
            foreach (EventComplement type in Enum.GetValues(typeof(EventComplement)))
            {
                if ((int)type == Common.ToInt32(gdList.DataKeys[i].Values["EventComplement"]))
                {
                    if (type == EventComplement.Amount)
                    {
                        if (Common.ToInt32(gdList.DataKeys[i].Values["ConditionalMinValue"]) > Common.ToInt32(gdList.DataKeys[i].Values["ConditionalMaxValue"]))
                        {
                            gdList.Rows[i].Cells[4].Text = Common.GetEnumDescription(type)
                               + gdList.DataKeys[i].Values["ConditionalMinValue"].ToString()
                               + "起";
                        }
                        else if (Common.ToInt32(gdList.DataKeys[i].Values["ConditionalMinValue"]) == Common.ToInt32(gdList.DataKeys[i].Values["ConditionalMaxValue"])
                            && Common.ToInt32(gdList.DataKeys[i].Values["EventType"])==(int)EventType.Cumulative)
                        {
                            gdList.Rows[i].Cells[4].Text = Common.GetEnumDescription(type)
                               + gdList.DataKeys[i].Values["ConditionalMinValue"].ToString();
                        }
                        else
                        {
                            gdList.Rows[i].Cells[4].Text = Common.GetEnumDescription(type)
                                + "在" + gdList.DataKeys[i].Values["ConditionalMinValue"].ToString()
                                + "到" + gdList.DataKeys[i].Values["ConditionalMaxValue"].ToString();
                        }
                    }
                    else if (type == EventComplement.Count)
                    {
                        gdList.Rows[i].Cells[4].Text = Common.GetEnumDescription(type)
                                                      + gdList.DataKeys[i].Values["ConditionalMinValue"].ToString();
                    }
                }
            }
            foreach (IntegrationStatus type in Enum.GetValues(typeof(IntegrationStatus)))
            {
                if ((int)type == Common.ToInt32(gdList.DataKeys[i].Values["Status"]))
                {
                    gdList.Rows[i].Cells[5].Text = Common.GetEnumDescription(type);
                }
            }
            if (gdList.Rows[i].Cells[4].Text == "0")
            {
                gdList.Rows[i].Cells[4].Text = "-";
            }

            LinkButton lbUpdate = (LinkButton)gdList.Rows[i].FindControl("lbtnUpdate");
            lbUpdate.PostBackUrl = "IntegrationRuleDetail.aspx?ID=" + gdList.DataKeys[i].Values["ID"].ToString();
            
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
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
        LoadData(0, Common.ToInt32(Label_LargePageCount.Text));
    }
    protected void gdList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
            Guid ID = new Guid(gdList.DataKeys[index].Values["ID"].ToString());
            IntegrationRule model = new IntegrationRule();
            model.ID = ID;
            model.Status = (int)IntegrationStatus.Delete;
            model.UpdateDate = DateTime.Now;
            model.UpdateUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
            iro.UpdateStatus(model);
            AspNetPager1.CurrentPageIndex = 0;
            LoadData(0, Common.ToInt32(Label_LargePageCount.Text));
        }
    }
}