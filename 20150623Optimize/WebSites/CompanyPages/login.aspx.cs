using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Web.Services;
using System.Configuration;
using System.Data;

public partial class CompanyPages_login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LogoutOperate();//注销
        }
    }
    /// <summary>
    /// 商户登录
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    [WebMethod]
    public static string Login(string userName, string password)
    {
        string result = "";
        VAEmployeeLogin vAEmployeeLogin = new VAEmployeeLogin();
        vAEmployeeLogin.userName = userName;
        vAEmployeeLogin.password = MD5Operate.getMd5Hash(password);
        EmployeeOperate employeeOperate = new EmployeeOperate();
        VAEmployeeLoginResponse vAEmployeeLoginResponse = employeeOperate.EmployeeLogin(vAEmployeeLogin);
        if (vAEmployeeLoginResponse.result == VAResult.VA_OK && vAEmployeeLoginResponse.employeeID > 0)
        {
            EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
            List<VAEmployeeCompany> employeeCompany = new List<VAEmployeeCompany>();
            int employeeID = vAEmployeeLoginResponse.employeeID;
            EmployeeInfo employee = employeeOperate.QueryEmployee(employeeID);
            if (employee.isViewAllocWorker == true)
            {
                employeeCompany = employeeConnShopOperate.QueryEmployeeCompanyRemoveNotShop(employeeID);//当前是友络员工，绕过是否可以进入收银宝后台权限控制
            }
            else
            {
                employeeCompany = employeeConnShopOperate.QueryEmployeeCompanyRemoveNotShop(employeeID, true);//当前不是友络员工，根据是否可以进入收银宝后台权限控制过滤查询
            }
            if (employeeCompany == null)
            {
                if (employee.isViewAllocWorker == true)//友络员工，没有门店，即可进入员工板块，无法进入商户板块
                {
                    HttpContext.Current.Session["MerchantsTreasureUserInfo"] = vAEmployeeLoginResponse;//记录session
                    if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] != null)
                    {
                        //string user = "[{" + "\"userName\":\"" + vAEmployeeLoginResponse.userName + "\"},{\"isSupportShopManagePage\":false}]";
                        string user = "[{" + "\"userName\":\"" + vAEmployeeLoginResponse.userName + "\"}]";
                        result = "{\"user\":" + user + "}";//isSupportShopManagePage 显示
                        HttpContext.Current.Session["logincompany"] = 0;
                        HttpContext.Current.Session["loginshop"] = 0;
                    }
                    else
                    {
                        result = "-1";//未能成功记录session，登录失败
                    }
                    return result;
                }
            }
            if (employeeCompany.Count > 0)
            {
                List<VAEmployeeShop> employeeShop = new List<VAEmployeeShop>();
                if (employee.isViewAllocWorker == true)
                {
                    employeeShop = employeeConnShopOperate.QueryEmployeeShopByCompanyAndEmplyee(employeeID, employeeCompany[0].companyID);//店铺列表
                }
                else
                {
                    employeeShop = employeeConnShopOperate.QueryEmployeeShopByCompanyAndEmplyee(employeeID, employeeCompany[0].companyID, true);//店铺列表
                }
                HttpContext.Current.Session["MerchantsTreasureUserInfo"] = vAEmployeeLoginResponse;//记录session
                if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] != null)
                {
                    try
                    {
                        if (employeeShop.Count > 0)
                        {
                            HttpContext.Current.Session["loginshop"] = employeeShop[0].shopID;
                        }
                        //SybEmployeeShopAuthority operate = new SybEmployeeShopAuthority();
                        //string isSupportShopManagePage = employee.isViewAllocWorker == true ? "true"
                        //: (operate.QuerySybAuthority(employeeID, employeeShop[0].shopID, "店员管理") == true ? "true"
                        //: "false");//查询是否具备店员管理页面权限
                        //string user = "[{" + "\"userName\":\"" + vAEmployeeLoginResponse.userName + "\"},{\"isSupportShopManagePage\":" + isSupportShopManagePage + "}]";
                        string user = "[{" + "\"userName\":\"" + vAEmployeeLoginResponse.userName + "\"}]";
                        result = "{\"user\":" + user + "}";//isSupportShopManagePage 显示
                    }
                    catch
                    {
                        result = "-1";//登录失败
                    }
                }
                else
                {
                    result = "-1";//未能成功记录session，登录失败
                }
            }
            else
            {
                return "-5";//当前员工没有可管理的门店
            }
        }
        else
        {
            //error
            switch (vAEmployeeLoginResponse.result)
            {
                case VAResult.VA_FAILED_DB_ERROR:
                    result = "-1";
                    break;
                case VAResult.VA_LOGIN_PASSWORD_INCORRECT:
                    result = "-2";
                    break;
                case VAResult.VA_LOGIN_USER_NOT_EXIST:
                    result = "-3";
                    break;
            }
        }
        return result;
    }
    /// <summary>
    /// 注销操作
    /// </summary>
    protected void LogoutOperate()
    {
        if (Session["MerchantsTreasureUserInfo"] != null)//直接可以判断是从初始化页面还是后台操作页面进来的
        {
            VAEmployeeLogout vAEmployeeLogout = new VAEmployeeLogout();
            VAEmployeeLoginResponse vAEmployeeLoginResponse = ((VAEmployeeLoginResponse)Session["MerchantsTreasureUserInfo"]);
            vAEmployeeLogout.statusGUID = vAEmployeeLoginResponse.statusGUID;
            vAEmployeeLogout.userName = vAEmployeeLoginResponse.userName;
            //注销操作
            EmployeeOperate employeeOperate = new EmployeeOperate();
            employeeOperate.EmployeeLogout(vAEmployeeLogout);

            Session["MerchantsTreasureUserInfo"] = null;
            Session.Clear();//清除session
            ClearClientPageCache();//清除浏览器缓存
        }
    }
    /// <summary>
    /// 清除缓存
    /// </summary>
    protected void ClearClientPageCache()
    {
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddDays(-1);
        Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
        Response.Cache.SetNoStore();
    }
}
