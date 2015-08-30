using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;

namespace UmengPush
{
    public class Common
    {

        /// <summary>
        /// 计算MD5
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetMD5(String s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
            bytes = md5.ComputeHash(bytes);
            md5.Clear();

            StringBuilder strReturn = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                strReturn.Append(Convert.ToString(bytes[i], 16).PadLeft(2, '0'));
            }

            return strReturn.ToString().PadLeft(32, '0');
        }

        /// <summary>  
        /// DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time"> DateTime时间格式</param>  
        /// <returns>Unix时间戳格式</returns>  
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }

        public static string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";

            byte[] postdata = Encoding.UTF8.GetBytes(postDataStr);
            request.ContentLength = postdata.Length;

            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(postdata, 0, postdata.Length);
                reqStream.Close();
            }
            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8"));
            StringBuilder strb = new StringBuilder();
            strb.Append(sr.ReadToEnd());
            return strb.ToString();
        }

        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }

        public static T JsonDeserialize<T>(string jsonString)
        {
            try
            {
                string p = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}";
                MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
                Regex reg = new Regex(p);
                jsonString = reg.Replace(jsonString, matchEvaluator);

                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                T obj = (T)ser.ReadObject(ms);
                return obj;
            }
            catch (System.Exception)
            {//如果解析字符串出现问题，则直接返回空对象
                return default(T);
            }
        }

        /// <summary>
        /// 将时间字符串转为Json时间
        /// </summary>
        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }
    }
}
