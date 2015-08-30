using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Autofac;
using VAGastronomistMobileApp.SQLServerDAL.Persistence;
using VAGastronomistMobileApp.SQLServerDAL.Persistence.Infrastructure;
using System.Text.RegularExpressions;

namespace WeatherCityCodeConvert
{
    class Program
    {
        const string baseUri = "http://flash.weather.com.cn/wmaps/xml/{0}.xml";
        private static IContainer container;
        private static void Main(string[] args)
        {
            container = AutofacConfig();

            var rootUri = string.Format(baseUri, "china");
            string xml = WebGet(rootUri);
            var xdoc = XDocument.Parse(xml);
            foreach (var item in (from a in xdoc.Element("china").Elements()
                                  select new
                                  {
                                      QuName = a.Attribute("quName") == null ? (string)null : a.Attribute("quName").Value,
                                      PyName = a.Attribute("pyName") == null ? (string)null : a.Attribute("pyName").Value
                                  }))
            {
                //                if (string.IsNullOrWhiteSpace(item.PyName))
                //                {
                //                    continue; ;
                //                }
                //                var cUri = string.Format(baseUri, item.PyName);
                //                var cxml = WebGet(cUri);
                //                var cxdoc = XDocument.Parse(cxml);
                //                foreach (var citem in (from a in cxdoc.Element(item.PyName).Elements()
                //                                       select new
                //                                           {
                //                                               CityName = a.Attribute("cityname") == null ? (string)null : a.Attribute("cityname").Value,
                //                                               PyName = a.Attribute("pyName") == null ? (string)null : a.Attribute("pyName").Value,
                //                                               Code = a.Attribute("url") == null ? (string)null : a.Attribute("url").Value
                //                                           }))
                //                {
                //                    if (string.IsNullOrWhiteSpace(citem.PyName))
                //                    {
                //                        continue;
                //                    }

                //                    var ccUri = string.Format(baseUri, citem.PyName);
                //                    var ccxml = WebGet(ccUri);
                //                    if (ccxml.IndexOf("页面没有找到") >= 0)
                //                    {
                //                        continue;
                //                    }
                //                    ccxml = ccxml.Replace((char)0x20, ' ');
                //                    if (!Regex.IsMatch(ccxml, "</" + citem.PyName + ">"))
                //                    {
                //                        Console.WriteLine(citem.PyName);
                //                        continue;
                //                    }
                //                    //if (ccxml.IndexOf(citem.PyName) >= 0)
                //                    //{
                //                    //    ccxml += "</aletai>";
                //                    //}
                //                    var ccxdoc = XDocument.Parse(ccxml);

                //                    /*
                //                     “System.Xml.XmlException”类型的未经处理的异常在 System.Xml.dll 中发生 
                //其他信息: 出现意外的文件结尾。以下元素未封闭: aletai. 第 7 行，位置 1。
                //                     */
                //                    foreach (var ccitme in (from a in ccxdoc.Element(citem.PyName).Elements()
                //                                            select new
                //                                                {
                //                                                    CityName = a.Attribute("cityname") == null ? (string)null : a.Attribute("cityname").Value,
                //                                                    //PyName = a.Attribute("pyName") == null ? (string)null : a.Attribute("pyName").Value,
                //                                                    Code = a.Attribute("url") == null ? (string)null : a.Attribute("url").Value
                //                                                }))
                //                    {
                //                        using (var scope = container.BeginLifetimeScope())
                //                        {
                //                            var cityRepository = scope.Resolve<ICityRepository>();

                //                            var city = cityRepository.Get(p => p.cityName == ccitme.CityName);
                //                            if (city != null)
                //                            {
                //                                city.WeatherCityCode = ccitme.Code;
                //                                cityRepository.Update(city);
                //                            }
                //                        }

                //                        //Console.WriteLine("{0}={1}", ccitme.CityName, ccitme.Code);
                //  }
                //}
                DG(item.PyName, 1);
            }

            //101010100,北京，北京，北京
            //101020100,上海,上海,上海
            //101030100,天津,天津,天津
            using (var scope = container.BeginLifetimeScope())
            {
                var cityRepository = scope.Resolve<ICityRepository>();

                //var city = cityRepository.Get(p => p.cityName.Contains("北京"));
                //if (city != null)
                //{
                //    city.WeatherCityCode = "101010100";
                //    cityRepository.Update(city);
                //}
                //city = cityRepository.Get(p => p.cityName.Contains("上海"));
                //if (city != null)
                //{
                //    city.WeatherCityCode = "101020100";
                //    cityRepository.Update(city);
                //}

                //city = cityRepository.Get(p => p.cityName.Contains("天津"));
                //if (city != null)
                //{
                //    city.WeatherCityCode = "101030100";
                //    cityRepository.Update(city);
                //}

            }
            Console.WriteLine("OVER");
            Console.Read();
        }

        private static void DG(string cityName, int lev)
        {
            if (string.IsNullOrWhiteSpace(cityName))
            {
                return;
            }


            var cUri = string.Format(baseUri, cityName);
            var cxml = WebGet(cUri);
            cityName = cityName.Replace(" ", "");

            if (cxml.IndexOf("页面没有找到") >= 0)
            {
                return;
            }
            cxml = cxml.Replace((char)0x20, ' ');
            if (!Regex.IsMatch(cxml, "</" + cityName + ">"))
            {
                Console.WriteLine(cityName);
                return;
            }

            var cxdoc = XDocument.Parse(cxml);
            foreach (var citem in (from a in cxdoc.Element(cityName).Elements()
                                   select new
                                   {
                                       CityName = a.Attribute("cityname") == null ? (string)null : a.Attribute("cityname").Value,
                                       PyName = a.Attribute("pyName") == null ? (string)null : a.Attribute("pyName").Value,
                                       Code = a.Attribute("url") == null ? (string)null : a.Attribute("url").Value
                                   }))
            {
                using (var scope = container.BeginLifetimeScope())
                {
                    var cityRepository = scope.Resolve<ICityRepository>();

                    var city = cityRepository.Get(p => p.cityName.Contains(citem.CityName));
                    if (city != null)
                    {
                        city.WeatherCityCode = citem.Code;
                        cityRepository.Update(city);
                    }
                }

                if (lev == 1)
                {
                    DG(citem.PyName, lev + 1);
                }

            }
        }

        private static IContainer AutofacConfig()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<EntityFrameworkCityRepository>().As<ICityRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerLifetimeScope();

            return builder.Build();
        }

        private static string WebGet(string uri)
        {
            using (var client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                try
                {
                    string result = client.DownloadString(uri);
                    return result;
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.ToString());
                    return string.Empty;
                }

            }
        }
    }
}
