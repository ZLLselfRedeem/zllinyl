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
using VA.CacheLogic;
using Web.Control;

public partial class FinanceManage_batchMoneyApplyManager : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            init_date.InnerHtml = "";
            new CityDropDownList().BindCity(ddlCity);
            if (!string.IsNullOrEmpty(Common.ToString(Request.QueryString["name"])))
            {
                string[] strRequest = Common.ToString(Request.QueryString["name"]).Split(',');//回传页面显示公司名称
                text.Value = strRequest[0];
                if (strRequest.Length > 1)
                {
                    lb_companyName.Text = strRequest[1];
                }
            }
            else
            {
                text.Value = string.Empty;
                lb_companyName.Text = string.Empty;
            }

            Button_LargePageCount_Click(Button_10, null);//分页选中10

            gdTable.Visible = false;
            //btnAllPay.Attributes.Add("onclick", "return confirm('打款后将不能对该申请进行操作，确认打款？');");
            btnAllPay.Attributes.Add("onclick", "return checkIsFirst();");
            btnSubmit.Attributes.Add("onclick", "return confirm('确认后将不能对该申请进行操作，确认提交？')");
            btnCancel.Attributes.Add("onclick", "return confirm('批量撤回后将不再显示该申请，确认撤回？')");
            //btnPaySuccess.Attributes.Add("onclick", "return confirm('批量确认银行打款 成功 对该申请进行操作，是否确认？')");
            //btnFail.Attributes.Add("onclick", "return confirm('批量确认银行打款 失败 对该申请进行操作，是否确认？')");

            BalanceAccountOperate bao = new BalanceAccountOperate();
            
            if (bao.IsHaveCheckAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID))
            {
                btnAllPay.Visible = false;
                btnSubmit.Visible = true;
                btnCancel.Visible = true;
                //btnPaySuccess.Visible = false;
                //btnFail.Visible = false;
            }
            else if (bao.IsHaveConfirmAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID))
            {
                btnAllPay.Visible = true;
                btnSubmit.Visible = false;
                btnCancel.Visible = true;
                //btnPaySuccess.Visible = true;
                //btnFail.Visible = true;
            }
            else
            {
                btnAllPay.Visible = true;
                btnSubmit.Visible = true;
                btnCancel.Visible = true;
                //btnPaySuccess.Visible = true;
                //btnFail.Visible = true;
            }

            ////测试用
            //btnAllPay.Visible = true;
            //btnSubmit.Visible = true;
            //btnCancel.Visible = true;
            //btnPaySuccess.Visible = true;
            //btnFail.Visible = true;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //if (string.IsNullOrEmpty(txtOperateBeginTime.Text) || string.IsNullOrEmpty(txtOperateEndTime.Text))
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请先选择查询时间区间')</script>");
        //    return;
        //}
        AspNetPager1.CurrentPageIndex = 1;
        loadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlStatus.SelectedValue));
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
        loadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlStatus.SelectedValue));
    }

    /// <summary>
    /// 加载列表
    /// </summary>
    /// <param name="shopId"></param>
    private void loadData(int str, int end, int status)
    {
        int shopID = Common.ToInt32(Request.QueryString["id"]);
        if (text.Value.Trim().Equals(string.Empty))
        {
            shopID = 0;
        }
        BatchMoneyOperate bmo = new BatchMoneyOperate();
        DataTable dt = bmo.SelectBatchMoneyApplyDetailByManager(txt_batchMoneyApplyDetailCode.Text.Trim(), shopID, Common.ToInt32(ddlStatus.SelectedValue), txtOperateBeginTime.Text, txtOperateEndTime.Text, Common.ToInt32(ddlCity.SelectedValue), Common.ToInt32(ddlIsFirst.SelectedValue));
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
        string sortedField = "";
        if (ViewState["SortedField"] != null)
        {
            Dictionary<string, string> sorted = (Dictionary<string, string>)ViewState["SortedField"];
            foreach (KeyValuePair<string, string> kvp in sorted)
            {
                sortedField = kvp.Key + "  " + kvp.Value;
            }
            dtPage.DefaultView.Sort = sortedField;
        }
        for (int i = 0; i < dtPage.Rows.Count; i++)
        {
            //if (dtPage.Rows[i]["playMoneyFlag"].ToString().Equals("0"))
            //{
            //    dtPage.Rows[i]["playMoneyFlag"] = "首次打款";
            //}
            //else
            //{
            //    dtPage.Rows[i]["playMoneyFlag"] = "非首次打款";
            //}

            //状态转换
            switch (dtPage.Rows[i]["status"].ToString())
            {
                case "5":
                    dtPage.Rows[i]["status"] = "申请未提交";
                    break;
                case "6":
                    dtPage.Rows[i]["status"] = "申请被撤回";
                    break;
                case "7":
                    dtPage.Rows[i]["status"] = "申请提交至出纳";
                    break;
                case "8":
                    dtPage.Rows[i]["status"] = "出纳已确认帐目";
                    break;
                case "9":
                    dtPage.Rows[i]["status"] = "主管提交至银行";
                    break;
                case "10":
                    dtPage.Rows[i]["status"] = "银行已受理";
                    break;
                case "11":
                    dtPage.Rows[i]["status"] = "银行未受理";
                    break;
                case "12":
                    dtPage.Rows[i]["status"] = "银行打款成功";
                    break;
                case "13":
                    dtPage.Rows[i]["status"] = "银行打款失败";
                    break;
                default:
                    break;
            }
            if (dtPage.Rows[i]["isFirst"].ToString().Equals("0"))
            {
                dtPage.Rows[i]["isFirst"] = "非首次打款";
            }
            else
            {
                dtPage.Rows[i]["isFirst"] = "首次打款";
            }
        }
        gdList.DataSource = null;
        gdList.DataSource = dtPage;
        gdList.DataBind();
        allCheck.Checked = false;
        BalanceAccountOperate bao = new BalanceAccountOperate();
        for (int i = 0; i < gdList.Rows.Count; i++)
        {
            LinkButton lbtnConfirm = (LinkButton)gdList.Rows[i].FindControl("lbtnConfirm");
            LinkButton lbtnPay = (LinkButton)gdList.Rows[i].FindControl("lbtnPay");
            LinkButton lbtnCancel = (LinkButton)gdList.Rows[i].FindControl("lbtnCancel");

            lbtnConfirm.Enabled = false;
            lbtnCancel.Enabled = false;
            lbtnPay.Enabled = false;
            HtmlInputCheckBox gdCheck = (HtmlInputCheckBox)gdList.Rows[i].FindControl("gdCheck");
            gdCheck.Disabled = true;

            //申请未提交
            if (bao.IsHaveCheckAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID) && gdList.DataKeys[i].Values["status"].ToString().Equals("申请提交至出纳"))
            {
                gdCheck.Disabled = false;
                lbtnConfirm.Enabled = true;
                lbtnCancel.Enabled = true;
                lbtnConfirm.Attributes.Add("onclick", "return confirm('确认后将不能对该申请进行操作，确认提交？');");
                lbtnCancel.Attributes.Add("onclick", "return confirm('撤回后将不能对该申请进行操作，确认撤回？');");
            }
            else if (bao.IsHaveConfirmAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID) && (gdList.DataKeys[i].Values["status"].ToString().Equals("出纳已确认帐目") || gdList.DataKeys[i].Values["status"].ToString().Equals("主管提交至银行")))
            {
                gdCheck.Disabled = false;
                lbtnPay.Enabled = true;
                lbtnCancel.Enabled = true;
                lbtnPay.Attributes.Add("onclick", "return confirm('打款后将不能对该申请进行操作，确认打款？');");
            }
            else
            {
                if (gdList.DataKeys[i].Values["status"].ToString().Equals("申请提交至出纳"))
                {
                    gdCheck.Disabled = false;
                    lbtnConfirm.Enabled = true;
                    lbtnCancel.Enabled = true;
                    lbtnConfirm.Attributes.Add("onclick", "return confirm('确认后将不能对该申请进行操作，确认提交？');");
                    lbtnCancel.Attributes.Add("onclick", "return confirm('撤回后将不能对该申请进行操作，确认撤回？');");
                }
                if (gdList.DataKeys[i].Values["status"].ToString().Equals("出纳已确认帐目"))
                {
                    gdCheck.Disabled = false;
                    lbtnPay.Enabled = true;
                    lbtnCancel.Enabled = true;
                    lbtnPay.Attributes.Add("onclick", "return confirm('打款后将不能对该申请进行操作，确认打款？');");
                }
                if (gdList.DataKeys[i].Values["status"].ToString().Equals("银行已受理"))
                {
                    gdCheck.Disabled = false;
                }
            }

            if (Common.ToDouble(gdList.DataKeys[i].Values["applyAmount"].ToString()) >= 50000)
            {
                gdList.Rows[i].Cells[13].ForeColor=System.Drawing.Color.Purple;
                gdList.Rows[i].Cells[13].Font.Bold = true;
            }

            ////交易量
            //gdList.Rows[i].Cells[10].Text = (Convert.ToDouble(gdList.DataKeys[i].Values["commissionAmount"].ToString()) + Convert.ToDouble(gdList.DataKeys[i].Values["applyAmount"].ToString())).ToString();

            ////计划实际佣金比例（暂不考虑每单固定抽成，否则比例没有实际意义）
            //if (gdList.DataKeys[i].Values["viewallocCommissionType"].ToString().Equals("2"))
            //{
            //    gdList.Rows[i].Cells[14].Text = "0.00";
            //    if (Common.ToDouble(gdList.DataKeys[i].Values["commissionAmount"].ToString()) != 0)
            //    {
            //        gdList.Rows[i].Cells[14].Text = Math.Round(Common.ToDouble(gdList.DataKeys[i].Values["commissionAmount"].ToString()) / Common.ToDouble(gdList.Rows[i].Cells[10].Text), 2).ToString();
            //    }
            //    else
            //    {
            //        gdList.Rows[i].Cells[14].Text = "0.00";
            //    }
            //    if (gdList.Rows[i].Cells[14].Text.Equals("0"))
            //    {
            //        gdList.Rows[i].Cells[14].Text = "0.00";
            //    }
            //}
            //else
            //{
            //    gdList.Rows[i].Cells[14].Text = "";
            //}
        }
        gdTable.Visible = true;
    }

    protected void btnAllPay_Click(object sender, EventArgs e)
    {
        BatchMoneyApplyDetail model = new BatchMoneyApplyDetail();
        BatchMoneyOperate bmo = new BatchMoneyOperate();
        BankMoneyRecordOperate bmro = new BankMoneyRecordOperate();
        int k = 0;
        string detailId = string.Empty;
        string errorID = string.Empty;
        string failID = string.Empty;
        string errorStatus = string.Empty;
        string errorBankInfoID = string.Empty;
        //int isFirst = 0;

        //for (int i = 0; i < gdList.Rows.Count; i++)
        //{
        //    if (gdList.DataKeys[i].Values["isFirst"].ToString().Equals("首次打款"))
        //    {
        //        isFirst++;
        //    }
        //}
        //if (isFirst > 0)
        //{
        //    Page.ClientScript.RegisterStartupScript(Page.GetType(), "", "<script>if(confirm('系统检测到有"+isFirst.ToString()+"个账号为首次打款，请确认是否继续打款？')){}else{return'}</script>");
        //}

        SystemConfigCacheLogic sccl = new SystemConfigCacheLogic();

        using (TransactionScope scope = new TransactionScope())
        {
            for (int i = 0; i < gdList.Rows.Count; i++)
            {
                HtmlInputCheckBox cbSelect = (HtmlInputCheckBox)gdList.Rows[i].FindControl("gdCheck");
                if (cbSelect.Checked)
                {//修改状态
                    k++;
                    if (!gdList.DataKeys[i].Values["status"].ToString().Equals("出纳已确认帐目"))
                    {
                        errorID += gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString() + ",";
                        continue;
                    }
                    if (checkBankInfo(i))
                    {
                        errorBankInfoID += gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString() + ",";
                        continue;
                    }

                    int batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString());
                    DataTable dt = bmo.SelectBatchMoneyApplyDetailByBatchMoneyApplyDetailIdNew(batchMoneyApplyDetailId);
                    if (!dt.Rows[0]["status"].ToString().Equals(((int)BatchMoneyStatus.confirmed).ToString()))
                    {
                        errorStatus += gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString() + ",";
                        continue;
                    }
                    
                    BankMoneyRecord modelRecord = new BankMoneyRecord();
                    modelRecord.Id = Common.CreateCombGuid();
                    modelRecord.BatchMoneyApplyDetail_Id = Common.ToInt32(gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString());
                    modelRecord.ElecChequeNo = gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString();
                    
                    //modelRecord.AcctNo = sccl.GetSystemConfig("payAcctNo", "6224080000955");
                    //modelRecord.AcctName = sccl.GetSystemConfig("payAcctName", "浦发2000148714");
                    //modelRecord.PayeeAcctNo = "6224080002395";
                    ////modelRecord.PayeeAcctNo = gdList.DataKeys[i].Values["accountNum"].ToString();
                    //modelRecord.PayeeName = "浦发2000939176";
                    ////modelRecord.PayeeName = gdList.DataKeys[i].Values["accountName"].ToString();

                    modelRecord.AcctNo = sccl.GetSystemConfig("payAcctNo", "95230154800001743");
                    modelRecord.AcctName = sccl.GetSystemConfig("payAcctName", "杭州友络软件科技有限公司");
                    modelRecord.PayeeAcctNo = gdList.DataKeys[i].Values["accountNum"].ToString();
                    modelRecord.PayeeName = gdList.DataKeys[i].Values["accountName"].ToString();
                    modelRecord.Amount = Common.ToDouble(gdList.DataKeys[i].Values["applyAmount"].ToString());
                    modelRecord.PayeeBankName = gdList.DataKeys[i].Values["bankName"].ToString() + gdList.DataKeys[i].Values["PayeeBankName"].ToString();
                    modelRecord.PayeeAddress = string.Empty;//gdList.DataKeys[i].Values["bankName"].ToString();//暂定收款行名称和地址相同
                    if (gdList.DataKeys[i].Values["bankName"].ToString().IndexOf("浦发银行") == -1 && gdList.DataKeys[i].Values["bankName"].ToString().IndexOf("浦东发展银行") == -1)
                    {//本行它行标志(0本行,1他行)
                        modelRecord.SysFlag = 1;
                    }
                    else
                    {
                        modelRecord.SysFlag = 0;
                    }

                    if (modelRecord.PayeeBankName.IndexOf("杭州") == -1)
                    {//本行它行标志(0本行,1他行)
                        modelRecord.RemitLocation = 1;
                    }
                    else
                    {
                        modelRecord.RemitLocation = 0;
                    }
                    modelRecord.DataStatus = 0;//默认值
                    modelRecord.AddTime = DateTime.Now;
                    modelRecord.ModifyTime = DateTime.Now;
                    modelRecord.ModifyUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
                    modelRecord.ModifyIP = IPHelper.GetRemoteIPAddress();
                    int result1=bmro.AddRecord(modelRecord);
                    if (result1 > 0)
                    {
                        model.batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString());
                        model.status = BatchMoneyStatus.sumbmit_bank;
                        
                        bool result = bmo.ModifyBatchMoneyApplyDetailStatusPay(model);
                        detailId += gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString() + ",";
                    }
                    else
                    {
                        failID += gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString() + ",";
                    }
                }
            }

            if (k.Equals(0))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请先勾选需要打款的明细')</script>");
                return;
            }
            if (detailId.Length > 0)
            {
                detailId = detailId.Substring(0, detailId.Length - 1);
            }

            scope.Complete();
        }
        if (detailId.Length > 0)
        {
            StoresMoneyLog modelLog = new StoresMoneyLog();
            modelLog.BatchMoneyApplyDetail_Id = detailId;
            modelLog.Content = "批量打款申请-主管提交至银行";
            modelLog.AddUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
            modelLog.AddTime = DateTime.Now;
            modelLog.Money = 0;
            modelLog.MoneyMerchantAccountDetail_AccountId = 0;
            modelLog.ShopInfo_ShopID = 0;
            modelLog.AddIP = IPHelper.GetRemoteIPAddress();

            StoresMoneyLogOperate smlo = new StoresMoneyLogOperate();
            smlo.AddStoresMoneyDetailLog(modelLog);
        }
        allCheck.Checked = false;
        AspNetPager1.CurrentPageIndex = 1;
        loadData(0, Common.ToInt32(Label_LargePageCount.Text), 0);

        string msg = string.Empty;
        if (errorID.Length > 0)
        {
            errorID = errorID.Substring(0, errorID.Length - 1);
            msg += "单号" + errorID + "非出纳已确认帐目状态不可操作!";
        }
        if (failID.Length > 0)
        {
            failID = failID.Substring(0, failID.Length - 1);
            msg += "单号" + errorID + "提交失败!";
        }

        if (errorBankInfoID.Length > 0)
        {
            errorBankInfoID = errorBankInfoID.Substring(0, errorBankInfoID.Length - 1);
            msg += "单号" + errorBankInfoID + "银行账号信息与最新信息不匹配!";
        }

        if (errorStatus.Length > 0)
        {
            errorStatus = errorStatus.Substring(0, errorStatus.Length - 1);
            msg += "单号" + errorStatus + "状态有误!";
        }

        if (msg.Length > 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + msg + "')</script>");
        }
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        loadData(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex, Common.ToInt32(ddlStatus.SelectedValue));
    }
    protected void GridView_OrderStatistics_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("back"))
        {
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
            ShopInfo modelShop = new ShopInfo();
            ShopOperate so = new ShopOperate();
            BalanceAccountOperate bao = new BalanceAccountOperate();
            if ((bao.IsHaveCheckAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID) && gdList.DataKeys[index].Values["status"].ToString().Equals("申请提交至出纳"))
                || (bao.IsHaveConfirmAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID) && gdList.DataKeys[index].Values["status"].ToString().Equals("出纳已确认帐目"))
                || (!bao.IsHaveCheckAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID) && !bao.IsHaveConfirmAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID) && (gdList.DataKeys[index].Values["status"].ToString().Equals("出纳已确认帐目") || gdList.DataKeys[index].Values["status"].ToString().Equals("申请提交至出纳"))))
            {

            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('当前状态和权限不允许撤回')</script>");
                return;
            }
            BatchMoneyOperate bmo = new BatchMoneyOperate();
            int batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[index].Values["batchMoneyApplyDetailId"].ToString());
            DataTable dt = bmo.SelectBatchMoneyApplyDetailByBatchMoneyApplyDetailIdNew(batchMoneyApplyDetailId);
            if (!dt.Rows[0]["status"].ToString().Equals(((int)BatchMoneyStatus.confirmed).ToString()) && !dt.Rows[0]["status"].ToString().Equals(((int)BatchMoneyStatus.wait_for_confirm).ToString()))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('当前状态不允许撤回')</script>");
                return;
            }

            BatchMoneyApplyDetail model = new BatchMoneyApplyDetail();
            
            string detailId = string.Empty;
            using (TransactionScope scope = new TransactionScope())
            {
                model.batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[index].Values["batchMoneyApplyDetailId"].ToString());
                model.shopId = Common.ToInt32(gdList.DataKeys[index].Values["shopID"].ToString());
                model.companyId = Common.ToInt32(gdList.DataKeys[index].Values["companyId"].ToString());
                model.applyAmount = Common.ToDouble(gdList.DataKeys[index].Values["applyAmount"].ToString());
                model.status = BatchMoneyStatus.check_rejected;
                bool result = bmo.ModifyBatchMoneyApplyDetailStatus(model, ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID.ToString());
                if (result == false)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('撤回失败')</script>");
                    return;
                }

                modelShop = new ShopInfo();
                modelShop.shopID = Common.ToInt32(gdList.DataKeys[index].Values["shopID"].ToString());
                modelShop.remainMoney = Common.ToDouble(gdList.DataKeys[index].Values["applyAmount"].ToString());
                //modelShop.remainRedEnvelopeAmount = Common.ToDouble(gdList.DataKeys[index].Values["redEnvelopeAmount"].ToString());
                //modelShop.remainFoodCouponAmount = Common.ToDouble(gdList.DataKeys[index].Values["foodCouponAmount"].ToString());
                //modelShop.remainAlipayAmount = Common.ToDouble(gdList.DataKeys[index].Values["alipayAmount"].ToString());
                //modelShop.remainWechatPayAmount = Common.ToDouble(gdList.DataKeys[index].Values["wechatPayAmount"].ToString());
                //modelShop.remainCommissionAmount = Common.ToDouble(gdList.DataKeys[index].Values["commissionAmount"].ToString());
                so.ModifyShopMoneyApplyBack(modelShop);

                detailId += gdList.DataKeys[index].Values["batchMoneyApplyDetailId"].ToString();
                scope.Complete();
            }
            StoresMoneyLog modelLog = new StoresMoneyLog();
            modelLog.BatchMoneyApplyDetail_Id = detailId;
            modelLog.Content = "打款申请-申请被撤回";
            modelLog.AddUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
            modelLog.AddTime = DateTime.Now;
            modelLog.AddIP = IPHelper.GetRemoteIPAddress();

            StoresMoneyLogOperate smlo = new StoresMoneyLogOperate();
            smlo.AddStoresMoneyDetailLog(modelLog);
            AspNetPager1.CurrentPageIndex = 1;
            loadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlStatus.SelectedValue));
        }
        else if (e.CommandName.Equals("Pay"))
        {
            BatchMoneyApplyDetail model = new BatchMoneyApplyDetail();
            BatchMoneyOperate bmo = new BatchMoneyOperate();
            BankMoneyRecordOperate bmro = new BankMoneyRecordOperate();
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
            string detailId = string.Empty;
            using (TransactionScope scope = new TransactionScope())
            {
                int batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[index].Values["batchMoneyApplyDetailId"].ToString());
                DataTable dt = bmo.SelectBatchMoneyApplyDetailByBatchMoneyApplyDetailIdNew(batchMoneyApplyDetailId);
                if (!dt.Rows[0]["status"].ToString().Equals(((int)BatchMoneyStatus.confirmed).ToString()))
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('当前状态不允许打款')</script>");
                    return;
                }

                model.batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[index].Values["batchMoneyApplyDetailId"].ToString());
                model.status = BatchMoneyStatus.sumbmit_bank;
                bool result = bmo.ModifyBatchMoneyApplyDetailStatusPay(model);
                if (result == false)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('打款失败')</script>");
                    return;
                }

                BankMoneyRecord modelRecord = new BankMoneyRecord();
                modelRecord.Id = Common.CreateCombGuid();
                modelRecord.BatchMoneyApplyDetail_Id = Common.ToInt32(gdList.DataKeys[index].Values["batchMoneyApplyDetailId"].ToString());
                modelRecord.ElecChequeNo = gdList.DataKeys[index].Values["batchMoneyApplyDetailCode"].ToString();

                SystemConfigCacheLogic sccl = new SystemConfigCacheLogic();
                modelRecord.AcctNo = sccl.GetSystemConfig("payAcctNo", "95230154800001743");
                modelRecord.AcctName = sccl.GetSystemConfig("payAcctName", "杭州友络软件科技有限公司");
                modelRecord.PayeeAcctNo = gdList.DataKeys[index].Values["accountNum"].ToString();
                modelRecord.PayeeName = gdList.DataKeys[index].Values["accountName"].ToString();
                modelRecord.Amount = Common.ToDouble(gdList.DataKeys[index].Values["applyAmount"].ToString());
                modelRecord.PayeeBankName = gdList.DataKeys[index].Values["bankName"].ToString() + gdList.DataKeys[index].Values["PayeeBankName"].ToString();
                modelRecord.PayeeAddress = string.Empty;//gdList.DataKeys[index].Values["bankName"].ToString();//暂定收款行名称和地址相同
                if (gdList.DataKeys[index].Values["bankName"].ToString().IndexOf("浦发银行") == -1 && gdList.DataKeys[index].Values["bankName"].ToString().IndexOf("浦东发展银行") == -1)
                {//本行它行标志(0本行,1他行)
                    modelRecord.SysFlag = 1;
                }
                else
                {
                    modelRecord.SysFlag = 0;
                }
                if (gdList.DataKeys[index].Values["bankName"].ToString().IndexOf("杭州") == -1)
                {//本行它行标志(0本行,1他行)
                    modelRecord.RemitLocation = 1;
                }
                else
                {
                    modelRecord.RemitLocation = 0;
                }
                modelRecord.DataStatus = 0;//默认值
                modelRecord.AddTime = DateTime.Now;
                modelRecord.ModifyTime = DateTime.Now;
                modelRecord.ModifyUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
                modelRecord.ModifyIP = IPHelper.GetRemoteIPAddress();
                int result1 = bmro.AddRecord(modelRecord);
                
                scope.Complete();
            }
            StoresMoneyLog modelLog = new StoresMoneyLog();
            modelLog.BatchMoneyApplyDetail_Id = detailId;
            modelLog.Content = "打款申请-主管提交至银行";
            modelLog.AddUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
            modelLog.AddTime = DateTime.Now;
            modelLog.AddIP = IPHelper.GetRemoteIPAddress();

            StoresMoneyLogOperate smlo = new StoresMoneyLogOperate();
            smlo.AddStoresMoneyDetailLog(modelLog);
            AspNetPager1.CurrentPageIndex = 1;
            loadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlStatus.SelectedValue));
        }
        else if (e.CommandName.Equals("confirm"))
        {
            BatchMoneyApplyDetail model = new BatchMoneyApplyDetail();
            BatchMoneyOperate bmo = new BatchMoneyOperate();
            int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
            string detailId = string.Empty;

            int batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[index].Values["batchMoneyApplyDetailId"].ToString());
            DataTable dt = bmo.SelectBatchMoneyApplyDetailByBatchMoneyApplyDetailIdNew(batchMoneyApplyDetailId);
            if (!dt.Rows[0]["status"].ToString().Equals(((int)BatchMoneyStatus.wait_for_confirm).ToString()))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('当前状态不允许确认')</script>");
                return;
            }

            using (TransactionScope scope = new TransactionScope())
            {
                model.batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[index].Values["batchMoneyApplyDetailId"].ToString());
                model.status = BatchMoneyStatus.confirmed;
                bool result = bmo.ModifyBatchMoneyApplyDetailStatus(model, ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID.ToString());
                if (result == false)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('确认失败')</script>");
                    return;
                }
                detailId += gdList.DataKeys[index].Values["batchMoneyApplyDetailId"].ToString();
                scope.Complete();
            }
            StoresMoneyLog modelLog = new StoresMoneyLog();
            modelLog.BatchMoneyApplyDetail_Id = detailId;
            modelLog.Content = "打款申请-出纳已确认账目";
            modelLog.AddUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
            modelLog.AddTime = DateTime.Now;
            modelLog.AddIP = IPHelper.GetRemoteIPAddress();

            StoresMoneyLogOperate smlo = new StoresMoneyLogOperate();
            smlo.AddStoresMoneyDetailLog(modelLog);
            AspNetPager1.CurrentPageIndex = 1;
            loadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlStatus.SelectedValue));
        }
    }

    protected void gdList_Sorting(object sender, GridViewSortEventArgs e)
    {
        Dictionary<string, string> sorted = new Dictionary<string, string>();
        if (ViewState["SortedField"] == null)
        {
            sorted.Add(e.SortExpression, "ASC");
            ViewState["SortedField"] = sorted;
        }
        else
        {
            sorted = (Dictionary<string, string>)ViewState["SortedField"];
            if (sorted.ContainsKey(e.SortExpression))
            {
                if (sorted[e.SortExpression] == "ASC")
                {
                    sorted[e.SortExpression] = "DESC";
                }
                else
                {
                    sorted[e.SortExpression] = "ASC";
                }
            }
            else
            {
                sorted.Clear();
                sorted.Add(e.SortExpression, "ASC");
                ViewState["SortedField"] = sorted;
            }
        }
        loadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlStatus.SelectedValue));
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string detailId = string.Empty;
        string failId = string.Empty;
        string errorID = string.Empty;
        string errorStatus = string.Empty;
        
        BatchMoneyApplyDetail model = new BatchMoneyApplyDetail();
        BatchMoneyOperate bmo = new BatchMoneyOperate();

        int k = 0;
        using (TransactionScope scope = new TransactionScope())
        {
            for (int i = 0; i < gdList.Rows.Count; i++)
            {
                HtmlInputCheckBox cbSelect = (HtmlInputCheckBox)gdList.Rows[i].FindControl("gdCheck");
                if (cbSelect.Checked)
                {//修改状态
                    k++;
                    if (!gdList.DataKeys[i].Values["status"].ToString().Equals("申请提交至出纳"))
                    {
                        errorID += gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString() + ",";
                        continue;
                    }

                    int batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString());
                    DataTable dt = bmo.SelectBatchMoneyApplyDetailByBatchMoneyApplyDetailIdNew(batchMoneyApplyDetailId);
                    if (!dt.Rows[0]["status"].ToString().Equals(((int)BatchMoneyStatus.wait_for_confirm).ToString()))
                    {
                        errorStatus += gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString() + ",";
                        continue;
                    }
                  
                    model.batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString());
                    model.status = BatchMoneyStatus.confirmed;
                    bool result = bmo.ModifyBatchMoneyApplyDetailStatus(model, ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID.ToString());
                    detailId += gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString() + ",";

                    if (result == false)
                    {
                        failId += gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString() + ",";
                    }
                }
            }

            if (k.Equals(0))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请先勾选需要确认的明细')</script>");
                return;
            }
            if (detailId.Length > 0)
            {
                detailId = detailId.Substring(0, detailId.Length - 1);
            }

            scope.Complete();
        }
        if (detailId.Length > 0)
        {
            StoresMoneyLog modelLog = new StoresMoneyLog();
            modelLog.BatchMoneyApplyDetail_Id = detailId;
            modelLog.Content = "批量打款申请-出纳已确认帐目";
            modelLog.AddUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
            modelLog.AddTime = DateTime.Now;
            modelLog.Money = 0;
            modelLog.MoneyMerchantAccountDetail_AccountId = 0;
            modelLog.ShopInfo_ShopID = 0;
            modelLog.AddIP = IPHelper.GetRemoteIPAddress();

            StoresMoneyLogOperate smlo = new StoresMoneyLogOperate();
            smlo.AddStoresMoneyDetailLog(modelLog);
        }
        allCheck.Checked = false;
        AspNetPager1.CurrentPageIndex = 1;
        loadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlStatus.SelectedValue));

        string msg = string.Empty;
        if (errorID.Length > 0)
        {
            errorID = errorID.Substring(0, errorID.Length - 1);
            msg += "单号" + errorID + "非申请提交至出纳状态不可操作!";
        }
        if (failId.Length > 0)
        {
            failId = failId.Substring(0, failId.Length - 1);
            msg += "单号" + errorID + "提交失败!";
        }

        if (errorStatus.Length > 0)
        {
            errorStatus = errorStatus.Substring(0, errorStatus.Length - 1);
            msg += "单号" + errorStatus + "状态有误!";
        }

        if (msg.Length > 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + msg + "')</script>");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        string detailId = string.Empty;
        string failId = string.Empty;
        string errorId = string.Empty;
        string msg = string.Empty;
        string errorStatus = string.Empty;
        BatchMoneyApplyDetail model = new BatchMoneyApplyDetail();
        BatchMoneyOperate bmo = new BatchMoneyOperate();
        ShopInfo modelShop = new ShopInfo();
        ShopOperate so = new ShopOperate();

        int k = 0;
        using (TransactionScope scope = new TransactionScope())
        {
            for (int i = 0; i < gdList.Rows.Count; i++)
            {
                HtmlInputCheckBox cbSelect = (HtmlInputCheckBox)gdList.Rows[i].FindControl("gdCheck");
                if (cbSelect.Checked)
                {//修改状态
                    k++;

                    BalanceAccountOperate bao = new BalanceAccountOperate();
                    if ((bao.IsHaveCheckAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID) && gdList.DataKeys[i].Values["status"].ToString().Equals("申请提交至出纳"))
                        || (bao.IsHaveConfirmAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID) && gdList.DataKeys[i].Values["status"].ToString().Equals("出纳已确认帐目"))
                        || (!bao.IsHaveCheckAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID) && !bao.IsHaveConfirmAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID) && (gdList.DataKeys[i].Values["status"].ToString().Equals("出纳已确认帐目") || gdList.DataKeys[i].Values["status"].ToString().Equals("申请提交至出纳"))))
                    {

                    }
                    else
                    {
                        errorId += model.batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString());
                        continue;
                    }

                    int batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString());
                    DataTable dt = bmo.SelectBatchMoneyApplyDetailByBatchMoneyApplyDetailIdNew(batchMoneyApplyDetailId);
                    if (!dt.Rows[0]["status"].ToString().Equals(((int)BatchMoneyStatus.wait_for_confirm).ToString()) && !dt.Rows[0]["status"].ToString().Equals(((int)BatchMoneyStatus.confirmed).ToString()))
                    {
                        errorStatus += gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString() + ",";
                        continue;
                    }

                    model.batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString());
                    model.shopId = Common.ToInt32(gdList.DataKeys[i].Values["shopID"].ToString());
                    model.companyId = Common.ToInt32(gdList.DataKeys[i].Values["companyId"].ToString());
                    model.applyAmount = Common.ToDouble(gdList.DataKeys[i].Values["applyAmount"].ToString());
                    model.status = BatchMoneyStatus.check_rejected;
                    bool result = bmo.ModifyBatchMoneyApplyDetailStatus(model, ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID.ToString());


                    if (result == false)
                    {
                        failId += gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString() + ",";
                    }
                    else
                    {
                        modelShop = new ShopInfo();
                        modelShop.shopID = Common.ToInt32(gdList.DataKeys[i].Values["shopID"].ToString());
                        modelShop.remainMoney = Common.ToDouble(gdList.DataKeys[i].Values["applyAmount"].ToString());
                        //modelShop.remainRedEnvelopeAmount = Common.ToDouble(gdList.DataKeys[i].Values["redEnvelopeAmount"].ToString());
                        //modelShop.remainFoodCouponAmount = Common.ToDouble(gdList.DataKeys[i].Values["foodCouponAmount"].ToString());
                        //modelShop.remainAlipayAmount = Common.ToDouble(gdList.DataKeys[i].Values["alipayAmount"].ToString());
                        //modelShop.remainWechatPayAmount = Common.ToDouble(gdList.DataKeys[i].Values["wechatPayAmount"].ToString());
                        //modelShop.remainCommissionAmount = Common.ToDouble(gdList.DataKeys[i].Values["commissionAmount"].ToString());
                        so.ModifyShopMoneyApplyBack(modelShop);
                        detailId += gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString() + ",";
                    }
                }
            }

            if (k.Equals(0))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请先勾选需要确认的明细')</script>");
                return;
            }

            scope.Complete();
        }
        StoresMoneyLog modelLog = new StoresMoneyLog();

        if (detailId.Length > 0)
        {
            detailId = detailId.Substring(0, detailId.Length - 1);
        }
        if (errorId.Length > 0)
        {
            errorId = errorId.Substring(0, errorId.Length - 1);
            msg += "单号：" + errorId + "当前状态和权限不允许撤回！";
        }
        if (failId.Length > 0)
        {
            failId = failId.Substring(0, failId.Length - 1);
            msg += "单号：" + failId + "提交撤回失败！";
        }

        if (errorStatus.Length > 0)
        {
            errorStatus = errorStatus.Substring(0, errorStatus.Length - 1);
            msg += "单号" + errorStatus + "状态有误!";
        }

        if (detailId.Length > 0)
        {
            modelLog.BatchMoneyApplyDetail_Id = detailId;
            modelLog.Content = "批量打款申请-申请被撤回";
            modelLog.AddUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
            modelLog.AddTime = DateTime.Now;
            modelLog.Money = 0;
            modelLog.MoneyMerchantAccountDetail_AccountId = 0;
            modelLog.ShopInfo_ShopID = 0;
            modelLog.AddIP = IPHelper.GetRemoteIPAddress();

            StoresMoneyLogOperate smlo = new StoresMoneyLogOperate();
            smlo.AddStoresMoneyDetailLog(modelLog);
            allCheck.Checked = false;
            AspNetPager1.CurrentPageIndex = 1;
            loadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlStatus.SelectedValue));
        }
        if (msg.Length > 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('"+msg+"')</script>");
        }
    }
    protected void btnPaySuccess_Click(object sender, EventArgs e)
    {
        string detailID = string.Empty;
        string failID = string.Empty;
        string errorID = string.Empty;
        string errorTimeID = string.Empty;
        BatchMoneyApplyDetail model = new BatchMoneyApplyDetail();
        BatchMoneyOperate bmo = new BatchMoneyOperate();
        SystemConfigCacheLogic sccl = new SystemConfigCacheLogic();
        int waitTime = Common.ToInt32(sccl.GetSystemConfig("payWaitTime", "4320"));

        int k = 0;
        using (TransactionScope scope = new TransactionScope())
        {
            for (int i = 0; i < gdList.Rows.Count; i++)
            {
                HtmlInputCheckBox cbSelect = (HtmlInputCheckBox)gdList.Rows[i].FindControl("gdCheck");
                if (cbSelect.Checked)
                {//修改状态
                    k++;

                    BalanceAccountOperate bao = new BalanceAccountOperate();
                    if ((bao.IsHaveConfirmAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID) && gdList.DataKeys[i].Values["status"].ToString().Equals("银行已受理"))
                        || !bao.IsHaveConfirmAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID) && !bao.IsHaveCheckAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID) && gdList.DataKeys[i].Values["status"].ToString().Equals("银行已受理"))
                    {
                    }
                    else
                    {
                        errorID += gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString() + ",";
                        continue;
                    }

                    DateTime dtPay = Common.ToDateTime(gdList.DataKeys[i].Values["financePlayMoneyTime"].ToString());
                    if (dtPay.AddMinutes(waitTime) > DateTime.Now)
                    {
                        errorTimeID += gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString() + ",";
                        continue;
                    }

                    model.batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString());
                    model.status = BatchMoneyStatus.bank_pay_success;
                    bool result = bmo.ModifyBatchMoneyApplyDetailStatus(model, ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID.ToString());
                    detailID += gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString() + ",";

                    if (result == false)
                    {
                        failID += gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString() + ",";
                    }
                }
            }

            if (k.Equals(0))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请先勾选需要确认的明细')</script>");
                return;
            }
            scope.Complete();
        }
        if (detailID.Length > 0)
        {
            detailID = detailID.Substring(0, detailID.Length - 1);

            StoresMoneyLog modelLog = new StoresMoneyLog();
            modelLog.BatchMoneyApplyDetail_Id = detailID;
            modelLog.Content = "批量打款申请-银行打款成功";
            modelLog.AddUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
            modelLog.AddTime = DateTime.Now;
            modelLog.Money = 0;
            modelLog.MoneyMerchantAccountDetail_AccountId = 0;
            modelLog.ShopInfo_ShopID = 0;
            modelLog.AddIP = IPHelper.GetRemoteIPAddress();
            StoresMoneyLogOperate smlo = new StoresMoneyLogOperate();
            smlo.AddStoresMoneyDetailLog(modelLog);
        }
        allCheck.Checked = false;
        AspNetPager1.CurrentPageIndex = 1;
        loadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlStatus.SelectedValue));

        string msg = string.Empty;
        if (errorID.Length > 0)
        {
            errorID = errorID.Substring(0, errorID.Length - 1);
            msg += "单号" + errorID + "非银行已受理状态，不可操作!";
        }
        if (failID.Length > 0)
        {
            failID = failID.Substring(0, failID.Length - 1);
            msg += "单号" + failID + "提交失败!";
        }
        if (errorTimeID.Length > 0)
        {
            errorTimeID = errorTimeID.Substring(0, errorTimeID.Length - 1);
            msg += "单号" + errorTimeID + "手动更改状态的间隔时间未到，间隔时间为" + waitTime + "分钟!";
        }

        if (msg.Length > 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + msg + "')</script>");
        }
    }
    //protected void btnFail_Click(object sender, EventArgs e)
    //{
    //    string detailID = string.Empty;
    //    string failID = string.Empty;
    //    string errorID = string.Empty;
    //    string errorTimeID = string.Empty;
    //    BatchMoneyApplyDetail model = new BatchMoneyApplyDetail();
    //    BatchMoneyOperate bmo = new BatchMoneyOperate();
    //    SystemConfigCacheLogic sccl = new SystemConfigCacheLogic();
    //    ShopInfo modelShop = new ShopInfo();
    //    ShopOperate so = new ShopOperate();
    //    int waitTime = Common.ToInt32(sccl.GetSystemConfig("payWaitTime", "4320"));
    //    int k = 0;
    //    using (TransactionScope scope = new TransactionScope())
    //    {
    //        for (int i = 0; i < gdList.Rows.Count; i++)
    //        {
    //            CheckBox cbSelect = (CheckBox)gdList.Rows[i].FindControl("gdCheck");
    //            if (cbSelect.Checked)
    //            {//修改状态
    //                k++;
    //                BalanceAccountOperate bao = new BalanceAccountOperate();
    //                if ((bao.IsHaveConfirmAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID) && gdList.DataKeys[i].Values["status"].ToString().Equals("银行已受理"))
    //                   || !bao.IsHaveConfirmAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID) && !bao.IsHaveCheckAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID) && gdList.DataKeys[i].Values["status"].ToString().Equals("银行已受理"))
    //                {
    //                }
    //                else
    //                {
    //                    errorID += gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString() + ",";
    //                    continue;
    //                }
    //                DateTime dtPay = Common.ToDateTime(gdList.DataKeys[i].Values["financePlayMoneyTime"].ToString());
    //                if (dtPay.AddMinutes(waitTime) > DateTime.Now)
    //                {
    //                    errorTimeID += gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString() + ",";
    //                    continue;
    //                }
    //                model.batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString());
    //                model.shopId = Common.ToInt32(gdList.DataKeys[i].Values["shopID"].ToString());
    //                model.companyId = Common.ToInt32(gdList.DataKeys[i].Values["companyId"].ToString());
    //                model.applyAmount = Common.ToDouble(gdList.DataKeys[i].Values["applyAmount"].ToString());
    //                model.status = BatchMoneyStatus.bank_pay_failure;
    //                bool result = bmo.ModifyBatchMoneyApplyDetailStatus(model, ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID.ToString());

    //                modelShop = new ShopInfo();
    //                modelShop.shopID = Common.ToInt32(gdList.DataKeys[i].Values["shopID"].ToString());
    //                modelShop.remainMoney = Common.ToDouble(gdList.DataKeys[i].Values["applyAmount"].ToString());
    //                modelShop.remainRedEnvelopeAmount = Common.ToDouble(gdList.DataKeys[i].Values["redEnvelopeAmount"].ToString());
    //                modelShop.remainFoodCouponAmount = Common.ToDouble(gdList.DataKeys[i].Values["foodCouponAmount"].ToString());
    //                modelShop.remainAlipayAmount = Common.ToDouble(gdList.DataKeys[i].Values["alipayAmount"].ToString());
    //                modelShop.remainWechatPayAmount = Common.ToDouble(gdList.DataKeys[i].Values["wechatPayAmount"].ToString());
    //                modelShop.remainCommissionAmount = Common.ToDouble(gdList.DataKeys[i].Values["commissionAmount"].ToString());
    //                so.ModifyShopMoneyApplyBack(modelShop);

    //                detailID += gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString() + ",";

    //                if (result == false)
    //                {
    //                    failID += gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString() + ",";
    //                }
    //            }
    //        }

    //        if (k.Equals(0))
    //        {
    //            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请先勾选需要确认的明细')</script>");
    //            return;
    //        }
    //        scope.Complete();
    //    }

    //    if (detailID.Length > 0)
    //    {
    //        detailID = detailID.Substring(0, detailID.Length - 1);

    //        StoresMoneyLog modelLog = new StoresMoneyLog();
    //        modelLog.BatchMoneyApplyDetail_Id = detailID;
    //        modelLog.Content = "批量打款申请-银行打款失败";
    //        modelLog.AddUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
    //        modelLog.AddTime = DateTime.Now;
    //        modelLog.Money = 0;
    //        modelLog.MoneyMerchantAccountDetail_AccountId = 0;
    //        modelLog.ShopInfo_ShopID = 0;
    //        modelLog.AddIP = IPHelper.GetRemoteIPAddress();

    //        StoresMoneyLogOperate smlo = new StoresMoneyLogOperate();
    //        smlo.AddStoresMoneyDetailLog(modelLog);
    //    }
    //    allCheck.Checked = false;
    //    AspNetPager1.CurrentPageIndex = 1;
    //    loadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlStatus.SelectedValue));

    //    string msg = string.Empty;
    //    if (errorID.Length > 0)
    //    {
    //        errorID = errorID.Substring(0, errorID.Length - 1);
    //        msg += "单号" + errorID + "非银行已受理状态，不可操作!";
    //    }
    //    if (failID.Length > 0)
    //    {
    //        failID = failID.Substring(0, failID.Length - 1);
    //        msg += "单号" + errorID + "提交失败!";
    //    }
    //    if (errorTimeID.Length > 0)
    //    {
    //        errorTimeID = errorTimeID.Substring(0, errorTimeID.Length - 1);
    //        msg += "单号" + errorTimeID + "手动更改状态的间隔时间未到，间隔时间为" + waitTime + "分钟!";
    //    }

    //    if (msg.Length > 0)
    //    {
    //        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + msg + "')</script>");
    //    }
    //}
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        CreateExcel(GetGrData());
    }

    private DataTable GetGrData()
    {
        DataTable dt = new DataTable();
        BatchMoneyOperate bmo = new BatchMoneyOperate();
        int shopID = Common.ToInt32(Request.QueryString["id"]);
        DataTable dtApply = bmo.SelectBatchMoneyApplyDetailByManager(txt_batchMoneyApplyDetailCode.Text.Trim(), shopID, Common.ToInt32(ddlStatus.SelectedValue), txtOperateBeginTime.Text, txtOperateEndTime.Text, Common.ToInt32(ddlCity.SelectedValue), Common.ToInt32(ddlIsFirst.SelectedValue));

        for (int i = 0; i < dtApply.Rows.Count; i++)
        {
            switch (dtApply.Rows[i]["status"].ToString())
            {
                case "5":
                    dtApply.Rows[i]["status"] = "申请未提交";
                    break;
                case "6":
                    dtApply.Rows[i]["status"] = "申请被撤回";
                    break;
                case "7":
                    dtApply.Rows[i]["status"] = "申请提交至出纳";
                    break;
                case "8":
                    dtApply.Rows[i]["status"] = "出纳已确认帐目";
                    break;
                case "9":
                    dtApply.Rows[i]["status"] = "主管提交至银行";
                    break;
                case "10":
                    dtApply.Rows[i]["status"] = "银行已受理";
                    break;
                case "11":
                    dtApply.Rows[i]["status"] = "银行未受理";
                    break;
                case "12":
                    dtApply.Rows[i]["status"] = "银行打款成功";
                    break;
                case "13":
                    dtApply.Rows[i]["status"] = "银行打款失败";
                    break;
                default:
                    break;
            }

            if (dtApply.Rows[i]["isFirst"].ToString().Equals("0"))
            {
                dtApply.Rows[i]["isFirst"] = "非首次打款";
            }
            else
            {
                dtApply.Rows[i]["isFirst"] = "首次打款";
            }
        }
        dt.Columns.Add("单号", typeof(string));
        dt.Columns.Add("门店名", typeof(string));
        dt.Columns.Add("公司名", typeof(string));
        dt.Columns.Add("开户银行", typeof(string));
        dt.Columns.Add("支行名称", typeof(string));
        dt.Columns.Add("开户名", typeof(string));
        dt.Columns.Add("账号", typeof(string));
        dt.Columns.Add("打款标识", typeof(string));
        dt.Columns.Add("申请打款时间", typeof(string));
        dt.Columns.Add("财务打款时间", typeof(string));
        dt.Columns.Add("申请结款金额", typeof(string));
        dt.Columns.Add("当前佣金比例", typeof(string));
        dt.Columns.Add("流水号或备注", typeof(string));
        dt.Columns.Add("状态", typeof(string));

        for (int i = 0; i < dtApply.Rows.Count; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = dtApply.Rows[i]["batchMoneyApplyDetailCode"].ToString();
            dr[1] = dtApply.Rows[i]["shopName"].ToString();
            dr[2] = dtApply.Rows[i]["companyName"].ToString();
            dr[3] = dtApply.Rows[i]["bankName"].ToString();
            dr[4] = dtApply.Rows[i]["PayeeBankName"].ToString();
            dr[5] = dtApply.Rows[i]["accountName"].ToString();
            dr[6] = dtApply.Rows[i]["accountNum"].ToString();
            dr[7] = dtApply.Rows[i]["isFirst"].ToString();
            dr[8] = dtApply.Rows[i]["createdTime"].ToString();
            dr[9] = dtApply.Rows[i]["financePlayMoneyTime"].ToString();
            dr[10] = dtApply.Rows[i]["applyAmount"].ToString();
            dr[11] = dtApply.Rows[i]["viewallocCommissionValue"].ToString();
            dr[12] = dtApply.Rows[i]["serialNumberOrRemark"].ToString();
            dr[13] = dtApply.Rows[i]["status"].ToString();
            dt.Rows.Add(dr);

        }
        return dt;
    }

    private void CreateExcel(DataTable dt)
    {
        if (dt.Rows.Count.Equals(0))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('查无相关明细')</script>");
            return;
        }
        string excelName = HttpUtility.UrlEncode("批量打款_" + DateTime.Now.ToString("yyyy/mm/dd_hh:mm:ss"), System.Text.Encoding.UTF8).ToString();
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
                else if (i == 0 || i == 6 || i == 10 || i == 11)
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

    private bool checkBankInfo(int rowIndex)
    {
        if (gdList.DataKeys[rowIndex].Values["bankName"].ToString().ToString() != gdList.DataKeys[rowIndex].Values["newbankName"].ToString()
            || gdList.DataKeys[rowIndex].Values["accountNum"].ToString().ToString() != gdList.DataKeys[rowIndex].Values["newaccountNum"].ToString()
            || gdList.DataKeys[rowIndex].Values["accountName"].ToString().ToString() != gdList.DataKeys[rowIndex].Values["newaccountName"].ToString()
            || gdList.DataKeys[rowIndex].Values["PayeeBankName"].ToString().ToString() != gdList.DataKeys[rowIndex].Values["newPayeeBankName"].ToString())
        {
            return true;
        }
        return false;
    }

    //请填写备注信息
    protected void btnFailSubmit_Click(object sender, EventArgs e)
    {
        if (checkRemark())
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请填写备注信息！')</script>");
            return;
        }

        if (checkRule())
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('非银行已受理状态，不可操作！')</script>");
            return;
        }

        if (checkTime())
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('手动更改状态的间隔时间未到！')</script>");
            return;
        }

        updateRemark();
    }

    private bool checkRemark()
    {
        if (txt_serialNumberOrRemark.Text.Trim().Equals("请填写备注信息") || txt_serialNumberOrRemark.Text.Trim().Equals(string.Empty))
        {
            return true;
        }
        return false;
    }

    private bool checkRule()
    {
        BalanceAccountOperate bao = new BalanceAccountOperate();
        for (int i = 0; i < gdList.Rows.Count; i++)
        {
            HtmlInputCheckBox cbSelect = (HtmlInputCheckBox)gdList.Rows[i].FindControl("gdCheck");
            if (cbSelect.Checked)
            if ((bao.IsHaveConfirmAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID) && gdList.DataKeys[i].Values["status"].ToString().Equals("银行已受理"))
                                       || !bao.IsHaveConfirmAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID) && !bao.IsHaveCheckAuthority(((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID) && gdList.DataKeys[i].Values["status"].ToString().Equals("银行已受理"))
            {
                return false;
            }
            
        }
        return true;
    }

    private bool checkTime()
    {
        SystemConfigCacheLogic sccl = new SystemConfigCacheLogic();
        int waitTime = Common.ToInt32(sccl.GetSystemConfig("payWaitTime", "4320"));
        for (int i = 0; i < gdList.Rows.Count; i++)
        {
            DateTime dtPay = Common.ToDateTime(gdList.DataKeys[i].Values["financePlayMoneyTime"].ToString());
            if (dtPay.AddMinutes(waitTime) > DateTime.Now)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 手动点击打款失败按钮，填写备注
    /// </summary>
    private void updateRemark()
    {
        BatchMoneyApplyDetail model = new BatchMoneyApplyDetail();
        BatchMoneyOperate bmo = new BatchMoneyOperate();
        ShopInfo modelShop = new ShopInfo();
        ShopOperate so = new ShopOperate();
        int id = 0;
        for (int i = 0; i < gdList.Rows.Count; i++)
        {
            HtmlInputCheckBox cbSelect = (HtmlInputCheckBox)gdList.Rows[i].FindControl("gdCheck");
            if (cbSelect.Checked)
            {//修改备注
                id = Common.ToInt32(gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString());
                model.batchMoneyApplyDetailId = id;
                model.serialNumberOrRemark = txt_serialNumberOrRemark.Text;

                using (TransactionScope scope = new TransactionScope())
                {

                    BalanceAccountOperate bao = new BalanceAccountOperate();
                    model.batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString());
                    model.shopId = Common.ToInt32(gdList.DataKeys[i].Values["shopID"].ToString());
                    model.companyId = Common.ToInt32(gdList.DataKeys[i].Values["companyId"].ToString());
                    model.applyAmount = Common.ToDouble(gdList.DataKeys[i].Values["applyAmount"].ToString());
                    model.status = BatchMoneyStatus.bank_pay_failure;
                    bool result = bmo.ModifyBatchMoneyApplyDetailStatus(model, ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID.ToString());

                    modelShop = new ShopInfo();
                    modelShop.shopID = Common.ToInt32(gdList.DataKeys[i].Values["shopID"].ToString());
                    modelShop.remainMoney = Common.ToDouble(gdList.DataKeys[i].Values["applyAmount"].ToString());
                    //modelShop.remainRedEnvelopeAmount = Common.ToDouble(gdList.DataKeys[i].Values["redEnvelopeAmount"].ToString());
                    //modelShop.remainFoodCouponAmount = Common.ToDouble(gdList.DataKeys[i].Values["foodCouponAmount"].ToString());
                    //modelShop.remainAlipayAmount = Common.ToDouble(gdList.DataKeys[i].Values["alipayAmount"].ToString());
                    //modelShop.remainWechatPayAmount = Common.ToDouble(gdList.DataKeys[i].Values["wechatPayAmount"].ToString());
                    //modelShop.remainCommissionAmount = Common.ToDouble(gdList.DataKeys[i].Values["commissionAmount"].ToString());
                    so.ModifyShopMoneyApplyBack(modelShop);
                    scope.Complete();

                    continue;
                }
            }
        }

        StoresMoneyLog modelLog = new StoresMoneyLog();
        modelLog.BatchMoneyApplyDetail_Id = id.ToString();
        modelLog.Content = "批量打款申请-银行打款失败";
        modelLog.AddUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
        modelLog.AddTime = DateTime.Now;
        modelLog.Money = 0;
        modelLog.MoneyMerchantAccountDetail_AccountId = 0;
        modelLog.ShopInfo_ShopID = 0;
        modelLog.AddIP = IPHelper.GetRemoteIPAddress();

        StoresMoneyLogOperate smlo = new StoresMoneyLogOperate();
        smlo.AddStoresMoneyDetailLog(modelLog);

        loadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlStatus.SelectedValue));
    }
}