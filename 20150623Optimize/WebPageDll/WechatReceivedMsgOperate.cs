using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VAGastronomistMobileApp.SQLServerDAL;
using VAGastronomistMobileApp.Model;

namespace VAGastronomistMobileApp.WebPageDll
{
    public class WechatReceivedMsgOperate
    {
        private WechatReceivedMsgManager receivedMsgMan;
        public WechatReceivedMsgOperate()
        {
            if (receivedMsgMan == null)
                receivedMsgMan = new WechatReceivedMsgManager();
        }
        //新增微信客户端发来的信息，文字，语音
        public int InsertReceivedMsg(WechatReceivedMsg receivedMsg)
        {
            return receivedMsgMan.InsertReceivedMsg(receivedMsg);
        }
    }
}
