using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;
using System.Threading;
using VAGastronomistMobileApp.WebPageDll.ThreadCallBacks;
using System.Configuration;

public partial class Customer_CustomerRepeatPay : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string customerPhone = TextBox_mobilePhoneNumber.Text.Trim();
        BindRepeatPay(customerPhone);
    }
    private void BindRepeatPay(string customerPhone)
    {
        RepeatPayOperate repeatPayOperate = new RepeatPayOperate();
        DataTable dt = repeatPayOperate.QueryRepeatedPay(customerPhone);
        if (dt != null && dt.Rows.Count > 0)
        {
            this.gdvCustomerList.DataSource = dt;
            this.gdvCustomerList.DataBind();
        }
        else
        {
            this.gdvCustomerList.DataSource = null;
            this.gdvCustomerList.DataBind();
        }
    }
    protected void lnkbtnEdit_OnCommand(object sender, CommandEventArgs e)
    {
        long outTradeNo = Common.ToInt64(e.CommandArgument);
        int row = ((GridViewRow)((LinkButton)sender).NamingContainer).RowIndex;//行号
        string payType = gdvCustomerList.DataKeys[row].Values[0].ToString();//支付方式
        long connId = Common.ToInt64(gdvCustomerList.DataKeys[row].Values[2]);//点单流水号
        string phone = gdvCustomerList.DataKeys[row].Values[3].ToString();//电话号码
        string userName = gdvCustomerList.DataKeys[row].Values[4].ToString();//昵称
        double payAmount = Common.ToDouble(gdvCustomerList.DataKeys[row].Values[5]);//支付金额

        if (e.CommandName == "refund")
        {
            OriginalRoadRefundInfo originalRoadRefund = new OriginalRoadRefundInfo();
            originalRoadRefund.type = VAOriginalRefundType.REPEAT_PREORDER;
            originalRoadRefund.connId = connId;
            originalRoadRefund.customerMobilephone = phone;
            originalRoadRefund.customerUserName = userName;
            originalRoadRefund.refundAmount = payAmount;
            originalRoadRefund.status = (int)VAOriginalRefundStatus.REMITTING;
            originalRoadRefund.employeeId = 0;
            originalRoadRefund.tradeNo = outTradeNo.ToString();

            switch (payType)
            {
                case "支付宝":
                    originalRoadRefund.RefundPayType = RefundPayType.支付宝;
                    break;
                case "微信":
                    originalRoadRefund.RefundPayType = RefundPayType.微信;
                    break;
            }
            PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
            bool isRefunded = preOrder19dianOperate.IsOriginalRoadRefunded(connId);
            if (isRefunded)
            {
                CommonPageOperate.AlterMsg(this, "该订单已经申请过退款，请转至【原路退款】页面查进度"); 
            }
            else
            {
                RepeatPayOperate repeatPayOperate = new RepeatPayOperate();
                repeatPayOperate.RepeatPayRefund(connId, originalRoadRefund);
                if (phone.Length == 11)
                {
                    string shopName = "";
                    DataTable dtShop = preOrder19dianOperate.QueryPreorder(connId);
                    if (dtShop.Rows.Count == 1)
                    {
                        shopName = Common.ToString(dtShop.Rows[0]["shopName"]);
                    }
                    string smsContent = ConfigurationManager.AppSettings["sybRefundMessage"].Trim();
                    smsContent = smsContent.Replace("{0}", shopName);
                    smsContent = smsContent.Replace("{1}", Common.ToString(payAmount));
                    Common.SendMessageBySms(phone, smsContent);
                }
                Thread.Sleep(1500);
                BindRepeatPay(phone);
                CommonPageOperate.AlterMsg(this, "退款申请已经提交，可在【原路退款】页面查进度");
            }
        }
    }
    protected void gdvCustomerList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            int orderStatus = Common.ToInt32(gdvCustomerList.DataKeys[e.Row.RowIndex].Values[1]);
            LinkButton lkb = e.Row.FindControl("lnkbtnModify") as LinkButton;
            Label lbOrderStatus = e.Row.FindControl("lbPayType") as Label;
            switch (orderStatus)
            {
                case (int)VAAlipayOrderStatus.PAID:
                    lkb.Visible = false;
                    lbOrderStatus.Text = Common.GetEnumDescription(VAAlipayOrderStatus.PAID);
                    break;
                case (int)VAAlipayOrderStatus.REPEAT_PAID:
                    lbOrderStatus.Text = Common.GetEnumDescription(VAAlipayOrderStatus.REPEAT_PAID);
                    break;
                case (int)VAAlipayOrderStatus.REPEAT_REFUNDING:
                    lbOrderStatus.Text = Common.GetEnumDescription(VAAlipayOrderStatus.REPEAT_REFUNDING);
                    lkb.Visible = false;
                    break;
                case (int)VAAlipayOrderStatus.REFUNDED:
                    lbOrderStatus.Text = Common.GetEnumDescription(VAAlipayOrderStatus.REFUNDED);
                    lkb.Visible = false;
                    break;
                default:
                    lkb.Visible = false;
                    break;
            }
        }
    }
}