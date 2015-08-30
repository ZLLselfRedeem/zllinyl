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

public partial class PreOrder19dianManage_FoodPreOrder19dianQuery : System.Web.UI.Page
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
        int cityID = Common.ToInt32(ddlCity.SelectedValue);
        RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
        Preorder19DianLineOperate pdo = new Preorder19DianLineOperate();
        DataTable dt = pdo.QueryFoodPreOrder19dian(beginTime, endTime, cityID);
        lbAmount.Text = dt.Rows[0]["Amount"].ToString();
        lbRefundAmount.Text = dt.Rows[0]["RefundAmount"].ToString();

        if (lbAmount.Text.Equals(string.Empty))
        {
            lbAmount.Text = "0.00";
        }
        if (lbRefundAmount.Text.Equals(string.Empty))
        {
            lbRefundAmount.Text = "0.00";
        }
    }
}