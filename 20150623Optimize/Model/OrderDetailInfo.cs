using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VAGastronomistMobileApp.Model
{
    /// <summary>
    /// 点单详情信息
    /// </summary>
    public class OrderDetailInfo
    {
        /// <summary>
        /// 点单详情编号
        /// </summary>
        public int OrderDetailID { get; set; }
        /// <summary>
        /// 点单编号
        /// </summary>
        public int OrderID { get; set; }
        /// <summary>
        /// 菜名编号
        /// </summary>
        public int DishID { get; set; }
        /// <summary>
        /// 菜价格编号
        /// </summary>
        public int DishPriceI18nID { get; set; }
        /// <summary>
        /// 菜价格
        /// </summary>
        public double DishPrice { get; set; }
        /// <summary>
        /// 菜原价
        /// </summary>
        public double DishPriceOriginal { get; set; }
        /// <summary>
        /// 菜价格小计
        /// </summary>
        public double DishPriceSum { get; set; }
        /// <summary>
        /// 菜数量
        /// </summary>
        public double DishQuantity { get; set; }
        /// <summary>
        /// 厨单已打印份数
        /// </summary>
        public double PrintQuantity { get; set; }
        /// <summary>
        /// 台单已打印份数
        /// </summary>
        public double TablePrintQuantity { get; set; }
        /// <summary>
        /// 退菜单已打印份数
        /// </summary>
        public double RetreatPrintQuantity { get; set; }
        /// <summary>
        /// 菜名称
        /// </summary>
        public string DishName { get; set; }
        /// <summary>
        /// 菜规格名称
        /// </summary>
        public string ScaleName { get; set; }
        /// <summary>
        /// 点单详情状态
        /// </summary>
        public int OrderDetailStatus { get; set; }
        /// <summary>
        /// 点单详情备注
        /// </summary>
        public string OrderDetailNote { get; set; }
        /// <summary>
        /// 该菜是否已称重
        /// </summary>
        public bool Weighed { get; set; }
        /// <summary>
        /// 该菜是否需要称重
        /// </summary>
        public bool DishNeedWeigh { get; set; }
        /// <summary>
        /// 点单详情其他备注
        /// </summary>
        public string orderDetailOtherNote { get; set; }//2012-6-28 15:48:40 tdq
        /// <summary>
        /// 点单详情折扣编号
        /// </summary>
        public int discountTypeId { get; set; }//2012-7-17 xiaoyu
    }
}
