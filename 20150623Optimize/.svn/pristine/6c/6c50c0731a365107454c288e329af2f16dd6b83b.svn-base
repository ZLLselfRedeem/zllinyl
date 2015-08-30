using System;
using System.IO;
using System.Text;
using System.Configuration;
using Senparc.Weixin.MP.Context;
using Senparc.Weixin.MP.Entities;
using Senparc.Weixin.MP.MessageHandlers;
using Senparc.Weixin.MP.Helpers;
using System.Collections.Generic;
using System.Data;
using VAGastronomistMobileApp.Model;

namespace WechatService
{
    /// <summary>
    /// 自定义MessageHandler
    /// 把MessageHandler作为基类，重写对应请求的处理方法
    /// </summary>
    public partial class CustomMessageHandler : MessageHandler<MessageContext>
    {
        /// <summary>
        /// 微信返回给用户的图文消息和图片名称
        /// </summary>
        private static string _serverAddress;
        private static string serverAddress
        {
            get
            {
                if (string.IsNullOrEmpty(_serverAddress))
                {
                    _serverAddress = ConfigurationManager.AppSettings["Server"];
                }
                return _serverAddress;
            }
        }
        private static string _imagePath;
        private static string imagePath
        {
            get
            {
                if (string.IsNullOrEmpty(_imagePath))
                {
                    _imagePath = ConfigurationManager.AppSettings["ImagePath"];
                }
                return _imagePath;
            }
        }

        private static string _wechatTitlePic1;
        /// <summary>
        /// 悠先下载banner
        /// </summary>
        private static string DownloadTitlePic
        {
            get
            {
                if (string.IsNullOrEmpty(_wechatTitlePic1))
                {
                    _wechatTitlePic1 = WebConfig.CdnDomain + WebConfig.ImagePath + ConfigurationManager.AppSettings["wechatTitlePic1"];
                }
                return _wechatTitlePic1;
            }
        }
        private static string _wechatTitlePic2;
        /// <summary>
        /// 推荐餐厅banner
        /// </summary>
        private static string RecomShopTitlePic
        {
            get
            {
                if (string.IsNullOrEmpty(_wechatTitlePic2))
                {
                    _wechatTitlePic2 = WebConfig.CdnDomain + WebConfig.ImagePath + ConfigurationManager.AppSettings["wechatTitlePic2"];
                }
                return _wechatTitlePic2;
            }
        }
        private static string _wechatTitlePic3;
        /// <summary>
        /// 本期热菜banner
        /// </summary>
        private static string HotMenuTitlePic
        {
            get
            {
                if (string.IsNullOrEmpty(_wechatTitlePic3))
                {
                    _wechatTitlePic3 = WebConfig.CdnDomain + WebConfig.ImagePath + ConfigurationManager.AppSettings["wechatTitlePic3"];
                }
                return _wechatTitlePic3;
            }
        }
        private static string _wechatTitlePic4;
        private static string wechatTitlePic4
        {
            get
            {
                if (string.IsNullOrEmpty(_wechatTitlePic4))
                {
                    _wechatTitlePic4 = WebConfig.CdnDomain + WebConfig.ImagePath + ConfigurationManager.AppSettings["wechatTitlePic4"];
                }
                return _wechatTitlePic4;
            }
        }

        private static string _wechatTitleName1;
        private static string wechatTitleName1
        {
            get
            {
                if (string.IsNullOrEmpty(_wechatTitleName1))
                {
                    _wechatTitleName1 = ConfigurationManager.AppSettings["wechatTitleName1"];
                }
                return _wechatTitleName1;
            }
        }
        private static string _wechatTitleName2;
        private static string wechatTitleName2
        {
            get
            {
                if (string.IsNullOrEmpty(_wechatTitleName2))
                {
                    _wechatTitleName2 = ConfigurationManager.AppSettings["wechatTitleName2"];
                }
                return _wechatTitleName2;
            }
        }
        private static string _wechatTitleName3;
        private static string wechatTitleName3
        {
            get
            {
                if (string.IsNullOrEmpty(_wechatTitleName3))
                {
                    _wechatTitleName3 = ConfigurationManager.AppSettings["wechatTitleName3"];
                }
                return _wechatTitleName3;
            }
        }
        //private static string _wechatTitleName4;
        //private static string wechatTitleName4
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(_wechatTitleName4))
        //        {
        //            _wechatTitleName4 = ConfigurationManager.AppSettings["wechatTitleName4"];
        //        }
        //        return _wechatTitleName4;
        //    }
        //}

