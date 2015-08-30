using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using System.Data.SqlClient;
using VAGastronomistMobileApp.DBUtility;
using VAGastronomistMobileApp;
using System.Configuration;
using VAGastronomistMobileApp.SQLServerDAL;

public partial class CompanyApplyPayment_BusniessApplyPayment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        text.Value = Common.ToString(Request.QueryString["name"]);//回传页面显示公司名称
        if (!IsPostBack)
        {
            init_date.InnerHtml = "";
            GetNeedToPayApprovedOrder(0, 10);
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetNeedToPayApprovedOrder(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }
    /// <summary>
    /// 查询与商家未结算完成的预点单对账信息
    /// </summary>
    protected void GetNeedToPayApprovedOrder(int str, int end)
    {
        int shopID = Common.ToInt32(Request.QueryString["id"]);
        if (shopID > 0)
        {
            lb_maxAmount.Text = Common.ToDouble(new SybMoneyMerchantManager().GetShopInfoRemainMoney(shopID)).ToString();
        }
        ShopOperate operate = new ShopOperate();
        ShopInfo shopInfo = operate.QueryShop(shopID);
        int companyID = shopInfo.companyID;
        CompanyOperate companyOperate = new CompanyOperate();
        CompanyInfo companyInfo = companyOperate.QueryCompany(companyID);
        Label_companyName.Text = companyInfo.companyName;
        CompanyApplyPaymentOperate oprate = new CompanyApplyPaymentOperate();
        DataTable dtBusinessPay = oprate.QueryBusinessPay(companyID, shopID);
        if (dtBusinessPay.Rows.Count > 0)
        {
            int tableCount = dtBusinessPay.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dtBusinessPay, str, end);//分页的DataTable
            GridView_CheckedNeedToPay.DataSource = dt_page;
            GridView_CheckedNeedToPay.DataBind();
        }
        else
        {
            GridView_CheckedNeedToPay.DataSource = dtBusinessPay;//绑定显示的是空数据，目的为清空显示
            GridView_CheckedNeedToPay.DataBind();
        }
    }
    /// <summary>
    /// 结账扣款
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        int shopId = Common.ToInt32(Request.QueryString["id"]);
        ShopOperate operate = new ShopOperate();
        ShopInfo shopInfo = operate.QueryShop(shopId);
        int companyID = shopInfo.companyID;
        double accountMoney = Common.ToDouble(TextBox_pay.Text);
        string remark = TextBox_remark.Text.Trim();
        if (shopId == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + "请选择店铺" + "');</script>");
            return;
        }
        else if (companyID == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + "请选择公司" + "');</script>");
            return;
        }
        else if (accountMoney == 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + "请输入扣款金额" + "');</script>");
            return;
        }
        else if (string.IsNullOrEmpty(remark))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + "请填写备注" + "');</script>");
            return;
        }
        else if (accountMoney > Common.ToDouble(new SybMoneyMerchantManager().GetShopInfoRemainMoney(shopId)))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + "输入结款金额大于当前门店可结款最大金额" + "');</script>");
            return;
        }
        else
        {
            VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
            int employeeID = vAEmployeeLoginResponse.employeeID;
            long accountId = 0;
            if (SybMoneyMerchantOperate.MerchantCheckout(companyID, shopId, accountMoney, employeeID, remark, ref accountId))
            {
                GetNeedToPayApprovedOrder(0, 10);
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('新增成功');</script>");
                TextBox_pay.Text = "";
                TextBox_remark.Text = "";
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + "操作失败" + "');</script>");
            }
        }
    }
    /// <summary>
    /// 修改备注
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GridView_CheckedNeedToPay_SelectedIndexChanged(object sender, EventArgs e)
    {
        txt_remark.Text = GridView_CheckedNeedToPay.DataKeys[GridView_CheckedNeedToPay.SelectedIndex].Values["remark"].ToString();
        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>ConfirmWindow('Panel_Role');</script>");
    }
    protected void Button_confirm_Click(object sender, EventArgs e)
    {
        long accountId = Common.ToInt64(GridView_CheckedNeedToPay.DataKeys[GridView_CheckedNeedToPay.SelectedIndex].Values["accountId"]);
        string newRemark = txt_remark.Text.Trim();
        CompanyApplyPaymentOperate operate = new CompanyApplyPaymentOperate();
        if (operate.ModifyRemark(accountId, newRemark))
        {
            GetNeedToPayApprovedOrder(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);//刷新当前页面
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('修改失败，请重试');</script>");
        }
    }
}




