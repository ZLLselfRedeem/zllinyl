using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 店铺杂项管理（wangcheng）
    /// </summary>
    public class SundryInfo
    {
        /// <summary>
        /// 店铺ID
        /// </summary>
        public int shopId { get; set; }
        /// <summary>
        /// 店铺杂项名称
        /// </summary>
        public string sundryName { get; set; }
        /// <summary>
        /// 杂项规格
        /// </summary>
        public string sundryStandard { get; set; }
        /// <summary>
        /// 杂项收费模式
        /// </summary>
        public int sundryChargeMode { get; set; }
        /// <summary>
        /// 备注：暂时不用
        /// </summary>
        public bool supportChangeQuantity { get; set; }
        /// <summary>
        /// 杂项单价
        /// </summary>
        public double price { get; set; }
        /// <summary>
        /// 是否开启，枚举，状态
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 杂项描述
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 享受折扣
        /// </summary>
        public bool vipDiscountable { get; set; }
        /// <summary>
        /// 支持返送
        /// </summary>
        public bool backDiscountable { get; set; }
        /// <summary>
        /// 是否必选
        /// </summary>
        public bool required { get; set; }
    }
}