        private static string _wechatURL1;
        private static string wechatURL1
        {
            get
            {
                if (string.IsNullOrEmpty(_wechatURL1))
                {
                    _wechatURL1 = ConfigurationManager.AppSettings["wechatURL1"];
                }
                return _wechatURL1;
            }
        }
        private static string _wechatURL2;
        private static string wechatURL2
        {
            get
            {
                if (string.IsNullOrEmpty(_wechatURL2))
                {
                    _wechatURL2 = ConfigurationManager.AppSettings["wechatURL2"];
                }
                return _wechatURL2;
            }
        }
        private static string _wechatURL3;
        private static string wechatURL3
        {
            get
            {
                if (string.IsNullOrEmpty(_wechatURL3))
                {
                    _wechatURL3 = ConfigurationManager.AppSettings["wechatURL3"];
                }
                return _wechatURL3;
            }
        }
        private static string _wechatURL4;
        /// <summary>
        /// 推荐餐厅链接
        /// </summary>
        private static string wechatURL4
        {
            get
            {
                if (string.IsNullOrEmpty(_wechatURL4))
                {
                    _wechatURL4 = ConfigurationManager.AppSettings["wechatRecommendShop"];
                }
                return _wechatURL4;
            }
        }
        private static string _wechatURL5;
        /// <summary>
        /// 本期热菜链接
        /// </summary>
        private static string wechatURL5
        {
            get
            {
                if (string.IsNullOrEmpty(_wechatURL5))
                {
                    _wechatURL5 = ConfigurationManager.AppSettings["wechatHotMenu"];
                }
                return _wechatURL5;
            }
        }
        public CustomMessageHandler(Stream inputStream)
            : base(inputStream)
        {
            //这里设置仅用于测试，实际开发可以在外部更全局的地方设置，
            //比如MessageHandler<MessageContext>.GlobalWeixinContext.ExpireMinutes = 3。
            WeixinContext.ExpireMinutes = 3;
        }

        public override void OnExecuting()
        {
            //测试MessageContext.StorageData
            if (CurrentMessageContext.StorageData == null)
            {
                CurrentMessageContext.StorageData = 0;
            }
            base.OnExecuting();
        }

        public override void OnExecuted()
        {
            base.OnExecuted();
            CurrentMessageContext.StorageData = ((int)CurrentMessageContext.StorageData) + 1;
        }

        /// <summary>
        /// 处理文字请求
        /// </summary>
        /// <returns></returns>
        public override IResponseMessageBase OnTextRequest(RequestMessageText requestMessage)
        {
            return GetTextVoiceResponse(requestMessage, null);
        }

        /// <summary>
        /// 处理位置请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnLocationRequest(RequestMessageLocation requestMessage)
        {
            //var locationService = new LocationService();
            //var responseMessage = locationService.GetResponseMessage(requestMessage as RequestMessageLocation);
            //return responseMessage;
            //return CreateImageResponse(requestMessage.FromUserName);
            return CreateDownloadAppImageResponse();
        }

        /// <summary>
        /// 处理图片请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnImageRequest(RequestMessageImage requestMessage)
        {
            //var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(RequestMessage);
            //responseMessage.Content = "这里是正文内容，一共将发2条Article。";
            //responseMessage.Articles.Add(new Article()
            //{
            //    Title = "我要点菜",
            //    Description = "点击将进入点菜界面",
            //    PicUrl = "http://viewalloc.com/UploadFiles/Images/ads.png",
            //    Url = "http://viewalloc.com"
            //});
            //responseMessage.Articles.Add(new Article()
            //{
            //    Title = "历史点单",
            //    Description = "第二条带连接的内容",
            //    PicUrl = "http://viewalloc.com/UploadFiles/Images/dishNotFound.jpg",
            //    Url = "http://viewalloc.com"
            //});
            //return responseMessage;
            //return CreateImageResponse(requestMessage.FromUserName);
            return CreateDownloadAppImageResponse();
        }

        /// <summary>
        /// 处理语音请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnVoiceRequest(RequestMessageVoice requestMessage)
        {
            return GetTextVoiceResponse(null, requestMessage);
        }

        /// <summary>
        /// 处理链接消息请求
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnLinkRequest(RequestMessageLink requestMessage)
        {
            //var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageText>(requestMessage);
            //responseMessage.Content = string.Format(@"您发送了一条连接信息：Title：{0} Description:{1} Url:{2}",
            //    requestMessage.Title, requestMessage.Description, requestMessage.Url);
            //return responseMessage;
            //return CreateImageResponse(requestMessage.FromUserName);
            return CreateDownloadAppImageResponse();
        }

