using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
using VA.Cache.HttpRuntime;
using VA.CacheLogic;
using System.Web;

//
//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2012-04-10.
//
namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary> 
    /// FileName: CityOperate.cs 
    /// CLRVersion: 4.0.30319.269 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 
    /// DateTime: 2012-05-16 09:35:51 
    /// </summary>
    public class CityOperate
    {
        /// <summary>
        /// 根据省份查询城市信息
        /// </summary>
        /// <returns></returns>
        public DataTable QueryCity(int provinceID)
        {
            CityManager cityManager = new CityManager();
            DataTable dtCity = cityManager.SelectCity();
            DataView dvCity = dtCity.DefaultView;
            dvCity.RowFilter = "ProvinceID=" + provinceID;
            return dvCity.ToTable();
        }

        /// <summary>
        /// 城市入驻/退出操作
        /// </summary>
        /// <param name="cityID"></param>
        /// <param name="status"></param>
        //public void CityUpdate(int cityID, string status)
        //{
        //    CityManager manager = new CityManager();
        //    if (status == "2")
        //    {
        //        manager.SetCityStatus(1, cityID);
        //    }
        //    else
        //    {
        //        manager.SetCityStatus(2, cityID);
        //        if (!manager.CityOnceOpened(cityID))
        //        {
        //            int createBy = ((VAEmployeeLoginResponse)HttpContext.Current.Session["UserInfo"]).employeeID;
        //            manager.AddNomalTitle(cityID, createBy);
        //        }
        //    }
        //}

        /// <summary>
        /// 客户端查询城市列表20140313
        /// </summary>
        /// <param name="cityListRequest"></param>
        /// <returns></returns>
        public VACityListResponse ClientCityList(VACityListRequest cityListRequest)
        {
            VACityListResponse cityListResponse = new VACityListResponse();
            cityListResponse.type = VAMessageType.CITY_LIST_RESPONSE;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(cityListRequest.cookie, cityListRequest.uuid, (int)cityListRequest.type, (int)VAMessageType.CITY_LIST_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                cityListResponse.result = VAResult.VA_OK;

                SystemConfigCacheLogic systemConfigCacheLogic = new SystemConfigCacheLogic();
                List<Province> onlineProvince = systemConfigCacheLogic.GetOnLineProvince();

                if (!onlineProvince.Any())
                {
                    cityListResponse.stateList = new List<VAStateInfo>();
                    return cityListResponse;
                }
                var provinceList = new List<VAStateInfo>();
                CityManager cityMan = new CityManager();
                foreach (var itemProvince in onlineProvince)
                {   
                    DataTable dtCity = systemConfigCacheLogic.GetCityOfProvince(itemProvince.provinceID);
                    DataRow[] drCity = dtCity.Select("status = " + (int)VACityStatus.YI_KAI_TONG + " and isClientShow=1");//过滤上线城市
                    if (drCity.Length <= 0)
                    {
                        break;//此行代码不应该执行
                    }
                    var onlineCityList = new List<VACityInfo>();
                    foreach (var itemCity in drCity)
                    {
                        onlineCityList.Add(new VACityInfo()
                        {
                            cityId = Common.ToInt32(itemCity["cityID"]),
                            cityName = Common.ToString(itemCity["cityName"]),
                            restaurantCount = Common.ToInt32(itemCity["restaurantCount"]),
                            requestCount = Common.ToInt32(itemCity["requestCount"]),
                            cityCenterLongitude = Common.ToDouble(itemCity["cityCenterLongitude"]),
                            cityCenterLatitude = Common.ToDouble(itemCity["cityCenterLatitude"]),
                            isOnline = true
                        });
                    }
                    provinceList.Add(new VAStateInfo()
                    {
                        stateName = itemProvince.provinceName,
                        onlineCityList = onlineCityList,
                        offlineCityList = new List<VACityInfo>(),
                        isOnline = true
                    });
                    cityListResponse.stateList = provinceList;
                }
            }
            else
            {
                cityListResponse.result = checkResult.result;
            }
            return cityListResponse;
        }

        /// <summary>
        /// 客户端求开通城市（更新城市开通请求次数）
        /// </summary>
        /// <param name="cityOpenningRequest"></param>
        /// <returns></returns>
        public VACityOpenningResponse CityOpenning(VACityOpenningRequest cityOpenningRequest)
        {
            VACityOpenningResponse cityOpenningResponse = new VACityOpenningResponse();
            cityOpenningResponse.type = VAMessageType.CITY_OPENNING_RESPONSE;
            CheckCookieAndMsgtypeInfo checkResult = Common.CheckCookieAndMsgtype(cityOpenningRequest.cookie,
                cityOpenningRequest.uuid, (int)cityOpenningRequest.type, (int)VAMessageType.CITY_OPENNING_REQUEST);
            if (checkResult.result == VAResult.VA_OK)
            {
                CityManager cityMan = new CityManager();
                int cityId = Common.ToInt32(cityOpenningRequest.cityId);
                DataTable dtCity = cityMan.SelectCity(cityId);
                if (dtCity.Rows.Count == 1)
                {
                    if (Common.ToInt32(dtCity.Rows[0]["status"]) == (int)VACityStatus.YI_KAI_TONG)
                    {
                        cityOpenningResponse.result = VAResult.VA_FAILED_CITY_ALREADY_ONLINE;
                    }
                    else
                    {
                        using (TransactionScope scope = new TransactionScope())
                        {
                            if (cityMan.UpdateCityRequestCount(cityId, Common.ToInt32(dtCity.Rows[0]["requestCount"]) + 1))
                            {
                                scope.Complete();
                                cityOpenningResponse.result = VAResult.VA_OK;
                            }
                            else
                            {
                                cityOpenningResponse.result = VAResult.VA_FAILED_DB_ERROR;
                            }
                        }
                    }
                }
                else
                {
                    cityOpenningResponse.result = VAResult.VA_FAILED_CITY_NOT_FOUND;
                }
            }
            else
            {
                cityOpenningResponse.result = checkResult.result;
            }
            return cityOpenningResponse;
        }
        /// <summary>
        /// 查询省市县名称
        /// </summary>
        /// <param name="countyID"></param>
        /// <returns></returns>
        public static SybShopHandeleDetail QueryCountyCityProvinceName(int countyID)
        {
            CityManager man = new CityManager();
            return man.SelectCountyCityProvinceName(countyID);
        }

        /// <summary>
        /// 查询上线城市列表
        /// </summary>
        /// <returns></returns>
        public List<CityViewModel> GetHandleCity()
        {
            return new CityManager().SelectHandleCity();
        }

        /// <summary>
        /// 查询门店名称和所在城市
        /// </summary>
        /// <param name="shopID"></param>
        /// <returns></returns>
        public CityExt GetCityNameAndShopName(int shopID)
        {
            return new CityManager().SelectCityNameAndShopName(shopID);
        }

        public static City GetCityByCityId(int cityId)
        {
            return new CityManager().GetCityById(cityId);
        }
    }
}
