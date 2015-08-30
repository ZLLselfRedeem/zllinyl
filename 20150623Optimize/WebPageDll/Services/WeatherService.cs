using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;

namespace VAGastronomistMobileApp.WebPageDll.Services
{

    public interface IWeatherService
    {
        string GetCityWeather(string city, string cityName);
    }

    public class WeatherService : IWeatherService
    {
        private const string baseUri = "http://www.weather.com.cn/data/cityinfo/{0}.html";
        public string GetCityWeather(string city, string cityName)
        {
            string resultWeather = "";
            if (!string.IsNullOrWhiteSpace(city))
            {
                string uri = string.Format(baseUri, city);
                using (var client = new WebClient())
                {
                    client.Encoding = Encoding.UTF8;
                    try
                    {
                        string result = client.DownloadString(uri);
                        if (!string.IsNullOrWhiteSpace(result))
                        {
                            var weather = JsonOperate.JsonDeserialize<weather>(result);
                            if (weather != null)
                            {
                                resultWeather = weather.weatherinfo.weather;
                            }
                            if (String.IsNullOrWhiteSpace(resultWeather))
                            {
                                resultWeather = GetCityWeather(cityName);
                            }
                        }
                    }
                    catch
                    {
                        resultWeather = GetCityWeather(cityName);
                    }
                }
            }
            return resultWeather;
        }

        public string GetCityWeather(string cityName)
        {
            try
            {
                string[] strArrs = new string[23];
                cn.com.webxml.www.WeatherWebService w = new cn.com.webxml.www.WeatherWebService();
                strArrs = w.getWeatherbyCityName(cityName);
                return strArrs[6].Split(' ')[1].ToString();
            }
            catch
            {
                return "";
            }
        }
    }
    /*
     weatherinfo: {
city: "余杭",
cityid: "101210106",
temp1: "13℃",
temp2: "21℃",
weather: "阵雨转多云",
img1: "d3.gif",
img2: "n1.gif",
ptime: "18:00"
}*/


    [DataContract]
    class weather
    {
        [DataMember]
        public weatherinfo weatherinfo { set; get; }
    }
    [DataContract]
    class weatherinfo
    {
        [DataMember]
        public string city { set; get; }
        [DataMember]
        public string cityid { set; get; }
        [DataMember]
        public string temp1 { set; get; }
        [DataMember]
        public string temp2 { set; get; }
        [DataMember]
        public string weather { set; get; }
        [DataMember]
        public string img1 { set; get; }
        [DataMember]
        public string img2 { set; get; }
        [DataMember]
        public string ptime { set; get; }
    }
}
