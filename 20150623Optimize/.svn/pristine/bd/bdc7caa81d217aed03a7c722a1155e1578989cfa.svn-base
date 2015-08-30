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

public partial class Integration_CustomerIntegrationDetailManage : System.Web.UI.Page
{
    private static Guid CustomerIntegrationID;
    private CustomerIntegrationDetailOperate cido = new CustomerIntegrationDetailOperate();
    private CustomerOperate co = new CustomerOperate();
    private EmployeeOperate eo = new EmployeeOperate();
    private IntegrationRuleOperate iro = new IntegrationRuleOperate();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(ddlCity);
            ddlCity.Items.Remove(ddlCity.Items[ddlCity.Items.Count - 1]);
            ddlCity.SelectedValue = "87";
            tbEndTime.Text = DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59";
            tbBeginTime.Text = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd") + " 00:00:00";

            Label_LargePageCount.Text = "10";
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (!Common.IsPhoneNumber(tbmobilePhoneNumber.Text.Trim()))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请输入正确的11位手机号码')</script>");
            return;
        }
        AspNetPager1.CurrentPageIndex = 1;
        LoadData(1, Common.ToInt32(Label_LargePageCount.Text));
    }

    private void LoadData(int str, int end)
    {
       
        DataTable dtCustomer = co.QueryCustomerByMobilephone(tbmobilePhoneNumber.Text);
        if (dtCustomer.Rows.Count == 0)
        {
            lbUserName.InnerText = string.Empty;
            lbUserName1.Text = string.Empty;
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('查无此手机号的相关信息')</script>");
            return;
        }
        DataTable dtIntegration = cido.CustomerIntegration((long)dtCustomer.Rows[0]["CustomerID"]);
        if (dtIntegration.Rows.Count == 0)
        {
            lbIntegration.Text = "0";
        }
        else
        {
            CustomerIntegrationID = new Guid(dtIntegration.Rows[0]["ID"].ToString());
            lbIntegration.Text = dtIntegration.Rows[0]["Integration"].ToString();
        }
        lbUserName.InnerText = dtCustomer.Rows[0]["UserName"].ToString();
        lbUserName1.Text = dtCustomer.Rows[0]["UserName"].ToString();
        lbCustomerID.InnerText = dtCustomer.Rows[0]["CustomerID"].ToString();

        DateTime BeginDate = Common.ToDateTime(tbBeginTime.Text);
        DateTime EndDate = Common.ToDateTime(tbEndTime.Text);

        AspNetPager1.RecordCount = cido.CountDetail(Common.ToInt32(ddlCity.SelectedValue), BeginDate, EndDate, (long)dtCustomer.Rows[0]["CustomerID"]);

        DataTable dt = cido.CustomerIntegrationDetails(Common.ToInt32(ddlCity.SelectedValue), BeginDate, EndDate, (long)dtCustomer.Rows[0]["CustomerID"], str, end);

        gdList.DataSource = dt;
        gdList.DataBind();

        string strRuleID = string.Empty;
        string strUser = string.Empty;
        for (int i = 0; i < gdList.Rows.Count; i++)
        {
            if (Common.ToInt32(gdList.Rows[i].Cells[2].Text) > 0)
            {
                gdList.Rows[i].Cells[2].Text = "+" + gdList.Rows[i].Cells[2].Text;
            }
            strRuleID += "'" + gdList.DataKeys[i].Values["RuleID"].ToString() + "',";
            if (!gdList.DataKeys[i].Values["CreateUser"].ToString().Equals("0"))
            {
                strUser += gdList.DataKeys[i].Values["CreateUser"].ToString() + ",";
            }
        }

        DataTable dtRule = new DataTable();
         DataTable dtUser = new DataTable();

        //if (strRuleID.Length > 0)
        //{
        //    strRuleID = strRuleID.Substring(0, strRuleID.Length - 1);
        //    dtRule = iro.Integrations(strRuleID);
        //}
        if (strUser.Length > 0)
        {
            strUser = strUser.Substring(0, strUser.Length - 1);
            dtUser = eo.SelectEmployeeInfoByemployeeIds(strUser);
        }

        for (int i = 0; i < gdList.Rows.Count; i++)
        {
            //if (dtRule != null)
            //{
            //    for (int j = 0; j < dtRule.Rows.Count; j++)
            //    {
            //        if (gdList.DataKeys[i].Values["RuleID"].ToString().Equals(dtRule.Rows[j]["ID"].ToString()))
            //        {
            //            gdList.Rows[i].Cells[1].Text = dtRule.Rows[j]["Description"].ToString();
            //        }
            //        else if (new Guid(gdList.DataKeys[i].Values["RuleID"].ToString()) == new Guid())
            //        {
            //            gdList.Rows[i].Cells[1].Text = dtRule.Rows[j]["Description"].ToString();
            //        }
            //    }
            //}
            if (dtUser != null)
            {
                for (int j = 0; j < dtUser.Rows.Count; j++)
                {
                    if (gdList.DataKeys[i].Values["CreateUser"].ToString().Equals(dtUser.Rows[j]["EmployeeID"].ToString()))
                    {
                        gdList.Rows[i].Cells[4].Text = dtUser.Rows[j]["EmployeeFirstName"].ToString();
                        if (!dtUser.Rows[j]["EmployeePhone"].ToString().Equals(string.Empty))
                        {
                            gdList.Rows[i].Cells[4].Text += "(" + dtUser.Rows[j]["UserName"].ToString() + ")";
                        }
                    }
                    else
                    {
                        gdList.Rows[i].Cells[4].Text = "悠先平台";
                    }
                }
            }
        }
    }

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        LoadData(AspNetPager1.StartRecordIndex , AspNetPager1.EndRecordIndex);
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
        AspNetPager1.CurrentPageIndex = 1;
        ActivityMessage model = new ActivityMessage();
        DateTime BeginDate = Common.ToDateTime(tbBeginTime.Text);
        DateTime EndDate = Common.ToDateTime(tbEndTime.Text);
        LoadData(1, Common.ToInt32(Label_LargePageCount.Text));
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (lbCustomerID.InnerText.Trim().Equals(string.Empty))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请先查询要调整的用户')</script>");
            return;
        }
        if (tbRemark.Text.Trim().Equals(string.Empty))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('事件说明不能为空')</script>");
            return;
        }
        if (Common.ToInt32(tbin.Text)==0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请填写要增加/减少的积分值')</script>");
            return;
        }

        int integration = Common.ToInt32(ddlChange.SelectedValue) == 1 ? Common.ToInt32(tbin.Text) : Common.ToInt32(tbin.Text) * -1;

        if ((Common.ToInt32(lbIntegration.Text) + integration) < 0 && integration < 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请填写要减少的积分值，必须小于等于当前积分值')</script>");
            return;
        }
        CustomerIntegrationDetail model = new CustomerIntegrationDetail()
        {
            ID = Guid.NewGuid(),
            CustomerID = Common.ToInt32(lbCustomerID.InnerText),
            Description = tbRemark.Text,
            RuleID = new Guid(),
            SubID = new Guid(),
            Integration = integration,
            CurrentIntegration = Common.ToInt32(lbIntegration.Text) + integration,
            CreateDate = DateTime.Now,
            CreateUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID
        };
        int result = cido.Insert(model, CustomerIntegrationID);
        if (result == 1)
        {
            tbin.Text = string.Empty;
            tbRemark.Text = string.Empty;
            LoadData(1, Common.ToInt32(Label_LargePageCount.Text));
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('添加失败')</script>");
            return;
        }
    }
}