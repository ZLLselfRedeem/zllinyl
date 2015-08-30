﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using Autofac;
using Autofac.Integration.Web;
using IDishMenuAsynUpdate;
using Microsoft.VisualBasic.CompilerServices;
using PagedList;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Transactions;
using System.Configuration;
using System.Threading;
using VA.AllNotifications;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using VAGastronomistMobileApp.WebPageDll.ThreadCallBacks;
using VA.Cache.HttpRuntime;
using VA.Cache.Distributed;
using VA.CacheLogic;
using VA.CacheLogic.ServiceClient;
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.Model.Interface;
using VA.CacheLogic.OrderClient;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class ShopNoticeOperate
    {
        /// <summary>
        /// 返回公告信息
        /// </summary>
        public VAClientShopNoticeResponse GetShopNotice(VAClientShopNoticeRequest request)
        {
            VAClientShopNoticeResponse response = new VAClientShopNoticeResponse()
            {
                type = VAMessageType.CLIENT_SHOP_NOTICE_RESPONSE,
                cookie = request.cookie,
                uuid = request.uuid
            };

            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(request.cookie, request.uuid, (int)request.type, (int)VAMessageType.CLIENT_SHOP_NOTICE_REQUEST);

            if (checkResult.result == VAResult.VA_OK)
            {
                long customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);

                LotteryOperate lotteryOperate = new LotteryOperate();
                bool canLottery = lotteryOperate.CheckCustomerCanLottery(request.uuid, request.shopId, checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"].ToString(), customerId);
                if (!canLottery)
                {
                    response.result = VAResult.VA_OK;
                    response.isCanLotteryToday = false;

                    AwardCacheLogic awardCacheLogic = new AwardCacheLogic();
                    response.notLotteryDesc = awardCacheLogic.GetAwardConfig("cannotLottery", "");
                }

                ShopNoticeManager objShopNoticeManager = new ShopNoticeManager();
                // 获取店家奖品最新版本
                DateTime createTimeShopNoticeVersion = objShopNoticeManager.GetShopNoticeVersion(request.shopId);
                response.noticeVersion = Common.ToInt64(Common.ToSecondFrom1970(Common.ToDateTime(createTimeShopNoticeVersion)));
                // 获取强制弹出公告的时间戳
                string changeTime = objShopNoticeManager.GetAwardResetTimeValue();
                response.noticeShowTime = Common.ToInt64(Common.ToSecondFrom1970(Common.ToDateTime(changeTime)));
                // 待抽奖页面完成后， 需要补允完整的URL
                response.lotteryNoticeUrl = WebConfig.ServerDomain + "Award/awardMobile/awardNotice/awardNotice.html?s={0}&c=" + request.cityId + "";
                response.lotteryNoticeUrl = string.Format(response.lotteryNoticeUrl, request.shopId);
                response.lotteryNoticeUrl = response.lotteryNoticeUrl + "&ww={0}&wh={1}";

                #region 公告信息、抵扣券、折扣、奖品都为空，URL返回空
                int shopID = request.shopId;
                // 开关规则
                var operateAwardConfig = new AwardConfigOperate();
                // 是否开启免排队
                //var isAvoidQueue = Convert.ToBoolean(Convert.ToInt32(operateAwardConfig.GetAwardConfig("AvoidQueue").ConfigCotent));
                var isAvoidQueue = lotteryOperate.IsAvoidQueueSwitchOpen(request.cityId, request.shopId);

                // 是否开启抽奖功能
                //var isLottery = Convert.ToBoolean(Convert.ToInt32(operateAwardConfig.GetAwardConfig("Lottery").ConfigCotent));
                var isLottery = lotteryOperate.IsLotterySwitchOpen(request.cityId, request.shopId);

                // 店铺奖品列表
                ShopAwardOperate shopAwardOperate = new ShopAwardOperate();
                var listShopAward = shopAwardOperate.SelectShopAwardList(shopID);
                var hasShopAward = listShopAward.Count > 0 ? true : false;
                // 全局奖品列表
                ViewAllocAwardOperate viewAllocAwardOperate = new ViewAllocAwardOperate();
                var listVAAward = viewAllocAwardOperate.SelectShopAwardList();
                var hasVAAward = listVAAward.Count > 0 ? true : false;
                // 中奖用户
                //var operateAwardConnPreOrder = new AwardConnPreOrderOperate();
                //var listAwardConnPreOrder = operateAwardConnPreOrder.GetAwardConnPreOrderList(shopID, isAvoidQueue);
                // 店铺的抵扣券列表
                var operate = new CouponOperate();
                var listCoupon = operate.GetShopCouponDetails(shopID);
                var hasCoupon = listCoupon.Count > 0 ? true : false;
                // 显示店铺的折扣
                var operateShopVip = new ShopVIPOperate();
                var hasShopVIP = operateShopVip.GetShopVipDiscount(shopID) > 0 ? true : false;
                //店铺公告
                var operateShopInfo = new ShopOperate();
                var objGiftDesc = Convert.ToString(operateShopInfo.GetShop(shopID).preorderGiftDesc);
                var hasGiftDesc = objGiftDesc.Length > 0 ? true : false;

                if (!isAvoidQueue && !isLottery && !hasCoupon && !hasShopVIP && !hasGiftDesc)
                {
                    response.lotteryNoticeUrl = "";
                }
                if (isAvoidQueue || isLottery)
                {
                    if (!hasShopAward && !hasVAAward && !hasCoupon && !hasShopVIP && !hasGiftDesc)
                    {
                        response.lotteryNoticeUrl = "";
                    }
                }
                // 还少白名单判断

                #endregion

                response.result = VAResult.VA_OK;
            }
            else
            {
                response.result = checkResult.result;
            }
            return response;
        }

        /// <summary>
        /// 获取对应商家的奖品列表
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public List<ShopAward> GetShopAwardByID(int shopID)
        {

            return new List<ShopAward>();
        }
    }
}
