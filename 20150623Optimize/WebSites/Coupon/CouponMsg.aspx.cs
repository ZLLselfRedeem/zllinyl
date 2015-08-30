﻿using Org.BouncyCastle.Utilities.Encoders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using VA.CacheLogic;
using VA.CacheLogic.OrderClient;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.Model.Interface;
using VAGastronomistMobileApp.Model.QueryObject;
using VAGastronomistMobileApp.WebPageDll;

/// <summary>
/// 供抵价券相关H5页面调用接口
/// </summary>
public partial class Coupon_CouponMsg : System.Web.UI.Page
{

    public void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["action"]))
            {
                try
                {
                    string action = Request.QueryString["action"].ToString();
                    string result = "";
                    switch (action)
                    {
                        case "coupon_add":
                            int shopId = Common.ToInt32(Request.Form["shopId"]);
                            string couponName = Common.ToString(Request.Form["couponName"]);
                            DateTime startDate = Common.ToDateTime(Request.Form["startDate"] + " 00:00:00");
                            DateTime endDate = Common.ToDateTime(Request.Form["endDate"] + " 23:59:59");
                            double RequirementMoney = Common.ToDouble(Request.Form["RequirementMoney"]);
                            double DeductibleAmount = Common.ToDouble(Request.Form["DeductibleAmount"]);
                            int ValidityPeriod = Common.ToInt32(Request.Form["ValidityPeriod"]);
                            int quantity = Common.ToInt32(Request.Form["quantity"]);
                            int employeeID = Common.ToInt32(Request.Form["employeeId"]);
                            double MaxAmount = Common.ToDouble(Request.Form["MaxAmount"]);

                            result = coupon_add(shopId, couponName, startDate, endDate, RequirementMoney, DeductibleAmount, ValidityPeriod, quantity, employeeID, MaxAmount);
                            Response.Write(result);
                            break;

                        case "query_promotionActivity_list":
                            shopId = Common.ToInt32(Request.Form["shopId"]);
                            int pageSize = Common.ToInt32(Request.Form["pageSize"]);
                            int pageIndex = Common.ToInt32(Request.Form["pageIndex"]);

                            result = query_promotionActivity_list(pageSize, pageIndex, shopId);
                            Response.Write(result);
                            break;

                        case "query_promotionActivity":
                            int activityType = Common.ToInt32(Request.Form["activityType"]);
                            int activityId = Common.ToInt32(Request.Form["activityId"]);

                            result = query_promotionActivity(activityType, activityId);
                            Response.Write(result);
                            break;

                        case "search_coupon":
                            string keyWord = Request.QueryString["keyWord"].Trim();
                            string idEncrypt = Request.QueryString["idEncrypt"].Trim();
                            pageSize = Common.ToInt32(Request.QueryString["pageSize"].Trim());
                            pageIndex = Common.ToInt32(Request.QueryString["pageIndex"].Trim());
                            int? cityId = null;
                            if (string.IsNullOrEmpty(Request.QueryString["cityId"]) == false)
                            {
                                cityId = Common.ToInt32(Request.QueryString["cityId"].Trim());
                            }

                            result = SearchCoupon(keyWord, idEncrypt, pageSize, pageIndex, cityId);
                            Response.Write(result);
                            break;

                        default:
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Response.Write(ex.Message);
                }
            }
        }
    }
    /// <summary>
    /// 领用抵价券接口
    /// </summary>
    /// <param name="couponId"></param>
    /// <param name="preOrder19DianId"></param>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetCoupon(int couponId, string preOrder19DianIdEncrypt, string phoneNumber)
    {
        VAGetCouponResponse response = new VAGetCouponResponse() { phoneNumber = phoneNumber };
        if (string.IsNullOrEmpty(phoneNumber) || phoneNumber.Length != 11)
        {
            response.getState = 0;
            return JsonOperate.JsonSerializer<VAGetCouponResponse>(response);
        }
        long preOrder19DianId = Convert.ToInt64(preOrder19DianIdEncrypt, 16);
        if (preOrder19DianId % 3 > 0)
        {
            response.getState = 0;
            return JsonOperate.JsonSerializer<VAGetCouponResponse>(response);
        }
        else
        {
            preOrder19DianId = preOrder19DianId / 3;
        }
        var list =
              CouponSendDetailOperate.GetListByQuery(new CouponSendDetailQueryObject() { PreOrder19DianId = preOrder19DianId });
        if (list == null || list.Count == 0)
        {
            response.getState = 0;
            return JsonOperate.JsonSerializer<VAGetCouponResponse>(response);
        }

        if (list[0].SendCount >= list[0].TotalCount || list[0].ValidityEnd <= DateTime.Now)
        {
            response.getState = 2;
            return JsonOperate.JsonSerializer<VAGetCouponResponse>(response);
        }
        var coupon = CouponOperate.GetEntityById(couponId);
        if (CouponGetDetailOperate.GetCountByQuery(new CouponGetDetailQueryObject() { SharePreOrder19DianId = preOrder19DianId, MobilePhoneNumber = phoneNumber }) > 0)
        {
            response.getState = 3;
            return JsonOperate.JsonSerializer<VAGetCouponResponse>(response);
        }
        if (coupon != null)
        {
            if (coupon.SheetNumber <= coupon.SendCount || coupon.EndDate <= DateTime.Now || coupon.CouponType == 2)
            {
                response.getState = 2;
                return JsonOperate.JsonSerializer<VAGetCouponResponse>(response);
            }
            CouponGetDetail couponGetDetail = new CouponGetDetail()
            {
                CouponId = couponId,
                GetTime = DateTime.Now,
                ValidityEnd = DateTime.Today.AddDays(coupon.ValidityPeriod).AddDays(1).AddSeconds(-1),
                RequirementMoney = coupon.RequirementMoney,
                DeductibleAmount = coupon.DeductibleAmount,
                State = 1,
                IsCorrected = false,
                MobilePhoneNumber = phoneNumber,
                CouponDetailNumber = phoneNumber.Substring(7, 4) + VAGastronomistMobileApp.WebPageDll.Common.ToSecondFrom1970(DateTime.Now).ToString(),
                SharePreOrder19DianId = preOrder19DianId
            };
            if (CouponGetDetailOperate.Add(couponGetDetail))
            {
                response.getState = 1;
                response.CouponGetDetailId = couponGetDetail.CouponGetDetailID;
                if (list != null && list.Count > 0)
                {
                    list[0].SendCount = list[0].SendCount + 1;
                    CouponSendDetailOperate.Update(list[0]);
                }
                long getCount = CouponGetDetailOperate.GetCountByQuery(new CouponGetDetailQueryObject() { CouponId = couponId });
                if (getCount >= coupon.SheetNumber)
                {
                    coupon.SendCount = coupon.SheetNumber;
                    coupon.IsGot = true;
                    coupon.IsDisplay = false;
                    CouponOperate.Update(coupon);
                }
                else if (getCount >= coupon.SendCount + 10)
                {
                    coupon.SendCount = (int)getCount;
                    CouponOperate.Update(coupon);
                }

                try
                {
                    CustomPushRecordOperate customPushRecordOperate = new CustomPushRecordOperate();
                    customPushRecordOperate.SendPushAfterGetCoupon(phoneNumber, couponGetDetail.CouponId);
                }
                catch
                {

                }
            }
            else
            {
                response.getState = 0;
            }
        }
        else
        {
            response.getState = 0;
        }
        return JsonOperate.JsonSerializer<VAGetCouponResponse>(response);
    }

    [WebMethod]
    public static string GetCouponDetail(string preOrder19DianIdEncrypt, string phoneNumber)
    {

        long preOrder19DianId = Convert.ToInt64(preOrder19DianIdEncrypt, 16) / 3;
        var queryObject = new CouponGetDetailQueryObject() { SharePreOrder19DianId = preOrder19DianId, MobilePhoneNumber = phoneNumber };
        var couponGetDetail =
            CouponGetDetailOperate.GetListByQuery(queryObject).FirstOrDefault();
        var response = new VACouponGetDetailResponse();
        if (couponGetDetail == null)
        {
            queryObject = new CouponGetDetailQueryObject() { CouponSendDetailID = preOrder19DianId, MobilePhoneNumber = phoneNumber };
            couponGetDetail =
                CouponGetDetailOperate.GetListByQuery(queryObject).FirstOrDefault();
        }
        if (couponGetDetail != null)
        {
            response.couponName = couponGetDetail.DeductibleAmount.ToString();
            var couponV = CouponVOperate.GetEntityById(couponGetDetail.CouponId);
            response.shopName = couponV.ShopName;
            response.IsCorrected = couponGetDetail.IsCorrected;
            response.couponGetDetailId = couponGetDetail.CouponGetDetailID;
            response.dishList = new List<DishDetail>();
            DishOperate dishOperate = new DishOperate();
            DishDetail dishDetail = null;
            string imageFormat = WebConfig.CdnDomain + WebConfig.ImagePath + "{0}{1}" + "@120w_90h_1e_1c";
            var dishTable = dishOperate.GetDishTableByShopIdAndPrice(couponV.ShopId, couponGetDetail.DeductibleAmount, 20);
            if (dishTable != null && dishTable.Rows.Count > 0)
            {
                foreach (DataRow row in dishTable.Rows)
                {
                    double price = double.Parse(row["DishPrice"].ToString());
                    int dishId = int.Parse(row["dishId"].ToString());
                    if (response.dishList.Exists(p => p.dishId == dishId))
                    {
                        continue;
                    }
                    dishDetail = new DishDetail()
                    {
                        dishName = row["DishName"].ToString(),
                        dishImage = string.Format(imageFormat, row["menuImagePath"], row["ImageName"]),
                        nowPrice = Math.Round((price - couponGetDetail.DeductibleAmount), 2),
                        originalPrice = Math.Round(price, 2),
                        dishId = dishId
                    };
                    response.dishList.Add(dishDetail);
                    if (response.dishList.Count >= 10)
                    {
                        break;
                    }
                }
            }
            else if (dishTable == null || dishTable.Rows.Count == 0)
            {
                dishTable = dishOperate.GetDishTableByShopId(couponV.ShopId, 10);
                if (dishTable != null)
                {
                    foreach (DataRow row in dishTable.Rows)
                    {
                        double price = double.Parse(row["DishPrice"].ToString());
                        int dishId = int.Parse(row["dishId"].ToString());
                        dishDetail = new DishDetail()
                        {
                            dishName = row["DishName"].ToString(),
                            dishImage = string.Format(imageFormat, row["menuImagePath"], row["ImageName"]),
                            nowPrice = price > couponGetDetail.DeductibleAmount ? Math.Round((price - couponGetDetail.DeductibleAmount), 2) : 0,
                            originalPrice = Math.Round(price, 2),
                            dishId = dishId
                        };
                        response.dishList.Add(dishDetail);
                    }
                }
            }
        }
        return JsonOperate.JsonSerializer<VACouponGetDetailResponse>(response);
    }
    /// <summary>
    /// 分享抵价券
    /// </summary>
    /// <param name="preOrder19DianId"></param>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    [WebMethod]
    public static string ShareCoupon(string preOrder19DianIdEncrypt, string phoneNumber, int pageSize, int pageIndex, int displayType, double longitude, double latitude)
    {
        long preOrder19DianId = Convert.ToInt64(preOrder19DianIdEncrypt, 16);
        VAShareCouponResponse response = new VAShareCouponResponse();
        CouponCacheLogic couponCacheLogic = new CouponCacheLogic();
        response.shareText = couponCacheLogic.GetCouponShareText();
        response.shareImage = WebConfig.CdnDomain + WebConfig.ImagePath + "15/Coupon/" + couponCacheLogic.GetCouponShareImage();
        response.shareTitle = "悠先点菜 吃de愉快";
        if (preOrder19DianId % 3 > 0)
        {
            response.shareState = 2;
            return JsonOperate.JsonSerializer<VAShareCouponResponse>(response);
        }
        else
        {
            preOrder19DianId = preOrder19DianId / 3;
        }
        PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
        var preOrder19dian = preOrder19dianOperate.GetPreOrder19dianById(preOrder19DianId);
        //不存在，或是未支付的单子不允许分享
        if (preOrder19dian == null || preOrder19dian.prePayTime.HasValue == false)
        {
            response.shareState = 2;
            return JsonOperate.JsonSerializer<VAShareCouponResponse>(response);
        };
        CouponSendDetailQueryObject queryObject = new CouponSendDetailQueryObject() { PreOrder19DianId = preOrder19DianId };
        var list = CouponSendDetailOperate.GetListByQuery(queryObject);
        CouponSendDetail couponSendDetail = null;
        if (list == null || list.Count == 0)
        {
            couponSendDetail = new CouponSendDetail()
            {
                PreOrder19DianId = preOrder19DianId,
                TotalCount = SystemConfigOperate.GetCouponCount(),
                SendCount = 0,
                ValidityEnd = preOrder19dian.prePayTime.Value.AddDays(SystemConfigOperate.GetCouponValidDate()),
                CreateTime = DateTime.Now,
                ShareType = 1
            };
            CouponSendDetailOperate.Add(couponSendDetail);
        }
        else
        {
            couponSendDetail = list[0];
        }
        if (string.IsNullOrEmpty(phoneNumber))
        {
            response.isGot = false;
        }
        else
        {
            CouponGetDetailQueryObject couponGetDetailQueryObject
                = new CouponGetDetailQueryObject() { MobilePhoneNumber = phoneNumber, SharePreOrder19DianId = preOrder19DianId };
            if (CouponGetDetailOperate.GetCountByQuery(couponGetDetailQueryObject) > 0)
            {
                response.isGot = true;
            }
            else
            {
                response.isGot = false;
            }
        }
        if (couponSendDetail.ValidityEnd < DateTime.Now)
        {
            response.shareState = 3;
        }
        else if (couponSendDetail.SendCount >= couponSendDetail.TotalCount)
        {
            response.shareState = 2;
        }
        else
        {
            response.shareState = 1;
        }
        if (response.isGot == false)
        {
            ShopOperate shopOperate = new ShopOperate();
            var shopInfo = shopOperate.QueryShop(preOrder19dian.shopId);
            if (response.shareState == 1)
            {
                var couponQueryObject = new CouponQueryObject()
                                        {
                                            State = 1,
                                            CityId = shopInfo.cityID,
                                            StartDateTo = DateTime.Now,
                                            EndDateFrom = DateTime.Now,
                                            Longitude = null,
                                            Latitude = null,
                                            IsDisplay = true
                                        };
                List<ICoupon> couponList = null;

                switch (displayType)
                {
                    case 1:
                        couponList = CouponOperate.GetListByQuery(pageSize, pageIndex, couponQueryObject, CouponOrderColumn.DeductibleProportion);
                        break;
                    case 2:
                        couponList = CouponOperate.GetListByQuery(pageSize, pageIndex, couponQueryObject, CouponOrderColumn.SendCount);
                        break;
                    case 3:

                        var location = Common.GetBaiduMapCoordinateFromGps(longitude, latitude);
                        couponQueryObject.Latitude = location.lat;
                        couponQueryObject.Longitude = location.lng;
                        couponList = CouponOperate.GetListByDistanceOrder(pageSize, pageIndex, couponQueryObject);
                        couponQueryObject.Latitude = null;
                        couponQueryObject.Longitude = null;
                        break;
                    default:
                        break;
                }

                if (couponList == null)
                {
                    couponList = new List<ICoupon>();
                }
                if (CouponOperate.GetCountByQuery(couponQueryObject) > (pageSize * pageIndex))
                {
                    response.isMore = true;
                }
                else
                {
                    response.isMore = false;
                }

                response.couponList = (from e in couponList
                                       select
                                           new CouponDetail()
                                           {
                                               couponId = e.CouponId,
                                               couponName = string.Format("满{0}减{1}", e.RequirementMoney, e.DeductibleAmount),
                                               shopId = e.ShopId.HasValue ? e.ShopId.Value : 0,
                                               shopName = e.ShopName.Trim(),
                                               shopLogo = WebConfig.CdnDomain + WebConfig.ImagePath + e.Image.Trim() + "@298w_140h_80Q_1e_1c",
                                               shopAddress = e.ShopAddress.Trim(),
                                               isGot = e.IsGot
                                           }).ToList();
            }
        }
        if (response.couponList == null)
        {
            response.couponList = new List<CouponDetail>();
        }
        return JsonOperate.JsonSerializer<VAShareCouponResponse>(response);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="activityIdEncrypt"></param>
    /// <param name="cityId"></param>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <param name="displayType"></param>
    /// <param name="longitude"></param>
    /// <param name="latitude"></param>
    /// <returns></returns>
    [WebMethod]
    public static string ShareSystemCoupon(string activityIdEncrypt, int cityId, int pageSize, int pageIndex,
                                        int displayType, double? longitude = null, double? latitude = null, string phoneNumber = "")
    {

        int activityId = Convert.ToInt32(activityIdEncrypt, 16);
        VAShareCouponResponse response = new VAShareCouponResponse();
        CouponCacheLogic couponCacheLogic = new CouponCacheLogic();
        response.shareText = couponCacheLogic.GetCouponShareText();
        response.shareImage = WebConfig.CdnDomain + WebConfig.ImagePath + "15/Coupon/" + couponCacheLogic.GetCouponShareImage();
        response.shareTitle = "悠先点菜 吃de愉快";

        if (displayType == 3 && (longitude == null || latitude == null))
        {
            response.shareState = 2;
            return JsonOperate.JsonSerializer<VAShareCouponResponse>(response);
        }

        if (activityId % 3 > 0)
        {
            response.shareState = 2;
            return JsonOperate.JsonSerializer<VAShareCouponResponse>(response);
        }
        else
        {
            activityId = activityId / 3;
        }

        var couponSendDetail = CouponSendDetailOperate.GetEntityById(activityId);
        if (couponSendDetail == null)
        {
            response.shareState = 2;
            return JsonOperate.JsonSerializer<VAShareCouponResponse>(response);
        }
        if (couponSendDetail.ValidityEnd < DateTime.Now)
        {
            response.shareState = 3;
        }
        else if (couponSendDetail.SendCount >= couponSendDetail.TotalCount)
        {
            response.shareState = 2;
        }
        else
        {
            response.shareState = 1;
        }

        if (string.IsNullOrEmpty(phoneNumber) == false)
        {
            int systemCouponGetInterval = int.Parse(new SystemConfigCacheLogic().GetSystemConfig("SystemCouponGetInterval", "3600"));
            if (CouponGetDetailOperate.GetCountByQuery(new CouponGetDetailQueryObject()
                                {
                                    GetTimeFrom = DateTime.Now.AddSeconds(0 - systemCouponGetInterval),
                                    CouponSendDetailID = activityId,
                                    MobilePhoneNumber = phoneNumber
                                }) > 0)
            {
                response.isGot = true;
            }
        }

        if (response.isGot == false)
        {
            ShopOperate shopOperate = new ShopOperate();

            if (response.shareState == 1)
            {
                var couponQueryObject = new CouponQueryObject()
                {
                    State = 1,
                    CityId = cityId,
                    StartDateTo = DateTime.Now,
                    EndDateFrom = DateTime.Now,
                    Longitude = null,
                    Latitude = null,
                    IsDisplay = true,
                    IsGiftCoupon = true
                };
                List<ICoupon> couponList = null;

                switch (displayType)
                {
                    case 1:
                        couponList = CouponOperate.GetListByQuery(pageSize, pageIndex, couponQueryObject, CouponOrderColumn.DeductibleProportion);
                        break;
                    case 2:
                        couponList = CouponOperate.GetListByQuery(pageSize, pageIndex, couponQueryObject, CouponOrderColumn.SendCount);
                        break;
                    case 3:
                        var location = Common.GetBaiduMapCoordinateFromGps(longitude.Value, latitude.Value);
                        couponQueryObject.Latitude = location.lat;
                        couponQueryObject.Longitude = location.lng;
                        couponList = CouponOperate.GetListByDistanceOrder(pageSize, pageIndex, couponQueryObject);

                        couponQueryObject.Latitude = null;
                        couponQueryObject.Longitude = null;
                        break;
                    default:
                        break;
                }

                if (couponList == null)
                {
                    couponList = new List<ICoupon>();
                }
                if (CouponOperate.GetCountByQuery(couponQueryObject) > (pageSize * pageIndex))
                {
                    response.isMore = true;
                }
                else
                {
                    response.isMore = false;
                }

                response.couponList = (from e in couponList
                                       select
                                           new CouponDetail()
                                           {
                                               couponId = e.CouponId,
                                               couponName = string.Format("满{0}减{1}", e.RequirementMoney, e.DeductibleAmount),
                                               shopId = e.ShopId.HasValue ? e.ShopId.Value : 0,
                                               shopName = e.ShopName.Trim(),
                                               shopLogo = WebConfig.CdnDomain + WebConfig.ImagePath + e.Image.Trim() + "@298w_140h_80Q_1e_1c",
                                               shopAddress = e.ShopAddress.Trim(),
                                               isGot = e.IsGot
                                           }).ToList();
            }
        }
        if (response.couponList == null)
        {
            response.couponList = new List<CouponDetail>();
        }
        return JsonOperate.JsonSerializer<VAShareCouponResponse>(response);
    }

    /// <summary>
    /// 领取系统分享抵扣券
    /// </summary>
    /// <param name="couponId"></param>
    /// <param name="activityIdEncrypt"></param>
    /// <param name="phoneNumber"></param>
    /// <returns></returns>
    [WebMethod]
    public static string GetSystemCoupon(int couponId, string activityIdEncrypt, string phoneNumber)
    {
        VAGetCouponResponse response = new VAGetCouponResponse() { phoneNumber = phoneNumber };
        if (string.IsNullOrEmpty(phoneNumber) || phoneNumber.Length != 11)
        {
            response.getState = 0;
            return JsonOperate.JsonSerializer<VAGetCouponResponse>(response);
        }
        int activityId = Convert.ToInt32(activityIdEncrypt, 16);
        if (activityId % 3 > 0)
        {
            response.getState = 0;
            return JsonOperate.JsonSerializer<VAGetCouponResponse>(response);
        }
        else
        {
            activityId = activityId / 3;
        }
        var couponSendDetail =
              CouponSendDetailOperate.GetEntityById(activityId);
        if (couponSendDetail == null)
        {
            response.getState = 0;
            return JsonOperate.JsonSerializer<VAGetCouponResponse>(response);
        }

        if (couponSendDetail.ValidityEnd <= DateTime.Now)
        {
            response.getState = 2;
            return JsonOperate.JsonSerializer<VAGetCouponResponse>(response);
        }
        var coupon = CouponOperate.GetEntityById(couponId);
        int systemCouponGetInterval = int.Parse(new SystemConfigCacheLogic().GetSystemConfig("SystemCouponGetInterval", "3600"));
        var queryObject = new CouponGetDetailQueryObject()
        {
            CouponSendDetailID = activityId,
            GetTimeFrom = DateTime.Now.AddSeconds(0 - systemCouponGetInterval),
            MobilePhoneNumber = phoneNumber
        };

        if (CouponGetDetailOperate.GetCountByQuery(queryObject) > 0)
        {
            response.getState = 3;
            return JsonOperate.JsonSerializer<VAGetCouponResponse>(response);
        }
        if (coupon != null)
        {
            if (coupon.SheetNumber <= coupon.SendCount || coupon.EndDate <= DateTime.Now || coupon.CouponType == 2)
            {
                response.getState = 2;
                return JsonOperate.JsonSerializer<VAGetCouponResponse>(response);
            }
            CouponGetDetail couponGetDetail = new CouponGetDetail()
            {
                CouponId = couponId,
                GetTime = DateTime.Now,
                ValidityEnd = DateTime.Today.AddDays(coupon.ValidityPeriod).AddDays(1).AddSeconds(-1),
                RequirementMoney = coupon.RequirementMoney,
                DeductibleAmount = coupon.DeductibleAmount,
                State = 1,
                IsCorrected = false,
                MobilePhoneNumber = phoneNumber,
                CouponDetailNumber = phoneNumber.Substring(7, 4) + VAGastronomistMobileApp.WebPageDll.Common.ToSecondFrom1970(DateTime.Now).ToString(),
                SharePreOrder19DianId = 0,
                CouponSendDetailID = activityId,

            };
            if (CouponGetDetailOperate.Add(couponGetDetail))
            {
                response.getState = 1;
                response.CouponGetDetailId = couponGetDetail.CouponGetDetailID;
                //if (couponSendDetail != null )
                //{
                //    couponSendDetail.SendCount = couponSendDetail.SendCount + 1;
                //    CouponSendDetailOperate.Update(couponSendDetail);
                //}
                long getCount = CouponGetDetailOperate.GetCountByQuery(new CouponGetDetailQueryObject() { CouponId = couponId });
                if (getCount >= coupon.SheetNumber)
                {
                    coupon.SendCount = coupon.SheetNumber;
                    coupon.IsGot = true;
                    coupon.IsDisplay = false;
                    CouponOperate.Update(coupon);
                }
                else if (getCount >= coupon.SendCount + 10)
                {
                    coupon.SendCount = (int)getCount;
                    CouponOperate.Update(coupon);
                }

                CustomPushRecordOperate customPushRecordOperate = new CustomPushRecordOperate();
                customPushRecordOperate.SendPushAfterGetCoupon(phoneNumber, couponGetDetail.CouponId);
            }
            else
            {
                response.getState = 0;
            }
        }
        else
        {
            response.getState = 0;
        }
        return JsonOperate.JsonSerializer<VAGetCouponResponse>(response);
    }

    [WebMethod]
    public static string CorrectNumber(long couponGetDetailId, string phoneNumber)
    {
        VACorrectCouponResponse response = new VACorrectCouponResponse();
        var entity = CouponGetDetailOperate.GetEntityById(couponGetDetailId);
        if (entity.IsCorrected == true)
        {
            response.correctState = 3;
            return JsonOperate.JsonSerializer<VACorrectCouponResponse>(response);
        }
        if (entity != null && entity.IsCorrected == false)
        {

            var queryObject = new CouponGetDetailQueryObject() { MobilePhoneNumber = phoneNumber };
            if (entity.SharePreOrder19DianId != 0)
            {
                queryObject.SharePreOrder19DianId = entity.SharePreOrder19DianId;
            }
            else
            {
                queryObject.CouponSendDetailID = entity.CouponGetDetailID;
                int systemCouponGetInterval = int.Parse(new SystemConfigCacheLogic().GetSystemConfig("SystemCouponGetInterval", "3600"));
                queryObject.GetTimeFrom = DateTime.Now.AddSeconds(0 - systemCouponGetInterval);

            }
            if (CouponGetDetailOperate.GetCountByQuery(queryObject) > 0)
            {
                response.correctState = 2;
                return JsonOperate.JsonSerializer<VACorrectCouponResponse>(response);
            }
            entity.IsCorrected = true;
            entity.OriginalNumber = entity.MobilePhoneNumber;
            entity.MobilePhoneNumber = phoneNumber;
            entity.CorrectTime = DateTime.Now;
            if (CouponGetDetailOperate.Update(entity))
            {
                response.correctState = 1;
            }
            else
            {
                response.correctState = 0;
            }
        }
        else
        {
            response.correctState = 0;
        }
        return JsonOperate.JsonSerializer<VACorrectCouponResponse>(response);
    }

    [WebMethod]
    public static string SearchShopName(string keyWord, string idEncrypt, int pageSize, int pageIndex, int? cityId = null)
    {
        long id = Convert.ToInt64(idEncrypt, 16);
        if (id % 3 > 0)
        {
            return string.Empty;
        }
        else
        {
            id = id / 3;
        }
        PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
        ShopOperate shopOperate = new ShopOperate();
        CouponQueryObject queryObject = new CouponQueryObject()
            {
                State = 1,
                StartDateTo = DateTime.Now,
                EndDateFrom = DateTime.Now,
                Longitude = null,
                Latitude = null,
                IsDisplay = true
            };
        if (cityId == null || cityId == 0)
        {
            var preOrder19dian = preOrder19dianOperate.GetPreOrder19dianById(id);
            if (preOrder19dian == null)
            {
                return string.Empty;
            }
            var shop = shopOperate.QueryShop(preOrder19dian.shopId);
            if (shop.shopID <= 0)
            {
                return string.Empty;
            }
            queryObject.CityId = shop.cityID;
        }
        else
        {
            queryObject.CityId = cityId;
        }
        var list = CouponOperate.SearchShopWithCouponByKeyWord(pageSize, pageIndex, keyWord, queryObject);
        if (list == null || list.Any() == false)
        {
            return string.Empty;
        }
        var shopNameList = (from e in list
                            select e.shopName).ToList();
        return JsonOperate.JsonSerializer<List<string>>(shopNameList);
    }

    public static string SearchCoupon(string keyWord, string idEncrypt, int pageSize, int pageIndex, int? cityId = null)
    {
        VASearchCouponResponse response = new VASearchCouponResponse();
        CouponQueryObject queryObject = new CouponQueryObject()
        {
            State = 1,
            StartDateTo = DateTime.Now,
            EndDateFrom = DateTime.Now,
            Longitude = null,
            Latitude = null,
            IsDisplay = true,
            ShopNameFuzzy = keyWord
        };
        long id = Convert.ToInt64(idEncrypt, 16);
        SybMsg2 sybMsg = new SybMsg2();
        if (id % 3 > 0)
        {
            sybMsg.Insert(-1, "ID错误");
            return sybMsg.Value;
        }
        else
        {
            id = id / 3;
        }
        PreOrder19dianOperate preOrder19dianOperate = new PreOrder19dianOperate();
        ShopOperate shopOperate = new ShopOperate();
        if (cityId == null || cityId == 0)
        {
            var preOrder19dian = preOrder19dianOperate.GetPreOrder19dianById(id);
            if (preOrder19dian == null)
            {
                return JsonOperate.JsonSerializer<VASearchCouponResponse>(response);
            }
            var shop = shopOperate.QueryShop(preOrder19dian.shopId);
            if (shop.shopID <= 0)
            {
                return JsonOperate.JsonSerializer<VASearchCouponResponse>(response);
            }
            queryObject.CityId = shop.cityID;
        }
        else
        {
            queryObject.CityId = cityId;
        }
        var list = CouponOperate.GetListByQuery(pageSize, pageIndex, queryObject);

        if (list == null)
        {
            list = new List<ICoupon>();
        }
        response.couponList = (from e in list
                               select
                                   new CouponDetail()
                                   {
                                       couponId = e.CouponId,
                                       couponName = string.Format("满{0}减{1}", e.RequirementMoney, e.DeductibleAmount),
                                       shopId = e.ShopId.HasValue ? e.ShopId.Value : 0,
                                       shopName = e.ShopName.Trim(),
                                       shopLogo = WebConfig.CdnDomain + WebConfig.ImagePath + e.Image.Trim() + "@298w_140h_80Q_1e_1c",
                                       shopAddress = e.ShopAddress.Trim(),
                                       isGot = e.IsGot
                                   }).ToList();
        if (CouponOperate.GetCountByQuery(queryObject) > (pageSize * pageIndex))
        {
            response.isMore = true;
        }
        else
        {
            response.isMore = false;
        }
        sybMsg.Insert(0, response);
        return sybMsg.Value;
    }




    #region 悠先服务抵扣券

    /// <summary>
    /// 增加抵扣券
    /// </summary>
    /// <param name="shopId"></param>
    /// <param name="couponName"></param>
    /// <param name="startDate"></param>
    /// <param name="endDate"></param>
    /// <param name="RequirementMoney"></param>
    /// <param name="DeductibleAmount"></param>
    /// <param name="ValidityPeriod"></param>
    /// <param name="quantity"></param>
    /// <param name="employeeID"></param>
    /// <returns></returns>
    public string coupon_add(int shopId, string couponName, DateTime startDate, DateTime endDate, double RequirementMoney, double DeductibleAmount, int ValidityPeriod, int quantity, int employeeID, double MaxAmount)
    {
        return CouponOperate.VACouponAdd(shopId, couponName, startDate, endDate, RequirementMoney, DeductibleAmount, ValidityPeriod, quantity, employeeID, MaxAmount);
    }


    /// <summary>
    /// 查询活动列表
    /// </summary>
    /// <param name="pageSize"></param>
    /// <param name="pageIndex"></param>
    /// <param name="shopId"></param>
    /// <returns></returns>
    public string query_promotionActivity_list(int pageSize, int pageIndex, int shopId)
    {
        VAPromotionActivityList VAPromotionActivity = CouponOperate.GetPromotionActivityList(pageSize, pageIndex, shopId);
        SybMsg2 sybMsg = new SybMsg2();
        sybMsg.Insert(0, VAPromotionActivity);
        return sybMsg.Value;
    }

    /// <summary>
    /// 查询某个活动详情
    /// </summary>
    /// <param name="activityType">活动类别</param>
    /// <param name="activityId">活动Id</param>
    /// <returns></returns>
    public string query_promotionActivity(int activityType, int activityId)
    {
        VAPromotionCouponDetail couponDetail = new VAPromotionCouponDetail();

        var couponList = CouponOperate.GetListByQuery(new CouponQueryObject() { CouponId = activityId });

        if (couponList != null && couponList.Count() > 0)
        {
            var coupon = couponList.First();
            couponDetail.CouponName = coupon.CouponName;
            couponDetail.CouponContent = "每满" + coupon.RequirementMoney + "元减" + coupon.DeductibleAmount + "元,最多减" + coupon.MaxAmount;
            couponDetail.StartDate = Common.ToSecondFrom1970(coupon.StartDate);
            couponDetail.EndDate = Common.ToSecondFrom1970(coupon.EndDate);
            couponDetail.SheetNumber = coupon.SheetNumber;
            var couponGetList = CouponGetDetailOperate.GetListByCouponId(coupon.CouponId);
            couponDetail.SendCount = couponGetList.Count();
            couponDetail.usedCount = couponGetList.Where(p => p.State == 2).Count();
            couponDetail.consumeAmount = couponGetList.Where(p=>p.UseTime.HasValue).Sum(p => p.PreOrderSum).Value;
            couponDetail.unusedCount = couponDetail.SendCount - couponDetail.usedCount;
            couponDetail.RefuseReason = coupon.RefuseReason;
            switch (coupon.State)
            {
                case 0:
                    couponDetail.activityStatus = 0;//已失效
                    break;
                case 1:
                    if (coupon.StartDate > DateTime.Now)
                    {
                        couponDetail.activityStatus = 1;//未开始
                    }
                    if (coupon.StartDate <= DateTime.Now && coupon.EndDate > DateTime.Now)
                    {
                        couponDetail.activityStatus = 2;//进行中
                    }
                    if (coupon.EndDate <= DateTime.Now)
                    {
                        couponDetail.activityStatus = 3;//已结束
                    }
                    break;
                case -1:
                    couponDetail.activityStatus = -1;//待审核
                    break;
                case -2:
                    couponDetail.activityStatus = -2;//未通过
                    break;
            }
        }
        SybMsg2 sybMsg = new SybMsg2();
        sybMsg.Insert(0, couponDetail);
        return sybMsg.Value;
    }

    #endregion
    public class VAGetCouponResponse
    {
        public long CouponGetDetailId { get; set; }

        public string phoneNumber { get; set; }

        public int getState { get; set; }
    }

    public class VAShareCouponResponse
    {
        public bool isGot { get; set; }

        public int shareState { get; set; }

        public bool isMore { get; set; }
        public List<CouponDetail> couponList { get; set; }

        public string shareImage { get; set; }
        public string shareTitle { get; set; }
        public string shareText { get; set; }

    }

    public class VASearchCouponResponse
    {

        public bool isMore { get; set; }
        public List<CouponDetail> couponList { get; set; }


    }

    public class CouponDetail
    {
        public long couponId { get; set; }
        public string couponName { get; set; }
        public string shopName { get; set; }
        public int shopId { get; set; }

        /// <summary>
        /// 门店图片
        /// </summary>
        public string shopLogo { get; set; }


        /// <summary>
        /// 门店图片
        /// </summary>
        public string shopAddress { get; set; }

        public bool isGot { get; set; }
    }

    public class VACorrectCouponResponse
    {
        public int correctState { get; set; }
    }

    public class VACouponGetDetailResponse
    {
        public string couponName { get; set; }
        public string shopName { get; set; }
        public long couponGetDetailId { get; set; }
        public bool IsCorrected { get; set; }

        public List<DishDetail> dishList { get; set; }

    }

    public class DishDetail
    {
        public string dishName { get; set; }
        public string dishImage { get; set; }
        public double originalPrice { get; set; }
        public double nowPrice { get; set; }

        public int dishId { get; set; }

    }



}