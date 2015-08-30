using System;
using System.Diagnostics;
using System.Web;
using Senparc.Weixin.MP.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.MessageHandlers;

namespace WechatService
{
    /// <summary>
    /// 自定义MessageHandler
    /// </summary>
    public partial class CustomMessageHandler : MessageHandler<MessageContext>
    {
        public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
        {
            var responseMessage = this.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "这条消息来自DefaultResponseMessage。";
            return responseMessage;
        }

        public override IResponseMessageBase OnEvent_EnterRequest(RequestMessageEvent_Enter requestMessage)
        {
            //var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            //responseMessage.Content = "您刚才发送了ENTER事件请求。";
            //return CreateImageResponse(requestMessage.FromUserName);
            return CreateDownloadAppImageResponse();
        }

        public override IResponseMessageBase OnEvent_LocationRequest(RequestMessageEvent_Location requestMessage)
        {
            //throw new Exception("暂不可用");
            //return CreateImageResponse(requestMessage.FromUserName);
            return CreateDownloadAppImageResponse();
        }

        /// <summary>
        /// 订阅（关注）事件
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_SubscribeRequest(RequestMessageEvent_Subscribe requestMessage)
        {
            //var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);

            //responseMessage.Content = "欢迎关注【美食点点微信公众平台】！\r\n回复1点菜，回复2查看历史点单。\r\n官方地址：http://viewalloc.com";
            //return responseMessage;
            return CreateDownloadAppImageResponse();
        }

        /// <summary>
        /// 退订
        /// 实际上用户无法收到非订阅账号的消息，所以这里可以随便写。
        /// unsubscribe事件的意义在于及时删除网站应用中已经记录的OpenID绑定，消除冗余数据。并且关注用户流失的情况。
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnEvent_UnsubscribeRequest(RequestMessageEvent_Unsubscribe requestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = "";
            return responseMessage;
        }

        public override IResponseMessageBase OnEvent_ClickRequest(RequestMessageEvent_Click requestMessage)
        {
            ResponseMessageBase responseMessage = null;
            switch (requestMessage.EventKey)
            {
                //推荐餐厅
                case "VA_WECHAT_BEIJING": //北京
                    responseMessage = GetRecommandShop(VAData.VARecommendShop.VA_WECHAT_BEIJING);
                    break;
                case "VA_WECHAT_SHANGHAI": //上海
                    responseMessage = GetRecommandShop(VAData.VARecommendShop.VA_WECHAT_SHANGHAI);
                    break;
                case "VA_WECHAT_GUANGZHOU": //广州
                    responseMessage = GetRecommandShop(VAData.VARecommendShop.VA_WECHAT_GUANGZHOU);
                    break;
                case "VA_WECHAT_HANGZHOU": //杭州
                    responseMessage = GetRecommandShop(VAData.VARecommendShop.VA_WECHAT_HANGZHOU);
                    break;
                case "VA_WECHAT_SHENZHEN": //深圳
                    responseMessage = GetRecommandShop(VAData.VARecommendShop.VA_WECHAT_SHENZHEN);
                    break;
                //悠先资讯
                case "VA_WECHAT_TOPPRICE": //本期大奖
                    responseMessage = GetTopPrice();
                    break;
                case "VA_WECHAT_FREECASE": //本期免单
                    responseMessage = GetFreeCase();
                    break;
                case "VA_WECHAT_HOTMENU": //本期热菜
                    responseMessage = GetHotMenu();
                    break;
                case "VA_WECHAT_LANDLADY": //亲聆老板娘
                    responseMessage = GetLandLadyVoice(requestMessage.FromUserName);
                    break;
                //悠先服务
                case "VA_WECHAT_DOWNLOAD": //悠先下载
                    responseMessage = CreateDownloadAppImageResponse();
                    break;
                case "VA_WECHAT_COOPERATION": //悠先合作
                    responseMessage = GetCooperation();
                    break;
                case "VA_WECHAT_Q_AND_A": //常见问答
                    responseMessage = GetQandA();
                    break;
                case "VA_WECHAT_PROPOSAL": //意见建议
                    responseMessage = GetProposal();
                    break;
                case "VA_WECHAT_COMPLAINT": //投诉处理
                    responseMessage = GetComplaint();
                    break;

                default:
                    {
                        responseMessage = CreateDownloadAppImageResponse(); //其它情况 都给出app下载信息
                    }
                    break;
            }
            return responseMessage;
        }
    }
}
