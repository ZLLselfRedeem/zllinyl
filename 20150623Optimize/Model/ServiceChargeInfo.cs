using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary> 
    /// FileName: ServiceChargeInfo.cs 
    /// CLRVersion: 4.0.30319.269 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 
    /// DateTime: 2012-07-31 16:27:51 
    /// </summary>
   public class ServiceChargeInfo
    {
        /// <summary>
        /// 服务费ID
        /// </summary>
        public int serviceChargeId { get; set; }
        /// <summary>
        /// 服务费名称
        /// </summary>
        public string serviceChargeName { get; set; }
        /// <summary>
        /// 服务费值
        /// </summary>
        public double serviceChargeValue { get; set; }
        /// <summary>
        /// 服务费状态
        /// </summary>
        public int serviceChargeStatus { get; set; }
        /// <summary>
        /// 服务费排序
        /// </summary>
        public int serviceChargeSequence { get; set; }
    }
}
