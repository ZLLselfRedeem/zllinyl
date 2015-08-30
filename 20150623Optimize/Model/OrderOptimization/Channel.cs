using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 增值页
    /// </summary>
    public class Channel
    {
        public int id { get; set; }

        public string channelName { get; set; }

        public int cityID { get; set; }

        public double price { get; set; }

        public int firstTitleID { get; set; }

        public bool status { get; set; }

        public int sign { get; set; }

        public string content { get; set; }

        public DateTime createTime { get; set; }

        public bool isDelete { get; set; }

        public int modelType { get; set; }
    }
}
