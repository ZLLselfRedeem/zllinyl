using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class AuthorizationManagement_ModifyPassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Session["UserInfo"] != null)
        {
            //根据session获取用户名
            VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)Session["UserInfo"];
            string userName = vAEmployeeLoginResponse.userName;
            string password = MD5Operate.getMd5Hash(TextBox_AuthorityPassword.Text.Trim());
            int employeeID = vAEmployeeLoginResponse.employeeID;
            //根据用户名和用户输入的密码，查询用户的基本信息
            EmployeeOperate employeeOperate = new EmployeeOperate();
            VAEmployeeLogin vAEmployeeLogin = new VAEmployeeLogin();
            vAEmployeeLogin.userName = userName;
            vAEmployeeLogin.password = password;

            VAEmployeeLoginResponse vAEmployeeLoginResponseNew = employeeOperate.EmployeeLogin(vAEmployeeLogin);
            if (vAEmployeeLoginResponseNew.result == VAResult.VA_OK)
            {

                bool i = employeeOperate.ModifyEmployeePwd(employeeID, MD5Operate.getMd5Hash(TextBox_AuthorityPasswordConfirm.Text));
                if (i)
                {
                    Common.RecordEmployeeOperateLog((int)VAEmployeeOperateLogOperatePageType.PASSWORD_MODIFY, (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE, "悠先后台修改密码为" + MD5Operate.getMd5Hash(TextBox_AuthorityPasswordConfirm.Text));
                    Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('修改密码成功！');</script>");
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('修改密码失败！');</script>");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "js", "<script>alert('原始密码错误！');</script>");
            }
        }
    }
}