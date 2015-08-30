using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Configuration;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Threading;
using VA.Cache.Distributed;
using VAGastronomistMobileApp.Model.Adjunction;
using System.Web;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;
using LogDll;


namespace VAGastronomistMobileApp.WebPageDll.Adjunction
{
    public class UserIntegralSend
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(UserIntegralSend));
        private static string userIntegralURI = ConfigurationManager.AppSettings["IntegralServer"];
        private const string INTEGRALOPERATIOAPI = "api/Config/IntegralOperateLists";
        private const string INTEGRALOPERATIONSKEY = "Adjunction_etag:IntegralOperates";
        private static IList<IntegralOperate> integralOperates;

        /// <summary>
        /// 业务接口发送请求
        /// </summary>
        /// <param name="requestJson"></param>
        public void Send<T>(T requestParam, string requestJson)
        {
            try
            {
                if (JsonConvert.DeserializeObject<VANetworkMessage>(requestJson).result != Model.VAResult.VA_OK)
                    return;
                new Thread(() => Run(requestParam)).Start();
            }
            catch (Exception ex)
            {
                LogDll.LogManager.WriteLog(LogFile.Error, DateTime.Now + "Send:业务接口发送请求" + ex.Message);
            }
        }

        /// <summary>
        /// 网页发送请求
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        public void Send(int type, Dictionary<string, object> parameters)
        {
            try
            {
                GetCacheIntegralOperations();
                if (integralOperates != null)
                {
                    IntegralOperate integralOperateEnt = integralOperates.First(p => p.Type == type);
                    string uri = integralOperateEnt.RequestAddress;
                    parameters.Add("ruleTypeId", integralOperateEnt.RuleTypeId);
                    uri = string.Concat(userIntegralURI, uri);
                    string para = string.Join("&", parameters.Select(p => string.Format("{0}={1}", p.Key, p.Value)));

                    string result = RequestHttp(uri, para);

                    if (!string.IsNullOrEmpty(result))
                    {
                        return;
                    }
                }
                new IntegralAnomalyManager().Add(new IntegralAnomaly { Id = CreateCombGuid(), Address = type.ToString(), Parameters = JsonConvert.SerializeObject(parameters) });
            }
            catch (Exception ex)
            {
                LogDll.LogManager.WriteLog(LogFile.Error, DateTime.Now + "Send:网页发送请求" + ex.Message);
            }
        }

        private void Run<T>(T requestParam)
        {
            try
            {
                GetCacheIntegralOperations();
                SendIntegral<T>(requestParam);
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
            }
        }

        private void GetCacheIntegralOperations()
        {
            if (integralOperates == null)
                integralOperates = GetIntegralOperations();
        }

        private void SendIntegral<T>(T requestParam)
        {
            LogDll.LogManager.WriteLog(LogFile.Error, DateTime.Now + "Send:step1");

            var uriAndParams = GetIntegralHttpUriAndParams(requestParam);


            LogDll.LogManager.WriteLog(LogFile.Error, DateTime.Now + "Send:step2");


            if (string.IsNullOrEmpty(uriAndParams.Item1) || string.IsNullOrEmpty(RequestHttp(string.Format("{0}/{1}", userIntegralURI, uriAndParams.Item1), string.Join("&", uriAndParams.Item2.Select(p => string.Format("{0}={1}", p.Key, p.Value))))))
            {

                LogDll.LogManager.WriteLog(LogFile.Error, DateTime.Now + "Send:step3");


                PropertyInfo property = typeof(T).GetProperty("type");
                int type = (int)property.GetValue(requestParam, null);
                new IntegralAnomalyManager().Add(new IntegralAnomaly { Id = CreateCombGuid(), Address = type.ToString(), Parameters = JsonConvert.SerializeObject(uriAndParams.Item2) });
            }

            LogDll.LogManager.WriteLog(LogFile.Error, DateTime.Now + "Send:step4");
        }

        private IList<IntegralOperate> GetIntegralOperations()
        {
            string value = RequestHttp(string.Format("{0}/{1}", userIntegralURI, INTEGRALOPERATIOAPI));
            if (string.IsNullOrEmpty(value))
                return null;
            var Response = JsonConvert.DeserializeObject<Response<IntegralOperateListsResponse>>(value);
            if (Response.Result != Results.Success)
                return null;
            return Response.Content.IntegralOperates;
        }

        private Tuple<string, Dictionary<string, object>> GetIntegralHttpUriAndParams<T>(T requestParam)
        {
            Dictionary<string, object> parameters = GetIntegralParams(requestParam);
            if (integralOperates == null)
                return new Tuple<string, Dictionary<string, object>>(null, parameters);
            PropertyInfo property = typeof(T).GetProperty("type");
            int type = (int)property.GetValue(requestParam, null);
            IntegralOperate integralOperateEnt = integralOperates.First(p => p.Type == type);
            string requestAddress = integralOperateEnt.RequestAddress;
            if (parameters.Any(p => p.Key == "userId"))
                parameters["userId"] = new CustomerManager().SelectCustomerByCookie(parameters["userId"].ToString());
            parameters.Add("ruleTypeId", integralOperateEnt.RuleTypeId.ToString());
            return new Tuple<string, Dictionary<string, object>>(requestAddress, parameters);
        }

        private Dictionary<string, object> GetIntegralParams<T>(T requestParam)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            return (from p in properties
                    where Attribute.IsDefined(p, typeof(UserIntegralParamsAttribute), false)
                    orderby p.Name
                    select p).ToDictionary(p => (Attribute.GetCustomAttribute(p, typeof(UserIntegralParamsAttribute), false) as UserIntegralParamsAttribute).ParamsName ?? p.Name, p => p.GetValue(requestParam, null));
        }

        /// <summary>
        /// 取数据
        /// </summary>
        /// <param name="uri">uri地址</param>
        /// <param name="value">参数</param>
        /// <returns></returns>
        private string RequestHttp(string uri, string value = null)
        {
            string strResult = null;
            Encoding encoding = Encoding.UTF8;
            byte[] byteArray = new byte[0];
            if (!string.IsNullOrEmpty(value))
                byteArray = encoding.GetBytes(value);
            HttpWebResponse response = null;
            HttpWebRequest request = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(uri);
                request.Timeout = 30000;
                request.Method = "POST";
                request.Headers.Set("Pragma", "no-cache");

                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                Stream newStream = request.GetRequestStream();
                //Send the data
                newStream.Write(byteArray, 0, byteArray.Length);
                newStream.Close();

                //Get response
                response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                strResult = streamReader.ReadToEnd();

                request.Abort();
                response.Close();
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
            }
            finally
            {
                if (request != null)
                {
                    request.Abort();
                }
                if (response != null)
                {
                    response.Close();
                }
            }
            return strResult;
        }

        public int Order { set; get; }

        /// <summary>
        /// 以顺序guid
        /// </summary>
        /// <returns></returns>
        public Guid CreateCombGuid()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();

            DateTime baseDate = new DateTime(1900, 1, 1);
            DateTime now = DateTime.Now;

            //Get the days and milliseconds which will be used to build the byte string 
            TimeSpan days = new TimeSpan(now.Ticks - baseDate.Ticks);
            TimeSpan msecs = now.TimeOfDay;

            //Convert to a byte array 
            //Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            //Reverse the bytes to match SQL Servers ordering 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            //Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }

    }
}
