using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WechatService
{
    public class VAData
    {
        public enum VARecommendShop
        {
            VA_WECHAT_BEIJING, 
            VA_WECHAT_SHANGHAI, 
            VA_WECHAT_GUANGZHOU, 
            VA_WECHAT_HANGZHOU, 
            VA_WECHAT_SHENZHEN
        }

        public enum VAUXianInformation
        {
            VA_WECHAT_TOPPRICE, 
            VA_WECHAT_FREECASE, 
            VA_WECHAT_HOTMENU, 
            VA_WECHAT_LANDLADY
        }

        public enum VAUXianService
        {
            VA_WECHAT_DOWNLOAD,
            VA_WECHAT_COOPERATION,
            VA_WECHAT_Q_AND_A,
            VA_WECHAT_PROPOSAL,
            VA_WECHAT_COMPLAINT
        }
    }
}
