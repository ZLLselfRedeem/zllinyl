<%@ WebHandler Language="C#" Class="wechatOrder" %>

using System;
using System.Web;
using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.Model;
using System.Collections.Generic;
using System.Web.SessionState;

public class wechatOrder : IHttpHandler, IRequiresSessionState
{
    /// <summary>
    /// add by wangc 20140318
    /// 微信点菜
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        string val = "";//返回值，json字符串
        string module = context.Request["m"] == null ? "" : Common.ToString(context.Request["m"].Trim());
        WechatOrderOperate wechatOper = new WechatOrderOperate();
        switch (module)
        {
            case "wechat_login"://微信登录进入点菜页面
                int cityId = Common.ToInt32(context.Request["cityId"]);
                string shopCookie = Common.ToString(context.Request["shopCookie"]);//门店Cookie
                string userCookie = Common.ToString(context.Request["userCookie"]);
                string userUuid = Common.ToString(context.Request["userUuid"]);
                val = wechatOper.WechatLogin(shopCookie, cityId, userCookie, userUuid);
                break;
            case "wechat_register"://微信注册
                shopCookie = Common.ToString(context.Request["shopCookie"]);
                cityId = Common.ToInt32(context.Request["cityId"]);
                userCookie = Common.ToString(context.Request["userCookie"]);
                userUuid = Common.ToString(context.Request["userUuid"]);
                string mobilePhoneNumber = Common.ToString(context.Request["mobilePhoneNumber"]);
                string verificationCode = Common.ToString(context.Request["verificationCode"]);
                val = wechatOper.WechatRegister(shopCookie, cityId, userCookie, userUuid, mobilePhoneNumber, verificationCode);
                break;
            case "wechat_save_order"://微信保存订单
                shopCookie = Common.ToString(context.Request["shopCookie"]);//门店Cookie
                cityId = Common.ToInt32(context.Request["cityId"]);
                userCookie = Common.ToString(context.Request["userCookie"]);
                userUuid = Common.ToString(context.Request["userUuid"]);
                long preOrderId = Common.ToInt64(context.Request["preOrderId"]);//第一次保存传0
                string orderJson = Common.ToString(context.Request["orderJson"]);//菜品json
                string sundryJson = Common.ToString(context.Request["sundryJson"]);//杂项Json
                double clientUxianPriceSum = Common.ToDouble(context.Request["discountPrice"]);//折后价，悠先价
                double clientCalculatedSum = Common.ToDouble(context.Request["originalPrice"]);//原价
                val = wechatOper.WechatSaveOrder(shopCookie, cityId, userCookie, userUuid, orderJson, sundryJson, clientUxianPriceSum, clientCalculatedSum, preOrderId);
                break;
            case "wechat_order_detail"://当前点单详情
                preOrderId = Common.ToInt64(context.Request["preOrderId"]);
                // 这里暂时注释，微信点单没有用了， 待有用时再开放 add by zhujinlei 2015/07/02
                //val = wechatOper.WechatOrderDetail(preOrderId);
                // add end
                
                break;
            case "wechat_order_sell_out"://当前门店沽清菜品编号
                shopCookie = Common.ToString(context.Request["shopCookie"]);//门店Cookie
                val = wechatOper.GetSellOutDish(shopCookie);
                break;
        }
        context.Response.Write(val);
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}