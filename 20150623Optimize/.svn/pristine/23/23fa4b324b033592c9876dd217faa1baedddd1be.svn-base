using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using System.Configuration;
using System.IO;

namespace SMSYimei
{
    public class SMSYimeiVoice
    {
        YimeiService.SDKService YimeiService = new YimeiService.SDKService();

        string softwareSerialNo = ConfigurationManager.AppSettings["softwareSerialNo"].ToString();//序列号
        string key = ConfigurationManager.AppSettings["key"].ToString();
        string serialpass = ConfigurationManager.AppSettings["serialpass"].ToString();//序列号密码
        string srcCharset = ConfigurationManager.AppSettings["srcCharset"].ToString();//字符编码
        int smsPriority = Convert.ToInt32(ConfigurationManager.AppSettings["smsPriority"]);//优先级
        string addSerial = string.Empty;//扩展号码

        /// <summary>
        /// 注册序列号
        /// </summary>
        /// <param name="softwareSerialNo">软件序列号</param>
        /// <param name="key">要注册的关键字</param>
        /// <param name="serialpass">软件序列号密码，密码（6位）</param>
        /// <returns></returns>
        public VAResult RegisterSN()
        {
            int result = -1;//调用方法返回值
            int returnResult = 0;//返回给请求方的结果值
            try
            {
                result = YimeiService.registEx(softwareSerialNo, key, serialpass);
                switch (result)
                {
                    case 0:
                        returnResult = (int)VAResult.VA_OK;
                        break;
                    default:
                         returnResult = (int)VAResult.VA_FAILED_SMS_REGISTER;
                        break;
                }
            }
            catch (Exception)
            { }
            return (VAResult)returnResult;
        }

        /// <summary>
        /// 注销序列号
        /// </summary>
        /// <returns></returns>
        public VAResult Logout()
        {
          int result = -1;//调用方法返回值
            int returnResult = 0;//返回给请求方的结果值
            try
            {
                result = YimeiService.logout(softwareSerialNo,key);
               switch (result)
                {
                    case 0:
                        returnResult = (int)VAResult.VA_OK;
                        break;             
                    default:
                        returnResult = (int)VAResult.VA_FAILED_SMS_LOGOUT;
                        break;
                }
            }
            catch(Exception)
            {}
             return (VAResult)returnResult;
        }

        /*
         短信发送函数,可发送即时短信，也可发送定时短信，
         当sendTime不为空且是正确的时间各式，那么该条短信就是定时短信；
         当sendTime值为空时，则为即时短信         
         
         Key 要注册的关键字，必须输入
        1．用户自定义key值， 长度不超过15个字符的字符串(可包含数字和字母)
        2．将key做好备份，不要遗忘
        3．请做好保密工作，因客户自身原因泄露该关键字造成的经济损失北京亿美软通科技有限公司不承担任何责任
                 
         短信内容(最多500个汉字或1000个纯英文，emay服务器程序能够自动分割；
         亿美有多个通道为客户提供服务，所以分割原则采用最短字数的通道为分割短信长度的规则，
         请客户应用程序不要自己分割短信以免造成混乱)
         
         扩展号码 (长度小于15的字符串) 用户可通过附加码自定义短信类别
         扩展号码的功能，需另外申请，当未申请扩展号码功能时，该参数默认为空值即可。

         短信ID，自定义唯一的消息ID，数字位数最大19位，与状态报告ID一 一对应，
         需用户自定义ID规则确保ID的唯一性。如果smsID为0将获取不到相应的状态报告信息。
         */

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="softwareSerialNo">软件序列号</param>
        /// <param name="key"></param>
        /// <param name="sendTime">定时短信的定时时间，格式为:年年年年月月日日时时分分秒秒</param>
        /// <param name="mobiles">手机号码(字符串数组,最多为200个手机号码)</param>
        /// <param name="smsContent">短信内容，最多500个汉字或1000个纯英文</param>
        /// <param name="addSerial">扩展号码</param>
        /// <param name="srcCharset">字符编码，默认为"GBK"</param>
        /// <param name="smsPriority">短信等级，范围1~5，数值越高优先级越高</param>
        /// <param name="smsID">短信ID</param>
        /// <returns></returns>
        public VAResult SendMessage(string sendTime, string[] mobiles, string smsContent, long smsID)
        {
            int result = -1;//调用方法返回值
            int returnResult = 0;//返回给请求方的结果值

            try
            {
                result = YimeiService.sendSMS(softwareSerialNo, key, sendTime, mobiles, smsContent, addSerial, srcCharset, smsPriority, smsID);


                switch (result)
                {
                    case 0://成功
                        returnResult = (int)VAResult.VA_OK;
                        break;
                    default:
                         returnResult = (int)VAResult.VA_FAILED_SMS_NOT_SEND;
                        break;
                }
            }
            catch (Exception)
            { }
            return (VAResult)returnResult;
        }