        /// <summary>
        /// 处理事件请求（这个方法一般不用重写，这里仅作为示例出现。除非需要在判断具体Event类型以外对Event信息进行统一操作
        /// </summary>
        /// <param name="requestMessage"></param>
        /// <returns></returns>
        public override IResponseMessageBase OnEventRequest(RequestMessageEventBase requestMessage)
        {
            var eventResponseMessage = base.OnEventRequest(requestMessage);//对于Event下属分类的重写方法，见：CustomerMessageHandler_Events.cs
            //TODO: 对Event信息进行统一操作
            return eventResponseMessage;
        }

        private ResponseMessageBase CreateImageResponse(string wechatOpenId)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(RequestMessage);
            responseMessage.Articles.Add(new Article()
            {
                Title = wechatTitleName1,
                Description = "点击将进入点菜界面",
                PicUrl = DownloadTitlePic,
                Url = wechatURL1
            });
            responseMessage.Articles.Add(new Article()
            {
                Title = wechatTitleName2,
                Description = "第二条带连接的内容",
                PicUrl = RecomShopTitlePic,
                Url = wechatURL2 + wechatOpenId
            });
            responseMessage.Articles.Add(new Article()
            {
                Title = wechatTitleName3,
                Description = "第三条带连接的内容",
                PicUrl = HotMenuTitlePic,
                Url = wechatURL3 + wechatOpenId
            });
            return responseMessage;
        }

        private ResponseMessageBase CreateDownloadAppImageResponse()
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(RequestMessage);
            responseMessage.Articles.Add(new Article()
            {
                Title = wechatTitleName1,
                Description = "点击将进入下载界面",
                PicUrl = DownloadTitlePic,
                Url = wechatURL1
            });
            return responseMessage;
        }

        private ResponseMessageBase CreateWechatDishImageResponse(string wechatOpenId)
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(RequestMessage);
            responseMessage.Articles.Add(new Article()
            {
                Title = wechatTitleName2,
                Description = "点击将进入点菜界面",
                PicUrl = wechatTitlePic4,
                Url = wechatURL2 + wechatOpenId
            });
            return responseMessage;
        }

        private ResponseMessageBase CreateTextResponse(string responseStr)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = responseStr;
            return responseMessage;
        }

        #region 按钮操作
        //推荐餐厅
        private ResponseMessageBase GetRecommandShop(VAData.VARecommendShop VARecommendShop)
        {
            List<string> shopList = new List<string>();
            switch (VARecommendShop)
            {
                case VAData.VARecommendShop.VA_WECHAT_BEIJING:
                    shopList = VAGetRecommendShop.GetRecommendShop("北京市");
                    return GetRecommendShopResponse(shopList);
                case VAData.VARecommendShop.VA_WECHAT_SHANGHAI:
                    shopList = VAGetRecommendShop.GetRecommendShop("上海市");
                    return GetRecommendShopResponse(shopList);
                case VAData.VARecommendShop.VA_WECHAT_GUANGZHOU:
                    shopList = VAGetRecommendShop.GetRecommendShop("广州市");
                    return GetRecommendShopResponse(shopList);
                case VAData.VARecommendShop.VA_WECHAT_HANGZHOU:
                    shopList = VAGetRecommendShop.GetRecommendShop("杭州市");
                    return GetRecommendShopResponse(shopList);
                case VAData.VARecommendShop.VA_WECHAT_SHENZHEN:
                    shopList = VAGetRecommendShop.GetRecommendShop("深圳市");
                    return GetRecommendShopResponse(shopList);
                default:
                    return CreateTextResponse("欢迎使用悠先微信公众平台，请提出宝贵的意见!");
            }

        }

        private ResponseMessageBase GetRecommendShopResponse(List<string> shopList)
        {
            //cityName,cityID,shopID,shopName
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(RequestMessage);
            for (int i = 0; i < shopList.Count; i++)
            {
                string[] split = shopList[i].Split(';');
                //string bas64 = "";
                //foreach (string s in split)
                //{
                //    bas64 += s + ",";
                //}
                //bas64 = bas64.TrimEnd(',');
                //string bas = Convert.ToBase64String(Encoding.Default.GetBytes(bas64));
                if (i == 0) //如果有主推荐餐厅，显示该餐厅的banner
                {
                    string picURL = split[5].ToString() == "0" ? RecomShopTitlePic : (WebConfig.CdnDomain + WebConfig.ImagePath + "imageNotFoundSmall.jpg");
                    responseMessage.Articles.Add(new Article()
                    {
                        Title = split[3],
                        Description = split[0],
                        PicUrl = picURL,
                        Url = wechatURL4 + "?cityID=" + split[1] + "&shopID=" + split[2],
                    });
                }
                else
                {
                    responseMessage.Articles.Add(new Article()
                    {
                        Title = split[3],
                        Description = split[0],
                        PicUrl = split[4],
                        Url = wechatURL4 + "?cityID=" + split[1] + "&shopID=" + split[2],
                    });
                }
            }
            if (shopList.Count == 0)
                return CreateTextResponse("该地区暂无推荐餐厅信息...");
            else
                return responseMessage;
        }

        //本期大奖
        private ResponseMessageBase GetTopPrice()
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = VAGetUxianInformation.GetTopPrice();
            return responseMessage;
        }

        //本期免单
        private ResponseMessageBase GetFreeCase()
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = VAGetUxianInformation.GetFreeCase();
            return responseMessage;
        }

        //本期热菜
        private ResponseMessageBase GetHotMenu()
        {
            var responseMessage = ResponseMessageBase.CreateFromRequestMessage<ResponseMessageNews>(RequestMessage);
            //返回一个界面信息，有一个热菜信息显示在上面，再给一个连接，跳转到具体的热菜信息界面 
            //cityName,shopName,shopAddress,DishName,DishPrice,ImageName,DishID,saleAmount
            DataTable dt = VAGetUxianInformation.GetHotMenu();
            if (dt == null || dt.Rows.Count == 0)
            {
                return CreateTextResponse("暂无热菜信息...");
            }
            string bjDish = "", shDish = "", gzDish = "", hzDish = "", szDish = "";
            string bjpars = "", shpars = "", gzpars = "", hzpars = "", szpars = "";
            string bjImg = "", shImg = "", gzImg = "", hzImg = "", szImg = "";
            string imgServer = "", imgFolder = "";
            try
            {
                imgServer = ConfigurationManager.AppSettings["Server"].ToString();
                imgFolder = ConfigurationManager.AppSettings["ImagePath"].ToString();
            }
            catch { }
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string imgUrl = imgServer + "/" + imgFolder + dr["ImageFolder"].ToString() + dr["ImageName"].ToString();
                    switch (dr["cityName"].ToString())
                    {
                        case "北京市":
                            bjDish = dr["DishName"].ToString();
                            bjpars = dr["DishID"].ToString();
                            bjImg = imgUrl;
                            break;
                        case "上海市":
                            shDish = dr["DishName"].ToString();
                            shpars = dr["DishID"].ToString();
                            shImg = imgUrl;
                            break;
                        case "广州市":
                            gzDish = dr["DishName"].ToString();
                            gzpars = dr["DishID"].ToString();
                            gzImg = imgUrl;
                            break;
                        case "杭州市":
                            hzDish = dr["DishName"].ToString();
                            hzpars = dr["DishID"].ToString();
                            hzImg = imgUrl;
                            break;
                        case "深圳市":
                            szDish = dr["DishName"].ToString();
                            szpars = dr["DishID"].ToString();
                            szImg = imgUrl;
                            break;
                    }
                }
            }
            responseMessage.Articles.Add(new Article()
            {
                Title = "悠先点菜，先点先吃!",
                Description = "点击查看热菜详情",
                PicUrl = HotMenuTitlePic,
                Url = wechatURL1
            });
            if (bjDish != "")
            {
                responseMessage.Articles.Add(new Article()
                {
                    Title = "北京热菜－" + bjDish,
                    Description = "点击查看热菜详情",
                    PicUrl = bjImg,
                    Url = wechatURL5 + "?DishID=" + bjpars
                });
            }
            if (shDish != "")
            {
                responseMessage.Articles.Add(new Article()
                {
                    Title = "上海热菜－" + shDish,
                    Description = "点击查看热菜详情",
                    PicUrl = shImg,
                    Url = wechatURL5 + "?DishID=" + shpars
                });
            }
            if (gzDish != "")
            {
                responseMessage.Articles.Add(new Article()
                {
                    Title = "广州热菜－" + gzDish,
                    Description = "点击查看热菜详情",
                    PicUrl = gzImg,
                    Url = wechatURL5 + "?DishID=" + gzpars
                });
            }
            if (hzDish != "")
            {
                responseMessage.Articles.Add(new Article()
                {
                    Title = "杭州热菜－" + hzDish,
                    Description = "点击查看热菜详情",
                    PicUrl = hzImg,
                    Url = wechatURL5 + "?DishID=" + hzpars
                });
            }
            if (szDish != "")
            {
                responseMessage.Articles.Add(new Article()
                {
                    Title = "深圳热菜－" + szDish,
                    Description = "点击查看热菜详情",
                    PicUrl = szImg,
                    Url = wechatURL5 + "?DishID=" + szpars
                });
            }
            return responseMessage;
        }

        //private string GetParsBase64Str(DataRow dr)
        //{
        //    string bas64 = "";
        //    for (int i = 0; i < dr.ItemArray.Length; i++)
        //    {
        //        bas64 += dr[i].ToString() + ",";
        //    }
        //    bas64 = bas64.TrimEnd(',');
        //    string bas = Convert.ToBase64String(Encoding.Default.GetBytes(bas64));
        //    return bas;
        //}

        //亲聆老板娘
        private ResponseMessageBase GetLandLadyVoice(string fromUserName)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            //获取media_id  ... 咋回复呐？
            //responseMessage.ToUserName = fromUserName;
            //responseMessage.FromUserName = "meishixiandian";//开发者 微信号
            //responseMessage.CreateTime = DateTime.Now;
            //responseMessage.MsgType = Senparc.Weixin.MP.ResponseMsgType.Music;
            responseMessage.Content = "敬请期待...";
            return responseMessage;
        }

        //悠先合作
        private ResponseMessageBase GetCooperation()
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = VAGetUxianService.GetUxianCooperation();
            return responseMessage;
        }

        //常见问答
        private ResponseMessageBase GetQandA()
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = VAGetUxianService.GetUxianQandA();
            return responseMessage;
        }

        //意见建议
        private ResponseMessageBase GetProposal()
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = VAGetUxianService.GetUxianProposal();
            return responseMessage;
        }

        //投诉处理
        private ResponseMessageBase GetComplaint()
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            responseMessage.Content = VAGetUxianService.GetUxianComplaint();
            return responseMessage;
        }
        #endregion

        #region 处理文字，语音信息
        private IResponseMessageBase GetTextVoiceResponse(RequestMessageText textRequestMessage, RequestMessageVoice voiceRequestMessage)
        {
            var responseMessage = base.CreateResponseMessage<ResponseMessageText>();
            string keyWord = textRequestMessage.Content;
            if (keyWord.StartsWith("合作"))
            {
                if (keyWord.Trim() != "合作")
                {
                    //保存用户发送的合作信息
                    if (textRequestMessage != null)
                        VAGetUxianService.InsertUxianCooperation(textRequestMessage);

                    responseMessage.Content = "感谢您发送的合作信息！";
                }
            }
            else if (keyWord.StartsWith("意见"))
            {
                if (keyWord.Trim() != "意见")
                {
                    //保存用户发送的意见信息
                    if (textRequestMessage != null)
                        VAGetUxianService.InsertUxianProposal(textRequestMessage);
                    else if (voiceRequestMessage != null)
                        VAGetUxianService.InsertUxianProposal(voiceRequestMessage);

                    responseMessage.Content = "感谢您的宝贵意见！我们将尽快处理。";
                }

            }
            else if (keyWord.StartsWith("投诉"))
            {
                if (keyWord.Trim() != "投诉")
                {
                    //保存投诉信息
                    if (textRequestMessage != null)
                        VAGetUxianService.InsertUxianComplaint(textRequestMessage);
                    else if (voiceRequestMessage != null)
                        VAGetUxianService.InsertUxianComplaint(voiceRequestMessage);

                    responseMessage.Content = "感谢您使用悠先点菜客户端！给您带来的不便，请谅解，我们将尽快给您反馈结果。";
                }
            }
            else
            {
                //int iert = 0;
                //先记录下该消息，功能以后规划
                if (textRequestMessage != null)
                    VARecordWechatMsg.InsertReceivedMsg(textRequestMessage, null);
                else if (voiceRequestMessage != null)
                    VARecordWechatMsg.InsertReceivedMsg(null, voiceRequestMessage);

                responseMessage.Content = "感谢您使用悠先客户端！";// +iert;
            }
            return responseMessage;
        }
        #endregion
    }
}
