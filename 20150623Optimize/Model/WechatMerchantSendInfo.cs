using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class WechatMerchantSendInfo
    {
        public int ID { get; set; }
        /// <summary>
        /// 有合作意向的商户 给公司微信公众平台发送的 信息
        /// </summary>
        public string msgContent { get; set; }

        public string senderWechatID { get; set; }
        /// <summary>
        /// 公众平台接收的时间
        /// </summary>
        public string receiveDateTime { get; set; }
        /// <summary>
        /// 合作信息处理状态  初始, 已处理
        /// </summary>
        public string status { get; set; }

        public int operaterID { get; set; }

        public string operateDateTime { get; set; }
    }
}
