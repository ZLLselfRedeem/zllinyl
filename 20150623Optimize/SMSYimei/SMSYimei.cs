using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using VAGastronomistMobileApp.Model;
//
//  Copyright 2011 View Alloc inc. All rights reserved.
//  Created by Jason Xiao on 2012-04-10.
//
namespace SMSYimei
{
    public class SMSYimei
    {
        //调用dll方法
        [DllImport("EUCPComm.dll", EntryPoint = "SendSMS")]  //即时发送
        private static extern int SendSMS(string sn, string mn, string ct, string priority);

        [DllImport("EUCPComm.dll", EntryPoint = "SendSMSEx")]  //即时发送(扩展)
        private static extern int SendSMSEx(string sn, string mn, string ct, string addi, string priority);

        [DllImport("EUCPComm.dll", EntryPoint = "SendScheSMS")]  // 定时发送
        private static extern int SendScheSMS(string sn, string mn, string ct, string ti, string priority);

        [DllImport("EUCPComm.dll", EntryPoint = "SendScheSMSEx")]  // 定时发送(扩展)
        private static extern int SendScheSMSEx(string sn, string mn, string ct, string ti, string addi, string priority);

        [DllImport("EUCPComm.dll", EntryPoint = "ReceiveSMS")]  // 接收短信
        private static extern int ReceiveSMS(string sn, deleSQF mySmsContent);

        [DllImport("EUCPComm.dll", EntryPoint = "ReceiveSMSEx")]  // 接收短信
        private static extern int ReceiveSMSEx(string sn, deleSQF mySmsContent);

        //[DllImport("EUCPComm.dll",EntryPoint="ReceiveStatusReport")]  // 接收短信报告
        //public static extern int ReceiveStatusReport(string sn,delegSMSReport mySmsReport);  

        //[DllImport("EUCPComm.dll",EntryPoint="ReceiveStatusReportEx")]  // 接收短信报告(带批量ID)
        //public static extern int ReceiveStatusReportEx(string sn,delegSMSReportEx mySmsReportEx);  

        [DllImport("EUCPComm.dll", EntryPoint = "Register")]   // 注册 
        private static extern int Register(string sn, string pwd, string EntName, string LinkMan, string Phone, string Mobile, string Email, string Fax, string sAddress, string Postcode);

        [DllImport("EUCPComm.dll", EntryPoint = "GetBalance", CallingConvention = CallingConvention.Winapi)] // 余额 
        private static extern int GetBalance(string m, System.Text.StringBuilder balance);


        [DllImport("EUCPComm.dll", EntryPoint = "ChargeUp")]  // 存值
        private static extern int ChargeUp(string sn, string acco, string pass);

        [DllImport("EUCPComm.dll", EntryPoint = "GetPrice")]  // 价格
        private static extern int GetPrice(string m, System.Text.StringBuilder balance);

        [DllImport("EUCPComm.dll", EntryPoint = "RegistryTransfer")]  //申请转接
        private static extern int RegistryTransfer(string sn, string mn);

        [DllImport("EUCPComm.dll", EntryPoint = "CancelTransfer")]  // 注销转接
        private static extern int CancelTransfer(string sn);

        [DllImport("EUCPComm.dll", EntryPoint = "UnRegister")]  // 注销
        private static extern int UnRegister(string sn);

        [DllImport("EUCPComm.dll", EntryPoint = "SetProxy")]  // 设置代理服务器 
        private static extern int SetProxy(string IP, string Port, string UserName, string PWD);

        [DllImport("EUCPComm.dll", EntryPoint = "RegistryPwdUpd")]  // 修改序列号密码
        private static extern int RegistryPwdUpd(string sn, string oldPWD, string newPWD);




        //回调(接收短信)
        /*回调函数参数说明(收到上行短信的各参数值)
		    mobile：手机号码（当falg=1时有内容）
		    senderaddi：发送者附加号码（当falg=1时有内容），无此项
		    recvaddi：接收者附加号码（当falg=1时有内容），无此项
		    ct：短信内容（当falg=1时有内容）
		    sd：接收时间（当falg=1时有内容，格式：yyyymmddhhnnss）
		    flag：1表示有短信，0表示无短信（不用在处理信息了）
         */
        static void getSMSContent(string mobile, string senderaddi, string recvaddi, string ct, string sd, ref int flag)
        {
            string mob = mobile;
            string content = ct;
            int myflag = flag;
            //MessageBox.Show(mob + "----" + content);
        }

