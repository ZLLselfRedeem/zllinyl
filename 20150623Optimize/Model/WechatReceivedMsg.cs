using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class WechatReceivedMsg
    {
        public int ID { get; set; }

        public string msgContent { get; set; }

        public int contentType { get; set; }

        public string senderWechatID { get; set; }

        public string media_id { get; set; }

        public string receiveDateTime { get; set; }

        public int status { get; set; }
    }
}
