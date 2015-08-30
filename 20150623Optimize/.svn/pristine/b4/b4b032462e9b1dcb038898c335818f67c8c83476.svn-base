using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMS.JianzhouService;
using System.Configuration;
using VAGastronomistMobileApp.Model;
using System.Xml;

namespace SMS
{
    public class SMSJianzhou
    {
        SMS.JianzhouService.BusinessServiceClient jianzhouClient = new BusinessServiceClient();

        string softwareSerialNo = ConfigurationManager.AppSettings["jianzhouSerialNo"].ToString();//序列号
        string key = ConfigurationManager.AppSettings["jianzhoukey"].ToString();

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobilePhone">多个手机号码用;分割，多个手机号码用;分割</param>
        /// <param name="message">短信内容</param>
        /// <returns></returns>
        public VAResult SendMessage(string mobilePhone, string message)
        {
            int result = jianzhouClient.sendBatchMessage(softwareSerialNo, key, mobilePhone, message);

            if (result > 0)
            {
                result = (int)VAResult.VA_OK;
            }
            else
            {
                switch (result)
                {
                    //余额不足
                    case -1:
                        result = (int)VAResult.VA_FAILED_SMS_MONEY_NOT_ENOUGH;
                        break;
                    //1.序列号未注册2.密码加密不正确3.密码已被修改4.序列号已注销
                    case -2:
                        result = (int)VAResult.VA_FAILED_SMS_USERID_INVALID;
                        break;
                    //其他错误
                    default:
                        result = (int)VAResult.VA_FAILED_SMS_OTHER;
                        break;
                }
            }
            return (VAResult)result;
        }
        /// <summary>
        /// 查询余额
        /// </summary>
        /// <returns></returns>
        public string GetBalance()
        {
            string result = jianzhouClient.getUserInfo(softwareSerialNo, key);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(result);
            XmlNode xn = xml.SelectSingleNode("userinfo/remainFee");
            return xn.InnerText;
        }
    }
}
