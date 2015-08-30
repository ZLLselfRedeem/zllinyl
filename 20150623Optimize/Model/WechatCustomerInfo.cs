using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 微信客户端 用户信息
    /// </summary>
    public class WechatCustomerInfo
    {
        public int ID { get; set; }
        //subscribe值为0时，代表此用户没有关注该公众号，拉取不到其余信息
        public string subscribe { get; set; }

        public string openid { get; set; }

        public string nickname { get; set; }

        public string sex { get; set; }

        public string language { get; set; }

        public string city { get; set; }

        public string province { get; set; }

        public string country { get; set; }
    }
}
