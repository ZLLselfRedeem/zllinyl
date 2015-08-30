using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using VAGastronomistMobileApp.Model;

namespace SMS
{
    public class SMSMandao
    {
        MandaoService.WebServiceSoapClient mandaoClient = new MandaoService.WebServiceSoapClient();

        string softwareSerialNo = ConfigurationManager.AppSettings["mandaoSerialNo"].ToString();//序列号
        string key = ConfigurationManager.AppSettings["mandaokey"].ToString();

        /// <summary>
        /// 获取md5(sn+password) 32位大写密文
        /// </summary>
        /// <returns></returns>
        private string GetPassword()
        {
            string key = ConfigurationManager.AppSettings["mandaokey"].ToString();
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(softwareSerialNo + key));
            return System.Text.Encoding.UTF8.GetString(result).ToUpper();
        }

        /// <summary>
        /// 获取账号基本信息
        /// </summary>
        public string GetMandaoInfo()
        {
            string mandaoMessage = mandaoClient.mdgetSninfo(softwareSerialNo, key);
            MandaoInfo mandao = JsonDeserialize<MandaoInfo>(mandaoMessage);
            if (Convert.ToInt32(mandao.Balance) > 0)
            {
                return (Convert.ToInt32(mandao.Balance) / 100).ToString();
            }
            else
            {
                return "0";
            }
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

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobilePhone">支持10000个手机号,建议<=5000，多个英文逗号隔开</param>
        /// <param name="message">短信内容</param>
        /// <param name="assignSendTime">定时时间，例如：2010-12-29 16:27:03（置空表示立即发送）</param>
        /// <returns></returns>
        public VAResult SendMessage(string mobilePhone, string message, string assignSendTime = "")
        {
            string content = HttpUtility.UrlEncode(message, System.Text.UTF8Encoding.UTF8);
            long result = Convert.ToInt64(mandaoClient.mdsmssend(softwareSerialNo, key, mobilePhone, content, "", assignSendTime, "", ""));

            if (result > 0)
            {
                result = (int)VAResult.VA_OK;
            }
            else
            {
                switch (result)
                {
                    //1.序列号未注册2.密码加密不正确3.密码已被修改4.序列号已注销
                    case -2:
                        result = (int)VAResult.VA_FAILED_SMS_USERID_INVALID;
                        break;
                    //余额不足
                    case -4:
                        result = (int)VAResult.VA_FAILED_SMS_MONEY_NOT_ENOUGH;
                        break;
                    //参数有误
                    case -6:
                        result = (int)VAResult.VA_FAILED_SMS_PARAMETER_ERROR;
                        break;
                    //内容长度长，单字节不能超过1000个字符，双字节不能超过500个字符
                    case -10:
                        result = (int)VAResult.VA_FAILED_SMS_MESSAGE_TOOLONG;
                        break;
                    //其他错误
                    default:
                        result = (int)VAResult.VA_FAILED_SMS_OTHER;
                        break;
                }
            }
            return (VAResult)result;
        }
    }
    public class MandaoInfo
    {
        public string Balance { get; set; }
        public string SN { get; set; }
        //{"Balance":4278300,"Flag":0,"ID":238210,"Memo":"","MoUrl":"","OldJifen":0,"PassWord":"E72C62C5BAA39F691E80836772E0CEAD","Priority":0,"RegistryStateId":2,"SN":"SDK-BBX-010-21781","Scode":"236277","Serverid":0,"ip":""}
    }
}
