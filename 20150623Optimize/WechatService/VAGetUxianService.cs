using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.WebPageDll;
using System.Data;
using Senparc.Weixin.MP.Entities;
using VAGastronomistMobileApp.Model;
using System.Net;
using System.IO;

namespace WechatService
{
    public class VAGetUxianService
    {
        //悠先合作
        public static string GetUxianCooperation()
        {
            WechatUxianCooperationOperate wco = new WechatUxianCooperationOperate();
            string sret = wco.GetCooperation();
            if (string.IsNullOrEmpty(sret))
                return "Coming Soon...";
            else
                return sret;
        }
        //根据openId获取用户的基本信息subscribe,openid,nickname,sex,language,city,province,country,headimgurl,subscribe_time
        //subscribe值为0时，代表此用户没有关注该公众号，拉取不到其余信息
        //private static void GetCustomerInfoByOpenIdAndInsert(string openId)
        //{
        //    string uriStr = "https://api.weixin.qq.com/cgi-bin/user/info?access_token=" + token + "&openid=" + openId;
        //    try
        //    {
        //        HttpWebRequest request = WebRequest.Create(uriStr) as HttpWebRequest;
        //        request.Method = "GET";
        //        request.ServicePoint.Expect100Continue = false;
        //        request.Timeout = 1000 * 60;
        //        request.ContentType = "application/x-www-form-urlencoded";

        //        WebResponse webResponse = request.GetResponse();

        //        StreamReader responseReader = new StreamReader(webResponse.GetResponseStream());

        //        string responseData = responseReader.ReadToEnd();
        //        //string json = "{\"subscribe\": 1, \"openid\": \"o6_bmjrPTlm6_2sgVt7hMZOPfL2M\", \"nickname\": \"Band\", \"sex\": 1, \"language\": \"zh_CN\", \"city\": \"广州\", \"province\": \"广东\", \"country\": \"中国\", \"headimgurl\":\"http://wx.qlogo.cn/mmopen/g3MonUZtNHkdmzicIlibx6iaFqAc56vxLSUfpb6n5WKSYVY0ChQKkiaJSgQ1dZuTOgvLLrhJbERQQ4eMsv84eavHiaiceqxibJxCfHe/0\", \"subscribe_time\": 1382694957}";
        //        WechatCustomerInfo cInfo = JsonOperate.JsonDeserialize<WechatCustomerInfo>(responseData);

        //        WechatCustomerOperate wco = new WechatCustomerOperate();
        //        wco.Insert(cInfo);
        //    }
        //    catch { }
        //}

        //新增合作信息
        public static void InsertUxianCooperation(RequestMessageText textRequestMessage)
        {
            WechatUxianCooperationOperate wco = new WechatUxianCooperationOperate();
            WechatMerchantSendInfo merchantSendInfo = new WechatMerchantSendInfo();
            merchantSendInfo.msgContent = textRequestMessage.Content;
            merchantSendInfo.senderWechatID = textRequestMessage.FromUserName; //openId
            merchantSendInfo.receiveDateTime = DateTime.Now.ToString();
            merchantSendInfo.status = "0";

            wco.InsertMerchantSendInfo(merchantSendInfo);

            //GetCustomerInfoByOpenIdAndInsert(textRequestMessage.FromUserName);
        }

        //常见问答
        public static string GetUxianQandA()
        {
            WechatUxianQandAOperate wqa = new WechatUxianQandAOperate();
            DataTable dt = wqa.GetUxianQandA();
            StringBuilder sb = new StringBuilder();
            if (dt != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append((i + 1) + ".");
                    sb.Append(dt.Rows[i][0].ToString() + "\n");
                    sb.Append(dt.Rows[i][1].ToString() + "\n");
                }
            }
            else
                sb.Append("Coming Soon...");

            return sb.ToString();
        }

