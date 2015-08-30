﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using VAGastronomistMobileApp.Model;
using VAGastronomistMobileApp.SQLServerDAL;
using System.Configuration;
using VA.Cache.HttpRuntime;
using VA.CacheLogic.OrderClient;
using VAGastronomistMobileApp.WebPageDll.Services.Infrastructure;
using VAGastronomistMobileApp.WebPageDll.Services;
using VAGastronomistMobileApp.Model.QueryObject;

namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    /// 店铺操作类
    /// </summary>
    public class ShopOperate
    {

        /// <summary>
        /// 新增店铺信息
        /// </summary>
        /// <returns></returns>
        public int AddShop(ShopInfo shop, List<ShopCoordinate> shopCoordinate)
        {
            int shopId = 0;
            ShopManager shopMan = new ShopManager();
            DataTable dtShop = shopMan.SelectCompanyShop(shop.companyID);
            DataView dvShop = dtShop.DefaultView;
            dvShop.RowFilter = "shopName = '" + shop.shopName + "'";
            if (dvShop.Count > 0 || shop.shopName == "" || shop.shopName == null)
            {//如果所加店铺信息的名称为空，或者对应公司的店铺信息表中已有该名称的店铺，则直接返回false

            }
            else
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    shopId = shopMan.InsertShop(shop);
                    if (shopId > 0)
                    {
                        bool result = true;
                        for (int i = 0; i < shopCoordinate.Count; i++)
                        {
                            shopCoordinate[i].shopId = shopId;
                            if (shopMan.InsertShopCoordinate(shopCoordinate[i]) <= 0)
                            {
                                result = false;
                            }
                        }
                        if (result)
                        {
                            scope.Complete();
                        }
                        else
                        {
                            shopId = 0;
                        }
                    }
                }
            }
            return shopId;
        }

        /// <summary>
        /// 删除店铺信息
        /// </summary>
        /// <returns></returns>
        public bool RemoveShop(int shopID)
        {
            ShopManager shopMan = new ShopManager();
            DataTable dtShop = shopMan.SelectShop(shopID);
            if (dtShop.Rows.Count == 1)
            {//判断此shopID是否存在，是则删除
                using (TransactionScope scope = new TransactionScope())
                {
                    if (shopMan.DeleteShop(shopID))
                    {//删除成功则返回true
                        scope.Complete();
                        return true;
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
        /// 修改店铺信息
        /// </summary>
        /// <returns></returns>
        public bool ModifyShop(ShopInfo shop)
        {
            ShopManager shopMan = new ShopManager();
            DataTable dtShop = shopMan.SelectCompanyShop(shop.companyID);
            DataView dvShopCopy = dtShop.DefaultView;
            dvShopCopy.RowFilter = "shopID = '" + shop.shopID + "'";
            if (dvShopCopy.Count == 1)
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (shopMan.UpdateShop(shop))
                    {
                        scope.Complete();
                        return true;
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
        /// 修改店铺赠送信息
        /// </summary>
        /// <returns></returns>
        public bool ModifyShopGift(ShopInfo shop)
        {
            ShopManager shopMan = new ShopManager();
            using (TransactionScope scope = new TransactionScope())
            {
                if (shopMan.UpdateShopGift(shop))
                {//修改成功则返回true
                    scope.Complete();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 修改店铺审批
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="isHandle"></param>
        /// <returns></returns>
        public bool ModifyShopIsHandle(int shopId, int isHandle)
        {
            ShopManager shopMan = new ShopManager();
            DataTable dtShop = shopMan.SelectShop(shopId);
            DataView dvShop = dtShop.DefaultView;
            dvShop.RowFilter = "shopID = '" + shopId + "'";
            if (1 == dvShop.Count)
            {//判断此shopID是否存在，是则修改
                using (TransactionScope scope = new TransactionScope())
                {
                    if (Common.ToInt32(dvShop[0]["isHandle"]) != isHandle)//审核状态变化了
                    {
                        if (shopMan.UpdateShopHandle(shopId, isHandle))
                        {//修改成功则返回true
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
                        scope.Complete();
                        return true;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据公司编号查询对应的所有店铺信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryCompanyShop(int companyID)
        {
            ShopManager shopMan = new ShopManager();
            return shopMan.SelectCompanyShop(companyID);
        }
        /// <summary>
        /// 根据名称模糊匹配 某城市店铺信息  2013-09-02  微信平台管理 餐厅推荐页面用
        /// </summary>
        public DataTable QueryShopByName(string ShopName, string cityName)
        {
            ShopManager shopMan = new ShopManager();
            return shopMan.SelectShopByName(ShopName, cityName);
        }
        /// <summary>
        /// 根据店铺编号查询店铺信息
        /// </summary>
        /// <returns></returns>
        public ShopInfo QueryShop(int shopID)
        {
            ShopManager shopMan = new ShopManager();
            ShopInfo shop = new ShopInfo();
            DataTable dtShop = shopMan.SelectShop(shopID);
            if (1 == dtShop.Rows.Count)
            {
                shop.shopID = shopID;
                shop.companyID = Common.ToInt32(dtShop.Rows[0]["companyID"]);
                shop.shopName = Common.ToString(dtShop.Rows[0]["shopName"]);
                shop.shopAddress = Common.ToString(dtShop.Rows[0]["shopAddress"]);
                shop.shopTelephone = Common.ToString(dtShop.Rows[0]["shopTelephone"]);
                shop.shopLogo = Common.ToString(dtShop.Rows[0]["shopLogo"]);
                shop.shopBusinessLicense = Common.ToString(dtShop.Rows[0]["shopBusinessLicense"]);
                shop.shopHygieneLicense = Common.ToString(dtShop.Rows[0]["shopHygieneLicense"]);
                shop.contactPerson = Common.ToString(dtShop.Rows[0]["contactPerson"]);
                shop.contactPhone = Common.ToString(dtShop.Rows[0]["contactPhone"]);
                shop.canTakeout = Common.ToBool(dtShop.Rows[0]["canTakeout"]);
                shop.canEatInShop = Common.ToBool(dtShop.Rows[0]["canEatInShop"]);
                shop.provinceID = Common.ToInt32(dtShop.Rows[0]["provinceID"]);
                shop.cityID = Common.ToInt32(dtShop.Rows[0]["cityID"]);
                shop.countyID = Common.ToInt32(dtShop.Rows[0]["countyID"]);
                shop.shopStatus = Common.ToInt32(dtShop.Rows[0]["shopStatus"]);
                shop.isHandle = Common.ToInt32(dtShop.Rows[0]["isHandle"]);
                shop.shopImagePath = Common.ToString(dtShop.Rows[0]["shopImagePath"]);
                shop.shopDescription = Common.ToString(dtShop.Rows[0]["shopDescription"]);
                shop.sinaWeiboName = Common.ToString(dtShop.Rows[0]["sinaWeiboName"]);
                shop.preorderGiftTitle = Common.ToString(dtShop.Rows[0]["preorderGiftTitle"]);
                shop.preorderGiftDesc = Common.ToString(dtShop.Rows[0]["preorderGiftDesc"]);
                shop.preorderGiftValidTimeType = Common.ToByte(dtShop.Rows[0]["preorderGiftValidTimeType"]);
                shop.preorderGiftValidTime = Common.ToDateTime(dtShop.Rows[0]["preorderGiftValidTime"]);
                shop.preorderGiftValidDay = Common.ToInt32(dtShop.Rows[0]["preorderGiftValidDay"]);
                shop.preorderGiftValid = Common.ToInt32(dtShop.Rows[0]["preorderGiftValid"]);
                shop.qqWeiboName = Common.ToString(dtShop.Rows[0]["qqWeiboName"]);
                shop.wechatPublicName = Common.ToString(dtShop.Rows[0]["wechatPublicName"]);
                shop.openTimes = Common.ToString(dtShop.Rows[0]["openTimes"]);
                shop.isSupportAccountsRound = Common.ToBool(dtShop.Rows[0]["isSupportAccountsRound"]);
                shop.shopRating = Common.ToDouble(dtShop.Rows[0]["shopRating"]);//店铺评分 2014-1-3 jinyanni
                shop.publicityPhotoPath = Common.ToString(dtShop.Rows[0]["publicityPhotoPath"]);//店铺形象展示照片 2014-1-3 jinyanni
                shop.acpp = Common.ToDouble(dtShop.Rows[0]["acpp"]);//人均消费 2014-1-7 jinyanni  
                shop.isSupportPayment = Common.ToBool(dtShop.Rows[0]["isSupportPayment"]);//支持付款
                shop.orderDishDesc = Common.ToString(dtShop.Rows[0]["orderDishDesc"]);//门店点菜描述
                shop.notPaymentReason = Common.ToString(dtShop.Rows[0]["notPaymentReason"]);//暂不支持付款原因
                shop.accountManager = Common.ToInt32(dtShop.Rows[0]["accountManager"]);//当前门店客户经理
                shop.bankAccount = Common.ToInt32(dtShop.Rows[0]["bankAccount"]);
                shop.isSupportRedEnvelopePayment = Common.ToBool(dtShop.Rows[0]["isSupportRedEnvelopePayment"]);
                shop.shopLevel = Common.ToInt32(dtShop.Rows[0]["shopLevel"]);
                shop.remainMoney = Common.ToDouble(dtShop.Rows[0]["remainMoney"]);

                if (dtShop.Rows[0]["AreaManager"] != DBNull.Value)
                {
                    shop.AreaManager = Common.ToInt32(dtShop.Rows[0]["AreaManager"]);
                }
            }
            return shop;
        }

        public ShopInfo GetShop(int shopID)
        {
            ShopManager shopMan = new ShopManager();
            ShopInfo shop = new ShopInfo();
            DataTable dtShop = shopMan.GetShop(shopID);
            if (1 == dtShop.Rows.Count)
            {
                shop.shopID = shopID;
                shop.companyID = Common.ToInt32(dtShop.Rows[0]["companyID"]);
                shop.shopName = Common.ToString(dtShop.Rows[0]["shopName"]);
                shop.shopAddress = Common.ToString(dtShop.Rows[0]["shopAddress"]);
                shop.shopTelephone = Common.ToString(dtShop.Rows[0]["shopTelephone"]);
                shop.shopLogo = Common.ToString(dtShop.Rows[0]["shopLogo"]);
                shop.shopBusinessLicense = Common.ToString(dtShop.Rows[0]["shopBusinessLicense"]);
                shop.shopHygieneLicense = Common.ToString(dtShop.Rows[0]["shopHygieneLicense"]);
                shop.contactPerson = Common.ToString(dtShop.Rows[0]["contactPerson"]);
                shop.contactPhone = Common.ToString(dtShop.Rows[0]["contactPhone"]);
                shop.canTakeout = Common.ToBool(dtShop.Rows[0]["canTakeout"]);
                shop.canEatInShop = Common.ToBool(dtShop.Rows[0]["canEatInShop"]);
                shop.provinceID = Common.ToInt32(dtShop.Rows[0]["provinceID"]);
                shop.cityID = Common.ToInt32(dtShop.Rows[0]["cityID"]);
                shop.countyID = Common.ToInt32(dtShop.Rows[0]["countyID"]);
                shop.shopStatus = Common.ToInt32(dtShop.Rows[0]["shopStatus"]);
                shop.isHandle = Common.ToInt32(dtShop.Rows[0]["isHandle"]);
                shop.shopImagePath = Common.ToString(dtShop.Rows[0]["shopImagePath"]);
                shop.shopDescription = Common.ToString(dtShop.Rows[0]["shopDescription"]);
                shop.sinaWeiboName = Common.ToString(dtShop.Rows[0]["sinaWeiboName"]);
                shop.preorderGiftTitle = Common.ToString(dtShop.Rows[0]["preorderGiftTitle"]);
                shop.preorderGiftDesc = Common.ToString(dtShop.Rows[0]["preorderGiftDesc"]);
                shop.preorderGiftValidTimeType = Common.ToByte(dtShop.Rows[0]["preorderGiftValidTimeType"]);
                shop.preorderGiftValidTime = Common.ToDateTime(dtShop.Rows[0]["preorderGiftValidTime"]);
                shop.preorderGiftValidDay = Common.ToInt32(dtShop.Rows[0]["preorderGiftValidDay"]);
                shop.preorderGiftValid = Common.ToInt32(dtShop.Rows[0]["preorderGiftValid"]);
                shop.qqWeiboName = Common.ToString(dtShop.Rows[0]["qqWeiboName"]);
                shop.wechatPublicName = Common.ToString(dtShop.Rows[0]["wechatPublicName"]);
                shop.openTimes = Common.ToString(dtShop.Rows[0]["openTimes"]);
                shop.isSupportAccountsRound = Common.ToBool(dtShop.Rows[0]["isSupportAccountsRound"]);
                shop.shopRating = Common.ToDouble(dtShop.Rows[0]["shopRating"]);//店铺评分 2014-1-3 jinyanni
                shop.publicityPhotoPath = Common.ToString(dtShop.Rows[0]["publicityPhotoPath"]);//店铺形象展示照片 2014-1-3 jinyanni
                shop.acpp = Common.ToDouble(dtShop.Rows[0]["acpp"]);//人均消费 2014-1-7 jinyanni  
                shop.isSupportPayment = Common.ToBool(dtShop.Rows[0]["isSupportPayment"]);//支持付款
                shop.orderDishDesc = Common.ToString(dtShop.Rows[0]["orderDishDesc"]);//门店点菜描述
                shop.notPaymentReason = Common.ToString(dtShop.Rows[0]["notPaymentReason"]);//暂不支持付款原因
                shop.accountManager = Common.ToInt32(dtShop.Rows[0]["accountManager"]);//当前门店客户经理
                shop.bankAccount = Common.ToInt32(dtShop.Rows[0]["bankAccount"]);
                shop.isSupportRedEnvelopePayment = Common.ToBool(dtShop.Rows[0]["isSupportRedEnvelopePayment"]);
                shop.shopLevel = Common.ToInt32(dtShop.Rows[0]["shopLevel"]);
                shop.remainMoney = Common.ToDouble(dtShop.Rows[0]["remainMoney"]);

                if (dtShop.Rows[0]["AreaManager"] != DBNull.Value)
                {
                    shop.AreaManager = Common.ToInt32(dtShop.Rows[0]["AreaManager"]);
                }
            }
            return shop;
        }

        /// <summary>
        /// 根据店铺编号查询店铺信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryShop()
        {
            ShopManager shopMan = new ShopManager();
            return shopMan.SelectShop();
        }

        /// <summary>
        /// 根据店铺名称店铺信息公司名称 
        /// </summary>
        /// <returns></returns>
        public DataTable QueryShopAndCompany()
        {
            ShopManager shopMan = new ShopManager();
            return shopMan.SelectShopAndCompany();
        }

        /// <summary>
        /// 查询某个城市的坐标
        /// </summary>
        /// <param name="mapId"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public ShopCoordinate QueryShopCoordinate(int mapId, int shopId)
        {
            ShopManager shopMan = new ShopManager();
            return shopMan.SelectShopCoordinate(mapId, shopId);
        }
        /// <summary>
        /// 修改某个城市的坐标
        /// </summary>
        /// <param name="shopCoordinate"></param>
        /// <returns></returns>
        public bool UpdateShopCoordinate(List<ShopCoordinate> shopCoordinate)
        {
            ShopManager shopMan = new ShopManager();
            using (TransactionScope scope = new TransactionScope())
            {
                int count = 0; ;
                for (int i = 0; i < shopCoordinate.Count; i++)
                {
                    bool deleteTag = shopMan.DeleteShopCoordinate(shopCoordinate[i].mapId, shopCoordinate[i].shopId);
                    if (deleteTag)
                    {//删除成功
                        int insertTag = shopMan.InsertShopCoordinate(shopCoordinate[i]);
                        if (insertTag > 0)
                        {//添加成功
                            count++;
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
                if (count > 0 && count == shopCoordinate.Count)
                {
                    scope.Complete();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// 客户端根据城市和公司编号查询对应的店铺信息
        /// </summary>
        /// <param name="restaurantListByCompanyRequest"></param>
        /// <returns></returns>
        public VARestaurantListByCompanyResponse ClientQueryShopByCompanyAndCity(VARestaurantListByCompanyRequest restaurantListByCompanyRequest)
        {
            VARestaurantListByCompanyResponse restaurantListByCompanyResponse = new VARestaurantListByCompanyResponse();
            restaurantListByCompanyResponse.type = VAMessageType.RESTAURANT_LIST_BY_COMPANY_RESPONSE;
            restaurantListByCompanyResponse.cookie = restaurantListByCompanyRequest.cookie;
            restaurantListByCompanyResponse.uuid = restaurantListByCompanyRequest.uuid;

            restaurantListByCompanyResponse.cityId = restaurantListByCompanyRequest.cityId;
            restaurantListByCompanyResponse.companyId = restaurantListByCompanyRequest.companyId;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(restaurantListByCompanyRequest.cookie,
                restaurantListByCompanyRequest.uuid, (int)restaurantListByCompanyRequest.type, (int)VAMessageType.RESTAURANT_LIST_BY_COMPANY_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                restaurantListByCompanyResponse.result = VAResult.VA_OK;
                ShopManager shopMan = new ShopManager();
                DataTable dtShop = shopMan.SelectShopByCompanyAndCity(restaurantListByCompanyRequest.companyId, restaurantListByCompanyRequest.cityId,
                    (VAAppType)Common.ToInt32(checkResult.dtCustomer.Rows[0]["appType"]));
                restaurantListByCompanyResponse.companyList = Common.FillRestaurant(dtShop);
            }
            else
            {
                restaurantListByCompanyResponse.result = checkResult.result;
            }
            return restaurantListByCompanyResponse;
        }
        ///// <summary>
        ///// 添加百度地图坐标
        ////2013-7-20 wangcheng 注释
        ///// </summary>
        ///// <returns></returns>
        public bool AddShopBaiduCoordinate()
        {
            int count = 0;
            using (TransactionScope scope = new TransactionScope())
            {
                ShopManager shopMan = new ShopManager();
                DataTable dtShop = shopMan.SelectShop();
                for (int i = 0; i < dtShop.Rows.Count; i++)
                {
                    MapLocation baiduLocation = Common.GetBaiduMapCoordinate(Common.ToString(dtShop.Rows[i]["shopAddress"]), "杭州市");
                    if (baiduLocation != null)
                    {
                        ShopCoordinate shopCoordinate = new ShopCoordinate();
                        shopCoordinate.latitude = baiduLocation.lat;
                        shopCoordinate.longitude = baiduLocation.lng;
                        shopCoordinate.mapId = 2;
                        shopCoordinate.shopId = Common.ToInt32(dtShop.Rows[i]["shopID"]);
                        int insertTag = shopMan.InsertShopCoordinate(shopCoordinate);
                        if (insertTag > 0)
                        {//添加成功
                            count++;
                        }
                    }
                }
                scope.Complete();
            }
            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 添加店铺的详情展示图片的信息
        /// </summary>
        /// <returns></returns>
        public int InsertShopRevealImage(int shopId, string uploadImageName, int status)
        {
            ShopManager shopMan = new ShopManager();
            return shopMan.InsertShopRevealPicture(shopId, uploadImageName, status);
        }
        /// <summary>
        /// 根据门店Id查询对应的门店详情图片信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable QueryShopRevealImageInfo(int shopId)
        {
            ShopManager shopMan = new ShopManager();
            return shopMan.SelectShopRevealImageInfo(shopId);
        }
        public bool DeleteShopRevealImage(string revealImageName, int shopId)
        {
            ShopManager shopMan = new ShopManager();
            return shopMan.DeleteShopRevealImageInfo(revealImageName, shopId);
        }
        public bool DeleteShopRevealImage(long id)
        {
            ShopManager shopMan = new ShopManager();
            return shopMan.DeleteShopRevealImageInfo(id);
        }
        /// <summary>
        /// 根据公司查询该公司下所有的门店信息
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public List<VAEmployeeShop> QueryShopInfoByCompanyId(int companyId)
        {
            ShopManager shopMan = new ShopManager();
            DataTable dtEmployeeShop = shopMan.SelectShopInfoByCompanyId(companyId);
            List<VAEmployeeShop> shopList = new List<VAEmployeeShop>();
            if (dtEmployeeShop.Rows.Count > 0)
            {
                for (int i = 0; i < dtEmployeeShop.Rows.Count; i++)
                {
                    VAEmployeeShop shop = new VAEmployeeShop();
                    shop.shopID = Common.ToInt32(dtEmployeeShop.Rows[i]["shopID"]);
                    shop.shopName = Common.ToString(dtEmployeeShop.Rows[i]["shopName"]);
                    shopList.Add(shop);
                }
            }
            else
            {
                shopList = null;
            }
            return shopList;
        }
        /// <summary>
        /// 查询所有上线门店
        /// </summary>
        /// <param name="shopName"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public List<VAEmployeeShop> QueryHandShopInfoByCompanyId(string shopName, int cityId)
        {
            ShopManager shopMan = new ShopManager();
            DataTable dtEmployeeShop = shopMan.SelectHandShopInfoByCompanyId(shopName, cityId);
            List<VAEmployeeShop> shopList = new List<VAEmployeeShop>();
            if (dtEmployeeShop.Rows.Count > 0)
            {
                int count = dtEmployeeShop.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    shopList.Add(new VAEmployeeShop()
                    {
                        shopID = Common.ToInt32(dtEmployeeShop.Rows[i]["shopID"]),
                        shopName = Common.ToString(dtEmployeeShop.Rows[i]["shopName"])
                    });
                }
            }
            return shopList;
        }
        /// <summary>
        /// 获取店铺VIP折扣信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable GetShopVipInfo(int shopId)
        {
            ShopManager _ShopM = new ShopManager();
            return _ShopM.SelectShopVipInfo(shopId);
        }
        /// <summary>
        /// 收银宝后台查询门店VIP等级和对应的平台等级信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable GetShopVipInfoAndViewAllocPlatformVipInfo(int shopId)
        {
            ShopManager _ShopM = new ShopManager();
            return _ShopM.SelectShopVipInfoAndViewAllocPlatformVipInfo(shopId);
        }
        /// <summary>
        /// 新增店铺VIP折扣信息
        /// </summary>
        /// <param name="platformVipId">平台VIP ID</param>
        /// <param name="name">名称</param>
        /// <param name="shopId">店铺ID</param>
        /// <param name="discount">折扣</param>
        /// <returns></returns>
        public int InsertShopVipInfo(int platformVipId, string name, int shopId, double discount)
        {
            ShopManager _ShopM = new ShopManager();
            return _ShopM.InsertShopVipInfo(platformVipId, name, shopId, discount);
        }

        /// <summary>
        /// 修改店铺VIP折扣信息
        /// </summary>
        /// <param name="id">店铺VIP 序列号</param>
        /// <param name="discount">折扣</param>,
        /// <returns></returns>
        public object[] UpdateShopVipInfo(int id, string name, int platformVipId, double discount)
        {
            ShopManager _ShopM = new ShopManager();
            return _ShopM.UpdateShopVipInfo(id, name, platformVipId, discount);
        }

        /// <summary>
        /// 删除店铺VIP折扣信息
        /// </summary>
        /// <param name="id">店铺VIP 序列号</param>
        /// <returns></returns>
        public object[] DeleteShopVipInfo(int id)
        {
            ShopManager _ShopM = new ShopManager();
            return _ShopM.DeleteShopVipInfo(id);
        }

        /// <summary>
        /// 根据店铺ID获取相应的下一级VIP等级ID及名称
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public DataTable GetAssignVipInfoForShop(int shopId)
        {
            ShopManager _ShopM = new ShopManager();
            return _ShopM.GetAssignVipInfoForShop(shopId);
        }

        /// <summary>
        /// 积分商城：查询店铺排行榜
        /// </summary>
        /// <param name="startTime">审核开始时间</param>
        /// <param name="endTime">审核结束时间</param>
        /// <param name="cityId">城市ID</param>
        /// <param name="orderBy">排序方式：ASC,DESC</param>
        /// <returns></returns>
        public DataTable QueryShopRanking(string startTime, string endTime, string cityId, string orderBy)
        {
            ShopManager _ShopManager = new ShopManager();
            return _ShopManager.QueryShopRanking(startTime, endTime, cityId, orderBy);
        }

        #region 门店微信公共帐号点菜配置 add by wangc 20140317
        /// <summary>
        /// 增加一条数据
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="shopId"></param>
        /// <param name="status"></param>
        /// <param name="wechatOrderUrl"></param>
        /// <returns></returns>
        public bool AddShopWechatOrderConfig(string cookie, int shopId, string wechatOrderUrl)
        {
            ShopManager shopMan = new ShopManager();
            return shopMan.InsertShopWechatOrderConfig(cookie, shopId, wechatOrderUrl) > 0;
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        /// <param name="shopId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public DataTable QueryShopWechatOrderConfig(int shopId, int companyId)
        {
            ShopManager shopMan = new ShopManager();
            return shopMan.SelectShopWechatOrderConfig(shopId, companyId);
        }
        /// <summary>
        /// 更新当前记录状态
        /// </summary>
        /// <param name="id">记录主键ID</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        public bool ModifyShopWechatOrderConfigStatus(long id)
        {
            ShopManager shopMan = new ShopManager();
            DataTable dt = shopMan.SelectShopWechatOrderConfigById(id);
            if (dt.Rows.Count == 1)
            {
                int status = Common.ToInt32(dt.Rows[0]["status"]) == 1 ? 0 : 1;
                return shopMan.UpdateShopWechatOrderConfigStatus(id, status);
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 根据Id查询唯一信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DataTable QueryShopWechatOrderConfigById(long id)
        {
            ShopManager shopMan = new ShopManager();
            return shopMan.SelectShopWechatOrderConfigById(id);
        }
        #endregion

        /// <summary>
        /// 获取指定城市指定个数的shopLogo
        /// Add at 2014-4-2
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="logoImageCount"></param>
        /// <returns></returns>
        public DataTable QueryShopLogo(int cityId, int logoImageCount)
        {
            ShopManager ShopManager = new ShopManager();
            return ShopManager.QueryShopLogo(cityId, logoImageCount);
        }
        /// <summary>
        /// 根据店铺编号查询门店详情 Modify by wangc 20140319
        /// </summary>
        /// <param name="brandDetailRequest"></param>
        /// <returns></returns>
        public VAClientShopDetailResponse ClientQueryShopEnvironment(VAClientShopDetailRequest brandDetailRequest)
        {
            VAClientShopDetailResponse shopResponse = new VAClientShopDetailResponse();
            shopResponse.type = VAMessageType.CLIENT_SHOP_DETAIL_RESPONSE;
            shopResponse.cookie = brandDetailRequest.cookie;
            shopResponse.uuid = brandDetailRequest.uuid;
            shopResponse.cityId = brandDetailRequest.cityId;
            CheckCookieAndMsgtypeInfo checkResult
                = Common.CheckCookieAndMsgtype(brandDetailRequest.cookie, brandDetailRequest.uuid, (int)brandDetailRequest.type, (int)VAMessageType.CLIENT_SHOP_DETAIL_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                ShopInfoCacheLogic shopInfoCacheLogic = new ShopInfoCacheLogic();
                ShopInfo shopInfo = shopInfoCacheLogic.GetShopInfo(brandDetailRequest.shopId);
                MenuOperate menuOperate = new MenuOperate();
                if (shopInfo != null)
                {
                    ShopManager shopmanager = new ShopManager();
                    CustomerManager customerManager = new CustomerManager();
                    #region 加载店铺信息
                    string imagePath = WebConfig.CdnDomain + WebConfig.ImagePath;
                    string shopImagePath = imagePath + shopInfo.shopImagePath + WebConfig.ShopImg;
                    VAAppType appType = (VAAppType)checkResult.dtCustomer.Rows[0]["appType"];
                    long CustomerID = Common.ToInt64(checkResult.dtCustomer.Rows[0]["CustomerID"]);
                    DataTable dtshopImage = shopmanager.QueryImageURL(brandDetailRequest.shopId);
                    List<string> imglist = new List<string>();
                    if (dtshopImage.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtshopImage.Rows.Count; i++)
                        {
                            imglist.Add(shopImagePath + Common.ToString(dtshopImage.Rows[i]["revealImageName"]));
                        }
                    }
                    shopResponse.shopImage = imglist;//门店图片地址
                    shopResponse.shopId = shopInfo.shopID;
                    if (!string.IsNullOrEmpty(shopInfo.publicityPhotoPath))
                    {
                        shopResponse.publicityPhotoPath = imagePath + shopInfo.publicityPhotoPath;
                    }
                    else
                    {
                        shopResponse.publicityPhotoPath = "";
                    }
                    shopResponse.shopName = shopInfo.shopName;
                    shopResponse.shopRating = shopInfo.shopRating.HasValue ? shopInfo.shopRating.Value : 0;
                    shopResponse.shopAddress = shopInfo.shopAddress;
                    shopResponse.shopTelephone = shopInfo.shopTelephone;
                    shopResponse.openTimes = shopInfo.openTimes;
                    if (string.IsNullOrEmpty(shopInfo.shopLogo))//门店LOGO
                    {
                        shopResponse.shopLogoPath = "";
                    }
                    else
                    {
                        shopResponse.shopLogoPath = imagePath + shopInfo.shopImagePath + shopInfo.shopLogo;
                    }
                    shopResponse.userShareUrl = WebConfig.ServerDomain + "shopShow.aspx?shopId=" + brandDetailRequest.shopId + "&customerId=" + CustomerID;//用户分享门店信息
                    string userShareMessage = string.Empty;
                    userShareMessage = WebConfig.ShopShare;
                    userShareMessage = userShareMessage.Replace("{0}", shopInfo.shopName);
                    shopResponse.userShareMessage = userShareMessage;
                    int currentPlatformVipGrade = Common.ToInt32(checkResult.dtCustomer.Rows[0]["currentPlatformVipGrade"]);//当前用户VIP等级 
                    ShopCoordinate shopCoordinateBaidu = shopInfoCacheLogic.GetShopCoordinate(brandDetailRequest.shopId);//百度经纬度
                    shopResponse.latitude = shopCoordinateBaidu.latitude;
                    shopResponse.longitude = shopCoordinateBaidu.longitude;
                    DataTable dtShopVipInfo = shopmanager.SelectShopVipInfo(shopInfo.shopID);//查询当前门店的VIP等级信息
                    //DataTable dtShopVipInfo = menuOperate.GetShopVipInfo(shopInfo.shopID);
                    if (dtShopVipInfo.Rows.Count > 0)
                    {
                        List<VAShopVipInfo> shopVipList = new List<VAShopVipInfo>();
                        VAShopVipInfo userVipInfo = new VAShopVipInfo();
                        ClientExtendOperate.GetUserVipInfo(currentPlatformVipGrade, dtShopVipInfo, userVipInfo, shopVipList);
                        shopResponse.currectDiscount = userVipInfo.discount;
                    }
                    else
                    {//店家未开通店铺折扣
                        shopResponse.currectDiscount = 1;
                    }
                    List<int> shopIdList = customerManager.SelectCustomerFavoriteShop(CustomerID);
                    if (shopIdList.Contains(shopInfo.shopID))
                    {
                        shopResponse.isFavorites = true;
                    }
                    else
                    {
                        shopResponse.isFavorites = false;
                    }
                    shopResponse.shopLevel = shopInfo.shopLevel;
                    shopResponse.prepayOrderCount = (int)shopInfo.prepayOrderCount;
                    ShopEvaluationDetailManager shopEvaluationDetailManager = new ShopEvaluationDetailManager();
                    ShopEvaluationDetailQueryObject shopEvaluationDetailQueryObject = new Model.ShopEvaluationDetailQueryObject()
                    {
                        ShopId = brandDetailRequest.shopId
                    };
                    List<ShopEvaluationDetail> shopEvaluationDetailList =
                        shopEvaluationDetailManager.GetShopEvaluationDetailByQuery(shopEvaluationDetailQueryObject);
                    if (shopEvaluationDetailList != null && shopEvaluationDetailList.Count > 0)
                    {
                        var query = (from q in shopEvaluationDetailList
                                     where q.EvaluationValue == 1
                                     select q.EvaluationCount).ToList();
                        shopResponse.goodEvaluationCount = query.Any() == true ? query[0] : 0;//返回给客户端好评数

                        double totalEvaluation = shopEvaluationDetailList.Sum(p => p.EvaluationCount);
                        var evaluationPercent = from e in shopEvaluationDetailList
                                                group e by e.EvaluationValue into g
                                                select new EvaluationPercent()
                                                {
                                                    evaluationValue = g.Key,
                                                    percent = Math.Round((g.Sum(p => p.EvaluationCount) / totalEvaluation), 4)
                                                };
                        if (evaluationPercent != null && evaluationPercent.Count() >= 0)
                        {
                            shopResponse.evaluationPercent = evaluationPercent.ToList();
                            if (evaluationPercent.Count() < 3)
                            {
                                for (int i = -1; i < 2; i++)
                                {
                                    EvaluationPercent entity = evaluationPercent.FirstOrDefault(p => p.evaluationValue == i);
                                    if (entity == null)
                                    {
                                        entity = new EvaluationPercent() { evaluationValue = i, percent = 0 };
                                        shopResponse.evaluationPercent.Add(entity);
                                    }
                                }
                            }
                        }
                    }

                    #endregion
                    if (brandDetailRequest.pageIndex > 0)
                    {
                        #region 加载店铺评价  
                        PreorderEvaluationQueryObject queryObject = new PreorderEvaluationQueryObject() { ShopId = shopInfo.shopID };
                        long queryCount = PreorderEvaluationOperate.GetCountByQuery(queryObject);
                        if (queryCount > brandDetailRequest.pageIndex * brandDetailRequest.pageSize)
                        {
                            shopResponse.isMore = true;
                        }
                        else
                        {
                            shopResponse.isMore = false;
                        }
                        List<PreorderEvaluation> preOrder19dianInfoList
                            = PreorderEvaluationOperate.GetListByQuery( brandDetailRequest.pageSize, brandDetailRequest.pageIndex,queryObject);
                        if (preOrder19dianInfoList!=null && preOrder19dianInfoList.Count > 0)
                        {
                            var returnEvaluationList = from e in preOrder19dianInfoList
                                                       select new EvaluationInfo()
                                                       {
                                                           customId = (int)e.CustomerId,
                                                           evaluationContent = e.EvaluationContent,
                                                           evaluationValue = e.EvaluationLevel,
                                                           evaluationDate = Common.ToSecondFrom1970(e.EvaluationTime)
                                                       };
                            shopResponse.evaluationList = returnEvaluationList.ToList();
                            foreach (var item in shopResponse.evaluationList)
                            {
                                item.mobilePhoneNumber = string.Empty;
                                DataTable dtCustomer = customerManager.SelectCustomer(item.customId);
                                if (dtCustomer != null && dtCustomer.Rows.Count > 0)
                                {
                                    string mobilePhoneNumber = dtCustomer.Rows[0]["mobilePhoneNumber"].ToString();
                                    if (mobilePhoneNumber.Length > 10)
                                    {
                                        item.mobilePhoneNumber = dtCustomer.Rows[0]["mobilePhoneNumber"].ToString().Remove(3, 6).Insert(3, "******");
                                    }
                                }
                            }

                        }

                        #endregion
                    }
                    // 添加抵扣券显示 add by zhujinlei 2015/06/17
                    var operate = new CouponOperate();
                    List<OrderPaymentCouponDetail> list = operate.GetShopCouponDetails(brandDetailRequest.shopId);
                    shopResponse.couponDetails = list;
                    // add end 
                    shopResponse.result = VAResult.VA_OK;

                    return shopResponse;
                }
            }
            else
            {
                shopResponse.result = checkResult.result;
            }


            return shopResponse;

        }

       

        /// <summary>
        /// 查询所有门店的审核状态信息
        /// </summary>
        /// <returns></returns>
        public List<ShopHandleListInfo> SybQueryShopHandleList()
        {
            ShopManager man = new ShopManager();
            return man.SybSelectShopHandleList();
        }

        public static List<ShopInfo> GetListByQuery(int pageSize, int pageIndex, ShopInfoQueryObject queryObject = null)
        {
            ShopManager shopInfoManager = new ShopManager();
            return shopInfoManager.GetListByQuery(pageSize, pageIndex, queryObject);
        }

        public static List<ShopInfo> GetListByQuery(ShopInfoQueryObject queryObject = null)
        {
            ShopManager shopInfoManager = new ShopManager();
            return shopInfoManager.GetListByQuery(queryObject);
        }

        public static long GetCountByQuery(ShopInfoQueryObject queryObject)
        {
            ShopManager shopInfoManager = new ShopManager();
            return shopInfoManager.GetCountByQuery(queryObject);
        }

        /// <summary>
        /// 获取收银宝会员服务中的商铺信息
        /// </summary>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public static string QueryShopDetail(int shopId)
        {
            
            return string.Empty;
        }

        /// <summary>
        /// 批量打款提交申请到出纳时扣除金额
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ModifyShopMoneyApply(ShopInfo model)
        {ShopManager shopInfoManager = new ShopManager();
        return shopInfoManager.UpdateShopMoneyApply(model);
        }

        /// <summary>
        /// 批量打款撤回及银行打款失败时，退还金额到门店
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool ModifyShopMoneyApplyBack(ShopInfo model)
        {
            ShopManager shopInfoManager = new ShopManager();
            return shopInfoManager.UpdateShopMoneyApplyBack(model);
        }

       /// <summary>
        /// 商户余额日志查询
       /// </summary>
       /// <param name="logDate"></param>
       /// <param name="cityID"></param>
       /// <param name="shopID"></param>
       /// <returns></returns>
        public DataTable QueryShopAmountLog(DateTime logDate, int cityID,int shopID)
        {
            ShopManager shopInfoManager = new ShopManager();
            return shopInfoManager.SelectShopAmountLog(logDate, cityID, shopID);
        }

        /// <summary>
        /// 添加一条商户余额日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int AddShopAmountLog(ShopAmountLog model)
        {
            ShopManager shopInfoManager = new ShopManager();
            return shopInfoManager.InsertShopAmountLog(model);
        }

        /// <summary>
        /// 添加商户余额日志
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void CreateShopAmountLog()
        {
            DateTime dtNow = DateTime.Now;
            ShopManager shopInfoManager = new ShopManager();
            ShopAmountLog model = new ShopAmountLog();
            DataTable dt = shopInfoManager.SelectShopAmount();
            List<ShopAmountLog> listSAL = new List<ShopAmountLog>();
            int k = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                model = new ShopAmountLog();
                model.shopID = Common.ToInt32(dt.Rows[i]["shopID"].ToString());
                model.logDate = dtNow;
                model.amount = Common.ToDouble(dt.Rows[i]["remainMoney"].ToString());
                listSAL.Add(model);
                CommonManager cm = new CommonManager();

                //shopInfoManager.InsertShopAmountLog(model);
                k++;
                if (k == 1000 || i == dt.Rows.Count - 1)
                {
                    DataTable dtShopAmountLog = Common.ListToDataTable((List<ShopAmountLog>)listSAL);
                    dtShopAmountLog.TableName = "ShopAmountLog";
                    cm.BatchInsert(dtShopAmountLog);
                    k = 0;
                    listSAL = new List<ShopAmountLog>();
                }
            }
           
        }

        /// <summary>
        /// 查询打款方式
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public int getWithdrawType(int shopid)
        {
            ShopManager shopInfoManager = new ShopManager();
            return shopInfoManager.getWithdrawType(shopid);
        }

        /// <summary>
        /// 修改打款方式
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public int updateWithdrawType(int shopid,int withdrawtype)
        {
            ShopManager shopInfoManager = new ShopManager();
            return shopInfoManager.updateWithdrawType(shopid, withdrawtype);
        }

        /// <summary>
        /// 查询佣金比例
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public double getViewallocCommissionValue(int shopid)
        {
            ShopManager shopInfoManager = new ShopManager();
            return shopInfoManager.getViewallocCommissionValue(shopid);
        }

        /// <summary>
        /// 修改佣金比例
        /// </summary>
        /// <param name="shopid"></param>
        /// <param name="viewalloccommissionvalue"></param>
        /// <returns></returns>
        public int updateViewallocCommissionValue(int shopid, double viewalloccommissionvalue)
        {
            ShopManager shopInfoManager = new ShopManager();
            return shopInfoManager.updateViewallocCommissionValue(shopid, viewalloccommissionvalue);
        }

        /// <summary>
        /// 查询折扣信息
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public double getShopVipInfo(int shopid)
        {
            ShopManager shopInfoManager = new ShopManager();
            return shopInfoManager.getShopVipInfo(shopid);
        }

        /// <summary>
        /// 修改折扣信息(无记录则插入一条)
        /// </summary>
        /// <param name="shopid"></param>
        /// <returns></returns>
        public int updateShopVipInfo(int shopid, double discount)
        {
            ShopManager shopInfoManager = new ShopManager();
            DataTable dt = shopInfoManager.SelectShopVipInfo(shopid);
            if (dt == null || dt.Rows.Count == 0)
            {//不存在记录，插入
                return shopInfoManager.insertShopVipInfo(shopid, discount);
            }
            return shopInfoManager.updateShopVipInfo(shopid, discount);
        }

         /// <summary>
        /// 获取门店默认LOG地址
        /// </summary>
        /// <returns></returns>
        public string getDefaultLogPath(int shopID)
        {
            ShopManager shopInfoManager = new ShopManager();
            return shopInfoManager.getDefaultLogPath(shopID);
        }
    }
}
