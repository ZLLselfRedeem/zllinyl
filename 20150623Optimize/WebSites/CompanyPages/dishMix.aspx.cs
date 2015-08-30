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

public partial class CompanyPages_dishMix : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    /// <summary>
    /// 获取列表数据分页方法
    /// </summary>
    /// <param name="PageSize"></param>页面大小
    /// <param name="PageIndex"></param>当前第几页
    /// <param name="strWhere"></param>分页条件
    /// <param name="dishname"></param>表名
    /// <param name="dishtypeid"></param>菜分类ID
    /// <returns></returns>
    /// 
    #region old
    //[WebMethod]
    //public static string GetDishListJson(int PageSize, int PageIndex, string dishname, int dishtypeid, int shopId)
    //{

    //    PaginationPager pg = new PaginationPager();
    //    pg.PageSize = PageSize;
    //    pg.PageIndex = PageIndex;
    //    pg.strWhere = "DishPriceInfo.DishPriceStatus='1'";
    //    if (dishname != "")
    //    {

    //        pg.strWhere += " and " + "(DishI18n.dishQuanPin like '%" + dishname + "%' or DishI18n.dishJianPin like '%" + dishname + "%')";

    //    }
    //    if (dishtypeid != 0)//非全部
    //    {
    //        //int menuid =Common.ToInt32(Common.GetFieldValue("DishTypeInfo","MenuID","DishTypeID='"+dishtypeid+"'"));

    //        pg.tblName = "DishPriceI18n inner join DishPriceInfo on DishPriceI18n.DishPriceI18nID =DishPriceInfo.DishPriceID inner join DishI18n on DishI18n.DishID=DishPriceInfo.DishID  left join DishConnType on DishConnType.DishID =DishPriceInfo.DishID left join DishTypeInfo on DishTypeInfo.DishTypeID =DishConnType.DishTypeID";
    //        if (shopId != 0)
    //        {
    //            int MenuID1 = Common.ToInt32(Common.GetFieldValue("MenuConnShop,MenuInfo", "MenuConnShop.menuId", "MenuInfo.MenuID =MenuConnShop.menuId and MenuInfo.MenuStatus='1' and MenuConnShop.shopId='" + shopId + "'"));
    //            pg.strWhere += " and " + "DishTypeInfo.MenuID ='" + MenuID1 + "'";
    //        }
    //        pg.strWhere += " and " + "DishTypeInfo.DishTypeID ='" + dishtypeid + "'";
    //    }
    //    else//全部
    //    {
    //        pg.tblName = "DishPriceI18n inner join DishPriceInfo on DishPriceI18n.DishPriceI18nID =DishPriceInfo.DishPriceID inner join DishI18n on DishI18n.DishID=DishPriceInfo.DishID inner join DishInfo on DishI18n.DishID =DishInfo.DishID";
    //        if (shopId != 0)
    //        {
    //            int MenuID2 = Common.ToInt32(Common.GetFieldValue("MenuConnShop,MenuInfo", "MenuConnShop.menuId", "MenuInfo.MenuID =MenuConnShop.menuId and MenuInfo.MenuStatus='1' and MenuConnShop.shopId='" + shopId + "'"));
    //            pg.strWhere += " and " + "DishInfo.MenuID ='" + MenuID2 + "'";
    //        }
    //    }

    //    pg.strGetFields = "DishPriceI18n.DishPriceI18nID DishPriceI18nID,DishPriceInfo.DishPrice ,DishPriceInfo.DishSoldout,DishPriceI18n.ScaleName,DishI18n.DishName,DishI18n.DishI18nID";

    //    pg.OrderType = 1;
    //    pg.OrderfldName = "DishPriceI18n.DishPriceI18nID";
    //    pg.realOrderfldName = "DishPriceI18nID";

    //    DataTable dtdish = Common.DbPager(pg);
    //    dtdish.Columns.Add("status", typeof(string));

    //    for (int i = 0; i < dtdish.Rows.Count; i++)
    //    {
    //        string status = Common.GetFieldValue("CurrentSellOffInfo", "status", "DishPriceI18nID ='" + dtdish.Rows[i]["DishPriceI18nID"] + "'");
    //        if (status != null && status != "")
    //        {
    //            status = "1";
    //        }
    //        else
    //        {
    //            status = "0";
    //        }

    //        dtdish.Rows[i]["status"] = status;
    //    }

    //    int ocount = pg.doCount;
    //    string json = "";
    //    if (dtdish.Rows.Count > 0)
    //    {
    //        json = Common.ConvertDateTableToJson(dtdish).Trim();
    //        json = json.TrimEnd('}');
    //        json += ",\"total\":[{\"totalAmount\":\"" + "" + "\"},{\"ocount\":" + ocount + "},{\"payTotalAmount\":\"" + "" + "\"}]}";

    //    }
    //    else
    //    {
    //    }
    //    return json;
    //}
    #endregion
    //返回
    //{ "TableJson":[ { "DishName":"上海生煎包","DishI18nID":"123","DishID":"123","imageurl":"http://192.168.1.12/UploadFiles/Images/waipojiacanyinjigou20121128023507838/waipojiawanxiangchengdian20121128023714164/waipojiawanxiangchengdiancaipu20121128025604737/1_1_20131013161745282.jpg","dishTypeList":"甜品冰沙刨冰,点心,饮料,果汁,酒水","dishScaleList":"22,55,11","dishPriceList":"6,8,11"}, { "DishName":"茶叶蛋","DishI18nID":"381","DishID":"381","imageurl":"http://192.168.1.12/UploadFiles/Images/waipojiacanyinjigou20121128023507838/waipojiawanxiangchengdian20121128023714164/waipojiawanxiangchengdiancaipu20121128025604737/1_17_20121128151641156.jpg","dishTypeList":"冷菜","dishScaleList":"只","dishPriceList":"1"}, { "DishName":"香油豆腐","DishI18nID":"382","DishID":"382","imageurl":"http://192.168.1.12/UploadFiles/Images/waipojiacanyinjigou20121128023507838/waipojiawanxiangchengdian20121128023714164/waipojiawanxiangchengdiancaipu20121128025604737/","dishTypeList":"","dishScaleList":"","dishPriceList":""}, { "DishName":"我爱凤爪","DishI18nID":"383","DishID":"383","imageurl":"http://192.168.1.12/UploadFiles/Images/waipojiacanyinjigou20121128023507838/waipojiawanxiangchengdian20121128023714164/waipojiawanxiangchengdiancaipu20121128025604737/1_17_20130813103021752.jpg","dishTypeList":"冷菜","dishScaleList":"只","dishPriceList":"3"}, { "DishName":"五香花生","DishI18nID":"385","DishID":"385","imageurl":"http://192.168.1.12/UploadFiles/Images/waipojiacanyinjigou20121128023507838/waipojiawanxiangchengdian20121128023714164/waipojiawanxiangchengdiancaipu20121128025604737/1_17_20121128151641906.jpg","dishTypeList":"冷菜","dishScaleList":"份","dishPriceList":"6"}, { "DishName":"口味凉皮","DishI18nID":"386","DishID":"386","imageurl":"http://192.168.1.12/UploadFiles/Images/waipojiacanyinjigou20121128023507838/waipojiawanxiangchengdian20121128023714164/waipojiawanxiangchengdiancaipu20121128025604737/1_17_20130814101844588.jpg","dishTypeList":"冷菜","dishScaleList":"份","dishPriceList":"63"}, { "DishName":"爽口海带结","DishI18nID":"387","DishID":"387","imageurl":"http://192.168.1.12/UploadFiles/Images/waipojiacanyinjigou20121128023507838/waipojiawanxiangchengdian20121128023714164/waipojiawanxiangchengdiancaipu20121128025604737/1_17_20121128151642031.jpg","dishTypeList":"冷菜","dishScaleList":"份","dishPriceList":"88"}, { "DishName":"手剥皮蛋","DishI18nID":"388","DishID":"388","imageurl":"http://192.168.1.12/UploadFiles/Images/waipojiacanyinjigou20121128023507838/waipojiawanxiangchengdian20121128023714164/waipojiawanxiangchengdiancaipu20121128025604737/1_17_20121128151642093.jpg","dishTypeList":"冷菜","dishScaleList":"份","dishPriceList":"8"}, { "DishName":"蜂窝豆干","DishI18nID":"389","DishID":"389","imageurl":"http://192.168.1.12/UploadFiles/Images/waipojiacanyinjigou20121128023507838/waipojiawanxiangchengdian20121128023714164/waipojiawanxiangchengdiancaipu20121128025604737/1_17_20121128151642171.jpg","dishTypeList":"冷菜","dishScaleList":"份","dishPriceList":"8"}, { "DishName":"长寿菜","DishI18nID":"390","DishID":"390","imageurl":"http://192.168.1.12/UploadFiles/Images/waipojiacanyinjigou20121128023507838/waipojiawanxiangchengdian20121128023714164/waipojiawanxiangchengdiancaipu20121128025604737/1_17_20121128151642234.jpg","dishTypeList":"冷菜","dishScaleList":"份","dishPriceList":"9"} ],"total":[{"totalAmount":""},{"ocount":255},{"payTotalAmount":""}]}
    [WebMethod]
    public static string GetDishListJson(int PageSize, int PageIndex, string dishname, int dishtypeid, int shopId)
    {
        string imagePath = WebConfig.CdnDomain + WebConfig.ImagePath;
        int MenuID = Common.ToInt32(Common.GetFieldValue("MenuConnShop,MenuInfo", "MenuConnShop.menuId", "MenuInfo.MenuID =MenuConnShop.menuId and MenuInfo.MenuStatus='1' and MenuConnShop.shopId='" + shopId + "'"));
        MenuOperate menuOperate = new MenuOperate();
        string menupath = menuOperate.QueryMenuImagePath(MenuID);
        imagePath += menupath;
        PaginationPager pg = new PaginationPager();
        pg.PageSize = PageSize;
        pg.PageIndex = PageIndex;
        pg.strWhere = "[DishInfo].DishID = [DishI18n].DishID  and [DishInfo].DishStatus > '0' and DishI18n.DishI18nStatus='1' ";

        if (dishname != "")
        {
            pg.strWhere += " and " + "(DishI18n.dishQuanPin like '%" + dishname + "%' or DishI18n.dishJianPin like '%" + dishname + "%')";
        }
        pg.strGetFields = "DishI18n.DishName,DishI18n.DishI18nID,[DishInfo].DishID";


        pg.strWhere += " and " + "DishInfo.MenuID ='" + MenuID + "'";
        if (dishtypeid != 0)//非全部
        {
            pg.tblName = "[DishInfo],[DishI18n],[DishConnType],[DishTypeInfo]";
            pg.strWhere += " and DishInfo.DishID = DishConnType.DishID and [DishConnType].DishTypeID =[DishTypeInfo].DishTypeID" + " and " + "DishTypeInfo.DishTypeID ='" + dishtypeid + "'";
        }
        else
        {
            pg.tblName = "[DishInfo],[DishI18n]";

        }
        pg.OrderType = 0;
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
                dtdish.Rows[i]["DishID"] + "'");
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
                price += dtScale.Rows[n]["DishPrice"] + ",";
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
    /// <param name="companyid"></param>公司ID
    /// <returns></returns>
    [WebMethod]
    public static string GetShopJson(int companyid)
    {
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
    /// <param name="shopid"></param>店铺ID
    /// <returns></returns>
    [WebMethod]
    public static string GetDishTypeJson(int shopid)
    {
        int MenuID = Common.ToInt32(Common.GetFieldValue("MenuConnShop,MenuInfo", "MenuConnShop.menuId", "MenuInfo.MenuID =MenuConnShop.menuId and MenuInfo.MenuStatus='1' and MenuConnShop.shopId='" + shopid + "'"));
        DishTypeOperate dishTypeOperate = new DishTypeOperate();
        DataTable dt = dishTypeOperate.QueryDishTypeInfo(MenuID);
        string json = Common.ConvertDateTableToJson(dt);
        return json;
    }
    /// <summary>
    /// 停售
    /// </summary>
    /// <param name="dishpriceid"></param>菜价格规格ID
    /// <returns></returns>
    [WebMethod]
    public static int SetSelloutJson(int dishpriceid)
    {
        return Common.UpdateStatus("DishPriceInfo", "DishPriceID", Common.ToString(dishpriceid), "DishSoldout", "1");
    }
    /// <summary>
    /// 取消停售
    /// </summary>
    /// <param name="dishpriceid"></param>菜价格规格ID
    /// <returns></returns>
    [WebMethod]
    public static int SetNoSelloutJson(int dishpriceid)
    {
        return Common.UpdateStatus("DishPriceInfo", "DishPriceID", Common.ToString(dishpriceid), "DishSoldout", "0");
    }
    /// <summary>
    /// 删除菜信息
    /// </summary>
    /// <param name="DishI18nID"></param>菜ID
    /// <returns></returns>
    [WebMethod]
    public static string RemoveDishInfo(int DishI18nID)
    {
        DishOperate dishOperate = new DishOperate();
        int dishID = Common.ToInt32(Common.GetFieldValue("DishI18n", "DishID", "DishI18nID ='" + DishI18nID + "'"));
        bool i = dishOperate.RemoveDishInfo(dishID);
        if (i == true)
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }
    /// <summary>
    /// 跟新菜谱
    /// </summary>
    /// <param name="shopid"></param>//店铺ID
    /// <returns></returns>
    [WebMethod]
    public static string UpdateMenu(int shopid)
    {
        try
        {
            #region 更新服务器
            int menuID = Common.ToInt32(Common.GetFieldValue("MenuConnShop,MenuInfo", "MenuConnShop.menuId", "MenuInfo.MenuID =MenuConnShop.menuId and MenuInfo.MenuStatus='1' and MenuConnShop.shopId='" + shopid + "'"));
            MenuOperate menuOperate = new MenuOperate();
            if (menuOperate.UpdateMenu(menuID) > 0)
            {
                return "1";
            }
            else
            {
                return "0";
            }
            #endregion
        }
        catch (Exception)
        {
            return "0";
        }
    }
}