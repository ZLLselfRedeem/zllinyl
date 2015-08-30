using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.DBUtility;
using System.Text;
using System.Configuration;
using System.Web.Services;
using VAGastronomistMobileApp.Model;
using System.Transactions;
using Newtonsoft.Json;


/// <summary>
/// 功能描述:代码review 待处理 需要整理到公用块便于调用
/// </summary>
public partial class CompanyPages_masterFunction : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    /// <summary>
    /// 根据公司ID获取店铺列表
    /// </summary>
    /// <param name="companyid">公司ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string GetShopJson(int companyId)
    {
        try
        {
            if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
            {
                return "-1000";
            }
            VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"];
            int employeeID = vAEmployeeLoginResponse.employeeID;
            EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
            List<VAEmployeeShop> employeeShop = new List<VAEmployeeShop>();
            EmployeeOperate employeeOperate = new EmployeeOperate();
            EmployeeInfo employee = employeeOperate.QueryEmployee(employeeID);
            if (employee.isViewAllocWorker == true)
            {
                employeeShop = employeeConnShopOperate.QueryEmployeeShopByCompanyAndEmplyee(Common.ToInt32(employeeID), Common.ToInt32(companyId));//友络管理员
                if (employeeShop == null)
                {
                    return "{" + "\"shop\":0}";
                }
            }
            else
            {
                employeeShop = employeeConnShopOperate.QueryEmployeeShopByCompanyAndEmplyee(Common.ToInt32(employeeID), Common.ToInt32(companyId), true);//商户服务员
            }
            List<VAEmployeeShopJson> employeeShopJson = new List<VAEmployeeShopJson>();
            for (int i = 0; i < employeeShop.Count; i++)
            {
                VAEmployeeShopJson employeeShopinfo = new VAEmployeeShopJson();
                employeeShopinfo.shopID = employeeShop[i].shopID;
                employeeShopinfo.shopName = employeeShop[i].shopName;
                employeeShopJson.Add(employeeShopinfo);
            }
            string jsonshop = Common.ConvertListToJson<List<VAEmployeeShopJson>>(employeeShopJson);
            return "{" + "\"shop\":" + jsonshop + "}";
        }
        catch (Exception)
        {
            return "-1000";
        }
    }
    /// <summary>
    /// 获取公司列表
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string GetCompanyListJson()
    {
        try
        {
            if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
            {
                return "-1000";
            }
            VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"];
            EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
            List<VAEmployeeCompany> employeeCompany = new List<VAEmployeeCompany>();
            int employeeID = vAEmployeeLoginResponse.employeeID;

            EmployeeOperate employeeOperate = new EmployeeOperate();
            EmployeeInfo employee = employeeOperate.QueryEmployee(employeeID);
            if (employee.isViewAllocWorker == true)
            {
                employeeCompany = employeeConnShopOperate.QueryEmployeeCompanyRemoveNotShop(employeeID);//友络管理员
                if (employeeCompany == null)
                {
                    return "{" + "\"company\":0}";
                }
            }
            else
            {
                employeeCompany = employeeConnShopOperate.QueryEmployeeCompanyRemoveNotShop(employeeID, true);//商户服务员
            }
            List<VAEmployeeCompanyJson> VAEmployeeCompanyList = new List<VAEmployeeCompanyJson>();
            for (int n = 0; n < employeeCompany.Count; n++)
            {
                VAEmployeeCompanyJson CompanyJson = new VAEmployeeCompanyJson();
                CompanyJson.ID = employeeCompany[n].companyID;
                CompanyJson.CN = employeeCompany[n].companyName;
                VAEmployeeCompanyList.Add(CompanyJson);
            }
            string company = Common.ConvertListToJson<List<VAEmployeeCompanyJson>>(VAEmployeeCompanyList);
            return "{" + "\"company\":" + company + "}";
        }
        catch (Exception)
        {
            return "-1000";
        }
    }
    /// <summary>
    /// 根据门店ID获取该门店LOGO图片
    /// </summary>
    /// <param name="companyId"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetShopLogo(string shopId)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        ShopOperate operate = new ShopOperate();
        ShopInfo shopInfo = operate.QueryShop(Common.ToInt32(shopId));
        if (shopInfo != null)
        {
            if (!String.IsNullOrEmpty(shopInfo.shopImagePath) && !String.IsNullOrEmpty(shopInfo.shopLogo))
            {
                return WebConfig.CdnDomain + WebConfig.ImagePath + shopInfo.shopImagePath + shopInfo.shopLogo;
            }
            else
            {
                return WebConfig.ServerDomain + "Images/uxian.png";//取系统默认图片
            }
        }
        else
        {
            return WebConfig.ServerDomain + "Images/uxian.png";//取系统默认图片
        }
    }
    /// <summary>
    /// 获取当前session记录的shopid
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string GetShopId()
    {
        return Common.ToString(HttpContext.Current.Session["loginshop"]);
    }
    /// <summary>
    /// 改变session shopid
    /// </summary>
    /// <param name="shopId"></param>
    [WebMethod]
    public static void ChangeShopId(string shopId)
    {
        HttpContext.Current.Session["loginshop"] = shopId;
    }
    /// <summary>
    /// 获取公司Id
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string GetCompanyId()
    {
        return Common.ToString(HttpContext.Current.Session["logincompany"]);
    }
    /// <summary>
    /// 改变session companyId
    /// </summary>
    /// <param name="shopId"></param>
    [WebMethod]
    public static void ChangeCompanyId(string companyId)
    {
        HttpContext.Current.Session["logincompany"] = companyId;
    }
}