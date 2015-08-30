using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class OpenIdInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 对应的用户编号
        /// </summary>
        public long customerId { get; set; }
        /// <summary>
        /// openIdUid
        /// </summary>
        public string openIdUid { get; set; }
        /// <summary>
        /// openId绑定时间
        /// </summary>
        public DateTime openIdBindTime { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime expirationDate { get; set; }
        /// <summary>
        /// openId类型
        /// </summary>
        public VAOpenIdVendor openIdType { get; set; }
        /// <summary>
        /// openId更新时间
        /// </summary>
        public DateTime openIdUpdateTime { get; set; }
    }
}