        //声明委托，对回调函数进行封装。
        public delegate void deleSQF(string mobile, string senderaddi, string recvaddi, string ct, string sd, ref int flag);
        deleSQF mySmsContent = new deleSQF(getSMSContent);

        //回调(接收状态报告)
        static void getSMSReport(string mobile, string errorCode, string serviceCodeAdd, string reportType, ref int flag)
        {
            string mob = mobile;
            int myflag = flag;
        }
        public delegate void delegSMSReport(string mobile, string errorCode, string serviceCodeAdd, string reportType, ref int flag);
        delegSMSReport mySmsReport = new delegSMSReport(getSMSReport);

        //回调(接收状态报告)带批量ID
        static void getSMSReportEx(ref long seq, string mobile, string errorCode, string serviceCodeAdd, string reportType, ref int flag)
        {
            string mob = mobile;
            int myflag = flag;
        }
        public delegate void delegSMSReportEx(ref long seq, string mobile, string errorCode, string serviceCodeAdd, string reportType, ref int flag);
        delegSMSReportEx mySmsReportEx = new delegSMSReportEx(getSMSReportEx);
        /*
         * 发送即时短信 SendSMS(这里是软件序列号, 手机号码,短信内容, 优先级)
         * 
         * 参数说明：
         * 软件序列号即注册序列号
         * 手机号码(最多一次发送200个手机号码,号码间用逗号分隔，逗号必须是半角状态的)
         * 短信内容(最多500个汉字或1000个纯英文，emay服务器程序能够自动分割；
         *  亿美有多个通道为客户提供服务，所以分割原则采用最短字数的通道为分割短信长度的规则，
         *  请客户应用程序不要自己分割短信以免造成混乱).亿美推荐短信长度70字以内 [扩展号]默认必须为空
         * 优先级代表优先级，范围1~5，数值越高优先级越高，当亿美通道的短信量特别大的时候，
         * 短信会在通道队列上排队，如果优先级越高，提交短信的速度会越快。
         */
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="serialNumber">短信注册序列号</param>
        /// <param name="phoneNumber">手机号</param>
        /// <param name="content">短信内容</param>
        /// <param name="priority">优先级:1-5,值越高优先级越高</param>
        /// <returns></returns>
        public static VAResult SendYimeiSMS(string serialNumber, string phoneNumber, string content, string priority)
        {//即时发送                
            int result = 0;
            int smsResult = (int)VAResult.VA_FAILED_SMS_OTHER;
            try
            {
                result = SendSMS(serialNumber, phoneNumber, content, priority);
            }
            catch (System.Exception)
            {
            }
            //if (result == 1)
            //    MessageBox.Show("发送成功");
            //else if (result == 101)
            //    MessageBox.Show("网络故障");
            //else if (result == 102)
            //    MessageBox.Show("其它故障");
            //else if (result == 0)
            //    MessageBox.Show("失败");
            //else if (result == 100)
            //    MessageBox.Show("序列号码为空或无效");
            //else if (result == 107)
            //    MessageBox.Show("手机号码为空或者超过1000个");
            //else if (result == 108)
            //    MessageBox.Show("手机号码分割符号不正确");
            //else if (result == 109)
            //    MessageBox.Show("部分手机号码不正确，已删除，其余手机号码被发送");
            //else if (result == 110)
            //    MessageBox.Show("短信内容为空或超长（70个汉字）");
            //else if (result == 201)
            //    MessageBox.Show("计费失败，请充值");
            //else
            //    MessageBox.Show("其他故障值：" + result.ToString());
            ;
            switch (result)
            {
                case 1:
                    smsResult = (int)VAResult.VA_OK;
                    break;
                //case 100:
                //    smsResult = (int)VAResult.VA_FAILED_SMS_SERIALNUMBER_INVALID;
                //    break;
                case 107:
                    smsResult = (int)VAResult.VA_FAILED_SMS_PHONENUMBER_INVALID;
                    break;
                case 201:
                    smsResult = (int)VAResult.VA_FAILED_SMS_MONEY_NOT_ENOUGH;
                    break;
                default:
                    smsResult = (int)VAResult.VA_FAILED_SMS_OTHER;
                    break;
            }
            return (VAResult)smsResult;
        }
    }
}
