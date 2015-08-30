using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.WebPageDll;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBox_UserName.Focus();
            label_message.Visible = false;
            HyperLink_Out_Click();//注销操作
        }
    }
    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton_Login_Click(object sender, ImageClickEventArgs e)
    {
        string userName = TextBox_UserName.Text.Trim();
        string password = TextBox_Password.Text.Trim();

        VAEmployeeLogin vAEmployeeLogin = new VAEmployeeLogin();
        vAEmployeeLogin.userName = userName;
        vAEmployeeLogin.password = MD5Operate.getMd5Hash(password);
        EmployeeOperate employeeOperate = new EmployeeOperate();
        VAEmployeeLoginResponse vAEmployeeLoginResponse = employeeOperate.EmployeeLogin(vAEmployeeLogin);
        if (vAEmployeeLoginResponse.result == VAResult.VA_OK)
        {
            if (employeeOperate.IsSupportLoginBgSYS(userName) == true)
            {
                Session["UserInfo"] = vAEmployeeLoginResponse;
                Response.Redirect("Default.aspx");
            }
            else
            {
                TextBox_Password.Text = "";
                TextBox_UserName.Text = "";
                TextBox_UserName.Focus();
                label_message.Visible = true;
                label_message.Text = "无权限登录，请联系管理员";
            }
        }
        else
        {
            TextBox_Password.Text = "";
            TextBox_UserName.Text = "";
            TextBox_UserName.Focus();
            label_message.Visible = true;
        }
    }
    /// <summary>
    /// 取消
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ImageButton_Cancel_Click(object sender, ImageClickEventArgs e)
    {
        TextBox_Password.Text = "";
        TextBox_UserName.Text = "";
        TextBox_UserName.Focus();
        label_message.Visible = false;
    }

    /// <summary>
    /// 2013-7-29 wangcheng（将函数方法从HeadControl.ascx页面后台移到当前位置）
    /// 主页面点击退出系统后执行的函数
    /// delete表中当前记录的信息，清除session信息
    /// </summary>
    protected void HyperLink_Out_Click()
    {
        if (Session["UserInfo"] != null)//直接可以判断是从初始化页面还是后台default页面进来的
        {
            VAEmployeeLogout vAEmployeeLogout = new VAEmployeeLogout();
            VAEmployeeLoginResponse vAEmployeeLoginResponse = ((VAEmployeeLoginResponse)Session["UserInfo"]);
            vAEmployeeLogout.statusGUID = vAEmployeeLoginResponse.statusGUID;
            vAEmployeeLogout.userName = vAEmployeeLoginResponse.userName;
            //注销操作
            EmployeeOperate employeeOperate = new EmployeeOperate();
            employeeOperate.EmployeeLogout(vAEmployeeLogout);

            Session["UserInfo"] = null;
            Session.Clear();//清除session
            ClearClientPageCache();//清除浏览器缓存
        }
    }
    public void ClearClientPageCache()
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.Cache.SetNoStore();
    }
}