        /*
         功能介绍
         语音验证码发送函数,可发送即时语音验证码，也可发送定时语音验证码，
         当sendTime不为空且是正确的时间各式，那么该条语音验证码就是定时语音验证码；
         当sendTime值为空时，则为即时语音验证码。
         
         语音验证码(长度≥4且≤6，格式必须为0~9的全英文半角数字字符)
         
         语音验证码序列ID，自定义唯一的序列ID，数字位数最大19位，与状态报告ID一一对应，
         需用户自定义ID规则确保ID的唯一性。如果smsID为0将获取不到相应的状态报告信息。
         该参数与短信smsID作用相同仅在语音验证码支持状态报告时有实际意义,
         与之对应的语音状态报告与短信状态报告接口共用。
         
         返回值：0:xxxx
         0表示语音验证码发送成功
         注：语言验证码返回值由两部分组成（由“:”号分割）如0:1873，前部分为状态值，后部分为4位随机码，用户只关心前部分即可。

         */
        /// <summary>
        /// 发送语音验证码
        /// </summary>
        /// <param name="softwareSerialNo">软件序列号</param>
        /// <param name="key"></param>
        /// <param name="sendTime">定时语音验证码的定时时间，格式为:年年年年月月日日时时分分秒秒</param>
        /// <param name="mobiles">手机号码</param>
        /// <param name="checkCode">语音验证码</param>
        /// <param name="addSerial">发送语音验证码时此项无实际意义可设定为null</param>
        /// <param name="srcCharset">字符编码，默认为"GBK"</param>
        /// <param name="smsPriority">语音验证码等级，范围1~5，数值越高优先级越高</param>
        /// <param name="smsID">语音验证码序列ID</param>
        /// <returns></returns>
        public VAResult SendVoice(string sendTime, string[] mobiles, string checkCode, long smsID)
        {
            string result = string.Empty;//调用方法返回值
            int returnResult = 0;//返回给请求方的结果值
            try
            {
                result = YimeiService.sendVoice(softwareSerialNo, key, sendTime, mobiles, checkCode, addSerial, srcCharset, smsPriority, smsID);

                string[] resultSplit = result.Split(':');
                switch (resultSplit[0])
                {
                    case "0"://成功
                        returnResult = (int)VAResult.VA_OK;
                        break;
                    default:
                        returnResult = (int)VAResult.VA_FAILED_SMS_NOT_SEND;
                        break;
                }
            }
            catch (Exception)
            {

            }
            return (VAResult)returnResult;
        }

        /// <summary>
        /// 获得序列号剩余金额
        /// </summary>
        /// <returns></returns>
        public double getBalance()
        {
            double balance = 0;
            try
            {
                balance = YimeiService.getBalance(softwareSerialNo, key);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return balance;
        }

        /// <summary>
        /// 获取发送一条短信所需要的费用
        /// </summary>
        /// <returns></returns>
        public double getEachFee()
        {
            double fee = 0;
            try
            {
                fee = YimeiService.getEachFee(softwareSerialNo, key);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
            return fee;
        }

        /*获得充值卡后，调用此方法传入您的序列号KEY，卡号及密码即可对该序列号充值。*/

        /// <summary>
        /// 序列号充值
        /// </summary>
        /// <param name="softwareSerialNo">软件序列号</param>
        /// <param name="key">关键字</param>
        /// <param name="cardNo">充值卡号</param>
        /// <param name="cardPass">充值卡密码</param>
        /// <returns></returns>
        public VAResult chargeUp(string softwareSerialNo, string key, string cardNo, string cardPass)
        {
            int result = -1;//调用方法返回值
            int returnResult = 0;//返回给请求方的结果值
            try
            {
                result = YimeiService.chargeUp(softwareSerialNo, key, cardNo, cardPass);
                switch (result)
                {
                    case 0://充值成功
                        returnResult = (int)VAResult.VA_OK;
                        break;
                    default://充值失败
                        returnResult = (int)VAResult.VA_FAILED_SMS_CHARGEUP;
                        break;
                }
            }
            catch (Exception)
            { }
            return (VAResult)returnResult;
        }
    }
}
