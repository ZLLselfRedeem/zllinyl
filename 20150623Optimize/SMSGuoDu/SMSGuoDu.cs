using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Net;
using System.IO;
using System.Web;
using System.Xml;

//
//  Copyright 2014 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2014-02-12.
//
namespace SMSGuoDu
{
    public class SMSGuoDu
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mob">手机号码(多个用逗号,分隔,最多200个，超过将被忽略)</param>
        /// <param name="msg">短信内容(60-70个字，超过自己做下循环)</param>
        /// <returns></returns>
        public static VAResult SentMessage(string userName, string pwd, string mob, string msg)
        {
            VAResult smsResult = VAResult.VA_FAILED_SMS_OTHER;
            try
            {
                string url = "http://221.179.180.158:9007/QxtSms/QxtFirewall";

                msg = HttpUtility.UrlEncode(msg, Encoding.GetEncoding("GBK"));
                string msgToSend = "OperID=" + userName + "&OperPass=" + pwd + "&SendTime=" + "" + "&ValidTime=" + "" + "&AppendID=" + "" + "&DesMobile=" + mob.Trim() + "&Content=" + msg + "&ContentType=" + "8";

                /*使用post方式发送消息*/

                string result = PostHttpRequest(msgToSend, url);
                result = result.Substring(result.IndexOf("<code>") + 6, 2);
                switch (result)
                {
                    case "01"://批量短信提交成功
                    case "03"://单条短信提交成功
                        smsResult = VAResult.VA_OK;
                        break;
                    default://其它错误！
                        smsResult = VAResult.VA_FAILED_SMS_OTHER;
                        break;
                }
            }
            catch (System.Exception)
            {

            }
            return smsResult;
        }

        /// <summary>
        /// 查询余额（剩余短信条数）
        /// </summary>
        /// <param name="operId"></param>
        /// <param name="operPass"></param>
        /// <returns></returns>
        public static string CheckBalance(string operId, string operPass)
        {
            string banlance = string.Empty;
            try
            {
                string url = "http://221.179.180.158:8081/QxtSms_surplus/surplus?OperID=" + operId + "&OperPass=" + operPass + "";

                string result = SendGetHttpRequest(url);
                banlance = GetStrForXmlDoc(result, "resRoot/rcode");
            }
            catch (Exception)
            { }
            return banlance;
        }

        /// <summary>
        /// 采用Post方式发送Http链接
        /// </summary>
        /// <param name="postData"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        private static string PostHttpRequest(string postData, string url, int timeout = 3000)
        {
            string result = string.Empty;
            HttpWebRequest myRequest = null;
            HttpWebResponse myResponse = null;
            try
            {
                Encoding encoding = Encoding.GetEncoding("GBK");
                byte[] data = encoding.GetBytes(postData);

                System.GC.Collect();
                // Prepare web request... 
                myRequest = (HttpWebRequest)WebRequest.Create(url);
                myRequest.Timeout = timeout;
                myRequest.ReadWriteTimeout = 500;
                myRequest.ProtocolVersion = HttpVersion.Version10;
                myRequest.Proxy = null;
                myRequest.KeepAlive = false;

                myRequest.Method = "POST";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = data.Length;

                Stream newStream = myRequest.GetRequestStream();

                // Send the data. 
                newStream.Write(data, 0, data.Length);
                newStream.Close();

                // Get response 
                myResponse = (HttpWebResponse)myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.GetEncoding("gbk"));
                result = reader.ReadToEnd();
                myRequest.Abort();
                myResponse.Close();

            }
            catch (System.Exception)
            {
                if (myRequest != null)
                {
                    myRequest.Abort();
                }
                if (myResponse != null)
                {
                    myResponse.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// 发送请求
        /// </summary>
        /// <param name="url">Url地址</param>
        /// <param name="method">方法（post或get）</param>
        /// <param name="method">数据类型</param>
        /// <param name="requestData">数据</param>
        public static string SendGetHttpRequest(string url)
        {
            WebRequest request = (WebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/x-www-form-urlencoded";
            string result = string.Empty;
            using (WebResponse response = request.GetResponse())
            {
                if (response != null)
                {
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 返回 XML字符串 节点value
        /// </summary>
        /// <param name="xmlDoc">XML格式 数据</param>
        /// <param name="xmlNode">节点</param>
        /// <returns>节点value</returns>
        public static string GetStrForXmlDoc(string xmlDoc, string xmlNode)
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlDoc);
            XmlNode xn = xml.SelectSingleNode(xmlNode);
            return xn.InnerText;
        }
    }
}
