using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control;
using Web.Control.Enum;

public partial class FinanceManage_BalanceAccountApply : System.Web.UI.Page
{
    SybMoneyMerchantOperate sybMoneyMerchantOperate = new SybMoneyMerchantOperate();
    public static int publicpageSize = 10;

    protected void Page_Load(object sender, EventArgs e)
    {
        text.Value = Common.ToString(Request.QueryString["name"]);//回传页面显示公司名称
        if (!IsPostBack)
        {
            init_date.InnerHtml = "";
            if (!string.IsNullOrEmpty(text.Value))
            {
                GetNeedToPayApprovedOrder(1, 10, 0);
            }
            ddlStatus.SelectedValue = "1";
            Button_LargePageCount_Click(Button_10, null);//分页选中10
        }
    }

    /// <summary>
    /// 查询与商家未结算完成的预点单对账信息
    /// </summary>
    protected void GetNeedToPayApprovedOrder(int pageIndex, int pageSize, int status)
    {
        BalanceAccountOperate balanceAccountOperate = new BalanceAccountOperate();
        int shopID = Common.ToInt32(Request.QueryString["id"]);
        if (shopID > 0)
        {
            BalanceAccountShopMoney shopMoney = balanceAccountOperate.QueryShopMoney(shopID);
            if (shopMoney != null)
            {
                lb_maxAmount.Text = Math.Round(shopMoney.remainMoney - shopMoney.amountFrozen, 2).ToString();
                lbAmountFrozen.Text = Math.Round(shopMoney.amountFrozen, 2).ToString();
            }
            ShopOperate operate = new ShopOperate();
            ShopInfo shopInfo = operate.QueryShop(shopID);
            int companyID = shopInfo.companyID;
            CompanyOperate companyOperate = new CompanyOperate();
            CompanyInfo companyInfo = companyOperate.QueryCompany(companyID);
            lbCompanyName.Text = companyInfo.companyName;
        }
        int cnt = 0;
        List<BalanceAccountDetail> BalanceAccountDetails = balanceAccountOperate.QueryBusinessPay(new VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure.Page(pageIndex, pageSize),
            shopID, status, 0, null, null,0,out cnt);
        if (BalanceAccountDetails != null && BalanceAccountDetails.Any())
        {
            AspNetPager1.PageSize = pageSize;
            AspNetPager1.RecordCount = cnt;
            GridView_CheckedNeedToPay.DataSource = BalanceAccountDetails;
        }
        else
        {
            GridView_CheckedNeedToPay.DataSource = null;
        }
        GridView_CheckedNeedToPay.DataBind();
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

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetNeedToPayApprovedOrder(AspNetPager1.CurrentPageIndex, publicpageSize, Common.ToInt32(ddlStatus.SelectedValue));
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        int shopId = Common.ToInt32(Request.QueryString["id"]);
        ShopOperate operate = new ShopOperate();
        ShopInfo shopInfo = operate.QueryShop(shopId);
        double accountMoney = Common.ToDouble(TextBox_pay.Text);

        BalanceAccountOperate balanceAccountOperate = new BalanceAccountOperate();
        BalanceAccountShopMoney shopMoney = balanceAccountOperate.QueryShopMoney(shopId);

        string remark = TextBox_remark.Text.Trim();
        if (shopId == 0)
        {
            CommonPageOperate.AlterMsg(this, "请选择店铺");
            return;
        }
        if (shopInfo.companyID == 0)
        {
            CommonPageOperate.AlterMsg(this, "请选择公司");
            return;
        }
        if (accountMoney <= 0)
        {
            CommonPageOperate.AlterMsg(this, "请输入正确的平账金额");
            return;
        }
        if (string.IsNullOrEmpty(remark))
        {
            CommonPageOperate.AlterMsg(this, "请填写备注");
            return;
        }
        if (ddlBalanceType.SelectedValue == "1" && Math.Round(accountMoney, 2) - Math.Round(shopMoney.remainMoney - shopMoney.amountFrozen, 2) > 0.001)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + "输入平账金额大于当前门店可结款最大金额" + "');</script>");
            return;
        }

        if (ddlBalanceType.SelectedValue == "1")
        {
            accountMoney = -accountMoney;

            bool undo = balanceAccountOperate.CheckHaveUndoApply(shopId);
            if (undo)
            {
                CommonPageOperate.AlterMsg(this, "门店有未提交的打款申请，请先处理");
                return;
            }
        }

        VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
        int employeeID = vAEmployeeLoginResponse.employeeID;
        long accountId = 0;

        string IPAddress = IPHelper.GetRemoteIPAddress();
        MoneyMerchantAccountDetail detail = new MoneyMerchantAccountDetail();
        detail.companyId = shopInfo.companyID;
        detail.shopId = shopId;
        detail.accountMoney = accountMoney;
        detail.operUser = vAEmployeeLoginResponse.userName;
        detail.remark = remark;
        if (BalanceAccountOperate.MerchantCheckout(detail, employeeID, IPAddress, ref accountId))
        {
            GetNeedToPayApprovedOrder(1, 10, 0);
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('新增成功');</script>");
            TextBox_pay.Text = "";
            TextBox_remark.Text = "";
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + "操作失败" + "');</script>");
        }
    }

    protected void GridView_CheckedNeedToPay_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView_CheckedNeedToPay.Rows.Count; i++)
        {
            GridView_CheckedNeedToPay.Rows[i].Cells[4].Text = GridView_CheckedNeedToPay.Rows[i].Cells[4].Text.Replace("0001/1/1 0:00:00", "");
            if (Common.ToDouble(GridView_CheckedNeedToPay.Rows[i].Cells[5].Text) > 0)
            {
                GridView_CheckedNeedToPay.Rows[i].Cells[5].Text = "充值" + GridView_CheckedNeedToPay.Rows[i].Cells[5].Text;
            }
            else
            {
                GridView_CheckedNeedToPay.Rows[i].Cells[5].Text = "扣款" + GridView_CheckedNeedToPay.Rows[i].Cells[5].Text.Replace("-", "");
            }
            switch (GridView_CheckedNeedToPay.Rows[i].Cells[7].Text)
            {
                case "wait_for_check":
                    GridView_CheckedNeedToPay.Rows[i].Cells[7].Text = Common.GetEnumDescription(BalanceAccountStatus.wait_for_check);
                    break;
                case "wait_for_confirm":
                    GridView_CheckedNeedToPay.Rows[i].Cells[7].Text = Common.GetEnumDescription(BalanceAccountStatus.wait_for_confirm);
                    break;
                case "confirmed":
                    GridView_CheckedNeedToPay.Rows[i].Cells[7].Text = Common.GetEnumDescription(BalanceAccountStatus.confirmed);
                    break;
                case "rejected":
                    GridView_CheckedNeedToPay.Rows[i].Cells[7].Text = Common.GetEnumDescription(BalanceAccountStatus.rejected);
                    break;
            }
        }
    }
    protected void Button_confirm_Click(object sender, EventArgs e)
    {
        long accountId = Common.ToInt64(GridView_CheckedNeedToPay.DataKeys[GridView_CheckedNeedToPay.SelectedIndex].Values["accountId"]);
        string newRemark = txt_remark.Text.Trim();
        CompanyApplyPaymentOperate operate = new CompanyApplyPaymentOperate();
        if (operate.ModifyRemark(accountId, newRemark))
        {
            GetNeedToPayApprovedOrder(AspNetPager1.StartRecordIndex, AspNetPager1.EndRecordIndex, Common.ToInt32(ddlStatus.SelectedValue));//刷新当前页面
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('修改失败，请重试');</script>");
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        BalanceAccountOperate balanceAccountOperate = new BalanceAccountOperate();
        int shopID = Common.ToInt32(Request.QueryString["id"]);

        List<BalanceAccountDetail> BalanceAccountDetails = balanceAccountOperate.QueryBusinessPay(shopID, Common.ToInt32(ddlStatus.SelectedValue), null, null, null);
        if (BalanceAccountDetails != null && BalanceAccountDetails.Any())
        {
            BalanceAccountDetails = BalanceAccountDetails.OrderByDescending(i => i.accountId).ToList();

            DataTable dt = new DataTable();
            dt.Columns.Add("单号", typeof(string));
            dt.Columns.Add("流水号", typeof(string));
            dt.Columns.Add("公司名称", typeof(string));
            dt.Columns.Add("门店名称", typeof(string));
            dt.Columns.Add("申请平账时间", typeof(string));
            dt.Columns.Add("财务平账时间", typeof(string));
            dt.Columns.Add("平账金额", typeof(string));
            dt.Columns.Add("备注", typeof(string));
            dt.Columns.Add("状态", typeof(string));

            foreach (BalanceAccountDetail detail in BalanceAccountDetails)
            {
                DataRow dr = dt.NewRow();
                dr["单号"] = detail.accountId;
                dr["流水号"] = detail.flowNumber;
                dr["公司名称"] = detail.companyName;
                dr["门店名称"] = detail.shopName;
                dr["申请平账时间"] = detail.operTime;
                dr["财务平账时间"] = detail.confirmTime;

                if (detail.confirmTime.ToString("yyyy/MM/dd") == "0001/01/01")
                {
                    dr["财务平账时间"] = "";
                }
                else
                {
                    dr["财务平账时间"] = detail.confirmTime;
                }
                if (detail.accountMoney > 0)
                {
                    dr["平账金额"] = "充值" + detail.accountMoney + "元";
                }
                else
                {
                    dr["平账金额"] = "扣款" + Math.Abs(detail.accountMoney) + "元";
                }
                dr["备注"] = detail.remark;
                switch (detail.status)
                {
                    case BalanceAccountStatus.wait_for_check:
                        dr["状态"] = Common.GetEnumDescription(BalanceAccountStatus.wait_for_check);
                        break;
                    case BalanceAccountStatus.wait_for_confirm:
                        dr["状态"] = Common.GetEnumDescription(BalanceAccountStatus.wait_for_confirm);
                        break;
                    case BalanceAccountStatus.confirmed:
                        dr["状态"] = Common.GetEnumDescription(BalanceAccountStatus.confirmed);
                        break;
                    case BalanceAccountStatus.rejected:
                        dr["状态"] = Common.GetEnumDescription(BalanceAccountStatus.rejected);
                        break;
                }
                dt.Rows.Add(dr);
            }
            CreateExcel(dt);
        }
    }

    void CreateExcel(DataTable dt)
    {
        string excelName = HttpUtility.UrlEncode("平账申请_" + DateTime.Now.ToString("yyyyMMddhhmmss"), System.Text.Encoding.UTF8).ToString();
        HttpResponse resp;
        resp = Page.Response;
        resp.Buffer = true;
        resp.ClearContent();
        resp.ClearHeaders();
        resp.Charset = "GB2312";
        resp.AppendHeader("Content-Disposition", "attachment;filename=" + excelName + ".xls");
        resp.ContentEncoding = System.Text.Encoding.Default;//设置输出流为简体中文   
        resp.ContentType = "application/ms-excel";//设置输出文件类型为excel文件。 
        string colHeaders = "", ls_item = "";
        DataRow[] myRow = dt.Select();
        int i = 0;
        int cl = dt.Columns.Count;
        for (i = 0; i < cl; i++)
        {
            if (i == (cl - 1))//最后一列，加n
            {
                colHeaders += dt.Columns[i].Caption.ToString().Trim() + "\n";
            }
            else
            {
                colHeaders += dt.Columns[i].Caption.ToString().Trim() + "\t";
            }
        }
        resp.Write(colHeaders);
        foreach (DataRow row in myRow)
        {
            for (i = 0; i < cl; i++)
            {
                if (i == (cl - 1))//最后一列，加n
                {
                    ls_item += row[i].ToString().Trim() + "\n";
                }
                else if (i == 1)
                {
                    ls_item += "'" + row[i].ToString().Trim() + "\t";
                }
                else
                {
                    ls_item += row[i].ToString().Trim() + "\t";
                }
            }
            resp.Write(ls_item);
            ls_item = "";
        }
        resp.End();
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        GetNeedToPayApprovedOrder(1, publicpageSize, Common.ToInt32(ddlStatus.SelectedValue));
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
        publicpageSize = Common.ToInt32(Label_LargePageCount.Text);
        GetNeedToPayApprovedOrder(AspNetPager1.CurrentPageIndex, publicpageSize, Common.ToInt32(ddlStatus.SelectedValue));
    }
}