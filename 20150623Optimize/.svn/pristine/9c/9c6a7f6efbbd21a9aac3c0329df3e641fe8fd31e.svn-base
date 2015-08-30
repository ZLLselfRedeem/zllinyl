using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using Web.Control.DDL;

public partial class RedEnvelope_RedEnvelopeFinanceQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            new CityDropDownList().BindCity(ddlCity);
            txbBeginTime.Text = DateTime.Now.AddDays(-7).ToString("yyyy/MM/dd 00:00:00");
            txbEndTime.Text = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd 23:59:59");
        }
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        DateTime beginTime = Common.ToDateTime(txbBeginTime.Text.Trim());
        DateTime endTime = Common.ToDateTime(txbEndTime.Text.Trim());
        if (endTime.AddMonths(-1) > beginTime)
        {
            CommonPageOperate.AlterMsg(this, "查询周期过长，请重新选择");
            return;
        }
        int activityType = Common.ToInt32(ddlActivityNameQuery.SelectedValue);
        int cityID = Common.ToInt32(ddlCity.SelectedValue);
        RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
        //lbRedEnvelopePayAmount.Text = redEnvelopeOperate.QueryFinanceRedEnvelopePay(beginTime, endTime, activityType).ToString();
        //lbRedEnvelopeRefundAmount.Text = redEnvelopeOperate.QueryFinanceRedEnvelopeRefund(beginTime, endTime).ToString();
        DataTable dt = redEnvelopeOperate.QueryRedEnvelopeFinance(beginTime, endTime, activityType, cityID);
        lbRedEnvelopePayAmount.Text = dt.Rows[0]["currectUsedAmounts"].ToString();
        lbRedEnvelopeRefundAmount.Text = dt.Rows[0]["refundRedEnvelopes"].ToString();
        lbPreOrder19dianIds.Text = dt.Rows[0]["preOrder19dianIds"].ToString();
        lbPrePaidSums.Text = dt.Rows[0]["prePaidSums"].ToString();

        if (lbRedEnvelopePayAmount.Text.Equals(string.Empty))
        {
            lbRedEnvelopePayAmount.Text = "0.00";
        }
        if (lbRedEnvelopeRefundAmount.Text.Equals(string.Empty))
        {
            lbRedEnvelopeRefundAmount.Text = "0.00";
        }
        if (lbPreOrder19dianIds.Text.Equals(string.Empty))
        {
            lbPreOrder19dianIds.Text = "0";
        }
        if (lbPrePaidSums.Text.Equals(string.Empty))
        {
            lbPrePaidSums.Text = "0.00";
        }
    }
}