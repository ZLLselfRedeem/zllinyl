using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.Net;
using System.IO;
using System.IO.Compression;

namespace NotificationService
{
    public class Jpush
    {
        public static string MD5Encryte(string strSource)
        {
            return MD5Encryte(strSource, 32);
        }

        private static string MD5Encryte(string strSource, int length)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(strSource);
            byte[] hashValue = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            switch (length)
            { 
                case 16:
                    for (int i = 4; i < 12; i++)
                        sb.Append(hashValue[i].ToString("x2"));
                    break;
                case 32:
                    for (int i = 0; i < 16; i++)
                        sb.Append(hashValue[i].ToString("x2"));
                    break;
                default :
                    for (int i = 0; i < hashValue.Length; i++)
                        sb.Append(hashValue[i].ToString("x2"));
                    break;
            }

            return sb.ToString();
        }
        //result : {"sendno":"1","msg_id":"474944259","errcode":0,"errmsg":"Succeed"} 
        public static string DoSend(long sendno, string receiverValue, string sendMsg, string extras, string type)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            string html = string.Empty;
            //int sendno = 1;
            string username="1014209011";
            //string receiverValue = "605990"; //这是别名
            //接收者类型。
            //1、指定的 IMEI。此时必须指定 appKeys。
            //2、指定的 tag。
            //3、指定的 alias。
            //4、 对指定 appkey 的所有用户推送消息。
            int receiverType = 3;
            string appkeys = "843bf421ef84beb6d09f5dc4";

            string input = sendno.ToString() + receiverType + receiverValue + "92bed2b84ab35aaf7affc23e";
            string verificationCode = MD5Encryte(input);
            //string content = "{\"n_content\":\"" + sendMsg + "\",\"n_builder_id\":\"1\",\"n_extras\":\"" + extras + "\"}"; //发送的内容
            string content = string.Empty;
            if (type == "2")
            {
                
                content = "{\"message\":\"" + sendMsg + "\",\"extras\":\"" + extras + "\"}"; //发送的内容
            } 
            else
            {
                content = "{\"n_content\":\"" + sendMsg + "\",\"n_extras\":" + extras + "}"; //发送的内容
                type = "1";
            }
            string loginUrl = "http://api.jpush.cn:8800/v2/push";
            parameters.Add("username", username);
            parameters.Add("sendno", sendno.ToString());
            parameters.Add("app_key", appkeys);
            parameters.Add("receiver_type", receiverType.ToString());
            parameters.Add("receiver_value", receiverValue);  
            parameters.Add("verification_code", verificationCode);
            parameters.Add("msg_type", type);//1.通知，2自定义消息
            parameters.Add("msg_content", content); //内容
            parameters.Add("platform", "android");

            HttpWebResponse response = HttpWebResponseUtility.CreatePostHttpResponse(loginUrl, parameters, null, null, Encoding.UTF8, null);

            if (response != null)
            { 
                //得到返回的数据流
                Stream receiveStream = response.GetResponseStream();
                //如果有压缩，则进行解压
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    receiveStream = new GZipStream(receiveStream, CompressionMode.Decompress);
                }

                //得到返回的字符串
                html = new StreamReader(receiveStream).ReadToEnd();
            }

            return html;
        }

        public static string DoSend(long sendno, string receiverValue, string sendMsg, string extras, string type, string username, string appkeys, string master_secret)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            string html = string.Empty;
            //int sendno = 1;
            //string receiverValue = "605990"; //这是别名
            //接收者类型。
            //1、指定的 IMEI。此时必须指定 appKeys。
            //2、指定的 tag。
            //3、指定的 alias。
            //4、 对指定 appkey 的所有用户推送消息。
            int receiverType = 3;

            string input = sendno.ToString() + receiverType + receiverValue + master_secret;
            string verificationCode = MD5Encryte(input);
            //string content = "{\"n_content\":\"" + sendMsg + "\",\"n_builder_id\":\"1\",\"n_extras\":\"" + extras + "\"}"; //发送的内容
            string content = string.Empty;
            if (type == "2")
            {

                content = "{\"message\":\"" + sendMsg + "\",\"extras\":\"" + extras + "\"}"; //发送的内容
            }
            else
            {
                content = "{\"n_content\":\"" + sendMsg + "\",\"n_extras\":" + extras + "}"; //发送的内容
                type = "1";
            }
            string loginUrl = "http://api.jpush.cn:8800/v2/push";
            parameters.Add("username", username);
            parameters.Add("sendno", sendno.ToString());
            parameters.Add("app_key", appkeys);
            parameters.Add("receiver_type", receiverType.ToString());
            parameters.Add("receiver_value", receiverValue);
            parameters.Add("verification_code", verificationCode);
            parameters.Add("msg_type", type);//1.通知，2自定义消息
            parameters.Add("msg_content", content); //内容
            parameters.Add("platform", "android");

            HttpWebResponse response = HttpWebResponseUtility.CreatePostHttpResponse(loginUrl, parameters, null, null, Encoding.UTF8, null);

            if (response != null)
            {
                //得到返回的数据流
                Stream receiveStream = response.GetResponseStream();
                //如果有压缩，则进行解压
                if (response.ContentEncoding.ToLower().Contains("gzip"))
                {
                    receiveStream = new GZipStream(receiveStream, CompressionMode.Decompress);
                }

                //得到返回的字符串
                html = new StreamReader(receiveStream).ReadToEnd();
            }

            return html;
        }
    }
}