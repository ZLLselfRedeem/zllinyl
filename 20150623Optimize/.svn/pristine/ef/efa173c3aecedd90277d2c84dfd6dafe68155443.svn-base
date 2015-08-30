/*
 * 功能描述：菜单添加修改页面
 * 创建标识：罗国华 20131025
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using System.Collections;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.DBUtility;
using System.Printing;
using System.Configuration;
using System.Transactions;
using System.IO;
using ChineseCharacterToPinyin;
using System.Drawing;
using System.Text;
using System.Web.Services;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using VAGastronomistMobileApp.SQLServerDAL;

public partial class CompanyPages_companyManage : System.Web.UI.Page
{
    #region 全局静态变量
    static string savePath = string.Empty;
    private static string validExtension = ConfigurationManager.AppSettings["extension"].ToString();

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    ///// <summary>
    ///// 获取菜的数据
    ///// </summary>
    ///// <returns></returns>
    //[WebMethod]
    //public static string GetDishTypeAll(string ShopID, string type, string dishI18nID)
    //{
    //    if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
    //    {
    //        return "-1000";
    //    }
    //    string DishId = Common.GetFieldValue("DishI18n", "DishID", "DishI18nID ='" + dishI18nID + "'");
    //    string MenuID = Common.GetFieldValue("MenuConnShop,MenuInfo", "MenuConnShop.menuId", "MenuInfo.MenuID =MenuConnShop.menuId and MenuInfo.MenuStatus='1' and MenuConnShop.shopId='" + ShopID + "'");
    //    DishTypeOperate dishTypeOperate = new DishTypeOperate();
    //    DataTable dtDishTypeInfo = dishTypeOperate.QueryDishTypeInfo(Common.ToInt32(MenuID));//初始化获取
    //    string json = Common.ConvertDateTableToJson(dtDishTypeInfo);
    //    json = json.Replace("TableJson", "DishTypeJson");
    //    string jsonDishinfo = "";
    //    string jsonDishPriceinfo = "";
    //    string jsonDishImage = "";
    //    if (type == "edit")
    //    {
    //        jsonDishinfo = GetDishTypeInfo(DishId);

    //        jsonDishinfo = ",{\"DishJson\":" + jsonDishinfo + "}";

    //        jsonDishPriceinfo = GetDishPriceJson(DishId);
    //        jsonDishPriceinfo = "," + jsonDishPriceinfo.Replace("TableJson", "DishPriceJson");
    //        jsonDishImage = GetDishImage(DishId, MenuID);
    //        jsonDishImage = "," + jsonDishImage.Replace("TableJson", "DishImageJson");
    //    }
    //    return "[" + json + jsonDishinfo + jsonDishPriceinfo + jsonDishImage + "]";

    //    //return SybDishInfoOperate.GetDishEditInfo(Common.ToInt32(DishId));
    //}
    ///// <summary>
    ///// 获取该菜的信息
    ///// </summary>
    ///// <returns></returns>
    //public static string GetDishTypeInfo(string DishID)
    //{
    //    if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
    //    {
    //        return "-1000";
    //    }
    //    if (DishID != null && DishID != "")
    //    {
    //        int int_DishID = Common.ToInt32(DishID);
    //        DishOperate dishOperate = new VAGastronomistMobileApp.WebPageDll.DishOperate();
    //        VADish VADish = dishOperate.QueryDishInfo(int_DishID);
    //        string json = Common.GetJSON<VADish>(VADish);
    //        return json;
    //    }
    //    else
    //    {
    //        return "";
    //    }
    //}
    ///// <summary>
    ///// 获取价格规格信息
    ///// </summary>
    ///// <returns></returns>
    //public static string GetDishPriceJson(string DishID)
    //{
    //    if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
    //    {
    //        return "-1000";
    //    }
    //    if (DishID != null && DishID != "")
    //    {
    //        int int_DishID = Common.ToInt32(DishID);
    //        DishOperate dishOperate = new DishOperate();
    //        DataTable dt = dishOperate.QueryDishPrice(int_DishID);

    //        dt.Columns.Add("status", typeof(string));

    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < dt.Rows.Count; i++)
    //            {
    //                dt.Rows[i]["status"] = "unedit";
    //            }
    //        }
    //        return Common.ConvertDateTableToJson(dt);
    //    }
    //    else return "[]";

    //}
    ///// <summary>
    ///// 菜图片信息
    ///// </summary>
    ///// <param name="DishID"></param>
    ///// <returns></returns>
    //public static string GetDishImage(string DishID, string MenuID)
    //{
    //    if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
    //    {
    //        return "-1000";
    //    }
    //    MenuManager man = new MenuManager();
    //    DataTable dt = man.QueryMenu(Common.ToInt32(MenuID));
    //    string imagePath = ConfigurationManager.AppSettings["Server"] + "/" + ConfigurationManager.AppSettings["ImagePath"].ToString();
    //    if (dt.Rows.Count > 0)
    //    {
    //        savePath = dt.Rows[0]["menuImagePath"].ToString();
    //    }
    //    string path = imagePath + dt.Rows[0]["menuImagePath"].ToString();
    //    string serverpath = @"../" + ConfigurationManager.AppSettings["ImagePath"].ToString() + dt.Rows[0]["menuImagePath"].ToString();
    //    DishOperate dishOperate = new DishOperate();
    //    DataTable dtimage = new DataTable();
    //    DataTable dtbigimage = dishOperate.QueryDishImage(Common.ToInt32(DishID), 0);//大图
    //    if (dtbigimage.Rows.Count <= 0)
    //    {
    //        dtbigimage.Rows.Add();
    //    }
    //    dtimage.Merge(dtbigimage);
    //    dtimage.Columns.Add("imgurlandname", typeof(string));
    //    dtimage.Columns.Add("imgurl", typeof(string));
    //    dtimage.Columns.Add("EditStatus", typeof(string));
    //    for (int i = 0; i < dtimage.Rows.Count; i++)
    //    {
    //        dtimage.Rows[i]["imgurl"] = path;
    //        if (Common.ToInt32(dtimage.Rows[i]["ImageScale"]) != 2 && Common.ToString(dtimage.Rows[i]["ImageName"]) != "")
    //        {
    //            HttpServerUtility server = System.Web.HttpContext.Current.Server;
    //            string urlandname = MakeUrl(Common.ToString(server.MapPath(serverpath + dtimage.Rows[i]["ImageName"])));
    //            dtimage.Rows[i]["imgurlandname"] = urlandname; ;
    //        }
    //        else
    //        {
    //            if (Common.ToString(dtimage.Rows[i]["ImageName"]) != "")
    //            {
    //                HttpServerUtility server = System.Web.HttpContext.Current.Server;
    //                string urlandname = MakeUrl(Common.ToString(server.MapPath(serverpath + dtimage.Rows[i]["ImageName"])));
    //                dtimage.Rows[i]["imgurlandname"] = urlandname;
    //            }
    //        }
    //        dtimage.Rows[i]["EditStatus"] = "unedit";
    //    }

    //    return Common.ConvertDateTableToJson(dtimage);
    //}
    // /// <summary>
    // /// 新增修改方法
    // /// </summary>
    // /// <param name="jsonall"></param>
    // /// <param name="dishjson"></param>
    // /// <param name="DishID"></param>
    // /// <param name="ShopID"></param>
    // /// <returns></returns>
    // [WebMethod]
    // public static string ModefyDish(string jsonPrice, string jsonImage, string dishjson, string dishI18nID, string ShopID)
    // {
    //     if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
    //     {
    //         return "-1000";
    //     }
    //     try
    //     {
    //         #region 获取并处理脏数据,wyh同志会传递undefined数据
    //         dishjson = dishjson.Replace("undefined", "0");
    //         dishjson = dishjson.Replace("\"undefined\"", "\"\"");

    //         DishJsonforCompanyPages dishjsoncompanypages = JsonOperate.JsonDeserialize<DishJsonforCompanyPages>(dishjson);
    //         DishPriceJsonforCompanyPages dishpricejsonforcompanypages = JsonOperate.JsonDeserialize<DishPriceJsonforCompanyPages>(jsonPrice);

    //         for (int i = dishpricejsonforcompanypages.DishPriceJson.Count - 1; i >= 0; i--)
    //         {
    //             if (Common.ToString(dishpricejsonforcompanypages.DishPriceJson[i].ScaleName) == "")
    //             {
    //                 return Common.ToString((int)VADishManageType.PLEASE_INPUTNAME);
    //             }
    //         }
    //         if (dishjsoncompanypages.DishJson[0].DishTypeID.Count == 0)
    //         {
    //             return Common.ToString((int)VADishManageType.PLEASE_CHOSE_TYPE);

    //         }
    //         DishImageJsonforCompanyPages dishimagejsonforcompanypages = JsonOperate.JsonDeserialize<DishImageJsonforCompanyPages>(jsonImage);
    //         #endregion
    //         #region 获取MenuID,int_DishID
    //         bool UpdateDish = true;
    //         string MenuID = Common.GetFieldValue("MenuConnShop,MenuInfo", "MenuConnShop.menuId", "MenuInfo.MenuID =MenuConnShop.menuId and MenuInfo.MenuStatus='1' and MenuConnShop.shopId='" + ShopID + "'");
    //         string DishIdfromDishI18nID = Common.GetFieldValue("DishI18n", "DishID", "DishI18nID ='" + dishI18nID + "'");
    //         int int_DishID = Common.ToInt32(DishIdfromDishI18nID);
    //         #endregion
    //         #region 处理菜信息
    //         VADish vADish = new VADish();
    //         vADish.dishDescDetail = Common.ToString(dishjsoncompanypages.DishJson[0].dishDescDetail);
    //         vADish.dishDescShort = Common.ToString(dishjsoncompanypages.DishJson[0].dishDescShort);
    //         vADish.dishDisplaySequence = Common.ToInt32(dishjsoncompanypages.DishJson[0].dishDisplaySequence);
    //         //vADish.dishI18nID = Common.ToInt32(dishjsoncompanypages.DishJson[0].dishI18nID);
    //         vADish.dishI18nID = Common.ToInt32(dishI18nID);
    //         vADish.dishID = int_DishID;
    //         vADish.dishName = Common.ToString(dishjsoncompanypages.DishJson[0].dishName);
    //         vADish.dishTotalQuantity = 0;//预留的值
    //         vADish.langID = 1;
    //         vADish.menuID = Common.ToInt32(MenuID);
    //         vADish.dishQuanPin = Common.ToString(dishjsoncompanypages.DishJson[0].dishQuanPin).Trim();
    //         vADish.dishJianPin = Common.ToString(dishjsoncompanypages.DishJson[0].dishJianPin).Trim();
    //         ArrayList bDishType = new ArrayList();
    //         string dishTypeLog = "";
    //         for (int n = 0; n < dishjsoncompanypages.DishJson[0].DishTypeID.Count; n++)
    //         {
    //             bDishType.Add(dishjsoncompanypages.DishJson[0].DishTypeID[n]);
    //             dishTypeLog += dishjsoncompanypages.DishJson[0].DishTypeID[n];
    //         }
    //         string logDataDish = "菜品基本信息:菜品dishID:" + vADish.dishID + ",菜品名称:" + vADish.dishName + ",DishI18nID:" + dishI18nID + ",排序:"
    //+ vADish.dishDisplaySequence + ",菜谱ID:" + vADish.menuID + ",详细描述:" + vADish.dishDescDetail + ",简介:" + vADish.dishDescShort + ",分类:" + dishTypeLog;

    //         CompanyPages_dishManage dishmanager = new CompanyPages_dishManage();
    //         vADish.DishTypeID = bDishType;
    //         vADish.cookPrinterName = "";            //暂时为空 以后处理
    //         if (bDishType.Count <= 0)
    //         {
    //             UpdateDish = false;
    //             return Common.ToString((int)VADishManageType.PLEASE_CHOSE_TYPE);//请选择分类
    //         }
    //         else
    //         {
    //             DishOperate DO = new DishOperate();
    //             if (int_DishID != 0)//修改
    //             {
    //                 DishOperate dOperate = new DishOperate();
    //                 bool i = dOperate.ModifyDishInfo(vADish);
    //                 UpdateDish = true;
    //                 //记录日志
    //                 Common.RecordEmployeeOperateLogBySYB((int)VAEmployeeOperateLogOperatePageType.DISHINFO
    //                     , (int)VAEmployeeOperateLogOperateType.UPDATE_OPERATE, "修改[" + logDataDish + "]");
    //             }
    //             else//新增
    //             {
    //                 bool IsrepeatName = DO.IsCanQueryThisDish(vADish.menuID, vADish.dishName);
    //                 if (IsrepeatName)
    //                 {
    //                     DishOperate dOperate = new DishOperate();
    //                     int dishID = dOperate.AddDishInfo(vADish);
    //                     if (dishID < 0)
    //                     {
    //                         UpdateDish = false;
    //                         return Common.ToString((int)VADishManageType.SAVE_FAIL);//添加失败
    //                     }
    //                     else
    //                     {
    //                         int_DishID = dishID;
    //                         UpdateDish = true;
    //                         //记录日志
    //                         Common.RecordEmployeeOperateLogBySYB((int)VAEmployeeOperateLogOperatePageType.DISHINFO
    //                             , (int)VAEmployeeOperateLogOperateType.ADD_OPERATE, "新增[" + logDataDish + ",新菜品dishID:" + int_DishID + "]");
    //                     }
    //                 }
    //                 else
    //                 {
    //                     UpdateDish = false;
    //                     return Common.ToString((int)VADishManageType.EXITS_SAME_DISHNAME);//该菜名已经存在
    //                 }
    //             }
    //         }
    //         #endregion
    //         #region 添加菜规格价格
    //         if (UpdateDish)
    //         {
    //             #region base
    //             DishOperate dishOperate = new DishOperate();
    //             VADishPrice vADishPrice = new VADishPrice();
    //             bool isdelete = dishOperate.DeleteDishPriceByDishID(int_DishID);
    //             string logDataDishPrice = "收银宝dishMange";
    //             for (int i = 0; i < dishpricejsonforcompanypages.DishPriceJson.Count; i++)
    //             {
    //                 #region wyh同志没提供状态，所以先删除所有信息 再新增
    //                 //if (dishOperate.QueryDishIsAddorUpdate(dishpricejsonforcompanypages.DishPriceJson[i].DishPriceID) != "")
    //                 //{
    //                 //    vADishPrice.dishID = Common.ToInt32(DishID);
    //                 //    vADishPrice.dishPrice = double.Parse(dishpricejsonforcompanypages.DishPriceJson[i].DishPrice);
    //                 //    vADishPrice.dishPriceI18nID = Common.ToInt32(dishpricejsonforcompanypages.DishPriceJson[i].DishPriceI18nID);
    //                 //    vADishPrice.dishPriceID = Common.ToInt32(dishpricejsonforcompanypages.DishPriceJson[i].DishPriceID);
    //                 //    vADishPrice.langID = 1;
    //                 //    vADishPrice.scaleName = Common.ToString(dishpricejsonforcompanypages.DishPriceJson[i].ScaleName);
    //                 //    vADishPrice.vipDiscountable = bool.Parse(dishpricejsonforcompanypages.DishPriceJson[i].vipDiscountable);
    //                 //    vADishPrice.markName = Common.ToString(dishpricejsonforcompanypages.DishPriceJson[i].markName);
    //                 //    vADishPrice.backDiscountable = bool.Parse(dishpricejsonforcompanypages.DishPriceJson[i].backDiscountable);
    //                 //    dishOperate.ModifyDishPricesh(vADishPrice);
    //                 //}
    //                 //else
    //                 //{
    //                 if (isdelete)
    //                 {
    //                     vADishPrice.dishID = int_DishID;
    //                     vADishPrice.dishPrice = double.Parse(Common.ToString(dishpricejsonforcompanypages.DishPriceJson[i].DishPrice));
    //                     vADishPrice.langID = 1;
    //                     vADishPrice.scaleName = Common.ToString(dishpricejsonforcompanypages.DishPriceJson[i].ScaleName);
    //                     vADishPrice.vipDiscountable = bool.Parse(dishpricejsonforcompanypages.DishPriceJson[i].vipDiscountable);
    //                     vADishPrice.markName = Common.ToString(dishpricejsonforcompanypages.DishPriceJson[i].markName);
    //                     vADishPrice.backDiscountable = bool.Parse(dishpricejsonforcompanypages.DishPriceJson[i].backDiscountable);
    //                     dishOperate.AddDishPricesh(vADishPrice);
    //                     string log = "菜品dishID:" + vADishPrice.dishID + ",价格:" + vADishPrice.dishPrice + ",规格名称:" + vADishPrice.scaleName +
    //                     ",是否称重:" + vADishPrice.dishNeedWeigh + ",VIP折扣:" + vADishPrice.vipDiscountable + ",是否返送:" + vADishPrice.backDiscountable +
    //                     ",掌中宝编号:" + vADishPrice.markName;
    //                     logDataDishPrice += "新增[" + log + "]";
    //                 }
    //                 //}
    //                 #endregion
    //             }
    //             //记录日志
    //             Common.RecordEmployeeOperateLogBySYB((int)VAEmployeeOperateLogOperatePageType.DISHINFO
    //                 , (int)VAEmployeeOperateLogOperateType.ADD_OPERATE, "新增[" + logDataDishPrice + "]");
    //             #endregion
    //         }
    //         #endregion
    //         #region 处理图片

    //         string id = dishimagejsonforcompanypages.DishImageJson[0].imgurlId;
    //         if (dishimagejsonforcompanypages.DishImageJson[0].selectRect.Count > 0 && id != "")//未修改图片，不做修改
    //         {
    //             int x = Common.ToInt32(dishimagejsonforcompanypages.DishImageJson[0].selectRect[0]);//客户端获取的坐标 
    //             int y = Common.ToInt32(dishimagejsonforcompanypages.DishImageJson[0].selectRect[1]);
    //             int w = Common.ToInt32(dishimagejsonforcompanypages.DishImageJson[0].selectRect[2]);//客户端获取的长宽
    //             int h = Common.ToInt32(dishimagejsonforcompanypages.DishImageJson[0].selectRect[3]);
    //             //删除老的图片信息
    //             DishOperate dishimageoperate = new DishOperate();
    //             bool removeimage = dishimageoperate.RemoveDishImagebyDishID(int_DishID);

    //             int kehuduanwidth = 320;

    //             string bigimageName;
    //             string smallimageName;

    //             ImageUploadOperate.ImageMake(MenuID, id, x, y, w, h, kehuduanwidth, out bigimageName, out smallimageName);

    //             #region 图片数据库信息添加
    //             ImageInfo bigimageInfo = new ImageInfo();
    //             bigimageInfo.DishID = int_DishID;
    //             bigimageInfo.ImageName = bigimageName;
    //             bigimageInfo.ImageScale = 0;
    //             bigimageInfo.ImageSequence = 1;
    //             bigimageInfo.ImageStatus = 1;
    //             ImageInfo smallimageInfo = new ImageInfo();
    //             smallimageInfo.DishID = int_DishID;
    //             smallimageInfo.ImageName = smallimageName;
    //             smallimageInfo.ImageScale = 1;
    //             smallimageInfo.ImageSequence = 1;
    //             smallimageInfo.ImageStatus = 1;
    //             DishOperate dishOperate2 = new DishOperate();
    //             dishOperate2.AddDishImage(bigimageInfo, smallimageInfo);
    //             #endregion



    //         }
    //         #endregion
    //         return Common.ToString((int)VADishManageType.SAVE_SUCCESS);

    //     }
    //     catch (Exception)
    //     {
    //         return Common.ToString((int)VADishManageType.SAVE_FAIL);
    //     }
    // }

    ///// <summary>
    ///// 从session获取图片
    ///// </summary>
    ///// <param name="url"></param>
    ///// <returns></returns>
    //public static string MakeUrl(string url)
    //{
    //    if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
    //    {
    //        return "-1000";
    //    }
    //    try
    //    {
    //        //Bitmap bmp = new Bitmap(url);
    //        System.Drawing.Image original_image = null;
    //        System.Drawing.Bitmap final_image = null;
    //        System.Drawing.Graphics graphic = null;
    //        MemoryStream ms = null;
    //        FileStream fileStream = new FileStream(url, FileMode.Open, FileAccess.Read, FileShare.Read);
    //        original_image = System.Drawing.Image.FromStream(fileStream);
    //        int width = original_image.Width;
    //        int height = original_image.Height;
    //        int target_width = 600;
    //        int target_height = 450;
    //        int new_width, new_height;

    //        float target_ratio = (float)target_width / (float)target_height;
    //        float image_ratio = (float)width / (float)height;

    //        if (target_ratio > image_ratio)
    //        {
    //            new_height = target_height;
    //            new_width = (int)Math.Floor(image_ratio * (float)target_height);
    //        }
    //        else
    //        {
    //            new_height = (int)Math.Floor((float)target_width / image_ratio);
    //            new_width = target_width;
    //        }

    //        new_width = new_width > target_width ? target_width : new_width;
    //        new_height = new_height > target_height ? target_height : new_height;


    //        // Create the thumbnail

    //        // Old way
    //        //thumbnail_image = original_image.GetThumbnailImage(new_width, new_height, null, System.IntPtr.Zero);
    //        // We don't have to create a Thumbnail since the DrawImage method will resize, but the GetThumbnailImage looks better
    //        // I've read about a problem with GetThumbnailImage. If a jpeg has an embedded thumbnail it will use and resize it which
    //        //  can result in a tiny 40x40 thumbnail being stretch up to our target size


    //        final_image = new System.Drawing.Bitmap(target_width, target_height);
    //        graphic = System.Drawing.Graphics.FromImage(final_image);
    //        graphic.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), new System.Drawing.Rectangle(0, 0, target_width, target_height));
    //        int paste_x = (target_width - new_width) / 2;
    //        int paste_y = (target_height - new_height) / 2;
    //        graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic; /* new way */
    //        //graphic.DrawImage(thumbnail_image, paste_x, paste_y, new_width, new_height);
    //        graphic.DrawImage(original_image, paste_x, paste_y, new_width, new_height);

    //        // Store the thumbnail in the session (Note: this is bad, it will take a lot of memory, but this is just a demo)
    //        ms = new MemoryStream();
    //        final_image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

    //        // Store the data in my custom Thumbnail object
    //        //string thumbnail_id = DateTime.Now.ToString("yyyyMMddHHmmssfff");
    //        string thumbnail_id = System.Guid.NewGuid().ToString();
    //        Thumbnail thumb = new Thumbnail(thumbnail_id, ms.GetBuffer());

    //        // Put it all in the Session (initialize the session if necessary)			
    //        List<Thumbnail> thumbnails = HttpContext.Current.Session["file_info"] as List<Thumbnail>;
    //        if (thumbnails == null)
    //        {
    //            thumbnails = new List<Thumbnail>();
    //            HttpContext.Current.Session["file_info"] = thumbnails;
    //        }
    //        thumbnails.Add(thumb);
    //        return thumbnail_id;
    //    }
    //    catch
    //    {
    //        return "";
    //    }
    //}
}