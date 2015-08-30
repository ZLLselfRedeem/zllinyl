﻿<%@ WebHandler Language="C#" Class="ShopHandler" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;

using VAGastronomistMobileApp.WebPageDll;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.Model.Interface;
using VA.CacheLogic.OrderClient;

public class ShopHandler : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "text/plain";
        string response = string.Empty;
        string module = context.Request["action"] == null ? "" : Common.ToString(context.Request["action"]); 
        switch (module)
        {
            case "query_shop_detail":
                int shopId = Common.ToInt32(context.Request["shopId"]);
                int employeeId = Common.ToInt32(context.Request["employeeId"]);

                context.Response.Write(QueryShopDetail(shopId, employeeId));
                break;
            case "query_evaluation_list":
                shopId = Common.ToInt32(context.Request["shopId"]);
                int pageSize = Common.ToInt32(context.Request["pageSize"]);
                int pageIndex = Common.ToInt32(context.Request["pageIndex"]);

                context.Response.Write(QueryEvaluationList(shopId, pageSize, pageIndex));
                break;
            default:
                break;
        }
        
    }

    private static string QueryEvaluationList(  int shopId, int pageSize, int pageIndex)
    {
        var list = PreorderEvaluationOperate.GetListByQuery(pageSize, pageIndex, new PreorderEvaluationQueryObject() { ShopId = shopId });
        var evaluationList = new EvaluationList();
        evaluationList.evaluationList = (from e in list
                                         select new evaluationDetail()
                                         {
                                             customId = e.CustomerId,
                                             evaluationValue = e.EvaluationLevel,
                                             evaluationTime = Common.ToSecondFrom1970(e.EvaluationTime),
                                             evaluationContent = e.EvaluationContent,
                                             preOrder19dianId = e.PreOrder19dianId
                                         }).ToList();
        CustomerInfo customerInfo = null;
        CustomerOperate customerOperate = new CustomerOperate();
        PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
        foreach (var item in evaluationList.evaluationList)
        {
            customerInfo = customerOperate.QueryCustomer(item.customId);
            if (customerInfo != null)
            {
                item.customName = customerInfo.UserName;
                item.mobilePhoneNumber = customerInfo.mobilePhoneNumber;
            }
            var preorder = preOrder19dianOperate.GetPreOrder19dianById(item.preOrder19dianId);
            if (preorder != null)
            {
                item.orderId = preorder.OrderId;
            }
        }
        if (PreorderEvaluationOperate.GetCountByQuery(new PreorderEvaluationQueryObject() { ShopId = shopId }) > (pageSize * pageIndex))
        {
            evaluationList.isMore = true;
        }
        else
        {
            evaluationList.isMore = false;
        }
        SybMsg2 sybMsg = new SybMsg2();
        sybMsg.Insert(0, evaluationList);
        return sybMsg.Value;
    }

    
    
    /// <summary>
    /// 查询门店信息
    /// </summary>
    /// <param name="shopId"></param>
    /// <param name="employeeId"></param>
    /// <returns></returns>
    private static string QueryShopDetail(int shopId, int employeeId)
    {
        ShopInfoCacheLogic shopInfoCacheLogic = new ShopInfoCacheLogic();
        var shopInfo = shopInfoCacheLogic.GetShopInfo(shopId);
        ZZBShopInfo zzbShopInfo = null;
        CustomerOperate customerOperate = new CustomerOperate();

        SybMsg2 sybMsg = new SybMsg2();

        if (shopInfo != null && shopInfo.shopID > 0)
        {

            ShopVIPOperate shopVIPOperate = new ShopVIPOperate();
            ShopEvaluationDetailOperate shopEvaluationDetailOperate = new ShopEvaluationDetailOperate();
            var shopEvaluationDetailList = shopEvaluationDetailOperate.GetShopEvaluationDetailByQuery(new ShopEvaluationDetailQueryObject() { ShopId = shopInfo.shopID });
            ZZB_ShopVipResponse zzb_ShopVipResponse = shopVIPOperate.GetZZBShopVipResponse(shopId, shopInfo.cityID);
            zzbShopInfo = new ZZBShopInfo()
            {
                shopId = shopInfo.shopID,
                shopLevel = shopInfo.shopLevel,
                shopName = shopInfo.shopName,
                shopScore = shopEvaluationDetailList.Sum(p => p.EvaluationValue * p.EvaluationCount),
                prepayOrderCount = shopInfo.prepayOrderCount,
                customerAddedCount = zzb_ShopVipResponse.currectMonthShopIncreasedCount,
                customerCount = zzb_ShopVipResponse.shopTotalCount
            };
            EmployeeOperate employeeOperate = new EmployeeOperate();
            var emloyee = employeeOperate.QueryEmployee(employeeId);
            if (emloyee != null && emloyee.isViewAllocWorker == true)
            {
                zzbShopInfo.sendCouponRight = true;
                zzbShopInfo.staticsRight = true;
            }
            else
            {
                zzbShopInfo.sendCouponRight = ServiceFactory.Resolve<IShopAuthorityService>()
                           .GetEmployeeHasShopAuthority(zzbShopInfo.shopId, employeeId, false,
                               ShopRole.抵扣券发布.GetString());
                zzbShopInfo.staticsRight = ServiceFactory.Resolve<IShopAuthorityService>()
                           .GetEmployeeHasShopAuthority(zzbShopInfo.shopId, employeeId, false,
                               ShopRole.抵扣券统计.GetString());
            }
            if (zzbShopInfo.shopScore < 0)
            {
                zzbShopInfo.shopScore = 0;
            }

            if (shopEvaluationDetailList.Count() < 3)
            {
                for (int i = -1; i < 2; i++)
                {
                    ShopEvaluationDetail entity = shopEvaluationDetailList.FirstOrDefault(p => p.EvaluationValue == i);
                    if (entity == null)
                    {
                        entity = new ShopEvaluationDetail() { EvaluationValue = i, EvaluationCount = 0 };
                        shopEvaluationDetailList.Add(entity);
                    }
                }
            }
            double totalEvaluation = shopEvaluationDetailList.Sum(p => p.EvaluationCount);
            var evaluationPercent = from e in shopEvaluationDetailList
                                    //group e by e.EvaluationValue into g
                                    select new EvaluationPercent()
                                    {
                                        evaluationValue = e.EvaluationValue,
                                        percent = totalEvaluation != 0 ? Math.Round((e.EvaluationCount / totalEvaluation), 4) : 0,
                                        count = e.EvaluationCount
                                    };
            zzbShopInfo.evaluationPercent = evaluationPercent.ToList();
            int[] levelList = new int[] { 2, 3, 4, 5, 6, 12, 18, 24, 30, 36, 72, 108, 144, 180 };
            int nextLevel = levelList.Where(p => p > shopInfo.shopLevel).Min();
            zzbShopInfo.upgradeScore = 15 * nextLevel * nextLevel - 5 * nextLevel - zzbShopInfo.shopScore;

            if (zzbShopInfo.shopLevel == 0)
            {
                zzbShopInfo.shopLevel = 1;
            }
            CouponQueryObject couponQueryObject = new CouponQueryObject()
            {
                ShopId = shopInfo.shopID,
                State = 1,
                EndDateFrom = DateTime.Now,
                StartDateTo = DateTime.Now
            };
            var couponList = CouponOperate.GetListByQuery(couponQueryObject, CouponOrderColumn.CreateTime);
            zzbShopInfo.currentCoupon = new couponDetail();
            if (couponList != null && couponList.Count() > 0)
            {
                var coupon = couponList.First();
                zzbShopInfo.currentCoupon.currentCouponId = coupon.CouponId;
                zzbShopInfo.currentCoupon.requirementMoney = coupon.RequirementMoney;
                zzbShopInfo.currentCoupon.deductibleAmount = coupon.DeductibleAmount;
                var couponGetList = CouponGetDetailOperate.GetListByCouponId(coupon.CouponId);
                zzbShopInfo.currentCoupon.sendCount = couponGetList.Count();
                DateTime yestoday = DateTime.Today.AddDays(-1);
                zzbShopInfo.currentCoupon.yesterdaySendCount = couponGetList.Count(p => p.GetTime >= yestoday && p.GetTime <= yestoday.AddDays(0.999999));
                zzbShopInfo.currentCoupon.totalAmount = couponGetList.Where(p => p.PreOrderSum.HasValue).Sum(p => p.PreOrderSum).Value;
                zzbShopInfo.currentCoupon.yesterdayAmount
                    = couponGetList.Where(p => p.PreOrderSum.HasValue && p.UseTime >= yestoday && p.UseTime <= DateTime.Today).Sum(p => p.PreOrderSum).Value;
            }

            //活动信息
            //获取正在进行中的抵扣券
            couponQueryObject =
                new CouponQueryObject() { StartDateTo = DateTime.Now, EndDateFrom = DateTime.Now, State = 1, CouponType = 1 };
            couponList = CouponOperate.GetListByQuery(couponQueryObject, CouponOrderColumn.CreateTime);
            zzbShopInfo.activityList = new List<string>();
            if (couponList != null && couponList.Count > 0)
            {
                var newActityCoupon = couponList.First();
                var newActityShop = shopInfoCacheLogic.GetShopInfo(newActityCoupon.ShopId.Value);
                string newActityStr =
                    string.Format("最新活动：{0}满{1}减{2}的活动,预计发放{3}张", newActityShop.shopName, newActityCoupon.RequirementMoney,
                                   newActityCoupon.DeductibleAmount, newActityCoupon.SheetNumber);
                var bestActityCouponList = from e in couponList
                                           group e by (e.DeductibleAmount / e.RequirementMoney) into g
                                           select g;
                var bestActityCoupon = bestActityCouponList.OrderByDescending(p => p.Key).First();
                ICoupon bestActity = null;
                if (bestActityCoupon.Count() > 1)
                {
                    bestActity = bestActityCoupon.OrderByDescending(p => shopInfoCacheLogic.GetShopInfo(p.ShopId.Value).shopLevel).First();
                }
                else
                {
                    bestActity = bestActityCoupon.First();
                }

                var bestActityShop = shopInfoCacheLogic.GetShopInfo(bestActity.ShopId.Value);
                string bestActityStr =
                    string.Format("最牛活动：{0}满{1}减{2}的活动,优惠{3}%", bestActityShop.shopName, bestActity.RequirementMoney,
                                   bestActity.DeductibleAmount, Math.Round((bestActity.DeductibleAmount * 100 / bestActity.RequirementMoney), 2));
                string hotStr = shopInfoCacheLogic.GetBestCouponStr();
                if (string.IsNullOrEmpty(hotStr))
                {
                    var hotCoupon =
                        couponList.OrderByDescending(p => CouponGetDetailOperate.GetCountByQuery(new CouponGetDetailQueryObject() { CouponId = p.CouponId })).First();
                    var hotShop = shopInfoCacheLogic.GetShopInfo(hotCoupon.ShopId.Value);
                    hotStr =
                        string.Format("最热活动：{0}满{1}减{2}的活动,已领取{3}张", hotShop.shopName, hotCoupon.RequirementMoney, hotCoupon.DeductibleAmount,
                                CouponGetDetailOperate.GetCountByQuery(new CouponGetDetailQueryObject() { CouponId = hotCoupon.CouponId }));
                }

                string mostEffectiveStr = shopInfoCacheLogic.GetMostEffectiveCouponStr();
                if (string.IsNullOrEmpty(mostEffectiveStr))
                {
                    var mostEffectiveCoupon =
                         couponList.OrderByDescending(p => CouponGetDetailOperate.GetListByCouponId(p.CouponId).Where(g => g.PreOrderSum.HasValue).Sum(h => h.PreOrderSum).Value).First();
                    var mostEffectiveShop = shopInfoCacheLogic.GetShopInfo(mostEffectiveCoupon.ShopId.Value);

                    mostEffectiveStr =
                      string.Format("最有效活动：{0}满{1}减{2}的活动,已带动消费¥{3}", mostEffectiveShop.shopName, mostEffectiveCoupon.RequirementMoney, mostEffectiveCoupon.DeductibleAmount,
                      CouponGetDetailOperate.GetListByCouponId(mostEffectiveCoupon.CouponId).Where(g => g.PreOrderSum.HasValue).Sum(h => h.PreOrderSum).Value);
                    shopInfoCacheLogic.SetMostEffectiveCouponStr(mostEffectiveStr);

                }
                zzbShopInfo.activityList.Add(newActityStr);
                zzbShopInfo.activityList.Add(bestActityStr);
                zzbShopInfo.activityList.Add(hotStr);
                zzbShopInfo.activityList.Add(mostEffectiveStr);
            }
            //好评
            var preorderEvaluationQueryObject = new PreorderEvaluationQueryObject() { ShopId = shopInfo.shopID };
            preorderEvaluationQueryObject.EvaluationLevel = 1;
            var goodEvaluationList
                = PreorderEvaluationOperate.GetListByQuery(5, 1, preorderEvaluationQueryObject, PreorderEvaluationOrderColumn.EvaluationTime);
            zzbShopInfo.goodEvaluationList = (from e in goodEvaluationList
                                              select new evaluationDetail
                                              {
                                                  customId = e.CustomerId,
                                                  evaluationContent = e.EvaluationContent,
                                                  evaluationTime = Common.ToSecondFrom1970(e.EvaluationTime),
                                                  evaluationValue = e.EvaluationLevel,
                                                  preOrder19dianId = e.PreOrder19dianId
                                              }).ToList();
            //中评
            preorderEvaluationQueryObject.EvaluationLevel = 0;
            var middleEvaluationList
                = PreorderEvaluationOperate.GetListByQuery(5, 1, preorderEvaluationQueryObject, PreorderEvaluationOrderColumn.EvaluationTime);
            zzbShopInfo.middleEvaluationList = (from e in middleEvaluationList
                                                select new evaluationDetail
                                                {
                                                    customId = e.CustomerId,
                                                    evaluationContent = e.EvaluationContent,
                                                    evaluationTime = Common.ToSecondFrom1970(e.EvaluationTime),
                                                    evaluationValue = e.EvaluationLevel,
                                                    preOrder19dianId = e.PreOrder19dianId
                                                }).ToList();

            //差评
            preorderEvaluationQueryObject.EvaluationLevel = -1;
            var badEvaluationList
                = PreorderEvaluationOperate.GetListByQuery(5, 1, preorderEvaluationQueryObject, PreorderEvaluationOrderColumn.EvaluationTime);

            zzbShopInfo.badEvaluationList = (from e in badEvaluationList
                                             select new evaluationDetail
                                             {
                                                 customId = e.CustomerId,
                                                 evaluationContent = e.EvaluationContent,
                                                 evaluationTime = Common.ToSecondFrom1970(e.EvaluationTime),
                                                 evaluationValue = e.EvaluationLevel,
                                                 preOrder19dianId = e.PreOrder19dianId
                                             }).ToList();
            CustomerInfo customerInfo = null;
            PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
            foreach (var evaluationDetail in zzbShopInfo.goodEvaluationList)
            {
                var preorder = preOrder19dianOperate.GetPreOrder19dianById(evaluationDetail.preOrder19dianId);
                customerInfo = customerOperate.QueryCustomer(evaluationDetail.customId);
                if (customerInfo != null)
                {
                    evaluationDetail.customName = customerInfo.UserName;
                    evaluationDetail.mobilePhoneNumber = customerInfo.mobilePhoneNumber;
                }
                evaluationDetail.orderId = preorder.OrderId;
            }

            foreach (var evaluationDetail in zzbShopInfo.middleEvaluationList)
            {
                var preorder = preOrder19dianOperate.GetPreOrder19dianById(evaluationDetail.preOrder19dianId);
                customerInfo = customerOperate.QueryCustomer(evaluationDetail.customId);
                if (customerInfo != null)
                {
                    evaluationDetail.customName = customerInfo.UserName;
                    evaluationDetail.mobilePhoneNumber = customerInfo.mobilePhoneNumber;
                }
                evaluationDetail.orderId = preorder.OrderId;
            }

            foreach (var evaluationDetail in zzbShopInfo.badEvaluationList)
            {
                var preorder = preOrder19dianOperate.GetPreOrder19dianById(evaluationDetail.preOrder19dianId);
                customerInfo = customerOperate.QueryCustomer(evaluationDetail.customId);
                if (customerInfo != null)
                {
                    evaluationDetail.customName = customerInfo.UserName;
                    evaluationDetail.mobilePhoneNumber = customerInfo.mobilePhoneNumber;
                }
                evaluationDetail.orderId = preorder.OrderId;
            }
            sybMsg.Insert(0, zzbShopInfo);
        }
        else
        {
            sybMsg.Insert(-1, "店铺不存在!");
        }
        return sybMsg.Value; 
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
 