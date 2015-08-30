﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary> 
    /// FileName: City.cs 
    /// CLRVersion: 4.0.30319.269 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 
    /// DateTime: 2012-05-15 15:24:56 
    /// </summary>
    public class City
    {
        /// <summary>
        /// 市ID
        /// </summary>
        public int cityID { get; set; }
        /// <summary>
        /// 市名称
        /// </summary>
        public string cityName { get; set; }
        /// <summary>
        /// 省ID
        /// </summary>
        public int provinceID { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string zipCode { get; set; }
        /// <summary>
        /// 求开通次数
        /// </summary>
        public int requestCount { get; set; }
        /// <summary>
        /// 市中心经度
        /// </summary>
        public double cityCenterLongitude { get; set; }
        /// <summary>
        /// 市中心纬度
        /// </summary>
        public double cityCenterLatitude { get; set; }
        /// <summary>
        /// 状态 1、未开通 2、已开通  3、优先服务开通，客户端未开通
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 签约门店数量
        /// </summary>
        public int restaurantCount { get; set; }

        /// <summary>
        /// 天气预报城市编码
        /// </summary>
        public string WeatherCityCode { set; get; }
    }
    /// <summary>
    /// 上线城市结构
    /// </summary>
    public class CityViewModel
    {
        public int cityID { get; set; }
        public string cityName { get; set; }
    }
    public class CityExt
    {
        public string cityName { get; set; }
        public string shopName { get; set; }
        public string isHandle { get; set; }
        public int accountManager { get; set; }
    }
}
