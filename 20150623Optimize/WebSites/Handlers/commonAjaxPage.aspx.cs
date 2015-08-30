﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using VAGastronomistMobileApp.Model;

public partial class Handlers_commonAjaxPage : System.Web.UI.Page
{
    /// <summary>
    /// created by wangc
    /// 20140415
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    #region 门店审核搜索全部门店
    /// <summary>
    /// 获取显示全部门店，仅仅过滤删除门店
    /// 使用范围：门店审核页面；
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetData(string str)
    {
       ShopOperate shopOper = new ShopOperate();
        DataTable dtShop = shopOper.QueryShop();
        DataRow[] dr = dtShop.Select("shopName like '%" + str + "%'");
        List<Shop> list = new List<Shop>();
        if (dr.Length > 0)
        {
            foreach (DataRow item in dr)
            {
                Shop model = new Shop();
                model.shopId = Common.ToInt32(item["shopID"]);
                model.shopName = Common.ToString(item["shopName"]);
                list.Add(model);
            }
        }
        return JsonOperate.JsonSerializer<List<Shop>>(list);
    }
    #endregion

    #region 批量打款搜索全部门店
    /// <summary>
    /// 获取显示全部门店，仅仅过滤删除门店
    /// 使用范围：批量打款申请页面；
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetDataAndCompany(string str)
    {
        ShopOperate shopOper = new ShopOperate();
        DataTable dtShop = shopOper.QueryShopAndCompany();
        DataRow[] dr = dtShop.Select("shopName like '%" + str + "%'");
        List<ShopAndCompany> list = new List<ShopAndCompany>();
        if (dr.Length > 0)
        {
            foreach (DataRow item in dr)
            {
                ShopAndCompany model = new ShopAndCompany();
                model.shopId = Common.ToInt32(item["shopID"]);
                model.shopName = Common.ToString(item["shopName"]);
                model.companyName = Common.ToString(item["companyName"]);
                model.remainMoney = Common.ToDouble(item["remainMoney"]);
                list.Add(model);
            }
        }
        return JsonOperate.JsonSerializer<List<ShopAndCompany>>(list);
    }
    #endregion

    #region 门店列表输入公司搜索门店列表
    [WebMethod]
    public static string GetCompany(string str)
    {
        EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
        List<VAEmployeeCompany> employeeCompany = new List<VAEmployeeCompany>();
        VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)HttpContext.Current.Session["UserInfo"];
        int employeeID = vAEmployeeLoginResponse.employeeID;
        employeeCompany = employeeConnShopOperate.QueryEmployeeCompany(employeeID);
        var query = (from c in employeeCompany
                     where c.companyName.Contains(str.Trim())
                     select c).ToList();
        return JsonOperate.JsonSerializer<List<VAEmployeeCompany>>(query);
    }
    #endregion

    #region 根据员工搜索门店列表
    [WebMethod]
    public static string GetShop(string str)
    {
        EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
        List<Shop> employeeShop = new List<Shop>();
        VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)HttpContext.Current.Session["UserInfo"];
        int employeeID = vAEmployeeLoginResponse.employeeID;
        DataTable dtShop = employeeConnShopOperate.QueryEmployeeShopByEmplyee(employeeID);
        if (dtShop.Rows.Count > 0)
        {
            foreach (DataRow item in dtShop.Rows)
            {
                Shop model = new Shop();
                model.shopId = Common.ToInt32(item["shopID"]);
                model.shopName = Common.ToString(item["shopName"]);
                employeeShop.Add(model);
            }
        }
        employeeShop = (from s in employeeShop
                        where s.shopName.Contains(str.Trim())
                        select s).ToList();
        return JsonOperate.JsonSerializer<List<Shop>>(employeeShop);
    }
    #endregion

    #region 门店添加搜索全部公司
    [WebMethod]
    public static string QueryAllCompany(string str)
    {
        CompanyOperate companyOperate = new CompanyOperate();
        DataTable dtCompany = companyOperate.QueryCompany();
        List<VAEmployeeCompany> companyList = new List<VAEmployeeCompany>();
        if (dtCompany.Rows.Count > 0)
        {
            DataView dvCompany = dtCompany.DefaultView;
            dvCompany.RowFilter = "companyName like '%" + str + "%'";
            dtCompany = dvCompany.ToTable();
            foreach (DataRow item in dtCompany.Rows)
            {
                VAEmployeeCompany company = new VAEmployeeCompany();
                company.companyID = Common.ToInt32(item["companyID"]);
                company.companyName = Common.ToString(item["companyName"]);
                companyList.Add(company);
            }
        }
        return JsonOperate.JsonSerializer<List<VAEmployeeCompany>>(companyList);
    }
    #endregion

    #region 美食广场配置
    /// <summary>
    /// 绑定门店列表数据
    /// </summary>
    [WebMethod]
    public static string GetSearchShop(string shopName, int cityId)
    {
        ShopOperate operate = new ShopOperate();
        List<VAEmployeeShop> list = operate.QueryHandShopInfoByCompanyId(shopName, cityId);
        return JsonOperate.JsonSerializer(list);
    }
    #endregion

    /// <summary>
    /// 首页改版广告添加页面查询店铺
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetAdvertShopData(string str,string cityID)
    {
        ShopOperate shopOper = new ShopOperate();
        DataTable dtShop = shopOper.QueryShop();
        DataRow[] dr = dtShop.Select("shopName like '%" + str + "%' and isHandle=1 and  cityID="+cityID);
        List<Shop> list = new List<Shop>();
        if (dr.Length > 0)
        {
            foreach (DataRow item in dr)
            {
                Shop model = new Shop();
                model.shopId = Common.ToInt32(item["shopID"]);
                model.shopName = Common.ToString(item["shopName"]);
                list.Add(model);
            }
        }
        return JsonOperate.JsonSerializer<List<Shop>>(list);
    }

    /// <summary>
    /// 获取菜品折扣和价格
    /// </summary>
    /// <param name="dishPriceID"></param>
    /// <param name="shopID"></param>
    /// <returns></returns>
    [WebMethod]
    public static string SearchDishPriceAndDiscount(int dishPriceID, int shopID)
    {
        DishChannelPrice response = ShopChannelDishOperate.SearchPriceAndDiscount(dishPriceID, shopID);
        return JsonOperate.JsonSerializer<DishChannelPrice>(response);
    }
}