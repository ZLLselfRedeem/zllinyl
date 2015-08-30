using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 公众号用于调用微信JS接口的临时票据
    /// 2015-1-22
    /// </summary>
    public class WechatJsApiTicket
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
        public string ticket { get; set; }
        public string expires_in { get; set; }

    }


    /// <summary>
    /// 使用JS-SDK的页面权限验证配置
    /// </summary>
    public class WechatJsApiConfig
    {
        /// <summary>
        /// 公众号的唯一标识
        /// </summary>
        public string appId { get; set; }
        /// <summary>
        /// 生成签名的时间戳
        /// </summary>
        public string timestamp { get; set; }
        /// <summary>
        /// 生成签名的随机串
        /// </summary>
        public string nonceStr { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string signature { get; set; }
    }
}
