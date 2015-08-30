using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;

public partial class ViewAllocWebSite_reportLoss : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBox_MobliePhone.Focus();
            label_message.Visible = false;
        }
    }

    protected void Button_ReportLoss_Click(object sender, EventArgs e)
    {
        string mobilePhone = TextBox_MobliePhone.Text.Trim();
        string password = TextBox_Password.Text.Trim();
        CustomerOperate customerOpe = new CustomerOperate();
        VAClientCustomerReportLossRequest clientCustomerReportLossRequest = new VAClientCustomerReportLossRequest();
        clientCustomerReportLossRequest.mobilePhone = mobilePhone;
        clientCustomerReportLossRequest.passwordMD5 = MD5Operate.getMd5Hash(password);
        VAClientCustomerReportLossResponse clientCustomerReportLossResponse = customerOpe.ClientCustomerReportLoss(clientCustomerReportLossRequest);
        if (clientCustomerReportLossResponse.result == VAResult.VA_OK)
        {
            label_message.Text = "操作成功！";
        }
        else
        {
            label_message.Text = "操作失败！";
        }
        label_message.Visible = true;
    }
}