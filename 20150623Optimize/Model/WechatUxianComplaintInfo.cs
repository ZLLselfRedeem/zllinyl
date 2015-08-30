using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 微信平台 投诉处理
    /// </summary>
    public class WechatUxianComplaintInfo
    {
        public int ID { get; set; }

        public string msgContent { get; set; }

        public string pubDateTime { get; set; }

        public int operaterID { get; set; }
    }
}
