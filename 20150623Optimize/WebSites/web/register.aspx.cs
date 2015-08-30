using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Data;
using System.Transactions;

public partial class ViewAllocWebSite_register : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Response.Cookies.Add(new HttpCookie("CheckCode", ""));
            label_message.Visible = false;
        }
    }

    /// <summary>
    /// 商户注册
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button_register_Click(object sender, EventArgs e)
    {
        if (Request.Cookies["CheckCode"] == null)
        {
            label_message.Text = "您的浏览器设置已禁用Cookies，您必须设置浏览器允许使用 Cookies 选项后才能使用本系统";
            label_message.Visible = true;
            return;
        }
        EmployeeOperate employeeOperate = new EmployeeOperate();
        DataTable dtEmployee = employeeOperate.QueryEmployeeByMobilephone(TextBox_UserName.Text.Trim(), true);
        if (dtEmployee.Rows.Count == 1)
        {
            DateTime verificationCodeTime = Common.ToDateTime(dtEmployee.Rows[0]["verificationCodeTime"]);
            string verificationCode = Common.ToString(dtEmployee.Rows[0]["verificationCode"]);
            if (!string.IsNullOrEmpty(TextBox_VerificationCode.Text.Trim()))
            {//验证码为空则为发送验证码请求
                if (((System.DateTime.Now - verificationCodeTime) < TimeSpan.FromMinutes(10)) && !string.IsNullOrEmpty(verificationCode))
                {
                    label_message.Text = "验证码已过期";
                    label_message.Visible = true;
                    return;
                }
                else
                {
                    if (String.Compare(Request.Cookies["CheckCode"].Value, TextBox_VerificationCode.Text.ToString().Trim(), true) != 0)
                    {
                        label_message.Text = "验证码输入错误";
                        label_message.Visible = true;
                        return;
                    }
                }
            }
            else
            {
                label_message.Text = "验证码无效";
                label_message.Visible = true;
                return;
            }
        }
        else
        {
            label_message.Text = "请先填写手机号码获取验证码";
            label_message.Visible = true;
            return;
        }
        if (TextBox_UserName.Text.Trim() == "" || TextBox_UserName.Text.Trim().Length != 11)
        {
            label_message.Text = "用户名只能是11位手机号码";
            label_message.Visible = true;
            return;
        }
        if (TextBox_CompanyName.Text.Trim() == "")
        {
            label_message.Text = "品牌名称不能为空";
            label_message.Visible = true;
            return;
        }
        if (TextBox_OwndCompany.Text.Trim() == "")
        {
            label_message.Text = "所属公司不能为空";
            label_message.Visible = true;
            return;
        }
        if (TextBox_CompanyTelePhone.Text.Trim() == "")
        {
            label_message.Text = "公司电话不能为空";
            label_message.Visible = true;
            return;
        }
        if (TextBox_ContactPerson.Text.Trim() == "")
        {
            label_message.Text = "公司联系人不能为空";
            label_message.Visible = true;
            return;
        }
        if (TextBox_ContactPhone.Text.Trim() == "")
        {
            label_message.Text = "公司联系人电话不能为空";
            label_message.Visible = true;
            return;
        }
        if (TextBox_CompanyAddress.Text.Trim() == "")
        {
            label_message.Text = "公司地址不能为空";
            label_message.Visible = true;
            return;
        }
        else
        {
            CompanyInfo companyInfo = new CompanyInfo();
            ShopInfo shopInfo = new ShopInfo();
            VAMenu vAMenu = new VAMenu();
            RegisterOperate registerOperate = new RegisterOperate();
            if (registerOperate.RegisterInfo(dtEmployee, TextBox_CompanyName.Text.Trim(), TextBox_UserName.Text.Trim(), TextBox_CompanyAddress.Text.Trim(),
                TextBox_CompanyTelePhone.Text.Trim(), TextBox_ContactPerson.Text.Trim(),
                TextBox_ContactPhone.Text.Trim(), TextBox_OwndCompany.Text.Trim(), ref companyInfo, ref shopInfo, ref vAMenu, 1))
            {
                FunctionResult functionResult = new FunctionResult();
                string password = string.Empty;
                functionResult = employeeOperate.Register(dtEmployee, companyInfo, shopInfo, vAMenu, ref password);//不会添加员工信息，发送验证码操作时已添加员工信息
                if (functionResult.returnResult > 0)
                {
                    //注册成功
                    bool result = Common.SendMessageBySms(TextBox_UserName.Text.Trim(), "悠先收银帐号" + TextBox_UserName.Text.Trim() + "，密码" + password + "");//短信推送帐号密码
                    VAEmployeeLogin vAEmployeeLogin = new VAEmployeeLogin();
                    vAEmployeeLogin.userName = TextBox_UserName.Text.Trim();
                    vAEmployeeLogin.password = password;
                    VAEmployeeLoginResponse vAEmployeeLoginResponse = employeeOperate.EmployeeLogin(vAEmployeeLogin);
                    if (vAEmployeeLoginResponse.result == VAResult.VA_OK)
                    {
                        Session["UserInfo"] = vAEmployeeLoginResponse;
                        Page.ClientScript.RegisterStartupScript(GetType(), null, "<script>RegiserComplete()</script>");
                    }
                    else
                    {
                        TextBox_UserName.Text = "";
                        TextBox_UserName.Focus();
                        label_message.Visible = true;
                        Page.ClientScript.RegisterStartupScript(GetType(), null, "<script>alert('恭喜你注册成功！您的用户名是您填写的手机号码，初始密码是" + password + "');window.location.href='../CompanyPages/Login.aspx'</script>");
                    }
                }
                else
                {
                    label_message.Text = functionResult.message;
                    label_message.Visible = true;
                }
            }

            label_message.Visible = true;
        }
    }

    /// <summary>
    /// 查询用户名是否存在
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    [WebMethod]
    public static bool QueryEmployeeByName(string TextBox_UserName)
    {
        EmployeeOperate employeeOperate = new EmployeeOperate();
        return employeeOperate.IsEmployeeUserNameExit(TextBox_UserName);
    }

    /// <summary>
    /// 查询公司名是否存在
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    [WebMethod]
    public static bool CompanyNameValid(string TextBox_CompanyName)
    {
        CompanyOperate companyOperate = new CompanyOperate();
        return companyOperate.CompanyNameValid(TextBox_CompanyName);
    }
    /// <summary>
    /// 获取验证码（推送）
    /// </summary>
    /// <param name="phoneNum"></param>
    [WebMethod]
    public static string GetVerificationCode(string phoneNum)
    {
        using (TransactionScope scope = new TransactionScope())
        {
            ZZBPreOrderOperate operate = new ZZBPreOrderOperate();
            EmployeeInfo employee = new EmployeeInfo();
            employee.UserName = phoneNum;
            employee.cookie = Common.ToString(Guid.NewGuid()) + Common.ToString(System.DateTime.Now.Ticks);
            employee.EmployeeStatus = -1;
            employee.EmployeePhone = phoneNum;
            employee.EmployeeAge = 28;
            employee.EmployeeFirstName = phoneNum;
            employee.EmployeeSequence = 10;
            employee.EmployeeSex = 1;
            Random randomNumber = new Random();
            employee.Password = MD5Operate.getMd5Hash(Common.ToString(Common.randomStrAndNum(6)));
            employee.position = "";
            employee.defaultPage = "";
            employee.isViewAllocWorker = false; //是否是友络工作人员
            employee.registerTime = DateTime.Now;
            EmployeeOperate employeeOper = new EmployeeOperate();
            if (employeeOper.AddEmployee(employee) > 0)
            {
                VAResult result = operate.SendVerificationCodeBySms(phoneNum, "");
                if (result == VAResult.VA_OK)
                {
                    //验证码发送成功
                    scope.Complete();
                    return "ok";
                }
                else
                {
                    //验证码发送失败
                    return "error";
                }
            }
            else
            {
                //用户添加失败
                return "error";
            }
        }
    }
}