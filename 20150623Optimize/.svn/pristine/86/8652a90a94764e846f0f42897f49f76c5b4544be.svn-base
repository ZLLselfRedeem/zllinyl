using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class AuthorizationManagement_EmployeeUpdate : System.Web.UI.Page
{
    protected static int count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetEmployeeInfo();
            Label_Password.Visible = false;
        }
        count++;
        HiddenField_HistoryCount.Value = count.ToString();
    }

    /// <summary>
    /// 获取员工信息
    /// </summary>
    protected void GetEmployeeInfo()
    {
        EmployeeOperate employeeOperate = new EmployeeOperate();
        int employeeID = Common.ToInt32(Request.QueryString["EmployeeID"].ToString());
        EmployeeInfo employeeInfo = employeeOperate.QueryEmployee(employeeID);
        TextBox_EmployeeAge.Text = employeeInfo.EmployeeAge.HasValue ? employeeInfo.EmployeeAge.ToString() : "";
        TextBox_EmployeeFirstName.Text = employeeInfo.EmployeeFirstName;
        //2014-2-23 取消LastName
        //TextBox_EmployeeLastName.Text = employeeInfo.EmployeeLastName;
        TextBox_EmployeePhone.Text = employeeInfo.EmployeePhone;
        TextBox_EmployeeSequence.Text = employeeInfo.EmployeeSequence.ToString();
        TextBox_UserName.Text = employeeInfo.UserName;
        //TextBox_RemoveChangeMaxValue.Text = employeeInfo.removeChangeMaxValue.ToString();
        TextBox_position.Text = employeeInfo.position;
        //男女
        RadioButtonList_EmployeeSex.Items.FindByValue(employeeInfo.EmployeeSex.ToString().ToLower().Trim()).Selected = true;

        //2014-2-23 取消 员工抹零金额最大值，快速结账权限，清台权限，称重权限
        //RadioButtonList_CanClearTable.Items.FindByValue(employeeInfo.canClearTable.ToString().ToLower().Trim()).Selected = true;
        //RadioButtonList_CanQuickCheckout.Items.FindByValue(employeeInfo.canQuickCheckout.ToString().ToLower().Trim()).Selected = true;
        //RadioButtonList_CanWeigh.Items.FindByValue(employeeInfo.canWeigh.ToString().ToLower().Trim()).Selected = true;

        TextBox_DefaultPage.Text = employeeInfo.defaultPage;
        TextBox_Birthday.Text = Common.ToString(employeeInfo.birthday);
        Label_RegisterDate.Text = Common.ToString(employeeInfo.registerTime);
        //2014-2-18 jinyanni
        //是否是友络工作人员
        RadioButtonList_isViewAllocWorker.Items.FindByValue(employeeInfo.isViewAllocWorker.ToString().ToLower().Trim()).Selected = true;
        //add by wangc 20140324
        rbl_isSupportLoginBgSYS.Items.FindByValue(employeeInfo.isSupportLoginBgSYS.ToString().ToLower().Trim()).Selected = true;
    }

    /// <summary>
    /// 点击修改按钮
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Update_Click(object sender, EventArgs e)
    {
        int employeeID = Common.ToInt32(Request.QueryString["EmployeeID"].ToString());
        EmployeeInfo employeeInfo = new EmployeeInfo();
        employeeInfo.EmployeeAge = Common.ToInt32(TextBox_EmployeeAge.Text);
        employeeInfo.EmployeeFirstName = TextBox_EmployeeFirstName.Text;
        //employeeInfo.EmployeeLastName = TextBox_EmployeeLastName.Text;
        employeeInfo.EmployeePhone = TextBox_EmployeePhone.Text;
        employeeInfo.EmployeeSequence = Common.ToInt32(TextBox_EmployeeSequence.Text);
        employeeInfo.EmployeeSex = Common.ToInt32(RadioButtonList_EmployeeSex.SelectedValue);
        employeeInfo.EmployeeStatus = 1;
        employeeInfo.UserName = TextBox_UserName.Text;
        employeeInfo.EmployeeID = employeeID;
        //employeeInfo.removeChangeMaxValue = Common.ToDouble(TextBox_RemoveChangeMaxValue.Text);
        //employeeInfo.canClearTable = Common.ToBool(RadioButtonList_CanClearTable.SelectedValue);
        //employeeInfo.canQuickCheckout = Common.ToBool(RadioButtonList_CanQuickCheckout.SelectedValue);
        //employeeInfo.canWeigh = Common.ToBool(RadioButtonList_CanWeigh.SelectedValue);
        employeeInfo.position = TextBox_position.Text;
        employeeInfo.defaultPage = TextBox_DefaultPage.Text;
        employeeInfo.isViewAllocWorker = Common.ToBool(RadioButtonList_isViewAllocWorker.SelectedValue); //是否是友络工作人员
        //add by wangc 20140324
        employeeInfo.isSupportLoginBgSYS = Common.ToBool(rbl_isSupportLoginBgSYS.SelectedValue);
        if (Common.ToDateTime(TextBox_Birthday.Text) == DateTime.MinValue)//匹配最小时间，不要用字符串匹配，每个pc时间格式不一样
        {
            employeeInfo.birthday = Common.ToDateTime("2014/01/01 00:00:00");
        }
        else
        {
            employeeInfo.birthday = Common.ToDateTime(TextBox_Birthday.Text);
        }
        employeeInfo.remark = Common.ToString(TextBox_Remark.Text.Trim());
        EmployeeOperate employeeOperate = new EmployeeOperate();
        bool i = employeeOperate.ModifyEmployee(employeeInfo);
        if (i == true)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('修改成功！');</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('修改失败！');</script>");
        }
    }
    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_Password_Click(object sender, EventArgs e)
    {
        int employeeID = Common.ToInt32(Request.QueryString["EmployeeID"].ToString());
        string newPwd = Common.randomStrAndNum(6);
        EmployeeOperate employeeOperate = new EmployeeOperate();
        bool i = employeeOperate.ModifyEmployeePwd(employeeID, MD5Operate.getMd5Hash(newPwd));
        if (i == true)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('重置密码成功！');</script>");
            Label_Password.Visible = true;
            Label_Password.Text = "密码为" + newPwd;
            Common.RecordEmployeeOperateLog((int)VAEmployeeOperateLogOperatePageType.PASSWORD_MODIFY, (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE, "悠先后台重置" + employeeID + "密码为" + MD5Operate.getMd5Hash(newPwd));
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('重置密码失败！');</script>");
        }
    }
    protected void Button_Back_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeManage.aspx");
    }
}