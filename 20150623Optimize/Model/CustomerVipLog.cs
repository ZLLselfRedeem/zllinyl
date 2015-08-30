using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    public class CustomerVipLog
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public long customerId { get; set; }
        /// <summary>
        /// 对应公司编号
        /// </summary>
        public int companyId { get; set; }
        /// <summary>
        /// 对应的公司Vip政策编号
        /// </summary>
        public long companyVipId { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime time { get; set; }
        /// <summary>
        /// 是否是升级
        /// </summary>
        public bool isLevelUp { get; set; }
    }
}
