﻿using Autofac.Integration.Web;
using CloudStorage;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using Autofac;
using VAGastronomistMobileApp.WebPageDll.Services;
using VA.CacheLogic.OrderClient;
using VAGastronomistMobileApp.Model.QueryObject;
using System.Collections;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class ClientExtendOperate
    {
        /// <summary>
        ///  获取客户端首页主数据
        /// </summary>
        /// <param name="dbDataList"></param>
        /// <param name="currentVipGride"></param>
        /// <param name="list"></param>
        /// <param name="cityId"></param>
        /// <param name="isNewBuild">标记是否为抵价券最新版本</param>
        /// <returns></returns>
        public List<VAIndexList> GetClientIndexData(List<VAIndexExt> dbDataList, int currentVipGride, List<VAIndexList> list, int cityId, bool isNewBuild, List<int> favorateShop)
        {
            var shopIds = new List<int>();
            if (isNewBuild)
            {
                shopIds = new CouponOperate().GetHadCouponShopId(cityId).Distinct().ToList();
            }

            string initPath = WebConfig.CdnDomain + WebConfig.ImagePath;
            ShopInfoCacheLogic shopInfoCacheLogic = new ShopInfoCacheLogic();
            foreach (var item in dbDataList)
            {
                VAIndexList vaIndexInfo = new VAIndexList();//主model
                int defaultMenuId = item.menuId;
                vaIndexInfo.shopId = item.shopID;//门店编号
                DataTable dtShopVipInfo = shopInfoCacheLogic.GetShopVipInfo(vaIndexInfo.shopId);//查询当前门店的VIP等级信息
                if (dtShopVipInfo.Rows.Count > 0)
                {
                    List<VAShopVipInfo> shopVipList = new List<VAShopVipInfo>();
                    VAShopVipInfo userVipInfo = new VAShopVipInfo();
                    GetUserVipInfo(currentVipGride, dtShopVipInfo, userVipInfo, shopVipList);
                    vaIndexInfo.shopVipInfo = shopVipList;
                    vaIndexInfo.userVipInfo = userVipInfo;
                }
                else
                {//店家未开通店铺折扣
                    vaIndexInfo.userVipInfo = new VAShopVipInfo()
                    {
                        discount = 1,//折扣
                        name = "",//名称
                        platformVipId = 0//VIP等级
                    };
                }

                if (favorateShop.Contains(vaIndexInfo.shopId))
                {
                    vaIndexInfo.isFavorite = true;//该门店已收藏
                }
                else
                {
                    vaIndexInfo.isFavorite = false;
                }
                vaIndexInfo.shopName = item.shopName;//门店名称
                vaIndexInfo.longitude = item.longitude;//门店经度
                vaIndexInfo.latitude = item.latitude; //门店纬度
                vaIndexInfo.acpp = item.acpp;//门店人均
                vaIndexInfo.shopRating = item.shopRating;//门店评分
                vaIndexInfo.orderDishDesc = item.orderDishDesc;//点菜描述
                vaIndexInfo.prepayOrderCount = item.prepayOrderCount;//当前门店支付点单次数
                vaIndexInfo.shopAddress = item.shopAddress;
                vaIndexInfo.shopLevel = item.shopLevel;
                vaIndexInfo.isSupportPayment = item.isSupportPayment;
                vaIndexInfo.goodEvaluationCount = item.goodEvaluationCount;
                string shopImagePath = item.shopImagePath;
                if (string.IsNullOrEmpty(shopImagePath))
                {
                    vaIndexInfo.shopLogoUrl = "";
                    vaIndexInfo.shopMedalUrl = new List<string>();
                }
                else
                {
                    vaIndexInfo.shopMedalUrl = shopInfoCacheLogic.GetShopMedalInfo(vaIndexInfo.shopId, initPath + shopImagePath);//门店勋章
                    vaIndexInfo.shopLogoUrl = String.IsNullOrEmpty(item.shopLogo) ? "" : initPath + shopImagePath + item.shopLogo;
                }
                vaIndexInfo.isSupportAccountsRound = item.isSupportAccountsRound;//门店是否支持四舍五入
                if (!String.IsNullOrEmpty(item.publicityPhotoPath))
                {
                    vaIndexInfo.publicityPhotoPath = initPath + item.publicityPhotoPath;//门店背景图片
                }
                else
                {
                    vaIndexInfo.publicityPhotoPath = "";
                }
                vaIndexInfo.menuList = Common.FillMenuForApp(shopInfoCacheLogic.GetShopMenuInfo(defaultMenuId));//填充手机端需要的菜谱信息
                vaIndexInfo.sundryInfo = Common.FillSundry(vaIndexInfo.shopId);//返回门店杂项基本信息

                vaIndexInfo.isHaveCoupon = isNewBuild ? shopIds.Contains(vaIndexInfo.shopId) : false;
                if (vaIndexInfo.isHaveCoupon)
                {
                    CouponOperate couponOperate = new CouponOperate();
                    Coupon coupon = couponOperate.GetCouponListOfShop(vaIndexInfo.shopId);
                    if (coupon != null && coupon.CouponId > 0)
                    {
                        vaIndexInfo.couponContent = "满" + coupon.RequirementMoney + "减" + coupon.DeductibleAmount;
                    }
                    else
                    {
                        vaIndexInfo.couponContent = "";
                    }
                }
                else
                {
                    vaIndexInfo.couponContent = "";
                }


                //-------------------------------------------------------------------------------------------------
                //返回给客户端 免排队、送菜、用券、折扣标识

                //规则如下：
                //免排队与送菜，优先显示“免排队”
                //折扣与满减，优先显示“折扣”
                //如果标签数量大于2个，则按上述规则选取
                //如果标签数量不超过2个，则全部显示

                List<string> signList = new List<string>();

                ShopAwardOperate shopAwardOperate = new ShopAwardOperate();

                //查询门店所有奖品
                //先看抽奖开关是否打开

                LotteryOperate lotteryOperate = new LotteryOperate();

                bool avoidQueue = lotteryOperate.IsAvoidQueueSwitchOpen(cityId, vaIndexInfo.shopId);
                bool lotterySwitch = lotteryOperate.IsLotterySwitchOpen(cityId, vaIndexInfo.shopId);

                if (lotterySwitch)//抽奖开关打开
                {
                    List<ShopAward> shopAwards = shopAwardOperate.SelectShopAwardList(vaIndexInfo.shopId);

                    if (shopAwards != null && shopAwards.Any())
                    {
                        foreach (ShopAward award in shopAwards)
                        {
                            if (avoidQueue && award.AwardType == AwardType.AvoidQueue)
                            {
                                signList.Add(Common.GetEnumDescription(AwardType.AvoidQueue));
                            }
                            if (award.AwardType == AwardType.PresentDish)
                            {
                                if (!signList.Contains(Common.GetEnumDescription(AwardType.PresentDish)))
                                {
                                    signList.Add(Common.GetEnumDescription(AwardType.PresentDish));
                                }
                            }
                        }
                    }
                }


                ShopVIPOperate shopVIPOperate = new ShopVIPOperate();
                double shopDiscount = shopVIPOperate.GetShopVipDiscount(vaIndexInfo.shopId);

                if (shopDiscount >= 1 && shopDiscount < 10)//有折扣显示折扣
                {
                    signList.Add(shopDiscount.ToString() + "折");
                }
                if (!string.IsNullOrEmpty(vaIndexInfo.couponContent))//有券则展示
                {
                    signList.Add(vaIndexInfo.couponContent);
                }

                if (signList.Count >= 3)
                {
                    signList.Remove(Common.GetEnumDescription(AwardType.PresentDish));
                }



                vaIndexInfo.signList = signList.ToArray();

                //-------------------------------------------------------------------------------------------------

                list.Add(vaIndexInfo);
            }
            return list;
        }

        /// <summary>
        /// 获取用户在某个店铺的折扣信息已经该店铺所有折扣信息20140313
        /// </summary>
        /// <param name="currentVipGride"></param>
        /// <param name="dtShopVipInfo"></param>
        /// <param name="shopVipList"></param>
        /// <param name="userVipInfo"></param>
        public static void GetUserVipInfo(int currentVipGride, DataTable dtShopVipInfo, VAShopVipInfo userVipInfo, List<VAShopVipInfo> shopVipList = null)
        {
            bool vipNullFlag = false;//用户Vip等级赋值标志位
            double maxDiscount = 1;
            string maxDiscountName = "";
            int maxDiscountPlatforVipId = 0;
            userVipInfo.discount = maxDiscount;//折扣
            userVipInfo.name = maxDiscountName;//名称
            userVipInfo.platformVipId = maxDiscountPlatforVipId;//VIP等级
            for (int i = 0; i < dtShopVipInfo.Rows.Count; i++)
            {
                double currentVipDiscount = Common.ToDouble(dtShopVipInfo.Rows[i]["discount"]);
                if (Common.ToInt32(dtShopVipInfo.Rows[i]["platformVipId"]) == currentVipGride && currentVipGride > 0)
                {
                    userVipInfo.discount = currentVipDiscount;//折扣
                    userVipInfo.name = Common.ToString(dtShopVipInfo.Rows[i]["name"]);//名称
                    userVipInfo.platformVipId = Common.ToInt32(dtShopVipInfo.Rows[i]["platformVipId"]);//VIP等级
                    vipNullFlag = true;
                }
                else
                {
                    if (currentVipDiscount <= maxDiscount && currentVipDiscount > 0)
                    {
                        maxDiscount = currentVipDiscount;
                        maxDiscountName = Common.ToString(dtShopVipInfo.Rows[i]["name"]);
                        maxDiscountPlatforVipId = Common.ToInt32(dtShopVipInfo.Rows[i]["platformVipId"]);
                    }
                    if (i == dtShopVipInfo.Rows.Count - 1)
                    {//如果用户是有平台vip等级但是却未匹配到店铺的相应设置，则取该店铺折扣最高的等级
                        if (currentVipGride > 0 && !vipNullFlag)
                        {
                            userVipInfo.discount = maxDiscount;//折扣
                            userVipInfo.name = maxDiscountName;//名称
                            userVipInfo.platformVipId = maxDiscountPlatforVipId;//VIP等级
                        }
                    }
                }
                if (shopVipList != null)
                {
                    VAShopVipInfo shopVipInfo = new VAShopVipInfo();
                    shopVipInfo.discount = Common.ToDouble(dtShopVipInfo.Rows[i]["discount"]);//折扣
                    shopVipInfo.name = Common.ToString(dtShopVipInfo.Rows[i]["name"]);//名称
                    shopVipInfo.platformVipId = Common.ToInt32(dtShopVipInfo.Rows[i]["platformVipId"]);//VIP等级
                    shopVipList.Add(shopVipInfo);
                }
            }
        }

        /// <summary>
        /// 悠先点菜客户端处理用户图片
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="personalImgInfo"></param>
        public static void ClientModifyPersonalImg(DataRow dataRow, string personalImgInfo)
        {
            byte[] bytes = Convert.FromBase64String(personalImgInfo);
            if (bytes.Length > 0)
            {
                string registerDatePath = "";
                if (String.IsNullOrEmpty(dataRow["registerDate"].ToString()))
                {
                    registerDatePath = "201408";
                }
                else
                {
                    registerDatePath = Common.ToDateTime(dataRow["registerDate"]).ToString("yyyyMM");
                }
                string path = "customer/" + registerDatePath + "/";
                long customerId = Common.ToInt64(dataRow["CustomerID"]);
                string oldPicturePath = path + Common.ToString(dataRow["Picture"]);
                using (var lifeScope = Bootstrapper.Container.BeginLifetimeScope(WebLifetime.Request))
                {
                    ICustomerInfoRepository customerInfoRepository = lifeScope.Resolve<ICustomerInfoRepository>();
                    string imageName = Guid.NewGuid().ToString("N") + ".jpg";
                    string imagePath = WebConfig.ImagePath + path + imageName;
                    using (var stream = new MemoryStream(bytes))
                    {
                        CloudStorageOperate.PutObject(imagePath, stream);
                        CloudStorageOperate.DeleteObject(WebConfig.ImagePath + oldPicturePath);//删除老图片
                    }
                    customerInfoRepository.UpdateCustomerPicture(customerId, imageName);
                }
            }
        }

        /// <summary>
        /// 查询点单已分享信息
        /// </summary>
        /// <param name="preorderId"></param>
        /// <returns></returns>
        public static List<int> GetShareFoodDiary(long preorderId)
        {
            List<int> listShared = new List<int>();
            var foodDiaryService = ServiceFactory.Resolve<IFoodDiaryService>();
            FoodDiary model = foodDiaryService.GetFoodDiaryByOrder(preorderId);
            if (model != null)
            {
                //当前类中有一个FoodDiaryShared方法，所以需要补全枚举全路径
                VAGastronomistMobileApp.Model.FoodDiaryShared fds = model.Shared;
                if ((VAGastronomistMobileApp.Model.FoodDiaryShared.新浪微博 & fds) != VAGastronomistMobileApp.Model.FoodDiaryShared.没有分享)
                {
                    listShared.Add((int)VAGastronomistMobileApp.Model.FoodDiaryShared.新浪微博);
                }
                if ((VAGastronomistMobileApp.Model.FoodDiaryShared.QQ空间 & fds) != VAGastronomistMobileApp.Model.FoodDiaryShared.没有分享)
                {
                    listShared.Add((int)VAGastronomistMobileApp.Model.FoodDiaryShared.QQ空间);
                }
                if ((VAGastronomistMobileApp.Model.FoodDiaryShared.微信朋友圈 & fds) != VAGastronomistMobileApp.Model.FoodDiaryShared.没有分享)
                {
                    listShared.Add((int)VAGastronomistMobileApp.Model.FoodDiaryShared.微信朋友圈);
                }
                if ((VAGastronomistMobileApp.Model.FoodDiaryShared.微信好友 & fds) != VAGastronomistMobileApp.Model.FoodDiaryShared.没有分享)
                {
                    listShared.Add((int)VAGastronomistMobileApp.Model.FoodDiaryShared.微信好友);
                }
                if ((VAGastronomistMobileApp.Model.FoodDiaryShared.QQ好友 & fds) != VAGastronomistMobileApp.Model.FoodDiaryShared.没有分享)
                {
                    listShared.Add((int)VAGastronomistMobileApp.Model.FoodDiaryShared.QQ好友);
                }
            }
            else
            {
                listShared.Add(0);
            }
            return listShared;
        }

        /// <summary>
        /// 悠先点菜举报信息获取和举报操作
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public VAClientShopReportResponse DoClientCustomerReportShop(VAClientShopReportRequest request)
        {
            VAClientShopReportResponse response = new VAClientShopReportResponse()
            {
                type = VAMessageType.CLIENT_SHOP_REPORT_RESPONSE,
                cookie = request.cookie,
                uuid = request.uuid
            };
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(request.cookie, request.uuid, (int)request.type, (int)VAMessageType.CLIENT_SHOP_REPORT_REQUEST);
            var reportList = new List<ShopReportInfo>();
            if (checkResult.result == VAResult.VA_OK)
            {
                long customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);//当前用户编号
                if (request.requestType == 1)//查询列表
                {
                    var list = CommonPageOperate.EnumToList(typeof(ShopReportEnum));//反射获取枚举Value和Description
                    if (list.Any())
                    {
                        reportList = (from q in list
                                      select new ShopReportInfo()
                                      {
                                          ReportDesc = q.Text,
                                          ReportValue = Common.ToInt32(q.Value)
                                      }).ToList();
                    }
                    response.result = VAResult.VA_OK;
                }
                else//处理举报操作，但步操作数据库，没有必要开启事务
                {
                    bool result = new ClientExtendManager().InsertShopReport(new ShopReport()
                    {
                        ShopReportId = 0,
                        CustomId = customerId,
                        ReportTime = DateTime.Now,
                        ShopId = request.shopId,
                        ReportValue = request.reportValue
                    }) > 0;
                    if (result == true)
                    {
                        response.result = VAResult.VA_OK;
                    }
                    else
                    {
                        response.result = VAResult.VA_FAILED_DB_ERROR;
                    }
                }
            }
            else
            {
                response.result = checkResult.result;
            }
            response.reportList = reportList;
            return response;
        }
    }
}
