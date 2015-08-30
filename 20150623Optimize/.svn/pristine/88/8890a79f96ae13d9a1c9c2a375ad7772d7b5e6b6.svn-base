using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;
using System.Transactions;
using System.Web.Services;

public partial class Customer_CustomerRetrospect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string mobile = Request.QueryString["MobilePhoneNumber"];
            if (!string.IsNullOrEmpty(mobile))
            {
                BindCustomerInfo(mobile, 0, 5);
                BindRedEnvelope(mobile, 0, 5);
            }
        }
    }

    /// <summary>
    /// 根据电话号码，找到此用户登录过的设备，再找到这些设备被哪些用户登录过
    /// </summary>
    /// <param name="PageSize">每页多少条</param>
    /// <param name="PageIndex">第几页</param>
    /// <param name="phone"></param>
    private void BindCustomerInfo(string phone, int str, int end)
    {
        CustomerOperate customerOper = new CustomerOperate();
        DataTable dtCustomer = customerOper.QueryCustomerInfoByPhone(phone);



        if (dtCustomer.Rows.Count <= 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "Phone", "<script>alert('当前手机号码用户不存在')</script>");
            this.gdvCustomerList.DataSource = null;
            this.gdvCustomerList.DataBind();
            this.gdvPaidOrder.DataSource = null;
            this.gdvPaidOrder.DataBind();
            AspNetPager2.Visible = false;
            return;
        }
        if (dtCustomer != null && dtCustomer.Rows.Count > 0)
        {
            var ordersCount = new PreOrder19dianOperate().GetMobileOrdersCount(phone);

            dtCustomer.Columns.Add("ordersCount", Type.GetType("System.Int32"));
            dtCustomer.Rows[0]["ordersCount"] = ordersCount.Item1;
            dtCustomer.Columns.Add("ordersAmount", Type.GetType("System.Decimal"));
            dtCustomer.Rows[0]["ordersAmount"] = ordersCount.Item2;

            this.gdvCustomerList.DataSource = dtCustomer;
            this.gdvCustomerList.DataBind();
            ViewState["customerId"] = Common.ToInt64(dtCustomer.Rows[0]["CustomerID"]);

            Label lbexe = gdvCustomerList.Rows[0].FindControl("executedRedEnvelopeAmount") as Label;
            Label lbnot = gdvCustomerList.Rows[0].FindControl("notExecutedRedEnvelopeAmount") as Label;

            RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
            double[] redEnvelope = redEnvelopeOperate.QueryCustomerRedEnvelope(phone);
            lbexe.Text = redEnvelope[0].ToString();
            lbnot.Text = redEnvelope[1].ToString();

            BindPaidOrder(Common.ToInt64(dtCustomer.Rows[0]["CustomerID"]), 0, 5);
        }
        else
        {
            this.gdvCustomerList.DataSource = null;
            this.gdvCustomerList.DataBind();
            this.gdvPaidOrder.DataSource = null;
            this.gdvPaidOrder.DataBind();
            AspNetPager2.Visible = false;
        }
    }

    private void BindPaidOrder(long customerId, int str, int end)
    {
        AspNetPager2.Visible = true;
        PreOrder19dianOperate preOrderOper = new PreOrder19dianOperate();
        Tuple<int, DataTable> dtPaidOrder = preOrderOper.GetPagePaidOrderByCustomerId(customerId, str, end);
        if (dtPaidOrder != null && dtPaidOrder.Item1 > 0)
        {
            int Count = dtPaidOrder.Item1;//总数
            AspNetPager2.RecordCount = Count;
            DataTable dt_page = dtPaidOrder.Item2;
            this.gdvPaidOrder.DataSource = dt_page;
            this.gdvPaidOrder.DataBind();
            this.AspNetPager2.Visible = true;
        }
        else
        {
            this.gdvPaidOrder.DataSource = null;
            this.gdvPaidOrder.DataBind();
            this.AspNetPager2.Visible = false;
        }
        for (int i = 0; i < gdvPaidOrder.Rows.Count; i++)
        {
            Label payModeDetail = gdvPaidOrder.Rows[i].FindControl("payModeDetail") as Label;
            //string thirdPayType = Common.ToString(gdvPaidOrder.DataKeys[i].Values["thirdPayType"]);

            PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
            double extendPay = preOrder19dianOperate.SelectExtendPay(Convert.ToInt64(gdvPaidOrder.DataKeys[i].Values["preOrder19dianId"]));//额外收取的钱

            //double thirdTotalFee = Common.ToDouble(gdvPaidOrder.DataKeys[i].Values["thirdTotalFee"]);
            //thirdTotalFee = thirdTotalFee - extendPay;

            //double prePaidSum = Common.ToDouble(gdvPaidOrder.DataKeys[i].Values["prePaidSum"]);
            //double consumeRedEnvelopeAmount = Common.ToDouble(gdvPaidOrder.DataKeys[i].Values["consumeRedEnvelopeAmount"]);
            //if (prePaidSum == thirdTotalFee)
            //{
            //    payModeDetail.Text = thirdPayType + "支付" + prePaidSum + "元";
            //}
            //else
            //{
            var amountDetails = new Preorder19DianLineOperate().GetListOfPreorder19DianId(Convert.ToInt64(gdvPaidOrder.DataKeys[i].Values["preOrder19dianId"]));
            foreach (var item in amountDetails)
                payModeDetail.Text += string.Format("{0}{1} ", Common.GetEnumDescription((VAOrderUsedPayMode)item.PayType), item.Amount);
            //}
            //if (extendPay > 0)
            //{
            //    payModeDetail.Text += "，扩展支付" + extendPay + "元";
            //}

            Label status = gdvPaidOrder.Rows[i].FindControl("status") as Label;
            int statusO = Common.ToInt32(gdvPaidOrder.DataKeys[i].Values["status"]);
            switch ((VAPreorderStatus)statusO)
            {
                case VAPreorderStatus.Initial:
                    status.Text = "未提交";
                    break;
                case VAPreorderStatus.Uploaded:
                    status.Text = "已提交";
                    break;
                case VAPreorderStatus.Prepaid:
                    status.Text = "已付款";
                    break;
                case VAPreorderStatus.Completed:
                    status.Text = "已审核";
                    break;
                case VAPreorderStatus.Deleted:
                    status.Text = "已删除";
                    break;
                case VAPreorderStatus.Refund:
                    status.Text = "已退款";
                    break;
                case VAPreorderStatus.Overtime:
                    status.Text = "已过期";
                    break;
                case VAPreorderStatus.OriginalRefunding:
                    status.Text = "退款中";
                    break;
            }
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        gdvCustomerList.DataSource = null;
        gdvCustomerList.DataBind();
        gdvPaidOrder.DataSource = null;
        gdvPaidOrder.DataBind();
        gdvRedEnvelopeConn.DataSource = null;
        gdvRedEnvelopeConn.DataBind();
        gdvReEnvelopeDetail.DataSource = null;
        gdvReEnvelopeDetail.DataBind();
        string phone = TextBox_mobilePhoneNumber.Text.Trim();

        if (string.IsNullOrEmpty(phone) || (!string.IsNullOrEmpty(phone) && phone.Length != 11))
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "Phone", "<script>alert('请输入正确的电话号码！')</script>");
            return;
        }
        BindCustomerInfo(phone, 0, 5);
        BindRedEnvelope(phone, 0, 5);
    }

    protected void lnkbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "change":
                string cookie = e.CommandArgument.ToString();
                this.hidCookie.Value = cookie;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 调整余额
    /// </summary>
    /// <param name="cookie"></param>
    /// <param name="amount">金额</param>
    /// <param name="remark">备注</param>
    /// <returns></returns>
    [WebMethod]
    public static string changeBalance(string cookie, double amount, string remark)
    {
        if (!string.IsNullOrEmpty(cookie) && !string.IsNullOrEmpty(remark) && amount != 0)
        {
            CustomerOperate customerOper = new CustomerOperate();
            using (TransactionScope ts = new TransactionScope())
            {
                VAEmployeeLoginResponse vAEmployeeLoginResponse = ((VAEmployeeLoginResponse)HttpContext.Current.Session["UserInfo"]);//根据session获取用户信息
                int employeeId = vAEmployeeLoginResponse.employeeID;
                bool change = customerOper.CustomerRecharge("", amount, cookie);
                if (change)
                {
                    RechargeLog log = new RechargeLog();
                    log.amount = amount;
                    log.cookie = cookie;
                    log.customerPhone = "";
                    log.employeeId = employeeId;
                    log.remark = remark;
                    log.operateTime = DateTime.Now;

                    RechargeLogOperate logOperate = new RechargeLogOperate();
                    if (logOperate.Add(log) == true)
                    {
                        ts.Complete();
                        return "充值成功";
                    }
                    else
                    {
                        return "日志添加失败";
                    }
                }
                else
                {
                    return "充值失败";
                }
            }
        }
        else
        {
            return "请输入正确的余额和备注";
        }
    }

    /// <summary>
    /// 分页操作
    /// </summary>
    protected void AspNetPager2_PageChanged(object sender, EventArgs e)
    {
        if (ViewState["customerId"] != null && ViewState["customerId"].ToString() != "")
        {
            BindPaidOrder(Common.ToInt32(ViewState["customerId"]), AspNetPager2.StartRecordIndex - 1, AspNetPager2.EndRecordIndex);
        }
    }

    private void BindRedEnvelope(string mobilePhoneNumber, int str, int end)
    {
        RedEnvelopeOperate operate = new RedEnvelopeOperate();
        DataTable dt = operate.QueryRedEnvelope(mobilePhoneNumber);
        if (dt != null && dt.Rows.Count > 0)
        {
            this.gdvReEnvelopeDetail.DataSource = dt;
            this.AspNetPager3.RecordCount = dt.Rows.Count;
        }
        else
        {
            this.gdvReEnvelopeDetail.DataSource = null;
        }
        this.gdvReEnvelopeDetail.DataBind();
    }

    /// <summary>
    /// 红包领取详情分页
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AspNetPager3_PageChanged(object sender, EventArgs e)
    {
        BindRedEnvelope(TextBox_mobilePhoneNumber.Text.Trim(), AspNetPager2.StartRecordIndex - 1, AspNetPager3.EndRecordIndex);
    }

    protected void lkbDetail_OnCommand(object sender, CommandEventArgs e)
    {
        long redEnvelopeId = Common.ToInt64(e.CommandArgument);
        if (e.CommandName == "detail")
        {
            RedEnvelopeConnPreOrderOperate Operate = new RedEnvelopeConnPreOrderOperate();
            DataTable dt = Operate.QueryRedEnvelopeConnPreOrder(redEnvelopeId);
            if (dt != null && dt.Rows.Count > 0)
            {
                gdvRedEnvelopeConn.DataSource = dt;
                AspNetPager4.RecordCount = dt.Rows.Count;
            }
            else
            {
                gdvRedEnvelopeConn.DataSource = null;
            }
            gdvRedEnvelopeConn.DataBind();
        }
    }
    protected void gdvReEnvelopeDetail_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < gdvReEnvelopeDetail.Rows.Count; i++)
        {
            int status = Common.ToInt32(gdvReEnvelopeDetail.DataKeys[i].Values["isExecuted"]);
            DateTime dtExpireTime = Common.ToDateTime(gdvReEnvelopeDetail.DataKeys[i].Values["expireTime"]);
            switch (status)
            {
                case (int)VARedEnvelopeStateType.未生效:
                    gdvReEnvelopeDetail.Rows[i].Cells[4].Text = "未生效";
                    break;
                case (int)VARedEnvelopeStateType.已生效:
                    gdvReEnvelopeDetail.Rows[i].Cells[4].Text = "已生效";
                    if (dtExpireTime < DateTime.Now)
                    {
                        gdvReEnvelopeDetail.Rows[i].Cells[4].Text = "已过期";
                    }
                    break;
                case (int)VARedEnvelopeStateType.已使用:
                    gdvReEnvelopeDetail.Rows[i].Cells[4].Text = "已使用";
                    break;
                case (int)VARedEnvelopeStateType.已过期:
                    gdvReEnvelopeDetail.Rows[i].Cells[4].Text = "已过期";
                    break;
                case (int)VARedEnvelopeStateType.红包满:
                    gdvReEnvelopeDetail.Rows[i].Cells[4].Text = "红包满";
                    break;
                case (int)VARedEnvelopeStateType.已删除:
                    gdvReEnvelopeDetail.Rows[i].Cells[4].Text = "已删除";
                    break;
                default:
                    break;
            }
        }
    }
}