        //意见建议
        public static string GetUxianProposal()
        {
            WechatUxianProposalOperate wpo = new WechatUxianProposalOperate();
            string sret = wpo.GetUxianProposal();
            if (string.IsNullOrEmpty(sret))
                return "Coming Soon...";
            else
                return sret;
        }
        //新增意见建议
        public static void InsertUxianProposal(RequestMessageText textRequestMessage)
        {
            WechatUxianProposalOperate wpo = new WechatUxianProposalOperate();
            WechatProposalReceiveInfo proposalReceiveInfo = new WechatProposalReceiveInfo();
            proposalReceiveInfo.contentType = (int)textRequestMessage.MsgType; //0 Text, 1 Location, 2 Image, 3 Voice, 4 Link, 5 Event
            proposalReceiveInfo.msgContent = textRequestMessage.Content;
            proposalReceiveInfo.senderWechatID = textRequestMessage.FromUserName;
            proposalReceiveInfo.receiveDateTime = DateTime.Now.ToString();
            proposalReceiveInfo.voicefileName = "";
            proposalReceiveInfo.status = "0";

            wpo.InsertProposalReceiveInfo(proposalReceiveInfo);

            //GetCustomerInfoByOpenIdAndInsert(textRequestMessage.FromUserName);
        }
        public static void InsertUxianProposal(RequestMessageVoice voiceRequestMessage)
        {
            WechatUxianProposalOperate wpo = new WechatUxianProposalOperate();
            WechatProposalReceiveInfo proposalReceiveInfo = new WechatProposalReceiveInfo();
            proposalReceiveInfo.contentType = (int)voiceRequestMessage.MsgType; //0 Text, 1 Location, 2 Image, 3 Voice, 4 Link, 5 Event
            proposalReceiveInfo.voicefileName = voiceRequestMessage.MediaId; //只提供一个ID，语音文件应该存在 微信服务端， 那我们的后台管理页面怎么获取到这个 语音文件？
            proposalReceiveInfo.senderWechatID = voiceRequestMessage.FromUserName;
            proposalReceiveInfo.receiveDateTime = DateTime.Now.ToString();
            proposalReceiveInfo.msgContent = "";
            proposalReceiveInfo.status = "0";

            wpo.InsertProposalReceiveInfo(proposalReceiveInfo);

            //GetCustomerInfoByOpenIdAndInsert(voiceRequestMessage.FromUserName);
        }

        //投诉处理
        public static string GetUxianComplaint()
        {
            WechatUxianComplaintOperate wco = new WechatUxianComplaintOperate();
            string sret = wco.GetUxianComplaint();
            if (string.IsNullOrEmpty(sret))
                return "Coming Soon...";
            else
                return sret;
        }
        //新增投诉信息
        public static void InsertUxianComplaint(RequestMessageText textRequestMessage)
        {
            WechatUxianComplaintOperate wco = new WechatUxianComplaintOperate();
            WechatComplaintReceiveInfo complaintReceiveInfo = new WechatComplaintReceiveInfo();
            complaintReceiveInfo.contentType = (int)textRequestMessage.MsgType;
            complaintReceiveInfo.msgContent = textRequestMessage.Content;
            complaintReceiveInfo.senderWechatID = textRequestMessage.FromUserName;
            complaintReceiveInfo.receiveDateTime = DateTime.Now.ToString();
            complaintReceiveInfo.voicefileName = "";
            complaintReceiveInfo.status = "0";

            wco.InsertComplaintReceiveInfo(complaintReceiveInfo);

            //GetCustomerInfoByOpenIdAndInsert(textRequestMessage.FromUserName);
        }
        public static void InsertUxianComplaint(RequestMessageVoice voiceRequestMessage)
        {
            WechatUxianComplaintOperate wco = new WechatUxianComplaintOperate();
            WechatComplaintReceiveInfo complaintReceiveInfo = new WechatComplaintReceiveInfo();
            complaintReceiveInfo.contentType = (int)voiceRequestMessage.MsgType;
            complaintReceiveInfo.voicefileName = voiceRequestMessage.MediaId;
            complaintReceiveInfo.senderWechatID = voiceRequestMessage.FromUserName;
            complaintReceiveInfo.receiveDateTime = DateTime.Now.ToString();
            complaintReceiveInfo.msgContent = "";
            complaintReceiveInfo.status = "0";

            wco.InsertComplaintReceiveInfo(complaintReceiveInfo);

            //GetCustomerInfoByOpenIdAndInsert(voiceRequestMessage.FromUserName);
        }
    }
}
