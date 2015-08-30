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

public partial class Integration_IntegrationRuleDetail : System.Web.UI.Page
{
    private static DataTable dt = new DataTable();
    private static Guid ID;
    private IntegrationRuleOperate iro = new IntegrationRuleOperate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new RuleDropDownList().BindRule(ddlBussnessEventType);
            if (Request.QueryString["id"] != null)
            {
                ID = new Guid(Request.QueryString["id"]);
                //加载数据
                DataTable dt = iro.IntegrationDetail(ID);
                if (dt.Rows.Count > 0)
                {
                    tbDescription.Text = dt.Rows[0]["Description"].ToString();
                    tbIntegration.Text = dt.Rows[0]["Integration"].ToString();
                    tbConditionalMinValue.Text = dt.Rows[0]["ConditionalMinValue"].ToString();
                    tbConditionalMaxValue.Text = dt.Rows[0]["ConditionalMaxValue"].ToString();
                    ddlBussnessEventType.SelectedValue = dt.Rows[0]["RuleTypeID"].ToString();
                    ChangeEventType(ddlBussnessEventType.SelectedValue);
                    ddlEventType.SelectedValue = dt.Rows[0]["EventType"].ToString();
                    ddlEventType_SelectedIndexChanged(sender, e);
                    ddlEventComplement.SelectedValue = dt.Rows[0]["EventComplement"].ToString();
                    ddlEventComplement_SelectedIndexChanged(sender, e);
                    ddlStatus.SelectedValue = dt.Rows[0]["Status"].ToString();
                    if (tbConditionalMaxValue.Text == "0" && Common.ToInt32(ddlEventComplement.SelectedValue) == (int)EventComplement.Amount)
                    {
                        tbConditionalMaxValue.Text = "";
                    }
                    btnUpdate.Text = "修 改";
                }
                else
                {
                    return;
                }

            }
            else
            {
                ddlBussnessEventType_SelectedIndexChanged(sender, e);
                btnUpdate.Text = "保 存";
            }
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PackageManager.aspx");
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //验证
        if(tbDescription.Text.Trim().Equals(string.Empty))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('业务事件描述不能为空')</script>");
            return;
        }

        if (Common.ToInt32(tbIntegration.Text) <= 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('积分值必须大于0')</script>");
            return;
        }
        if (trEventComplement.Visible == true)
        {
            if (Common.ToInt32(tbConditionalMinValue.Text) < 0 && tbConditionalMinValue.Visible == true)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('事件补充最小值必须大于等于0')</script>");
                return;
            }

            if (Common.ToInt32(tbConditionalMinValue.Text) <= 0 && tbConditionalMinValue.Visible == true && tbConditionalMaxValue.Visible == false && tbConditionalMinValue.Text != string.Empty)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('事件补充最小值必须大于0')</script>");
                return;
            }

            if (Common.ToInt32(tbConditionalMaxValue.Text) <= 0 && tbConditionalMaxValue.Visible == true && tbConditionalMaxValue.Text != string.Empty)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('事件补充最大值必须大于0')</script>");
                return;
            }

            if (tbConditionalMinValue.Visible == true && tbConditionalMaxValue.Visible == true && (Common.ToInt32(tbConditionalMaxValue.Text) - Common.ToInt32(tbConditionalMinValue)) < 0)
            {
                if (Common.ToInt32(ddlEventComplement.SelectedValue) != (int)EventComplement.Amount && tbConditionalMaxValue.Text != string.Empty)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('事件补充最大值必须大于等于最小值')</script>");
                    return;
                }
            }

            if (tbConditionalMinValue.Visible == true && tbConditionalMaxValue.Visible == false)
            {
                tbConditionalMaxValue.Text = tbConditionalMinValue.Text;
            }
            else if (tbConditionalMinValue.Visible == false && tbConditionalMaxValue.Visible == false)
            {
                tbConditionalMinValue.Text = "0";
                tbConditionalMaxValue.Text = "0";
            }
        }
        else
        {
            tbConditionalMinValue.Text = "0";
            tbConditionalMaxValue.Text = "0";
        }

        IntegrationRule model = new IntegrationRule();
        model.Description = tbDescription.Text;
        model.Integration = Common.ToInt32(tbIntegration.Text);
        model.RuleTypeID = new Guid(ddlBussnessEventType.SelectedValue);
        model.EventType = Common.ToInt32(ddlEventType.SelectedValue);
        model.EventComplement = Common.ToInt32(ddlEventComplement.SelectedValue);
        model.ConditionalMinValue = Common.ToInt32(tbConditionalMinValue.Text);
        model.ConditionalMaxValue = Common.ToInt32(tbConditionalMaxValue.Text);
        model.Status = Common.ToInt32(ddlStatus.SelectedValue);
      
        model.CreateUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
        model.CreateDate = DateTime.Now;
        model.UpdateUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
        model.UpdateDate = model.CreateDate;
        if (Request.QueryString["id"] == null)
        {
            //insert
            if (iro.Insert(model) == 1)
            {
                Response.Redirect("IntegrationRuleManage.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('添加失败')</script>");
                return;
            }
        }
        else
        {
            //update
            model.ID = ID;
            if (iro.Update(model) == 1)
            {
                Response.Redirect("IntegrationRuleManage.aspx");
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败')</script>");
                return;
            }
        }
    }
    protected void ddlBussnessEventType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ChangeEventType(ddlBussnessEventType.SelectedValue);
        ddlEventType_SelectedIndexChanged(sender, e);
    }

    /// <summary>
    /// 根据类型显示不同的内容
    /// </summary>
    /// <param name="BussnessEventType"></param>
    private void ChangeEventType(string strBussnessEventType)
    {
        strBussnessEventType = strBussnessEventType.ToUpperInvariant();
        trEventComplement.Visible = true;
        if (strBussnessEventType == BussnessEventType.OrderPayment || strBussnessEventType== BussnessEventType.OrderSeat)
        {
            ddlEventType.Items.Clear();
            ddlEventType.Items.Add(new ListItem("首次", "1"));
            ddlEventType.Items.Add(new ListItem("区间", "2"));
            ddlEventType.Items.Add(new ListItem("单次", "3"));
            ddlEventType.Items.Add(new ListItem("累计", "4"));
            if (Common.ToInt32(ddlEventType.SelectedValue) == (int)EventType.First)
            {
                ddlEventComplement.Items.Clear();
                ddlEventComplement.Items.Add(new ListItem("无", "0"));
                ddlEventComplement.Items.Add(new ListItem("金额", "1"));
            }
        }
        else if (strBussnessEventType == BussnessEventType.OrderEvaluation
            || strBussnessEventType == BussnessEventType.DishesPraise
            || strBussnessEventType == BussnessEventType.CollectShop
            || strBussnessEventType == BussnessEventType.ShareShop)
        {
            ddlEventType.Items.Clear();
            ddlEventType.Items.Add(new ListItem("首次", "1"));
            ddlEventType.Items.Add(new ListItem("单次", "3"));
            ddlEventType.Items.Add(new ListItem("累计", "4"));

            if (Common.ToInt32(ddlEventType.SelectedValue) == (int)EventType.First)
            {
                ddlEventComplement.Items.Clear();
                trEventComplement.Visible = false;
            }
        }
        else if (strBussnessEventType == BussnessEventType.EditUserInfo)
        {
            ddlEventType.Items.Clear();
            ddlEventType.Items.Add(new ListItem("首次", "1"));
            trEventComplement.Visible = false;
        }
    }
    protected void ddlEventType_SelectedIndexChanged(object sender, EventArgs e)
    {
        trEventComplement.Visible = true;
        if (Common.ToInt32(ddlEventType.SelectedValue) == (int)EventType.First)
        {
            if (ddlBussnessEventType.SelectedValue.ToUpperInvariant() == BussnessEventType.OrderPayment || ddlBussnessEventType.SelectedValue.ToUpperInvariant() == BussnessEventType.OrderSeat)
            {
                ddlEventComplement.Items.Clear();
                ddlEventComplement.Items.Add(new ListItem("无", "0"));
                ddlEventComplement.Items.Add(new ListItem("金额", "1"));
            }
            else
            {
                trEventComplement.Visible = false;
            }
        }
        else if (Common.ToInt32(ddlEventType.SelectedValue) == (int)EventType.Section)
        {
            trEventComplement.Visible = true;
            ddlEventComplement.Items.Clear();
            ddlEventComplement.Items.Add(new ListItem("金额", "1"));
        }
        else if (Common.ToInt32(ddlEventType.SelectedValue) == (int)EventType.Single)
        {
            if (ddlBussnessEventType.SelectedValue.ToUpperInvariant() == BussnessEventType.OrderPayment || ddlBussnessEventType.SelectedValue.ToUpperInvariant() == BussnessEventType.OrderSeat)
            {
                ddlEventComplement.Items.Clear();
                ddlEventComplement.Items.Add(new ListItem("无", "0"));
                ddlEventComplement.Items.Add(new ListItem("金额", "1"));
            }
            else
            {
                trEventComplement.Visible = false;
            }
        }
        else if (Common.ToInt32(ddlEventType.SelectedValue) == (int)EventType.Cumulative)
        {
            if (ddlBussnessEventType.SelectedValue.ToUpperInvariant() == BussnessEventType.OrderPayment || ddlBussnessEventType.SelectedValue.ToUpperInvariant() == BussnessEventType.OrderSeat)
            {
                ddlEventComplement.Items.Clear();
                ddlEventComplement.Items.Add(new ListItem("金额", "1"));
                ddlEventComplement.Items.Add(new ListItem("次数", "2"));
            }
            else
            {
                ddlEventComplement.Items.Clear();
                ddlEventComplement.Items.Add(new ListItem("次数", "2"));
            }
        }

        ddlEventComplement_SelectedIndexChanged(sender, e);
    }
    protected void ddlEventComplement_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlEventComplement.SelectedValue == "0")
        {
            lbBegin.Visible = false;
            lbEnd.Visible = false;
            tbConditionalMinValue.Visible = false;
            tbConditionalMaxValue.Visible = false;
        }
        else if (Common.ToInt32(ddlEventComplement.SelectedValue) == (int)EventComplement.Amount)
        {
            if (ddlBussnessEventType.SelectedValue.ToUpperInvariant() == BussnessEventType.OrderSeat || ddlBussnessEventType.SelectedValue.ToUpperInvariant() == BussnessEventType.OrderPayment)
            {
                if (Common.ToInt32(ddlEventType.SelectedValue) == (int)EventType.First)
                {
                    lbBegin.Visible = true;
                    lbEnd.Visible = true;
                    tbConditionalMinValue.Visible = true;
                    tbConditionalMaxValue.Visible = true;
                }
                else if (Common.ToInt32(ddlEventType.SelectedValue) == (int)EventType.Section)
                {
                    lbBegin.Visible = true;
                    lbEnd.Visible = true;
                    tbConditionalMinValue.Visible = true;
                    tbConditionalMaxValue.Visible = true;
                }
                else if (Common.ToInt32(ddlEventType.SelectedValue) == (int)EventType.Single)
                {
                    lbBegin.Visible = true;
                    lbEnd.Visible = true;
                    tbConditionalMinValue.Visible = true;
                    tbConditionalMaxValue.Visible = true;
                }
                else if (Common.ToInt32(ddlEventType.SelectedValue) == (int)EventType.Cumulative)
                {
                    lbBegin.Visible = false;
                    lbEnd.Visible = false;
                    tbConditionalMinValue.Visible = true;
                    tbConditionalMaxValue.Visible = false;
                }
            }
            else
            {
                if (Common.ToInt32(ddlEventType.SelectedValue) == (int)EventType.Cumulative)
                {
                    lbBegin.Visible = false;
                    lbEnd.Visible = false;
                    tbConditionalMinValue.Visible = true;
                    tbConditionalMaxValue.Visible = false;
                }
                else
                {
                    lbBegin.Visible = false;
                    lbEnd.Visible = false;
                    tbConditionalMinValue.Visible = false;
                    tbConditionalMaxValue.Visible = false;
                }
            }
           
        }
        else if (Common.ToInt32(ddlEventComplement.SelectedValue) == (int)EventComplement.Count)
        {
            lbBegin.Visible = false;
            lbEnd.Visible = false;
            tbConditionalMinValue.Visible = true;
            tbConditionalMaxValue.Visible = false;
        }
    }
}