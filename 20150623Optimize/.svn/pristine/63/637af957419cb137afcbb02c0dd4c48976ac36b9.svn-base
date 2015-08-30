using System;
using System.Diagnostics;
using System.Web;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.Helpers;

namespace WechatService
{
    /// <summary>
    /// 事件处理程序，此代码的简化MessageHandler方法已由/CustomerMessageHandler/CustomerMessageHandler_Event.cs完成，
    /// 此文件不再更新。
    /// </summary>
    public class EventService
    {
        public ResponseMessageBase GetResponseMessage(RequestMessageEventBase requestMessage)
        {
            ResponseMessageBase responseMessage = null;
            switch (requestMessage.Event)
            {
                case Event.ENTER:
                    {
                        var strongResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
                        strongResponseMessage.Content = "您刚才发送了ENTER事件请求。";
                        responseMessage = strongResponseMessage;
                        break;
                    }
                case Event.LOCATION:
                    //throw new Exception("暂不可用");
                    break;
                case Event.subscribe://订阅
                    {
                        var strongResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();

                        strongResponseMessage.Content = "欢迎关注【美食先点微信公众平台】！\r\n回复1点菜，回复2查看历史点单。\r\n官方地址：http://viewalloc.com";
                        responseMessage = strongResponseMessage;
                        break;
                    }
                case Event.unsubscribe://退订
                    {
                        //实际上用户无法收到非订阅账号的消息，所以这里可以随便写。
                        //unsubscribe事件的意义在于及时删除网站应用中已经记录的OpenID绑定，消除冗余数据。
                        var strongResponseMessage = requestMessage.CreateResponseMessage<ResponseMessageText>();
                        strongResponseMessage.Content = "You will back!";
                        responseMessage = strongResponseMessage;
                        break;
                    }
                case Event.CLICK://菜单点击事件，根据自己需要修改
                    throw new Exception("Demo中还没有加入CLICK的测试！");
                //break;
                default:
                    //throw new ArgumentOutOfRangeException();
                    break;
            }

            return responseMessage;
        }
    }
}
