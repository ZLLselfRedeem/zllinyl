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

public partial class FinanceManage_batchMoneyApplyDS : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            init_date.InnerHtml = "";
            new CityDropDownList().BindCity(ddlCity);
            new CityDropDownList().BindCity(ddlCitySearch);
            new CityDropDownList().BindCity(ddlCityWithdrawType);
            if (!string.IsNullOrEmpty(Common.ToString(Request.QueryString["name"])))
            {
                string[] strRequest = Common.ToString(Request.QueryString["name"]).Split(',');//回传页面显示公司名称
                text.Value = strRequest[0];
                if (strRequest.Length > 1)
                {
                    txt_companyName.Text = strRequest[1];
                }
                if (strRequest.Length > 2)
                {
                    txt_remainMoney.Text = strRequest[2];
                }
            }
            else
            {
                text.Value = string.Empty;
                txt_companyName.Text = string.Empty;
                txt_remainMoney.Text = string.Empty;
            }
            ddlStatus.SelectedValue = "5";
           
            Button_LargePageCount_Click(Button_10, null);//分页选中10
            //gdTable.Visible = false;
            //if (Request.QueryString["Pid"] != null)
            //{
            //    ViewState["vsApplyId"] = Common.ToInt32(Request.QueryString["Pid"]);
            //    loadData(0, Common.ToInt32(Label_LargePageCount.Text), 0);
            //}
            btnAllCancel.Attributes.Add("onclick", "return confirm('删除后列表将不再显示该申请，确认删除？');");
            btnAllSubmit.Attributes.Add("onclick","return confirm('提交后将不能对该申请进行操作，确认提交？')");
        }
    }
    /// <summary>
    /// 生成打款记录编号
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_create_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(txt_companyName.Text))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('条件输入不能为空')</script>");
            return;
        }
        if (Common.ToDouble(txt_remainMoney.Text) <= 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('余额不足')</script>");
            return;
        }
        int shopId = Common.ToInt32(Request.QueryString["id"]);
        int resultInsert = 0;
        using (TransactionScope scope = new TransactionScope())
        {
            int cityId = Common.ToInt32(ddlCity.SelectedValue);
            BatchMoneyOperate operate = new BatchMoneyOperate();
            DataTable dtBatchMoneyMerchantApply = operate.QueryBatchMoneyMerchantApplyByShop(shopId);
            if (dtBatchMoneyMerchantApply.Rows.Count > 0)
            {
                double sumObject = Common.ToDouble(txt_remainMoney.Text);//总打款金额
                int countObject = 1;//总打款记录
                int employeeId = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
                string employeeName = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
                BatchMoneyApply batchMoneyApply = new BatchMoneyApply()
                {
                    advanceAmount = sumObject,
                    createdTime = DateTime.Now,
                    operateEmployee = employeeId,
                    advanceCount = countObject,
                    practicalAmount = 0,
                    practicalCount = 0,
                    remark = employeeName + "帐号登录生成" + ddlCity.SelectedItem.Text + "批量打款记录",
                    status = 1,//有效打款记录
                    cityId = cityId
                };
                resultInsert = operate.AddBatchMoneyApply(batchMoneyApply);
                if (resultInsert > 0)
                {
                    //int flagCount = 0;
                    DataTable dtTemp = new DataTable();
                    dtTemp.Columns.Add(new DataColumn("batchMoneyApplyId", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("accountId", typeof(Int64)));
                    dtTemp.Columns.Add(new DataColumn("operateEmployee", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("companyId", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("shopId", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("accountNum", typeof(String)));
                    dtTemp.Columns.Add(new DataColumn("bankName", typeof(String)));
                    dtTemp.Columns.Add(new DataColumn("accountName", typeof(String)));
                    dtTemp.Columns.Add(new DataColumn("applyAmount", typeof(Double)));
                    dtTemp.Columns.Add(new DataColumn("serialNumberOrRemark", typeof(String)));
                    dtTemp.Columns.Add(new DataColumn("status", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("haveAdjustAmount", typeof(Double)));
                    dtTemp.Columns.Add(new DataColumn("cityId", typeof(Int32)));
                    //dtTemp.Columns.Add(new DataColumn("redEnvelopeAmount", typeof(Double)));
                    //dtTemp.Columns.Add(new DataColumn("foodCouponAmount", typeof(Double)));
                    //dtTemp.Columns.Add(new DataColumn("alipayAmount", typeof(Double)));
                    //dtTemp.Columns.Add(new DataColumn("wechatPayAmount", typeof(Double)));
                    //dtTemp.Columns.Add(new DataColumn("commissionAmount", typeof(Double)));
                    dtTemp.Columns.Add(new DataColumn("batchMoneyApplyDetailCode", typeof(String)));
                    dtTemp.Columns.Add(new DataColumn("payeeBankName", typeof(string)));
                    //生成批量打款申请明细
                    foreach (DataRow dr in dtBatchMoneyMerchantApply.Rows)
                    {
                        DataRow drTemp = dtTemp.NewRow();
                        drTemp[0] = resultInsert;
                        drTemp[1] = 0;
                        drTemp[2] = 0;
                        drTemp[3] = Common.ToInt32(dr["companyID"]);
                        drTemp[4] = Common.ToInt32(dr["shopID"]);
                        drTemp[5] = Common.ToString(dr["accountNum"]);
                        drTemp[6] = Common.ToString(dr["bankName"]);
                        drTemp[7] = Common.ToString(dr["accountName"]);
                        //drTemp[8] = Common.ToDouble(dr["remainMoney"]);
                        drTemp[9] = "";
                        drTemp[10] = (int)BatchMoneyStatus.wait_for_check;
                        drTemp[11] = 0;
                        drTemp[12] = Common.ToInt32(dr["cityId"]);
                        //按比例计算
                        double[] amount = new double[7];
                        //amount[0] = Common.ToDouble(dr["remainRedEnvelopeAmount"]);
                        //amount[1] = Common.ToDouble(dr["remainFoodCouponAmount"]);
                        //amount[2] = Common.ToDouble(dr["remainAlipayAmount"]);
                        //amount[3] = Common.ToDouble(dr["remainWechatPayAmount"]);
                        //amount[4] = Common.ToDouble(dr["remainCommissionAmount"]);
                        //amount[5] = Common.ToDouble(dr["remainMoney"]);
                        //amount[6] = Common.ToDouble(dr["remainMoney"]) - Common.ToDouble(dr["amountFrozen"]);
                        //double[] newAmount = mathAmount(amount);
                        //drTemp[13] = Common.ToDouble(newAmount[0]);
                        //drTemp[14] = Common.ToDouble(newAmount[1]);
                        //drTemp[15] = Common.ToDouble(newAmount[2]);
                        //drTemp[16] = Common.ToDouble(newAmount[3]);
                        //drTemp[17] = Common.ToDouble(newAmount[4]);
                        drTemp[13] = Common.GetElecChequeNo;
                        drTemp[14] = Common.ToString(dr["PayeeBankName"]);
                        //打款金额
                        //drTemp[8] = Common.ToDouble(newAmount[5]);
                        drTemp[8]=Common.ToDouble(dr["remainMoney"]) - Common.ToDouble(dr["amountFrozen"]);
                        dtTemp.Rows.Add(drTemp);
                    }
                    bool flagResult = operate.BatchInsertBatchMoneyApplyDetailNew(dtTemp);//批量插入数据库

                    if (flagResult)
                    {
                        scope.Complete();
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('生成成功')</script>");

                        //recordId.Text = " 记录" + resultInsert.ToString() + " ";
                        //totleCount.Text = countObject.ToString();
                        //totleAmount.Text = sumObject.ToString();
                        //recordId.PostBackUrl = "~/CustomerServiceProcessing/batchMoneyDetail.aspx?batchMoneyApplyId=" + resultInsert;//跳转详情页面
                        //加载datagrid
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('批量打款明细添加失败')</script>");
                        return;
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('新建批量打款申请失败')</script>");
                    return;
                }

                //ViewState["vsApplyId"] = resultInsert;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('未找到符合当前条件的记录信息，或者当前门店有未提交的打款申请')</script>");
                return;
            }
        }
        //操作日志
        StoresMoneyLog modelLog = new StoresMoneyLog();
        modelLog.BatchMoneyApply_Id = resultInsert;
        modelLog.Content = "生成批量打款申请";
        modelLog.AddUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
        modelLog.AddTime = DateTime.Now;
        modelLog.BatchMoneyApplyDetail_Id = "";
        modelLog.Money = 0;
        modelLog.MoneyMerchantAccountDetail_AccountId = 0;
        modelLog.ShopInfo_ShopID = shopId;

        modelLog.AddIP = IPHelper.GetRemoteIPAddress();
        StoresMoneyLogOperate smlo = new StoresMoneyLogOperate();
        smlo.AddStoresMoneyLog(modelLog);
        AspNetPager1.CurrentPageIndex = 1;
        ddlStatus.SelectedValue = "5";
        ViewState["vsShopId"] = Common.ToInt32(Request.QueryString["id"]);
        text_ShopName.Text = string.Empty;
        loadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlStatus.SelectedValue));
    }
    protected void btn_create_city_Click(object sender, EventArgs e)
    {
        if (Common.ToDouble(txt_remainMoney_city.Text) <= 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请输入金额范围')</script>");
            return;
        }

        double minAmount = Common.ToDouble(txt_remainMoney_city.Text);
        int resultInsert = 0;
        using (TransactionScope scope = new TransactionScope())
        {
            int cityId = Common.ToInt32(ddlCity.SelectedValue);
            BatchMoneyOperate operate = new BatchMoneyOperate();
            DataTable dtBatchMoneyMerchantApply = operate.QueryBatchMoneyMerchantApplyNew(minAmount, cityId);
            if (dtBatchMoneyMerchantApply.Rows.Count > 0)
            {
                double sumObject = Common.ToDouble(dtBatchMoneyMerchantApply.Compute("Sum(remainMoney)", "1=1"));//总打款金额
                int countObject = Common.ToInt32(dtBatchMoneyMerchantApply.Compute("count(shopID)", "1=1"));//总打款记录
                int employeeId = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
                string employeeName = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
                BatchMoneyApply batchMoneyApply = new BatchMoneyApply()
                {
                    advanceAmount = sumObject,
                    createdTime = DateTime.Now,
                    operateEmployee = employeeId,
                    advanceCount = countObject,
                    practicalAmount = 0,
                    practicalCount = 0,
                    remark = employeeName + "帐号登录生成" + ddlCity.SelectedItem.Text + "批量打款记录",
                    status = 1,//有效打款记录
                    cityId = cityId
                };
                resultInsert = operate.AddBatchMoneyApply(batchMoneyApply);
                if (resultInsert > 0)
                {
                    //int flagCount = 0;
                    DataTable dtTemp = new DataTable();
                    dtTemp.Columns.Add(new DataColumn("batchMoneyApplyId", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("accountId", typeof(Int64)));
                    dtTemp.Columns.Add(new DataColumn("operateEmployee", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("companyId", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("shopId", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("accountNum", typeof(String)));
                    dtTemp.Columns.Add(new DataColumn("bankName", typeof(String)));
                    dtTemp.Columns.Add(new DataColumn("accountName", typeof(String)));
                    dtTemp.Columns.Add(new DataColumn("applyAmount", typeof(Double)));
                    dtTemp.Columns.Add(new DataColumn("serialNumberOrRemark", typeof(String)));
                    dtTemp.Columns.Add(new DataColumn("status", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("haveAdjustAmount", typeof(Double)));
                    dtTemp.Columns.Add(new DataColumn("cityId", typeof(Int32)));
                    //dtTemp.Columns.Add(new DataColumn("redEnvelopeAmount", typeof(Double)));
                    //dtTemp.Columns.Add(new DataColumn("foodCouponAmount", typeof(Double)));
                    //dtTemp.Columns.Add(new DataColumn("alipayAmount", typeof(Double)));
                    //dtTemp.Columns.Add(new DataColumn("wechatPayAmount", typeof(Double)));
                    //dtTemp.Columns.Add(new DataColumn("commissionAmount", typeof(Double)));
                    dtTemp.Columns.Add(new DataColumn("batchMoneyApplyDetailCode",typeof(String)));
                    dtTemp.Columns.Add(new DataColumn("payeeBankName",typeof(string)));
                    //生成批量打款申请明细
                    foreach (DataRow dr in dtBatchMoneyMerchantApply.Rows)
                    {
                        DataRow drTemp = dtTemp.NewRow();
                        drTemp[0] = resultInsert;
                        drTemp[1] = 0;
                        drTemp[2] = 0;
                        drTemp[3] = Common.ToInt32(dr["companyID"]);
                        drTemp[4] = Common.ToInt32(dr["shopID"]);
                        drTemp[5] = Common.ToString(dr["accountNum"]);
                        drTemp[6] = Common.ToString(dr["bankName"]);
                        drTemp[7] = Common.ToString(dr["accountName"]);
                        //drTemp[8] = Common.ToDouble(dr["remainMoney"]);
                        drTemp[9] = "";
                        drTemp[10] = (int)BatchMoneyStatus.wait_for_check; ;
                        drTemp[11] = 0;
                        drTemp[12] = Common.ToInt32(dr["cityId"]);

                        //按比例计算
                        //double[] amount = new double[7];
                        //amount[0] = Common.ToDouble(dr["remainRedEnvelopeAmount"]);
                        //amount[1] = Common.ToDouble(dr["remainFoodCouponAmount"]);
                        //amount[2] = Common.ToDouble(dr["remainAlipayAmount"]);
                        //amount[3] = Common.ToDouble(dr["remainWechatPayAmount"]);
                        //amount[4] = Common.ToDouble(dr["remainCommissionAmount"]);
                        //amount[5] = Common.ToDouble(dr["remainMoney"]);
                        //amount[6] = Common.ToDouble(dr["remainMoney"]) - Common.ToDouble(dr["amountFrozen"]);
                        //double[] newAmount = mathAmount(amount);
                        //drTemp[13] = Common.ToDouble(newAmount[0]);
                        //drTemp[14] = Common.ToDouble(newAmount[1]);
                        //drTemp[15] = Common.ToDouble(newAmount[2]);
                        //drTemp[16] = Common.ToDouble(newAmount[3]);
                        //drTemp[17] = Common.ToDouble(newAmount[4]);
                        drTemp[13] = Common.GetElecChequeNo;
                        drTemp[14] = Common.ToString(dr["payeeBankName"]);
                        //打款金额
                        drTemp[8] = Common.ToDouble(dr["remainMoney"]) - Common.ToDouble(dr["amountFrozen"]);
                        dtTemp.Rows.Add(drTemp);
                    }
                    bool flagResult = operate.BatchInsertBatchMoneyApplyDetailNew(dtTemp);//批量插入数据库

                    if (flagResult)
                    {
                        scope.Complete();
                      
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('生成成功')</script>");
                        //recordId.Text = " 记录" + resultInsert.ToString() + " ";
                        //totleCount.Text = countObject.ToString();
                        //totleAmount.Text = sumObject.ToString();
                        //recordId.PostBackUrl = "~/CustomerServiceProcessing/batchMoneyDetail.aspx?batchMoneyApplyId=" + resultInsert;//跳转详情页面
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('批量打款明细添加失败')</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('新建批量打款申请失败')</script>");
                }

                //ViewState["vsApplyId"] = resultInsert;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('未找到符合当前条件的记录信息')</script>");
                return;
            }
        }
        //操作日志
        StoresMoneyLog modelLog = new StoresMoneyLog();
        modelLog.BatchMoneyApply_Id = resultInsert;
        modelLog.Content = "生成批量打款申请";
        modelLog.AddUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
        modelLog.AddTime = DateTime.Now;
        modelLog.BatchMoneyApplyDetail_Id = "";
        modelLog.Money = 0;
        modelLog.MoneyMerchantAccountDetail_AccountId = 0;
        modelLog.ShopInfo_ShopID = 0;

        modelLog.AddIP = IPHelper.GetRemoteIPAddress();
        StoresMoneyLogOperate smlo = new StoresMoneyLogOperate();
        smlo.AddStoresMoneyLog(modelLog);
        AspNetPager1.CurrentPageIndex = 1;
        ddlStatus.SelectedValue = "5";
        text_ShopName.Text = string.Empty;
        ViewState["vsShopId"] = 0;
        text.Value = string.Empty;
        txt_companyName.Text = string.Empty;
        txt_remainMoney.Text = string.Empty;
        loadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlStatus.SelectedValue));
    }
    protected void btnAllSubmit_Click(object sender, EventArgs e)
    {
        BatchMoneyApplyDetail model = new BatchMoneyApplyDetail();
        BatchMoneyOperate bmo = new BatchMoneyOperate();
        ShopInfo shopModel = new ShopInfo();
        ShopOperate so = new ShopOperate();
        int k = 0;
        string errorID = string.Empty;
        string failID = string.Empty;
        string detailId = string.Empty;
        string errorStatus = string.Empty;

        for (int i = 0; i < gdList.Rows.Count; i++)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                HtmlInputCheckBox cbSelect = (HtmlInputCheckBox)gdList.Rows[i].FindControl("gdCheck");
                if (cbSelect.Checked)
                {//修改状态
                    k++;
                    if (erroBankInfo(i))
                    {
                        int batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString());
                        DataTable dt = bmo.SelectBatchMoneyApplyDetailByBatchMoneyApplyDetailIdNew(batchMoneyApplyDetailId);
                        if (!dt.Rows[0]["status"].ToString().Equals(((int)BatchMoneyStatus.wait_for_check).ToString()))
                        {
                            errorStatus += gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString() + ",";
                            continue;
                        }

                        bool result = false;
                        model.batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString());
                        model.shopId = Common.ToInt32(gdList.DataKeys[i].Values["shopID"].ToString());
                        model.companyId = Common.ToInt32(gdList.DataKeys[i].Values["companyId"].ToString());
                        model.applyAmount = Common.ToDouble(gdList.DataKeys[i].Values["applyAmount"].ToString());
                        model.status = BatchMoneyStatus.wait_for_confirm;
                        result = bmo.ModifyBatchMoneyApplyDetailStatus(model, ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID.ToString());

                        //修改商铺金额
                        shopModel.remainMoney = Common.ToDouble(gdList.DataKeys[i].Values["applyAmount"].ToString());
                        shopModel.shopID = Common.ToInt32(gdList.DataKeys[i].Values["shopID"].ToString());
                        //shopModel.remainRedEnvelopeAmount = Common.ToDouble(gdList.DataKeys[i].Values["redEnvelopeAmount"].ToString());
                        //shopModel.remainFoodCouponAmount = Common.ToDouble(gdList.DataKeys[i].Values["foodCouponAmount"].ToString());
                        //shopModel.remainAlipayAmount = Common.ToDouble(gdList.DataKeys[i].Values["alipayAmount"].ToString());
                        //shopModel.remainWechatPayAmount = Common.ToDouble(gdList.DataKeys[i].Values["wechatPayAmount"].ToString());
                        //shopModel.remainCommissionAmount = Common.ToDouble(gdList.DataKeys[i].Values["commissionAmount"].ToString());
                        bool result1 = so.ModifyShopMoneyApply(shopModel);

                        if (result == false)
                        {
                            failID += gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString() + ",";
                        }
                        else
                        {
                            detailId += gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString() + ",";
                        }
                    }
                    else
                    {
                        errorID += gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString() + ",";
                    }
                }
                scope.Complete();
            }

            if (detailId.Length > 0)
            {
                detailId = detailId.Substring(0, detailId.Length - 1);
            }

            if (errorID.Length > 0)
            {
                errorID = errorID.Substring(0, errorID.Length - 1);
            }

            if (failID.Length > 0)
            {
                failID = failID.Substring(0, failID.Length - 1);
            }
            if (errorStatus.Length > 0)
            {
                errorStatus = errorStatus.Substring(0, errorStatus.Length - 1);
            }
        }
        if (k.Equals(0))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请先勾选需要提交的明细')</script>");
        }
        else
        {
            if (detailId.Length > 0)
            {
                //操作日志
                StoresMoneyLog modelLog = new StoresMoneyLog();
                modelLog.BatchMoneyApplyDetail_Id = detailId;
                modelLog.Content = "批量提交申请";
                modelLog.AddUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
                modelLog.AddTime = DateTime.Now;
                modelLog.Money = 0;
                modelLog.MoneyMerchantAccountDetail_AccountId = 0;
                modelLog.ShopInfo_ShopID = 0;
                modelLog.AddIP = IPHelper.GetRemoteIPAddress();

                StoresMoneyLogOperate smlo = new StoresMoneyLogOperate();
                smlo.AddStoresMoneyDetailLog(modelLog);
            }

            //allCheck.Checked = false;
            int index = AspNetPager1.CurrentPageIndex;
            loadData((index - 1) * Common.ToInt32(Label_LargePageCount.Text), (index - 1) * Common.ToInt32(Label_LargePageCount.Text) + Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlStatus.SelectedValue));

            string msg = string.Empty;
            if (errorID.Length > 0)
            {
                msg = "单号" + errorID + "银行帐户信息不全，请补充完整!";
            }
            if(failID.Length>0)
            {
                msg = "单号" + failID + "提交失败，请验证门店当前余额是否大于等于当前提取金额（注意冻结金额）!";
            }
            if (errorStatus.Length > 0)
            {
                msg = "单号" + errorStatus + "状态有误!";
            }

            if (msg.Length > 0)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + msg + "')</script>");
            }
        }
    }

    /// <summary>
    /// 加载列表
    /// </summary>
    /// <param name="shopId"></param>
    private void loadData(int str, int end,int status)
    {
        BatchMoneyOperate bmo = new BatchMoneyOperate();
        DataTable dt = bmo.SelectBatchMoneyApplyDetailByShop(Common.ToInt32(ViewState["vsShopId"]), status, Common.ToInt32(ddlCitySearch.SelectedValue), Common.ToInt32(ddlIsFirst.SelectedValue), text_ShopName.Text.Trim());
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
        for (int i = 0; i < gdList.Rows.Count; i++)
        {
            LinkButton lbtnUpdate = (LinkButton)gdList.Rows[i].FindControl("lbtnUpdate");
            LinkButton lbtnCancel = (LinkButton)gdList.Rows[i].FindControl("lbtnCancel");
           
            //申请未提交
            if (!gdList.DataKeys[i].Values["status"].ToString().Equals("申请未提交"))
            {
                HtmlInputCheckBox gdCheck = (HtmlInputCheckBox)gdList.Rows[i].FindControl("gdCheck");
                gdCheck.Disabled = true;
                lbtnCancel.Enabled = false;
                lbtnUpdate.Enabled = false;
            }
            else
            {
                lbtnCancel.Attributes.Add("onclick", "return confirm('删除后列表将不再显示该申请，确认删除？');");
                string rid = string.Empty;
                string rname = string.Empty;
                if (Request.QueryString["id"] != null)
                {
                    rid = Request.QueryString["id"].ToString();
                }
                if (Request.QueryString["name"] != null)
                {
                    rname = Request.QueryString["name"].ToString();
                }
                lbtnUpdate.PostBackUrl = "~/FinanceManage/batchMoneyApplyDetailDS.aspx?id=" + rid + "&name=" + rname + "&batchMoneyApplyDetailId=" + gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString() + "&Pid=" + gdList.DataKeys[i].Values["batchMoneyApplyId"].ToString();
            }
            ////交易量
            //gdList.Rows[i].Cells[10].Text = (Convert.ToDouble(gdList.DataKeys[i].Values["commissionAmount"].ToString()) + Convert.ToDouble(gdList.DataKeys[i].Values["applyAmount"].ToString())).ToString();

            ////计划实际佣金比例（暂不考虑每单固定抽成，否则比例没有实际意义）
            //if (gdList.DataKeys[i].Values["viewallocCommissionType"].ToString().Equals("2"))
            //{
            //    gdList.Rows[i].Cells[14].Text = "0.00";
            //    if (Common.ToDouble(gdList.Rows[i].Cells[10].Text) != 0)
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
    }
    protected void allCheck_CheckedChanged(object sender, EventArgs e)
    {
        if (allCheck.Checked)
        {
            gdCheckOperate(true);
        }
        else
        {
            gdCheckOperate(false);
        }
    }

    /// <summary>
    /// 全选事件
    /// </summary>
    private void gdCheckOperate(bool checkFlag)
    {
        for (int i = 0; i < gdList.Rows.Count; i++)
        {
            CheckBox gdCheck = (CheckBox)gdList.Rows[i].FindControl("gdCheck");
            if (gdCheck.Enabled.Equals(true))
            {
                gdCheck.Checked = checkFlag;
            }
        }
    }

    protected void btnAllCancel_Click(object sender, EventArgs e)
    {
        BatchMoneyApplyDetail model = new BatchMoneyApplyDetail();
        BatchMoneyOperate bmo = new BatchMoneyOperate();
        int k = 0;
        string detailId = string.Empty;
        string errorStatus = string.Empty;

        using (TransactionScope scope = new TransactionScope())
        {
            for (int i = 0; i < gdList.Rows.Count; i++)
            {
                HtmlInputCheckBox cbSelect = (HtmlInputCheckBox)gdList.Rows[i].FindControl("gdCheck");
                if (cbSelect.Checked)
                {//修改状态
                    k++;

                    int batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString());
                    DataTable dt = bmo.SelectBatchMoneyApplyDetailByBatchMoneyApplyDetailIdNew(batchMoneyApplyDetailId);
                    if (!dt.Rows[0]["status"].ToString().Equals(((int)BatchMoneyStatus.wait_for_check).ToString()))
                    {
                        errorStatus += gdList.DataKeys[i].Values["batchMoneyApplyDetailCode"].ToString() + ",";
                        continue;
                    }

                    model.batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString());
                    model.status = BatchMoneyStatus.delete;
                    bool result = bmo.ModifyBatchMoneyApplyDetailStatus(model,((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID.ToString());
                    if (result == false)
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('批量删除失败')</script>");
                        return;
                    }
                    detailId += gdList.DataKeys[i].Values["batchMoneyApplyDetailId"].ToString() + ",";
                }
            }

            if (k.Equals(0))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('请先勾选需要提交的明细')</script>");
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
            modelLog.Content = "批量打款申请删除";
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
        if (errorStatus.Length > 0)
        {
            msg = "单号" + errorStatus + "状态有误!";
        }

        if (msg.Length > 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('" + msg + "')</script>");
        }
    }
    //protected void gdList_ItemCommand(object source, DataGridCommandEventArgs e)
    //{
    //    //if (e.CommandName.Equals("update"))
    //    //{//跳转到修改页面
    //    //    LinkButton lbtnUpdate = (LinkButton)e.Item.FindControl("lbtnUpdate");
    //    //}
    //    //else 
    //    if (e.CommandName.Equals("del"))
    //    {
    //        BatchMoneyApplyDetail model = new BatchMoneyApplyDetail();
    //        BatchMoneyOperate bmo = new BatchMoneyOperate();

    //        string detailId = string.Empty;
    //        using (TransactionScope scope = new TransactionScope())
    //        {
    //            model.batchMoneyApplyDetailId = Common.ToInt32(e.Item.Cells[2].Text);
    //            model.status = -1;
    //            bool result = bmo.ModifyBatchMoneyApplyDetailStatus(model);
    //            if (result == false)
    //            {
    //                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除失败')</script>");
    //                return;
    //            }
    //            detailId = e.Item.Cells[2].Text;
    //            scope.Complete();
    //        }
    //        StoresMoneyLog modelLog = new StoresMoneyLog();
    //        modelLog.BatchMoneyApplyDetail_Id = detailId;
    //        modelLog.Content = "打款申请删除";
    //        modelLog.AddUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
    //        modelLog.AddTime = DateTime.Now;
    //        IPAddress[] arrIPAddresses = Dns.GetHostAddresses(Dns.GetHostName());
    //        foreach (IPAddress ip in arrIPAddresses)
    //        {
    //            if (ip.AddressFamily.Equals(AddressFamily.InterNetwork))
    //            {
    //                modelLog.AddIP = ip.ToString();
    //            }
    //        }

    //        StoresMoneyLogOperate smlo = new StoresMoneyLogOperate();
    //        smlo.AddStoresMoneyDetailLog(modelLog);
    //        AspNetPager1.CurrentPageIndex = 1;
    //        loadData(0, Common.ToInt32(Label_LargePageCount.Text), 0);
    //    }
    //}
    /// <summary>
    /// 分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        loadData(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex, Common.ToInt32(ddlStatus.SelectedValue));
    }


    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        AspNetPager1.CurrentPageIndex = 1;
        loadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlStatus.SelectedValue));
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        CreateExcel(GetGrData());
    }

    private DataTable GetGrData()
    {
        DataTable dt = new DataTable();
        BatchMoneyOperate bmo = new BatchMoneyOperate();
        DataTable dtApply = bmo.SelectBatchMoneyApplyDetailByShop(Common.ToInt32(ViewState["vsShopId"]), Common.ToInt32(ddlStatus.SelectedValue), Common.ToInt32(ddlCitySearch.SelectedValue), Common.ToInt32(ddlIsFirst.SelectedValue), text_ShopName.Text.Trim());

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
    protected void GridView_OrderStatistics_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("del"))
        {
            BatchMoneyApplyDetail model = new BatchMoneyApplyDetail();
            BatchMoneyOperate bmo = new BatchMoneyOperate();
             int index = ((GridViewRow)(((LinkButton)e.CommandSource).Parent.Parent)).RowIndex;//转换为按钮类型，获取其所在的行的索引
            string detailId = string.Empty;

            int batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[index].Values["batchMoneyApplyDetailId"].ToString());
            DataTable dt = bmo.SelectBatchMoneyApplyDetailByBatchMoneyApplyDetailIdNew(batchMoneyApplyDetailId);
            if (!dt.Rows[0]["status"].ToString().Equals(((int)BatchMoneyStatus.wait_for_check).ToString()))
            {
                 Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('非申请未提交状态不可删除')</script>");
                 return;
            }

            using (TransactionScope scope = new TransactionScope())
            {
                model.batchMoneyApplyDetailId = Common.ToInt32(gdList.DataKeys[index].Values["batchMoneyApplyDetailId"].ToString());
                model.status = BatchMoneyStatus.delete;
                bool result = bmo.ModifyBatchMoneyApplyDetailStatus(model, ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID.ToString());
                if (result == false)
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('删除失败')</script>");
                    return;
                }
                detailId = gdList.DataKeys[index].Values["batchMoneyApplyDetailId"].ToString();
                scope.Complete();
            }
            StoresMoneyLog modelLog = new StoresMoneyLog();
            modelLog.BatchMoneyApplyDetail_Id = detailId;
            modelLog.Content = "打款申请删除";
            modelLog.AddUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
            modelLog.AddTime = DateTime.Now;
            modelLog.AddIP = IPHelper.GetRemoteIPAddress();

            StoresMoneyLogOperate smlo = new StoresMoneyLogOperate();
            smlo.AddStoresMoneyDetailLog(modelLog);
            AspNetPager1.CurrentPageIndex = 1;
            loadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlStatus.SelectedValue));
        }
    }
    protected void gdList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.Header)
        //{
        //    if (ViewState["SortedField"] != null)
        //    {
        //        Dictionary<string, string> order = (Dictionary<string, string>)ViewState["SortedField"];
        //        for (int i = 0; i < e.Row.Cells.Count; i++)
        //        {
        //            if (e.Row.Cells[i].Controls.Count > 0)
        //            {
        //                LinkButton lb = e.Row.Cells[i].Controls[0] as LinkButton;
        //                if (lb != null)
        //                {
        //                    Literal li = new Literal();
        //                    if (order.ContainsKey(lb.CommandArgument))
        //                    {
        //                        if (order[lb.CommandArgument] == "ASC")
        //                        {
        //                            li.Text = "▲";
        //                        }
        //                        else
        //                        {
        //                            li.Text = "▼";
        //                        }
        //                    }
        //                    else
        //                    {
        //                        li.Text = "▲▼";

        //                    }
        //                    e.Row.Cells[i].Controls.Add(li);
        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        for (int i = 0; i < e.Row.Cells.Count; i++)
        //        {
        //            if (e.Row.Cells[i].Controls.Count > 0)
        //            {
        //                LinkButton lb = e.Row.Cells[i].Controls[0] as LinkButton;
        //                if (lb != null)
        //                {
        //                    Literal li = new Literal();
        //                    li.Text = "▲▼";
        //                    e.Row.Cells[i].Controls.Add(li);
        //                }
        //            }
        //        }
        //    }
        //}
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

    /// <summary>
    /// 红包，粮票，支付宝，微信按比例提取
    /// 传入值顺序为 红包、粮票、支付宝、微信、佣金、可用余额、提交额度
    /// 返回顺序 红包，粮票，支付宝，微信，佣金，提交额度
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    private double[] mathAmount(double[] amount)
    {
        double[] newAmount = new double[6];
        //double redP = Math.Round(amount[0] / amount[5], 2);
        ////double foodP = Math.Round(amount[1] / amount[5], 2);
        //double aliP = Math.Round(amount[2] / amount[5], 2);
        //double weP = Math.Round(amount[3] / amount[5], 2);
        //double realamount = amount[5] - amount[6];余额有可能为用户输入 so这里不固定死作为[6]传入
        double realamount = amount[6];

        double amountP = Math.Round(amount[6] / amount[5], 8);

        newAmount[0] = Math.Round(amount[0] * amountP,2);
        newAmount[1] = Math.Round(amount[1] * amountP, 2);
        newAmount[2] = Math.Round(amount[2] * amountP, 2);
        newAmount[3] = Math.Round(amount[3] * amountP, 2);

        newAmount[4] = Math.Round(realamount / amount[5] * amount[4], 2);
        newAmount[5] = realamount;

        double overplus = Math.Round(realamount - newAmount[0] - newAmount[1] - newAmount[2] - newAmount[3],2);
        if (overplus > 0)
        {
            if (newAmount[0] > 0)
            {
                newAmount[0] += overplus;
            }
            else if (newAmount[1] > 0)
            {
                newAmount[1] += overplus;
            }
            else if (newAmount[3] > 0)
            {
                newAmount[3] += overplus;
            }
            else if (newAmount[2] > 0)
            {
                newAmount[2] += overplus;
            }
        }
        else if (overplus < 0)
        {
            overplus = overplus * -1;

            if (newAmount[0] > overplus)
            {
                newAmount[0] -= overplus;
            }
            else if (newAmount[1] > overplus)
            {
                newAmount[1] -= overplus;
            }
            else if (newAmount[3] > overplus)
            {
                newAmount[3] -= overplus;
            }
            else if (newAmount[2] > overplus)
            {
                newAmount[2] -= overplus;
            }
        }

        return newAmount;
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //if (this.text.Value.Equals(string.Empty))
        //{
        //    ViewState["vsShopId"] = 0;
        //}
        //else
        //{
        //    ViewState["vsShopId"] = Common.ToInt32(Request.QueryString["id"]);
        //}
        AspNetPager1.CurrentPageIndex = 1;
        ViewState["vsShopId"] = 0;
        loadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlStatus.SelectedValue));
    }

    private bool erroBankInfo(int index)
    {
        if (gdList.DataKeys[index].Values["bankName"].ToString().Equals(string.Empty) || gdList.DataKeys[index].Values["bankName"].ToString().Equals("&nbsp;")
                        || gdList.DataKeys[index].Values["PayeeBankName"].ToString().Equals(string.Empty) || gdList.DataKeys[index].Values["PayeeBankName"].ToString().Equals("&nbsp;")
                        || gdList.DataKeys[index].Values["accountName"].ToString().Equals(string.Empty) || gdList.DataKeys[index].Values["accountName"].ToString().Equals("&nbsp;")
                         || gdList.DataKeys[index].Values["accountNum"].ToString().Equals(string.Empty) || gdList.DataKeys[index].Values["accountNum"].ToString().Equals("&nbsp;"))
        {
            return false;
        }
        return true;

    }
    protected void btnWithdrawType_Click(object sender, EventArgs e)
    {
        double minAmount = Common.ToDouble(txt_remainMoney_city.Text);
        int resultInsert = 0;
        using (TransactionScope scope = new TransactionScope())
        {
            int cityId = Common.ToInt32(ddlCityWithdrawType.SelectedValue);
            BatchMoneyOperate operate = new BatchMoneyOperate();
            DataTable dtBatchMoneyMerchantApply = operate.QueryBatchMoneyMerchantApplyByWithdrawType(cityId);
            if (dtBatchMoneyMerchantApply.Rows.Count > 0)
            {
                double sumObject = Common.ToDouble(dtBatchMoneyMerchantApply.Compute("Sum(remainMoney)", "1=1"));//总打款金额
                int countObject = Common.ToInt32(dtBatchMoneyMerchantApply.Compute("count(shopID)", "1=1"));//总打款记录
                int employeeId = ((VAEmployeeLoginResponse)Session["UserInfo"]).employeeID;
                string employeeName = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
                BatchMoneyApply batchMoneyApply = new BatchMoneyApply()
                {
                    advanceAmount = sumObject,
                    createdTime = DateTime.Now,
                    operateEmployee = employeeId,
                    advanceCount = countObject,
                    practicalAmount = 0,
                    practicalCount = 0,
                    remark = employeeName + "帐号登录生成" + ddlCityWithdrawType.SelectedItem.Text + "批量打款记录",
                    status = 1,//有效打款记录
                    cityId = cityId
                };
                resultInsert = operate.AddBatchMoneyApply(batchMoneyApply);
                if (resultInsert > 0)
                {
                    //int flagCount = 0;
                    DataTable dtTemp = new DataTable();
                    dtTemp.Columns.Add(new DataColumn("batchMoneyApplyId", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("accountId", typeof(Int64)));
                    dtTemp.Columns.Add(new DataColumn("operateEmployee", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("companyId", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("shopId", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("accountNum", typeof(String)));
                    dtTemp.Columns.Add(new DataColumn("bankName", typeof(String)));
                    dtTemp.Columns.Add(new DataColumn("accountName", typeof(String)));
                    dtTemp.Columns.Add(new DataColumn("applyAmount", typeof(Double)));
                    dtTemp.Columns.Add(new DataColumn("serialNumberOrRemark", typeof(String)));
                    dtTemp.Columns.Add(new DataColumn("status", typeof(Int32)));
                    dtTemp.Columns.Add(new DataColumn("haveAdjustAmount", typeof(Double)));
                    dtTemp.Columns.Add(new DataColumn("cityId", typeof(Int32)));
                    //dtTemp.Columns.Add(new DataColumn("redEnvelopeAmount", typeof(Double)));
                    //dtTemp.Columns.Add(new DataColumn("foodCouponAmount", typeof(Double)));
                    //dtTemp.Columns.Add(new DataColumn("alipayAmount", typeof(Double)));
                    //dtTemp.Columns.Add(new DataColumn("wechatPayAmount", typeof(Double)));
                    //dtTemp.Columns.Add(new DataColumn("commissionAmount", typeof(Double)));
                    dtTemp.Columns.Add(new DataColumn("batchMoneyApplyDetailCode", typeof(String)));
                    dtTemp.Columns.Add(new DataColumn("payeeBankName", typeof(string)));
                    //生成批量打款申请明细
                    foreach (DataRow dr in dtBatchMoneyMerchantApply.Rows)
                    {
                        DataRow drTemp = dtTemp.NewRow();
                        drTemp[0] = resultInsert;
                        drTemp[1] = 0;
                        drTemp[2] = 0;
                        drTemp[3] = Common.ToInt32(dr["companyID"]);
                        drTemp[4] = Common.ToInt32(dr["shopID"]);
                        drTemp[5] = Common.ToString(dr["accountNum"]);
                        drTemp[6] = Common.ToString(dr["bankName"]);
                        drTemp[7] = Common.ToString(dr["accountName"]);
                        //drTemp[8] = Common.ToDouble(dr["remainMoney"]);
                        drTemp[9] = "";
                        drTemp[10] = (int)BatchMoneyStatus.wait_for_check;
                        drTemp[11] = 0;
                        drTemp[12] = Common.ToInt32(dr["cityId"]);
                        //按比例计算
                        double[] amount = new double[7];
                        //amount[0] = Common.ToDouble(dr["remainRedEnvelopeAmount"]);
                        //amount[1] = Common.ToDouble(dr["remainFoodCouponAmount"]);
                        //amount[2] = Common.ToDouble(dr["remainAlipayAmount"]);
                        //amount[3] = Common.ToDouble(dr["remainWechatPayAmount"]);
                        //amount[4] = Common.ToDouble(dr["remainCommissionAmount"]);
                        //amount[5] = Common.ToDouble(dr["remainMoney"]);
                        //amount[6] = Common.ToDouble(dr["remainMoney"]) - Common.ToDouble(dr["amountFrozen"]);
                        //double[] newAmount = mathAmount(amount);
                        //drTemp[13] = Common.ToDouble(newAmount[0]);
                        //drTemp[14] = Common.ToDouble(newAmount[1]);
                        //drTemp[15] = Common.ToDouble(newAmount[2]);
                        //drTemp[16] = Common.ToDouble(newAmount[3]);
                        //drTemp[17] = Common.ToDouble(newAmount[4]);
                        drTemp[13] = Common.GetElecChequeNo;
                        drTemp[14] = Common.ToString(dr["PayeeBankName"]);
                        //打款金额
                        //drTemp[8] = Common.ToDouble(newAmount[5]);
                        drTemp[8] = Common.ToDouble(dr["remainMoney"]) - Common.ToDouble(dr["amountFrozen"]);
                        dtTemp.Rows.Add(drTemp);
                    }
                    bool flagResult = operate.BatchInsertBatchMoneyApplyDetailNew(dtTemp);//批量插入数据库

                    if (flagResult)
                    {
                        scope.Complete();

                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('生成成功')</script>");
                        //recordId.Text = " 记录" + resultInsert.ToString() + " ";
                        //totleCount.Text = countObject.ToString();
                        //totleAmount.Text = sumObject.ToString();
                        //recordId.PostBackUrl = "~/CustomerServiceProcessing/batchMoneyDetail.aspx?batchMoneyApplyId=" + resultInsert;//跳转详情页面
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('批量打款明细添加失败')</script>");
                    }
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('新建批量打款申请失败')</script>");
                }

                //ViewState["vsApplyId"] = resultInsert;
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('未找到符合当前条件的记录信息')</script>");
                return;
            }
        }
        //操作日志
        StoresMoneyLog modelLog = new StoresMoneyLog();
        modelLog.BatchMoneyApply_Id = resultInsert;
        modelLog.Content = "生成批量打款申请";
        modelLog.AddUser = ((VAEmployeeLoginResponse)Session["UserInfo"]).userName;
        modelLog.AddTime = DateTime.Now;
        modelLog.BatchMoneyApplyDetail_Id = "";
        modelLog.Money = 0;
        modelLog.MoneyMerchantAccountDetail_AccountId = 0;
        modelLog.ShopInfo_ShopID = 0;

        modelLog.AddIP = IPHelper.GetRemoteIPAddress();
        StoresMoneyLogOperate smlo = new StoresMoneyLogOperate();
        smlo.AddStoresMoneyLog(modelLog);
        AspNetPager1.CurrentPageIndex = 1;
        ddlStatus.SelectedValue = "5";
        text_ShopName.Text = string.Empty;
        ViewState["vsShopId"] = 0;
        text.Value = string.Empty;
        txt_companyName.Text = string.Empty;
        txt_remainMoney.Text = string.Empty;
        loadData(0, Common.ToInt32(Label_LargePageCount.Text), Common.ToInt32(ddlStatus.SelectedValue));
    }
}