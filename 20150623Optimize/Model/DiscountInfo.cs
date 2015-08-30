using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary> 
    /// FileName: DiscountInfo.cs 
    /// CLRVersion: 4.0.30319.269 
    /// Author: TDQ 
    /// Corporation:杭州友络科技有限公司
    /// Description: 
    /// DateTime: 2005-09-05 14:50:04 
    /// </summary>
    public class DiscountInfo
    {
        /// <summary>
        /// 折扣ID
        /// </summary>
        public int discountID { get; set; }
        /// <summary>
        /// 折扣名称
        /// </summary>
        public string discountName { get; set; }
        /// <summary>
        /// 折扣值
        /// </summary>
        public double discountValue { get; set; }
        /// <summary>
        /// 折扣状态
        /// </summary>
        public int discountStatus { get; set; }
        /// <summary>
        /// 折扣排序
        /// </summary>
        public int discountSequence { get; set; }
    }
}
