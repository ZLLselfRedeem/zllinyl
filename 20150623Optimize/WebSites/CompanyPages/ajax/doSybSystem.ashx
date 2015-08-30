<%@ WebHandler Language="C#" Class="doSybSystem" %>

using System;
using System.Web;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Collections.Generic;
using System.Web.SessionState;
/// <summary>
/// 功能描述:收银包后台系统前台和后台交互页面 
///         例如 doSybSystem.ashx?m=dish_info_editinfo&dishid=1234 
///         说明：m：菜品修改模块（模块参数） dishid代表菜品的Id（功能参数,根据模块不同而不一样，参数个数也会不同）
/// 创建标识:wangc20140515
/// </summary>
public class doSybSystem : IHttpHandler, IRequiresSessionState
{
    public void ProcessRequest(HttpContext context)
    {
        string val = "";//返回值，json字符串
        if (HttpContext.Current.Session["MerchantsTreasureUserInfo"] == null)
        {
            val = "-1000";
            context.Response.Write(val);
        }
        else
        {
            string module = context.Request["m"] == null ? "" : Common.ToString(context.Request["m"].Trim());
            switch (module)
            {
                //涉及到上传图片的，待处理，不确定前端使用的上传控件
                /*
                 * 切换系统 
                 */
                case "syb_switched_systems":
                    int interactionStatus = Common.ToInt32(context.Request["status"]);
                    val = MerchantInfoOperate.GetUserAuthorityString(interactionStatus);
                    break;
                /*
                 * 公司信息    
                 */
                case "syb_add_update_company"://收银宝添加公司，修改公司，判断条件json中companyId是否为0
                    string companyInfoJson = Common.ToString(context.Request["addcompanyjson"]);//收银宝添加公司基本信息
                    val = MerchantInfoOperate.CompanyOperate(companyInfoJson);//return: 1:添加成功，修改成功;-1:添加失败，修改失败;-2:填写信息有误
                    break;
                case "syb_delete_company"://收银宝删除公司
                    int companyId = Common.ToInt32(context.Request["companyId"]);
                    val = MerchantInfoOperate.DeleteCompany(companyId);//return：1：删除成功，-2：删除失败，-1：未找到公司信息
                    break;
                case "syb_get_company_list"://收银宝查询公司列表
                    int pageSize = Common.ToInt32(context.Request["pageSize"]);
                    int pageIndex = Common.ToInt32(context.Request["pageIndex"]);
                    string searchKeyWords = Common.ToString(context.Request["searchKeyWords"]);//公司搜索关键字，初始化传“”
                    val = MerchantInfoOperate.GetCompany(pageSize, pageIndex, searchKeyWords);
                    break;
                case "syb_get_company_detail"://收银宝查询公司详情
                    companyId = Common.ToInt32(context.Request["companyId"]);
                    val = MerchantInfoOperate.GetCompanyDetail(companyId);
                    break;
                /*
                公司银行帐号
                */
                case "syb_get_bankaccount_list"://收银宝查询公司银行帐号列表
                    companyId = Common.ToInt32(context.Request["companyId"]);
                    pageSize = Common.ToInt32(context.Request["pageSize"]);
                    pageIndex = Common.ToInt32(context.Request["pageIndex"]);
                    val = MerchantInfoOperate.GetCompanyBankAcount(companyId, pageIndex, pageSize);
                    break;
                case "syb_delete_bankaccount"://收银宝删除公司银行帐号
                    int accountId = Common.ToInt32(context.Request["accountId"]);
                    val = MerchantInfoOperate.DeleteCompanyBankAccount(accountId);
                    break;
                case "syb_add_update_bankaccount"://收银宝添加公司银行帐号
                    string accountJson = Common.ToString(context.Request["accountJson"]);
                    val = MerchantInfoOperate.CompanyBankAccountOperate(accountJson);
                    break;
                case "syb_get_bankaccount_detail"://收银宝查询公司银行帐号详情
                    accountId = Common.ToInt32(context.Request["accountId"]);
                    val = MerchantInfoOperate.GetCompanyBankAccount(accountId);
                    break;
                /*
                设置佣金
                */
                case "syb_get_commissioninfo"://加载友络佣金信息
                    companyId = Common.ToInt32(context.Request["companyId"]);
                    val = MerchantInfoOperate.GetCommissionInfo(companyId);
                    break;
                case "syb_add_update_commissioninfo"://添加和保存友络佣金信息
                    string commissionJson = Common.ToString(context.Request["commissionJson"]);
                    val = MerchantInfoOperate.CommissionOperate(commissionJson);
                    break;
                /*
                 菜谱信息    
                */
                case "syb_company_menu_list":
                    companyId = Common.ToInt32(context.Request["companyId"]);
                    pageSize = Common.ToInt32(context.Request["pageSize"]);
                    pageIndex = Common.ToInt32(context.Request["pageIndex"]);
                    val = MerchantInfoOperate.GetCompanyMenuList(companyId, pageSize, pageIndex, true);
                    break;
                case "syb_before_add_update_menu"://收银宝进入添加和修改菜谱
                    int menuCompanyId = Common.ToInt32(context.Request["menuCompanyId"]);
                    companyId = Common.ToInt32(context.Request["companyId"]);
                    val = MerchantInfoOperate.BeforeMenuOperate(menuCompanyId, companyId);//修改：传递menuCompanyId参数值，新增传递companyId值
                    break;
                case "syb_add_update_menu"://收银宝添加菜谱，修改菜谱，判断条件json中menuCompanyId是否为0
                    string menuJson = Common.ToString(context.Request["menujson"]);
                    val = MerchantInfoOperate.MenuOperate(menuJson);
                    break;
                case "syb_delete_menu"://收银宝删除菜谱信息
                    menuCompanyId = Common.ToInt32(context.Request["menuCompanyId"]);
                    val = MerchantInfoOperate.DeleteMenu(menuCompanyId);//return：1：删除成功，-2：删除失败，-1：未找到菜谱信息
                    break;
                /*
                 门店信息
                */
                case "syb_get_shop_list"://获取门店列表信息
                    pageIndex = Common.ToInt32(context.Request["pageIndex"]);
                    pageSize = Common.ToInt32(context.Request["pageSize"]);
                    searchKeyWords = Common.ToString(context.Request["searchKeyWords"]);//门店搜索关键字，初始化传“”
                    val = MerchantInfoOperate.GetShopList(pageIndex, pageSize, searchKeyWords);
                    break;
                case "syb_before_add_update_shop"://进入修改和添加门店页面加载信息
                    int shopid = Common.ToInt32(context.Request["shopid"]);
                    val = MerchantInfoOperate.BeforeShopOperate(shopid);
                    break;
                case "syb_delete_shop"://删除门店信息
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    val = MerchantInfoOperate.DeleteShop(shopid);
                    break;
                case "syb_check_shopname"://检测当前门店名称是否存在
                    string shopName = Common.ToString(context.Request["shopname"].Trim());
                    val = MerchantInfoOperate.CheckShopName(shopName);
                    break;
                case "syb_add_update_shop"://添加，修改门店信息保存数据
                    string shopJson = Common.ToString(context.Request["shopjson"]);
                    val = MerchantInfoOperate.ShopOperate(shopJson);
                    break;
                case "syb_shopmenu_list"://门店选择菜谱列表（公司下所有菜谱）
                    companyId = Common.ToInt32(context.Request["companyId"]);
                    val = MerchantInfoOperate.GetCompanyMenuList(companyId, 1, 100, false);
                    break;
                case "get_viewalloc_employee"://门店操作修改查询客户经理
                    searchKeyWords = Common.ToString(context.Request["searchKeyWords"]);
                    val = MerchantInfoOperate.GetViewAllocEmployee(searchKeyWords);
                    break;
                case "get_search_company":
                    searchKeyWords = Common.ToString(context.Request["searchKeyWords"]);
                    val = MerchantInfoOperate.QueryAllCompany(context.Request["searchKeyWords"]);
                    break;
                case "get_shop_baidu_coordinates"://获取门店经纬度坐标信息
                    string shopDetailAddress = Common.ToString(context.Request["shopDetailAddress"]);//门店的全地址
                    string cityName = Common.ToString(context.Request["cityName"]);//门店所在城市名称
                    val = MerchantInfoOperate.QueryBaiduCoordinates(shopDetailAddress, cityName);
                    break;
                case "get_shop_bankaccount_list"://获取门店银行帐号列表信息
                    companyId = Common.ToInt32(context.Request["companyId"]);
                    val = MerchantInfoOperate.GetShopAccountList(companyId);
                    break;
                /*
                 门店二维码操作     
                */
                case "syb_before_qrcode"://二维码页面加载
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    val = MerchantInfoOperate.BeforeQrcodeOperate(shopid);
                    break;
                case "syb_qrcode_operate"://生成二维码
                    string typeId = Common.ToString(context.Request["typeId"]);
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    val = MerchantInfoOperate.QrcodeOperate(typeId, shopid);
                    break;
                case "syb_download_qrcode"://下载二维码
                    string qrcodePath = Common.ToString(context.Request["qrcodePath"]);
                    val = MerchantInfoOperate.QrcodeDownload(qrcodePath);
                    break;
                /*
                 门店杂项信息
                */
                case "syb_get_sundry_list"://杂项列表展示
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    pageIndex = Common.ToInt32(context.Request["pageIndex"]);
                    pageSize = Common.ToInt32(context.Request["pageSize"]);
                    val = MerchantInfoOperate.GetSundryList(shopid, pageSize, pageIndex);
                    break;
                case "syb_open_close_sundry"://开启关闭杂项信息
                    long sundryId = Common.ToInt64(context.Request["sundryId"]);
                    int status = Common.ToInt32(context.Request["status"]);
                    val = MerchantInfoOperate.OperateSundry(sundryId, status);
                    break;
                case "syb_before_update_sundry"://修改杂项信息页面加载页面信息
                    sundryId = Common.ToInt64(context.Request["sundryId"]);
                    val = MerchantInfoOperate.BeforeSundryOperate(sundryId);
                    break;
                case "syb_add_update_sundry"://修改，添加杂项信息保存数据
                    string sundryJson = Common.ToString(context.Request["sundryjson"]);
                    val = MerchantInfoOperate.SundryOperate(sundryJson);
                    break;
                /*
                 门店折扣信息
                */
                case "syb_get_shopvip_list"://门店vip等级列表信息
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    pageIndex = Common.ToInt32(context.Request["pageIndex"]);
                    pageSize = Common.ToInt32(context.Request["pageSize"]);
                    val = MerchantInfoOperate.GetShopVip(shopid, pageIndex, pageSize);
                    break;
                case "syb_before_add_update_shopvip"://添加，修改门店vip等级信息初始化信息
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    int shopVipId = Common.ToInt32(context.Request["shopVipId"]);
                    val = MerchantInfoOperate.BeforeShopVipOperate(shopid, shopVipId);
                    break;
                case "syb_delete_shopvip"://删除门店vip等级信息
                    shopVipId = Common.ToInt32(context.Request["shopVipId"]);
                    val = MerchantInfoOperate.DeleteShopVip(shopVipId);
                    break;
                case "syb_add_update_shopvip"://添加，修改门店vip等级信息
                    string shopVipJson = Common.ToString(context.Request["shopVipJson"]);
                    val = MerchantInfoOperate.ShopVipOperate(shopVipJson);
                    break;
                /*
                 门店环境图片
                */
                case "syb_get_shoprevelationimg"://获取门店环境图片地址
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    val = MerchantInfoOperate.GetShopRevelationImage(shopid);
                    break;
                case "syb_delete_shoprevelationimg"://删除门店环境图片
                    long imgId = Common.ToInt64(context.Request["id"]);
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    val = MerchantInfoOperate.DeleteShopRevelationImage(imgId, shopid);
                    break;
                /*
                 门店审核    
                */
                case "syb_get_shopishandle_list"://获取门店审核列表
                    pageIndex = Common.ToInt32(context.Request["pageIndex"]);
                    pageSize = Common.ToInt32(context.Request["pageSize"]);
                    searchKeyWords = Common.ToString(context.Request["searchKeyWords"]);
                    val = MerchantInfoOperate.GetShopHandleList(pageIndex, pageSize, searchKeyWords);
                    break;
                case "syb_shop_ishandle_before"://门店审核页面加载
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    val = MerchantInfoOperate.ShopHandleBefore(shopid);
                    break;
                case "syb_shop_ishandle"://门店审核操作
                    int handleStatus = Common.ToInt32(context.Request["handleStatus"]);
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    val = MerchantInfoOperate.ShopHandleOperate(shopid, handleStatus);
                    break;
                case "syb_shop_ishandle_search"://门店审核搜索全部公司
                    searchKeyWords = Common.ToString(context.Request["searchKeyWords"]);
                    val = MerchantInfoOperate.QueryAllShop(searchKeyWords);
                    break;
                /*
                获取公司和门店列表 
                */
                case "syb_compamy_shop_search":
                    int searchType = Common.ToInt32(context.Request["searchType"]);//1：公司；2：门店
                    searchKeyWords = Common.ToString(context.Request["searchKeyWords"]);
                    val = MerchantInfoOperate.SearchCompanyShop(searchType, searchKeyWords);
                    //待处理
                    break;
                /*
                 * 获取省市县联动信息
                 */
                case "get_provinces_cities_counties":
                    string type = Common.ToString(context.Request["type"]);//province;city;country
                    int id = Common.ToInt32(context.Request["id"]);
                    val = MerchantInfoOperate.GetProvinceCityCountry(type, id);
                    break;
                /*
                 * 商圈配置相关
                 */
                case "get_first_businessdistrict":
                    int cityId = Common.ToInt32(context.Request["cityId"]);
                    val = MerchantInfoOperate.GetLevel1(cityId);
                    break;
                case "get_second_businessdistrict":
                    int tagId = Common.ToInt32(context.Request["tagId"]);
                    val = MerchantInfoOperate.GetLevel2(tagId);
                    break;
                case "get_had_businessdistrict":
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    val = MerchantInfoOperate.GetShopTag(shopid);
                    break;
                case "add_businessdistrict":
                    string tagIds = Common.ToString(context.Request["tagId"]);
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    val = MerchantInfoOperate.AddShopTag(tagIds, shopid);
                    break;
                case "remove_businessdistrict":
                    tagIds = Common.ToString(context.Request["tagId"]);
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    val = MerchantInfoOperate.RemoveShopTag(tagIds, shopid);
                    break;
                //提款方式
                case "syb_get_withdrawtype":
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    val = MerchantInfoOperate.GetWithdrawType(shopid);
                    break;
                case "syb_modify_withdrawtype":
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    string strWithdrawtype = context.Request["withdrawtype"];
                    val = MerchantInfoOperate.UpdateWithdrawType(shopid, strWithdrawtype);
                    break;
                //设置佣金
                case "syb_get_viewalloccommissionvalue":
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    val = MerchantInfoOperate.GetViewallocCommissionValue(shopid);
                    break;
                case "syb_modify_viewalloccommissionvalue":
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    double viewalloccommissionvalue = Common.ToDouble(context.Request["viewalloccommissionvalue"]);
                    val = MerchantInfoOperate.UpdateViewallocCommissionValue(shopid, viewalloccommissionvalue);
                    break;
                //设置折扣
                case "syb_get_discount":
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    val = MerchantInfoOperate.GetShopVipInfo(shopid);
                    break;
                case "syb_modify_discount":
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    double discount = Common.ToDouble(context.Request["discount"]);
                    val = MerchantInfoOperate.UpdateShopVipInfo(shopid,discount);
                    break;
                //修收银行信息
                case "syb_get_account":
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    val = MerchantInfoOperate.GetAccountInfo(shopid);
                    break;
                case "syb_modify_account":
                    shopid = Common.ToInt32(context.Request["shopId"]);
                    string accountInfoJson = Common.ToString(context.Request["accountJson"]);
                    val = MerchantInfoOperate.UpdateAccountInfo(shopid, accountInfoJson);
                    break;
                default:
                    break;

            }
            context.Response.Write(val);
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}