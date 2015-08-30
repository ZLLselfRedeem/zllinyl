using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;

public partial class RedEnvelope_redEnvelopeStatistalQuery : System.Web.UI.Page
{
    RedEnvelopeStatisticalOperate operate = new RedEnvelopeStatisticalOperate();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Initial();
        }
    }

    private void Initial()
    {
        txbBeginTime.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd") + " 00:00:00";
        txbEndTime.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd") + " 23:59:59";
        txbPayTimeBegin.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd") + " 00:00:00";
        txbPayTimeEnd.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd") + " 23:59:59";
        txbRegisterBegin.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd") + " 00:00:00";
        txbRegisterEnd.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd") + " 23:59:59";
        txbPayTime.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd");
    }

    private void clearParticipate()
    {
        lbParticipateAmout.Text = "";
        lbParticipateCount.Text = ""; 
    }
    private void clearReal()
    {
        lbRealAmount.Text = "";
        lbRealCount.Text = ""; 
    }
    private void clearUse()
    {
        lbUseAmount.Text = "";
        lbUseCount.Text = ""; 
    }
    private void clearOrder()
    {
        lbOrderCount.Text = "";
        lbPrePaidSum.Text = "";
        lbRedenvelopeUsedAmount.Text = "";
        lbRefundMoneySum.Text = ""; 
    }
    private void clearShop()
    {
        lbShopOrderCount.Text = "";
        lbShopPrePaidSum.Text = "";
        lbShopRefundMoneySum.Text = "";
        lbTopShopName.Text = "";
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        string name = txbActivityName.Text.Trim();
        DateTime getTimeBegin = Common.ToDateTime(txbBeginTime.Text);
        DateTime getTimeEnd = Common.ToDateTime(txbEndTime.Text);
        DateTime payTimeBegin = Common.ToDateTime(txbPayTimeBegin.Text);
        DateTime payTimeEnd = Common.ToDateTime(txbPayTimeEnd.Text);
        DateTime registerBegin = Common.ToDateTime(txbRegisterBegin.Text);
        DateTime registerEnd = Common.ToDateTime(txbRegisterEnd.Text);

        DataTable dtParticipate = operate.QueryData(name, getTimeBegin, getTimeEnd);
        if (dtParticipate != null && dtParticipate.Rows.Count > 0)
        {
            lbParticipateCount.Text = dtParticipate.Rows[0]["Count"].ToString();//参与人数
            lbParticipateAmout.Text = Common.ToDouble(dtParticipate.Rows[0]["Amount"]).ToString();//生成红包金额
        }
        else
        {
            clearParticipate();
        }

        DataTable dtReal = new DataTable();
        if (ckbRegisterTime.Checked)
        {
            clearParticipate();
            dtReal = operate.QueryNewCustomer(name, getTimeBegin, getTimeEnd, registerBegin, registerEnd);
        }
        else
        {
            dtReal = operate.QueryData(name, getTimeBegin, getTimeEnd, true);
        }
        if (dtReal != null && dtReal.Rows.Count > 0)
        {
            lbRealCount.Text = dtReal.Rows[0]["Count"].ToString();//.Replace("0", "");//抢到红包人数
            lbRealAmount.Text = Common.ToDouble(dtReal.Rows[0]["Amount"]).ToString();//抢到红包总金额
        }
        else
        {
            clearReal();
        }

        DataTable dtUsed = new DataTable();
        if (ckbRegisterTime.Checked)
        {
            dtUsed = operate.QueryNewCustomer(name, getTimeBegin, getTimeEnd, registerBegin, registerEnd, true);
        }
        else
        {
            dtUsed = operate.QueryData(name, getTimeBegin, getTimeEnd, true, true);
        }
        if (dtUsed != null && dtUsed.Rows.Count > 0)
        {
            lbUseCount.Text = dtUsed.Rows[0]["usedCount"].ToString();//.Replace("0", "");//使用红包人数
            lbUseAmount.Text = Common.ToDouble(dtUsed.Rows[0]["usedAmount"]).ToString();//使用红包总金额
        }
        else
        {
            clearUse();
        }

        //所有用户，所有类型红包
        if (!ckbRegisterTime.Checked && Common.ToInt32(ddlActivityType.SelectedValue) == 0)
        {
            DataTable dtOrder1 = operate.QueryOrder(name, getTimeBegin, getTimeEnd, registerBegin, registerEnd, payTimeBegin, payTimeEnd, Common.ToInt32(ddlCity.SelectedValue), 0, false, true);
            if (dtOrder1 != null && dtOrder1.Rows.Count > 0)
            {
                lbOrderCount.Text = dtOrder1.Rows[0]["orderCount"].ToString();
                lbRedenvelopeUsedAmount.Text =  Common.ToDouble(dtOrder1.Rows[0]["UsedAmount"]).ToString();
                lbPrePaidSum.Text = Common.ToDouble(dtOrder1.Rows[0]["prePaidSum"]).ToString();
                lbRefundMoneySum.Text = Common.ToDouble(dtOrder1.Rows[0]["refundMoneySum"]).ToString();
            }
            else
            {
                clearOrder();
            }

            DataTable dtShop1 = operate.QueryTopShop(name, getTimeBegin, getTimeEnd, registerBegin, registerEnd, payTimeBegin, payTimeEnd, Common.ToInt32(ddlCity.SelectedValue), 0, false, true);
            if (dtShop1 != null && dtShop1.Rows.Count > 0)
            {
                lbTopShopName.Text = dtShop1.Rows[0]["shopName"].ToString();
                lbShopOrderCount.Text = dtShop1.Rows[0]["orderCount"].ToString();
                lbShopPrePaidSum.Text = Common.ToDouble(dtShop1.Rows[0]["prePaidSum"]).ToString();
                lbShopRefundMoneySum.Text = Common.ToDouble(dtShop1.Rows[0]["refundMoneySum"]).ToString();
            }
            else
            {
                clearShop();
            }
        }

        //新用户，所有类型红包
        if (ckbRegisterTime.Checked && Common.ToInt32(ddlActivityType.SelectedValue) == 0)
        {
            DataTable dtOrder2 = operate.QueryOrder(name, getTimeBegin, getTimeEnd, registerBegin, registerEnd, payTimeBegin, payTimeEnd, Common.ToInt32(ddlCity.SelectedValue), 0, true, true);
            if (dtOrder2 != null && dtOrder2.Rows.Count > 0)
            {
                lbOrderCount.Text = dtOrder2.Rows[0]["orderCount"].ToString();
                lbRedenvelopeUsedAmount.Text = Common.ToDouble(dtOrder2.Rows[0]["UsedAmount"]).ToString();
                lbPrePaidSum.Text = Common.ToDouble(dtOrder2.Rows[0]["prePaidSum"]).ToString();
                lbRefundMoneySum.Text = Common.ToDouble(dtOrder2.Rows[0]["refundMoneySum"]).ToString();
            }
            else
            {
                clearOrder();
            }
            DataTable dtShop2 = operate.QueryTopShop(name, getTimeBegin, getTimeEnd, registerBegin, registerEnd, payTimeBegin, payTimeEnd, Common.ToInt32(ddlCity.SelectedValue), 0, true, true);
            if (dtShop2 != null && dtShop2.Rows.Count > 0)
            {
                lbTopShopName.Text = dtShop2.Rows[0]["shopName"].ToString();
                lbShopOrderCount.Text = dtShop2.Rows[0]["orderCount"].ToString();
                lbShopPrePaidSum.Text = Common.ToDouble(dtShop2.Rows[0]["prePaidSum"]).ToString();
                lbShopRefundMoneySum.Text = Common.ToDouble(dtShop2.Rows[0]["refundMoneySum"]).ToString();
            }
            else
            {
                clearShop();
            }
        }

        //所有用户，区分类型
        if (!ckbRegisterTime.Checked && Common.ToInt32(ddlActivityType.SelectedValue) > 0)
        {
            DataTable dtOrder3 = operate.QueryOrder(name, getTimeBegin, getTimeEnd, registerBegin, registerEnd, payTimeBegin, payTimeEnd, Common.ToInt32(ddlCity.SelectedValue), Common.ToInt32(ddlActivityType.SelectedValue), false, false);
            if (dtOrder3 != null && dtOrder3.Rows.Count > 0)
            {
                lbOrderCount.Text = dtOrder3.Rows[0]["orderCount"].ToString();
                lbRedenvelopeUsedAmount.Text = Common.ToDouble(dtOrder3.Rows[0]["UsedAmount"]).ToString();
                lbPrePaidSum.Text = Common.ToDouble(dtOrder3.Rows[0]["prePaidSum"]).ToString();
                lbRefundMoneySum.Text = Common.ToDouble(dtOrder3.Rows[0]["refundMoneySum"]).ToString();
            }
            else
            {
                clearOrder();
            }
            DataTable dtShop3 = operate.QueryTopShop(name, getTimeBegin, getTimeEnd, registerBegin, registerEnd, payTimeBegin, payTimeEnd, Common.ToInt32(ddlCity.SelectedValue), Common.ToInt32(ddlActivityType.SelectedValue), false, false);
            if (dtShop3 != null && dtShop3.Rows.Count > 0)
            {
                lbTopShopName.Text = dtShop3.Rows[0]["shopName"].ToString();
                lbShopOrderCount.Text = dtShop3.Rows[0]["orderCount"].ToString();
                lbShopPrePaidSum.Text = Common.ToDouble(dtShop3.Rows[0]["prePaidSum"]).ToString();
                lbShopRefundMoneySum.Text = Common.ToDouble(dtShop3.Rows[0]["refundMoneySum"]).ToString();
            }
            else
            {
                clearShop();
            }
        }

        //新用户，区分类型
        if (ckbRegisterTime.Checked && Common.ToInt32(ddlActivityType.SelectedValue) > 0)
        {
            DataTable dtOrder4 = operate.QueryOrder(name, getTimeBegin, getTimeEnd, registerBegin, registerEnd, payTimeBegin, payTimeEnd, Common.ToInt32(ddlCity.SelectedValue), Common.ToInt32(ddlActivityType.SelectedValue), true, false);
            if (dtOrder4 != null && dtOrder4.Rows.Count > 0)
            {
                lbOrderCount.Text = dtOrder4.Rows[0]["orderCount"].ToString();
                lbRedenvelopeUsedAmount.Text = Common.ToDouble(dtOrder4.Rows[0]["UsedAmount"]).ToString();
                lbPrePaidSum.Text = Common.ToDouble(dtOrder4.Rows[0]["prePaidSum"]).ToString();
                lbRefundMoneySum.Text = Common.ToDouble(dtOrder4.Rows[0]["refundMoneySum"]).ToString();
            }
            else
            {
                clearOrder();
            }
            DataTable dtShop4 = operate.QueryTopShop(name, getTimeBegin, getTimeEnd, registerBegin, registerEnd, payTimeBegin, payTimeEnd, Common.ToInt32(ddlCity.SelectedValue), Common.ToInt32(ddlActivityType.SelectedValue), true, false);
            if (dtShop4 != null && dtShop4.Rows.Count > 0)
            {
                lbTopShopName.Text = dtShop4.Rows[0]["shopName"].ToString();
                lbShopOrderCount.Text = dtShop4.Rows[0]["orderCount"].ToString();
                lbShopPrePaidSum.Text = Common.ToDouble(dtShop4.Rows[0]["prePaidSum"]).ToString();
                lbShopRefundMoneySum.Text = Common.ToDouble(dtShop4.Rows[0]["refundMoneySum"]).ToString();
            }
            else
            {
                clearShop();
            }
        }

        int dayNum = 30, payNum = 2;
        if (!string.IsNullOrEmpty(txbDayNum.Text))
        {
            dayNum = -Common.ToInt32(txbDayNum.Text);
        }
        if (!string.IsNullOrEmpty(txbPayNum.Text))
        {
            payNum = Common.ToInt32(txbPayNum.Text);
        }
        DateTime payBegin = Common.ToDateTime(txbPayTime.Text + " 00:00:00");
        DateTime payEnd = Common.ToDateTime(txbPayTime.Text + " 23:59:59");
        int count = operate.QueryPrePaidCount(dayNum, payNum, payBegin, payEnd);
        txbCount.Text = count.ToString();
    }
}