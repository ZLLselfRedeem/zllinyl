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
using System.Web.UI.HtmlControls;
using Web.Control.DDL;

public partial class FinanceManage_BalanceAccountManage : System.Web.UI.Page
{
    public bool checkRole = false;
    public bool confirmRole = false;
    public int publicpageSize = 10;
    protected void Page_Load(object sender, EventArgs e)
    {
        text.Value = Common.ToString(Request.QueryString["name"]);//回传页面显示公司名称
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(ddlCity);
            AuthorityInitail();
            init_date.InnerHtml = "";
            //GetNeedToPayApprovedOrder(1, 10, 0, 0, null, null);
            divCheckAll.Visible = false;
            Button_LargePageCount_Click(Button_10, null);//分页选中10
        }
    }

    private void AuthorityInitail()
    {
        BalanceAccountOperate balanceAccountOperate = new BalanceAccountOperate();
        int employeeId = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;

        checkRole = balanceAccountOperate.IsHaveCheckAuthority(employeeId);
        confirmRole = balanceAccountOperate.IsHaveConfirmAuthority(employeeId);

        if (checkRole)
        {
            btnBatchConfirm.Visible = false;
            ddlStatus.SelectedValue = "1";
        }

        if (confirmRole)
        {
            btnBatchCheck.Visible = false;
            ddlStatus.SelectedValue = "2";
        }
    }

    protected void GetNeedToPayApprovedOrder(int pageIndex, int pageSize, int status, long accountId, DateTime? beginTime, DateTime? endTime)
    {
        int shopID = Common.ToInt32(Request.QueryString["id"]);
        if (shopID > 0)
        {
            ShopOperate operate = new ShopOperate();
            ShopInfo shopInfo = operate.QueryShop(shopID);
            int companyID = shopInfo.companyID;
            CompanyOperate companyOperate = new CompanyOperate();
            CompanyInfo companyInfo = companyOperate.QueryCompany(companyID);
            lbCompanyName.Text = companyInfo.companyName;
        }
        BalanceAccountOperate balanceAccountOperate = new BalanceAccountOperate();
        int cnt = 0;
        List<BalanceAccountDetail> BalanceAccountDetails = balanceAccountOperate.QueryBusinessPay(new VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure.Page(pageIndex, pageSize),
            shopID, status, accountId, beginTime, endTime,Common.ToInt32(ddlCity.SelectedValue), out cnt);
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

    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        GetList();
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        if ((!string.IsNullOrEmpty(txtOperateBeginTime.Text) && string.IsNullOrEmpty(txtOperateEndTime.Text)) || (string.IsNullOrEmpty(txtOperateBeginTime.Text) && !string.IsNullOrEmpty(txtOperateEndTime.Text)))
        {
            CommonPageOperate.AlterMsg(this, "时间区间请选择完整");
            return;
        }
        AspNetPager1.CurrentPageIndex = 1;
        GetList();
    }

    private void GetList()
    {
        publicpageSize = Common.ToInt32(Label_LargePageCount.Text);
        DateTime beginTime = new DateTime();
        DateTime endTime = new DateTime();
        if (!string.IsNullOrEmpty(txtOperateBeginTime.Text) && !string.IsNullOrEmpty(txtOperateEndTime.Text))
        {
            beginTime = Common.ToDateTime(txtOperateBeginTime.Text);
            endTime = Common.ToDateTime(txtOperateEndTime.Text);
            GetNeedToPayApprovedOrder(AspNetPager1.CurrentPageIndex, publicpageSize, Common.ToInt32(ddlStatus.SelectedValue), Common.ToInt64(txbAccountId.Text), Common.ToDateTime(txtOperateBeginTime.Text), Common.ToDateTime(txtOperateEndTime.Text));
        }
        else
        {
            GetNeedToPayApprovedOrder(AspNetPager1.CurrentPageIndex, publicpageSize, Common.ToInt32(ddlStatus.SelectedValue), Common.ToInt64(txbAccountId.Text), null, null);
        }
        divCheckAll.Visible = true;
    }

    protected void GridView_CheckedNeedToPay_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView_CheckedNeedToPay.Rows.Count; i++)
        {
            GridView_CheckedNeedToPay.Rows[i].Cells[5].Text = GridView_CheckedNeedToPay.Rows[i].Cells[5].Text.Replace("0001/1/1 0:00:00", "");
            if (Common.ToDouble(GridView_CheckedNeedToPay.Rows[i].Cells[6].Text) > 0)
            {
                GridView_CheckedNeedToPay.Rows[i].Cells[6].Text = "充值" + GridView_CheckedNeedToPay.Rows[i].Cells[6].Text;
            }
            else
            {
                GridView_CheckedNeedToPay.Rows[i].Cells[6].Text = "扣款" + GridView_CheckedNeedToPay.Rows[i].Cells[6].Text.Replace("-", "");
            }

            LinkButton lkbCheck = (LinkButton)GridView_CheckedNeedToPay.Rows[i].FindControl("lkbCheck");
            LinkButton lkbConfirm = (LinkButton)GridView_CheckedNeedToPay.Rows[i].FindControl("lkbConfirm");
            LinkButton lkbReject = (LinkButton)GridView_CheckedNeedToPay.Rows[i].FindControl("lkbReject");

            if (checkRole)
            {
                lkbConfirm.Visible = false;
            }

            if (confirmRole)
            {
                lkbCheck.Visible = false;
            }

            switch (GridView_CheckedNeedToPay.Rows[i].Cells[8].Text)
            {
                case "wait_for_check":
                    GridView_CheckedNeedToPay.Rows[i].Cells[8].Text = Common.GetEnumDescription(BalanceAccountStatus.wait_for_check);
                    lkbConfirm.Visible = false;
                    if (confirmRole)
                    {
                        lkbReject.Visible = false;
                    }
                    break;
                case "wait_for_confirm":
                    GridView_CheckedNeedToPay.Rows[i].Cells[8].Text = Common.GetEnumDescription(BalanceAccountStatus.wait_for_confirm);
                    lkbCheck.Visible = false;
                    break;
                case "confirmed":
                    GridView_CheckedNeedToPay.Rows[i].Cells[8].Text = Common.GetEnumDescription(BalanceAccountStatus.confirmed);
                    lkbCheck.Visible = false;
                    lkbConfirm.Visible = false;
                    lkbReject.Visible = false;
                    HtmlInputCheckBox cbSelect = (HtmlInputCheckBox)GridView_CheckedNeedToPay.Rows[i].FindControl("ckbSelect");
                    cbSelect.Disabled = true;
                    break;
                case "rejected":
                    GridView_CheckedNeedToPay.Rows[i].Cells[8].Text = Common.GetEnumDescription(BalanceAccountStatus.rejected);
                    lkbCheck.Visible = false;
                    lkbConfirm.Visible = false;
                    lkbReject.Visible = false;
                    break;
            }
        }
    }

    protected void lnkbtn_OnCommand(object sender, CommandEventArgs e)
    {
        int accountId = Common.ToInt32(e.CommandArgument);
        ViewState["accountId"] = accountId;
        bool objResult = false;

        BalanceAccountOperate accountOperate = new BalanceAccountOperate();
        MoneyMerchantAccountDetail accountDetail = accountOperate.QueryMoneyMerchantAccountDetail(accountId);

        //操作日志模板
        StoresMoneyLog log = new StoresMoneyLog()
        {
            AddIP = IPHelper.GetRemoteIPAddress(),
            AddTime = DateTime.Now,
            AddUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName.ToString(),
            ShopInfo_ShopID = accountDetail.shopId,
            BatchMoneyApply_Id = 0,
            BatchMoneyApplyDetail_Id = "",
            MoneyMerchantAccountDetail_AccountId = accountId,
            Money = accountDetail.accountMoney
        };

        switch (e.CommandName)
        {
            case "check"://出纳确认，1更改状态，2写log
                if (accountDetail.status == (int)BalanceAccountStatus.wait_for_check)
                {
                    accountDetail.status = (int)BalanceAccountStatus.wait_for_confirm;
                    log.Content = "出纳确认单据" + accountId;
                    objResult = accountOperate.CheckBalanceAmount(accountDetail, log);
                    if (objResult)
                    {
                        GetList();
                        CommonPageOperate.AlterMsg(this, "确认成功");
                    }
                    else
                    {
                        CommonPageOperate.AlterMsg(this, "确认失败");
                    }
                }
                else
                {
                    CommonPageOperate.AlterMsg(this, "单据状态错误，无法确认");
                }
                break;
            case "confirm"://财务平账，1更改状态，2扣余额、解冻，3写log
                if (accountDetail.status == (int)BalanceAccountStatus.wait_for_confirm)
                {
                    log.Content = "财务平账单据" + accountId;
                    accountDetail.status = (int)BalanceAccountStatus.confirmed;
                    accountDetail.confirmTime = DateTime.Now;
                    objResult = accountOperate.ConfirmBalanceAmount(accountDetail, log);
                    if (objResult)
                    {
                        GetList();
                        CommonPageOperate.AlterMsg(this, "平账成功");
                    }
                    else
                    {
                        CommonPageOperate.AlterMsg(this, "平账失败");
                    }
                }
                else
                {
                    CommonPageOperate.AlterMsg(this, "单据状态错误，无法平账");
                }
                break;
            case "reject"://出纳 || 财务，1更改状态，2写log
                if (accountDetail.status == (int)BalanceAccountStatus.wait_for_confirm || accountDetail.status == (int)BalanceAccountStatus.wait_for_check)
                {
                    if (accountDetail.status == (int)BalanceAccountStatus.wait_for_confirm)
                    {
                        log.Content = "财务撤回单据" + accountId;
                    }
                    else
                    {
                        log.Content = "出纳撤回单据" + accountId;
                    }
                    accountDetail.status = (int)BalanceAccountStatus.rejected;
                    objResult = accountOperate.RejectBalanceAmount(accountDetail, log);
                    if (objResult)
                    {
                        GetList();
                        CommonPageOperate.AlterMsg(this, "撤回成功");
                    }
                    else
                    {
                        CommonPageOperate.AlterMsg(this, "撤回失败");
                    }
                }
                else
                {
                    CommonPageOperate.AlterMsg(this, "单据状态错误，无法撤回");
                }
                break;
            default:
                break;
        }
    }

    //protected void ckbCheckAll_CheckedChanged(object sender, EventArgs e)
    //{
    //    if (ckbCheckAll.Checked)
    //    {
    //        CheckAll(true);
    //    }
    //    else
    //    {
    //        CheckAll(false);
    //    }
    //}

    private void CheckAll(bool checkFlag)
    {
        for (int i = 0; i < GridView_CheckedNeedToPay.Rows.Count; i++)
        {
            HtmlInputCheckBox ckbSelect = (HtmlInputCheckBox)GridView_CheckedNeedToPay.Rows[i].FindControl("ckbSelect");

            ckbSelect.Checked = checkFlag;
        }
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
        publicpageSize = Common.ToInt32(Label_LargePageCount.Text);
        AspNetPager1.CurrentPageIndex = 0;
        GetList();
    }

    protected void btnBatchConfirm_Click(object sender, EventArgs e)
    {
        bool objResult = false;
        string err = "";
        for (int i = 0; i < GridView_CheckedNeedToPay.Rows.Count; i++)
        {
            HtmlInputCheckBox cbSelect = (HtmlInputCheckBox)GridView_CheckedNeedToPay.Rows[i].FindControl("ckbSelect");

            if (cbSelect.Checked)
            {
                long accountId = Common.ToInt64(GridView_CheckedNeedToPay.DataKeys[i].Values["accountId"]);
                BalanceAccountOperate accountOperate = new BalanceAccountOperate();
                MoneyMerchantAccountDetail accountDetail = accountOperate.QueryMoneyMerchantAccountDetail(accountId);

                //操作日志模板
                StoresMoneyLog log = new StoresMoneyLog()
                {
                    AddIP = IPHelper.GetRemoteIPAddress(),
                    AddTime = DateTime.Now,
                    AddUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID.ToString(),
                    ShopInfo_ShopID = accountDetail.shopId,
                    BatchMoneyApply_Id = 0,
                    BatchMoneyApplyDetail_Id = "",
                    MoneyMerchantAccountDetail_AccountId = accountId,
                    Money = accountDetail.accountMoney
                };

                if (accountDetail.status == (int)BalanceAccountStatus.wait_for_confirm)
                {
                    log.Content = "财务平账单据" + accountId;
                    accountDetail.status = (int)BalanceAccountStatus.confirmed;
                    accountDetail.confirmTime = DateTime.Now;
                    objResult = accountOperate.ConfirmBalanceAmount(accountDetail, log);
                    if (!objResult)
                    {
                        err += accountId + ",";
                    }
                }
            }
        }
        if (err.Length > 0)
        {
            CommonPageOperate.AlterMsg(this, "以下单据平账失败，单号" + err);
        }
        GetList();
    }

    protected void btnBatchCheck_Click(object sender, EventArgs e)
    {
        bool objResult = false;
        string err = "";
        for (int i = 0; i < GridView_CheckedNeedToPay.Rows.Count; i++)
        {
            HtmlInputCheckBox cbSelect = (HtmlInputCheckBox)GridView_CheckedNeedToPay.Rows[i].FindControl("ckbSelect");

            if (cbSelect.Checked)
            {
                long accountId = Common.ToInt64(GridView_CheckedNeedToPay.DataKeys[i].Values["accountId"]);
                BalanceAccountOperate accountOperate = new BalanceAccountOperate();
                MoneyMerchantAccountDetail accountDetail = accountOperate.QueryMoneyMerchantAccountDetail(accountId);

                //操作日志模板
                StoresMoneyLog log = new StoresMoneyLog()
                {
                    AddIP = IPHelper.GetRemoteIPAddress(),
                    AddTime = DateTime.Now,
                    AddUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID.ToString(),
                    ShopInfo_ShopID = accountDetail.shopId,
                    BatchMoneyApply_Id = 0,
                    BatchMoneyApplyDetail_Id = "",
                    MoneyMerchantAccountDetail_AccountId = accountId,
                    Money = accountDetail.accountMoney
                };
                if (accountDetail.status == (int)BalanceAccountStatus.wait_for_check)
                {
                    accountDetail.status = (int)BalanceAccountStatus.wait_for_confirm;
                    log.Content = "出纳确认单据" + accountId;
                    objResult = accountOperate.CheckBalanceAmount(accountDetail, log);
                    if (!objResult)
                    {
                        err += accountId + ",";
                    }
                }
            }
        }
        if (err.Length > 0)
        {
            CommonPageOperate.AlterMsg(this, "以下单据确认失败，单号" + err);
        }
        GetList();
    }

    protected void btnBatchReject_Click(object sender, EventArgs e)
    {
        bool objResult = false;
        string err = "";
        for (int i = 0; i < GridView_CheckedNeedToPay.Rows.Count; i++)
        {
            HtmlInputCheckBox cbSelect = (HtmlInputCheckBox)GridView_CheckedNeedToPay.Rows[i].FindControl("ckbSelect");

            if (cbSelect.Checked)
            {
                long accountId = Common.ToInt64(GridView_CheckedNeedToPay.DataKeys[i].Values["accountId"]);
                BalanceAccountOperate accountOperate = new BalanceAccountOperate();
                MoneyMerchantAccountDetail accountDetail = accountOperate.QueryMoneyMerchantAccountDetail(accountId);

                //操作日志模板
                StoresMoneyLog log = new StoresMoneyLog()
                {
                    AddIP = IPHelper.GetRemoteIPAddress(),
                    AddTime = DateTime.Now,
                    AddUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID.ToString(),
                    ShopInfo_ShopID = accountDetail.shopId,
                    BatchMoneyApply_Id = 0,
                    BatchMoneyApplyDetail_Id = "",
                    MoneyMerchantAccountDetail_AccountId = accountId,
                    Money = accountDetail.accountMoney
                };
                if (accountDetail.status == (int)BalanceAccountStatus.wait_for_confirm || accountDetail.status == (int)BalanceAccountStatus.wait_for_check)
                {
                    if (accountDetail.status == (int)BalanceAccountStatus.wait_for_confirm)
                    {
                        log.Content = "财务撤回单据" + accountId;
                    }
                    else
                    {
                        log.Content = "出纳撤回单据" + accountId;
                    }
                    accountDetail.status = (int)BalanceAccountStatus.rejected;
                    objResult = accountOperate.RejectBalanceAmount(accountDetail, log);
                    if (!objResult)
                    {
                        err += accountId + ",";
                    }
                }
            }
        }
        if (err.Length > 0)
        {
            CommonPageOperate.AlterMsg(this, "以下单据平账失败，单号" + err);
        }
        GetList();
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        if ((!string.IsNullOrEmpty(txtOperateBeginTime.Text) && string.IsNullOrEmpty(txtOperateEndTime.Text)) || (string.IsNullOrEmpty(txtOperateBeginTime.Text) && !string.IsNullOrEmpty(txtOperateEndTime.Text)))
        {
            CommonPageOperate.AlterMsg(this, "时间区间请选择完整");
            return;
        }
        BalanceAccountOperate balanceAccountOperate = new BalanceAccountOperate();
        int shopID = Common.ToInt32(Request.QueryString["id"]);

        int pageIndex = AspNetPager1.CurrentPageIndex;
        int pageSize = publicpageSize;

        List<BalanceAccountDetail> BalanceAccountDetails = new List<BalanceAccountDetail>();
        DateTime beginTime = new DateTime();
        DateTime endTime = new DateTime();
        if (!string.IsNullOrEmpty(txtOperateBeginTime.Text) && !string.IsNullOrEmpty(txtOperateEndTime.Text))
        {
            beginTime = Common.ToDateTime(txtOperateBeginTime.Text);
            endTime = Common.ToDateTime(txtOperateEndTime.Text);

            BalanceAccountDetails = balanceAccountOperate.QueryBusinessPay(shopID, Common.ToInt32(ddlStatus.SelectedValue), Common.ToInt64(txbAccountId.Text), beginTime, endTime);
        }
        else
        {
            BalanceAccountDetails = balanceAccountOperate.QueryBusinessPay(shopID, Common.ToInt32(ddlStatus.SelectedValue), Common.ToInt64(txbAccountId.Text), null, null);
        }

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
        string excelName = HttpUtility.UrlEncode("平账管理_" + DateTime.Now.ToString("yyyyMMddhhmmss"), System.Text.Encoding.UTF8).ToString();
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
}