using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using System.Configuration;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using CE.iPhone.PList;
using System.Web;
using Castle.Components.DictionaryAdapter;
using CloudStorage;
using VA.Cache.Distributed;
using VA.Cache.HttpRuntime;
using VA.CacheLogic.OrderClient;
using VA.CacheLogic;
using VA.CacheLogic.Dish;
using VAGastronomistMobileApp.Model.QueryObject;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// Menu操作类
    /// </summary>
    /// <returns></returns>
    public class MenuOperate
    {
        private readonly MenuManager menuMan = new MenuManager();

        /// <summary>
        /// 查询有审核门店在使用的菜单信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryMenuForShopHandled()
        {
            return menuMan.SelectMenuForShopHandled();
        }
        /// <summary>
        /// 查询Menu信息
        /// </summary>
        /// <returns></returns>
        public string QueryMenuImagePath(int menuId)
        {
            //MenuManager menuMan = new MenuManager();
            DataTable dtMenu = menuMan.QueryMenu(menuId);//查询Menu信息
            string menuImagePath = "";
            if (dtMenu != null && dtMenu.Rows.Count > 0)
            {
                menuImagePath = Common.ToString(dtMenu.Rows[0]["menuImagePath"]);
            }
            return menuImagePath;
        }
        /// <summary>
        /// 根据菜谱编号查询对应公司信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public DataTable QueryCompanyByMenu(int menuId)
        {
            //MenuManager menuMan = new MenuManager();
            return menuMan.SelectCompanyByMenu(menuId);
        }
        /// <summary>
        /// 查询总的菜谱版本号
        /// </summary>
        /// <returns></returns>
        //public int QueryMenuVersion()
        //{
        //    MenuManager menuMan = new MenuManager();
        //    return menuMan.QueryVersion();
        //}

        /// <summary>
        /// 新增加菜谱，同时添加菜谱和店铺的对应关系，如果此店铺已经存在菜谱，则删除原菜谱，再新增
        /// </summary>
        /// <param name="vaMenu"></param>
        /// <param name="menuConnShop"></param>
        public FunctionResult AddMenuAndMenuShop(VAMenu vaMenu, List<MenuConnShop> listMenuConnShop)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                FunctionResult functionResult = new FunctionResult();
                bool allInsertPass = true;
                int menuId = AddMenu(vaMenu);
                if (menuId > 0)
                {
                    for (int i = 0; i < listMenuConnShop.Count; i++)
                    {
                        listMenuConnShop[i].menuId = menuId;
                        MenuConnShopOperate menuConnShopOperate = new MenuConnShopOperate();

                        //先查看此店铺之前有无设置过菜谱
                        DataTable dtExist = menuConnShopOperate.QueryMenusByShopId(listMenuConnShop[i].shopId);
                        bool deleteResult = false;
                        if (dtExist != null && dtExist.Rows.Count > 0)//如果设置过，先把之前的对应关系删除
                        {
                            deleteResult = menuConnShopOperate.RemoveMenuConnShopByShopId(listMenuConnShop[i].shopId);
                        }
                        else
                        {
                            deleteResult = true;
                        }
                        if (deleteResult && menuConnShopOperate.AddMenuShop(listMenuConnShop[i]) > 0)
                        {
                            functionResult.returnResult = menuId;
                        }
                        else
                        {
                            functionResult.message = "菜谱店铺对应关系添加失败";
                            functionResult.returnResult = 0;
                            allInsertPass = false;
                        }
                    }

                }
                else
                {
                    functionResult.message = "菜谱信息添加失败";
                    functionResult.returnResult = 0;
                    allInsertPass = false;
                }
                if (allInsertPass)
                {
                    scope.Complete();
                }
                return functionResult;
            }
        }

        /// <summary>
        /// 新增加菜谱，同时添加菜谱和公司的对应关系
        /// </summary>
        /// <param name="vaMenu"></param>
        /// <param name="menuConnCompany"></param>
        public bool AddMenuAndMenuCompany(VAMenu vaMenu, MenuConnCompany menuConnCompany)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                MenuConnShopOperate operate = new MenuConnShopOperate();
                int menuId = AddMenu(vaMenu);
                bool flag = false;
                if (menuId > 0)
                {
                    menuConnCompany.menuId = menuId;
                    if (operate.Add(menuConnCompany) > 0)
                    {
                        scope.Complete();
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
                else
                {
                    flag = false;
                }
                return flag;
            }
        }
        /// <summary>
        /// 新增Menu信息
        /// </summary>
        /// <returns></returns>
        public int AddMenu(VAMenu vaMenu)
        {
            DataTable dtMenu = menuMan.QueryMenu(vaMenu.menuID);
            int menuID = 0;
            if (0 == dtMenu.Rows.Count)
            {//如果传入的MenuID不存在，则新增，否则直接写入多语言表
                MenuInfo menu = new MenuInfo();
                menu.CreateTime = System.DateTime.Now;
                menu.MenuVersion = 0;//备用
                menu.UpdateTime = System.DateTime.Now;
                menu.MenuStatus = 1;
                menu.MenuSequence = vaMenu.menuSequence;
                menu.menuImagePath = vaMenu.menuImagePath;
                menuID = menuMan.InsertMenu(menu);
            }
            if (menuID > 0 || dtMenu.Rows.Count > 0)
            {
                //插入MenuInfo表成功或者该MenuID已存在，则直接插入MenuI18n表                
                if (string.IsNullOrEmpty(vaMenu.menuName))
                {//如果所加Menu信息的名称为空，或者Menu信息表中已有该名称的Menu，则返回false
                    return 0;
                }
                else
                {
                    MenuI18n menuI18n = new MenuI18n();
                    menuI18n.MenuID = menuID;
                    menuI18n.LangID = vaMenu.langID;
                    menuI18n.MenuName = vaMenu.menuName;
                    menuI18n.MenuDesc = vaMenu.menuDesc;
                    menuI18n.MenuI18nStatus = 1;
                    if (menuMan.InsertMenuI18n(menuI18n) > 0)
                    {//插入MenuI18n表成功，则返回true
                        return menuID;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            else
            {
                return menuID;
            }
        }
        /// <summary>
        /// 删除Menu信息
        /// </summary>
        /// <returns></returns>
        public bool RemoveMenu(int menuID)
        {
            DataTable dtMenu = menuMan.QueryMenu(menuID);
            if (dtMenu.Rows.Count > 0)
            {//判断此menuID是否存在，是则删除
                if (menuMan.DeleteMenuByID(menuID))
                {//删除成功则返回true
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 修改Menu信息
        /// </summary>
        /// <returns></returns>
        public bool ModifyMenu(VAMenu vaMenu)
        {
            DataTable dtMenu = menuMan.QueryMenu(vaMenu.menuID);
            if (dtMenu.Rows.Count > 0 && vaMenu.menuName != ""
                && vaMenu.menuName != null)//          
            {//判断此menuID是否存在且修改内容的名称不为空，则修改
                MenuInfo menu = new MenuInfo();
                menu.UpdateTime = System.DateTime.Now;
                menu.MenuStatus = 1;
                menu.MenuID = vaMenu.menuID;
                menu.MenuSequence = vaMenu.menuSequence;
                using (TransactionScope scope = new TransactionScope())
                {
                    if (menuMan.UpdateMenu(menu))
                    {
                        MenuI18n menuI18n = new MenuI18n();
                        menuI18n.MenuID = vaMenu.menuID;
                        menuI18n.LangID = vaMenu.langID;
                        menuI18n.MenuName = vaMenu.menuName;
                        menuI18n.MenuDesc = vaMenu.menuDesc;
                        menuI18n.MenuI18nStatus = 1;
                        menuI18n.MenuI18nID = vaMenu.menuI18nID;
                        if (menuMan.UpdateMenuI18n(menuI18n))
                        {//基本表和多语言表更新成功则返回true
                            scope.Complete();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新菜谱版本，同时修改菜谱文件
        /// </summary>
        public int UpdateMenu(int menuId, string fixedFilePath = "")
        {
            string filePath = WebConfig.ImagePath;
            int oldMenuVersion = menuMan.SelectMenuVersion(menuId);
            DataTable dtCurrentMenu = menuMan.SelectMenu(menuId);
            string menuImagePath = string.Empty;
            if (dtCurrentMenu.Rows.Count == 1)
            {
                menuImagePath = Common.ToString(dtCurrentMenu.Rows[0]["menuImagePath"]);
                filePath += menuImagePath;
            }
            else
            {
                return 0;
            }
            string imagePath = string.Empty;
            imagePath = WebConfig.CdnDomain + WebConfig.ImagePath + menuImagePath;
            //更新版本号
            bool flagUpdateMenu = false;
            using (TransactionScope scope = new TransactionScope())
            {
                flagUpdateMenu = menuMan.UpdateMenuVersionAndTime(menuId);
                if (flagUpdateMenu)
                {
                    scope.Complete();
                }
            }
            int newMenuVersion = 0;
            if (flagUpdateMenu)
            {
                newMenuVersion = menuMan.SelectMenuVersion(menuId);
                string newMenuVersionString = Common.ToString(newMenuVersion);
                string menuIdString = Common.ToString(menuId);
                string oldfileZip = filePath + menuIdString + "_" + Common.ToString(oldMenuVersion) + ".zip";
                string oldfileWechatOrderText = filePath + menuIdString + "_" + Common.ToString(oldMenuVersion) + "_wechat.txt";//生成微信点单txt//套用安卓菜单txt文件，只是改变保存路径 wangc 20140322

                string tempPath = WebConfig.Temp;//服务器临时文件的根目录
                string newFilePathAndNameForPlist_temp = tempPath + menuIdString + "_" + newMenuVersionString + ".plist";
                string newFilePathAndNameForTxt_temp = tempPath + menuIdString + "_" + newMenuVersionString + ".txt";
                string newFileWechatOrderForTxt_temp = tempPath + menuIdString + "_" + newMenuVersionString + "_wechat.txt";//生成微信点菜txt文件路径 wangc 20140322
                string newfileZip_temp = tempPath + menuIdString + "_" + newMenuVersionString + ".zip";

                if (!string.IsNullOrEmpty(fixedFilePath))
                {
                    newFilePathAndNameForPlist_temp = fixedFilePath + menuIdString + "_" + newMenuVersionString + ".plist";
                    newFilePathAndNameForTxt_temp = fixedFilePath + menuIdString + "_" + newMenuVersionString + ".txt";
                    newFileWechatOrderForTxt_temp = fixedFilePath + menuIdString + "_" + newMenuVersionString + "_wechat.txt";//生成微信点菜txt文件路径 wangc 20140322
                    newfileZip_temp = fixedFilePath + menuIdString + "_" + newMenuVersionString + ".zip";
                }
                //OSS路径
                string newFilePathAndNameForPlist = filePath + menuIdString + "_" + newMenuVersionString + ".plist";
                string newFilePathAndNameForTxt = filePath + menuIdString + "_" + newMenuVersionString + ".txt";
                string newFileWechatOrderForTxt = filePath + menuIdString + "_" + newMenuVersionString + "_wechat.txt";//生成微信点菜txt文件路径 wangc 20140322
                string newfileZip = filePath + menuIdString + "_" + newMenuVersionString + ".zip";
                try
                {
                    DishManager plistDishMan = new DishManager();
                    LangManager plistLangMan = new LangManager();
                    MenuManager plistMenuMan = new MenuManager();
                    DishTypeManager plistDishTypeMan = new DishTypeManager();
                    var dicRoot = new PListDict();
                    VAMenuInfoForAndroid menuInfoForAndroid = new VAMenuInfoForAndroid();
                    //添加语言信息
                    LangOperate langOpe = new LangOperate();
                    int defaultLangID = langOpe.DefaultLangID();//获取默认语言ID

                    var arrLanguages = new PListArray();
                    List<string> languagesList = new List<string>();
                    DataTable dtLang = plistLangMan.QueryLang();
                    for (int i = 0; i < dtLang.Rows.Count; i++)
                    {
                        string langNameEn = Common.ToString(dtLang.Rows[i]["langNameEn"]);
                        arrLanguages.Add(new PListString(langNameEn));
                        languagesList.Add(langNameEn);

                    }
                    dicRoot["languages"] = arrLanguages;
                    menuInfoForAndroid.languages = languagesList;

                    //添加菜单详细信息
                    #region 菜单Plist生成
                    var arrMenu = new PListArray();
                    List<VAMenuForAndroid> menuForAndroidList = new List<VAMenuForAndroid>();

                    DataTable dtMenu = plistMenuMan.QueryMenu(menuId);//查询菜单详细信息
                    DataTable dtMenuCopy = dtMenu.Copy();
                    DataView dvCurrentMenu = dtMenu.DefaultView;
                    dvCurrentMenu.RowFilter = "LangID = '" + defaultLangID + "'";//根据默认语言过滤菜单

                    for (int i = 0; i < dvCurrentMenu.Count; i++)
                    {//循环插入各个菜单详细信息
                        var dicMenu = new PListDict();
                        VAMenuForAndroid menuForAndroid = new VAMenuForAndroid();

                        var dictMenuDesc = new PListDict();
                        Dictionary<string, string> dictionaryMenuDesc = new Dictionary<string, string>();
                        var dictMenuName = new PListDict();
                        Dictionary<string, string> dictionaryMenuName = new Dictionary<string, string>();
                        var dictMenuId = new PListDict();

                        DataView dvMenuLang = dtMenuCopy.DefaultView;
                        dvMenuLang.RowFilter = "MenuID = '" + menuId + "'";
                        for (int j = 0; j < dtLang.Rows.Count; j++)
                        {
                            string langNameEn = Common.ToString(dtLang.Rows[j]["langNameEn"]);
                            string menuDesc = Common.ToString(dvMenuLang[j]["MenuDesc"]);
                            string menuName = Common.ToString(dvMenuLang[j]["MenuName"]);
                            dictMenuDesc[langNameEn] = new PListString(menuDesc);//菜单描述
                            dictMenuName[langNameEn] = new PListString(menuName);//菜单名称
                            dictionaryMenuDesc.Add(langNameEn, menuDesc);
                            dictionaryMenuName.Add(langNameEn, menuName);
                        }
                        //菜谱编号
                        dicMenu["menuId"] = new PListInteger(menuId);
                        menuForAndroid.menuId = menuId;
                        //菜单描述
                        dicMenu["menuDesc"] = dictMenuDesc;
                        menuForAndroid.menuDesc = dictionaryMenuDesc;
                        //菜单名称
                        dicMenu["menuName"] = dictMenuName;
                        menuForAndroid.menuName = dictionaryMenuName;
                        //备用
                        dicMenu["menuIcon"] = new PListString("reserve for file name of menu icon");
                        menuForAndroid.menuIcon = "";

                        //菜分类
                        var arrDishType = new PListArray();
                        List<VAMenuTypeForAndroid> menuTypeList = new List<VAMenuTypeForAndroid>();
                        DataTable dtDishType = plistDishTypeMan.QueryDishType(menuId);//查询菜分类详细信息
                        DataTable dtDishTypeCopy = dtDishType.Copy();
                        DataView dvDishType = dtDishType.DefaultView;
                        dvDishType.RowFilter = "LangID = '" + defaultLangID + "'";
                        dvDishType.Sort = "DishTypeSequence ASC,DishTypeName ASC";
                        for (int j = 0; j < dvDishType.Count; j++)
                        {
                            int dishTypeID = Common.ToInt32(dvDishType[j]["DishTypeID"]);
                            var dicDishType = new PListDict();
                            VAMenuTypeForAndroid menuType = new VAMenuTypeForAndroid();
                            var arrDish = new PListArray();
                            List<VAMenuDishForAndroid> menuDishList = new List<VAMenuDishForAndroid>();
                            DataTable dtDish = plistDishMan.QueryDishByMenu(menuId);
                            DataTable dtDishCopy = dtDish.Copy();
                            DataView dvDishNumber = dtDish.DefaultView;
                            dvDishNumber.RowFilter = "DishTypeID ='" + dishTypeID + "' and LangID = '" + defaultLangID + "'";
                            dvDishNumber.Sort = "DishDisplaySequence ASC,dishSalesIn19dian DESC";
                            //循环菜详情
                            for (int k = 0; k < dvDishNumber.Count; k++)
                            {
                                var dicDish = new PListDict();
                                VAMenuDishForAndroid menuDish = new VAMenuDishForAndroid();
                                int dishID = Common.ToInt32(dvDishNumber[k]["DishID"]);
                                dicDish["dishId"] = new PListInteger(dishID);
                                menuDish.dishId = dishID;
                                int dishStatus = Common.ToInt32(dvDishNumber[k]["DishStatus"]);
                                dicDish["dishStatus"] = new PListInteger(dishStatus);
                                menuDish.dishStatus = dishStatus;
                                //19点的销量
                                double dishSalesIn19dian = Common.ToDouble(dvDishNumber[k]["dishSalesIn19dian"]);
                                dicDish["soldCount"] = new PListReal(dishSalesIn19dian);
                                menuDish.soldCount = dishSalesIn19dian;
                                DataTable dtDishImage = plistDishMan.QueryDishImage(dishID);
                                DataTable dtDishImageCopy = dtDishImage.Copy();
                                DataView dvDishImageScale = dtDishImage.DefaultView;
                                dvDishImageScale.RowFilter = "ImageScale = '0'" + " and LangID = '" + defaultLangID + "'";//过滤图片表只留该菜的缩略图
                                if (dvDishImageScale.Count > 0)
                                {
                                    string dishdishThumbnailURL = "";// imagePath + Common.ToString(dvDishImageScale[0]["ImageName"]);
                                    dicDish["dishThumbnail"] = new PListString(dishdishThumbnailURL);
                                    menuDish.dishThumbnail = "";//dishdishThumbnailURL;
                                }
                                else
                                {//如果该菜没有缩略图，则此项为空
                                    dicDish["dishThumbnail"] = new PListString("");
                                    menuDish.dishThumbnail = "";
                                }
                                DataView dvDishLang = dtDishCopy.DefaultView;
                                dvDishLang.RowFilter = "DishTypeID ='" + dishTypeID + "' and DishID = '" + dishID + "'";
                                var dicDishDescDetail = new PListDict();
                                Dictionary<string, string> dictionaryDishDescDetail = new Dictionary<string, string>();
                                var dicDishDescShort = new PListDict();
                                Dictionary<string, string> dictionaryDishDescShort = new Dictionary<string, string>();
                                var dicDishName = new PListDict();
                                Dictionary<string, string> dictionaryDishName = new Dictionary<string, string>();
                                var dicDishQuanPin = new PListDict();
                                Dictionary<string, string> dictionaryDishQuanPin = new Dictionary<string, string>();
                                var dicDishJianPin = new PListDict();
                                Dictionary<string, string> dictionaryDishJianPin = new Dictionary<string, string>();

                                for (int u = 0; u < dtLang.Rows.Count; u++)
                                {
                                    string langNameEn = Common.ToString(dtLang.Rows[u]["langNameEn"]);
                                    string dishDescDetail = Common.ToString(dvDishLang[u]["DishDescDetail"]);
                                    string dishDescShort = Common.ToString(dvDishLang[u]["DishDescShort"]);
                                    string dishName = Common.ToString(dvDishLang[u]["DishName"]);
                                    string dishQuanPin = Common.ToString(dvDishLang[u]["dishQuanPin"]);
                                    string dishJianPin = Common.ToString(dvDishLang[u]["dishJianPin"]);
                                    dicDishDescDetail[langNameEn] = new PListString(dishDescDetail);//菜详细描述
                                    dicDishDescShort[langNameEn] = new PListString(dishDescShort);//菜简单描述
                                    dicDishName[langNameEn] = new PListString(dishName);//菜名称
                                    dicDishQuanPin[langNameEn] = new PListString(dishQuanPin);//菜名全拼
                                    dicDishJianPin[langNameEn] = new PListString(dishJianPin);//菜名首字母
                                    dictionaryDishDescDetail.Add(langNameEn, dishDescDetail);
                                    dictionaryDishDescShort.Add(langNameEn, dishDescShort);
                                    dictionaryDishName.Add(langNameEn, dishName);
                                    dictionaryDishQuanPin.Add(langNameEn, dishQuanPin);
                                    dictionaryDishJianPin.Add(langNameEn, dishJianPin);
                                }
                                dicDish["dishDescDetail"] = dicDishDescDetail;
                                dicDish["dishDescShort"] = dicDishDescShort;
                                dicDish["dishName"] = dicDishName;
                                dicDish["dishQuanPin"] = dicDishQuanPin;
                                dicDish["dishJianPin"] = dicDishJianPin;
                                menuDish.dishDescDetail = dictionaryDishDescDetail;
                                menuDish.dishDescShort = dictionaryDishDescShort;
                                menuDish.dishName = dictionaryDishName;
                                menuDish.dishQuanPin = dictionaryDishQuanPin;
                                menuDish.dishJianPin = dictionaryDishJianPin;
                                //菜图片信息，每个菜多个图
                                var arrDishImage = new PListArray();
                                List<string> dishImageList = new List<string>();
                                DataView dvDishImageNumber = dtDishImageCopy.DefaultView;
                                dvDishImageNumber.RowFilter = "LangID = '" + defaultLangID + "'" + "and ImageScale = '0'";
                                //2011-9-30 xiaoyu 此处只显示菜的大图，增加过滤Imagescale = 0
                                for (int u = 0; u < dvDishImageNumber.Count; u++)
                                {
                                    string dishImageURL = imagePath + Common.ToString(dvDishImageNumber[u]["ImageName"]);
                                    arrDishImage.Add(new PListString(dishImageURL));
                                    dishImageList.Add(dishImageURL);
                                }
                                dicDish["dishImages"] = arrDishImage;
                                menuDish.dishImages = dishImageList;
                                //菜视频信息，每个菜只有一个视频(将多个视频修改为单个视频，plist从array修改到string)
                                //var arrDishVideo = new PListArray();
                                DataTable dtDishVideo = plistDishMan.QueryDishVideo(dishID);
                                DataView dvDishVideo = dtDishVideo.DefaultView;
                                dvDishVideo.RowFilter = "LangID = '" + defaultLangID + "'";
                                if (dvDishVideo.Count > 0)
                                {
                                    string dishVideoURL = imagePath + Common.ToString(dvDishVideo[0]["VideoName"]);
                                    dicDish["dishVideos"] = new PListString(dishVideoURL);
                                    menuDish.dishVideos = dishVideoURL;
                                }
                                else
                                {//如果该菜没有视频，则此项为空
                                    dicDish["dishVideos"] = new PListString("");
                                    menuDish.dishVideos = "";
                                }

                                //菜规格信息
                                var arrDishPrice = new PListArray();
                                List<VAMenuDishPriceForAndroid> menuDishPriceList = new List<VAMenuDishPriceForAndroid>();
                                DataTable dtDishPrice = plistDishMan.QueryDishPrice(dishID);
                                DataTable dtDishPriceCopy = dtDishPrice.Copy();
                                DataView dvDishPriceNumber = dtDishPrice.DefaultView;
                                dvDishPriceNumber.RowFilter = "LangID = '" + defaultLangID + "' and DishSoldout = False";//由于手机客户端暂时没有售罄功能，因此暂时将售罄的菜过滤掉
                                dvDishPriceNumber.Sort = "DishPrice asc";
                                for (int u = 0; u < dvDishPriceNumber.Count; u++)
                                {
                                    var dicDishPrice = new PListDict();
                                    VAMenuDishPriceForAndroid menuDishPrice = new VAMenuDishPriceForAndroid();
                                    //价格
                                    double dishPrice = Common.ToDouble(dvDishPriceNumber[u]["DishPrice"]);
                                    dicDishPrice["dishPrice"] = new PListReal(dishPrice);
                                    menuDishPrice.dishPrice = dishPrice;
                                    //规格ID
                                    int dishPriceID = Common.ToInt32(dvDishPriceNumber[u]["DishPriceID"]);
                                    dicDishPrice["dishPriceId"] = new PListInteger(dishPriceID);
                                    menuDishPrice.dishPriceId = dishPriceID;
                                    //是否售罄
                                    string dishSoldout = Common.ToString(dvDishPriceNumber[u]["DishSoldout"]);
                                    dicDishPrice["dishSoldout"] = new PListString(dishSoldout);
                                    menuDishPrice.dishSoldout = dishSoldout.ToLower();
                                    //是否需要称重
                                    string dishNeedWeigh = Common.ToString(dvDishPriceNumber[u]["DishNeedWeigh"]);
                                    dicDishPrice["dishNeedWeigh"] = new PListString(dishNeedWeigh);
                                    menuDishPrice.dishNeedWeigh = dishNeedWeigh.ToLower();
                                    //是否享受Vip折扣
                                    string vipDiscountable = Common.ToString(dvDishPriceNumber[u]["vipDiscountable"]);
                                    dicDishPrice["vipDiscountable"] = new PListString(vipDiscountable);
                                    menuDishPrice.vipDiscountable = vipDiscountable.ToLower();
                                    //配料份数限定
                                    int dishIngredientsMinAmount = Common.ToInt32(dvDishPriceNumber[u]["dishIngredientsMinAmount"]);
                                    int dishIngredientsMaxAmount = Common.ToInt32(dvDishPriceNumber[u]["dishIngredientsMaxAmount"]);
                                    dicDishPrice["dishIngredientsMinAmount"] = new PListInteger(dishIngredientsMinAmount);
                                    dicDishPrice["dishIngredientsMaxAmount"] = new PListInteger(dishIngredientsMaxAmount);
                                    menuDishPrice.dishIngredientsMinAmount = dishIngredientsMinAmount;
                                    menuDishPrice.dishIngredientsMaxAmount = dishIngredientsMaxAmount;

                                    //口味，配料
                                    DataTable dtDishTaste = Common.GetDataTableFieldValue("DishTaste,DishPriceConnTaste", "DishTaste.tasteId,DishTaste.tasteName",
                                        "menuId ='" + menuId + "' and tasteStatus='1' and DishPriceConnTaste.dishPriceId ='" + dishPriceID + "' and DishTaste.tasteId =DishPriceConnTaste.tasteId");
                                    menuDishPrice.dishTasteList = ConvertHelper<VADishTaste>.ConvertToList(dtDishTaste);
                                    DataTable dtDishIngredients = Common.GetDataTableFieldValue("DishIngredients,DishPriceConnIngredients", "DishIngredients.ingredientsId,ingredientsName,ingredientsPrice,vipDiscountable",
                                        "menuId ='" + menuId + "' and ingredientsStatus='1' and DishPriceConnIngredients.dishPriceId ='" + dishPriceID + "' and DishIngredients.ingredientsId =DishPriceConnIngredients.ingredientsId");
                                    List<VADishIngredients> ingredientList = new List<VADishIngredients>();
                                    for (int v = 0; v < dtDishIngredients.Rows.Count; v++)
                                    {
                                        VADishIngredients ingredients = new VADishIngredients();
                                        ingredients.ingredientsId = Common.ToInt32(dtDishIngredients.Rows[v]["ingredientsId"]);
                                        ingredients.ingredientsName = Common.ToString(dtDishIngredients.Rows[v]["ingredientsName"]);
                                        ingredients.ingredientsPrice = Common.ToDouble(dtDishIngredients.Rows[v]["ingredientsPrice"]);
                                        ingredients.vipDiscountable = Common.ToString(dtDishIngredients.Rows[v]["vipDiscountable"]).ToLower();
                                        ingredientList.Add(ingredients);
                                    }
                                    menuDishPrice.dishIngredientsList = ingredientList;
                                    var arrdishTaste = new PListArray();
                                    for (int w = 0; w < dtDishTaste.Rows.Count; w++)
                                    {
                                        var dicdishTaste = new PListDict();
                                        dicdishTaste["tasteId"] = new PListInteger(Common.ToInt32(dtDishTaste.Rows[w]["tasteId"]));
                                        dicdishTaste["tasteName"] = new PListString(Common.ToString(dtDishTaste.Rows[w]["tasteName"]));
                                        arrdishTaste.Add(dicdishTaste);
                                    }
                                    dicDishPrice["dishTasteList"] = arrdishTaste;
                                    var arrdishIngredients = new PListArray();
                                    for (int x = 0; x < dtDishIngredients.Rows.Count; x++)
                                    {
                                        var dicdishIngredients = new PListDict();
                                        dicdishIngredients["ingredientsId"] = new PListInteger(Common.ToInt32(dtDishIngredients.Rows[x]["ingredientsId"]));
                                        dicdishIngredients["ingredientsName"] = new PListString(Common.ToString(dtDishIngredients.Rows[x]["ingredientsName"]));
                                        dicdishIngredients["ingredientsPrice"] = new PListReal(Common.ToDouble(dtDishIngredients.Rows[x]["ingredientsPrice"]));
                                        dicdishIngredients["vipDiscountable"] = new PListString(Common.ToString(dtDishIngredients.Rows[x]["vipDiscountable"]));
                                        arrdishIngredients.Add(dicdishIngredients);
                                    }
                                    dicDishPrice["dishIngredientsList"] = arrdishIngredients;
                                    //规格名称
                                    var dicDishScaleName = new PListDict();
                                    Dictionary<string, string> dictionaryDishScaleName = new Dictionary<string, string>();
                                    DataView dvDishPriceLang = dtDishPriceCopy.DefaultView;
                                    dvDishPriceLang.RowFilter = "DishPriceID = '" + dishPriceID + "'";//按照当前价格ID过滤价格信息
                                    for (int v = 0; v < dtLang.Rows.Count; v++)
                                    {
                                        string langNameEn = Common.ToString(dtLang.Rows[v]["langNameEn"]);
                                        string scaleName = Common.ToString(dvDishPriceLang[v]["ScaleName"]);
                                        dicDishScaleName[langNameEn] = new PListString(scaleName);
                                        dictionaryDishScaleName.Add(langNameEn, scaleName);
                                    }
                                    dicDishPrice["scaleName"] = dicDishScaleName;
                                    menuDishPrice.scaleName = dictionaryDishScaleName;
                                    arrDishPrice.Add(dicDishPrice);
                                    menuDishPriceList.Add(menuDishPrice);
                                }
                                dicDish["dishPrices"] = arrDishPrice;
                                menuDish.dishPrices = menuDishPriceList;
                                arrDish.Add(dicDish);
                                menuDishList.Add(menuDish);
                            }
                            dicDishType["dishList"] = arrDish;
                            menuType.dishList = menuDishList;
                            //菜分类名称
                            var dicDishTypeName = new PListDict();
                            Dictionary<string, string> dictionaryDishTypeName = new Dictionary<string, string>();
                            DataView dvDishTypeLang = dtDishTypeCopy.DefaultView;
                            dvDishTypeLang.RowFilter = "DishTypeID = '" + dishTypeID + "'";
                            for (int k = 0; k < dtLang.Rows.Count; k++)
                            {
                                string langNameEn = Common.ToString(dtLang.Rows[k]["langNameEn"]);
                                string dishTypeName = Common.ToString(dvDishTypeLang[k]["DishTypeName"]);
                                dicDishTypeName[langNameEn] = new PListString(dishTypeName);
                                dictionaryDishTypeName.Add(langNameEn, dishTypeName);
                            }
                            dicDishType["dishTypeName"] = dicDishTypeName;
                            menuType.dishTypeName = dictionaryDishTypeName;
                            arrDishType.Add(dicDishType);
                            menuTypeList.Add(menuType);
                        }
                        dicMenu["typeList"] = arrDishType;
                        menuForAndroid.typeList = menuTypeList;
                        arrMenu.Add(dicMenu);
                        menuForAndroidList.Add(menuForAndroid);
                    }
                    dicRoot["menus"] = arrMenu;
                    menuInfoForAndroid.menus = menuForAndroidList;
                    #endregion

                    //添加版本信息
                    dicRoot["version"] = new PListString(newMenuVersionString);
                    menuInfoForAndroid.version = newMenuVersionString;

                    #region 暂时取消
                    //添加备注信息
                    //DishOptionManager dishOptionMan = new DishOptionManager();
                    //var arrDishOption = new PListArray();
                    //DataTable dtDishOptionType = dishOptionMan.SelectDishOptionType();
                    //for (int i = 0; i < dtDishOptionType.Rows.Count; i++)
                    //{
                    //    var dicDishOption = new PListDict();
                    //    dicDishOption["optionName"] = new PListString(dtDishOptionType.Rows[i]["optionName"].ToString());
                    //    dicDishOption["internalName"] = new PListString(dtDishOptionType.Rows[i]["internalName"].ToString());
                    //    var arrDishOptionDetail = new PListArray();
                    //    DataTable dtDishOptionDetail = dishOptionMan.SelectDishOptionDetail(Common.ToInt32(dtDishOptionType.Rows[i]["id"]));
                    //    for (int j = 0; j < dtDishOptionDetail.Rows.Count; j++)
                    //    {
                    //        arrDishOptionDetail.Add(new PListString(dtDishOptionDetail.Rows[j]["optionDetailName"].ToString()));
                    //    }
                    //    dicDishOption["optionList"] = arrDishOptionDetail;
                    //    arrDishOption.Add(dicDishOption);
                    //}
                    //dicRoot["orderOptions"] = arrDishOption;

                    //添加折扣分类信息
                    //var arrDiscountType = new PListArray();
                    //DiscountTypeOperate discountTypeOperate = new DiscountTypeOperate();
                    //DataTable dtDiscountType = discountTypeOperate.QueryDiscountType();
                    //for (int i = 0; i < dtDiscountType.Rows.Count; i++)
                    //{
                    //    var dicDiscountType = new PListDict();
                    //    dicDiscountType["discountTypeID"] = new PListString(dtDiscountType.Rows[i]["DiscountTypeID"].ToString());
                    //    dicDiscountType["discountTypeName"] = new PListString(dtDiscountType.Rows[i]["DiscountTypeName"].ToString());
                    //    arrDiscountType.Add(dicDiscountType);
                    //}
                    //dicRoot["discountType"] = arrDiscountType;
                    #endregion
                    var myRoot = new PListRoot();
                    myRoot.Root = dicRoot;
                    //生成临时文件
                    string menuStringForAndroid = JsonOperate.JsonSerializer<VAMenuInfoForAndroid>(menuInfoForAndroid);
                    string menuStringForWechat = "{\"d\":\"" + menuStringForAndroid.Replace("\"", "\\\"") + "\"}";//微信点菜菜谱txt文件，跟安卓相似，只不过处理头尾，20140520 wangc
                    if (string.IsNullOrEmpty(fixedFilePath))
                    {
                        HttpServerUtility server = HttpContext.Current.Server;
                        myRoot.Save(server.MapPath(newFilePathAndNameForPlist_temp), PListFormat.Xml);//生成临时Plist文件
                        File.WriteAllText(server.MapPath(newFilePathAndNameForTxt_temp), menuStringForAndroid);
                        File.WriteAllText(server.MapPath(newFileWechatOrderForTxt_temp), menuStringForWechat);//生成临时微信点单txt文件
                        if (!Common.ToZipFile(server.MapPath(newFilePathAndNameForPlist_temp) + "*" + server.MapPath(newFilePathAndNameForTxt_temp),
                            server.MapPath(newfileZip_temp), ConfigurationManager.AppSettings["menuFileZipPassword"]))
                        {
                            return 0;
                        }
                        //上传到云服务器
                        CloudStorageResult result = CloudStorageOperate.PutObject(newfileZip, server.MapPath(newfileZip_temp));//zip包
                        result = CloudStorageOperate.PutObject(newFileWechatOrderForTxt, server.MapPath(newFileWechatOrderForTxt_temp));//微信点菜txt
                        if (!result.code)
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        myRoot.Save(newFilePathAndNameForPlist_temp, PListFormat.Xml);//生成临时Plist文件
                        File.WriteAllText(newFilePathAndNameForTxt_temp, menuStringForAndroid);
                        File.WriteAllText(newFileWechatOrderForTxt_temp, menuStringForWechat);//生成临时微信点单txt文件
                        if (!Common.ToZipFile(newFilePathAndNameForPlist_temp + "*" + newFilePathAndNameForTxt_temp,
                            newfileZip_temp, ConfigurationManager.AppSettings["menuFileZipPassword"]))
                        {
                            return 0;
                        }
                        //上传到云服务器
                        CloudStorageResult result = CloudStorageOperate.PutObject(newfileZip, newfileZip_temp);//zip包
                        result = CloudStorageOperate.PutObject(newFileWechatOrderForTxt, newFileWechatOrderForTxt_temp);//微信点菜txt
                        if (!result.code)
                        {
                            return 0;
                        }
                    }
                }
                catch (System.Exception)
                {
                    return 0;
                }
                try
                {
                    if (string.IsNullOrEmpty(fixedFilePath))
                    {
                        //删除临时文件
                        if (File.Exists(HttpContext.Current.Server.MapPath(newfileZip_temp)))
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(newfileZip_temp));
                        }
                        if (File.Exists(HttpContext.Current.Server.MapPath(newFileWechatOrderForTxt_temp)))
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(newFileWechatOrderForTxt_temp));
                        }
                        if (File.Exists(HttpContext.Current.Server.MapPath(newFilePathAndNameForPlist_temp)))
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(newFilePathAndNameForPlist_temp));
                        }
                        if (File.Exists(HttpContext.Current.Server.MapPath(newFilePathAndNameForTxt_temp)))
                        {
                            File.Delete(HttpContext.Current.Server.MapPath(newFilePathAndNameForTxt_temp));
                        }
                    }
                    else
                    {
                        //删除临时文件
                        if (File.Exists(newfileZip_temp))
                        {
                            File.Delete(newfileZip_temp);
                        }
                        if (File.Exists(newFileWechatOrderForTxt_temp))
                        {
                            File.Delete(newFileWechatOrderForTxt_temp);
                        }
                        if (File.Exists(newFilePathAndNameForPlist_temp))
                        {
                            File.Delete(newFilePathAndNameForPlist_temp);
                        }
                        if (File.Exists(newFilePathAndNameForTxt_temp))
                        {
                            File.Delete(newFilePathAndNameForTxt_temp);
                        }
                    }
                    //删除云控件上上个版本文件
                    CloudStorageOperate.DeleteObject(oldfileZip);//zip包
                    CloudStorageOperate.DeleteObject(oldfileWechatOrderText);//微信txt文件
                }
                catch (System.Exception)
                {
                }
            }
            else
            {
                return 0;
            }

            //清除菜谱缓存
            DataTable dtable = new MenuConnShopOperate().QueryShopsByMenuId(menuId);

            foreach (DataRow item in dtable.Rows)
            {
                string key = "shopOfMenu_" + Common.ToString(item["shopId"]);
                if (MemcachedHelper.IsKeyExists(key))
                {
                    MemcachedHelper.DeleteMemcached(key);
                }
            }
            //返回更改后的版本号
            return newMenuVersion;
        }


        /// <summary>
        /// 查询某个菜谱下面菜的所有图片
        /// </summary>
        /// <param name="menuId"></param>
        /// <param name="imageScale">0,大图；1，小图</param>
        /// <returns></returns>
        public DataTable QueryMenuImages(int menuId, int imageScale)
        {
            //MenuManager menuMan = new MenuManager();
            return menuMan.SelectMenuImages(menuId, imageScale);
        }
        /// <summary>
        /// 根据菜谱编号查询对应的店铺信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public DataTable QueryShopByMenu(int menuId)
        {
            return menuMan.SelectShopByMenu(menuId);
        }
        /// <summary>
        /// 根据员工编号查询对应的菜谱信息
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public DataTable QueryMenuByEmployee(int employeeId)
        {
            return menuMan.SelectMenuByEmployee(employeeId);
        }
        /// <summary>
        /// 删除菜谱以及对应的菜和分类信息
        /// </summary>
        /// <param name="dishId"></param>
        /// <returns></returns>
        public bool ClearMenuAndDish(int dishId)
        {
            return menuMan.DeleteMenuAndDish(dishId);
        }
        /// <summary>
        /// 根据店铺编号查询对应的沽清菜品20140313
        /// </summary>
        /// <param name="shopSellOffDishRequest"></param>
        /// <returns></returns>
        public VAShopSellOffDishResponse ClientQuerySellOffDishs(VAShopSellOffDishRequest shopSellOffDishRequest)
        {
            VAShopSellOffDishResponse shopSellOffDishResponse = new VAShopSellOffDishResponse();
            shopSellOffDishResponse.type = VAMessageType.CLIENT_SHOP_SELLOFF_RESPONSE;
            shopSellOffDishResponse.cookie = shopSellOffDishRequest.cookie;
            shopSellOffDishResponse.uuid = shopSellOffDishRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(shopSellOffDishRequest.cookie,
                shopSellOffDishRequest.uuid, (int)shopSellOffDishRequest.type, (int)VAMessageType.CLIENT_SHOP_SELLOFF_REQUEST, false);
            List<DishPraiseInfo> dishPraiseNumList = new List<DishPraiseInfo>();
            if (checkResult.result == VAResult.VA_OK)
            {
                //RedEnvelopeDetailOperate redEnvelopeOperate = new RedEnvelopeDetailOperate();
                //double executedRedEnvelopeAmount = 0;
                //bool redEnvelopeFlag = redEnvelopeOperate.DoExpirationRedEnvelopeLogic(Common.ToString(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"]), ref  executedRedEnvelopeAmount);
                //if (!redEnvelopeFlag)
                //{
                //    shopSellOffDishResponse.result = VAResult.VA_FAILED_DB_ERROR;
                //    return shopSellOffDishResponse;
                //}
                RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
                long customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);
                double executedRedEnvelopeAmount = 0;
                var preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                {
                    CustomerId = customerId,
                    CreateTimeFrom = DateTime.Today,
                    PayType =  (int)VAOrderUsedPayMode.REDENVELOPE ,
                    IsRefoundOut = true
                };

                var uuidPreorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                {
                    Uuid = shopSellOffDishRequest.uuid,
                    CreateTimeFrom = DateTime.Today,
                    PayType = (int)VAOrderUsedPayMode.REDENVELOPE,
                    IsRefoundOut = true
                }; 

                if (!string.IsNullOrEmpty(Common.ToString(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"])) &&
                     Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject) ==  0 &&
                     Preorder19DianLineOperate.GetCountByQuery(uuidPreorder19DianLineQueryObject) == 0)
                {
                    executedRedEnvelopeAmount = redEnvelopeOperate.QueryCustomerExcutedRedEnvelope(Common.ToString(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"]));
                }
                else
                {
                    executedRedEnvelopeAmount = 0;
                }

                int shopId = shopSellOffDishRequest.shopId;//请求参数shopId

                ShopInfoCacheLogic shopInfoCacheLogic = new ShopInfoCacheLogic();
                SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();

                DataTable dtMenuShop = shopInfoCacheLogic.GetShopOfMenu(shopId);
                if (dtMenuShop.Rows.Count == 1)
                {
                    shopSellOffDishResponse.dishsSellOff = shopInfoCacheLogic.GetDishSellOffOfShop(shopId);
                    shopSellOffDishResponse.dishIngredientsSellOff = new CurrectIngredientsSellOffInfoOperate().Select(shopId);//沽清配菜      
                    if (checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"].Equals("23588776637"))
                    {
                        shopSellOffDishResponse.payModeList = ((List<VAPayMode>)systemConfigCacheLogic.GetServerPayModel()).FindAll(p => p.payModeId == 1); ;//服务器第三方支付方式
                        shopSellOffDishResponse.defaultPayment = Common.ToInt32(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"]) == 1 ? 1 : 0;//默认支付方式
                    }
                    else
                    {
                        shopSellOffDishResponse.payModeList = (List<VAPayMode>)systemConfigCacheLogic.GetServerPayModel();//服务器第三方支付方式
                        shopSellOffDishResponse.defaultPayment = Common.ToInt32(checkResult.dtCustomer.Rows[0]["defaultPayment"]);//默认支付方式 
                    }
                    shopSellOffDishResponse.rationBalance = Common.ToDouble(checkResult.dtCustomer.Rows[0]["money19dianRemained"]);//粮票金额

                    if (Common.CheckLatestBuild_August(shopSellOffDishRequest.appType, shopSellOffDishRequest.clientBuild))
                    {
                        shopSellOffDishResponse.executedRedEnvelopeAmount = executedRedEnvelopeAmount;//红包金额
                    }
                    else
                    {
                        shopSellOffDishResponse.executedRedEnvelopeAmount = 0;//老版本不支持使用红包
                    }

                    shopSellOffDishResponse.menuList = Common.FillMenuForApp(dtMenuShop);//填充手机端需要的菜谱信息

                    if (Common.CheckLatestBuild_January(shopSellOffDishRequest.appType, shopSellOffDishRequest.clientBuild) == true)
                    {
                        int menuId = Common.ToInt32(dtMenuShop.Rows[0]["MenuID"]);
                        dishPraiseNumList = new DishCacheLogic().GetDishPraiseNumByMenuOfCache(menuId);
                    }

                    ShopInfo shopInfo = shopInfoCacheLogic.GetShopInfo(shopId);//查询门店基本信息
                    if (shopInfo != null)
                    {
                        shopSellOffDishResponse.shopTelephone = shopInfo.shopTelephone;
                        shopSellOffDishResponse.shopAddress = shopInfo.shopAddress;
                        ShopCoordinate shopCoordinateBaidu = shopInfoCacheLogic.GetShopCoordinate(shopId);//百度经纬度
                        if (shopCoordinateBaidu != null)
                        {
                            shopSellOffDishResponse.latitude = shopCoordinateBaidu.latitude;
                            shopSellOffDishResponse.longitude = shopCoordinateBaidu.longitude;
                        }
                        else
                        {
                            shopSellOffDishResponse.latitude = 0;
                            shopSellOffDishResponse.longitude = 0;
                        }
                        if (!shopInfo.isSupportRedEnvelopePayment)
                        {
                            //门店不支持红包支付，直接把红包金额赋值为0，简化客户端操作流程
                            shopSellOffDishResponse.executedRedEnvelopeAmount = 0;
                        }
                        //将当前访问加入该用户访问门店记录表
                        new CustomerCheckedShopManager().Add(new CustomerCheckedShop()
                        {
                            Id = 0,
                            customerId = customerId,
                            companyId = shopInfo.companyID,
                            shopId = shopId,
                            checkTime = DateTime.Now,
                            cityId = shopSellOffDishRequest.cityId
                        });
                    }
                    else
                    {
                        shopSellOffDishResponse.latitude = 0;
                        shopSellOffDishResponse.longitude = 0;
                        shopSellOffDishResponse.shopTelephone = "";
                        shopSellOffDishResponse.shopAddress = "";
                    }
                    DataTable dtShopVipInfo = shopInfoCacheLogic.GetShopVipInfo(shopId);//门店的VIP等级信息
                    VAShopVipInfo userVipInfo = new VAShopVipInfo()
                    {
                        discount = 1,//折扣
                        name = "",//名称
                        platformVipId = 0//VIP等级
                    };
                    if (dtShopVipInfo.Rows.Count > 0)
                    {
                        int currentVipGride = Common.ToInt32(checkResult.dtCustomer.Rows[0]["currentPlatformVipGrade"]);//返回当前用户在平台的VIP等级   
                        ClientExtendOperate.GetUserVipInfo(currentVipGride, dtShopVipInfo, userVipInfo);
                    }
                    shopSellOffDishResponse.userVipInfo = userVipInfo;
                    if (Common.CheckLatestBuild_February(shopSellOffDishRequest.appType, shopSellOffDishRequest.clientBuild))
                    {
                        var operate = new CouponOperate();
                        List<OrderPaymentCouponDetail> list = operate.GetShopCouponDetails(Common.ToString(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"]), shopId,shopSellOffDishRequest.appType,shopSellOffDishRequest.clientBuild);
                        shopSellOffDishResponse.couponDetails = list;
                        shopSellOffDishResponse.isHaveUnusedCoupon = list.Any();
                    }
                    else
                    {
                        shopSellOffDishResponse.couponDetails = new List<OrderPaymentCouponDetail>();
                        shopSellOffDishResponse.isHaveUnusedCoupon = false;
                    }
                    if (shopSellOffDishResponse.rationBalance > 0)
                    {
                        if (Preorder19DianLineOperate.GetCountByQuery(new Preorder19DianLineQueryObject() 
                        { CustomerId = customerId, PayType = (int)VAOrderUsedPayMode.ALIPAY }) > 0 ||
                            Preorder19DianLineOperate.GetCountByQuery(new Preorder19DianLineQueryObject() { CustomerId = customerId, PayType = (int)VAOrderUsedPayMode.WECHAT }) > 0)
                        {
                            shopSellOffDishResponse.isUsedThirdPay = true;
                        }
                        else
                        {
                            shopSellOffDishResponse.isUsedThirdPay = false;
                        }

                    }
                    // 添加杂项 add by zhujinlei 2015/08/07
                    shopSellOffDishResponse.sundryInfo = Common.FillSundry(shopId);//返回门店杂项基本信息

                    shopSellOffDishResponse.result = VAResult.VA_OK;
                }
                else
                {
                    shopSellOffDishResponse.result = VAResult.VA_FAILED_MENUORSHOP_NOT_FOUND;
                }
            }
            else
            {
                shopSellOffDishResponse.result = checkResult.result;
            }
            shopSellOffDishResponse.dishPraiseNumList = dishPraiseNumList;//菜品点赞次数
            shopSellOffDishResponse.recommendedDishList = new List<int>();//推荐菜
            return shopSellOffDishResponse;
        }

        /// <summary>
        /// 收银宝后台检测当前公司的菜谱名称是否存在
        /// </summary>
        /// <param name="menuName"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public bool BoolCheckShopMenuName(string menuName, int companyId)
        {
            return menuMan.BoolCheckShopMenuName(menuName, companyId);
        }
    }
}
