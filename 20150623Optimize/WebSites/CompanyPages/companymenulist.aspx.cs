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
using VAGastronomistMobileApp.SQLServerDAL;
using CloudStorage;

public partial class CompanyPages_companymenulist : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    /// <summary>
    /// 获取菜的列表
    /// </summary>
    /// <param name="PageSize"></param>
    /// <param name="PageIndex"></param>
    /// <param name="dishname"></param>
    /// <param name="dishtypeid"></param>
    /// <param name="shopId"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetDishListJson(int PageSize, int PageIndex, string dishname, int dishtypeid, int shopId)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        string imagePath = WebConfig.CdnDomain + WebConfig.ImagePath;
        int MenuID = DishOperate.GetMenuIdByShopId(shopId);
        MenuOperate menuOperate = new MenuOperate();
        string menupath = menuOperate.QueryMenuImagePath(MenuID);
        imagePath += menupath;
        PaginationPager pg = new PaginationPager();
        pg.PageSize = PageSize;
        pg.PageIndex = PageIndex;
        pg.strWhere = "[DishInfo].DishID = [DishI18n].DishID  and [DishInfo].DishStatus > '0' and DishI18n.DishI18nStatus='1' ";

        if (dishname != "")
        {
            pg.strWhere += " and " + "(DishI18n.DishName like '%" + dishname + "%' or DishI18n.dishQuanPin like '%" + dishname + "%' or DishI18n.dishJianPin like '%" + dishname + "%')";
        }
        pg.strGetFields = "DishI18n.DishName,DishI18n.DishI18nID,[DishInfo].DishID";


        pg.strWhere += " and " + "DishInfo.MenuID ='" + MenuID + "'";
        if (dishtypeid != 0)//非全部
        {
            pg.tblName = "[DishInfo],[DishI18n],[DishConnType],[DishTypeInfo]";
            pg.strWhere += " and DishInfo.DishID = DishConnType.DishID and [DishConnType].DishTypeID =[DishTypeInfo].DishTypeID" + " and " + "DishTypeInfo.DishTypeID ='" + dishtypeid + "' and DishConnType.DishConnTypeStatus ='1'";

        }
        else
        {
            pg.tblName = "[DishInfo],[DishI18n]";

        }
        pg.OrderType = 1;
        pg.OrderfldName = "DishInfo.DishID";
        pg.realOrderfldName = "DishID";

        DataTable dtdish = Common.DbPager(pg);
        dtdish.Columns.Add("imageurl");//图片路径
        dtdish.Columns.Add("dishTypeList");//分类名称
        dtdish.Columns.Add("dishScaleList");//规格名称
        dtdish.Columns.Add("dishPriceList");//价格名称
        for (int i = 0; i < dtdish.Rows.Count; i++)
        {
            string img = Common.GetFieldValue("ImageInfo", "ImageName", "DishID ='" + dtdish.Rows[i]["DishID"] + "' and ImageScale ='1' and ImageStatus='1'");
            if (img != "")
            {
                dtdish.Rows[i]["imageurl"] = imagePath + img;
            }
            else
            {
                dtdish.Rows[i]["imageurl"] = "";
            }
            DataTable dttype = Common.GetDataTableFieldValue("DishConnType,DishTypeI18n", "DishTypeName",
                "DishConnType.DishTypeID =DishTypeI18n.DishTypeID and DishTypeI18n.DishTypeI18nStatus='1' and DishID ='" +
                dtdish.Rows[i]["DishID"] + "' and DishConnType.DishConnTypeStatus='1'");
            string typename = "";
            for (int j = 0; j < dttype.Rows.Count; j++)
            {
                typename += dttype.Rows[j]["DishTypeName"] + ",";
            }
            dtdish.Rows[i]["dishTypeList"] = typename.TrimEnd(',');
            DataTable dtScale = Common.GetDataTableFieldValue("DishPriceInfo,DishPriceI18n", "ScaleName,DishPriceInfo.DishPrice",
                "DishPriceInfo.DishPriceID =DishPriceI18n.DishPriceID and DishID='" + dtdish.Rows[i]["DishID"] + "' and DishPriceInfo.DishPriceStatus ='1' and DishPriceI18n.DishPriceI18nStatus='1'");
            string scalename = "";
            string price = "";
            for (int n = 0; n < dtScale.Rows.Count; n++)
            {
                scalename += dtScale.Rows[n]["ScaleName"] + ",";
                price += Common.ToDouble(dtScale.Rows[n]["DishPrice"]) + ",";
            }
            dtdish.Rows[i]["dishScaleList"] = scalename.TrimEnd(',');
            dtdish.Rows[i]["dishPriceList"] = price.TrimEnd(',');
        }
        int ocount = pg.doCount;
        string json = "";
        if (dtdish.Rows.Count > 0)
        {
            json = Common.ConvertDateTableToJson(dtdish).Trim();
            json = json.TrimEnd('}');
            json += ",\"total\":[{\"totalAmount\":\"" + "" + "\"},{\"ocount\":" + ocount + "},{\"payTotalAmount\":\"" + "" + "\"}]}";

        }
        else
        {
        }
        return json;
    }
    /// <summary>
    /// 获取公司列表
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string GetCompanyJson()
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
        List<VAEmployeeCompany> employeeCompany = new List<VAEmployeeCompany>();
        VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"];
        int employeeID = vAEmployeeLoginResponse.employeeID;
        employeeCompany = employeeConnShopOperate.QueryEmployeeCompany(employeeID);
        string json = Common.ConvertListToJson<List<VAEmployeeCompany>>(employeeCompany);
        return json;
    }
    /// <summary>
    /// 根据公司ID获取店铺列表
    /// </summary>
    /// <param name="companyid">公司ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string GetShopJson(int companyid)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        EmployeeConnShopOperate employeeConnShopOperate = new EmployeeConnShopOperate();
        List<VAEmployeeShop> employeeShop = new List<VAEmployeeShop>();
        VAEmployeeLoginResponse vAEmployeeLoginResponse = (VAEmployeeLoginResponse)HttpContext.Current.Session["MerchantsTreasureUserInfo"];
        int employeeID = vAEmployeeLoginResponse.employeeID;
        int companyID = Common.ToInt32(companyid);
        employeeShop = employeeConnShopOperate.QueryEmployeeShopByCompanyAndEmplyee(employeeID, companyID);
        string json = Common.ConvertListToJson<List<VAEmployeeShop>>(employeeShop);
        return json;
    }
    /// <summary>
    /// 根据店铺ID获取分类列表
    /// </summary>
    /// <param name="shopid">店铺ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string GetDishTypeJson(int shopid)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        int MenuID = DishOperate.GetMenuIdByShopId(shopid);
        //int MenuID = Common.ToInt32(Common.GetFieldValue("MenuConnShop,MenuInfo", "MenuConnShop.menuId", "MenuInfo.MenuID =MenuConnShop.menuId and MenuInfo.MenuStatus='1' and MenuConnShop.shopId='" + shopid + "'"));
        DishTypeOperate dishTypeOperate = new DishTypeOperate();
        DataTable dt = dishTypeOperate.QueryDishTypeInfo(MenuID);
        string json = Common.ConvertDateTableToJson(dt);
        return json;
    }
    /// <summary>
    /// 停售
    /// </summary>
    /// <param name="dishpriceid">菜价格规格ID</param>
    /// <returns></returns>
    [WebMethod]
    public static int SetSelloutJson(int dishpriceid)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return -1000;
        }
        return Common.UpdateStatus("DishPriceInfo", "DishPriceID", Common.ToString(dishpriceid), "DishSoldout", "1");
    }
    /// <summary>
    /// 取消停售
    /// </summary>
    /// <param name="dishpriceid">菜价格规格ID</param>
    /// <returns></returns>
    [WebMethod]
    public static int SetNoSelloutJson(int dishpriceid)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return -1000;
        }
        return Common.UpdateStatus("DishPriceInfo", "DishPriceID", Common.ToString(dishpriceid), "DishSoldout", "0");
    }

    /// <summary>
    /// 删除菜信息
    /// </summary>
    /// <param name="DishI18nID">菜ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string RemoveDishInfo(int DishI18nID)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        using (TransactionScope scope = new TransactionScope())
        {
            DishOperate dishOperate = new DishOperate();
            int dishID = Common.ToInt32(Common.GetFieldValue("DishI18n", "DishID", "DishI18nID ='" + DishI18nID + "'"));
            bool remove = dishOperate.RemoveDishInfo(dishID);
            DishTypeOperate dishTypeOper = new DishTypeOperate();
            bool flag = dishTypeOper.DeleteDishConnTypeStatus(dishID);//删除菜品和分了的关联关系 add by wangc 20140416
            if (remove == true && flag)
            {
                DataTable dt = dishOperate.SelectDishImageInfo(dishID);
                string objeckKey = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objeckKey = WebConfig.ImagePath + dt.Rows[i]["menuImagePath"].ToString() + dt.Rows[i]["ImageName"].ToString();
                        CloudStorageOperate.DeleteObject(objeckKey);
                    }
                }
                scope.Complete();
                return "1";
            }
            else
            {
                return "0";
            }
        }
    }
    /// <summary>
    /// 更新菜谱，同步到客户端
    /// </summary>
    /// <param name="shopid">店铺ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string UpdateMenu(int shopid)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        try
        {
            #region 更新服务器
            int menuID = DishOperate.GetMenuIdByShopId(shopid);
            if (menuID > 0)
            {
                MenuOperate menuOperate = new MenuOperate();
                if (menuOperate.UpdateMenu(menuID) > 0)
                {
                    return "1";
                }
                else
                {
                    return "-1";
                }
            }
            else
            {
                return "-1";
            }
            #endregion
        }
        catch (Exception)
        {
            return "0";
        }
    }
}