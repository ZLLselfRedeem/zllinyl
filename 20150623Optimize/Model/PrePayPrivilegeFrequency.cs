using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    //created  by wancgheng 
    //2013-11-14
    /// <summary>
    /// 记录返券次数
    /// </summary>
    public class PrePayPrivilegeFrequency
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long id { get; set; }
        /// <summary>
        /// 预付返现（返券）活动主键
        /// </summary>
        public int prePayPrivilegeId { get; set; }
        /// <summary>
        /// 返券时间
        /// </summary>
        public DateTime achieveActivistsTime { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public long customerID { get; set; }
    }
}
