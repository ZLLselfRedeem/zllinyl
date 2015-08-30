using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 微信平台 接收微信客户端发送过来的 投诉信息
    /// </summary>
    public class WechatComplaintReceiveInfo
    {
        public int ID { get; set; }

        public string msgContent { get; set; }
        /// <summary>
        /// 内容类型 0纯文本,1语音
        /// </summary>
        public int contentType { get; set; }
        /// <summary>
        /// 发送者微信号
        /// </summary>
        public string senderWechatID { get; set; }
        /// <summary>
        /// 状态 初始,已处理
        /// </summary>
        public string status { get; set; }
        /// <summary>
        /// 接收时间
        /// </summary>
        public string receiveDateTime { get; set; }

        public string voicefileName { get; set; }

        public int operaterID { get; set; }

        public string operateDateTime { get; set; }
    }
}
