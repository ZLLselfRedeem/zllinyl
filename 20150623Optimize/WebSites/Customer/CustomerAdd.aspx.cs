using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
public partial class Customer_CustomerAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetCustomerRank();
            TextBox_CustomerBirthday.Text = DateTime.Now.ToString("yyyy-MM-dd");
            TextBox_RegisterDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
        }
    }

    /// <summary>
    /// 获取用户等级
    /// </summary>
    protected void GetCustomerRank()
    {
        CustomerOperate customerOperate = new CustomerOperate();
        DataTable dt = customerOperate.QueryCustomerRank();
        DropDownList_CustomerRankID.DataSource = dt;
        DropDownList_CustomerRankID.DataTextField = "CustomerRankName";
        DropDownList_CustomerRankID.DataValueField = "CustomerRankID";
        DropDownList_CustomerRankID.DataBind();
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        //CustomerInfo customerInfo = new CustomerInfo();
        //customerInfo.CustomerAddress = TextBox_CustomerAddress.Text;
        //customerInfo.CustomerBirthday = Common.ToDateTime(TextBox_CustomerBirthday.Text);
        //customerInfo.CustomerFirstName = TextBox_CustomerFirstName.Text;
        //customerInfo.CustomerLastName = TextBox_CustomerLastName.Text;
        ////customerInfo.CustomerNote = TextBox_CustomerNote.Text;
        //customerInfo.mobilePhoneNumber = TextBox_CustomerPhone.Text;
        //customerInfo.CustomerRankID = Common.ToInt32(DropDownList_CustomerRankID.SelectedValue);
        //customerInfo.CustomerSex = Common.ToInt32(RadioButtonList_CustomerSex.SelectedValue);
        //customerInfo.CustomerStatus = 1;
        //customerInfo.Password = MD5Operate.getMd5Hash(TextBox_ConfirmPassword.Text.Trim());
        //customerInfo.RegisterDate = Common.ToDateTime(TextBox_RegisterDate.Text);
        //customerInfo.UserName = TextBox_UserName.Text;

        //CustomerOperate customerOperate = new CustomerOperate();
        //bool i = customerOperate.AddCustomer(customerInfo);
        //if (i == true)
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('添加成功！');</script>");
        //}
        //else
        //{
        //    Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('添加失败！');</script>");
        //}


    }
}