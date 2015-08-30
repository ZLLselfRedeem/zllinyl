<%@ WebHandler Language="C#" Class="doSybChannel" %>

using System;
using System.Web;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Collections.Generic;
using System.Web.SessionState;
using VAGastronomistMobileApp.SQLServerDAL;
/// <summary>
/// 功能描述:收银包后台系统前台和后台交互页面 
/// 用于商户广告频道页面
/// PS：测试请求，先登录收银宝， 然后打开指定页面/CompanyPages/ajax/doSybChannel.ashx?m=test&shopID=18
/// </summary>
public class doSybChannel : IHttpHandler, IRequiresSessionState
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
                //case "test":
                //    int shopID = Common.ToInt32(context.Request["shopID"]);
                //    val = "测试测试测试" + shopID;
                //    break;
                case "shopChannelList": //商户增值页列表接口需要传入对应的商户ID，返回参数id,name,sign(标签),status（开启状态）
                    int shopID = Common.ToInt32(context.Request["shopID"]);
                    new ShopChannelManager().DefaultInsert(shopID);
                    val = ShopChannelOperate.SelectShopChannel(shopID);
                    break;
                case "shopChannelSwitch": //收银宝商户增值页开启/关闭接口，传入参数：商户增值页id和对应的开启/关闭状态值status,返回参数：一个布尔值标识是否操作成功
                    int shopChannelID = Common.ToInt32(context.Request["id"]);
                    int status = Common.ToInt32(context.Request["status"]);
                    val = ShopChannelOperate.SwichRequest(shopChannelID, status);
                    break;
                case "channelSort": // 商户增值页面排序接口，传入参数：channelSort=channelID1,channelIndex1;channelID2,channelIndex2
                    string channelSortedStr = Common.ToString(context.Request["channelSort"]);
                    List<Tuple<int, int>> channelList = ConvertStrToList(channelSortedStr);
                    val = ShopChannelOperate.Sort(channelList);
                    break;
                case "dishSort": // 商户增值页面菜品排序接口，传入参数：disSort=dishID1,dishIndex1;dishID2,dishIndex2
                    string dishSortedStr = Common.ToString(context.Request["dishSort"]);
                    List<Tuple<int, int>> dishList = ConvertStrToList(dishSortedStr);
                    val = ShopChannelDishOperate.Sort(dishList);
                    break;
                case "dishList": // 商户增值页菜品列表，传入参数为shopChannelID
                    shopChannelID = Common.ToInt32(context.Request["shopChannelID"]);
                    val = ShopChannelDishOperate.Select(shopChannelID);
                    break;
                case "noPublicDelete": // 点击前端设置按钮，删除未发布状态的数据，并且把isDelete=1的状态改成0
                    shopChannelID = Common.ToInt32(context.Request["shopChannelID"]);
                    val = ShopChannelDishOperate.NoPublicDelete(shopChannelID);
                    break;
                case "uploadDishImage":
                    HttpPostedFile image_upload = context.Request.Files["Filedata"];//获取图片流文件
                    System.IO.Stream img_Stream = image_upload.InputStream;
                    shopID = Common.ToInt32(context.Request["shopID"]);
                    val = ShopChannelDishOperate.UploadShopDishImage(shopID, img_Stream);
                    break;
                case "channelDishSave":
                    int id = Common.ToInt32(context.Request["id"]);
                    shopChannelID = Common.ToInt32(context.Request["shopChannelID"]);
                    int dishID = Common.ToInt32(context.Request["dishID"]);
                    int dishPriceID = Common.ToInt32(context.Request["dishPriceID"]);
                    int dishIndex = Common.ToInt32(context.Request["dishIndex"]);
                    string dishName = Common.ToString(context.Request["dishName"]).Trim();
                    string dishImageUrl = Common.ToString(context.Request["dishImageUrl"]).Trim();
                    string dishContent = Common.ToString(context.Request["dishContent"]);
                    string operate = Common.ToString(context.Request["operate"]).Trim();
                    if (operate == "add")
                    {
                        val = ShopChannelDishOperate.ChannelDishAdd(shopChannelID, dishID, dishPriceID, dishIndex, dishName, dishImageUrl, dishContent);
                    }
                    else if (operate == "update")
                    {
                        val = ShopChannelDishOperate.ChannelDishUpdate(id, dishID, dishPriceID, dishIndex, dishName, dishImageUrl, dishContent, shopChannelID);
                    }
                    break;
                case "channelDishDelete":
                    string channelDishIDS = Common.ToString(context.Request["channelDishIDS"]).Trim(); // PS: 1,2,4
                    val = ShopChannelDishOperate.ChannelDishDelete(channelDishIDS);
                    break;
                case "searchShopDish":
                    shopID = Common.ToInt32(context.Request["shopID"]);
                    string key = Common.ToString(context.Request["key"]);
                    int pageIndex = Common.ToInt32(context.Request["pageIndex"]);
                    int pageSize = Common.ToInt32(context.Request["pageSize"]);
                    val = ShopChannelDishOperate.SearchShopDish(shopID, key, pageIndex, pageSize);
                    break;
                case "searchShopChannelDish":
                    int shopChannelDishID = Common.ToInt32(context.Request["shopChannelDishID"]);
                    val = ShopChannelDishOperate.SearchShopChannelDish(shopChannelDishID);
                    break;
                case "SearchDishPriceAndDiscount":
                    dishPriceID = Common.ToInt32(context.Request["dishPriceID"]);
                    shopID = Common.ToInt32(context.Request["shopID"]);
                    val = ShopChannelDishOperate.SearchDishPriceAndDiscount(dishPriceID,shopID);
                    break;
                case "ShopChannelDishRelease":
                    shopChannelID = Common.ToInt32(context.Request["shopChannelID"]);
                    val = ShopChannelDishOperate.ShopChannelDishRelease(shopChannelID);
                    break;   
                default:
                    break;

            }
            context.Response.Write(val);
        }
    }


    private static List<Tuple<int, int>> ConvertStrToList(string channelSortedStr)
    {
        string[] splitArray = channelSortedStr.Split(new char[2] { ',', ';' });
        List<Tuple<int, int>> sortedList = new List<Tuple<int, int>>();
        for (int i = 0; i < splitArray.Length; i = i + 2)
        {
            int id = Common.ToInt32(splitArray[i]);
            int indext = Common.ToInt32(splitArray[i + 1]);
            sortedList.Add(new Tuple<int, int>(id, indext));
        }
        return sortedList;
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}