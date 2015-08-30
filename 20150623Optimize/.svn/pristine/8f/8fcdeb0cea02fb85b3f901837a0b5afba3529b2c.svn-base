using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
public partial class AuthorizationManagement_EmployeeAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        EmployeeInfo employeeInfo = new EmployeeInfo();
        employeeInfo.EmployeeAge = Common.ToInt32(TextBox_EmployeeAge.Text);
        employeeInfo.EmployeeFirstName = TextBox_EmployeeFirstName.Text;
        //employeeInfo.EmployeeLastName = TextBox_EmployeeLastName.Text;//2014-2-23 取消LastName
        employeeInfo.EmployeePhone = TextBox_EmployeePhone.Text;
        employeeInfo.EmployeeSequence = Common.ToInt32(TextBox_EmployeeSequence.Text);
        employeeInfo.EmployeeSex = Common.ToInt32(RadioButtonList_EmployeeSex.SelectedValue);
        employeeInfo.EmployeeStatus = 1;
        string password = Common.randomStrAndNum(6);//六位数随即密码 wangcheng
        employeeInfo.Password = MD5Operate.getMd5Hash(password);
        employeeInfo.UserName = TextBox_UserName.Text;
        employeeInfo.position = TextBox_position.Text;
        employeeInfo.defaultPage = TextBox_DefaultPage.Text;
        employeeInfo.cookie = Common.ToString(Guid.NewGuid()) + Common.ToString(System.DateTime.Now.Ticks);
        employeeInfo.isViewAllocWorker = Common.ToBool(RadioButtonList_isViewAllocWorker.SelectedValue); //是否是友络工作人员
        employeeInfo.registerTime = DateTime.Now;//服务员注册时间wangcheng
        //
        employeeInfo.isSupportLoginBgSYS = Common.ToBool(rbl_isSupportLoginBgSYS.SelectedValue);//add by wangc 20140324
        EmployeeOperate employeeOperate = new EmployeeOperate();
        int i = employeeOperate.AddEmployee(employeeInfo);
        if (i > 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('添加成功！你的初始密码是" + password + "');</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('添加失败！');</script>");
        }
    }
}