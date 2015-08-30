using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 获取配置文件key value
    /// </summary>
    public static class WebConfig
    {
        private static string uploadFiles;
        /// <summary>
        ///等价值： UploadFiles/
        /// </summary>
        public static string UploadFiles
        {
            get
            {
                if (string.IsNullOrEmpty(uploadFiles))
                {
                    uploadFiles = ConfigurationManager.AppSettings["UploadFiles"].ToString();
                }
                return uploadFiles;
            }
        }

        private static string vipImg;
        /// <summary>
        ///等价值： VipImg/
        /// </summary>
        public static string VipImg
        {
            get
            {
                if (string.IsNullOrEmpty(vipImg))
                {
                    vipImg = ConfigurationManager.AppSettings["VipImg"].ToString();
                }
                return vipImg;
            }
        }

        private static string webImages;
        /// <summary>
        ///等价值： web/images/
        /// </summary>
        public static string WebImages
        {
            get
            {
                if (string.IsNullOrEmpty(webImages))
                {
                    webImages = ConfigurationManager.AppSettings["webImages"].ToString();
                }
                return webImages;
            }
        }

        private static string ossDomain;
        /// <summary>
        ///等价值： http://image.u-xian.com/
        /// </summary>
        public static string OssDomain
        {
            get
            {
                if (string.IsNullOrEmpty(ossDomain))
                {
                    ossDomain = ConfigurationManager.AppSettings["ossDomain"].ToString();
                }
                return ossDomain;
            }
        }

        private static string cdnDomain;
        /// <summary>
        /// 等价于 http://image220.u-xian.com/
        /// </summary>
        public static string CdnDomain
        {
            get
            {
                if (string.IsNullOrEmpty(cdnDomain))
                {
                    cdnDomain = ConfigurationManager.AppSettings["cdnDomain"].ToString();
                }
                return cdnDomain;
            }
        }

        private static string imagePath;
        /// <summary>
        ///等价值： UploadFiles/Images/
        /// </summary>
        public static string ImagePath
        {
            get
            {
                if (string.IsNullOrEmpty(imagePath))
                {
                    imagePath = ConfigurationManager.AppSettings["ImagePath"].ToString();
                }
                return imagePath;
            }
        }

        private static string goods;
        /// <summary>
        ///等价值： UploadFiles/Images/Goods/
        /// </summary>
        public static string Goods
        {
            get
            {
                if (string.IsNullOrEmpty(goods))
                {
                    goods = ConfigurationManager.AppSettings["Goods"].ToString();
                }
                return goods;
            }
        }

        private static string qrCodeImage;
        /// <summary>
        /// 等价值：QRCodeImage/
        /// </summary>
        public static string QRCodeImage
        {
            get
            {
                if (string.IsNullOrEmpty(qrCodeImage))
                {
                    qrCodeImage = ConfigurationManager.AppSettings["QRCodeImage"].ToString();
                }
                return qrCodeImage;
            }
        }

        private static string temp;
        /// <summary>
        ///等价值： UploadFiles/temp/
        /// </summary>
        public static string Temp
        {
            get
            {
                if (string.IsNullOrEmpty(temp))
                {
                    temp = "~/" + ConfigurationManager.AppSettings["temp"].ToString();
                }
                return temp;
            }
        }

        private static string extension = ConfigurationManager.AppSettings["extension"].ToString();
        /// <summary>
        ///等价值： .jpg|.gif|.png
        /// </summary>
        public static string Extension
        {
            get { return extension; }
            set { extension = value; }
        }

        private static string advertisement;
        /// <summary>
        ///等价值： Advertisement/
        /// </summary>
        public static string Advertisement
        {
            get
            {
                if (string.IsNullOrEmpty(advertisement))
                {
                    advertisement = ConfigurationManager.AppSettings["Advertisement"].ToString();
                }
                return advertisement;
            }
        }

        private static string complaintURL;
        /// <summary>
        ///等价值：http://u-xian.com/apppages/manual/complaint.aspx?p={0}
        /// </summary>
        public static string ComplaintURL
        {
            get
            {
                if (string.IsNullOrEmpty(complaintURL))
                {
                    complaintURL = ConfigurationManager.AppSettings["complaintURL"].ToString();
                }
                return complaintURL;
            }
        }

        private static string myWechat;
        /// <summary>
        ///等价值：悠先点菜
        /// </summary>
        public static string MyWechat
        {
            get
            {
                if (string.IsNullOrEmpty(myWechat))
                {
                    myWechat = ConfigurationManager.AppSettings["myWechat"].ToString();
                }
                return myWechat;
            }
        }

        private static string preorder_wx;
        /// <summary>
        ///等价值：我在{0} 分享了{1} 的点单{2}
        /// </summary>
        public static string Preorder_wx
        {
            get
            {
                if (string.IsNullOrEmpty(preorder_wx))
                {
                    preorder_wx = ConfigurationManager.AppSettings["preorder_wx"].ToString();
                }
                return preorder_wx;
            }
        }

        private static string serverDomain;
        /// <summary>
        ///等价值：http://u-xian.com/
        /// </summary>
        public static string ServerDomain
        {
            get
            {
                if (string.IsNullOrEmpty(serverDomain))
                {
                    serverDomain = ConfigurationManager.AppSettings["Server"].ToString();
                }
                return serverDomain;
            }
        }

        private static string serviceUrl;
        /// <summary>
        ///等价值：http://u-xian.com/apppages/manual/service.html
        /// </summary>
        public static string ServiceUrl
        {
            get
            {
                if (string.IsNullOrEmpty(serviceUrl))
                {
                    serviceUrl = ConfigurationManager.AppSettings["serviceURL"].ToString();
                }
                return serviceUrl;
            }
        }

        private static string guideUrl;
        /// <summary>
        ///等价值：http://u-xian.com/apppages/manual/guide.html
        /// </summary>
        public static string GuideUrl
        {
            get
            {
                if (string.IsNullOrEmpty(guideUrl))
                {
                    guideUrl = ConfigurationManager.AppSettings["guideUrl"].ToString();
                }
                return guideUrl;
            }
        }

        private static string introductionUrl;
        /// <summary>
        ///等价值：http://u-xian.com/apppages/manual/introduction.html
        /// </summary>
        public static string IntroductionUrl
        {
            get
            {
                if (string.IsNullOrEmpty(introductionUrl))
                {
                    introductionUrl = ConfigurationManager.AppSettings["introductionUrl"].ToString();
                }
                return introductionUrl;
            }
        }

        private static string faqUrl;
        /// <summary>
        ///等价值：http://u-xian.com/apppages/manual/faq.html
        /// </summary>
        public static string FaqUrl
        {
            get
            {
                if (string.IsNullOrEmpty(faqUrl))
                {
                    faqUrl = ConfigurationManager.AppSettings["faqUrl"].ToString();
                }
                return faqUrl;
            }
        }

        private static string feedbackUrl;
        /// <summary>
        ///等价值：http://u-xian.com/apppages/manual/feedback.aspx?c={0}
        /// </summary>
        public static string FeedbackUrl
        {
            get
            {
                if (string.IsNullOrEmpty(feedbackUrl))
                {
                    feedbackUrl = ConfigurationManager.AppSettings["feedbackUrl"].ToString();
                }
                return feedbackUrl;
            }
        }

        private static string shopImg;
        /// <summary>
        ///等价值： ShopImage/
        /// </summary>
        public static string ShopImg
        {
            get
            {
                if (string.IsNullOrEmpty(shopImg))
                {
                    shopImg = ConfigurationManager.AppSettings["shopImg"].ToString();
                }
                return shopImg;
            }
        }

        private static string shopShare;
        /// <summary>
        ///等价值：我刚在{0}用悠先点了菜,味道真不错,赶紧来试试吧
        /// </summary>
        public static string ShopShare
        {
            get
            {
                if (string.IsNullOrEmpty(shopShare))
                {
                    // shopShare = ConfigurationManager.AppSettings["shopShare"].ToString();
                }
                return "我刚在{0}用悠先点了菜,味道真不错,赶紧来试试吧";
            }
        }

        private static string wechatPrefix;
        /// <summary>
        ///等价值：va,正式服务器和测试服务器必须区分开
        /// </summary>
        public static string WechatPrefix
        {
            get
            {
                if (string.IsNullOrEmpty(wechatPrefix))
                {
                    wechatPrefix = ConfigurationManager.AppSettings["wechatPrefix"].ToString();
                }
                return wechatPrefix;
            }
        }

        private static string shoppingMallURL;
        /// <summary>
        ///等价值：http://u-xian.com/shoppingmall.aspx?c={0}&amp;t={1}
        /// </summary>
        public static string ShoppingMallURL
        {
            get
            {
                if (string.IsNullOrEmpty(shoppingMallURL))
                {
                    shoppingMallURL = ConfigurationManager.AppSettings["shoppingMallURL"].ToString();
                }
                return shoppingMallURL;
            }
        }

        private static string companyMedalImage;
        /// <summary>
        ///公司勋章，等价值：CompanyMedalImage/
        /// </summary>
        public static string CompanyMedalImage
        {
            get
            {
                if (string.IsNullOrEmpty(companyMedalImage))
                {
                    companyMedalImage = ConfigurationManager.AppSettings["CompanyMedalImage"].ToString();
                }
                return companyMedalImage;
            }
        }

        private static string shopMedalImage;
        /// <summary>
        ///门店勋章，等价值：ShopMedalImage/
        /// </summary>
        public static string ShopMedalImage
        {
            get
            {
                if (string.IsNullOrEmpty(shopMedalImage))
                {
                    shopMedalImage = ConfigurationManager.AppSettings["ShopMedalImage"].ToString();
                }
                return shopMedalImage;
            }
        }

        private static string smsContent;
        /// <summary>
        ///悠先验证码内容，等价值：9527【悠先验证码】
        /// </summary>
        public static string SmsContent
        {
            get
            {
                if (string.IsNullOrEmpty(smsContent))
                {
                    smsContent = ConfigurationManager.AppSettings["SmsContent"].ToString();
                }
                return smsContent;
            }
        }

        private static string smsType;
        /// <summary>
        ///短信验证码发送平台，等价值:Guodu/Yimei
        /// </summary>
        public static string SmsType
        {
            get
            {
                if (string.IsNullOrEmpty(smsType))
                {
                    smsType = ConfigurationManager.AppSettings["SmsType"].ToString();
                }
                return smsType;
            }
        }

        private static string smsUid;
        /// <summary>
        ///国都短信用户名
        /// </summary>
        public static string SmsUid
        {
            get
            {
                if (string.IsNullOrEmpty(smsUid))
                {
                    smsUid = ConfigurationManager.AppSettings["SmsUid"].ToString();
                }
                return smsUid;
            }
        }

        private static string smsPwd;
        /// <summary>
        ///国都短信密码
        /// </summary>
        public static string SmsPwd
        {
            get
            {
                if (string.IsNullOrEmpty(smsPwd))
                {
                    smsPwd = ConfigurationManager.AppSettings["SmsPwd"].ToString();
                }
                return smsPwd;
            }
        }

        public static string FoodDiaryImagePath = "FoodDiary/";

        private static string dishImageWidth;
        /// <summary>
        /// 菜图最小宽度
        /// </summary>
        public static string DishImageWidth
        {
            get
            {
                if (string.IsNullOrEmpty(dishImageWidth))
                {
                    dishImageWidth = ConfigurationManager.AppSettings["dishImageWidth"].ToString();
                }
                return dishImageWidth;
            }
        }
        private static string dishImageHeight;
        /// <summary>
        /// 菜图最小高度
        /// </summary>
        public static string DishImageHeight
        {
            get
            {
                if (string.IsNullOrEmpty(dishImageHeight))
                {
                    dishImageHeight = ConfigurationManager.AppSettings["dishImageHeight"].ToString();
                }
                return dishImageHeight;
            }
        }
        private static int dishImageSpace;
        /// <summary>
        /// 菜图大小
        /// </summary>
        public static int DishImageSpace
        {
            get
            {
                if (dishImageSpace <= 0)
                {
                    dishImageSpace = Convert.ToInt32(ConfigurationManager.AppSettings["dishImageSpace"].ToString());
                }
                return dishImageSpace;
            }
        }
        private static int bannerSpace;
        /// <summary>
        /// Banner图大小
        /// </summary>
        public static int BannerSpace
        {
            get
            {
                if (bannerSpace <= 0)
                {
                    bannerSpace = Convert.ToInt32(ConfigurationManager.AppSettings["bannerSpace"].ToString());
                }
                return bannerSpace;
            }
        }
        private static int shopFaceSpace;
        /// <summary>
        /// 门脸图大小
        /// </summary>
        public static int ShopFaceSpace
        {
            get
            {
                if (shopFaceSpace <= 0)
                {
                    shopFaceSpace = Convert.ToInt32(ConfigurationManager.AppSettings["shopFaceSpace"].ToString());
                }
                return shopFaceSpace;
            }
        }
        private static int shopLogoSpace;
        /// <summary>
        /// 门店Logo图大小
        /// </summary>
        public static int ShopLogoSpace
        {
            get
            {
                if (shopLogoSpace <= 0)
                {
                    shopLogoSpace = Convert.ToInt32(ConfigurationManager.AppSettings["shopLogoSpace"].ToString());
                }
                return shopLogoSpace;
            }
        }
        private static int shopEnvironmentSpace;
        /// <summary>
        /// 门店环境大小
        /// </summary>
        public static int ShopEnvironmentSpace
        {
            get
            {
                if (shopEnvironmentSpace <= 0)
                {
                    shopEnvironmentSpace = Convert.ToInt32(ConfigurationManager.AppSettings["shopEnvironmentSpace"].ToString());
                }
                return shopEnvironmentSpace;
            }
        }

        private static string customerPicturePath;

        /// <summary>
        /// 客户头像地址
        /// </summary>
        public static string CustomerPicturePath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(customerPicturePath))
                {
                    if (ConfigurationManager.AppSettings.AllKeys.Contains("CustomerPicturePath"))
                    {
                        customerPicturePath = ConfigurationManager.AppSettings["CustomerPicturePath"];
                    }
                    else
                    {
                        return "customer/";
                    }
                }
                return customerPicturePath;
            }
        }

        private static string redEnvelopeRange;
        /// <summary>
        /// 节日免单红包金额区间
        /// </summary>
        public static string RedEnvelopeRange
        {
            get
            {
                if (String.IsNullOrWhiteSpace(redEnvelopeRange))
                {
                    redEnvelopeRange = ConfigurationManager.AppSettings["RedEnvelopeRange"].ToString();
                }
                return redEnvelopeRange;
            }
        }
        private static string redEnvelopeScale;
        /// <summary>
        /// 节日免单红包概率配置
        /// </summary>
        public static string RedEnvelopeScale
        {
            get
            {
                if (String.IsNullOrWhiteSpace(redEnvelopeScale))
                {
                    redEnvelopeScale = ConfigurationManager.AppSettings["RedEnvelopeScale"].ToString();
                }
                return redEnvelopeScale;
            }
        }
        /// <summary>
        /// 年夜饭名称描述
        /// </summary>
        private static string mealOrderNameDesc;
        public static string MealOrderNameDesc
        {
            get
            {
                if (String.IsNullOrWhiteSpace(mealOrderNameDesc))
                {
                    mealOrderNameDesc = ConfigurationManager.AppSettings["MealOrderNameDesc"].ToString();
                }
                return mealOrderNameDesc;
            }
        }
        /// <summary>
        /// 年夜饭定制成功短信提醒模版
        /// </summary>
        private static string mealOrderSmsReminderContent;
        public static string MealOrderSmsReminderContent
        {
            get
            {
                if (String.IsNullOrWhiteSpace(mealOrderSmsReminderContent))
                {
                    mealOrderSmsReminderContent = ConfigurationManager.AppSettings["MealOrderSmsReminderContent"].ToString();
                }
                return mealOrderSmsReminderContent;
            }
        }

        private static string uxianAppServerDomain;
        /// <summary>
        /// 悠先点菜新架构域名
        /// </summary>
        public static string UxianAppServerDomain
        {
            get
            {
                if (string.IsNullOrEmpty(uxianAppServerDomain))
                {
                    uxianAppServerDomain = ConfigurationManager.AppSettings["UxianAppServer"].ToString();
                }
                return uxianAppServerDomain;
            }
        }

        private static string integralServerDomain;
        /// <summary>
        /// 悠先点菜新架构域名
        /// </summary>
         public static string IntegralServerDomain
        {
            get
            {
                if (string.IsNullOrEmpty(integralServerDomain))
                {
                    integralServerDomain = ConfigurationManager.AppSettings["IntegralServerDomain"].ToString();
                }
                return integralServerDomain;
            }
        }


        
    }
}
