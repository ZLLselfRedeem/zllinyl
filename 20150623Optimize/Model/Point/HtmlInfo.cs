using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 积分商城Html内容类
    /// jinyanni 2014-2-21
    /// </summary>
    public class HtmlInfo
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 城市ID
        /// </summary>
        public int cityId { get; set; }
        /// <summary>
        /// Html内容
        /// </summary>
        public string html { get; set; }
        /// <summary>
        /// 数据状态
        /// </summary>
        public int status { get; set; }
    }
}
