using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 微信 本期大奖 
    /// </summary>
    public class WechatTopPriceInfo
    {
        public int ID { get; set; }
        //消息内容
        public string MsgContent { get; set; }
        //发布时间
        public string PubDateTime { get; set; }
        //操作者 
        public int OperaterID { get; set; }
        //状态 已过期,
        public string Status { get; set; }
    }
}
