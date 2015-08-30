using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
namespace VAGastronomistMobileApp.WebPageDll
{
    /// <summary>
    ///Json序列化和反序列化类
    /// </summary>
    public class JsonOperate
    {
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
