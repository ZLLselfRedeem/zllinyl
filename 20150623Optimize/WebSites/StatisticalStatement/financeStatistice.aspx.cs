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

public partial class StatisticalStatement_financeStatistice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //new CityDropDownList().BindCity(ddlCity);
            //new CompanyDropDownList().BindCompany(ddlCompany, Common.ToInt32(ddlCity.SelectedValue));
            //new ShopDropDownList().BindShop(ddlShop, Common.ToInt32(ddlCompany.SelectedValue));
        }
    }
    private void DataBind(int str, int end)
    {
        string phone = txtCustomerPhone.Text.Trim();
        string userName = txtUserName.Text.Trim();
        if (phone.Length != 11 && String.IsNullOrWhiteSpace(userName))
        {
            CommonPageOperate.AlterMsg(this, "查询条件不能为空"); return;
        }
        DataTable dt = new StatisticalStatementOperate().GetCustomerExpenseRecord(phone, userName);
        if (dt.Rows.Count > 0)
        {
            int tableCount = dt.Rows.Count;
            AspNetPager1.RecordCount = tableCount;
            DataTable dt_page = Common.GetPageDataTable(dt, str, end);
            grDataList.DataSource = dt_page;
        }
        else
        {
            grDataList.DataSource = dt;
        }
        grDataList.DataBind();
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DataBind(0, 10);
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        DataBind(AspNetPager1.StartRecordIndex - 1, AspNetPager1.EndRecordIndex);
    }

    private void LbDataBind()
    {
        string strTime = txtTimeStar.Text + " 00:00:00";
        string endTime = txtTimeEnd.Text + " 23:59:59";
        if (String.IsNullOrWhiteSpace(strTime) || String.IsNullOrWhiteSpace(endTime))
        {
            CommonPageOperate.AlterMsg(this, "查询时间不能为空"); return;
        }
        double initMoney = 251242.83;//基数粮票金额，20140912 17:28，发布程序需要实施去外网数据库查询数据修改当前初始值和初始时间
        //全部余额：685775.1
        //未绑定手机号码余额：434532.27
        //绑定手机号码余额： 251242.83
        string initDate = "2014-9-26 15:44:26";//及时粮票金额锁定时间
        double[] data = new StatisticalStatementOperate().GetGetCustomerMoneyRecord(initMoney, initDate, strTime, endTime);
        lbPresentAmount.Text = data[0].ToString();//本期活动赠送金额
        lbPayAmount.Text = data[1].ToString();//本期账户余额支付金额
        lbGatheringAmount.Text = data[2].ToString();//本期账户充值收款金额
        lbAliAmount.Text = data[3].ToString();//本期账户支付宝收款金额
        lbWechatAmount.Text = data[4].ToString();//本期账户财富通收款金额
        lbRefundAmount.Text = data[5].ToString(); //本期退款金额
        lbStarAmount.Text = data[6].ToString();//期初余额
        lbEndAmount.Text = data[7].ToString();//期末余额
        //string log = String.Format("财务统计用户余额查询起止时间：{0}-{1}，本期活动赠送金额：{2}，本期账户余额支付金额：{3}，本期账户收款金额：{4}，期初余额：{5}，期末余额：{6}",
        //   strTime, endTime, data[0], data[1], data[2], data[3], data[4]);
        //Common.RecordEmployeeOperateLog((int)VAEmployeeOperateLogOperatePageType.FINANCE_STATISITICE, (int)VAEmployeeOperateLogOperateType.QUERY_OPERATE, log);
    }
    protected void btnQuery_Click(object sender, EventArgs e)
    {
        LbDataBind();
    }
}