﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Data;
using System.Configuration;
using System.Transactions;
using VAGastronomistMobileApp.DBUtility;
using System.Threading;
using VAGastronomistMobileApp.TheThirdPartyPaymentDll;
using System.Web;
using System.Xml;
using System.Data.SqlClient;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using VA.Cache.HttpRuntime;
using VA.CacheLogic.OrderClient;
using VA.CacheLogic;
using VA.Cache.Distributed;
using VAGastronomistMobileApp.Model.QueryObject;
using System.IO;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 201401版本客户端刷新首页，收藏和删除门店，搜索门店接口设计代码具体实现
    /// created by wangcheng
    /// </summary>
    public class ClientIndexListOperate
    {
        private readonly ClientExtendManager extendManager = new ClientExtendManager();
        /// <summary>
        /// 客户端获取首页列表信息
        /// </summary>
        /// <param name="clientIndexListRequest">城市编号</param>
        /// <returns>返回具体列表信息</returns>
        public ClientIndexListResponse ClientQueryIndexList(ClientIndexListRequest clientIndexListRequest)
        {
            ClientIndexListResponse clientIndexListResponse = new ClientIndexListResponse();
            clientIndexListResponse.type = VAMessageType.CLIENT_INDEX_LIST_RESPONSE;
            clientIndexListResponse.cookie = clientIndexListRequest.cookie;
            clientIndexListResponse.uuid = clientIndexListRequest.uuid;
            clientIndexListResponse.cityId = clientIndexListRequest.cityId;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(clientIndexListRequest.cookie, clientIndexListRequest.uuid, (int)clientIndexListRequest.type, (int)VAMessageType.CLIENT_INDEX_LIST_REQUEST);
            clientIndexListResponse.dataType = clientIndexListRequest.dataType;
            if (checkResult.result == VAResult.VA_OK)
            {
                string imgPath = WebConfig.CdnDomain + WebConfig.ImagePath;
                DataRow checkResult_dtCustomer = checkResult.dtCustomer.Rows[0];//用户信息
                clientIndexListResponse.result = VAResult.VA_OK;
                //获取客户端首页背景图片
                clientIndexListResponse.clientBgImage = "";
                string clientBgImage = "";
                if (!String.IsNullOrEmpty(clientBgImage))
                {
                    clientIndexListResponse.clientBgImage = imgPath + clientBgImage;
                }

                clientIndexListResponse.currentPlatformVipGrade = Common.ToInt32(checkResult_dtCustomer["currentPlatformVipGrade"]);//用户在平台的VIP等级   
                long customerId = Common.ToInt64(checkResult_dtCustomer["CustomerID"]);
                clientIndexListResponse.notEvaluatedCount = new PreOrderCacheLogic().GetCustomerPreorderCountForNotEvaluated(customerId);

                //分页软加载请求数据
                int dataCount = 0;
                clientIndexListResponse.indexList = ClientSearchShopListManager(clientIndexListRequest, customerId, clientIndexListResponse.currentPlatformVipGrade, ref dataCount, 0);
                clientIndexListResponse.isHaveMoreData = dataCount > clientIndexListRequest.pageIndex;

                List<VAIndexList> favourite = new List<VAIndexList>();
                List<VABrandBannerList> adList = new List<VABrandBannerList>();
                if (clientIndexListRequest.pageIndex == 1)//加载第一页
                {
                    clientIndexListResponse.favoritesIndexList = favourite;
                    DataTable dtCityCompanyBannerold = new BannerCacheLogic().GeBannerByCityId(clientIndexListRequest.cityId);
                    //获取滚动广告信息
                    adList = FillBrandBannerList(dtCityCompanyBannerold, imgPath);
                    clientIndexListResponse.vaAd = Common.GetRandomList<VABrandBannerList>(adList);
                    //更新广告滚动次数
                    string str = CommonPageOperate.SplicingAlphabeticStr(dtCityCompanyBannerold, "advertisementConnAdColumnId", false);
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        AdvertisementManager advertisementMan = new AdvertisementManager();
                        advertisementMan.UpdateAdvertisementDisplayCount(Common.ToString(str), 1);
                    }

                    int currentCityId = Common.ToInt32(checkResult_dtCustomer["currentCityId"]);
                    if (!clientIndexListRequest.cityId.Equals(currentCityId))
                    {
                        CustomerManager customerManager = new CustomerManager();
                        customerManager.UpdateCustomerCurrentCityId(customerId, clientIndexListRequest.cityId);
                    }
                }
                else
                {
                    clientIndexListResponse.favoritesIndexList = favourite;
                    clientIndexListResponse.vaAd = adList;
                }
            }
            else clientIndexListResponse.result = checkResult.result;
            #region 刷新首页列表记录 add by wangc 20140331
            PreOrder19dianOperate operate = new PreOrder19dianOperate();
            Thread thread = new Thread(operate.Excuit);
            thread.IsBackground = true;
            thread.Start(VAInvokedAPIType.API_REFRESH_COMPANY_LIST);
            #endregion
            return clientIndexListResponse;
        }

        /// <summary>
        /// 填充悠先点菜客户端首页广告信息
        /// </summary>
        /// <param name="dtBrandBanner"></param>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public List<VABrandBannerList> FillBrandBannerList(DataTable dtBrandBanner, string imagePath)
        {
            List<VABrandBannerList> brandList = new List<VABrandBannerList>();
            CompanyManager listshop = new CompanyManager();
            for (int i = 0; i < dtBrandBanner.Rows.Count; i++)
            {
                VABrandBannerList brand = new VABrandBannerList();
                brand.bannerImageUrlString = imagePath + Common.ToString(dtBrandBanner.Rows[i]["imageURL"]);
                brand.bannerType = Common.ToInt32(dtBrandBanner.Rows[i]["advertisementAreaId"]);
                brand.shopId = listshop.GetShopIdbyCompanyId(Common.ToInt64(dtBrandBanner.Rows[i]["advertisementId"]));
                brand.bannerName = Common.ToString(dtBrandBanner.Rows[i]["name"]);
                brand.bannerDescript = Common.ToString(dtBrandBanner.Rows[i]["advertisementDescription"]);
                brand.bannerUrl = Common.ToString(dtBrandBanner.Rows[i]["webAdvertisementUrl"]);//网页广告地址
                brandList.Add(brand);
            }
            return brandList;
        }
        /// <summary>
        /// 客户端收藏和删除收藏接口
        /// （保留原有接口，created by wangcheng）
        /// </summary>
        /// <param name="vaUserSetFavoriteShopRequest"></param>
        /// <returns></returns>
        public VAUserSetFavoriteShopResponse ClientSetFavoriteCompany(VAUserSetFavoriteShopRequest vaUserSetFavoriteShopRequest)
        {
            VAUserSetFavoriteShopResponse vaUserSetFavoriteShopResponse = new VAUserSetFavoriteShopResponse();
            vaUserSetFavoriteShopResponse.type = VAMessageType.USER_SETFAVORITESHOP_RESPONSE;
            vaUserSetFavoriteShopResponse.cookie = vaUserSetFavoriteShopRequest.cookie;
            vaUserSetFavoriteShopResponse.uuid = vaUserSetFavoriteShopRequest.uuid;
            vaUserSetFavoriteShopResponse.shopId = vaUserSetFavoriteShopRequest.shopId;
            vaUserSetFavoriteShopResponse.isFavorite = vaUserSetFavoriteShopRequest.isFavorite;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(vaUserSetFavoriteShopRequest.cookie, vaUserSetFavoriteShopRequest.uuid, (int)vaUserSetFavoriteShopRequest.type, (int)VAMessageType.USER_SETFAVORITESHOP_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                CustomerManager customerMan = new CustomerManager();
                long customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);
                ShopManager shopMan = new ShopManager();
                DataTable dtCustomerFavoriteShop = shopMan.SelectCustomerFavoriteShop(customerId, vaUserSetFavoriteShopRequest.shopId);
                if (vaUserSetFavoriteShopRequest.isFavorite)
                {
                    if (dtCustomerFavoriteShop.Rows.Count == 0)
                    {
                        CustomerFavoriteCompany customerFavoriteCompany = new CustomerFavoriteCompany();
                        customerFavoriteCompany.customerId = customerId;
                        customerFavoriteCompany.shopId = vaUserSetFavoriteShopRequest.shopId;
                        customerFavoriteCompany.companyId = 0;//此时收藏的是门店信息，此处设置companyId=0，待处理

                        if (customerMan.InsertCustomerFavoriteCompany(customerFavoriteCompany) > 0)//添加收藏
                        {
                            vaUserSetFavoriteShopResponse.result = VAResult.VA_OK;
                        }
                        else
                        {
                            vaUserSetFavoriteShopResponse.result = VAResult.VA_FAILED_DB_ERROR;
                        }
                    }
                    else
                    {
                        vaUserSetFavoriteShopResponse.result = VAResult.VA_FAILED_SHOP_COLLECTED;
                    }
                }
                else
                {
                    if (dtCustomerFavoriteShop.Rows.Count >= 1)
                    {
                        if (customerMan.DeleteCustomerFavoriteCompany(Common.ToInt64(dtCustomerFavoriteShop.Rows[0]["id"])))
                        {
                            vaUserSetFavoriteShopResponse.result = VAResult.VA_OK;
                        }
                        else
                        {
                            vaUserSetFavoriteShopResponse.result = VAResult.VA_FAILED_DB_ERROR;
                        }
                    }
                    else vaUserSetFavoriteShopResponse.result = VAResult.VA_FAILED_SHOP_NOT_COLLECTED;
                }
            }
            else
            {
                vaUserSetFavoriteShopResponse.result = checkResult.result;
            }
            return vaUserSetFavoriteShopResponse;
        }
        /// <summary>
        /// 201401客户端查询门店列表
        /// </summary>
        /// <param name="vaClientSearchShopListRequest"></param>
        /// <returns>返回门店基本信息列表信息，没有返回广告信息和背景图片信息</returns>
        public VAClientSearchShopListReponse ClientSearchShopList(VAClientSearchShopListRequest vaClientSearchShopListRequest)
        {
            VAClientSearchShopListReponse vaClientSearchShopListReponse = new VAClientSearchShopListReponse();
            vaClientSearchShopListReponse.type = VAMessageType.CLIENT_SEARCH_SHOP_LIST_RESPONSE;
            vaClientSearchShopListReponse.cookie = vaClientSearchShopListRequest.cookie;
            vaClientSearchShopListReponse.uuid = vaClientSearchShopListRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult =
            checkResult = Common.CheckCookieAndMsgtype(vaClientSearchShopListRequest.cookie, vaClientSearchShopListRequest.uuid, (int)vaClientSearchShopListRequest.type, (int)VAMessageType.CLIENT_SEARCH_SHOP_LIST_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                List<VAIndexList> mainList = new List<VAIndexList>();
                long customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);
                ClientIndexListRequest requestList = new ClientIndexListRequest()
                {
                    dataType = 0,
                    pageIndex = 1,
                    pageSize = vaClientSearchShopListRequest.pageSize,
                    cityId = vaClientSearchShopListRequest.cityId,
                    locateLongitude = -10000,
                    locateLatitude = -10000 //待处理
                };
                //此处默认的pageIndex设置为1，查询门店列表只需要返回当前第一页
                int dataCount = 0;
                mainList = ClientSearchShopListManager(requestList, customerId, Common.ToInt32(checkResult.dtCustomer.Rows[0]["currentPlatformVipGrade"]),
                    ref dataCount, vaClientSearchShopListRequest.shopId, vaClientSearchShopListRequest.searchKeyWord);
                if (mainList.Count > 0)
                {
                    vaClientSearchShopListReponse.indexList = mainList;
                    vaClientSearchShopListReponse.result = VAResult.VA_OK;
                }
                else vaClientSearchShopListReponse.result = VAResult.VA_FAILED_CLIENT_SEARCH_NOT_FOUND;//返回表示当前无任何查询结果
            }
            else vaClientSearchShopListReponse.result = checkResult.result;
            return vaClientSearchShopListReponse;
        }
        /// <summary>
        /// 调用DB获取数据
        /// </summary>
        /// <param name="clientIndexListRequest"></param>
        /// <param name="customerId"></param>
        /// <param name="currentVipGride"></param>
        /// <param name="dataCount"></param>
        /// <param name="shopId"></param>
        /// <param name="searchKeyWord"></param>
        /// <returns></returns>
        public List<VAIndexList> ClientSearchShopListManager(ClientIndexListRequest clientIndexListRequest, long customerId, int currentVipGride, ref int dataCount, int shopId, string searchKeyWord = "")
        {
            int cityId = clientIndexListRequest.cityId;
            int pageSize = clientIndexListRequest.pageSize;
            int pageIndex = clientIndexListRequest.pageIndex;
            double userLongtitude = clientIndexListRequest.locateLongitude;//经度
            double userLatitude = clientIndexListRequest.locateLatitude;//纬度
            int dataType = clientIndexListRequest.dataType;
            if (dataType != (int)VAIndexSorting.我吃过的 && dataType != (int)VAIndexSorting.关注的店
                && dataType != (int)VAIndexSorting.我看过的 && dataType != (int)VAIndexSorting.有券的店)
            {
                //中国经度范围：73°33′E至135°05′E 纬度范围：3°51′N至53°33′N
                if (userLongtitude < 70 || userLongtitude > 140 || userLatitude < 3 || userLatitude > 55)//经纬度有误
                {
                    //按照销量排序
                    dataType = 3;
                }
                else
                {
                    //定位排序
                    dataType = 2;
                }
            }
            searchKeyWord = string.IsNullOrEmpty(searchKeyWord) ? searchKeyWord = "" : searchKeyWord.Replace("'", "");
            List<VAIndexList> list = new List<VAIndexList>();
            List<VAIndexExt> dbDataList = new ClientIndexCacheLogic().TransformCacheData(cityId, userLongtitude, userLatitude, shopId,
            pageIndex, pageSize, customerId, dataType, clientIndexListRequest.tagId, ref dataCount, searchKeyWord.Trim());
            CustomerManager customerManager = new CustomerManager();
            List<int> favorateShop = customerManager.SelectCustomerFavoriteShop(customerId, cityId);
            return new ClientExtendOperate().GetClientIndexData(dbDataList, currentVipGride, list, cityId, Common.CheckLatestBuild_February(clientIndexListRequest.appType, clientIndexListRequest.clientBuild), favorateShop);
        }

        /// <summary>
        /// 客户端显示常用菜品列表
        /// </summary>
        /// <param name="vaCommonDishListRequest"></param>
        /// <returns></returns>
        public VACommonDishListReponse ClientCommonDishList(VACommonDishListRequest vaCommonDishListRequest)
        {
            VACommonDishListReponse vaCommonDishListReponse = new VACommonDishListReponse();
            vaCommonDishListReponse.type = VAMessageType.CLIENT_COMMON_DISHLIST_REPONSE;
            vaCommonDishListReponse.cookie = vaCommonDishListRequest.cookie;
            vaCommonDishListReponse.uuid = vaCommonDishListRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(vaCommonDishListRequest.cookie, vaCommonDishListRequest.uuid, (int)vaCommonDishListRequest.type, (int)VAMessageType.CLIENT_COMMON_DISHLIST_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {

                int shopId = vaCommonDishListRequest.shopId;
                List<int> commonDishIdList = new List<int>();
                DishManager DishM = new DishManager();
                DataTable dtDishID = DishM.GetCommonDishList(shopId);
                if (dtDishID.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtDishID.Rows)
                    {
                        commonDishIdList.Add(Common.ToInt32(dr[0]));
                    }
                    vaCommonDishListReponse.commonDishId = commonDishIdList;
                    vaCommonDishListReponse.result = VAResult.VA_OK;
                }
                else
                {
                    vaCommonDishListReponse.result = VAResult.VA_FAILED_COMMONDISH_NOT_FOUND;
                }
            }
            else
            {
                vaCommonDishListReponse.result = checkResult.result;
            }
            return vaCommonDishListReponse;
        }

        /// <summary>
        /// 客户端查询当前用户基本信息
        /// </summary>
        /// <param name="vaClientUserInrfoRequest"></param>
        /// <returns></returns>
        public static VAClientUserInfoReponse ClientQueryUserInfo(VAClientUserInfoRequest vaClientUserInrfoRequest)
        {
            VAClientUserInfoReponse vaClientUserInfoReponse = new VAClientUserInfoReponse();
            vaClientUserInfoReponse.type = VAMessageType.USER_INFO_QUERY_REPONSE;
            vaClientUserInfoReponse.cookie = vaClientUserInrfoRequest.cookie;
            vaClientUserInfoReponse.uuid = vaClientUserInrfoRequest.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(vaClientUserInrfoRequest.cookie, vaClientUserInrfoRequest.uuid, (int)vaClientUserInrfoRequest.type, (int)VAMessageType.USER_INFO_QUERY_REQUEST, false);
            List<VAPayMode> payModeList = new List<VAPayMode>();
            if (checkResult.result == VAResult.VA_OK)
            {
                DataRow checkResult_dtCustomer_Rows = checkResult.dtCustomer.Rows[0];
                //RedEnvelopeDetailOperate redEnvelopeOperate = new RedEnvelopeDetailOperate();
                //double executedRedEnvelopeAmount = 0;
                //bool redEnvelopeFlag = redEnvelopeOperate.DoExpirationRedEnvelopeLogic(Common.ToString(checkResult_dtCustomer_Rows["mobilePhoneNumber"]), ref  executedRedEnvelopeAmount);
                //if (!redEnvelopeFlag)
                //{
                //    vaClientUserInfoReponse.result = VAResult.VA_FAILED_DB_ERROR;
                //    return vaClientUserInfoReponse;
                //}

                double[] redEnvelope = new double[] { 0, 0 };
                RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
                if (!string.IsNullOrEmpty(Common.ToString(checkResult_dtCustomer_Rows["mobilePhoneNumber"])))
                {
                    redEnvelope = redEnvelopeOperate.QueryCustomerRedEnvelope(Common.ToString(checkResult_dtCustomer_Rows["mobilePhoneNumber"]));
                }

                double executedRedEnvelopeAmount = redEnvelope[0];
                double notExecutedRedEnvelopeAmount = redEnvelope[1];

                long customerId = Common.ToInt64(checkResult_dtCustomer_Rows["CustomerID"]);
                CustomerManager customerMan = new CustomerManager();
                DataTable dtCustomer = customerMan.SelectCustomerPartInfoAndVIPInfo(customerId);
                if (dtCustomer.Rows.Count == 1)
                {
                    vaClientUserInfoReponse.result = VAResult.VA_OK;
                    vaClientUserInfoReponse.servicePhone = SystemConfigManager.GetServicePhone();
                    vaClientUserInfoReponse.notEvaluatedCount = customerMan.SelectCustomerPreorderCountForNotEvaluated(customerId);
                    vaClientUserInfoReponse.cureentPlatformVipName = Common.ToString(dtCustomer.Rows[0]["name"]);
                    vaClientUserInfoReponse.currentPlatformVipGrade = Common.ToInt32(dtCustomer.Rows[0]["currentPlatformVipGrade"]);
                    vaClientUserInfoReponse.customerSex = Common.ToInt32(dtCustomer.Rows[0]["customerSex"]);
                    vaClientUserInfoReponse.mobilePhoneNumber = Common.ToString(dtCustomer.Rows[0]["mobilePhoneNumber"]);
                    vaClientUserInfoReponse.userName = Common.ToString(dtCustomer.Rows[0]["userName"]);
                    vaClientUserInfoReponse.money19dianRemained = Common.ToDouble(dtCustomer.Rows[0]["money19dianRemained"]);
                    vaClientUserInfoReponse.executedRedEnvelopeAmount = Common.ToDouble(executedRedEnvelopeAmount);
                    vaClientUserInfoReponse.notExecutedRedEnvelopeAmount = Common.ToDouble(notExecutedRedEnvelopeAmount);

                    //关于悠先的部分页面URL地址
                    vaClientUserInfoReponse.serviceUrl = WebConfig.ServiceUrl;//悠先服务
                    vaClientUserInfoReponse.guideUrl = WebConfig.GuideUrl;//引导说明
                    vaClientUserInfoReponse.introductionUrl = WebConfig.IntroductionUrl;//功能介绍
                    vaClientUserInfoReponse.faqUrl = WebConfig.FaqUrl;//常见问题
                    vaClientUserInfoReponse.feedbackUrl = WebConfig.FeedbackUrl;//反馈


                    if (Common.CheckLatestBuild_August(vaClientUserInrfoRequest.appType, vaClientUserInrfoRequest.clientBuild))
                    {
                        if (checkResult_dtCustomer_Rows["personalImgInfo"] != null && checkResult_dtCustomer_Rows["personalImgInfo"].ToString().ToLower().IndexOf("http") == 0)
                            vaClientUserInfoReponse.personalImgInfo = Common.ToString(checkResult_dtCustomer_Rows["personalImgInfo"]);
                        else
                        {
                            string picture = Common.ToString(dtCustomer.Rows[0]["Picture"]);
                            DateTime registerDate = Common.ToDateTime(dtCustomer.Rows[0]["registerDate"]);
                            vaClientUserInfoReponse.personalImgInfo = string.IsNullOrEmpty(picture)
                                ? ""
                                : WebConfig.CdnDomain + WebConfig.ImagePath + WebConfig.CustomerPicturePath +
                                  registerDate.ToString("yyyyMM/") + picture;
                        }
                    }
                    else
                    {
                        vaClientUserInfoReponse.personalImgInfo = Common.ToString(checkResult_dtCustomer_Rows["personalImgInfo"]);
                    }
                    vaClientUserInfoReponse.vipImgUrl = WebConfig.CdnDomain + WebConfig.UploadFiles + WebConfig.VipImg + Common.ToString(dtCustomer.Rows[0]["vipImg"]);
                    vaClientUserInfoReponse.validPreorderCount = customerMan.SelectCustomerValidPreorderCount(customerId);
                    if (vaClientUserInfoReponse.mobilePhoneNumber.Equals("23588776637"))
                    {
                        vaClientUserInfoReponse.defaultPayment = Common.ToInt32(checkResult_dtCustomer_Rows["defaultPayment"]) == 1 ? 1 : 0;
                    }
                    else
                    {
                        vaClientUserInfoReponse.defaultPayment = Common.ToInt32(checkResult_dtCustomer_Rows["defaultPayment"]);//返回数据库存储默认支付方式
                    }

                    vaClientUserInfoReponse.customerRedEnvelopeDetailUrl = WebConfig.ServerDomain
                                                                      + "AppPages/RedEnvelope/list.aspx?mobilePhone=" + vaClientUserInfoReponse.mobilePhoneNumber;
                    vaClientUserInfoReponse.isShowRechargeActivities = SystemConfigOperate.ClientRechargeFeatureIsOpen();//客户端是否显示充值购粮票栏位
                    ClientRechargeOperate operate = new ClientRechargeOperate();
                    vaClientUserInfoReponse.isHaveEffectiveActivities = operate.ServerIsHaveEffectiveActivities(vaClientUserInfoReponse.isShowRechargeActivities);//是否有在有效时间范围内的活动，客户端闹钟是否显示小红点

                    if (vaClientUserInfoReponse.mobilePhoneNumber.Equals("23588776637"))
                    {
                        payModeList = (ClientIndexListOperate.GetServerPayModelList()).FindAll(p => p.payModeId == 1);
                    }
                    else
                    {
                        payModeList = ClientIndexListOperate.GetServerPayModelList();
                    }
                    List<RechargeActivitiesInfo> list = new List<RechargeActivitiesInfo>();
                    if (vaClientUserInfoReponse.isShowRechargeActivities)//客户端充值活动功能开启
                    {
                        DataTable dt = operate.ClientQueryRecharge();
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow item in dt.Rows)
                            {
                                RechargeActivitiesInfo model = new RechargeActivitiesInfo();
                                model.beginTime = Common.ToString(item["beginTime"]);
                                model.endTime = Common.ToString(item["endTime"]);
                                model.externalSold = Common.ToInt32(item["externalSold"]);
                                model.id = Common.ToInt32(item["id"]);
                                model.name = Common.ToString(item["name"]);
                                model.sequence = Common.ToInt32(item["sequence"]);
                                list.Add(model);
                            }
                        }
                    }
                    vaClientUserInfoReponse.recharge = list;
                    vaClientUserInfoReponse.haveCouponCount = string.IsNullOrWhiteSpace(vaClientUserInfoReponse.mobilePhoneNumber) ? 0 :
                        (Common.CheckLatestBuild_February(vaClientUserInrfoRequest.appType, vaClientUserInrfoRequest.clientBuild)
                        ? new CouponOperate().GetHadCouponCount(vaClientUserInfoReponse.mobilePhoneNumber) : 0);

                    int platCouponCount = new CouponCacheLogic().GetCouponCount();
                    //告知客户端平台是否有配置抵扣券
                    vaClientUserInfoReponse.haveCoupon = platCouponCount > 0 ? true : false;

                    CouponOperate couponOperate = new CouponOperate();
                    vaClientUserInfoReponse.haveUncheckCoupon = couponOperate.HaveUnCheckCoupon(vaClientUserInfoReponse.mobilePhoneNumber);

                    //积分入口
                    SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();
                    vaClientUserInfoReponse.integralEntrance = new ClientIntegralEntrance()
                    {
                        name = systemConfigCacheLogic.GetSystemConfig("ClientIntegralEntranceName", ""),
                        url = string.Format(systemConfigCacheLogic.GetSystemConfig("ClientIntegralEntranceURL", ""), customerId)
                    };
                }
                else
                {
                    //表示当前用户信息获取有误
                    vaClientUserInfoReponse.result = VAResult.VA_FAILED_DB_ERROR;
                }

            }
            else vaClientUserInfoReponse.result = checkResult.result;
            vaClientUserInfoReponse.payModeList = payModeList;

            if (vaClientUserInfoReponse.result == VAResult.VA_OK && string.IsNullOrEmpty(vaClientUserInfoReponse.userName) && !string.IsNullOrEmpty(vaClientUserInfoReponse.mobilePhoneNumber))
                vaClientUserInfoReponse.userName = new SystemConfigCacheLogic().GetSystemConfig("visitorDefaultName", "悠先吃货") + vaClientUserInfoReponse.mobilePhoneNumber.Substring(7);

            return vaClientUserInfoReponse;
        }
        /// <summary>
        /// 支付点单，短信提醒服务员 add by wangc 20140324
        /// </summary>
        static void SMSRemindEmployee(object payOrderSmsRemainModel)
        {
            try
            {
                PayOrderSMSRemaid model = (PayOrderSMSRemaid)payOrderSmsRemainModel;

                //一 、给年夜饭点单成功用户发送短信提醒 2015-6-26取消
                //var mealOrderIds = new MealOperate().GetMealOrderIds(model.customerId);//当前用户年夜饭的点单
                //if (mealOrderIds.Contains(model.preOrderId))
                //{
                //    var tuperModel = MealScheduleManager.GetDinnerTime(model.preOrderId);

                //    Common.SendMessageBySms(model.customerPhone, String.Format(WebConfig.MealOrderSmsReminderContent, model.shopName, tuperModel.Item1, tuperModel.Item2));
                //}

                //二、给服务员发送短信
                ShopManager shopMan = new ShopManager();
                DataTable dtEmployee = shopMan.SelectShopEmployeePhone(model.shopId);
                if (dtEmployee.Rows.Count > 0)
                {
                    string phoneStr = CommonPageOperate.SplicingAlphabeticStr(dtEmployee, "phone");
                    string message = ConfigurationManager.AppSettings["payOrderMessage"];
                    message = message.Replace("{0}", Common.ToString(dtEmployee.Rows[0]["shopName"]));
                    message = message.Replace("{1}", Common.ToString(model.amount));
                    phoneStr = phoneStr.TrimEnd(')').TrimStart('(');
                    Common.SendMessageBySms(phoneStr, message);
                }
                var preorder = new PreOrder19dianOperate().GetPreOrder19dianById(model.preOrderId);
                //三、给客户发推送
                UniPushInfo uniPushInfo = new UniPushInfo()
                {
                    customerPhone = model.customerPhone,
                    shopName = model.shopName,
                    preOrder19dianId = model.preOrderId,
                    pushMessage = VAPushMessage.支付成功,
                    clientBuild = model.clientBuild,
                    orderId = preorder.OrderId
                };
                CustomPushRecordOperate customPushRecordOperate = new CustomPushRecordOperate();
                customPushRecordOperate.UniPush(uniPushInfo);
            }
            catch
            {
                return;
            }
        }

        /*
         20140504 客户端增加充值功能，需要调整对应的点单支付和直接支付接口
         考虑老版本兼容问题，新接口实现方法重构 wangc
        */
        /// <summary>
        /// 客户端个人中心充值抢购粮票
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static ClientPersonCenterRechargeReponse ClientPersonCenterRecharge(ClientPersonCenterRechargeRequest request)
        {
            ClientPersonCenterRechargeReponse response = new ClientPersonCenterRechargeReponse();
            response.type = VAMessageType.CLIENT_PERSON_CENTER_RECHARGE_REPONSE;
            response.cookie = request.cookie;
            response.uuid = request.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(request.cookie, request.uuid, (int)request.type, (int)VAMessageType.CLIENT_PERSON_CENTER_RECHARGE_REQUEST);
            List<VAPayMode> payModeList = new List<VAPayMode>();//支付方式
            if (checkResult.result == VAResult.VA_OK)
            {
                if (SystemConfigOperate.ClientRechargeFeatureIsOpen())
                {
                    response.result = VAResult.VA_RECHARGE_ACTIVITY_CLOSED;//充值总开关关闭//当前充值活动已关闭
                    return response;
                }
                string mobilePhoneNumber = Common.ToString(checkResult.dtCustomer.Rows[0]["mobilePhoneNumber"]);//获取当前用户绑定的手机号码
                long customerId = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);//用户编号
                ClientRechargeOperate rechargeOper = new ClientRechargeOperate();
                DataTable dtRecharge = rechargeOper.QueryRecharge(request.rechargeActivityId);
                DataRow[] drRecharge = dtRecharge.Select("status=1");
                if (drRecharge.Length <= 0)
                {
                    response.result = VAResult.VA_RECHARGE_ACTIVITY_CLOSED;//当前充值活动已关闭
                    return response;
                }
                DateTime timeNow = Common.ToDateTime(DateTime.Now.ToString("HH:mm:ss"));//当前系统时间
                if (timeNow < Common.ToDateTime(drRecharge[0]["beginTime"]))
                {
                    response.result = VAResult.VA_RECHARGE_ACTIVITY_BEFORE_DATE;//当前充值活动未开始
                    return response;
                }
                if (timeNow > Common.ToDateTime(drRecharge[0]["endTime"]))
                {
                    response.result = VAResult.VA_RECHARGE_ACTIVITY_OUT_DATE;//当前充值活动已过期
                    return response;
                }
                double rechargeAmount = Common.ToDouble(drRecharge[0]["rechargeCondition"]);//充值金额
                double presentAmount = Common.ToDouble(drRecharge[0]["present"]);//赠送金额
                if (!String.IsNullOrEmpty(mobilePhoneNumber))
                {
                    bool flagUpdateRechargeSoldCount = false;
                    bool flagUpdateCustomerRecharge = false;
                    if (request.payMode > 0)//支付方式有效，保存到数据库
                    {
                        CustomerManager customerMan = new CustomerManager();
                        customerMan.UpdateCustomerDefaultPayment(request.payMode, request.cookie);//保存用户默认支付方式到数据库，每次调用支付接口都会保存，支付方式小于0直接排除，减轻数据库压力
                    }
                    using (TransactionScope scope = new TransactionScope())
                    {
                        if (request.boolDualPayment == false && request.rechargeOrderId <= 0)//第一次调用充值接口
                        {
                            #region 第一次调用充值接口
                            int shopId = 0;
                            if (Common.ToInt32(request.preOrder19dianId) <= 0)
                            {
                                request.preOrder19dianId = 0;
                            }
                            else
                            {
                                PreOrder19dianOperate orderOperate = new PreOrder19dianOperate();
                                DataTable dtOrder = orderOperate.QueryPreorder(request.preOrder19dianId);
                                shopId = Common.ToInt32(dtOrder.Rows[0]["shopId"]);
                            }
                            CustomerRechargeInfo info = new CustomerRechargeInfo()
                                           {
                                               customerId = customerId,
                                               payStatus = (int)VACustomerChargeOrderStatus.NOT_PAID,
                                               preOrder19dianId = request.preOrder19dianId,
                                               rechargeId = request.rechargeActivityId,
                                               shopId = shopId,
                                               rechargeTime = DateTime.Now,
                                               payMode = request.payMode,
                                               customerCookie = request.cookie,
                                               customerUUID = request.uuid,
                                               rechargeCondition = rechargeAmount,
                                               rechargePresent = presentAmount
                                           };
                            long rechargeOrderId = rechargeOper.Insert(info);//记录充值订单
                            if (rechargeOrderId <= 0)
                            {
                                response.result = VAResult.VA_FAILED_ADD_RECHARGE_ORDER;
                                return response;
                            }
                            #region 第三方支付
                            double totalFee = rechargeAmount;//充值金额
                            int payMode = 0;
                            if (request.payMode >= 0)
                            {
                                SystemConfigOperate _systemOper = new SystemConfigOperate();//BLL
                                bool payModeValid = _systemOper.CheckPayMode(request.payMode);
                                if (!payModeValid)//支付方式无效
                                {
                                    payModeList = GetServerPayModelList();
                                    request.result = VAResult.VA_PAYMODE_NULLITY;//支付方式无效
                                }
                                else
                                {
                                    #region 第三方URL
                                    payMode = request.payMode;
                                    switch (payMode)
                                    {
                                        default:
                                        case (int)VAClientPayMode.ALI_PAY_PLUGIN://支付宝客户端
                                            AlipayOperate alipayOpe = new AlipayOperate();
                                            AlipayOrderInfo alipayOrder = new AlipayOrderInfo();
                                            string callBackUrl = WebConfig.ServerDomain + Config.Call_back_url;
                                            alipayOrder.orderCreatTime = System.DateTime.Now;
                                            alipayOrder.orderStatus = VAAlipayOrderStatus.NOT_PAID;
                                            alipayOrder.totalFee = totalFee;
                                            string subject = Common.GetEnumDescription(VAPayOrderType.PAY_CHARGE);//支付饭票
                                            if (subject.Length > 128)
                                            {
                                                subject = subject.Substring(0, 128);
                                            }
                                            alipayOrder.subject = subject;
                                            alipayOrder.conn19dianOrderType = VAPayOrderType.PAY_CHARGE;
                                            alipayOrder.connId = rechargeOrderId;
                                            alipayOrder.customerId = customerId;
                                            long outTradeNo = alipayOpe.AddAlipayOrder(alipayOrder);
                                            if (outTradeNo > 0)
                                            {
                                                scope.Complete();
                                                if (payMode == (int)VAClientPayMode.ALI_PAY_PLUGIN)
                                                {
                                                    //组装待签名数据
                                                    string alipayOrderString = "partner=" + "\"" + Config.Partner + "\"";
                                                    alipayOrderString += "&";
                                                    alipayOrderString += "seller=" + "\"" + Config.Partner + "\"";
                                                    alipayOrderString += "&";
                                                    alipayOrderString += "out_trade_no=" + "\"" + outTradeNo + "\"";
                                                    alipayOrderString += "&";
                                                    alipayOrderString += "subject=" + "\"" + subject + "\"";
                                                    alipayOrderString += "&";
                                                    alipayOrderString += "body=" + "\"" + "购买粮票充值" + "\"";
                                                    alipayOrderString += "&";
                                                    alipayOrderString += "total_fee=" + "\"" + totalFee + "\"";
                                                    alipayOrderString += "&";
                                                    string notifyURL = WebConfig.ServerDomain + Config.QuickNotify_url;
                                                    alipayOrderString += "notify_url=" + "\"" + notifyURL + "\"";
                                                    string orderSign = RSAFromPkcs8.sign(alipayOrderString, Config.PrivateKey, Config.Input_charset_UTF8);
                                                    alipayOrderString += "&";
                                                    alipayOrderString += "sign=" + "\"" + HttpUtility.UrlEncode(orderSign, Encoding.GetEncoding(Config.Input_charset_UTF8)) + "\"";
                                                    alipayOrderString += "&";
                                                    alipayOrderString += "sign_type=\"RSA\"";
                                                    response.alipayOrder = HttpUtility.UrlEncode(alipayOrderString, Encoding.GetEncoding(Config.Input_charset_UTF8));
                                                    response.result = VAResult.VA_MEET_THE_CONDITIONS_RECHARGE;
                                                }
                                            }
                                            break;
                                        case (int)VAClientPayMode.UNION_PAY_PLUGIN://银联客户端支付
                                            UnionPayOperate operate = new UnionPayOperate();
                                            UnionPayInfo orderInfo = new UnionPayInfo();
                                            orderInfo.merchantOrderTime = System.DateTime.Now;
                                            orderInfo.orderStatus = VAAlipayOrderStatus.NOT_PAID;
                                            orderInfo.conn19dianOrderType = VAPayOrderType.PAY_CHARGE;
                                            orderInfo.merchantOrderAmt = totalFee;
                                            orderInfo.customerId = customerId;
                                            string subjectTwo = Common.GetEnumDescription(VAPayOrderType.PAY_CHARGE);
                                            if (subjectTwo.Length > 128)
                                            {
                                                subjectTwo = subjectTwo.Substring(0, 128);
                                            }
                                            orderInfo.merchantOrderDesc = subjectTwo;
                                            orderInfo.connId = rechargeOrderId;
                                            long payOrderId = operate.AddUnionpayOrder(orderInfo);
                                            if (payOrderId > 0)
                                            {
                                                scope.Complete();
                                                try
                                                {
                                                    ////订单号,  从数据库中获取@@identity 自增长列
                                                    orderInfo.merchantOrderId = UnionPayParameters.UNION_PAY_FRONT_CODE + payOrderId;
                                                    StringBuilder originalInfo = new StringBuilder();
                                                    originalInfo
                                                        .Append("merchantName=").Append(UnionPayParameters.merchantName)
                                                        .Append("&merchantId=").Append(UnionPayParameters.merchantId)
                                                        .Append("&merchantOrderId=").Append(orderInfo.merchantOrderId)
                                                        .Append("&merchantOrderTime=").Append(orderInfo.merchantOrderTime.ToString("yyyyMMddHHmmss"))
                                                        .Append("&merchantOrderAmt=").Append(orderInfo.merchantOrderAmt * 100)
                                                        .Append("&merchantOrderDesc=").Append(orderInfo.merchantOrderDesc)
                                                        .Append("&transTimeout=").Append(UnionPayParameters.transTimeout);
                                                    string originalsign = originalInfo.ToString();
                                                    string xmlSign = UnionPaySignEncode.CreateSign(originalsign, UnionPayParameters.alias, UnionPayParameters.password, UnionPayParameters.PrivatePath);
                                                    PreOrder19dianOperate preOrderOper = new PreOrder19dianOperate();
                                                    string Submit = preOrderOper.SubmitString(orderInfo, xmlSign);
                                                    String returnContent = UnionPaySender.Send(UnionPayParameters.UNION_PAY_SERVER, Submit);
                                                    string decodeStr = HttpUtility.UrlDecode(returnContent);
                                                    XmlDocument xml = new XmlDocument();
                                                    xml.LoadXml(decodeStr);
                                                    string merchantId = xml.SelectSingleNode("/upomp/merchantId").InnerText;
                                                    string merchantOrderIdNew = xml.SelectSingleNode("/upomp/merchantOrderId").InnerText;
                                                    string merchantOrderTime = xml.SelectSingleNode("/upomp/merchantOrderTime").InnerText;
                                                    string resCode = xml.SelectSingleNode("/upomp/respCode").InnerText;
                                                    string resDesc = xml.SelectSingleNode("/upomp/respDesc").InnerText;
                                                    if (resCode == "0000")
                                                    {
                                                        StringBuilder builder = new StringBuilder();
                                                        builder
                                                            .Append("merchantId=").Append(merchantId)
                                                            .Append("&merchantOrderId=").Append(merchantOrderIdNew)
                                                            .Append("&merchantOrderTime=").Append(merchantOrderTime);
                                                        string threeElement = builder.ToString();
                                                        string threeElementSign = UnionPaySignEncode.CreateSign(threeElement, UnionPayParameters.alias, UnionPayParameters.password, UnionPayParameters.PrivatePath);
                                                        //生成xml格式字符串
                                                        String unionPayOrder = "<?xml version=" + "'1.0' "
                                                             + "encoding=" + "'utf-8' " + "standalone='yes'" + "?>" + "<upomp  application=" + "'LanchPay.Req' " + "version=" + "'1.0.0'" + ">"
                                                             + "<merchantId>"
                                                             + merchantId
                                                             + "</merchantId>"
                                                             + "<merchantOrderId>"
                                                             + merchantOrderIdNew
                                                             + "</merchantOrderId>"
                                                             + "<merchantOrderTime>"
                                                             + merchantOrderTime
                                                             + "</merchantOrderTime>"
                                                             + "<sign>"
                                                             + threeElementSign
                                                             + "</sign>" + "</upomp>";
                                                        response.unionpayOrder = unionPayOrder;
                                                        response.result = VAResult.VA_MEET_THE_CONDITIONS_RECHARGE;
                                                    }
                                                    else
                                                    {
                                                        response.result = VAResult.VA_FAILED_UNION_PAY_ORDER_SUMMIT_ERROR;
                                                    }
                                                }
                                                catch { }
                                            }
                                            else
                                            {
                                                response.result = VAResult.VA_FAILED_DB_ERROR;
                                            }
                                            break;
                                        case (int)VAClientPayMode.WECHAT_PAY_PLUGIN://微信支付
                                            //首次：获取AccessToken，保存预支付点单至DB，获取PrePayId并更新预支付点单，返回客户端数据
                                            //1.先获取AccessToken
                                            WechatAccessTokenOperate _accessTokenOperate = new WechatAccessTokenOperate();//BLL
                                            string access_token = _accessTokenOperate.CheckExistsAccessTokenIsExpire();
                                            if (!string.IsNullOrEmpty(access_token))//获取到AccessToken
                                            {
                                                //2再组装预支付订单，调用接口获取prepayId
                                                WechatPayOperate _wechatOperate = new WechatPayOperate();//BLL
                                                WechatPayOrderInfo wechatOrder = new WechatPayOrderInfo();//Model

                                                //获取预支付订单的Model
                                                wechatOrder = _wechatOperate.GetWechatPayOrderModel(customerId, rechargeOrderId, totalFee, VAPayOrderType.PAY_CHARGE, "");

                                                //2.1 先将预支付订单保存至DB
                                                long wechatOutTradeNo = _wechatOperate.AddWechatPayOrder(wechatOrder);
                                                if (wechatOutTradeNo > 0)
                                                {
                                                    response.result = VAResult.VA_MEET_THE_CONDITIONS_RECHARGE;

                                                    //2.2 向微信索取相应的prePayId，同时记录随即串和时间戳，用于调起微信支付
                                                    object[] objResult = _wechatOperate.CreatePrePay(access_token, wechatOutTradeNo.ToString(), (totalFee * 100).ToString(), VAPayOrderType.PAY_CHARGE, "");
                                                    string prePayId = objResult[0].ToString();
                                                    string noncestr = objResult[1].ToString();
                                                    string timestamp = objResult[2].ToString();
                                                    string errcode = objResult[3].ToString();

                                                    if (!string.IsNullOrEmpty(prePayId))//获取到prePayId                                                                            
                                                    {
                                                        //3.更新订单prePayId信息
                                                        bool updatePrePayId = _wechatOperate.UpdateWechatPrePayId(wechatOutTradeNo, prePayId);

                                                        if (updatePrePayId)
                                                        {
                                                            scope.Complete();
                                                            //4.最后返回给客户端调用支付接口所需信息
                                                            VAClientWechatPay weChatPay = _wechatOperate.GetClientWechatPay(prePayId, noncestr, timestamp);
                                                            response.ClientWechatPay = weChatPay;
                                                        }
                                                        else
                                                        {
                                                            response.result = VAResult.VA_FAILED_WECHATPAY_UPDATE_PREPAYID;//更新微信支付预点单的prepayId失败
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //如果没有获取到prepayId，则去检查首次获取AccessToken的返回结果，若是AccessToken已过期，则重新走一遍流程
                                                        //第二次：直接调接口获取AccessToken，获取PrePayId并更新预支付点单，返回客户端数据
                                                        if (errcode == "42001")//AccessToken过期，目前微信的返回信息：{"errcode":42001,"errmsg":"access_token expired"}
                                                        {
                                                            //重新获取
                                                            access_token = _accessTokenOperate.GetAccessToken();
                                                            if (!string.IsNullOrEmpty(access_token))
                                                            {
                                                                //获取PrePayId
                                                                object[] objResultAgain = _wechatOperate.CreatePrePay(access_token, wechatOutTradeNo.ToString(), (totalFee * 100).ToString(), VAPayOrderType.PAY_CHARGE, "");
                                                                prePayId = objResultAgain[0].ToString();
                                                                noncestr = objResultAgain[1].ToString();
                                                                timestamp = objResultAgain[2].ToString();

                                                                if (!string.IsNullOrEmpty(prePayId))//第二次成功获取到了prePayId                                                                            
                                                                {
                                                                    //3.更新订单prePayId信息
                                                                    bool updatePrePayId = _wechatOperate.UpdateWechatPrePayId(wechatOutTradeNo, prePayId);

                                                                    if (updatePrePayId)
                                                                    {
                                                                        scope.Complete();
                                                                        //4.最后返回给客户端调用支付接口所需信息
                                                                        VAClientWechatPay weChatPay = _wechatOperate.GetClientWechatPay(prePayId, noncestr, timestamp);
                                                                        response.ClientWechatPay = weChatPay;
                                                                        response.result = VAResult.VA_MEET_THE_CONDITIONS_RECHARGE;
                                                                    }
                                                                    else
                                                                    {
                                                                        response.result = VAResult.VA_FAILED_WECHATPAY_UPDATE_PREPAYID;//更新微信支付预点单的prepayId失败
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    response.result = VAResult.VA_FAILED_WECHATPAY_NOTFOUND_PREPAYID;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                response.result = VAResult.VA_FAILED_WECHATPAY_NOTFOUND_ACCESSTOKEN;//调用微信支付接口未获取到AccessToken
                                                            }
                                                        }
                                                        else
                                                        {
                                                            response.result = VAResult.VA_FAILED_WECHATPAY_NOTFOUND_PREPAYID;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    response.result = VAResult.VA_FAILED_DB_ERROR; //保存预点单信息出错
                                                }
                                            }
                                            else
                                            {
                                                response.result = VAResult.VA_FAILED_WECHATPAY_NOTFOUND_ACCESSTOKEN;//调用微信支付接口未获取到AccessToken
                                            }
                                            break;
                                    }
                                    #endregion
                                }
                            }
                            else
                            {
                                response.result = VAResult.VA_FAILED_OTHER;
                            }
                            #endregion
                            #endregion
                        }
                        else//二次支付，更新用户余额
                        {
                            CustomerManager customerMan = new CustomerManager();
                            flagUpdateRechargeSoldCount = rechargeOper.UpdateRechargeSoldCount(request.rechargeActivityId);
                            flagUpdateCustomerRecharge = rechargeOper.UpdateCustomerRecharge(request.rechargeOrderId, (int)VACustomerChargeOrderStatus.PAID, DateTime.Now, request.payMode);
                            if (flagUpdateRechargeSoldCount && flagUpdateCustomerRecharge)
                            {
                                scope.Complete();
                            }
                            else
                            {
                                response.result = VAResult.VA_FAILED_DB_ERROR;
                                return response;
                            }
                        }
                    }
                    #region 注释代码，回调支付接口
                    //以上充值操作完成
                    //以下为支付点单操作，客户端统一回调支付接口

                    //if (flagUpdateRechargeSoldCount && flagUpdateCustomerRecharge)//点单流水号
                    //{
                    //    using (StreamWriter file = new StreamWriter(@filePath, true))
                    //    {
                    //        file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + " 111");
                    //    }
                    //    DataTable dt = rechargeOper.QueryCustomerRecharge(request.rechargeOrderId);
                    //    long preOrderId = Common.ToInt64(dt.Rows[0]["preOrder19dianId"]);
                    //    PreOrder19dianOperate orderOperate = new PreOrder19dianOperate();
                    //    DataTable dtOrder = orderOperate.QueryPreorder(preOrderId);
                    //    if (dtOrder.Rows.Count == 1)
                    //    {
                    //        using (StreamWriter file = new StreamWriter(@filePath, true))
                    //        {
                    //            file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "222");
                    //        }
                    //        if (String.IsNullOrEmpty(dtOrder.Rows[0]["orderInJson"].ToString()) && String.IsNullOrEmpty(dtOrder.Rows[0]["sundryJson"].ToString()))
                    //        {
                    //            using (StreamWriter file = new StreamWriter(@filePath, true))
                    //            {
                    //                file.WriteLine(System.DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff") + "333");
                    //            }
                    //            #region 回调直接支付接口
                    //            ClientRechargeDirectPaymentRequest directPaymentRequest = new ClientRechargeDirectPaymentRequest()
                    //                                           {
                    //                                               preOrderId = preOrderId,
                    //                                               shopId = Common.ToInt32(dtOrder.Rows[0]["shopId"]),
                    //                                               preOrderPayMode = request.payMode,
                    //                                               boolDualPayment = true,//是否二次支付，服务器端使用，客户端不需要处理
                    //                                               cookie = request.cookie,
                    //                                               uuid = request.uuid,
                    //                                               type = VAMessageType.CLIENT_RECHARGE_DIRECT_PAYMENT_REQUEST,
                    //                                               payAmount = Common.ToDouble(dtOrder.Rows[0]["preOrderSum"]),
                    //                                               deskNumber = Common.ToString(dtOrder.Rows[0]["deskNumber"]),
                    //                                               rechargeActivityId = 0//不设置为0会进入死循环
                    //                                           };
                    //            ClientRechargeDirectPaymentResponse directPaymentResponse = ClientRechargeDirectPayment(directPaymentRequest);
                    //            response.result = directPaymentResponse.result;
                    //            #endregion
                    //        }
                    //        else
                    //        {
                    //            #region 回调点单支付接口
                    //            ClientRechargePaymentOrderRequest paymentOrderRequest = new ClientRechargePaymentOrderRequest()
                    //                                           {
                    //                                               preOrderId = preOrderId,
                    //                                               orderInJson = Common.ToString(dtOrder.Rows[0]["orderInJson"]),//选择菜品JSON
                    //                                               shopId = Common.ToInt32(dtOrder.Rows[0]["shopId"]),
                    //                                               isAddbyList = 1,
                    //                                               sundryList = JsonOperate.JsonDeserialize<List<VASundryInfo>>(Common.ToString(dtOrder.Rows[0]["sundryJson"])),
                    //                                               preOrderPayMode = request.payMode,
                    //                                               boolDualPayment = true,
                    //                                               cookie = request.cookie,
                    //                                               uuid = request.uuid,
                    //                                               type = VAMessageType.CLIENT_RECHARGE_PAYMENT_ORDER_REQUEST,
                    //                                               rechargeActivityId = 0//不设置为0会进入死循环
                    //                                           };
                    //            ClientRechargePaymentOrderResponse paymentOrderResponse = ClientRechargePaymentOrder(paymentOrderRequest);//调用支付接口 
                    //            response.result = paymentOrderResponse.result;
                    //            #endregion
                    //        }
                    //    }
                    //    else
                    //    {
                    //        response.result = VAResult.VA_OK;
                    //    }
                    //} 
                    #endregion
                }
                else
                {
                    //返回表示当前用户未绑定手机号码
                    response.result = VAResult.VA_FAILED_NOT_BINDING_MOBILE_PHONE_NUMBER;
                }
            }
            else
            {
                response.result = checkResult.result;
            }
            response.payModeList = payModeList;
            return response;
        }
        /// <summary>
        /// 客户端带充值功能支付接口add by wangc 20140504
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ClientRechargePaymentOrderResponse ClientRechargePaymentOrder(ClientRechargePaymentOrderRequest request)
        {
            ClientRechargePaymentOrderResponse response = new ClientRechargePaymentOrderResponse();
            response.type = VAMessageType.CLIENT_RECHARGE_PAYMENT_ORDER_RESPONSE;
            response.cookie = request.cookie;
            response.uuid = request.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(request.cookie, request.uuid, (int)request.type, (int)VAMessageType.CLIENT_RECHARGE_PAYMENT_ORDER_REQUEST, false);
            List<VAPayMode> payModeList = new List<VAPayMode>();//支付方式
            List<RechargeActivitiesInfo> list = new List<RechargeActivitiesInfo>();
            if (checkResult.result == VAResult.VA_OK)
            {
                DataRow drCustomer = checkResult.dtCustomer.Rows[0];
                long customerId = Common.ToInt64(drCustomer["CustomerID"]);
                string mobilePhoneNumber = Common.ToString(drCustomer["mobilePhoneNumber"]);
                int redPayType = (int)VAOrderUsedPayMode.REDENVELOPE;
                if (String.IsNullOrEmpty(mobilePhoneNumber))//校验手机号码
                {
                    response.result = VAResult.VA_FAILED_NOT_BINDING_MOBILE_PHONE_NUMBER;
                    return response;
                }

                //处理过期红包
                //RedEnvelopeDetailOperate redEnvelopeOperate = new RedEnvelopeDetailOperate();
                //double executedRedEnvelopeAmount = 0;
                //bool redEnvelopeFlag = redEnvelopeOperate.DoExpirationRedEnvelopeLogic(mobilePhoneNumber, ref  executedRedEnvelopeAmount);
                //if (!redEnvelopeFlag)
                //{
                //    response.result = VAResult.VA_FAILED_DB_ERROR;
                //    return response;
                //}

                //客户端传来了0元的点单
                if (request.clientUxianPriceSum <= 0 && request.boolDualPayment == false)
                {
                    response.message = "不允许0元点单";
                    response.result = VAResult.VA_NOT_ALLOW_ZERO_PAY;
                    return response;
                }


                int shopId = request.shopId;//门店编号
                DataTable dtPartShopInfo = new ShopInfoCacheLogic().GetPartShopInfoCache(shopId);
                var payOrderType = VAPayOrderType.PAY_PREORDER_AND_RECHARGE;
                if (dtPartShopInfo == null || dtPartShopInfo.Rows.Count <= 0)//校验门店信息
                {
                    response.result = VAResult.VA_FAILED_DB_ERROR;
                    return response;
                }
                if (Common.ToInt32(dtPartShopInfo.Rows[0]["isHandle"]) != (int)VAShopHandleStatus.SHOP_Pass)//校验门店是否上线
                {
                    response.result = VAResult.VA_FAILED_SHOP_NOT_ONLINE;
                    return response;
                }
                bool isSupportPayment = Common.ToBool(dtPartShopInfo.Rows[0]["isSupportPayment"]);
                if (isSupportPayment == false)//校验门店是否支持付款
                {
                    response.notPaymentReason = Common.ToString(dtPartShopInfo.Rows[0]["notPaymentReason"]);
                    response.result = VAResult.VA_SHOP_NOT_SUPPOORT_PAYMENY;
                    return response;
                }


                //------------------------------------------------------------------------------

                AwardConnPreOrderOperate awardConnPreOrderOperate = new AwardConnPreOrderOperate();
                bool checkCanPay = awardConnPreOrderOperate.CheckCusCanLotteryByUnConfirmedOrderCntOfVA(customerId, request.uuid);
                if (!checkCanPay)
                {
                    response.result = VAResult.VA_PAY_FAIL;
                    AwardCacheLogic awardCacheLogic = new AwardCacheLogic();
                    response.message = awardCacheLogic.GetAwardConfig("CannotPay", "");
                    return response;
                }

                //------------------------------------------------------------------------------


                PreOrder19dianManager preOrderMan = new PreOrder19dianManager();

                PreOrder19dianInfo preOrder19Dian = null;
                Order order = null;
                //粮票支付金额
                double rationBalancePayment = 0;

                response.rationBalance = Common.ToDouble(drCustomer["money19dianRemained"]);//用户粮票
                if (request.preOrderId > 0) //DB需要检测确定当前菜品价格是否和客户端一致
                {
                    DataTable oldPreOrderInfo = preOrderMan.SelectPartPreOrderInfo(request.preOrderId);
                    if (oldPreOrderInfo.Rows.Count <= 0)//校验订单信息是否存在
                    {
                        response.result = VAResult.VA_FAILED_DB_ERROR;
                        return response;
                    }
                    response.preOrderId = request.preOrderId;
                    response.orderId = new Guid(oldPreOrderInfo.Rows[0]["OrderId"].ToString());

                    if (Common.ToInt32(oldPreOrderInfo.Rows[0]["isPaid"]) == (int)VAPreorderIsPaid.PAID)//订单已支付
                    {
                        int preOrderInfoStatus = Common.ToInt32(oldPreOrderInfo.Rows[0]["status"]);
                        //已退款的点单则返回支付点单服务器端计算金额和客户端不一致,使客户端停留在点菜页面
                        if (preOrderInfoStatus == (int)VAPreorderStatus.Refund || preOrderInfoStatus == (int)VAPreorderStatus.OriginalRefunding)
                        {
                            response.result = VAResult.VA_FAILED_PREORDER_COUNT_SERVER_NOT_EQUAL_CLIENT;
                            response.message = @"红包虽好，每天只能抵用一单哦~改用支付宝/微信支付结账吧！(已支付金额系统将自动退回)";
                            response.preOrderId = 0;
                            if (Preorder19DianLineOperate.GetCountByQuery(new Preorder19DianLineQueryObject()
                            {
                                PayType = redPayType,
                                CustomerId = customerId,
                                IsRefoundOut = true
                            }) > 0 ||
                            Preorder19DianLineOperate.GetCountByQuery(new Preorder19DianLineQueryObject()
                            {
                                PayType = redPayType,
                                Uuid = request.uuid,
                                IsRefoundOut = true
                            }) > 0)
                            {
                                response.isUsedRedEnvelopeToday = true;
                            }
                            else
                            {
                                response.isUsedRedEnvelopeToday = false;
                            }
                            return response;
                        }
                        response.result = VAResult.VA_OK;
                        return response;
                    }
                    if (request.isAddbyList == 2)//列表传递，客户端未传递json信息
                    {
                        request.orderInJson = Common.ToString(oldPreOrderInfo.Rows[0]["orderInJson"]);
                        request.sundryList = JsonOperate.JsonDeserialize<List<VASundryInfo>>(Common.ToString(oldPreOrderInfo.Rows[0]["sundryJson"]));
                    }
                    if (request.boolDualPayment == false)//不处理第三方回调支付情况
                    {
                        if (!preOrderMan.UpdatePreorderAppInfo(response.preOrderId, (int)request.appType, request.clientBuild))//更新点单信息
                        {
                            response.result = VAResult.VA_FAILED_DB_ERROR;
                            return response;
                        }
                    }
                }
                string orderInJson = OverRideOrderJson(request.orderInJson, preOrderMan); //重组OrderJson 
                if (string.IsNullOrEmpty(orderInJson))
                {
                    response.message = "支付失败";
                    response.result = VAResult.VA_PAY_FAIL;
                    return response;
                }

                request.orderInJson = orderInJson;
                preOrder19Dian = new PreOrder19dianInfo();
                preOrder19Dian.orderInJson = request.orderInJson;
                preOrder19Dian.sundryJson = OverRideSundryList(request.sundryList);//重组SundryJson
                if (request.preOrderId <= 0) //新增点单
                {
                    #region PreOrder19dianInfo 基本信息持久化到DB
                    order = new Order()
                    {
                        Id = Guid.NewGuid(),
                        CustomerId = customerId,
                        ShopId = shopId,
                        PreOrderTime = DateTime.Now,
                        Status = (int)VAPreorderStatus.Uploaded,
                        PreOrderServerSum = request.clientCalculatedSum,
                        InvoiceTitle = string.Empty
                    };
                    if (OrderOperate.Add(order) == false)
                    {
                        response.result = VAResult.VA_FAILED_DB_ERROR;
                        return response;
                    }
                    preOrder19Dian.customerId = customerId;
                    preOrder19Dian.companyId = Common.ToInt32(dtPartShopInfo.Rows[0]["companyID"]);
                    preOrder19Dian.shopId = shopId;
                    preOrder19Dian.preOrderTime = DateTime.Now;
                    preOrder19Dian.menuId = Common.ToInt32(dtPartShopInfo.Rows[0]["MenuID"]);
                    preOrder19Dian.status = VAPreorderStatus.Uploaded;
                    preOrder19Dian.customerUUID = request.uuid;
                    preOrder19Dian.preOrderSum = request.clientCalculatedSum;
                    preOrder19Dian.deskNumber = string.Empty;
                    preOrder19Dian.appBuild = request.clientBuild;
                    preOrder19Dian.appType = (int)request.appType;
                    preOrder19Dian.OrderType = OrderTypeEnum.Normal;
                    preOrder19Dian.OrderId = order.Id;
                    response.preOrderId = new PreOrder19dianOperate().AddPreOrder19Dian(preOrder19Dian);//保存订单
                    #endregion
                }
                if (request.preOrderPayMode > 0 && request.preOrderPayMode != Common.ToInt32(drCustomer["defaultPayment"]))//保存为常用支付方式
                {
                    new CustomerManager().UpdateCustomerDefaultPayment(request.preOrderPayMode, request.cookie);
                }
                long preOrder19DianId = response.preOrderId;
                if (preOrder19DianId <= 0)
                {
                    response.result = VAResult.VA_FAILED_DB_ERROR;
                    return response;
                }
                response.serviceUrl = WebConfig.ServiceUrl;//悠先服务条款
                DataTable newPreOrderInfo = preOrderMan.SelectPreOrderAndCompanyByPayMent(preOrder19DianId);
                if (newPreOrderInfo.Rows.Count <= 0)
                {
                    response.result = VAResult.VA_FAILED_NOT_FOUND_ORDER;
                    return response;
                }
                string oldOrderInJson = Common.ToString(newPreOrderInfo.Rows[0]["orderInJson"]);//DB中最新点单信息
                string oldSundryJson = Common.ToString(newPreOrderInfo.Rows[0]["sundryJson"]);//DB中最新杂项信息
                bool flag = false;
                if (oldOrderInJson != request.orderInJson//有修改菜品操作，或者菜品价格发生变化
                    || oldSundryJson != JsonOperate.JsonSerializer<List<VASundryInfo>>(request.sundryList))//有修改杂项操作，或者杂项价格发生变化
                {
                    flag = true;
                }
                #region 计算菜品原价，折后价，杂项原价，折后价
                double currentDiscount = 1;
                ShopInfoCacheLogic shopInfoCacheLogic = new ShopInfoCacheLogic();
                DataTable dtShopVipInfo = shopInfoCacheLogic.GetShopVipInfo(shopId);//当前门店的vip等级信息
                if (dtShopVipInfo.Rows.Count > 0)
                {
                    int currentPlatformVipGrade = Common.ToInt32(drCustomer["currentPlatformVipGrade"]);//用户VA平台VIP等级
                    VAShopVipInfo currentUserShopVipInfo = new VAShopVipInfo();
                    ClientExtendOperate.GetUserVipInfo(currentPlatformVipGrade, dtShopVipInfo, currentUserShopVipInfo);
                    currentDiscount = currentUserShopVipInfo.discount;
                }
                List<SundryInfoResponse> sundryInfoList = new List<SundryInfoResponse>();
                PreOrder19dianOperate preOrderOper = new PreOrder19dianOperate();
                double originalPrice = preOrderOper.CalcPreorederOriginalPrice(request.orderInJson);//计算菜品的原价
                double deductionCoupon;//可以参与优惠券抵扣的金额
                double discountPrice = preOrderOper.CalcPreorederVIPPrice(request.orderInJson, currentDiscount, out deductionCoupon);//菜品折扣后价格
                double originalsundryPrice = preOrderOper.CalcSundryCount(originalPrice, request.sundryList, ref sundryInfoList, 0);//计算杂项原价
                double discountSundryPrice = Common.ToDouble(preOrderOper.CalcSundryCount(originalPrice, request.sundryList, ref sundryInfoList, currentDiscount));//杂项折扣后价格
                double serverCalculatedSum = originalPrice + originalsundryPrice;//整体折扣前价格（原价，小计）
                double serverUxianPriceSum = discountPrice + discountSundryPrice;//整体折扣后价格（悠先价）
                response.serverCalculatedSum = Common.ToDouble(serverCalculatedSum);//原价
                bool isAccountsRound = Common.ToBool(dtPartShopInfo.Rows[0]["isSupportAccountsRound"]);
                if (isAccountsRound)
                {
                    deductionCoupon = Common.ToDouble(CommonPageOperate.YouLuoRound(deductionCoupon, 0));
                    response.serverUxianPriceSum = Common.ToDouble(CommonPageOperate.YouLuoRound(serverUxianPriceSum, 0));
                }
                else
                {
                    deductionCoupon = Common.ToDouble(deductionCoupon);
                    response.serverUxianPriceSum = Common.ToDouble(serverUxianPriceSum);
                }
                #endregion
                //优惠券参与计算
                int usedCouponResult;//标记是否使用优惠券
                CouponOperate couponOperate = new CouponOperate();
                double realDeductibleAmount;
                //计算点单折扣后优惠券抵扣金额
                double afterDeductionCoupon =
                    couponOperate.GetCurrectPreOrderAfterDiscountAmount(request.couponId, response.serverUxianPriceSum, deductionCoupon,
                    request.appType, request.clientBuild, out usedCouponResult, out realDeductibleAmount);
                if (usedCouponResult == -2 || usedCouponResult == -3)
                {
                    response.result = VAResult.VA_FILED_FOUND_COUPON;//未找到指定优惠券，或优惠券不存在
                    response.couponDetails = couponOperate.GetShopCouponDetails(mobilePhoneNumber, shopId, request.appType, request.clientBuild);
                    return response;
                }

                bool priceChanges = true;//客户端计算还需支付是否正确标记
                bool isSupportRedEnvelopePayment = Common.ToBool(dtPartShopInfo.Rows[0]["isSupportRedEnvelopePayment"]);
                bool canRedEnvelopePay = isSupportRedEnvelopePayment;
                bool needThirdPartyPayment = true;//需要使用第三方支付
                double serverStillNeedPaySum = 0;

                RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
                double executedRedEnvelopeAmount = redEnvelopeOperate.QueryCustomerExcutedRedEnvelope(mobilePhoneNumber);

                if (canRedEnvelopePay == true)
                {
                    var preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                    {
                        CreateTimeFrom = DateTime.Today,
                        CustomerId = customerId,
                        PayType = redPayType,
                        IsRefoundOut = true
                    };
                    //今日使用过红包支付
                    if (Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject) > 0)
                    {
                        canRedEnvelopePay = false;
                        response.isUsedRedEnvelopeToday = true;
                        response.message = "红包虽好，每天只能抵用一单哦~改用支付宝/微信支付结账吧！(已支付金额系统将自动退回)";
                    }
                    if (canRedEnvelopePay)
                    {
                        preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                        {
                            CreateTimeFrom = DateTime.Today,
                            Uuid = request.uuid,
                            PayType = redPayType,
                            IsRefoundOut = true
                        };
                        if (Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject) > 0)
                        {
                            canRedEnvelopePay = false;
                            response.isUsedRedEnvelopeToday = true;
                            response.message = "红包虽好，每天只能抵用一单哦~改用支付宝/微信支付结账吧！(已支付金额系统将自动退回)";
                        }
                    }
                }

                if (canRedEnvelopePay)//红包可参与支付
                {
                    serverStillNeedPaySum = Common.ToDouble((afterDeductionCoupon - executedRedEnvelopeAmount - response.rationBalance)) > 0
                                                ? Common.ToDouble((afterDeductionCoupon - executedRedEnvelopeAmount - response.rationBalance)) : 0;
                }
                else
                {
                    serverStillNeedPaySum = Common.ToDouble((afterDeductionCoupon - response.rationBalance)) > 0
                                              ? Common.ToDouble((afterDeductionCoupon - response.rationBalance)) : 0;
                }

                response.serverStillNeedPaySum = serverStillNeedPaySum;//服务器计算还需支付
                priceChanges = serverStillNeedPaySum == Common.ToDouble(request.clientStillNeedPaySum) ? true : false;

                if (!(((Common.ToDouble(response.serverCalculatedSum) == Common.ToDouble(request.clientCalculatedSum)
                     && Common.ToDouble(response.serverUxianPriceSum) == Common.ToDouble(request.clientUxianPriceSum))
                     && priceChanges) || request.boolDualPayment == true))
                {
                    response.executedRedEnvelopeAmount = executedRedEnvelopeAmount;
                    response.result = VAResult.VA_FAILED_PREORDER_COUNT_SERVER_NOT_EQUAL_CLIENT;//客户端和服务端计算原价和悠先价不一致
                    return response;
                }

                DataView dvPreorder = newPreOrderInfo.DefaultView;
                preOrder19Dian = PreOrder19dianOperate.GetPreOrder19dianInfo(dvPreorder, response.serverUxianPriceSum, currentDiscount);
                order = OrderOperate.GetEntityById(preOrder19Dian.OrderId);
                if (order == null)
                {
                    response.result = VAResult.VA_FAILED_DB_ERROR;
                    return response;
                }


                using (TransactionScope paymentOrderScope = new TransactionScope())
                {
                    bool flagUpdateDb = true;
                    if (flag == true)//菜品信息被修改
                    {
                        flagUpdateDb = new PreOrder19dianOperate().ModifyPreOrderAndSundryJson(preOrder19DianId, request.orderInJson,
                               JsonOperate.JsonSerializer<List<VASundryInfo>>(request.sundryList), response.serverCalculatedSum);//修改当前菜品的订单，杂项，金额和支付金额信息
                    }
                    if (flagUpdateDb == true || request.boolDualPayment == true)
                    {
                        if (request.rechargeActivityId > 0)
                        {
                            #region 充值操作逻辑
                            ClientPersonCenterRechargeRequest rechargeRequest = new ClientPersonCenterRechargeRequest()
                                                                           {
                                                                               boolDualPayment = false,
                                                                               cityId = request.cityId,
                                                                               cookie = request.cookie,
                                                                               payMode = request.preOrderPayMode,
                                                                               rechargeActivityId = request.rechargeActivityId,
                                                                               rechargeOrderId = 0,
                                                                               uuid = request.uuid,
                                                                               type = VAMessageType.CLIENT_PERSON_CENTER_RECHARGE_REQUEST,
                                                                               preOrder19dianId = preOrder19DianId
                                                                           };
                            ClientPersonCenterRechargeReponse rechargeResponse = ClientPersonCenterRecharge(rechargeRequest);
                            //返回充值一系列url链接
                            response.result = rechargeResponse.result;
                            response.alipayOrder = rechargeResponse.alipayOrder;
                            response.ClientWechatPay = rechargeResponse.ClientWechatPay;
                            response.unionpayOrder = rechargeResponse.unionpayOrder;
                            response.urlToContinuePayment = rechargeResponse.urlToContinuePayment;
                            paymentOrderScope.Complete();
                            #endregion
                        }
                        else
                        {
                            #region if 粮票余额支付逻辑
                            //bool insertResult = false;
                            bool modifyRedEnvelope = false;
                            bool batchRedEnvelopeConnPreOrder = false;
                            DateTime expireTime = Convert.ToDateTime("1970-1-1");
                            if (canRedEnvelopePay)//门店支持使用红包
                            {
                                response.executedRedEnvelopeAmount = executedRedEnvelopeAmount;
                                needThirdPartyPayment = !(Common.ToDouble(response.rationBalance + executedRedEnvelopeAmount - afterDeductionCoupon) > -0.01);
                            }
                            else
                            {
                                response.executedRedEnvelopeAmount = 0;
                                needThirdPartyPayment = !(Common.ToDouble(response.rationBalance - afterDeductionCoupon) > -0.01);
                            }

                            //第三方支付金额为0,粮票余额为0,则支付金额+0.01
                            //第三方支付金额为0,粮票余额不为0,历史未使用第三方支付则支付金额+0.01
                            if (!request.boolDualPayment && canRedEnvelopePay == true && executedRedEnvelopeAmount > 0 && response.serverStillNeedPaySum == 0)
                            {
                                if (response.rationBalance > 0)
                                {
                                    var preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                                    {
                                        CustomerId = customerId,
                                        PayType = (int)VAOrderUsedPayMode.WECHAT
                                    };
                                    long weixinPayCount = Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject);
                                    long aliPayCount = 0;
                                    if (weixinPayCount == 0)
                                    {
                                        preorder19DianLineQueryObject.PayType = (int)VAOrderUsedPayMode.ALIPAY;
                                        aliPayCount = Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject);
                                        if (aliPayCount == 0)
                                        {
                                            response.serverStillNeedPaySum = 0.01;
                                            response.result = VAResult.VA_FAILED_SMS_MONEY_NOT_ENOUGH;
                                            response.message = @"红包虽好，每天只能抵用一单哦~改用支付宝/微信支付结账吧！(已支付金额系统将自动退回)";
                                            needThirdPartyPayment = true;
                                            priceChanges = true;
                                        }
                                    }
                                }
                                else
                                {
                                    response.serverStillNeedPaySum = 0.01;

                                    response.result = VAResult.VA_FAILED_SMS_MONEY_NOT_ENOUGH;
                                    response.message = @"红包虽好，每天只能抵用一单哦~改用支付宝/微信支付结账吧！(已支付金额系统将自动退回)";
                                    needThirdPartyPayment = true;
                                    priceChanges = true;
                                }
                                if (response.serverStillNeedPaySum > 0)
                                {
                                    preOrderMan.UpdateExtendPay(0.01, preOrder19DianId);
                                }
                            }

                            if (!needThirdPartyPayment)
                            {

                                double extendPay = PreOrder19dianOperate.GetExtendPayByPreOrder19DianId(request.preOrderId);
                                //红包支付业务逻辑
                                RedEnvelopeOperate redEnvelopeOper = new RedEnvelopeOperate();
                                redEnvelopeOper.DoRedEnvelopePaymentLogic(request.cookie, executedRedEnvelopeAmount, afterDeductionCoupon, mobilePhoneNumber,
preOrder19DianId, canRedEnvelopePay, extendPay, ref rationBalancePayment, ref modifyRedEnvelope, ref batchRedEnvelopeConnPreOrder, ref expireTime);
                                if (batchRedEnvelopeConnPreOrder && modifyRedEnvelope)
                                {
                                    bool result1 = true;
                                    if (rationBalancePayment > 0)
                                    {
                                        PreOrder19dianOperate.ChangeMoney(preOrder19DianId, customerId, 0, shopId, rationBalancePayment, ref result1);
                                    }
                                    //更新支付点单信息
                                    preOrder19Dian.preOrderServerSum = afterDeductionCoupon;
                                    preOrder19Dian.prePaidSum = afterDeductionCoupon;
                                    //preOrder19Dian.verifiedSaving = response.serverCalculatedSum - afterDeductionCoupon;
                                    preOrder19Dian.verifiedSaving = response.serverCalculatedSum - response.serverUxianPriceSum;

                                    bool expireRedEnvelope = Common.CheckExpireRedEnvelopeBuild_February(request.appType, request.clientBuild);
                                    if (expireRedEnvelope)//只有高于等于设置版本才设置点单过期时间
                                    {
                                        preOrder19Dian.expireTime = expireTime;
                                    }
                                    else//否则不设置
                                    {
                                        preOrder19Dian.expireTime = Convert.ToDateTime("1970-1-1");
                                    }


                                    order.IsPaid = preOrder19Dian.isPaid.Value;
                                    order.PrePaidSum = preOrder19Dian.prePaidSum.Value;
                                    order.VerifiedSaving = preOrder19Dian.verifiedSaving.Value;
                                    order.ExpireTime = preOrder19Dian.expireTime.Value;
                                    order.PrePayTime = preOrder19Dian.prePayTime.Value;
                                    if (preOrder19Dian.OrderType == OrderTypeEnum.Normal)
                                    {
                                        order.Status = (byte)preOrder19Dian.status;
                                    }
                                    if (OrderOperate.Update(order) == false)
                                    {
                                        response.result = VAResult.VA_FAILED_DB_ERROR;
                                        return response;
                                    }
                                    bool updatePreOrder = preOrderMan.UpdatePreOrder19dian(preOrder19Dian);
                                    bool result2 = true;
                                    if (usedCouponResult == 1)
                                    {
                                        result2 =
                                            couponOperate.AddPayOrderConnCoupon(dtPartShopInfo.Rows[0]["shopName"].ToString(),
                                                                                 request.couponId, preOrder19DianId, realDeductibleAmount, deductionCoupon);
                                    }
                                    if (result1 && updatePreOrder && result2)
                                    {
                                        paymentOrderScope.Complete();
                                        response.result = VAResult.VA_OK;

                                        #region 短信提醒门店服务员门店有支付点单
                                        PayOrderSMSRemaid model = new PayOrderSMSRemaid();
                                        model.shopId = shopId;
                                        model.shopName = dtPartShopInfo.Rows[0]["shopName"].ToString();
                                        model.amount = response.serverUxianPriceSum;
                                        model.preOrderId = preOrder19DianId;
                                        model.customerPhone = mobilePhoneNumber;
                                        model.customerId = customerId;
                                        model.clientBuild = request.clientBuild;
                                        Thread payOrderSMSRemaidThread = new Thread(SMSRemindEmployee);
                                        payOrderSMSRemaidThread.Start((object)model);

                                        #endregion
                                    }
                                    else
                                    {
                                        response.result = VAResult.VA_FAILED_DB_ERROR;
                                    }
                                }
                                else
                                {
                                    response.result = VAResult.VA_FAILED_DB_ERROR;
                                }
                            }
                            #endregion
                            #region else 第三方支付URL逻辑拼接
                            else
                            {
                                // 余额不足，支付宝客户端，银联客户端支付URL和签名组装
                                List<RechargeActivitiesInfo> rechargeActivityList = new List<RechargeActivitiesInfo>(); //GetServerRechargeActivitiesList();
                                response.rechargeActivityList = rechargeActivityList;
                                if (request.preOrderPayMode <= 0) //判断客户端选择支付方式
                                {
                                    response.result = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH;
                                    return response;
                                }
                                SystemConfigOperate systemOper = new SystemConfigOperate();
                                bool payModeValid = systemOper.CheckPayMode(request.preOrderPayMode);
                                if (!payModeValid)//支付方式无效
                                {
                                    SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();
                                    if (mobilePhoneNumber.Equals("23588776637"))
                                    {
                                        payModeList = ((List<VAPayMode>)systemConfigCacheLogic.GetServerPayModel()).FindAll(p => p.payModeId == 1);
                                    }
                                    else
                                    {
                                        payModeList = (List<VAPayMode>)systemConfigCacheLogic.GetServerPayModel();
                                    }
                                    response.result = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH_AND_NO_PREORDERPAYMODE;
                                }
                                else
                                {
                                    response.result = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH;
                                    if (usedCouponResult == 1) //需要记录优惠券
                                    {
                                        var dbResult = new CouponGetDetailManager().UpdateCouponState(request.couponId, preOrder19DianId, CouponUseStateType.inUse);
                                        if (dbResult == false)
                                        {
                                            response.result = VAResult.VA_FAILED_DB_ERROR;
                                        }
                                    }
                                    //double totalFee = canRedEnvelopePay == true
                                    //    ? Common.ToDouble(afterDeductionCoupon - response.rationBalance - executedRedEnvelopeAmount)
                                    //    : Common.ToDouble(afterDeductionCoupon - response.rationBalance);//实际支付=悠先价-余额
                                    double totalFee = response.serverStillNeedPaySum;
                                    int payMode = request.preOrderPayMode;
                                    ThirdPaymentSign thirdPaymentSignOper = new ThirdPaymentSign();
                                    dynamic dynamicResult = null;
                                    string shopName = dtPartShopInfo.Rows[0]["shopName"].ToString().Replace(" ", "");
                                    switch (payMode)
                                    {
                                        default:
                                        case (int)VAClientPayMode.ALI_PAY_PLUGIN://支付宝客户端
                                            dynamicResult = thirdPaymentSignOper.AliSignPackage(paymentOrderScope, totalFee, shopName, preOrder19DianId, customerId, payMode, payOrderType);
                                            response.alipayOrder = dynamicResult.value;
                                            response.result = dynamicResult.status;
                                            break;
                                        case (int)VAClientPayMode.UNION_PAY_PLUGIN://银联客户端支付
                                            dynamicResult = thirdPaymentSignOper.UnionSignPackage(preOrderOper, paymentOrderScope, totalFee, shopName, preOrder19DianId, customerId, payMode, payOrderType);
                                            response.unionpayOrder = dynamicResult.value;
                                            response.result = dynamicResult.status;
                                            break;
                                        case (int)VAClientPayMode.WECHAT_PAY_PLUGIN://微信支付
                                            dynamicResult = thirdPaymentSignOper.WechatSignPackage(paymentOrderScope, totalFee, shopName, preOrder19DianId, customerId, payMode, payOrderType);
                                            response.ClientWechatPay = dynamicResult.value;
                                            response.result = dynamicResult.status;
                                            break;
                                    }
                                }

                            }
                            #endregion
                        }
                    }
                    else
                    {
                        response.result = VAResult.VA_FAILED_DB_ERROR;//表示更新当前点单（菜品信息和杂项信息）信息失败
                    }
                }

                if (response.result == VAResult.VA_OK)
                {

                    RedEnvelopeManager redEnvelopeManager = new RedEnvelopeManager();
                    List<RedEnvelopeConnOrder3> connPreOrder = redEnvelopeManager.SelectRedEnvelopeConnPreOrder3(preOrder19Dian.preOrder19dianId);
                    double redEnvelopePayAmount = 0;
                    if (connPreOrder != null && connPreOrder.Count > 0)
                    {
                        redEnvelopePayAmount = connPreOrder.Sum(p => p.currectUsedAmount);
                    }
                    if (preOrder19Dian.isPaid == 1)
                    {
                        RecordPreorder19DianLine(request, customerId, preOrder19Dian, rationBalancePayment,
                                                realDeductibleAmount, canRedEnvelopePay, redEnvelopePayAmount);
                    }
                    if (canRedEnvelopePay && redEnvelopePayAmount > 0 && request.boolDualPayment && preOrder19Dian.isPaid == 1)
                    {
                        //当天已经用该支付ID付款过红包点单
                        if (!string.IsNullOrEmpty(request.thirdPayAccount))
                        {
                            var preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                            {
                                CreateTimeFrom = DateTime.Today,
                                PayType = (int)VAOrderUsedPayMode.REDENVELOPE,
                                PayAccount = request.thirdPayAccount,
                                IsRefoundOut = true
                            };
                            if (Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject) > 1)
                            {
                                response.message = "红包虽好，每天只能抵用一单哦~改用支付宝/微信支付结账吧！(已支付金额系统将自动退回)";
                                response.isUsedRedEnvelopeToday = true;
                                PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
                                var originalRefundRequest = new VAOriginalRefundRequest()
                                {
                                    preOrder19dianId = request.preOrderId,
                                    cookie = request.cookie,
                                    cityId = request.cityId,
                                    uuid = request.uuid,
                                    appType = request.appType,
                                    type = VAMessageType.CLIENT_ORIGINAL_REFUNF_REQUEST,
                                    clientBuild = request.clientBuild,
                                    employeeId = 29
                                };
                                var originalRefundResponse = preOrder19dianOperate.ClientOriginalRefund(originalRefundRequest);
                            }
                        }
                    }
                    ModifyPreorderAndDishCount(request.shopId, request.orderInJson);  //支付成功统计菜品销量
                }
            }
            else
            {
                response.result = checkResult.result;
            }
            response.payModeList = payModeList;
            return response;
        }

        /// <summary>
        /// 点菜支付
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static ClientRechargePaymentOrderResponse ClientPaymentOrder(ClientRechargePaymentOrderRequest request)
        {
            ClientRechargePaymentOrderResponse response = new ClientRechargePaymentOrderResponse();
            response.type = VAMessageType.CLIENT_RECHARGE_PAYMENT_ORDER_V1_RESPONSE;
            response.cookie = request.cookie;
            response.uuid = request.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(request.cookie, request.uuid, (int)request.type, (int)VAMessageType.CLIENT_RECHARGE_PAYMENT_ORDER_V1_REQUEST, false);
            List<VAPayMode> payModeList = new List<VAPayMode>();//支付方式
            List<RechargeActivitiesInfo> list = new List<RechargeActivitiesInfo>();
            if (checkResult.result == VAResult.VA_OK)
            {
                DataRow drCustomer = checkResult.dtCustomer.Rows[0];
                long customerId = Common.ToInt64(drCustomer["CustomerID"]);
                string mobilePhoneNumber = Common.ToString(drCustomer["mobilePhoneNumber"]);
                int redPayType = (int)VAOrderUsedPayMode.REDENVELOPE;
                if (String.IsNullOrEmpty(mobilePhoneNumber))//校验手机号码
                {
                    response.result = VAResult.VA_FAILED_NOT_BINDING_MOBILE_PHONE_NUMBER;
                    return response;
                }

                if (request.clientUxianPriceSum <= 0 && request.boolDualPayment == false)
                {
                    response.message = "不允许0元点单";
                    response.result = VAResult.VA_NOT_ALLOW_ZERO_PAY;
                    return response;
                }


                int shopId = request.shopId;//门店编号
                DataTable dtPartShopInfo = new ShopInfoCacheLogic().GetPartShopInfoCache(shopId);
                var payOrderType = VAPayOrderType.PAY_PREORDER_AND_RECHARGE;
                if (dtPartShopInfo == null || dtPartShopInfo.Rows.Count <= 0)//校验门店信息
                {
                    response.result = VAResult.VA_FAILED_DB_ERROR;
                    return response;
                }
                if (Common.ToInt32(dtPartShopInfo.Rows[0]["isHandle"]) != (int)VAShopHandleStatus.SHOP_Pass)//校验门店是否上线
                {
                    response.result = VAResult.VA_FAILED_SHOP_NOT_ONLINE;
                    return response;
                }
                bool isSupportPayment = Common.ToBool(dtPartShopInfo.Rows[0]["isSupportPayment"]);
                if (isSupportPayment == false)//校验门店是否支持付款
                {
                    response.notPaymentReason = Common.ToString(dtPartShopInfo.Rows[0]["notPaymentReason"]);
                    response.result = VAResult.VA_SHOP_NOT_SUPPOORT_PAYMENY;
                    return response;
                }


                //------------------------------------------------------------------------------

                AwardConnPreOrderOperate awardConnPreOrderOperate = new AwardConnPreOrderOperate();
                bool checkCanPay = awardConnPreOrderOperate.CheckCusCanLotteryByUnConfirmedOrderCntOfVA(customerId, request.uuid);
                if (!checkCanPay)
                {
                    response.result = VAResult.VA_PAY_FAIL;
                    AwardCacheLogic awardCacheLogic = new AwardCacheLogic();
                    response.message = awardCacheLogic.GetAwardConfig("CannotPay", "");
                    return response;
                }

                //------------------------------------------------------------------------------



                PreOrder19dianManager preOrderMan = new PreOrder19dianManager();

                PreOrder19dianInfo preOrder19Dian = null;
                //粮票支付金额
                double rationBalancePayment = 0;

                response.rationBalance = Common.ToDouble(drCustomer["money19dianRemained"]);//用户粮票
                if (request.preOrderId > 0) //DB需要检测确定当前菜品价格是否和客户端一致
                {
                    DataTable oldPreOrderInfo = preOrderMan.SelectPartPreOrderInfo(request.preOrderId);
                    if (oldPreOrderInfo.Rows.Count <= 0)//校验订单信息是否存在
                    {
                        response.result = VAResult.VA_FAILED_DB_ERROR;
                        return response;
                    }
                    response.preOrderId = request.preOrderId;
                    response.orderId = new Guid(oldPreOrderInfo.Rows[0]["OrderId"].ToString());

                    if (Common.ToInt32(oldPreOrderInfo.Rows[0]["isPaid"]) == (int)VAPreorderIsPaid.PAID)//订单已支付
                    {
                        int preOrderInfoStatus = Common.ToInt32(oldPreOrderInfo.Rows[0]["status"]);
                        //已退款的点单则返回支付点单服务器端计算金额和客户端不一致,使客户端停留在点菜页面
                        if (preOrderInfoStatus == (int)VAPreorderStatus.Refund || preOrderInfoStatus == (int)VAPreorderStatus.OriginalRefunding)
                        {
                            response.result = VAResult.VA_FAILED_PREORDER_COUNT_SERVER_NOT_EQUAL_CLIENT;
                            response.message = @"红包虽好，每天只能抵用一单哦~改用支付宝/微信支付结账吧！(已支付金额系统将自动退回)";
                            response.preOrderId = 0;
                            if (Preorder19DianLineOperate.GetCountByQuery(new Preorder19DianLineQueryObject()
                            {
                                PayType = redPayType,
                                CustomerId = customerId,
                                IsRefoundOut = true
                            }) > 0 ||
                            Preorder19DianLineOperate.GetCountByQuery(new Preorder19DianLineQueryObject()
                            {
                                PayType = redPayType,
                                Uuid = request.uuid,
                                IsRefoundOut = true
                            }) > 0)
                            {
                                response.isUsedRedEnvelopeToday = true;
                            }
                            else
                            {
                                response.isUsedRedEnvelopeToday = false;
                            }
                            return response;
                        }
                        response.result = VAResult.VA_OK;
                        return response;
                    }
                    if (request.isAddbyList == 2)//列表传递，客户端未传递json信息
                    {
                        request.orderInJson = Common.ToString(oldPreOrderInfo.Rows[0]["orderInJson"]);
                        request.sundryList = JsonOperate.JsonDeserialize<List<VASundryInfo>>(Common.ToString(oldPreOrderInfo.Rows[0]["sundryJson"]));
                    }
                    if (request.boolDualPayment == false)//不处理第三方回调支付情况
                    {
                        if (!preOrderMan.UpdatePreorderAppInfo(response.preOrderId, (int)request.appType, request.clientBuild))//更新点单信息
                        {
                            response.result = VAResult.VA_FAILED_DB_ERROR;
                            return response;
                        }
                    }
                }
                string orderInJson = OverRideOrderJson(request.orderInJson, preOrderMan); //重组OrderJson 
                if (string.IsNullOrEmpty(orderInJson))
                {
                    response.message = "支付失败";
                    response.result = VAResult.VA_PAY_FAIL;
                    return response;
                }

                request.orderInJson = orderInJson;
                preOrder19Dian = new PreOrder19dianInfo();
                preOrder19Dian.orderInJson = request.orderInJson;
                preOrder19Dian.sundryJson = OverRideSundryList(request.sundryList);//重组SundryJson
                Order order = null;
                if (request.preOrderId <= 0) //新增点单
                {
                    #region PreOrder19dianInfo 基本信息持久化到DB
                    order = new Order()
                    {
                        Id = Guid.NewGuid(),
                        CustomerId = customerId,
                        ShopId = shopId,
                        PreOrderTime = DateTime.Now,
                        Status = (int)VAPreorderStatus.Uploaded,
                        PreOrderServerSum = request.clientCalculatedSum,
                        InvoiceTitle = string.Empty,
                        IsPaid = 0,
                        IsShopConfirmed = 0,
                        IsEvaluation = 0,
                        RefundMoneyClosedSum = 0,
                        RefundMoneySum = 0,
                        PrePaidSum = 0,
                        PrePayTime = DateTime.Now,
                        ExpireTime = DateTime.MaxValue,
                        PayDifferenceSum = 0,
                        VerifiedSaving = 0
                    };
                    if (OrderOperate.Add(order) == false)
                    {
                        response.result = VAResult.VA_FAILED_DB_ERROR;
                        return response;
                    }
                    response.orderId = order.Id;
                    preOrder19Dian.customerId = customerId;
                    preOrder19Dian.companyId = Common.ToInt32(dtPartShopInfo.Rows[0]["companyID"]);
                    preOrder19Dian.shopId = shopId;
                    preOrder19Dian.preOrderTime = DateTime.Now;
                    preOrder19Dian.menuId = Common.ToInt32(dtPartShopInfo.Rows[0]["MenuID"]);
                    preOrder19Dian.status = VAPreorderStatus.Uploaded;
                    preOrder19Dian.customerUUID = request.uuid;
                    preOrder19Dian.preOrderSum = request.clientCalculatedSum;
                    preOrder19Dian.deskNumber = string.Empty;
                    preOrder19Dian.appBuild = request.clientBuild;
                    preOrder19Dian.appType = (int)request.appType;
                    preOrder19Dian.OrderType = OrderTypeEnum.Normal;
                    preOrder19Dian.OrderId = order.Id;
                    response.preOrderId = new PreOrder19dianOperate().AddPreOrder19Dian(preOrder19Dian);//保存订单
                    #endregion
                }
                if (request.preOrderPayMode > 0 && request.preOrderPayMode != Common.ToInt32(drCustomer["defaultPayment"]))//保存为常用支付方式
                {
                    new CustomerManager().UpdateCustomerDefaultPayment(request.preOrderPayMode, request.cookie);
                }
                long preOrder19DianId = response.preOrderId;
                if (preOrder19DianId <= 0)
                {
                    response.result = VAResult.VA_FAILED_DB_ERROR;
                    return response;
                }
                response.serviceUrl = WebConfig.ServiceUrl;//悠先服务条款
                DataTable newPreOrderInfo = preOrderMan.SelectPreOrderAndCompanyByPayMent(preOrder19DianId);
                if (newPreOrderInfo.Rows.Count <= 0)
                {
                    response.result = VAResult.VA_FAILED_NOT_FOUND_ORDER;
                    return response;
                }
                string oldOrderInJson = Common.ToString(newPreOrderInfo.Rows[0]["orderInJson"]);//DB中最新点单信息
                string oldSundryJson = Common.ToString(newPreOrderInfo.Rows[0]["sundryJson"]);//DB中最新杂项信息
                bool flag = false;
                if (oldOrderInJson != request.orderInJson//有修改菜品操作，或者菜品价格发生变化
                    || oldSundryJson != JsonOperate.JsonSerializer<List<VASundryInfo>>(request.sundryList))//有修改杂项操作，或者杂项价格发生变化
                {
                    flag = true;
                }
                #region 计算菜品原价，折后价，杂项原价，折后价
                double currentDiscount = 1;
                ShopInfoCacheLogic shopInfoCacheLogic = new ShopInfoCacheLogic();
                DataTable dtShopVipInfo = shopInfoCacheLogic.GetShopVipInfo(shopId);//当前门店的vip等级信息
                if (dtShopVipInfo.Rows.Count > 0)
                {
                    int currentPlatformVipGrade = Common.ToInt32(drCustomer["currentPlatformVipGrade"]);//用户VA平台VIP等级
                    VAShopVipInfo currentUserShopVipInfo = new VAShopVipInfo();
                    ClientExtendOperate.GetUserVipInfo(currentPlatformVipGrade, dtShopVipInfo, currentUserShopVipInfo);
                    currentDiscount = currentUserShopVipInfo.discount;
                }
                List<SundryInfoResponse> sundryInfoList = new List<SundryInfoResponse>();
                PreOrder19dianOperate preOrderOper = new PreOrder19dianOperate();
                double originalPrice = preOrderOper.CalcPreorederOriginalPrice(request.orderInJson);//计算菜品的原价
                double deductionCoupon;//可以参与优惠券抵扣的金额
                double discountPrice = preOrderOper.CalcPreorederVIPPrice(request.orderInJson, currentDiscount, out deductionCoupon);//菜品折扣后价格
                double originalsundryPrice = preOrderOper.CalcSundryCount(originalPrice, request.sundryList, ref sundryInfoList, 0);//计算杂项原价
                double discountSundryPrice = Common.ToDouble(preOrderOper.CalcSundryCount(originalPrice, request.sundryList, ref sundryInfoList, currentDiscount));//杂项折扣后价格
                double serverCalculatedSum = originalPrice + originalsundryPrice;//整体折扣前价格（原价，小计）
                double serverUxianPriceSum = discountPrice + discountSundryPrice;//整体折扣后价格（悠先价）
                response.serverCalculatedSum = Common.ToDouble(serverCalculatedSum);//原价
                bool isAccountsRound = Common.ToBool(dtPartShopInfo.Rows[0]["isSupportAccountsRound"]);
                if (isAccountsRound)
                {
                    deductionCoupon = Common.ToDouble(CommonPageOperate.YouLuoRound(deductionCoupon, 0));
                    response.serverUxianPriceSum = Common.ToDouble(CommonPageOperate.YouLuoRound(serverUxianPriceSum, 0));
                }
                else
                {
                    deductionCoupon = Common.ToDouble(deductionCoupon);
                    response.serverUxianPriceSum = Common.ToDouble(serverUxianPriceSum);
                }
                #endregion
                //优惠券参与计算
                int usedCouponResult;//标记是否使用优惠券
                CouponOperate couponOperate = new CouponOperate();
                double realDeductibleAmount;
                //计算点单折扣后优惠券抵扣金额
                //double afterDeductionCoupon = CouponOperate.GetAmountAfterUseCoupon(request.couponId, response.serverUxianPriceSum, deductionCoupon,
                //                                out usedCouponResult, out realDeductibleAmount);

                int couponGetDetailIDNew = 0;
                double afterDeductionCoupon
                    = CouponOperate.GetAmountAfterUseCoupon(request.couponId, response.serverUxianPriceSum, deductionCoupon, request.couponType,
                    mobilePhoneNumber, preOrder19DianId, out usedCouponResult, out realDeductibleAmount, out couponGetDetailIDNew);

                // 店铺抵扣券没有可领用的
                if (usedCouponResult == -4)
                {
                    response.result = VAResult.VA_FILED_FOUND_COUPON;
                    response.message = "抱歉，抵扣券已被领用完。";
                    return response;
                }

                // 店铺抵扣券自动领取后， 更新couponID
                if (request.couponType == (int)CouponTypeEnum.Bussiness)
                {
                    request.couponId = couponGetDetailIDNew;
                }

                if (usedCouponResult == -2 || usedCouponResult == -3)
                {
                    response.result = VAResult.VA_FILED_FOUND_COUPON;//未找到指定优惠券，或优惠券不存在
                    response.couponDetails = couponOperate.GetShopCouponDetails(mobilePhoneNumber, shopId, request.appType, request.clientBuild);
                    return response;
                }

                bool priceChanges = true;//客户端计算还需支付是否正确标记 
                bool canRedEnvelopePay = Common.ToBool(dtPartShopInfo.Rows[0]["isSupportRedEnvelopePayment"]);
                bool needThirdPartyPayment = true;//需要使用第三方支付
                double serverStillNeedPaySum = 0;

                RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
                double executedRedEnvelopeAmount = redEnvelopeOperate.QueryCustomerExcutedRedEnvelope(mobilePhoneNumber);

                if (canRedEnvelopePay == true)
                {
                    if (CheckIsUsedRedEnvelopeToday(customerId, request.uuid))
                    {
                        canRedEnvelopePay = false;
                        response.message = "红包虽好，每天只能抵用一单哦~改用支付宝/微信支付结账吧！(已支付金额系统将自动退回)";
                        response.isUsedRedEnvelopeToday = true;
                    }
                }

                if (canRedEnvelopePay)//红包可参与支付
                {
                    serverStillNeedPaySum = Common.ToDouble((afterDeductionCoupon - executedRedEnvelopeAmount - response.rationBalance)) > 0
                                                ? Common.ToDouble((afterDeductionCoupon - executedRedEnvelopeAmount - response.rationBalance)) : 0;
                }
                else
                {
                    serverStillNeedPaySum = Common.ToDouble((afterDeductionCoupon - response.rationBalance)) > 0
                                              ? Common.ToDouble((afterDeductionCoupon - response.rationBalance)) : 0;
                }

                response.serverStillNeedPaySum = serverStillNeedPaySum;//服务器计算还需支付
                priceChanges = serverStillNeedPaySum == Common.ToDouble(request.clientStillNeedPaySum) ? true : false;

                if (!(((Common.ToDouble(response.serverCalculatedSum) == Common.ToDouble(request.clientCalculatedSum)
                     && Common.ToDouble(response.serverUxianPriceSum) == Common.ToDouble(request.clientUxianPriceSum))
                     && priceChanges) || request.boolDualPayment == true))
                {
                    response.executedRedEnvelopeAmount = executedRedEnvelopeAmount;
                    response.result = VAResult.VA_FAILED_PREORDER_COUNT_SERVER_NOT_EQUAL_CLIENT;//客户端和服务端计算原价和悠先价不一致
                    return response;
                }

                DataView dvPreorder = newPreOrderInfo.DefaultView;
                preOrder19Dian = PreOrder19dianOperate.GetPreOrder19dianInfo(dvPreorder, response.serverUxianPriceSum, currentDiscount);

                order = OrderOperate.GetEntityById(preOrder19Dian.OrderId);
                if (order == null)
                {
                    response.result = VAResult.VA_FAILED_DB_ERROR;
                    return response;
                }
                response.orderId = order.Id;


                using (TransactionScope paymentOrderScope = new TransactionScope())
                {
                    bool flagUpdateDb = true;
                    if (flag == true)//菜品信息被修改
                    {
                        flagUpdateDb = new PreOrder19dianOperate().ModifyPreOrderAndSundryJson(preOrder19DianId, request.orderInJson,
                               JsonOperate.JsonSerializer<List<VASundryInfo>>(request.sundryList), response.serverCalculatedSum);//修改当前菜品的订单，杂项，金额和支付金额信息
                    }
                    if (flagUpdateDb == true)
                    {
                        #region if 粮票余额支付逻辑
                        //bool insertResult = false;
                        bool modifyRedEnvelope = false;
                        bool batchRedEnvelopeConnPreOrder = false;
                        DateTime expireTime = Convert.ToDateTime("1970-1-1");
                        if (canRedEnvelopePay)//门店支持使用红包
                        {
                            response.executedRedEnvelopeAmount = executedRedEnvelopeAmount;
                            needThirdPartyPayment = !(Common.ToDouble(response.rationBalance + executedRedEnvelopeAmount - afterDeductionCoupon) > -0.01);
                        }
                        else
                        {
                            response.executedRedEnvelopeAmount = 0;
                            needThirdPartyPayment = !(Common.ToDouble(response.rationBalance - afterDeductionCoupon) > -0.01);
                        }

                        //第三方支付金额为0,粮票余额为0,则支付金额+0.01
                        //第三方支付金额为0,粮票余额不为0,历史未使用第三方支付则支付金额+0.01
                        if (!request.boolDualPayment && canRedEnvelopePay == true && executedRedEnvelopeAmount > 0 && response.serverStillNeedPaySum == 0)
                        {
                            if (response.rationBalance > 0)
                            {
                                var preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                                {
                                    CustomerId = customerId,
                                    PayType = (int)VAOrderUsedPayMode.WECHAT
                                };
                                long weixinPayCount = Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject);
                                long aliPayCount = 0;
                                if (weixinPayCount == 0)
                                {
                                    preorder19DianLineQueryObject.PayType = (int)VAOrderUsedPayMode.ALIPAY;
                                    aliPayCount = Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject);
                                    if (aliPayCount == 0)
                                    {
                                        response.serverStillNeedPaySum = 0.01;
                                        response.result = VAResult.VA_FAILED_SMS_MONEY_NOT_ENOUGH;
                                        response.message = @"红包虽好，每天只能抵用一单哦~改用支付宝/微信支付结账吧！(已支付金额系统将自动退回)";
                                        needThirdPartyPayment = true;
                                        priceChanges = true;
                                    }
                                }
                            }
                            else
                            {
                                response.serverStillNeedPaySum = 0.01;
                                response.result = VAResult.VA_FAILED_SMS_MONEY_NOT_ENOUGH;
                                response.message = @"红包虽好，每天只能抵用一单哦~改用支付宝/微信支付结账吧！(已支付金额系统将自动退回)";
                                needThirdPartyPayment = true;
                                priceChanges = true;
                            }
                            if (response.serverStillNeedPaySum > 0)
                            {
                                preOrderMan.UpdateExtendPay(0.01, preOrder19DianId);
                            }
                        }

                        if (!needThirdPartyPayment)
                        {

                            double extendPay = PreOrder19dianOperate.GetExtendPayByPreOrder19DianId(request.preOrderId);
                            //红包支付业务逻辑
                            RedEnvelopeOperate redEnvelopeOper = new RedEnvelopeOperate();
                            redEnvelopeOper.DoRedEnvelopePaymentLogic(request.cookie, executedRedEnvelopeAmount, afterDeductionCoupon, mobilePhoneNumber,
preOrder19DianId, canRedEnvelopePay, extendPay, ref rationBalancePayment, ref modifyRedEnvelope, ref batchRedEnvelopeConnPreOrder, ref expireTime);
                            if (batchRedEnvelopeConnPreOrder && modifyRedEnvelope)
                            {
                                bool result1 = true;
                                if (rationBalancePayment > 0)
                                {
                                    PreOrder19dianOperate.ChangeMoney(preOrder19DianId, customerId, 0, shopId, rationBalancePayment, ref result1);
                                }
                                //更新支付点单信息
                                preOrder19Dian.preOrderServerSum = afterDeductionCoupon;
                                preOrder19Dian.prePaidSum = afterDeductionCoupon;
                                //preOrder19Dian.verifiedSaving = response.serverCalculatedSum - afterDeductionCoupon;
                                preOrder19Dian.verifiedSaving = response.serverCalculatedSum - response.serverUxianPriceSum;

                                bool expireRedEnvelope = Common.CheckExpireRedEnvelopeBuild_February(request.appType, request.clientBuild);
                                if (expireRedEnvelope)//只有高于等于设置版本才设置点单过期时间
                                {
                                    preOrder19Dian.expireTime = expireTime;
                                }
                                else//否则不设置
                                {
                                    preOrder19Dian.expireTime = Convert.ToDateTime("1970-1-1");
                                }
                                order.PreOrderServerSum
                                    = preOrder19Dian.preOrderServerSum.Value + realDeductibleAmount + preOrder19Dian.verifiedSaving.Value;
                                order.IsPaid = preOrder19Dian.isPaid.Value;
                                order.PrePaidSum = preOrder19Dian.prePaidSum.Value;
                                order.VerifiedSaving = preOrder19Dian.verifiedSaving.Value;
                                order.ExpireTime = preOrder19Dian.expireTime.Value;
                                order.PrePayTime = preOrder19Dian.prePayTime.Value;

                                if (preOrder19Dian.OrderType == OrderTypeEnum.Normal)
                                {
                                    order.Status = (byte)preOrder19Dian.status;
                                }
                                if (OrderOperate.Update(order) == false)
                                {
                                    response.result = VAResult.VA_FAILED_DB_ERROR;
                                    return response;
                                }
                                bool updatePreOrder = preOrderMan.UpdatePreOrder19dian(preOrder19Dian);
                                bool result2 = true;
                                //bool hasCoupon = false; // 是否还有可用的抵扣券， 用于判断店铺的可用券， 处理并发
                                if (usedCouponResult == 1)
                                {
                                    result2 =
                                        couponOperate.AddPayOrderConnCoupon(dtPartShopInfo.Rows[0]["shopName"].ToString(),
                                                                             request.couponId, preOrder19DianId, realDeductibleAmount, deductionCoupon);
                                    //result2 = couponOperate.AddPayOrderConnCoupon(dtPartShopInfo.Rows[0]["shopName"].ToString(), request.couponId, preOrder19DianId, realDeductibleAmount, request.couponType, mobilePhoneNumber,out hasCoupon);
                                }
                                if (result1 && updatePreOrder && result2)
                                {
                                    paymentOrderScope.Complete();
                                    response.result = VAResult.VA_OK;

                                    #region 短信提醒门店服务员门店有支付点单
                                    PayOrderSMSRemaid model = new PayOrderSMSRemaid();
                                    model.shopId = shopId;
                                    model.shopName = dtPartShopInfo.Rows[0]["shopName"].ToString();
                                    model.amount = response.serverUxianPriceSum;
                                    model.preOrderId = preOrder19DianId;
                                    model.customerPhone = mobilePhoneNumber;
                                    model.customerId = customerId;
                                    model.clientBuild = request.clientBuild;
                                    Thread payOrderSMSRemaidThread = new Thread(SMSRemindEmployee);
                                    payOrderSMSRemaidThread.Start((object)model);

                                    #endregion
                                }
                                else
                                {
                                    response.result = VAResult.VA_FAILED_DB_ERROR;
                                }
                            }
                            else
                            {
                                response.result = VAResult.VA_FAILED_DB_ERROR;
                            }
                        }
                        #endregion
                        #region else 第三方支付URL逻辑拼接
                        else
                        {
                            //更新支付点单信息
                            preOrder19Dian.preOrderServerSum = afterDeductionCoupon;
                            preOrder19Dian.prePaidSum = afterDeductionCoupon;
                            //preOrder19Dian.verifiedSaving = response.serverCalculatedSum - afterDeductionCoupon;
                            preOrder19Dian.verifiedSaving = response.serverCalculatedSum - response.serverUxianPriceSum;
                            preOrder19Dian.isPaid = 0;
                            preOrder19Dian.status = VAPreorderStatus.Uploaded;
                            preOrderMan.UpdatePreOrder19dian(preOrder19Dian);
                            //未支付的单子需同步更新Order金额
                            if (order.IsPaid == 0)
                            {
                                order.PreOrderServerSum
                                    = preOrder19Dian.preOrderServerSum.Value + realDeductibleAmount + preOrder19Dian.verifiedSaving.Value;
                                order.PrePaidSum = preOrder19Dian.prePaidSum.Value;
                                order.VerifiedSaving = preOrder19Dian.verifiedSaving.Value;
                                OrderOperate.Update(order);
                            }
                            // 余额不足，支付宝客户端，银联客户端支付URL和签名组装
                            List<RechargeActivitiesInfo> rechargeActivityList = new List<RechargeActivitiesInfo>(); //GetServerRechargeActivitiesList();
                            response.rechargeActivityList = rechargeActivityList;
                            if (request.preOrderPayMode <= 0) //判断客户端选择支付方式
                            {
                                response.result = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH;
                                return response;
                            }
                            SystemConfigOperate systemOper = new SystemConfigOperate();
                            bool payModeValid = systemOper.CheckPayMode(request.preOrderPayMode);
                            if (!payModeValid)//支付方式无效
                            {
                                SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();
                                if (mobilePhoneNumber.Equals("23588776637"))
                                {
                                    payModeList = ((List<VAPayMode>)systemConfigCacheLogic.GetServerPayModel()).FindAll(p => p.payModeId == 1);
                                }
                                else
                                {
                                    payModeList = (List<VAPayMode>)systemConfigCacheLogic.GetServerPayModel();
                                }
                                response.result = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH_AND_NO_PREORDERPAYMODE;
                            }
                            else
                            {
                                response.result = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH;
                                if (usedCouponResult == 1 && request.couponType == (int)CouponTypeEnum.OneSelf) //需要记录优惠券
                                {
                                    var dbResult = new CouponGetDetailManager().UpdateCouponState(request.couponId, preOrder19DianId, CouponUseStateType.inUse);
                                    if (dbResult == false)
                                    {
                                        response.result = VAResult.VA_FAILED_DB_ERROR;
                                    }
                                }
                                //double totalFee = canRedEnvelopePay == true
                                //    ? Common.ToDouble(afterDeductionCoupon - response.rationBalance - executedRedEnvelopeAmount)
                                //    : Common.ToDouble(afterDeductionCoupon - response.rationBalance);//实际支付=悠先价-余额
                                double totalFee = response.serverStillNeedPaySum;
                                int payMode = request.preOrderPayMode;
                                ThirdPaymentSign thirdPaymentSignOper = new ThirdPaymentSign();
                                dynamic dynamicResult = null;
                                string shopName = dtPartShopInfo.Rows[0]["shopName"].ToString().Replace(" ", "");
                                switch (payMode)
                                {
                                    default:
                                    case (int)VAClientPayMode.ALI_PAY_PLUGIN://支付宝客户端
                                        dynamicResult = thirdPaymentSignOper.AliSignPackage(paymentOrderScope, totalFee, shopName, preOrder19DianId, customerId, payMode, payOrderType);
                                        response.alipayOrder = dynamicResult.value;
                                        response.result = dynamicResult.status;
                                        break;
                                    case (int)VAClientPayMode.UNION_PAY_PLUGIN://银联客户端支付
                                        dynamicResult = thirdPaymentSignOper.UnionSignPackage(preOrderOper, paymentOrderScope, totalFee, shopName, preOrder19DianId, customerId, payMode, payOrderType);
                                        response.unionpayOrder = dynamicResult.value;
                                        response.result = dynamicResult.status;
                                        break;
                                    case (int)VAClientPayMode.WECHAT_PAY_PLUGIN://微信支付
                                        dynamicResult = thirdPaymentSignOper.WechatSignPackage(paymentOrderScope, totalFee, shopName, preOrder19DianId, customerId, payMode, payOrderType);
                                        response.ClientWechatPay = dynamicResult.value;
                                        response.result = dynamicResult.status;
                                        break;
                                }
                            }

                        }
                        #endregion
                    }
                    else
                    {
                        response.result = VAResult.VA_FAILED_DB_ERROR;//表示更新当前点单（菜品信息和杂项信息）信息失败
                    }
                }

                if (response.result == VAResult.VA_OK)
                {

                    RedEnvelopeManager redEnvelopeManager = new RedEnvelopeManager();
                    List<RedEnvelopeConnOrder3> connPreOrder = redEnvelopeManager.SelectRedEnvelopeConnPreOrder3(preOrder19Dian.preOrder19dianId);
                    double redEnvelopePayAmount = 0;
                    if (connPreOrder != null && connPreOrder.Any())
                    {
                        redEnvelopePayAmount = connPreOrder.Sum(p => p.currectUsedAmount);
                    }
                    if (preOrder19Dian.isPaid == 1)
                    {
                        RecordPreorder19DianLine(request, customerId, preOrder19Dian, rationBalancePayment,
                                                realDeductibleAmount, canRedEnvelopePay, redEnvelopePayAmount);
                    }
                    if (canRedEnvelopePay && redEnvelopePayAmount > 0 && request.boolDualPayment && preOrder19Dian.isPaid == 1)
                    {
                        //当天已经用该支付ID付款过红包点单
                        if (!string.IsNullOrEmpty(request.thirdPayAccount))
                        {
                            var preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                            {
                                CreateTimeFrom = DateTime.Today,
                                PayType = (int)VAOrderUsedPayMode.REDENVELOPE,
                                PayAccount = request.thirdPayAccount,
                                IsRefoundOut = true
                            };
                            if (Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject) > 1)
                            {
                                response.message = "红包虽好，每天只能抵用一单哦~改用支付宝/微信支付结账吧！(已支付金额系统将自动退回)";
                                response.isUsedRedEnvelopeToday = true;
                                PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
                                var originalRefundRequest = new VAOriginalRefundRequest()
                                {
                                    preOrder19dianId = request.preOrderId,
                                    cookie = request.cookie,
                                    cityId = request.cityId,
                                    uuid = request.uuid,
                                    appType = request.appType,
                                    type = VAMessageType.CLIENT_ORIGINAL_REFUNF_REQUEST,
                                    clientBuild = request.clientBuild,
                                    employeeId = 29
                                };
                                var originalRefundResponse = preOrder19dianOperate.ClientOriginalRefund(originalRefundRequest);
                            }
                        }
                    }
                    ModifyPreorderAndDishCount(request.shopId, request.orderInJson);  //支付成功统计菜品销量
                }
            }
            else
            {
                response.result = checkResult.result;
            }
            response.payModeList = payModeList;
            return response;
        }

        private static bool CheckIsUsedRedEnvelopeToday(long customerId, string uuid)
        {
            var preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
            {
                CreateTimeFrom = DateTime.Today,
                CustomerId = customerId,
                PayType = 6,
                IsRefoundOut = true
            };
            //今日使用过红包支付
            if (Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject) > 0)
            {
                return true;
            }
            preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
            {
                CreateTimeFrom = DateTime.Today,
                Uuid = uuid,
                PayType = 6,
                IsRefoundOut = true
            };
            if (Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject) > 0)
            {
                return true;
            }

            return false;
        }

        private static void RecordPreorder19DianLine(ClientRechargePaymentOrderRequest request, long customerId, PreOrder19dianInfo preOrder19Dian,
                                double rationBalancePayment, double realDeductibleAmount, bool canRedEnvelopePay, double redEnvelopePayAmount)
        {
            int prepaidState = (int)VAPreorderStatus.Prepaid;
            #region 记录支付明细数据
            var preorder19DianLineList = new List<Preorder19DianLine>();
            Preorder19DianLine preorder19DianLine = null;
            if (request.thirdPayAccount == null)
            {
                request.thirdPayAccount = string.Empty;
            }
            //第三方支付金额
            if (request.thirdPayAmount > 0)
            {
                preorder19DianLine = new Preorder19DianLine()
                {
                    CustomerId = customerId,
                    PayType = request.thirdPayType,
                    PayAccount = request.thirdPayAccount,
                    CreateTime = DateTime.Now,
                    Preorder19DianId = preOrder19Dian.preOrder19dianId,
                    State = prepaidState,
                    Amount = request.thirdPayAmount,
                    Remark = string.Empty,
                    Uuid = request.uuid,
                    RefundAmount = 0
                };
                preorder19DianLineList.Add(preorder19DianLine);
            }
            //抵扣券支付金额 
            if (realDeductibleAmount > 0)
            {
                preorder19DianLine = new Preorder19DianLine()
                {
                    CustomerId = customerId,
                    PayType = (int)VAOrderUsedPayMode.COUPON,
                    PayAccount = request.thirdPayAccount,
                    CreateTime = DateTime.Now,
                    Preorder19DianId = preOrder19Dian.preOrder19dianId,
                    State = prepaidState,
                    Amount = realDeductibleAmount,
                    Remark = string.Empty,
                    Uuid = request.uuid,
                    RefundAmount = 0
                };
                preorder19DianLineList.Add(preorder19DianLine);
            }
            //红包支付金额
            if (canRedEnvelopePay == true && redEnvelopePayAmount > 0)
            {
                preorder19DianLine = new Preorder19DianLine()
                {
                    CustomerId = customerId,
                    PayType = (int)VAOrderUsedPayMode.REDENVELOPE,
                    PayAccount = request.thirdPayAccount,
                    CreateTime = DateTime.Now,
                    Preorder19DianId = preOrder19Dian.preOrder19dianId,
                    State = prepaidState,
                    //当前点单使用红包抵扣的金额
                    Amount = redEnvelopePayAmount,
                    Remark = string.Empty,
                    Uuid = request.uuid,
                    RefundAmount = 0
                };
                preorder19DianLineList.Add(preorder19DianLine);
            }
            //粮票支付金额,二次回调不记录粮票
            if (rationBalancePayment > request.thirdPayAmount)
            {
                var preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                {
                    CustomerId = preOrder19Dian.customerId
                };
                var rationBalancePreorder19DianLine
                    = Preorder19DianLineOperate.GetFirstByQuery(preorder19DianLineQueryObject, Preorder19DianLineOrderColumn.CreateTime, SortOrder.Descending);
                preorder19DianLine = new Preorder19DianLine()
                {
                    CustomerId = customerId,
                    PayType = (int)VAOrderUsedPayMode.BALANCE,
                    PayAccount = rationBalancePreorder19DianLine != null ? rationBalancePreorder19DianLine.PayAccount : string.Empty,
                    CreateTime = DateTime.Now,
                    Preorder19DianId = preOrder19Dian.preOrder19dianId,
                    State = prepaidState,
                    Amount = rationBalancePayment - request.thirdPayAmount,
                    Remark = string.Empty,
                    Uuid = request.uuid,
                    RefundAmount = 0
                };
                preorder19DianLineList.Add(preorder19DianLine);
            }
            Preorder19DianLineOperate.AddList(preorder19DianLineList);


            #endregion
        }

        private static void RecordPreorder19DianLine(double thirdPayAmount, string thirdPayAccount, int thirdPayType, long customerId, PreOrder19dianInfo preOrder19Dian, double rationBalancePayment, double realDeductibleAmount, bool canRedEnvelopePay, double redEnvelopePayAmount)
        {
            int prepaidState = (int)VAPreorderStatus.Prepaid;
            #region 记录支付明细数据
            var preorder19DianLineList = new List<Preorder19DianLine>();
            Preorder19DianLine preorder19DianLine = null;
            //第三方支付金额
            if (thirdPayAmount > 0)
            {
                preorder19DianLine = new Preorder19DianLine()
                {
                    CustomerId = customerId,
                    PayType = thirdPayType,
                    PayAccount = thirdPayAccount,
                    CreateTime = DateTime.Now,
                    Preorder19DianId = preOrder19Dian.preOrder19dianId,
                    State = prepaidState,
                    Amount = thirdPayAmount,
                    Remark = string.Empty,
                    Uuid = preOrder19Dian.customerUUID,
                    RefundAmount = 0
                };
                preorder19DianLineList.Add(preorder19DianLine);
            }
            //抵扣券支付金额 
            if (realDeductibleAmount > 0)
            {
                preorder19DianLine = new Preorder19DianLine()
                {
                    CustomerId = customerId,
                    PayType = (int)VAOrderUsedPayMode.COUPON,
                    PayAccount = thirdPayAccount,
                    CreateTime = DateTime.Now,
                    Preorder19DianId = preOrder19Dian.preOrder19dianId,
                    State = prepaidState,
                    Amount = realDeductibleAmount,
                    Remark = string.Empty,
                    Uuid = preOrder19Dian.customerUUID,
                    RefundAmount = 0
                };
                preorder19DianLineList.Add(preorder19DianLine);
            }
            //红包支付金额
            if (canRedEnvelopePay == true && redEnvelopePayAmount > 0)
            {
                preorder19DianLine = new Preorder19DianLine()
                {
                    CustomerId = customerId,
                    PayType = (int)VAOrderUsedPayMode.REDENVELOPE,
                    PayAccount = thirdPayAccount,
                    CreateTime = DateTime.Now,
                    Preorder19DianId = preOrder19Dian.preOrder19dianId,
                    State = prepaidState,
                    //当前点单使用红包抵扣的金额
                    Amount = redEnvelopePayAmount,
                    Remark = string.Empty,
                    Uuid = preOrder19Dian.customerUUID,
                    RefundAmount = 0
                };
                preorder19DianLineList.Add(preorder19DianLine);
            }
            //粮票支付金额,二次回调不记录粮票
            if (rationBalancePayment > thirdPayAmount)
            {
                var preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                {
                    CustomerId = preOrder19Dian.customerId
                };
                var rationBalancePreorder19DianLine
                    = Preorder19DianLineOperate.GetFirstByQuery(preorder19DianLineQueryObject, Preorder19DianLineOrderColumn.CreateTime, SortOrder.Descending);
                preorder19DianLine = new Preorder19DianLine()
                {
                    CustomerId = customerId,
                    PayType = (int)VAOrderUsedPayMode.BALANCE,
                    PayAccount = rationBalancePreorder19DianLine != null ? rationBalancePreorder19DianLine.PayAccount : string.Empty,
                    CreateTime = DateTime.Now,
                    Preorder19DianId = preOrder19Dian.preOrder19dianId,
                    State = prepaidState,
                    Amount = Common.ToDouble(rationBalancePayment - thirdPayAmount),
                    Remark = string.Empty,
                    Uuid = preOrder19Dian.customerUUID,
                    RefundAmount = 0
                };
                preorder19DianLineList.Add(preorder19DianLine);
            }
            Preorder19DianLineOperate.AddList(preorder19DianLineList);


            #endregion
        }
        /// <summary>
        /// 客户端带充值功能直接支付接口add by wangc 20140504
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ClientRechargeDirectPaymentResponse ClientRechargeDirectPayment(ClientRechargeDirectPaymentRequest request)
        {
            ClientRechargeDirectPaymentResponse response = new ClientRechargeDirectPaymentResponse();
            response.type = VAMessageType.CLIENT_RECHARGE_DIRECT_PAYMENT_RESPONSE;
            response.cookie = request.cookie;
            response.uuid = request.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(request.cookie, request.uuid, (int)request.type, (int)VAMessageType.CLIENT_RECHARGE_DIRECT_PAYMENT_REQUEST, false);
            List<VAPayMode> payModeList = new List<VAPayMode>();//支付方式
            if (checkResult.result == VAResult.VA_OK)
            {
                DataRow drCustomer = checkResult.dtCustomer.Rows[0];
                string mobilePhoneNumber = Common.ToString(drCustomer["mobilePhoneNumber"]);
                if (String.IsNullOrEmpty(mobilePhoneNumber))//校验是否绑定手机号码
                {
                    response.result = VAResult.VA_FAILED_NOT_BINDING_MOBILE_PHONE_NUMBER;
                    return response;
                }
                //客户端传来了0元的点单
                if (request.payAmount <= 0 && request.boolDualPayment == false)
                {
                    response.message = "不允许0元点单";
                    response.result = VAResult.VA_NOT_ALLOW_ZERO_PAY;
                    return response;
                }
                //处理过期红包
                //RedEnvelopeDetailOperate redEnvelopeOperate = new RedEnvelopeDetailOperate();
                //double executedRedEnvelopeAmount = 0;
                //bool redEnvelopeFlag = redEnvelopeOperate.DoExpirationRedEnvelopeLogic(mobilePhoneNumber, ref  executedRedEnvelopeAmount);
                //if (!redEnvelopeFlag)
                //{
                //    response.result = VAResult.VA_FAILED_DB_ERROR;
                //    return response;
                //} 
                long customerId = Common.ToInt64(drCustomer["CustomerID"]);//用户编号

                int shopId = request.shopId;//门店编号
                DataTable dtPartShopInfo = new ShopInfoCacheLogic().GetPartShopInfoCache(shopId);
                var payOrderType = VAPayOrderType.DIRECT_PAYMENT_AND_RECHARGE;

                if (dtPartShopInfo == null || dtPartShopInfo.Rows.Count <= 0)//校验门店信息
                {
                    response.result = VAResult.VA_FAILED_DB_ERROR;
                    return response;
                }
                if (Common.ToInt32(dtPartShopInfo.Rows[0]["isHandle"]) != (int)VAShopHandleStatus.SHOP_Pass)//校验门店是否上线
                {
                    response.result = VAResult.VA_FAILED_SHOP_NOT_ONLINE;
                    return response;
                }
                bool isSupportPayment = Common.ToBool(dtPartShopInfo.Rows[0]["isSupportPayment"]);
                if (isSupportPayment != true)//支持付款
                {
                    response.notPaymentReason = Common.ToString(dtPartShopInfo.Rows[0]["notPaymentReason"]);
                    response.result = VAResult.VA_SHOP_NOT_SUPPOORT_PAYMENY;//该门店不支持付款
                    return response;
                }


                //------------------------------------------------------------------------------

                AwardConnPreOrderOperate awardConnPreOrderOperate = new AwardConnPreOrderOperate();
                bool checkCanPay = awardConnPreOrderOperate.CheckCusCanLotteryByUnConfirmedOrderCntOfVA(customerId, request.uuid);
                if (!checkCanPay)
                {
                    response.result = VAResult.VA_PAY_FAIL;
                    AwardCacheLogic awardCacheLogic = new AwardCacheLogic();
                    response.message = awardCacheLogic.GetAwardConfig("CannotPay", "");
                    return response;
                }

                //------------------------------------------------------------------------------



                bool isSupportRedEnvelopePayment = Common.ToBool(dtPartShopInfo.Rows[0]["isSupportRedEnvelopePayment"]);
                const double currentDiscount = 1;//直接付款没有折扣
                request.deskNumber = String.IsNullOrEmpty(request.deskNumber) ? "" : Common.ToClearSpecialCharString(request.deskNumber);//桌号
                PreOrder19dianManager preOrderMan = new PreOrder19dianManager();
                PreOrder19dianInfo preOrder19Dian = null;
                Order order = null;
                //粮票支付金额
                double rationBalancePayment = 0;
                if (request.preOrderId <= 0)//*新增点单
                {
                    #region 新增点单基本信息

                    order = new Order()
                    {
                        CustomerId = customerId,
                        ShopId = shopId,
                        PreOrderTime = DateTime.Now,
                        Status = (int)VAPreorderStatus.Uploaded,
                        PreOrderServerSum = request.payAmount,
                        InvoiceTitle = string.Empty,
                        IsPaid = 0,
                        IsShopConfirmed = 0,
                        IsEvaluation = 0,
                        RefundMoneyClosedSum = 0,
                        RefundMoneySum = 0
                    };
                    if (OrderOperate.Add(order) == false)
                    {
                        response.result = VAResult.VA_FAILED_DB_ERROR;
                        return response;
                    }
                    preOrder19Dian = new PreOrder19dianInfo()
                    {
                        customerId = customerId,
                        companyId = Common.ToInt32(dtPartShopInfo.Rows[0]["companyID"]),
                        shopId = shopId,
                        preOrderTime = DateTime.Now,
                        menuId = Common.ToInt32(dtPartShopInfo.Rows[0]["MenuID"]),
                        status = VAPreorderStatus.Uploaded,
                        customerUUID = request.uuid,
                        preOrderSum = request.payAmount,//直接支付金额(缺少安全校验 >0)
                        orderInJson = "",
                        sundryJson = "",
                        deskNumber = request.deskNumber,
                        appType = (int)request.appType,
                        appBuild = request.clientBuild
                    };
                    response.preOrderId = new PreOrder19dianOperate().AddPreOrder19Dian(preOrder19Dian); //新增点单持久化到DB
                    if (response.preOrderId <= 0)
                    {
                        response.result = VAResult.VA_FAILED_DB_ERROR;
                        return response;
                    }
                    #endregion
                }
                else//*支付点单
                {
                    response.preOrderId = request.preOrderId;
                    if (!request.boolDualPayment)//不处理第三方回调支付
                    {
                        if (!preOrderMan.UpdatePreorderAppInfo(response.preOrderId, (int)request.appType, request.clientBuild))//更新点单支付客户端版本号和app类型
                        {
                            response.result = VAResult.VA_FAILED_DB_ERROR;
                            return response;
                        }
                    }
                }
                if (request.preOrderPayMode > 0 && request.preOrderPayMode != Common.ToInt32(drCustomer["defaultPayment"]))//保存为常用支付方式
                {
                    new CustomerManager().UpdateCustomerDefaultPayment(request.preOrderPayMode, request.cookie);
                }
                response.rationBalance = Common.ToDouble(drCustomer["money19dianRemained"]);//用户粮票
                response.serviceUrl = WebConfig.ServiceUrl;//悠先服务条款
                long preOrder19DianId = response.preOrderId;
                DataTable newPreOrderInfo = preOrderMan.SelectPreOrderAndCompanyByPayMent(preOrder19DianId);//获取最新DB中这条点单数据
                if (newPreOrderInfo.Rows.Count <= 0)//点单未找到
                {
                    response.result = VAResult.VA_FAILED_NOT_FOUND_ORDER;
                    return response;
                }
                if (Common.ToInt32(newPreOrderInfo.Rows[0]["isPaid"]) == (int)VAPreorderIsPaid.PAID)//订单已支付
                {
                    int preOrderInfoStatus = Common.ToInt32(newPreOrderInfo.Rows[0]["status"]);
                    //已退款的点单则返回支付点单服务器端计算金额和客户端不一致,使客户端停留在点菜页面
                    if (preOrderInfoStatus == (int)VAPreorderStatus.Refund || preOrderInfoStatus == (int)VAPreorderStatus.OriginalRefunding)
                    {
                        response.executedRedEnvelopeAmount = 0;
                        response.result = VAResult.VA_FAILED_PREORDER_COUNT_SERVER_NOT_EQUAL_CLIENT;
                        response.message = @"红包虽好，每天只能抵用一单哦~改用支付宝/微信支付结账吧！(已支付金额系统将自动退回)";
                        response.preOrderId = 0;
                        if (Preorder19DianLineOperate.GetCountByQuery(
                            new Preorder19DianLineQueryObject()
                            {
                                PayType = (int)VAOrderUsedPayMode.REDENVELOPE,
                                CustomerId = customerId,
                                IsRefoundOut = true
                            }) > 0 ||
                            Preorder19DianLineOperate.GetCountByQuery(
                            new Preorder19DianLineQueryObject()
                            {
                                PayType = (int)VAOrderUsedPayMode.REDENVELOPE,
                                Uuid = request.uuid,
                                IsRefoundOut = true
                            }) > 0)
                        {
                            response.isUsedRedEnvelopeToday = true;
                        }
                        else
                        {
                            response.isUsedRedEnvelopeToday = false;
                        }
                        return response;
                    }
                    response.result = VAResult.VA_OK;
                    return response;
                }

                double price = Common.ToDouble(newPreOrderInfo.Rows[0]["preOrderSum"]);//数据库存储点单金额
                double newPrice = Common.ToDouble(request.payAmount);//客户端请求金额
                string oldDeskNumber = Common.ToString(newPreOrderInfo.Rows[0]["deskNumber"]);//数据库存贮桌号

                bool flag = true;
                bool flag1 = true;
                bool priceChanges = false;
                bool latestBuild = Common.CheckLatestBuild_August(request.appType, request.clientBuild);

                RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
                double executedRedEnvelopeAmount = redEnvelopeOperate.QueryCustomerExcutedRedEnvelope(mobilePhoneNumber);

                executedRedEnvelopeAmount = latestBuild ? executedRedEnvelopeAmount : 0;
                bool canRedEnvelopePay = isSupportRedEnvelopePayment && latestBuild;
                bool needThirdPartyPayment = true;//需要使用第三方支付标记

                if (Common.ToDouble(price) != Common.ToDouble(newPrice))//客户端存在修改价格操作
                {
                    flag = new PreOrder19dianOperate().ModifyPreOrderAndSundryJson(preOrder19DianId, "", "", newPrice);//修改当前菜品的订单,杂项,金额和支付金额信息
                    price = newPrice;
                }
                if (oldDeskNumber != request.deskNumber)
                {
                    flag1 = preOrderMan.UpdateDeskNumber(preOrder19DianId, request.deskNumber);//更新桌号信息
                }
                //优惠券参与计算
                int usedCouponResult;//标记是否使用优惠券
                double realDeductionAmount;
                CouponOperate couponOperate = new CouponOperate();
                double afterDeductionCoupon =
                    couponOperate.GetCurrectPreOrderAfterDiscountAmount(request.couponId, price, price, request.appType, request.clientBuild, out usedCouponResult, out realDeductionAmount);//计算点单折扣后优惠券抵扣金额
                if (usedCouponResult == -2 || usedCouponResult == -3)
                {
                    response.result = VAResult.VA_FILED_FOUND_COUPON;//未找到指定优惠券，或优惠券不存在
                    response.couponDetails = couponOperate.GetShopCouponDetails(mobilePhoneNumber, shopId, request.appType, request.clientBuild);
                    return response;
                }
                //今日该用户或该设备使用过红包,则不允许红包支付 
                if (canRedEnvelopePay == true)
                {
                    var preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                    {
                        CreateTimeFrom = DateTime.Today,
                        CustomerId = customerId,
                        PayType = (int)VAOrderUsedPayMode.REDENVELOPE,
                        IsRefoundOut = true
                    };
                    //今日使用过红包支付
                    if (Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject) > 0)
                    {
                        canRedEnvelopePay = false;
                        response.isUsedRedEnvelopeToday = true;
                    }
                    if (canRedEnvelopePay)
                    {
                        preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                        {
                            CreateTimeFrom = DateTime.Today,
                            Uuid = request.uuid,
                            PayType = (int)VAOrderUsedPayMode.REDENVELOPE,
                            IsRefoundOut = true
                        };
                        if (Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject) > 0)
                        {
                            canRedEnvelopePay = false;
                            response.isUsedRedEnvelopeToday = true;
                        }
                    }
                }



                if (canRedEnvelopePay)//支持红包支付
                {
                    response.executedRedEnvelopeAmount = executedRedEnvelopeAmount;
                    double serverStillNeedPaySum = Common.ToDouble((afterDeductionCoupon - executedRedEnvelopeAmount - response.rationBalance)) > 0 ? Common.ToDouble((afterDeductionCoupon - executedRedEnvelopeAmount - response.rationBalance)) : 0;
                    response.serverStillNeedPaySum = serverStillNeedPaySum;
                    priceChanges = (price == Common.ToDouble(request.payAmount) && serverStillNeedPaySum == Common.ToDouble(request.clientStillNeedPaySum)) ? true : false;
                    needThirdPartyPayment = (Common.ToDouble(response.rationBalance + executedRedEnvelopeAmount - afterDeductionCoupon) > -0.01) ? false : true;
                }
                else//不支持红包支付
                {
                    response.executedRedEnvelopeAmount = 0;
                    double serverStillNeedPaySum = Common.ToDouble((afterDeductionCoupon - response.rationBalance)) > 0 ? Common.ToDouble((afterDeductionCoupon - response.rationBalance)) : 0;
                    response.serverStillNeedPaySum = serverStillNeedPaySum;
                    priceChanges = (price == Common.ToDouble(request.payAmount) && serverStillNeedPaySum == Common.ToDouble(request.clientStillNeedPaySum)) ? true : false;
                    needThirdPartyPayment = (Common.ToDouble(response.rationBalance - afterDeductionCoupon) > -0.01) ? false : true;
                }
                //第三方支付金额为0,粮票余额为0,则支付金额+0.01
                //第三方支付金额为0,粮票余额不为0,历史未使用第三方支付则支付金额+0.01
                if (!request.boolDualPayment && canRedEnvelopePay == true && executedRedEnvelopeAmount > 0 && response.serverStillNeedPaySum == 0)
                {
                    if (response.rationBalance > 0)
                    {
                        var preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                        {
                            CustomerId = customerId,
                            PayType = (int)VAOrderUsedPayMode.WECHAT
                        };
                        long weixinPayCount = Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject);
                        long aliPayCount = 0;
                        if (weixinPayCount == 0)
                        {
                            preorder19DianLineQueryObject.PayType = (int)VAOrderUsedPayMode.ALIPAY;
                            aliPayCount = Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject);
                            if (aliPayCount == 0)
                            {
                                response.serverStillNeedPaySum = 0.01;
                                response.result = VAResult.VA_FAILED_SMS_MONEY_NOT_ENOUGH;
                                response.message = @"红包虽好，每天只能抵用一单哦~改用支付宝/微信支付结账吧！(已支付金额系统将自动退回)";
                                needThirdPartyPayment = true;
                                priceChanges = true;
                            }
                        }
                    }
                    else
                    {
                        response.serverStillNeedPaySum = 0.01;
                        response.result = VAResult.VA_FAILED_SMS_MONEY_NOT_ENOUGH;
                        response.message = @"红包虽好，每天只能抵用一单哦~改用支付宝/微信支付结账吧！(已支付金额系统将自动退回)";
                        needThirdPartyPayment = true;
                        priceChanges = true;
                    }

                    if (response.serverStillNeedPaySum > 0)
                    {
                        preOrderMan.UpdateExtendPay(0.01, preOrder19DianId);
                    }
                }
                //比较客户端和服务器段计算总价格和悠先价格，或者是二次支付
                if (flag && flag1 && (priceChanges || request.boolDualPayment == true))
                {
                    DataView dvPreorder = newPreOrderInfo.DefaultView;
                    RedEnvelopeOperate redEnvelopeOper = new RedEnvelopeOperate();
                    using (TransactionScope paymentOrderScope = new TransactionScope())
                    {
                        if (request.rechargeActivityId > 0)
                        {
                            #region 充值操作逻辑
                            ClientPersonCenterRechargeRequest rechargeRequest = new ClientPersonCenterRechargeRequest()
                                                                       {
                                                                           boolDualPayment = false,
                                                                           cityId = request.cityId,
                                                                           cookie = request.cookie,
                                                                           payMode = request.preOrderPayMode,
                                                                           rechargeActivityId = request.rechargeActivityId,
                                                                           rechargeOrderId = 0,
                                                                           uuid = request.uuid,
                                                                           type = VAMessageType.CLIENT_PERSON_CENTER_RECHARGE_REQUEST,
                                                                           preOrder19dianId = preOrder19DianId
                                                                       };
                            ClientPersonCenterRechargeReponse rechargeResponse = ClientPersonCenterRecharge(rechargeRequest);
                            //返回充值一系列url链接
                            response.result = rechargeResponse.result;
                            response.alipayOrder = rechargeResponse.alipayOrder;
                            response.ClientWechatPay = rechargeResponse.ClientWechatPay;
                            response.unionpayOrder = rechargeResponse.unionpayOrder;
                            response.urlToContinuePayment = rechargeResponse.urlToContinuePayment;
                            paymentOrderScope.Complete();
                            #endregion
                        }
                        else
                        {
                            #region if 粮票余额红包支付逻辑
                            //bool insertResult = false;
                            bool modifyRedEnvelope = false;
                            bool batchRedEnvelopeConnPreOrder = false;
                            DateTime expireTime = Convert.ToDateTime("1970-1-1");

                            if (!needThirdPartyPayment)
                            {
                                double extendPay = PreOrder19dianOperate.GetExtendPayByPreOrder19DianId(request.preOrderId);
                                redEnvelopeOper.DoRedEnvelopePaymentLogic(request.cookie, executedRedEnvelopeAmount, afterDeductionCoupon, mobilePhoneNumber,
preOrder19DianId, canRedEnvelopePay, extendPay, ref rationBalancePayment, ref modifyRedEnvelope, ref batchRedEnvelopeConnPreOrder, ref expireTime);
                                if (batchRedEnvelopeConnPreOrder && modifyRedEnvelope)
                                {
                                    bool result1 = true;

                                    if (rationBalancePayment > 0)
                                    {
                                        PreOrder19dianOperate.ChangeMoney(preOrder19DianId, customerId, 0, shopId, rationBalancePayment, ref result1);
                                    }
                                    //更新当前点单服务器计算点单金额
                                    preOrder19Dian = PreOrder19dianOperate.GetPreOrder19dianInfo(dvPreorder, afterDeductionCoupon, currentDiscount);
                                    order = OrderOperate.GetEntityById(preOrder19Dian.OrderId);
                                    if (order == null)
                                    {
                                        response.result = VAResult.VA_FAILED_DB_ERROR;
                                        return response;
                                    }
                                    preOrder19Dian.verifiedSaving = 0;
                                    preOrder19Dian.preOrderServerSum = afterDeductionCoupon;
                                    preOrder19Dian.prePaidSum = afterDeductionCoupon;

                                    bool expireRedEnvelope = Common.CheckExpireRedEnvelopeBuild_February(request.appType, request.clientBuild);
                                    if (expireRedEnvelope)//只有高于等于设置版本才设置点单过期时间
                                    {
                                        preOrder19Dian.expireTime = expireTime;
                                    }
                                    else//否则不设置
                                    {
                                        preOrder19Dian.expireTime = Convert.ToDateTime("1970-1-1");
                                    }

                                    order.IsPaid = preOrder19Dian.isPaid.Value;
                                    order.PrePaidSum = preOrder19Dian.prePaidSum.Value;
                                    order.VerifiedSaving = preOrder19Dian.verifiedSaving.Value;
                                    order.ExpireTime = preOrder19Dian.expireTime.Value;
                                    order.PrePayTime = preOrder19Dian.prePayTime.Value;
                                    if (preOrder19Dian.OrderType == OrderTypeEnum.Normal)
                                    {
                                        order.Status = (byte)preOrder19Dian.status;
                                    }
                                    if (OrderOperate.Update(order) == false)
                                    {
                                        response.result = VAResult.VA_FAILED_DB_ERROR;
                                        return response;
                                    }
                                    //更新支付点单信息
                                    //bool updatePreOrder = preOrderMan.UpdatePreOrder19dianOrderInfo(preOrder19Dian);
                                    bool updatePreOrder = preOrderMan.UpdatePreOrder19dian(preOrder19Dian);
                                    bool result2 = true;
                                    if (usedCouponResult == 1)//表示成功使用了优惠券
                                    {
                                        result2 =
                                            couponOperate.AddPayOrderConnCoupon(dtPartShopInfo.Rows[0]["shopName"].ToString(), request.couponId,
                                            preOrder19DianId, realDeductionAmount, price);
                                    }
                                    if (result1 && updatePreOrder && result2)
                                    {
                                        paymentOrderScope.Complete(); //点单余额支付成功
                                        response.result = VAResult.VA_OK;
                                        #region 短信提醒门店服务员当前门店有支付点单 & 年夜饭订单客户提醒
                                        PayOrderSMSRemaid model = new PayOrderSMSRemaid();
                                        model.shopId = shopId;
                                        model.shopName = dtPartShopInfo.Rows[0]["shopName"].ToString();
                                        model.amount = price;
                                        model.customerPhone = mobilePhoneNumber;
                                        model.preOrderId = preOrder19DianId;
                                        model.customerId = customerId;
                                        model.clientBuild = request.clientBuild;
                                        Thread payOrderSmsRemaidThread = new Thread(SMSRemindEmployee);
                                        payOrderSmsRemaidThread.Start((object)model);
                                        #endregion
                                    }
                                    else
                                    {
                                        response.result = VAResult.VA_FAILED_DB_ERROR;
                                    }
                                }
                                else
                                {
                                    response.result = VAResult.VA_FAILED_DB_ERROR;
                                }
                            }
                            #endregion
                            #region else 第三方支付URL逻辑拼接 余额不足，支付宝客户端，银联客户端支付，银联支付
                            else
                            {
                                if (request.preOrderPayMode <= 0)//有支付方式
                                {
                                    SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();
                                    if (mobilePhoneNumber.Equals("23588776637"))
                                    {
                                        response.payModeList = ((List<VAPayMode>)systemConfigCacheLogic.GetServerPayModel()).FindAll(p => p.payModeId == 1);
                                    }
                                    else
                                    {
                                        response.payModeList = (List<VAPayMode>)systemConfigCacheLogic.GetServerPayModel();
                                    }
                                    response.result = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH;
                                    return response;
                                }
                                SystemConfigOperate systemOper = new SystemConfigOperate();
                                bool payModeValid = systemOper.CheckPayMode(request.preOrderPayMode);
                                if (!payModeValid)//支付方式无效
                                {
                                    SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();
                                    if (mobilePhoneNumber.Equals("23588776637"))
                                    {
                                        response.payModeList = ((List<VAPayMode>)systemConfigCacheLogic.GetServerPayModel()).FindAll(p => p.payModeId == 1);
                                    }
                                    else
                                    {
                                        response.payModeList = (List<VAPayMode>)systemConfigCacheLogic.GetServerPayModel();
                                    }
                                    response.result = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH_AND_NO_PREORDERPAYMODE;
                                    return response;
                                }
                                List<RechargeActivitiesInfo> rechargeActivityList = new List<RechargeActivitiesInfo>(); //GetServerRechargeActivitiesList();//获取活动列表
                                response.rechargeActivityList = rechargeActivityList;
                                response.result = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH;
                                if (usedCouponResult == 1) //需要记录优惠券
                                {
                                    var dbResult = new CouponGetDetailManager().UpdateCouponState(request.couponId, preOrder19DianId, CouponUseStateType.inUse);
                                    if (dbResult == false)
                                    {
                                        response.result = VAResult.VA_FAILED_DB_ERROR;
                                    }
                                }
                                //double totalFee = canRedEnvelopePay == true
                                //    ? Common.ToDouble(afterDeductionCoupon - response.rationBalance - executedRedEnvelopeAmount)
                                //: Common.ToDouble(afterDeductionCoupon - response.rationBalance);//实际支付=悠先价-余额-用户红包（版本没有跟上默认就是0）
                                double totalFee = Common.ToDouble(response.serverStillNeedPaySum);
                                int payMode = request.preOrderPayMode;
                                ThirdPaymentSign thirdPaymentSignOper = new ThirdPaymentSign();
                                dynamic dynamicResult = null;
                                string shopName = dtPartShopInfo.Rows[0]["shopName"].ToString().Replace(" ", "");
                                switch (payMode)
                                {
                                    default:
                                    case (int)VAClientPayMode.ALI_PAY_PLUGIN://支付宝客户端
                                        dynamicResult = thirdPaymentSignOper.AliSignPackage(paymentOrderScope, totalFee, shopName, preOrder19DianId, customerId, payMode, payOrderType);
                                        response.alipayOrder = dynamicResult.value;
                                        response.result = dynamicResult.status;
                                        break;
                                    case (int)VAClientPayMode.UNION_PAY_PLUGIN://银联客户端支付
                                        PreOrder19dianOperate preOrderOper = new PreOrder19dianOperate();
                                        dynamicResult = thirdPaymentSignOper.UnionSignPackage(preOrderOper, paymentOrderScope, totalFee, shopName, preOrder19DianId, customerId, payMode, payOrderType);
                                        response.unionpayOrder = dynamicResult.value;
                                        response.result = dynamicResult.status;
                                        break;
                                    case (int)VAClientPayMode.WECHAT_PAY_PLUGIN://微信支付
                                        dynamicResult = thirdPaymentSignOper.WechatSignPackage(paymentOrderScope, totalFee, shopName, preOrder19DianId, customerId, payMode, payOrderType);
                                        response.ClientWechatPay = dynamicResult.value;
                                        response.result = dynamicResult.status;
                                        break;
                                }
                            }
                            #endregion
                        }
                    }
                }
                else
                {
                    response.executedRedEnvelopeAmount = executedRedEnvelopeAmount;
                    response.result = VAResult.VA_FAILED_PREORDER_COUNT_SERVER_NOT_EQUAL_CLIENT;//客户端和服务端计算原价和悠先价不一致
                }
                if (response.result == VAResult.VA_OK)
                {
                    if (preOrder19Dian.isPaid == 1)
                    {
                        RedEnvelopeManager redEnvelopeManager = new RedEnvelopeManager();
                        List<RedEnvelopeConnOrder3> connPreOrder = redEnvelopeManager.SelectRedEnvelopeConnPreOrder3(preOrder19Dian.preOrder19dianId);
                        double redEnvelopePayAmount = 0;

                        if (connPreOrder != null && connPreOrder.Count > 0)
                        {
                            redEnvelopePayAmount = connPreOrder.Sum(p => p.currectUsedAmount);
                        }
                        #region 记录支付明细数据
                        if (request.thirdPayAccount == null)
                        {
                            request.thirdPayAccount = string.Empty;
                        }
                        var preorder19DianLineList = new List<Preorder19DianLine>();
                        Preorder19DianLine preorder19DianLine = null;
                        int prepaidState = (int)VAPreorderStatus.Prepaid;
                        //第三方支付金额
                        if (request.thirdPayAmount > 0)
                        {
                            preorder19DianLine = new Preorder19DianLine()
                            {
                                Uuid = request.uuid,
                                CustomerId = customerId,
                                PayType = request.thirdPayType,
                                PayAccount = request.thirdPayAccount,
                                CreateTime = DateTime.Now,
                                Preorder19DianId = preOrder19Dian.preOrder19dianId,
                                State = prepaidState,
                                Amount = request.thirdPayAmount,
                                Remark = string.Empty,
                                RefundAmount = 0
                            };
                            preorder19DianLineList.Add(preorder19DianLine);
                        }
                        //抵扣券支付金额
                        var coupon
                            = CouponGetDetailOperate.GetListByQuery(new CouponGetDetailQueryObject() { PreOrder19DianId = preOrder19Dian.preOrder19dianId });
                        if (coupon != null && coupon.Count > 0)
                        {
                            preorder19DianLine = new Preorder19DianLine()
                            {
                                Uuid = request.uuid,
                                CustomerId = customerId,
                                PayType = (int)VAOrderUsedPayMode.COUPON,
                                PayAccount = request.thirdPayAccount,
                                CreateTime = DateTime.Now,
                                Preorder19DianId = preOrder19Dian.preOrder19dianId,
                                State = prepaidState,
                                Amount = coupon.Sum(p => p.DeductibleAmount),
                                Remark = string.Empty,
                                RefundAmount = 0
                            };
                            preorder19DianLineList.Add(preorder19DianLine);
                        }
                        //红包支付金额
                        if (redEnvelopePayAmount > 0)
                        {
                            preorder19DianLine = new Preorder19DianLine()
                            {
                                Uuid = request.uuid,
                                CustomerId = customerId,
                                PayType = (int)VAOrderUsedPayMode.REDENVELOPE,
                                PayAccount = request.thirdPayAccount,
                                CreateTime = DateTime.Now,
                                Preorder19DianId = preOrder19Dian.preOrder19dianId,
                                State = prepaidState,
                                Amount = redEnvelopePayAmount,//当前点单使用红包抵扣的金额
                                Remark = string.Empty,
                                RefundAmount = 0
                            };
                            preorder19DianLineList.Add(preorder19DianLine);
                        }

                        //粮票支付金额,二次支付的粮票支付不记录数据库
                        if (rationBalancePayment > request.thirdPayAmount)
                        {
                            var preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                            {
                                CustomerId = customerId
                            };
                            var rationBalancePreorder19DianLine
                                = Preorder19DianLineOperate.GetFirstByQuery(preorder19DianLineQueryObject, Preorder19DianLineOrderColumn.CreateTime);
                            preorder19DianLine = new Preorder19DianLine()
                            {
                                Uuid = request.uuid,
                                CustomerId = customerId,
                                PayType = (int)VAOrderUsedPayMode.BALANCE,
                                PayAccount = rationBalancePreorder19DianLine != null ? rationBalancePreorder19DianLine.PayAccount : string.Empty,
                                CreateTime = DateTime.Now,
                                Preorder19DianId = preOrder19Dian.preOrder19dianId,
                                State = prepaidState,
                                Amount = rationBalancePayment - request.thirdPayAmount,
                                Remark = string.Empty,
                                RefundAmount = 0
                            };
                            preorder19DianLineList.Add(preorder19DianLine);
                        }
                        Preorder19DianLineOperate.AddList(preorder19DianLineList);


                        #endregion

                        //当天已经用该支付ID付款过含红包点单
                        if (redEnvelopePayAmount > 0 && request.boolDualPayment)
                        {
                            var preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                            {
                                CreateTimeFrom = DateTime.Today,
                                PayType = (int)VAOrderUsedPayMode.REDENVELOPE,
                                PayAccount = request.thirdPayAccount,
                                IsRefoundOut = true
                            };
                            if (Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject) > 1)
                            {
                                PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
                                var originalRefundRequest = new VAOriginalRefundRequest()
                                {
                                    preOrder19dianId = request.preOrderId,
                                    cookie = request.cookie,
                                    cityId = request.cityId,
                                    uuid = request.uuid,
                                    appType = request.appType,
                                    type = VAMessageType.CLIENT_ORIGINAL_REFUNF_REQUEST,
                                    clientBuild = request.clientBuild,
                                    employeeId = 29
                                };
                                var originalRefundResponse = preOrder19dianOperate.ClientOriginalRefund(originalRefundRequest);
                            }
                        }
                    }
                    ModifyPreorderAndDishCount(request.shopId, "");//统计菜品
                }
            }
            else
            {
                response.result = checkResult.result;
            }
            response.payModeList = payModeList;
            return response;
        }

        /// <summary>
        /// 查询服务器有效支付方式
        /// </summary>
        /// <returns></returns>
        public static List<VAPayMode> GetServerPayModelList()
        {
            List<VAPayMode> payModeList = new List<VAPayMode>();
            SystemConfigManager sysConfigMan = new SystemConfigManager();
            DataTable dtPayMode = sysConfigMan.SelectPayMode();
            if (dtPayMode.Rows.Count > 0)
            {
                for (int i = 0; i < dtPayMode.Rows.Count; i++)
                {
                    VAPayMode payModeInfo = new VAPayMode();
                    payModeInfo.payModeId = Common.ToInt32(dtPayMode.Rows[i]["payModeValue"]);
                    payModeInfo.payModeName = Common.ToString(dtPayMode.Rows[i]["payModeName"]);
                    payModeList.Add(payModeInfo);
                }
            }
            return payModeList;
        }
        List<RechargeActivitiesInfo> GetServerRechargeActivitiesList()
        {
            List<RechargeActivitiesInfo> list = new List<RechargeActivitiesInfo>();
            ClientRechargeOperate clientRechargeOper = new ClientRechargeOperate();
            if (SystemConfigOperate.ClientRechargeFeatureIsOpen())
            {
                DataTable dtRechargeActivities = clientRechargeOper.ClientQueryRecharge();
                if (dtRechargeActivities.Rows.Count > 0)
                {
                    foreach (DataRow item in dtRechargeActivities.Rows)
                    {
                        RechargeActivitiesInfo model = new RechargeActivitiesInfo();
                        model.beginTime = Common.ToString(item["beginTime"]);
                        model.endTime = Common.ToString(item["endTime"]);
                        model.externalSold = Common.ToInt32(item["externalSold"]);
                        model.id = Common.ToInt32(item["id"]);
                        model.name = Common.ToString(item["name"]);
                        model.sequence = Common.ToInt32(item["sequence"]);
                        list.Add(model);
                    }
                }
            }
            else//活动未开启
            {
                //list 不赋值，返回空【】
            }
            return list;
        }

        /// <summary>
        /// 返回重组后的菜品信息
        /// </summary>
        /// <param name="orderInJson"></param>
        /// <param name="preOrderMan"></param>
        /// <returns>如菜谱中菜谱数量小于等于0,则返回空字符串</returns>
        static string OverRideOrderJson(string orderInJson, PreOrder19dianManager preOrderMan)
        {
            List<PreOrderIn19dian> listOrderInfo = JsonOperate.JsonDeserialize<List<PreOrderIn19dian>>(orderInJson);
            if (listOrderInfo.Exists(p => p.quantity <= 0 || p.dishIngredients.Exists(g => g.quantity <= 0)))
            {
                return string.Empty;
            }
            if (listOrderInfo == null || !listOrderInfo.Any())
            {
                return "";
            }
            string stringSql = "(0,";//定义菜品id字符串，初始化有默认id=0，防止异常出现
            string ingredientsIdStr = "(0,";//定义配菜id字符串
            if (listOrderInfo.Count > 0)
            {
                for (int i = 0; i < listOrderInfo.Count; i++)//遍历菜品数组
                {
                    stringSql += listOrderInfo[i].dishPriceI18nId + ",";//菜品编号
                    if (listOrderInfo[i].dishIngredients != null && listOrderInfo[i].dishIngredients.Count > 0)
                    {
                        for (int j = 0; j < listOrderInfo[i].dishIngredients.Count; j++)//遍历菜品数组子项配菜数组
                        {
                            ingredientsIdStr += listOrderInfo[i].dishIngredients[j].ingredientsId + ",";//配菜编号
                        }
                    }
                }
            }
            string strSql = stringSql.TrimEnd(',') + ")";
            string sqlIngredients = ingredientsIdStr.TrimEnd(',') + ")";
            DataTable dt = preOrderMan.SelectMarkNameByDishPriceI18nId(strSql);//优化：移除被删除的菜品
            if (dt == null || dt.Rows.Count == 0)
            {
                return string.Empty;
            }
            DataTable dtIngredients = DishIngredientsManager.SelectDishIngredients(sqlIngredients);//优化：移除被删除的配菜
            for (int i = 0; i < listOrderInfo.Count; i++)//循环orderJson
            {
                if (dt.Rows.Count > 0)
                {
                    var query = from q in dt.AsEnumerable()
                                where q.Field<int>("dishPriceI18nId") == listOrderInfo[i].dishPriceI18nId
                                select q.Field<int>("dishPriceI18nId");
                    bool effective = (query != null & query.ToList().Any());
                    if (effective)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)//循环orderJson中所有菜品DataTable
                        {
                            if (listOrderInfo[i].dishPriceI18nId == Common.ToInt32(dt.Rows[j]["dishPriceI18nId"]))
                            {
                                listOrderInfo[i].markName = Common.ToString(dt.Rows[j]["markName"]);//处理了为null的case
                                listOrderInfo[i].unitPrice = Common.ToDouble(dt.Rows[j]["DishPrice"]);
                                listOrderInfo[i].dishName = Common.ToString(dt.Rows[j]["DishName"]);
                                listOrderInfo[i].dishPriceName = Common.ToString(dt.Rows[j]["ScaleName"]);
                                break;
                            }
                        }
                    }
                    else
                    {
                        listOrderInfo.Remove(listOrderInfo[i]);//当前菜品已被删除
                        break;
                    }
                }
                if (dtIngredients.Rows.Count > 0)
                {
                    if (listOrderInfo[i].dishIngredients != null && listOrderInfo[i].dishIngredients.Count > 0)//循环配菜
                    {
                        for (int j = 0; j < listOrderInfo[i].dishIngredients.Count; j++)
                        {
                            var query = from q in dtIngredients.AsEnumerable()
                                        where q.Field<int>("ingredientsId") == listOrderInfo[i].dishIngredients[j].ingredientsId
                                        select q.Field<int>("ingredientsId");
                            bool effective = (query != null & query.ToList().Any());
                            if (effective)
                            {
                                foreach (DataRow item in dtIngredients.Rows)//循环orderJson中所有配菜DataTable
                                {
                                    if (listOrderInfo[i].dishIngredients[j].ingredientsId == Common.ToInt32(item["ingredientsId"]))
                                    {
                                        listOrderInfo[i].dishIngredients[j].ingredientsPrice = Common.ToDouble(item["ingredientsPrice"]);
                                        listOrderInfo[i].dishIngredients[j].ingredientsName = Common.ToString(item["ingredientsName"]);
                                        //listOrderInfo[i].dishIngredients[j].quantity 客户端传递数量
                                        listOrderInfo[i].dishIngredients[j].vipDiscountable = Common.ToString(item["vipDiscountable"]);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                listOrderInfo[i].dishIngredients.Remove(listOrderInfo[i].dishIngredients[j]);//当前配菜已被删除
                                break;
                            }
                        }
                    }
                }
            }
            return JsonOperate.JsonSerializer<List<PreOrderIn19dian>>(listOrderInfo);
        }
        private static string OverRideSundryList(List<VASundryInfo> sundrylist)
        {
            string sundryJson = "";
            if (sundrylist != null && sundrylist.Count > 0)
            {
                string stringSql = "(0,";//默认id=0，防止异常
                for (int i = 0; i < sundrylist.Count; i++)
                {
                    stringSql += sundrylist[i].sundryId + ",";
                }
                string strSql = stringSql.TrimEnd(',') + ")";
                SundryManager sundryMan = new SundryManager();
                DataTable dtSundry = sundryMan.SelectSundryInfoBySundryId(strSql);
                if (dtSundry.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtSundry.Rows)
                    {
                        var query = from q in sundrylist
                                    where q.sundryId == Common.ToInt32(dr["sundryId"])
                                    select q.sundryId;
                        bool effective = (query != null & query.ToList().Any());
                        if (effective)
                        {
                            for (int i = 0; i < sundrylist.Count; i++)
                            {
                                if (Common.ToInt32(dr["sundryId"]) == Common.ToInt32(sundrylist[i].sundryId))
                                {
                                    sundrylist[i].price = Common.ToDouble(dr["price"]);
                                    sundrylist[i].sundryName = Common.ToString(dr["sundryName"]);
                                    sundrylist[i].sundryStandard = Common.ToString(dr["sundryStandard"]);
                                    sundrylist[i].sundryChargeMode = Common.ToInt32(dr["sundryChargeMode"]); //杂项收费模式:1固定金额,2按比例收取,3按人次
                                    sundrylist[i].supportChangeQuantity = Common.ToBool(dr["supportChangeQuantity"]);
                                    //quantity { get; set; }//该项数量，取客户端传递数量
                                    sundrylist[i].vipDiscountable = Common.ToBool(dr["vipDiscountable"]);//支持折扣
                                    //backDiscountable { get; set; }//享受返送
                                    sundrylist[i].required = Common.ToBool(dr["required"]);//是否必选
                                    break;
                                }
                            }
                        }
                    }
                    sundryJson = JsonOperate.JsonSerializer<List<VASundryInfo>>(sundrylist);
                }
            }
            return sundryJson;
        }
        /// <summary>
        /// 更新公司和店铺的点单量，更新对应菜品的销量
        /// 如果不需要更新菜品OrderInJson传空值
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="orderInJson"></param>
        static void ModifyPreorderAndDishCount(int shopId, string orderInJson)
        {
            PreorderAndDishUpdateInfo preorderAndDishUpdateInfo = new PreorderAndDishUpdateInfo();
            preorderAndDishUpdateInfo.shopId = shopId;
            preorderAndDishUpdateInfo.orderInJson = orderInJson;
            Thread encourageThread = new Thread(UpdatePreorderAndDishCount);
            encourageThread.Start((object)preorderAndDishUpdateInfo);
        }
        /// <summary>
        /// 更新公司和店铺未结账预点单数量和菜的下单数量
        /// </summary>
        /// <param name="preorderAndDishUpdateInfo"></param>
        public static void UpdatePreorderAndDishCount(object preorderAndDishUpdateInfo)
        {
            try
            {
                PreorderAndDishUpdateInfo preorderAndDishUpdateDetail = (PreorderAndDishUpdateInfo)preorderAndDishUpdateInfo;
                List<PreOrderIn19dian> listOrderInfo = JsonOperate.JsonDeserialize<List<PreOrderIn19dian>>(preorderAndDishUpdateDetail.orderInJson);
                PreOrder19dianManager preOrder19dianMan = new PreOrder19dianManager();
                using (TransactionScope scope = new TransactionScope())
                {
                    preOrder19dianMan.UpdateShopPreorderCount(preorderAndDishUpdateDetail.shopId);
                    foreach (PreOrderIn19dian preorder in listOrderInfo)
                    {//更新菜的点击量
                        preOrder19dianMan.UpdateDishSalesCount(preorder.dishPriceI18nId, preorder.quantity);
                    }
                    scope.Complete();
                }
            }
            catch (System.Exception)
            {
                //啥也不用干
            }
        }
        /// <summary>
        /// 悠先点菜根据城市查看商圈
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ClientCheckBusinessDistrictResponse ClientCheckBusinessDistrict(ClientCheckBusinessDistrictRequest request)
        {
            ClientCheckBusinessDistrictResponse reponse = new ClientCheckBusinessDistrictResponse();
            reponse.type = VAMessageType.CLIENT_CHECK_BUSINESSDISTRICT_RESPONSE;
            reponse.cookie = request.cookie;
            reponse.uuid = request.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(request.cookie, request.uuid, (int)request.type, (int)VAMessageType.CLIENT_CHECK_BUSINESSDISTRICT_REQUEST);
            List<BusinessDistrictTag> list = new List<BusinessDistrictTag>();
            if (checkResult.result == VAResult.VA_OK)
            {
                //IShopTagService shopVipSpeedConfig = ServiceFactory.Resolve<IShopTagService>();
                //list = shopVipSpeedConfig.GetShopBusinessDistricts(request.cityId);
                list = GetBusinessDistrictTag(request.cityId);
                reponse.result = VAResult.VA_OK;
            }
            else
            {
                reponse.result = checkResult.result;
            }
            reponse.tagList = list;
            return reponse;
        }
        /// <summary>
        /// 从Cache中读取商圈信息，缓存5分钟
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        private List<BusinessDistrictTag> GetBusinessDistrictTag(int cityId)
        {
            var businessDistrictTagCache = MemcachedHelper.GetMemcached<List<BusinessDistrictTag>>("businessDistrictTag_" + cityId);
            if (businessDistrictTagCache == null)
            {
                IShopTagService shopVipSpeedConfig = ServiceFactory.Resolve<IShopTagService>();
                businessDistrictTagCache = shopVipSpeedConfig.GetShopBusinessDistricts(cityId);//查询当前门店的VIP等级信息
                if (businessDistrictTagCache != null)
                {
                    MemcachedHelper.AddMemcached("businessDistrictTag_" + cityId, businessDistrictTagCache, 60 * 5);//五分钟
                }
            }
            return businessDistrictTagCache;
        }

        public ClientRechargeDirectPaymentResponse ClientDirectPayment(ClientRechargeDirectPaymentRequest request)
        {
            //if (request.boolDualPayment)
            //{
            //    LogDll.LogManager.WriteLog(LogDll.LogFile.Error, string.Format("时间:{0},thirdPayAmount:{1},thirdPayType:{2},preOrderId:{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), request.thirdPayAmount, request.thirdPayType, request.preOrderId));
            //}
            ClientRechargeDirectPaymentResponse response = new ClientRechargeDirectPaymentResponse();
            response.type = VAMessageType.CLIENT_RECHARGE_DIRECT_PAYMENT_V1_RESPONSE;
            response.cookie = request.cookie;
            response.uuid = request.uuid;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(request.cookie, request.uuid, (int)request.type, (int)VAMessageType.CLIENT_RECHARGE_DIRECT_PAYMENT_V1_REQUEST, false);
            List<VAPayMode> payModeList = new List<VAPayMode>();//支付方式
            if (checkResult.result == VAResult.VA_OK)
            {
                DataRow drCustomer = checkResult.dtCustomer.Rows[0];
                string mobilePhoneNumber = Common.ToString(drCustomer["mobilePhoneNumber"]);
                if (String.IsNullOrEmpty(mobilePhoneNumber))//校验是否绑定手机号码
                {
                    response.result = VAResult.VA_FAILED_NOT_BINDING_MOBILE_PHONE_NUMBER;
                    return response;
                }
                //客户端传来了0元的点单
                if (request.payAmount <= 0 && request.boolDualPayment == false)
                {
                    response.message = "不允许0元点单";
                    response.result = VAResult.VA_NOT_ALLOW_ZERO_PAY;
                    return response;
                }
                //处理过期红包
                //RedEnvelopeDetailOperate redEnvelopeOperate = new RedEnvelopeDetailOperate();
                //double executedRedEnvelopeAmount = 0;
                //bool redEnvelopeFlag = redEnvelopeOperate.DoExpirationRedEnvelopeLogic(mobilePhoneNumber, ref  executedRedEnvelopeAmount);
                //if (!redEnvelopeFlag)
                //{
                //    response.result = VAResult.VA_FAILED_DB_ERROR;
                //    return response;
                //} 
                long customerId = Common.ToInt64(drCustomer["CustomerID"]);//用户编号

                int shopId = request.shopId;//门店编号
                DataTable dtPartShopInfo = new ShopInfoCacheLogic().GetPartShopInfoCache(shopId);
                var payOrderType = VAPayOrderType.DIRECT_PAYMENT_AND_RECHARGE;

                if (dtPartShopInfo == null || dtPartShopInfo.Rows.Count <= 0)//校验门店信息
                {
                    response.result = VAResult.VA_FAILED_DB_ERROR;
                    return response;
                }
                if (Common.ToInt32(dtPartShopInfo.Rows[0]["isHandle"]) != (int)VAShopHandleStatus.SHOP_Pass)//校验门店是否上线
                {
                    response.result = VAResult.VA_FAILED_SHOP_NOT_ONLINE;
                    return response;
                }
                bool isSupportPayment = Common.ToBool(dtPartShopInfo.Rows[0]["isSupportPayment"]);
                if (isSupportPayment != true)//支持付款
                {
                    response.notPaymentReason = Common.ToString(dtPartShopInfo.Rows[0]["notPaymentReason"]);
                    response.result = VAResult.VA_SHOP_NOT_SUPPOORT_PAYMENY;//该门店不支持付款
                    return response;
                }


                //------------------------------------------------------------------------------

                AwardConnPreOrderOperate awardConnPreOrderOperate = new AwardConnPreOrderOperate();
                bool checkCanPay = awardConnPreOrderOperate.CheckCusCanLotteryByUnConfirmedOrderCntOfVA(customerId, request.uuid);
                if (!checkCanPay)
                {
                    response.result = VAResult.VA_PAY_FAIL;
                    AwardCacheLogic awardCacheLogic = new AwardCacheLogic();
                    response.message = awardCacheLogic.GetAwardConfig("CannotPay", "");
                    return response;
                }

                //------------------------------------------------------------------------------



                bool isSupportRedEnvelopePayment = Common.ToBool(dtPartShopInfo.Rows[0]["isSupportRedEnvelopePayment"]);
                const double currentDiscount = 1;//直接付款没有折扣
                request.deskNumber = String.IsNullOrEmpty(request.deskNumber) ? "" : Common.ToClearSpecialCharString(request.deskNumber);//桌号
                PreOrder19dianManager preOrderMan = new PreOrder19dianManager();
                PreOrder19dianInfo preOrder19Dian = null;
                Order order = null;
                //粮票支付金额
                double rationBalancePayment = 0;
                if (request.preOrderId <= 0)//*新增点单
                {
                    #region 新增点单基本信息

                    order = new Order()
                    {
                        Id = Guid.NewGuid(),
                        CustomerId = customerId,
                        ShopId = shopId,
                        PreOrderTime = DateTime.Now,
                        Status = (int)VAPreorderStatus.Uploaded,
                        PreOrderServerSum = request.payAmount,
                        InvoiceTitle = string.Empty,
                        IsPaid = 0,
                        IsShopConfirmed = 0,
                        IsEvaluation = 0,
                        RefundMoneyClosedSum = 0,
                        RefundMoneySum = 0,
                        PrePayTime = DateTime.Now,
                        PayDifferenceSum = 0,
                        ExpireTime = DateTime.Now.AddYears(10),
                        VerifiedSaving = 0,
                        PrePaidSum = 0,
                        Remark = string.Empty
                    };
                    if (OrderOperate.Add(order) == false)
                    {
                        response.result = VAResult.VA_FAILED_DB_ERROR;
                        return response;
                    }
                    preOrder19Dian = new PreOrder19dianInfo()
                    {
                        customerId = customerId,
                        companyId = Common.ToInt32(dtPartShopInfo.Rows[0]["companyID"]),
                        shopId = shopId,
                        preOrderTime = DateTime.Now,
                        menuId = Common.ToInt32(dtPartShopInfo.Rows[0]["MenuID"]),
                        status = VAPreorderStatus.Uploaded,
                        customerUUID = request.uuid,
                        preOrderSum = request.payAmount,//直接支付金额(缺少安全校验 >0)
                        orderInJson = "",
                        sundryJson = "",
                        deskNumber = request.deskNumber,
                        appType = (int)request.appType,
                        appBuild = request.clientBuild,
                        OrderId = order.Id,
                        OrderType = OrderTypeEnum.Normal
                    };
                    response.preOrderId = new PreOrder19dianOperate().AddPreOrder19Dian(preOrder19Dian); //新增点单持久化到DB
                    if (response.preOrderId <= 0)
                    {
                        response.result = VAResult.VA_FAILED_DB_ERROR;
                        return response;
                    }
                    #endregion
                }
                else//*支付点单
                {
                    response.preOrderId = request.preOrderId;
                    if (!request.boolDualPayment)//不处理第三方回调支付
                    {
                        if (!preOrderMan.UpdatePreorderAppInfo(response.preOrderId, (int)request.appType, request.clientBuild))//更新点单支付客户端版本号和app类型
                        {
                            response.result = VAResult.VA_FAILED_DB_ERROR;
                            return response;
                        }
                    }
                }
                if (request.preOrderPayMode > 0 && request.preOrderPayMode != Common.ToInt32(drCustomer["defaultPayment"]))//保存为常用支付方式
                {
                    new CustomerManager().UpdateCustomerDefaultPayment(request.preOrderPayMode, request.cookie);
                }
                response.rationBalance = Common.ToDouble(drCustomer["money19dianRemained"]);//用户粮票
                response.serviceUrl = WebConfig.ServiceUrl;//悠先服务条款
                long preOrder19DianId = response.preOrderId;
                DataTable newPreOrderInfo = preOrderMan.SelectPreOrderAndCompanyByPayMent(preOrder19DianId);//获取最新DB中这条点单数据
                if (newPreOrderInfo.Rows.Count <= 0)//点单未找到
                {
                    response.result = VAResult.VA_FAILED_NOT_FOUND_ORDER;
                    return response;
                }
                if (Common.ToInt32(newPreOrderInfo.Rows[0]["isPaid"]) == (int)VAPreorderIsPaid.PAID)//订单已支付
                {
                    int preOrderInfoStatus = Common.ToInt32(newPreOrderInfo.Rows[0]["status"]);
                    //已退款的点单则返回支付点单服务器端计算金额和客户端不一致,使客户端停留在点菜页面
                    if (preOrderInfoStatus == (int)VAPreorderStatus.Refund || preOrderInfoStatus == (int)VAPreorderStatus.OriginalRefunding)
                    {
                        response.executedRedEnvelopeAmount = 0;
                        response.result = VAResult.VA_FAILED_PREORDER_COUNT_SERVER_NOT_EQUAL_CLIENT;
                        response.message = @"红包虽好，每天只能抵用一单哦~改用支付宝/微信支付结账吧！(已支付金额系统将自动退回)";
                        response.preOrderId = 0;

                        if (Preorder19DianLineOperate.GetCountByQuery(
                            new Preorder19DianLineQueryObject()
                            {
                                PayType = (int)VAOrderUsedPayMode.REDENVELOPE,
                                CustomerId = customerId,
                                IsRefoundOut = true
                            }) > 0 ||
                            Preorder19DianLineOperate.GetCountByQuery(
                            new Preorder19DianLineQueryObject()
                            {
                                PayType = (int)VAOrderUsedPayMode.REDENVELOPE,
                                Uuid = request.uuid,
                                IsRefoundOut = true
                            }) > 0)
                        {
                            response.isUsedRedEnvelopeToday = true;
                        }
                        else
                        {
                            response.isUsedRedEnvelopeToday = false;
                        }
                        return response;
                    }
                    response.result = VAResult.VA_OK;
                    return response;
                }

                double price = Common.ToDouble(newPreOrderInfo.Rows[0]["preOrderSum"]);//数据库存储点单金额
                double newPrice = Common.ToDouble(request.payAmount);//客户端请求金额
                string oldDeskNumber = Common.ToString(newPreOrderInfo.Rows[0]["deskNumber"]);//数据库存贮桌号

                bool flag = true;
                bool flag1 = true;
                bool priceChanges = false;
                bool latestBuild = Common.CheckLatestBuild_August(request.appType, request.clientBuild);

                RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
                double executedRedEnvelopeAmount = redEnvelopeOperate.QueryCustomerExcutedRedEnvelope(mobilePhoneNumber);

                executedRedEnvelopeAmount = latestBuild ? executedRedEnvelopeAmount : 0;
                bool canRedEnvelopePay = isSupportRedEnvelopePayment && latestBuild;
                bool needThirdPartyPayment = true;//需要使用第三方支付标记

                if (Common.ToDouble(price) != Common.ToDouble(newPrice))//客户端存在修改价格操作
                {
                    flag = new PreOrder19dianOperate().ModifyPreOrderAndSundryJson(preOrder19DianId, "", "", newPrice);//修改当前菜品的订单,杂项,金额和支付金额信息
                    price = newPrice;
                }
                if (oldDeskNumber != request.deskNumber)
                {
                    flag1 = preOrderMan.UpdateDeskNumber(preOrder19DianId, request.deskNumber);//更新桌号信息
                }
                //优惠券参与计算
                int usedCouponResult;//标记是否使用优惠券
                double realDeductionAmount;
                CouponOperate couponOperate = new CouponOperate();
                //计算点单折扣后优惠券抵扣金额
                //double afterDeductionCoupon =
                //    CouponOperate.GetAmountAfterUseCoupon(request.couponId, price, price, out usedCouponResult, out realDeductionAmount);



                //LogDll.LogManager.WriteLog(LogDll.LogFile.Error, string.Format("时间:{0},preOrderId:{1},price:{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),  request.preOrderId, price));



                int couponGetDetailIDNew = 0;
                double afterDeductionCoupon =
                    CouponOperate.GetAmountAfterUseCoupon(request.couponId, price, price, request.couponType, mobilePhoneNumber, preOrder19DianId, out usedCouponResult, out realDeductionAmount, out couponGetDetailIDNew);

                //LogDll.LogManager.WriteLog(LogDll.LogFile.Error, string.Format("时间:{0},afterDeductionCoupon:{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), afterDeductionCoupon));



                // 店铺抵扣券没有可领用的
                if (usedCouponResult == -4)
                {
                    response.result = VAResult.VA_FILED_FOUND_COUPON;
                    response.message = "抱歉，抵扣券已被领用完。";
                    return response;
                }

                // 店铺抵扣券自动领取后， 更新couponID
                if (request.couponType == (int)CouponTypeEnum.Bussiness)
                {
                    request.couponId = couponGetDetailIDNew;
                }

                if (usedCouponResult == -2 || usedCouponResult == -3)
                {
                    response.result = VAResult.VA_FILED_FOUND_COUPON;//未找到指定优惠券，或优惠券不存在
                    response.couponDetails = couponOperate.GetShopCouponDetails(mobilePhoneNumber, shopId, request.appType, request.clientBuild);
                    return response;
                }

                //今日该用户或该设备使用过红包,则不允许红包支付 
                if (canRedEnvelopePay == true)
                {
                    var preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                    {
                        CreateTimeFrom = DateTime.Today,
                        CustomerId = customerId,
                        PayType = (int)VAOrderUsedPayMode.REDENVELOPE,
                        IsRefoundOut = true
                    };
                    //今日使用过红包支付
                    if (Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject) > 0)
                    {
                        canRedEnvelopePay = false;
                        response.isUsedRedEnvelopeToday = true;
                    }
                    if (canRedEnvelopePay)
                    {
                        preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                        {
                            CreateTimeFrom = DateTime.Today,
                            Uuid = request.uuid,
                            PayType = (int)VAOrderUsedPayMode.REDENVELOPE,
                            IsRefoundOut = true
                        };
                        if (Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject) > 0)
                        {
                            canRedEnvelopePay = false;
                            response.isUsedRedEnvelopeToday = true;
                        }
                    }
                }



                if (canRedEnvelopePay)//支持红包支付
                {
                    response.executedRedEnvelopeAmount = executedRedEnvelopeAmount;
                    double serverStillNeedPaySum = Common.ToDouble((afterDeductionCoupon - executedRedEnvelopeAmount - response.rationBalance)) > 0 ? Common.ToDouble((afterDeductionCoupon - executedRedEnvelopeAmount - response.rationBalance)) : 0;
                    response.serverStillNeedPaySum = serverStillNeedPaySum;
                    priceChanges = (price == Common.ToDouble(request.payAmount) && serverStillNeedPaySum == Common.ToDouble(request.clientStillNeedPaySum)) ? true : false;
                    needThirdPartyPayment = (Common.ToDouble(response.rationBalance + executedRedEnvelopeAmount - afterDeductionCoupon) > -0.01) ? false : true;


                    //LogDll.LogManager.WriteLog(LogDll.LogFile.Error, string.Format("时间:{0},支持红包支付priceChanges:{1},二次支付:{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), priceChanges, request.boolDualPayment));
                }
                else//不支持红包支付
                {
                    response.executedRedEnvelopeAmount = 0;
                    double serverStillNeedPaySum = Common.ToDouble((afterDeductionCoupon - response.rationBalance)) > 0 ? Common.ToDouble((afterDeductionCoupon - response.rationBalance)) : 0;
                    response.serverStillNeedPaySum = serverStillNeedPaySum;
                    priceChanges = (price == Common.ToDouble(request.payAmount) && serverStillNeedPaySum == Common.ToDouble(request.clientStillNeedPaySum)) ? true : false;
                    needThirdPartyPayment = (Common.ToDouble(response.rationBalance - afterDeductionCoupon) > -0.01) ? false : true;

                    //LogDll.LogManager.WriteLog(LogDll.LogFile.Error, string.Format("时间:{0},不支持红包支付priceChanges:{1},二次支付:{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), priceChanges, request.boolDualPayment));
                }
                //第三方支付金额为0,粮票余额为0,则支付金额+0.01
                //第三方支付金额为0,粮票余额不为0,历史未使用第三方支付则支付金额+0.01
                if (!request.boolDualPayment && canRedEnvelopePay == true && executedRedEnvelopeAmount > 0 && response.serverStillNeedPaySum == 0)
                {
                    if (response.rationBalance > 0)
                    {
                        var preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                        {
                            CustomerId = customerId,
                            PayType = (int)VAOrderUsedPayMode.WECHAT
                        };
                        long weixinPayCount = Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject);
                        long aliPayCount = 0;
                        if (weixinPayCount == 0)
                        {
                            preorder19DianLineQueryObject.PayType = (int)VAOrderUsedPayMode.ALIPAY;
                            aliPayCount = Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject);
                            if (aliPayCount == 0)
                            {
                                response.serverStillNeedPaySum = 0.01;
                                response.result = VAResult.VA_FAILED_SMS_MONEY_NOT_ENOUGH;
                                response.message = @"红包虽好，每天只能抵用一单哦~改用支付宝/微信支付结账吧！(已支付金额系统将自动退回)";
                                needThirdPartyPayment = true;
                                priceChanges = true;
                            }
                        }
                    }
                    else
                    {
                        response.serverStillNeedPaySum = 0.01;
                        response.result = VAResult.VA_FAILED_SMS_MONEY_NOT_ENOUGH;
                        response.message = @"红包虽好，每天只能抵用一单哦~改用支付宝/微信支付结账吧！(已支付金额系统将自动退回)";
                        needThirdPartyPayment = true;
                        priceChanges = true;
                    }

                    if (response.serverStillNeedPaySum > 0)
                    {
                        preOrderMan.UpdateExtendPay(0.01, preOrder19DianId);
                    }
                }

                LogDll.LogManager.WriteLog(LogDll.LogFile.Error, string.Format("时间：{0}，修改订单:{1}，更新桌号：{2},priceChanges：{3},request.boolDualPayment:{4}", DateTime.Now.ToString(), flag, flag1, priceChanges, request.boolDualPayment));

                //比较客户端和服务器段计算总价格和悠先价格，或者是二次支付
                if (flag && flag1 && (priceChanges || request.boolDualPayment == true))
                {
                    DataView dvPreorder = newPreOrderInfo.DefaultView;
                    preOrder19Dian = PreOrder19dianOperate.GetPreOrder19dianInfo(dvPreorder, afterDeductionCoupon, currentDiscount);
                    RedEnvelopeOperate redEnvelopeOper = new RedEnvelopeOperate();
                    using (TransactionScope paymentOrderScope = new TransactionScope())
                    {
                        if (request.rechargeActivityId > 0)
                        {
                            #region 充值操作逻辑
                            ClientPersonCenterRechargeRequest rechargeRequest = new ClientPersonCenterRechargeRequest()
                            {
                                boolDualPayment = false,
                                cityId = request.cityId,
                                cookie = request.cookie,
                                payMode = request.preOrderPayMode,
                                rechargeActivityId = request.rechargeActivityId,
                                rechargeOrderId = 0,
                                uuid = request.uuid,
                                type = VAMessageType.CLIENT_PERSON_CENTER_RECHARGE_REQUEST,
                                preOrder19dianId = preOrder19DianId
                            };
                            ClientPersonCenterRechargeReponse rechargeResponse = ClientPersonCenterRecharge(rechargeRequest);
                            //返回充值一系列url链接
                            response.result = rechargeResponse.result;
                            response.alipayOrder = rechargeResponse.alipayOrder;
                            response.ClientWechatPay = rechargeResponse.ClientWechatPay;
                            response.unionpayOrder = rechargeResponse.unionpayOrder;
                            response.urlToContinuePayment = rechargeResponse.urlToContinuePayment;
                            paymentOrderScope.Complete();
                            #endregion
                        }
                        else
                        {
                            #region if 粮票余额红包支付逻辑
                            //bool insertResult = false;
                            bool modifyRedEnvelope = false;
                            bool batchRedEnvelopeConnPreOrder = false;
                            DateTime expireTime = Convert.ToDateTime("1970-1-1");

                            if (!needThirdPartyPayment)
                            {
                                double extendPay = PreOrder19dianOperate.GetExtendPayByPreOrder19DianId(request.preOrderId);
                                redEnvelopeOper.DoRedEnvelopePaymentLogic(request.cookie, executedRedEnvelopeAmount, afterDeductionCoupon, mobilePhoneNumber,
preOrder19DianId, canRedEnvelopePay, extendPay, ref rationBalancePayment, ref modifyRedEnvelope, ref batchRedEnvelopeConnPreOrder, ref expireTime);
                                if (batchRedEnvelopeConnPreOrder && modifyRedEnvelope)
                                {
                                    bool result1 = true;

                                    if (rationBalancePayment > 0)
                                    {
                                        PreOrder19dianOperate.ChangeMoney(preOrder19DianId, customerId, 0, shopId, rationBalancePayment, ref result1);
                                    }
                                    //更新当前点单服务器计算点单金额 
                                    order = OrderOperate.GetEntityById(preOrder19Dian.OrderId);
                                    if (order == null)
                                    {
                                        response.result = VAResult.VA_FAILED_DB_ERROR;
                                        return response;
                                    }
                                    preOrder19Dian.verifiedSaving = 0;
                                    preOrder19Dian.preOrderServerSum = afterDeductionCoupon;
                                    preOrder19Dian.prePaidSum = afterDeductionCoupon;

                                    bool expireRedEnvelope = Common.CheckExpireRedEnvelopeBuild_February(request.appType, request.clientBuild);
                                    if (expireRedEnvelope)//只有高于等于设置版本才设置点单过期时间
                                    {
                                        preOrder19Dian.expireTime = expireTime;
                                    }
                                    else//否则不设置
                                    {
                                        preOrder19Dian.expireTime = Convert.ToDateTime("1970-1-1");
                                    }

                                    order.PreOrderServerSum
                                        = preOrder19Dian.preOrderServerSum.Value + preOrder19Dian.verifiedSaving.Value + realDeductionAmount;
                                    order.IsPaid = preOrder19Dian.isPaid.Value;
                                    order.PrePaidSum = preOrder19Dian.prePaidSum.Value;
                                    order.VerifiedSaving = preOrder19Dian.verifiedSaving.Value;
                                    order.ExpireTime = preOrder19Dian.expireTime.Value;
                                    order.PrePayTime = preOrder19Dian.prePayTime.Value;

                                    if (preOrder19Dian.OrderType == OrderTypeEnum.Normal)
                                    {
                                        order.Status = (byte)preOrder19Dian.status;
                                    }
                                    if (OrderOperate.Update(order) == false)
                                    {
                                        response.result = VAResult.VA_FAILED_DB_ERROR;
                                        return response;
                                    }
                                    //更新支付点单信息
                                    //bool updatePreOrder = preOrderMan.UpdatePreOrder19dianOrderInfo(preOrder19Dian);
                                    bool updatePreOrder = preOrderMan.UpdatePreOrder19dian(preOrder19Dian);
                                    bool result2 = true;
                                    //bool hasCoupon = false;  // 是否还有可用的抵扣券， 用于判断店铺的可用券， 处理并发下券的领用情况
                                    if (usedCouponResult == 1)//表示成功使用了优惠券
                                    {
                                        result2 = couponOperate.AddPayOrderConnCoupon(dtPartShopInfo.Rows[0]["shopName"].ToString(), request.couponId, preOrder19DianId, realDeductionAmount, price);
                                        //result2 = couponOperate.AddPayOrderConnCoupon(dtPartShopInfo.Rows[0]["shopName"].ToString(), request.couponId, preOrder19DianId, realDeductionAmount,request.couponType,mobilePhoneNumber,out hasCoupon);
                                    }
                                    if (result1 && updatePreOrder && result2)
                                    {
                                        paymentOrderScope.Complete(); //点单余额支付成功
                                        response.result = VAResult.VA_OK;
                                        #region 短信提醒门店服务员当前门店有支付点单 & 年夜饭订单客户提醒
                                        PayOrderSMSRemaid model = new PayOrderSMSRemaid();
                                        model.shopId = shopId;
                                        model.shopName = dtPartShopInfo.Rows[0]["shopName"].ToString();
                                        model.amount = price;
                                        model.customerPhone = mobilePhoneNumber;
                                        model.preOrderId = preOrder19DianId;
                                        model.customerId = customerId;
                                        model.clientBuild = request.clientBuild;
                                        Thread payOrderSmsRemaidThread = new Thread(SMSRemindEmployee);
                                        payOrderSmsRemaidThread.Start((object)model);
                                        #endregion
                                    }
                                    else
                                    {
                                        response.result = VAResult.VA_FAILED_DB_ERROR;
                                    }
                                }
                                else
                                {
                                    response.result = VAResult.VA_FAILED_DB_ERROR;
                                }
                            }
                            #endregion
                            #region else 第三方支付URL逻辑拼接 余额不足，支付宝客户端，银联客户端支付，银联支付
                            else
                            {
                                //更新支付点单信息
                                preOrder19Dian.preOrderServerSum = afterDeductionCoupon;
                                preOrder19Dian.prePaidSum = afterDeductionCoupon;
                                //preOrder19Dian.verifiedSaving = response.serverCalculatedSum - afterDeductionCoupon;
                                preOrder19Dian.verifiedSaving = 0;
                                preOrderMan.UpdatePreOrder19dian(preOrder19Dian);
                                order = OrderOperate.GetEntityById(preOrder19Dian.OrderId);
                                if (order != null)
                                {
                                    if (order.IsPaid == 0 && preOrder19Dian.OrderType == OrderTypeEnum.Normal)
                                    {
                                        order.PayDifferenceSum = 0;
                                        order.VerifiedSaving = 0;
                                        order.PreOrderServerSum = preOrder19Dian.preOrderSum;
                                        order.PrePaidSum = 0;
                                    }
                                    OrderOperate.Update(order);
                                }
                                if (request.preOrderPayMode <= 0)//有支付方式
                                {
                                    SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();
                                    if (mobilePhoneNumber.Equals("23588776637"))
                                    {
                                        response.payModeList = ((List<VAPayMode>)systemConfigCacheLogic.GetServerPayModel()).FindAll(p => p.payModeId == 1);
                                    }
                                    else
                                    {
                                        response.payModeList = (List<VAPayMode>)systemConfigCacheLogic.GetServerPayModel();
                                    }
                                    response.result = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH;
                                    return response;
                                }
                                SystemConfigOperate systemOper = new SystemConfigOperate();
                                bool payModeValid = systemOper.CheckPayMode(request.preOrderPayMode);
                                if (!payModeValid)//支付方式无效
                                {
                                    SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();
                                    if (mobilePhoneNumber.Equals("23588776637"))
                                    {
                                        response.payModeList = ((List<VAPayMode>)systemConfigCacheLogic.GetServerPayModel()).FindAll(p => p.payModeId == 1);
                                    }
                                    else
                                    {
                                        response.payModeList = (List<VAPayMode>)systemConfigCacheLogic.GetServerPayModel();
                                    }
                                    response.result = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH_AND_NO_PREORDERPAYMODE;
                                    return response;
                                }
                                List<RechargeActivitiesInfo> rechargeActivityList = new List<RechargeActivitiesInfo>(); //GetServerRechargeActivitiesList();//获取活动列表
                                response.rechargeActivityList = rechargeActivityList;
                                response.result = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH;
                                if (usedCouponResult == 1 && request.couponType == (int)CouponTypeEnum.OneSelf) //需要记录优惠券
                                {
                                    //var couponGetDetail = CouponGetDetailOperate.GetEntityById(request.couponId);
                                    //if (couponGetDetail == null)
                                    //{
                                    //    response.result = VAResult.VA_FAILED_DB_ERROR;
                                    //    return response;
                                    //}
                                    //couponGetDetail.RealDeductibleAmount = realDeductionAmount;
                                    //couponGetDetail.State = (int)CouponUseStateType.inUse;
                                    //couponGetDetail.PreOrder19DianId = request.preOrderId;
                                    //if (CouponGetDetailOperate.Update(couponGetDetail) == false)
                                    //{
                                    //    response.result = VAResult.VA_FAILED_DB_ERROR;
                                    //}

                                    var dbResult = new CouponGetDetailManager().UpdateCouponState(request.couponId, preOrder19DianId, CouponUseStateType.inUse);
                                    if (dbResult == false)
                                    {
                                        response.result = VAResult.VA_FAILED_DB_ERROR;
                                    }

                                }
                                //double totalFee = canRedEnvelopePay == true
                                //    ? Common.ToDouble(afterDeductionCoupon - response.rationBalance - executedRedEnvelopeAmount)
                                //: Common.ToDouble(afterDeductionCoupon - response.rationBalance);//实际支付=悠先价-余额-用户红包（版本没有跟上默认就是0）
                                double totalFee = response.serverStillNeedPaySum;
                                int payMode = request.preOrderPayMode;
                                ThirdPaymentSign thirdPaymentSignOper = new ThirdPaymentSign();
                                dynamic dynamicResult = null;
                                string shopName = dtPartShopInfo.Rows[0]["shopName"].ToString().Replace(" ", "");
                                switch (payMode)
                                {
                                    default:
                                    case (int)VAClientPayMode.ALI_PAY_PLUGIN://支付宝客户端
                                        dynamicResult = thirdPaymentSignOper.AliSignPackage(paymentOrderScope, totalFee, shopName, preOrder19DianId, customerId, payMode, payOrderType);
                                        response.alipayOrder = dynamicResult.value;
                                        response.result = dynamicResult.status;
                                        break;
                                    case (int)VAClientPayMode.UNION_PAY_PLUGIN://银联客户端支付
                                        PreOrder19dianOperate preOrderOper = new PreOrder19dianOperate();
                                        dynamicResult = thirdPaymentSignOper.UnionSignPackage(preOrderOper, paymentOrderScope, totalFee, shopName, preOrder19DianId, customerId, payMode, payOrderType);
                                        response.unionpayOrder = dynamicResult.value;
                                        response.result = dynamicResult.status;
                                        break;
                                    case (int)VAClientPayMode.WECHAT_PAY_PLUGIN://微信支付
                                        dynamicResult = thirdPaymentSignOper.WechatSignPackage(paymentOrderScope, totalFee, shopName, preOrder19DianId, customerId, payMode, payOrderType);
                                        response.ClientWechatPay = dynamicResult.value;
                                        response.result = dynamicResult.status;
                                        break;
                                }
                            }
                            #endregion
                        }
                    }
                }
                else
                {
                    response.executedRedEnvelopeAmount = executedRedEnvelopeAmount;
                    response.result = VAResult.VA_FAILED_PREORDER_COUNT_SERVER_NOT_EQUAL_CLIENT;//客户端和服务端计算原价和悠先价不一致
                }



                if (response.result == VAResult.VA_OK)
                {
                    //LogDll.LogManager.WriteLog(LogDll.LogFile.Error, string.Format("时间：{0}，response.result:{1}，preOrder19Dian.isPaid：{2},request.thirdPayAmount：{3}", DateTime.Now.ToString(), response.result, preOrder19Dian.isPaid, request.thirdPayAmount));

                    if (preOrder19Dian.isPaid == 1)
                    {
                        RedEnvelopeManager redEnvelopeManager = new RedEnvelopeManager();
                        List<RedEnvelopeConnOrder3> connPreOrder = redEnvelopeManager.SelectRedEnvelopeConnPreOrder3(preOrder19Dian.preOrder19dianId);
                        double redEnvelopePayAmount = 0;

                        if (connPreOrder != null && connPreOrder.Count > 0)
                        {
                            redEnvelopePayAmount = connPreOrder.Sum(p => p.currectUsedAmount);
                        }
                        #region 记录支付明细数据
                        if (request.thirdPayAccount == null)
                        {
                            request.thirdPayAccount = string.Empty;
                        }
                        var preorder19DianLineList = new List<Preorder19DianLine>();
                        Preorder19DianLine preorder19DianLine = null;
                        int prepaidState = (int)VAPreorderStatus.Prepaid;

                        //第三方支付金额
                        if (request.thirdPayAmount > 0)
                        {
                            preorder19DianLine = new Preorder19DianLine()
                            {
                                Uuid = request.uuid,
                                CustomerId = customerId,
                                PayType = request.thirdPayType,
                                PayAccount = request.thirdPayAccount,
                                CreateTime = DateTime.Now,
                                Preorder19DianId = preOrder19Dian.preOrder19dianId,
                                State = prepaidState,
                                Amount = request.thirdPayAmount,
                                Remark = string.Empty,
                                RefundAmount = 0
                            };
                            preorder19DianLineList.Add(preorder19DianLine);
                        }
                        //抵扣券支付金额
                        if (realDeductionAmount > 0)
                        {
                            preorder19DianLine = new Preorder19DianLine()
                            {
                                Uuid = request.uuid,
                                CustomerId = customerId,
                                PayType = (int)VAOrderUsedPayMode.COUPON,
                                PayAccount = request.thirdPayAccount,
                                CreateTime = DateTime.Now,
                                Preorder19DianId = preOrder19Dian.preOrder19dianId,
                                State = prepaidState,
                                Amount = realDeductionAmount,
                                Remark = string.Empty,
                                RefundAmount = 0
                            };
                            preorder19DianLineList.Add(preorder19DianLine);
                        }
                        //红包支付金额
                        if (redEnvelopePayAmount > 0)
                        {
                            preorder19DianLine = new Preorder19DianLine()
                            {
                                Uuid = request.uuid,
                                CustomerId = customerId,
                                PayType = (int)VAOrderUsedPayMode.REDENVELOPE,
                                PayAccount = request.thirdPayAccount,
                                CreateTime = DateTime.Now,
                                Preorder19DianId = preOrder19Dian.preOrder19dianId,
                                State = prepaidState,
                                Amount = redEnvelopePayAmount,//当前点单使用红包抵扣的金额
                                Remark = string.Empty,
                                RefundAmount = 0
                            };
                            preorder19DianLineList.Add(preorder19DianLine);
                        }

                        //粮票支付金额,二次支付的粮票支付不记录数据库
                        if (rationBalancePayment > request.thirdPayAmount)
                        {
                            var preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                            {
                                CustomerId = customerId
                            };
                            var rationBalancePreorder19DianLine
                                = Preorder19DianLineOperate.GetFirstByQuery(preorder19DianLineQueryObject, Preorder19DianLineOrderColumn.CreateTime);
                            preorder19DianLine = new Preorder19DianLine()
                            {
                                Uuid = request.uuid,
                                CustomerId = customerId,
                                PayType = (int)VAOrderUsedPayMode.BALANCE,
                                PayAccount = rationBalancePreorder19DianLine != null ? rationBalancePreorder19DianLine.PayAccount : string.Empty,
                                CreateTime = DateTime.Now,
                                Preorder19DianId = preOrder19Dian.preOrder19dianId,
                                State = prepaidState,
                                Amount = rationBalancePayment - request.thirdPayAmount,
                                Remark = string.Empty,
                                RefundAmount = 0
                            };
                            preorder19DianLineList.Add(preorder19DianLine);
                        }
                        Preorder19DianLineOperate.AddList(preorder19DianLineList);


                        #endregion

                        //当天已经用该支付ID付款过含红包点单
                        if (redEnvelopePayAmount > 0 && request.boolDualPayment)
                        {
                            var preorder19DianLineQueryObject = new Preorder19DianLineQueryObject()
                            {
                                CreateTimeFrom = DateTime.Today,
                                PayType = (int)VAOrderUsedPayMode.REDENVELOPE,
                                PayAccount = request.thirdPayAccount,
                                IsRefoundOut = true
                            };
                            if (Preorder19DianLineOperate.GetCountByQuery(preorder19DianLineQueryObject) > 1)
                            {
                                PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
                                var originalRefundRequest = new VAOriginalRefundRequest()
                                {
                                    preOrder19dianId = request.preOrderId,
                                    cookie = request.cookie,
                                    cityId = request.cityId,
                                    uuid = request.uuid,
                                    appType = request.appType,
                                    type = VAMessageType.CLIENT_ORIGINAL_REFUNF_REQUEST,
                                    clientBuild = request.clientBuild,
                                    employeeId = 29
                                };
                                var originalRefundResponse = preOrder19dianOperate.ClientOriginalRefund(originalRefundRequest);
                            }
                        }
                    }
                    ModifyPreorderAndDishCount(request.shopId, "");//统计菜品
                }
            }
            else
            {
                response.result = checkResult.result;
            }
            response.payModeList = payModeList;
            return response;
        }

        public PayDiffenenceResponse ClinetPayDiffenence(PayDiffenenceRequest request)
        {
            var response = new PayDiffenenceResponse();
            response.type = VAMessageType.CLIENT_PAY_DIFFERENCE_RESPONSE;
            if (request.payDeifference <= 0)
            {
                response.result = VAResult.VA_NOT_ALLOW_ZERO_PAY;
                return response;
            }
            var customerManager = new CustomerManager();
            RedEnvelopeOperate redEnvelopeOperate = new RedEnvelopeOperate();
            CheckCookieAndMsgtypeInfo checkResult =
                Common.CheckCookieAndMsgtype(request.cookie, request.uuid, (int)request.type, (int)VAMessageType.CLIENT_PAY_DIFFERENCE_REQUEST, false);
            long customerId = long.Parse(checkResult.dtCustomer.Rows[0]["CustomerID"].ToString());
            customerManager.UpdateCustomerDefaultPayment(request.preOrderPayMode, request.cookie);
            bool canRedEnvelopePay = false;
            double rationBalancePayment = 0;
            var customer = customerManager.GetModelOfId(customerId);
            var order = OrderOperate.GetEntityById(request.orderId);
            var shop = new ShopOperate().GetShop(order.ShopId);
            if (order == null)
            {
                response.result = VAResult.VA_PAY_FAIL;
                return response;
            }
            var preorder19DianOperate = new PreOrder19dianOperate();
            var preOrder19dianInfos = preorder19DianOperate.GetPreOrder19dianByOrderId(request.orderId);
            PreOrder19dianInfo preOrder19dianInfo = null;
            if (request.preorderId <= 0 && preOrder19dianInfos.Count(p => p.isPaid != 1 && p.OrderType == OrderTypeEnum.PayDifference) == 0)//无未支付单子
            {
                preOrder19dianInfo = new PreOrder19dianInfo()
                {
                    customerId = customerId,
                    companyId = shop.companyID,
                    shopId = order.ShopId,
                    preOrderTime = DateTime.Now,
                    menuId = 0,
                    status = VAPreorderStatus.Uploaded,
                    customerUUID = request.uuid,
                    preOrderSum = request.payDeifference,//直接支付金额(缺少安全校验 >0)
                    orderInJson = string.Empty,
                    sundryJson = string.Empty,
                    deskNumber = string.Empty,
                    appType = (int)request.appType,
                    appBuild = request.clientBuild,
                    OrderId = order.Id,
                    OrderType = OrderTypeEnum.PayDifference,
                    prePaidSum = request.payDeifference,
                    isPaid = 0,
                    isEvaluation = 0,
                    isShopConfirmed = 0,
                    refundMoneyClosedSum = 0,
                    verifiedSaving = 0,
                    invoiceTitle = string.Empty,
                    preOrderServerSum = request.payDeifference,
                    isApproved = 0,
                    refundMoneySum = 0,
                    refundRedEnvelope = 0,
                    discount = 1
                };
                response.preorderId = preorder19DianOperate.AddPreOrder19Dian(preOrder19dianInfo); //新增点单持久化到DB
                if (response.preorderId <= 0)
                {
                    response.result = VAResult.VA_FAILED_DB_ERROR;
                    return response;
                }
            }
            else
            {
                preOrder19dianInfo = preOrder19dianInfos.FirstOrDefault(p => p.isPaid != 1 && p.OrderType == OrderTypeEnum.PayDifference);
                preOrder19dianInfo.preOrderServerSum = request.payDeifference;
                preOrder19dianInfo.preOrderSum = request.payDeifference;
                preOrder19dianInfo.prePaidSum = request.payDeifference;
                preOrder19dianInfo.appType = (int)request.appType;
                preOrder19dianInfo.appBuild = request.clientBuild;
                preOrder19dianInfo.isPaid = 0;
                preOrder19dianInfo.isShopConfirmed = 0;
                new PreOrder19dianManager().UpdatePreOrder19dian(preOrder19dianInfo);

            }
            response.preorderId = preOrder19dianInfo.preOrder19dianId;
            double executedRedEnvelopeAmount = 0;
            double balance = Common.ToDouble(checkResult.dtCustomer.Rows[0]["money19dianRemained"]);//用户粮票; 
            if (shop.isSupportRedEnvelopePayment && CheckIsUsedRedEnvelopeToday(customerId, request.uuid) == false)
            {
                canRedEnvelopePay = true;
                executedRedEnvelopeAmount = redEnvelopeOperate.QueryCustomerExcutedRedEnvelope(customer.mobilePhoneNumber);
            }
            double needPayAmount = Common.ToDouble(request.payDeifference - executedRedEnvelopeAmount - balance);
            DateTime expireTime = Convert.ToDateTime("1970-1-1");
            if (needPayAmount <= 0)
            {
                rationBalancePayment = 0;
                bool changeMoneyResult = true;
                bool modifyRedEnvelope = false;
                bool batchRedEnvelopeConnPreOrder = false;
                redEnvelopeOperate.DoRedEnvelopePaymentLogic(request.cookie, executedRedEnvelopeAmount, request.payDeifference,
                    customer.mobilePhoneNumber, response.preorderId, canRedEnvelopePay, 0, ref rationBalancePayment, ref modifyRedEnvelope,
                    ref batchRedEnvelopeConnPreOrder, ref expireTime);
                if (batchRedEnvelopeConnPreOrder && modifyRedEnvelope)
                {
                    if (rationBalancePayment > 0)
                    {
                        PreOrder19dianOperate.ChangeMoney(response.preorderId, customerId, 0, shop.shopID, rationBalancePayment, ref changeMoneyResult);
                        if (changeMoneyResult == false)
                        {
                            response.result = VAResult.VA_FAILED_DB_ERROR;
                            return response;
                        }
                    }
                }
                preOrder19dianInfo.verifiedSaving = 0;
                preOrder19dianInfo.preOrderServerSum = request.payDeifference;
                preOrder19dianInfo.prePaidSum = request.payDeifference;
                preOrder19dianInfo.isPaid = 1;
                preOrder19dianInfo.status = VAPreorderStatus.Completed;
                preOrder19dianInfo.prePayTime = DateTime.Now;
                preOrder19dianInfo.isShopConfirmed = 1;
                if (new PreOrder19dianManager().UpdatePreOrder19dian(preOrder19dianInfo))
                {
                    order.InvoiceTitle = string.Empty;
                    order.ExpireTime = expireTime;
                    order.PayDifferenceSum = order.PayDifferenceSum + request.payDeifference;
                    order.PrePaidSum = order.PrePaidSum + request.payDeifference;
                    order.PreOrderServerSum = order.PreOrderServerSum + request.payDeifference;
                    OrderOperate.Update(order);
                    response.result = VAResult.VA_OK;

                    RedEnvelopeManager redEnvelopeManager = new RedEnvelopeManager();
                    List<RedEnvelopeConnOrder3> connPreOrder = redEnvelopeManager.SelectRedEnvelopeConnPreOrder3(preOrder19dianInfo.preOrder19dianId);
                    RecordPreorder19DianLine(0, string.Empty, 0, customer.CustomerID, preOrder19dianInfo, rationBalancePayment, 0.00, canRedEnvelopePay, connPreOrder.Sum(p => p.currectUsedAmount));
                    if (response.result == VAResult.VA_OK)
                    {
                        PayOrderSMSRemaid model = new PayOrderSMSRemaid();
                        model.shopId = order.ShopId;
                        model.shopName = shop.shopName;
                        model.amount = order.PayDifferenceSum;
                        model.preOrderId = preOrder19dianInfo.preOrder19dianId;
                        model.customerPhone = customer.mobilePhoneNumber;
                        model.customerId = customerId;
                        model.clientBuild = request.clientBuild;
                        Thread payOrderSMSRemaidThread = new Thread(SMSRemindEmployee);
                        payOrderSMSRemaidThread.Start((object)model);
                    }
                    return response;
                }
                else
                {
                    response.result = VAResult.VA_FAILED_DB_ERROR;
                }
            }
            else
            {
                if (request.preOrderPayMode <= 0)//有支付方式
                {
                    response.result = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH;
                    return response;
                }
                SystemConfigOperate systemOper = new SystemConfigOperate();
                bool payModeValid = systemOper.CheckPayMode(request.preOrderPayMode);
                if (!payModeValid)//支付方式无效
                {
                    response.result = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH_AND_NO_PREORDERPAYMODE;
                    return response;
                }
                response.result = VAResult.VA_FAILED_MONEYREMAINED_NOT_ENOUGH;
                using (TransactionScope paymentOrderScope = new TransactionScope())
                {
                    var payOrderType = VAPayOrderType.PAY_DIFFENENCE;
                    var payMode = (VAClientPayMode)request.preOrderPayMode;
                    ThirdPaymentSign thirdPaymentSignOper = new ThirdPaymentSign();
                    dynamic dynamicResult = null;
                    string shopName = shop.shopName;
                    switch (payMode)
                    {
                        default:
                        case VAClientPayMode.ALI_PAY_PLUGIN://支付宝客户端
                            dynamicResult = thirdPaymentSignOper.AliSignPackage(paymentOrderScope, needPayAmount, shopName, response.preorderId, customerId, (int)payMode, payOrderType);
                            response.alipayOrder = dynamicResult.value;
                            response.result = dynamicResult.status;
                            break;
                        case VAClientPayMode.WECHAT_PAY_PLUGIN://微信支付
                            dynamicResult = thirdPaymentSignOper.WechatSignPackage(paymentOrderScope, needPayAmount, shopName, response.preorderId, customerId, (int)payMode, payOrderType);
                            response.ClientWechatPay = dynamicResult.value;
                            response.result = dynamicResult.status;
                            break;
                    }
                }
            }
            return response;
        }

        public PayDiffenenceQueryResponse ClinetPayDiffenenceQuery(PayDiffenenceQueryRequest request)
        {
            var response = new PayDiffenenceQueryResponse();
            response.type = VAMessageType.CLIENT_PAY_DIFFERENCE_QUERY_RESPONSE;
            CheckCookieAndMsgtypeInfo checkResult =
                Common.CheckCookieAndMsgtype(request.cookie, request.uuid, (int)request.type, (int)VAMessageType.CLIENT_PAY_DIFFERENCE_QUERY_REQUEST, false);
            if (checkResult.result != VAResult.VA_OK)
            {
                response.result = checkResult.result;
                return response;
            }
            PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
            var systemConfigCacheLogic = new SystemConfigCacheLogic();
            var redEnvelopeOperate = new RedEnvelopeOperate();
            var customerOperate = new CustomerOperate();
            var preOrder19dians = preOrder19dianOperate.GetPreOrder19dianByOrderId(request.orderId);
            if (preOrder19dians != null && preOrder19dians.Any())
            {
                var notPayDiffenence = preOrder19dians.FirstOrDefault(p => p.OrderType == OrderTypeEnum.PayDifference && p.isPaid != 1);
                if (notPayDiffenence != null)
                {
                    response.preorderId = notPayDiffenence.preOrder19dianId;
                }
            }
            long customerId = long.Parse(checkResult.dtCustomer.Rows[0]["CustomerID"].ToString());
            var order = OrderOperate.GetEntityById(request.orderId);
            var customer = customerOperate.QueryCustomer(customerId);
            var shop = new ShopOperate().GetShop(order.ShopId);
            response.rationBalance = Common.ToDouble(checkResult.dtCustomer.Rows[0]["money19dianRemained"]);//用户粮票;
            if (shop.isSupportRedEnvelopePayment && CheckIsUsedRedEnvelopeToday(customerId, request.uuid) == false)
            {
                response.executedRedEnvelopeAmount = redEnvelopeOperate.QueryCustomerExcutedRedEnvelope(customer.mobilePhoneNumber);
            }
            if (customer.mobilePhoneNumber.Equals("23588776637"))
            {
                response.payModeList = ((List<VAPayMode>)systemConfigCacheLogic.GetServerPayModel()).FindAll(p => p.payModeId == 1);
            }
            else
            {
                response.payModeList = (List<VAPayMode>)systemConfigCacheLogic.GetServerPayModel();
            }
            response.result = VAResult.VA_OK;
            return response;
        }

        /// <summary>
        /// 第三方支付回调
        /// </summary>
        /// <param name="preorderId"></param>
        /// <param name="payMode"></param>
        /// <param name="payAmount"></param>
        /// <returns></returns>
        public bool ThirdPayDiffenenceQuery(long preorderId, string payAccount, int payMode, double payAmount)
        {
            var preorderOperate = new PreOrder19dianOperate();
            var preorder = preorderOperate.GetPreOrder19dianById(preorderId);
            if (preorder == null)
            {
                return false;
            }
            bool canRedEnvelopePay = false;
            var redEnvelopeOperate = new RedEnvelopeOperate();
            var customer = new CustomerOperate().QueryCustomer(preorder.customerId);
            double executedRedEnvelopeAmount = 0;
            ShopOperate shopOperate = new ShopOperate();
            var shop = shopOperate.GetShop(preorder.shopId);
            if (shop.isSupportRedEnvelopePayment &&
                CheckIsUsedRedEnvelopeToday(preorder.customerId, preorder.customerUUID) == false)
            {
                canRedEnvelopePay = true;
                executedRedEnvelopeAmount = redEnvelopeOperate.QueryCustomerExcutedRedEnvelope(customer.mobilePhoneNumber);
            }
            bool modifyRedEnvelope = false;
            bool batchRedEnvelopeConnPreOrder = false;
            bool changeMoneyResult = false;
            double rationBalancePayment = 0;
            DateTime expireTime = Convert.ToDateTime("1970-1-1");
            redEnvelopeOperate.DoRedEnvelopePaymentLogic(customer.cookie, executedRedEnvelopeAmount, preorder.preOrderServerSum.Value,
                customer.mobilePhoneNumber, preorderId, canRedEnvelopePay, 0, ref rationBalancePayment, ref modifyRedEnvelope,
                ref batchRedEnvelopeConnPreOrder, ref expireTime);
            if (batchRedEnvelopeConnPreOrder && modifyRedEnvelope)
            {
                if (rationBalancePayment > 0)
                {
                    PreOrder19dianOperate.ChangeMoney(preorderId, customer.CustomerID, 0, preorder.shopId, rationBalancePayment, ref changeMoneyResult);
                }
            }
            var order = OrderOperate.GetEntityById(preorder.OrderId);
            order.PayDifferenceSum = order.PayDifferenceSum + preorder.preOrderServerSum.Value;
            order.PreOrderServerSum = order.PreOrderServerSum + preorder.preOrderServerSum.Value;
            order.PrePaidSum = order.PrePaidSum + preorder.preOrderServerSum.Value;
            order.PrePayTime = DateTime.Now;
            RedEnvelopeManager redEnvelopeManager = new RedEnvelopeManager();
            List<RedEnvelopeConnOrder3> connPreOrder = redEnvelopeManager.SelectRedEnvelopeConnPreOrder3(preorderId);
            RecordPreorder19DianLine(payAmount, payAccount, payMode, customer.CustomerID, preorder, rationBalancePayment, 0.00, canRedEnvelopePay, connPreOrder.Sum(p => p.currectUsedAmount));
            preorder.isPaid = 1;
            preorder.prePaidSum = preorder.preOrderServerSum.Value;
            preorder.prePayTime = DateTime.Now;
            PreOrder19dianManager preOrder19dianManager = new PreOrder19dianManager();
            preOrder19dianManager.UpdatePreOrder19dian(preorder);
            preorderOperate.ShopConfrimedPreOrder(preorder.preOrder19dianId, 1, 29, "", "");
            return OrderOperate.Update(order);
        }

        public ClientpaySuccessResponse PaySuccessRequest(ClientPaySuccessRequest request)
        {
            ClientpaySuccessResponse response = new ClientpaySuccessResponse();
            if (request == null)
            {
                response.result = VAResult.VA_FAILED_DB_ERROR;
                return response;
            }
            PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
            var preOrder = preOrder19dianOperate.GetPreOrder19dianById(request.preorderId);
            if (preOrder == null || preOrder.preOrder19dianId == 0)
            {
                response.result = VAResult.VA_FAILED_DB_ERROR;
                return response;
            }
            if (preOrder.isPaid == 1)
            {
                response.result = VAResult.VA_OK;
                return response;
            }
            preOrder.status = VAPreorderStatus.Handling;
            new PreOrder19dianManager().UpdatePreOrder19dian(preOrder);
            response.result = VAResult.VA_OK;
            return response;
        }

    }
}
