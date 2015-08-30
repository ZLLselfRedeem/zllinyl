using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 19点预点单微博分享信息
    /// </summary>
    public class PreOrderWeiboShare
    {
        /// <summary>
        /// 分享编号
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 预点单编号
        /// </summary>
        public long preorder19dianId { get; set; }
        /// <summary>
        /// 微博类型
        /// </summary>
        public VAOpenIdVendor openIdVendor { get; set; }
        /// <summary>
        /// 分享时间
        /// </summary>
        public DateTime shareTime { get; set; }
    }
}
