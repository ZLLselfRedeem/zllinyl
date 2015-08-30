using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.Model;
using Senparc.Weixin.MP.Entities;
using VAGastronomistMobileApp.WebPageDll;

namespace WechatService
{
    public class VARecordWechatMsg
    {
        //记录微信客户端发来的文本消息，语音消息(记录下media_id）通过平台+token+voice 再去下载语音文件
        public static int InsertReceivedMsg(RequestMessageText textRequestMessage = null, RequestMessageVoice voiceRequestMessage = null)
        {
            WechatReceivedMsg receivedMsg = new WechatReceivedMsg();
            if (textRequestMessage != null)
            {
                receivedMsg.msgContent = textRequestMessage.Content;
                receivedMsg.contentType = (int)textRequestMessage.MsgType;
                receivedMsg.senderWechatID = textRequestMessage.FromUserName;
                receivedMsg.media_id = "";
            }
            if (voiceRequestMessage != null)
            {
                receivedMsg.msgContent = "";
                receivedMsg.contentType = (int)voiceRequestMessage.MsgType;
                receivedMsg.senderWechatID = voiceRequestMessage.FromUserName;
                receivedMsg.media_id = voiceRequestMessage.MediaId;
            }
            receivedMsg.receiveDateTime = DateTime.Now.ToString();
            receivedMsg.status = 0;

            try
            {
                WechatReceivedMsgOperate wmo = new WechatReceivedMsgOperate();
                return wmo.InsertReceivedMsg(receivedMsg);
            }
            catch { return -1; }
        }
    }
}
