using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 菜折扣分类信息
    /// </summary>
    public class DiscountType
    {
        /// <summary>
        /// 菜折扣分类编号
        /// </summary>
        public int DiscountTypeID { get; set; }
        /// <summary>
        /// 菜折扣分类名称
        /// </summary>
        public string DiscountTypeName { get; set; }
        /// <summary>
        /// 菜折扣分类状态
        /// </summary>
        public int DiscountTypeStatus { get; set; }
        /// <summary>
        /// 分类打印机名称
        /// </summary>
        public string PrinterName { get; set; }
        /// <summary>
        /// 分类账单打印机
        /// </summary>
        public string discountTypeOrderPrinter { get; set; }
        /// <summary>
        /// 分类账单打印机份数
        /// </summary>
        public int discountTypeOrderCopy { get; set; }
        /// <summary>
        /// 是否打印分类账单
        /// </summary>
        public bool printdiscountTypeOrder { get; set; }
        /// <summary>
        /// 厨单样式（2每个菜一单、1台单模式）
        /// </summary>
        public int cookOrderStyle { get; set; }
        /// <summary>
        /// 厨单打印份数
        /// </summary>
        public int cookOrderCopy { get; set; }
        /// <summary>
        /// 财务分类
        /// </summary>
        public int financialTypeID { get; set; }
    }
}