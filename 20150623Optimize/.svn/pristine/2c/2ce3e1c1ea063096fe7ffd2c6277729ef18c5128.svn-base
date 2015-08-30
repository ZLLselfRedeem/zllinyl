using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
public partial class Customer_CustomerUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCustomerInfo();
        }
    }
    /// <summary>
    /// 获取用户信息
    /// </summary>
    protected void GetCustomerInfo()
    {
        CustomerOperate CustomerOperate = new CustomerOperate();
        long customerID = Common.ToInt64(Request.QueryString["customerId"]);
        CustomerInfo customerInfo = CustomerOperate.QueryCustomer(customerID);
        if (customerInfo != null)
        {
            TextBox_CustomerAddress.Text = customerInfo.CustomerAddress;
            if (customerInfo.CustomerBirthday != DateTime.MinValue)
            {
                TextBox_CustomerBirthday.Text = customerInfo.CustomerBirthday.Value.ToString("yyyy-MM-dd");
            }
            Label_mobilePhoneNumber.Text = customerInfo.mobilePhoneNumber;
            TextBox_customerEmail.Text = customerInfo.customerEmail;
            try
            {
                RadioButtonList_CustomerSex.Items.FindByValue(customerInfo.CustomerSex.ToString().ToLower().Trim()).Selected = true;
            }
            catch { }
            Label_RegisterDate.Text = customerInfo.RegisterDate.ToString();
            TextBox_UserName.Text = customerInfo.UserName;
        }
    }
    protected void Button_Modify_Click(object sender, EventArgs e)
    {
        CustomerInfo customerInfo = new CustomerInfo();
        long customerID = Common.ToInt64(Request.QueryString["customerId"]);
        customerInfo.CustomerID = customerID;
        customerInfo.CustomerAddress = TextBox_CustomerAddress.Text;
        customerInfo.CustomerBirthday = Common.ToDateTime(TextBox_CustomerBirthday.Text);
        customerInfo.CustomerSex = Common.ToInt32(RadioButtonList_CustomerSex.SelectedValue);
        customerInfo.UserName = TextBox_UserName.Text;
        customerInfo.customerEmail = TextBox_customerEmail.Text;
        CustomerOperate customerOperate = new CustomerOperate();
        bool i = customerOperate.ModifyCustomerBaseInfo(customerInfo);
        if (i == true)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改成功！');</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "message", "<script language='javascript' defer>alert('修改失败！');</script>");
        }
    }
}