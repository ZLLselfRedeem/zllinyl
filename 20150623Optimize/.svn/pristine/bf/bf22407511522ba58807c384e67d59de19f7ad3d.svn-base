using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Web.Services;
using System.Transactions;
using System.Text;
using Web.Control;

public partial class CompanyPages_currentSellOff : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    /// <summary>
    /// 显示当前门店当前菜谱沽清所有的菜 //已沽清
    /// </summary>
    /// <param name="PageSize">页面尺寸</param>
    /// <param name="PageIndex">页码</param>
    /// <param name="shopId">门店ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string ShopAllCurrentSellOffInfo(int PageSize, int PageIndex, int shopId)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        int menuId = GetMenuIdByShopId(shopId);
        PaginationPager pg = new PaginationPager();
        pg.strGetFields = " CurrentSellOffInfo.menuId,CurrentSellOffInfo.DishI18nID,CurrentSellOffInfo.status,CurrentSellOffInfo.companyId,CurrentSellOffInfo.shopId,DishI18n.DishName,DishPriceI18n.ScaleName,DishPriceInfo.DishPrice,DishPriceI18n.DishPriceI18nID";
        pg.PageSize = PageSize;
        pg.PageIndex = PageIndex;
        pg.tblName = " CurrentSellOffInfo inner join DishI18n on CurrentSellOffInfo.DishI18nID=DishI18n.DishI18nID inner join DishPriceInfo on DishI18n.DishID=DishPriceInfo.DishID inner join DishPriceI18n on DishPriceI18n.DishPriceID=DishPriceInfo.DishPriceID inner join DishInfo on DishInfo.DishID=DishI18n.DishI18nID";
        //过滤掉已被删除和已被售罄的菜品
        pg.strWhere = "  CurrentSellOffInfo.status=1 and CurrentSellOffInfo.expirationTime>GETDATE() and CurrentSellOffInfo.DishPriceI18nID=DishPriceI18n.DishPriceI18nID and CurrentSellOffInfo.menuId='" + menuId + "' and CurrentSellOffInfo.shopId='" + shopId + "'  and DishPriceInfo.DishPriceStatus='1' and DishPriceInfo.DishSoldout=0";
        pg.OrderType = 0;
        pg.OrderfldName = "CurrentSellOffInfo.DishI18nID";//格式
        pg.realOrderfldName = "DishI18nID";//排序公共字段名称
        string json = Common.ConvertDateTableToJson(Common.DbPager(pg));
        int ocount = pg.doCount;
        if (!String.IsNullOrEmpty(json))
        {
            json = json.TrimEnd('}');
            json += ",\"total\":[{\"totalAmount\":\"\"},{\"ocount\":" + ocount + "}]}";
        }
        else
        {
            json = "";
        }
        return json;
    }

    /// <summary>
    /// 显示当前门店当前菜谱所有菜信息（点击查询按钮获得查询信息）
    /// Add at 2014-4-9
    /// </summary>
    /// <param name="PageSize">页尺寸</param>
    /// <param name="PageIndex">页码</param>
    /// <param name="shopId">门店ID</param>
    /// <param name="dishFilter">dishFilter为文本框查询条件，初始化传“”</param>
    /// <param name="dishTypeId">菜品分类ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string ShopCommonCurrentSellOffInfo(int PageSize, int PageIndex, int shopId, string dishFilter, int dishTypeId)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        int menuId = GetMenuIdByShopId(shopId);
        if (menuId == 0)
        {
            return "";
        }
        dishFilter = String.IsNullOrEmpty(dishFilter) ? "" : HttpUtility.UrlDecode(dishFilter);
        string json = "";
        int ocount = 0;
        //初始化调用
        //点击菜品查询调用
        PaginationPager pg = new PaginationPager();

        pg.strGetFields = " B.DishName,B.DishI18nID ,C.ScaleName,D.DishPrice,A.MenuID as menuId,C.DishPriceI18nID ,(case when C.DishPriceI18nID in (select CurrentSellOffInfo.DishPriceI18nID from CurrentSellOffInfo where isnull(CurrentSellOffInfo.status,1)=1 and (CurrentSellOffInfo.expirationTime is null or CurrentSellOffInfo.expirationTime>GETDATE()) and CurrentSellOffInfo.menuId='" + menuId + "' and CurrentSellOffInfo.shopId='" + shopId + "') then '1' else '0' end) as isOrNotSellOff";
        pg.PageSize = PageSize;
        pg.PageIndex = PageIndex;
        pg.tblName = " DishInfo A inner join DishI18n B on A.DishID=B.DishID inner join DishPriceInfo D on A.DishID=D.DishID inner join DishPriceI18n C on D.DishPriceID=C.DishPriceID left join CurrentSellOffInfo on CurrentSellOffInfo.menuId=A.MenuID";
        pg.strWhere = "1=1 and ";
        // pg.strWhere = " isnull(CurrentSellOffInfo.status,1)=1 and (CurrentSellOffInfo.expirationTime is null or CurrentSellOffInfo.expirationTime>GETDATE()) and ";
        if (dishFilter.Trim() == "")
        {
            pg.strWhere += " A.MenuID='" + menuId + "' and A.DishStatus=1 and C.DishPriceI18nStatus=1 ";
        }
        else
        {
            //查询条件不为空
            pg.strWhere += " (B.DishName like '%" + dishFilter.Trim() + "%' or B.dishJianPin like '%" + dishFilter.Trim() + "%'  or B.dishQuanPin like '%" + dishFilter.Trim() + "%') and A.MenuID='" + menuId + "' and A.DishStatus=1 and C.DishPriceI18nStatus=1 ";
        }
        if (dishTypeId > 0)//有选择分类
        {
            pg.tblName += " inner join DishConnType E on E.DishID=B.DishID";
            pg.strWhere += " and E.DishTypeID='" + dishTypeId + "' group by B.DishName,B.DishI18nID ,C.ScaleName,D.DishPrice,A.MenuID,C.DishPriceI18nID";//根据菜谱过滤查询常用沽清信息
        }
        else
        {
            pg.strWhere += " group by B.DishName,B.DishI18nID ,C.ScaleName,D.DishPrice,A.MenuID,C.DishPriceI18nID";
        }
        pg.OrderType = 0;
        pg.OrderfldName = "B.DishI18nID";
        pg.realOrderfldName = "DishI18nID";
        DataTable dtPager = Common.DbPager(pg);
        DataView dvPager = dtPager.DefaultView;
        dvPager.Sort = "isOrNotSellOff desc";
        DataTable dt = dvPager.ToTable();
        json = Common.ConvertDateTableToJson(dt);
        ocount = pg.doCount;

        if (!String.IsNullOrEmpty(json))
        {
            json = json.TrimEnd('}');
            json += ",\"total\":[{\"totalAmount\":\"\"},{\"ocount\":" + ocount + "}]}";
        }
        else
        {
            json = "";
        }
        return json;
    }
    /// <summary>
    /// 沽清当前选中的菜
    /// 成功，返回沽清当前门店当前菜谱的所有沽清菜信息
    /// 失败，返回“”
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static int CurrentSellOff(int companyId, int shopId, int DishI18nID, int DishPriceI18nID)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return -1000;
        }
        int menuId = GetMenuIdByShopId(shopId);
        CurrentSellOffInfo currentSellOffInfo = new CurrentSellOffInfo()
        {
            companyId = companyId,
            shopId = shopId,
            menuId = menuId,
            DishI18nID = DishI18nID,
            status = 1,
            DishPriceI18nID = DishPriceI18nID,
            expirationTime = Common.ToDateTime(DateTime.Now.ToString("yyyy/MM/dd") + " 23:59:59"),
            operateEmployeeId = SybSessionHelper.GetCurrectSessionEmployeeId()
        };

        DishOperate dishOperate = new DishOperate();
        DataTable dtQueryCurrentSellOffInfo = dishOperate.QueryCurrentSellOffInfo(menuId, shopId);
        DataView dv = dtQueryCurrentSellOffInfo.DefaultView;
        dv.RowFilter = "DishPriceI18nID=" + DishPriceI18nID;
        if (dv.Count == 0)
        {
            //事物开启
            using (TransactionScope scope = new TransactionScope())
            {
                //操作沽清，加入沽清表
                long result = dishOperate.InsertCurrentSellOffInfo(currentSellOffInfo);
                if (result > 0)//添加成功
                {
                    //常用沽清表信息
                    DataTable dtCommonCurrentSellOffInfo = dishOperate.QueryCommonCurrentSellOffInfo(menuId, DishPriceI18nID);
                    if (dtCommonCurrentSellOffInfo.Rows.Count <= 0)
                    {
                        CommonCurrentSellOffInfo commonCurrentSellOffInfo = new CommonCurrentSellOffInfo()
                        {
                            currentSellOffCount = 1,//新添加默认沽清次数为1
                            DishI18nID = DishI18nID,
                            menuId = menuId,
                            DishPriceI18nID = DishPriceI18nID,
                            status = 1
                        };
                        long insertCommonCurrentSellOffInfoResult = dishOperate.InsertCommonCurrentSellOffInfo(commonCurrentSellOffInfo);
                        if (insertCommonCurrentSellOffInfoResult > 0)
                        {
                            //成功
                            scope.Complete();
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        //表示常用沽清表有这条记录，则修改沽清次数
                        if (dtCommonCurrentSellOffInfo.Rows.Count == 1)
                        {
                            bool returnCurrentSellOffCount = dishOperate.UpdateCommonCurrentSellOffCount(DishI18nID, DishPriceI18nID);
                            if (returnCurrentSellOffCount == true)
                            {
                                Common.RecordEmployeeOperateLogBySYB((int)VAEmployeeOperateLogOperatePageType.DISH_CURRENTSELLOFF, (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE, "收银宝沽清菜品，DishPriceI18nID:" + DishPriceI18nID);
                                //修改成功
                                scope.Complete();
                                return 1;
                            }
                            else
                            {
                                return 0;
                            }
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    ///前端页面需要调用当前门店所有沽清的菜的信息，刷新页面
                }
                else
                {
                    return 0;//沽清失败
                }
            }
        }
        else
        {
            return -1;//表示当前菜已被沽清
        }
    }
    /// <summary>
    /// 取消沽清
    /// 批量取消沽清
    /// 全部取消沽清
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static int CancleCurrentSellOff(int shopId, string DishPriceI18nID)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return -1000;
        }
        DishOperate dishOperate = new DishOperate();
        bool deleteResult = false;
        int menuId = GetMenuIdByShopId(shopId);
        using (TransactionScope tScope = new TransactionScope())
        {
            //case1:取消沽清当前一个菜
            if (!DishPriceI18nID.Contains(","))
            {
                //case3:全部取消沽清
                if (DishPriceI18nID == "cancleAll")
                {
                    Common.RecordEmployeeOperateLogBySYB((int)VAEmployeeOperateLogOperatePageType.DISH_CURRENTSELLOFF, (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE, "收银宝全部取消沽清，shopId:" + shopId);
                    deleteResult = dishOperate.DeleteAllCurrentSellOffInfo(shopId, menuId, SybSessionHelper.GetCurrectSessionEmployeeId());
                }
                else
                {
                    Common.RecordEmployeeOperateLogBySYB((int)VAEmployeeOperateLogOperatePageType.DISH_CURRENTSELLOFF, (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE, "收银宝取消沽清，DishPriceI18nID:" + DishPriceI18nID);
                    deleteResult = dishOperate.DeleteCurrentSellOff(Common.ToInt32(DishPriceI18nID), shopId, SybSessionHelper.GetCurrectSessionEmployeeId());
                }
            }
            //case2:批量取消沽清
            else if (DishPriceI18nID.Contains(","))
            {
                try
                {
                    string[] dishPriceI18nId = DishPriceI18nID.Split(new char[1] { ',' });
                    if (dishPriceI18nId.Length > 0)
                    {
                        int count = 0;
                        bool result = false;
                        for (int i = 0; i < dishPriceI18nId.Length; i++)
                        {
                            result = dishOperate.DeleteCurrentSellOff(Common.ToInt32(dishPriceI18nId[i]), shopId, SybSessionHelper.GetCurrectSessionEmployeeId());
                            if (result)
                            {
                                count++;
                                Common.RecordEmployeeOperateLogBySYB((int)VAEmployeeOperateLogOperatePageType.DISH_CURRENTSELLOFF, (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE, "取消沽清，DishPriceI18nID:" + dishPriceI18nId[i]);
                            }
                        }
                        if (count == dishPriceI18nId.Length)
                        {
                            deleteResult = true;
                        }
                    }
                }
                catch (Exception)
                {
                    deleteResult = false;
                }
            }
            if (deleteResult)
            {
                tScope.Complete();//只有返回结果为true时，结束事物
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }

    public struct DishTypeNameAndDishTypeName
    {
        public string dishTypeName { get; set; }//菜谱分类名称
        public int dishTypeID { get; set; }//菜谱分类ID
    }
    /// <summary>
    /// 获取当前菜谱下的菜品分类名称和分类ID
    /// </summary>
    /// <param name="shopId">门店ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string QueryDishTypeIdAndDiahTypeName(int shopId)
    {
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            return "-1000";
        }
        int menuId = GetMenuIdByShopId(shopId);
        try
        {
            List<DishTypeNameAndDishTypeName> dishTypeList = new List<DishTypeNameAndDishTypeName>();
            DishTypeNameAndDishTypeName dishTypeNameAndDishTypeName = new DishTypeNameAndDishTypeName();
            DishTypeOperate dishTypeOperate = new DishTypeOperate();
            DataTable dt = dishTypeOperate.QueryDishTypeInfo();
            DataView dv = dt.DefaultView;
            dv.RowFilter = "MenuID = " + menuId;
            for (int i = 0; i < dv.Count; i++)
            {
                dishTypeNameAndDishTypeName.dishTypeName = Common.ToString(dv[i]["DishTypeName"]);
                dishTypeNameAndDishTypeName.dishTypeID = Common.ToInt32(dv[i]["DishTypeID"]);
                dishTypeList.Add(dishTypeNameAndDishTypeName);
            }
            return Common.ConvertListToJson<List<DishTypeNameAndDishTypeName>>(dishTypeList);
        }
        catch (Exception)
        {
            return "";
        }
    }
    /// <summary>
    /// 根据shopId获取menuId
    /// </summary>
    /// <param name="shopId">门店ID</param>
    public static int GetMenuIdByShopId(int shopId)
    {
        int menuId = DishOperate.GetMenuIdByShopId(shopId);
        //int menuId = Common.ToInt32(Common.GetFieldValue("MenuConnShop,MenuInfo", "MenuConnShop.menuId", "MenuInfo.MenuID =MenuConnShop.menuId and MenuInfo.MenuStatus='1' and MenuConnShop.shopId='" + shopId + "'"));
        return menuId;
    }
}
